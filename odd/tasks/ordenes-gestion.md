# Feature: ordenes-gestion (budget lines, approval and state changes of a work order)

Locator: `odd/tasks/ordenes-gestion.md` — Engram mirror: `odd/ordenes-gestion/tasks`
Branch: `feature/TM-0006` (branched from `main` at `f90c074`)

## Objective

From the work orders board, the operator opens one work order (OT) and takes it from `RECEPCIONADA` to `ENTREGADA`: loads the budget, registers the client's approval, registers the work done, closes it and delivers the vehicle. Every state change leaves a history row.

## Problem

The board (`FrmOrdenes`) is read-only. No screen writes `ot_detalle`, the header totals, `observaciones_mecanico`, `fecha_finalizacion`, `fecha_entrega`, nor any state after `RECEPCIONADA`, so an OT can be opened but never moved.

## Why

Requested by the user on 2026-10-05 as the next step of the agreed build order. Decisions taken by the user in this conversation:

- "Iniciar trabajo", real quantities / hours and "Finalizar" are included in this screen for `ADMINISTRADOR` and `OPERADOR`, so the whole flow can be tested before the mechanic screen exists. The mechanic screen ("Mis órdenes asignadas") comes later on the same tables.

Assumptions stated to the user before starting and not objected (the user can revert them):

- "Presupuestar" is an explicit button and needs at least one line.
- Description and price are copied from the service; quantity accepts decimals.
- Approval is per line; with no approved line the operator must use "Rechazar".
- The mechanic can be assigned on this screen and is mandatory to start the work.
- "Anular" is admin only, asks for a reason, and is available in every non-final state (up to `FINALIZADA`).
- "Entregar" updates `vehiculo.km_actual`.
- One PR with work-unit commits, as in `odd/tasks/recepcion.md`.

Decisions taken by the agent (not asked; the user can revert them):

- A new form `FrmOrdenGestion`, opened from the board with a "Gestionar orden" button as a modal dialog (`ShowDialog`); the board reloads when it closes.
- The copied price is not editable on the line; the same service can be added once per OT (a second add tells the operator to change the quantity).
- A `PRESUPUESTADA` OT keeps its detail editable (`estado_ot.permite_edicion_detalle`); there is no way back to `RECEPCIONADA`.
- `RECHAZADA` is reachable only from `PRESUPUESTADA`.
- Real quantity and real hours are mandatory on every approved line before "Finalizar"; technical notes are mandatory to finalize.
- No database migration: every column needed already exists.
- No printed budget yet.

## Scope

`FrmOrdenes` (board): a "Gestionar orden" button, enabled with a selected row; opens `FrmOrdenGestion` for that OT and reloads the summary and the grid afterwards.

`FrmOrdenGestion` (new):

- **Header** (read-only): number, state, plate and vehicle, client, reception date, promised date, entry km, reported symptom, reception notes.
- **Mechanic**: combo of active mechanics and "Guardar mecánico", while the state is not final.
- **Budget lines**: grid of `ot_detalle`; service combo (active services), quantity, "Agregar", "Quitar", change quantity. Editable only when the state allows it. Shows budgeted and approved totals.
- **Transitions**, one button per valid step, enabled by the current state:
  - "Presupuestar": `RECEPCIONADA` → `PRESUPUESTADA`.
  - "Registrar aprobación": `PRESUPUESTADA` → `APROBADA`, with the approved flag per line.
  - "Rechazar": `PRESUPUESTADA` → `RECHAZADA`.
  - "Iniciar trabajo": `APROBADA` → `EN_PROCESO`.
  - "Guardar ejecución": real quantity and real hours per approved line, in `EN_PROCESO`.
  - "Finalizar": `EN_PROCESO` → `FINALIZADA`, with technical notes and the finish date.
  - "Entregar": `FINALIZADA` → `ENTREGADA`, with the delivery date and the vehicle km.
  - "Anular": any non-final state → `ANULADA`, admin only, with a reason.
- **History**: read-only grid of `ot_historial_estado` (date, state, user, note).

Out of scope: mechanic screen, diagnosis written by the mechanic, photos uploaded by the mechanic, printed budget, reports, ABMs of services / categories / mechanics, spare parts, payments.

## Rules

- The detail is editable only when `estado_ot.permite_edicion_detalle = 1` (`RECEPCIONADA`, `PRESUPUESTADA`).
- Line subtotal = quantity × unit price. `total_presupuestado` = sum of subtotals; `total_aprobado` = sum of subtotals of approved lines. Both are written in one `UPDATE`, in the same transaction as the detail change (the table has `CHECK total_aprobado <= total_presupuestado`).
- Quantity must be greater than 0.
- Every state change runs in one transaction: re-read the current state of the OT, refuse if it is no longer the expected one, update the OT, insert the `ot_historial_estado` row with `Sesion.IdUsuario`.
- State ids are looked up by `estado_ot.codigo`, never hardcoded.
- Rows are never deleted on annulment or rejection.
- Finish and delivery dates are the current date and time of the server (`NOW()`).
- On delivery, `vehiculo.km_actual` is set to the OT's `km_ingreso` when it is higher than the current value.

## Constraints

- Professor's event-driven style: designer-built forms, `Friend WithEvents` + `Handles`, SQL inside the handlers, `Using cn` / `Using cmd`, `AddWithValue`, validation ending in `Exit Sub`, `BeginTransaction` for multi-step saves, simple `If` blocks, short Spanish comments without accents. No DAO classes, no helper classes, no `AddHandler`, no controls built in code.
- Shared UI look (light gray background, big title with subtitle, white card, flat buttons with a dark primary button, white grid).
- Never stage `WinFormsApp1/ConexionBD.vb` (skip-worktree).
- UI text in Spanish.
- Roles: the form blocks anything other than `ADMINISTRADOR` and `OPERADOR` in `Load`; "Anular" only for `ADMINISTRADOR`.

## TDD

Mode: disabled. Source: no test project or runner exists in the solution. Checks are functional: `dotnet build`, code read-back, then a manual run by the user.

## Tasks

- [ ] **T1 — `FrmOrdenGestion`: header, mechanic, budget lines, "Presupuestar", history; opened from the board** (route: delegated writer; trigger: 2+ non-trivial files)
  - `WinFormsApp1/FrmOrdenGestion.vb`, `.Designer.vb`, `.resx` (new), `WinFormsApp1/FrmOrdenes.vb`, `WinFormsApp1/FrmOrdenes.Designer.vb`.
  - Checks: `dotnet build WinFormsApp1.slnx --no-incremental` with 0 errors; detail and totals transaction read back by the parent.
- [ ] **T2 — Approval, rejection, execution, close, delivery and annulment** (route: delegated writer, after T1)
  - `WinFormsApp1/FrmOrdenGestion.vb`, `.Designer.vb`, `.resx`.
  - Checks: build as above; every transition read back by the parent (state re-read, update, history row, commit / rollback).
- [ ] **T3 — Docs** (route: delegated writer, with T2 or after)
  - `README.md`, `doc/Docu.md`, `taller-mecanico-especificacion.md` (only where the decisions above settle something the specification left open).
  - Checks: structural read-back; `@tsg-docs:auto` markers kept.

## Acceptance criteria

- "Gestionar orden" opens the selected OT for `admin` and `operador`; closing it refreshes the board.
- Lines can be added, changed and removed in `RECEPCIONADA` and `PRESUPUESTADA`, and the totals follow; from `APROBADA` on the detail is frozen.
- "Presupuestar" is refused with no lines.
- Approval with no approved line is refused; "Rechazar" closes the OT as `RECHAZADA`.
- "Iniciar trabajo" is refused without a mechanic.
- "Finalizar" is refused while an approved line lacks real quantity or hours, or the notes are empty.
- "Entregar" sets the delivery date and raises the vehicle km.
- "Anular" is visible only to `admin`, needs a reason, and keeps every row.
- Each transition adds exactly one history row; a failure changes nothing.
- Only the buttons valid for the current state are enabled; a final state enables none.
- Build passes with 0 errors.

## Delivery

Strategy: `single-pr` (stated to the user before starting, precedent in `odd/tasks/recepcion.md`). Forecast: about 900 authored changed lines (`FrmOrdenGestion.vb` about 800, `FrmOrdenes.vb` about 30, docs about 70), over the 400-line budget; designer files are counted as generated. One PR with one work-unit commit per task.

## Progress

| Task | Status | Commit | Review | Evidence |
|---|---|---|---|---|
| T1 | pending | | | |
| T2 | pending | | | |
| T3 | pending | | | |

Reviewed boundary: `f90c074` (branch point).

## Next step

Delegate T1.

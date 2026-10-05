# Feature: recepcion (vehicle reception wizard and work orders board)

Locator: `odd/tasks/recepcion.md` — Engram mirror: `odd/recepcion/tasks`
Branch: `feature/TM-0005` (branched from `feature/TM-0004` at `fdb8199`)

## Objective

The operator receives a vehicle step by step and opens its work order (OT) in state `RECEPCIONADA`, with photos of the vehicle, and anyone at the desk can see in which state every OT is.

## Problem

The core flow of the system does not exist yet: the menu buttons "Recepción" and "Órdenes de trabajo" do nothing, and no screen writes to `orden_trabajo`.

## Why

Requested by the user on 2026-10-02. Decisions taken by the user in this conversation:

- The reception screen loads the data step by step (wizard).
- The state of every OT must be visible.
- Photos of the vehicle angles are loaded at reception, "as it is done for an insurance policy".
- Besides the photos, only one more piece of data is added: the promised delivery date. Inventory of items, structured previous damages and "who delivers the vehicle" were discarded; they go in the existing free-text field `observaciones_recepcion`.
- Photo design approved as proposed: five fixed angles, one photo per angle, optional with a warning, stored in the database, shown in the work orders screen.

Decisions taken by the agent (not asked; the user can revert them):

- The OT list is a separate screen under the existing menu button "Órdenes de trabajo", not part of the wizard.
- A vehicle that is not registered is not created from the wizard (the specification, section 5.1, allows it). The wizard tells the operator to register it in Vehículos first.
- A vehicle with an OT in a non-final state cannot be received again.
- No printed "comanda de taller" yet.

## Scope

Database (13th table, one more than the 12-table model agreed with the chair — the user must tell the professor):

- New table `ot_foto`: `id_ot_foto`, `id_orden_trabajo` (FK, `ON DELETE CASCADE`), `angulo` `ENUM('FRENTE','TRASERA','LATERAL_IZQUIERDO','LATERAL_DERECHO','TABLERO')`, `imagen LONGBLOB`, `id_usuario` (FK), `fecha_hora`. `UNIQUE (id_orden_trabajo, angulo)`.
- New column `orden_trabajo.fecha_prometida DATE NULL`.
- `database/07_recepcion_fotos.sql` migrates an existing database; `database/01_ddl_estructura.sql` gets the same structure for fresh installs.

`FrmRecepcion` (wizard, four steps in one form, one panel per step):

1. **Vehículo** — search by plate; shows vehicle, owner and previous OTs.
2. **Datos de ingreso** — entry km, fuel level, reported symptom, reception notes, mechanic (optional), promised delivery date (optional).
3. **Fotos** — five slots (Frente, Trasera, Lateral izquierdo, Lateral derecho, Tablero), each with "Cargar foto", a preview and "Quitar".
4. **Confirmación** — read-only summary and "Confirmar recepción".

Confirming saves, in one transaction: the OT (next `nro_orden`, state `RECEPCIONADA`, owner at that moment, `Sesion.IdUsuario`), the first row of `ot_historial_estado`, and one `ot_foto` row per loaded photo.

`FrmOrdenes` (work orders board):

- Count of OTs per state.
- Grid of every OT: number, reception date, plate, vehicle, client, mechanic, state, promised date and whether it is late.
- Filter by state and by text (plate or client).
- Photos of the selected OT.

Out of scope: budget lines, approval, state transitions, mechanic view, printing, photos uploaded by the mechanic, webcam capture, creating clients or vehicles from the wizard.

## Rules

- Plate search is exact, on active vehicles only.
- Entry km must be a whole number, not lower than the vehicle's `km_actual` nor than the highest `km_ingreso` of its previous OTs (specification 8.3).
- Reported symptom is mandatory.
- Promised date, when set, cannot be earlier than today.
- Photos: JPG or PNG chosen from disk, reduced to 1280 pixels on the longer side and saved as JPEG. Missing photos do not block; the wizard asks "Faltan las fotos de: … ¿Continuar igual?".
- An OT is late when its promised date is before today and its state is not final (`estado_ot.es_estado_final = 0`).
- `vehiculo.km_actual` is not updated at reception (the specification updates it at delivery).

## Constraints

- Professor's event-driven style: designer-built forms, `Friend WithEvents` + `Handles`, SQL inside the handlers, `Using cn` / `Using cmd`, `AddWithValue`, validation ending in `Exit Sub`, `BeginTransaction` for the multi-step save, simple `If` blocks, short Spanish comments. No DAO classes, no helper classes.
- Shared UI look (light gray background, big title with subtitle, white card, flat buttons with a dark primary button, white grid).
- Forms open inside the content panel of `FrmPrincipal` (`TopLevel = False`, `Dock = Fill`); the main window is at least 1100x700.
- Never stage `WinFormsApp1/ConexionBD.vb` (skip-worktree).
- UI text in Spanish.

## TDD

Mode: disabled. Source: no test project or runner exists in the solution. Checks are functional: `dotnet build`, code read-back, then a manual run by the user. The SQL script cannot be executed by the agent.

## Tasks

- [x] **T1 — Database: `ot_foto` and `fecha_prometida`** (route: delegated writer 1; trigger: 2+ non-trivial files)
  - `database/07_recepcion_fotos.sql` (new), `database/01_ddl_estructura.sql`, `database/04_consultas_verificacion.sql`, `taller-mecanico-especificacion.md`.
  - Checks: script read back; structure identical in 01 and 07.
- [x] **T2 — `FrmRecepcion` wizard and menu button** (route: delegated writer 1)
  - `WinFormsApp1/FrmRecepcion.vb`, `.Designer.vb`, `.resx` (new), `WinFormsApp1/FrmPrincipal.vb`.
  - Checks: `dotnet build` with 0 errors; save transaction read back by the parent.
- [x] **T3 — `FrmOrdenes` board, menu button and docs** (route: delegated writer 2, after T2)
  - `WinFormsApp1/FrmOrdenes.vb`, `.Designer.vb`, `.resx` (new), `WinFormsApp1/FrmPrincipal.vb`, `README.md`, `doc/Docu.md`.
  - Checks: `dotnet build` with 0 errors; queries read back by the parent.

## Acceptance criteria

- "Recepción" and "Órdenes de trabajo" open their screens for `admin` and `operador`.
- An unknown plate, an inactive vehicle and a vehicle with an open OT cannot go past step 1, each with its own message.
- A km lower than the minimum, an empty symptom and a past promised date are rejected in step 2.
- Photos can be loaded, previewed and removed; going back and forward keeps the data.
- Confirming creates the OT with the next number in state `RECEPCIONADA`, its history row and its photos; a failure saves nothing.
- The board shows the new OT, the counts per state, the filters work, late OTs are marked, and the photos of the selected OT are shown.
- Build passes with 0 errors.

## Delivery

Strategy: `ask-on-risk`. Forecast: about 800 authored changed lines (SQL and docs about 120, `FrmRecepcion.vb` about 450, `FrmOrdenes.vb` about 250), over the 400-line budget; designer files are counted as generated. One PR with three work-unit commits, following the precedent of `odd/tasks/usuarios.md`: the user opens and merges one PR per feature on GitHub, and the three commits are reviewable one by one. The user can ask to split it.

## Progress

All three tasks done on 2026-10-02 by two delegated writers, one after the other. The parent read back the migration script and the save transaction, checked the menu wiring, and re-ran a full build after each writer.

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | `c46c2f9` | `ot_foto` is identical in `01_ddl_estructura.sql` and `07_recepcion_fotos.sql` (apart from `IF NOT EXISTS`); `fecha_prometida` sits after `fecha_entrega` in both. Script 07 is safe to run twice by reading (`ADD COLUMN IF NOT EXISTS`, `CREATE TABLE IF NOT EXISTS`). Specification updated: section 5.1, 13 tables, new rules 8.8 (photos) and 8.9 (late orders). |
| T2 | done | `e375487` | `dotnet build WinFormsApp1.slnx --no-incremental`: 0 warnings, 0 errors. No `AddHandler` or code-built control in `FrmRecepcion.vb`. Transaction read back: state id looked up by code, next `nro_orden`, OT insert, history row, one `ot_foto` row per loaded photo, `Commit`; any error rolls back; MariaDB error 1062 has its own message. |
| T3 | done | `a674fef` | Build as above. `FrmOrdenes.vb` has no `INSERT` / `UPDATE` / `DELETE`. The grid query does not select `imagen`; photos are read only when a row is clicked. Docs keep their five `@tsg-docs:auto` markers each. |

Size: 3080 insertions and 24 deletions in 13 files. Authored code: `FrmRecepcion.vb` 776 lines, `FrmOrdenes.vb` 334 lines; the two designer files add 1529 lines.

Decided by the writers beyond the plan:

- Entry km is a `NumericUpDown` (as in `FrmVehiculos`), with a "Mínimo: N km" hint.
- Phone photos are rotated according to their EXIF orientation; PNG transparency is flattened onto white.
- Both forms block roles other than `ADMINISTRADOR` and `OPERADOR` in `Load`.
- The board has a "Solo demoradas" check box; late rows are painted `MistyRose`; an empty photo slot shows "Sin foto".
- "Today" for the late rule is the date of the desk PC, as in the promised-date validation.

Not verified (no app run, no Visual Studio designer, no SQL executed):

- Script 07 and the changed script 01 against MariaDB 12.
- Both forms in the Visual Studio designer and at runtime: layout inside the content panel, text widths, grid row counts.
- Every acceptance criterion, including photo loading, EXIF rotation, the BLOB insert against the server's `max_allowed_packet`, and reading the photos back in the board.

Known gaps, accepted for now:

- The open-OT check and the km minimum are evaluated when the vehicle is searched, not again inside the save transaction; two desks receiving the same vehicle at the same moment could both succeed.
- `FrmPrincipal.AbrirFormulario` clears the content panel without disposing the previous form (it already did), so the bitmaps of an abandoned wizard are released only by the garbage collector.
- A vehicle whose km exceeds 9,999,999 makes the search fail with a generic message.
- `taller-mecanico.dbml` still describes 12 tables.
- Specification section 5.1 still says an unknown vehicle is created on the spot; the wizard sends the operator to Vehículos.
- The forms do not use `Anchor`, so on a maximized window they keep their designed size.

Layout redesign on 2026-10-05 (branch `feature/TM-0007`), asked by the user after using the wizard: step bar with clickable completed steps, a permanent summary card, a fixed footer (Cancelar, Anterior, Siguiente / Confirmar recepción), photo tiles, Lucide icons, and anchors so the form follows the window. Data, validations, photo processing and the save transaction are unchanged (the diff shows no SQL or transaction line touched). Build 0 warnings, 0 errors, re-run by the parent. Not seen on screen by the agent.

## Next step

The user runs `database/07_recepcion_fotos.sql` on the local database, opens both forms in the Visual Studio designer and tests the acceptance criteria as `operador`; then decides push / PR for `feature/TM-0005`. The user must tell the professor about the 13th table. Next feature: budget lines and state changes in the work orders screen.

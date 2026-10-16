# Feature: historial (work order history by license plate)

Locator: `odd/tasks/historial.md` — Engram mirror: `odd/historial/tasks`
Branch: `feature/TM-0012` (from `main` at `9fc84b6`)

## Objective

Any logged-in user opens "Historial", finds a vehicle by its license plate, and sees the vehicle, its current owner, every work order it ever had and the services of the selected order.

## Problem

The "Historial" menu button is visible to the three roles but has no Click handler: it does nothing. The specification (`taller-mecanico-especificacion.md` sections 9 and 10) lists "Consulta de historial por patente" for every role.

## Why

Requested by the user on 2026-10-16: "same design as the other screens".

Decisions taken by the agent (the user can revert them):

- Layout mirrors `FrmClientes` / `FrmMisOrdenes`: a white card at the left with a search box and the vehicle list; at the right, the selected vehicle's data, its orders grid and the services of the selected order.
- The vehicle list includes vehicles with a logical delete (`activo = 0`), marked as "Baja": their history still matters.
- Every order is listed, any state, most recent first, including annulled and rejected ones.
- Amounts (order total, price, subtotal) are shown to `ADMINISTRADOR` and `OPERADOR` only. For `MECANICO` they are never selected from the database, the same rule as "Mis órdenes".
- The mechanic sees the history of every vehicle, not only orders assigned to them: the specification says "Todos" for this query and it is read-only.
- Read-only screen: no button changes data. Reception photos stay out of this first version.

## Scope

- `FrmHistorial` (new): designer form, code-behind, resx.
- `FrmPrincipal`: `btnHistorial_Click` opens it embedded like the other screens.
- Docs: README / `doc/Docu.md` inside the `@tsg-docs:auto` markers.

Out of scope: photos, printing, database changes, the management reports.

## Rules

- No write statement anywhere in `FrmHistorial`.
- Every query is parameterized.
- For the role `MECANICO`, no query selects `total_presupuestado`, `total_aprobado`, `precio_unitario` or `subtotal`, and no amount column exists in the grids.

## Constraints

Professor's event-driven style: controls declared in the designer with `Friend WithEvents`, handlers with `Handles`, SQL in the handlers, no `AddHandler`, no controls built in code, no data-access classes. Look and spacing of `FrmClientes` (spacing system in `odd/tasks/abm-rediseno.md`). Spanish UI text. Never stage `ConexionBD.vb`.

## TDD

No test project exists and the screen is read-only UI over SQL; there is no meaningful runnable RED. Checks: clean build, off-screen render of the real form against the local database as `ADMINISTRADOR` and as `MECANICO`, and a read of every query for the amount rule.

## Tasks

- [x] **T1 — `FrmHistorial` and the menu handler** (route: delegated writer; trigger: 2+ non-trivial files)
- [x] **T2 — Docs** (route: delegated writer, with T1)

Writer decisions beyond the list above (the user can revert them): selecting a vehicle selects its most recent order, so its services are always visible; "Total" is `total_aprobado` when it is above 0, otherwise `total_presupuestado` (a rejected or not yet approved order shows its budget); the three grids are not sortable by column, so the chosen row never changes by itself; the search box types in upper case; a vehicle with a logical delete also shows a "Dado de baja" label next to its plate.

## Acceptance criteria

- Clicking "Historial" opens the screen embedded, for the three roles.
- Typing part of a plate filters the vehicle list; selecting a vehicle shows its data and its orders; selecting an order shows its services and the mechanic's notes.
- A vehicle without orders shows an empty-state message.
- As `MECANICO`, no amount is visible anywhere.
- Build: 0 warnings, 0 errors.

## Delivery

Strategy `ask-on-risk`. Forecast well under 400 authored lines excluding the designer file.

## Progress

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | 93d7726 | `dotnet build WinFormsApp1.slnx --no-incremental`: 0 warnings, 0 errors. Off-screen render of the real form against the local database (read paths only), plate `AB123CD` (1 order, 3 services): as `ADMINISTRADOR` at 870 x 630 and 1690 x 940 the orders grid has a "Total" column and the services grid a "Subtotal" column; as `MECANICO` the bound columns are `id_orden_trabajo, nro_orden, fecha_recepcion, codigo_estado, estado, km_ingreso, mecanico, sintoma_reportado, observaciones_mecanico` and `descripcion, cantidad, aprobado, horas_reales` (no amount). Empty states rendered: no vehicle selected (help text) and plate `AC654DE` without orders (notice). Grep of `FrmHistorial.vb`: no INSERT/UPDATE/DELETE; `total_`, `precio_unitario` and `subtotal` appear only in the non-mechanic branches. Not observed: the menu click in the running app (the handler compiles); no vehicle with a logical delete exists locally, so the gray row and "Dado de baja" label were not rendered. |
| T2 | done | 93d7726 | README (diagram and "Estado", inside the `@tsg-docs:auto` markers) and `doc/Docu.md` (diagram inside the markers; screen description next to "Mis órdenes", menu note and roadmap, which sit outside the markers) updated; structural readback only. |

Native review of `9fc84b6..93d7726`: medium, granted, one reliability lens, approved and acknowledged (`review-27b11037e1f36200`). Non-blocking notes left open: a failed order load is shown as the "no orders" notice after the error message; `km_actual` read with `CInt` (not a defect: the column is `NOT NULL DEFAULT 0`); the menu click and the inactive-vehicle row are not observed at runtime.

## Next step

User: click "Historial" in the running app for each role, then open the PR.

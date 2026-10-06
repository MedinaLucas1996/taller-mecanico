# Feature: mis-ordenes (the mechanic's assigned work orders)

Locator: `odd/tasks/mis-ordenes.md` — Engram mirror: `odd/mis-ordenes/tasks`
Branch: `feature/TM-0011` (from `main` at `0ac446b`)

## Objective

A mechanic logs in, sees only the work orders assigned to them, starts the work, registers the hours of each service, writes their notes and finishes the job.

## Problem

A user with the role `MECANICO` sees only "Historial" in the menu, and that button does nothing. Starting, registering hours and finishing can only be done by the administrator or the operator in `FrmOrdenGestion`.

## Why

Requested by the user on 2026-10-06, mockup approved. Decisions taken by the user:

- A screen of its own for the mechanic: three groups at the top that also filter (En curso, Para empezar, Terminadas), the list at the left and the order's working data at the right, with one primary button.
- The mechanic can: start the work, register the hours of each service, write notes and save progress, finish the work, see the reception photos.
- The mechanic cannot: see prices or amounts, touch the budget, register the client's approval or rejection, deliver the vehicle, annul, change the assigned mechanic.
- The primary button opens the same order management dialog the operator uses, in a mechanic mode, so the rules live in one place.
- Left out for now (both need database changes): a diagnosis written by the mechanic before the budget, and photos uploaded by the mechanic.

Decisions taken by the agent (the user can revert them):

- The menu button "Mis órdenes" is shown only to the role `MECANICO`; administrators and operators have no linked mechanic and already see every order on the board.
- "Terminadas" lists the mechanic's orders in `FINALIZADA` and `ENTREGADA`, most recent first, read-only.
- Orders assigned to the mechanic that are still `RECEPCIONADA` or `PRESUPUESTADA` are not listed: there is nothing for the mechanic to do on them yet.
- Ownership is enforced in the database transaction, not only by hiding buttons: every write the mechanic can trigger re-reads the order under lock and refuses unless `id_mecanico` is the logged-in mechanic.
- A mechanic user whose linked mechanic is missing (`Sesion.IdMecanico = 0`) gets a message and an empty screen.

## Scope

- `FrmOrdenGestion`: a mechanic mode.
- `FrmMisOrdenes` (new): the mechanic's list and working summary.
- `FrmPrincipal`: the menu button for the role `MECANICO`.
- Docs and the role matrix in the specification.

Out of scope: diagnosis field, mechanic photos, Historial, Reportes, any database change.

## Rules

- The mechanic only ever sees and changes orders whose `id_mecanico` is `Sesion.IdMecanico`.
- Start: `APROBADA` → `EN_PROCESO`. Save progress and finish: only in `EN_PROCESO`, with the existing completeness rules (hours on every approved line, notes mandatory to finish).
- No price, subtotal or total is shown to the mechanic anywhere: not in the list, not in the summary, not in the dialog.
- Every state change keeps writing one history row with the logged-in user.
- For `ADMINISTRADOR` and `OPERADOR` the dialog and the board behave exactly as today.

## Constraints

Professor's event-driven style (designer-declared controls, `Friend WithEvents` + `Handles`, SQL in the handlers, no `AddHandler`, no controls built in code, no classes for data access). Same look as the redesigned board and master data screens. Controls that start hidden never rely on docking order. UI text in Spanish. Never stage `WinFormsApp1/ConexionBD.vb`.

## TDD

Mode: disabled (no test project). Checks: `dotnet build WinFormsApp1.slnx --no-incremental`, code read-back, off-screen renders of read paths, manual run by the user as `mecanico`.

## Tasks

- [x] **T1 — Mechanic mode in `FrmOrdenGestion`** (route: delegated writer; trigger: 2+ non-trivial files)
- [x] **T2 — `FrmMisOrdenes` and the menu button** (route: delegated writer, after T1)
- [x] **T3 — Docs and role matrix** (route: delegated writer, with T2)

## Acceptance criteria

- As `mecanico`, the menu shows "Mis órdenes"; the screen lists only that mechanic's orders, grouped and filterable by the three groups.
- The summary shows vehicle, km, what the client reported, reception notes, the services to do with their hours, and the reception photos (enlargeable, not changeable).
- "Iniciar trabajo" moves an `APROBADA` order to `EN_PROCESO`; hours and notes can be saved; "Finalizar trabajo" needs hours on every service and the notes.
- No amount of money is visible to the mechanic.
- An order of another mechanic cannot be opened or changed, even if its id is passed to the dialog.
- As `admin` and `operador`, nothing changes.
- Build passes with 0 errors.

## Delivery

Strategy: `single-pr`, opened only when the user asks, one work-unit commit per task.

## Progress

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | see git log | Build 0 warnings, 0 errors, re-run by the parent. 12 `BeginTransaction` and 12 `Commit`. Rendered as mechanic and as operator for the same `EN_PROCESO` order; the parent viewed the mechanic render: no amount visible. |
| T2 | done | see git log | Rendered as two mechanics at 870 x 630 and 1690 x 940; the parent viewed one. Every order query carries the mechanic filter. Menu rendered for the three roles. |
| T3 | done | see git log | README, doc/Docu.md and specification sections 4 and 9. |

Not verified: every write (start, save progress, finish) and its refusals; the group switch after returning from the dialog; "Para empezar" with data and the `APROBADA` view (the local database has no approved order); thumbnails with real photos; another mechanic's order being refused (read in code only).

Native review of `0ac446b..f00cf7f`: medium, granted, approved and acknowledged (`review-e8ca8f307d56eda5`). Non-blocking notes left open: a failed load is shown as an empty state (no services, counts at 0, "Sin foto"); a failed reload after a group change leaves the previous rows under the newly highlighted card; the "Horas" column sorts as text.

Verified by the user at runtime on 2026-10-06 ("funciona todo"); the agent did not observe it.

## Next step

Nothing pending for this feature. Not built, by the user's decision: a diagnosis written by the mechanic before the budget, and photos uploaded by the mechanic (both need database changes).

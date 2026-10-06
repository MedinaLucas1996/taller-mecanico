# Feature: abm-rediseno (list and record layout for the master data screens)

Locator: `odd/tasks/abm-rediseno.md` — Engram mirror: `odd/abm-rediseno/tasks`
Branch: `feature/TM-0010` (from `main` at `3a4def0`)

## Objective

The seven master data screens (Clientes, Vehículos, Marcas y modelos, Servicios, Categorías, Mecánicos, Usuarios) share one layout: the list at the left, the record at the right, using the whole window, with only the actions that apply.

## Problem

These screens come from the first stage of the project. The form sits in a fixed block at the top left and half the window is empty; four buttons are always visible whether they apply or not; the only sign of "new" versus "editing" is whether the ID box has a number; the internal ID is shown in the form and in the grid, and its read-only box takes the focus.

## Why

Requested by the user on 2026-10-05, mockup approved. Decisions taken by the user:

- List at the left, record at the right, as in the work orders board.
- The internal ID is not shown anywhere and nothing unusable takes the focus.
- Deactivated records, when listed, are visibly different: gray text in the list, a "Dado de baja" label in the record, and the button reads "Reactivar".
- It has to end up tidy and orderly.

Decisions taken by the agent (the user can revert them):

- Clientes is built first as the model screen; the user tests it and sends a screenshot; the other six replicate it only after the user is satisfied.
- The "Activo" Sí / No column added with the reactivation is removed; gray text and the record label replace it. Deactivated rows are listed after the active ones.
- Buttons by context: editing shows "Guardar cambios" and "Dar de baja" (or "Reactivar"); a new record shows "Guardar" and "Cancelar". "Modificar" and "Limpiar" disappear as separate buttons; a "Nuevo …" button at the top starts a new record.
- The record card shows a line of context where it is cheap to compute (for a client: number of vehicles and the date it was registered).

## Scope

Layout and interaction of the seven screens. Rules, validations, messages, queries and transactions stay as they are (including the reactivation rules of `569921f`).

Out of scope: new fields, new rules, the mechanic screen, Historial, Reportes.

## Layout rules (the tidy part)

- One spacing grid for the seven screens: same outer margin, same gap between cards, same card padding, same row pitch, same label style (small muted label above its input), same input and button heights.
- Fields aligned through a designer-declared `TableLayoutPanel`, not by hand-placed coordinates, so columns line up and follow the card width.
- The list stretches with the window; the record card keeps a fixed width at the right and stretches vertically.
- Tab order follows the reading order of the record; labels, the status label and read-only texts do not take the focus.
- Must stay usable at the minimum window (content panel about 870 x 630).

## Constraints

Professor's event-driven style (designer-declared controls, `Friend WithEvents` + `Handles`, SQL in the handlers, no `AddHandler`, no controls built in code, no classes for data access). Menu-ordering lesson: controls that start hidden must not rely on docking order; use explicit rows. UI text in Spanish. Never stage `WinFormsApp1/ConexionBD.vb`.

## TDD

Mode: disabled (no test project). Checks: `dotnet build WinFormsApp1.slnx --no-incremental`, code read-back, manual run and screenshot by the user.

## Tasks

- [x] **T1 — Clientes as the model screen** (route: delegated writer; trigger: 2+ non-trivial files)
- [x] **T2 — User validation of the model** (the user tests and sends a screenshot; adjustments until approved)
- [x] **T3 — Vehículos and Usuarios** (route: delegated writer, after T2)
- [x] **T4 — Mecánicos, Servicios and Categorías** (route: delegated writer, after T2)
- [x] **T5 — Marcas y modelos** (route: delegated writer, after T2; two related lists, needs its own arrangement of the same model)

## Acceptance criteria

- No ID visible in any of the seven screens; Tab never lands on something that cannot be edited.
- With nothing selected the record card invites to pick or create; selecting a row loads it and the title names it; "Nuevo …" clears it and the title says so.
- Only the buttons that apply are visible in each mode.
- Deactivated rows are gray and listed last; the record shows "Dado de baja" and offers "Reactivar".
- Every rule and message of the current screens still applies.
- The seven screens look and behave alike.
- Build passes with 0 errors.

## Delivery

Strategy: `single-pr`, opened only when the user asks, with one work-unit commit per task. Forecast: well over the 400-line budget (seven designer files rewritten); the user reviews each batch at runtime.

## Progress

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | same commit as this document | Build 0 warnings, 0 errors, re-run by the parent. Rendered off-screen from the real form at 870 x 630, 954 x 696 and 1690 x 940 in edit, deactivated and new modes; the parent viewed the images. Fields, buttons and card edges coincide at the three sizes. Saves, bajas and dialogs were not exercised. |
| T2 | done | `08bfb3e`, `91dc157` | The user tested Clientes at runtime on 2026-10-06 and approved it ("funciona todo"), after the review fixes and after the parent fixed `FrmPrincipal.CerrarPantallaActual`, which only looked at the welcome label and never closed the open screen. Native review of `a249e48..08bfb3e`: medium, granted, approved and acknowledged (`review-319e7873d5538127`). |
| T3 | done | see git log | Build 0 warnings, 0 errors, re-run by the parent. Rendered off-screen at 870 x 630 and 1690 x 940; the parent viewed Vehículos. Tab order and guarded sort observed in the harness. |
| T4 | done | see git log | As T3; the parent viewed Servicios. No deactivated mechanic, service or category exists locally, so the gray rows and "Reactivar" were not rendered for these screens. |
| T5 | done | see git log | Two cards, Marcas and "Modelos de …", each with its list and one edit box; capabilities unchanged (physical delete, no search, no logical delete). The parent viewed the render. |

Spacing system fixed by T1 (designer units; this machine renders x 7/8, y 3/4): outer margin 30; cards start at y 110; gap between cards 10; card padding 16; record card width 380; header button 200 x 40; grid header 40 high, white, DimGray bold 8.25, one bottom line; grid rows 26, light horizontal lines only; record title 12 pt bold on its own line, status label under it; field table from y 78 with columns 40 % / 60 %; field label 8.25 pt DimGray above its input; the multi-line field takes the remaining height (minimum 64); action buttons 40 high, anchored 16 from the bottom.

Review of `a249e48`: the native review could not run (bound STATUS timed out twice on this lineage; occurrence reported upstream with the user's consent). An independent read-only reviewer read the screen instead: save logic and the three modes correct; fixed in the next commit — leaving the screen lost unsaved edits (now `FrmPrincipal.CerrarPantallaActual` closes the embedded form and `FrmClientes` cancels in `FormClosing`), Tab trapped in the grid (`StandardTab`), current cell not restored on "No", header sort switching the open client (sort is now programmatic and guarded), `cargando` not exception-safe, and three minor items. What the other six screens must copy: grid settings (`StandardTab`, single full-row selection, read-only, the light look), tab indexes ("Nuevo" 0, list 1, record 2), the form fields `id…`, `creando`, `registroActivo`, `hayCambios`, `cargando`, the routines `Cargar…`, `MarcarFila`, `RefrescarRegistro`, `MostrarAyuda`, `CargarCampos`, `MostrarRegistro`, `NuevoRegistro`, `DescartarCambios`, `ElegirFila`, `ValidarCampos`, and the handlers `Load`, `Shown`, `FormClosing`, filter, check box, `DataBindingComplete`, `ColumnHeaderMouseClick`, `Sorted`, `SelectionChanged`, `CellClick`, field changes, new, cancel, save, baja.

Review of the six screens (`cc38db0`, `c5934ef`, `ba114ad`): the native review refused the range at start with `lens_context_budget_exceeded` (7424 lines). Two independent read-only reviewers read the six `.vb` files in full against their previous versions and the Clientes model: no confirmed defect, no rule lost, no wrong-target write, no accidental divergence from the model. To settle at runtime: opening the first record in Vehículos or Servicios may flag it as edited by itself (a combo created after the loading guard is released); in Marcas y modelos a typed new brand name is dropped if the list takes keyboard focus (Shift+Tab). Minor, inherited: a vehicle with no year shows and saves the current year; a whitespace-only password is accepted; two administrators demoting each other from two stations can leave no administrator.

Verified by the user at runtime on 2026-10-06 ("funciona todo") for the six screens; the agent did not observe it.

## Next step

Nothing pending for this feature. Open follow-ups, not requested: a whitespace-only password is accepted; a vehicle with no year shows and saves the current year.

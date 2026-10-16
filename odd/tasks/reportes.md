# Feature: reportes (printed reports)

Locator: `odd/tasks/reportes.md` — Engram mirror: `odd/reportes/tasks`
Branch: `feature/TM-0013` (from `main` at `451da36`)

## Objective

Four printed reports with preview, built the way the professor taught in Biblioteca2026 class 07: Orden de trabajo (any state), Presupuesto, Acta de recepción (with the five photos) and Ficha del cliente.

## Problem

The professor requires at least three reports. The specification lists the presupuesto as the printing requirement of the assignment. Nothing in the app prints today.

## Why

Requested by the user on 2026-10-16. Decisions taken by the user:

- Report technology: the professor's approach, designer `PrintDocument` + `PrintPreviewDialog`, page drawn by hand in the `PrintPage` handler with `e.Graphics`. No report library.
- Four reports: Ficha del cliente (client, vehicles, orders summary, no photos), Orden de trabajo printable in any state showing what the order has so far, Presupuesto, Acta de recepción with the five reception photos and signatures.
- Reports are styled (fonts, header band, boxes, right-aligned amounts, zebra rows, page footer) and must stay readable in black and white.
- Each report has a print button that opens the preview; the preview's own printer button prints.

Decisions taken by the agent (the user can revert them):

- The three order reports live in `FrmOrdenGestion` (the order dialog the board and "Mis órdenes" both open): one "Imprimir" button with a designer `ContextMenuStrip` (Orden de trabajo, Presupuesto, Acta de recepción). One place, no duplicated queries, and the mechanic reaches it through "Ver orden".
- For the role `MECANICO` only "Orden de trabajo" is offered, and it is drawn without any amount; its queries never select amounts.
- "Presupuesto" is enabled only when the order has detail lines and is not annulled.
- An annulled order prints a light "ANULADA" watermark and the reason from `ot_historial_estado.observacion`; a rejected one prints its reason too.
- There is no workshop identity in the database; the header shows "Taller Mecánico" as a constant in one shared place so it can be changed later.
- Shared drawing helpers (header band, section box, label/value rows, table, footer "Hoja N", watermark) go in a new `Module Impresion`, like the existing `Fotos` module. Queries stay in the forms.
- The Ficha del cliente is a button "Imprimir ficha" in `FrmClientes` for the selected saved client, and paginates with `e.HasMorePages`.

## Scope

- `Impresion.vb` (new module).
- `FrmOrdenGestion`: button, menu, three `PrintDocument`s, one `PrintPreviewDialog`.
- `FrmClientes`: button, one `PrintDocument`, one `PrintPreviewDialog`.
- Docs.

Out of scope: management charts (ScottPlot), workshop identity table, printing straight to a printer without preview, database changes.

## Rules

- Every query is parameterized; no write statement is added.
- For `MECANICO`, no query selects `total_presupuestado`, `total_aprobado`, `precio_unitario` or `subtotal`.
- Fonts, brushes, pens and images created for a page are disposed.
- Images keep their aspect ratio.
- Multi-page reports reset their row cursor in `BeginPrint`, so the preview and the real print both start at page 1.

## Constraints

Professor's event-driven style: designer-declared components (`Friend WithEvents`), `Handles`, SQL in the form, no `AddHandler`, no controls built in code, no data-access classes. Same look as the other screens for the buttons. Spanish UI and report text. Never stage `ConexionBD.vb`.

## TDD

No test project; the output is drawing on a page. No meaningful runnable RED. Checks per task: clean build, and an off-screen render of each report page (the `PrintDocument` drawn into a bitmap through a preview controller) against the local database, viewed by the parent, for an order in several states and as `MECANICO`.

## Tasks

- [x] **T1 — `Impresion` module + Orden de trabajo report in `FrmOrdenGestion`** (route: delegated writer; trigger: 2+ non-trivial files)
- [ ] **T2 — Presupuesto and Acta de recepción** (route: delegated writer, continues T1)
- [ ] **T3 — Ficha del cliente in `FrmClientes`** (route: delegated writer)
- [ ] **T4 — Docs** (route: delegated writer, with T3)

## Acceptance criteria

- Each report opens a maximized preview from its button and prints from it.
- The OT report prints for each of the eight states with the sections that apply.
- As `MECANICO`, only the OT report is offered and no amount appears.
- The acta shows the five photos (or "Sin foto") keeping proportions.
- A client with many orders spans several pages with "Hoja N" in the footer.
- Build: 0 warnings, 0 errors.

## Delivery

Strategy `single-pr`, following the user's working pattern (one `feature/TM-00NN` branch per batch, the user tests and merges on GitHub). Forecast: about 1200 authored lines plus designer changes, above the 400 heuristic; kept as one PR with one commit per task.

## Progress

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | | `dotnet build WinFormsApp1.slnx --no-incremental`: 0 warnings, 0 errors. Off-screen render through `PreviewPrintController` against the local DB (local states: EN_PROCESO, FINALIZADA, ENTREGADA only): EN_PROCESO as ADMINISTRADOR and as MECANICO (no amount columns, no totals), FINALIZADA, ENTREGADA (2 sheets, history continues with "(continuación)" and "Hoja 2"). ANULADA (watermark + reason), RECHAZADA (reason, Aprobado = No, no work section) and RECEPCIONADA with 41 lines (3 sheets, totals after the table) simulated in memory only, no DB writes. Grep: no INSERT/UPDATE/DELETE added; the mechanic SELECT texts have no `total_`, `precio_unitario` or `subtotal`. Menu: Presupuesto and Acta items exist hidden until T2; the preview opens with `ShowDialog(Me)` because the order form is itself modal. |
| T2 | pending | | |
| T3 | pending | | |
| T4 | pending | | |

## Next step

T1.

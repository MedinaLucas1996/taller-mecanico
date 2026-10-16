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
- [x] **T2 — Presupuesto and Acta de recepción** (route: delegated writer, continues T1)
- [x] **T3 — Ficha del cliente in `FrmClientes`** (route: delegated writer)
- [x] **T4 — Docs** (route: delegated writer, with T3)

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
| T1 | done | 40c26da | `dotnet build WinFormsApp1.slnx --no-incremental`: 0 warnings, 0 errors. Off-screen render through `PreviewPrintController` against the local DB (local states: EN_PROCESO, FINALIZADA, ENTREGADA only): EN_PROCESO as ADMINISTRADOR and as MECANICO (no amount columns, no totals), FINALIZADA, ENTREGADA (2 sheets, history continues with "(continuación)" and "Hoja 2"). ANULADA (watermark + reason), RECHAZADA (reason, Aprobado = No, no work section) and RECEPCIONADA with 41 lines (3 sheets, totals after the table) simulated in memory only, no DB writes. Grep: no INSERT/UPDATE/DELETE added; the mechanic SELECT texts have no `total_`, `precio_unitario` or `subtotal`. Menu: Presupuesto and Acta items exist hidden until T2; the preview opens with `ShowDialog(Me)` because the order form is itself modal. |
| T2 | done | 8aa6847 | `dotnet build WinFormsApp1.slnx --no-incremental`: 0 warnings, 0 errors. Off-screen render through `PreviewPrintController`: Presupuesto for all five local orders with lines (EN_PROCESO, FINALIZADA, ENTREGADA; Aprobado column + Total aprobado shown since all are past APROBADA; validity note with emission = last PRESUPUESTADA history date + 15 days; signature lines Firma/Aclaración/Fecha). Acta: the local DB has no `ot_foto` rows, so the real acta renders five "Sin foto" boxes; a second render injects generated 16:9, 9:16 and 1:1 bitmaps plus one unreadable photo in memory (no DB writes) and shows them fitted without distortion, "Sin foto" and "Foto ilegible"; "Recibió" shows the user of the RECEPCIONADA history row. Menu as MECANICO: only "Orden de trabajo" visible; as ADMINISTRADOR all three, Presupuesto enabled only with lines and not ANULADA. Review fixes: R3-preview-dialog-not-disposed (preview dialog, the three PrintDocuments and the photos are disposed in FormClosed); R3-print-exception-unguarded (every PrintPage goes through a Try/Catch that sets HasMorePages = False and shows one message; verified with a forced DBNull km: one message, errorImpresion = True, no crash). Grep: the only added SQL is `SELECT angulo, imagen FROM ot_foto`; no INSERT/UPDATE/DELETE; mechanic SELECTs still without amounts. |
| T3 | done | 40f29bf | `dotnet build WinFormsApp1.slnx --no-incremental`: 0 warnings, 0 errors. "Imprimir ficha" (white secondary button, header left of "Nuevo cliente", anchored Top/Right; `lblTitulo` narrowed 520→420 so they never overlap): disabled with no client, enabled after `MostrarRegistro`, rendered at 870x630 and 1600x900. Off-screen render as ADMINISTRADOR through `PreviewPrintController`: client 9 (3 vehicles, 2 orders, one vehicle without orders) 1 sheet; client 2 (no orders) 1 sheet; client 9 with 70 extra orders and one vehicle marked as no longer owned, in memory only: 3 sheets, table continues with "(continuación)", "Vehículos que ya no son del cliente" group and summary on sheet 3. The local DB has no sold vehicles. PrintPage guarded with Try/Catch like T2; preview and document disposed in `FormClosed`; preview opened with `ShowDialog(Me.TopLevelControl)` because Clientes is embedded in `FrmPrincipal`. FrmClientes is in the menu only for ADMINISTRADOR and OPERADOR (`FrmPrincipal_Load`), so the ficha shows amounts. No change to `FrmOrdenGestion` or `Impresion` (the engine loop is small and stays per form); OT re-rendered as ADMINISTRADOR and MECANICO, unchanged. Grep: only SELECTs added. |
| T4 | done | 935e6d0 | README: Clientes bullet for "Imprimir ficha", Gestión "Al pie" mentions "Imprimir", new "Reportes impresos" section (outside the auto blocks, with the screen descriptions) with the four reports, button locations and the mechanic rule; inside the auto blocks: `Module Impresion` in the diagram and the Estado lines. doc/Docu.md: technical "Reportes impresos" bullet under Cómo funciona, Clientes sub-bullet, diagram node, "Decisión cerrada: motor de reportes", roadmap items 3 and 4, permissions note. Spec 11.1: note that the project uses PrintDocument + PrintPreviewDialog as taught in class. Markers checked: two auto-start/auto-end pairs per file, unchanged. |

Native reviews: `451da36..40c26da` medium, granted, approved and acknowledged (`review-63a68092c386f46b`); its two warnings (preview dialog not disposed, print exceptions unguarded) were fixed in T2. `40c26da..935e6d0` medium, granted, approved and acknowledged (`review-5c03da70f7e29a12`); notes left open: a NULL photo or km would abort the load (not reachable: `ot_foto.imagen` and `vehiculo.km_actual` are `NOT NULL`), and the Presupuesto menu item is gated on the lines loaded in the grid.

Not verified at runtime by the user yet: the four buttons in the running app, real photos in the acta (the local database has none), printing on a real printer.

## Next step

User: test the four reports in the running app, then push and open the PR.

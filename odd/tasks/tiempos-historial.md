# Feature: tiempos-historial (stage times report and printed vehicle history)

Objective: close the specification's report list: report 7 "Tiempos por etapa" and the printed "Historial por patente". Branch `feature/TM-0018` from `main` at a27e923. Delivery strategy: single-pr. Test runner: none; checks are a clean `dotnet build` and the off-screen render harness (read paths only; print pages through a `PreviewPrintController`).

Style (mandatory): the professor's event-driven VB.NET: designer-built controls (`.Designer.vb`, `Friend WithEvents`, handlers with `Handles`), SQL in the form with `Using cn As New MySqlConnection(CADENA)` / `Using cmd` and `AddWithValue`, reports as a designer `PrintDocument` + `PrintPreviewDialog` shown maximized with `ShowDialog` and the page drawn by hand with the `Module Impresion` helpers, short lowercase Spanish comments without accents, `Sesion.ROL_*` for roles, `LeerIcono` for icons. No DAO, no `AddHandler`, no controls created in code, no new packages. UI texts in neutral Spanish. Keep each file's existing line endings (CRLF in this working copy).

## Specs

- S1 Tiempos por etapa, in `FrmTableros` (administrador only, as the screen already is).
  - A stage is a row of `ot_historial_estado`; consecutive rows of the same order with the same `id_estado_ot` collapse into one stage started by the first of them (for example the "Cliente avisado" rows). Its duration runs until the next row of that order with a different `id_estado_ot`. Stages with no such next row (the current stage of an open order) are excluded. Only non-final states count (`es_estado_final = 0`: Recepcionada, Presupuestada, Aprobada, En proceso, Finalizada), ordered by `orden_flujo`. A stage belongs to the selected period when it starts inside it, with the same `@desde` / `@hasta` as the other charts.
  - New chart "Duración promedio por etapa" (renamed after review): horizontal bars in flow order, bar length in days on an axis labelled "días", bar label with the same unit text as the grid, colored with `ColorEstado`; "Sin datos" through `MostrarSinDatos` when empty.
  - New read-only grid "Detalle por etapa": columns Etapa, Órdenes, Promedio, Mínimo, Máximo; a duration under one hour shows as "N min", under one day as "N h", otherwise "N,N días" (parent change after the first render: real stages last minutes and "0 h" said nothing).
  - Layout: `tlpGraficos` becomes 3 columns x 2 rows at 33 % each: row 1 Facturación, Servicios, Días por etapa; row 2 Horas, Estados, Detalle por etapa. Nothing may overlap at 870x630 nor maximized.
  - New button "Imprimir tiempos" in the filters panel, same style as "Actualizar", opening the preview of a one-page sheet: header "Tiempos por etapa", the period, the stage table, one horizontal bar per stage drawn proportional to the largest average, and the note "Cada etapa se mide desde que la orden entra al estado hasta que pasa al siguiente. No se cuentan las etapas en curso."; page foot with "Hoja 1" and the date.
  - One query feeds the chart, the grid and the sheet. Read only.
- S2 Historial por patente impreso, in `FrmHistorial` (all roles, as the screen already is).
  - New header button "Imprimir" left of "Exportar", same style; enabled only while a vehicle is selected, exactly like "Exportar". The title label is narrowed so nothing overlaps at 870x630.
  - Sheet "Historial del vehículo", read fresh from the database when printing (not from the grids): vehicle block (patente, marca y modelo, año, color, kilometraje actual, and "Dado de baja" when inactive); current owner (razón social, documento, teléfono); table of all its orders in any state, most recent first: N.º, Recepción, Estado, Km ingreso, Mecánico, Entrega, Total aprobado; then one block per order with the reported symptom, its approved services (descripción, cantidad, horas trabajadas) and the mechanic's notes when present. A vehicle without orders prints its data and "Sin órdenes de trabajo registradas.".
  - The mechanic prints the same sheet without any amount (no "Total aprobado" column).
  - Several pages when needed, following the client sheet pattern of `FrmClientes` (tables continue on the next page with "(continuación)"), page foot "Hoja N" and the date.

## Units and edit surfaces

- U-A (writer A): S1. `WinFormsApp1/FrmTableros.vb`, `WinFormsApp1/FrmTableros.Designer.vb`, `WinFormsApp1/FrmTableros.resx`.
- U-B (writer B): S2. `WinFormsApp1/FrmHistorial.vb`, `WinFormsApp1/FrmHistorial.Designer.vb`, `WinFormsApp1/FrmHistorial.resx`.
- `WinFormsApp1/Impresion.vb` is shared and stays untouched: a missing helper goes as a private routine inside the writer's own form. Parent: README.md, doc/Docu.md, this document, git.

## Tasks

- [x] T1 S1 — delegated writer A — commit ca49a2c (parent changed the short duration format to minutes)
- [x] T2 S2 — delegated writer B — commit bc24782 (writer rebuilt FrmHistorial.vb after a line-ending accident; parent diffed it against HEAD: only two comments widened and the new lines added, CRLF, no BOM, same as before)
- [x] T3 — parent — README/Docu in the closing docs commit

## Log

- L1 (2026-10-22) User: "Arrancá TM-0018: reporte de tiempos por etapa e historial por patente impreso, con tus recomendaciones."
- Recommendations applied (from the previous answer): collapse consecutive rows of the same state; exclude the stage in progress; chart plus printable sheet in Tableros; printed history reusing the client sheet pattern, without amounts for the mechanic; no database change.
- Route: two writers in parallel, independent behaviours with disjoint files, each well over 100 lines (Writer trigger (a)). Risk: none expected (read-only reports, new controls, no shared helper changed).
- Verification (2026-10-22): both writers stayed inside their files; all edited files CRLF. Combined `dotnet build` to a scratch OutDir: 0 warnings, 0 errors. Writer A render (read paths only): Tableros at 870x630 and 1690x940 with the 3x2 grid, sheet printed through a PreviewPrintController for the data period and an empty one; existing chart titles and ticks adjusted to avoid overlap. Parent harness after the format change: Recepcionada 6 | 36 min | 1 min | 2 h; Presupuestada 6 | 0 min | 0 min | 1 min; Aprobada 6 | 0 min | 0 min | 1 min; En proceso 4 | 1 min | 0 min | 4 min; Finalizada 4 | 3,5 días | 0 min | 13,9 días. Writer B: sheet for AB123CD as administrador and mecánico (1 page, no total column for the mechanic), vehicle without orders (1 page), in-memory stress copy of 34 orders (11 pages per role, tables continue with "(continuación)"). Not exercised: the real preview dialog (ShowDialog).
- Known, not changed: the Facturado card shows only "$" at 870x630 when the amount has seven digits (card width vs font, pre-existing); the pie chart draws smaller after maximize then restore (existing SquareZoomOut rule).
- Risk: none (read-only reports and new controls; no shared helper changed; Impresion.vb untouched).
- Review (2026-10-22, lineage review-358eaef94a33d010, lenses readability/reliability, consent granted): APPROVED and acknowledged. All six findings fixed in bc5afaa: NULL total_aprobado in the printed history (WARNING, would stop the sheet); chart/sheet unit mismatch (WARNING) by renaming to "Duración promedio por etapa" with unit-aware labels and a "días" axis; rounding before choosing the unit; shared TextosEtapa routine; PARTES_POR_ORDEN constant. Build 0/0; harness grid: Recepcionada 6 | 36 min | 1 min | 2 h ... Finalizada 4 | 3,5 días | 0 min | 13,9 días.
- Follow-up review (2026-10-22, lineage review-1f52bad333fe7872, lens reliability, consent granted): APPROVED and acknowledged. Its only suggestion (unit boundaries unproved) checked in the harness: 0 -> 0 min, 59.4 -> 59 min, 59.6 -> 1 h, 89 -> 1 h, 1409 -> 23 h, 1430 -> 1,0 días, 1440 -> 1,0 días, 5019.5 -> 3,5 días.

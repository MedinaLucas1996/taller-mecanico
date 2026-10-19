# Feature: tableros (management dashboard)

Locator: `odd/tasks/tableros.md` — Engram mirror: `odd/tableros/tasks`
Branch: `feature/TM-0014` (from `main` at `3570261`)

## Objective

The admin-only menu button "Reportes" becomes "Tableros" and opens a dashboard with indicators and charts about the workshop, built with ScottPlot as the professor showed in Biblioteca2026 class 08 (`FormTablero`).

## Problem

The "Reportes" button is visible to the administrator but has no Click handler. The specification's management reports (sections 9 and 10: most requested services, mechanic productivity, open orders) have no screen.

## Why

Requested by the user on 2026-10-16/19. Decisions taken by the user:

- Rename the menu button to "Tableros"; it stays administrator-only.
- One screen with a period filter (desde / hasta, default: current month).
- Six indicators: órdenes ingresadas (in the period), vehículos en el taller ahora (orders not ENTREGADA / RECHAZADA / ANULADA), facturado (sum of orders delivered in the period), ticket promedio (facturado / delivered count), aprobación de presupuestos (% of budgets the client approved, in the period), demoradas (orders past `fecha_prometida` and not FINALIZADA / ENTREGADA / RECHAZADA / ANULADA).
- Four charts in a 2 x 2 grid: facturación por mes (bars, always the last 6 months), servicios más pedidos (horizontal bars, top 8, in the period), horas por mecánico estimadas vs reales (grouped bars, in the period), órdenes por estado (pie, in the period, using the app's state colors).
- A demo data SQL script (about 40 orders across several months) for the presentation.
- Tiempo por etapa (spec report 7) is left for later.
- The user allowed a temporary database on their local MariaDB to verify the script and render the charts, dropped afterwards; the workshop database is not touched.

Decisions taken by the agent (the user can revert them):

- Dates: "ingresadas" by `fecha_recepcion`, "facturado" / "ticket" by `fecha_entrega` of ENTREGADA orders using `total_aprobado`, "aprobación" = orders whose history reached APROBADA divided by orders whose history reached PRESUPUESTADA, both with the PRESUPUESTADA event in the period.
- "Horas estimadas" = `servicio.tiempo_estimado_horas * ot_detalle.cantidad` of approved lines; "horas reales" = `ot_detalle.horas_reales`; for orders finished (`fecha_finalizacion`) in the period.
- Demo data goes in `database/08_datos_demo.sql`, idempotent where practical, inserting through the same tables and rules the app uses (history rows for every state change, totals consistent with lines, CHECK constraints respected).

## Scope

- `FrmPrincipal`: button text "Tableros" and `btnReportes_Click` (or renamed `btnTableros`) opening the new screen embedded.
- `FrmTableros` (new): designer form, code-behind, resx, four `ScottPlot.WinForms.FormsPlot` controls.
- `WinFormsApp1.vbproj`: package `ScottPlot.WinForms` (same version as the professor: 5.1.59).
- `database/08_datos_demo.sql` (new).
- Docs.

Out of scope: tiempo por etapa, printing the dashboard, exporting.

## Rules

- Read-only screen: no write statement.
- Every query is parameterized (period dates as parameters).
- Administrator only, also checked in `Load` like other admin screens.

## Constraints

Professor's event-driven style: controls declared in the designer (`Friend WithEvents`), `Handles`, SQL in the form, no `AddHandler`, no controls built in code, no data-access classes; ScottPlot usage like `FormTablero.vb` (lists → arrays, `Plot.Clear`, `Add.Bars` / `Add.Pie`, `Refresh`). Look of the other screens (title + subtitle, white cards, KPI tiles like the board's state cards). Spanish UI text. Never stage `ConexionBD.vb`.

## TDD

No test project; no meaningful runnable RED. Checks: clean build; the demo script loads cleanly into a temporary database together with scripts 01–07 and its integrity queries pass; off-screen render of the dashboard against that temporary database for two periods; the temporary database is dropped.

## Tasks

- [x] **T1 — Demo data script `database/08_datos_demo.sql`** (route: delegated writer; trigger: SQL design + verification)
- [x] **T2 — `FrmTableros`, ScottPlot package, menu rename and handler** (route: delegated writer; trigger: 2+ non-trivial files)
- [x] **T3 — Docs** (route: delegated writer, with T2)

## Acceptance criteria

- The admin sees "Tableros" in the menu and it opens the dashboard; other roles do not see it.
- Changing the period recalculates indicators and the three period charts; the monthly chart always shows the last 6 months.
- Empty period: indicators at 0 and charts with a "Sin datos" message, no exception.
- Build: 0 warnings, 0 errors.

## Delivery

Strategy `single-pr`, the user's pattern. Forecast about 700 authored lines plus designer and SQL.

## Progress

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | | `database/08_datos_demo.sql` (40 orders, 56 lines, 199 history rows; 8 clients, 12 vehicles, mechanic Diego Martínez added if missing; dates relative to CURDATE; re-runnable via `[demo]` marker). Loaded 01, 02, 03, 06, 07, 08 into temp `taller_verif_tmp` with no errors, 08 run twice (same counts). Integrity queries all 0: totals vs lines, approved <= budgeted, subtotal = qty*price, history present/first RECEPCIONADA at fecha_recepcion/last = current state/ordered/not in future, fecha_finalizacion and fecha_entrega = history rows, mechanic iff APROBADA reached, finished approved lines have hours. States: ENTREGADA 25, FINALIZADA 2, EN_PROCESO 3, APROBADA 1, PRESUPUESTADA 2, RECEPCIONADA 1, RECHAZADA 3, ANULADA 3; 4 late. Received per month Apr..Oct (run 2026-10-19): 5,4,4,5,4,7,11. |
| T2 | done | | `FrmTableros` (+Designer, resx), `btnReportes` renamed `btnTableros` with handler, `ScottPlot.WinForms` 5.1.59. Build: 0 errors, 6 warnings = 3 distinct NU1701 (OpenTK, OpenTK.GLControl, SkiaSharp.Views.WindowsForms, transitive from ScottPlot; not suppressed, decision pending). Grep FrmTableros.vb: no INSERT/UPDATE/DELETE; period queries use @desde/@hasta (chart 1 @inicio/@fin, demoradas @hoy). Off-screen render against `taller_verif_tmp` (project copy in scratchpad with patched ConexionBD copy): current month, 6 months, empty period at 870x630 and 1690x940, 0 error dialogs; KPIs month = 11 / 9 / $ 152.800 / $ 50.933 / 80 % / 4. Temp DB dropped. |
| T3 | done | | README (Tableros section, menu table, run table row 08, diagram, Estado) and doc/Docu.md (screen detail, menu, diagram, stack, decisions, edge cases, roadmap). |

## Next step

Parent commits T1, T2, T3; decide on the NU1701 warnings.

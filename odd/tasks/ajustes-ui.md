# Feature: ajustes-ui

## Objective
Polish two UI defects found while testing as admin.

## Problem
1. In the ABM screens (Clientes, Vehículos, Usuarios, Mecánicos, Servicios, Categorías), toggling "mostrar dados de baja" (`chkBajas`) makes the grid visibly shift/reflow even though the data is correct.
2. In the dashboard (`FrmTableros`), the "Órdenes por estado" pie chart is very small and its size changes with the window/screen resolution.

## Scope / constraints
- VB.NET WinForms, professor's event-driven style: SQL in forms, designer-built forms, `Handles`, Spanish code comments in the existing tone.
- Fix the root cause once and apply the same fix to every ABM screen with `chkBajas`.
- Verification through the off-screen render harness (read paths only, never touch windows outside its own process).

## Tasks
- [x] T1 Diagnose and fix the grid shift on `chkBajas` toggle in all ABM screens (route: delegated writer — 6 files)
- [x] T2 Make the pie chart fill its cell consistently at any size (route: delegated writer, same worker)
- [x] T3 Build + render before/after evidence

## Acceptance criteria
- Toggling `chkBajas` keeps column layout and grid position stable.
- Pie chart occupies most of its cell, legend still readable, proportional at 870x630 and maximized.

## Progress
- Branch: feature/TM-0014 (dashboard not merged yet).
- T1, T2, T3 done.

## Verification evidence
- Root cause T1: FillWeight set before MinimumWidth let the grid rewrite weights on first binding; and Load ran before the form was docked (grid 340 wide vs 449). Fix: minimums first then weights; first load moved to Shown. Measured widths identical across off/on/off in all six screens, standalone and hosted.
- T2: SetLimits(-1.08..1.08) + SquareZoomOut rule; pie fills cell height at 870x630 and 1600x900.
- Build: 0 warnings, 0 errors. Render harness, read paths only.
- Commits: 5253036 (T1), 449b8a7 (T2).
- Open: a toggle after manually resizing the window can still re-spread columns slightly; not fixed (would require keeping columns across reloads).

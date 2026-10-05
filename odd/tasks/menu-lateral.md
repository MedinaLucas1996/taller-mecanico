# Feature: menu-lateral (side menu order, icons and collapse)

Locator: `odd/tasks/menu-lateral.md` — Engram mirror: `odd/menu-lateral/tasks`
Branch: `feature/TM-0006` (already pushed; this work adds commits on top)

## Objective

The side menu of `FrmPrincipal` shows what is used most first, carries an icon per option, and can be collapsed to an icon-only column.

## Problem

The menu is stacked upside down: "Reportes" is at the top, "Recepción" at the bottom, and each section title sits below its buttons. It has no icons and a fixed width.

## Why

Requested by the user on 2026-10-05. Decisions taken by the user:

- Daily operations first; master data (configuration, ABMs) at the bottom of the menu.
- Icons from Lucide on the side menu.
- The menu can be collapsed, leaving only the icons.
- The user authorized downloading `lucide-static` and `@resvg/resvg-js` from npm to a temporary folder to convert the icons.

Decisions taken by the agent (the user can revert them):

- Order, top to bottom: Operaciones (Recepción, Órdenes de trabajo, Historial), Reportes, then Datos maestros at the bottom (Clientes, Vehículos, Marcas y modelos, Servicios, Categorías, Mecánicos, Usuarios).
- Collapsed menu shows the option name as a tooltip; section titles are hidden.
- Icons are 20 px white PNG files in `WinFormsApp1/Recursos/iconos/`, copied to the output folder and loaded at runtime, following the precedent of the login background. The Lucide licence text (ISC) is kept next to them.
- The empty `panelContenido_Paint` handler left by the Visual Studio designer is removed, as proposed to the user.

## Scope

`WinFormsApp1/FrmPrincipal.vb`, `WinFormsApp1/FrmPrincipal.Designer.vb`, `WinFormsApp1/WinFormsApp1.vbproj`, `WinFormsApp1/Recursos/iconos/*`, `README.md`, `doc/Docu.md`.

Out of scope: screens for Historial, Servicios, Categorías and Reportes (their buttons still do nothing); remembering the collapsed state between sessions.

## Constraints

Professor's event-driven style (designer-declared controls, `Friend WithEvents` + `Handles`, no `AddHandler`, no controls built in code). Role visibility of each button stays exactly as it is. UI text in Spanish. Never stage `WinFormsApp1/ConexionBD.vb`.

## TDD

Mode: disabled (no test project). Checks: `dotnet build WinFormsApp1.slnx --no-incremental`, code read-back, manual run by the user.

## Tasks

- [x] **T1 — Icons** (route: inline, parent): 14 PNG files rendered from `lucide-static` 1.52.0 with `@resvg/resvg-js` 2.6.2, plus `LICENCIA-lucide.txt`.
- [x] **T2 — Menu order, icons, collapse** (route: delegated writer; trigger: 2+ non-trivial files)
  - Checks: build with 0 errors; role visibility read back by the parent; icons present in the output folder.

## Acceptance criteria

- As `admin`, the menu reads from top to bottom: Operaciones, Reportes, Datos maestros, each title above its buttons.
- As `operador` and `mecanico`, only their buttons are visible and no gap is left between them.
- Every button shows its icon; the collapse button narrows the menu to icons only and expands it back; the content area takes the freed width.
- Collapsed, hovering a button shows its name.
- A missing icon file does not stop the application.
- Build passes with 0 errors.

## Progress

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | with T2 | 14 files, 276 to 557 bytes each |
| T2 | done | same commit as this document | `dotnet build WinFormsApp1.slnx --no-incremental`: 0 warnings, 0 errors, re-run by the parent; 14 PNG files in the output folder; role blocks read back (ADMINISTRADOR all eleven, OPERADOR six, MECANICO only Historial). Menu 230 wide expanded, 56 collapsed; buttons 40 high so the full admin menu fits the 700 px minimum window. Not seen on screen nor opened in the designer. |

Decided by the writer beyond the plan: no separator lines when collapsed; a collapsed button without icon shows its initial; logo text 14 pt left-aligned next to the toggle; "Cerrar sesión" got its icon; a missing icon gives no message.

Verified by the user at runtime on 2026-10-05 ("funciona todo"); the agent did not observe it. Native review of `ef200e7`: medium, granted, approved and acknowledged (`review-3dbd78d788230328`); open non-blocking notes: menu widths 230 and 56 are fixed pixels (may clip under display scaling) and the icon bitmaps are not disposed.

## Next step

Nothing pending for this feature.

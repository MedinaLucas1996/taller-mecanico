# Feature: menu-por-rol (role-based menu in FrmPrincipal)

Locator: `odd/tasks/menu-por-rol.md` — Engram mirror: `odd/menu-por-rol/tasks`
Branch: `feature/TM-0004` (branched from `feature/TM-0003` at `61c5930`)

## Objective

Each role sees only its own options in the main menu, and `FrmPrincipal` is built in the designer with `Handles`, like the professor's `frmPrincipal`.

## Problem

- Only the "Usuarios" button is restricted. A `MECANICO` still sees Clientes, Vehículos and Marcas y modelos.
- `FrmPrincipal` creates every control in code (`New Button`, `AddHandler`) inside the constructor. The role rule decides whether a button is created, so applying the full matrix would add more code-built UI.

## Why

Requested by the user on 2026-10-02, after pulling the professor's class 06 (`Biblioteca2026` commit `9f075cf`, "Login y hash de claves"). The professor's pattern is: the buttons exist in the designer and `frmPrincipal_Load` shows them by role (`If Sesion.rolUsuario = "ADMIN" Then btnUsuarios.Visible = True`). The user approved doing the designer migration and the role rule together, in two commits.

## Scope

- `FrmPrincipal.Designer.vb` holds every control: three panels, logo, section titles, one named button per menu option, user label, logout button, welcome labels.
- `FrmPrincipal.vb` keeps only event handlers (`Handles`), `AbrirFormulario`, the logout logic and the `Load` handler.
- `FrmPrincipal_Load` shows the menu options by role.
- Docs: role matrix in `README.md` and `doc/Docu.md`.

Out of scope: `FrmLogin` to the designer, renaming `Seguridad` / `Sesion` members, PBKDF2 iteration count, per-button permissions inside each form, new screens (the buttons without a screen keep doing nothing).

## Role matrix

| Menu option | ADMINISTRADOR | OPERADOR | MECANICO |
|---|---|---|---|
| Recepción | yes | yes | no |
| Órdenes de trabajo | yes | yes | no |
| Historial | yes | yes | yes |
| Clientes | yes | yes | no |
| Vehículos | yes | yes | no |
| Marcas y modelos | yes | yes | no |
| Servicios | yes | no | no |
| Categorías | yes | no | no |
| Mecánicos | yes | no | no |
| Usuarios | yes | no | no |
| Reportes | yes | no | no |

Section titles: "OPERACIONES" for every role, "DATOS MAESTROS" for ADMINISTRADOR and OPERADOR, "REPORTES" for ADMINISTRADOR only.

## Constraints

- Professor's event-driven style: designer-built form, `Friend WithEvents` + `Handles`, simple `If` blocks, short Spanish comments. No helper classes, no dictionaries of permissions.
- Same look as today: colors, sizes, fonts and menu order must not change.
- Menu options are hidden by default in the designer and shown by role in `Load` (an unknown role sees no restricted option).
- Never stage `WinFormsApp1/ConexionBD.vb` (skip-worktree, holds a local password).
- UI text stays in Spanish, as in the rest of the project.

## TDD

Mode: disabled. Source: no test project or runner exists in the solution. Checks are functional: `dotnet build`, then a manual run by the user.

## Tasks

- [x] **T1 — Move `FrmPrincipal` to the designer, no behavior change** (route: delegated writer; trigger: 2 non-trivial files)
  - `WinFormsApp1/FrmPrincipal.Designer.vb`, `WinFormsApp1/FrmPrincipal.vb`.
  - "Usuarios" stays admin-only; every other option stays visible, as today.
  - Checks: `dotnet build` with 0 errors; no `New Button` / `New Label` / `New Panel` / `AddHandler` left in `FrmPrincipal.vb`.
- [x] **T2 — Role matrix in `FrmPrincipal_Load`, with docs** (route: delegated writer, same writer)
  - `WinFormsApp1/FrmPrincipal.vb`, `WinFormsApp1/FrmPrincipal.Designer.vb` (default `Visible = False`), `README.md`, `doc/Docu.md`.
  - Checks: `dotnet build` with 0 errors; `Load` read back against the matrix above.

## Acceptance criteria

- `admin` sees every menu option.
- `operador` sees Recepción, Órdenes de trabajo, Historial, Clientes, Vehículos and Marcas y modelos, with no gaps in the menu.
- `mecanico` sees only Historial under "OPERACIONES"; the other two section titles are hidden.
- The top bar shows the logged user and role.
- "Cerrar sesión" asks for confirmation, returns to an empty login, and the next login shows the menu of the new user.
- Closing the main window ends the application.
- The form opens in the Visual Studio designer.
- Build passes with 0 errors.

## Delivery

Strategy: `ask-on-risk`. Forecast: about 350 authored changed lines (`FrmPrincipal.vb` loses about 250 lines and gains about 80, plus docs). `FrmPrincipal.Designer.vb` is counted as a generated file: Visual Studio rewrites it when the form is opened. One PR, two work-unit commits.

## Progress

Both tasks done on 2026-10-02 by one delegated writer. The parent read back `FrmPrincipal.vb`, checked the `Controls.Add` order and the default `Visible = False` values in the designer file, and re-ran a full build.

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | `7060bc8` | `dotnet build WinFormsApp1.slnx --no-incremental`: 0 warnings, 0 errors. No `New Button` / `New Label` / `New Panel` / `AddHandler` left in `FrmPrincipal.vb` (404 lines down to 107). `panelMenu.Controls.Add` runs from `btnReportes` to `panelLogo`, the reverse of the visual order. |
| T2 | done | `b92e9d5` | Build as above. `Load` read back: three `If Sesion.Rol = ...` blocks that match the role matrix; every menu button and section title is `Visible = False` in the designer, so an unknown role sees no option. Docs keep their five `@tsg-docs:auto` markers each. |

Size: 548 insertions and 364 deletions in total; `FrmPrincipal.Designer.vb` accounts for 450 of the insertions.

Not verified (no app run, no Visual Studio designer):

- The form opens in the Visual Studio designer. Positions and sizes of docked controls were written by hand; Visual Studio will rewrite them when the form is saved, so a designer diff is expected.
- The look and the menu order at runtime, and every per-role acceptance criterion.
- "Cerrar sesión", now wired with `Handles`.
- Display scaling above 100%: controls are now created inside `InitializeComponent`, so WinForms auto-scaling applies to them, which it did not when they were added after it.

Known gap, out of scope: only the menu is filtered. Screens other than `FrmUsuarios` do not re-check the role when they open.

Changed on purpose: menu buttons now have `TabIndex` values from top to bottom (all were 0), and handlers were renamed from `BtnX_Click` to `btnX_Click`.

## Next step

The user opens `WinFormsApp1.slnx`, checks `FrmPrincipal` in the designer, and logs in as `admin`, `operador` and `mecanico` to test the acceptance criteria; then decides push / PR for `feature/TM-0004`.

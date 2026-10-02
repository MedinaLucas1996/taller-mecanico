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

Added on 2026-10-02 at the user's request, once the menu was tested: `FrmLogin` is also built in the designer (task T3).

Out of scope: renaming `Seguridad` / `Sesion` members, PBKDF2 iteration count, per-button permissions inside each form, new screens (the buttons without a screen keep doing nothing).

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
- [x] **T3 — Move `FrmLogin` to the designer, no behavior change** (route: inline; one designer file plus a mechanical deletion in `FrmLogin.vb`)
  - `WinFormsApp1/FrmLogin.Designer.vb`, `WinFormsApp1/FrmLogin.vb`.
  - The background image, the music and the validation against the database are not touched.
  - Checks: `dotnet build` with 0 errors; no `New Label` / `New TextBox` / `New Button` / `AddHandler` left in `FrmLogin.vb`.

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

| T3 | done | `c72440f` | Build as above. `FrmLogin.vb` went from 243 to 162 lines with no code-built control left; seven controls declared `Friend WithEvents`; `btnIngresar_Click` uses `Handles btnIngresar.Click`. The form size changed from an outer 500x500 to the equivalent `ClientSize` of 484x461. |

Size: T1 and T2 add up to 548 insertions and 364 deletions (`FrmPrincipal.Designer.vb` is 450 of the insertions); T3 adds 95 insertions and 86 deletions.

Review: the user declined the review of T1 and T2 alone. After T3, the review of the whole branch (base `61c5930`, 7 files, risk `medium`) was granted, approved by the reliability lens and acknowledged on 2026-10-02. It left three non-blocking notes: the login and the role matrix had no recorded runtime check (both tested by the user, see below), and an unknown role gets an empty menu with no message (accepted: `usuario.rol` is an `ENUM` with the three exact values).

Verified by the user at runtime on 2026-10-02:

- Menu of `admin`, `operador` and `mecanico`, and "Cerrar sesión".
- Login: looks and behaves as before the migration.

Not verified:

- Display scaling above 100%: controls are now created inside `InitializeComponent`, so WinForms auto-scaling applies to them, which it did not when they were added after it.

Note: positions and sizes in both designer files were written by hand; Visual Studio rewrites them when a form is saved, so a designer diff is expected the first time.

Known gap, out of scope: only the menu is filtered. Screens other than `FrmUsuarios` do not re-check the role when they open.

Changed on purpose: menu buttons now have `TabIndex` values from top to bottom (all were 0), and handlers were renamed from `BtnX_Click` to `btnX_Click`.

## Next step

Branch `feature/TM-0004` pushed on 2026-10-02 at the user's request; the user opens and merges the PR on GitHub. Next feature in the roadmap: Recepción.

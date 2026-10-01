# Feature: usuarios (ABM de Usuarios)

Locator: `odd/tasks/usuarios.md` — Engram mirror: `odd/usuarios/tasks`
Branch: `feature/TM-0003` (branched from `main` at `55cbd45`)

## Objective

A users screen where the administrator creates, modifies and deactivates the users of the system.

## Problem

The only users are the three test rows loaded by the SQL scripts. Passwords are stored hashed, so nobody can create a user or set a new password without a screen that calls `Seguridad`.

## Why

Requested by the user on 2026-10-01, right after the login was merged (`odd/tasks/login.md`). Decisions taken by the user:

- Full ABM (create, modify, logical delete), not create-only.
- Only a user with role `ADMINISTRADOR` can open the screen.
- Fields asked for: user name, password, and a combo box to choose the role.

## Scope

- New form `FrmUsuarios`, built in the designer format, same look and pattern as `FrmClientes`.
- Fields: user name, full name, password, role combo, mechanic combo.
  - Full name is added because `usuario.nombre_completo` is `NOT NULL` and it is what the top bar shows.
  - Mechanic combo is added because the table CHECK `val_usuario_rol_mecanico` requires `id_mecanico` if and only if the role is `MECANICO`. It is enabled only for that role.
- Passwords saved with `Seguridad.GenerarSalt` + `Seguridad.HashearClave`. On modify, an empty password keeps the current one.
- Logical delete (`activo = 0`), like Clientes. The grid lists active users only.
- Menu button "Usuarios" in `FrmPrincipal` opens the screen and exists only for the administrator.

Out of scope: role-based permissions for the other screens, ABM Mecánicos, password change by the user themselves, reactivating a deactivated user, password strength rules.

## Constraints

- Professor's event-driven style: designer-built form with `WithEvents` + `Handles`, SQL inside the handlers, `Using cn` / `Using cmd`, `AddWithValue`, validation ending in `Exit Sub`, short Spanish comments. No DAO classes.
- Shared UI look (light gray background, big title with subtitle, white card with fields and buttons, flat buttons with a dark "Guardar", white grid).
- Never stage `WinFormsApp1/ConexionBD.vb` (skip-worktree).
- Do not touch `FrmPrincipal.Designer.vb` / `FrmPrincipal.resx` (user's uncommitted designer changes).
- Never show, log or load a password hash or salt into the form controls or the grid.

## TDD

Mode: disabled. Source: no test project or runner exists in the solution. Checks are functional: `dotnet build`, then a manual run by the user.

## Tasks

- [x] **T1 — `FrmUsuarios` form** (route: delegated writer; trigger: 2+ non-trivial files, reading that prepares the write)
  - `WinFormsApp1/FrmUsuarios.vb`, `FrmUsuarios.Designer.vb`, `FrmUsuarios.resx`.
  - Checks: `dotnet build` with 0 errors; code read back by the parent.
- [x] **T2 — Menu wiring and admin-only access** (route: delegated writer, same writer)
  - `WinFormsApp1/FrmPrincipal.vb`: the "Usuarios" button opens `FrmUsuarios` and is only added when `Sesion.Rol = "ADMINISTRADOR"`.
  - Checks: `dotnet build`.
- [x] **T3 — Docs sync** (route: delegated writer, same writer)
  - `README.md`, `doc/Docu.md`: Usuarios screen done, admin-only, what remains.

## Acceptance criteria

- The administrator sees the "Usuarios" button; `operador` and `mecanico` do not.
- Creating a user with user name, full name, password and role works, and that user can log in.
- Role `MECANICO` requires choosing a mechanic; the other roles save `id_mecanico` as NULL.
- A repeated user name shows a friendly message (MariaDB error 1062).
- Modifying with an empty password keeps the old password; with a password, the new one works at login.
- Deactivating a user removes it from the grid and that user can no longer log in.
- The logged administrator cannot deactivate themselves nor change their own role.
- Build passes with 0 errors.

## Delivery

Strategy: `ask-on-risk`. Forecast: ~450–550 authored changed lines, most of it designer layout code in `FrmUsuarios.Designer.vb`. One PR: the form is a single coherent unit and the designer file cannot be split meaningfully.

## Progress

All three tasks done on 2026-10-01 by one delegated writer. The parent read back `FrmUsuarios.vb` and the `FrmPrincipal.vb` diff, and re-ran a full (non-incremental) build.

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | `771e00e` | `dotnet build --no-incremental`: 0 warnings, 0 errors. Code read back: no SELECT reads `hash_contrasena` or `salt`; `id_mecanico` is NULL unless the role is `MECANICO`; self-deactivation and own-role change are blocked. The designer layout was written by hand and NOT opened in the Visual Studio designer. |
| T2 | done | `6dbd753` | Build as above. Diff read back: the button is only added for `ADMINISTRADOR`. |
| T3 | done | docs commit after `6dbd753` | Markers intact; no password values printed. |

Not verified (no app run, no database access):

- The form opens and looks right in the Visual Studio designer and at runtime (control positions and sizes were estimated).
- Every acceptance criterion at runtime.

Known gap, accepted for now: when the logged administrator renames themselves, `Sesion` is updated but the top bar label in `FrmPrincipal` keeps the old text until the next login.

## Next step

The user opens the solution, checks the form in the designer, and tests the acceptance criteria as `admin`; then decides push / PR for `feature/TM-0003`.

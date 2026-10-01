# Feature: login

Locator: `odd/tasks/login.md` — Engram mirror: `odd/login/tasks`
Branch: `feature/login` (branched from `main` at `be0fdd8`)

## Objective

Make the login real: validate user and password against the `usuario` table and keep the logged user available to the rest of the app.

## Problem

`FrmLogin` opens `FrmPrincipal` without validating anything, and `FrmPrincipal` shows a hardcoded "Usuario: Administrador". Later screens need the logged user (`orden_trabajo.id_usuario_alta`, `ot_historial_estado.id_usuario`).

## Why

The user asked for the modules the login needs. Hashing decision taken by the user on 2026-10-01: **PBKDF2 like the professor** (replaces the BCrypt note in the DDL and spec).

## Scope

- Password scheme identical to the professor's reference (`D:\PROGRAMACION - BBDD\Biblioteca2026\database\Biblioteca_MariaDB_Completa_Seed.sql`): PBKDF2 + SHA256, 100000 iterations, 32-byte hash, 16-byte salt, both Base64, salt in its own column.
- New modules `Seguridad` and `Sesion`, same style as `ConexionBD`.
- `FrmLogin` validates credentials; `FrmPrincipal` shows the logged user.

Out of scope: role-based permissions per screen, ABM Usuarios, password change, rewriting `FrmLogin`/`FrmPrincipal` with the designer.

## Constraints

- Professor's event-driven style: SQL inside the form event handler, `Using cn` / `Using cmd`, `AddWithValue`, validation ending in `Exit Sub`, short Spanish comments. No DAO classes.
- Keep the teammate's login background and music (PR #1) working.
- `WinFormsApp1/ConexionBD.vb` is `skip-worktree` locally: never stage it.
- `FrmPrincipal.Designer.vb` and `FrmPrincipal.resx` hold the user's uncommitted designer changes: do not touch or stage them.
- Never print seed passwords in docs.

## TDD

Mode: disabled. Source: no test project or runner exists in the solution (`openspec/config.yaml`, repo scan). Checks are functional: `dotnet build` and the PBKDF2 vector check below.

## Tasks

- [x] **T1 — Database: salt column and PBKDF2 seeds** (route: delegated writer; trigger: 2+ non-trivial files)
  - `database/01_ddl_estructura.sql`: add `salt VARCHAR(100) NOT NULL` after `hash_contrasena`; fix the column comment (no longer BCrypt).
  - `taller-mecanico.dbml`: same column.
  - `database/02_dml_catalogos.sql`, `database/03_dml_prueba.sql`: seed users with PBKDF2 hash + salt.
  - New `database/06_migracion_hash_pbkdf2.sql`: `ALTER TABLE` + `UPDATE` for databases that are already loaded.
  - Checks: SQL read back; hashes match the generated values.
- [x] **T2 — Modules `Seguridad` and `Sesion`** (route: delegated writer)
  - `WinFormsApp1/Seguridad.vb`: `GenerarSalt`, `HashearClave`, `VerificarClave`.
  - `WinFormsApp1/Sesion.vb`: logged user data (`IdUsuario`, `NombreUsuario`, `NombreCompleto`, `Rol`, `IdMecanico`) and `CerrarSesion`.
  - Checks: `dotnet build`; vector check against the professor's seed (`admin123` + salt `LlZHaHCvXUbSKOX2rxBBdA==` → `RxvV6GUtSRUe8Ei1PO8QneNVuJ9855Ik6vN7bojhnWk=`).
- [x] **T3 — Login validation and logged user in the main form** (route: delegated writer)
  - `WinFormsApp1/FrmLogin.vb`: validate empty fields, query `usuario` (active only), verify the hash, fill `Sesion`, open `FrmPrincipal`. Same generic message for unknown user and wrong password.
  - `WinFormsApp1/FrmPrincipal.vb`: top bar label shows the logged user and role.
  - Checks: `dotnet build`. Manual run by the user (needs the local DB migrated with script 06).
- [x] **T4 — Docs sync** (route: inline; one mechanical pass over two docs)
  - `README.md`, `doc/Docu.md`: login validates, hashing decision closed, new `salt` column and script 06, new modules. Edits inside `@tsg-docs:auto` markers where they apply.

## Acceptance criteria

- Valid seed user + password opens `FrmPrincipal` and the top bar shows that user.
- Wrong password, unknown user or inactive user shows one generic message and stays on the login.
- Empty user or password shows a message and does not query the database.
- `Sesion` holds id, user name, full name, role and mechanic id of the logged user.
- Build passes with no new warnings from the new code.

## Delivery

Strategy: `ask-on-risk`. Forecast: ~250 authored changed lines (under the ~400 budget), one PR.

## Progress

All four tasks done on 2026-10-01 by one delegated writer (T4 was also delegated to the same writer, not done inline). The parent read back the diffs and re-ran the build.

| Task | Status | Commit | Evidence |
|---|---|---|---|
| T1 | done | `7751a1e` | SQL and dbml read back; seed hashes equal the generated values. Not executed against a database. |
| T2 | done | `ebd9997` | `dotnet build`: 0 warnings, 0 errors. Vector check with the real `Seguridad.vb`: professor vector matches; three seed users verify True; wrong password False; old BCrypt value False without exception; salt is 16 random bytes. |
| T3 | done | `3c17a95` | `dotnet build`: 0 warnings, 0 errors. The commit also carries the user's pre-existing empty `FrmPrincipal_Load` handler (same file). Login flow NOT exercised: no app run, no database access. |
| T4 | done | docs commit after `3c17a95` | Docs read back; markers intact; no password values printed. |
| T5 | done | seed users commit after T4 | Accepted user change (2026-10-01): test users are `admin`, `operador`, `mecanico` (was `rgomez`), each with password = user name + `123`, like the professor's seed. `03_dml_prueba.sql` and script 06 updated; script 06 renames `rgomez` and is safe to re-run. New hashes recomputed and matched; the old shared password no longer verifies. Not executed against a database. |

Pending checks (manual, by the user):

- Run `database/06_migracion_hash_pbkdf2.sql` again on each already-loaded local database (the user ran the first version on 2026-10-01; the second run renames `rgomez` and sets the new passwords).
- Run the app and try the acceptance criteria (valid user, wrong password, empty fields).

## Next step

The user runs script 06 and tests the login manually; then decides push / PR for `feature/login`.

# Proposal: base-de-datos

## Intent

Create the MariaDB database for the 12-table, services-only model defined in
`taller-mecanico.dbml` (source of truth) and `taller-mecanico-especificacion.md`.
The previous 25-table database (billing, payment methods, stock) is obsolete:
the chair excluded billing and payment methods from scope.

## Scope

In scope:
- `database/01_ddl_estructura.sql`: database, 12 tables, enums, PK/FK, indexes, CHECK constraints
- `database/02_dml_catalogos.sql`: OT states, service categories, initial admin user
- `database/03_dml_prueba.sql`: sample data for development only
- `database/04_consultas_verificacion.sql`: queries to confirm the load

Out of scope:
- Data access layer (DAO), UI wiring, reports
- Any billing, payment, stock or supplies table

## Approach

Translate the dbml one to one into MariaDB DDL. Apply the constraints the spec requires
(section 8) as CHECK constraints where MariaDB allows it, and keep business rules that
need other rows (km validation, state transitions) for the application layer.

## Risks

- `01` starts with `DROP DATABASE IF EXISTS taller_mecanico`, which deletes the obsolete
  25-table database with the same name. Accepted: that database is no longer used.
- MariaDB >= 10.5 error 1901: a CHECK cannot reference a column that belongs to a FK with a
  CASCADE action. Mitigation: every FK uses `ON UPDATE RESTRICT` (spec 11.2).

## Rollback

Re-run the scripts or drop the `taller_mecanico` database. No application code depends on it yet.

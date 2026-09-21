# Tasks: base-de-datos

## Review Workload Forecast

- Estimated changed lines: ~350 (SQL only)
- 400-line budget risk: Low
- Chained PRs recommended: No
- Decision needed before apply: No

## 1. Structure

- [x] 1.1 Write `database/01_ddl_estructura.sql` with the 12 tables, FKs, indexes and CHECKs

## 2. Data

- [x] 2.1 Write `database/02_dml_catalogos.sql` (states, categories, admin with BCrypt hash)
- [x] 2.2 Write `database/03_dml_prueba.sql` (sample master data)
- [x] 2.3 Write `database/04_consultas_verificacion.sql`

## 3. Verification

- [x] 3.1 Run 01 -> 02 -> 03 on a real MariaDB and check counts
- [x] 3.2 Confirm CHECK / FK constraints reject invalid rows

## Verification evidence (2026-09-21, mariadb:10.11 in Docker)

- 01, 02, 03 ran without errors; 12 tables; counts: usuario 3, mecanico 3, cliente 5, marca 6,
  modelo 14, vehiculo 7, categoria_servicio 5, servicio 14, estado_ot 8
- Rejected as expected: role/mechanic mismatch (4025 x2), duplicate plate and document (1062),
  negative price/km (4025), zero quantity (4025), approved > budgeted (4025),
  delete brand/service in use (1451)
- Accepted as expected: valid OT, detail line and history row; deleting an OT cascades to detail and history
- BCrypt hashes ($2a$11) verified against admin123 / taller123

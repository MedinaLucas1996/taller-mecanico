# base-de-datos Specification

## Purpose

MariaDB database for the Taller Mecanico services-only model. Traceability: `taller-mecanico.dbml`,
spec sections 4, 6, 7, 8 and 11.

## Requirements

### Requirement: Schema matches the dbml

The DDL MUST create exactly the 12 tables of `taller-mecanico.dbml` with the same names,
columns, types, nullability, defaults, unique keys and indexes.

#### Scenario: fresh install

- GIVEN an empty MariaDB server (>= 10.6)
- WHEN `01_ddl_estructura.sql` runs
- THEN database `taller_mecanico` exists with 12 InnoDB tables in `utf8mb4_unicode_ci`

### Requirement: Referential actions

Every FK MUST use `ON UPDATE RESTRICT`. Detail tables (`ot_detalle`, `ot_historial_estado`)
MUST use `ON DELETE CASCADE` towards `orden_trabajo`; every other FK MUST use `ON DELETE RESTRICT`.

#### Scenario: master data in use cannot be deleted

- GIVEN a service referenced by an `ot_detalle` row
- WHEN the service is deleted
- THEN the database rejects the delete

### Requirement: Role and mechanic coherence (spec 8.6)

A user SHALL have `id_mecanico` if and only if `rol = 'MECANICO'`.

#### Scenario: operator with mechanic

- GIVEN a user with `rol = 'OPERADOR'`
- WHEN it is inserted with a non-null `id_mecanico`
- THEN the database rejects the insert

### Requirement: Non-negative amounts

Prices, quantities, hours and mileage MUST NOT be negative; `ot_detalle.cantidad` MUST be greater than zero.

#### Scenario: zero quantity line

- GIVEN an order
- WHEN a detail line with `cantidad = 0` is inserted
- THEN the database rejects the insert

### Requirement: Stable OT state catalog (spec 6)

`02_dml_catalogos.sql` MUST insert the 8 states with explicit, stable IDs and the
`permite_edicion_detalle` / `es_estado_final` flags from the spec table.

#### Scenario: editable states

- WHEN querying states with `permite_edicion_detalle = 1`
- THEN only RECEPCIONADA and PRESUPUESTADA are returned

### Requirement: Initial administrator (spec 4.2, 11.4)

The catalog script MUST create user `admin` with role ADMINISTRADOR and a BCrypt hash, never plain text.

### Requirement: Sample data is optional

`03_dml_prueba.sql` MAY be skipped; the system MUST be usable with only `01` + `02`.

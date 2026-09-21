# Design: base-de-datos

## Decisions

| Decision | Rationale |
|---|---|
| Scripts live in `database/` at the project root | Versioned with the project, easy to open from DBeaver |
| Enums as MariaDB `ENUM` (`rol`, `nivel_combustible`) | Mirrors the dbml; values never change |
| `estado_ot` keeps explicit IDs 1..8 | App code references states by ID; must be stable across reinstalls |
| `ON UPDATE RESTRICT` on all FKs | Surrogate keys never change; avoids MariaDB error 1901 with CHECK |
| `ON DELETE CASCADE` only for `ot_detalle` and `ot_historial_estado` | Spec 11.2; orders are voided, never deleted (8.7), so it only matters in dev resets |
| Constraint prefixes in Spanish: `cp_`, `cf_`, `un_`, `ind_`, `val_` | The chair requires no English in the database; names also make rejected-row errors readable |
| Rules needing other rows stay in the app | km >= last order km (8.3), state transitions (6), detail freeze (8.5) cannot be CHECKs |
| `nro_orden` has no DB default | Generation strategy (multi-station concurrency) is decided in the OT change |

## Not modeled in the DB

- Subtotal and totals are stored but not computed by triggers: the DAO recalculates them in
  the same transaction (spec 11.5). Keeping one place for the logic avoids double sources of truth.

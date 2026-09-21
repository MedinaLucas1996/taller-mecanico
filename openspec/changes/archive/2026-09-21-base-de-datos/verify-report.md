```yaml
schema: gentle-ai.verify-result/v1
evidence_revision: sha256:79a43864c6053b638574340bcb22cf18d2f4db6600938b9d21a63d0a5983d7d8
verdict: pass
blockers: 0
critical_findings: 0
requirements: 7/7
scenarios: 5/5
test_command: docker run mariadb:10.11 + run database/01..03 + constraint probes (harness recorded in verify evidence)
test_exit_code: 0
test_output_hash: sha256:79a43864c6053b638574340bcb22cf18d2f4db6600938b9d21a63d0a5983d7d8
build_command: dotnet build WinFormsApp1.slnx -nologo -v q
build_exit_code: 0
build_output_hash: sha256:0206591697c211a87568abf78fe1f44335c1d60e35a1cdea7aedddcc93c19797
```

## Verification Report

**Change**: base-de-datos
**Version**: N/A
**Mode**: Standard (no test project; strict_tdd false)

### Completeness
| Metric | Value |
|--------|-------|
| Tasks total | 6 |
| Tasks complete | 6 |
| Tasks incomplete | 0 |

### Build & Tests Execution
**Build**: ✅ Passed
```text
dotnet build WinFormsApp1.slnx -nologo -v q
Compilación correcta. 0 Advertencia(s) 0 Errores
```

**Tests**: ✅ 7 requirement checks passed / 0 failed / 0 skipped
```text
Fresh mariadb:10.11 container (isolated; the user's local database was not touched).
RUN 01_ddl_estructura: OK | RUN 02_dml_catalogos: OK | RUN 03_dml_prueba: OK
tables: 12 | outside InnoDB/utf8mb4_unicode_ci: 0
FKs with ON UPDATE other than RESTRICT: 0 | ON DELETE CASCADE: ot_detalle, ot_historial_estado
delete service in use: ERROR 1451
OPERADOR with mechanic: ERROR 4025 | MECANICO without mechanic: ERROR 4025
zero quantity / negative price / negative km: ERROR 4025 (x3)
states: 8 | editable: RECEPCIONADA, PRESUPUESTADA | final: ENTREGADA, RECHAZADA, ANULADA
admin: ADMINISTRADOR, $2a$11$ (BCrypt)
only 01+02: new client/vehicle/order accepted
container removed
```

**Coverage**: ➖ Not available (SQL scripts, no unit test runner)

### Spec Compliance Matrix
| Requirement | Scenario | Test | Result |
|-------------|----------|------|--------|
| Schema matches the dbml | fresh install | table count + engine/collation probe | ✅ COMPLIANT |
| Referential actions | master data in use cannot be deleted | DELETE servicio in use → 1451 | ✅ COMPLIANT |
| Role and mechanic coherence (8.6) | operator with mechanic | INSERT usuario OPERADOR + mechanic → 4025 | ✅ COMPLIANT |
| Non-negative amounts | zero quantity line | INSERT ot_detalle cantidad 0 → 4025 | ✅ COMPLIANT |
| Stable OT state catalog (6) | editable states | SELECT permite_edicion_detalle = 1 | ✅ COMPLIANT |

**Compliance summary**: 5/5 scenarios compliant

### Correctness (Static Evidence)
| Requirement | Status | Notes |
|------------|--------|-------|
| Schema matches the dbml | ✅ Implemented | Column-by-column parity reviewed during apply, not by an automated diff |
| Referential actions | ✅ Implemented | Every FK uses ON UPDATE RESTRICT (avoids MariaDB error 1901) |
| Role and mechanic coherence | ✅ Implemented | CHECK `val_usuario_rol_mecanico` |
| Non-negative amounts | ✅ Implemented | CHECK constraints `val_*` |
| Stable OT state catalog | ✅ Implemented | Explicit IDs 1..8 |
| Initial administrator | ✅ Implemented | admin / BCrypt $2a$11$ |
| Sample data is optional | ✅ Implemented | 01 + 02 alone yield a working database |

### Coherence (Design)
| Decision | Followed? | Notes |
|----------|-----------|-------|
| Scripts in `database/` | ✅ Yes | |
| MariaDB ENUM for rol / nivel_combustible | ✅ Yes | |
| Stable estado_ot IDs | ✅ Yes | |
| ON UPDATE RESTRICT on all FKs | ✅ Yes | |
| ON DELETE CASCADE only on detail tables | ✅ Yes | |
| Spanish constraint prefixes | ✅ Yes | Changed at the chair's request; recorded in design.md |
| Rules needing other rows stay in the app | ✅ Yes | km, transitions and detail freeze are not CHECKs |

### Issues Found
**CRITICAL**: None
**WARNING**: None
**SUGGESTION**: Decide password hashing (the professor's reference uses PBKDF2+SHA256+salt; this change uses BCrypt) in the future login change.

### Verdict
PASS
All 7 requirements and 5 scenarios pass on a fresh MariaDB, and the build is clean.

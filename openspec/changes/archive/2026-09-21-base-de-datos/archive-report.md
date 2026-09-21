# Archive report: base-de-datos

- Archived: 2026-09-21
- Archived to: `openspec/changes/archive/2026-09-21-base-de-datos/`
- Store: hybrid (openspec files + Engram `sdd/base-de-datos/archive-report`)
- Readiness: `gentle-ai sdd-status` reported `archive: ready`, `nextRecommended: archive`, and no blocked reasons
- gentle-ai attempt `verify-bd-20260921-01`: settled `passed`, state `complete`

## Specs synced

| Domain | Action | Details |
|--------|--------|---------|
| base-de-datos | Created | New main spec `openspec/specs/base-de-datos/spec.md` (7 requirements, 5 scenarios), copied mechanically with an empty `diff -r` |

## Archive contents

- proposal.md ✅
- specs/base-de-datos/spec.md ✅
- design.md ✅
- tasks.md ✅ (6/6 tasks complete, 0 unchecked)
- verify-report.md ✅ (verdict pass, validated by `gentle-ai sdd-verify-validate`)

## Final state

- The database scripts `database/01`–`04` are in place, loaded on the user's local MariaDB 12.2, and verified on a fresh `mariadb:10.11` container.
- During verification, the spec headings were corrected from `##`/`###` to `### Requirement:` / `#### Scenario:` so native status counts them. Requirement content did not change.
- The git repository was initialized during this cycle so the gentle-ai attempt ledger could run. No commits have been made yet; delivery follows ordinary repository policy.

## Open items (outside this change)

- Password hashing decision (PBKDF2+SHA256+salt, as in the professor's reference, versus the current BCrypt). This is deferred to the login change.
- Screens built after this change (Clientes, Vehículos, Marcas y modelos) have no SDD change record.

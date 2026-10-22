# Feature: turnos (appointments, part 1)

Objective: book a workshop time slot for a vehicle and close it when the vehicle is received. Branch `feature/TM-0022` from `main` at 62a6763. Delivery strategy: single-pr. Test runner: none; checks are a clean `dotnet build` to a scratch OutDir, a throwaway MariaDB and the harness that drives the forms (the user authorized end-to-end tests on the local database for TM-0021).

Style (mandatory): the professor's event-driven VB.NET, same rules as `odd/tasks/recordatorio-service.md`. UI texts in neutral Spanish.

## Specs

- S1 Design agreed by the user: a turno reserves a time slot of the workshop, not a mechanic. User: "Perfecto".
- S2 Slots agreed by the user ("Esta bien esa franja"): Monday to Friday 8 to 17 (9 one-hour slots, the last starts at 16), Saturday 8 to 12 (4 slots), 2 vehicles per slot. Constants in code. Sunday closed.
- S3 Table `turno`: vehicle, date and time (start of the slot), reason, state `PENDIENTE`, `ATENDIDO`, `CANCELADO` or `AUSENTE`, order generated (optional), user and creation date. Idempotent migration `database/12_turnos.sql`; `01` creates it; the model goes from 15 to 16 tables. The vehicle must exist (new vehicles are registered first; the reception quick registration already exists).
- S4 Screen "Turnos" for administrador and operador: one day at a time with previous/next/today; one row per slot with its turnos and the free places; give, cancel and mark a turno absent.
- S5 Recepción: when the plate has a turno today it warns "Tiene turno a las 10:00 (motivo)"; confirming the reception marks the turno `ATENDIDO` and links the order.
- Part 2, not here: "Dar turno" from Services por vencer, Inicio card "Turnos de hoy", WhatsApp confirmation.
- Assumptions: a cancelled turno frees its place; a vehicle can have only one pending turno from now on; a slot can be booked until it ends; "Marcar ausente" only once the slot started; the reason prefills the reception symptom when that box is empty.

## Tasks

- [ ] T1 S3 — inline
- [ ] T2 S4 — inline
- [ ] T3 S5 — inline
- [ ] T4 docs — inline

## Log

- L1 (2026-10-22) User: "Bien, el tema de turnos, que opinas? como lo harias vos? Cual consideras que seria la mejor opcion?" Proposal (slot of the workshop, hourly slots with a cupo, table turno, day view, Recepción link; part 2 later) answered "Perfecto"; slots answered "Esta bien esa franja".
- Route: inline (the screen and the reception change share the table and the slot rules; no disjoint units).

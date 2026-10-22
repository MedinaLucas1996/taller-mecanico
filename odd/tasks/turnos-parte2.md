# Feature: turnos-parte2 (appointments from reminders, Inicio card, WhatsApp confirmation)

Objective: close the loop reminder -> appointment -> reception. Branch `feature/TM-0024` from `feature/TM-0023` at 44fbabf (PR #27 still open and this change touches the same screen). Delivery strategy: single-pr. Test runner: none; checks are a clean `dotnet build`, the throwaway MariaDB and the harness that drives the forms.

Style (mandatory): the professor's event-driven VB.NET, same rules as `odd/tasks/turnos.md`. UI texts in neutral Spanish.

## Specs

- S1 "Dar turno" from Services por vencer: opens Turnos with the vehicle and the reason (its listed services) already filled, on the first day with a free place. Agreed part 2 item: "'Dar turno' desde Services por vencer".
- S2 Inicio card "Turnos de hoy" that opens Turnos. Agreed part 2 item: "tarjeta 'Turnos de hoy' en Inicio".
- S3 WhatsApp confirmation of a turno copied to the clipboard, same pattern as the service reminder. Agreed part 2 item: "confirmación por WhatsApp".
- Assumptions: the confirmation is offered right after giving a turno and from the card of any pending turno; it is not recorded in the database. Services por vencer shows the vehicle's next pending turno in a new column, and a vehicle with a pending turno counts as handled (grey, at the end, not counted by the Inicio reminder card), so nobody writes to a customer who already booked. The Inicio card counts today's pending turnos.

## Tasks

- [x] T1 S3 — inline — commit 95d3d54
- [x] T2 S1 — inline — commit 95d3d54 (shares FrmTurnos with T1)
- [x] T3 S2 — inline — commit 71531b7
- [x] T4 docs — inline — closing docs commit

## Log

- L1 (2026-10-22) User: "Avanza por la segunda parte" (part 2 as listed in the TM-0022 PR and the previous answers).
- Route: inline (all units share FrmTurnos and the reminder query).
- User (2026-10-22): "ya esta el merge" (PR #27 merged while this branch was in progress).
- Verification (2026-10-22): `dotnet build` 0 warnings, 0 errors. Harness on the throwaway MariaDB (port 3399) extended with part 2, 47 checks passed: the confirmation of AB123CD contains "para confirmar su turno del lunes 26 de octubre a las 09:00", the motive and the closing line, and the dialog names the phone; Inicio "Turnos de hoy" matches the database and its click opens Turnos; Services por vencer shows AB123CD with "26/10 09:00", grey and last, and Dar turno on it says it already has one; Dar turno on AF456GH opens Turnos highlighted in the menu, on the first day with places (today 14:00), with the plate searched and "Service: Cambio de aceite y filtro, Alineación y balanceo"; saving offers the confirmation (answered No) and leaves it PENDIENTE; back in the list AF456GH is grey with its turno; the Inicio services card counts only overdue services without notice or turno (1); plus the 32 previous checks of TM-0022.

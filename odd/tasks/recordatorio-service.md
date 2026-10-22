# Feature: recordatorio-service (service reminders with a WhatsApp message)

Objective: tell the operador which vehicles are due for a periodic service and give them a ready-to-paste WhatsApp message, recording every notice. Branch `feature/TM-0021` from `main` at f58e96d. Delivery strategy: single-pr. Test runner: none; checks are a clean `dotnet build` to a scratch OutDir and the off-screen render harness (read paths only).

Style (mandatory): the professor's event-driven VB.NET, same rules as `odd/tasks/diagnostico-fotos.md`: designer-built controls with `Handles`, SQL in the form with `Using` and `AddWithValue`, validation ending in `Exit Sub`, multi-step saves in `BeginTransaction`, short lowercase Spanish comments without accents, `Sesion.ROL_*`, `MessageBox.Show`, `LeerIcono`. No DAO, no new packages. UI texts in neutral Spanish.

## Specs

- S1 Schema. New nullable columns `servicio.intervalo_meses` (SMALLINT) and `servicio.intervalo_km` (INT). New table `aviso_service` (id, `id_vehiculo` FK, `id_servicio` FK, `id_usuario` FK, `fecha_hora` default now). Idempotent migration `database/10_recordatorio_service.sql`; `01_ddl_estructura.sql` creates both; the specification goes from 14 to 15 tables.
- S2 Servicios ABM (admin only): two optional fields "Repetir cada N meses" and "cada N km". Services without them never generate reminders. Assumption: the reminder date needs the months interval, so km without months is refused with a message; km is only shown as a reference (the system only knows the km when the car enters).
- S3 Due rule: for each active vehicle and each service with `intervalo_meses`, the last ENTREGADA order with an approved line of that service; due date = `fecha_entrega` + interval months; reference km = `km_ingreso` + `intervalo_km`. The current titular (`vehiculo.id_cliente`) receives the message. Assumption: vehicles with an open (non-final) order are left out, since they are in the workshop.
- S4 New screen "Services por vencer" in OPERACIONES for admin and operador: filter "Vencidos" / "Vencen en los próximos 30 días"; grid with client, phone, vehicle, service, last service (date and km) and due date; buttons "Copiar mensaje" and "Exportar CSV".
- S5 "Copiar mensaje" copies to the clipboard only (user: "Copiar al portapapeles nomas"). User: "lo que si el mensaje que sea mas agradable, y que finalice con un saludo". Agreed text, "usted" like the existing notice:
  "¡Hola Juan Pérez! 👋 Le escribimos de Taller Mecánico para recordarle que a su Ford Fiesta (patente NN333MM) ya le corresponde el cambio de aceite: el último lo hicimos el 15/03/2026, a los 45.000 km.
  Mantener el service al día cuida el motor y evita sorpresas en la ruta. Si le parece, le reservamos un turno cuando le quede cómodo.
  ¡Muchas gracias por confiar en nosotros! Saludos cordiales, el equipo de Taller Mecánico."
  Not yet due: "se acerca el ... (vence el dd/MM/yyyy)". Several services due for the same vehicle go in one message. No km when unknown.
- S6 Every copy records one `aviso_service` row per service in the message. A vehicle noticed less than 15 days ago shows "Avisado el ... por ...", grey and at the end; copying again asks first.
- S7 Inicio: a fourth card "Services vencidos" that opens the new screen.
- S8 README, `doc/Docu.md` and the specification updated.

## Tasks

- [ ] T1 S1 — inline
- [ ] T2 S2 — inline
- [ ] T3 S3-S6 — inline
- [ ] T4 S7 — inline
- [ ] T5 S8 — inline

## Log

- L1 (2026-10-22) User: "Firma del Cliente no lo quiero, los otros 2 me interesa la verdad" (service reminder and appointments; the reminder goes first).
- Decisions (2026-10-22): interval per service, date drives and km is a reference (user accepted the recommendation); "Copiar al portapapeles nomas, ya con eso quedaria bien"; "Si, registralo" (the notice); design with the screen, Inicio card and migration 10 approved with "Dale".
- Route: inline. The units share the due rule and the Inicio card reads the same query, so there are no disjoint units for parallel writers.

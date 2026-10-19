# Feature: mejoras-robustez (robustness, configuration and UX improvements)

Objective: harden the app against database outages and lost work, take the connection string out of the code, and polish the UX of the main menu, the boards and the master data screens. Branch `feature/TM-0016` from `feature/TM-0015` at f856b08. Delivery strategy: single-pr (the user's convention is one branch per batch, PR when asked). Test runner: none (no test project); checks are a clean `dotnet build` plus an off-screen render of the touched forms, read paths only.

## Specs

- S1 Unhandled exceptions no longer kill the app with the .NET dialog: `ApplicationEvents` handles `UnhandledException`, writes the error to a log file under `%LocalAppData%\TallerMecanico\errores.log` and shows a friendly message. New `Module Errores` with `RegistrarError` and `MostrarError`; `MostrarError` recognises a MySQL connection failure and says "No se pudo conectar con la base de datos." instead of the raw driver message, and logs every error it shows.
- S2 The connection string can be overridden without touching code: `ConexionBD.CADENA` reads `conexion.txt` next to the executable when that file exists, otherwise it keeps the current default (`localhost`, `root`, empty password). `WinFormsApp1/conexion.ejemplo.txt` documents the format; `WinFormsApp1/conexion.txt` is gitignored and copied to the output folder when present. README and Docu updated.
- S3 `FrmLogin`: any exception while verifying is handled through `MostrarError` (today only `MySqlException` is caught); the wait cursor shows and the button is disabled while verifying; the empty `Load` handler is removed.
- S4 `FrmRecepcion`: leaving the screen (menu, logout, close) with data loaded asks "¿Cancelar la recepción? Se pierden los datos y las fotos cargadas." like the Cancel button does; the five photos are disposed when the form closes; the confirmation re-checks inside the transaction that the vehicle is active and has no open order (two workstations cannot open two orders for the same vehicle); `Commit` runs outside the inner `Try` like the other 22 transactions.
- S5 `FrmOrdenes` frees its five thumbnails on `FormClosed` (as `FrmMisOrdenes` already does). `Fotos.CargarImagenReducida` disposes the new bitmap when drawing fails.
- S6 `FrmUsuarios`: a new or changed password must have at least 6 characters and cannot be only spaces ("La contraseña debe tener al menos 6 caracteres."). Assumption: 6 is the minimum; the seeded test passwords are 8 characters long.
- S7 `FrmClientes`, `FrmVehiculos` and `FrmMarcasModelos` check `Sesion.Rol` in `Load` like the other screens (administrador or operador), so a screen opened outside the menu does not work for a mechanic. Role names become constants in `Module Sesion` and every literal in the forms uses them.
- S8 `FrmPrincipal`: the option of the open screen stays highlighted in the side menu, and the window title shows the open screen ("Taller Mecánico - Clientes").
- S9 Double-click on a row of the orders grid opens the order in `FrmOrdenes` and `FrmMisOrdenes` (same as the button). Escape triggers "Cancelar" in the six master data screens (`CancelButton`). `FrmTableros` shows the wait cursor while it recalculates and, when the database is unreachable, shows one message instead of five.

## Tasks

- [ ] T1 S1 S2 S3 — route inline — Errores module, ApplicationEvents handler, conexion.txt, login
- [ ] T2 S4 S5 S6 S7 — route inline — reception closing/race/dispose, board dispose, Fotos, password rule, role checks and constants
- [ ] T3 S8 S9 — route inline — active menu option + title, double-click, Escape, dashboard cursor and single outage message
- [ ] T4 — build, off-screen render of FrmPrincipal/FrmLogin/an ABM, docs (README, Docu, this file)

## Log

- L1 (2026-10-19) User: "Buenas, quiero que mejores el proyecto, analiza con profundidad y añade mejoras que aportan valor al desarrollo, si consideras mejoras de UI/UX aplicalos"
- Analysis (2026-10-19): read-only audit of the 18 forms and 5 modules. Sound: all 141 commands parameterised, every multi-step save in a transaction, every handler that runs SQL has Try/Catch. Gaps: no global exception handler; raw driver messages on outage (5 dialogs in a row in Tableros); login catches only MySqlException; FrmRecepcion loses the wizard silently when the menu changes, commits inside the inner Try and does not re-check the open-order rule in the transaction; FrmOrdenes thumbnails never disposed; no password rules; Clientes/Vehículos/Marcas have no role check; no visual cue of the open screen in the menu; no double-click, no Escape, no wait cursor anywhere. Left out on purpose (schema or product decisions): UNIQUE index on mecanico name, audit rows for mechanic reassignment and detail edits, debounce of the filter boxes.

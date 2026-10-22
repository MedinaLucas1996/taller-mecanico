# Taller Mecánico — Sistema de gestión

## Qué es

Aplicación de escritorio Windows Forms (VB.NET, .NET 10) sobre MariaDB que administra la actividad de servicios de un taller mecánico. Es un trabajo práctico universitario y sigue a propósito el estilo de la cátedra de programación orientada a eventos: SQL dentro de los formularios, sin capa de acceso a datos. Queda fuera la facturación, los pagos, la venta de insumos y el control de stock.

Lo que hace, por rol:

- **Administrador y operador**: clientes, vehículos, marcas y modelos; turnos del taller; recepción del vehículo con fotos y apertura de la orden de trabajo; presupuesto, aprobación del cliente y entrega; avisos de service por WhatsApp; historial por patente; reportes impresos. El administrador además administra servicios, categorías, mecánicos y usuarios, y ve los Tableros.
- **Mecánico**: sus órdenes asignadas (diagnóstico, inicio del trabajo, horas y observaciones, fotos del trabajo, finalización) y el historial, sin importes.

El sistema tiene 22 pantallas y una base de 16 tablas. El detalle de cada pantalla, el flujo de la orden y las decisiones técnicas están en [doc/Docu.md](./doc/Docu.md).

## Requisitos

- Windows (el proyecto apunta a `net10.0-windows`).
- .NET 10 SDK.
- Visual Studio con la carga de trabajo de desarrollo de escritorio de .NET (Windows Forms y Visual Basic). El proyecto se abre desde `WinFormsApp1.slnx`; en modo *Folder View* el diseñador de formularios queda deshabilitado.
- MariaDB 10.6 o superior en ejecución (el proyecto se desarrolló con MariaDB 12.2) y un cliente SQL para ejecutar los scripts (DBeaver, HeidiSQL o la consola `mariadb`).

## Instalación

Ejecutar en este orden los scripts de `database/` sobre el servidor MariaDB:

| Paso | Script | Qué hace |
|---|---|---|
| 1 | `01_ddl_estructura.sql` | Crea la base `taller_mecanico` y sus 16 tablas. **Elimina la base si ya existe** (`DROP DATABASE IF EXISTS`): comentar esa línea si hay datos que conservar. |
| 2 | `02_dml_catalogos.sql` | Carga los catálogos mínimos (estados de la orden y categorías de servicio) y el usuario `admin`. |
| 3 | `03_dml_prueba.sql` | **Requerido.** Carga marcas, modelos y servicios, más mecánicos, los usuarios `operador` y `mecanico`, clientes y vehículos de prueba. Sin él no se puede recepcionar ni presupuestar. |
| 4 (opcional) | `08_datos_demo.sql` | Unas 40 órdenes de los últimos siete meses, con fechas relativas al día en que se ejecuta, para que los Tableros muestren datos. Requiere `01` a `03`. Se puede ejecutar más de una vez: borra antes sus propias órdenes (marcadas con `[demo]`). |
| 5 (opcional) | `11_datos_demo_avisos.sql` | Seis órdenes entregadas sobre los vehículos de `03`, para que Services por vencer muestre services vencidos y por vencer. Requiere `01` a `03`. Se puede ejecutar más de una vez. |

> **Bases creadas con una versión anterior de `01`.** Los scripts `06` (contraseñas PBKDF2), `07` (fecha prometida y fotos de recepción), `09` (diagnóstico y fotos del trabajo), `10` (intervalos de service y avisos) y `12` (turnos) son migraciones: agregan lo que falta sin tocar los datos existentes y se pueden ejecutar más de una vez. Una instalación nueva con `01` a `03` no las necesita. Los scripts `04` y `05` son consultas de verificación y de práctica.

## Configuración

**Cadena de conexión.** Por defecto la aplicación se conecta a `localhost`, puerto 3306, usuario `root` sin contraseña, base `taller_mecanico`. Para otro servidor u otras credenciales, copiar `WinFormsApp1/conexion.ejemplo.txt` como `WinFormsApp1/conexion.txt` y editar `Server`, `Port`, `User ID` y `Password`. El archivo está en `.gitignore` (la contraseña local no se sube al repositorio), se copia a la carpeta de salida al compilar y `Module ConexionBD` lo lee al iniciar. Si existe pero no se puede leer o no tiene ninguna cadena, el login no deja entrar y pide corregirlo o eliminarlo, para que un puesto configurado no termine conectado al servidor por defecto sin querer. No se usan variables de entorno ni archivos `appsettings`.

- **Registro de errores.** `Module Errores` escribe los errores no atrapados y los que muestra cada pantalla (`Errores.MostrarError`) en `%LocalAppData%\TallerMecanico\errores.log`, con fecha, usuario, contexto y detalle. Ante un error no atrapado el sistema pregunta si se sigue trabajando o se sale; si el error se repite tres veces seguidas en pocos segundos, se cierra solo. Cuando la base no responde, el mensaje dice "No se pudo conectar con la base de datos."; cuando el servidor rechaza el usuario o la contraseña, indica revisar `conexion.txt`.

## Ejecución

Abrir `WinFormsApp1.slnx` en Visual Studio y ejecutar (F5), o desde una terminal en la raíz del repositorio:

```bash
dotnet run --project WinFormsApp1
```

No hay pruebas automatizadas que ejecutar.

## Usuarios de prueba

Los crean `02_dml_catalogos.sql` (`admin`) y `03_dml_prueba.sql` (`operador` y `mecanico`). La contraseña es el nombre de usuario seguido de `123`.

| Usuario | Contraseña | Rol | Qué ve |
|---|---|---|---|
| `admin` | `admin123` | ADMINISTRADOR | Todo el menú: operaciones, datos maestros y Tableros. |
| `operador` | `operador123` | OPERADOR | Operaciones, Clientes, Vehículos y Marcas y modelos. |
| `mecanico` | `mecanico123` | MECANICO | Mis órdenes e Historial. Es el mecánico Ramón Gómez. |

Cualquier usuario cambia su propia contraseña desde "Cambiar contraseña" en la barra superior (pide la actual y la nueva, de al menos 6 caracteres). Tras cinco intentos fallidos seguidos, el botón INGRESAR del login se deshabilita 30 segundos.

## Guion de demostración

Con `01`, `02`, `03`, `08` y `11` cargados. La patente `AB123CD` es un vehículo de `03` sin orden abierta.

1. **Ingresar como `admin`.** Se abre **Inicio**: tarjetas con las órdenes prometidas para hoy, las demoradas, las listas para entregar, los turnos de hoy y los services vencidos, y la grilla con las órdenes que piden atención.
2. **Turnos.** Clic en un lugar libre de la agenda de hoy (o "Nuevo turno"): patente `AB123CD`, motivo "Ruido al frenar", "Dar turno". Aceptar copiar la confirmación por WhatsApp y mostrar el texto pegándolo en cualquier editor.
3. **Recepción.** Buscar `AB123CD`: al lado de la patente aparece "Tiene turno hoy a las ..." y el motivo completa el síntoma. Paso 2: kilometraje no menor al último registrado, mecánico **Ramón Gómez** y una fecha prometida. Paso 3: cargar alguna foto (opcional). Paso 4: "Confirmar recepción". El turno queda atendido y vinculado a la orden.
4. **Cerrar sesión e ingresar como `mecanico`.** En **Mis órdenes**, grupo "Para diagnosticar", abrir la orden con "Cargar diagnóstico", escribir lo que encontró y "Guardar diagnóstico".
5. **Ingresar como `operador`.** En **Órdenes de trabajo**, elegir la orden y "Gestionar orden": el diagnóstico se lee encima de las líneas. Agregar dos o tres servicios y **Presupuestar**; "Imprimir" → "Presupuesto". Después tildar las líneas que el cliente aprueba y **Registrar aprobación**.
6. **Ingresar como `mecanico`.** En Mis órdenes, grupo "Para empezar", "Iniciar trabajo". Luego, en "En curso", "Cargar horas y finalizar": horas por línea, observaciones del mecánico, una foto del trabajo y **Finalizar trabajo**.
7. **Ingresar como `admin`.** En Órdenes de trabajo abrir la orden: **Avisar al cliente** (copia el mensaje para WhatsApp y deja la fila "Cliente avisado" en el historial) y **Entregar vehículo**. "Imprimir" → "Orden de trabajo", "Acta de recepción" y "Fotos del trabajo".
8. **Historial por patente.** Buscar `AB123CD`, recorrer sus órdenes y **Imprimir** el historial del vehículo.
9. **Services por vencer.** Con el filtro "Vencidos", **Copiar mensaje** de un vehículo (queda registrado el aviso) y **Dar turno** desde otra fila: abre Turnos en el primer día con lugar, con la patente y el motivo ya cargados.
10. **Tableros.** Indicadores y gráficos del mes; cambiar "Desde" a seis meses atrás y **Actualizar**; "Imprimir tiempos" abre la hoja de tiempos por etapa.
11. **Datos maestros** (opcional). En Clientes, "Imprimir ficha" de un cliente con varias órdenes; en Servicios, "Repetir cada (meses)" es el origen de los avisos de service; en Usuarios, baja lógica y reactivación con "Mostrar dados de baja".

## Limitaciones conocidas

- No hay pruebas automatizadas; la verificación es manual con los datos de `03` y `08`.
- Los permisos por rol se aplican en el menú y al abrir cada pantalla; dentro de una pantalla no hay permisos por botón, salvo en Gestión de la orden (anular es solo del administrador y el mecánico tiene su propio modo sin importes).
- Los turnos no tienen recordatorio automático ni se reprograman: se cancela el turno y se da otro. Las franjas y el cupo son constantes del código.
- El vencimiento de los services se calcula por fecha; el kilometraje es solo una referencia, porque el sistema conoce los km del vehículo únicamente cuando ingresa al taller.
- Las fotos se guardan dentro de la base (`LONGBLOB`): su tamaño y el de las copias de respaldo crecen con cada orden.
- El tablero de órdenes trae todas las órdenes sin paginar y no se actualiza solo; los cambios de otro puesto se ven al cambiar un filtro.
- El bloqueo del login tras cinco intentos vive en memoria: reiniciar la aplicación lo reinicia.
- El historial de git conserva una contraseña de desarrollo publicada en una versión anterior; la recomendación es cambiarla en esa base.

## Documentación

- [doc/manual-usuario.md](./doc/manual-usuario.md): manual de usuario, con una captura de cada pantalla, los pasos de cada tarea y el flujo completo de una orden de trabajo.
- [doc/Docu.md](./doc/Docu.md): documento técnico (arquitectura, modelo de datos, pantallas, flujo de la orden de trabajo, reportes y decisiones).
- [taller-mecanico-especificacion.md](./taller-mecanico-especificacion.md): especificación funcional.
- [taller-mecanico.dbml](./taller-mecanico.dbml): modelo de datos en DBML.

Estructura del repositorio:

| Carpeta o archivo | Contenido |
|---|---|
| `WinFormsApp1/` | El proyecto: formularios `Frm*.vb` con su `Designer`, módulos compartidos (`ConexionBD`, `Seguridad`, `Sesion`, `Errores`, `Fotos`, `Impresion`, `Exportar`, `Grillas`, `Recordatorios`, `Turnos`) y `Recursos/` (imagen y música del login, iconos). |
| `database/` | Los scripts SQL `01` a `12` descritos en [Instalación](#instalación). |
| `doc/` | La documentación técnica. |
| `WinFormsApp1.slnx` | La solución que se abre en Visual Studio. |

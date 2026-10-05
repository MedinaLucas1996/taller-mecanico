# WinFormsApp1 (Taller Mecánico) — Documentación técnica

<!-- Lo delimitado por @tsg-docs:auto-start/auto-end se regenera automáticamente. Editar fuera de esos bloques. -->

---

## Qué es

Aplicación de escritorio Windows Forms (VB.NET, .NET 10) conectada a una base MariaDB, que administra los servicios de un taller mecánico. Es un trabajo práctico universitario. El alcance acordado con la cátedra el 27/08 cubre solo la actividad de **servicios**: queda fuera la facturación, los pagos, la venta de insumos y el control de stock (`taller-mecanico-especificacion.md`, sección 2).

Repositorio: https://github.com/MedinaLucas1996/taller-mecanico (público).

## Cómo funciona

- El proyecto de inicio es `FrmLogin` (`MainForm` en `My Project/Application.myapp`). Muestra una imagen de fondo y reproduce música en bucle, ambas desde `WinFormsApp1/Recursos/`. El botón INGRESAR (también con Enter) valida que usuario y contraseña no estén vacíos, busca el usuario activo en `usuario` por `nombre_usuario` y verifica la contraseña con `Seguridad.VerificarClave`. Si el usuario no existe, está inactivo o la clave es incorrecta, muestra el mismo mensaje ("Usuario o contraseña incorrectos."), limpia la contraseña y permanece en el login. Si es correcto, completa el `Module Sesion` (`IdUsuario`, `NombreUsuario`, `NombreCompleto`, `Rol`, `IdMecanico`, este último en 0 si el usuario no es mecánico), abre `FrmPrincipal`, detiene la música y oculta el login.
- `Module Seguridad` implementa el hash de contraseñas con PBKDF2 + SHA256 (100000 iteraciones, hash de 32 bytes, salt aleatorio de 16 bytes, ambos en Base64): `GenerarSalt`, `HashearClave` y `VerificarClave` (comparación en tiempo constante; devuelve `False` si el hash o el salt guardados no son Base64 válido, por ejemplo un hash BCrypt anterior). `Module Sesion` guarda los datos del usuario que inició sesión y tiene `CerrarSesion`.
- `FrmPrincipal` está armado en el diseñador (`FrmPrincipal.Designer.vb`), con manejadores `Handles`: menú lateral, barra superior y un panel de contenido. Las pantallas se abren dentro del panel con `AbrirFormulario` (`TopLevel = False`, `Dock = Fill`). Al cerrar `FrmPrincipal` se llama a `Application.Exit()`, salvo cuando se cierra por el botón "Cerrar sesión". La barra superior muestra el nombre completo y el rol del usuario de `Sesion`, y a su derecha el botón "Cerrar sesión": pide confirmación, limpia `Sesion` con `CerrarSesion()`, llama a `FrmLogin.PrepararNuevoIngreso()` (vacía usuario y contraseña, vuelve a mostrar el login y reinicia la música) y cierra `FrmPrincipal`. Al ingresar otro usuario se crea un `FrmPrincipal` nuevo, por lo que el menú se muestra según el rol de ese usuario.
- El menú se filtra por rol. Los botones del menú y los tres títulos de sección tienen `Visible = False` en el diseñador, y `FrmPrincipal_Load` muestra los que corresponden con un bloque `If Sesion.Rol = "..." Then` por rol (ver [Menú por rol](#menú-por-rol)). Un rol desconocido no ve ninguna opción del menú.
- Menú lateral (`panelMenu`, acoplado a la izquierda): tiene tres paneles declarados en el diseñador.
  - `panelLogo` (arriba): el botón `btnMenu`, que contrae y expande el menú, y el nombre del sistema.
  - `panelMenuArriba` (arriba): Operaciones (Recepción, Órdenes de trabajo, Historial) y Reportes, cada título sobre sus botones.
  - `panelMenuAbajo` (acoplado al borde inferior): Datos maestros (Clientes, Vehículos, Marcas y modelos, Servicios, Categorías, Mecánicos, Usuarios). Sus botones se acoplan hacia abajo, de modo que las opciones ocultas por rol no dejan huecos y el espacio libre queda entre Reportes y Datos maestros.
  - Iconos: `LeerIcono` lee en `FrmPrincipal_Load` cada PNG de `Recursos\iconos` junto al ejecutable (el proyecto los copia a la carpeta de salida con `Recursos\iconos\*`). Si un archivo falta o no se puede leer, el botón queda sin icono. Los iconos provienen de Lucide (licencia ISC, texto en `Recursos/iconos/LICENCIA-lucide.txt`).
  - Contraer y expandir: `AplicarMenu` cambia el ancho del menú entre 230 y 56. Contraído oculta los títulos de sección y el nombre del sistema, deja en cada botón solo el icono centrado y le asigna su nombre como tooltip (`tipMenu`); un botón sin icono muestra su inicial. El nombre de cada opción se guarda una sola vez, en el `Tag` del botón. Como el panel de contenido está acoplado con `Fill`, ocupa el ancho que el menú libera. El estado no se guarda: el menú arranca siempre expandido.
  - Títulos de sección: un título se muestra solo con el menú expandido y cuando el rol tiene al menos una opción en esa sección.
- Cada pantalla terminada abre su propia conexión con `Using cn As New MySqlConnection(CADENA)` dentro del evento correspondiente, ejecuta una consulta parametrizada y vuelca el resultado a la grilla con `DataTable.Load`. `CADENA` es una constante del `Module ConexionBD`.
- Pantallas terminadas:
  - **Clientes** (`FrmClientes`): alta, modificación y baja lógica (`activo = 0`) con confirmación; búsqueda en vivo por nombre o documento (`txtFiltro_TextChanged`); el error 1062 de MariaDB se traduce al mensaje "Ya existe un cliente con ese documento".
  - **Vehículos** (`FrmVehiculos`): alta, modificación y baja lógica; combo de titular (clientes activos); combos de marca y modelo en cascada (el modelo se recarga al cambiar la marca); búsqueda por patente, titular, marca o modelo; el error 1062 se traduce al mensaje de patente duplicada.
  - **Marcas y modelos** (`FrmMarcasModelos`): maestro-detalle (grilla de marcas con cantidad de modelos y grilla de modelos de la marca elegida); la baja es **física** (`DELETE`) y el error 1451 de clave foránea se traduce a un mensaje ("la marca tiene modelos cargados" o "hay vehículos cargados con ese modelo").
  - **Usuarios** (`FrmUsuarios`, solo administrador): alta, modificación y baja lógica (`activo = 0`) con confirmación; la grilla lista solo usuarios activos (con el nombre del mecánico asociado, si lo hay) y nunca lee `hash_contrasena` ni `salt`; búsqueda en vivo por usuario o nombre completo. Al crear, la contraseña es obligatoria y se guarda con `Seguridad.GenerarSalt` + `Seguridad.HashearClave`; al modificar, una contraseña vacía conserva la actual y una contraseña escrita genera un salt y un hash nuevos. El combo de rol (`ADMINISTRADOR`, `OPERADOR`, `MECANICO`) habilita el combo de mecánico solo para `MECANICO` y ese rol exige elegir un mecánico; para los demás roles `id_mecanico` se guarda como NULL (regla 8.6, respaldada por el CHECK `val_usuario_rol_mecanico`). El error 1062 se traduce al mensaje de nombre de usuario duplicado, que puede corresponder a un usuario dado de baja. Reglas de autoprotección: el usuario logueado no puede darse de baja ni cambiar su propio rol, y si modifica su propio usuario se actualizan `Sesion.NombreUsuario` y `Sesion.NombreCompleto`. Como respaldo del menú, al abrirse con un rol distinto de `ADMINISTRADOR` muestra un aviso y deshabilita el formulario.
  - **Mecánicos** (`FrmMecanicos`, solo administrador): alta, modificación, baja lógica y reactivación sobre la tabla `mecanico`. Como respaldo del menú, al abrirse con un rol distinto de `ADMINISTRADOR` muestra un aviso y deshabilita el formulario.
    - Campos: nombre completo (obligatorio, hasta 100 caracteres), especialidad (hasta 80) y teléfono (hasta 30); los dos últimos, si quedan vacíos, se guardan como NULL.
    - Grilla: lista todos los mecánicos, activos y dados de baja, con la columna "Activo" (Sí / No); los activos primero. Búsqueda en vivo por nombre o especialidad.
    - Nombre repetido: no se guarda ni se modifica un mecánico con el nombre de otro mecánico activo (se compara sin espacios sobrantes y sin distinguir mayúsculas).
    - Baja y reactivación: con un mecánico activo seleccionado el botón dice "Dar de baja" y, con uno dado de baja, "Reactivar"; ambos piden confirmación. La baja (`activo = 0`) se rechaza si el mecánico tiene órdenes de trabajo en un estado no final; la comprobación y el cambio se hacen en una transacción.
  - **Recepción** (`FrmRecepcion`, administrador y operador): asistente de cuatro pasos en un solo formulario, con un panel por paso y los botones Anterior, Siguiente, Cancelar y Confirmar recepción. Volver a un paso anterior no pierde lo cargado.
    1. *Vehículo*: búsqueda por patente exacta sobre vehículos activos. Muestra marca y modelo, año, color, kilometraje actual, titular y las órdenes anteriores del vehículo. No deja avanzar si la patente no existe o el vehículo está inactivo (hay que registrarlo antes en Vehículos; el asistente no crea clientes ni vehículos) ni si el vehículo ya tiene una orden en un estado no final.
    2. *Datos de ingreso*: kilometraje de ingreso (no menor al `km_actual` del vehículo ni al mayor `km_ingreso` de sus órdenes, regla 8.3), nivel de combustible, síntoma reportado (obligatorio), observaciones de recepción, mecánico (opcional) y fecha prometida de entrega (opcional; si se indica, no puede ser anterior a hoy).
    3. *Fotos*: cinco cuadros fijos (frente, trasera, lateral izquierdo, lateral derecho y tablero), cada uno con "Cargar foto", vista previa y "Quitar". Se eligen archivos JPG o PNG del disco; la foto se endereza según su orientación EXIF y se reduce a 1280 píxeles en su lado mayor. Las fotos son opcionales.
    4. *Confirmación*: resumen de solo lectura. Al confirmar, si faltan fotos se pregunta si se continúa igual, y luego se guardan en una sola transacción la orden (`nro_orden` siguiente, estado `RECEPCIONADA`, el titular de ese momento como cliente de la orden y `Sesion.IdUsuario` como usuario de alta), la primera fila de `ot_historial_estado` y una fila de `ot_foto` por cada foto cargada, en JPEG. Si algo falla no se guarda nada. La recepción no modifica `vehiculo.km_actual`.
  - **Órdenes de trabajo** (`FrmOrdenes`, administrador y operador): tablero; no escribe en la base. Sus controles usan `Anchor`, por lo que el tablero sigue el tamaño del panel de contenido: las tarjetas y los filtros se estiran a lo ancho, la grilla en los dos sentidos y la tarjeta de detalle queda a la derecha con ancho fijo.
    - Título: el subtítulo indica cuántas órdenes están listadas y cuántas de ellas están demoradas.
    - Tarjetas de estado: ocho tarjetas declaradas en el diseñador dentro de un `TableLayoutPanel` de ocho columnas iguales. `CargarTarjetas` asigna a cada una un estado de `estado_ot` en el orden de `orden_flujo`, con su cantidad de órdenes, incluidos los estados sin órdenes (número atenuado). Cuentan siempre todas las órdenes y no aplican los filtros. Un clic en una tarjeta filtra la lista por ese estado y la resalta; otro clic en la misma tarjeta quita el filtro. Reemplazan a la grilla de resumen y al combo de estado.
    - Filtros: texto contenido en la patente o en el nombre del cliente, que se aplica mientras se escribe, y casilla "Solo demoradas". "Limpiar filtros" los reinicia, quita el estado elegido y recarga. No hay botón "Buscar".
    - Grilla: una fila por orden, la más reciente primero, con número, fecha y hora de recepción, patente, vehículo (marca y modelo), cliente de la orden (`orden_trabajo.id_cliente`, no el titular actual del vehículo), mecánico ("Sin asignar" si no tiene), estado, fecha prometida y situación. Vehículo, cliente y mecánico se reparten el ancho disponible. La celda del estado se colorea según `estado_ot.codigo`, que la consulta trae en una columna oculta (`ColorEstado`).
    - Demoradas: una orden está demorada cuando su `fecha_prometida` es anterior a hoy y su estado no es final (`estado_ot.es_estado_final = 0`). La columna de situación dice "Demorada" y la fila se muestra con fondo rojo suave.
    - Detalle: al seleccionar una orden con el mouse o con el teclado, la tarjeta de la derecha muestra número, estado, patente y vehículo, cliente, mecánico, fecha de recepción, fecha prometida y total presupuestado, tomados de la fila de la grilla. Sin orden seleccionada muestra solo una ayuda y oculta el resto, incluido el botón "Gestionar orden".
    - Fotos: al seleccionar una orden se leen de `ot_foto` solo las fotos de esa orden y se muestran en cinco miniaturas, una por ángulo; el ángulo sin foto muestra "Sin foto". La consulta de la grilla nunca lee la columna `imagen`. Un clic en una miniatura con foto la abre ampliada en `FrmFoto`, una ventana modal con el ángulo como título.
    - "Gestionar orden": abre `FrmOrdenGestion` como ventana modal (`ShowDialog`) y, al cerrarla, recarga las tarjetas y la grilla. La orden queda seleccionada si sigue en la lista; si un filtro la deja fuera, el detalle vuelve a la ayuda.
  - **Gestión de la orden** (`FrmOrdenGestion`, administrador y operador): ventana modal que lleva una orden desde `RECEPCIONADA` hasta `ENTREGADA`. Recibe la orden en el campo público `IdOrdenTrabajo`. La rutina `CargarOrden` lee la orden y su estado (`estado_ot.codigo`, `permite_edicion_detalle`, `es_estado_final`), habilita los controles que corresponden a ese estado y se vuelve a ejecutar después de cada guardado.
    - Cabecera (solo lectura): número, estado, vehículo, cliente de la orden, kilometraje de ingreso, fechas de recepción, prometida, finalización y entrega, síntoma y observaciones de recepción.
    - Presupuesto (`ot_detalle`): combo de servicios activos con su precio, cantidad, "Agregar", "Actualizar cantidad" y "Quitar". Solo se edita cuando el estado tiene `permite_edicion_detalle = 1`. Cada cambio recalcula `total_presupuestado` y `total_aprobado` en la misma transacción.
    - Mecánico: combo de mecánicos activos con la opción "(sin asignar)" y "Guardar mecánico", en cualquier estado no final.
    - Cambios de estado: un botón por transición (ver la tabla siguiente). Cada uno vuelve a leer el estado dentro de su transacción, actualiza la orden e inserta una fila en `ot_historial_estado` con `Sesion.IdUsuario`.
    - Ejecución: cantidad real y horas reales de la línea aprobada seleccionada ("Guardar ejecución") y observaciones del mecánico, que se escriben con la orden en `EN_PROCESO` y se guardan al finalizar.
    - Historial: grilla de solo lectura de `ot_historial_estado`, el cambio más reciente primero.

    | Botón | Transición | Condiciones y efecto |
    |---|---|---|
    | Presupuestar | `RECEPCIONADA` → `PRESUPUESTADA` | Exige al menos una línea. |
    | Registrar aprobación | `PRESUPUESTADA` → `APROBADA` | La columna "Aprobado" de la grilla se tilda línea por línea (solo es editable en `PRESUPUESTADA`). Exige al menos una línea aprobada, pide confirmación con el total aprobado y guarda las tildes y los totales. |
    | Rechazar | `PRESUPUESTADA` → `RECHAZADA` | Pide confirmación. Deja todas las líneas sin aprobar y el total aprobado en cero. |
    | Iniciar trabajo | `APROBADA` → `EN_PROCESO` | Exige un mecánico asignado. |
    | Finalizar | `EN_PROCESO` → `FINALIZADA` | Exige cantidad real y horas reales en todas las líneas aprobadas y las observaciones del mecánico. Guarda `observaciones_mecanico` y `fecha_finalizacion = NOW()`. |
    | Entregar | `FINALIZADA` → `ENTREGADA` | Pide confirmación. Guarda `fecha_entrega = NOW()` y sube `vehiculo.km_actual` al `km_ingreso` de la orden cuando es mayor. |
    | Anular | Estado no final → `ANULADA` | Solo visible para el administrador. Exige un motivo (hasta 255 caracteres), que se guarda como observación del historial. No borra ninguna fila. |

- Pendiente en el flujo de la orden de trabajo: vista del mecánico e impresión del presupuesto o de la comanda de taller.
- Opciones de menú sin pantalla (el botón existe pero no tiene evento): Historial, Servicios, Categorías y Reportes.

### Menú por rol

Opciones del menú principal que ve cada rol (`FrmPrincipal_Load`):

| Opción del menú | ADMINISTRADOR | OPERADOR | MECANICO |
|---|---|---|---|
| Recepción | Sí | Sí | No |
| Órdenes de trabajo | Sí | Sí | No |
| Historial | Sí | Sí | Sí |
| Clientes | Sí | Sí | No |
| Vehículos | Sí | Sí | No |
| Marcas y modelos | Sí | Sí | No |
| Servicios | Sí | No | No |
| Categorías | Sí | No | No |
| Mecánicos | Sí | No | No |
| Usuarios | Sí | No | No |
| Reportes | Sí | No | No |

Los títulos de sección siguen la misma regla: "OPERACIONES" se muestra a todos los roles, "DATOS MAESTROS" al administrador y al operador, y "REPORTES" solo al administrador. Como las opciones ocultas no ocupan lugar, el menú de cada rol queda sin huecos.

## Por qué existe

Es un trabajo práctico universitario que sigue el estilo de programación orientada a eventos de la cátedra. El dominio (cliente, vehículo, orden de trabajo, presupuesto) genera de forma natural las relaciones que justifican un modelo relacional, y la consigna exige una aplicación de escritorio en dos capas con salida impresa (el presupuesto). La especificación funcional completa está en `taller-mecanico-especificacion.md`.

---

<!-- @tsg-docs:auto-start -->

## Arquitectura general

### Diagrama de paquetes

```mermaid
graph TD
    subgraph Solucion["WinFormsApp1.slnx"]
        subgraph Proyecto["WinFormsApp1.vbproj (WinExe, net10.0-windows)"]
            subgraph Acceso["Acceso"]
                Login["FrmLogin<br/>(MainForm)"]
                Seguridad["Module Seguridad<br/>(hash PBKDF2)"]
                Sesion["Module Sesion<br/>(usuario logueado)"]
            end
            subgraph Contenedor["Contenedor"]
                Principal["FrmPrincipal<br/>(menú y panel de contenido)"]
            end
            subgraph Terminadas["Pantallas terminadas"]
                Clientes["FrmClientes"]
                Vehiculos["FrmVehiculos"]
                Marcas["FrmMarcasModelos"]
                Usuarios["FrmUsuarios<br/>(solo administrador)"]
                Recepcion["FrmRecepcion<br/>(asistente de recepción)"]
                Ordenes["FrmOrdenes<br/>(tablero de órdenes)"]
            end
            subgraph Pendientes["Opciones de menú sin pantalla"]
                Pend["Historial, Servicios, Categorías,<br/>Mecánicos, Reportes"]
            end
            Conexion["Module ConexionBD<br/>(Public Const CADENA)"]
            Recursos["Recursos/<br/>fondo-login-underground.png<br/>musica-login.mp3"]
        end
        Driver["MySqlConnector 2.6.2"]
    end

    subgraph Datos["Base de datos"]
        DB[("MariaDB: taller_mecanico<br/>13 tablas")]
        Scripts["database/<br/>scripts 01 a 07"]
    end

    Login --> Principal
    Login -.-> Recursos
    Login --> Seguridad
    Login --> Sesion
    Login --> Conexion
    Principal -.-> Sesion
    Principal --> Clientes
    Principal --> Vehiculos
    Principal --> Marcas
    Principal -->|"rol ADMINISTRADOR"| Usuarios
    Principal -->|"ADMINISTRADOR y OPERADOR"| Recepcion
    Principal -->|"ADMINISTRADOR y OPERADOR"| Ordenes
    Principal -.-> Pend
    Recepcion --> Conexion
    Recepcion -.-> Sesion
    Ordenes --> Conexion
    Ordenes -.-> Sesion
    Clientes --> Conexion
    Vehiculos --> Conexion
    Marcas --> Conexion
    Usuarios --> Conexion
    Usuarios --> Seguridad
    Usuarios -.-> Sesion
    Conexion --> Driver
    Driver --> DB
    Scripts -.->|"crean y cargan"| DB
```

### Stack técnico

| Capa | Tecnología |
|---|---|
| Lenguaje | VB.NET |
| Plataforma | .NET 10 (`net10.0-windows`), Windows Forms, `OutputType` WinExe |
| Patrones | Programación orientada a eventos con SQL dentro de los formularios; un único proyecto; sin DAO ni capas (ver [Decisiones técnicas](#decisiones-técnicas)) |
| Infraestructura | Cliente de escritorio de dos capas contra MariaDB (desarrollado con 12.2; el DDL indica 10.6 o superior), driver MySqlConnector 2.6.2 |

<!-- @tsg-docs:auto-end -->

---

## Modelo de dominio

El modelo tiene **13 tablas**. `taller-mecanico.dbml` describe las 12 del modelo original; la tabla `ot_foto` (fotos de recepción) y la columna `orden_trabajo.fecha_prometida` se agregaron después y están definidas en `database/01_ddl_estructura.sql` y en la sección 7 de `taller-mecanico-especificacion.md`, no en el archivo DBML. Los scripts de `database/` implementan el modelo sobre MariaDB con InnoDB, `utf8mb4` y todas las claves foráneas en `ON UPDATE RESTRICT`. Una base creada con la versión anterior de los scripts (12 tablas) debe ejecutar `database/07_recepcion_fotos.sql`, que agrega la columna y la tabla sin tocar los datos existentes.

| Grupo | Tablas |
|---|---|
| Seguridad | `usuario` (roles `ADMINISTRADOR`, `OPERADOR`, `MECANICO`; vínculo opcional con `mecanico`; `hash_contrasena` y `salt` en Base64 para PBKDF2) |
| Clientes y vehículos | `cliente`, `marca`, `modelo`, `vehiculo`, `mecanico` |
| Catálogo de servicios | `categoria_servicio`, `servicio` |
| Orden de trabajo y presupuesto | `estado_ot`, `orden_trabajo` (incluye `fecha_prometida`, fecha prometida de entrega, opcional), `ot_detalle`, `ot_historial_estado` |
| Fotos de recepción | `ot_foto` (orden, ángulo `FRENTE`, `TRASERA`, `LATERAL_IZQUIERDO`, `LATERAL_DERECHO` o `TABLERO`, imagen JPEG en `LONGBLOB`, usuario y fecha; una sola foto por orden y ángulo; se elimina junto con su orden) |

Relaciones principales:

```mermaid
graph TD
    cliente["cliente"] -->|"1 a N"| vehiculo["vehiculo"]
    marca["marca"] -->|"1 a N"| modelo["modelo"]
    modelo -->|"1 a N"| vehiculo
    vehiculo -->|"1 a N"| orden_trabajo["orden_trabajo"]
    cliente -->|"titular al momento"| orden_trabajo
    mecanico["mecanico"] -->|"asignado"| orden_trabajo
    estado_ot["estado_ot"] --> orden_trabajo
    orden_trabajo -->|"1 a N"| ot_detalle["ot_detalle"]
    servicio["servicio"] -->|"trazabilidad"| ot_detalle
    categoria_servicio["categoria_servicio"] --> servicio
    orden_trabajo -->|"1 a N"| ot_historial_estado["ot_historial_estado"]
    orden_trabajo -->|"1 a N (una por ángulo)"| ot_foto["ot_foto"]
    usuario["usuario"] -.-> orden_trabajo
    usuario -.-> ot_historial_estado
    usuario -.-> ot_foto
    mecanico -.->|"opcional"| usuario
```

Estados de la orden de trabajo (`database/02_dml_catalogos.sql`): `RECEPCIONADA`, `PRESUPUESTADA`, `APROBADA`, `EN_PROCESO`, `FINALIZADA`, `ENTREGADA`, `RECHAZADA`, `ANULADA`. Los estados llevan ID explícito (1 a 8). La recepción busca el estado inicial por su código (`RECEPCIONADA`), la gestión de la orden busca cada estado de destino también por su código, y el tablero de órdenes los lee de la tabla, ordenados por `orden_flujo`.

Estado de implementación: la aplicación opera hoy sobre `cliente`, `vehiculo`, `marca` y `modelo`; el login lee `usuario` y el ABM de Usuarios la escribe (y lee `mecanico` solo para llenar el combo de mecánicos). La recepción escribe `orden_trabajo`, `ot_historial_estado` y `ot_foto`, y el tablero de órdenes las lee junto con `estado_ot`. La gestión de la orden escribe `ot_detalle`, `orden_trabajo` (mecánico, estado, totales, observaciones del mecánico y fechas de finalización y entrega), `ot_historial_estado` y `vehiculo.km_actual`, y lee `servicio` y `mecanico`. El ABM de Mecánicos escribe `mecanico` y lee `orden_trabajo` y `estado_ot` para rechazar la baja de un mecánico con órdenes sin cerrar. Las tablas `servicio` y `categoria_servicio` todavía no tienen pantalla de carga: sus datos provienen de `database/03_dml_prueba.sql`.

Reglas de negocio relevantes de la especificación (sección 8):

- Precios y descripciones se **copian** al detalle de la orden al presupuestar (`ot_detalle`), para que cambiar la lista de precios no altere presupuestos ya emitidos.
- El titular de cada orden se guarda en `orden_trabajo.id_cliente`; cambiar el titular del vehículo no pierde el historial (esto ya se refleja en un comentario de `FrmVehiculos.btnModificar_Click`).
- Aprobación parcial por línea (`ot_detalle.aprobado`); detalle congelado desde `APROBADA`.
- Las órdenes anuladas no se eliminan físicamente.

Reglas agregadas con la recepción y el tablero de órdenes:

- El kilometraje de ingreso no puede ser menor al `km_actual` del vehículo ni al mayor `km_ingreso` de sus órdenes anteriores (regla 8.3). `vehiculo.km_actual` no se actualiza en la recepción; la especificación lo actualiza en la entrega.
- Un vehículo con una orden en un estado no final no puede recepcionarse de nuevo.
- La fecha prometida de entrega es opcional y, si se indica, no puede ser anterior al día de la recepción.
- Las fotos de recepción son opcionales: una por ángulo, guardadas en la base como JPEG de hasta 1280 píxeles en su lado mayor.
- Una orden está demorada cuando su fecha prometida es anterior a hoy y su estado no es final (`estado_ot.es_estado_final = 0`).

Reglas agregadas con la gestión de la orden:

- "Presupuestar" es un paso explícito y exige al menos una línea. Una orden `PRESUPUESTADA` mantiene su detalle editable y no vuelve a `RECEPCIONADA`.
- El precio copiado del servicio no se edita en la línea. Un servicio va una sola vez por orden; para pedir más se cambia la cantidad. La cantidad admite dos decimales y debe ser mayor a cero.
- Subtotal de la línea = cantidad × precio unitario. `total_presupuestado` es la suma de los subtotales y `total_aprobado` la suma de los subtotales de las líneas aprobadas; los dos se escriben en un solo `UPDATE` por el CHECK `total_aprobado <= total_presupuestado`.
- La aprobación exige al menos una línea aprobada; si el cliente no aprueba ninguna, la orden se rechaza. `RECHAZADA` solo se alcanza desde `PRESUPUESTADA`.
- Para iniciar el trabajo la orden debe tener mecánico, y desde `EN_PROCESO` no puede quedar sin mecánico.
- Para finalizar, todas las líneas aprobadas deben tener cantidad real y horas reales (hasta 999,99), y las observaciones del mecánico son obligatorias.
- La anulación es exclusiva del administrador, exige un motivo y se permite en cualquier estado no final, es decir, hasta `FINALIZADA`. La orden, sus líneas y su historial se conservan.
- Las fechas de finalización y de entrega son la fecha y hora del servidor (`NOW()`). En la entrega, `vehiculo.km_actual` toma el `km_ingreso` de la orden solo cuando es mayor al valor actual.

---

## Flujo principal

Alta de un cliente desde el menú principal (el flujo de las demás pantallas terminadas es análogo).

```mermaid
sequenceDiagram
    autonumber
    actor U as Usuario
    participant L as FrmLogin
    participant P as FrmPrincipal
    participant C as FrmClientes
    participant M as ConexionBD
    participant DB as MariaDB

    U->>L: Escribe usuario y contraseña y presiona INGRESAR
    L->>DB: SELECT usuario activo por nombre_usuario
    DB-->>L: Fila con hash_contrasena y salt
    L->>L: Seguridad.VerificarClave
    alt Usuario inexistente, inactivo o clave incorrecta
        L-->>U: "Usuario o contraseña incorrectos." (sigue en el login)
    else Credenciales correctas
        L->>L: Completa Sesion con los datos del usuario
        L->>P: Abre FrmPrincipal y oculta el login
    end
    U->>P: Clic en "Clientes"
    P->>C: AbrirFormulario(New FrmClientes())
    C->>DB: SELECT clientes activos (CargarClientes)
    DB-->>C: DataTable
    C-->>U: Grilla cargada
    U->>C: Completa el formulario y presiona GUARDAR
    C->>C: ValidarCampos (nombre y documento obligatorios)
    alt Validación fallida
        C-->>U: MessageBox y Exit Sub
    else Validación correcta
        C->>M: Lee CADENA
        C->>DB: INSERT INTO cliente (parámetros)
        opt Documento repetido
            DB-->>C: Error 1062
            C-->>U: "Ya existe un cliente con ese documento."
        end
        DB-->>C: Filas afectadas
        C->>DB: SELECT clientes activos (recarga)
        C-->>U: Formulario limpio y grilla actualizada
    end
```

---

## Decisiones técnicas

| Decisión | Motivo | Fuente |
|---|---|---|
| Stack fijo: .NET 10 + VB.NET + MariaDB | Decisión del 2026-09-21. Descarta RDLC/ReportViewer, que solo funciona con .NET Framework. | `openspec/config.yaml` |
| Estilo de programación orientada a eventos de la cátedra, aplicado a propósito: `Module ConexionBD` con `Public Const CADENA`; formularios del diseñador con `Handles`; SQL dentro de los eventos con `Using cn` / `Using cmd`; grillas con `DataTable.Load`; parámetros con `AddWithValue`; validaciones que terminan en `Exit Sub`; comentarios cortos en español. | Es la convención obligatoria del curso. **No es** una arquitectura Hexagonal, MVC ni por capas. | `openspec/config.yaml` |
| Sin clases DAO ni proyecto de datos separado | La regla del estilo de cátedra reemplaza el apartado 3.2 de la especificación, que proponía `TallerMecanico.UI` y `TallerMecanico.Datos`. | `openspec/config.yaml`, `taller-mecanico-especificacion.md` |
| Driver MySqlConnector (no `MySql.Data`) | Mejor soporte asincrónico y compatibilidad con MariaDB. | `taller-mecanico-especificacion.md` (11.3), `WinFormsApp1.vbproj` |
| Baja lógica en clientes y vehículos (`activo = 0`) | Pueden tener órdenes de trabajo asociadas; no se borran. | `FrmClientes.vb`, `FrmVehiculos.vb` |
| Baja lógica con reactivación en mecánicos; la grilla muestra también los dados de baja | Las órdenes de trabajo conservan a su mecánico, y un mecánico que vuelve al taller se reactiva sin cargarlo de nuevo. | `FrmMecanicos.vb` |
| Baja física en marcas y modelos | Son catálogos; la base impide borrar si hay dependencias (error 1451) y la aplicación lo traduce a un mensaje. | `FrmMarcasModelos.vb` |
| Claves foráneas con `ON UPDATE RESTRICT` | Desde MariaDB 10.5 una columna con FK en `CASCADE` no puede usarse en un `CHECK` (error 1901). | `database/01_ddl_estructura.sql` |
| Fotos de recepción guardadas en la base (`ot_foto.imagen`, `LONGBLOB`), reducidas a 1280 píxeles y en JPEG | La base queda completa por sí sola, sin una carpeta de archivos que mantener junto a ella; la reducción limita el tamaño de cada fila. El tablero de órdenes lee las imágenes solo de la orden seleccionada. | `database/01_ddl_estructura.sql`, `FrmRecepcion.vb`, `FrmOrdenes.vb` |
| Alta de la orden en una sola transacción (`BeginTransaction`) | La orden, su primera fila de historial y sus fotos se guardan todas o ninguna, como pide la especificación. | `FrmRecepcion.vb` |
| Cada cambio del presupuesto y cada cambio de estado en una transacción que vuelve a leer el estado de la orden con `SELECT ... FOR UPDATE` | Otro puesto puede haber cambiado la orden después de cargarla en pantalla; la lectura con bloqueo evita guardar sobre un estado que ya no es el esperado. La línea, los totales, el estado y la fila de historial se guardan todos o ninguno. | `FrmOrdenGestion.vb` |
| Rutinas del formulario que reciben la conexión y la transacción (`PermiteEditarDetalle`, `LeerCodigoEstado`, `RecalcularTotales`, `CambiarEstado`) | Evitan repetir en cada botón la lectura del estado, el recálculo de totales y el cambio de estado con su fila de historial. Son rutinas del propio formulario, no clases de acceso a datos. | `FrmOrdenGestion.vb` |
| Música del login con `winmm.dll` (`mciSendStringW`) | Permite reproducir un MP3 sin dependencias adicionales. | `FrmLogin.vb` |
| Recursos del login copiados al directorio de salida (`CopyToOutputDirectory`) | La imagen y la música se leen desde `AppContext.BaseDirectory\Recursos`. | `WinFormsApp1.vbproj`, `FrmLogin.vb` |

**Decisión cerrada: hash de contraseñas.** Se adopta PBKDF2 + SHA256 como la referencia de la cátedra (100000 iteraciones, hash de 32 bytes, salt de 16 bytes, ambos en Base64, salt en su propia columna `usuario.salt`), con las clases del propio .NET (`Rfc2898DeriveBytes`), sin paquetes adicionales. La sección 11.4 de `taller-mecanico-especificacion.md` todavía menciona BCrypt (`BCrypt.Net-Next`) y queda reemplazada por esta decisión (el archivo de la especificación no se modificó). Las bases ya cargadas con hashes BCrypt se migran con `database/06_migracion_hash_pbkdf2.sql`; una instalación nueva con los scripts 01 a 03 no la necesita.

**Decisión abierta: motor de reportes.** Se descartó RDLC; la opción .NET compatible (por ejemplo QuestPDF o FastReport Open Source, según `openspec/config.yaml`) queda sin elegir.

---

## Trade-offs evaluados

| Alternativa | Ventaja | Costo | Resultado |
|---|---|---|---|
| SQL dentro de los formularios (estilo de cátedra) | Código directo, fácil de seguir en el contexto del curso; coincide con la referencia de la cátedra. | Acopla interfaz y acceso a datos, repite consultas y manejo de errores entre formularios, y dificulta las pruebas automatizadas. | Adoptado, por requisito del curso. |
| Capa de acceso a datos con clases DAO (propuesta original de la especificación 3.2) | Separa responsabilidades y facilita probar. | Contradice el estilo exigido por la cátedra. | Descartado. |
| RDLC / ReportViewer | Diseñador visual integrado. | Requiere .NET Framework 4.8. | Descartado al fijar .NET 10. |
| Cadena de conexión como constante en código | Simple, idéntica a la referencia del curso. | Mezcla credenciales con el código y obliga a ajustes locales por desarrollador. | Adoptado; riesgo registrado en el checklist. |

---

## Edge cases

Casos manejados en el código:

- **Documento duplicado** (cliente), **patente duplicada** (vehículo), **marca duplicada** y **modelo duplicado dentro de la misma marca**: se captura `MySqlException` con número 1062 y se muestra un mensaje específico.
- **Borrado con dependencias** (marcas y modelos): se captura el error 1451 y se muestra un mensaje específico en lugar del error técnico.
- **Alta con registro seleccionado**: si hay un ID cargado, GUARDAR avisa que debe usarse MODIFICAR o LIMPIAR.
- **Modificar o dar de baja sin selección**: se muestra un mensaje y se sale del evento.
- **Combos en cascada**: la opción "Seleccione una marca" (clave 0) vacía la lista de modelos y la validación rechaza la clave 0.
- **Año vacío en la base** al seleccionar un vehículo: se usa el año actual como valor por defecto.
- **Nombre de usuario duplicado** (usuarios): el error 1062 se traduce a un mensaje que aclara que puede corresponder a un usuario dado de baja; no hay reactivación desde la pantalla.
- **Usuario logueado sobre sí mismo**: no puede darse de baja ni cambiar su rol a uno distinto de administrador.
- **Rol sin mecánico o mecánico con otro rol**: el rol `MECANICO` exige elegir un mecánico; el combo de mecánico se deshabilita y se reinicia con cualquier otro rol, y `id_mecanico` se guarda como NULL.
- **Contraseña vacía al modificar un usuario**: se conserva el hash y el salt actuales; la contraseña nunca se carga en el formulario.
- **Recursos del login ausentes o MP3 que no abre**: se muestra un mensaje y la aplicación continúa.
- **Patente inexistente, vehículo inactivo o vehículo con una orden abierta** (recepción): cada caso muestra su propio mensaje y no deja pasar del paso 1. Si se cambia la patente después de buscar, el vehículo cargado se descarta.
- **Kilometraje menor al mínimo, síntoma vacío o fecha prometida pasada** (recepción): se rechazan en el paso 2 y se vuelven a validar al confirmar.
- **Archivo que no es una imagen** (recepción): se avisa y el cuadro de la foto queda como estaba.
- **Fotos faltantes** (recepción): no bloquean; al confirmar se listan las que faltan y se pregunta si se continúa.
- **Número de orden repetido** (recepción): si otro puesto registra una orden con el mismo número al mismo tiempo, el error 1062 se traduce a un mensaje, no se guarda nada y se puede confirmar de nuevo.
- **Cancelar con datos cargados** (recepción): se pide confirmación antes de descartarlos.
- **Orden sin mecánico o sin fecha prometida** (tablero de órdenes): se muestra "Sin asignar" y la fecha vacía; una orden sin fecha prometida nunca figura como demorada.
- **Orden sin fotos o con ángulos faltantes** (tablero de órdenes): cada miniatura sin foto muestra "Sin foto" y no se amplía; una imagen que no se puede leer muestra "Ilegible".
- **Orden modificada desde otro puesto** (gestión de la orden): cada guardado vuelve a leer el estado dentro de la transacción; si ya no es el esperado, no se guarda nada, se avisa y se recarga la orden.
- **Servicio repetido o dado de baja** (gestión de la orden): un servicio que ya está en el presupuesto se rechaza con el aviso de cambiar la cantidad; un servicio que dejó de estar activo tampoco se agrega.
- **Línea quitada desde otro puesto** (gestión de la orden): si "Actualizar cantidad" o "Quitar" no encuentran la línea, se avisa que ya no existe y no se guarda nada.
- **Aprobación sin líneas aprobadas o presupuesto vacío** (gestión de la orden): "Registrar aprobación" se rechaza e indica usar "Rechazar".
- **Línea no aprobada** (gestión de la orden): "Guardar ejecución" la rechaza; solo se ejecutan las líneas aprobadas.
- **Mecánico de la orden dado de baja** (gestión de la orden): se agrega al combo para poder mostrarlo.

Riesgos conocidos no cubiertos:

- Los permisos por rol se aplican solo en el menú, ocultando opciones (ver [Menú por rol](#menú-por-rol)). Las pantallas no vuelven a comprobar el rol al abrirse, salvo `FrmUsuarios`, `FrmMecanicos`, `FrmRecepcion`, `FrmOrdenes` y `FrmOrdenGestion`, y dentro de cada pantalla no hay permisos por botón, salvo "Anular" en `FrmOrdenGestion`, que es solo del administrador.
- En la gestión de la orden, las tildes de aprobación y las observaciones del mecánico viven en pantalla hasta presionar "Registrar aprobación" o "Finalizar": las tildes se pierden si antes se modifica el presupuesto o se guarda el mecánico, y las observaciones se pierden si se cierra la ventana.
- Una base creada con la versión anterior de los scripts y sin `database/07_recepcion_fotos.sql` no tiene `orden_trabajo.fecha_prometida` ni `ot_foto`: Recepción y Órdenes de trabajo muestran el error de MariaDB y no funcionan hasta ejecutar la migración.
- Las fotos se guardan dentro de la base, por lo que su tamaño y el de sus copias de respaldo crecen con cada recepción (hasta cinco imágenes por orden).
- El tablero de órdenes trae todas las órdenes que cumplen los filtros, sin paginar, y no se actualiza solo: los cambios hechos desde otro puesto se ven al cambiar un filtro, elegir una tarjeta de estado o presionar "Limpiar filtros".
- Un usuario cuyo `salt` esté vacío o cuyo hash no sea Base64 válido (por ejemplo un hash BCrypt de una base sin migrar) no puede ingresar: `VerificarClave` devuelve `False` y se muestra el mensaje genérico. En ese caso hay que ejecutar `database/06_migracion_hash_pbkdf2.sql`.
- Si la base no responde durante el login se muestra el error de MariaDB en un `MessageBox` y se permanece en el login.
- Si la base no está disponible, cada pantalla muestra el mensaje de la excepción en un `MessageBox`; no hay reintentos ni registro de errores.
- La búsqueda en vivo ejecuta una consulta por cada cambio de texto.

---

## Tests / Cobertura

No hay pruebas automatizadas: no existe proyecto de pruebas ni framework de testing en la solución. La verificación es manual, ejecutando la aplicación contra la base cargada con `database/03_dml_prueba.sql`. Los scripts `database/04_consultas_verificacion.sql` y `database/05_consultas_basicas.sql` sirven para comprobar el estado de la base, no para probar la aplicación.

Cobertura: no medida.

---

<!-- @tsg-docs:auto-start -->

## Checklist de production-readiness

| Ítem | Estado | Notas |
|---|---|---|
| Dockerfile multi-stage | ❌ | No aplica: aplicación de escritorio WinForms. |
| docker-compose para dev local | ❌ | No aplica: aplicación de escritorio WinForms. La base se crea con los scripts de `database/`. |
| Health endpoints | ❌ | No aplica: aplicación de escritorio WinForms. |
| Logging estructurado | ❌ | No implementado. Los errores se muestran con `MessageBox.Show`. |
| Tracing distribuido (OpenTelemetry) | ❌ | No aplica: aplicación de escritorio WinForms. |
| Métricas | ❌ | No aplica: aplicación de escritorio WinForms. |
| Tests E2E (Testcontainers/Playwright) | ❌ | No implementado. No hay pruebas automatizadas de ningún tipo. |
| Configuración por entorno | ❌ | No implementado. Hay una única cadena de conexión constante en `ConexionBD.vb`; no hay `appsettings` ni variables de entorno. |
| Secrets fuera del repo | ❌ | La cadena de conexión, incluida la contraseña, está en `ConexionBD.vb` y la versión confirmada está en un repositorio público. Cada desarrollador usa `git update-index --skip-worktree WinFormsApp1/ConexionBD.vb` para su valor local, lo que no retira el valor ya publicado. |
| Documentación de API (si aplica) | ❌ | No aplica: la aplicación no expone API. |

**Leyenda:** ✅ presente y configurado · ⚠️ presente pero incompleto · ❌ falta

<!-- @tsg-docs:auto-end -->

---

## Roadmap

Pendiente según el estado actual del código y `taller-mecanico-especificacion.md` (sección 13):

1. **Permisos por rol**: el login ya valida usuario y contraseña y deja el rol en `Sesion.Rol`, y el menú principal ya se filtra por rol; falta aplicar permisos por rol (administrador, operador, mecánico) dentro de cada pantalla (hoy solo Usuarios, Mecánicos, Recepción, Órdenes de trabajo y Gestión de la orden comprueban el rol al abrirse).
2. **ABM restantes**: Servicios y Categorías (los ABM de Usuarios y de Mecánicos ya están terminados).
3. **Flujo de orden de trabajo**: la recepción (asistente con fotos, que crea la orden y su primera fila de historial en una transacción), el tablero de estado de las órdenes y la gestión de la orden (líneas de presupuesto, aprobación del cliente total o parcial por línea, y cambios de estado con su fila de historial en una transacción) ya están terminados. Falta: vista del mecánico, impresión (presupuesto y comanda de taller) y consulta de historial por patente.
4. **Reportes**: elegir el motor compatible con .NET 10 y construir primero el presupuesto; después los reportes de gestión (servicios más solicitados, productividad por mecánico, órdenes abiertas, tiempos por etapa).
5. **Credenciales fuera del repositorio**: cambiar la contraseña de la base de desarrollo y mover la cadena de conexión a configuración local no versionada.
6. **Pruebas automatizadas**: no hay plan definido. _TODO: completar manualmente_

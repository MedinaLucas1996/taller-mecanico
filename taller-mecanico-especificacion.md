# Sistema de Gestión de Servicios para Taller Mecánico

**Documento de especificación funcional y técnica**

Aplicación de escritorio en dos capas — Windows Forms (.NET) + MariaDB

*Versión alineada al alcance acordado con la cátedra el 27/08*

---

## 1. Contexto y objetivo

El sistema administra el ciclo de atención de servicios de un taller mecánico: desde la recepción del vehículo hasta la entrega, pasando por el presupuesto, la aprobación del cliente y la ejecución del trabajo.

El eje del sistema es la **Orden de Trabajo (OT)**, que atraviesa una serie de estados desde que el vehículo ingresa hasta que se entrega. La OT es también el presupuesto: no son entidades separadas. Sobre ella se apoyan tanto la operación diaria como los reportes de gestión.

El dominio genera de forma natural las relaciones que justifican un modelo relacional: un cliente tiene varios vehículos, un vehículo acumula varias órdenes a lo largo del tiempo, y cada orden agrupa las líneas de servicio presupuestadas.

---

## 2. Alcance

### 2.1 Incluido

- Administración de clientes y vehículos
- Catálogo de servicios agrupados por categoría
- Administración de mecánicos y usuarios
- Recepción de vehículos y apertura de órdenes de trabajo
- Presupuestación y registro de la aprobación del cliente, incluida la aprobación parcial
- Seguimiento del estado de la orden con trazabilidad completa
- Registro de la ejecución real del trabajo
- Emisión del presupuesto impreso y reportes de gestión
- Autenticación de usuarios con tres niveles de permisos

### 2.2 Excluido

El taller desarrolla dos actividades: **venta de insumos** y **prestación de servicios**. Este sistema cubre únicamente la segunda.

Queda fuera del alcance:

- Facturación, comprobantes fiscales y formas de pago
- Venta de insumos, artículos y repuestos
- Control de stock e inventario
- Portal web o acceso remoto para clientes
- Gestión contable, de compras o de proveedores

El documento imprimible del sistema es el **presupuesto**. Los precios de los servicios se manejan como importe final, sin desglose de impuestos.

---

## 3. Arquitectura

### 3.1 Modelo de dos capas

La aplicación responde al esquema cliente-servidor clásico:

| Capa | Responsabilidad | Ubicación |
|---|---|---|
| **Presentación y lógica de negocio** | Formularios, validaciones, reglas de negocio, generación de reportes | Ejecutable Windows Forms instalado en cada estación de trabajo |
| **Datos** | Almacenamiento, integridad referencial, transacciones | Servidor MariaDB en la red local del taller |

Todas las estaciones ejecutan el mismo binario y se conectan a una única base de datos compartida.

### 3.2 Organización de la solución

Aunque la arquitectura de despliegue sea de dos capas, internamente el código se separa en dos proyectos dentro de la misma solución:

```
TallerMecanico.sln
├── TallerMecanico.UI          → Windows Forms (formularios, reportes)
└── TallerMecanico.Datos       → Class Library (clases de acceso a datos)
```

**Regla de diseño:** el proyecto de interfaz nunca instancia un objeto de conexión ni escribe SQL. Toda consulta se canaliza a través de las clases de acceso a datos (`ClienteDAO`, `VehiculoDAO`, `OrdenTrabajoDAO`, etc.).

---

## 4. Usuarios y permisos

El sistema define tres roles.

| Acción | Administrador | Operador | Mecánico |
|---|:--:|:--:|:--:|
| ABM de usuarios | ✅ | ❌ | ❌ |
| ABM de servicios y categorías | ✅ | ❌ | ❌ |
| ABM de mecánicos | ✅ | ❌ | ❌ |
| ABM de clientes y vehículos | ✅ | ✅ | ❌ |
| Recepcionar vehículo (abrir OT) | ✅ | ✅ | ❌ |
| Cargar el detalle del presupuesto | ✅ | ✅ | ❌ |
| Registrar la aprobación del cliente | ✅ | ✅ | ❌ |
| Ver las órdenes propias asignadas | ✅ | ✅ | ✅ |
| Registrar ejecución real y cierre técnico | ✅ | ✅ | ✅ |
| Entregar el vehículo | ✅ | ✅ | ❌ |
| Reportes de gestión | ✅ | ❌ | ❌ |
| Anular órdenes | ✅ | ❌ | ❌ |

### 4.1 Vínculo entre usuario y mecánico

La tabla `usuario` tiene una clave foránea opcional hacia `mecanico`. Cuando el rol es `MECANICO`, ese campo identifica a qué mecánico corresponde el usuario, lo que permite filtrar las órdenes que ve:

```sql
SELECT ... FROM orden_trabajo
WHERE id_mecanico = @idMecanico
```

Un mecánico puede existir como dato maestro sin usuario asociado. Esto cubre dos casos reales: mecánicos que no necesitan cargar su propio trabajo, y mecánicos que ya no trabajan en el taller pero deben seguir apareciendo en el historial de órdenes anteriores.

### 4.2 Autenticación

El inicio de sesión responde a dos necesidades: la **auditoría** —cada orden y cada cambio de estado registran el usuario responsable— y la **separación de permisos**, que habilita menús y acciones según el rol.

Las contraseñas se almacenan mediante función de hash, nunca en texto plano.

---

## 5. Flujo operativo

### 5.0 Preparación inicial

El administrador deja cargado el catálogo antes de operar: categorías de servicio, servicios con su precio y tiempo estimado, marcas, modelos y mecánicos. Se hace una vez y se actualiza cuando cambian los precios.

### 5.1 Recepción del vehículo

El operador busca el vehículo por patente.

- **Si existe:** el sistema recupera el vehículo, su titular y el historial de órdenes anteriores.
- **Si no existe:** se dan de alta cliente y vehículo en el momento, eligiendo marca y modelo en cascada.

Se abre la orden de trabajo registrando:

| Dato | Detalle |
|---|---|
| Número de orden | Correlativo; es el número visible del presupuesto |
| Vehículo | El vehículo que ingresa |
| Cliente | El titular **en este momento**, no el actual |
| Kilometraje | Debe ser mayor o igual al de la última orden del vehículo |
| Síntoma reportado | Texto libre, con las palabras del cliente |
| Nivel de combustible | Registro del estado al ingresar |
| Observaciones de recepción | Rayones, faltantes, estado general |
| Mecánico | Puede quedar sin asignar y completarse después |
| Fecha prometida de entrega | Opcional; no puede ser anterior al día de la recepción |
| Fotos del vehículo | Cinco ángulos: frente, trasera, lateral izquierdo, lateral derecho y tablero. Son opcionales |

**Estado resultante:** `RECEPCIONADA` · **Salida:** comanda de taller

### 5.2 Diagnóstico y presupuesto

El mecánico revisa el vehículo. El operador carga las líneas de detalle, eligiendo servicios del catálogo.

Al agregar cada línea, el sistema **copia** la descripción y el precio desde el catálogo hacia el detalle de la orden. No guarda solo la referencia al servicio.

Se calcula el subtotal de cada línea y el total presupuestado en la cabecera.

El precio copiado no se edita en la línea. Cada servicio figura una sola vez por orden: para presupuestar más unidades se cambia la cantidad de su línea.

El paso a `PRESUPUESTADA` es explícito (botón "Presupuestar") y exige al menos una línea cargada.

**Estado resultante:** `PRESUPUESTADA` · **Salida:** presupuesto impreso

### 5.3 Aprobación del cliente

Tres caminos posibles:

- **Aprobación total** → todas las líneas se marcan como aprobadas
- **Rechazo** → estado `RECHAZADA`, la orden se cierra
- **Aprobación parcial** → se marcan solo algunas líneas

La aprobación parcial es el escenario más frecuente en la operación real —"hacéme los frenos ahora, la correa el mes que viene"— y por eso el indicador de aprobación está en cada línea del detalle, no en la cabecera.

Se recalcula el total aprobado sumando únicamente las líneas marcadas.

Registrar la aprobación exige al menos una línea aprobada. Si el cliente no aprueba ninguna, la orden se rechaza.

**Estado resultante:** `APROBADA`

### 5.4 Ejecución del trabajo

El mecánico realiza las tareas aprobadas. Sobre cada línea aprobada se registra:

- **Cantidad real:** lo efectivamente ejecutado, que puede diferir de lo presupuestado
- **Horas reales:** el tiempo insumido, que se contrasta con el tiempo estimado del catálogo

Ese contraste entre estimado y real alimenta el reporte de productividad.

Para iniciar el trabajo la orden debe tener un mecánico asignado.

**Estado resultante:** `EN_PROCESO`

### 5.5 Cierre técnico

Se completan las observaciones del mecánico —lo que se hizo y las recomendaciones para el cliente— y se registra la fecha de finalización.

Para finalizar, todas las líneas aprobadas deben tener cargadas la cantidad real y las horas reales, y las observaciones del mecánico son obligatorias.

**Estado resultante:** `FINALIZADA`

### 5.6 Entrega

Se registra la fecha de entrega y se actualiza el kilometraje del vehículo con el valor de esta orden.

**Estado resultante:** `ENTREGADA`

---

## 6. Estados de la orden de trabajo

```
RECEPCIONADA → PRESUPUESTADA → APROBADA → EN_PROCESO → FINALIZADA → ENTREGADA
                     │
                     ▼
                RECHAZADA

     ANULADA  (salida disponible desde cualquier estado no final)
```

| Estado | Edita detalle | Es final |
|---|:--:|:--:|
| `RECEPCIONADA` | ✅ | ❌ |
| `PRESUPUESTADA` | ✅ | ❌ |
| `APROBADA` | ❌ | ❌ |
| `EN_PROCESO` | ❌ | ❌ |
| `FINALIZADA` | ❌ | ❌ |
| `ENTREGADA` | ❌ | ✅ |
| `RECHAZADA` | ❌ | ✅ |
| `ANULADA` | ❌ | ✅ |

Cada transición inserta una fila en la tabla de historial con estado, fecha, hora y usuario responsable. Esto habilita, sin desarrollo adicional, el reporte de tiempos por etapa y la trazabilidad completa de cada orden.

El indicador de edición de detalle controla algo concreto: hasta el estado `PRESUPUESTADA` se pueden agregar o quitar líneas; desde `APROBADA` en adelante el detalle se congela, porque el cliente ya aceptó ese presupuesto.

---

## 7. Modelo de datos

El modelo consta de **13 tablas**.

### 7.1 Datos maestros

| Entidad | Campos principales |
|---|---|
| **Usuario** | Nombre de usuario, hash de contraseña, nombre completo, rol, mecánico asociado (opcional), estado |
| **Cliente** | Razón social o nombre, documento, domicilio, localidad, teléfono, correo electrónico |
| **Vehículo** | Patente (única), titular, modelo, año, color, número de motor, número de chasis, kilometraje |
| **Marca** | Descripción |
| **Modelo** | Marca, descripción |
| **Mecánico** | Nombre completo, especialidad, teléfono, estado |
| **Categoría de servicio** | Descripción |
| **Servicio** | Código, descripción, categoría, precio, tiempo estimado en horas |
| **Estado de OT** | Código, descripción, permite edición, es final, orden de flujo |

**Sobre el documento del cliente:** se almacena como texto libre con restricción de unicidad. El sistema no distingue tipos ni aplica reglas fiscales sobre él; sirve para identificar al cliente y para imprimirlo en el presupuesto.

### 7.2 Datos transaccionales

| Tabla | Contenido |
|---|---|
| `orden_trabajo` | Cabecera: número, vehículo, cliente, mecánico, estado, fechas (incluida la fecha prometida de entrega), kilometraje, síntoma, observaciones, totales |
| `ot_detalle` | Líneas del presupuesto: servicio, descripción y precio congelados, cantidad, subtotal, aprobación, ejecución real |
| `ot_historial_estado` | Trazabilidad de los cambios de estado |
| `ot_foto` | Fotos del vehículo tomadas en la recepción: orden, ángulo, imagen, usuario, fecha y hora |

---

## 8. Reglas de negocio

### 8.1 Congelamiento de precios y descripciones

Al cargar una línea del presupuesto, la descripción y el precio se **copian** desde el catálogo de servicios hacia el detalle de la orden.

**Fundamento:** si el reporte de presupuesto obtuviera el precio mediante una consulta al catálogo, cualquier actualización posterior de la lista de precios modificaría retroactivamente el importe de todos los presupuestos ya entregados. Un documento emitido debe ser reproducible tal como se imprimió.

Esta redundancia entre `ot_detalle` y `servicio` es intencional y necesaria. La referencia al servicio se conserva únicamente para trazabilidad en los reportes.

### 8.2 Aprobación parcial

Solo las líneas marcadas como aprobadas se ejecutan y se contabilizan en el total aprobado. El indicador vive en cada línea del detalle.

### 8.3 Validación de kilometraje

El kilometraje ingresado en una orden nueva no puede ser inferior al de la última orden registrada para ese vehículo ni al kilometraje actual cargado en el vehículo.

### 8.4 Cambio de titularidad del vehículo

Cuando un vehículo cambia de dueño, no debe sobrescribirse el titular sin más, ya que se perdería la referencia de a quién se le presupuestó antes.

La solución adoptada es almacenar el cliente en cada orden de trabajo, además del titular actual en el vehículo. Así cada orden conserva su titular histórico sin necesidad de una tabla de vigencias.

### 8.5 Congelamiento del detalle tras la aprobación

Una vez que la orden pasa a estado `APROBADA`, no se pueden agregar ni quitar líneas. La validación se apoya en el indicador `permite_edicion_detalle` del estado.

### 8.6 Coherencia entre rol y mecánico asociado

Un usuario tiene mecánico asociado si y solo si su rol es `MECANICO`.

### 8.7 Anulación

Las órdenes anuladas conservan su registro y su historial. No se eliminan físicamente.

La anulación exige un motivo, que queda registrado en el historial de estados, y se permite en cualquier estado no final, es decir, hasta `FINALIZADA` inclusive.

### 8.8 Fotos de recepción

En la recepción se puede cargar una foto por cada uno de los cinco ángulos del vehículo: frente, trasera, lateral izquierdo, lateral derecho y tablero. Se aceptan archivos JPG o PNG, que se reducen a 1280 píxeles en su lado mayor y se guardan en la base de datos en formato JPEG.

Las fotos son opcionales: si falta alguna, el sistema lo advierte y permite continuar. Cada orden admite una sola foto por ángulo.

### 8.9 Órdenes demoradas

Una orden está demorada cuando su fecha prometida de entrega es anterior a la fecha actual y su estado no es final. Las órdenes sin fecha prometida no se consideran demoradas.

---

## 9. Pantallas

| Grupo | Pantalla | Rol |
|---|---|---|
| **Acceso** | Inicio de sesión | Todos |
| | Menú principal (contenedor MDI) | Todos |
| **Datos maestros** | ABM de clientes | Admin, Operador |
| | ABM de vehículos | Admin, Operador |
| | ABM de servicios | Admin |
| | ABM de categorías de servicio | Admin |
| | ABM de mecánicos | Admin |
| | ABM de usuarios | Admin |
| **Operación** | Recepción de vehículo | Admin, Operador |
| | Gestión de orden de trabajo | Admin, Operador |
| | Mis órdenes asignadas | Mecánico |
| | Consulta de historial por patente | Todos |
| **Salidas** | Menú de reportes | Admin |

Aproximadamente trece formularios, un alcance abordable en el plazo de un trabajo práctico.

**Sobre el cambio de estados:** conviene resolverlo con un botón por transición válida —"Presupuestar", "Registrar aprobación", "Iniciar trabajo", "Finalizar", "Entregar"— habilitado según el estado actual. Es más fácil de programar y de validar que un desplegable con todos los estados disponibles.

---

## 10. Reportes

| # | Reporte | Origen de datos |
|---|---|---|
| 1 | **Presupuesto** | `orden_trabajo` + `ot_detalle` + `cliente` + `vehiculo` |
| 2 | **Orden de trabajo (comanda de taller)** | `orden_trabajo` + `vehiculo` + `mecanico` |
| 3 | **Historial por patente** | `vehiculo` + `orden_trabajo` + `ot_detalle` |
| 4 | **Servicios más solicitados** | `ot_detalle` agrupado por servicio o categoría, con filtro de período |
| 5 | **Productividad por mecánico** | `orden_trabajo` agrupada por mecánico, con horas estimadas contra reales |
| 6 | **Órdenes abiertas / vehículos en taller** | `orden_trabajo` filtrada por estado |
| 7 | **Tiempos por etapa** | `ot_historial_estado` |

El reporte 1 es el que satisface el requisito de impresión de la consigna. Los restantes surgen del modelo sin desarrollo adicional significativo.

---

## 11. Consideraciones técnicas

### 11.1 Motor de reportes

Es la decisión técnica de mayor impacto, porque condiciona la versión de framework de todo el proyecto. Debe definirse antes de comenzar el desarrollo.

| Opción | Framework | Observaciones |
|---|---|---|
| **RDLC / ReportViewer** | .NET Framework 4.8 | Alternativa tradicional, con diseñador visual integrado. No está soportado en .NET moderno. |
| **QuestPDF** | .NET 8 | Generación de PDF por código, resultado muy prolijo. Licencia gratuita por debajo de cierto umbral de facturación. |
| **FastReport Open Source** | .NET 8 | Incluye diseñador visual, similar en enfoque a Crystal Reports. |

Si la cátedra espera el enfoque clásico con diseñador visual, la primera opción es la más segura.

### 11.2 Restricción de MariaDB sobre CHECK y claves foráneas

Desde MariaDB 10.5, una columna que forma parte de una clave foránea con acción referencial `CASCADE` no puede referenciarse dentro de una cláusula `CHECK`; el intento produce el error 1901.

Por ese motivo todas las claves foráneas del modelo usan `ON UPDATE RESTRICT`. Como las claves primarias son subrogadas y nunca cambian de valor, la cláusula `CASCADE` no aportaba nada en la práctica. Los `ON DELETE CASCADE` sí se mantienen, en las tablas de detalle respecto de su cabecera.

### 11.3 Conector de base de datos

Utilizar el paquete NuGet **`MySqlConnector`**, no `MySql.Data`. Ofrece mejor soporte asincrónico y mayor compatibilidad con MariaDB.

### 11.4 Seguridad de contraseñas

Almacenamiento mediante hash con `BCrypt.Net-Next` (NuGet). La implementación requiere pocas líneas y evita el almacenamiento en texto plano.

### 11.5 Transaccionalidad

Deben ejecutarse dentro de una transacción, al menos:

- La creación de la orden junto con su primera fila de historial de estado
- Todo cambio de estado junto con el registro correspondiente en el historial
- El guardado del detalle junto con el recálculo de los totales de la cabecera

---

## 12. Estado de normalización

El modelo cumple **1FN, 2FN y 3FN**, con desnormalizaciones controladas que se documentan a continuación para dejar constancia de que son decisiones de diseño y no omisiones.

### 12.1 Atributos temporales (no son violaciones de 3FN)

Los datos copiados en el detalle no constituyen redundancia: representan el valor **vigente al momento** de presupuestar, que es un hecho distinto del valor actual del dato maestro. No existe dependencia funcional desde la clave foránea, por lo tanto no hay transitividad.

- `ot_detalle.descripcion` y `ot_detalle.precio_unitario`
- `orden_trabajo.id_cliente` (titular al momento de la orden)

### 12.2 Campos calculados (desnormalización deliberada)

Derivables por cálculo, almacenados por rendimiento y por reproducibilidad del presupuesto impreso:

- `ot_detalle.subtotal` = cantidad × precio unitario
- `orden_trabajo.total_presupuestado` y `total_aprobado`

---

## 13. Próximos pasos

1. Definir el motor de reportes, ya que determina la versión de framework
2. Generar el script DDL a partir del modelo y los datos iniciales de catálogo
3. Desarrollar la capa de acceso a datos, comenzando por la clase de conexión
4. Desarrollar los formularios de ABM de datos maestros
5. Implementar el flujo de orden de trabajo con sus transiciones de estado
6. Diseñar el reporte de presupuesto
7. Desarrollar los reportes de gestión restantes

---

*Documento de especificación — Proyecto académico*

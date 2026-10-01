-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 01 - DDL: estructura de la base de datos (MariaDB >= 10.6)
-- Fuente: taller-mecanico.dbml (12 tablas, solo servicios)
--
-- ATENCIÓN: la primera sentencia elimina la base completa.
-- Sirve para reiniciar durante el desarrollo. Comentarla cuando
-- haya datos que se quieran conservar.
--
-- Convención de nombres de restricciones:
--   cp_  clave primaria        cf_  clave foránea
--   un_  valor único           ind_ índice
--   val_ validación (CHECK)
--
-- Todas las claves foráneas usan ON UPDATE RESTRICT: desde MariaDB 10.5
-- una columna con clave foránea en CASCADE no puede usarse en una
-- validación CHECK (error 1901), y las claves subrogadas nunca cambian.
-- =====================================================================

DROP DATABASE IF EXISTS taller_mecanico;

CREATE DATABASE taller_mecanico
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE taller_mecanico;

-- ---------------------------------------------------------------------
-- MECÁNICOS (se crea antes que usuario por la FK opcional)
-- ---------------------------------------------------------------------

CREATE TABLE mecanico (
    id_mecanico      INT          NOT NULL AUTO_INCREMENT,
    nombre_completo  VARCHAR(100) NOT NULL,
    especialidad     VARCHAR(80)  NULL,
    telefono         VARCHAR(30)  NULL,
    activo           BOOLEAN      NOT NULL DEFAULT TRUE,
    CONSTRAINT cp_mecanico PRIMARY KEY (id_mecanico)
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- SEGURIDAD
-- ---------------------------------------------------------------------

CREATE TABLE usuario (
    id_usuario       INT          NOT NULL AUTO_INCREMENT,
    nombre_usuario   VARCHAR(50)  NOT NULL,
    hash_contrasena  VARCHAR(255) NOT NULL COMMENT 'Hash PBKDF2-SHA256 en Base64. Nunca texto plano',
    salt             VARCHAR(100) NOT NULL COMMENT 'Salt aleatorio de 16 bytes en Base64',
    nombre_completo  VARCHAR(100) NOT NULL,
    rol              ENUM('ADMINISTRADOR', 'OPERADOR', 'MECANICO') NOT NULL,
    id_mecanico      INT          NULL COMMENT 'Solo cuando rol = MECANICO',
    activo           BOOLEAN      NOT NULL DEFAULT TRUE,
    fecha_alta       DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT cp_usuario PRIMARY KEY (id_usuario),
    CONSTRAINT un_usuario_nombre UNIQUE (nombre_usuario),
    CONSTRAINT cf_usuario_mecanico FOREIGN KEY (id_mecanico)
        REFERENCES mecanico (id_mecanico)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    -- Regla 8.6: mecánico asociado si y solo si el rol es MECANICO
    CONSTRAINT val_usuario_rol_mecanico CHECK (
        (rol = 'MECANICO' AND id_mecanico IS NOT NULL)
        OR (rol <> 'MECANICO' AND id_mecanico IS NULL)
    )
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- CLIENTES Y VEHÍCULOS
-- ---------------------------------------------------------------------

CREATE TABLE cliente (
    id_cliente     INT          NOT NULL AUTO_INCREMENT,
    razon_social   VARCHAR(150) NOT NULL COMMENT 'Nombre de la persona o de la empresa',
    documento      VARCHAR(20)  NOT NULL COMMENT 'DNI o CUIT, texto libre',
    domicilio      VARCHAR(200) NULL,
    localidad      VARCHAR(100) NULL,
    telefono       VARCHAR(30)  NULL,
    email          VARCHAR(100) NULL,
    observaciones  TEXT         NULL,
    activo         BOOLEAN      NOT NULL DEFAULT TRUE,
    fecha_alta     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT cp_cliente PRIMARY KEY (id_cliente),
    CONSTRAINT un_cliente_documento UNIQUE (documento),
    INDEX ind_cliente_razon_social (razon_social)
) ENGINE = InnoDB;

CREATE TABLE marca (
    id_marca     INT         NOT NULL AUTO_INCREMENT,
    descripcion  VARCHAR(50) NOT NULL,
    CONSTRAINT cp_marca PRIMARY KEY (id_marca),
    CONSTRAINT un_marca_descripcion UNIQUE (descripcion)
) ENGINE = InnoDB;

CREATE TABLE modelo (
    id_modelo    INT         NOT NULL AUTO_INCREMENT,
    id_marca     INT         NOT NULL,
    descripcion  VARCHAR(80) NOT NULL,
    CONSTRAINT cp_modelo PRIMARY KEY (id_modelo),
    CONSTRAINT un_modelo_marca_descripcion UNIQUE (id_marca, descripcion),
    CONSTRAINT cf_modelo_marca FOREIGN KEY (id_marca)
        REFERENCES marca (id_marca)
        ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB;

CREATE TABLE vehiculo (
    id_vehiculo    INT         NOT NULL AUTO_INCREMENT,
    patente        VARCHAR(10) NOT NULL COMMENT 'Clave natural de búsqueda',
    id_cliente     INT         NOT NULL COMMENT 'Titular ACTUAL',
    id_modelo      INT         NOT NULL,
    anio           SMALLINT    NULL,
    color          VARCHAR(30) NULL,
    nro_motor      VARCHAR(50) NULL,
    nro_chasis     VARCHAR(50) NULL,
    km_actual      INT         NOT NULL DEFAULT 0,
    observaciones  TEXT        NULL,
    activo         BOOLEAN     NOT NULL DEFAULT TRUE,
    fecha_alta     DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT cp_vehiculo PRIMARY KEY (id_vehiculo),
    CONSTRAINT un_vehiculo_patente UNIQUE (patente),
    CONSTRAINT cf_vehiculo_cliente FOREIGN KEY (id_cliente)
        REFERENCES cliente (id_cliente)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_vehiculo_modelo FOREIGN KEY (id_modelo)
        REFERENCES modelo (id_modelo)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT val_vehiculo_km CHECK (km_actual >= 0),
    CONSTRAINT val_vehiculo_anio CHECK (anio IS NULL OR anio BETWEEN 1900 AND 2100)
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- CATÁLOGO DE SERVICIOS
-- ---------------------------------------------------------------------

CREATE TABLE categoria_servicio (
    id_categoria_servicio  INT         NOT NULL AUTO_INCREMENT,
    descripcion            VARCHAR(60) NOT NULL,
    activo                 BOOLEAN     NOT NULL DEFAULT TRUE,
    CONSTRAINT cp_categoria_servicio PRIMARY KEY (id_categoria_servicio),
    CONSTRAINT un_categoria_servicio_descripcion UNIQUE (descripcion)
) ENGINE = InnoDB;

CREATE TABLE servicio (
    id_servicio            INT           NOT NULL AUTO_INCREMENT,
    codigo                 VARCHAR(30)   NOT NULL,
    descripcion            VARCHAR(200)  NOT NULL,
    id_categoria_servicio  INT           NOT NULL,
    precio                 DECIMAL(12,2) NOT NULL DEFAULT 0 COMMENT 'Importe final VIGENTE',
    tiempo_estimado_horas  DECIMAL(5,2)  NULL,
    activo                 BOOLEAN       NOT NULL DEFAULT TRUE,
    CONSTRAINT cp_servicio PRIMARY KEY (id_servicio),
    CONSTRAINT un_servicio_codigo UNIQUE (codigo),
    INDEX ind_servicio_descripcion (descripcion),
    CONSTRAINT cf_servicio_categoria FOREIGN KEY (id_categoria_servicio)
        REFERENCES categoria_servicio (id_categoria_servicio)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT val_servicio_precio CHECK (precio >= 0),
    CONSTRAINT val_servicio_tiempo CHECK (tiempo_estimado_horas IS NULL OR tiempo_estimado_horas >= 0)
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- ORDEN DE TRABAJO Y PRESUPUESTO
-- ---------------------------------------------------------------------

CREATE TABLE estado_ot (
    id_estado_ot             TINYINT     NOT NULL AUTO_INCREMENT,
    codigo                   VARCHAR(20) NOT NULL,
    descripcion              VARCHAR(50) NOT NULL,
    permite_edicion_detalle  BOOLEAN     NOT NULL DEFAULT FALSE,
    es_estado_final          BOOLEAN     NOT NULL DEFAULT FALSE,
    orden_flujo              TINYINT     NOT NULL,
    CONSTRAINT cp_estado_ot PRIMARY KEY (id_estado_ot),
    CONSTRAINT un_estado_ot_codigo UNIQUE (codigo)
) ENGINE = InnoDB;

CREATE TABLE orden_trabajo (
    id_orden_trabajo         INT           NOT NULL AUTO_INCREMENT,
    nro_orden                INT           NOT NULL COMMENT 'Número visible del presupuesto',
    id_vehiculo              INT           NOT NULL,
    id_cliente               INT           NOT NULL COMMENT 'Titular AL MOMENTO de la OT',
    id_mecanico              INT           NULL,
    id_estado_ot             TINYINT       NOT NULL,
    fecha_recepcion          DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fecha_finalizacion       DATETIME      NULL,
    fecha_entrega            DATETIME      NULL,
    km_ingreso               INT           NOT NULL COMMENT 'No menor al km de la última OT (se valida en la app)',
    nivel_combustible        ENUM('VACIO', 'UN_CUARTO', 'MEDIO', 'TRES_CUARTOS', 'LLENO') NULL,
    sintoma_reportado        TEXT          NOT NULL,
    observaciones_recepcion  TEXT          NULL,
    observaciones_mecanico   TEXT          NULL,
    total_presupuestado      DECIMAL(12,2) NOT NULL DEFAULT 0,
    total_aprobado           DECIMAL(12,2) NOT NULL DEFAULT 0,
    id_usuario_alta          INT           NOT NULL,
    CONSTRAINT cp_orden_trabajo PRIMARY KEY (id_orden_trabajo),
    CONSTRAINT un_orden_trabajo_nro UNIQUE (nro_orden),
    INDEX ind_orden_trabajo_fecha_recepcion (fecha_recepcion),
    INDEX ind_orden_trabajo_estado (id_estado_ot),
    CONSTRAINT cf_orden_trabajo_vehiculo FOREIGN KEY (id_vehiculo)
        REFERENCES vehiculo (id_vehiculo)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_orden_trabajo_cliente FOREIGN KEY (id_cliente)
        REFERENCES cliente (id_cliente)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_orden_trabajo_mecanico FOREIGN KEY (id_mecanico)
        REFERENCES mecanico (id_mecanico)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_orden_trabajo_estado FOREIGN KEY (id_estado_ot)
        REFERENCES estado_ot (id_estado_ot)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_orden_trabajo_usuario_alta FOREIGN KEY (id_usuario_alta)
        REFERENCES usuario (id_usuario)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT val_orden_trabajo_nro CHECK (nro_orden > 0),
    CONSTRAINT val_orden_trabajo_km CHECK (km_ingreso >= 0),
    CONSTRAINT val_orden_trabajo_totales CHECK (
        total_presupuestado >= 0
        AND total_aprobado >= 0
        AND total_aprobado <= total_presupuestado
    ),
    CONSTRAINT val_orden_trabajo_fechas CHECK (
        (fecha_finalizacion IS NULL OR fecha_finalizacion >= fecha_recepcion)
        AND (fecha_entrega IS NULL OR fecha_entrega >= fecha_recepcion)
    )
) ENGINE = InnoDB;

CREATE TABLE ot_detalle (
    id_ot_detalle     INT           NOT NULL AUTO_INCREMENT,
    id_orden_trabajo  INT           NOT NULL,
    id_servicio       INT           NOT NULL COMMENT 'Solo trazabilidad. El precio NO se lee de acá',
    descripcion       VARCHAR(200)  NOT NULL COMMENT 'Copiada del catálogo al presupuestar',
    cantidad          DECIMAL(12,2) NOT NULL,
    precio_unitario   DECIMAL(12,2) NOT NULL COMMENT 'CONGELADO al momento del presupuesto',
    subtotal          DECIMAL(12,2) NOT NULL,
    aprobado          BOOLEAN       NOT NULL DEFAULT FALSE,
    cantidad_real     DECIMAL(12,2) NULL,
    horas_reales      DECIMAL(5,2)  NULL,
    CONSTRAINT cp_ot_detalle PRIMARY KEY (id_ot_detalle),
    INDEX ind_ot_detalle_orden (id_orden_trabajo),
    CONSTRAINT cf_ot_detalle_orden FOREIGN KEY (id_orden_trabajo)
        REFERENCES orden_trabajo (id_orden_trabajo)
        ON DELETE CASCADE ON UPDATE RESTRICT,
    CONSTRAINT cf_ot_detalle_servicio FOREIGN KEY (id_servicio)
        REFERENCES servicio (id_servicio)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT val_ot_detalle_cantidad CHECK (cantidad > 0),
    CONSTRAINT val_ot_detalle_importes CHECK (precio_unitario >= 0 AND subtotal >= 0),
    CONSTRAINT val_ot_detalle_real CHECK (
        (cantidad_real IS NULL OR cantidad_real >= 0)
        AND (horas_reales IS NULL OR horas_reales >= 0)
    )
) ENGINE = InnoDB;

CREATE TABLE ot_historial_estado (
    id_ot_historial   INT          NOT NULL AUTO_INCREMENT,
    id_orden_trabajo  INT          NOT NULL,
    id_estado_ot      TINYINT      NOT NULL,
    fecha_hora        DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_usuario        INT          NOT NULL,
    observacion       VARCHAR(255) NULL,
    CONSTRAINT cp_ot_historial_estado PRIMARY KEY (id_ot_historial),
    INDEX ind_ot_historial_orden (id_orden_trabajo),
    CONSTRAINT cf_ot_historial_orden FOREIGN KEY (id_orden_trabajo)
        REFERENCES orden_trabajo (id_orden_trabajo)
        ON DELETE CASCADE ON UPDATE RESTRICT,
    CONSTRAINT cf_ot_historial_estado FOREIGN KEY (id_estado_ot)
        REFERENCES estado_ot (id_estado_ot)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_ot_historial_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuario (id_usuario)
        ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB;

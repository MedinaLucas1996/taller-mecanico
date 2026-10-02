-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 07 - Migración: fotos de recepción y fecha prometida de entrega
--
-- Para bases que ya se cargaron con una versión anterior de los scripts
-- 01 a 03 (12 tablas, sin fotos ni fecha prometida). Una instalación
-- nueva que ejecuta 01, 02 y 03 NO necesita este script.
--
-- Qué agrega:
--   * orden_trabajo.fecha_prometida: fecha en la que se promete entregar
--     el vehículo. Es opcional.
--   * ot_foto: fotos del vehículo tomadas en la recepción. Una foto por
--     ángulo y por orden de trabajo, guardada en la base como JPEG.
--
-- Se puede ejecutar más de una vez sin error. No modifica ni elimina
-- datos existentes: las órdenes ya cargadas quedan sin fecha prometida
-- y sin fotos.
-- =====================================================================

USE taller_mecanico;

-- La columna queda después de fecha_entrega, igual que en
-- 01_ddl_estructura.sql.
ALTER TABLE orden_trabajo
    ADD COLUMN IF NOT EXISTS fecha_prometida          DATE          NULL COMMENT 'Fecha prometida de entrega. Opcional'
        AFTER fecha_entrega;

CREATE TABLE IF NOT EXISTS ot_foto (
    id_ot_foto        INT      NOT NULL AUTO_INCREMENT,
    id_orden_trabajo  INT      NOT NULL,
    angulo            ENUM('FRENTE', 'TRASERA', 'LATERAL_IZQUIERDO', 'LATERAL_DERECHO', 'TABLERO') NOT NULL,
    imagen            LONGBLOB NOT NULL COMMENT 'JPEG reducido a 1280 px en su lado mayor',
    id_usuario        INT      NOT NULL COMMENT 'Usuario que cargó la foto',
    fecha_hora        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT cp_ot_foto PRIMARY KEY (id_ot_foto),
    -- Una sola foto por ángulo en cada orden
    CONSTRAINT un_ot_foto_orden_angulo UNIQUE (id_orden_trabajo, angulo),
    CONSTRAINT cf_ot_foto_orden FOREIGN KEY (id_orden_trabajo)
        REFERENCES orden_trabajo (id_orden_trabajo)
        ON DELETE CASCADE ON UPDATE RESTRICT,
    CONSTRAINT cf_ot_foto_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuario (id_usuario)
        ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB;

-- Verificación: se espera una fila con la columna nueva y la tabla
-- ot_foto con sus seis columnas
SELECT column_name, column_type, is_nullable
FROM information_schema.columns
WHERE table_schema = 'taller_mecanico'
  AND ((table_name = 'orden_trabajo' AND column_name = 'fecha_prometida')
       OR table_name = 'ot_foto')
ORDER BY table_name, ordinal_position;

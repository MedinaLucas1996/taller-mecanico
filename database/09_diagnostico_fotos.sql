-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 09 - Migración: diagnóstico del mecánico y fotos del trabajo
--
-- Para bases que ya se cargaron con una versión anterior de los scripts
-- (13 tablas, sin diagnóstico ni fotos del trabajo). Una instalación
-- nueva que ejecuta 01, 02 y 03 NO necesita este script. Una base que
-- tampoco tiene ot_foto ejecuta antes 07_recepcion_fotos.sql.
--
-- Qué agrega:
--   * orden_trabajo.diagnostico: lo que encontró el mecánico al revisar
--     el vehículo, antes de armar el presupuesto. Es opcional.
--   * ot_foto_trabajo: fotos que se cargan durante el trabajo, sin
--     límite por orden, cada una con una descripción opcional. Es una
--     tabla aparte de ot_foto, que guarda las cinco fotos de recepción.
--
-- Se puede ejecutar más de una vez sin error. No modifica ni elimina
-- datos existentes: las órdenes ya cargadas quedan sin diagnóstico y
-- sin fotos del trabajo.
-- =====================================================================

USE taller_mecanico;

-- La columna queda antes de observaciones_mecanico, igual que en
-- 01_ddl_estructura.sql.
ALTER TABLE orden_trabajo
    ADD COLUMN IF NOT EXISTS diagnostico              TEXT          NULL COMMENT 'Lo escribe el mecánico antes del presupuesto. Opcional'
        AFTER observaciones_recepcion;

CREATE TABLE IF NOT EXISTS ot_foto_trabajo (
    id_ot_foto_trabajo  INT          NOT NULL AUTO_INCREMENT,
    id_orden_trabajo    INT          NOT NULL,
    imagen              LONGBLOB     NOT NULL COMMENT 'JPEG reducido a 1280 px en su lado mayor',
    descripcion         VARCHAR(100) NULL COMMENT 'Qué muestra la foto. Opcional',
    id_usuario          INT          NOT NULL COMMENT 'Usuario que cargó la foto',
    fecha_hora          DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT cp_ot_foto_trabajo PRIMARY KEY (id_ot_foto_trabajo),
    -- Sin límite de fotos por orden
    INDEX ind_ot_foto_trabajo_orden (id_orden_trabajo),
    CONSTRAINT cf_ot_foto_trabajo_orden FOREIGN KEY (id_orden_trabajo)
        REFERENCES orden_trabajo (id_orden_trabajo)
        ON DELETE CASCADE ON UPDATE RESTRICT,
    CONSTRAINT cf_ot_foto_trabajo_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuario (id_usuario)
        ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB;

-- Verificación: se espera una fila con la columna nueva y la tabla
-- ot_foto_trabajo con sus seis columnas
SELECT table_name, column_name, column_type, is_nullable
FROM information_schema.columns
WHERE table_schema = 'taller_mecanico'
  AND ((table_name = 'orden_trabajo' AND column_name = 'diagnostico')
       OR table_name = 'ot_foto_trabajo')
ORDER BY table_name, ordinal_position;

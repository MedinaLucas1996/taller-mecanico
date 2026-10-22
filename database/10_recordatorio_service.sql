-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 10 - Migración: recordatorio de service
--
-- Para bases que ya se cargaron con una versión anterior de los scripts
-- (14 tablas, sin intervalos de service ni avisos). Una instalación
-- nueva que ejecuta 01, 02 y 03 NO necesita este script. Una base que
-- tampoco tiene ot_foto_trabajo ejecuta antes 09_diagnostico_fotos.sql.
--
-- Qué agrega:
--   * servicio.intervalo_meses: cada cuántos meses se repite el
--     servicio. Solo los servicios que lo tienen generan avisos.
--   * servicio.intervalo_km: cada cuántos km se repite. Es solo una
--     referencia: el sistema conoce los km cuando el auto ingresa.
--   * aviso_service: cada aviso de service enviado a un cliente
--     (vehículo, servicio, usuario y fecha).
--
-- Se puede ejecutar más de una vez sin error. No modifica ni elimina
-- datos existentes: los servicios ya cargados quedan sin intervalo
-- hasta que el administrador se lo cargue.
-- =====================================================================

USE taller_mecanico;

-- Las columnas quedan después de tiempo_estimado_horas, igual que en
-- 01_ddl_estructura.sql.
ALTER TABLE servicio
    ADD COLUMN IF NOT EXISTS intervalo_meses        SMALLINT      NULL COMMENT 'Cada cuántos meses se repite. NULL: no genera avisos'
        AFTER tiempo_estimado_horas;

ALTER TABLE servicio
    ADD COLUMN IF NOT EXISTS intervalo_km           INT           NULL COMMENT 'Cada cuántos km se repite. Solo referencia'
        AFTER intervalo_meses;

-- Las restricciones se agregan aparte: ADD CONSTRAINT no admite
-- IF NOT EXISTS en un CHECK, así que se borran antes si ya existen.
ALTER TABLE servicio DROP CONSTRAINT IF EXISTS val_servicio_intervalos;
ALTER TABLE servicio
    ADD CONSTRAINT val_servicio_intervalos CHECK (
        (intervalo_meses IS NULL OR intervalo_meses BETWEEN 1 AND 120)
        AND (intervalo_km IS NULL OR intervalo_km > 0)
    );

CREATE TABLE IF NOT EXISTS aviso_service (
    id_aviso_service  INT      NOT NULL AUTO_INCREMENT,
    id_vehiculo       INT      NOT NULL,
    id_servicio       INT      NOT NULL,
    id_usuario        INT      NOT NULL COMMENT 'Usuario que copió el mensaje',
    fecha_hora        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT cp_aviso_service PRIMARY KEY (id_aviso_service),
    -- El último aviso de cada vehículo y servicio se busca por estas columnas
    INDEX ind_aviso_service_vehiculo (id_vehiculo, id_servicio, fecha_hora),
    CONSTRAINT cf_aviso_service_vehiculo FOREIGN KEY (id_vehiculo)
        REFERENCES vehiculo (id_vehiculo)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_aviso_service_servicio FOREIGN KEY (id_servicio)
        REFERENCES servicio (id_servicio)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_aviso_service_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuario (id_usuario)
        ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB;

-- Verificación: se esperan las dos columnas nuevas de servicio y la
-- tabla aviso_service con sus cinco columnas
SELECT table_name, column_name, column_type, is_nullable
FROM information_schema.columns
WHERE table_schema = 'taller_mecanico'
  AND ((table_name = 'servicio' AND column_name IN ('intervalo_meses', 'intervalo_km'))
       OR table_name = 'aviso_service')
ORDER BY table_name, ordinal_position;

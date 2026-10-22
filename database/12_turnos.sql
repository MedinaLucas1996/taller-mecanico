-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 12 - Migración: turnos
--
-- Para bases que ya se cargaron con una versión anterior de los scripts
-- (15 tablas, sin turnos). Una instalación nueva que ejecuta 01, 02 y
-- 03 NO necesita este script. Una base que tampoco tiene aviso_service
-- ejecuta antes 10_recordatorio_service.sql.
--
-- Qué agrega:
--   * turno: reserva de un horario del taller para un vehículo. Guarda
--     el inicio de la franja, el motivo, el estado (PENDIENTE,
--     ATENDIDO, CANCELADO o AUSENTE), la orden de trabajo que generó al
--     recepcionar el vehículo y quién lo cargó.
--
-- Se puede ejecutar más de una vez sin error. No modifica ni elimina
-- datos existentes.
-- =====================================================================

USE taller_mecanico;

CREATE TABLE IF NOT EXISTS turno (
    id_turno          INT          NOT NULL AUTO_INCREMENT,
    id_vehiculo       INT          NOT NULL,
    fecha_hora        DATETIME     NOT NULL COMMENT 'Inicio de la franja reservada',
    motivo            VARCHAR(200) NULL COMMENT 'Lo que el cliente pide. Opcional',
    estado            ENUM('PENDIENTE', 'ATENDIDO', 'CANCELADO', 'AUSENTE') NOT NULL DEFAULT 'PENDIENTE',
    id_orden_trabajo  INT          NULL COMMENT 'Orden creada al recepcionar el vehículo del turno',
    id_usuario        INT          NOT NULL COMMENT 'Usuario que dio el turno',
    fecha_alta        DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT cp_turno PRIMARY KEY (id_turno),
    -- Los turnos de un día se buscan por fecha y hora; el cupo de cada
    -- franja se cuenta por este índice
    INDEX ind_turno_fecha_hora (fecha_hora),
    INDEX ind_turno_vehiculo (id_vehiculo, estado),
    CONSTRAINT cf_turno_vehiculo FOREIGN KEY (id_vehiculo)
        REFERENCES vehiculo (id_vehiculo)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_turno_orden FOREIGN KEY (id_orden_trabajo)
        REFERENCES orden_trabajo (id_orden_trabajo)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT cf_turno_usuario FOREIGN KEY (id_usuario)
        REFERENCES usuario (id_usuario)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    -- Solo un turno atendido tiene orden de trabajo, y siempre la tiene
    CONSTRAINT val_turno_orden CHECK ((estado = 'ATENDIDO') = (id_orden_trabajo IS NOT NULL))
) ENGINE = InnoDB;

-- Verificación: se espera la tabla turno con sus ocho columnas
SELECT table_name, column_name, column_type, is_nullable
FROM information_schema.columns
WHERE table_schema = 'taller_mecanico' AND table_name = 'turno'
ORDER BY ordinal_position;

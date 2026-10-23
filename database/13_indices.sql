-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 13 - Migración: índices de consulta y servicio único por orden
--
-- Para bases que ya se cargaron con una versión anterior de los scripts.
-- Una instalación nueva que ejecuta 01, 02 y 03 NO necesita este script.
--
-- Qué agrega:
--   * un_ot_detalle_orden_servicio: un servicio aparece una sola vez en
--     cada orden. La app ya lo controla al agregar la línea; la base lo
--     garantiza cuando dos puestos agregan el mismo servicio a la vez.
--   * ind_orden_trabajo_mecanico_estado: Mis órdenes busca las órdenes
--     de un mecánico por estado y las ordena por fecha prometida.
--   * ind_orden_trabajo_estado_entrega: los tableros suman lo facturado
--     por estado y fecha de entrega.
--   * ind_orden_trabajo_prometida: Inicio, Órdenes y Tableros filtran
--     las órdenes por fecha prometida.
--   * ind_orden_trabajo_finalizacion: los tableros filtran las órdenes
--     terminadas por fecha de finalización.
--   * ind_ot_historial_estado_fecha: la tasa de aprobación busca los
--     cambios a un estado dentro de un período.
--
-- Se puede ejecutar más de una vez sin error. No modifica ni elimina
-- datos existentes.
-- =====================================================================

USE taller_mecanico;

-- Antes de crear el valor único: si esta consulta devuelve filas, hay
-- órdenes con un servicio repetido y el ALTER siguiente falla. Hay que
-- unificar esas líneas a mano antes de seguir.
SELECT id_orden_trabajo, id_servicio, COUNT(*) AS veces
FROM ot_detalle
GROUP BY id_orden_trabajo, id_servicio
HAVING COUNT(*) > 1;

ALTER TABLE ot_detalle
    ADD UNIQUE KEY IF NOT EXISTS un_ot_detalle_orden_servicio (id_orden_trabajo, id_servicio);

CREATE INDEX IF NOT EXISTS ind_orden_trabajo_mecanico_estado
    ON orden_trabajo (id_mecanico, id_estado_ot, fecha_prometida);

CREATE INDEX IF NOT EXISTS ind_orden_trabajo_estado_entrega
    ON orden_trabajo (id_estado_ot, fecha_entrega);

CREATE INDEX IF NOT EXISTS ind_orden_trabajo_prometida
    ON orden_trabajo (fecha_prometida);

CREATE INDEX IF NOT EXISTS ind_orden_trabajo_finalizacion
    ON orden_trabajo (fecha_finalizacion);

CREATE INDEX IF NOT EXISTS ind_ot_historial_estado_fecha
    ON ot_historial_estado (id_estado_ot, fecha_hora);

-- Actualiza las estadísticas para que el optimizador use los índices nuevos
ANALYZE TABLE orden_trabajo, ot_detalle, ot_historial_estado;

-- Verificación: se esperan los seis índices nuevos
SELECT table_name, index_name, GROUP_CONCAT(column_name ORDER BY seq_in_index) AS columnas
FROM information_schema.statistics
WHERE table_schema = 'taller_mecanico'
  AND index_name IN ('un_ot_detalle_orden_servicio', 'ind_orden_trabajo_mecanico_estado',
                     'ind_orden_trabajo_estado_entrega', 'ind_orden_trabajo_prometida',
                     'ind_orden_trabajo_finalizacion', 'ind_ot_historial_estado_fecha')
GROUP BY table_name, index_name
ORDER BY table_name, index_name;

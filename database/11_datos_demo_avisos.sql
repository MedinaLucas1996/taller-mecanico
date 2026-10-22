-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 11 - DML: datos de demostración para los avisos de service (OPCIONAL)
--
-- Carga seis órdenes ya entregadas, más viejas que las de 08, sobre los
-- vehículos de 03 que no tienen órdenes, para que la pantalla Services
-- por vencer muestre casos de cada tipo:
--   * AB123CD: cambio de aceite hace unos siete meses (vencido).
--   * AF456GH: cambio de aceite y alineación en la misma visita, hace
--     más de un año (dos services vencidos en un solo mensaje).
--   * AD789EF: alineación hace trece meses (vencida).
--   * AG321HJ: cambio de aceite hace unos cinco meses y medio (vence
--     en los próximos 30 días).
--   * MNO456: líquido de frenos hace casi dos años (vence en los
--     próximos 30 días).
--   * AE987KL: cambio de aceite hace dos meses (todavía no aparece).
--
-- Requisitos: ejecutar después de 01, 02, 03 y 10 (usa los usuarios, un
-- mecánico activo y los intervalos del catálogo de servicios).
--
-- Las fechas son relativas al día en que se ejecuta el script. Se puede
-- ejecutar más de una vez: las órdenes llevan la marca [demo-avisos] al
-- final de las observaciones de recepción y se borran al empezar, junto
-- con los avisos registrados para estos vehículos.
-- =====================================================================

USE taller_mecanico;

-- ---------------------------------------------------------------------
-- BORRADO DE UNA CARGA ANTERIOR
-- ---------------------------------------------------------------------

DELETE FROM aviso_service
WHERE id_vehiculo IN (SELECT id_vehiculo FROM vehiculo
                      WHERE patente IN ('AB123CD', 'AF456GH', 'AD789EF', 'AG321HJ', 'MNO456', 'AE987KL'));

DELETE FROM orden_trabajo WHERE observaciones_recepcion LIKE '%[demo-avisos]';

SET @hoy = CURDATE();
SET @u_admin = (SELECT id_usuario FROM usuario WHERE nombre_usuario = 'admin');
SET @u_oper = (SELECT id_usuario FROM usuario WHERE nombre_usuario = 'operador');
SET @mecanico = (SELECT MIN(id_mecanico) FROM mecanico WHERE activo = 1);
SET @base = (SELECT COALESCE(MAX(nro_orden), 0) FROM orden_trabajo);

-- ---------------------------------------------------------------------
-- ÓRDENES
-- dias: días antes de hoy en que se recepcionó; la orden pasa por cada
-- estado en el mismo día y se entrega seis horas después
-- ---------------------------------------------------------------------

DROP TEMPORARY TABLE IF EXISTS demo_aviso_orden;
CREATE TEMPORARY TABLE demo_aviso_orden (
    n INT, patente VARCHAR(10), dias INT, km INT, sintoma TEXT, recepcion DATETIME NULL
);

INSERT INTO demo_aviso_orden (n, patente, dias, km, sintoma) VALUES
    (1, 'AB123CD', 205, 85200,  'Service de aceite y filtro por kilometraje.'),
    (2, 'AF456GH', 390, 176400, 'Service de aceite y el volante tira hacia la derecha.'),
    (3, 'AD789EF', 400, 64100,  'El volante tira hacia la izquierda.'),
    (4, 'AG321HJ', 168, 139500, 'Service de aceite y filtro por kilometraje.'),
    (5, 'MNO456',  715, 196800, 'El pedal de freno se siente esponjoso.'),
    (6, 'AE987KL', 60,  15900,  'Service de aceite y filtro por kilometraje.');

UPDATE demo_aviso_orden SET recepcion = TIMESTAMP(DATE_SUB(@hoy, INTERVAL dias DAY), '09:00:00');

INSERT INTO orden_trabajo
    (nro_orden, id_vehiculo, id_cliente, id_mecanico, id_estado_ot, fecha_recepcion,
     fecha_finalizacion, fecha_entrega, fecha_prometida, km_ingreso, nivel_combustible,
     sintoma_reportado, observaciones_recepcion, observaciones_mecanico,
     total_presupuestado, total_aprobado, id_usuario_alta)
SELECT @base + d.n, v.id_vehiculo, v.id_cliente, @mecanico, 6, d.recepcion,
       d.recepcion + INTERVAL 300 MINUTE, d.recepcion + INTERVAL 360 MINUTE, DATE(d.recepcion),
       d.km, 'MEDIO', d.sintoma, 'Vehículo recibido sin daños visibles. [demo-avisos]',
       'Trabajo realizado, sin novedades.', 0, 0, @u_oper
FROM demo_aviso_orden AS d
JOIN vehiculo AS v ON v.patente = d.patente;

-- ---------------------------------------------------------------------
-- LÍNEAS: todas aprobadas, con el precio y las horas del catálogo
-- ---------------------------------------------------------------------

DROP TEMPORARY TABLE IF EXISTS demo_aviso_linea;
CREATE TEMPORARY TABLE demo_aviso_linea (n INT, codigo VARCHAR(30));

INSERT INTO demo_aviso_linea (n, codigo) VALUES
    (1, 'MEC-ACE-01'),
    (2, 'MEC-ACE-01'),
    (2, 'SUS-ALI-01'),
    (3, 'SUS-ALI-01'),
    (4, 'MEC-ACE-01'),
    (5, 'FRE-LIQ-01'),
    (6, 'MEC-ACE-01');

INSERT INTO ot_detalle
    (id_orden_trabajo, id_servicio, descripcion, cantidad, precio_unitario, subtotal, aprobado, horas_reales)
SELECT o.id_orden_trabajo, s.id_servicio, s.descripcion, 1, s.precio, s.precio, TRUE,
       COALESCE(s.tiempo_estimado_horas, 1)
FROM demo_aviso_linea AS l
JOIN orden_trabajo AS o ON o.nro_orden = @base + l.n
JOIN servicio AS s ON s.codigo = l.codigo;

-- Totales calculados con las líneas, igual que en la aplicación
UPDATE orden_trabajo AS o
SET o.total_presupuestado = (SELECT COALESCE(SUM(d.subtotal), 0) FROM ot_detalle AS d
                             WHERE d.id_orden_trabajo = o.id_orden_trabajo),
    o.total_aprobado = (SELECT COALESCE(SUM(d.subtotal), 0) FROM ot_detalle AS d
                        WHERE d.id_orden_trabajo = o.id_orden_trabajo AND d.aprobado = 1)
WHERE o.observaciones_recepcion LIKE '%[demo-avisos]';

-- ---------------------------------------------------------------------
-- HISTORIAL DE ESTADOS: una fila por cada estado, de RECEPCIONADA a
-- ENTREGADA
-- ---------------------------------------------------------------------

INSERT INTO ot_historial_estado (id_orden_trabajo, id_estado_ot, fecha_hora, id_usuario, observacion)
SELECT o.id_orden_trabajo, x.id_estado_ot, d.recepcion + INTERVAL x.minutos MINUTE, x.id_usuario, x.observacion
FROM demo_aviso_orden AS d
JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
JOIN (SELECT 1 AS id_estado_ot, 0 AS minutos, @u_oper AS id_usuario, 'Recepción del vehículo' AS observacion
      UNION ALL SELECT 2, 60, @u_admin, 'Presupuesto cargado'
      UNION ALL SELECT 3, 120, @u_oper, 'Aprobación del cliente registrada'
      UNION ALL SELECT 4, 180, @u_admin, 'Inicio del trabajo'
      UNION ALL SELECT 5, 300, @u_admin, 'Trabajo finalizado'
      UNION ALL SELECT 6, 360, @u_oper, 'Vehículo entregado al cliente') AS x;

DROP TEMPORARY TABLE IF EXISTS demo_aviso_orden;
DROP TEMPORARY TABLE IF EXISTS demo_aviso_linea;

-- Verificación: se esperan seis órdenes entregadas, siete líneas y
-- treinta y seis filas de historial
SELECT COUNT(*) AS ordenes,
       (SELECT COUNT(*) FROM ot_detalle AS d JOIN orden_trabajo AS o2 ON o2.id_orden_trabajo = d.id_orden_trabajo
        WHERE o2.observaciones_recepcion LIKE '%[demo-avisos]') AS lineas,
       (SELECT COUNT(*) FROM ot_historial_estado AS h JOIN orden_trabajo AS o3 ON o3.id_orden_trabajo = h.id_orden_trabajo
        WHERE o3.observaciones_recepcion LIKE '%[demo-avisos]') AS historial
FROM orden_trabajo AS o
WHERE o.observaciones_recepcion LIKE '%[demo-avisos]';

-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 08 - DML: datos de demostración para los tableros (OPCIONAL)
--
-- Carga unas 40 órdenes de trabajo repartidas en los últimos siete
-- meses, con algunos clientes y vehículos nuevos, para que la pantalla
-- Tableros muestre indicadores y gráficos con datos. No es necesario
-- para que el sistema funcione: sirve para la presentación.
--
-- Requisitos: ejecutar después de 01, 02 y 03 (usa los usuarios admin,
-- operador y mecanico, los mecánicos y el catálogo de servicios).
--
-- Las fechas son relativas al día en que se ejecuta el script: la orden
-- más vieja queda unos 200 días atrás y la más nueva, ayer. Así los
-- datos siempre caen en los últimos meses.
--
-- Cada orden se carga igual que desde la aplicación: líneas copiadas del
-- catálogo con el precio congelado, totales calculados con las líneas,
-- una fila de historial por cada estado alcanzado y horas reales en las
-- líneas aprobadas desde que el trabajo empezó. No carga fotos.
--
-- Se puede ejecutar más de una vez: las órdenes de demostración llevan
-- la marca [demo] al final de las observaciones de recepción y se
-- borran al empezar (sus líneas e historial se borran en cascada).
-- Los clientes, vehículos y el mecánico nuevos se agregan solo si no
-- existen. Los números de orden siguen después del último existente.
--
-- Para quitar los datos de demostración alcanza con:
--   DELETE FROM orden_trabajo WHERE observaciones_recepcion LIKE '%[demo]';
-- =====================================================================

USE taller_mecanico;

-- ---------------------------------------------------------------------
-- BORRADO DE UNA CARGA ANTERIOR
-- ---------------------------------------------------------------------

DELETE FROM orden_trabajo WHERE observaciones_recepcion LIKE '%[demo]';

-- ---------------------------------------------------------------------
-- CLIENTES, VEHÍCULOS Y MECÁNICO NUEVOS (solo si no existen)
-- ---------------------------------------------------------------------

DROP TEMPORARY TABLE IF EXISTS demo_cliente;
CREATE TEMPORARY TABLE demo_cliente (
    razon_social VARCHAR(150), documento VARCHAR(20), domicilio VARCHAR(200),
    localidad VARCHAR(100), telefono VARCHAR(30), email VARCHAR(100)
);

INSERT INTO demo_cliente VALUES
    ('Carlos Alberto Duarte', '27845123', 'Av. López Torres 2210', 'Posadas', '3764410201', 'cduarte@correo.com'),
    ('Gabriela Noemí Vera', '31258963', 'Santa Fe 1450', 'Posadas', '3764410202', 'gvera@correo.com'),
    ('Distribuidora Misiones S.A.', '30-70987654-1', 'Av. Quaranta 4500', 'Posadas', '3764410203', 'flota@dismisiones.com'),
    ('Martín Ezequiel Ojeda', '35741852', 'Av. San Martín 980', 'Garupá', '3764410204', NULL),
    ('Rosa Elena Krause', '24369874', 'Brasil 455', 'Oberá', '3755410205', 'rkrause@correo.com'),
    ('Pablo Andrés Giménez', '33102457', 'Mitre 1675', 'Candelaria', '3764410206', 'pgimenez@correo.com'),
    ('Agro Servicios del Alto Paraná S.R.L.', '30-71555888-4', 'Ruta 12 km 1520', 'Eldorado', '3751410207', 'admin@agroap.com'),
    ('Natalia Soledad Benítez', '38965412', 'Junín 2340', 'Posadas', '3764410208', 'nbenitez@correo.com');

INSERT INTO cliente (razon_social, documento, domicilio, localidad, telefono, email)
SELECT d.razon_social, d.documento, d.domicilio, d.localidad, d.telefono, d.email
FROM demo_cliente AS d
WHERE NOT EXISTS (SELECT 1 FROM cliente AS c WHERE c.documento = d.documento);

DROP TEMPORARY TABLE IF EXISTS demo_vehiculo;
CREATE TEMPORARY TABLE demo_vehiculo (
    patente VARCHAR(10), documento VARCHAR(20), marca VARCHAR(50), modelo VARCHAR(80),
    anio SMALLINT, color VARCHAR(30)
);

INSERT INTO demo_vehiculo VALUES
    ('AF210KP', '27845123', 'Toyota', 'Corolla', 2019, 'Blanco'),
    ('AE517RT', '31258963', 'Volkswagen', 'Gol', 2017, 'Gris'),
    ('AG034LM', '30-70987654-1', 'Renault', 'Kangoo', 2021, 'Blanco'),
    ('AG035LM', '30-70987654-1', 'Fiat', 'Strada', 2022, 'Blanco'),
    ('AD882WS', '30-70987654-1', 'Toyota', 'Hilux', 2020, 'Gris'),
    ('OPQ318', '35741852', 'Chevrolet', 'Onix', 2015, 'Rojo'),
    ('AB447TY', '24369874', 'Ford', 'Focus', 2017, 'Azul'),
    ('AF903NB', '33102457', 'Fiat', 'Cronos', 2021, 'Negro'),
    ('AH120CC', '30-71555888-4', 'Volkswagen', 'Amarok', 2023, 'Blanco'),
    ('AE640DF', '30-71555888-4', 'Ford', 'Ranger', 2018, 'Gris'),
    ('AH256XZ', '38965412', 'Chevrolet', 'Tracker', 2024, 'Blanco'),
    ('AC771PL', '27845123', 'Renault', 'Sandero', 2016, 'Gris');

-- El modelo se busca por marca y descripción (cargados en 03)
INSERT INTO vehiculo (patente, id_cliente, id_modelo, anio, color, km_actual)
SELECT d.patente, c.id_cliente, mo.id_modelo, d.anio, d.color, 0
FROM demo_vehiculo AS d
JOIN cliente AS c ON c.documento = d.documento
JOIN marca AS ma ON ma.descripcion = d.marca
JOIN modelo AS mo ON mo.id_marca = ma.id_marca AND mo.descripcion = d.modelo
WHERE NOT EXISTS (SELECT 1 FROM vehiculo AS v WHERE v.patente = d.patente);

INSERT INTO mecanico (nombre_completo, especialidad, telefono, activo)
SELECT 'Diego Martínez', 'Electricidad y diagnóstico', '3764400104', TRUE FROM DUAL
WHERE NOT EXISTS (SELECT 1 FROM mecanico WHERE nombre_completo = 'Diego Martínez');

-- ---------------------------------------------------------------------
-- USUARIOS Y MECÁNICOS QUE SE USAN EN EL HISTORIAL
-- ---------------------------------------------------------------------

SET @hoy = CURDATE();
SET @u_admin = (SELECT id_usuario FROM usuario WHERE nombre_usuario = 'admin');
SET @u_oper  = COALESCE((SELECT id_usuario FROM usuario WHERE nombre_usuario = 'operador'), @u_admin);
SET @u_mec   = COALESCE((SELECT id_usuario FROM usuario WHERE nombre_usuario = 'mecanico'), @u_admin);

DROP TEMPORARY TABLE IF EXISTS demo_mecanico;
CREATE TEMPORARY TABLE demo_mecanico (clave CHAR(1), id_mecanico INT, id_usuario INT);

-- R: Ramón Gómez (tiene usuario propio), L: Lucas Benítez,
-- D: Diego Martínez, H: Hugo Fernández (ya inactivo, trabajó en las
-- órdenes más viejas). Si alguno no existe se usa Ramón Gómez.
SET @m_ramon = (SELECT id_mecanico FROM mecanico WHERE nombre_completo = 'Ramón Gómez');
INSERT INTO demo_mecanico VALUES
    ('R', @m_ramon, @u_mec),
    ('L', COALESCE((SELECT id_mecanico FROM mecanico WHERE nombre_completo = 'Lucas Benítez'), @m_ramon), @u_admin),
    ('D', COALESCE((SELECT id_mecanico FROM mecanico WHERE nombre_completo = 'Diego Martínez'), @m_ramon), @u_admin),
    ('H', COALESCE((SELECT id_mecanico FROM mecanico WHERE nombre_completo = 'Hugo Fernández'), @m_ramon), @u_admin);

-- ---------------------------------------------------------------------
-- ÓRDENES DE TRABAJO
-- dias: días antes de hoy en que se recepcionó
-- m2 a m8: minutos desde la recepción en que la orden pasó a cada
--          estado (2 PRESUPUESTADA ... 8 ANULADA), NULL si no pasó
-- prometida: días después de la recepción (fecha prometida)
-- ajuste: precios más bajos en los meses anteriores (inflación)
-- ---------------------------------------------------------------------

DROP TEMPORARY TABLE IF EXISTS demo_orden;
CREATE TEMPORARY TABLE demo_orden (
    n INT, patente VARCHAR(10), dias INT, hora TIME, estado TINYINT, mecanico CHAR(1),
    prometida INT, km INT, nivel VARCHAR(20), sintoma TEXT, obs_mecanico TEXT, motivo VARCHAR(255),
    ajuste DECIMAL(4,3), m2 INT, m3 INT, m4 INT, m5 INT, m6 INT, m7 INT, m8 INT,
    recepcion DATETIME NULL
);

INSERT INTO demo_orden (n, patente, dias, hora, estado, mecanico, prometida, km, nivel, sintoma, obs_mecanico, motivo,
                        ajuste, m2, m3, m4, m5, m6, m7, m8) VALUES
    (1, 'AF210KP', 198, '08:01:00', 6, 'H', 2, 61824, 'MEDIO', 'Se encendió la luz de check engine.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.830, 1525, 2313, 2484, 3451, 5889, NULL, NULL),
    (2, 'AE517RT', 193, '11:21:00', 6, 'R', 2, 98885, 'MEDIO', 'Vibra el pedal al frenar.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.830, 1391, 2500, 3786, 7345, 9057, NULL, NULL),
    (3, 'AG034LM', 186, '10:58:00', 7, NULL, NULL, 74682, 'UN_CUARTO', 'Golpe en la suspensión delantera en los pozos.', NULL, NULL, 0.830, 659, NULL, NULL, NULL, NULL, 2473, NULL),
    (4, 'AG035LM', 180, '12:24:00', 6, 'L', 4, 51203, 'UN_CUARTO', 'Ruido metálico al frenar.', 'Trabajo realizado, se recomienda control en 10.000 km.', NULL, 0.832, 658, 2109, 2961, 3395, 5137, NULL, NULL),
    (5, 'AD882WS', 172, '10:56:00', 6, 'H', 3, 112528, 'LLENO', 'Ruido en el tren delantero al girar.', 'Trabajo realizado, se recomienda control en 10.000 km.', NULL, 0.839, 423, 920, 1835, 2671, 4857, NULL, NULL),
    (6, 'OPQ318', 165, '09:06:00', 6, 'R', 2, 139808, 'UN_CUARTO', 'Ruido metálico al frenar.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.846, 527, 787, 870, 4316, 6660, NULL, NULL),
    (7, 'AB447TY', 158, '10:35:00', 8, NULL, NULL, 104077, 'TRES_CUARTOS', 'Golpe en la suspensión delantera en los pozos.', NULL, 'El cliente retiró el vehículo sin hacer el trabajo.', 0.853, NULL, NULL, NULL, NULL, NULL, NULL, 5566),
    (8, 'AF903NB', 151, '08:31:00', 6, 'L', 4, 38281, 'UN_CUARTO', 'El volante tira hacia la derecha.', 'Se probó el vehículo en calle, funciona correctamente.', NULL, 0.859, 381, 696, 1341, 4856, 7563, NULL, NULL),
    (9, 'AH120CC', 144, '08:29:00', 6, 'R', 3, 27744, 'TRES_CUARTOS', 'Golpe en la suspensión delantera en los pozos.', 'Trabajo realizado, se recomienda control en 10.000 km.', NULL, 0.866, 1168, 1874, 3185, 6686, 9245, NULL, NULL),
    (10, 'AE640DF', 137, '09:04:00', 6, 'D', 2, 156444, 'MEDIO', 'El pedal de freno se siente esponjoso.', 'Se reemplazaron las piezas indicadas, sin novedades.', NULL, 0.872, 303, 1506, 1593, 4884, 5848, NULL, NULL),
    (11, 'AH256XZ', 129, '08:56:00', 7, NULL, 3, 9260, 'UN_CUARTO', 'El volante tira hacia la derecha.', NULL, NULL, 0.880, 1293, NULL, NULL, NULL, NULL, 3766, NULL),
    (12, 'AC771PL', 122, '08:12:00', 6, 'L', 5, 131415, 'MEDIO', 'Se encendió la luz de check engine.', 'Trabajo realizado, se recomienda control en 10.000 km.', NULL, 0.886, 1339, 2989, 3623, 5833, 7301, NULL, NULL),
    (13, 'AF210KP', 115, '11:09:00', 6, 'R', 3, 67000, 'TRES_CUARTOS', 'Pierde líquido refrigerante.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.893, 1505, 2297, 2906, 3124, 4961, NULL, NULL),
    (14, 'AE517RT', 108, '10:09:00', 6, 'D', 5, 104985, 'VACIO', 'El pedal de freno se siente esponjoso.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.899, 1143, 2943, 3011, 6123, 8548, NULL, NULL),
    (15, 'AG034LM', 101, '11:44:00', 6, 'L', 2, 81435, 'TRES_CUARTOS', 'Ruido metálico al frenar.', 'Trabajo realizado, se recomienda control en 10.000 km.', NULL, 0.906, 1548, 2353, 2933, 3845, 4377, NULL, NULL),
    (16, 'AG035LM', 94, '10:50:00', 8, NULL, 3, 57927, 'UN_CUARTO', 'Service de aceite y filtro por kilometraje.', NULL, 'Orden cargada por error, duplicada.', 0.912, 860, NULL, NULL, NULL, NULL, NULL, 4380),
    (17, 'AD882WS', 88, '09:50:00', 6, 'R', 3, 117099, 'TRES_CUARTOS', 'Ruido metálico al frenar.', 'Se reemplazaron las piezas indicadas, sin novedades.', NULL, 0.918, 1468, 1838, 2542, 5760, 7053, NULL, NULL),
    (18, 'OPQ318', 81, '11:26:00', 6, 'D', 5, 145765, 'VACIO', 'Corresponde cambio de distribución por kilometraje.', 'Se reemplazaron las piezas indicadas, sin novedades.', NULL, 0.924, 1072, 3262, 3725, 4924, 6567, NULL, NULL),
    (19, 'AB447TY', 74, '08:53:00', 6, 'L', 3, 112872, 'UN_CUARTO', 'Vibra el pedal al frenar.', 'Se reemplazaron las piezas indicadas, sin novedades.', NULL, 0.931, 301, 1227, 1959, 5227, 5624, NULL, NULL),
    (20, 'AF903NB', 67, '08:26:00', 6, 'R', 6, 43252, 'UN_CUARTO', 'Ruido en el tren delantero al girar.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.937, 662, 2222, 3185, 4093, 4663, NULL, NULL),
    (21, 'AH120CC', 60, '11:56:00', 7, NULL, 3, 31950, 'TRES_CUARTOS', 'Le cuesta arrancar a la mañana.', NULL, NULL, 0.944, 756, NULL, NULL, NULL, NULL, 3684, NULL),
    (22, 'AE640DF', 53, '08:29:00', 6, 'D', 5, 160675, 'UN_CUARTO', 'Golpe en la suspensión delantera en los pozos.', 'Se probó el vehículo en calle, funciona correctamente.', NULL, 0.951, 373, 638, 1492, 3315, 5546, NULL, NULL),
    (23, 'AH256XZ', 46, '09:03:00', 6, 'L', 6, 15798, 'UN_CUARTO', 'El embrague patina en las subidas.', 'Trabajo realizado, se recomienda control en 10.000 km.', NULL, 0.957, 581, 2146, 2634, 5646, 7573, NULL, NULL),
    (24, 'AC771PL', 39, '08:26:00', 6, 'R', 5, 138466, 'LLENO', 'El volante tira hacia la derecha.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.964, 765, 2775, 2835, 3122, 5659, NULL, NULL),
    (25, 'AF210KP', 32, '08:26:00', 6, 'D', NULL, 70703, 'UN_CUARTO', 'Pierde líquido refrigerante.', 'Se probó el vehículo en calle, funciona correctamente.', NULL, 0.970, 450, 1649, 2779, 4445, 6273, NULL, NULL),
    (26, 'AE517RT', 27, '09:34:00', 6, 'L', 4, 108142, 'LLENO', 'Service de aceite y filtro por kilometraje.', 'Se probó el vehículo en calle, funciona correctamente.', NULL, 0.975, 1303, 4054, 4188, 7307, 8332, NULL, NULL),
    (27, 'AG034LM', 24, '11:17:00', 6, 'R', 4, 90797, 'VACIO', 'Tironea al acelerar y consume más combustible.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.978, 409, 2619, 3146, 3677, 4302, NULL, NULL),
    (28, 'AG035LM', 21, '08:24:00', 6, 'D', 4, 62597, 'LLENO', 'Se encendió la luz de check engine.', 'Se reemplazaron las piezas indicadas, sin novedades.', NULL, 0.980, 592, 2401, 3301, 5924, 8440, NULL, NULL),
    (29, 'AD882WS', 19, '09:38:00', 4, 'R', 2, 122779, 'LLENO', 'Pierde líquido refrigerante.', NULL, NULL, 0.982, 588, 1347, 2146, NULL, NULL, NULL, NULL),
    (30, 'OPQ318', 16, '11:14:00', 6, 'L', 2, 155889, 'MEDIO', 'Service de aceite y filtro por kilometraje.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.985, 505, 3185, 3305, 4283, 6155, NULL, NULL),
    (31, 'AB447TY', 14, '11:50:00', 4, 'D', 4, 121449, 'LLENO', 'Golpe en la suspensión delantera en los pozos.', NULL, NULL, 0.987, 156, 1043, 2385, NULL, NULL, NULL, NULL),
    (32, 'AF903NB', 12, '11:16:00', 8, 'L', 3, 53375, 'UN_CUARTO', 'Tironea al acelerar y consume más combustible.', NULL, 'El cliente decidió hacer el trabajo en el concesionario.', 0.989, 373, 686, 1691, NULL, NULL, NULL, 4562),
    (33, 'AH120CC', 11, '09:37:00', 3, 'R', 4, 38727, 'UN_CUARTO', 'Se encendió la luz de check engine.', NULL, NULL, 0.990, 1251, 1692, NULL, NULL, NULL, NULL, NULL),
    (34, 'AE640DF', 9, '10:42:00', 6, 'L', 2, 168560, 'LLENO', 'Service de aceite y filtro por kilometraje.', 'Trabajo realizado, se recomienda control en 10.000 km.', NULL, 0.992, 180, 1400, 2302, 5899, 7436, NULL, NULL),
    (35, 'AH256XZ', 8, '09:38:00', 2, NULL, 3, 21929, 'LLENO', 'Pierde líquido refrigerante.', NULL, NULL, 0.993, 1448, NULL, NULL, NULL, NULL, NULL, NULL),
    (36, 'AC771PL', 6, '11:52:00', 4, 'D', 8, 144338, 'TRES_CUARTOS', 'Pierde líquido refrigerante.', NULL, NULL, 0.994, 1140, 3808, 5169, NULL, NULL, NULL, NULL),
    (37, 'AF210KP', 5, '11:51:00', 5, 'R', 3, 86260, 'MEDIO', 'Service de aceite y filtro por kilometraje.', 'Trabajo realizado, se recomienda control en 10.000 km.', NULL, 0.995, 745, 3338, 3822, 4659, NULL, NULL, NULL),
    (38, 'AE517RT', 4, '11:11:00', 5, 'L', 6, 111614, 'LLENO', 'El pedal de freno se siente esponjoso.', 'Se detectó desgaste adicional, avisado al cliente.', NULL, 0.996, 866, 2003, 2627, 5029, NULL, NULL, NULL),
    (39, 'AG034LM', 2, '09:35:00', 2, NULL, 6, 97015, 'VACIO', 'Ruido metálico al frenar.', NULL, NULL, 0.998, 1403, NULL, NULL, NULL, NULL, NULL, NULL),
    (40, 'AG035LM', 1, '09:19:00', 1, NULL, 4, 67495, 'VACIO', 'Golpe en la suspensión delantera en los pozos.', NULL, NULL, 0.999, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

UPDATE demo_orden SET recepcion = TIMESTAMP(DATE_SUB(@hoy, INTERVAL dias DAY), hora);

-- Los números de orden siguen después del último existente
SET @base = (SELECT COALESCE(MAX(nro_orden), 0) FROM orden_trabajo);

-- El mecánico se asigna desde que la orden se aprueba (m3)
INSERT INTO orden_trabajo
    (nro_orden, id_vehiculo, id_cliente, id_mecanico, id_estado_ot, fecha_recepcion,
     fecha_finalizacion, fecha_entrega, fecha_prometida, km_ingreso, nivel_combustible,
     sintoma_reportado, observaciones_recepcion, observaciones_mecanico,
     total_presupuestado, total_aprobado, id_usuario_alta)
SELECT @base + d.n, v.id_vehiculo, v.id_cliente,
       CASE WHEN d.m3 IS NULL THEN NULL ELSE dm.id_mecanico END,
       d.estado, d.recepcion,
       CASE WHEN d.m5 IS NULL THEN NULL ELSE d.recepcion + INTERVAL d.m5 MINUTE END,
       CASE WHEN d.m6 IS NULL THEN NULL ELSE d.recepcion + INTERVAL d.m6 MINUTE END,
       CASE WHEN d.prometida IS NULL THEN NULL ELSE DATE(d.recepcion) + INTERVAL d.prometida DAY END,
       d.km, d.nivel, d.sintoma, 'Vehículo recibido sin daños visibles. [demo]', d.obs_mecanico,
       0, 0, @u_oper
FROM demo_orden AS d
JOIN vehiculo AS v ON v.patente = d.patente
LEFT JOIN demo_mecanico AS dm ON dm.clave = d.mecanico
ORDER BY d.n;

-- ---------------------------------------------------------------------
-- LÍNEAS DEL PRESUPUESTO
-- La descripción y el precio se copian del catálogo (precio con el
-- ajuste del mes). factor: horas reales / horas estimadas, NULL si la
-- línea todavía no tiene horas cargadas.
-- ---------------------------------------------------------------------

DROP TEMPORARY TABLE IF EXISTS demo_linea;
CREATE TEMPORARY TABLE demo_linea (n INT, codigo VARCHAR(30), cantidad DECIMAL(12,2), aprobado BOOLEAN, factor DECIMAL(4,2));

INSERT INTO demo_linea (n, codigo, cantidad, aprobado, factor) VALUES
    (1, 'ELE-ESC-01', 1, TRUE, 1.29),
    (2, 'FRE-DIS-01', 2, TRUE, 1.16),
    (3, 'SUS-AMO-01', 1, FALSE, NULL),
    (4, 'FRE-PAS-01', 1, TRUE, 1.29),
    (5, 'SUS-TRE-01', 1, TRUE, 1.03),
    (6, 'FRE-PAS-01', 1, TRUE, 1.23),
    (8, 'SUS-ALI-01', 1, TRUE, 1.19),
    (8, 'SUS-TRE-01', 1, TRUE, 0.84),
    (9, 'SUS-AMO-01', 1, TRUE, 0.99),
    (9, 'SUS-ALI-01', 1, TRUE, 0.89),
    (10, 'FRE-LIQ-01', 1, TRUE, 1.26),
    (10, 'FRE-PAS-01', 1, TRUE, 1.32),
    (11, 'SUS-ALI-01', 1, FALSE, NULL),
    (12, 'ELE-ESC-01', 1, TRUE, 0.88),
    (13, 'REF-LIQ-01', 1, TRUE, 0.90),
    (13, 'REF-RAD-01', 1, TRUE, 0.99),
    (14, 'FRE-LIQ-01', 1, TRUE, 0.84),
    (15, 'FRE-PAS-01', 1, TRUE, 1.05),
    (16, 'MEC-ACE-01', 1, FALSE, NULL),
    (17, 'FRE-PAS-01', 1, TRUE, 1.26),
    (17, 'FRE-DIS-01', 2, FALSE, NULL),
    (18, 'MEC-DIS-01', 1, TRUE, 1.27),
    (19, 'FRE-DIS-01', 1, TRUE, 0.75),
    (19, 'FRE-PAS-01', 1, TRUE, 1.05),
    (20, 'SUS-TRE-01', 1, TRUE, 1.18),
    (21, 'ELE-BAT-01', 1, FALSE, NULL),
    (22, 'SUS-AMO-01', 1, TRUE, 0.81),
    (22, 'SUS-ALI-01', 1, TRUE, 0.85),
    (23, 'MEC-EMB-01', 1, TRUE, 1.08),
    (23, 'MEC-ACE-01', 1, TRUE, 1.17),
    (24, 'SUS-ALI-01', 1, TRUE, 1.29),
    (24, 'SUS-TRE-01', 1, FALSE, NULL),
    (25, 'REF-LIQ-01', 1, TRUE, 0.97),
    (26, 'MEC-ACE-01', 1, TRUE, 1.01),
    (26, 'ELE-ESC-01', 1, TRUE, 0.99),
    (27, 'MEC-AFI-01', 1, TRUE, 1.09),
    (27, 'ELE-ESC-01', 1, TRUE, 1.32),
    (27, 'MEC-ACE-01', 1, FALSE, NULL),
    (28, 'ELE-ESC-01', 1, TRUE, 1.13),
    (29, 'REF-LIQ-01', 1, TRUE, 0.65),
    (30, 'MEC-ACE-01', 1, TRUE, 1.16),
    (30, 'FRE-LIQ-01', 1, TRUE, 0.91),
    (31, 'SUS-AMO-01', 1, TRUE, 0.67),
    (31, 'SUS-TRE-01', 1, TRUE, NULL),
    (32, 'MEC-AFI-01', 1, TRUE, 0.66),
    (32, 'ELE-ESC-01', 1, TRUE, NULL),
    (33, 'ELE-ESC-01', 1, TRUE, NULL),
    (34, 'MEC-ACE-01', 1, TRUE, 0.78),
    (35, 'REF-LIQ-01', 1, FALSE, NULL),
    (36, 'REF-LIQ-01', 1, TRUE, 0.72),
    (37, 'MEC-ACE-01', 1, TRUE, 0.89),
    (37, 'SUS-ALI-01', 1, TRUE, 1.19),
    (38, 'FRE-LIQ-01', 1, TRUE, 1.21),
    (39, 'FRE-PAS-01', 1, FALSE, NULL),
    (39, 'FRE-LIQ-01', 1, FALSE, NULL),
    (39, 'FRE-DIS-01', 2, FALSE, NULL);

INSERT INTO ot_detalle
    (id_orden_trabajo, id_servicio, descripcion, cantidad, precio_unitario, subtotal, aprobado, horas_reales)
SELECT o.id_orden_trabajo, s.id_servicio, s.descripcion, l.cantidad,
       ROUND(s.precio * d.ajuste, -2),
       l.cantidad * ROUND(s.precio * d.ajuste, -2),
       l.aprobado,
       CASE WHEN l.factor IS NULL THEN NULL
            ELSE ROUND(COALESCE(s.tiempo_estimado_horas, 1) * l.cantidad * l.factor, 2) END
FROM demo_linea AS l
JOIN demo_orden AS d ON d.n = l.n
JOIN orden_trabajo AS o ON o.nro_orden = @base + l.n
JOIN servicio AS s ON s.codigo = l.codigo;

-- Totales calculados con las líneas, igual que en la aplicación
UPDATE orden_trabajo AS o
SET o.total_presupuestado = (SELECT COALESCE(SUM(d.subtotal), 0) FROM ot_detalle AS d
                             WHERE d.id_orden_trabajo = o.id_orden_trabajo),
    o.total_aprobado = (SELECT COALESCE(SUM(d.subtotal), 0) FROM ot_detalle AS d
                        WHERE d.id_orden_trabajo = o.id_orden_trabajo AND d.aprobado = 1)
WHERE o.observaciones_recepcion LIKE '%[demo]';

-- ---------------------------------------------------------------------
-- HISTORIAL DE ESTADOS: una fila por cada estado alcanzado
-- ---------------------------------------------------------------------

INSERT INTO ot_historial_estado (id_orden_trabajo, id_estado_ot, fecha_hora, id_usuario, observacion)
SELECT x.id_orden_trabajo, x.id_estado_ot, x.fecha_hora, x.id_usuario, x.observacion
FROM (
    SELECT o.id_orden_trabajo, 1 AS id_estado_ot, d.recepcion AS fecha_hora,
           @u_oper AS id_usuario, 'Recepción del vehículo' AS observacion
    FROM demo_orden AS d
    JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
    UNION ALL SELECT o.id_orden_trabajo, 2 AS id_estado_ot, d.recepcion + INTERVAL d.m2 MINUTE AS fecha_hora,
           @u_admin AS id_usuario, 'Presupuesto cargado' AS observacion
    FROM demo_orden AS d
    JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
    WHERE d.m2 IS NOT NULL
    UNION ALL SELECT o.id_orden_trabajo, 3 AS id_estado_ot, d.recepcion + INTERVAL d.m3 MINUTE AS fecha_hora,
           @u_oper AS id_usuario, 'Aprobación del cliente registrada' AS observacion
    FROM demo_orden AS d
    JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
    WHERE d.m3 IS NOT NULL
    UNION ALL SELECT o.id_orden_trabajo, 4 AS id_estado_ot, d.recepcion + INTERVAL d.m4 MINUTE AS fecha_hora,
           COALESCE(dm.id_usuario, @u_admin) AS id_usuario, 'Inicio del trabajo' AS observacion
    FROM demo_orden AS d
    JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
    LEFT JOIN demo_mecanico AS dm ON dm.clave = d.mecanico
    WHERE d.m4 IS NOT NULL
    UNION ALL SELECT o.id_orden_trabajo, 5 AS id_estado_ot, d.recepcion + INTERVAL d.m5 MINUTE AS fecha_hora,
           COALESCE(dm.id_usuario, @u_admin) AS id_usuario, 'Trabajo finalizado' AS observacion
    FROM demo_orden AS d
    JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
    LEFT JOIN demo_mecanico AS dm ON dm.clave = d.mecanico
    WHERE d.m5 IS NOT NULL
    UNION ALL SELECT o.id_orden_trabajo, 6 AS id_estado_ot, d.recepcion + INTERVAL d.m6 MINUTE AS fecha_hora,
           @u_oper AS id_usuario, 'Vehículo entregado al cliente' AS observacion
    FROM demo_orden AS d
    JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
    WHERE d.m6 IS NOT NULL
    UNION ALL SELECT o.id_orden_trabajo, 7 AS id_estado_ot, d.recepcion + INTERVAL d.m7 MINUTE AS fecha_hora,
           @u_oper AS id_usuario, 'Presupuesto rechazado por el cliente' AS observacion
    FROM demo_orden AS d
    JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
    WHERE d.m7 IS NOT NULL
    UNION ALL SELECT o.id_orden_trabajo, 8 AS id_estado_ot, d.recepcion + INTERVAL d.m8 MINUTE AS fecha_hora,
           @u_admin AS id_usuario, d.motivo AS observacion
    FROM demo_orden AS d
    JOIN orden_trabajo AS o ON o.nro_orden = @base + d.n
    WHERE d.m8 IS NOT NULL
) AS x
ORDER BY x.fecha_hora;

-- El kilometraje de cada vehículo queda en el de su última orden
UPDATE vehiculo AS v
JOIN (SELECT patente, MAX(km) AS km FROM demo_orden GROUP BY patente) AS d ON d.patente = v.patente
SET v.km_actual = GREATEST(v.km_actual, d.km);

DROP TEMPORARY TABLE IF EXISTS demo_linea;
DROP TEMPORARY TABLE IF EXISTS demo_orden;
DROP TEMPORARY TABLE IF EXISTS demo_mecanico;
DROP TEMPORARY TABLE IF EXISTS demo_vehiculo;
DROP TEMPORARY TABLE IF EXISTS demo_cliente;

-- Verificación: cantidad de órdenes de demostración por estado
SELECT e.codigo, COUNT(*) AS ordenes
FROM orden_trabajo AS o
JOIN estado_ot AS e ON e.id_estado_ot = o.id_estado_ot
WHERE o.observaciones_recepcion LIKE '%[demo]'
GROUP BY e.id_estado_ot, e.codigo
ORDER BY e.id_estado_ot;

-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 03 - DML: datos de prueba (SOLO DESARROLLO)
-- Ejecutar después de 01 y 02. No es necesario para la entrega final.
--
-- Usuarios de prueba (la contraseña es el nombre de usuario seguido de 123)
--   operador  -> rol OPERADOR   contraseña: operador123
--   mecanico  -> rol MECANICO   contraseña: mecanico123
--                (vinculado al mecánico Ramón Gómez)
-- =====================================================================

USE taller_mecanico;

-- ---------------------------------------------------------------------
-- MECÁNICOS
-- ---------------------------------------------------------------------

INSERT INTO mecanico (id_mecanico, nombre_completo, especialidad, telefono, activo)
VALUES
    (1, 'Ramón Gómez',     'Motor y transmisión',   '3764400101', TRUE),
    (2, 'Lucas Benítez',   'Frenos y suspensión',   '3764400102', TRUE),
    (3, 'Hugo Fernández',  'Electricidad',          '3764400103', FALSE);

-- ---------------------------------------------------------------------
-- USUARIOS DE PRUEBA
-- ---------------------------------------------------------------------

INSERT INTO usuario (id_usuario, nombre_usuario, hash_contrasena, salt, nombre_completo, rol, id_mecanico)
VALUES
    (2, 'operador', 'GzGPY5I05QpfaQK94AiSndUBFO8JB5BNmyY/JdAHOQ0=', 'LqJ28hJo2vcSzu8Kt86Pow==', 'Laura Sosa',  'OPERADOR', NULL),
    (3, 'mecanico', '9DdtO86SbaBlvt3+BAM/INWOw+t0wa5n8IsTEH5Ocno=', 'Txt3RwxzEYpp+DqPbRLkJA==', 'Ramón Gómez', 'MECANICO', 1);

-- ---------------------------------------------------------------------
-- MARCAS Y MODELOS
-- ---------------------------------------------------------------------

INSERT INTO marca (id_marca, descripcion)
VALUES
    (1, 'Toyota'),
    (2, 'Volkswagen'),
    (3, 'Ford'),
    (4, 'Chevrolet'),
    (5, 'Renault'),
    (6, 'Fiat');

INSERT INTO modelo (id_modelo, id_marca, descripcion)
VALUES
    (1,  1, 'Corolla'),
    (2,  1, 'Hilux'),
    (3,  1, 'Etios'),
    (4,  2, 'Gol'),
    (5,  2, 'Amarok'),
    (6,  2, 'Vento'),
    (7,  3, 'Ranger'),
    (8,  3, 'Focus'),
    (9,  4, 'Onix'),
    (10, 4, 'Tracker'),
    (11, 5, 'Sandero'),
    (12, 5, 'Kangoo'),
    (13, 6, 'Cronos'),
    (14, 6, 'Strada');

-- ---------------------------------------------------------------------
-- CLIENTES
-- ---------------------------------------------------------------------

INSERT INTO cliente (id_cliente, razon_social, documento, domicilio, localidad, telefono, email)
VALUES
    (1, 'Juan Pérez',                    '30111222',      'Av. Uruguay 1234',       'Posadas',  '3764000001', 'juan.perez@correo.com'),
    (2, 'María Laura Acosta',            '28456789',      'Calle Colón 850',        'Posadas',  '3764000002', 'mlacosta@correo.com'),
    (3, 'Transporte del Litoral S.R.L.', '30-71234567-8', 'Ruta 12 km 8',           'Garupá',   '3764000003', 'contacto@litoral.com'),
    (4, 'Silvia Beatriz Ramírez',        '32987654',      'Av. Las Américas 500',   'Oberá',    '3764000004', NULL),
    (5, 'Ferretería San Jorge',          '20-25123456-3', 'Bolívar 1900',           'Posadas',  '3764000005', 'sanjorge@correo.com');

-- ---------------------------------------------------------------------
-- VEHÍCULOS
-- ---------------------------------------------------------------------

INSERT INTO vehiculo (id_vehiculo, patente, id_cliente, id_modelo, anio, color, nro_motor, nro_chasis, km_actual)
VALUES
    (1, 'AB123CD', 1, 1,  2020, 'Gris',   'MOT100001', 'CHA100001', 85000),
    (2, 'AC654DE', 1, 11, 2018, 'Rojo',   'MOT100002', 'CHA100002', 120500),
    (3, 'AD789EF', 5, 14, 2021, 'Blanco', 'MOT100003', 'CHA100003', 64000),
    (4, 'AF456GH', 3, 5,  2022, 'Blanco', 'MOT100004', 'CHA100004', 52000),
    (5, 'AG321HJ', 3, 7,  2019, 'Negro',  'MOT100005', 'CHA100005', 143000),
    (6, 'MNO456',  4, 4,  2012, 'Azul',   'MOT100006', 'CHA100006', 210300),
    (7, 'AE987KL', 2, 13, 2023, 'Gris',   'MOT100007', 'CHA100007', 18000);

-- ---------------------------------------------------------------------
-- SERVICIOS
-- ---------------------------------------------------------------------

INSERT INTO servicio (id_servicio, codigo, descripcion, id_categoria_servicio, precio, tiempo_estimado_horas)
VALUES
    (1,  'MEC-ACE-01', 'Cambio de aceite y filtro',               1, 45000.00,  1.00),
    (2,  'MEC-DIS-01', 'Cambio de kit de distribución',           1, 180000.00, 4.00),
    (3,  'MEC-AFI-01', 'Afinación de motor',                      1, 90000.00,  2.50),
    (4,  'MEC-EMB-01', 'Cambio de embrague',                      1, 220000.00, 5.00),
    (5,  'FRE-PAS-01', 'Cambio de pastillas delanteras',          2, 60000.00,  1.50),
    (6,  'FRE-DIS-01', 'Rectificado de discos',                   2, 50000.00,  1.00),
    (7,  'FRE-LIQ-01', 'Purgado y cambio de líquido de frenos',   2, 35000.00,  1.00),
    (8,  'SUS-AMO-01', 'Cambio de amortiguadores delanteros',     3, 120000.00, 2.50),
    (9,  'SUS-ALI-01', 'Alineación y balanceo',                   3, 40000.00,  1.00),
    (10, 'SUS-TRE-01', 'Revisión de tren delantero',              3, 30000.00,  1.00),
    (11, 'ELE-BAT-01', 'Diagnóstico y cambio de batería',         4, 25000.00,  0.50),
    (12, 'ELE-ESC-01', 'Diagnóstico por escáner',                 4, 30000.00,  0.75),
    (13, 'REF-RAD-01', 'Limpieza de radiador',                    5, 55000.00,  2.00),
    (14, 'REF-LIQ-01', 'Cambio de líquido refrigerante',          5, 28000.00,  0.75);

-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 02 - DML: catálogos que el sistema necesita para funcionar
-- Ejecutar una vez, después de 01_ddl_estructura.sql
--
-- Los estados de OT llevan ID explícito: el código de la aplicación
-- los referencia por número y deben ser estables entre reinstalaciones.
-- =====================================================================

USE taller_mecanico;

-- ---------------------------------------------------------------------
-- ESTADOS DE LA ORDEN DE TRABAJO (especificación, sección 6)
-- ---------------------------------------------------------------------

INSERT INTO estado_ot
    (id_estado_ot, codigo, descripcion, permite_edicion_detalle, es_estado_final, orden_flujo)
VALUES
    (1, 'RECEPCIONADA',  'Recepcionada',  TRUE,  FALSE, 1),
    (2, 'PRESUPUESTADA', 'Presupuestada', TRUE,  FALSE, 2),
    (3, 'APROBADA',      'Aprobada',      FALSE, FALSE, 3),
    (4, 'EN_PROCESO',    'En proceso',    FALSE, FALSE, 4),
    (5, 'FINALIZADA',    'Finalizada',    FALSE, FALSE, 5),
    (6, 'ENTREGADA',     'Entregada',     FALSE, TRUE,  6),
    (7, 'RECHAZADA',     'Rechazada',     FALSE, TRUE,  7),
    (8, 'ANULADA',       'Anulada',       FALSE, TRUE,  8);

-- ---------------------------------------------------------------------
-- CATEGORÍAS DE SERVICIO
-- ---------------------------------------------------------------------

INSERT INTO categoria_servicio (id_categoria_servicio, descripcion)
VALUES
    (1, 'Mecánica general'),
    (2, 'Frenos'),
    (3, 'Suspensión'),
    (4, 'Electricidad'),
    (5, 'Refrigeración');

-- ---------------------------------------------------------------------
-- USUARIO ADMINISTRADOR INICIAL
-- Usuario: admin   Contraseña: admin123   (cambiarla desde la aplicación)
-- ---------------------------------------------------------------------

INSERT INTO usuario (id_usuario, nombre_usuario, hash_contrasena, nombre_completo, rol, id_mecanico)
VALUES
    (1, 'admin', '$2a$11$5N4Vi5zZnTmZ0RZvn3XLZ.JNydCkmE9CHxAHPbcwuXdBZ.JzJOSNm', 'Administrador del sistema', 'ADMINISTRADOR', NULL);

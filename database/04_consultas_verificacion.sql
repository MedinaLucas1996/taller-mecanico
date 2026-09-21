-- =====================================================================
-- 04 - Consultas de verificación
-- Ejecutar de a una (Ctrl+Enter en DBeaver) después de 01, 02 y 03.
-- =====================================================================

USE taller_mecanico;

-- 1. Cantidad de tablas (esperado: 12)
SELECT COUNT(*) AS tablas
FROM information_schema.tables
WHERE table_schema = 'taller_mecanico';

-- 2. Filas por tabla
SELECT 'usuario' AS tabla, COUNT(*) AS filas FROM usuario
UNION ALL SELECT 'mecanico', COUNT(*) FROM mecanico
UNION ALL SELECT 'cliente', COUNT(*) FROM cliente
UNION ALL SELECT 'marca', COUNT(*) FROM marca
UNION ALL SELECT 'modelo', COUNT(*) FROM modelo
UNION ALL SELECT 'vehiculo', COUNT(*) FROM vehiculo
UNION ALL SELECT 'categoria_servicio', COUNT(*) FROM categoria_servicio
UNION ALL SELECT 'servicio', COUNT(*) FROM servicio
UNION ALL SELECT 'estado_ot', COUNT(*) FROM estado_ot
UNION ALL SELECT 'orden_trabajo', COUNT(*) FROM orden_trabajo
UNION ALL SELECT 'ot_detalle', COUNT(*) FROM ot_detalle
UNION ALL SELECT 'ot_historial_estado', COUNT(*) FROM ot_historial_estado;

-- 3. Estados de OT con sus indicadores
SELECT id_estado_ot, codigo, permite_edicion_detalle, es_estado_final, orden_flujo
FROM estado_ot
ORDER BY orden_flujo;

-- 4. Usuarios con su rol y mecánico asociado
SELECT u.nombre_usuario, u.rol, m.nombre_completo AS mecanico
FROM usuario u
LEFT JOIN mecanico m ON m.id_mecanico = u.id_mecanico
ORDER BY u.id_usuario;

-- 5. Búsqueda por patente (la que usará la pantalla de recepción)
SELECT v.patente, ma.descripcion AS marca, mo.descripcion AS modelo, v.anio,
       v.km_actual, c.razon_social AS titular, c.documento
FROM vehiculo v
JOIN modelo mo ON mo.id_modelo = v.id_modelo
JOIN marca ma ON ma.id_marca = mo.id_marca
JOIN cliente c ON c.id_cliente = v.id_cliente
WHERE v.patente = 'AB123CD';

-- 6. Vehículos por cliente
SELECT c.razon_social, COUNT(v.id_vehiculo) AS cantidad,
       GROUP_CONCAT(v.patente ORDER BY v.patente SEPARATOR ', ') AS patentes
FROM cliente c
LEFT JOIN vehiculo v ON v.id_cliente = c.id_cliente
GROUP BY c.id_cliente, c.razon_social
ORDER BY cantidad DESC, c.razon_social;

-- 7. Catálogo de servicios por categoría
SELECT cs.descripcion AS categoria, s.codigo, s.descripcion, s.precio, s.tiempo_estimado_horas
FROM servicio s
JOIN categoria_servicio cs ON cs.id_categoria_servicio = s.id_categoria_servicio
ORDER BY cs.descripcion, s.codigo;

-- 8. Modelos por marca (alimenta el combo en cascada)
SELECT ma.descripcion AS marca, COUNT(mo.id_modelo) AS modelos
FROM marca ma
LEFT JOIN modelo mo ON mo.id_marca = ma.id_marca
GROUP BY ma.id_marca, ma.descripcion
ORDER BY ma.descripcion;

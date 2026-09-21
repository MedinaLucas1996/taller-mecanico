-- =====================================================================
-- 05 - Consultas básicas de práctica (solo lectura)
-- Ejecutar de a una: pararse sobre la consulta y presionar Ctrl+Enter.
-- Ninguna modifica datos.
-- =====================================================================

USE taller_mecanico;

-- ---------------------------------------------------------------------
-- 1. SELECT: traer todos los datos de una tabla
-- ---------------------------------------------------------------------

-- 1.1 Todas las columnas de todos los clientes
SELECT * FROM cliente;

-- 1.2 Solo algunas columnas
SELECT razon_social, documento, telefono FROM cliente;

-- 1.3 Columnas con un nombre más legible (alias con AS)
SELECT razon_social AS nombre, documento AS dni_cuit FROM cliente;

-- ---------------------------------------------------------------------
-- 2. WHERE: filtrar filas
-- ---------------------------------------------------------------------

-- 2.1 Solo los clientes activos
SELECT razon_social, activo FROM cliente WHERE activo = 1;

-- 2.2 Clientes de una localidad
SELECT razon_social, localidad FROM cliente WHERE localidad = 'Posadas';

-- 2.3 Vehículos con más de 100.000 km
SELECT patente, km_actual FROM vehiculo WHERE km_actual > 100000;

-- 2.4 Dos condiciones a la vez (AND)
SELECT patente, anio, km_actual FROM vehiculo WHERE anio >= 2020 AND km_actual < 70000;

-- 2.5 Servicios en un rango de precios (BETWEEN)
SELECT codigo, descripcion, precio FROM servicio WHERE precio BETWEEN 30000 AND 60000;

-- ---------------------------------------------------------------------
-- 3. LIKE: buscar por parte de un texto (% = cualquier cosa)
-- ---------------------------------------------------------------------

-- 3.1 Clientes cuyo nombre contiene "ez"
SELECT razon_social FROM cliente WHERE razon_social LIKE '%ez%';

-- 3.2 Patentes que empiezan con "A"
SELECT patente FROM vehiculo WHERE patente LIKE 'A%';

-- 3.3 Servicios de frenos, por el prefijo del código
SELECT codigo, descripcion FROM servicio WHERE codigo LIKE 'FRE-%';

-- ---------------------------------------------------------------------
-- 4. ORDER BY y LIMIT: ordenar y limitar
-- ---------------------------------------------------------------------

-- 4.1 Servicios del más caro al más barato
SELECT descripcion, precio FROM servicio ORDER BY precio DESC;

-- 4.2 Los 3 servicios más caros
SELECT descripcion, precio FROM servicio ORDER BY precio DESC LIMIT 3;

-- 4.3 Vehículos ordenados por año, y a igual año por patente
SELECT patente, anio FROM vehiculo ORDER BY anio, patente;

-- ---------------------------------------------------------------------
-- 5. Funciones de agregado: COUNT, SUM, AVG, MIN, MAX
-- ---------------------------------------------------------------------

-- 5.1 Cuántos clientes hay
SELECT COUNT(*) AS cantidad_clientes FROM cliente;

-- 5.2 Precio promedio, mínimo y máximo de los servicios
SELECT AVG(precio) AS promedio, MIN(precio) AS minimo, MAX(precio) AS maximo FROM servicio;

-- 5.3 Kilómetros sumados de todos los vehículos
SELECT SUM(km_actual) AS km_totales FROM vehiculo;

-- ---------------------------------------------------------------------
-- 6. JOIN: combinar tablas relacionadas
-- ---------------------------------------------------------------------

-- 6.1 Cada vehículo con el nombre de su titular
SELECT v.patente, c.razon_social AS titular
FROM vehiculo AS v
JOIN cliente AS c ON c.id_cliente = v.id_cliente;

-- 6.2 Cada modelo con su marca
SELECT ma.descripcion AS marca, mo.descripcion AS modelo
FROM modelo AS mo
JOIN marca AS ma ON ma.id_marca = mo.id_marca
ORDER BY marca, modelo;

-- 6.3 Tres tablas: vehículo + modelo + marca
SELECT v.patente, ma.descripcion AS marca, mo.descripcion AS modelo, v.anio
FROM vehiculo AS v
JOIN modelo AS mo ON mo.id_modelo = v.id_modelo
JOIN marca AS ma ON ma.id_marca = mo.id_marca;

-- 6.4 Servicios con el nombre de su categoría
SELECT cs.descripcion AS categoria, s.descripcion AS servicio, s.precio
FROM servicio AS s
JOIN categoria_servicio AS cs ON cs.id_categoria_servicio = s.id_categoria_servicio
ORDER BY categoria, servicio;

-- ---------------------------------------------------------------------
-- 7. GROUP BY: agrupar y contar por grupo
-- ---------------------------------------------------------------------

-- 7.1 Cuántos vehículos tiene cada cliente
SELECT c.razon_social, COUNT(v.id_vehiculo) AS vehiculos
FROM cliente AS c
JOIN vehiculo AS v ON v.id_cliente = c.id_cliente
GROUP BY c.id_cliente, c.razon_social;

-- 7.2 Cantidad de servicios y precio promedio por categoría
SELECT cs.descripcion AS categoria, COUNT(*) AS servicios, AVG(s.precio) AS precio_promedio
FROM servicio AS s
JOIN categoria_servicio AS cs ON cs.id_categoria_servicio = s.id_categoria_servicio
GROUP BY cs.id_categoria_servicio, cs.descripcion;

-- 7.3 HAVING: filtrar grupos (solo clientes con más de un vehículo)
SELECT c.razon_social, COUNT(v.id_vehiculo) AS vehiculos
FROM cliente AS c
JOIN vehiculo AS v ON v.id_cliente = c.id_cliente
GROUP BY c.id_cliente, c.razon_social
HAVING COUNT(v.id_vehiculo) > 1;

-- ---------------------------------------------------------------------
-- 8. LEFT JOIN: incluir también los que no tienen relación
-- ---------------------------------------------------------------------

-- 8.1 Todos los mecánicos, tengan o no usuario en el sistema
--     (los que no tienen usuario muestran NULL)
SELECT m.nombre_completo, u.nombre_usuario
FROM mecanico AS m
LEFT JOIN usuario AS u ON u.id_mecanico = m.id_mecanico;

-- 8.2 Solo los mecánicos SIN usuario
SELECT m.nombre_completo
FROM mecanico AS m
LEFT JOIN usuario AS u ON u.id_mecanico = m.id_mecanico
WHERE u.id_usuario IS NULL;

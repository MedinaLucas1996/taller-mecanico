-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 14 - Usuario de la aplicación con permisos mínimos
--
-- Crea el usuario de MariaDB "taller_app", que la aplicación usa en
-- lugar de root. Solo puede leer, agregar, modificar y eliminar filas
-- de la base taller_mecanico: no puede crear ni borrar tablas, tocar
-- otras bases ni administrar usuarios del servidor. Así, quien lea la
-- cadena de conexión de un puesto no obtiene el control del servidor.
--
-- Antes de ejecutarlo:
--   * Reemplazar CAMBIAR_ESTA_CLAVE por una contraseña propia de al
--     menos 12 caracteres. El script se niega a seguir con la clave de
--     ejemplo o con una más corta.
--   * Si los puestos se conectan desde otras PC de la red, cambiar
--     @host por la red del taller, por ejemplo '192.168.1.%'. Con
--     'localhost' solo se conecta la aplicación instalada en el mismo
--     equipo que el servidor.
--
-- Después de ejecutarlo, poner el usuario y la contraseña en el
-- conexion.txt de cada puesto (ver WinFormsApp1/conexion.ejemplo.txt).
-- Los scripts 01 a 14 se siguen ejecutando con root o con otro usuario
-- administrador: taller_app no puede crear tablas ni índices.
--
-- Se puede ejecutar más de una vez: si el usuario ya existe, le cambia
-- la contraseña y le vuelve a dar los mismos permisos.
-- =====================================================================

SET @clave = 'CAMBIAR_ESTA_CLAVE';
SET @host = 'localhost';

DELIMITER //

BEGIN NOT ATOMIC
    IF @clave = 'CAMBIAR_ESTA_CLAVE' OR CHAR_LENGTH(@clave) < 12 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Reemplazar CAMBIAR_ESTA_CLAVE por una contraseña propia de al menos 12 caracteres';
    END IF;

    EXECUTE IMMEDIATE CONCAT('CREATE USER IF NOT EXISTS ''taller_app''@', QUOTE(@host),
                             ' IDENTIFIED BY ', QUOTE(@clave));
    -- si el usuario ya existia, CREATE USER no le cambia la clave
    EXECUTE IMMEDIATE CONCAT('ALTER USER ''taller_app''@', QUOTE(@host),
                             ' IDENTIFIED BY ', QUOTE(@clave));
    EXECUTE IMMEDIATE CONCAT('GRANT SELECT, INSERT, UPDATE, DELETE ON taller_mecanico.* ',
                             'TO ''taller_app''@', QUOTE(@host));
END//

DELIMITER ;

-- No dejo la contraseña en una variable de la sesión
SET @clave = NULL;

-- Verificación: se esperan dos líneas, GRANT USAGE ON *.* (el acceso
-- al servidor, con el hash de la clave) y
-- GRANT SELECT, INSERT, UPDATE, DELETE ON `taller_mecanico`.*
EXECUTE IMMEDIATE CONCAT('SHOW GRANTS FOR ''taller_app''@', QUOTE(@host));

-- =====================================================================
-- Sistema de Gestión para Taller Mecánico
-- 06 - Migración: contraseñas con PBKDF2 (columna salt)
--
-- Para bases que ya se cargaron con una versión anterior de los scripts
-- 01 a 03 (hash BCrypt y sin columna salt). Una instalación nueva que
-- ejecuta 01, 02 y 03 NO necesita este script.
--
-- Esquema de contraseñas: PBKDF2 + SHA256, 100000 iteraciones, hash de
-- 32 bytes y salt aleatorio de 16 bytes, ambos guardados en Base64.
--
-- Se puede ejecutar más de una vez sin error. Solo actualiza los tres
-- usuarios semilla (admin, operador y rgomez). Cualquier otro usuario
-- creado a mano queda con salt vacío y no podrá ingresar hasta que se
-- le cargue un hash y un salt válidos.
-- =====================================================================

USE taller_mecanico;

-- Se agrega la columna con un valor por defecto vacío para que MariaDB
-- pueda completar las filas existentes (la columna es NOT NULL).
-- Luego se quita el valor por defecto para dejar la columna igual que
-- en 01_ddl_estructura.sql.
ALTER TABLE usuario
    ADD COLUMN IF NOT EXISTS salt VARCHAR(100) NOT NULL DEFAULT ''
        COMMENT 'Salt aleatorio de 16 bytes en Base64'
        AFTER hash_contrasena;

ALTER TABLE usuario
    MODIFY COLUMN hash_contrasena VARCHAR(255) NOT NULL
        COMMENT 'Hash PBKDF2-SHA256 en Base64. Nunca texto plano';

-- Se reemplazan los hashes BCrypt de los usuarios semilla
UPDATE usuario
SET hash_contrasena = 'gMzaPOaHGnFQOEOvssep1VHY1FZwBXgsu5bL14XbFEg=',
    salt            = 'qeMXCYtoPeTRs4kVyKy4GQ=='
WHERE nombre_usuario = 'admin';

UPDATE usuario
SET hash_contrasena = '8+4L7BPCiTaxVk19LQq0JnEfCEqojv0W2iSY7lhTVTQ=',
    salt            = 'FodR9x+nyUeJFsAP9LHUIg=='
WHERE nombre_usuario = 'operador';

UPDATE usuario
SET hash_contrasena = 'wGUWEOfk1cVG5hTyXcsYK8S+fzm0JNjcj+E5SSIHCFg=',
    salt            = 'Qwciw9sDeyLACUbG8/wzhw=='
WHERE nombre_usuario = 'rgomez';

ALTER TABLE usuario
    ALTER COLUMN salt DROP DEFAULT;

-- Verificación: se esperan hash de 44 caracteres y salt de 24
SELECT nombre_usuario,
       LENGTH(hash_contrasena) AS largo_hash,
       LENGTH(salt)            AS largo_salt
FROM usuario
ORDER BY id_usuario;

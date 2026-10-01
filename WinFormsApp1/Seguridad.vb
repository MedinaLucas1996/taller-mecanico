'importo las clases de criptografia
Imports System.Security.Cryptography
Module Seguridad
    'declaro constantes del esquema PBKDF2 (igual que la referencia de la catedra)
    Private Const ITERACIONES As Integer = 100000
    Private Const LARGO_HASH As Integer = 32
    Private Const LARGO_SALT As Integer = 16

    'genero un salt aleatorio y lo devuelvo en Base64
    Public Function GenerarSalt() As String
        Dim bytesSalt As Byte() = RandomNumberGenerator.GetBytes(LARGO_SALT)
        Return Convert.ToBase64String(bytesSalt)
    End Function

    'calculo el hash PBKDF2 de la clave con el salt y lo devuelvo en Base64
    Public Function HashearClave(clave As String, salt As String) As String
        Dim bytesSalt As Byte() = Convert.FromBase64String(salt)
        Dim bytesHash As Byte() = Rfc2898DeriveBytes.Pbkdf2(clave, bytesSalt, ITERACIONES, HashAlgorithmName.SHA256, LARGO_HASH)
        Return Convert.ToBase64String(bytesHash)
    End Function

    'verifico la clave contra el hash guardado
    Public Function VerificarClave(clave As String, hashGuardado As String, salt As String) As Boolean
        Try
            Dim bytesGuardado As Byte() = Convert.FromBase64String(hashGuardado)
            Dim bytesSalt As Byte() = Convert.FromBase64String(salt)
            Dim bytesCalculado As Byte() = Rfc2898DeriveBytes.Pbkdf2(clave, bytesSalt, ITERACIONES, HashAlgorithmName.SHA256, LARGO_HASH)
            'comparo en tiempo constante
            Return CryptographicOperations.FixedTimeEquals(bytesCalculado, bytesGuardado)
        Catch ex As FormatException
            'hash o salt no son Base64 valido (por ejemplo un hash BCrypt viejo)
            Return False
        End Try
    End Function

End Module

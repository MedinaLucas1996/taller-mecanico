Module Sesion
    'nombres de los roles tal como estan guardados en usuario.rol
    Public Const ROL_ADMINISTRADOR As String = "ADMINISTRADOR"
    Public Const ROL_OPERADOR As String = "OPERADOR"
    Public Const ROL_MECANICO As String = "MECANICO"

    'declaro variables con los datos del usuario que inicio sesion
    Public IdUsuario As Integer
    Public NombreUsuario As String = ""
    Public NombreCompleto As String = ""
    Public Rol As String = ""
    'queda en 0 cuando el usuario no es mecanico
    Public IdMecanico As Integer

    'limpio los datos al cerrar sesion
    Public Sub CerrarSesion()
        IdUsuario = 0
        NombreUsuario = ""
        NombreCompleto = ""
        Rol = ""
        IdMecanico = 0
    End Sub

End Module

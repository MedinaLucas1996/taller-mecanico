'importo lo necesario para leer el archivo de conexion
Imports System.IO
Module ConexionBD
    'cadena de conexion por defecto, la misma para todo el equipo
    Private Const CADENA_POR_DEFECTO As String =
        "Server=localhost;Port=3306;Database=taller_mecanico;" &
        "User ID=root;Password=;"

    'archivo opcional junto al ejecutable con la cadena de este puesto (no se sube al repositorio)
    'el formato esta en conexion.ejemplo.txt
    Public Const ARCHIVO_CONEXION As String = "conexion.txt"

    'cadena de conexion que usan todas las pantallas: la del archivo si existe, si no la por defecto
    Public ReadOnly CADENA As String = LeerCadena()

    Function LeerCadena() As String
        'leo la cadena del archivo de conexion, si esta; tomo la primera linea con contenido que no sea un comentario
        'si el archivo existe pero no sirve, lo dejo anotado en el registro de errores antes de seguir con la cadena por defecto:
        'asi una conexion que termina en el servidor equivocado deja rastro
        Dim ruta As String = Path.Combine(AppContext.BaseDirectory, ARCHIVO_CONEXION)
        If Not File.Exists(ruta) Then Return CADENA_POR_DEFECTO

        Try
            For Each linea As String In File.ReadAllLines(ruta)
                Dim texto As String = linea.Trim()
                If texto <> "" AndAlso Not texto.StartsWith("#") Then Return texto
            Next

            RegistrarError("El archivo " & ARCHIVO_CONEXION & " no tiene ninguna cadena de conexión; se usa la cadena por defecto.",
                           New InvalidDataException(ruta))
        Catch ex As Exception
            RegistrarError("No se pudo leer " & ARCHIVO_CONEXION & "; se usa la cadena por defecto.", ex)
        End Try

        Return CADENA_POR_DEFECTO
    End Function

End Module

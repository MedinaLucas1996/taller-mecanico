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
        Try
            Dim ruta As String = Path.Combine(AppContext.BaseDirectory, ARCHIVO_CONEXION)
            If File.Exists(ruta) Then
                For Each linea As String In File.ReadAllLines(ruta)
                    Dim texto As String = linea.Trim()
                    If texto <> "" AndAlso Not texto.StartsWith("#") Then Return texto
                Next
            End If
        Catch ex As Exception
            'si el archivo no se puede leer sigo con la cadena por defecto
        End Try

        Return CADENA_POR_DEFECTO
    End Function

End Module

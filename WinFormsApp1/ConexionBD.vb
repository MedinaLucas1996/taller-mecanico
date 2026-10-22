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

    'motivo por el que el archivo de conexion de este puesto no se pudo usar; vacio cuando no hay problema
    'va declarado antes que CADENA porque LeerCadena lo completa
    Private problemaConexion As String = ""

    'cadena de conexion que usan todas las pantallas: la del archivo si existe, si no la por defecto
    Public ReadOnly CADENA As String = LeerCadena()

    'el login no deja entrar mientras haya un problema: un puesto configurado para otro servidor
    'no tiene que conectarse al de por defecto sin que nadie lo note
    Public ReadOnly Property PROBLEMA_CONEXION As String
        Get
            Return problemaConexion
        End Get
    End Property

    Function LeerCadena() As String
        'leo la cadena del archivo de conexion, si esta; tomo la primera linea con contenido que no sea un comentario
        'si el archivo existe pero no sirve, dejo anotado el motivo en PROBLEMA_CONEXION y en el registro de errores
        Dim ruta As String = Path.Combine(AppContext.BaseDirectory, ARCHIVO_CONEXION)
        If Not File.Exists(ruta) Then Return CADENA_POR_DEFECTO

        Try
            For Each linea As String In File.ReadAllLines(ruta)
                Dim texto As String = linea.Trim()
                If texto <> "" AndAlso Not texto.StartsWith("#") Then Return texto
            Next

            problemaConexion = "El archivo " & ARCHIVO_CONEXION & " existe pero no tiene ninguna cadena de conexión."
            RegistrarError(problemaConexion, New InvalidDataException(ruta))
        Catch ex As Exception
            problemaConexion = "No se pudo leer el archivo " & ARCHIVO_CONEXION & ": " & ex.Message
            RegistrarError(problemaConexion, ex)
        End Try

        Return CADENA_POR_DEFECTO
    End Function

End Module

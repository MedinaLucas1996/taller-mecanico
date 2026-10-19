'importo lo necesario para escribir el archivo de errores y reconocer los errores de MySQL
Imports System.IO
Imports MySqlConnector

Module Errores
    'rutinas compartidas para avisar y registrar errores
    'las usan el login, el manejador global de la aplicacion y las pantallas que lo necesiten

    'queda en True cuando el manejador global decidio cerrar el sistema: las pantallas no deben frenar ese cierre con preguntas
    Public CierreForzado As Boolean = False

    'carpeta de datos del usuario donde queda el archivo de errores
    Public ReadOnly Property RutaRegistro As String
        Get
            Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                "TallerMecanico", "errores.log")
        End Get
    End Property

    Function RegistrarError(contexto As String, ex As Exception) As Boolean
        'agrego al archivo una entrada con la fecha, el usuario, el contexto y el detalle completo
        'devuelve False si el archivo no se pudo escribir; el sistema sigue igual, el registro es una ayuda, no una obligacion
        Try
            Directory.CreateDirectory(Path.GetDirectoryName(RutaRegistro))

            Dim entrada As String =
                Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & " | " & Sesion.NombreUsuario & " | " & contexto & vbCrLf &
                ex.ToString() & vbCrLf & vbCrLf

            File.AppendAllText(RutaRegistro, entrada)
            Return True
        Catch
            'nada que hacer: no hay donde registrar que no se pudo registrar
            Return False
        End Try
    End Function

    Function EsErrorDeConexion(ex As Exception) As Boolean
        'indica si el error es por no poder llegar al servidor de la base de datos
        Dim errorMySql As MySqlException = TryCast(ex, MySqlException)
        If errorMySql Is Nothing Then Return False

        Return errorMySql.ErrorCode = MySqlErrorCode.UnableToConnectToHost
    End Function

    Function EsErrorDeAcceso(ex As Exception) As Boolean
        'indica si el servidor respondio pero rechazo el usuario o la contraseña de la cadena de conexion
        Dim errorMySql As MySqlException = TryCast(ex, MySqlException)
        If errorMySql Is Nothing Then Return False

        Return errorMySql.ErrorCode = MySqlErrorCode.AccessDenied
    End Function

    Sub MostrarError(mensaje As String, ex As Exception)
        'muestro un mensaje claro para el usuario y guardo el detalle tecnico en el archivo de errores
        RegistrarError(mensaje, ex)

        Dim texto As String
        If EsErrorDeConexion(ex) Then
            'el detalle del driver no le sirve al usuario: le digo que revisar
            texto = mensaje & vbCrLf & vbCrLf &
                    "No se pudo conectar con la base de datos." & vbCrLf &
                    "Verifique que el servidor MariaDB esté iniciado y vuelva a intentar."
        ElseIf EsErrorDeAcceso(ex) Then
            'el servidor esta, pero el usuario o la contraseña de este puesto no sirven: el arreglo es el archivo de conexion
            texto = mensaje & vbCrLf & vbCrLf &
                    "La base de datos rechazó el usuario o la contraseña de la conexión." & vbCrLf &
                    "Revise el archivo " & ARCHIVO_CONEXION & " junto al ejecutable (formato en conexion.ejemplo.txt)."
        Else
            texto = mensaje & vbCrLf & vbCrLf & "Detalle: " & ex.Message
        End If

        MessageBox.Show(texto, "Taller Mecánico", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Module

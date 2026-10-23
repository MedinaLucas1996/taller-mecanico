Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports MySqlConnector

Public Class FrmLogin
    'uso el reproductor de Windows para el archivo MP3
    <DllImport("winmm.dll", CharSet:=CharSet.Unicode, EntryPoint:="mciSendStringW")>
    Private Shared Function EnviarAudio(comando As String, respuesta As StringBuilder, longitud As Integer, ventana As IntPtr) As Integer
    End Function

    Private musicaAbierta As Boolean = False

    'bloqueo por intentos fallidos: despues de MAXIMO_INTENTOS seguidos el boton se apaga SEGUNDOS_BLOQUEO segundos
    'cuenta igual un usuario inexistente que una clave incorrecta; vive solo en memoria, no toca la base
    Private Const MAXIMO_INTENTOS As Integer = 5
    Private Const SEGUNDOS_BLOQUEO As Integer = 30
    Private intentosFallidos As Integer = 0
    Private segundosRestantes As Integer = 0

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click

        'valido que no haya campos vacios
        If txtUsuario.Text.Trim = "" Then
            MessageBox.Show("Falta el usuario")
            txtUsuario.Focus()
            Exit Sub
        End If

        If txtPassword.Text = "" Then
            MessageBox.Show("Falta la contraseña")
            txtPassword.Focus()
            Exit Sub
        End If

        'si el archivo de conexion de este puesto esta roto no intento entrar: conectaria al servidor por defecto sin que nadie lo note
        If PROBLEMA_CONEXION <> "" Then
            MessageBox.Show(PROBLEMA_CONEXION & vbCrLf & vbCrLf &
                            "Corrija o elimine el archivo " & ARCHIVO_CONEXION & " que está junto al ejecutable y vuelva a abrir el sistema.",
                            "Taller Mecánico", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        'mientras se verifica la clave muestro el reloj de espera y evito un segundo click
        Cursor.Current = Cursors.WaitCursor
        btnIngresar.Enabled = False

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'busco el usuario, solo entre los activos
                Dim consulta As String =
                    "SELECT id_usuario, nombre_usuario, hash_contrasena, salt, " &
                    "nombre_completo, rol, id_mecanico " &
                    "FROM usuario " &
                    "WHERE nombre_usuario = @nombre_usuario AND activo = 1;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@nombre_usuario", txtUsuario.Text.Trim)

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'uso el mismo mensaje si el usuario no existe o la clave es incorrecta
                    If tabla.Rows.Count = 0 Then
                        RegistrarIntentoFallido()
                        MessageBox.Show("Usuario o contraseña incorrectos.")
                        txtPassword.Clear()
                        txtPassword.Focus()
                        Exit Sub
                    End If

                    Dim fila As DataRow = tabla.Rows(0)

                    If Not VerificarClave(txtPassword.Text, fila("hash_contrasena").ToString, fila("salt").ToString) Then
                        RegistrarIntentoFallido()
                        MessageBox.Show("Usuario o contraseña incorrectos.")
                        txtPassword.Clear()
                        txtPassword.Focus()
                        Exit Sub
                    End If

                    'entro bien: los intentos fallidos anteriores dejan de contar
                    intentosFallidos = 0

                    'guardo los datos del usuario en la sesion
                    Sesion.IdUsuario = Convert.ToInt32(fila("id_usuario"))
                    Sesion.NombreUsuario = fila("nombre_usuario").ToString
                    Sesion.NombreCompleto = fila("nombre_completo").ToString
                    Sesion.Rol = fila("rol").ToString
                    'id_mecanico es NULL cuando el usuario no es mecanico
                    If IsDBNull(fila("id_mecanico")) Then
                        Sesion.IdMecanico = 0
                    Else
                        Sesion.IdMecanico = Convert.ToInt32(fila("id_mecanico"))
                    End If
                End Using
            End Using
        Catch ex As Exception
            'cualquier falla (base apagada, datos ilegibles) se avisa con un mensaje claro y queda registrada
            MostrarError("No se pudo iniciar sesión.", ex)
            Exit Sub
        Finally
            'pase lo que pase, el cursor vuelve a la normalidad
            'el boton tambien, salvo que este intento haya completado el bloqueo: ahi lo vuelve a prender el reloj
            Cursor.Current = Cursors.Default
            btnIngresar.Enabled = Not tmrBloqueo.Enabled
        End Try

        Dim principal As New FrmPrincipal()

        principal.Show()
        DetenerMusica()
        Me.Hide()

    End Sub

    Private Sub RegistrarIntentoFallido()
        'sumo un intento fallido; al llegar al maximo apago el boton y arranco la cuenta regresiva
        intentosFallidos = intentosFallidos + 1
        If intentosFallidos < MAXIMO_INTENTOS Then Exit Sub

        segundosRestantes = SEGUNDOS_BLOQUEO
        lblBloqueo.Text = "Demasiados intentos. Espere " & segundosRestantes & " s"
        lblBloqueo.Visible = True
        btnIngresar.Enabled = False
        tmrBloqueo.Start()
    End Sub

    Private Sub tmrBloqueo_Tick(sender As Object, e As EventArgs) Handles tmrBloqueo.Tick
        'cada segundo bajo la cuenta; al llegar a cero se puede volver a intentar
        segundosRestantes = segundosRestantes - 1

        If segundosRestantes > 0 Then
            lblBloqueo.Text = "Demasiados intentos. Espere " & segundosRestantes & " s"
            Exit Sub
        End If

        tmrBloqueo.Stop()
        lblBloqueo.Visible = False
        intentosFallidos = 0
        btnIngresar.Enabled = True
        txtPassword.Focus()
    End Sub

    Private Sub FrmLogin_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'cargo el fondo y mantengo legibles los textos
        Try
            Dim rutaImagen As String = Path.Combine(AppContext.BaseDirectory, "Recursos", "fondo-login-underground.png")
            Using imagen As Image = Image.FromFile(rutaImagen)
                Me.BackgroundImage = New Bitmap(imagen)
            End Using
            For Each control As Control In Me.Controls
                If TypeOf control Is Label Then
                    control.BackColor = Color.Transparent
                    control.ForeColor = Color.White
                End If
            Next
            'el aviso de bloqueo cae sobre una zona clara de la imagen: le doy una franja oscura propia para que se lea
            lblBloqueo.BackColor = Color.FromArgb(210, 140, 20, 20)
        Catch ex As Exception
            'sin fondo el login sigue sirviendo: lo anoto en el registro y no molesto al usuario
            RegistrarError("No se pudo cargar el fondo del login", ex)
        End Try

        IniciarMusica()
    End Sub

    'dejo el login vacio y a la vista para que ingrese otro usuario
    'se llama desde el menu principal al cerrar sesion
    Public Sub PrepararNuevoIngreso()
        txtUsuario.Clear()
        txtPassword.Clear()
        Me.Show()
        IniciarMusica()
        txtUsuario.Focus()
    End Sub

    Private Sub IniciarMusica()
        'si la musica ya esta sonando no la abro de nuevo
        If musicaAbierta Then Exit Sub

        Dim rutaMusica As String = Path.Combine(AppContext.BaseDirectory, "Recursos", "musica-login.mp3")
        If Not File.Exists(rutaMusica) Then
            'la musica es un adorno: si falta, queda en el registro y el login sigue
            RegistrarAviso("Música del login", "No se encontró el archivo " & rutaMusica)
            Exit Sub
        End If

        Dim resultado As Integer = EnviarAudio("open """ & rutaMusica & """ type mpegvideo alias musicaLogin", Nothing, 0, IntPtr.Zero)
        If resultado <> 0 Then
            RegistrarAviso("Música del login", "No se pudo abrir el archivo. Código MCI: " & resultado)
            Exit Sub
        End If

        musicaAbierta = True
        'repito el tema hasta ingresar o cerrar el formulario
        resultado = EnviarAudio("play musicaLogin repeat", Nothing, 0, IntPtr.Zero)
        If resultado <> 0 Then
            DetenerMusica()
            RegistrarAviso("Música del login", "No se pudo reproducir el archivo. Código MCI: " & resultado)
        End If
    End Sub

    Private Sub DetenerMusica()
        If Not musicaAbierta Then Exit Sub
        EnviarAudio("close musicaLogin", Nothing, 0, IntPtr.Zero)
        musicaAbierta = False
    End Sub

    Private Sub FrmLogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        DetenerMusica()
        If Me.BackgroundImage IsNot Nothing Then
            Me.BackgroundImage.Dispose()
            Me.BackgroundImage = Nothing
        End If
    End Sub
End Class

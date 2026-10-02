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
                        MessageBox.Show("Usuario o contraseña incorrectos.")
                        txtPassword.Clear()
                        txtPassword.Focus()
                        Exit Sub
                    End If

                    Dim fila As DataRow = tabla.Rows(0)

                    If Not VerificarClave(txtPassword.Text, fila("hash_contrasena").ToString, fila("salt").ToString) Then
                        MessageBox.Show("Usuario o contraseña incorrectos.")
                        txtPassword.Clear()
                        txtPassword.Focus()
                        Exit Sub
                    End If

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
        Catch ex As MySqlException
            MessageBox.Show("Error al iniciar sesión: " & ex.Message)
            Exit Sub
        End Try

        Dim principal As New FrmPrincipal()

        principal.Show()
        DetenerMusica()
        Me.Hide()

    End Sub

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        Catch ex As Exception
            MessageBox.Show("No se pudo cargar el fondo: " & ex.Message)
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
            MessageBox.Show("No se encontró la música del login.")
            Exit Sub
        End If

        Dim resultado As Integer = EnviarAudio("open """ & rutaMusica & """ type mpegvideo alias musicaLogin", Nothing, 0, IntPtr.Zero)
        If resultado <> 0 Then
            MessageBox.Show("No se pudo abrir la música del login. Código: " & resultado)
            Exit Sub
        End If

        musicaAbierta = True
        'repito el tema hasta ingresar o cerrar el formulario
        resultado = EnviarAudio("play musicaLogin repeat", Nothing, 0, IntPtr.Zero)
        If resultado <> 0 Then
            DetenerMusica()
            MessageBox.Show("No se pudo reproducir la música. Código: " & resultado)
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

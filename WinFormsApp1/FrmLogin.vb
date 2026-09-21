Public Class FrmLogin

    Public Sub New()
        InitializeComponent()
        ConfigurarPantalla()
    End Sub

    Private Sub ConfigurarPantalla()

        Me.Text = "Taller Mecánico - Iniciar sesión"
        Me.Size = New Size(500, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        Dim lblTitulo As New Label With {
            .Text = "TALLER MECÁNICO",
            .Font = New Font("Segoe UI", 22, FontStyle.Bold),
            .AutoSize = True,
            .Location = New Point(105, 60)
        }

        Dim lblSubtitulo As New Label With {
            .Text = "Sistema de Gestión",
            .Font = New Font("Segoe UI", 11),
            .AutoSize = True,
            .Location = New Point(165, 105)
        }

        Dim lblUsuario As New Label With {
            .Text = "Usuario",
            .Location = New Point(90, 170),
            .AutoSize = True
        }

        Dim txtUsuario As New TextBox With {
            .Name = "txtUsuario",
            .Location = New Point(90, 195),
            .Size = New Size(300, 30)
        }

        Dim lblPassword As New Label With {
            .Text = "Contraseña",
            .Location = New Point(90, 245),
            .AutoSize = True
        }

        Dim txtPassword As New TextBox With {
            .Name = "txtPassword",
            .Location = New Point(90, 270),
            .Size = New Size(300, 30),
            .UseSystemPasswordChar = True
        }

        Dim btnIngresar As New Button With {
            .Name = "btnIngresar",
            .Text = "INGRESAR",
            .Location = New Point(90, 330),
            .Size = New Size(300, 45)
        }

        AddHandler btnIngresar.Click, AddressOf BtnIngresar_Click

        Me.Controls.AddRange({
            lblTitulo,
            lblSubtitulo,
            lblUsuario,
            txtUsuario,
            lblPassword,
            txtPassword,
            btnIngresar
        })

    End Sub

    Private Sub BtnIngresar_Click(sender As Object, e As EventArgs)

        Dim principal As New FrmPrincipal()

        principal.Show()
        Me.Hide()

    End Sub

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
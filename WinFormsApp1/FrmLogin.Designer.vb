<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblTitulo = New Label()
        lblSubtitulo = New Label()
        lblUsuario = New Label()
        txtUsuario = New TextBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        btnIngresar = New Button()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 22F, FontStyle.Bold)
        lblTitulo.Location = New Point(105, 60)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(290, 41)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "TALLER MECÁNICO"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.Location = New Point(165, 105)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(138, 20)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Sistema de Gestión"
        '
        ' lblUsuario
        '
        lblUsuario.AutoSize = True
        lblUsuario.Location = New Point(90, 170)
        lblUsuario.Name = "lblUsuario"
        lblUsuario.Size = New Size(47, 15)
        lblUsuario.TabIndex = 2
        lblUsuario.Text = "Usuario"
        '
        ' txtUsuario
        '
        txtUsuario.Location = New Point(90, 195)
        txtUsuario.Name = "txtUsuario"
        txtUsuario.Size = New Size(300, 23)
        txtUsuario.TabIndex = 3
        '
        ' lblPassword
        '
        lblPassword.AutoSize = True
        lblPassword.Location = New Point(90, 245)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(67, 15)
        lblPassword.TabIndex = 4
        lblPassword.Text = "Contraseña"
        '
        ' txtPassword
        '
        txtPassword.Location = New Point(90, 270)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(300, 23)
        txtPassword.TabIndex = 5
        txtPassword.UseSystemPasswordChar = True
        '
        ' btnIngresar
        '
        btnIngresar.Location = New Point(90, 330)
        btnIngresar.Name = "btnIngresar"
        btnIngresar.Size = New Size(300, 45)
        btnIngresar.TabIndex = 6
        btnIngresar.Text = "INGRESAR"
        btnIngresar.UseVisualStyleBackColor = True
        '
        ' FrmLogin
        '
        AcceptButton = btnIngresar
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(484, 461)
        Controls.Add(lblTitulo)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblUsuario)
        Controls.Add(txtUsuario)
        Controls.Add(lblPassword)
        Controls.Add(txtPassword)
        Controls.Add(btnIngresar)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "FrmLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Taller Mecánico - Iniciar sesión"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents lblUsuario As Label
    Friend WithEvents txtUsuario As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents btnIngresar As Button

End Class

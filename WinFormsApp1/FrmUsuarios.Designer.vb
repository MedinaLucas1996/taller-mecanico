<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUsuarios
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        lblTitulo = New Label()
        lblSubtitulo = New Label()
        pnlDatos = New Panel()
        lblID = New Label()
        txtID = New TextBox()
        lblUsuario = New Label()
        txtUsuario = New TextBox()
        lblRol = New Label()
        cboRol = New ComboBox()
        lblNombreCompleto = New Label()
        txtNombreCompleto = New TextBox()
        lblMecanico = New Label()
        cboMecanico = New ComboBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        lblAyudaPassword = New Label()
        btnGuardar = New Button()
        btnModificar = New Button()
        btnEliminar = New Button()
        btnLimpiar = New Button()
        txtFiltro = New TextBox()
        dgvUsuarios = New DataGridView()
        pnlDatos.SuspendLayout()
        CType(dgvUsuarios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(40, 25)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(158, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Usuarios"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(43, 78)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(212, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Administración de usuarios"
        '
        ' pnlDatos
        '
        pnlDatos.BackColor = Color.White
        pnlDatos.Controls.Add(lblID)
        pnlDatos.Controls.Add(txtID)
        pnlDatos.Controls.Add(lblUsuario)
        pnlDatos.Controls.Add(txtUsuario)
        pnlDatos.Controls.Add(lblRol)
        pnlDatos.Controls.Add(cboRol)
        pnlDatos.Controls.Add(lblNombreCompleto)
        pnlDatos.Controls.Add(txtNombreCompleto)
        pnlDatos.Controls.Add(lblMecanico)
        pnlDatos.Controls.Add(cboMecanico)
        pnlDatos.Controls.Add(lblPassword)
        pnlDatos.Controls.Add(txtPassword)
        pnlDatos.Controls.Add(lblAyudaPassword)
        pnlDatos.Controls.Add(btnGuardar)
        pnlDatos.Controls.Add(btnModificar)
        pnlDatos.Controls.Add(btnEliminar)
        pnlDatos.Controls.Add(btnLimpiar)
        pnlDatos.Location = New Point(40, 120)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(930, 230)
        pnlDatos.TabIndex = 2
        '
        ' lblID
        '
        lblID.AutoSize = True
        lblID.ForeColor = Color.DimGray
        lblID.Location = New Point(20, 21)
        lblID.Name = "lblID"
        lblID.Size = New Size(24, 20)
        lblID.TabIndex = 0
        lblID.Text = "ID"
        '
        ' txtID
        '
        txtID.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtID.Location = New Point(170, 18)
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.Size = New Size(80, 27)
        txtID.TabIndex = 1
        txtID.TabStop = False
        '
        ' lblUsuario
        '
        lblUsuario.AutoSize = True
        lblUsuario.Location = New Point(20, 63)
        lblUsuario.Name = "lblUsuario"
        lblUsuario.Size = New Size(78, 20)
        lblUsuario.TabIndex = 2
        lblUsuario.Text = "Usuario (*)"
        '
        ' txtUsuario
        '
        txtUsuario.Location = New Point(170, 60)
        txtUsuario.MaxLength = 50
        txtUsuario.Name = "txtUsuario"
        txtUsuario.Size = New Size(270, 27)
        txtUsuario.TabIndex = 3
        '
        ' lblRol
        '
        lblRol.AutoSize = True
        lblRol.Location = New Point(470, 63)
        lblRol.Name = "lblRol"
        lblRol.Size = New Size(47, 20)
        lblRol.TabIndex = 4
        lblRol.Text = "Rol (*)"
        '
        ' cboRol
        '
        cboRol.DropDownStyle = ComboBoxStyle.DropDownList
        cboRol.FormattingEnabled = True
        cboRol.Items.AddRange(New Object() {"Seleccione un rol", "ADMINISTRADOR", "OPERADOR", "MECANICO"})
        cboRol.Location = New Point(590, 60)
        cboRol.Name = "cboRol"
        cboRol.Size = New Size(170, 28)
        cboRol.TabIndex = 5
        '
        ' lblNombreCompleto
        '
        lblNombreCompleto.AutoSize = True
        lblNombreCompleto.Location = New Point(20, 103)
        lblNombreCompleto.Name = "lblNombreCompleto"
        lblNombreCompleto.Size = New Size(143, 20)
        lblNombreCompleto.TabIndex = 6
        lblNombreCompleto.Text = "Nombre completo (*)"
        '
        ' txtNombreCompleto
        '
        txtNombreCompleto.Location = New Point(170, 100)
        txtNombreCompleto.MaxLength = 100
        txtNombreCompleto.Name = "txtNombreCompleto"
        txtNombreCompleto.Size = New Size(270, 27)
        txtNombreCompleto.TabIndex = 7
        '
        ' lblMecanico
        '
        lblMecanico.AutoSize = True
        lblMecanico.Location = New Point(470, 103)
        lblMecanico.Name = "lblMecanico"
        lblMecanico.Size = New Size(87, 20)
        lblMecanico.TabIndex = 8
        lblMecanico.Text = "Mecánico (*)"
        '
        ' cboMecanico
        '
        cboMecanico.DropDownStyle = ComboBoxStyle.DropDownList
        cboMecanico.Enabled = False
        cboMecanico.FormattingEnabled = True
        cboMecanico.Location = New Point(590, 100)
        cboMecanico.Name = "cboMecanico"
        cboMecanico.Size = New Size(170, 28)
        cboMecanico.TabIndex = 9
        '
        ' lblPassword
        '
        lblPassword.AutoSize = True
        lblPassword.Location = New Point(20, 143)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(100, 20)
        lblPassword.TabIndex = 10
        lblPassword.Text = "Contraseña (*)"
        '
        ' txtPassword
        '
        txtPassword.Location = New Point(170, 140)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(270, 27)
        txtPassword.TabIndex = 11
        txtPassword.UseSystemPasswordChar = True
        '
        ' lblAyudaPassword
        '
        lblAyudaPassword.AutoSize = True
        lblAyudaPassword.Font = New Font("Segoe UI", 8.5F)
        lblAyudaPassword.ForeColor = Color.DimGray
        lblAyudaPassword.Location = New Point(170, 172)
        lblAyudaPassword.Name = "lblAyudaPassword"
        lblAyudaPassword.Size = New Size(316, 20)
        lblAyudaPassword.TabIndex = 12
        lblAyudaPassword.Text = "Al modificar, dejar vacía para conservar la actual."
        '
        ' btnGuardar
        '
        btnGuardar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGuardar.Cursor = Cursors.Hand
        btnGuardar.FlatAppearance.BorderSize = 0
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGuardar.FlatStyle = FlatStyle.Flat
        btnGuardar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGuardar.ForeColor = Color.White
        btnGuardar.Location = New Point(790, 18)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(120, 40)
        btnGuardar.TabIndex = 13
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = False
        '
        ' btnModificar
        '
        btnModificar.BackColor = Color.White
        btnModificar.Cursor = Cursors.Hand
        btnModificar.FlatAppearance.BorderColor = Color.Silver
        btnModificar.FlatStyle = FlatStyle.Flat
        btnModificar.Font = New Font("Segoe UI", 10F)
        btnModificar.Location = New Point(790, 66)
        btnModificar.Name = "btnModificar"
        btnModificar.Size = New Size(120, 40)
        btnModificar.TabIndex = 14
        btnModificar.Text = "Modificar"
        btnModificar.UseVisualStyleBackColor = False
        '
        ' btnEliminar
        '
        btnEliminar.BackColor = Color.White
        btnEliminar.Cursor = Cursors.Hand
        btnEliminar.FlatAppearance.BorderColor = Color.Silver
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnEliminar.Font = New Font("Segoe UI", 10F)
        btnEliminar.ForeColor = Color.Firebrick
        btnEliminar.Location = New Point(790, 114)
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Size = New Size(120, 40)
        btnEliminar.TabIndex = 15
        btnEliminar.Text = "Dar de baja"
        btnEliminar.UseVisualStyleBackColor = False
        '
        ' btnLimpiar
        '
        btnLimpiar.BackColor = Color.White
        btnLimpiar.Cursor = Cursors.Hand
        btnLimpiar.FlatAppearance.BorderColor = Color.Silver
        btnLimpiar.FlatStyle = FlatStyle.Flat
        btnLimpiar.Font = New Font("Segoe UI", 10F)
        btnLimpiar.Location = New Point(790, 162)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New Size(120, 40)
        btnLimpiar.TabIndex = 16
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        ' txtFiltro
        '
        txtFiltro.Font = New Font("Segoe UI", 11F)
        txtFiltro.Location = New Point(40, 370)
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por usuario o nombre..."
        txtFiltro.Size = New Size(420, 32)
        txtFiltro.TabIndex = 3
        '
        ' dgvUsuarios
        '
        dgvUsuarios.AllowUserToAddRows = False
        dgvUsuarios.AllowUserToDeleteRows = False
        dgvUsuarios.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvUsuarios.BackgroundColor = Color.White
        dgvUsuarios.BorderStyle = BorderStyle.FixedSingle
        dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvUsuarios.Location = New Point(40, 420)
        dgvUsuarios.MultiSelect = False
        dgvUsuarios.Name = "dgvUsuarios"
        dgvUsuarios.ReadOnly = True
        dgvUsuarios.RowHeadersVisible = False
        dgvUsuarios.RowHeadersWidth = 51
        dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvUsuarios.Size = New Size(930, 240)
        dgvUsuarios.TabIndex = 4
        '
        ' FrmUsuarios
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1010, 690)
        Controls.Add(dgvUsuarios)
        Controls.Add(txtFiltro)
        Controls.Add(pnlDatos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmUsuarios"
        Text = "Usuarios"
        pnlDatos.ResumeLayout(False)
        pnlDatos.PerformLayout()
        CType(dgvUsuarios, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblID As Label
    Friend WithEvents txtID As TextBox
    Friend WithEvents lblUsuario As Label
    Friend WithEvents txtUsuario As TextBox
    Friend WithEvents lblRol As Label
    Friend WithEvents cboRol As ComboBox
    Friend WithEvents lblNombreCompleto As Label
    Friend WithEvents txtNombreCompleto As TextBox
    Friend WithEvents lblMecanico As Label
    Friend WithEvents cboMecanico As ComboBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblAyudaPassword As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents dgvUsuarios As DataGridView
End Class

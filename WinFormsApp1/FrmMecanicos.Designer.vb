<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMecanicos
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
        lblEstado = New Label()
        lblNombreCompleto = New Label()
        txtNombreCompleto = New TextBox()
        lblEspecialidad = New Label()
        txtEspecialidad = New TextBox()
        lblTelefono = New Label()
        txtTelefono = New TextBox()
        btnGuardar = New Button()
        btnModificar = New Button()
        btnEliminar = New Button()
        btnLimpiar = New Button()
        txtFiltro = New TextBox()
        dgvMecanicos = New DataGridView()
        pnlDatos.SuspendLayout()
        CType(dgvMecanicos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(40, 25)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(203, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Mecánicos"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(43, 78)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(240, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Administración de mecánicos"
        '
        ' pnlDatos
        '
        pnlDatos.BackColor = Color.White
        pnlDatos.Controls.Add(lblID)
        pnlDatos.Controls.Add(txtID)
        pnlDatos.Controls.Add(lblEstado)
        pnlDatos.Controls.Add(lblNombreCompleto)
        pnlDatos.Controls.Add(txtNombreCompleto)
        pnlDatos.Controls.Add(lblEspecialidad)
        pnlDatos.Controls.Add(txtEspecialidad)
        pnlDatos.Controls.Add(lblTelefono)
        pnlDatos.Controls.Add(txtTelefono)
        pnlDatos.Controls.Add(btnGuardar)
        pnlDatos.Controls.Add(btnModificar)
        pnlDatos.Controls.Add(btnEliminar)
        pnlDatos.Controls.Add(btnLimpiar)
        pnlDatos.Location = New Point(40, 120)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(930, 220)
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
        ' lblEstado
        '
        lblEstado.ForeColor = Color.DimGray
        lblEstado.Location = New Point(265, 21)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(400, 20)
        lblEstado.TabIndex = 2
        '
        ' lblNombreCompleto
        '
        lblNombreCompleto.AutoSize = True
        lblNombreCompleto.Location = New Point(20, 63)
        lblNombreCompleto.Name = "lblNombreCompleto"
        lblNombreCompleto.Size = New Size(151, 20)
        lblNombreCompleto.TabIndex = 3
        lblNombreCompleto.Text = "Nombre completo (*)"
        '
        ' txtNombreCompleto
        '
        txtNombreCompleto.Location = New Point(170, 60)
        txtNombreCompleto.MaxLength = 100
        txtNombreCompleto.Name = "txtNombreCompleto"
        txtNombreCompleto.Size = New Size(400, 27)
        txtNombreCompleto.TabIndex = 4
        '
        ' lblEspecialidad
        '
        lblEspecialidad.AutoSize = True
        lblEspecialidad.Location = New Point(20, 103)
        lblEspecialidad.Name = "lblEspecialidad"
        lblEspecialidad.Size = New Size(93, 20)
        lblEspecialidad.TabIndex = 5
        lblEspecialidad.Text = "Especialidad"
        '
        ' txtEspecialidad
        '
        txtEspecialidad.Location = New Point(170, 100)
        txtEspecialidad.MaxLength = 80
        txtEspecialidad.Name = "txtEspecialidad"
        txtEspecialidad.Size = New Size(400, 27)
        txtEspecialidad.TabIndex = 6
        '
        ' lblTelefono
        '
        lblTelefono.AutoSize = True
        lblTelefono.Location = New Point(20, 143)
        lblTelefono.Name = "lblTelefono"
        lblTelefono.Size = New Size(67, 20)
        lblTelefono.TabIndex = 7
        lblTelefono.Text = "Teléfono"
        '
        ' txtTelefono
        '
        txtTelefono.Location = New Point(170, 140)
        txtTelefono.MaxLength = 30
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(220, 27)
        txtTelefono.TabIndex = 8
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
        btnGuardar.TabIndex = 9
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
        btnModificar.TabIndex = 10
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
        btnEliminar.TabIndex = 11
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
        btnLimpiar.TabIndex = 12
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        ' txtFiltro
        '
        txtFiltro.Font = New Font("Segoe UI", 11F)
        txtFiltro.Location = New Point(40, 360)
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por nombre o especialidad..."
        txtFiltro.Size = New Size(420, 32)
        txtFiltro.TabIndex = 3
        '
        ' dgvMecanicos
        '
        dgvMecanicos.AllowUserToAddRows = False
        dgvMecanicos.AllowUserToDeleteRows = False
        dgvMecanicos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvMecanicos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMecanicos.BackgroundColor = Color.White
        dgvMecanicos.BorderStyle = BorderStyle.FixedSingle
        dgvMecanicos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMecanicos.Location = New Point(40, 410)
        dgvMecanicos.MultiSelect = False
        dgvMecanicos.Name = "dgvMecanicos"
        dgvMecanicos.ReadOnly = True
        dgvMecanicos.RowHeadersVisible = False
        dgvMecanicos.RowHeadersWidth = 51
        dgvMecanicos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMecanicos.Size = New Size(930, 250)
        dgvMecanicos.TabIndex = 4
        '
        ' FrmMecanicos
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1010, 690)
        Controls.Add(dgvMecanicos)
        Controls.Add(txtFiltro)
        Controls.Add(pnlDatos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmMecanicos"
        Text = "Mecánicos"
        pnlDatos.ResumeLayout(False)
        pnlDatos.PerformLayout()
        CType(dgvMecanicos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblID As Label
    Friend WithEvents txtID As TextBox
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblNombreCompleto As Label
    Friend WithEvents txtNombreCompleto As TextBox
    Friend WithEvents lblEspecialidad As Label
    Friend WithEvents txtEspecialidad As TextBox
    Friend WithEvents lblTelefono As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents dgvMecanicos As DataGridView
End Class

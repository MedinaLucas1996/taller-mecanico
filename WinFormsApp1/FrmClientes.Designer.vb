<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmClientes
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
        lblRazonSocial = New Label()
        txtRazonSocial = New TextBox()
        lblDocumento = New Label()
        txtDocumento = New TextBox()
        lblDomicilio = New Label()
        txtDomicilio = New TextBox()
        lblLocalidad = New Label()
        txtLocalidad = New TextBox()
        lblTelefono = New Label()
        txtTelefono = New TextBox()
        lblEmail = New Label()
        txtEmail = New TextBox()
        lblObservaciones = New Label()
        txtObservaciones = New TextBox()
        btnGuardar = New Button()
        btnModificar = New Button()
        btnEliminar = New Button()
        btnLimpiar = New Button()
        txtFiltro = New TextBox()
        chkBajas = New CheckBox()
        dgvClientes = New DataGridView()
        pnlDatos.SuspendLayout()
        CType(dgvClientes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(40, 25)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(151, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Clientes"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(43, 78)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(203, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Administración de clientes"
        '
        ' pnlDatos
        '
        pnlDatos.BackColor = Color.White
        pnlDatos.Controls.Add(lblID)
        pnlDatos.Controls.Add(txtID)
        pnlDatos.Controls.Add(lblRazonSocial)
        pnlDatos.Controls.Add(txtRazonSocial)
        pnlDatos.Controls.Add(lblDocumento)
        pnlDatos.Controls.Add(txtDocumento)
        pnlDatos.Controls.Add(lblDomicilio)
        pnlDatos.Controls.Add(txtDomicilio)
        pnlDatos.Controls.Add(lblLocalidad)
        pnlDatos.Controls.Add(txtLocalidad)
        pnlDatos.Controls.Add(lblTelefono)
        pnlDatos.Controls.Add(txtTelefono)
        pnlDatos.Controls.Add(lblEmail)
        pnlDatos.Controls.Add(txtEmail)
        pnlDatos.Controls.Add(lblObservaciones)
        pnlDatos.Controls.Add(txtObservaciones)
        pnlDatos.Controls.Add(btnGuardar)
        pnlDatos.Controls.Add(btnModificar)
        pnlDatos.Controls.Add(btnEliminar)
        pnlDatos.Controls.Add(btnLimpiar)
        pnlDatos.Location = New Point(40, 120)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(930, 250)
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
        txtID.Location = New Point(140, 18)
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.Size = New Size(80, 27)
        txtID.TabIndex = 1
        txtID.TabStop = False
        '
        ' lblRazonSocial
        '
        lblRazonSocial.AutoSize = True
        lblRazonSocial.Location = New Point(20, 63)
        lblRazonSocial.Name = "lblRazonSocial"
        lblRazonSocial.Size = New Size(83, 20)
        lblRazonSocial.TabIndex = 2
        lblRazonSocial.Text = "Nombre (*)"
        '
        ' txtRazonSocial
        '
        txtRazonSocial.Location = New Point(140, 60)
        txtRazonSocial.MaxLength = 150
        txtRazonSocial.Name = "txtRazonSocial"
        txtRazonSocial.Size = New Size(300, 27)
        txtRazonSocial.TabIndex = 3
        '
        ' lblDocumento
        '
        lblDocumento.AutoSize = True
        lblDocumento.Location = New Point(470, 63)
        lblDocumento.Name = "lblDocumento"
        lblDocumento.Size = New Size(104, 20)
        lblDocumento.TabIndex = 4
        lblDocumento.Text = "Documento (*)"
        '
        ' txtDocumento
        '
        txtDocumento.Location = New Point(590, 60)
        txtDocumento.MaxLength = 20
        txtDocumento.Name = "txtDocumento"
        txtDocumento.Size = New Size(170, 27)
        txtDocumento.TabIndex = 5
        '
        ' lblDomicilio
        '
        lblDomicilio.AutoSize = True
        lblDomicilio.Location = New Point(20, 103)
        lblDomicilio.Name = "lblDomicilio"
        lblDomicilio.Size = New Size(72, 20)
        lblDomicilio.TabIndex = 6
        lblDomicilio.Text = "Domicilio"
        '
        ' txtDomicilio
        '
        txtDomicilio.Location = New Point(140, 100)
        txtDomicilio.MaxLength = 200
        txtDomicilio.Name = "txtDomicilio"
        txtDomicilio.Size = New Size(300, 27)
        txtDomicilio.TabIndex = 7
        '
        ' lblLocalidad
        '
        lblLocalidad.AutoSize = True
        lblLocalidad.Location = New Point(470, 103)
        lblLocalidad.Name = "lblLocalidad"
        lblLocalidad.Size = New Size(74, 20)
        lblLocalidad.TabIndex = 8
        lblLocalidad.Text = "Localidad"
        '
        ' txtLocalidad
        '
        txtLocalidad.Location = New Point(590, 100)
        txtLocalidad.MaxLength = 100
        txtLocalidad.Name = "txtLocalidad"
        txtLocalidad.Size = New Size(170, 27)
        txtLocalidad.TabIndex = 9
        '
        ' lblTelefono
        '
        lblTelefono.AutoSize = True
        lblTelefono.Location = New Point(20, 143)
        lblTelefono.Name = "lblTelefono"
        lblTelefono.Size = New Size(67, 20)
        lblTelefono.TabIndex = 10
        lblTelefono.Text = "Teléfono"
        '
        ' txtTelefono
        '
        txtTelefono.Location = New Point(140, 140)
        txtTelefono.MaxLength = 30
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(300, 27)
        txtTelefono.TabIndex = 11
        '
        ' lblEmail
        '
        lblEmail.AutoSize = True
        lblEmail.Location = New Point(470, 143)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New Size(54, 20)
        lblEmail.TabIndex = 12
        lblEmail.Text = "Correo"
        '
        ' txtEmail
        '
        txtEmail.Location = New Point(590, 140)
        txtEmail.MaxLength = 100
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(170, 27)
        txtEmail.TabIndex = 13
        '
        ' lblObservaciones
        '
        lblObservaciones.AutoSize = True
        lblObservaciones.Location = New Point(20, 183)
        lblObservaciones.Name = "lblObservaciones"
        lblObservaciones.Size = New Size(105, 20)
        lblObservaciones.TabIndex = 14
        lblObservaciones.Text = "Observaciones"
        '
        ' txtObservaciones
        '
        txtObservaciones.Location = New Point(140, 180)
        txtObservaciones.Multiline = True
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.ScrollBars = ScrollBars.Vertical
        txtObservaciones.Size = New Size(620, 50)
        txtObservaciones.TabIndex = 15
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
        btnGuardar.TabIndex = 16
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
        btnModificar.TabIndex = 17
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
        btnEliminar.TabIndex = 18
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
        btnLimpiar.TabIndex = 19
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        ' txtFiltro
        '
        txtFiltro.Font = New Font("Segoe UI", 11F)
        txtFiltro.Location = New Point(40, 390)
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por nombre o documento..."
        txtFiltro.Size = New Size(420, 32)
        txtFiltro.TabIndex = 3
        '
        ' chkBajas
        '
        chkBajas.AutoSize = True
        chkBajas.Location = New Point(480, 394)
        chkBajas.Name = "chkBajas"
        chkBajas.Size = New Size(190, 24)
        chkBajas.TabIndex = 5
        chkBajas.Text = "Mostrar dados de baja"
        chkBajas.UseVisualStyleBackColor = True
        '
        ' dgvClientes
        '
        dgvClientes.AllowUserToAddRows = False
        dgvClientes.AllowUserToDeleteRows = False
        dgvClientes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvClientes.BackgroundColor = Color.White
        dgvClientes.BorderStyle = BorderStyle.FixedSingle
        dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvClientes.Location = New Point(40, 440)
        dgvClientes.MultiSelect = False
        dgvClientes.Name = "dgvClientes"
        dgvClientes.ReadOnly = True
        dgvClientes.RowHeadersVisible = False
        dgvClientes.RowHeadersWidth = 51
        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvClientes.Size = New Size(930, 220)
        dgvClientes.TabIndex = 4
        '
        ' FrmClientes
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1010, 690)
        Controls.Add(dgvClientes)
        Controls.Add(chkBajas)
        Controls.Add(txtFiltro)
        Controls.Add(pnlDatos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmClientes"
        Text = "Clientes"
        pnlDatos.ResumeLayout(False)
        pnlDatos.PerformLayout()
        CType(dgvClientes, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblID As Label
    Friend WithEvents txtID As TextBox
    Friend WithEvents lblRazonSocial As Label
    Friend WithEvents txtRazonSocial As TextBox
    Friend WithEvents lblDocumento As Label
    Friend WithEvents txtDocumento As TextBox
    Friend WithEvents lblDomicilio As Label
    Friend WithEvents txtDomicilio As TextBox
    Friend WithEvents lblLocalidad As Label
    Friend WithEvents txtLocalidad As TextBox
    Friend WithEvents lblTelefono As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblObservaciones As Label
    Friend WithEvents txtObservaciones As TextBox
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents chkBajas As CheckBox
    Friend WithEvents dgvClientes As DataGridView
End Class

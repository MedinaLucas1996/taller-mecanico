<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAltaRapida
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
        pnlTitular = New Panel()
        lblTitularTitulo = New Label()
        rbExistente = New RadioButton()
        rbNuevo = New RadioButton()
        pnlExistente = New Panel()
        txtBuscarCliente = New TextBox()
        dgvClientes = New DataGridView()
        lblClienteElegido = New Label()
        pnlNuevo = New Panel()
        lblRazonSocial = New Label()
        txtRazonSocial = New TextBox()
        lblDocumento = New Label()
        txtDocumento = New TextBox()
        lblTelefono = New Label()
        txtTelefono = New TextBox()
        pnlVehiculo = New Panel()
        lblVehiculoTitulo = New Label()
        lblPatente = New Label()
        txtPatente = New TextBox()
        lblMarca = New Label()
        cboMarca = New ComboBox()
        lblModelo = New Label()
        cboModelo = New ComboBox()
        lblAyudaModelo = New Label()
        chkAnio = New CheckBox()
        nudAnio = New NumericUpDown()
        lblColor = New Label()
        txtColor = New TextBox()
        btnCancelar = New Button()
        btnRegistrar = New Button()
        pnlTitular.SuspendLayout()
        pnlExistente.SuspendLayout()
        CType(dgvClientes, ComponentModel.ISupportInitialize).BeginInit()
        pnlNuevo.SuspendLayout()
        pnlVehiculo.SuspendLayout()
        CType(nudAnio, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblTitulo.Location = New Point(24, 12)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(220, 32)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Registrar vehículo"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(26, 48)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(710, 24)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Indique el titular y los datos del vehículo. Al registrar, la recepción continúa con esa patente."
        '
        ' pnlTitular
        '
        pnlTitular.BackColor = Color.White
        pnlTitular.Controls.Add(lblTitularTitulo)
        pnlTitular.Controls.Add(rbExistente)
        pnlTitular.Controls.Add(rbNuevo)
        pnlTitular.Controls.Add(pnlExistente)
        pnlTitular.Controls.Add(pnlNuevo)
        pnlTitular.Location = New Point(24, 80)
        pnlTitular.Name = "pnlTitular"
        pnlTitular.Size = New Size(712, 310)
        pnlTitular.TabIndex = 2
        '
        ' lblTitularTitulo
        '
        lblTitularTitulo.AutoSize = True
        lblTitularTitulo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblTitularTitulo.Location = New Point(16, 10)
        lblTitularTitulo.Name = "lblTitularTitulo"
        lblTitularTitulo.Size = New Size(80, 23)
        lblTitularTitulo.TabIndex = 0
        lblTitularTitulo.Text = "1. Titular"
        '
        ' rbExistente
        '
        rbExistente.AutoSize = True
        rbExistente.Checked = True
        rbExistente.Location = New Point(16, 42)
        rbExistente.Name = "rbExistente"
        rbExistente.Size = New Size(180, 24)
        rbExistente.TabIndex = 1
        rbExistente.TabStop = True
        rbExistente.Text = "Cliente ya registrado"
        rbExistente.UseVisualStyleBackColor = True
        '
        ' rbNuevo
        '
        rbNuevo.AutoSize = True
        rbNuevo.Location = New Point(250, 42)
        rbNuevo.Name = "rbNuevo"
        rbNuevo.Size = New Size(125, 24)
        rbNuevo.TabIndex = 2
        rbNuevo.Text = "Cliente nuevo"
        rbNuevo.UseVisualStyleBackColor = True
        '
        ' pnlExistente
        '
        pnlExistente.Controls.Add(txtBuscarCliente)
        pnlExistente.Controls.Add(dgvClientes)
        pnlExistente.Controls.Add(lblClienteElegido)
        pnlExistente.Location = New Point(16, 76)
        pnlExistente.Name = "pnlExistente"
        pnlExistente.Size = New Size(680, 224)
        pnlExistente.TabIndex = 3
        '
        ' txtBuscarCliente
        '
        txtBuscarCliente.Location = New Point(0, 2)
        txtBuscarCliente.MaxLength = 150
        txtBuscarCliente.Name = "txtBuscarCliente"
        txtBuscarCliente.PlaceholderText = "Buscar por documento o nombre..."
        txtBuscarCliente.Size = New Size(420, 27)
        txtBuscarCliente.TabIndex = 0
        '
        ' dgvClientes
        '
        dgvClientes.AllowUserToAddRows = False
        dgvClientes.AllowUserToDeleteRows = False
        dgvClientes.AllowUserToResizeRows = False
        dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvClientes.BackgroundColor = Color.White
        dgvClientes.BorderStyle = BorderStyle.FixedSingle
        dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvClientes.Location = New Point(0, 42)
        dgvClientes.MultiSelect = False
        dgvClientes.Name = "dgvClientes"
        dgvClientes.ReadOnly = True
        dgvClientes.RowHeadersVisible = False
        dgvClientes.RowHeadersWidth = 51
        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvClientes.Size = New Size(680, 146)
        dgvClientes.TabIndex = 1
        '
        ' lblClienteElegido
        '
        lblClienteElegido.AutoEllipsis = True
        lblClienteElegido.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblClienteElegido.Location = New Point(0, 196)
        lblClienteElegido.Name = "lblClienteElegido"
        lblClienteElegido.Size = New Size(680, 24)
        lblClienteElegido.TabIndex = 2
        lblClienteElegido.Text = "Titular elegido: ninguno"
        '
        ' pnlNuevo
        '
        pnlNuevo.Controls.Add(lblRazonSocial)
        pnlNuevo.Controls.Add(txtRazonSocial)
        pnlNuevo.Controls.Add(lblDocumento)
        pnlNuevo.Controls.Add(txtDocumento)
        pnlNuevo.Controls.Add(lblTelefono)
        pnlNuevo.Controls.Add(txtTelefono)
        pnlNuevo.Location = New Point(16, 76)
        pnlNuevo.Name = "pnlNuevo"
        pnlNuevo.Size = New Size(680, 224)
        pnlNuevo.TabIndex = 4
        pnlNuevo.Visible = False
        '
        ' lblRazonSocial
        '
        lblRazonSocial.AutoSize = True
        lblRazonSocial.Location = New Point(0, 10)
        lblRazonSocial.Name = "lblRazonSocial"
        lblRazonSocial.Size = New Size(190, 20)
        lblRazonSocial.TabIndex = 0
        lblRazonSocial.Text = "Nombre o razón social (*)"
        '
        ' txtRazonSocial
        '
        txtRazonSocial.Location = New Point(220, 6)
        txtRazonSocial.MaxLength = 150
        txtRazonSocial.Name = "txtRazonSocial"
        txtRazonSocial.Size = New Size(440, 27)
        txtRazonSocial.TabIndex = 1
        '
        ' lblDocumento
        '
        lblDocumento.AutoSize = True
        lblDocumento.Location = New Point(0, 54)
        lblDocumento.Name = "lblDocumento"
        lblDocumento.Size = New Size(104, 20)
        lblDocumento.TabIndex = 2
        lblDocumento.Text = "Documento (*)"
        '
        ' txtDocumento
        '
        txtDocumento.Location = New Point(220, 50)
        txtDocumento.MaxLength = 20
        txtDocumento.Name = "txtDocumento"
        txtDocumento.Size = New Size(220, 27)
        txtDocumento.TabIndex = 3
        '
        ' lblTelefono
        '
        lblTelefono.AutoSize = True
        lblTelefono.Location = New Point(0, 98)
        lblTelefono.Name = "lblTelefono"
        lblTelefono.Size = New Size(67, 20)
        lblTelefono.TabIndex = 4
        lblTelefono.Text = "Teléfono"
        '
        ' txtTelefono
        '
        txtTelefono.Location = New Point(220, 94)
        txtTelefono.MaxLength = 30
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(220, 27)
        txtTelefono.TabIndex = 5
        '
        ' pnlVehiculo
        '
        pnlVehiculo.BackColor = Color.White
        pnlVehiculo.Controls.Add(lblVehiculoTitulo)
        pnlVehiculo.Controls.Add(lblPatente)
        pnlVehiculo.Controls.Add(txtPatente)
        pnlVehiculo.Controls.Add(lblMarca)
        pnlVehiculo.Controls.Add(cboMarca)
        pnlVehiculo.Controls.Add(lblModelo)
        pnlVehiculo.Controls.Add(cboModelo)
        pnlVehiculo.Controls.Add(lblAyudaModelo)
        pnlVehiculo.Controls.Add(chkAnio)
        pnlVehiculo.Controls.Add(nudAnio)
        pnlVehiculo.Controls.Add(lblColor)
        pnlVehiculo.Controls.Add(txtColor)
        pnlVehiculo.Location = New Point(24, 400)
        pnlVehiculo.Name = "pnlVehiculo"
        pnlVehiculo.Size = New Size(712, 204)
        pnlVehiculo.TabIndex = 3
        '
        ' lblVehiculoTitulo
        '
        lblVehiculoTitulo.AutoSize = True
        lblVehiculoTitulo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblVehiculoTitulo.Location = New Point(16, 10)
        lblVehiculoTitulo.Name = "lblVehiculoTitulo"
        lblVehiculoTitulo.Size = New Size(100, 23)
        lblVehiculoTitulo.TabIndex = 0
        lblVehiculoTitulo.Text = "2. Vehículo"
        '
        ' lblPatente
        '
        lblPatente.AutoSize = True
        lblPatente.Location = New Point(16, 50)
        lblPatente.Name = "lblPatente"
        lblPatente.Size = New Size(80, 20)
        lblPatente.TabIndex = 1
        lblPatente.Text = "Patente (*)"
        '
        ' txtPatente
        '
        txtPatente.CharacterCasing = CharacterCasing.Upper
        txtPatente.Location = New Point(130, 46)
        txtPatente.MaxLength = 10
        txtPatente.Name = "txtPatente"
        txtPatente.Size = New Size(170, 27)
        txtPatente.TabIndex = 2
        '
        ' lblMarca
        '
        lblMarca.AutoSize = True
        lblMarca.Location = New Point(16, 92)
        lblMarca.Name = "lblMarca"
        lblMarca.Size = New Size(72, 20)
        lblMarca.TabIndex = 3
        lblMarca.Text = "Marca (*)"
        '
        ' cboMarca
        '
        cboMarca.DropDownStyle = ComboBoxStyle.DropDownList
        cboMarca.FormattingEnabled = True
        cboMarca.Location = New Point(130, 88)
        cboMarca.Name = "cboMarca"
        cboMarca.Size = New Size(230, 28)
        cboMarca.TabIndex = 4
        '
        ' lblModelo
        '
        lblModelo.AutoSize = True
        lblModelo.Location = New Point(378, 92)
        lblModelo.Name = "lblModelo"
        lblModelo.Size = New Size(82, 20)
        lblModelo.TabIndex = 5
        lblModelo.Text = "Modelo (*)"
        '
        ' cboModelo
        '
        cboModelo.DropDownStyle = ComboBoxStyle.DropDownList
        cboModelo.FormattingEnabled = True
        cboModelo.Location = New Point(466, 88)
        cboModelo.Name = "cboModelo"
        cboModelo.Size = New Size(230, 28)
        cboModelo.TabIndex = 6
        '
        ' lblAyudaModelo
        '
        lblAyudaModelo.Font = New Font("Segoe UI", 8.5F)
        lblAyudaModelo.ForeColor = Color.DimGray
        lblAyudaModelo.Location = New Point(130, 124)
        lblAyudaModelo.Name = "lblAyudaModelo"
        lblAyudaModelo.Size = New Size(566, 20)
        lblAyudaModelo.TabIndex = 7
        lblAyudaModelo.Text = "Si la marca o el modelo no figura, se agrega en ""Marcas y modelos""."
        '
        ' chkAnio
        '
        chkAnio.AutoSize = True
        chkAnio.Location = New Point(16, 158)
        chkAnio.Name = "chkAnio"
        chkAnio.Size = New Size(58, 24)
        chkAnio.TabIndex = 8
        chkAnio.Text = "Año"
        chkAnio.UseVisualStyleBackColor = True
        '
        ' nudAnio
        '
        nudAnio.Enabled = False
        nudAnio.Location = New Point(130, 156)
        nudAnio.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        nudAnio.Minimum = New Decimal(New Integer() {1900, 0, 0, 0})
        nudAnio.Name = "nudAnio"
        nudAnio.Size = New Size(100, 27)
        nudAnio.TabIndex = 9
        nudAnio.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        '
        ' lblColor
        '
        lblColor.AutoSize = True
        lblColor.Location = New Point(378, 160)
        lblColor.Name = "lblColor"
        lblColor.Size = New Size(45, 20)
        lblColor.TabIndex = 10
        lblColor.Text = "Color"
        '
        ' txtColor
        '
        txtColor.Location = New Point(466, 156)
        txtColor.MaxLength = 30
        txtColor.Name = "txtColor"
        txtColor.Size = New Size(230, 27)
        txtColor.TabIndex = 11
        '
        ' btnCancelar
        '
        btnCancelar.BackColor = Color.White
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.DialogResult = DialogResult.Cancel
        btnCancelar.FlatAppearance.BorderColor = Color.Silver
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 10F)
        btnCancelar.Location = New Point(346, 618)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(130, 42)
        btnCancelar.TabIndex = 5
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = False
        '
        ' btnRegistrar
        '
        btnRegistrar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnRegistrar.Cursor = Cursors.Hand
        btnRegistrar.FlatAppearance.BorderSize = 0
        btnRegistrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnRegistrar.FlatStyle = FlatStyle.Flat
        btnRegistrar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnRegistrar.ForeColor = Color.White
        btnRegistrar.Location = New Point(486, 618)
        btnRegistrar.Name = "btnRegistrar"
        btnRegistrar.Size = New Size(250, 42)
        btnRegistrar.TabIndex = 4
        btnRegistrar.Text = " Registrar y continuar"
        btnRegistrar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnRegistrar.UseVisualStyleBackColor = False
        '
        ' FrmAltaRapida
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        CancelButton = btnCancelar
        ClientSize = New Size(760, 676)
        Controls.Add(btnRegistrar)
        Controls.Add(btnCancelar)
        Controls.Add(pnlVehiculo)
        Controls.Add(pnlTitular)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "FrmAltaRapida"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Registrar vehículo"
        pnlTitular.ResumeLayout(False)
        pnlTitular.PerformLayout()
        pnlExistente.ResumeLayout(False)
        pnlExistente.PerformLayout()
        CType(dgvClientes, ComponentModel.ISupportInitialize).EndInit()
        pnlNuevo.ResumeLayout(False)
        pnlNuevo.PerformLayout()
        pnlVehiculo.ResumeLayout(False)
        pnlVehiculo.PerformLayout()
        CType(nudAnio, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlTitular As Panel
    Friend WithEvents lblTitularTitulo As Label
    Friend WithEvents rbExistente As RadioButton
    Friend WithEvents rbNuevo As RadioButton
    Friend WithEvents pnlExistente As Panel
    Friend WithEvents txtBuscarCliente As TextBox
    Friend WithEvents dgvClientes As DataGridView
    Friend WithEvents lblClienteElegido As Label
    Friend WithEvents pnlNuevo As Panel
    Friend WithEvents lblRazonSocial As Label
    Friend WithEvents txtRazonSocial As TextBox
    Friend WithEvents lblDocumento As Label
    Friend WithEvents txtDocumento As TextBox
    Friend WithEvents lblTelefono As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents pnlVehiculo As Panel
    Friend WithEvents lblVehiculoTitulo As Label
    Friend WithEvents lblPatente As Label
    Friend WithEvents txtPatente As TextBox
    Friend WithEvents lblMarca As Label
    Friend WithEvents cboMarca As ComboBox
    Friend WithEvents lblModelo As Label
    Friend WithEvents cboModelo As ComboBox
    Friend WithEvents lblAyudaModelo As Label
    Friend WithEvents chkAnio As CheckBox
    Friend WithEvents nudAnio As NumericUpDown
    Friend WithEvents lblColor As Label
    Friend WithEvents txtColor As TextBox
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnRegistrar As Button
End Class

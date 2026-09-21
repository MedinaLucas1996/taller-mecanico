<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVehiculos
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
        lblTitular = New Label()
        cboTitular = New ComboBox()
        lblPatente = New Label()
        txtPatente = New TextBox()
        lblMarca = New Label()
        cboMarca = New ComboBox()
        lblModelo = New Label()
        cboModelo = New ComboBox()
        lblAnio = New Label()
        nudAnio = New NumericUpDown()
        lblColor = New Label()
        txtColor = New TextBox()
        lblMotor = New Label()
        txtMotor = New TextBox()
        lblChasis = New Label()
        txtChasis = New TextBox()
        lblKilometraje = New Label()
        nudKilometraje = New NumericUpDown()
        lblObservaciones = New Label()
        txtObservaciones = New TextBox()
        btnGuardar = New Button()
        btnModificar = New Button()
        btnEliminar = New Button()
        btnLimpiar = New Button()
        txtFiltro = New TextBox()
        dgvVehiculos = New DataGridView()
        pnlDatos.SuspendLayout()
        CType(nudAnio, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudKilometraje, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvVehiculos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(40, 25)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(180, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Vehículos"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(43, 78)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(221, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Administración de vehículos"
        '
        ' pnlDatos
        '
        pnlDatos.BackColor = Color.White
        pnlDatos.Controls.Add(lblID)
        pnlDatos.Controls.Add(txtID)
        pnlDatos.Controls.Add(lblTitular)
        pnlDatos.Controls.Add(cboTitular)
        pnlDatos.Controls.Add(lblPatente)
        pnlDatos.Controls.Add(txtPatente)
        pnlDatos.Controls.Add(lblMarca)
        pnlDatos.Controls.Add(cboMarca)
        pnlDatos.Controls.Add(lblModelo)
        pnlDatos.Controls.Add(cboModelo)
        pnlDatos.Controls.Add(lblAnio)
        pnlDatos.Controls.Add(nudAnio)
        pnlDatos.Controls.Add(lblColor)
        pnlDatos.Controls.Add(txtColor)
        pnlDatos.Controls.Add(lblMotor)
        pnlDatos.Controls.Add(txtMotor)
        pnlDatos.Controls.Add(lblChasis)
        pnlDatos.Controls.Add(txtChasis)
        pnlDatos.Controls.Add(lblKilometraje)
        pnlDatos.Controls.Add(nudKilometraje)
        pnlDatos.Controls.Add(lblObservaciones)
        pnlDatos.Controls.Add(txtObservaciones)
        pnlDatos.Controls.Add(btnGuardar)
        pnlDatos.Controls.Add(btnModificar)
        pnlDatos.Controls.Add(btnEliminar)
        pnlDatos.Controls.Add(btnLimpiar)
        pnlDatos.Location = New Point(40, 120)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(930, 290)
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
        ' lblTitular
        '
        lblTitular.AutoSize = True
        lblTitular.Location = New Point(20, 63)
        lblTitular.Name = "lblTitular"
        lblTitular.Size = New Size(75, 20)
        lblTitular.TabIndex = 2
        lblTitular.Text = "Titular (*)"
        '
        ' cboTitular
        '
        cboTitular.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cboTitular.AutoCompleteSource = AutoCompleteSource.ListItems
        cboTitular.Location = New Point(140, 60)
        cboTitular.Name = "cboTitular"
        cboTitular.Size = New Size(300, 28)
        cboTitular.TabIndex = 3
        '
        ' lblPatente
        '
        lblPatente.AutoSize = True
        lblPatente.Location = New Point(470, 63)
        lblPatente.Name = "lblPatente"
        lblPatente.Size = New Size(79, 20)
        lblPatente.TabIndex = 4
        lblPatente.Text = "Patente (*)"
        '
        ' txtPatente
        '
        txtPatente.CharacterCasing = CharacterCasing.Upper
        txtPatente.Location = New Point(590, 60)
        txtPatente.MaxLength = 10
        txtPatente.Name = "txtPatente"
        txtPatente.Size = New Size(170, 27)
        txtPatente.TabIndex = 5
        '
        ' lblMarca
        '
        lblMarca.AutoSize = True
        lblMarca.Location = New Point(20, 103)
        lblMarca.Name = "lblMarca"
        lblMarca.Size = New Size(72, 20)
        lblMarca.TabIndex = 6
        lblMarca.Text = "Marca (*)"
        '
        ' cboMarca
        '
        cboMarca.DropDownStyle = ComboBoxStyle.DropDownList
        cboMarca.Location = New Point(140, 100)
        cboMarca.Name = "cboMarca"
        cboMarca.Size = New Size(300, 28)
        cboMarca.TabIndex = 7
        '
        ' lblModelo
        '
        lblModelo.AutoSize = True
        lblModelo.Location = New Point(470, 103)
        lblModelo.Name = "lblModelo"
        lblModelo.Size = New Size(80, 20)
        lblModelo.TabIndex = 8
        lblModelo.Text = "Modelo (*)"
        '
        ' cboModelo
        '
        cboModelo.DropDownStyle = ComboBoxStyle.DropDownList
        cboModelo.Location = New Point(590, 100)
        cboModelo.Name = "cboModelo"
        cboModelo.Size = New Size(170, 28)
        cboModelo.TabIndex = 9
        '
        ' lblAnio
        '
        lblAnio.AutoSize = True
        lblAnio.Location = New Point(20, 143)
        lblAnio.Name = "lblAnio"
        lblAnio.Size = New Size(36, 20)
        lblAnio.TabIndex = 10
        lblAnio.Text = "Año"
        '
        ' nudAnio
        '
        nudAnio.Location = New Point(140, 140)
        nudAnio.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        nudAnio.Minimum = New Decimal(New Integer() {1900, 0, 0, 0})
        nudAnio.Name = "nudAnio"
        nudAnio.Size = New Size(120, 27)
        nudAnio.TabIndex = 11
        nudAnio.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        '
        ' lblColor
        '
        lblColor.AutoSize = True
        lblColor.Location = New Point(470, 143)
        lblColor.Name = "lblColor"
        lblColor.Size = New Size(45, 20)
        lblColor.TabIndex = 12
        lblColor.Text = "Color"
        '
        ' txtColor
        '
        txtColor.Location = New Point(590, 140)
        txtColor.MaxLength = 30
        txtColor.Name = "txtColor"
        txtColor.Size = New Size(170, 27)
        txtColor.TabIndex = 13
        '
        ' lblMotor
        '
        lblMotor.AutoSize = True
        lblMotor.Location = New Point(20, 183)
        lblMotor.Name = "lblMotor"
        lblMotor.Size = New Size(83, 20)
        lblMotor.TabIndex = 14
        lblMotor.Text = "N.º motor"
        '
        ' txtMotor
        '
        txtMotor.Location = New Point(140, 180)
        txtMotor.MaxLength = 50
        txtMotor.Name = "txtMotor"
        txtMotor.Size = New Size(300, 27)
        txtMotor.TabIndex = 15
        '
        ' lblChasis
        '
        lblChasis.AutoSize = True
        lblChasis.Location = New Point(470, 183)
        lblChasis.Name = "lblChasis"
        lblChasis.Size = New Size(84, 20)
        lblChasis.TabIndex = 16
        lblChasis.Text = "N.º chasis"
        '
        ' txtChasis
        '
        txtChasis.Location = New Point(590, 180)
        txtChasis.MaxLength = 50
        txtChasis.Name = "txtChasis"
        txtChasis.Size = New Size(170, 27)
        txtChasis.TabIndex = 17
        '
        ' lblKilometraje
        '
        lblKilometraje.AutoSize = True
        lblKilometraje.Location = New Point(20, 223)
        lblKilometraje.Name = "lblKilometraje"
        lblKilometraje.Size = New Size(86, 20)
        lblKilometraje.TabIndex = 18
        lblKilometraje.Text = "Kilometraje"
        '
        ' nudKilometraje
        '
        nudKilometraje.Location = New Point(140, 220)
        nudKilometraje.Maximum = New Decimal(New Integer() {9999999, 0, 0, 0})
        nudKilometraje.Name = "nudKilometraje"
        nudKilometraje.Size = New Size(150, 27)
        nudKilometraje.TabIndex = 19
        nudKilometraje.ThousandsSeparator = True
        '
        ' lblObservaciones
        '
        lblObservaciones.AutoSize = True
        lblObservaciones.Location = New Point(470, 223)
        lblObservaciones.Name = "lblObservaciones"
        lblObservaciones.Size = New Size(105, 20)
        lblObservaciones.TabIndex = 20
        lblObservaciones.Text = "Observaciones"
        '
        ' txtObservaciones
        '
        txtObservaciones.Location = New Point(590, 220)
        txtObservaciones.Multiline = True
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.ScrollBars = ScrollBars.Vertical
        txtObservaciones.Size = New Size(170, 50)
        txtObservaciones.TabIndex = 21
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
        btnGuardar.TabIndex = 22
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
        btnModificar.TabIndex = 23
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
        btnEliminar.TabIndex = 24
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
        btnLimpiar.TabIndex = 25
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        ' txtFiltro
        '
        txtFiltro.Font = New Font("Segoe UI", 11F)
        txtFiltro.Location = New Point(40, 430)
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por patente, titular, marca o modelo..."
        txtFiltro.Size = New Size(420, 32)
        txtFiltro.TabIndex = 3
        '
        ' dgvVehiculos
        '
        dgvVehiculos.AllowUserToAddRows = False
        dgvVehiculos.AllowUserToDeleteRows = False
        dgvVehiculos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvVehiculos.BackgroundColor = Color.White
        dgvVehiculos.BorderStyle = BorderStyle.FixedSingle
        dgvVehiculos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvVehiculos.Location = New Point(40, 480)
        dgvVehiculos.MultiSelect = False
        dgvVehiculos.Name = "dgvVehiculos"
        dgvVehiculos.ReadOnly = True
        dgvVehiculos.RowHeadersVisible = False
        dgvVehiculos.RowHeadersWidth = 51
        dgvVehiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvVehiculos.Size = New Size(930, 220)
        dgvVehiculos.TabIndex = 4
        '
        ' FrmVehiculos
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1010, 730)
        Controls.Add(dgvVehiculos)
        Controls.Add(txtFiltro)
        Controls.Add(pnlDatos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmVehiculos"
        Text = "Vehículos"
        pnlDatos.ResumeLayout(False)
        pnlDatos.PerformLayout()
        CType(nudAnio, ComponentModel.ISupportInitialize).EndInit()
        CType(nudKilometraje, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvVehiculos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblID As Label
    Friend WithEvents txtID As TextBox
    Friend WithEvents lblTitular As Label
    Friend WithEvents cboTitular As ComboBox
    Friend WithEvents lblPatente As Label
    Friend WithEvents txtPatente As TextBox
    Friend WithEvents lblMarca As Label
    Friend WithEvents cboMarca As ComboBox
    Friend WithEvents lblModelo As Label
    Friend WithEvents cboModelo As ComboBox
    Friend WithEvents lblAnio As Label
    Friend WithEvents nudAnio As NumericUpDown
    Friend WithEvents lblColor As Label
    Friend WithEvents txtColor As TextBox
    Friend WithEvents lblMotor As Label
    Friend WithEvents txtMotor As TextBox
    Friend WithEvents lblChasis As Label
    Friend WithEvents txtChasis As TextBox
    Friend WithEvents lblKilometraje As Label
    Friend WithEvents nudKilometraje As NumericUpDown
    Friend WithEvents lblObservaciones As Label
    Friend WithEvents txtObservaciones As TextBox
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents dgvVehiculos As DataGridView
End Class

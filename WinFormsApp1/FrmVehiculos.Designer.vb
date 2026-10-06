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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        lblTitulo = New Label()
        lblSubtitulo = New Label()
        btnNuevo = New Button()
        pnlLista = New Panel()
        picBuscar = New PictureBox()
        txtFiltro = New TextBox()
        chkBajas = New CheckBox()
        dgvVehiculos = New DataGridView()
        pnlFicha = New Panel()
        lblAyuda = New Label()
        pnlRegistro = New Panel()
        lblRegistroTitulo = New Label()
        lblEstadoRegistro = New Label()
        tlpCampos = New TableLayoutPanel()
        lblPatente = New Label()
        txtPatente = New TextBox()
        lblTitular = New Label()
        cboTitular = New ComboBox()
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
        lblContexto = New Label()
        btnGuardar = New Button()
        btnBaja = New Button()
        btnCancelar = New Button()
        pnlLista.SuspendLayout()
        pnlFicha.SuspendLayout()
        pnlRegistro.SuspendLayout()
        tlpCampos.SuspendLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvVehiculos, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudAnio, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudKilometraje, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(520, 60)
        lblTitulo.TabIndex = 3
        lblTitulo.Text = "Vehículos"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 72)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(160, 25)
        lblSubtitulo.TabIndex = 4
        lblSubtitulo.Text = "-"
        '
        ' btnNuevo
        '
        btnNuevo.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnNuevo.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnNuevo.Cursor = Cursors.Hand
        btnNuevo.FlatAppearance.BorderSize = 0
        btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnNuevo.FlatStyle = FlatStyle.Flat
        btnNuevo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnNuevo.ForeColor = Color.White
        btnNuevo.Location = New Point(640, 34)
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Size = New Size(200, 40)
        btnNuevo.TabIndex = 0
        btnNuevo.Text = " Nuevo vehículo"
        btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText
        btnNuevo.UseVisualStyleBackColor = False
        '
        ' pnlLista
        '
        pnlLista.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlLista.BackColor = Color.White
        pnlLista.Controls.Add(picBuscar)
        pnlLista.Controls.Add(txtFiltro)
        pnlLista.Controls.Add(chkBajas)
        pnlLista.Controls.Add(dgvVehiculos)
        pnlLista.Location = New Point(30, 110)
        pnlLista.Name = "pnlLista"
        pnlLista.Size = New Size(420, 510)
        pnlLista.TabIndex = 1
        '
        ' picBuscar
        '
        picBuscar.Location = New Point(16, 18)
        picBuscar.Name = "picBuscar"
        picBuscar.Size = New Size(24, 28)
        picBuscar.SizeMode = PictureBoxSizeMode.Normal
        picBuscar.TabIndex = 3
        picBuscar.TabStop = False
        '
        ' txtFiltro
        '
        txtFiltro.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtFiltro.Location = New Point(46, 16)
        txtFiltro.MaxLength = 150
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por patente, titular, marca o modelo"
        txtFiltro.Size = New Size(358, 27)
        txtFiltro.TabIndex = 0
        '
        ' chkBajas
        '
        chkBajas.AutoSize = True
        chkBajas.Location = New Point(16, 52)
        chkBajas.Name = "chkBajas"
        chkBajas.Size = New Size(190, 24)
        chkBajas.TabIndex = 1
        chkBajas.Text = "Mostrar dados de baja"
        chkBajas.UseVisualStyleBackColor = True
        '
        ' dgvVehiculos
        '
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.White
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.DimGray
        DataGridViewCellStyle1.SelectionBackColor = Color.White
        DataGridViewCellStyle1.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        dgvVehiculos.AllowUserToAddRows = False
        dgvVehiculos.AllowUserToDeleteRows = False
        dgvVehiculos.AllowUserToResizeRows = False
        dgvVehiculos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvVehiculos.BackgroundColor = Color.White
        dgvVehiculos.BorderStyle = BorderStyle.None
        dgvVehiculos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvVehiculos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvVehiculos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvVehiculos.ColumnHeadersHeight = 40
        dgvVehiculos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvVehiculos.EnableHeadersVisualStyles = False
        dgvVehiculos.GridColor = Color.Gainsboro
        dgvVehiculos.Location = New Point(16, 86)
        dgvVehiculos.MultiSelect = False
        dgvVehiculos.Name = "dgvVehiculos"
        dgvVehiculos.ReadOnly = True
        dgvVehiculos.RowHeadersVisible = False
        dgvVehiculos.RowHeadersWidth = 51
        dgvVehiculos.RowTemplate.Height = 26
        dgvVehiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvVehiculos.Size = New Size(388, 408)
        dgvVehiculos.StandardTab = True
        dgvVehiculos.TabIndex = 2
        '
        ' pnlFicha
        '
        pnlFicha.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pnlFicha.BackColor = Color.White
        pnlFicha.Controls.Add(lblAyuda)
        pnlFicha.Controls.Add(pnlRegistro)
        pnlFicha.Location = New Point(460, 110)
        pnlFicha.Name = "pnlFicha"
        pnlFicha.Size = New Size(380, 510)
        pnlFicha.TabIndex = 2
        '
        ' lblAyuda
        '
        lblAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(16, 16)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(348, 48)
        lblAyuda.TabIndex = 1
        lblAyuda.Text = "Seleccione un vehículo de la lista o cree uno nuevo."
        '
        ' pnlRegistro
        '
        pnlRegistro.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlRegistro.Controls.Add(lblRegistroTitulo)
        pnlRegistro.Controls.Add(lblEstadoRegistro)
        pnlRegistro.Controls.Add(tlpCampos)
        pnlRegistro.Controls.Add(btnGuardar)
        pnlRegistro.Controls.Add(btnBaja)
        pnlRegistro.Controls.Add(btnCancelar)
        pnlRegistro.Location = New Point(0, 0)
        pnlRegistro.Name = "pnlRegistro"
        pnlRegistro.Size = New Size(380, 510)
        pnlRegistro.TabIndex = 0
        pnlRegistro.Visible = False
        '
        ' lblRegistroTitulo
        '
        lblRegistroTitulo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblRegistroTitulo.AutoEllipsis = True
        lblRegistroTitulo.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblRegistroTitulo.Location = New Point(16, 12)
        lblRegistroTitulo.Name = "lblRegistroTitulo"
        lblRegistroTitulo.Size = New Size(348, 30)
        lblRegistroTitulo.TabIndex = 3
        lblRegistroTitulo.Text = "Nuevo vehículo"
        lblRegistroTitulo.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblEstadoRegistro
        '
        lblEstadoRegistro.AutoSize = True
        lblEstadoRegistro.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        lblEstadoRegistro.Location = New Point(18, 46)
        lblEstadoRegistro.Name = "lblEstadoRegistro"
        lblEstadoRegistro.Padding = New Padding(8, 3, 8, 3)
        lblEstadoRegistro.Size = New Size(64, 25)
        lblEstadoRegistro.TabIndex = 4
        lblEstadoRegistro.Text = "Activo"
        '
        ' tlpCampos
        '
        tlpCampos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpCampos.ColumnCount = 2
        tlpCampos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpCampos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpCampos.Controls.Add(lblPatente, 0, 0)
        tlpCampos.Controls.Add(txtPatente, 0, 1)
        tlpCampos.Controls.Add(lblTitular, 0, 2)
        tlpCampos.Controls.Add(cboTitular, 0, 3)
        tlpCampos.Controls.Add(lblMarca, 0, 4)
        tlpCampos.Controls.Add(cboMarca, 0, 5)
        tlpCampos.Controls.Add(lblModelo, 1, 4)
        tlpCampos.Controls.Add(cboModelo, 1, 5)
        tlpCampos.Controls.Add(lblAnio, 0, 6)
        tlpCampos.Controls.Add(nudAnio, 0, 7)
        tlpCampos.Controls.Add(lblColor, 1, 6)
        tlpCampos.Controls.Add(txtColor, 1, 7)
        tlpCampos.Controls.Add(lblMotor, 0, 8)
        tlpCampos.Controls.Add(txtMotor, 0, 9)
        tlpCampos.Controls.Add(lblChasis, 1, 8)
        tlpCampos.Controls.Add(txtChasis, 1, 9)
        tlpCampos.Controls.Add(lblKilometraje, 0, 10)
        tlpCampos.Controls.Add(nudKilometraje, 0, 11)
        tlpCampos.Controls.Add(lblObservaciones, 0, 12)
        tlpCampos.Controls.Add(txtObservaciones, 0, 13)
        tlpCampos.Controls.Add(lblContexto, 0, 14)
        tlpCampos.Location = New Point(16, 78)
        tlpCampos.Name = "tlpCampos"
        tlpCampos.RowCount = 15
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.Size = New Size(348, 364)
        tlpCampos.TabIndex = 0
        tlpCampos.SetColumnSpan(lblPatente, 2)
        tlpCampos.SetColumnSpan(txtPatente, 2)
        tlpCampos.SetColumnSpan(lblTitular, 2)
        tlpCampos.SetColumnSpan(cboTitular, 2)
        tlpCampos.SetColumnSpan(lblObservaciones, 2)
        tlpCampos.SetColumnSpan(txtObservaciones, 2)
        tlpCampos.SetColumnSpan(lblContexto, 2)
        '
        ' lblPatente
        '
        lblPatente.AutoSize = True
        lblPatente.Font = New Font("Segoe UI", 8.25F)
        lblPatente.ForeColor = Color.DimGray
        lblPatente.Location = New Point(0, 0)
        lblPatente.Margin = New Padding(0, 8, 0, 2)
        lblPatente.Name = "lblPatente"
        lblPatente.Size = New Size(100, 19)
        lblPatente.TabIndex = 0
        lblPatente.Text = "Patente (*)"
        '
        ' txtPatente
        '
        txtPatente.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtPatente.CharacterCasing = CharacterCasing.Upper
        txtPatente.Location = New Point(0, 0)
        txtPatente.Margin = New Padding(0, 0, 0, 0)
        txtPatente.MaxLength = 10
        txtPatente.Name = "txtPatente"
        txtPatente.Size = New Size(348, 27)
        txtPatente.TabIndex = 1
        '
        ' lblTitular
        '
        lblTitular.AutoSize = True
        lblTitular.Font = New Font("Segoe UI", 8.25F)
        lblTitular.ForeColor = Color.DimGray
        lblTitular.Location = New Point(0, 0)
        lblTitular.Margin = New Padding(0, 8, 0, 2)
        lblTitular.Name = "lblTitular"
        lblTitular.Size = New Size(100, 19)
        lblTitular.TabIndex = 2
        lblTitular.Text = "Titular (*)"
        '
        ' cboTitular
        '
        cboTitular.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cboTitular.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cboTitular.AutoCompleteSource = AutoCompleteSource.ListItems
        cboTitular.FormattingEnabled = True
        cboTitular.Location = New Point(0, 0)
        cboTitular.Margin = New Padding(0, 0, 0, 0)
        cboTitular.Name = "cboTitular"
        cboTitular.Size = New Size(348, 28)
        cboTitular.TabIndex = 3
        '
        ' lblMarca
        '
        lblMarca.AutoSize = True
        lblMarca.Font = New Font("Segoe UI", 8.25F)
        lblMarca.ForeColor = Color.DimGray
        lblMarca.Location = New Point(0, 0)
        lblMarca.Margin = New Padding(0, 8, 6, 2)
        lblMarca.Name = "lblMarca"
        lblMarca.Size = New Size(100, 19)
        lblMarca.TabIndex = 4
        lblMarca.Text = "Marca (*)"
        '
        ' cboMarca
        '
        cboMarca.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cboMarca.DropDownStyle = ComboBoxStyle.DropDownList
        cboMarca.FormattingEnabled = True
        cboMarca.Location = New Point(0, 0)
        cboMarca.Margin = New Padding(0, 0, 6, 0)
        cboMarca.Name = "cboMarca"
        cboMarca.Size = New Size(168, 28)
        cboMarca.TabIndex = 5
        '
        ' lblModelo
        '
        lblModelo.AutoSize = True
        lblModelo.Font = New Font("Segoe UI", 8.25F)
        lblModelo.ForeColor = Color.DimGray
        lblModelo.Location = New Point(0, 0)
        lblModelo.Margin = New Padding(6, 8, 0, 2)
        lblModelo.Name = "lblModelo"
        lblModelo.Size = New Size(100, 19)
        lblModelo.TabIndex = 6
        lblModelo.Text = "Modelo (*)"
        '
        ' cboModelo
        '
        cboModelo.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cboModelo.DropDownStyle = ComboBoxStyle.DropDownList
        cboModelo.FormattingEnabled = True
        cboModelo.Location = New Point(0, 0)
        cboModelo.Margin = New Padding(6, 0, 0, 0)
        cboModelo.Name = "cboModelo"
        cboModelo.Size = New Size(168, 28)
        cboModelo.TabIndex = 7
        '
        ' lblAnio
        '
        lblAnio.AutoSize = True
        lblAnio.Font = New Font("Segoe UI", 8.25F)
        lblAnio.ForeColor = Color.DimGray
        lblAnio.Location = New Point(0, 0)
        lblAnio.Margin = New Padding(0, 8, 6, 2)
        lblAnio.Name = "lblAnio"
        lblAnio.Size = New Size(100, 19)
        lblAnio.TabIndex = 8
        lblAnio.Text = "Año"
        '
        ' nudAnio
        '
        nudAnio.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        nudAnio.Location = New Point(0, 0)
        nudAnio.Margin = New Padding(0, 0, 6, 0)
        nudAnio.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        nudAnio.Minimum = New Decimal(New Integer() {1900, 0, 0, 0})
        nudAnio.Name = "nudAnio"
        nudAnio.Size = New Size(168, 27)
        nudAnio.TabIndex = 9
        nudAnio.Value = New Decimal(New Integer() {2000, 0, 0, 0})
        '
        ' lblColor
        '
        lblColor.AutoSize = True
        lblColor.Font = New Font("Segoe UI", 8.25F)
        lblColor.ForeColor = Color.DimGray
        lblColor.Location = New Point(0, 0)
        lblColor.Margin = New Padding(6, 8, 0, 2)
        lblColor.Name = "lblColor"
        lblColor.Size = New Size(100, 19)
        lblColor.TabIndex = 10
        lblColor.Text = "Color"
        '
        ' txtColor
        '
        txtColor.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtColor.Location = New Point(0, 0)
        txtColor.Margin = New Padding(6, 0, 0, 0)
        txtColor.MaxLength = 30
        txtColor.Name = "txtColor"
        txtColor.Size = New Size(168, 27)
        txtColor.TabIndex = 11
        '
        ' lblMotor
        '
        lblMotor.AutoSize = True
        lblMotor.Font = New Font("Segoe UI", 8.25F)
        lblMotor.ForeColor = Color.DimGray
        lblMotor.Location = New Point(0, 0)
        lblMotor.Margin = New Padding(0, 8, 6, 2)
        lblMotor.Name = "lblMotor"
        lblMotor.Size = New Size(100, 19)
        lblMotor.TabIndex = 12
        lblMotor.Text = "N.º de motor"
        '
        ' txtMotor
        '
        txtMotor.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtMotor.Location = New Point(0, 0)
        txtMotor.Margin = New Padding(0, 0, 6, 0)
        txtMotor.MaxLength = 50
        txtMotor.Name = "txtMotor"
        txtMotor.Size = New Size(168, 27)
        txtMotor.TabIndex = 13
        '
        ' lblChasis
        '
        lblChasis.AutoSize = True
        lblChasis.Font = New Font("Segoe UI", 8.25F)
        lblChasis.ForeColor = Color.DimGray
        lblChasis.Location = New Point(0, 0)
        lblChasis.Margin = New Padding(6, 8, 0, 2)
        lblChasis.Name = "lblChasis"
        lblChasis.Size = New Size(100, 19)
        lblChasis.TabIndex = 14
        lblChasis.Text = "N.º de chasis"
        '
        ' txtChasis
        '
        txtChasis.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtChasis.Location = New Point(0, 0)
        txtChasis.Margin = New Padding(6, 0, 0, 0)
        txtChasis.MaxLength = 50
        txtChasis.Name = "txtChasis"
        txtChasis.Size = New Size(168, 27)
        txtChasis.TabIndex = 15
        '
        ' lblKilometraje
        '
        lblKilometraje.AutoSize = True
        lblKilometraje.Font = New Font("Segoe UI", 8.25F)
        lblKilometraje.ForeColor = Color.DimGray
        lblKilometraje.Location = New Point(0, 0)
        lblKilometraje.Margin = New Padding(0, 8, 6, 2)
        lblKilometraje.Name = "lblKilometraje"
        lblKilometraje.Size = New Size(100, 19)
        lblKilometraje.TabIndex = 16
        lblKilometraje.Text = "Kilometraje actual"
        '
        ' nudKilometraje
        '
        nudKilometraje.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        nudKilometraje.Location = New Point(0, 0)
        nudKilometraje.Margin = New Padding(0, 0, 6, 0)
        nudKilometraje.Maximum = New Decimal(New Integer() {9999999, 0, 0, 0})
        nudKilometraje.Name = "nudKilometraje"
        nudKilometraje.Size = New Size(168, 27)
        nudKilometraje.TabIndex = 17
        nudKilometraje.ThousandsSeparator = True
        '
        ' lblObservaciones
        '
        lblObservaciones.AutoSize = True
        lblObservaciones.Font = New Font("Segoe UI", 8.25F)
        lblObservaciones.ForeColor = Color.DimGray
        lblObservaciones.Location = New Point(0, 0)
        lblObservaciones.Margin = New Padding(0, 8, 0, 2)
        lblObservaciones.Name = "lblObservaciones"
        lblObservaciones.Size = New Size(100, 19)
        lblObservaciones.TabIndex = 18
        lblObservaciones.Text = "Observaciones"
        '
        ' txtObservaciones
        '
        txtObservaciones.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtObservaciones.Location = New Point(0, 0)
        txtObservaciones.Margin = New Padding(0, 0, 0, 0)
        txtObservaciones.MinimumSize = New Size(0, 64)
        txtObservaciones.Multiline = True
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.ScrollBars = ScrollBars.Vertical
        txtObservaciones.Size = New Size(348, 64)
        txtObservaciones.TabIndex = 19
        '
        ' lblContexto
        '
        lblContexto.AutoSize = True
        lblContexto.Font = New Font("Segoe UI", 8.25F)
        lblContexto.ForeColor = Color.DimGray
        lblContexto.Location = New Point(0, 0)
        lblContexto.Margin = New Padding(0, 10, 0, 0)
        lblContexto.Name = "lblContexto"
        lblContexto.Size = New Size(100, 19)
        lblContexto.TabIndex = 20
        lblContexto.Text = "-"
        '
        ' btnGuardar
        '
        btnGuardar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnGuardar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGuardar.Cursor = Cursors.Hand
        btnGuardar.FlatAppearance.BorderSize = 0
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGuardar.FlatStyle = FlatStyle.Flat
        btnGuardar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGuardar.ForeColor = Color.White
        btnGuardar.Location = New Point(174, 454)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(190, 40)
        btnGuardar.TabIndex = 1
        btnGuardar.Text = " Guardar"
        btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnGuardar.UseVisualStyleBackColor = False
        '
        ' btnBaja
        '
        btnBaja.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnBaja.BackColor = Color.White
        btnBaja.Cursor = Cursors.Hand
        btnBaja.FlatAppearance.BorderColor = Color.Silver
        btnBaja.FlatStyle = FlatStyle.Flat
        btnBaja.Font = New Font("Segoe UI", 10F)
        btnBaja.ForeColor = Color.Firebrick
        btnBaja.Location = New Point(16, 454)
        btnBaja.Name = "btnBaja"
        btnBaja.Size = New Size(146, 40)
        btnBaja.TabIndex = 2
        btnBaja.Text = " Dar de baja"
        btnBaja.TextImageRelation = TextImageRelation.ImageBeforeText
        btnBaja.UseVisualStyleBackColor = False
        '
        ' btnCancelar
        '
        btnCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCancelar.BackColor = Color.White
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.FlatAppearance.BorderColor = Color.Silver
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 10F)
        btnCancelar.Location = New Point(16, 454)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(146, 40)
        btnCancelar.TabIndex = 3
        btnCancelar.Text = " Cancelar"
        btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCancelar.UseVisualStyleBackColor = False
        btnCancelar.Visible = False
        '
        ' FrmVehiculos
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(pnlFicha)
        Controls.Add(pnlLista)
        Controls.Add(btnNuevo)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmVehiculos"
        Text = "Vehículos"
        pnlLista.ResumeLayout(False)
        pnlLista.PerformLayout()
        pnlFicha.ResumeLayout(False)
        pnlRegistro.ResumeLayout(False)
        pnlRegistro.PerformLayout()
        tlpCampos.ResumeLayout(False)
        tlpCampos.PerformLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvVehiculos, ComponentModel.ISupportInitialize).EndInit()
        CType(nudAnio, ComponentModel.ISupportInitialize).EndInit()
        CType(nudKilometraje, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents btnNuevo As Button
    Friend WithEvents pnlLista As Panel
    Friend WithEvents picBuscar As PictureBox
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents chkBajas As CheckBox
    Friend WithEvents dgvVehiculos As DataGridView
    Friend WithEvents pnlFicha As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents pnlRegistro As Panel
    Friend WithEvents lblRegistroTitulo As Label
    Friend WithEvents lblEstadoRegistro As Label
    Friend WithEvents tlpCampos As TableLayoutPanel
    Friend WithEvents lblPatente As Label
    Friend WithEvents txtPatente As TextBox
    Friend WithEvents lblTitular As Label
    Friend WithEvents cboTitular As ComboBox
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
    Friend WithEvents lblContexto As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnBaja As Button
    Friend WithEvents btnCancelar As Button
End Class

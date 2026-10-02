<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRecepcion
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
        lblPaso1 = New Label()
        lblPaso2 = New Label()
        lblPaso3 = New Label()
        lblPaso4 = New Label()
        pnlTarjeta = New Panel()
        pnlPaso1 = New Panel()
        lblPatente = New Label()
        txtPatente = New TextBox()
        btnBuscar = New Button()
        lblMarcaModelo = New Label()
        txtMarcaModelo = New TextBox()
        lblAnio = New Label()
        txtAnio = New TextBox()
        lblColor = New Label()
        txtColor = New TextBox()
        lblKmActual = New Label()
        txtKmActual = New TextBox()
        lblTitular = New Label()
        txtTitular = New TextBox()
        lblDocumento = New Label()
        txtDocumento = New TextBox()
        lblAnteriores = New Label()
        dgvAnteriores = New DataGridView()
        pnlPaso2 = New Panel()
        lblKmIngreso = New Label()
        nudKmIngreso = New NumericUpDown()
        lblKmMinimo = New Label()
        lblCombustible = New Label()
        cboCombustible = New ComboBox()
        lblMecanico = New Label()
        cboMecanico = New ComboBox()
        lblSintoma = New Label()
        txtSintoma = New TextBox()
        lblObservaciones = New Label()
        txtObservaciones = New TextBox()
        lblFechaPrometida = New Label()
        dtpFechaPrometida = New DateTimePicker()
        lblAyudaFecha = New Label()
        pnlPaso3 = New Panel()
        lblAyudaFotos = New Label()
        lblFrente = New Label()
        picFrente = New PictureBox()
        btnCargarFrente = New Button()
        btnQuitarFrente = New Button()
        lblTrasera = New Label()
        picTrasera = New PictureBox()
        btnCargarTrasera = New Button()
        btnQuitarTrasera = New Button()
        lblLateralIzq = New Label()
        picLateralIzq = New PictureBox()
        btnCargarLateralIzq = New Button()
        btnQuitarLateralIzq = New Button()
        lblLateralDer = New Label()
        picLateralDer = New PictureBox()
        btnCargarLateralDer = New Button()
        btnQuitarLateralDer = New Button()
        lblTablero = New Label()
        picTablero = New PictureBox()
        btnCargarTablero = New Button()
        btnQuitarTablero = New Button()
        pnlPaso4 = New Panel()
        lblResumen = New Label()
        txtResumen = New TextBox()
        btnCancelar = New Button()
        btnAnterior = New Button()
        btnSiguiente = New Button()
        btnConfirmar = New Button()
        dlgFoto = New OpenFileDialog()
        pnlTarjeta.SuspendLayout()
        pnlPaso1.SuspendLayout()
        CType(dgvAnteriores, ComponentModel.ISupportInitialize).BeginInit()
        pnlPaso2.SuspendLayout()
        CType(nudKmIngreso, ComponentModel.ISupportInitialize).BeginInit()
        pnlPaso3.SuspendLayout()
        CType(picFrente, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTrasera, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralIzq, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralDer, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTablero, ComponentModel.ISupportInitialize).BeginInit()
        pnlPaso4.SuspendLayout()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(442, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Recepción de vehículos"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 62)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(380, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Apertura de la orden de trabajo paso a paso"
        '
        ' lblPaso1
        '
        lblPaso1.AutoSize = True
        lblPaso1.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblPaso1.ForeColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        lblPaso1.Location = New Point(33, 97)
        lblPaso1.Name = "lblPaso1"
        lblPaso1.Size = New Size(98, 23)
        lblPaso1.TabIndex = 2
        lblPaso1.Text = "1. Vehículo"
        '
        ' lblPaso2
        '
        lblPaso2.AutoSize = True
        lblPaso2.Font = New Font("Segoe UI", 10F)
        lblPaso2.ForeColor = Color.Gray
        lblPaso2.Location = New Point(190, 97)
        lblPaso2.Name = "lblPaso2"
        lblPaso2.Size = New Size(160, 23)
        lblPaso2.TabIndex = 3
        lblPaso2.Text = "2. Datos de ingreso"
        '
        ' lblPaso3
        '
        lblPaso3.AutoSize = True
        lblPaso3.Font = New Font("Segoe UI", 10F)
        lblPaso3.ForeColor = Color.Gray
        lblPaso3.Location = New Point(420, 97)
        lblPaso3.Name = "lblPaso3"
        lblPaso3.Size = New Size(70, 23)
        lblPaso3.TabIndex = 4
        lblPaso3.Text = "3. Fotos"
        '
        ' lblPaso4
        '
        lblPaso4.AutoSize = True
        lblPaso4.Font = New Font("Segoe UI", 10F)
        lblPaso4.ForeColor = Color.Gray
        lblPaso4.Location = New Point(560, 97)
        lblPaso4.Name = "lblPaso4"
        lblPaso4.Size = New Size(135, 23)
        lblPaso4.TabIndex = 5
        lblPaso4.Text = "4. Confirmación"
        '
        ' pnlTarjeta
        '
        pnlTarjeta.BackColor = Color.White
        pnlTarjeta.Controls.Add(pnlPaso1)
        pnlTarjeta.Controls.Add(pnlPaso2)
        pnlTarjeta.Controls.Add(pnlPaso3)
        pnlTarjeta.Controls.Add(pnlPaso4)
        pnlTarjeta.Location = New Point(30, 128)
        pnlTarjeta.Name = "pnlTarjeta"
        pnlTarjeta.Size = New Size(810, 432)
        pnlTarjeta.TabIndex = 6
        '
        ' pnlPaso1
        '
        pnlPaso1.Controls.Add(lblPatente)
        pnlPaso1.Controls.Add(txtPatente)
        pnlPaso1.Controls.Add(btnBuscar)
        pnlPaso1.Controls.Add(lblMarcaModelo)
        pnlPaso1.Controls.Add(txtMarcaModelo)
        pnlPaso1.Controls.Add(lblAnio)
        pnlPaso1.Controls.Add(txtAnio)
        pnlPaso1.Controls.Add(lblColor)
        pnlPaso1.Controls.Add(txtColor)
        pnlPaso1.Controls.Add(lblKmActual)
        pnlPaso1.Controls.Add(txtKmActual)
        pnlPaso1.Controls.Add(lblTitular)
        pnlPaso1.Controls.Add(txtTitular)
        pnlPaso1.Controls.Add(lblDocumento)
        pnlPaso1.Controls.Add(txtDocumento)
        pnlPaso1.Controls.Add(lblAnteriores)
        pnlPaso1.Controls.Add(dgvAnteriores)
        pnlPaso1.Location = New Point(0, 0)
        pnlPaso1.Name = "pnlPaso1"
        pnlPaso1.Size = New Size(810, 432)
        pnlPaso1.TabIndex = 0
        '
        ' lblPatente
        '
        lblPatente.AutoSize = True
        lblPatente.Location = New Point(20, 23)
        lblPatente.Name = "lblPatente"
        lblPatente.Size = New Size(78, 20)
        lblPatente.TabIndex = 0
        lblPatente.Text = "Patente (*)"
        '
        ' txtPatente
        '
        txtPatente.CharacterCasing = CharacterCasing.Upper
        txtPatente.Location = New Point(150, 20)
        txtPatente.MaxLength = 10
        txtPatente.Name = "txtPatente"
        txtPatente.Size = New Size(150, 27)
        txtPatente.TabIndex = 1
        '
        ' btnBuscar
        '
        btnBuscar.BackColor = Color.White
        btnBuscar.Cursor = Cursors.Hand
        btnBuscar.FlatAppearance.BorderColor = Color.Silver
        btnBuscar.FlatStyle = FlatStyle.Flat
        btnBuscar.Font = New Font("Segoe UI", 10F)
        btnBuscar.Location = New Point(315, 16)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New Size(110, 34)
        btnBuscar.TabIndex = 2
        btnBuscar.Text = "Buscar"
        btnBuscar.UseVisualStyleBackColor = False
        '
        ' lblMarcaModelo
        '
        lblMarcaModelo.AutoSize = True
        lblMarcaModelo.ForeColor = Color.DimGray
        lblMarcaModelo.Location = New Point(20, 73)
        lblMarcaModelo.Name = "lblMarcaModelo"
        lblMarcaModelo.Size = New Size(112, 20)
        lblMarcaModelo.TabIndex = 3
        lblMarcaModelo.Text = "Marca y modelo"
        '
        ' txtMarcaModelo
        '
        txtMarcaModelo.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtMarcaModelo.Location = New Point(150, 70)
        txtMarcaModelo.Name = "txtMarcaModelo"
        txtMarcaModelo.ReadOnly = True
        txtMarcaModelo.Size = New Size(275, 27)
        txtMarcaModelo.TabIndex = 4
        txtMarcaModelo.TabStop = False
        '
        ' lblAnio
        '
        lblAnio.AutoSize = True
        lblAnio.ForeColor = Color.DimGray
        lblAnio.Location = New Point(460, 73)
        lblAnio.Name = "lblAnio"
        lblAnio.Size = New Size(36, 20)
        lblAnio.TabIndex = 5
        lblAnio.Text = "Año"
        '
        ' txtAnio
        '
        txtAnio.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtAnio.Location = New Point(560, 70)
        txtAnio.Name = "txtAnio"
        txtAnio.ReadOnly = True
        txtAnio.Size = New Size(100, 27)
        txtAnio.TabIndex = 6
        txtAnio.TabStop = False
        '
        ' lblColor
        '
        lblColor.AutoSize = True
        lblColor.ForeColor = Color.DimGray
        lblColor.Location = New Point(20, 111)
        lblColor.Name = "lblColor"
        lblColor.Size = New Size(45, 20)
        lblColor.TabIndex = 7
        lblColor.Text = "Color"
        '
        ' txtColor
        '
        txtColor.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtColor.Location = New Point(150, 108)
        txtColor.Name = "txtColor"
        txtColor.ReadOnly = True
        txtColor.Size = New Size(275, 27)
        txtColor.TabIndex = 8
        txtColor.TabStop = False
        '
        ' lblKmActual
        '
        lblKmActual.AutoSize = True
        lblKmActual.ForeColor = Color.DimGray
        lblKmActual.Location = New Point(460, 111)
        lblKmActual.Name = "lblKmActual"
        lblKmActual.Size = New Size(74, 20)
        lblKmActual.TabIndex = 9
        lblKmActual.Text = "Km actual"
        '
        ' txtKmActual
        '
        txtKmActual.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtKmActual.Location = New Point(560, 108)
        txtKmActual.Name = "txtKmActual"
        txtKmActual.ReadOnly = True
        txtKmActual.Size = New Size(130, 27)
        txtKmActual.TabIndex = 10
        txtKmActual.TabStop = False
        '
        ' lblTitular
        '
        lblTitular.AutoSize = True
        lblTitular.ForeColor = Color.DimGray
        lblTitular.Location = New Point(20, 149)
        lblTitular.Name = "lblTitular"
        lblTitular.Size = New Size(52, 20)
        lblTitular.TabIndex = 11
        lblTitular.Text = "Titular"
        '
        ' txtTitular
        '
        txtTitular.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtTitular.Location = New Point(150, 146)
        txtTitular.Name = "txtTitular"
        txtTitular.ReadOnly = True
        txtTitular.Size = New Size(275, 27)
        txtTitular.TabIndex = 12
        txtTitular.TabStop = False
        '
        ' lblDocumento
        '
        lblDocumento.AutoSize = True
        lblDocumento.ForeColor = Color.DimGray
        lblDocumento.Location = New Point(460, 149)
        lblDocumento.Name = "lblDocumento"
        lblDocumento.Size = New Size(87, 20)
        lblDocumento.TabIndex = 13
        lblDocumento.Text = "Documento"
        '
        ' txtDocumento
        '
        txtDocumento.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtDocumento.Location = New Point(560, 146)
        txtDocumento.Name = "txtDocumento"
        txtDocumento.ReadOnly = True
        txtDocumento.Size = New Size(230, 27)
        txtDocumento.TabIndex = 14
        txtDocumento.TabStop = False
        '
        ' lblAnteriores
        '
        lblAnteriores.AutoSize = True
        lblAnteriores.ForeColor = Color.DimGray
        lblAnteriores.Location = New Point(20, 192)
        lblAnteriores.Name = "lblAnteriores"
        lblAnteriores.Size = New Size(276, 20)
        lblAnteriores.TabIndex = 15
        lblAnteriores.Text = "Órdenes de trabajo anteriores del vehículo"
        '
        ' dgvAnteriores
        '
        dgvAnteriores.AllowUserToAddRows = False
        dgvAnteriores.AllowUserToDeleteRows = False
        dgvAnteriores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAnteriores.BackgroundColor = Color.White
        dgvAnteriores.BorderStyle = BorderStyle.FixedSingle
        dgvAnteriores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAnteriores.Location = New Point(20, 218)
        dgvAnteriores.MultiSelect = False
        dgvAnteriores.Name = "dgvAnteriores"
        dgvAnteriores.ReadOnly = True
        dgvAnteriores.RowHeadersVisible = False
        dgvAnteriores.RowHeadersWidth = 51
        dgvAnteriores.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAnteriores.Size = New Size(770, 194)
        dgvAnteriores.TabIndex = 16
        '
        ' pnlPaso2
        '
        pnlPaso2.Controls.Add(lblKmIngreso)
        pnlPaso2.Controls.Add(nudKmIngreso)
        pnlPaso2.Controls.Add(lblKmMinimo)
        pnlPaso2.Controls.Add(lblCombustible)
        pnlPaso2.Controls.Add(cboCombustible)
        pnlPaso2.Controls.Add(lblMecanico)
        pnlPaso2.Controls.Add(cboMecanico)
        pnlPaso2.Controls.Add(lblSintoma)
        pnlPaso2.Controls.Add(txtSintoma)
        pnlPaso2.Controls.Add(lblObservaciones)
        pnlPaso2.Controls.Add(txtObservaciones)
        pnlPaso2.Controls.Add(lblFechaPrometida)
        pnlPaso2.Controls.Add(dtpFechaPrometida)
        pnlPaso2.Controls.Add(lblAyudaFecha)
        pnlPaso2.Location = New Point(0, 0)
        pnlPaso2.Name = "pnlPaso2"
        pnlPaso2.Size = New Size(810, 432)
        pnlPaso2.TabIndex = 1
        pnlPaso2.Visible = False
        '
        ' lblKmIngreso
        '
        lblKmIngreso.AutoSize = True
        lblKmIngreso.Location = New Point(20, 23)
        lblKmIngreso.Name = "lblKmIngreso"
        lblKmIngreso.Size = New Size(124, 20)
        lblKmIngreso.TabIndex = 0
        lblKmIngreso.Text = "Km de ingreso (*)"
        '
        ' nudKmIngreso
        '
        nudKmIngreso.Location = New Point(230, 20)
        nudKmIngreso.Maximum = New Decimal(New Integer() {9999999, 0, 0, 0})
        nudKmIngreso.Name = "nudKmIngreso"
        nudKmIngreso.Size = New Size(150, 27)
        nudKmIngreso.TabIndex = 1
        nudKmIngreso.ThousandsSeparator = True
        '
        ' lblKmMinimo
        '
        lblKmMinimo.AutoSize = True
        lblKmMinimo.Font = New Font("Segoe UI", 8.5F)
        lblKmMinimo.ForeColor = Color.DimGray
        lblKmMinimo.Location = New Point(395, 24)
        lblKmMinimo.Name = "lblKmMinimo"
        lblKmMinimo.Size = New Size(96, 20)
        lblKmMinimo.TabIndex = 2
        lblKmMinimo.Text = "Mínimo: 0 km"
        '
        ' lblCombustible
        '
        lblCombustible.AutoSize = True
        lblCombustible.Location = New Point(20, 63)
        lblCombustible.Name = "lblCombustible"
        lblCombustible.Size = New Size(152, 20)
        lblCombustible.TabIndex = 3
        lblCombustible.Text = "Nivel de combustible"
        '
        ' cboCombustible
        '
        cboCombustible.DropDownStyle = ComboBoxStyle.DropDownList
        cboCombustible.FormattingEnabled = True
        cboCombustible.Items.AddRange(New Object() {"Sin registrar", "Vacío", "Un cuarto", "Medio", "Tres cuartos", "Lleno"})
        cboCombustible.Location = New Point(230, 60)
        cboCombustible.Name = "cboCombustible"
        cboCombustible.Size = New Size(200, 28)
        cboCombustible.TabIndex = 4
        '
        ' lblMecanico
        '
        lblMecanico.AutoSize = True
        lblMecanico.Location = New Point(460, 63)
        lblMecanico.Name = "lblMecanico"
        lblMecanico.Size = New Size(71, 20)
        lblMecanico.TabIndex = 5
        lblMecanico.Text = "Mecánico"
        '
        ' cboMecanico
        '
        cboMecanico.DropDownStyle = ComboBoxStyle.DropDownList
        cboMecanico.FormattingEnabled = True
        cboMecanico.Location = New Point(545, 60)
        cboMecanico.Name = "cboMecanico"
        cboMecanico.Size = New Size(245, 28)
        cboMecanico.TabIndex = 6
        '
        ' lblSintoma
        '
        lblSintoma.AutoSize = True
        lblSintoma.Location = New Point(20, 103)
        lblSintoma.Name = "lblSintoma"
        lblSintoma.Size = New Size(154, 20)
        lblSintoma.TabIndex = 7
        lblSintoma.Text = "Síntoma reportado (*)"
        '
        ' txtSintoma
        '
        txtSintoma.Location = New Point(230, 100)
        txtSintoma.MaxLength = 2000
        txtSintoma.Multiline = True
        txtSintoma.Name = "txtSintoma"
        txtSintoma.ScrollBars = ScrollBars.Vertical
        txtSintoma.Size = New Size(560, 95)
        txtSintoma.TabIndex = 8
        '
        ' lblObservaciones
        '
        lblObservaciones.AutoSize = True
        lblObservaciones.Location = New Point(20, 213)
        lblObservaciones.Name = "lblObservaciones"
        lblObservaciones.Size = New Size(196, 20)
        lblObservaciones.TabIndex = 9
        lblObservaciones.Text = "Observaciones de recepción"
        '
        ' txtObservaciones
        '
        txtObservaciones.Location = New Point(230, 210)
        txtObservaciones.MaxLength = 2000
        txtObservaciones.Multiline = True
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.PlaceholderText = "Rayones, faltantes, estado general, quién entrega el vehículo..."
        txtObservaciones.ScrollBars = ScrollBars.Vertical
        txtObservaciones.Size = New Size(560, 95)
        txtObservaciones.TabIndex = 10
        '
        ' lblFechaPrometida
        '
        lblFechaPrometida.AutoSize = True
        lblFechaPrometida.Location = New Point(20, 323)
        lblFechaPrometida.Name = "lblFechaPrometida"
        lblFechaPrometida.Size = New Size(196, 20)
        lblFechaPrometida.TabIndex = 11
        lblFechaPrometida.Text = "Fecha prometida de entrega"
        '
        ' dtpFechaPrometida
        '
        dtpFechaPrometida.Checked = False
        dtpFechaPrometida.Format = DateTimePickerFormat.Short
        dtpFechaPrometida.Location = New Point(230, 320)
        dtpFechaPrometida.Name = "dtpFechaPrometida"
        dtpFechaPrometida.ShowCheckBox = True
        dtpFechaPrometida.Size = New Size(170, 27)
        dtpFechaPrometida.TabIndex = 12
        '
        ' lblAyudaFecha
        '
        lblAyudaFecha.AutoSize = True
        lblAyudaFecha.Font = New Font("Segoe UI", 8.5F)
        lblAyudaFecha.ForeColor = Color.DimGray
        lblAyudaFecha.Location = New Point(415, 324)
        lblAyudaFecha.Name = "lblAyudaFecha"
        lblAyudaFecha.Size = New Size(330, 20)
        lblAyudaFecha.TabIndex = 13
        lblAyudaFecha.Text = "Opcional. Marque la casilla para indicar una fecha."
        '
        ' pnlPaso3
        '
        pnlPaso3.Controls.Add(lblAyudaFotos)
        pnlPaso3.Controls.Add(lblFrente)
        pnlPaso3.Controls.Add(picFrente)
        pnlPaso3.Controls.Add(btnCargarFrente)
        pnlPaso3.Controls.Add(btnQuitarFrente)
        pnlPaso3.Controls.Add(lblTrasera)
        pnlPaso3.Controls.Add(picTrasera)
        pnlPaso3.Controls.Add(btnCargarTrasera)
        pnlPaso3.Controls.Add(btnQuitarTrasera)
        pnlPaso3.Controls.Add(lblLateralIzq)
        pnlPaso3.Controls.Add(picLateralIzq)
        pnlPaso3.Controls.Add(btnCargarLateralIzq)
        pnlPaso3.Controls.Add(btnQuitarLateralIzq)
        pnlPaso3.Controls.Add(lblLateralDer)
        pnlPaso3.Controls.Add(picLateralDer)
        pnlPaso3.Controls.Add(btnCargarLateralDer)
        pnlPaso3.Controls.Add(btnQuitarLateralDer)
        pnlPaso3.Controls.Add(lblTablero)
        pnlPaso3.Controls.Add(picTablero)
        pnlPaso3.Controls.Add(btnCargarTablero)
        pnlPaso3.Controls.Add(btnQuitarTablero)
        pnlPaso3.Location = New Point(0, 0)
        pnlPaso3.Name = "pnlPaso3"
        pnlPaso3.Size = New Size(810, 432)
        pnlPaso3.TabIndex = 2
        pnlPaso3.Visible = False
        '
        ' lblAyudaFotos
        '
        lblAyudaFotos.AutoSize = True
        lblAyudaFotos.ForeColor = Color.DimGray
        lblAyudaFotos.Location = New Point(20, 23)
        lblAyudaFotos.Name = "lblAyudaFotos"
        lblAyudaFotos.Size = New Size(560, 20)
        lblAyudaFotos.TabIndex = 0
        lblAyudaFotos.Text = "Cargue una foto por cada ángulo del vehículo (JPG o PNG). Las fotos son opcionales."
        '
        ' lblFrente
        '
        lblFrente.AutoSize = True
        lblFrente.Location = New Point(20, 70)
        lblFrente.Name = "lblFrente"
        lblFrente.Size = New Size(50, 20)
        lblFrente.TabIndex = 1
        lblFrente.Text = "Frente"
        '
        ' picFrente
        '
        picFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picFrente.BorderStyle = BorderStyle.FixedSingle
        picFrente.Location = New Point(20, 96)
        picFrente.Name = "picFrente"
        picFrente.Size = New Size(146, 130)
        picFrente.SizeMode = PictureBoxSizeMode.Zoom
        picFrente.TabIndex = 2
        picFrente.TabStop = False
        '
        ' btnCargarFrente
        '
        btnCargarFrente.BackColor = Color.White
        btnCargarFrente.Cursor = Cursors.Hand
        btnCargarFrente.FlatAppearance.BorderColor = Color.Silver
        btnCargarFrente.FlatStyle = FlatStyle.Flat
        btnCargarFrente.Location = New Point(20, 236)
        btnCargarFrente.Name = "btnCargarFrente"
        btnCargarFrente.Size = New Size(146, 34)
        btnCargarFrente.TabIndex = 3
        btnCargarFrente.Text = "Cargar foto"
        btnCargarFrente.UseVisualStyleBackColor = False
        '
        ' btnQuitarFrente
        '
        btnQuitarFrente.BackColor = Color.White
        btnQuitarFrente.Cursor = Cursors.Hand
        btnQuitarFrente.FlatAppearance.BorderColor = Color.Silver
        btnQuitarFrente.FlatStyle = FlatStyle.Flat
        btnQuitarFrente.ForeColor = Color.Firebrick
        btnQuitarFrente.Location = New Point(20, 276)
        btnQuitarFrente.Name = "btnQuitarFrente"
        btnQuitarFrente.Size = New Size(146, 34)
        btnQuitarFrente.TabIndex = 4
        btnQuitarFrente.Text = "Quitar"
        btnQuitarFrente.UseVisualStyleBackColor = False
        '
        ' lblTrasera
        '
        lblTrasera.AutoSize = True
        lblTrasera.Location = New Point(176, 70)
        lblTrasera.Name = "lblTrasera"
        lblTrasera.Size = New Size(56, 20)
        lblTrasera.TabIndex = 5
        lblTrasera.Text = "Trasera"
        '
        ' picTrasera
        '
        picTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTrasera.BorderStyle = BorderStyle.FixedSingle
        picTrasera.Location = New Point(176, 96)
        picTrasera.Name = "picTrasera"
        picTrasera.Size = New Size(146, 130)
        picTrasera.SizeMode = PictureBoxSizeMode.Zoom
        picTrasera.TabIndex = 6
        picTrasera.TabStop = False
        '
        ' btnCargarTrasera
        '
        btnCargarTrasera.BackColor = Color.White
        btnCargarTrasera.Cursor = Cursors.Hand
        btnCargarTrasera.FlatAppearance.BorderColor = Color.Silver
        btnCargarTrasera.FlatStyle = FlatStyle.Flat
        btnCargarTrasera.Location = New Point(176, 236)
        btnCargarTrasera.Name = "btnCargarTrasera"
        btnCargarTrasera.Size = New Size(146, 34)
        btnCargarTrasera.TabIndex = 7
        btnCargarTrasera.Text = "Cargar foto"
        btnCargarTrasera.UseVisualStyleBackColor = False
        '
        ' btnQuitarTrasera
        '
        btnQuitarTrasera.BackColor = Color.White
        btnQuitarTrasera.Cursor = Cursors.Hand
        btnQuitarTrasera.FlatAppearance.BorderColor = Color.Silver
        btnQuitarTrasera.FlatStyle = FlatStyle.Flat
        btnQuitarTrasera.ForeColor = Color.Firebrick
        btnQuitarTrasera.Location = New Point(176, 276)
        btnQuitarTrasera.Name = "btnQuitarTrasera"
        btnQuitarTrasera.Size = New Size(146, 34)
        btnQuitarTrasera.TabIndex = 8
        btnQuitarTrasera.Text = "Quitar"
        btnQuitarTrasera.UseVisualStyleBackColor = False
        '
        ' lblLateralIzq
        '
        lblLateralIzq.AutoSize = True
        lblLateralIzq.Location = New Point(332, 70)
        lblLateralIzq.Name = "lblLateralIzq"
        lblLateralIzq.Size = New Size(119, 20)
        lblLateralIzq.TabIndex = 9
        lblLateralIzq.Text = "Lateral izquierdo"
        '
        ' picLateralIzq
        '
        picLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralIzq.BorderStyle = BorderStyle.FixedSingle
        picLateralIzq.Location = New Point(332, 96)
        picLateralIzq.Name = "picLateralIzq"
        picLateralIzq.Size = New Size(146, 130)
        picLateralIzq.SizeMode = PictureBoxSizeMode.Zoom
        picLateralIzq.TabIndex = 10
        picLateralIzq.TabStop = False
        '
        ' btnCargarLateralIzq
        '
        btnCargarLateralIzq.BackColor = Color.White
        btnCargarLateralIzq.Cursor = Cursors.Hand
        btnCargarLateralIzq.FlatAppearance.BorderColor = Color.Silver
        btnCargarLateralIzq.FlatStyle = FlatStyle.Flat
        btnCargarLateralIzq.Location = New Point(332, 236)
        btnCargarLateralIzq.Name = "btnCargarLateralIzq"
        btnCargarLateralIzq.Size = New Size(146, 34)
        btnCargarLateralIzq.TabIndex = 11
        btnCargarLateralIzq.Text = "Cargar foto"
        btnCargarLateralIzq.UseVisualStyleBackColor = False
        '
        ' btnQuitarLateralIzq
        '
        btnQuitarLateralIzq.BackColor = Color.White
        btnQuitarLateralIzq.Cursor = Cursors.Hand
        btnQuitarLateralIzq.FlatAppearance.BorderColor = Color.Silver
        btnQuitarLateralIzq.FlatStyle = FlatStyle.Flat
        btnQuitarLateralIzq.ForeColor = Color.Firebrick
        btnQuitarLateralIzq.Location = New Point(332, 276)
        btnQuitarLateralIzq.Name = "btnQuitarLateralIzq"
        btnQuitarLateralIzq.Size = New Size(146, 34)
        btnQuitarLateralIzq.TabIndex = 12
        btnQuitarLateralIzq.Text = "Quitar"
        btnQuitarLateralIzq.UseVisualStyleBackColor = False
        '
        ' lblLateralDer
        '
        lblLateralDer.AutoSize = True
        lblLateralDer.Location = New Point(488, 70)
        lblLateralDer.Name = "lblLateralDer"
        lblLateralDer.Size = New Size(111, 20)
        lblLateralDer.TabIndex = 13
        lblLateralDer.Text = "Lateral derecho"
        '
        ' picLateralDer
        '
        picLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralDer.BorderStyle = BorderStyle.FixedSingle
        picLateralDer.Location = New Point(488, 96)
        picLateralDer.Name = "picLateralDer"
        picLateralDer.Size = New Size(146, 130)
        picLateralDer.SizeMode = PictureBoxSizeMode.Zoom
        picLateralDer.TabIndex = 14
        picLateralDer.TabStop = False
        '
        ' btnCargarLateralDer
        '
        btnCargarLateralDer.BackColor = Color.White
        btnCargarLateralDer.Cursor = Cursors.Hand
        btnCargarLateralDer.FlatAppearance.BorderColor = Color.Silver
        btnCargarLateralDer.FlatStyle = FlatStyle.Flat
        btnCargarLateralDer.Location = New Point(488, 236)
        btnCargarLateralDer.Name = "btnCargarLateralDer"
        btnCargarLateralDer.Size = New Size(146, 34)
        btnCargarLateralDer.TabIndex = 15
        btnCargarLateralDer.Text = "Cargar foto"
        btnCargarLateralDer.UseVisualStyleBackColor = False
        '
        ' btnQuitarLateralDer
        '
        btnQuitarLateralDer.BackColor = Color.White
        btnQuitarLateralDer.Cursor = Cursors.Hand
        btnQuitarLateralDer.FlatAppearance.BorderColor = Color.Silver
        btnQuitarLateralDer.FlatStyle = FlatStyle.Flat
        btnQuitarLateralDer.ForeColor = Color.Firebrick
        btnQuitarLateralDer.Location = New Point(488, 276)
        btnQuitarLateralDer.Name = "btnQuitarLateralDer"
        btnQuitarLateralDer.Size = New Size(146, 34)
        btnQuitarLateralDer.TabIndex = 16
        btnQuitarLateralDer.Text = "Quitar"
        btnQuitarLateralDer.UseVisualStyleBackColor = False
        '
        ' lblTablero
        '
        lblTablero.AutoSize = True
        lblTablero.Location = New Point(644, 70)
        lblTablero.Name = "lblTablero"
        lblTablero.Size = New Size(58, 20)
        lblTablero.TabIndex = 17
        lblTablero.Text = "Tablero"
        '
        ' picTablero
        '
        picTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTablero.BorderStyle = BorderStyle.FixedSingle
        picTablero.Location = New Point(644, 96)
        picTablero.Name = "picTablero"
        picTablero.Size = New Size(146, 130)
        picTablero.SizeMode = PictureBoxSizeMode.Zoom
        picTablero.TabIndex = 18
        picTablero.TabStop = False
        '
        ' btnCargarTablero
        '
        btnCargarTablero.BackColor = Color.White
        btnCargarTablero.Cursor = Cursors.Hand
        btnCargarTablero.FlatAppearance.BorderColor = Color.Silver
        btnCargarTablero.FlatStyle = FlatStyle.Flat
        btnCargarTablero.Location = New Point(644, 236)
        btnCargarTablero.Name = "btnCargarTablero"
        btnCargarTablero.Size = New Size(146, 34)
        btnCargarTablero.TabIndex = 19
        btnCargarTablero.Text = "Cargar foto"
        btnCargarTablero.UseVisualStyleBackColor = False
        '
        ' btnQuitarTablero
        '
        btnQuitarTablero.BackColor = Color.White
        btnQuitarTablero.Cursor = Cursors.Hand
        btnQuitarTablero.FlatAppearance.BorderColor = Color.Silver
        btnQuitarTablero.FlatStyle = FlatStyle.Flat
        btnQuitarTablero.ForeColor = Color.Firebrick
        btnQuitarTablero.Location = New Point(644, 276)
        btnQuitarTablero.Name = "btnQuitarTablero"
        btnQuitarTablero.Size = New Size(146, 34)
        btnQuitarTablero.TabIndex = 20
        btnQuitarTablero.Text = "Quitar"
        btnQuitarTablero.UseVisualStyleBackColor = False
        '
        ' pnlPaso4
        '
        pnlPaso4.Controls.Add(lblResumen)
        pnlPaso4.Controls.Add(txtResumen)
        pnlPaso4.Location = New Point(0, 0)
        pnlPaso4.Name = "pnlPaso4"
        pnlPaso4.Size = New Size(810, 432)
        pnlPaso4.TabIndex = 3
        pnlPaso4.Visible = False
        '
        ' lblResumen
        '
        lblResumen.AutoSize = True
        lblResumen.ForeColor = Color.DimGray
        lblResumen.Location = New Point(20, 23)
        lblResumen.Name = "lblResumen"
        lblResumen.Size = New Size(420, 20)
        lblResumen.TabIndex = 0
        lblResumen.Text = "Revise los datos antes de confirmar la recepción del vehículo."
        '
        ' txtResumen
        '
        txtResumen.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtResumen.BorderStyle = BorderStyle.FixedSingle
        txtResumen.Font = New Font("Segoe UI", 10F)
        txtResumen.Location = New Point(20, 55)
        txtResumen.Multiline = True
        txtResumen.Name = "txtResumen"
        txtResumen.ReadOnly = True
        txtResumen.ScrollBars = ScrollBars.Vertical
        txtResumen.Size = New Size(770, 357)
        txtResumen.TabIndex = 1
        txtResumen.TabStop = False
        '
        ' btnCancelar
        '
        btnCancelar.BackColor = Color.White
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.FlatAppearance.BorderColor = Color.Silver
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 10F)
        btnCancelar.ForeColor = Color.Firebrick
        btnCancelar.Location = New Point(30, 572)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(120, 40)
        btnCancelar.TabIndex = 7
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = False
        '
        ' btnAnterior
        '
        btnAnterior.BackColor = Color.White
        btnAnterior.Cursor = Cursors.Hand
        btnAnterior.FlatAppearance.BorderColor = Color.Silver
        btnAnterior.FlatStyle = FlatStyle.Flat
        btnAnterior.Font = New Font("Segoe UI", 10F)
        btnAnterior.Location = New Point(500, 572)
        btnAnterior.Name = "btnAnterior"
        btnAnterior.Size = New Size(120, 40)
        btnAnterior.TabIndex = 8
        btnAnterior.Text = "Anterior"
        btnAnterior.UseVisualStyleBackColor = False
        btnAnterior.Visible = False
        '
        ' btnSiguiente
        '
        btnSiguiente.BackColor = Color.White
        btnSiguiente.Cursor = Cursors.Hand
        btnSiguiente.FlatAppearance.BorderColor = Color.Silver
        btnSiguiente.FlatStyle = FlatStyle.Flat
        btnSiguiente.Font = New Font("Segoe UI", 10F)
        btnSiguiente.Location = New Point(630, 572)
        btnSiguiente.Name = "btnSiguiente"
        btnSiguiente.Size = New Size(210, 40)
        btnSiguiente.TabIndex = 9
        btnSiguiente.Text = "Siguiente"
        btnSiguiente.UseVisualStyleBackColor = False
        '
        ' btnConfirmar
        '
        btnConfirmar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnConfirmar.Cursor = Cursors.Hand
        btnConfirmar.FlatAppearance.BorderSize = 0
        btnConfirmar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnConfirmar.FlatStyle = FlatStyle.Flat
        btnConfirmar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnConfirmar.ForeColor = Color.White
        btnConfirmar.Location = New Point(630, 572)
        btnConfirmar.Name = "btnConfirmar"
        btnConfirmar.Size = New Size(210, 40)
        btnConfirmar.TabIndex = 10
        btnConfirmar.Text = "Confirmar recepción"
        btnConfirmar.UseVisualStyleBackColor = False
        btnConfirmar.Visible = False
        '
        ' dlgFoto
        '
        dlgFoto.Filter = "Imágenes (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
        dlgFoto.Title = "Seleccionar foto"
        '
        ' FrmRecepcion
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(btnConfirmar)
        Controls.Add(btnSiguiente)
        Controls.Add(btnAnterior)
        Controls.Add(btnCancelar)
        Controls.Add(pnlTarjeta)
        Controls.Add(lblPaso4)
        Controls.Add(lblPaso3)
        Controls.Add(lblPaso2)
        Controls.Add(lblPaso1)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmRecepcion"
        Text = "Recepción de vehículos"
        pnlTarjeta.ResumeLayout(False)
        pnlPaso1.ResumeLayout(False)
        pnlPaso1.PerformLayout()
        CType(dgvAnteriores, ComponentModel.ISupportInitialize).EndInit()
        pnlPaso2.ResumeLayout(False)
        pnlPaso2.PerformLayout()
        CType(nudKmIngreso, ComponentModel.ISupportInitialize).EndInit()
        pnlPaso3.ResumeLayout(False)
        pnlPaso3.PerformLayout()
        CType(picFrente, ComponentModel.ISupportInitialize).EndInit()
        CType(picTrasera, ComponentModel.ISupportInitialize).EndInit()
        CType(picLateralIzq, ComponentModel.ISupportInitialize).EndInit()
        CType(picLateralDer, ComponentModel.ISupportInitialize).EndInit()
        CType(picTablero, ComponentModel.ISupportInitialize).EndInit()
        pnlPaso4.ResumeLayout(False)
        pnlPaso4.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents lblPaso1 As Label
    Friend WithEvents lblPaso2 As Label
    Friend WithEvents lblPaso3 As Label
    Friend WithEvents lblPaso4 As Label
    Friend WithEvents pnlTarjeta As Panel
    Friend WithEvents pnlPaso1 As Panel
    Friend WithEvents lblPatente As Label
    Friend WithEvents txtPatente As TextBox
    Friend WithEvents btnBuscar As Button
    Friend WithEvents lblMarcaModelo As Label
    Friend WithEvents txtMarcaModelo As TextBox
    Friend WithEvents lblAnio As Label
    Friend WithEvents txtAnio As TextBox
    Friend WithEvents lblColor As Label
    Friend WithEvents txtColor As TextBox
    Friend WithEvents lblKmActual As Label
    Friend WithEvents txtKmActual As TextBox
    Friend WithEvents lblTitular As Label
    Friend WithEvents txtTitular As TextBox
    Friend WithEvents lblDocumento As Label
    Friend WithEvents txtDocumento As TextBox
    Friend WithEvents lblAnteriores As Label
    Friend WithEvents dgvAnteriores As DataGridView
    Friend WithEvents pnlPaso2 As Panel
    Friend WithEvents lblKmIngreso As Label
    Friend WithEvents nudKmIngreso As NumericUpDown
    Friend WithEvents lblKmMinimo As Label
    Friend WithEvents lblCombustible As Label
    Friend WithEvents cboCombustible As ComboBox
    Friend WithEvents lblMecanico As Label
    Friend WithEvents cboMecanico As ComboBox
    Friend WithEvents lblSintoma As Label
    Friend WithEvents txtSintoma As TextBox
    Friend WithEvents lblObservaciones As Label
    Friend WithEvents txtObservaciones As TextBox
    Friend WithEvents lblFechaPrometida As Label
    Friend WithEvents dtpFechaPrometida As DateTimePicker
    Friend WithEvents lblAyudaFecha As Label
    Friend WithEvents pnlPaso3 As Panel
    Friend WithEvents lblAyudaFotos As Label
    Friend WithEvents lblFrente As Label
    Friend WithEvents picFrente As PictureBox
    Friend WithEvents btnCargarFrente As Button
    Friend WithEvents btnQuitarFrente As Button
    Friend WithEvents lblTrasera As Label
    Friend WithEvents picTrasera As PictureBox
    Friend WithEvents btnCargarTrasera As Button
    Friend WithEvents btnQuitarTrasera As Button
    Friend WithEvents lblLateralIzq As Label
    Friend WithEvents picLateralIzq As PictureBox
    Friend WithEvents btnCargarLateralIzq As Button
    Friend WithEvents btnQuitarLateralIzq As Button
    Friend WithEvents lblLateralDer As Label
    Friend WithEvents picLateralDer As PictureBox
    Friend WithEvents btnCargarLateralDer As Button
    Friend WithEvents btnQuitarLateralDer As Button
    Friend WithEvents lblTablero As Label
    Friend WithEvents picTablero As PictureBox
    Friend WithEvents btnCargarTablero As Button
    Friend WithEvents btnQuitarTablero As Button
    Friend WithEvents pnlPaso4 As Panel
    Friend WithEvents lblResumen As Label
    Friend WithEvents txtResumen As TextBox
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnAnterior As Button
    Friend WithEvents btnSiguiente As Button
    Friend WithEvents btnConfirmar As Button
    Friend WithEvents dlgFoto As OpenFileDialog
End Class

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
        tlpPasos = New TableLayoutPanel()
        pnlBarra1 = New Panel()
        lblNomPaso1 = New Label()
        lblNumPaso1 = New Label()
        pnlBarra2 = New Panel()
        lblNomPaso2 = New Label()
        lblNumPaso2 = New Label()
        pnlBarra3 = New Panel()
        lblNomPaso3 = New Label()
        lblNumPaso3 = New Label()
        pnlBarra4 = New Panel()
        lblNomPaso4 = New Label()
        lblNumPaso4 = New Label()
        pnlTarjeta = New Panel()
        pnlPaso1 = New Panel()
        lblPatente = New Label()
        txtPatente = New TextBox()
        btnBuscar = New Button()
        lblAyudaPatente = New Label()
        pnlNoRegistrada = New Panel()
        lblNoRegistrada = New Label()
        lblNoRegistradaAyuda = New Label()
        btnRegistrarVehiculo = New Button()
        pnlVehiculo = New Panel()
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
        lblFechaPrometida = New Label()
        dtpFechaPrometida = New DateTimePicker()
        lblAyudaFecha = New Label()
        lblSintoma = New Label()
        txtSintoma = New TextBox()
        lblObservaciones = New Label()
        txtObservaciones = New TextBox()
        pnlPaso3 = New Panel()
        lblAyudaFotos = New Label()
        tlpFotos = New TableLayoutPanel()
        pnlFotoFrente = New Panel()
        picFrente = New PictureBox()
        lblFrente = New Label()
        btnCargarFrente = New Button()
        btnQuitarFrente = New Button()
        pnlFotoTrasera = New Panel()
        picTrasera = New PictureBox()
        lblTrasera = New Label()
        btnCargarTrasera = New Button()
        btnQuitarTrasera = New Button()
        pnlFotoLateralIzq = New Panel()
        picLateralIzq = New PictureBox()
        lblLateralIzq = New Label()
        btnCargarLateralIzq = New Button()
        btnQuitarLateralIzq = New Button()
        pnlFotoLateralDer = New Panel()
        picLateralDer = New PictureBox()
        lblLateralDer = New Label()
        btnCargarLateralDer = New Button()
        btnQuitarLateralDer = New Button()
        pnlFotoTablero = New Panel()
        picTablero = New PictureBox()
        lblTablero = New Label()
        btnCargarTablero = New Button()
        btnQuitarTablero = New Button()
        pnlPaso4 = New Panel()
        lblResumen = New Label()
        txtResumen = New TextBox()
        lblVistas = New Label()
        tlpVistas = New TableLayoutPanel()
        picVistaFrente = New PictureBox()
        picVistaTrasera = New PictureBox()
        picVistaLateralIzq = New PictureBox()
        picVistaLateralDer = New PictureBox()
        picVistaTablero = New PictureBox()
        pnlResumen = New Panel()
        lblResTitulo = New Label()
        picResVehiculo = New PictureBox()
        lblResVehiculoTit = New Label()
        lblResVehiculo = New Label()
        picResCliente = New PictureBox()
        lblResClienteTit = New Label()
        lblResCliente = New Label()
        picResKm = New PictureBox()
        lblResKmTit = New Label()
        lblResKm = New Label()
        picResCombustible = New PictureBox()
        lblResCombustibleTit = New Label()
        lblResCombustible = New Label()
        picResMecanico = New PictureBox()
        lblResMecanicoTit = New Label()
        lblResMecanico = New Label()
        picResFecha = New PictureBox()
        lblResFechaTit = New Label()
        lblResFecha = New Label()
        picResFotos = New PictureBox()
        lblResFotosTit = New Label()
        lblResFotos = New Label()
        picResSintoma = New PictureBox()
        lblResSintomaTit = New Label()
        lblResSintoma = New Label()
        btnCancelar = New Button()
        btnAnterior = New Button()
        btnSiguiente = New Button()
        btnConfirmar = New Button()
        dlgFoto = New OpenFileDialog()
        tlpPasos.SuspendLayout()
        pnlBarra1.SuspendLayout()
        pnlBarra2.SuspendLayout()
        pnlBarra3.SuspendLayout()
        pnlBarra4.SuspendLayout()
        pnlTarjeta.SuspendLayout()
        pnlPaso1.SuspendLayout()
        pnlNoRegistrada.SuspendLayout()
        pnlVehiculo.SuspendLayout()
        pnlPaso2.SuspendLayout()
        pnlPaso3.SuspendLayout()
        tlpFotos.SuspendLayout()
        pnlFotoFrente.SuspendLayout()
        pnlFotoTrasera.SuspendLayout()
        pnlFotoLateralIzq.SuspendLayout()
        pnlFotoLateralDer.SuspendLayout()
        pnlFotoTablero.SuspendLayout()
        pnlPaso4.SuspendLayout()
        tlpVistas.SuspendLayout()
        pnlResumen.SuspendLayout()
        CType(dgvAnteriores, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudKmIngreso, ComponentModel.ISupportInitialize).BeginInit()
        CType(picResVehiculo, ComponentModel.ISupportInitialize).BeginInit()
        CType(picResCliente, ComponentModel.ISupportInitialize).BeginInit()
        CType(picResKm, ComponentModel.ISupportInitialize).BeginInit()
        CType(picResCombustible, ComponentModel.ISupportInitialize).BeginInit()
        CType(picResMecanico, ComponentModel.ISupportInitialize).BeginInit()
        CType(picResFecha, ComponentModel.ISupportInitialize).BeginInit()
        CType(picResFotos, ComponentModel.ISupportInitialize).BeginInit()
        CType(picResSintoma, ComponentModel.ISupportInitialize).BeginInit()
        CType(picFrente, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTrasera, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralIzq, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralDer, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTablero, ComponentModel.ISupportInitialize).BeginInit()
        CType(picVistaFrente, ComponentModel.ISupportInitialize).BeginInit()
        CType(picVistaTrasera, ComponentModel.ISupportInitialize).BeginInit()
        CType(picVistaLateralIzq, ComponentModel.ISupportInitialize).BeginInit()
        CType(picVistaLateralDer, ComponentModel.ISupportInitialize).BeginInit()
        CType(picVistaTablero, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(430, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Recepción de vehículo"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 62)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(110, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Paso 1 de 4"
        '
        ' tlpPasos
        '
        tlpPasos.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tlpPasos.ColumnCount = 4
        tlpPasos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpPasos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpPasos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpPasos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        tlpPasos.Controls.Add(pnlBarra1, 0, 0)
        tlpPasos.Controls.Add(pnlBarra2, 1, 0)
        tlpPasos.Controls.Add(pnlBarra3, 2, 0)
        tlpPasos.Controls.Add(pnlBarra4, 3, 0)
        tlpPasos.Location = New Point(30, 94)
        tlpPasos.Name = "tlpPasos"
        tlpPasos.RowCount = 1
        tlpPasos.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpPasos.Size = New Size(810, 50)
        tlpPasos.TabIndex = 2
        '
        ' pnlBarra1
        '
        pnlBarra1.BackColor = Color.White
        pnlBarra1.Controls.Add(lblNomPaso1)
        pnlBarra1.Controls.Add(lblNumPaso1)
        pnlBarra1.Dock = DockStyle.Fill
        pnlBarra1.Location = New Point(0, 0)
        pnlBarra1.Margin = New Padding(0, 0, 6, 0)
        pnlBarra1.Name = "pnlBarra1"
        pnlBarra1.Size = New Size(196, 50)
        pnlBarra1.TabIndex = 0
        '
        ' lblNomPaso1
        '
        lblNomPaso1.AutoEllipsis = True
        lblNomPaso1.Dock = DockStyle.Fill
        lblNomPaso1.Font = New Font("Segoe UI", 10F)
        lblNomPaso1.ForeColor = Color.Gray
        lblNomPaso1.Location = New Point(44, 0)
        lblNomPaso1.Name = "lblNomPaso1"
        lblNomPaso1.Size = New Size(152, 50)
        lblNomPaso1.TabIndex = 1
        lblNomPaso1.Text = "Vehículo"
        lblNomPaso1.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblNumPaso1
        '
        lblNumPaso1.Dock = DockStyle.Left
        lblNumPaso1.Font = New Font("Segoe UI Symbol", 12F, FontStyle.Bold)
        lblNumPaso1.ForeColor = Color.Gray
        lblNumPaso1.Location = New Point(0, 0)
        lblNumPaso1.Name = "lblNumPaso1"
        lblNumPaso1.Size = New Size(44, 50)
        lblNumPaso1.TabIndex = 0
        lblNumPaso1.Text = "1"
        lblNumPaso1.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlBarra2
        '
        pnlBarra2.BackColor = Color.White
        pnlBarra2.Controls.Add(lblNomPaso2)
        pnlBarra2.Controls.Add(lblNumPaso2)
        pnlBarra2.Dock = DockStyle.Fill
        pnlBarra2.Location = New Point(202, 0)
        pnlBarra2.Margin = New Padding(0, 0, 6, 0)
        pnlBarra2.Name = "pnlBarra2"
        pnlBarra2.Size = New Size(196, 50)
        pnlBarra2.TabIndex = 1
        '
        ' lblNomPaso2
        '
        lblNomPaso2.AutoEllipsis = True
        lblNomPaso2.Dock = DockStyle.Fill
        lblNomPaso2.Font = New Font("Segoe UI", 10F)
        lblNomPaso2.ForeColor = Color.Gray
        lblNomPaso2.Location = New Point(44, 0)
        lblNomPaso2.Name = "lblNomPaso2"
        lblNomPaso2.Size = New Size(152, 50)
        lblNomPaso2.TabIndex = 1
        lblNomPaso2.Text = "Datos de ingreso"
        lblNomPaso2.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblNumPaso2
        '
        lblNumPaso2.Dock = DockStyle.Left
        lblNumPaso2.Font = New Font("Segoe UI Symbol", 12F, FontStyle.Bold)
        lblNumPaso2.ForeColor = Color.Gray
        lblNumPaso2.Location = New Point(0, 0)
        lblNumPaso2.Name = "lblNumPaso2"
        lblNumPaso2.Size = New Size(44, 50)
        lblNumPaso2.TabIndex = 0
        lblNumPaso2.Text = "2"
        lblNumPaso2.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlBarra3
        '
        pnlBarra3.BackColor = Color.White
        pnlBarra3.Controls.Add(lblNomPaso3)
        pnlBarra3.Controls.Add(lblNumPaso3)
        pnlBarra3.Dock = DockStyle.Fill
        pnlBarra3.Location = New Point(404, 0)
        pnlBarra3.Margin = New Padding(0, 0, 6, 0)
        pnlBarra3.Name = "pnlBarra3"
        pnlBarra3.Size = New Size(196, 50)
        pnlBarra3.TabIndex = 2
        '
        ' lblNomPaso3
        '
        lblNomPaso3.AutoEllipsis = True
        lblNomPaso3.Dock = DockStyle.Fill
        lblNomPaso3.Font = New Font("Segoe UI", 10F)
        lblNomPaso3.ForeColor = Color.Gray
        lblNomPaso3.Location = New Point(44, 0)
        lblNomPaso3.Name = "lblNomPaso3"
        lblNomPaso3.Size = New Size(152, 50)
        lblNomPaso3.TabIndex = 1
        lblNomPaso3.Text = "Fotos"
        lblNomPaso3.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblNumPaso3
        '
        lblNumPaso3.Dock = DockStyle.Left
        lblNumPaso3.Font = New Font("Segoe UI Symbol", 12F, FontStyle.Bold)
        lblNumPaso3.ForeColor = Color.Gray
        lblNumPaso3.Location = New Point(0, 0)
        lblNumPaso3.Name = "lblNumPaso3"
        lblNumPaso3.Size = New Size(44, 50)
        lblNumPaso3.TabIndex = 0
        lblNumPaso3.Text = "3"
        lblNumPaso3.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlBarra4
        '
        pnlBarra4.BackColor = Color.White
        pnlBarra4.Controls.Add(lblNomPaso4)
        pnlBarra4.Controls.Add(lblNumPaso4)
        pnlBarra4.Dock = DockStyle.Fill
        pnlBarra4.Location = New Point(606, 0)
        pnlBarra4.Margin = New Padding(0, 0, 0, 0)
        pnlBarra4.Name = "pnlBarra4"
        pnlBarra4.Size = New Size(196, 50)
        pnlBarra4.TabIndex = 3
        '
        ' lblNomPaso4
        '
        lblNomPaso4.AutoEllipsis = True
        lblNomPaso4.Dock = DockStyle.Fill
        lblNomPaso4.Font = New Font("Segoe UI", 10F)
        lblNomPaso4.ForeColor = Color.Gray
        lblNomPaso4.Location = New Point(44, 0)
        lblNomPaso4.Name = "lblNomPaso4"
        lblNomPaso4.Size = New Size(152, 50)
        lblNomPaso4.TabIndex = 1
        lblNomPaso4.Text = "Confirmación"
        lblNomPaso4.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblNumPaso4
        '
        lblNumPaso4.Dock = DockStyle.Left
        lblNumPaso4.Font = New Font("Segoe UI Symbol", 12F, FontStyle.Bold)
        lblNumPaso4.ForeColor = Color.Gray
        lblNumPaso4.Location = New Point(0, 0)
        lblNumPaso4.Name = "lblNumPaso4"
        lblNumPaso4.Size = New Size(44, 50)
        lblNumPaso4.TabIndex = 0
        lblNumPaso4.Text = "4"
        lblNumPaso4.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlTarjeta
        '
        pnlTarjeta.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlTarjeta.BackColor = Color.White
        pnlTarjeta.Controls.Add(pnlPaso1)
        pnlTarjeta.Controls.Add(pnlPaso2)
        pnlTarjeta.Controls.Add(pnlPaso3)
        pnlTarjeta.Controls.Add(pnlPaso4)
        pnlTarjeta.Location = New Point(30, 152)
        pnlTarjeta.Name = "pnlTarjeta"
        pnlTarjeta.Size = New Size(440, 420)
        pnlTarjeta.TabIndex = 3
        '
        ' pnlPaso1
        '
        pnlPaso1.Controls.Add(lblPatente)
        pnlPaso1.Controls.Add(txtPatente)
        pnlPaso1.Controls.Add(btnBuscar)
        pnlPaso1.Controls.Add(lblAyudaPatente)
        pnlPaso1.Controls.Add(pnlNoRegistrada)
        pnlPaso1.Controls.Add(pnlVehiculo)
        pnlPaso1.Controls.Add(lblAnteriores)
        pnlPaso1.Controls.Add(dgvAnteriores)
        pnlPaso1.Dock = DockStyle.Fill
        pnlPaso1.Location = New Point(0, 0)
        pnlPaso1.Name = "pnlPaso1"
        pnlPaso1.Size = New Size(440, 420)
        pnlPaso1.TabIndex = 0
        '
        ' lblPatente
        '
        lblPatente.AutoSize = True
        lblPatente.Location = New Point(20, 14)
        lblPatente.Name = "lblPatente"
        lblPatente.Size = New Size(100, 20)
        lblPatente.TabIndex = 0
        lblPatente.Text = "Patente del vehículo"
        '
        ' txtPatente
        '
        txtPatente.CharacterCasing = CharacterCasing.Upper
        txtPatente.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        txtPatente.Location = New Point(20, 40)
        txtPatente.MaxLength = 10
        txtPatente.Name = "txtPatente"
        txtPatente.Size = New Size(200, 43)
        txtPatente.TabIndex = 1
        '
        ' btnBuscar
        '
        btnBuscar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnBuscar.Cursor = Cursors.Hand
        btnBuscar.FlatAppearance.BorderSize = 0
        btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnBuscar.FlatStyle = FlatStyle.Flat
        btnBuscar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnBuscar.ForeColor = Color.White
        btnBuscar.Name = "btnBuscar"
        btnBuscar.TabIndex = 2
        btnBuscar.Text = " Buscar"
        btnBuscar.UseVisualStyleBackColor = False
        btnBuscar.Location = New Point(230, 40)
        btnBuscar.Size = New Size(120, 43)
        btnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText
        '
        ' lblAyudaPatente
        '
        lblAyudaPatente.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyudaPatente.ForeColor = Color.DimGray
        lblAyudaPatente.Location = New Point(20, 98)
        lblAyudaPatente.Name = "lblAyudaPatente"
        lblAyudaPatente.Size = New Size(400, 44)
        lblAyudaPatente.TabIndex = 3
        lblAyudaPatente.Text = "Escriba la patente y presione Buscar. El vehículo tiene que estar registrado en Vehículos."
        '
        ' pnlNoRegistrada
        '
        pnlNoRegistrada.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        pnlNoRegistrada.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        pnlNoRegistrada.Controls.Add(lblNoRegistrada)
        pnlNoRegistrada.Controls.Add(lblNoRegistradaAyuda)
        pnlNoRegistrada.Controls.Add(btnRegistrarVehiculo)
        pnlNoRegistrada.Location = New Point(20, 96)
        pnlNoRegistrada.Name = "pnlNoRegistrada"
        pnlNoRegistrada.Size = New Size(400, 132)
        pnlNoRegistrada.TabIndex = 20
        pnlNoRegistrada.Visible = False
        '
        ' lblNoRegistrada
        '
        lblNoRegistrada.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblNoRegistrada.AutoEllipsis = True
        lblNoRegistrada.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblNoRegistrada.Location = New Point(12, 10)
        lblNoRegistrada.Name = "lblNoRegistrada"
        lblNoRegistrada.Size = New Size(376, 26)
        lblNoRegistrada.TabIndex = 0
        lblNoRegistrada.Text = "La patente no está registrada."
        '
        ' lblNoRegistradaAyuda
        '
        lblNoRegistradaAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblNoRegistradaAyuda.AutoEllipsis = True
        lblNoRegistradaAyuda.ForeColor = Color.DimGray
        lblNoRegistradaAyuda.Location = New Point(12, 40)
        lblNoRegistradaAyuda.Name = "lblNoRegistradaAyuda"
        lblNoRegistradaAyuda.Size = New Size(376, 26)
        lblNoRegistradaAyuda.TabIndex = 1
        lblNoRegistradaAyuda.Text = "Puede registrar el vehículo y su titular ahora, sin salir de la recepción."
        '
        ' btnRegistrarVehiculo
        '
        btnRegistrarVehiculo.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnRegistrarVehiculo.Cursor = Cursors.Hand
        btnRegistrarVehiculo.FlatAppearance.BorderSize = 0
        btnRegistrarVehiculo.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnRegistrarVehiculo.FlatStyle = FlatStyle.Flat
        btnRegistrarVehiculo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnRegistrarVehiculo.ForeColor = Color.White
        btnRegistrarVehiculo.Location = New Point(12, 76)
        btnRegistrarVehiculo.Name = "btnRegistrarVehiculo"
        btnRegistrarVehiculo.Size = New Size(230, 44)
        btnRegistrarVehiculo.TabIndex = 2
        btnRegistrarVehiculo.Text = " Registrar vehículo"
        btnRegistrarVehiculo.TextImageRelation = TextImageRelation.ImageBeforeText
        btnRegistrarVehiculo.UseVisualStyleBackColor = False
        '
        ' pnlVehiculo
        '
        pnlVehiculo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        pnlVehiculo.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        pnlVehiculo.Controls.Add(lblMarcaModelo)
        pnlVehiculo.Controls.Add(txtMarcaModelo)
        pnlVehiculo.Controls.Add(lblAnio)
        pnlVehiculo.Controls.Add(txtAnio)
        pnlVehiculo.Controls.Add(lblColor)
        pnlVehiculo.Controls.Add(txtColor)
        pnlVehiculo.Controls.Add(lblKmActual)
        pnlVehiculo.Controls.Add(txtKmActual)
        pnlVehiculo.Controls.Add(lblTitular)
        pnlVehiculo.Controls.Add(txtTitular)
        pnlVehiculo.Controls.Add(lblDocumento)
        pnlVehiculo.Controls.Add(txtDocumento)
        pnlVehiculo.Location = New Point(20, 96)
        pnlVehiculo.Name = "pnlVehiculo"
        pnlVehiculo.Size = New Size(400, 182)
        pnlVehiculo.TabIndex = 4
        pnlVehiculo.Visible = False
        '
        ' lblMarcaModelo
        '
        lblMarcaModelo.AutoSize = True
        lblMarcaModelo.Location = New Point(12, 13)
        lblMarcaModelo.Name = "lblMarcaModelo"
        lblMarcaModelo.Size = New Size(100, 20)
        lblMarcaModelo.TabIndex = 0
        lblMarcaModelo.Text = "Vehículo"
        lblMarcaModelo.ForeColor = Color.DimGray
        '
        ' txtMarcaModelo
        '
        txtMarcaModelo.BackColor = Color.White
        txtMarcaModelo.Name = "txtMarcaModelo"
        txtMarcaModelo.ReadOnly = True
        txtMarcaModelo.TabIndex = 1
        txtMarcaModelo.TabStop = False
        txtMarcaModelo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMarcaModelo.Location = New Point(100, 10)
        txtMarcaModelo.Size = New Size(288, 27)
        '
        ' lblAnio
        '
        lblAnio.AutoSize = True
        lblAnio.Location = New Point(12, 47)
        lblAnio.Name = "lblAnio"
        lblAnio.Size = New Size(100, 20)
        lblAnio.TabIndex = 2
        lblAnio.Text = "Año"
        lblAnio.ForeColor = Color.DimGray
        '
        ' txtAnio
        '
        txtAnio.BackColor = Color.White
        txtAnio.Name = "txtAnio"
        txtAnio.ReadOnly = True
        txtAnio.TabIndex = 3
        txtAnio.TabStop = False
        txtAnio.Location = New Point(100, 44)
        txtAnio.Size = New Size(70, 27)
        '
        ' lblColor
        '
        lblColor.AutoSize = True
        lblColor.Location = New Point(184, 47)
        lblColor.Name = "lblColor"
        lblColor.Size = New Size(100, 20)
        lblColor.TabIndex = 4
        lblColor.Text = "Color"
        lblColor.ForeColor = Color.DimGray
        '
        ' txtColor
        '
        txtColor.BackColor = Color.White
        txtColor.Name = "txtColor"
        txtColor.ReadOnly = True
        txtColor.TabIndex = 5
        txtColor.TabStop = False
        txtColor.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtColor.Location = New Point(234, 44)
        txtColor.Size = New Size(154, 27)
        '
        ' lblKmActual
        '
        lblKmActual.AutoSize = True
        lblKmActual.Location = New Point(12, 81)
        lblKmActual.Name = "lblKmActual"
        lblKmActual.Size = New Size(100, 20)
        lblKmActual.TabIndex = 6
        lblKmActual.Text = "Km actual"
        lblKmActual.ForeColor = Color.DimGray
        '
        ' txtKmActual
        '
        txtKmActual.BackColor = Color.White
        txtKmActual.Name = "txtKmActual"
        txtKmActual.ReadOnly = True
        txtKmActual.TabIndex = 7
        txtKmActual.TabStop = False
        txtKmActual.Location = New Point(100, 78)
        txtKmActual.Size = New Size(120, 27)
        '
        ' lblTitular
        '
        lblTitular.AutoSize = True
        lblTitular.Location = New Point(12, 115)
        lblTitular.Name = "lblTitular"
        lblTitular.Size = New Size(100, 20)
        lblTitular.TabIndex = 8
        lblTitular.Text = "Titular"
        lblTitular.ForeColor = Color.DimGray
        '
        ' txtTitular
        '
        txtTitular.BackColor = Color.White
        txtTitular.Name = "txtTitular"
        txtTitular.ReadOnly = True
        txtTitular.TabIndex = 9
        txtTitular.TabStop = False
        txtTitular.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtTitular.Location = New Point(100, 112)
        txtTitular.Size = New Size(288, 27)
        '
        ' lblDocumento
        '
        lblDocumento.AutoSize = True
        lblDocumento.Location = New Point(12, 149)
        lblDocumento.Name = "lblDocumento"
        lblDocumento.Size = New Size(100, 20)
        lblDocumento.TabIndex = 10
        lblDocumento.Text = "Documento"
        lblDocumento.ForeColor = Color.DimGray
        '
        ' txtDocumento
        '
        txtDocumento.BackColor = Color.White
        txtDocumento.Name = "txtDocumento"
        txtDocumento.ReadOnly = True
        txtDocumento.TabIndex = 11
        txtDocumento.TabStop = False
        txtDocumento.Location = New Point(100, 146)
        txtDocumento.Size = New Size(170, 27)
        '
        ' lblAnteriores
        '
        lblAnteriores.AutoSize = True
        lblAnteriores.Location = New Point(20, 286)
        lblAnteriores.Name = "lblAnteriores"
        lblAnteriores.Size = New Size(100, 20)
        lblAnteriores.TabIndex = 5
        lblAnteriores.Text = "Órdenes anteriores del vehículo"
        lblAnteriores.ForeColor = Color.DimGray
        lblAnteriores.Visible = False
        '
        ' dgvAnteriores
        '
        dgvAnteriores.AllowUserToAddRows = False
        dgvAnteriores.AllowUserToDeleteRows = False
        dgvAnteriores.AllowUserToResizeRows = False
        dgvAnteriores.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAnteriores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAnteriores.BackgroundColor = Color.White
        dgvAnteriores.BorderStyle = BorderStyle.FixedSingle
        dgvAnteriores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAnteriores.Location = New Point(20, 310)
        dgvAnteriores.MultiSelect = False
        dgvAnteriores.Name = "dgvAnteriores"
        dgvAnteriores.ReadOnly = True
        dgvAnteriores.RowHeadersVisible = False
        dgvAnteriores.RowHeadersWidth = 51
        dgvAnteriores.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAnteriores.Size = New Size(400, 98)
        dgvAnteriores.TabIndex = 6
        dgvAnteriores.Visible = False
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
        pnlPaso2.Controls.Add(lblFechaPrometida)
        pnlPaso2.Controls.Add(dtpFechaPrometida)
        pnlPaso2.Controls.Add(lblAyudaFecha)
        pnlPaso2.Controls.Add(lblSintoma)
        pnlPaso2.Controls.Add(txtSintoma)
        pnlPaso2.Controls.Add(lblObservaciones)
        pnlPaso2.Controls.Add(txtObservaciones)
        pnlPaso2.Dock = DockStyle.Fill
        pnlPaso2.Location = New Point(0, 0)
        pnlPaso2.Name = "pnlPaso2"
        pnlPaso2.Size = New Size(440, 420)
        pnlPaso2.TabIndex = 1
        pnlPaso2.Visible = False
        '
        ' lblKmIngreso
        '
        lblKmIngreso.AutoSize = True
        lblKmIngreso.Location = New Point(20, 12)
        lblKmIngreso.Name = "lblKmIngreso"
        lblKmIngreso.Size = New Size(100, 20)
        lblKmIngreso.TabIndex = 0
        lblKmIngreso.Text = "Km de ingreso (*)"
        '
        ' nudKmIngreso
        '
        nudKmIngreso.Location = New Point(20, 38)
        nudKmIngreso.Maximum = New Decimal(New Integer() {9999999, 0, 0, 0})
        nudKmIngreso.Name = "nudKmIngreso"
        nudKmIngreso.Size = New Size(180, 27)
        nudKmIngreso.TabIndex = 1
        nudKmIngreso.ThousandsSeparator = True
        '
        ' lblKmMinimo
        '
        lblKmMinimo.AutoSize = True
        lblKmMinimo.Location = New Point(20, 70)
        lblKmMinimo.Name = "lblKmMinimo"
        lblKmMinimo.Size = New Size(100, 20)
        lblKmMinimo.TabIndex = 2
        lblKmMinimo.Text = "Mínimo: 0 km"
        lblKmMinimo.Font = New Font("Segoe UI", 8.5F)
        lblKmMinimo.ForeColor = Color.DimGray
        '
        ' lblCombustible
        '
        lblCombustible.AutoSize = True
        lblCombustible.Location = New Point(230, 12)
        lblCombustible.Name = "lblCombustible"
        lblCombustible.Size = New Size(100, 20)
        lblCombustible.TabIndex = 3
        lblCombustible.Text = "Nivel de combustible"
        '
        ' cboCombustible
        '
        cboCombustible.DropDownStyle = ComboBoxStyle.DropDownList
        cboCombustible.FormattingEnabled = True
        cboCombustible.Items.AddRange(New Object() {"Sin registrar", "Vacío", "Un cuarto", "Medio", "Tres cuartos", "Lleno"})
        cboCombustible.Location = New Point(230, 38)
        cboCombustible.Name = "cboCombustible"
        cboCombustible.Size = New Size(190, 28)
        cboCombustible.TabIndex = 4
        '
        ' lblMecanico
        '
        lblMecanico.AutoSize = True
        lblMecanico.Location = New Point(20, 98)
        lblMecanico.Name = "lblMecanico"
        lblMecanico.Size = New Size(100, 20)
        lblMecanico.TabIndex = 5
        lblMecanico.Text = "Mecánico"
        '
        ' cboMecanico
        '
        cboMecanico.DropDownStyle = ComboBoxStyle.DropDownList
        cboMecanico.FormattingEnabled = True
        cboMecanico.Location = New Point(20, 124)
        cboMecanico.Name = "cboMecanico"
        cboMecanico.Size = New Size(190, 28)
        cboMecanico.TabIndex = 6
        '
        ' lblFechaPrometida
        '
        lblFechaPrometida.AutoSize = True
        lblFechaPrometida.Location = New Point(230, 98)
        lblFechaPrometida.Name = "lblFechaPrometida"
        lblFechaPrometida.Size = New Size(100, 20)
        lblFechaPrometida.TabIndex = 7
        lblFechaPrometida.Text = "Fecha prometida de entrega"
        '
        ' dtpFechaPrometida
        '
        dtpFechaPrometida.Checked = False
        dtpFechaPrometida.Format = DateTimePickerFormat.Short
        dtpFechaPrometida.Location = New Point(230, 124)
        dtpFechaPrometida.Name = "dtpFechaPrometida"
        dtpFechaPrometida.ShowCheckBox = True
        dtpFechaPrometida.Size = New Size(170, 27)
        dtpFechaPrometida.TabIndex = 8
        '
        ' lblAyudaFecha
        '
        lblAyudaFecha.AutoSize = True
        lblAyudaFecha.Location = New Point(230, 156)
        lblAyudaFecha.Name = "lblAyudaFecha"
        lblAyudaFecha.Size = New Size(100, 20)
        lblAyudaFecha.TabIndex = 9
        lblAyudaFecha.Text = "Opcional: marque la casilla."
        lblAyudaFecha.Font = New Font("Segoe UI", 8.5F)
        lblAyudaFecha.ForeColor = Color.DimGray
        '
        ' lblSintoma
        '
        lblSintoma.AutoSize = True
        lblSintoma.Location = New Point(20, 184)
        lblSintoma.Name = "lblSintoma"
        lblSintoma.Size = New Size(100, 20)
        lblSintoma.TabIndex = 10
        lblSintoma.Text = "Síntoma reportado por el cliente (*)"
        '
        ' txtSintoma
        '
        txtSintoma.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtSintoma.Location = New Point(20, 210)
        txtSintoma.MaxLength = 2000
        txtSintoma.Multiline = True
        txtSintoma.Name = "txtSintoma"
        txtSintoma.ScrollBars = ScrollBars.Vertical
        txtSintoma.Size = New Size(400, 80)
        txtSintoma.TabIndex = 11
        '
        ' lblObservaciones
        '
        lblObservaciones.AutoSize = True
        lblObservaciones.Location = New Point(20, 298)
        lblObservaciones.Name = "lblObservaciones"
        lblObservaciones.Size = New Size(100, 20)
        lblObservaciones.TabIndex = 12
        lblObservaciones.Text = "Observaciones de recepción"
        '
        ' txtObservaciones
        '
        txtObservaciones.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtObservaciones.Location = New Point(20, 324)
        txtObservaciones.MaxLength = 2000
        txtObservaciones.Multiline = True
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.PlaceholderText = "Rayones, faltantes, estado general, quién entrega el vehículo..."
        txtObservaciones.ScrollBars = ScrollBars.Vertical
        txtObservaciones.Size = New Size(400, 84)
        txtObservaciones.TabIndex = 13
        '
        ' pnlPaso3
        '
        pnlPaso3.Controls.Add(lblAyudaFotos)
        pnlPaso3.Controls.Add(tlpFotos)
        pnlPaso3.Dock = DockStyle.Fill
        pnlPaso3.Location = New Point(0, 0)
        pnlPaso3.Name = "pnlPaso3"
        pnlPaso3.Size = New Size(440, 420)
        pnlPaso3.TabIndex = 2
        pnlPaso3.Visible = False
        '
        ' lblAyudaFotos
        '
        lblAyudaFotos.AutoSize = True
        lblAyudaFotos.Location = New Point(20, 14)
        lblAyudaFotos.Name = "lblAyudaFotos"
        lblAyudaFotos.Size = New Size(100, 20)
        lblAyudaFotos.TabIndex = 0
        lblAyudaFotos.Text = "Son opcionales. Cargadas 0 de 5."
        lblAyudaFotos.ForeColor = Color.DimGray
        '
        ' tlpFotos
        '
        tlpFotos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpFotos.ColumnCount = 5
        tlpFotos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpFotos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpFotos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpFotos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpFotos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpFotos.Controls.Add(pnlFotoFrente, 0, 0)
        tlpFotos.Controls.Add(pnlFotoTrasera, 1, 0)
        tlpFotos.Controls.Add(pnlFotoLateralIzq, 2, 0)
        tlpFotos.Controls.Add(pnlFotoLateralDer, 3, 0)
        tlpFotos.Controls.Add(pnlFotoTablero, 4, 0)
        tlpFotos.Location = New Point(20, 44)
        tlpFotos.Name = "tlpFotos"
        tlpFotos.RowCount = 1
        tlpFotos.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpFotos.Size = New Size(400, 364)
        tlpFotos.TabIndex = 1
        '
        ' pnlFotoFrente
        '
        pnlFotoFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        pnlFotoFrente.Controls.Add(picFrente)
        pnlFotoFrente.Controls.Add(lblFrente)
        pnlFotoFrente.Controls.Add(btnCargarFrente)
        pnlFotoFrente.Controls.Add(btnQuitarFrente)
        pnlFotoFrente.Dock = DockStyle.Fill
        pnlFotoFrente.Location = New Point(0, 0)
        pnlFotoFrente.Margin = New Padding(0, 0, 6, 0)
        pnlFotoFrente.Name = "pnlFotoFrente"
        pnlFotoFrente.Padding = New Padding(4)
        pnlFotoFrente.Size = New Size(74, 364)
        pnlFotoFrente.TabIndex = 0
        '
        ' picFrente
        '
        picFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picFrente.Cursor = Cursors.Hand
        picFrente.Dock = DockStyle.Fill
        picFrente.Location = New Point(4, 34)
        picFrente.Name = "picFrente"
        picFrente.Size = New Size(66, 258)
        picFrente.SizeMode = PictureBoxSizeMode.Zoom
        picFrente.TabIndex = 0
        picFrente.TabStop = False
        '
        ' lblFrente
        '
        lblFrente.AutoEllipsis = True
        lblFrente.Dock = DockStyle.Top
        lblFrente.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblFrente.Location = New Point(4, 4)
        lblFrente.Name = "lblFrente"
        lblFrente.Size = New Size(66, 30)
        lblFrente.TabIndex = 1
        lblFrente.Text = "Frente"
        lblFrente.TextAlign = ContentAlignment.MiddleCenter
        '
        ' btnCargarFrente
        '
        btnCargarFrente.BackColor = Color.White
        btnCargarFrente.Cursor = Cursors.Hand
        btnCargarFrente.Dock = DockStyle.Bottom
        btnCargarFrente.FlatAppearance.BorderColor = Color.Silver
        btnCargarFrente.FlatStyle = FlatStyle.Flat
        btnCargarFrente.Location = New Point(4, 292)
        btnCargarFrente.Name = "btnCargarFrente"
        btnCargarFrente.Size = New Size(66, 34)
        btnCargarFrente.TabIndex = 2
        btnCargarFrente.Text = " Cargar foto"
        btnCargarFrente.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCargarFrente.UseVisualStyleBackColor = False
        '
        ' btnQuitarFrente
        '
        btnQuitarFrente.BackColor = Color.White
        btnQuitarFrente.Cursor = Cursors.Hand
        btnQuitarFrente.Dock = DockStyle.Bottom
        btnQuitarFrente.FlatAppearance.BorderColor = Color.Silver
        btnQuitarFrente.FlatStyle = FlatStyle.Flat
        btnQuitarFrente.ForeColor = Color.Firebrick
        btnQuitarFrente.Location = New Point(4, 326)
        btnQuitarFrente.Name = "btnQuitarFrente"
        btnQuitarFrente.Size = New Size(66, 34)
        btnQuitarFrente.TabIndex = 3
        btnQuitarFrente.Text = " Quitar"
        btnQuitarFrente.TextImageRelation = TextImageRelation.ImageBeforeText
        btnQuitarFrente.UseVisualStyleBackColor = False
        btnQuitarFrente.Visible = False
        '
        ' pnlFotoTrasera
        '
        pnlFotoTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        pnlFotoTrasera.Controls.Add(picTrasera)
        pnlFotoTrasera.Controls.Add(lblTrasera)
        pnlFotoTrasera.Controls.Add(btnCargarTrasera)
        pnlFotoTrasera.Controls.Add(btnQuitarTrasera)
        pnlFotoTrasera.Dock = DockStyle.Fill
        pnlFotoTrasera.Location = New Point(80, 0)
        pnlFotoTrasera.Margin = New Padding(0, 0, 6, 0)
        pnlFotoTrasera.Name = "pnlFotoTrasera"
        pnlFotoTrasera.Padding = New Padding(4)
        pnlFotoTrasera.Size = New Size(74, 364)
        pnlFotoTrasera.TabIndex = 1
        '
        ' picTrasera
        '
        picTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTrasera.Cursor = Cursors.Hand
        picTrasera.Dock = DockStyle.Fill
        picTrasera.Location = New Point(4, 34)
        picTrasera.Name = "picTrasera"
        picTrasera.Size = New Size(66, 258)
        picTrasera.SizeMode = PictureBoxSizeMode.Zoom
        picTrasera.TabIndex = 0
        picTrasera.TabStop = False
        '
        ' lblTrasera
        '
        lblTrasera.AutoEllipsis = True
        lblTrasera.Dock = DockStyle.Top
        lblTrasera.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTrasera.Location = New Point(4, 4)
        lblTrasera.Name = "lblTrasera"
        lblTrasera.Size = New Size(66, 30)
        lblTrasera.TabIndex = 1
        lblTrasera.Text = "Trasera"
        lblTrasera.TextAlign = ContentAlignment.MiddleCenter
        '
        ' btnCargarTrasera
        '
        btnCargarTrasera.BackColor = Color.White
        btnCargarTrasera.Cursor = Cursors.Hand
        btnCargarTrasera.Dock = DockStyle.Bottom
        btnCargarTrasera.FlatAppearance.BorderColor = Color.Silver
        btnCargarTrasera.FlatStyle = FlatStyle.Flat
        btnCargarTrasera.Location = New Point(4, 292)
        btnCargarTrasera.Name = "btnCargarTrasera"
        btnCargarTrasera.Size = New Size(66, 34)
        btnCargarTrasera.TabIndex = 2
        btnCargarTrasera.Text = " Cargar foto"
        btnCargarTrasera.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCargarTrasera.UseVisualStyleBackColor = False
        '
        ' btnQuitarTrasera
        '
        btnQuitarTrasera.BackColor = Color.White
        btnQuitarTrasera.Cursor = Cursors.Hand
        btnQuitarTrasera.Dock = DockStyle.Bottom
        btnQuitarTrasera.FlatAppearance.BorderColor = Color.Silver
        btnQuitarTrasera.FlatStyle = FlatStyle.Flat
        btnQuitarTrasera.ForeColor = Color.Firebrick
        btnQuitarTrasera.Location = New Point(4, 326)
        btnQuitarTrasera.Name = "btnQuitarTrasera"
        btnQuitarTrasera.Size = New Size(66, 34)
        btnQuitarTrasera.TabIndex = 3
        btnQuitarTrasera.Text = " Quitar"
        btnQuitarTrasera.TextImageRelation = TextImageRelation.ImageBeforeText
        btnQuitarTrasera.UseVisualStyleBackColor = False
        btnQuitarTrasera.Visible = False
        '
        ' pnlFotoLateralIzq
        '
        pnlFotoLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        pnlFotoLateralIzq.Controls.Add(picLateralIzq)
        pnlFotoLateralIzq.Controls.Add(lblLateralIzq)
        pnlFotoLateralIzq.Controls.Add(btnCargarLateralIzq)
        pnlFotoLateralIzq.Controls.Add(btnQuitarLateralIzq)
        pnlFotoLateralIzq.Dock = DockStyle.Fill
        pnlFotoLateralIzq.Location = New Point(160, 0)
        pnlFotoLateralIzq.Margin = New Padding(0, 0, 6, 0)
        pnlFotoLateralIzq.Name = "pnlFotoLateralIzq"
        pnlFotoLateralIzq.Padding = New Padding(4)
        pnlFotoLateralIzq.Size = New Size(74, 364)
        pnlFotoLateralIzq.TabIndex = 2
        '
        ' picLateralIzq
        '
        picLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralIzq.Cursor = Cursors.Hand
        picLateralIzq.Dock = DockStyle.Fill
        picLateralIzq.Location = New Point(4, 34)
        picLateralIzq.Name = "picLateralIzq"
        picLateralIzq.Size = New Size(66, 258)
        picLateralIzq.SizeMode = PictureBoxSizeMode.Zoom
        picLateralIzq.TabIndex = 0
        picLateralIzq.TabStop = False
        '
        ' lblLateralIzq
        '
        lblLateralIzq.AutoEllipsis = True
        lblLateralIzq.Dock = DockStyle.Top
        lblLateralIzq.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblLateralIzq.Location = New Point(4, 4)
        lblLateralIzq.Name = "lblLateralIzq"
        lblLateralIzq.Size = New Size(66, 30)
        lblLateralIzq.TabIndex = 1
        lblLateralIzq.Text = "Lateral izquierdo"
        lblLateralIzq.TextAlign = ContentAlignment.MiddleCenter
        '
        ' btnCargarLateralIzq
        '
        btnCargarLateralIzq.BackColor = Color.White
        btnCargarLateralIzq.Cursor = Cursors.Hand
        btnCargarLateralIzq.Dock = DockStyle.Bottom
        btnCargarLateralIzq.FlatAppearance.BorderColor = Color.Silver
        btnCargarLateralIzq.FlatStyle = FlatStyle.Flat
        btnCargarLateralIzq.Location = New Point(4, 292)
        btnCargarLateralIzq.Name = "btnCargarLateralIzq"
        btnCargarLateralIzq.Size = New Size(66, 34)
        btnCargarLateralIzq.TabIndex = 2
        btnCargarLateralIzq.Text = " Cargar foto"
        btnCargarLateralIzq.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCargarLateralIzq.UseVisualStyleBackColor = False
        '
        ' btnQuitarLateralIzq
        '
        btnQuitarLateralIzq.BackColor = Color.White
        btnQuitarLateralIzq.Cursor = Cursors.Hand
        btnQuitarLateralIzq.Dock = DockStyle.Bottom
        btnQuitarLateralIzq.FlatAppearance.BorderColor = Color.Silver
        btnQuitarLateralIzq.FlatStyle = FlatStyle.Flat
        btnQuitarLateralIzq.ForeColor = Color.Firebrick
        btnQuitarLateralIzq.Location = New Point(4, 326)
        btnQuitarLateralIzq.Name = "btnQuitarLateralIzq"
        btnQuitarLateralIzq.Size = New Size(66, 34)
        btnQuitarLateralIzq.TabIndex = 3
        btnQuitarLateralIzq.Text = " Quitar"
        btnQuitarLateralIzq.TextImageRelation = TextImageRelation.ImageBeforeText
        btnQuitarLateralIzq.UseVisualStyleBackColor = False
        btnQuitarLateralIzq.Visible = False
        '
        ' pnlFotoLateralDer
        '
        pnlFotoLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        pnlFotoLateralDer.Controls.Add(picLateralDer)
        pnlFotoLateralDer.Controls.Add(lblLateralDer)
        pnlFotoLateralDer.Controls.Add(btnCargarLateralDer)
        pnlFotoLateralDer.Controls.Add(btnQuitarLateralDer)
        pnlFotoLateralDer.Dock = DockStyle.Fill
        pnlFotoLateralDer.Location = New Point(240, 0)
        pnlFotoLateralDer.Margin = New Padding(0, 0, 6, 0)
        pnlFotoLateralDer.Name = "pnlFotoLateralDer"
        pnlFotoLateralDer.Padding = New Padding(4)
        pnlFotoLateralDer.Size = New Size(74, 364)
        pnlFotoLateralDer.TabIndex = 3
        '
        ' picLateralDer
        '
        picLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralDer.Cursor = Cursors.Hand
        picLateralDer.Dock = DockStyle.Fill
        picLateralDer.Location = New Point(4, 34)
        picLateralDer.Name = "picLateralDer"
        picLateralDer.Size = New Size(66, 258)
        picLateralDer.SizeMode = PictureBoxSizeMode.Zoom
        picLateralDer.TabIndex = 0
        picLateralDer.TabStop = False
        '
        ' lblLateralDer
        '
        lblLateralDer.AutoEllipsis = True
        lblLateralDer.Dock = DockStyle.Top
        lblLateralDer.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblLateralDer.Location = New Point(4, 4)
        lblLateralDer.Name = "lblLateralDer"
        lblLateralDer.Size = New Size(66, 30)
        lblLateralDer.TabIndex = 1
        lblLateralDer.Text = "Lateral derecho"
        lblLateralDer.TextAlign = ContentAlignment.MiddleCenter
        '
        ' btnCargarLateralDer
        '
        btnCargarLateralDer.BackColor = Color.White
        btnCargarLateralDer.Cursor = Cursors.Hand
        btnCargarLateralDer.Dock = DockStyle.Bottom
        btnCargarLateralDer.FlatAppearance.BorderColor = Color.Silver
        btnCargarLateralDer.FlatStyle = FlatStyle.Flat
        btnCargarLateralDer.Location = New Point(4, 292)
        btnCargarLateralDer.Name = "btnCargarLateralDer"
        btnCargarLateralDer.Size = New Size(66, 34)
        btnCargarLateralDer.TabIndex = 2
        btnCargarLateralDer.Text = " Cargar foto"
        btnCargarLateralDer.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCargarLateralDer.UseVisualStyleBackColor = False
        '
        ' btnQuitarLateralDer
        '
        btnQuitarLateralDer.BackColor = Color.White
        btnQuitarLateralDer.Cursor = Cursors.Hand
        btnQuitarLateralDer.Dock = DockStyle.Bottom
        btnQuitarLateralDer.FlatAppearance.BorderColor = Color.Silver
        btnQuitarLateralDer.FlatStyle = FlatStyle.Flat
        btnQuitarLateralDer.ForeColor = Color.Firebrick
        btnQuitarLateralDer.Location = New Point(4, 326)
        btnQuitarLateralDer.Name = "btnQuitarLateralDer"
        btnQuitarLateralDer.Size = New Size(66, 34)
        btnQuitarLateralDer.TabIndex = 3
        btnQuitarLateralDer.Text = " Quitar"
        btnQuitarLateralDer.TextImageRelation = TextImageRelation.ImageBeforeText
        btnQuitarLateralDer.UseVisualStyleBackColor = False
        btnQuitarLateralDer.Visible = False
        '
        ' pnlFotoTablero
        '
        pnlFotoTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        pnlFotoTablero.Controls.Add(picTablero)
        pnlFotoTablero.Controls.Add(lblTablero)
        pnlFotoTablero.Controls.Add(btnCargarTablero)
        pnlFotoTablero.Controls.Add(btnQuitarTablero)
        pnlFotoTablero.Dock = DockStyle.Fill
        pnlFotoTablero.Location = New Point(320, 0)
        pnlFotoTablero.Margin = New Padding(0, 0, 0, 0)
        pnlFotoTablero.Name = "pnlFotoTablero"
        pnlFotoTablero.Padding = New Padding(4)
        pnlFotoTablero.Size = New Size(74, 364)
        pnlFotoTablero.TabIndex = 4
        '
        ' picTablero
        '
        picTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTablero.Cursor = Cursors.Hand
        picTablero.Dock = DockStyle.Fill
        picTablero.Location = New Point(4, 34)
        picTablero.Name = "picTablero"
        picTablero.Size = New Size(66, 258)
        picTablero.SizeMode = PictureBoxSizeMode.Zoom
        picTablero.TabIndex = 0
        picTablero.TabStop = False
        '
        ' lblTablero
        '
        lblTablero.AutoEllipsis = True
        lblTablero.Dock = DockStyle.Top
        lblTablero.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTablero.Location = New Point(4, 4)
        lblTablero.Name = "lblTablero"
        lblTablero.Size = New Size(66, 30)
        lblTablero.TabIndex = 1
        lblTablero.Text = "Tablero"
        lblTablero.TextAlign = ContentAlignment.MiddleCenter
        '
        ' btnCargarTablero
        '
        btnCargarTablero.BackColor = Color.White
        btnCargarTablero.Cursor = Cursors.Hand
        btnCargarTablero.Dock = DockStyle.Bottom
        btnCargarTablero.FlatAppearance.BorderColor = Color.Silver
        btnCargarTablero.FlatStyle = FlatStyle.Flat
        btnCargarTablero.Location = New Point(4, 292)
        btnCargarTablero.Name = "btnCargarTablero"
        btnCargarTablero.Size = New Size(66, 34)
        btnCargarTablero.TabIndex = 2
        btnCargarTablero.Text = " Cargar foto"
        btnCargarTablero.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCargarTablero.UseVisualStyleBackColor = False
        '
        ' btnQuitarTablero
        '
        btnQuitarTablero.BackColor = Color.White
        btnQuitarTablero.Cursor = Cursors.Hand
        btnQuitarTablero.Dock = DockStyle.Bottom
        btnQuitarTablero.FlatAppearance.BorderColor = Color.Silver
        btnQuitarTablero.FlatStyle = FlatStyle.Flat
        btnQuitarTablero.ForeColor = Color.Firebrick
        btnQuitarTablero.Location = New Point(4, 326)
        btnQuitarTablero.Name = "btnQuitarTablero"
        btnQuitarTablero.Size = New Size(66, 34)
        btnQuitarTablero.TabIndex = 3
        btnQuitarTablero.Text = " Quitar"
        btnQuitarTablero.TextImageRelation = TextImageRelation.ImageBeforeText
        btnQuitarTablero.UseVisualStyleBackColor = False
        btnQuitarTablero.Visible = False
        '
        ' pnlPaso4
        '
        pnlPaso4.Controls.Add(lblResumen)
        pnlPaso4.Controls.Add(txtResumen)
        pnlPaso4.Controls.Add(lblVistas)
        pnlPaso4.Controls.Add(tlpVistas)
        pnlPaso4.Dock = DockStyle.Fill
        pnlPaso4.Location = New Point(0, 0)
        pnlPaso4.Name = "pnlPaso4"
        pnlPaso4.Size = New Size(440, 420)
        pnlPaso4.TabIndex = 3
        pnlPaso4.Visible = False
        '
        ' lblResumen
        '
        lblResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblResumen.ForeColor = Color.DimGray
        lblResumen.Location = New Point(20, 12)
        lblResumen.Name = "lblResumen"
        lblResumen.Size = New Size(400, 44)
        lblResumen.TabIndex = 0
        lblResumen.Text = "Revise los datos. Al confirmar se crea la orden de trabajo en estado Recepcionada, con su número y sus fotos."
        '
        ' txtResumen
        '
        txtResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtResumen.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtResumen.BorderStyle = BorderStyle.FixedSingle
        txtResumen.Font = New Font("Segoe UI", 10F)
        txtResumen.Location = New Point(20, 60)
        txtResumen.Multiline = True
        txtResumen.Name = "txtResumen"
        txtResumen.ReadOnly = True
        txtResumen.ScrollBars = ScrollBars.Vertical
        txtResumen.Size = New Size(400, 230)
        txtResumen.TabIndex = 1
        txtResumen.TabStop = False
        '
        ' lblVistas
        '
        lblVistas.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblVistas.AutoSize = True
        lblVistas.ForeColor = Color.DimGray
        lblVistas.Location = New Point(20, 298)
        lblVistas.Name = "lblVistas"
        lblVistas.Size = New Size(110, 20)
        lblVistas.TabIndex = 2
        lblVistas.Text = "Fotos cargadas"
        '
        ' tlpVistas
        '
        tlpVistas.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpVistas.ColumnCount = 5
        tlpVistas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpVistas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpVistas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpVistas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpVistas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpVistas.Controls.Add(picVistaFrente, 0, 0)
        tlpVistas.Controls.Add(picVistaTrasera, 1, 0)
        tlpVistas.Controls.Add(picVistaLateralIzq, 2, 0)
        tlpVistas.Controls.Add(picVistaLateralDer, 3, 0)
        tlpVistas.Controls.Add(picVistaTablero, 4, 0)
        tlpVistas.Location = New Point(20, 322)
        tlpVistas.Name = "tlpVistas"
        tlpVistas.RowCount = 1
        tlpVistas.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpVistas.Size = New Size(400, 86)
        tlpVistas.TabIndex = 3
        '
        ' picVistaFrente
        '
        picVistaFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picVistaFrente.BorderStyle = BorderStyle.FixedSingle
        picVistaFrente.Dock = DockStyle.Fill
        picVistaFrente.Location = New Point(0, 0)
        picVistaFrente.Margin = New Padding(0, 0, 6, 0)
        picVistaFrente.Name = "picVistaFrente"
        picVistaFrente.Size = New Size(74, 86)
        picVistaFrente.SizeMode = PictureBoxSizeMode.Zoom
        picVistaFrente.TabIndex = 0
        picVistaFrente.TabStop = False
        '
        ' picVistaTrasera
        '
        picVistaTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picVistaTrasera.BorderStyle = BorderStyle.FixedSingle
        picVistaTrasera.Dock = DockStyle.Fill
        picVistaTrasera.Location = New Point(80, 0)
        picVistaTrasera.Margin = New Padding(0, 0, 6, 0)
        picVistaTrasera.Name = "picVistaTrasera"
        picVistaTrasera.Size = New Size(74, 86)
        picVistaTrasera.SizeMode = PictureBoxSizeMode.Zoom
        picVistaTrasera.TabIndex = 1
        picVistaTrasera.TabStop = False
        '
        ' picVistaLateralIzq
        '
        picVistaLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picVistaLateralIzq.BorderStyle = BorderStyle.FixedSingle
        picVistaLateralIzq.Dock = DockStyle.Fill
        picVistaLateralIzq.Location = New Point(160, 0)
        picVistaLateralIzq.Margin = New Padding(0, 0, 6, 0)
        picVistaLateralIzq.Name = "picVistaLateralIzq"
        picVistaLateralIzq.Size = New Size(74, 86)
        picVistaLateralIzq.SizeMode = PictureBoxSizeMode.Zoom
        picVistaLateralIzq.TabIndex = 2
        picVistaLateralIzq.TabStop = False
        '
        ' picVistaLateralDer
        '
        picVistaLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picVistaLateralDer.BorderStyle = BorderStyle.FixedSingle
        picVistaLateralDer.Dock = DockStyle.Fill
        picVistaLateralDer.Location = New Point(240, 0)
        picVistaLateralDer.Margin = New Padding(0, 0, 6, 0)
        picVistaLateralDer.Name = "picVistaLateralDer"
        picVistaLateralDer.Size = New Size(74, 86)
        picVistaLateralDer.SizeMode = PictureBoxSizeMode.Zoom
        picVistaLateralDer.TabIndex = 3
        picVistaLateralDer.TabStop = False
        '
        ' picVistaTablero
        '
        picVistaTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picVistaTablero.BorderStyle = BorderStyle.FixedSingle
        picVistaTablero.Dock = DockStyle.Fill
        picVistaTablero.Location = New Point(320, 0)
        picVistaTablero.Margin = New Padding(0, 0, 0, 0)
        picVistaTablero.Name = "picVistaTablero"
        picVistaTablero.Size = New Size(74, 86)
        picVistaTablero.SizeMode = PictureBoxSizeMode.Zoom
        picVistaTablero.TabIndex = 4
        picVistaTablero.TabStop = False
        '
        ' pnlResumen
        '
        pnlResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pnlResumen.BackColor = Color.White
        pnlResumen.Controls.Add(lblResTitulo)
        pnlResumen.Controls.Add(picResVehiculo)
        pnlResumen.Controls.Add(lblResVehiculoTit)
        pnlResumen.Controls.Add(lblResVehiculo)
        pnlResumen.Controls.Add(picResCliente)
        pnlResumen.Controls.Add(lblResClienteTit)
        pnlResumen.Controls.Add(lblResCliente)
        pnlResumen.Controls.Add(picResKm)
        pnlResumen.Controls.Add(lblResKmTit)
        pnlResumen.Controls.Add(lblResKm)
        pnlResumen.Controls.Add(picResCombustible)
        pnlResumen.Controls.Add(lblResCombustibleTit)
        pnlResumen.Controls.Add(lblResCombustible)
        pnlResumen.Controls.Add(picResMecanico)
        pnlResumen.Controls.Add(lblResMecanicoTit)
        pnlResumen.Controls.Add(lblResMecanico)
        pnlResumen.Controls.Add(picResFecha)
        pnlResumen.Controls.Add(lblResFechaTit)
        pnlResumen.Controls.Add(lblResFecha)
        pnlResumen.Controls.Add(picResFotos)
        pnlResumen.Controls.Add(lblResFotosTit)
        pnlResumen.Controls.Add(lblResFotos)
        pnlResumen.Controls.Add(picResSintoma)
        pnlResumen.Controls.Add(lblResSintomaTit)
        pnlResumen.Controls.Add(lblResSintoma)
        pnlResumen.Location = New Point(480, 152)
        pnlResumen.Name = "pnlResumen"
        pnlResumen.Size = New Size(360, 420)
        pnlResumen.TabIndex = 4
        '
        ' lblResTitulo
        '
        lblResTitulo.AutoSize = True
        lblResTitulo.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblResTitulo.Location = New Point(16, 12)
        lblResTitulo.Name = "lblResTitulo"
        lblResTitulo.Size = New Size(230, 25)
        lblResTitulo.TabIndex = 0
        lblResTitulo.Text = "Resumen de la recepción"
        '
        ' picResVehiculo
        '
        picResVehiculo.Location = New Point(12, 46)
        picResVehiculo.Name = "picResVehiculo"
        picResVehiculo.Size = New Size(26, 28)
        picResVehiculo.SizeMode = PictureBoxSizeMode.CenterImage
        picResVehiculo.TabIndex = 1
        picResVehiculo.TabStop = False
        '
        ' lblResVehiculoTit
        '
        lblResVehiculoTit.AutoSize = True
        lblResVehiculoTit.Location = New Point(42, 50)
        lblResVehiculoTit.Name = "lblResVehiculoTit"
        lblResVehiculoTit.Size = New Size(100, 20)
        lblResVehiculoTit.TabIndex = 2
        lblResVehiculoTit.Text = "Vehículo"
        lblResVehiculoTit.ForeColor = Color.DimGray
        '
        ' lblResVehiculo
        '
        lblResVehiculo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblResVehiculo.AutoEllipsis = True
        lblResVehiculo.Location = New Point(164, 50)
        lblResVehiculo.Name = "lblResVehiculo"
        lblResVehiculo.Size = New Size(180, 22)
        lblResVehiculo.TabIndex = 3
        lblResVehiculo.Text = "-"
        '
        ' picResCliente
        '
        picResCliente.Location = New Point(12, 74)
        picResCliente.Name = "picResCliente"
        picResCliente.Size = New Size(26, 28)
        picResCliente.SizeMode = PictureBoxSizeMode.CenterImage
        picResCliente.TabIndex = 4
        picResCliente.TabStop = False
        '
        ' lblResClienteTit
        '
        lblResClienteTit.AutoSize = True
        lblResClienteTit.Location = New Point(42, 78)
        lblResClienteTit.Name = "lblResClienteTit"
        lblResClienteTit.Size = New Size(100, 20)
        lblResClienteTit.TabIndex = 5
        lblResClienteTit.Text = "Cliente"
        lblResClienteTit.ForeColor = Color.DimGray
        '
        ' lblResCliente
        '
        lblResCliente.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblResCliente.AutoEllipsis = True
        lblResCliente.Location = New Point(164, 78)
        lblResCliente.Name = "lblResCliente"
        lblResCliente.Size = New Size(180, 22)
        lblResCliente.TabIndex = 6
        lblResCliente.Text = "-"
        '
        ' picResKm
        '
        picResKm.Location = New Point(12, 102)
        picResKm.Name = "picResKm"
        picResKm.Size = New Size(26, 28)
        picResKm.SizeMode = PictureBoxSizeMode.CenterImage
        picResKm.TabIndex = 7
        picResKm.TabStop = False
        '
        ' lblResKmTit
        '
        lblResKmTit.AutoSize = True
        lblResKmTit.Location = New Point(42, 106)
        lblResKmTit.Name = "lblResKmTit"
        lblResKmTit.Size = New Size(100, 20)
        lblResKmTit.TabIndex = 8
        lblResKmTit.Text = "Km de ingreso"
        lblResKmTit.ForeColor = Color.DimGray
        '
        ' lblResKm
        '
        lblResKm.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblResKm.AutoEllipsis = True
        lblResKm.Location = New Point(164, 106)
        lblResKm.Name = "lblResKm"
        lblResKm.Size = New Size(180, 22)
        lblResKm.TabIndex = 9
        lblResKm.Text = "-"
        '
        ' picResCombustible
        '
        picResCombustible.Location = New Point(12, 130)
        picResCombustible.Name = "picResCombustible"
        picResCombustible.Size = New Size(26, 28)
        picResCombustible.SizeMode = PictureBoxSizeMode.CenterImage
        picResCombustible.TabIndex = 10
        picResCombustible.TabStop = False
        '
        ' lblResCombustibleTit
        '
        lblResCombustibleTit.AutoSize = True
        lblResCombustibleTit.Location = New Point(42, 134)
        lblResCombustibleTit.Name = "lblResCombustibleTit"
        lblResCombustibleTit.Size = New Size(100, 20)
        lblResCombustibleTit.TabIndex = 11
        lblResCombustibleTit.Text = "Combustible"
        lblResCombustibleTit.ForeColor = Color.DimGray
        '
        ' lblResCombustible
        '
        lblResCombustible.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblResCombustible.AutoEllipsis = True
        lblResCombustible.Location = New Point(164, 134)
        lblResCombustible.Name = "lblResCombustible"
        lblResCombustible.Size = New Size(180, 22)
        lblResCombustible.TabIndex = 12
        lblResCombustible.Text = "-"
        '
        ' picResMecanico
        '
        picResMecanico.Location = New Point(12, 158)
        picResMecanico.Name = "picResMecanico"
        picResMecanico.Size = New Size(26, 28)
        picResMecanico.SizeMode = PictureBoxSizeMode.CenterImage
        picResMecanico.TabIndex = 13
        picResMecanico.TabStop = False
        '
        ' lblResMecanicoTit
        '
        lblResMecanicoTit.AutoSize = True
        lblResMecanicoTit.Location = New Point(42, 162)
        lblResMecanicoTit.Name = "lblResMecanicoTit"
        lblResMecanicoTit.Size = New Size(100, 20)
        lblResMecanicoTit.TabIndex = 14
        lblResMecanicoTit.Text = "Mecánico"
        lblResMecanicoTit.ForeColor = Color.DimGray
        '
        ' lblResMecanico
        '
        lblResMecanico.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblResMecanico.AutoEllipsis = True
        lblResMecanico.Location = New Point(164, 162)
        lblResMecanico.Name = "lblResMecanico"
        lblResMecanico.Size = New Size(180, 22)
        lblResMecanico.TabIndex = 15
        lblResMecanico.Text = "-"
        '
        ' picResFecha
        '
        picResFecha.Location = New Point(12, 186)
        picResFecha.Name = "picResFecha"
        picResFecha.Size = New Size(26, 28)
        picResFecha.SizeMode = PictureBoxSizeMode.CenterImage
        picResFecha.TabIndex = 16
        picResFecha.TabStop = False
        '
        ' lblResFechaTit
        '
        lblResFechaTit.AutoSize = True
        lblResFechaTit.Location = New Point(42, 190)
        lblResFechaTit.Name = "lblResFechaTit"
        lblResFechaTit.Size = New Size(100, 20)
        lblResFechaTit.TabIndex = 17
        lblResFechaTit.Text = "Fecha prometida"
        lblResFechaTit.ForeColor = Color.DimGray
        '
        ' lblResFecha
        '
        lblResFecha.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblResFecha.AutoEllipsis = True
        lblResFecha.Location = New Point(164, 190)
        lblResFecha.Name = "lblResFecha"
        lblResFecha.Size = New Size(180, 22)
        lblResFecha.TabIndex = 18
        lblResFecha.Text = "-"
        '
        ' picResFotos
        '
        picResFotos.Location = New Point(12, 214)
        picResFotos.Name = "picResFotos"
        picResFotos.Size = New Size(26, 28)
        picResFotos.SizeMode = PictureBoxSizeMode.CenterImage
        picResFotos.TabIndex = 19
        picResFotos.TabStop = False
        '
        ' lblResFotosTit
        '
        lblResFotosTit.AutoSize = True
        lblResFotosTit.Location = New Point(42, 218)
        lblResFotosTit.Name = "lblResFotosTit"
        lblResFotosTit.Size = New Size(100, 20)
        lblResFotosTit.TabIndex = 20
        lblResFotosTit.Text = "Fotos"
        lblResFotosTit.ForeColor = Color.DimGray
        '
        ' lblResFotos
        '
        lblResFotos.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblResFotos.AutoEllipsis = True
        lblResFotos.Location = New Point(164, 218)
        lblResFotos.Name = "lblResFotos"
        lblResFotos.Size = New Size(180, 22)
        lblResFotos.TabIndex = 21
        lblResFotos.Text = "-"
        '
        ' picResSintoma
        '
        picResSintoma.Location = New Point(12, 246)
        picResSintoma.Name = "picResSintoma"
        picResSintoma.Size = New Size(26, 28)
        picResSintoma.SizeMode = PictureBoxSizeMode.CenterImage
        picResSintoma.TabIndex = 22
        picResSintoma.TabStop = False
        '
        ' lblResSintomaTit
        '
        lblResSintomaTit.AutoSize = True
        lblResSintomaTit.Location = New Point(42, 250)
        lblResSintomaTit.Name = "lblResSintomaTit"
        lblResSintomaTit.Size = New Size(100, 20)
        lblResSintomaTit.TabIndex = 23
        lblResSintomaTit.Text = "Síntoma reportado"
        lblResSintomaTit.ForeColor = Color.DimGray
        '
        ' lblResSintoma
        '
        lblResSintoma.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblResSintoma.AutoEllipsis = True
        lblResSintoma.Location = New Point(16, 278)
        lblResSintoma.Name = "lblResSintoma"
        lblResSintoma.Size = New Size(328, 130)
        lblResSintoma.TabIndex = 24
        lblResSintoma.Text = "-"
        '
        ' btnCancelar
        '
        btnCancelar.BackColor = Color.White
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.FlatAppearance.BorderColor = Color.Silver
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Name = "btnCancelar"
        btnCancelar.TabIndex = 5
        btnCancelar.Text = " Cancelar"
        btnCancelar.UseVisualStyleBackColor = False
        btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCancelar.Font = New Font("Segoe UI", 10F)
        btnCancelar.Location = New Point(30, 582)
        btnCancelar.Size = New Size(120, 38)
        '
        ' btnAnterior
        '
        btnAnterior.BackColor = Color.White
        btnAnterior.Cursor = Cursors.Hand
        btnAnterior.FlatAppearance.BorderColor = Color.Silver
        btnAnterior.FlatStyle = FlatStyle.Flat
        btnAnterior.Name = "btnAnterior"
        btnAnterior.TabIndex = 6
        btnAnterior.Text = " Anterior"
        btnAnterior.UseVisualStyleBackColor = False
        btnAnterior.TextImageRelation = TextImageRelation.ImageBeforeText
        btnAnterior.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnAnterior.Font = New Font("Segoe UI", 10F)
        btnAnterior.Location = New Point(500, 582)
        btnAnterior.Size = New Size(120, 38)
        btnAnterior.Visible = False
        '
        ' btnSiguiente
        '
        btnSiguiente.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnSiguiente.Cursor = Cursors.Hand
        btnSiguiente.FlatAppearance.BorderSize = 0
        btnSiguiente.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnSiguiente.FlatStyle = FlatStyle.Flat
        btnSiguiente.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnSiguiente.ForeColor = Color.White
        btnSiguiente.Name = "btnSiguiente"
        btnSiguiente.TabIndex = 7
        btnSiguiente.Text = "Siguiente "
        btnSiguiente.UseVisualStyleBackColor = False
        btnSiguiente.TextImageRelation = TextImageRelation.TextBeforeImage
        btnSiguiente.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSiguiente.Location = New Point(630, 582)
        btnSiguiente.Size = New Size(210, 38)
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
        btnConfirmar.Name = "btnConfirmar"
        btnConfirmar.TabIndex = 8
        btnConfirmar.Text = " Confirmar recepción"
        btnConfirmar.UseVisualStyleBackColor = False
        btnConfirmar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnConfirmar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnConfirmar.Location = New Point(630, 582)
        btnConfirmar.Size = New Size(210, 38)
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
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(btnConfirmar)
        Controls.Add(btnSiguiente)
        Controls.Add(btnAnterior)
        Controls.Add(btnCancelar)
        Controls.Add(pnlResumen)
        Controls.Add(pnlTarjeta)
        Controls.Add(tlpPasos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmRecepcion"
        Text = "Recepción de vehículo"
        tlpPasos.ResumeLayout(False)
        pnlBarra1.ResumeLayout(False)
        pnlBarra2.ResumeLayout(False)
        pnlBarra3.ResumeLayout(False)
        pnlBarra4.ResumeLayout(False)
        pnlTarjeta.ResumeLayout(False)
        pnlNoRegistrada.ResumeLayout(False)
        pnlPaso1.ResumeLayout(False)
        pnlPaso1.PerformLayout()
        pnlVehiculo.ResumeLayout(False)
        pnlVehiculo.PerformLayout()
        pnlPaso2.ResumeLayout(False)
        pnlPaso2.PerformLayout()
        pnlPaso3.ResumeLayout(False)
        pnlPaso3.PerformLayout()
        tlpFotos.ResumeLayout(False)
        pnlFotoFrente.ResumeLayout(False)
        pnlFotoTrasera.ResumeLayout(False)
        pnlFotoLateralIzq.ResumeLayout(False)
        pnlFotoLateralDer.ResumeLayout(False)
        pnlFotoTablero.ResumeLayout(False)
        pnlPaso4.ResumeLayout(False)
        pnlPaso4.PerformLayout()
        tlpVistas.ResumeLayout(False)
        pnlResumen.ResumeLayout(False)
        pnlResumen.PerformLayout()
        CType(dgvAnteriores, ComponentModel.ISupportInitialize).EndInit()
        CType(nudKmIngreso, ComponentModel.ISupportInitialize).EndInit()
        CType(picResVehiculo, ComponentModel.ISupportInitialize).EndInit()
        CType(picResCliente, ComponentModel.ISupportInitialize).EndInit()
        CType(picResKm, ComponentModel.ISupportInitialize).EndInit()
        CType(picResCombustible, ComponentModel.ISupportInitialize).EndInit()
        CType(picResMecanico, ComponentModel.ISupportInitialize).EndInit()
        CType(picResFecha, ComponentModel.ISupportInitialize).EndInit()
        CType(picResFotos, ComponentModel.ISupportInitialize).EndInit()
        CType(picResSintoma, ComponentModel.ISupportInitialize).EndInit()
        CType(picFrente, ComponentModel.ISupportInitialize).EndInit()
        CType(picTrasera, ComponentModel.ISupportInitialize).EndInit()
        CType(picLateralIzq, ComponentModel.ISupportInitialize).EndInit()
        CType(picLateralDer, ComponentModel.ISupportInitialize).EndInit()
        CType(picTablero, ComponentModel.ISupportInitialize).EndInit()
        CType(picVistaFrente, ComponentModel.ISupportInitialize).EndInit()
        CType(picVistaTrasera, ComponentModel.ISupportInitialize).EndInit()
        CType(picVistaLateralIzq, ComponentModel.ISupportInitialize).EndInit()
        CType(picVistaLateralDer, ComponentModel.ISupportInitialize).EndInit()
        CType(picVistaTablero, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents tlpPasos As TableLayoutPanel
    Friend WithEvents pnlBarra1 As Panel
    Friend WithEvents lblNomPaso1 As Label
    Friend WithEvents lblNumPaso1 As Label
    Friend WithEvents pnlBarra2 As Panel
    Friend WithEvents lblNomPaso2 As Label
    Friend WithEvents lblNumPaso2 As Label
    Friend WithEvents pnlBarra3 As Panel
    Friend WithEvents lblNomPaso3 As Label
    Friend WithEvents lblNumPaso3 As Label
    Friend WithEvents pnlBarra4 As Panel
    Friend WithEvents lblNomPaso4 As Label
    Friend WithEvents lblNumPaso4 As Label
    Friend WithEvents pnlTarjeta As Panel
    Friend WithEvents pnlPaso1 As Panel
    Friend WithEvents lblPatente As Label
    Friend WithEvents txtPatente As TextBox
    Friend WithEvents btnBuscar As Button
    Friend WithEvents lblAyudaPatente As Label
    Friend WithEvents pnlNoRegistrada As Panel
    Friend WithEvents lblNoRegistrada As Label
    Friend WithEvents lblNoRegistradaAyuda As Label
    Friend WithEvents btnRegistrarVehiculo As Button
    Friend WithEvents pnlVehiculo As Panel
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
    Friend WithEvents lblFechaPrometida As Label
    Friend WithEvents dtpFechaPrometida As DateTimePicker
    Friend WithEvents lblAyudaFecha As Label
    Friend WithEvents lblSintoma As Label
    Friend WithEvents txtSintoma As TextBox
    Friend WithEvents lblObservaciones As Label
    Friend WithEvents txtObservaciones As TextBox
    Friend WithEvents pnlPaso3 As Panel
    Friend WithEvents lblAyudaFotos As Label
    Friend WithEvents tlpFotos As TableLayoutPanel
    Friend WithEvents pnlFotoFrente As Panel
    Friend WithEvents picFrente As PictureBox
    Friend WithEvents lblFrente As Label
    Friend WithEvents btnCargarFrente As Button
    Friend WithEvents btnQuitarFrente As Button
    Friend WithEvents pnlFotoTrasera As Panel
    Friend WithEvents picTrasera As PictureBox
    Friend WithEvents lblTrasera As Label
    Friend WithEvents btnCargarTrasera As Button
    Friend WithEvents btnQuitarTrasera As Button
    Friend WithEvents pnlFotoLateralIzq As Panel
    Friend WithEvents picLateralIzq As PictureBox
    Friend WithEvents lblLateralIzq As Label
    Friend WithEvents btnCargarLateralIzq As Button
    Friend WithEvents btnQuitarLateralIzq As Button
    Friend WithEvents pnlFotoLateralDer As Panel
    Friend WithEvents picLateralDer As PictureBox
    Friend WithEvents lblLateralDer As Label
    Friend WithEvents btnCargarLateralDer As Button
    Friend WithEvents btnQuitarLateralDer As Button
    Friend WithEvents pnlFotoTablero As Panel
    Friend WithEvents picTablero As PictureBox
    Friend WithEvents lblTablero As Label
    Friend WithEvents btnCargarTablero As Button
    Friend WithEvents btnQuitarTablero As Button
    Friend WithEvents pnlPaso4 As Panel
    Friend WithEvents lblResumen As Label
    Friend WithEvents txtResumen As TextBox
    Friend WithEvents lblVistas As Label
    Friend WithEvents tlpVistas As TableLayoutPanel
    Friend WithEvents picVistaFrente As PictureBox
    Friend WithEvents picVistaTrasera As PictureBox
    Friend WithEvents picVistaLateralIzq As PictureBox
    Friend WithEvents picVistaLateralDer As PictureBox
    Friend WithEvents picVistaTablero As PictureBox
    Friend WithEvents pnlResumen As Panel
    Friend WithEvents lblResTitulo As Label
    Friend WithEvents picResVehiculo As PictureBox
    Friend WithEvents lblResVehiculoTit As Label
    Friend WithEvents lblResVehiculo As Label
    Friend WithEvents picResCliente As PictureBox
    Friend WithEvents lblResClienteTit As Label
    Friend WithEvents lblResCliente As Label
    Friend WithEvents picResKm As PictureBox
    Friend WithEvents lblResKmTit As Label
    Friend WithEvents lblResKm As Label
    Friend WithEvents picResCombustible As PictureBox
    Friend WithEvents lblResCombustibleTit As Label
    Friend WithEvents lblResCombustible As Label
    Friend WithEvents picResMecanico As PictureBox
    Friend WithEvents lblResMecanicoTit As Label
    Friend WithEvents lblResMecanico As Label
    Friend WithEvents picResFecha As PictureBox
    Friend WithEvents lblResFechaTit As Label
    Friend WithEvents lblResFecha As Label
    Friend WithEvents picResFotos As PictureBox
    Friend WithEvents lblResFotosTit As Label
    Friend WithEvents lblResFotos As Label
    Friend WithEvents picResSintoma As PictureBox
    Friend WithEvents lblResSintomaTit As Label
    Friend WithEvents lblResSintoma As Label
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnAnterior As Button
    Friend WithEvents btnSiguiente As Button
    Friend WithEvents btnConfirmar As Button
    Friend WithEvents dlgFoto As OpenFileDialog
End Class

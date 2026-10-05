<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOrdenes
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
        tlpEstados = New TableLayoutPanel()
        pnlEstado1 = New Panel()
        lblNom1 = New Label()
        lblCant1 = New Label()
        pnlEstado2 = New Panel()
        lblNom2 = New Label()
        lblCant2 = New Label()
        pnlEstado3 = New Panel()
        lblNom3 = New Label()
        lblCant3 = New Label()
        pnlEstado4 = New Panel()
        lblNom4 = New Label()
        lblCant4 = New Label()
        pnlEstado5 = New Panel()
        lblNom5 = New Label()
        lblCant5 = New Label()
        pnlEstado6 = New Panel()
        lblNom6 = New Label()
        lblCant6 = New Label()
        pnlEstado7 = New Panel()
        lblNom7 = New Label()
        lblCant7 = New Label()
        pnlEstado8 = New Panel()
        lblNom8 = New Label()
        lblCant8 = New Label()
        pnlFiltros = New Panel()
        lblTexto = New Label()
        txtTexto = New TextBox()
        chkDemoradas = New CheckBox()
        btnLimpiar = New Button()
        dgvOrdenes = New DataGridView()
        pnlDetalle = New Panel()
        lblAyuda = New Label()
        pnlDatosOrden = New Panel()
        lblDetNumero = New Label()
        lblDetEstado = New Label()
        lblDetVehiculoTit = New Label()
        lblDetVehiculo = New Label()
        lblDetClienteTit = New Label()
        lblDetCliente = New Label()
        lblDetMecanicoTit = New Label()
        lblDetMecanico = New Label()
        lblDetRecepcionTit = New Label()
        lblDetRecepcion = New Label()
        lblDetPrometidaTit = New Label()
        lblDetPrometida = New Label()
        lblDetTotalTit = New Label()
        lblDetTotal = New Label()
        lblFotos = New Label()
        lblSinFrente = New Label()
        picFrente = New PictureBox()
        lblFrente = New Label()
        lblSinTrasera = New Label()
        picTrasera = New PictureBox()
        lblTrasera = New Label()
        lblSinLateralIzq = New Label()
        picLateralIzq = New PictureBox()
        lblLateralIzq = New Label()
        lblSinLateralDer = New Label()
        picLateralDer = New PictureBox()
        lblLateralDer = New Label()
        lblSinTablero = New Label()
        picTablero = New PictureBox()
        lblTablero = New Label()
        btnGestionar = New Button()
        tlpEstados.SuspendLayout()
        pnlEstado1.SuspendLayout()
        pnlEstado2.SuspendLayout()
        pnlEstado3.SuspendLayout()
        pnlEstado4.SuspendLayout()
        pnlEstado5.SuspendLayout()
        pnlEstado6.SuspendLayout()
        pnlEstado7.SuspendLayout()
        pnlEstado8.SuspendLayout()
        pnlFiltros.SuspendLayout()
        pnlDetalle.SuspendLayout()
        pnlDatosOrden.SuspendLayout()
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).BeginInit()
        CType(picFrente, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTrasera, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralIzq, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralDer, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTablero, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(372, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Órdenes de trabajo"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 62)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(220, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "0 órdenes · 0 demoradas"
        '
        ' tlpEstados
        '
        tlpEstados.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tlpEstados.ColumnCount = 8
        tlpEstados.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlpEstados.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlpEstados.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlpEstados.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlpEstados.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlpEstados.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlpEstados.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlpEstados.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.5F))
        tlpEstados.Controls.Add(pnlEstado1, 0, 0)
        tlpEstados.Controls.Add(pnlEstado2, 1, 0)
        tlpEstados.Controls.Add(pnlEstado3, 2, 0)
        tlpEstados.Controls.Add(pnlEstado4, 3, 0)
        tlpEstados.Controls.Add(pnlEstado5, 4, 0)
        tlpEstados.Controls.Add(pnlEstado6, 5, 0)
        tlpEstados.Controls.Add(pnlEstado7, 6, 0)
        tlpEstados.Controls.Add(pnlEstado8, 7, 0)
        tlpEstados.Location = New Point(30, 94)
        tlpEstados.Name = "tlpEstados"
        tlpEstados.RowCount = 1
        tlpEstados.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpEstados.Size = New Size(810, 76)
        tlpEstados.TabIndex = 2
        '
        ' pnlEstado1
        '
        pnlEstado1.BackColor = Color.White
        pnlEstado1.Controls.Add(lblNom1)
        pnlEstado1.Controls.Add(lblCant1)
        pnlEstado1.Cursor = Cursors.Hand
        pnlEstado1.Dock = DockStyle.Fill
        pnlEstado1.Location = New Point(0, 0)
        pnlEstado1.Margin = New Padding(0, 0, 6, 0)
        pnlEstado1.Name = "pnlEstado1"
        pnlEstado1.Size = New Size(95, 76)
        pnlEstado1.TabIndex = 0
        '
        ' lblNom1
        '
        lblNom1.Dock = DockStyle.Fill
        lblNom1.Font = New Font("Segoe UI", 8.25F)
        lblNom1.ForeColor = Color.DimGray
        lblNom1.Location = New Point(0, 46)
        lblNom1.Name = "lblNom1"
        lblNom1.Size = New Size(95, 30)
        lblNom1.TabIndex = 1
        lblNom1.Text = "-"
        lblNom1.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCant1
        '
        lblCant1.Dock = DockStyle.Top
        lblCant1.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCant1.Location = New Point(0, 0)
        lblCant1.Name = "lblCant1"
        lblCant1.Size = New Size(95, 46)
        lblCant1.TabIndex = 0
        lblCant1.Text = "0"
        lblCant1.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlEstado2
        '
        pnlEstado2.BackColor = Color.White
        pnlEstado2.Controls.Add(lblNom2)
        pnlEstado2.Controls.Add(lblCant2)
        pnlEstado2.Cursor = Cursors.Hand
        pnlEstado2.Dock = DockStyle.Fill
        pnlEstado2.Location = New Point(101, 0)
        pnlEstado2.Margin = New Padding(0, 0, 6, 0)
        pnlEstado2.Name = "pnlEstado2"
        pnlEstado2.Size = New Size(95, 76)
        pnlEstado2.TabIndex = 1
        '
        ' lblNom2
        '
        lblNom2.Dock = DockStyle.Fill
        lblNom2.Font = New Font("Segoe UI", 8.25F)
        lblNom2.ForeColor = Color.DimGray
        lblNom2.Location = New Point(0, 46)
        lblNom2.Name = "lblNom2"
        lblNom2.Size = New Size(95, 30)
        lblNom2.TabIndex = 1
        lblNom2.Text = "-"
        lblNom2.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCant2
        '
        lblCant2.Dock = DockStyle.Top
        lblCant2.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCant2.Location = New Point(0, 0)
        lblCant2.Name = "lblCant2"
        lblCant2.Size = New Size(95, 46)
        lblCant2.TabIndex = 0
        lblCant2.Text = "0"
        lblCant2.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlEstado3
        '
        pnlEstado3.BackColor = Color.White
        pnlEstado3.Controls.Add(lblNom3)
        pnlEstado3.Controls.Add(lblCant3)
        pnlEstado3.Cursor = Cursors.Hand
        pnlEstado3.Dock = DockStyle.Fill
        pnlEstado3.Location = New Point(202, 0)
        pnlEstado3.Margin = New Padding(0, 0, 6, 0)
        pnlEstado3.Name = "pnlEstado3"
        pnlEstado3.Size = New Size(95, 76)
        pnlEstado3.TabIndex = 2
        '
        ' lblNom3
        '
        lblNom3.Dock = DockStyle.Fill
        lblNom3.Font = New Font("Segoe UI", 8.25F)
        lblNom3.ForeColor = Color.DimGray
        lblNom3.Location = New Point(0, 46)
        lblNom3.Name = "lblNom3"
        lblNom3.Size = New Size(95, 30)
        lblNom3.TabIndex = 1
        lblNom3.Text = "-"
        lblNom3.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCant3
        '
        lblCant3.Dock = DockStyle.Top
        lblCant3.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCant3.Location = New Point(0, 0)
        lblCant3.Name = "lblCant3"
        lblCant3.Size = New Size(95, 46)
        lblCant3.TabIndex = 0
        lblCant3.Text = "0"
        lblCant3.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlEstado4
        '
        pnlEstado4.BackColor = Color.White
        pnlEstado4.Controls.Add(lblNom4)
        pnlEstado4.Controls.Add(lblCant4)
        pnlEstado4.Cursor = Cursors.Hand
        pnlEstado4.Dock = DockStyle.Fill
        pnlEstado4.Location = New Point(303, 0)
        pnlEstado4.Margin = New Padding(0, 0, 6, 0)
        pnlEstado4.Name = "pnlEstado4"
        pnlEstado4.Size = New Size(95, 76)
        pnlEstado4.TabIndex = 3
        '
        ' lblNom4
        '
        lblNom4.Dock = DockStyle.Fill
        lblNom4.Font = New Font("Segoe UI", 8.25F)
        lblNom4.ForeColor = Color.DimGray
        lblNom4.Location = New Point(0, 46)
        lblNom4.Name = "lblNom4"
        lblNom4.Size = New Size(95, 30)
        lblNom4.TabIndex = 1
        lblNom4.Text = "-"
        lblNom4.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCant4
        '
        lblCant4.Dock = DockStyle.Top
        lblCant4.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCant4.Location = New Point(0, 0)
        lblCant4.Name = "lblCant4"
        lblCant4.Size = New Size(95, 46)
        lblCant4.TabIndex = 0
        lblCant4.Text = "0"
        lblCant4.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlEstado5
        '
        pnlEstado5.BackColor = Color.White
        pnlEstado5.Controls.Add(lblNom5)
        pnlEstado5.Controls.Add(lblCant5)
        pnlEstado5.Cursor = Cursors.Hand
        pnlEstado5.Dock = DockStyle.Fill
        pnlEstado5.Location = New Point(404, 0)
        pnlEstado5.Margin = New Padding(0, 0, 6, 0)
        pnlEstado5.Name = "pnlEstado5"
        pnlEstado5.Size = New Size(95, 76)
        pnlEstado5.TabIndex = 4
        '
        ' lblNom5
        '
        lblNom5.Dock = DockStyle.Fill
        lblNom5.Font = New Font("Segoe UI", 8.25F)
        lblNom5.ForeColor = Color.DimGray
        lblNom5.Location = New Point(0, 46)
        lblNom5.Name = "lblNom5"
        lblNom5.Size = New Size(95, 30)
        lblNom5.TabIndex = 1
        lblNom5.Text = "-"
        lblNom5.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCant5
        '
        lblCant5.Dock = DockStyle.Top
        lblCant5.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCant5.Location = New Point(0, 0)
        lblCant5.Name = "lblCant5"
        lblCant5.Size = New Size(95, 46)
        lblCant5.TabIndex = 0
        lblCant5.Text = "0"
        lblCant5.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlEstado6
        '
        pnlEstado6.BackColor = Color.White
        pnlEstado6.Controls.Add(lblNom6)
        pnlEstado6.Controls.Add(lblCant6)
        pnlEstado6.Cursor = Cursors.Hand
        pnlEstado6.Dock = DockStyle.Fill
        pnlEstado6.Location = New Point(505, 0)
        pnlEstado6.Margin = New Padding(0, 0, 6, 0)
        pnlEstado6.Name = "pnlEstado6"
        pnlEstado6.Size = New Size(95, 76)
        pnlEstado6.TabIndex = 5
        '
        ' lblNom6
        '
        lblNom6.Dock = DockStyle.Fill
        lblNom6.Font = New Font("Segoe UI", 8.25F)
        lblNom6.ForeColor = Color.DimGray
        lblNom6.Location = New Point(0, 46)
        lblNom6.Name = "lblNom6"
        lblNom6.Size = New Size(95, 30)
        lblNom6.TabIndex = 1
        lblNom6.Text = "-"
        lblNom6.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCant6
        '
        lblCant6.Dock = DockStyle.Top
        lblCant6.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCant6.Location = New Point(0, 0)
        lblCant6.Name = "lblCant6"
        lblCant6.Size = New Size(95, 46)
        lblCant6.TabIndex = 0
        lblCant6.Text = "0"
        lblCant6.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlEstado7
        '
        pnlEstado7.BackColor = Color.White
        pnlEstado7.Controls.Add(lblNom7)
        pnlEstado7.Controls.Add(lblCant7)
        pnlEstado7.Cursor = Cursors.Hand
        pnlEstado7.Dock = DockStyle.Fill
        pnlEstado7.Location = New Point(606, 0)
        pnlEstado7.Margin = New Padding(0, 0, 6, 0)
        pnlEstado7.Name = "pnlEstado7"
        pnlEstado7.Size = New Size(95, 76)
        pnlEstado7.TabIndex = 6
        '
        ' lblNom7
        '
        lblNom7.Dock = DockStyle.Fill
        lblNom7.Font = New Font("Segoe UI", 8.25F)
        lblNom7.ForeColor = Color.DimGray
        lblNom7.Location = New Point(0, 46)
        lblNom7.Name = "lblNom7"
        lblNom7.Size = New Size(95, 30)
        lblNom7.TabIndex = 1
        lblNom7.Text = "-"
        lblNom7.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCant7
        '
        lblCant7.Dock = DockStyle.Top
        lblCant7.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCant7.Location = New Point(0, 0)
        lblCant7.Name = "lblCant7"
        lblCant7.Size = New Size(95, 46)
        lblCant7.TabIndex = 0
        lblCant7.Text = "0"
        lblCant7.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlEstado8
        '
        pnlEstado8.BackColor = Color.White
        pnlEstado8.Controls.Add(lblNom8)
        pnlEstado8.Controls.Add(lblCant8)
        pnlEstado8.Cursor = Cursors.Hand
        pnlEstado8.Dock = DockStyle.Fill
        pnlEstado8.Location = New Point(707, 0)
        pnlEstado8.Margin = New Padding(0, 0, 0, 0)
        pnlEstado8.Name = "pnlEstado8"
        pnlEstado8.Size = New Size(95, 76)
        pnlEstado8.TabIndex = 7
        '
        ' lblNom8
        '
        lblNom8.Dock = DockStyle.Fill
        lblNom8.Font = New Font("Segoe UI", 8.25F)
        lblNom8.ForeColor = Color.DimGray
        lblNom8.Location = New Point(0, 46)
        lblNom8.Name = "lblNom8"
        lblNom8.Size = New Size(95, 30)
        lblNom8.TabIndex = 1
        lblNom8.Text = "-"
        lblNom8.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCant8
        '
        lblCant8.Dock = DockStyle.Top
        lblCant8.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCant8.Location = New Point(0, 0)
        lblCant8.Name = "lblCant8"
        lblCant8.Size = New Size(95, 46)
        lblCant8.TabIndex = 0
        lblCant8.Text = "0"
        lblCant8.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlFiltros
        '
        pnlFiltros.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        pnlFiltros.BackColor = Color.White
        pnlFiltros.Controls.Add(lblTexto)
        pnlFiltros.Controls.Add(txtTexto)
        pnlFiltros.Controls.Add(chkDemoradas)
        pnlFiltros.Controls.Add(btnLimpiar)
        pnlFiltros.Location = New Point(30, 178)
        pnlFiltros.Name = "pnlFiltros"
        pnlFiltros.Size = New Size(810, 50)
        pnlFiltros.TabIndex = 3
        '
        ' lblTexto
        '
        lblTexto.AutoSize = True
        lblTexto.Location = New Point(16, 15)
        lblTexto.Name = "lblTexto"
        lblTexto.Size = New Size(122, 20)
        lblTexto.TabIndex = 0
        lblTexto.Text = "Patente o cliente"
        '
        ' txtTexto
        '
        txtTexto.Location = New Point(150, 12)
        txtTexto.MaxLength = 150
        txtTexto.Name = "txtTexto"
        txtTexto.PlaceholderText = "Parte de la patente o del nombre"
        txtTexto.Size = New Size(270, 27)
        txtTexto.TabIndex = 1
        '
        ' chkDemoradas
        '
        chkDemoradas.AutoSize = True
        chkDemoradas.Location = New Point(440, 14)
        chkDemoradas.Name = "chkDemoradas"
        chkDemoradas.Size = New Size(135, 24)
        chkDemoradas.TabIndex = 2
        chkDemoradas.Text = "Solo demoradas"
        chkDemoradas.UseVisualStyleBackColor = True
        '
        ' btnLimpiar
        '
        btnLimpiar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnLimpiar.BackColor = Color.White
        btnLimpiar.Cursor = Cursors.Hand
        btnLimpiar.FlatAppearance.BorderColor = Color.Silver
        btnLimpiar.FlatStyle = FlatStyle.Flat
        btnLimpiar.Location = New Point(654, 8)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New Size(140, 34)
        btnLimpiar.TabIndex = 3
        btnLimpiar.Text = "Limpiar filtros"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        ' dgvOrdenes
        '
        dgvOrdenes.AllowUserToAddRows = False
        dgvOrdenes.AllowUserToDeleteRows = False
        dgvOrdenes.AllowUserToResizeRows = False
        dgvOrdenes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOrdenes.BackgroundColor = Color.White
        dgvOrdenes.BorderStyle = BorderStyle.FixedSingle
        dgvOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrdenes.Location = New Point(30, 236)
        dgvOrdenes.MultiSelect = False
        dgvOrdenes.Name = "dgvOrdenes"
        dgvOrdenes.ReadOnly = True
        dgvOrdenes.RowHeadersVisible = False
        dgvOrdenes.RowHeadersWidth = 51
        dgvOrdenes.RowTemplate.Height = 24
        dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrdenes.Size = New Size(420, 384)
        dgvOrdenes.TabIndex = 4
        '
        ' pnlDetalle
        '
        pnlDetalle.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pnlDetalle.BackColor = Color.White
        pnlDetalle.Controls.Add(pnlDatosOrden)
        pnlDetalle.Controls.Add(lblAyuda)
        pnlDetalle.Location = New Point(460, 236)
        pnlDetalle.Name = "pnlDetalle"
        pnlDetalle.Size = New Size(380, 384)
        pnlDetalle.TabIndex = 5
        '
        ' lblAyuda
        '
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(16, 16)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(348, 44)
        lblAyuda.TabIndex = 1
        lblAyuda.Text = "Seleccione una orden para ver su detalle."
        '
        ' pnlDatosOrden
        '
        pnlDatosOrden.Controls.Add(lblDetNumero)
        pnlDatosOrden.Controls.Add(lblDetEstado)
        pnlDatosOrden.Controls.Add(lblDetVehiculoTit)
        pnlDatosOrden.Controls.Add(lblDetVehiculo)
        pnlDatosOrden.Controls.Add(lblDetClienteTit)
        pnlDatosOrden.Controls.Add(lblDetCliente)
        pnlDatosOrden.Controls.Add(lblDetMecanicoTit)
        pnlDatosOrden.Controls.Add(lblDetMecanico)
        pnlDatosOrden.Controls.Add(lblDetRecepcionTit)
        pnlDatosOrden.Controls.Add(lblDetRecepcion)
        pnlDatosOrden.Controls.Add(lblDetPrometidaTit)
        pnlDatosOrden.Controls.Add(lblDetPrometida)
        pnlDatosOrden.Controls.Add(lblDetTotalTit)
        pnlDatosOrden.Controls.Add(lblDetTotal)
        pnlDatosOrden.Controls.Add(lblFotos)
        pnlDatosOrden.Controls.Add(lblSinFrente)
        pnlDatosOrden.Controls.Add(picFrente)
        pnlDatosOrden.Controls.Add(lblFrente)
        pnlDatosOrden.Controls.Add(lblSinTrasera)
        pnlDatosOrden.Controls.Add(picTrasera)
        pnlDatosOrden.Controls.Add(lblTrasera)
        pnlDatosOrden.Controls.Add(lblSinLateralIzq)
        pnlDatosOrden.Controls.Add(picLateralIzq)
        pnlDatosOrden.Controls.Add(lblLateralIzq)
        pnlDatosOrden.Controls.Add(lblSinLateralDer)
        pnlDatosOrden.Controls.Add(picLateralDer)
        pnlDatosOrden.Controls.Add(lblLateralDer)
        pnlDatosOrden.Controls.Add(lblSinTablero)
        pnlDatosOrden.Controls.Add(picTablero)
        pnlDatosOrden.Controls.Add(lblTablero)
        pnlDatosOrden.Controls.Add(btnGestionar)
        pnlDatosOrden.Dock = DockStyle.Fill
        pnlDatosOrden.Location = New Point(0, 0)
        pnlDatosOrden.Name = "pnlDatosOrden"
        pnlDatosOrden.Size = New Size(380, 384)
        pnlDatosOrden.TabIndex = 0
        pnlDatosOrden.Visible = False
        '
        ' lblDetNumero
        '
        lblDetNumero.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblDetNumero.Location = New Point(16, 10)
        lblDetNumero.Name = "lblDetNumero"
        lblDetNumero.Size = New Size(200, 30)
        lblDetNumero.TabIndex = 0
        lblDetNumero.Text = "Orden N.º"
        lblDetNumero.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblDetEstado
        '
        lblDetEstado.Location = New Point(224, 12)
        lblDetEstado.Name = "lblDetEstado"
        lblDetEstado.Size = New Size(140, 26)
        lblDetEstado.TabIndex = 1
        lblDetEstado.Text = "-"
        lblDetEstado.TextAlign = ContentAlignment.MiddleCenter
        '
        ' lblDetVehiculoTit
        '
        lblDetVehiculoTit.AutoSize = True
        lblDetVehiculoTit.ForeColor = Color.DimGray
        lblDetVehiculoTit.Location = New Point(16, 48)
        lblDetVehiculoTit.Name = "lblDetVehiculoTit"
        lblDetVehiculoTit.Size = New Size(80, 20)
        lblDetVehiculoTit.TabIndex = 2
        lblDetVehiculoTit.Text = "Vehículo"
        '
        ' lblDetVehiculo
        '
        lblDetVehiculo.AutoEllipsis = True
        lblDetVehiculo.Location = New Point(130, 48)
        lblDetVehiculo.Name = "lblDetVehiculo"
        lblDetVehiculo.Size = New Size(234, 22)
        lblDetVehiculo.TabIndex = 3
        lblDetVehiculo.Text = "-"
        '
        ' lblDetClienteTit
        '
        lblDetClienteTit.AutoSize = True
        lblDetClienteTit.ForeColor = Color.DimGray
        lblDetClienteTit.Location = New Point(16, 72)
        lblDetClienteTit.Name = "lblDetClienteTit"
        lblDetClienteTit.Size = New Size(80, 20)
        lblDetClienteTit.TabIndex = 4
        lblDetClienteTit.Text = "Cliente"
        '
        ' lblDetCliente
        '
        lblDetCliente.AutoEllipsis = True
        lblDetCliente.Location = New Point(130, 72)
        lblDetCliente.Name = "lblDetCliente"
        lblDetCliente.Size = New Size(234, 22)
        lblDetCliente.TabIndex = 5
        lblDetCliente.Text = "-"
        '
        ' lblDetMecanicoTit
        '
        lblDetMecanicoTit.AutoSize = True
        lblDetMecanicoTit.ForeColor = Color.DimGray
        lblDetMecanicoTit.Location = New Point(16, 96)
        lblDetMecanicoTit.Name = "lblDetMecanicoTit"
        lblDetMecanicoTit.Size = New Size(80, 20)
        lblDetMecanicoTit.TabIndex = 6
        lblDetMecanicoTit.Text = "Mecánico"
        '
        ' lblDetMecanico
        '
        lblDetMecanico.AutoEllipsis = True
        lblDetMecanico.Location = New Point(130, 96)
        lblDetMecanico.Name = "lblDetMecanico"
        lblDetMecanico.Size = New Size(234, 22)
        lblDetMecanico.TabIndex = 7
        lblDetMecanico.Text = "-"
        '
        ' lblDetRecepcionTit
        '
        lblDetRecepcionTit.AutoSize = True
        lblDetRecepcionTit.ForeColor = Color.DimGray
        lblDetRecepcionTit.Location = New Point(16, 120)
        lblDetRecepcionTit.Name = "lblDetRecepcionTit"
        lblDetRecepcionTit.Size = New Size(80, 20)
        lblDetRecepcionTit.TabIndex = 8
        lblDetRecepcionTit.Text = "Recepción"
        '
        ' lblDetRecepcion
        '
        lblDetRecepcion.AutoEllipsis = True
        lblDetRecepcion.Location = New Point(130, 120)
        lblDetRecepcion.Name = "lblDetRecepcion"
        lblDetRecepcion.Size = New Size(234, 22)
        lblDetRecepcion.TabIndex = 9
        lblDetRecepcion.Text = "-"
        '
        ' lblDetPrometidaTit
        '
        lblDetPrometidaTit.AutoSize = True
        lblDetPrometidaTit.ForeColor = Color.DimGray
        lblDetPrometidaTit.Location = New Point(16, 144)
        lblDetPrometidaTit.Name = "lblDetPrometidaTit"
        lblDetPrometidaTit.Size = New Size(80, 20)
        lblDetPrometidaTit.TabIndex = 10
        lblDetPrometidaTit.Text = "Prometida"
        '
        ' lblDetPrometida
        '
        lblDetPrometida.AutoEllipsis = True
        lblDetPrometida.Location = New Point(130, 144)
        lblDetPrometida.Name = "lblDetPrometida"
        lblDetPrometida.Size = New Size(234, 22)
        lblDetPrometida.TabIndex = 11
        lblDetPrometida.Text = "-"
        '
        ' lblDetTotalTit
        '
        lblDetTotalTit.AutoSize = True
        lblDetTotalTit.ForeColor = Color.DimGray
        lblDetTotalTit.Location = New Point(16, 168)
        lblDetTotalTit.Name = "lblDetTotalTit"
        lblDetTotalTit.Size = New Size(80, 20)
        lblDetTotalTit.TabIndex = 12
        lblDetTotalTit.Text = "Presupuestado"
        '
        ' lblDetTotal
        '
        lblDetTotal.AutoEllipsis = True
        lblDetTotal.Location = New Point(130, 168)
        lblDetTotal.Name = "lblDetTotal"
        lblDetTotal.Size = New Size(234, 22)
        lblDetTotal.TabIndex = 13
        lblDetTotal.Text = "-"
        '
        ' lblFotos
        '
        lblFotos.AutoSize = True
        lblFotos.ForeColor = Color.DimGray
        lblFotos.Location = New Point(16, 196)
        lblFotos.Name = "lblFotos"
        lblFotos.Size = New Size(138, 20)
        lblFotos.TabIndex = 14
        lblFotos.Text = "Fotos de recepción"
        '
        ' lblSinFrente
        '
        lblSinFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinFrente.Font = New Font("Segoe UI", 7F)
        lblSinFrente.ForeColor = Color.Gray
        lblSinFrente.Location = New Point(18, 236)
        lblSinFrente.Name = "lblSinFrente"
        lblSinFrente.Size = New Size(52, 20)
        lblSinFrente.TabIndex = 15
        lblSinFrente.Text = "Sin foto"
        lblSinFrente.TextAlign = ContentAlignment.MiddleCenter
        lblSinFrente.Visible = False
        '
        ' picFrente
        '
        picFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picFrente.BorderStyle = BorderStyle.FixedSingle
        picFrente.Location = New Point(16, 218)
        picFrente.Name = "picFrente"
        picFrente.Size = New Size(56, 56)
        picFrente.SizeMode = PictureBoxSizeMode.Zoom
        picFrente.TabIndex = 16
        picFrente.TabStop = False
        '
        ' lblFrente
        '
        lblFrente.Font = New Font("Segoe UI", 7.5F)
        lblFrente.ForeColor = Color.DimGray
        lblFrente.Location = New Point(8, 276)
        lblFrente.Name = "lblFrente"
        lblFrente.Size = New Size(72, 18)
        lblFrente.TabIndex = 17
        lblFrente.Text = "Frente"
        lblFrente.TextAlign = ContentAlignment.TopCenter
        '
        ' lblSinTrasera
        '
        lblSinTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinTrasera.Font = New Font("Segoe UI", 7F)
        lblSinTrasera.ForeColor = Color.Gray
        lblSinTrasera.Location = New Point(91, 236)
        lblSinTrasera.Name = "lblSinTrasera"
        lblSinTrasera.Size = New Size(52, 20)
        lblSinTrasera.TabIndex = 18
        lblSinTrasera.Text = "Sin foto"
        lblSinTrasera.TextAlign = ContentAlignment.MiddleCenter
        lblSinTrasera.Visible = False
        '
        ' picTrasera
        '
        picTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTrasera.BorderStyle = BorderStyle.FixedSingle
        picTrasera.Location = New Point(89, 218)
        picTrasera.Name = "picTrasera"
        picTrasera.Size = New Size(56, 56)
        picTrasera.SizeMode = PictureBoxSizeMode.Zoom
        picTrasera.TabIndex = 19
        picTrasera.TabStop = False
        '
        ' lblTrasera
        '
        lblTrasera.Font = New Font("Segoe UI", 7.5F)
        lblTrasera.ForeColor = Color.DimGray
        lblTrasera.Location = New Point(81, 276)
        lblTrasera.Name = "lblTrasera"
        lblTrasera.Size = New Size(72, 18)
        lblTrasera.TabIndex = 20
        lblTrasera.Text = "Trasera"
        lblTrasera.TextAlign = ContentAlignment.TopCenter
        '
        ' lblSinLateralIzq
        '
        lblSinLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinLateralIzq.Font = New Font("Segoe UI", 7F)
        lblSinLateralIzq.ForeColor = Color.Gray
        lblSinLateralIzq.Location = New Point(164, 236)
        lblSinLateralIzq.Name = "lblSinLateralIzq"
        lblSinLateralIzq.Size = New Size(52, 20)
        lblSinLateralIzq.TabIndex = 21
        lblSinLateralIzq.Text = "Sin foto"
        lblSinLateralIzq.TextAlign = ContentAlignment.MiddleCenter
        lblSinLateralIzq.Visible = False
        '
        ' picLateralIzq
        '
        picLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralIzq.BorderStyle = BorderStyle.FixedSingle
        picLateralIzq.Location = New Point(162, 218)
        picLateralIzq.Name = "picLateralIzq"
        picLateralIzq.Size = New Size(56, 56)
        picLateralIzq.SizeMode = PictureBoxSizeMode.Zoom
        picLateralIzq.TabIndex = 22
        picLateralIzq.TabStop = False
        '
        ' lblLateralIzq
        '
        lblLateralIzq.Font = New Font("Segoe UI", 7.5F)
        lblLateralIzq.ForeColor = Color.DimGray
        lblLateralIzq.Location = New Point(154, 276)
        lblLateralIzq.Name = "lblLateralIzq"
        lblLateralIzq.Size = New Size(72, 18)
        lblLateralIzq.TabIndex = 23
        lblLateralIzq.Text = "Lat. izq."
        lblLateralIzq.TextAlign = ContentAlignment.TopCenter
        '
        ' lblSinLateralDer
        '
        lblSinLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinLateralDer.Font = New Font("Segoe UI", 7F)
        lblSinLateralDer.ForeColor = Color.Gray
        lblSinLateralDer.Location = New Point(237, 236)
        lblSinLateralDer.Name = "lblSinLateralDer"
        lblSinLateralDer.Size = New Size(52, 20)
        lblSinLateralDer.TabIndex = 24
        lblSinLateralDer.Text = "Sin foto"
        lblSinLateralDer.TextAlign = ContentAlignment.MiddleCenter
        lblSinLateralDer.Visible = False
        '
        ' picLateralDer
        '
        picLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralDer.BorderStyle = BorderStyle.FixedSingle
        picLateralDer.Location = New Point(235, 218)
        picLateralDer.Name = "picLateralDer"
        picLateralDer.Size = New Size(56, 56)
        picLateralDer.SizeMode = PictureBoxSizeMode.Zoom
        picLateralDer.TabIndex = 25
        picLateralDer.TabStop = False
        '
        ' lblLateralDer
        '
        lblLateralDer.Font = New Font("Segoe UI", 7.5F)
        lblLateralDer.ForeColor = Color.DimGray
        lblLateralDer.Location = New Point(227, 276)
        lblLateralDer.Name = "lblLateralDer"
        lblLateralDer.Size = New Size(72, 18)
        lblLateralDer.TabIndex = 26
        lblLateralDer.Text = "Lat. der."
        lblLateralDer.TextAlign = ContentAlignment.TopCenter
        '
        ' lblSinTablero
        '
        lblSinTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinTablero.Font = New Font("Segoe UI", 7F)
        lblSinTablero.ForeColor = Color.Gray
        lblSinTablero.Location = New Point(310, 236)
        lblSinTablero.Name = "lblSinTablero"
        lblSinTablero.Size = New Size(52, 20)
        lblSinTablero.TabIndex = 27
        lblSinTablero.Text = "Sin foto"
        lblSinTablero.TextAlign = ContentAlignment.MiddleCenter
        lblSinTablero.Visible = False
        '
        ' picTablero
        '
        picTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTablero.BorderStyle = BorderStyle.FixedSingle
        picTablero.Location = New Point(308, 218)
        picTablero.Name = "picTablero"
        picTablero.Size = New Size(56, 56)
        picTablero.SizeMode = PictureBoxSizeMode.Zoom
        picTablero.TabIndex = 28
        picTablero.TabStop = False
        '
        ' lblTablero
        '
        lblTablero.Font = New Font("Segoe UI", 7.5F)
        lblTablero.ForeColor = Color.DimGray
        lblTablero.Location = New Point(300, 276)
        lblTablero.Name = "lblTablero"
        lblTablero.Size = New Size(72, 18)
        lblTablero.TabIndex = 29
        lblTablero.Text = "Tablero"
        lblTablero.TextAlign = ContentAlignment.TopCenter
        '
        ' btnGestionar
        '
        btnGestionar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnGestionar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGestionar.Cursor = Cursors.Hand
        btnGestionar.FlatAppearance.BorderSize = 0
        btnGestionar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGestionar.FlatStyle = FlatStyle.Flat
        btnGestionar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGestionar.ForeColor = Color.White
        btnGestionar.Location = New Point(16, 330)
        btnGestionar.Name = "btnGestionar"
        btnGestionar.Size = New Size(348, 40)
        btnGestionar.TabIndex = 30
        btnGestionar.Text = "Gestionar orden"
        btnGestionar.UseVisualStyleBackColor = False
        '
        ' FrmOrdenes
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(pnlDetalle)
        Controls.Add(dgvOrdenes)
        Controls.Add(pnlFiltros)
        Controls.Add(tlpEstados)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmOrdenes"
        Text = "Órdenes de trabajo"
        tlpEstados.ResumeLayout(False)
        pnlEstado1.ResumeLayout(False)
        pnlEstado2.ResumeLayout(False)
        pnlEstado3.ResumeLayout(False)
        pnlEstado4.ResumeLayout(False)
        pnlEstado5.ResumeLayout(False)
        pnlEstado6.ResumeLayout(False)
        pnlEstado7.ResumeLayout(False)
        pnlEstado8.ResumeLayout(False)
        pnlFiltros.ResumeLayout(False)
        pnlFiltros.PerformLayout()
        pnlDetalle.ResumeLayout(False)
        pnlDatosOrden.ResumeLayout(False)
        pnlDatosOrden.PerformLayout()
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).EndInit()
        CType(picFrente, ComponentModel.ISupportInitialize).EndInit()
        CType(picTrasera, ComponentModel.ISupportInitialize).EndInit()
        CType(picLateralIzq, ComponentModel.ISupportInitialize).EndInit()
        CType(picLateralDer, ComponentModel.ISupportInitialize).EndInit()
        CType(picTablero, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents tlpEstados As TableLayoutPanel
    Friend WithEvents pnlEstado1 As Panel
    Friend WithEvents lblNom1 As Label
    Friend WithEvents lblCant1 As Label
    Friend WithEvents pnlEstado2 As Panel
    Friend WithEvents lblNom2 As Label
    Friend WithEvents lblCant2 As Label
    Friend WithEvents pnlEstado3 As Panel
    Friend WithEvents lblNom3 As Label
    Friend WithEvents lblCant3 As Label
    Friend WithEvents pnlEstado4 As Panel
    Friend WithEvents lblNom4 As Label
    Friend WithEvents lblCant4 As Label
    Friend WithEvents pnlEstado5 As Panel
    Friend WithEvents lblNom5 As Label
    Friend WithEvents lblCant5 As Label
    Friend WithEvents pnlEstado6 As Panel
    Friend WithEvents lblNom6 As Label
    Friend WithEvents lblCant6 As Label
    Friend WithEvents pnlEstado7 As Panel
    Friend WithEvents lblNom7 As Label
    Friend WithEvents lblCant7 As Label
    Friend WithEvents pnlEstado8 As Panel
    Friend WithEvents lblNom8 As Label
    Friend WithEvents lblCant8 As Label
    Friend WithEvents pnlFiltros As Panel
    Friend WithEvents lblTexto As Label
    Friend WithEvents txtTexto As TextBox
    Friend WithEvents chkDemoradas As CheckBox
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents dgvOrdenes As DataGridView
    Friend WithEvents pnlDetalle As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents pnlDatosOrden As Panel
    Friend WithEvents lblDetNumero As Label
    Friend WithEvents lblDetEstado As Label
    Friend WithEvents lblDetVehiculoTit As Label
    Friend WithEvents lblDetVehiculo As Label
    Friend WithEvents lblDetClienteTit As Label
    Friend WithEvents lblDetCliente As Label
    Friend WithEvents lblDetMecanicoTit As Label
    Friend WithEvents lblDetMecanico As Label
    Friend WithEvents lblDetRecepcionTit As Label
    Friend WithEvents lblDetRecepcion As Label
    Friend WithEvents lblDetPrometidaTit As Label
    Friend WithEvents lblDetPrometida As Label
    Friend WithEvents lblDetTotalTit As Label
    Friend WithEvents lblDetTotal As Label
    Friend WithEvents lblFotos As Label
    Friend WithEvents lblSinFrente As Label
    Friend WithEvents picFrente As PictureBox
    Friend WithEvents lblFrente As Label
    Friend WithEvents lblSinTrasera As Label
    Friend WithEvents picTrasera As PictureBox
    Friend WithEvents lblTrasera As Label
    Friend WithEvents lblSinLateralIzq As Label
    Friend WithEvents picLateralIzq As PictureBox
    Friend WithEvents lblLateralIzq As Label
    Friend WithEvents lblSinLateralDer As Label
    Friend WithEvents picLateralDer As PictureBox
    Friend WithEvents lblLateralDer As Label
    Friend WithEvents lblSinTablero As Label
    Friend WithEvents picTablero As PictureBox
    Friend WithEvents lblTablero As Label
    Friend WithEvents btnGestionar As Button
End Class

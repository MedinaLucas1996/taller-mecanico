<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOrdenGestion
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
        lblNroOrdenTit = New Label()
        lblNroOrden = New Label()
        lblEstadoTit = New Label()
        lblEstado = New Label()
        lblKmIngresoTit = New Label()
        lblKmIngreso = New Label()
        lblVehiculoTit = New Label()
        lblVehiculo = New Label()
        lblClienteTit = New Label()
        lblCliente = New Label()
        lblFechaRecepcionTit = New Label()
        lblFechaRecepcion = New Label()
        lblFechaPrometidaTit = New Label()
        lblFechaPrometida = New Label()
        lblFechaFinalizacionTit = New Label()
        lblFechaFinalizacion = New Label()
        lblFechaEntregaTit = New Label()
        lblFechaEntrega = New Label()
        lblSintoma = New Label()
        txtSintoma = New TextBox()
        lblObsRecepcion = New Label()
        txtObsRecepcion = New TextBox()
        pnlDetalle = New Panel()
        lblDetalle = New Label()
        lblServicio = New Label()
        cboServicio = New ComboBox()
        lblCantidad = New Label()
        nudCantidad = New NumericUpDown()
        btnAgregar = New Button()
        dgvDetalle = New DataGridView()
        btnActualizar = New Button()
        btnQuitar = New Button()
        lblTotalPresupuestado = New Label()
        lblTotalAprobado = New Label()
        lblCantidadReal = New Label()
        nudCantidadReal = New NumericUpDown()
        lblHorasReales = New Label()
        nudHorasReales = New NumericUpDown()
        btnGuardarEjecucion = New Button()
        pnlMecanico = New Panel()
        lblMecanico = New Label()
        cboMecanico = New ComboBox()
        btnGuardarMecanico = New Button()
        lblNotasTecnicas = New Label()
        txtNotasTecnicas = New TextBox()
        pnlAcciones = New Panel()
        btnPresupuestar = New Button()
        btnAprobar = New Button()
        btnRechazar = New Button()
        btnIniciar = New Button()
        btnFinalizar = New Button()
        btnEntregar = New Button()
        txtMotivoAnulacion = New TextBox()
        btnAnular = New Button()
        lblHistorial = New Label()
        dgvHistorial = New DataGridView()
        pnlDatos.SuspendLayout()
        pnlDetalle.SuspendLayout()
        CType(nudCantidad, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvDetalle, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudCantidadReal, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudHorasReales, ComponentModel.ISupportInitialize).BeginInit()
        pnlMecanico.SuspendLayout()
        pnlAcciones.SuspendLayout()
        CType(dgvHistorial, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 6)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(390, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Gestión de la orden"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 56)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(480, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Presupuesto, aprobación, ejecución y entrega de la orden de trabajo"
        '
        ' pnlDatos
        '
        pnlDatos.BackColor = Color.White
        pnlDatos.Controls.Add(lblNroOrdenTit)
        pnlDatos.Controls.Add(lblNroOrden)
        pnlDatos.Controls.Add(lblEstadoTit)
        pnlDatos.Controls.Add(lblEstado)
        pnlDatos.Controls.Add(lblKmIngresoTit)
        pnlDatos.Controls.Add(lblKmIngreso)
        pnlDatos.Controls.Add(lblVehiculoTit)
        pnlDatos.Controls.Add(lblVehiculo)
        pnlDatos.Controls.Add(lblClienteTit)
        pnlDatos.Controls.Add(lblCliente)
        pnlDatos.Controls.Add(lblFechaRecepcionTit)
        pnlDatos.Controls.Add(lblFechaRecepcion)
        pnlDatos.Controls.Add(lblFechaPrometidaTit)
        pnlDatos.Controls.Add(lblFechaPrometida)
        pnlDatos.Controls.Add(lblFechaFinalizacionTit)
        pnlDatos.Controls.Add(lblFechaFinalizacion)
        pnlDatos.Controls.Add(lblFechaEntregaTit)
        pnlDatos.Controls.Add(lblFechaEntrega)
        pnlDatos.Controls.Add(lblSintoma)
        pnlDatos.Controls.Add(txtSintoma)
        pnlDatos.Controls.Add(lblObsRecepcion)
        pnlDatos.Controls.Add(txtObsRecepcion)
        pnlDatos.Location = New Point(30, 86)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(1004, 122)
        pnlDatos.TabIndex = 2
        '
        ' lblNroOrdenTit
        '
        lblNroOrdenTit.AutoSize = True
        lblNroOrdenTit.ForeColor = Color.DimGray
        lblNroOrdenTit.Location = New Point(16, 8)
        lblNroOrdenTit.Name = "lblNroOrdenTit"
        lblNroOrdenTit.Size = New Size(50, 20)
        lblNroOrdenTit.TabIndex = 0
        lblNroOrdenTit.Text = "Orden"
        '
        ' lblNroOrden
        '
        lblNroOrden.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblNroOrden.Location = New Point(100, 8)
        lblNroOrden.Name = "lblNroOrden"
        lblNroOrden.Size = New Size(90, 20)
        lblNroOrden.TabIndex = 1
        lblNroOrden.Text = "-"
        '
        ' lblEstadoTit
        '
        lblEstadoTit.AutoSize = True
        lblEstadoTit.ForeColor = Color.DimGray
        lblEstadoTit.Location = New Point(200, 8)
        lblEstadoTit.Name = "lblEstadoTit"
        lblEstadoTit.Size = New Size(54, 20)
        lblEstadoTit.TabIndex = 2
        lblEstadoTit.Text = "Estado"
        '
        ' lblEstado
        '
        lblEstado.AutoEllipsis = True
        lblEstado.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblEstado.Location = New Point(260, 8)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(170, 20)
        lblEstado.TabIndex = 3
        lblEstado.Text = "-"
        '
        ' lblKmIngresoTit
        '
        lblKmIngresoTit.AutoSize = True
        lblKmIngresoTit.ForeColor = Color.DimGray
        lblKmIngresoTit.Location = New Point(436, 8)
        lblKmIngresoTit.Name = "lblKmIngresoTit"
        lblKmIngresoTit.Size = New Size(82, 20)
        lblKmIngresoTit.TabIndex = 4
        lblKmIngresoTit.Text = "Km ingreso"
        '
        ' lblKmIngreso
        '
        lblKmIngreso.Location = New Point(522, 8)
        lblKmIngreso.Name = "lblKmIngreso"
        lblKmIngreso.Size = New Size(70, 20)
        lblKmIngreso.TabIndex = 5
        lblKmIngreso.Text = "-"
        '
        ' lblVehiculoTit
        '
        lblVehiculoTit.AutoSize = True
        lblVehiculoTit.ForeColor = Color.DimGray
        lblVehiculoTit.Location = New Point(16, 30)
        lblVehiculoTit.Name = "lblVehiculoTit"
        lblVehiculoTit.Size = New Size(65, 20)
        lblVehiculoTit.TabIndex = 6
        lblVehiculoTit.Text = "Vehículo"
        '
        ' lblVehiculo
        '
        lblVehiculo.AutoEllipsis = True
        lblVehiculo.Location = New Point(100, 30)
        lblVehiculo.Name = "lblVehiculo"
        lblVehiculo.Size = New Size(490, 20)
        lblVehiculo.TabIndex = 7
        lblVehiculo.Text = "-"
        '
        ' lblClienteTit
        '
        lblClienteTit.AutoSize = True
        lblClienteTit.ForeColor = Color.DimGray
        lblClienteTit.Location = New Point(16, 52)
        lblClienteTit.Name = "lblClienteTit"
        lblClienteTit.Size = New Size(55, 20)
        lblClienteTit.TabIndex = 8
        lblClienteTit.Text = "Cliente"
        '
        ' lblCliente
        '
        lblCliente.AutoEllipsis = True
        lblCliente.Location = New Point(100, 52)
        lblCliente.Name = "lblCliente"
        lblCliente.Size = New Size(490, 20)
        lblCliente.TabIndex = 9
        lblCliente.Text = "-"
        '
        ' lblFechaRecepcionTit
        '
        lblFechaRecepcionTit.AutoSize = True
        lblFechaRecepcionTit.ForeColor = Color.DimGray
        lblFechaRecepcionTit.Location = New Point(16, 74)
        lblFechaRecepcionTit.Name = "lblFechaRecepcionTit"
        lblFechaRecepcionTit.Size = New Size(76, 20)
        lblFechaRecepcionTit.TabIndex = 10
        lblFechaRecepcionTit.Text = "Recepción"
        '
        ' lblFechaRecepcion
        '
        lblFechaRecepcion.Location = New Point(100, 74)
        lblFechaRecepcion.Name = "lblFechaRecepcion"
        lblFechaRecepcion.Size = New Size(150, 20)
        lblFechaRecepcion.TabIndex = 11
        lblFechaRecepcion.Text = "-"
        '
        ' lblFechaPrometidaTit
        '
        lblFechaPrometidaTit.AutoSize = True
        lblFechaPrometidaTit.ForeColor = Color.DimGray
        lblFechaPrometidaTit.Location = New Point(260, 74)
        lblFechaPrometidaTit.Name = "lblFechaPrometidaTit"
        lblFechaPrometidaTit.Size = New Size(77, 20)
        lblFechaPrometidaTit.TabIndex = 12
        lblFechaPrometidaTit.Text = "Prometida"
        '
        ' lblFechaPrometida
        '
        lblFechaPrometida.Location = New Point(345, 74)
        lblFechaPrometida.Name = "lblFechaPrometida"
        lblFechaPrometida.Size = New Size(120, 20)
        lblFechaPrometida.TabIndex = 13
        lblFechaPrometida.Text = "-"
        '
        ' lblFechaFinalizacionTit
        '
        lblFechaFinalizacionTit.AutoSize = True
        lblFechaFinalizacionTit.ForeColor = Color.DimGray
        lblFechaFinalizacionTit.Location = New Point(16, 96)
        lblFechaFinalizacionTit.Name = "lblFechaFinalizacionTit"
        lblFechaFinalizacionTit.Size = New Size(88, 20)
        lblFechaFinalizacionTit.TabIndex = 14
        lblFechaFinalizacionTit.Text = "Finalización"
        '
        ' lblFechaFinalizacion
        '
        lblFechaFinalizacion.Location = New Point(108, 96)
        lblFechaFinalizacion.Name = "lblFechaFinalizacion"
        lblFechaFinalizacion.Size = New Size(142, 20)
        lblFechaFinalizacion.TabIndex = 15
        lblFechaFinalizacion.Text = "-"
        '
        ' lblFechaEntregaTit
        '
        lblFechaEntregaTit.AutoSize = True
        lblFechaEntregaTit.ForeColor = Color.DimGray
        lblFechaEntregaTit.Location = New Point(260, 96)
        lblFechaEntregaTit.Name = "lblFechaEntregaTit"
        lblFechaEntregaTit.Size = New Size(60, 20)
        lblFechaEntregaTit.TabIndex = 16
        lblFechaEntregaTit.Text = "Entrega"
        '
        ' lblFechaEntrega
        '
        lblFechaEntrega.Location = New Point(345, 96)
        lblFechaEntrega.Name = "lblFechaEntrega"
        lblFechaEntrega.Size = New Size(150, 20)
        lblFechaEntrega.TabIndex = 17
        lblFechaEntrega.Text = "-"
        '
        ' lblSintoma
        '
        lblSintoma.AutoSize = True
        lblSintoma.ForeColor = Color.DimGray
        lblSintoma.Location = New Point(600, 6)
        lblSintoma.Name = "lblSintoma"
        lblSintoma.Size = New Size(132, 20)
        lblSintoma.TabIndex = 18
        lblSintoma.Text = "Síntoma reportado"
        '
        ' txtSintoma
        '
        txtSintoma.BackColor = Color.White
        txtSintoma.Location = New Point(600, 28)
        txtSintoma.Multiline = True
        txtSintoma.Name = "txtSintoma"
        txtSintoma.ReadOnly = True
        txtSintoma.ScrollBars = ScrollBars.Vertical
        txtSintoma.Size = New Size(190, 86)
        txtSintoma.TabIndex = 19
        txtSintoma.TabStop = False
        '
        ' lblObsRecepcion
        '
        lblObsRecepcion.AutoSize = True
        lblObsRecepcion.ForeColor = Color.DimGray
        lblObsRecepcion.Location = New Point(800, 6)
        lblObsRecepcion.Name = "lblObsRecepcion"
        lblObsRecepcion.Size = New Size(195, 20)
        lblObsRecepcion.TabIndex = 20
        lblObsRecepcion.Text = "Observaciones de recepción"
        '
        ' txtObsRecepcion
        '
        txtObsRecepcion.BackColor = Color.White
        txtObsRecepcion.Location = New Point(800, 28)
        txtObsRecepcion.Multiline = True
        txtObsRecepcion.Name = "txtObsRecepcion"
        txtObsRecepcion.ReadOnly = True
        txtObsRecepcion.ScrollBars = ScrollBars.Vertical
        txtObsRecepcion.Size = New Size(190, 86)
        txtObsRecepcion.TabIndex = 21
        txtObsRecepcion.TabStop = False
        '
        ' pnlDetalle
        '
        pnlDetalle.BackColor = Color.White
        pnlDetalle.Controls.Add(lblDetalle)
        pnlDetalle.Controls.Add(lblServicio)
        pnlDetalle.Controls.Add(cboServicio)
        pnlDetalle.Controls.Add(lblCantidad)
        pnlDetalle.Controls.Add(nudCantidad)
        pnlDetalle.Controls.Add(btnAgregar)
        pnlDetalle.Controls.Add(dgvDetalle)
        pnlDetalle.Controls.Add(btnActualizar)
        pnlDetalle.Controls.Add(btnQuitar)
        pnlDetalle.Controls.Add(lblTotalPresupuestado)
        pnlDetalle.Controls.Add(lblTotalAprobado)
        pnlDetalle.Controls.Add(lblCantidadReal)
        pnlDetalle.Controls.Add(nudCantidadReal)
        pnlDetalle.Controls.Add(lblHorasReales)
        pnlDetalle.Controls.Add(nudHorasReales)
        pnlDetalle.Controls.Add(btnGuardarEjecucion)
        pnlDetalle.Location = New Point(30, 216)
        pnlDetalle.Name = "pnlDetalle"
        pnlDetalle.Size = New Size(730, 254)
        pnlDetalle.TabIndex = 3
        '
        ' lblDetalle
        '
        lblDetalle.AutoSize = True
        lblDetalle.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblDetalle.Location = New Point(16, 6)
        lblDetalle.Name = "lblDetalle"
        lblDetalle.Size = New Size(110, 23)
        lblDetalle.TabIndex = 0
        lblDetalle.Text = "Presupuesto"
        '
        ' lblServicio
        '
        lblServicio.AutoSize = True
        lblServicio.Location = New Point(16, 39)
        lblServicio.Name = "lblServicio"
        lblServicio.Size = New Size(61, 20)
        lblServicio.TabIndex = 1
        lblServicio.Text = "Servicio"
        '
        ' cboServicio
        '
        cboServicio.DropDownStyle = ComboBoxStyle.DropDownList
        cboServicio.FormattingEnabled = True
        cboServicio.Location = New Point(84, 35)
        cboServicio.Name = "cboServicio"
        cboServicio.Size = New Size(372, 28)
        cboServicio.TabIndex = 2
        '
        ' lblCantidad
        '
        lblCantidad.AutoSize = True
        lblCantidad.Location = New Point(466, 39)
        lblCantidad.Name = "lblCantidad"
        lblCantidad.Size = New Size(69, 20)
        lblCantidad.TabIndex = 3
        lblCantidad.Text = "Cantidad"
        '
        ' nudCantidad
        '
        nudCantidad.DecimalPlaces = 2
        nudCantidad.Location = New Point(540, 36)
        nudCantidad.Maximum = New Decimal(New Integer() {999999999, 0, 0, 131072})
        nudCantidad.Name = "nudCantidad"
        nudCantidad.Size = New Size(84, 27)
        nudCantidad.TabIndex = 4
        nudCantidad.TextAlign = HorizontalAlignment.Right
        nudCantidad.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        ' btnAgregar
        '
        btnAgregar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnAgregar.Cursor = Cursors.Hand
        btnAgregar.FlatAppearance.BorderSize = 0
        btnAgregar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnAgregar.FlatStyle = FlatStyle.Flat
        btnAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregar.ForeColor = Color.White
        btnAgregar.Location = New Point(632, 34)
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Size = New Size(82, 30)
        btnAgregar.TabIndex = 5
        btnAgregar.Text = "Agregar"
        btnAgregar.UseVisualStyleBackColor = False
        '
        ' dgvDetalle
        '
        dgvDetalle.AllowUserToAddRows = False
        dgvDetalle.AllowUserToDeleteRows = False
        dgvDetalle.AllowUserToResizeRows = False
        dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDetalle.BackgroundColor = Color.White
        dgvDetalle.BorderStyle = BorderStyle.FixedSingle
        dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDetalle.Location = New Point(16, 70)
        dgvDetalle.MultiSelect = False
        dgvDetalle.Name = "dgvDetalle"
        dgvDetalle.ReadOnly = True
        dgvDetalle.RowHeadersVisible = False
        dgvDetalle.RowHeadersWidth = 51
        dgvDetalle.RowTemplate.Height = 22
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDetalle.Size = New Size(698, 100)
        dgvDetalle.TabIndex = 6
        '
        ' btnActualizar
        '
        btnActualizar.BackColor = Color.White
        btnActualizar.Cursor = Cursors.Hand
        btnActualizar.FlatAppearance.BorderColor = Color.Silver
        btnActualizar.FlatStyle = FlatStyle.Flat
        btnActualizar.Location = New Point(16, 178)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Size = New Size(170, 30)
        btnActualizar.TabIndex = 7
        btnActualizar.Text = "Actualizar cantidad"
        btnActualizar.UseVisualStyleBackColor = False
        '
        ' btnQuitar
        '
        btnQuitar.BackColor = Color.White
        btnQuitar.Cursor = Cursors.Hand
        btnQuitar.FlatAppearance.BorderColor = Color.Silver
        btnQuitar.FlatStyle = FlatStyle.Flat
        btnQuitar.Location = New Point(196, 178)
        btnQuitar.Name = "btnQuitar"
        btnQuitar.Size = New Size(90, 30)
        btnQuitar.TabIndex = 8
        btnQuitar.Text = "Quitar"
        btnQuitar.UseVisualStyleBackColor = False
        '
        ' lblTotalPresupuestado
        '
        lblTotalPresupuestado.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTotalPresupuestado.Location = New Point(350, 174)
        lblTotalPresupuestado.Name = "lblTotalPresupuestado"
        lblTotalPresupuestado.Size = New Size(364, 20)
        lblTotalPresupuestado.TabIndex = 9
        lblTotalPresupuestado.Text = "Total presupuestado: -"
        lblTotalPresupuestado.TextAlign = ContentAlignment.TopRight
        '
        ' lblTotalAprobado
        '
        lblTotalAprobado.ForeColor = Color.DimGray
        lblTotalAprobado.Location = New Point(350, 196)
        lblTotalAprobado.Name = "lblTotalAprobado"
        lblTotalAprobado.Size = New Size(364, 20)
        lblTotalAprobado.TabIndex = 10
        lblTotalAprobado.Text = "Total aprobado: -"
        lblTotalAprobado.TextAlign = ContentAlignment.TopRight
        '
        ' lblCantidadReal
        '
        lblCantidadReal.AutoSize = True
        lblCantidadReal.Location = New Point(16, 224)
        lblCantidadReal.Name = "lblCantidadReal"
        lblCantidadReal.Size = New Size(99, 20)
        lblCantidadReal.TabIndex = 11
        lblCantidadReal.Text = "Cantidad real"
        '
        ' nudCantidadReal
        '
        nudCantidadReal.DecimalPlaces = 2
        nudCantidadReal.Location = New Point(120, 221)
        nudCantidadReal.Maximum = New Decimal(New Integer() {999999999, 0, 0, 131072})
        nudCantidadReal.Name = "nudCantidadReal"
        nudCantidadReal.Size = New Size(90, 27)
        nudCantidadReal.TabIndex = 12
        nudCantidadReal.TextAlign = HorizontalAlignment.Right
        '
        ' lblHorasReales
        '
        lblHorasReales.AutoSize = True
        lblHorasReales.Location = New Point(226, 224)
        lblHorasReales.Name = "lblHorasReales"
        lblHorasReales.Size = New Size(92, 20)
        lblHorasReales.TabIndex = 13
        lblHorasReales.Text = "Horas reales"
        '
        ' nudHorasReales
        '
        nudHorasReales.DecimalPlaces = 2
        nudHorasReales.Location = New Point(324, 221)
        nudHorasReales.Maximum = New Decimal(New Integer() {99999, 0, 0, 131072})
        nudHorasReales.Name = "nudHorasReales"
        nudHorasReales.Size = New Size(80, 27)
        nudHorasReales.TabIndex = 14
        nudHorasReales.TextAlign = HorizontalAlignment.Right
        '
        ' btnGuardarEjecucion
        '
        btnGuardarEjecucion.BackColor = Color.White
        btnGuardarEjecucion.Cursor = Cursors.Hand
        btnGuardarEjecucion.FlatAppearance.BorderColor = Color.Silver
        btnGuardarEjecucion.FlatStyle = FlatStyle.Flat
        btnGuardarEjecucion.Location = New Point(416, 219)
        btnGuardarEjecucion.Name = "btnGuardarEjecucion"
        btnGuardarEjecucion.Size = New Size(160, 30)
        btnGuardarEjecucion.TabIndex = 15
        btnGuardarEjecucion.Text = "Guardar ejecución"
        btnGuardarEjecucion.UseVisualStyleBackColor = False
        '
        ' pnlMecanico
        '
        pnlMecanico.BackColor = Color.White
        pnlMecanico.Controls.Add(lblMecanico)
        pnlMecanico.Controls.Add(cboMecanico)
        pnlMecanico.Controls.Add(btnGuardarMecanico)
        pnlMecanico.Controls.Add(lblNotasTecnicas)
        pnlMecanico.Controls.Add(txtNotasTecnicas)
        pnlMecanico.Location = New Point(770, 216)
        pnlMecanico.Name = "pnlMecanico"
        pnlMecanico.Size = New Size(264, 254)
        pnlMecanico.TabIndex = 4
        '
        ' lblMecanico
        '
        lblMecanico.AutoSize = True
        lblMecanico.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblMecanico.Location = New Point(16, 6)
        lblMecanico.Name = "lblMecanico"
        lblMecanico.Size = New Size(164, 23)
        lblMecanico.TabIndex = 0
        lblMecanico.Text = "Mecánico asignado"
        '
        ' cboMecanico
        '
        cboMecanico.DropDownStyle = ComboBoxStyle.DropDownList
        cboMecanico.FormattingEnabled = True
        cboMecanico.Location = New Point(16, 35)
        cboMecanico.Name = "cboMecanico"
        cboMecanico.Size = New Size(232, 28)
        cboMecanico.TabIndex = 1
        '
        ' btnGuardarMecanico
        '
        btnGuardarMecanico.BackColor = Color.White
        btnGuardarMecanico.Cursor = Cursors.Hand
        btnGuardarMecanico.FlatAppearance.BorderColor = Color.Silver
        btnGuardarMecanico.FlatStyle = FlatStyle.Flat
        btnGuardarMecanico.Location = New Point(16, 72)
        btnGuardarMecanico.Name = "btnGuardarMecanico"
        btnGuardarMecanico.Size = New Size(160, 30)
        btnGuardarMecanico.TabIndex = 2
        btnGuardarMecanico.Text = "Guardar mecánico"
        btnGuardarMecanico.UseVisualStyleBackColor = False
        '
        ' lblNotasTecnicas
        '
        lblNotasTecnicas.AutoSize = True
        lblNotasTecnicas.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblNotasTecnicas.Location = New Point(16, 110)
        lblNotasTecnicas.Name = "lblNotasTecnicas"
        lblNotasTecnicas.Size = New Size(236, 23)
        lblNotasTecnicas.TabIndex = 3
        lblNotasTecnicas.Text = "Observaciones del mecánico"
        '
        ' txtNotasTecnicas
        '
        txtNotasTecnicas.Location = New Point(16, 138)
        txtNotasTecnicas.MaxLength = 2000
        txtNotasTecnicas.Multiline = True
        txtNotasTecnicas.Name = "txtNotasTecnicas"
        txtNotasTecnicas.ReadOnly = True
        txtNotasTecnicas.ScrollBars = ScrollBars.Vertical
        txtNotasTecnicas.Size = New Size(232, 106)
        txtNotasTecnicas.TabIndex = 4
        '
        ' pnlAcciones
        '
        pnlAcciones.BackColor = Color.White
        pnlAcciones.Controls.Add(btnPresupuestar)
        pnlAcciones.Controls.Add(btnAprobar)
        pnlAcciones.Controls.Add(btnRechazar)
        pnlAcciones.Controls.Add(btnIniciar)
        pnlAcciones.Controls.Add(btnFinalizar)
        pnlAcciones.Controls.Add(btnEntregar)
        pnlAcciones.Controls.Add(txtMotivoAnulacion)
        pnlAcciones.Controls.Add(btnAnular)
        pnlAcciones.Location = New Point(30, 478)
        pnlAcciones.Name = "pnlAcciones"
        pnlAcciones.Size = New Size(1004, 44)
        pnlAcciones.TabIndex = 5
        '
        ' btnPresupuestar
        '
        btnPresupuestar.BackColor = Color.White
        btnPresupuestar.Cursor = Cursors.Hand
        btnPresupuestar.FlatAppearance.BorderColor = Color.Silver
        btnPresupuestar.FlatStyle = FlatStyle.Flat
        btnPresupuestar.Location = New Point(16, 7)
        btnPresupuestar.Name = "btnPresupuestar"
        btnPresupuestar.Size = New Size(110, 30)
        btnPresupuestar.TabIndex = 0
        btnPresupuestar.Text = "Presupuestar"
        btnPresupuestar.UseVisualStyleBackColor = False
        '
        ' btnAprobar
        '
        btnAprobar.BackColor = Color.White
        btnAprobar.Cursor = Cursors.Hand
        btnAprobar.FlatAppearance.BorderColor = Color.Silver
        btnAprobar.FlatStyle = FlatStyle.Flat
        btnAprobar.Location = New Point(132, 7)
        btnAprobar.Name = "btnAprobar"
        btnAprobar.Size = New Size(160, 30)
        btnAprobar.TabIndex = 1
        btnAprobar.Text = "Registrar aprobación"
        btnAprobar.UseVisualStyleBackColor = False
        '
        ' btnRechazar
        '
        btnRechazar.BackColor = Color.White
        btnRechazar.Cursor = Cursors.Hand
        btnRechazar.FlatAppearance.BorderColor = Color.Silver
        btnRechazar.FlatStyle = FlatStyle.Flat
        btnRechazar.Location = New Point(298, 7)
        btnRechazar.Name = "btnRechazar"
        btnRechazar.Size = New Size(80, 30)
        btnRechazar.TabIndex = 2
        btnRechazar.Text = "Rechazar"
        btnRechazar.UseVisualStyleBackColor = False
        '
        ' btnIniciar
        '
        btnIniciar.BackColor = Color.White
        btnIniciar.Cursor = Cursors.Hand
        btnIniciar.FlatAppearance.BorderColor = Color.Silver
        btnIniciar.FlatStyle = FlatStyle.Flat
        btnIniciar.Location = New Point(384, 7)
        btnIniciar.Name = "btnIniciar"
        btnIniciar.Size = New Size(115, 30)
        btnIniciar.TabIndex = 3
        btnIniciar.Text = "Iniciar trabajo"
        btnIniciar.UseVisualStyleBackColor = False
        '
        ' btnFinalizar
        '
        btnFinalizar.BackColor = Color.White
        btnFinalizar.Cursor = Cursors.Hand
        btnFinalizar.FlatAppearance.BorderColor = Color.Silver
        btnFinalizar.FlatStyle = FlatStyle.Flat
        btnFinalizar.Location = New Point(505, 7)
        btnFinalizar.Name = "btnFinalizar"
        btnFinalizar.Size = New Size(80, 30)
        btnFinalizar.TabIndex = 4
        btnFinalizar.Text = "Finalizar"
        btnFinalizar.UseVisualStyleBackColor = False
        '
        ' btnEntregar
        '
        btnEntregar.BackColor = Color.White
        btnEntregar.Cursor = Cursors.Hand
        btnEntregar.FlatAppearance.BorderColor = Color.Silver
        btnEntregar.FlatStyle = FlatStyle.Flat
        btnEntregar.Location = New Point(591, 7)
        btnEntregar.Name = "btnEntregar"
        btnEntregar.Size = New Size(80, 30)
        btnEntregar.TabIndex = 5
        btnEntregar.Text = "Entregar"
        btnEntregar.UseVisualStyleBackColor = False
        '
        ' txtMotivoAnulacion
        '
        txtMotivoAnulacion.Location = New Point(700, 9)
        txtMotivoAnulacion.MaxLength = 255
        txtMotivoAnulacion.Name = "txtMotivoAnulacion"
        txtMotivoAnulacion.PlaceholderText = "Motivo de la anulación"
        txtMotivoAnulacion.Size = New Size(210, 27)
        txtMotivoAnulacion.TabIndex = 6
        '
        ' btnAnular
        '
        btnAnular.BackColor = Color.White
        btnAnular.Cursor = Cursors.Hand
        btnAnular.FlatAppearance.BorderColor = Color.Silver
        btnAnular.FlatStyle = FlatStyle.Flat
        btnAnular.Location = New Point(916, 7)
        btnAnular.Name = "btnAnular"
        btnAnular.Size = New Size(72, 30)
        btnAnular.TabIndex = 7
        btnAnular.Text = "Anular"
        btnAnular.UseVisualStyleBackColor = False
        '
        ' lblHistorial
        '
        lblHistorial.AutoSize = True
        lblHistorial.ForeColor = Color.DimGray
        lblHistorial.Location = New Point(30, 526)
        lblHistorial.Name = "lblHistorial"
        lblHistorial.Size = New Size(146, 20)
        lblHistorial.TabIndex = 6
        lblHistorial.Text = "Historial de estados"
        '
        ' dgvHistorial
        '
        dgvHistorial.AllowUserToAddRows = False
        dgvHistorial.AllowUserToDeleteRows = False
        dgvHistorial.AllowUserToResizeRows = False
        dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvHistorial.BackgroundColor = Color.White
        dgvHistorial.BorderStyle = BorderStyle.FixedSingle
        dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvHistorial.Location = New Point(30, 549)
        dgvHistorial.MultiSelect = False
        dgvHistorial.Name = "dgvHistorial"
        dgvHistorial.ReadOnly = True
        dgvHistorial.RowHeadersVisible = False
        dgvHistorial.RowHeadersWidth = 51
        dgvHistorial.RowTemplate.Height = 22
        dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistorial.Size = New Size(1004, 91)
        dgvHistorial.TabIndex = 7
        dgvHistorial.TabStop = False
        '
        ' FrmOrdenGestion
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1064, 650)
        Controls.Add(dgvHistorial)
        Controls.Add(lblHistorial)
        Controls.Add(pnlAcciones)
        Controls.Add(pnlMecanico)
        Controls.Add(pnlDetalle)
        Controls.Add(pnlDatos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "FrmOrdenGestion"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Gestión de la orden de trabajo"
        pnlDatos.ResumeLayout(False)
        pnlDatos.PerformLayout()
        pnlDetalle.ResumeLayout(False)
        pnlDetalle.PerformLayout()
        CType(nudCantidad, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvDetalle, ComponentModel.ISupportInitialize).EndInit()
        CType(nudCantidadReal, ComponentModel.ISupportInitialize).EndInit()
        CType(nudHorasReales, ComponentModel.ISupportInitialize).EndInit()
        pnlMecanico.ResumeLayout(False)
        pnlMecanico.PerformLayout()
        pnlAcciones.ResumeLayout(False)
        pnlAcciones.PerformLayout()
        CType(dgvHistorial, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblNroOrdenTit As Label
    Friend WithEvents lblNroOrden As Label
    Friend WithEvents lblEstadoTit As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblKmIngresoTit As Label
    Friend WithEvents lblKmIngreso As Label
    Friend WithEvents lblVehiculoTit As Label
    Friend WithEvents lblVehiculo As Label
    Friend WithEvents lblClienteTit As Label
    Friend WithEvents lblCliente As Label
    Friend WithEvents lblFechaRecepcionTit As Label
    Friend WithEvents lblFechaRecepcion As Label
    Friend WithEvents lblFechaPrometidaTit As Label
    Friend WithEvents lblFechaPrometida As Label
    Friend WithEvents lblFechaFinalizacionTit As Label
    Friend WithEvents lblFechaFinalizacion As Label
    Friend WithEvents lblFechaEntregaTit As Label
    Friend WithEvents lblFechaEntrega As Label
    Friend WithEvents lblSintoma As Label
    Friend WithEvents txtSintoma As TextBox
    Friend WithEvents lblObsRecepcion As Label
    Friend WithEvents txtObsRecepcion As TextBox
    Friend WithEvents pnlDetalle As Panel
    Friend WithEvents lblDetalle As Label
    Friend WithEvents lblServicio As Label
    Friend WithEvents cboServicio As ComboBox
    Friend WithEvents lblCantidad As Label
    Friend WithEvents nudCantidad As NumericUpDown
    Friend WithEvents btnAgregar As Button
    Friend WithEvents dgvDetalle As DataGridView
    Friend WithEvents btnActualizar As Button
    Friend WithEvents btnQuitar As Button
    Friend WithEvents lblTotalPresupuestado As Label
    Friend WithEvents lblTotalAprobado As Label
    Friend WithEvents lblCantidadReal As Label
    Friend WithEvents nudCantidadReal As NumericUpDown
    Friend WithEvents lblHorasReales As Label
    Friend WithEvents nudHorasReales As NumericUpDown
    Friend WithEvents btnGuardarEjecucion As Button
    Friend WithEvents pnlMecanico As Panel
    Friend WithEvents lblMecanico As Label
    Friend WithEvents cboMecanico As ComboBox
    Friend WithEvents btnGuardarMecanico As Button
    Friend WithEvents lblNotasTecnicas As Label
    Friend WithEvents txtNotasTecnicas As TextBox
    Friend WithEvents pnlAcciones As Panel
    Friend WithEvents btnPresupuestar As Button
    Friend WithEvents btnAprobar As Button
    Friend WithEvents btnRechazar As Button
    Friend WithEvents btnIniciar As Button
    Friend WithEvents btnFinalizar As Button
    Friend WithEvents btnEntregar As Button
    Friend WithEvents txtMotivoAnulacion As TextBox
    Friend WithEvents btnAnular As Button
    Friend WithEvents lblHistorial As Label
    Friend WithEvents dgvHistorial As DataGridView
End Class

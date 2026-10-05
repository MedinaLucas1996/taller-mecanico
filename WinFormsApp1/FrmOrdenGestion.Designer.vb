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
        pnlMecanico = New Panel()
        lblMecanico = New Label()
        cboMecanico = New ComboBox()
        btnGuardarMecanico = New Button()
        pnlAcciones = New Panel()
        btnPresupuestar = New Button()
        lblHistorial = New Label()
        dgvHistorial = New DataGridView()
        pnlDatos.SuspendLayout()
        pnlDetalle.SuspendLayout()
        CType(nudCantidad, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvDetalle, ComponentModel.ISupportInitialize).BeginInit()
        pnlMecanico.SuspendLayout()
        pnlAcciones.SuspendLayout()
        CType(dgvHistorial, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
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
        lblSubtitulo.Location = New Point(33, 62)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(480, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Presupuesto, mecánico e historial de la orden de trabajo"
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
        pnlDatos.Controls.Add(lblSintoma)
        pnlDatos.Controls.Add(txtSintoma)
        pnlDatos.Controls.Add(lblObsRecepcion)
        pnlDatos.Controls.Add(txtObsRecepcion)
        pnlDatos.Location = New Point(30, 94)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(1004, 100)
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
        ' lblSintoma
        '
        lblSintoma.AutoSize = True
        lblSintoma.ForeColor = Color.DimGray
        lblSintoma.Location = New Point(600, 6)
        lblSintoma.Name = "lblSintoma"
        lblSintoma.Size = New Size(132, 20)
        lblSintoma.TabIndex = 14
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
        txtSintoma.Size = New Size(190, 64)
        txtSintoma.TabIndex = 15
        txtSintoma.TabStop = False
        '
        ' lblObsRecepcion
        '
        lblObsRecepcion.AutoSize = True
        lblObsRecepcion.ForeColor = Color.DimGray
        lblObsRecepcion.Location = New Point(800, 6)
        lblObsRecepcion.Name = "lblObsRecepcion"
        lblObsRecepcion.Size = New Size(195, 20)
        lblObsRecepcion.TabIndex = 16
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
        txtObsRecepcion.Size = New Size(190, 64)
        txtObsRecepcion.TabIndex = 17
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
        pnlDetalle.Location = New Point(30, 202)
        pnlDetalle.Name = "pnlDetalle"
        pnlDetalle.Size = New Size(680, 254)
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
        cboServicio.Size = New Size(322, 28)
        cboServicio.TabIndex = 2
        '
        ' lblCantidad
        '
        lblCantidad.AutoSize = True
        lblCantidad.Location = New Point(416, 39)
        lblCantidad.Name = "lblCantidad"
        lblCantidad.Size = New Size(69, 20)
        lblCantidad.TabIndex = 3
        lblCantidad.Text = "Cantidad"
        '
        ' nudCantidad
        '
        nudCantidad.DecimalPlaces = 2
        nudCantidad.Location = New Point(490, 36)
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
        btnAgregar.Location = New Point(582, 34)
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
        dgvDetalle.Size = New Size(648, 100)
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
        lblTotalPresupuestado.Location = New Point(300, 174)
        lblTotalPresupuestado.Name = "lblTotalPresupuestado"
        lblTotalPresupuestado.Size = New Size(364, 20)
        lblTotalPresupuestado.TabIndex = 9
        lblTotalPresupuestado.Text = "Total presupuestado: -"
        lblTotalPresupuestado.TextAlign = ContentAlignment.TopRight
        '
        ' lblTotalAprobado
        '
        lblTotalAprobado.ForeColor = Color.DimGray
        lblTotalAprobado.Location = New Point(300, 196)
        lblTotalAprobado.Name = "lblTotalAprobado"
        lblTotalAprobado.Size = New Size(364, 20)
        lblTotalAprobado.TabIndex = 10
        lblTotalAprobado.Text = "Total aprobado: -"
        lblTotalAprobado.TextAlign = ContentAlignment.TopRight
        '
        ' pnlMecanico
        '
        pnlMecanico.BackColor = Color.White
        pnlMecanico.Controls.Add(lblMecanico)
        pnlMecanico.Controls.Add(cboMecanico)
        pnlMecanico.Controls.Add(btnGuardarMecanico)
        pnlMecanico.Location = New Point(720, 202)
        pnlMecanico.Name = "pnlMecanico"
        pnlMecanico.Size = New Size(314, 254)
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
        cboMecanico.Size = New Size(282, 28)
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
        ' pnlAcciones
        '
        pnlAcciones.BackColor = Color.White
        pnlAcciones.Controls.Add(btnPresupuestar)
        pnlAcciones.Location = New Point(30, 464)
        pnlAcciones.Name = "pnlAcciones"
        pnlAcciones.Size = New Size(1004, 44)
        pnlAcciones.TabIndex = 5
        '
        ' btnPresupuestar
        '
        btnPresupuestar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnPresupuestar.Cursor = Cursors.Hand
        btnPresupuestar.FlatAppearance.BorderSize = 0
        btnPresupuestar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnPresupuestar.FlatStyle = FlatStyle.Flat
        btnPresupuestar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnPresupuestar.ForeColor = Color.White
        btnPresupuestar.Location = New Point(16, 7)
        btnPresupuestar.Name = "btnPresupuestar"
        btnPresupuestar.Size = New Size(120, 30)
        btnPresupuestar.TabIndex = 0
        btnPresupuestar.Text = "Presupuestar"
        btnPresupuestar.UseVisualStyleBackColor = False
        '
        ' lblHistorial
        '
        lblHistorial.AutoSize = True
        lblHistorial.ForeColor = Color.DimGray
        lblHistorial.Location = New Point(30, 513)
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
        dgvHistorial.Location = New Point(30, 536)
        dgvHistorial.MultiSelect = False
        dgvHistorial.Name = "dgvHistorial"
        dgvHistorial.ReadOnly = True
        dgvHistorial.RowHeadersVisible = False
        dgvHistorial.RowHeadersWidth = 51
        dgvHistorial.RowTemplate.Height = 22
        dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistorial.Size = New Size(1004, 102)
        dgvHistorial.TabIndex = 7
        dgvHistorial.TabStop = False
        '
        ' FrmOrdenGestion
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1064, 648)
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
        pnlMecanico.ResumeLayout(False)
        pnlMecanico.PerformLayout()
        pnlAcciones.ResumeLayout(False)
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
    Friend WithEvents pnlMecanico As Panel
    Friend WithEvents lblMecanico As Label
    Friend WithEvents cboMecanico As ComboBox
    Friend WithEvents btnGuardarMecanico As Button
    Friend WithEvents pnlAcciones As Panel
    Friend WithEvents btnPresupuestar As Button
    Friend WithEvents lblHistorial As Label
    Friend WithEvents dgvHistorial As DataGridView
End Class

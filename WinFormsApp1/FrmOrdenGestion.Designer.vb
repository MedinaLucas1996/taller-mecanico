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
        lblEstado = New Label()
        lblSubtitulo = New Label()
        tlpEtapas = New TableLayoutPanel()
        pnlEtapa1 = New Panel()
        lblNomEtapa1 = New Label()
        lblMarcaEtapa1 = New Label()
        pnlEtapa2 = New Panel()
        lblNomEtapa2 = New Label()
        lblMarcaEtapa2 = New Label()
        pnlEtapa3 = New Panel()
        lblNomEtapa3 = New Label()
        lblMarcaEtapa3 = New Label()
        pnlEtapa4 = New Panel()
        lblNomEtapa4 = New Label()
        lblMarcaEtapa4 = New Label()
        pnlEtapa5 = New Panel()
        lblNomEtapa5 = New Label()
        lblMarcaEtapa5 = New Label()
        pnlEtapa6 = New Panel()
        lblNomEtapa6 = New Label()
        lblMarcaEtapa6 = New Label()
        lblCierre = New Label()
        pnlTrabajo = New Panel()
        dgvDetalle = New DataGridView()
        colMarca = New DataGridViewImageColumn()
        colDescripcion = New DataGridViewTextBoxColumn()
        colCantidad = New DataGridViewTextBoxColumn()
        colPrecio = New DataGridViewTextBoxColumn()
        colSubtotal = New DataGridViewTextBoxColumn()
        colAprobado = New DataGridViewCheckBoxColumn()
        colHoras = New DataGridViewTextBoxColumn()
        colIdDetalle = New DataGridViewTextBoxColumn()
        pnlPieLineas = New Panel()
        btnActualizar = New Button()
        btnQuitar = New Button()
        lblTotalPresupuestado = New Label()
        lblTotalAprobado = New Label()
        pnlAvisoMecanico = New Panel()
        picAvisoMecanico = New PictureBox()
        lblAvisoMecanico = New Label()
        pnlNotas = New Panel()
        lblNotas = New Label()
        txtNotasTecnicas = New TextBox()
        pnlEditor = New Panel()
        lblServicio = New Label()
        cboServicio = New ComboBox()
        lblCantidad = New Label()
        nudCantidad = New NumericUpDown()
        btnAgregar = New Button()
        pnlCabeceraEtapa = New Panel()
        lblEtapaTitulo = New Label()
        lblConteo = New Label()
        lblEtapaAyuda = New Label()
        pnlDatos = New Panel()
        lblDatosTitulo = New Label()
        picMecanico = New PictureBox()
        lblMecanicoTit = New Label()
        cboMecanico = New ComboBox()
        lblMecanico = New Label()
        btnGuardarMecanico = New Button()
        picKm = New PictureBox()
        lblKmTit = New Label()
        lblKm = New Label()
        picRecepcion = New PictureBox()
        lblRecepcionTit = New Label()
        lblRecepcion = New Label()
        picFinalizacion = New PictureBox()
        lblFinalizacionTit = New Label()
        lblFinalizacion = New Label()
        picEntrega = New PictureBox()
        lblEntregaTit = New Label()
        lblEntrega = New Label()
        picSintoma = New PictureBox()
        lblSintomaTit = New Label()
        txtSintoma = New TextBox()
        lblObsRecepcionTit = New Label()
        txtObsRecepcion = New TextBox()
        pnlHistorial = New Panel()
        picHistorial = New PictureBox()
        lblHistorialTitulo = New Label()
        dgvHistorial = New DataGridView()
        colHistorial = New DataGridViewTextBoxColumn()
        btnAnular = New Button()
        lblProximo = New Label()
        btnSecundario = New Button()
        btnPrimario = New Button()
        tlpEtapas.SuspendLayout()
        pnlEtapa1.SuspendLayout()
        pnlEtapa2.SuspendLayout()
        pnlEtapa3.SuspendLayout()
        pnlEtapa4.SuspendLayout()
        pnlEtapa5.SuspendLayout()
        pnlEtapa6.SuspendLayout()
        pnlTrabajo.SuspendLayout()
        pnlPieLineas.SuspendLayout()
        pnlAvisoMecanico.SuspendLayout()
        pnlNotas.SuspendLayout()
        pnlEditor.SuspendLayout()
        pnlCabeceraEtapa.SuspendLayout()
        pnlDatos.SuspendLayout()
        pnlHistorial.SuspendLayout()
        CType(dgvDetalle, ComponentModel.ISupportInitialize).BeginInit()
        CType(picAvisoMecanico, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudCantidad, ComponentModel.ISupportInitialize).BeginInit()
        CType(picMecanico, ComponentModel.ISupportInitialize).BeginInit()
        CType(picKm, ComponentModel.ISupportInitialize).BeginInit()
        CType(picRecepcion, ComponentModel.ISupportInitialize).BeginInit()
        CType(picFinalizacion, ComponentModel.ISupportInitialize).BeginInit()
        CType(picEntrega, ComponentModel.ISupportInitialize).BeginInit()
        CType(picSintoma, ComponentModel.ISupportInitialize).BeginInit()
        CType(picHistorial, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvHistorial, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblTitulo.AutoEllipsis = True
        lblTitulo.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTitulo.Location = New Point(24, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(970, 42)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Orden N.º"
        lblTitulo.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblEstado
        '
        lblEstado.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblEstado.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblEstado.Location = New Point(1006, 16)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(150, 32)
        lblEstado.TabIndex = 1
        lblEstado.Text = "-"
        lblEstado.TextAlign = ContentAlignment.MiddleCenter
        '
        ' lblSubtitulo
        '
        lblSubtitulo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblSubtitulo.AutoEllipsis = True
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(26, 54)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(1130, 24)
        lblSubtitulo.TabIndex = 2
        lblSubtitulo.Text = "-"
        '
        ' tlpEtapas
        '
        tlpEtapas.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tlpEtapas.ColumnCount = 6
        tlpEtapas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.666666F))
        tlpEtapas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.666666F))
        tlpEtapas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.666666F))
        tlpEtapas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.666666F))
        tlpEtapas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.666666F))
        tlpEtapas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.666666F))
        tlpEtapas.Controls.Add(pnlEtapa1, 0, 0)
        tlpEtapas.Controls.Add(pnlEtapa2, 1, 0)
        tlpEtapas.Controls.Add(pnlEtapa3, 2, 0)
        tlpEtapas.Controls.Add(pnlEtapa4, 3, 0)
        tlpEtapas.Controls.Add(pnlEtapa5, 4, 0)
        tlpEtapas.Controls.Add(pnlEtapa6, 5, 0)
        tlpEtapas.Location = New Point(24, 86)
        tlpEtapas.Name = "tlpEtapas"
        tlpEtapas.RowCount = 1
        tlpEtapas.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpEtapas.Size = New Size(1132, 48)
        tlpEtapas.TabIndex = 3
        '
        ' pnlEtapa1
        '
        pnlEtapa1.BackColor = Color.White
        pnlEtapa1.Controls.Add(lblNomEtapa1)
        pnlEtapa1.Controls.Add(lblMarcaEtapa1)
        pnlEtapa1.Dock = DockStyle.Fill
        pnlEtapa1.Location = New Point(0, 0)
        pnlEtapa1.Margin = New Padding(0, 0, 6, 0)
        pnlEtapa1.Name = "pnlEtapa1"
        pnlEtapa1.Size = New Size(182, 48)
        pnlEtapa1.TabIndex = 0
        '
        ' lblNomEtapa1
        '
        lblNomEtapa1.AutoEllipsis = True
        lblNomEtapa1.Dock = DockStyle.Fill
        lblNomEtapa1.Font = New Font("Segoe UI", 9.5F)
        lblNomEtapa1.ForeColor = Color.Gray
        lblNomEtapa1.Location = New Point(38, 0)
        lblNomEtapa1.Name = "lblNomEtapa1"
        lblNomEtapa1.Size = New Size(144, 48)
        lblNomEtapa1.TabIndex = 1
        lblNomEtapa1.Text = "Recepcionada"
        lblNomEtapa1.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblMarcaEtapa1
        '
        lblMarcaEtapa1.Dock = DockStyle.Left
        lblMarcaEtapa1.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblMarcaEtapa1.ForeColor = Color.Gray
        lblMarcaEtapa1.Location = New Point(0, 0)
        lblMarcaEtapa1.Name = "lblMarcaEtapa1"
        lblMarcaEtapa1.Size = New Size(38, 48)
        lblMarcaEtapa1.TabIndex = 0
        lblMarcaEtapa1.Text = "1"
        lblMarcaEtapa1.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlEtapa2
        '
        pnlEtapa2.BackColor = Color.White
        pnlEtapa2.Controls.Add(lblNomEtapa2)
        pnlEtapa2.Controls.Add(lblMarcaEtapa2)
        pnlEtapa2.Dock = DockStyle.Fill
        pnlEtapa2.Location = New Point(188, 0)
        pnlEtapa2.Margin = New Padding(0, 0, 6, 0)
        pnlEtapa2.Name = "pnlEtapa2"
        pnlEtapa2.Size = New Size(182, 48)
        pnlEtapa2.TabIndex = 1
        '
        ' lblNomEtapa2
        '
        lblNomEtapa2.AutoEllipsis = True
        lblNomEtapa2.Dock = DockStyle.Fill
        lblNomEtapa2.Font = New Font("Segoe UI", 9.5F)
        lblNomEtapa2.ForeColor = Color.Gray
        lblNomEtapa2.Location = New Point(38, 0)
        lblNomEtapa2.Name = "lblNomEtapa2"
        lblNomEtapa2.Size = New Size(144, 48)
        lblNomEtapa2.TabIndex = 1
        lblNomEtapa2.Text = "Presupuestada"
        lblNomEtapa2.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblMarcaEtapa2
        '
        lblMarcaEtapa2.Dock = DockStyle.Left
        lblMarcaEtapa2.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblMarcaEtapa2.ForeColor = Color.Gray
        lblMarcaEtapa2.Location = New Point(0, 0)
        lblMarcaEtapa2.Name = "lblMarcaEtapa2"
        lblMarcaEtapa2.Size = New Size(38, 48)
        lblMarcaEtapa2.TabIndex = 0
        lblMarcaEtapa2.Text = "2"
        lblMarcaEtapa2.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlEtapa3
        '
        pnlEtapa3.BackColor = Color.White
        pnlEtapa3.Controls.Add(lblNomEtapa3)
        pnlEtapa3.Controls.Add(lblMarcaEtapa3)
        pnlEtapa3.Dock = DockStyle.Fill
        pnlEtapa3.Location = New Point(376, 0)
        pnlEtapa3.Margin = New Padding(0, 0, 6, 0)
        pnlEtapa3.Name = "pnlEtapa3"
        pnlEtapa3.Size = New Size(182, 48)
        pnlEtapa3.TabIndex = 2
        '
        ' lblNomEtapa3
        '
        lblNomEtapa3.AutoEllipsis = True
        lblNomEtapa3.Dock = DockStyle.Fill
        lblNomEtapa3.Font = New Font("Segoe UI", 9.5F)
        lblNomEtapa3.ForeColor = Color.Gray
        lblNomEtapa3.Location = New Point(38, 0)
        lblNomEtapa3.Name = "lblNomEtapa3"
        lblNomEtapa3.Size = New Size(144, 48)
        lblNomEtapa3.TabIndex = 1
        lblNomEtapa3.Text = "Aprobada"
        lblNomEtapa3.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblMarcaEtapa3
        '
        lblMarcaEtapa3.Dock = DockStyle.Left
        lblMarcaEtapa3.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblMarcaEtapa3.ForeColor = Color.Gray
        lblMarcaEtapa3.Location = New Point(0, 0)
        lblMarcaEtapa3.Name = "lblMarcaEtapa3"
        lblMarcaEtapa3.Size = New Size(38, 48)
        lblMarcaEtapa3.TabIndex = 0
        lblMarcaEtapa3.Text = "3"
        lblMarcaEtapa3.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlEtapa4
        '
        pnlEtapa4.BackColor = Color.White
        pnlEtapa4.Controls.Add(lblNomEtapa4)
        pnlEtapa4.Controls.Add(lblMarcaEtapa4)
        pnlEtapa4.Dock = DockStyle.Fill
        pnlEtapa4.Location = New Point(564, 0)
        pnlEtapa4.Margin = New Padding(0, 0, 6, 0)
        pnlEtapa4.Name = "pnlEtapa4"
        pnlEtapa4.Size = New Size(182, 48)
        pnlEtapa4.TabIndex = 3
        '
        ' lblNomEtapa4
        '
        lblNomEtapa4.AutoEllipsis = True
        lblNomEtapa4.Dock = DockStyle.Fill
        lblNomEtapa4.Font = New Font("Segoe UI", 9.5F)
        lblNomEtapa4.ForeColor = Color.Gray
        lblNomEtapa4.Location = New Point(38, 0)
        lblNomEtapa4.Name = "lblNomEtapa4"
        lblNomEtapa4.Size = New Size(144, 48)
        lblNomEtapa4.TabIndex = 1
        lblNomEtapa4.Text = "En proceso"
        lblNomEtapa4.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblMarcaEtapa4
        '
        lblMarcaEtapa4.Dock = DockStyle.Left
        lblMarcaEtapa4.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblMarcaEtapa4.ForeColor = Color.Gray
        lblMarcaEtapa4.Location = New Point(0, 0)
        lblMarcaEtapa4.Name = "lblMarcaEtapa4"
        lblMarcaEtapa4.Size = New Size(38, 48)
        lblMarcaEtapa4.TabIndex = 0
        lblMarcaEtapa4.Text = "4"
        lblMarcaEtapa4.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlEtapa5
        '
        pnlEtapa5.BackColor = Color.White
        pnlEtapa5.Controls.Add(lblNomEtapa5)
        pnlEtapa5.Controls.Add(lblMarcaEtapa5)
        pnlEtapa5.Dock = DockStyle.Fill
        pnlEtapa5.Location = New Point(752, 0)
        pnlEtapa5.Margin = New Padding(0, 0, 6, 0)
        pnlEtapa5.Name = "pnlEtapa5"
        pnlEtapa5.Size = New Size(182, 48)
        pnlEtapa5.TabIndex = 4
        '
        ' lblNomEtapa5
        '
        lblNomEtapa5.AutoEllipsis = True
        lblNomEtapa5.Dock = DockStyle.Fill
        lblNomEtapa5.Font = New Font("Segoe UI", 9.5F)
        lblNomEtapa5.ForeColor = Color.Gray
        lblNomEtapa5.Location = New Point(38, 0)
        lblNomEtapa5.Name = "lblNomEtapa5"
        lblNomEtapa5.Size = New Size(144, 48)
        lblNomEtapa5.TabIndex = 1
        lblNomEtapa5.Text = "Finalizada"
        lblNomEtapa5.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblMarcaEtapa5
        '
        lblMarcaEtapa5.Dock = DockStyle.Left
        lblMarcaEtapa5.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblMarcaEtapa5.ForeColor = Color.Gray
        lblMarcaEtapa5.Location = New Point(0, 0)
        lblMarcaEtapa5.Name = "lblMarcaEtapa5"
        lblMarcaEtapa5.Size = New Size(38, 48)
        lblMarcaEtapa5.TabIndex = 0
        lblMarcaEtapa5.Text = "5"
        lblMarcaEtapa5.TextAlign = ContentAlignment.MiddleCenter
        '
        ' pnlEtapa6
        '
        pnlEtapa6.BackColor = Color.White
        pnlEtapa6.Controls.Add(lblNomEtapa6)
        pnlEtapa6.Controls.Add(lblMarcaEtapa6)
        pnlEtapa6.Dock = DockStyle.Fill
        pnlEtapa6.Location = New Point(940, 0)
        pnlEtapa6.Margin = New Padding(0, 0, 0, 0)
        pnlEtapa6.Name = "pnlEtapa6"
        pnlEtapa6.Size = New Size(182, 48)
        pnlEtapa6.TabIndex = 5
        '
        ' lblNomEtapa6
        '
        lblNomEtapa6.AutoEllipsis = True
        lblNomEtapa6.Dock = DockStyle.Fill
        lblNomEtapa6.Font = New Font("Segoe UI", 9.5F)
        lblNomEtapa6.ForeColor = Color.Gray
        lblNomEtapa6.Location = New Point(38, 0)
        lblNomEtapa6.Name = "lblNomEtapa6"
        lblNomEtapa6.Size = New Size(144, 48)
        lblNomEtapa6.TabIndex = 1
        lblNomEtapa6.Text = "Entregada"
        lblNomEtapa6.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblMarcaEtapa6
        '
        lblMarcaEtapa6.Dock = DockStyle.Left
        lblMarcaEtapa6.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblMarcaEtapa6.ForeColor = Color.Gray
        lblMarcaEtapa6.Location = New Point(0, 0)
        lblMarcaEtapa6.Name = "lblMarcaEtapa6"
        lblMarcaEtapa6.Size = New Size(38, 48)
        lblMarcaEtapa6.TabIndex = 0
        lblMarcaEtapa6.Text = "6"
        lblMarcaEtapa6.TextAlign = ContentAlignment.MiddleCenter
        '
        ' lblCierre
        '
        lblCierre.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblCierre.AutoEllipsis = True
        lblCierre.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblCierre.ForeColor = Color.Firebrick
        lblCierre.Location = New Point(26, 138)
        lblCierre.Name = "lblCierre"
        lblCierre.Size = New Size(1130, 24)
        lblCierre.TabIndex = 4
        '
        ' pnlTrabajo
        '
        pnlTrabajo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlTrabajo.BackColor = Color.White
        pnlTrabajo.Controls.Add(dgvDetalle)
        pnlTrabajo.Controls.Add(pnlPieLineas)
        pnlTrabajo.Controls.Add(pnlAvisoMecanico)
        pnlTrabajo.Controls.Add(pnlNotas)
        pnlTrabajo.Controls.Add(pnlEditor)
        pnlTrabajo.Controls.Add(pnlCabeceraEtapa)
        pnlTrabajo.Location = New Point(24, 168)
        pnlTrabajo.Name = "pnlTrabajo"
        pnlTrabajo.Padding = New Padding(16, 12, 16, 12)
        pnlTrabajo.Size = New Size(772, 612)
        pnlTrabajo.TabIndex = 5
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
        dgvDetalle.Columns.AddRange(New DataGridViewColumn() {colMarca, colDescripcion, colCantidad, colPrecio, colSubtotal, colAprobado, colHoras, colIdDetalle})
        dgvDetalle.Dock = DockStyle.Fill
        dgvDetalle.Location = New Point(16, 130)
        dgvDetalle.MultiSelect = False
        dgvDetalle.Name = "dgvDetalle"
        dgvDetalle.ReadOnly = True
        dgvDetalle.RowHeadersVisible = False
        dgvDetalle.RowHeadersWidth = 51
        dgvDetalle.RowTemplate.Height = 26
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDetalle.Size = New Size(740, 208)
        dgvDetalle.TabIndex = 2
        '
        ' colMarca
        '
        colMarca.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        colMarca.HeaderText = ""
        colMarca.MinimumWidth = 6
        colMarca.Name = "colMarca"
        colMarca.ReadOnly = True
        colMarca.Resizable = DataGridViewTriState.False
        colMarca.Visible = False
        colMarca.Width = 36
        '
        ' colDescripcion
        '
        colDescripcion.DataPropertyName = "descripcion"
        colDescripcion.HeaderText = "Descripción"
        colDescripcion.MinimumWidth = 6
        colDescripcion.Name = "colDescripcion"
        colDescripcion.ReadOnly = True
        '
        ' colCantidad
        '
        colCantidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        colCantidad.DataPropertyName = "cantidad"
        colCantidad.HeaderText = "Cantidad"
        colCantidad.MinimumWidth = 6
        colCantidad.Name = "colCantidad"
        colCantidad.ReadOnly = True
        '
        ' colPrecio
        '
        colPrecio.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        colPrecio.DataPropertyName = "precio_unitario"
        colPrecio.HeaderText = "Precio"
        colPrecio.MinimumWidth = 6
        colPrecio.Name = "colPrecio"
        colPrecio.ReadOnly = True
        '
        ' colSubtotal
        '
        colSubtotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        colSubtotal.DataPropertyName = "subtotal"
        colSubtotal.HeaderText = "Subtotal"
        colSubtotal.MinimumWidth = 6
        colSubtotal.Name = "colSubtotal"
        colSubtotal.ReadOnly = True
        '
        ' colAprobado
        '
        colAprobado.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        colAprobado.DataPropertyName = "aprobado"
        colAprobado.HeaderText = "Aprobado"
        colAprobado.MinimumWidth = 6
        colAprobado.Name = "colAprobado"
        colAprobado.ReadOnly = True
        colAprobado.Visible = False
        '
        ' colHoras
        '
        colHoras.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        colHoras.DataPropertyName = "horas_reales"
        colHoras.HeaderText = "Horas trabajadas"
        colHoras.MinimumWidth = 6
        colHoras.Name = "colHoras"
        colHoras.ReadOnly = True
        colHoras.Visible = False
        '
        ' colIdDetalle
        '
        colIdDetalle.DataPropertyName = "id_ot_detalle"
        colIdDetalle.HeaderText = "ID"
        colIdDetalle.MinimumWidth = 6
        colIdDetalle.Name = "colIdDetalle"
        colIdDetalle.ReadOnly = True
        colIdDetalle.Visible = False
        '
        ' pnlPieLineas
        '
        pnlPieLineas.Controls.Add(btnActualizar)
        pnlPieLineas.Controls.Add(btnQuitar)
        pnlPieLineas.Controls.Add(lblTotalPresupuestado)
        pnlPieLineas.Controls.Add(lblTotalAprobado)
        pnlPieLineas.Dock = DockStyle.Bottom
        pnlPieLineas.Location = New Point(16, 338)
        pnlPieLineas.Name = "pnlPieLineas"
        pnlPieLineas.Size = New Size(740, 64)
        pnlPieLineas.TabIndex = 3
        '
        ' btnActualizar
        '
        btnActualizar.BackColor = Color.White
        btnActualizar.Cursor = Cursors.Hand
        btnActualizar.FlatAppearance.BorderColor = Color.Silver
        btnActualizar.FlatStyle = FlatStyle.Flat
        btnActualizar.Name = "btnActualizar"
        btnActualizar.TabIndex = 0
        btnActualizar.Text = " Actualizar cantidad"
        btnActualizar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnActualizar.UseVisualStyleBackColor = False
        btnActualizar.Location = New Point(0, 12)
        btnActualizar.Size = New Size(210, 40)
        '
        ' btnQuitar
        '
        btnQuitar.BackColor = Color.White
        btnQuitar.Cursor = Cursors.Hand
        btnQuitar.FlatAppearance.BorderColor = Color.Silver
        btnQuitar.FlatStyle = FlatStyle.Flat
        btnQuitar.Name = "btnQuitar"
        btnQuitar.TabIndex = 1
        btnQuitar.Text = " Quitar"
        btnQuitar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnQuitar.UseVisualStyleBackColor = False
        btnQuitar.Location = New Point(218, 12)
        btnQuitar.Size = New Size(120, 40)
        '
        ' lblTotalPresupuestado
        '
        lblTotalPresupuestado.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblTotalPresupuestado.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTotalPresupuestado.Location = New Point(390, 10)
        lblTotalPresupuestado.Name = "lblTotalPresupuestado"
        lblTotalPresupuestado.Size = New Size(350, 22)
        lblTotalPresupuestado.TabIndex = 2
        lblTotalPresupuestado.Text = "Total presupuestado: -"
        lblTotalPresupuestado.TextAlign = ContentAlignment.TopRight
        '
        ' lblTotalAprobado
        '
        lblTotalAprobado.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblTotalAprobado.ForeColor = Color.DimGray
        lblTotalAprobado.Location = New Point(390, 34)
        lblTotalAprobado.Name = "lblTotalAprobado"
        lblTotalAprobado.Size = New Size(350, 22)
        lblTotalAprobado.TabIndex = 3
        lblTotalAprobado.Text = "Total aprobado: -"
        lblTotalAprobado.TextAlign = ContentAlignment.TopRight
        '
        ' pnlAvisoMecanico
        '
        pnlAvisoMecanico.Controls.Add(picAvisoMecanico)
        pnlAvisoMecanico.Controls.Add(lblAvisoMecanico)
        pnlAvisoMecanico.Dock = DockStyle.Bottom
        pnlAvisoMecanico.Location = New Point(16, 402)
        pnlAvisoMecanico.Name = "pnlAvisoMecanico"
        pnlAvisoMecanico.Size = New Size(740, 44)
        pnlAvisoMecanico.TabIndex = 4
        pnlAvisoMecanico.Visible = False
        '
        ' picAvisoMecanico
        '
        picAvisoMecanico.Location = New Point(0, 8)
        picAvisoMecanico.Name = "picAvisoMecanico"
        picAvisoMecanico.Size = New Size(26, 28)
        picAvisoMecanico.SizeMode = PictureBoxSizeMode.CenterImage
        picAvisoMecanico.TabIndex = 0
        picAvisoMecanico.TabStop = False
        '
        ' lblAvisoMecanico
        '
        lblAvisoMecanico.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAvisoMecanico.AutoEllipsis = True
        lblAvisoMecanico.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblAvisoMecanico.Location = New Point(32, 10)
        lblAvisoMecanico.Name = "lblAvisoMecanico"
        lblAvisoMecanico.Size = New Size(706, 24)
        lblAvisoMecanico.TabIndex = 1
        lblAvisoMecanico.Text = "-"
        lblAvisoMecanico.TextAlign = ContentAlignment.MiddleLeft
        '
        ' pnlNotas
        '
        pnlNotas.Controls.Add(lblNotas)
        pnlNotas.Controls.Add(txtNotasTecnicas)
        pnlNotas.Dock = DockStyle.Bottom
        pnlNotas.Location = New Point(16, 446)
        pnlNotas.Name = "pnlNotas"
        pnlNotas.Size = New Size(740, 154)
        pnlNotas.TabIndex = 5
        pnlNotas.Visible = False
        '
        ' lblNotas
        '
        lblNotas.AutoSize = True
        lblNotas.Location = New Point(0, 8)
        lblNotas.Name = "lblNotas"
        lblNotas.Size = New Size(100, 20)
        lblNotas.TabIndex = 0
        lblNotas.Text = "Observaciones del mecánico"
        lblNotas.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        '
        ' txtNotasTecnicas
        '
        txtNotasTecnicas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtNotasTecnicas.Location = New Point(0, 36)
        txtNotasTecnicas.MaxLength = 2000
        txtNotasTecnicas.Multiline = True
        txtNotasTecnicas.Name = "txtNotasTecnicas"
        txtNotasTecnicas.PlaceholderText = "Qué se hizo y qué se le recomienda al cliente."
        txtNotasTecnicas.ScrollBars = ScrollBars.Vertical
        txtNotasTecnicas.Size = New Size(740, 114)
        txtNotasTecnicas.TabIndex = 1
        '
        ' pnlEditor
        '
        pnlEditor.Controls.Add(lblServicio)
        pnlEditor.Controls.Add(cboServicio)
        pnlEditor.Controls.Add(lblCantidad)
        pnlEditor.Controls.Add(nudCantidad)
        pnlEditor.Controls.Add(btnAgregar)
        pnlEditor.Dock = DockStyle.Top
        pnlEditor.Location = New Point(16, 78)
        pnlEditor.Name = "pnlEditor"
        pnlEditor.Size = New Size(740, 52)
        pnlEditor.TabIndex = 1
        '
        ' lblServicio
        '
        lblServicio.AutoSize = True
        lblServicio.Location = New Point(0, 14)
        lblServicio.Name = "lblServicio"
        lblServicio.Size = New Size(100, 20)
        lblServicio.TabIndex = 0
        lblServicio.Text = "Servicio"
        '
        ' cboServicio
        '
        cboServicio.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cboServicio.DropDownStyle = ComboBoxStyle.DropDownList
        cboServicio.FormattingEnabled = True
        cboServicio.Location = New Point(68, 10)
        cboServicio.Name = "cboServicio"
        cboServicio.Size = New Size(350, 28)
        cboServicio.TabIndex = 1
        '
        ' lblCantidad
        '
        lblCantidad.AutoSize = True
        lblCantidad.Location = New Point(430, 14)
        lblCantidad.Name = "lblCantidad"
        lblCantidad.Size = New Size(100, 20)
        lblCantidad.TabIndex = 0
        lblCantidad.Text = "Cantidad"
        lblCantidad.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        '
        ' nudCantidad
        '
        nudCantidad.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        nudCantidad.DecimalPlaces = 2
        nudCantidad.Location = New Point(504, 11)
        nudCantidad.Maximum = New Decimal(New Integer() {999999999, 0, 0, 131072})
        nudCantidad.Name = "nudCantidad"
        nudCantidad.Size = New Size(96, 27)
        nudCantidad.TabIndex = 3
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
        btnAgregar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnAgregar.ForeColor = Color.White
        btnAgregar.Name = "btnAgregar"
        btnAgregar.TabIndex = 4
        btnAgregar.Text = " Agregar"
        btnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnAgregar.UseVisualStyleBackColor = False
        btnAgregar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnAgregar.Location = New Point(612, 5)
        btnAgregar.Size = New Size(128, 40)
        '
        ' pnlCabeceraEtapa
        '
        pnlCabeceraEtapa.Controls.Add(lblEtapaTitulo)
        pnlCabeceraEtapa.Controls.Add(lblConteo)
        pnlCabeceraEtapa.Controls.Add(lblEtapaAyuda)
        pnlCabeceraEtapa.Dock = DockStyle.Top
        pnlCabeceraEtapa.Location = New Point(16, 12)
        pnlCabeceraEtapa.Name = "pnlCabeceraEtapa"
        pnlCabeceraEtapa.Size = New Size(740, 66)
        pnlCabeceraEtapa.TabIndex = 0
        '
        ' lblEtapaTitulo
        '
        lblEtapaTitulo.AutoSize = True
        lblEtapaTitulo.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblEtapaTitulo.Location = New Point(0, 0)
        lblEtapaTitulo.Name = "lblEtapaTitulo"
        lblEtapaTitulo.Size = New Size(140, 28)
        lblEtapaTitulo.TabIndex = 0
        lblEtapaTitulo.Text = "-"
        '
        ' lblConteo
        '
        lblConteo.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblConteo.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblConteo.ForeColor = Color.DimGray
        lblConteo.Location = New Point(500, 4)
        lblConteo.Name = "lblConteo"
        lblConteo.Size = New Size(240, 24)
        lblConteo.TabIndex = 1
        lblConteo.TextAlign = ContentAlignment.MiddleRight
        '
        ' lblEtapaAyuda
        '
        lblEtapaAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblEtapaAyuda.AutoEllipsis = True
        lblEtapaAyuda.ForeColor = Color.DimGray
        lblEtapaAyuda.Location = New Point(2, 36)
        lblEtapaAyuda.Name = "lblEtapaAyuda"
        lblEtapaAyuda.Size = New Size(738, 24)
        lblEtapaAyuda.TabIndex = 2
        lblEtapaAyuda.Text = "-"
        '
        ' pnlDatos
        '
        pnlDatos.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnlDatos.BackColor = Color.White
        pnlDatos.Controls.Add(lblDatosTitulo)
        pnlDatos.Controls.Add(picMecanico)
        pnlDatos.Controls.Add(lblMecanicoTit)
        pnlDatos.Controls.Add(cboMecanico)
        pnlDatos.Controls.Add(lblMecanico)
        pnlDatos.Controls.Add(btnGuardarMecanico)
        pnlDatos.Controls.Add(picKm)
        pnlDatos.Controls.Add(lblKmTit)
        pnlDatos.Controls.Add(lblKm)
        pnlDatos.Controls.Add(picRecepcion)
        pnlDatos.Controls.Add(lblRecepcionTit)
        pnlDatos.Controls.Add(lblRecepcion)
        pnlDatos.Controls.Add(picFinalizacion)
        pnlDatos.Controls.Add(lblFinalizacionTit)
        pnlDatos.Controls.Add(lblFinalizacion)
        pnlDatos.Controls.Add(picEntrega)
        pnlDatos.Controls.Add(lblEntregaTit)
        pnlDatos.Controls.Add(lblEntrega)
        pnlDatos.Controls.Add(picSintoma)
        pnlDatos.Controls.Add(lblSintomaTit)
        pnlDatos.Controls.Add(txtSintoma)
        pnlDatos.Controls.Add(lblObsRecepcionTit)
        pnlDatos.Controls.Add(txtObsRecepcion)
        pnlDatos.Location = New Point(808, 168)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(348, 400)
        pnlDatos.TabIndex = 6
        '
        ' lblDatosTitulo
        '
        lblDatosTitulo.AutoSize = True
        lblDatosTitulo.Location = New Point(16, 10)
        lblDatosTitulo.Name = "lblDatosTitulo"
        lblDatosTitulo.Size = New Size(100, 20)
        lblDatosTitulo.TabIndex = 0
        lblDatosTitulo.Text = "Datos de la orden"
        lblDatosTitulo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        '
        ' picMecanico
        '
        picMecanico.Location = New Point(12, 40)
        picMecanico.Name = "picMecanico"
        picMecanico.Size = New Size(26, 28)
        picMecanico.SizeMode = PictureBoxSizeMode.CenterImage
        picMecanico.TabIndex = 0
        picMecanico.TabStop = False
        '
        ' lblMecanicoTit
        '
        lblMecanicoTit.AutoSize = True
        lblMecanicoTit.Location = New Point(42, 44)
        lblMecanicoTit.Name = "lblMecanicoTit"
        lblMecanicoTit.Size = New Size(100, 20)
        lblMecanicoTit.TabIndex = 0
        lblMecanicoTit.Text = "Mecánico"
        lblMecanicoTit.ForeColor = Color.DimGray
        '
        ' cboMecanico
        '
        cboMecanico.DropDownStyle = ComboBoxStyle.DropDownList
        cboMecanico.FormattingEnabled = True
        cboMecanico.Location = New Point(130, 40)
        cboMecanico.Name = "cboMecanico"
        cboMecanico.Size = New Size(204, 28)
        cboMecanico.TabIndex = 1
        '
        ' lblMecanico
        '
        lblMecanico.AutoEllipsis = True
        lblMecanico.Location = New Point(130, 44)
        lblMecanico.Name = "lblMecanico"
        lblMecanico.Size = New Size(204, 22)
        lblMecanico.TabIndex = 0
        lblMecanico.Text = "-"
        lblMecanico.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblMecanico.Visible = False
        '
        ' btnGuardarMecanico
        '
        btnGuardarMecanico.BackColor = Color.White
        btnGuardarMecanico.Cursor = Cursors.Hand
        btnGuardarMecanico.FlatAppearance.BorderColor = Color.Silver
        btnGuardarMecanico.FlatStyle = FlatStyle.Flat
        btnGuardarMecanico.Name = "btnGuardarMecanico"
        btnGuardarMecanico.TabIndex = 2
        btnGuardarMecanico.Text = " Guardar mecánico"
        btnGuardarMecanico.TextImageRelation = TextImageRelation.ImageBeforeText
        btnGuardarMecanico.UseVisualStyleBackColor = False
        btnGuardarMecanico.Location = New Point(130, 76)
        btnGuardarMecanico.Size = New Size(204, 38)
        '
        ' picKm
        '
        picKm.Location = New Point(12, 120)
        picKm.Name = "picKm"
        picKm.Size = New Size(26, 28)
        picKm.SizeMode = PictureBoxSizeMode.CenterImage
        picKm.TabIndex = 0
        picKm.TabStop = False
        '
        ' lblKmTit
        '
        lblKmTit.AutoSize = True
        lblKmTit.Location = New Point(42, 124)
        lblKmTit.Name = "lblKmTit"
        lblKmTit.Size = New Size(100, 20)
        lblKmTit.TabIndex = 0
        lblKmTit.Text = "Km de ingreso"
        lblKmTit.ForeColor = Color.DimGray
        '
        ' lblKm
        '
        lblKm.AutoEllipsis = True
        lblKm.Location = New Point(160, 124)
        lblKm.Name = "lblKm"
        lblKm.Size = New Size(174, 22)
        lblKm.TabIndex = 0
        lblKm.Text = "-"
        '
        ' picRecepcion
        '
        picRecepcion.Location = New Point(12, 148)
        picRecepcion.Name = "picRecepcion"
        picRecepcion.Size = New Size(26, 28)
        picRecepcion.SizeMode = PictureBoxSizeMode.CenterImage
        picRecepcion.TabIndex = 0
        picRecepcion.TabStop = False
        '
        ' lblRecepcionTit
        '
        lblRecepcionTit.AutoSize = True
        lblRecepcionTit.Location = New Point(42, 152)
        lblRecepcionTit.Name = "lblRecepcionTit"
        lblRecepcionTit.Size = New Size(100, 20)
        lblRecepcionTit.TabIndex = 0
        lblRecepcionTit.Text = "Recepción"
        lblRecepcionTit.ForeColor = Color.DimGray
        '
        ' lblRecepcion
        '
        lblRecepcion.AutoEllipsis = True
        lblRecepcion.Location = New Point(160, 152)
        lblRecepcion.Name = "lblRecepcion"
        lblRecepcion.Size = New Size(174, 22)
        lblRecepcion.TabIndex = 0
        lblRecepcion.Text = "-"
        '
        ' picFinalizacion
        '
        picFinalizacion.Location = New Point(12, 176)
        picFinalizacion.Name = "picFinalizacion"
        picFinalizacion.Size = New Size(26, 28)
        picFinalizacion.SizeMode = PictureBoxSizeMode.CenterImage
        picFinalizacion.TabIndex = 0
        picFinalizacion.TabStop = False
        '
        ' lblFinalizacionTit
        '
        lblFinalizacionTit.AutoSize = True
        lblFinalizacionTit.Location = New Point(42, 180)
        lblFinalizacionTit.Name = "lblFinalizacionTit"
        lblFinalizacionTit.Size = New Size(100, 20)
        lblFinalizacionTit.TabIndex = 0
        lblFinalizacionTit.Text = "Finalización"
        lblFinalizacionTit.ForeColor = Color.DimGray
        '
        ' lblFinalizacion
        '
        lblFinalizacion.AutoEllipsis = True
        lblFinalizacion.Location = New Point(160, 180)
        lblFinalizacion.Name = "lblFinalizacion"
        lblFinalizacion.Size = New Size(174, 22)
        lblFinalizacion.TabIndex = 0
        lblFinalizacion.Text = "-"
        '
        ' picEntrega
        '
        picEntrega.Location = New Point(12, 204)
        picEntrega.Name = "picEntrega"
        picEntrega.Size = New Size(26, 28)
        picEntrega.SizeMode = PictureBoxSizeMode.CenterImage
        picEntrega.TabIndex = 0
        picEntrega.TabStop = False
        '
        ' lblEntregaTit
        '
        lblEntregaTit.AutoSize = True
        lblEntregaTit.Location = New Point(42, 208)
        lblEntregaTit.Name = "lblEntregaTit"
        lblEntregaTit.Size = New Size(100, 20)
        lblEntregaTit.TabIndex = 0
        lblEntregaTit.Text = "Entrega"
        lblEntregaTit.ForeColor = Color.DimGray
        '
        ' lblEntrega
        '
        lblEntrega.AutoEllipsis = True
        lblEntrega.Location = New Point(160, 208)
        lblEntrega.Name = "lblEntrega"
        lblEntrega.Size = New Size(174, 22)
        lblEntrega.TabIndex = 0
        lblEntrega.Text = "-"
        '
        ' picSintoma
        '
        picSintoma.Location = New Point(12, 234)
        picSintoma.Name = "picSintoma"
        picSintoma.Size = New Size(26, 28)
        picSintoma.SizeMode = PictureBoxSizeMode.CenterImage
        picSintoma.TabIndex = 0
        picSintoma.TabStop = False
        '
        ' lblSintomaTit
        '
        lblSintomaTit.AutoSize = True
        lblSintomaTit.Location = New Point(42, 238)
        lblSintomaTit.Name = "lblSintomaTit"
        lblSintomaTit.Size = New Size(100, 20)
        lblSintomaTit.TabIndex = 0
        lblSintomaTit.Text = "Síntoma reportado"
        lblSintomaTit.ForeColor = Color.DimGray
        '
        ' txtSintoma
        '
        txtSintoma.BackColor = Color.White
        txtSintoma.Location = New Point(16, 264)
        txtSintoma.Multiline = True
        txtSintoma.Name = "txtSintoma"
        txtSintoma.ReadOnly = True
        txtSintoma.ScrollBars = ScrollBars.Vertical
        txtSintoma.Size = New Size(318, 52)
        txtSintoma.TabIndex = 3
        txtSintoma.TabStop = False
        '
        ' lblObsRecepcionTit
        '
        lblObsRecepcionTit.AutoSize = True
        lblObsRecepcionTit.Location = New Point(16, 322)
        lblObsRecepcionTit.Name = "lblObsRecepcionTit"
        lblObsRecepcionTit.Size = New Size(100, 20)
        lblObsRecepcionTit.TabIndex = 0
        lblObsRecepcionTit.Text = "Observaciones de recepción"
        lblObsRecepcionTit.ForeColor = Color.DimGray
        '
        ' txtObsRecepcion
        '
        txtObsRecepcion.BackColor = Color.White
        txtObsRecepcion.Location = New Point(16, 346)
        txtObsRecepcion.Multiline = True
        txtObsRecepcion.Name = "txtObsRecepcion"
        txtObsRecepcion.ReadOnly = True
        txtObsRecepcion.ScrollBars = ScrollBars.Vertical
        txtObsRecepcion.Size = New Size(318, 44)
        txtObsRecepcion.TabIndex = 4
        txtObsRecepcion.TabStop = False
        '
        ' pnlHistorial
        '
        pnlHistorial.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pnlHistorial.BackColor = Color.White
        pnlHistorial.Controls.Add(picHistorial)
        pnlHistorial.Controls.Add(lblHistorialTitulo)
        pnlHistorial.Controls.Add(dgvHistorial)
        pnlHistorial.Location = New Point(808, 578)
        pnlHistorial.Name = "pnlHistorial"
        pnlHistorial.Size = New Size(348, 202)
        pnlHistorial.TabIndex = 7
        '
        ' picHistorial
        '
        picHistorial.Location = New Point(12, 6)
        picHistorial.Name = "picHistorial"
        picHistorial.Size = New Size(26, 28)
        picHistorial.SizeMode = PictureBoxSizeMode.CenterImage
        picHistorial.TabIndex = 0
        picHistorial.TabStop = False
        '
        ' lblHistorialTitulo
        '
        lblHistorialTitulo.AutoSize = True
        lblHistorialTitulo.Location = New Point(42, 10)
        lblHistorialTitulo.Name = "lblHistorialTitulo"
        lblHistorialTitulo.Size = New Size(100, 20)
        lblHistorialTitulo.TabIndex = 0
        lblHistorialTitulo.Text = "Historial"
        lblHistorialTitulo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        '
        ' dgvHistorial
        '
        dgvHistorial.AllowUserToAddRows = False
        dgvHistorial.AllowUserToDeleteRows = False
        dgvHistorial.AllowUserToResizeColumns = False
        dgvHistorial.AllowUserToResizeRows = False
        dgvHistorial.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvHistorial.BackgroundColor = Color.White
        dgvHistorial.BorderStyle = BorderStyle.None
        dgvHistorial.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvHistorial.ColumnHeadersVisible = False
        dgvHistorial.Columns.AddRange(New DataGridViewColumn() {colHistorial})
        dgvHistorial.GridColor = Color.Gainsboro
        dgvHistorial.Location = New Point(12, 40)
        dgvHistorial.MultiSelect = False
        dgvHistorial.Name = "dgvHistorial"
        dgvHistorial.ReadOnly = True
        dgvHistorial.RowHeadersVisible = False
        dgvHistorial.RowHeadersWidth = 51
        dgvHistorial.RowTemplate.Height = 40
        dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistorial.Size = New Size(324, 152)
        dgvHistorial.TabIndex = 2
        dgvHistorial.TabStop = False
        '
        ' colHistorial
        '
        colHistorial.DataPropertyName = "titulo"
        colHistorial.HeaderText = "Historial"
        colHistorial.MinimumWidth = 6
        colHistorial.Name = "colHistorial"
        colHistorial.ReadOnly = True
        '
        ' btnAnular
        '
        btnAnular.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnAnular.BackColor = Color.White
        btnAnular.Cursor = Cursors.Hand
        btnAnular.FlatAppearance.BorderColor = Color.Silver
        btnAnular.FlatStyle = FlatStyle.Flat
        btnAnular.Font = New Font("Segoe UI", 10F)
        btnAnular.ForeColor = Color.Firebrick
        btnAnular.Location = New Point(24, 800)
        btnAnular.Name = "btnAnular"
        btnAnular.Size = New Size(180, 42)
        btnAnular.TabIndex = 8
        btnAnular.Text = " Anular orden"
        btnAnular.TextImageRelation = TextImageRelation.ImageBeforeText
        btnAnular.UseVisualStyleBackColor = False
        btnAnular.Visible = False
        '
        ' lblProximo
        '
        lblProximo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lblProximo.ForeColor = Color.DimGray
        lblProximo.Location = New Point(560, 810)
        lblProximo.Name = "lblProximo"
        lblProximo.Size = New Size(140, 24)
        lblProximo.TabIndex = 9
        lblProximo.Text = "Próximo paso"
        lblProximo.TextAlign = ContentAlignment.MiddleRight
        '
        ' btnSecundario
        '
        btnSecundario.BackColor = Color.White
        btnSecundario.Cursor = Cursors.Hand
        btnSecundario.FlatAppearance.BorderColor = Color.Silver
        btnSecundario.FlatStyle = FlatStyle.Flat
        btnSecundario.Name = "btnSecundario"
        btnSecundario.TabIndex = 10
        btnSecundario.Text = " Guardar avance"
        btnSecundario.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSecundario.UseVisualStyleBackColor = False
        btnSecundario.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSecundario.Font = New Font("Segoe UI", 10F)
        btnSecundario.Location = New Point(708, 800)
        btnSecundario.Size = New Size(190, 42)
        btnSecundario.Visible = False
        '
        ' btnPrimario
        '
        btnPrimario.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnPrimario.Cursor = Cursors.Hand
        btnPrimario.FlatAppearance.BorderSize = 0
        btnPrimario.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnPrimario.FlatStyle = FlatStyle.Flat
        btnPrimario.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnPrimario.ForeColor = Color.White
        btnPrimario.Name = "btnPrimario"
        btnPrimario.TabIndex = 11
        btnPrimario.Text = " Cerrar"
        btnPrimario.TextImageRelation = TextImageRelation.ImageBeforeText
        btnPrimario.UseVisualStyleBackColor = False
        btnPrimario.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnPrimario.Location = New Point(906, 800)
        btnPrimario.Size = New Size(250, 42)
        '
        ' FrmOrdenGestion
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1180, 856)
        Controls.Add(btnPrimario)
        Controls.Add(btnSecundario)
        Controls.Add(lblProximo)
        Controls.Add(btnAnular)
        Controls.Add(pnlHistorial)
        Controls.Add(pnlDatos)
        Controls.Add(pnlTrabajo)
        Controls.Add(lblCierre)
        Controls.Add(tlpEtapas)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblEstado)
        Controls.Add(lblTitulo)
        MinimizeBox = False
        MinimumSize = New Size(1000, 780)
        Name = "FrmOrdenGestion"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Gestión de la orden de trabajo"
        tlpEtapas.ResumeLayout(False)
        pnlEtapa1.ResumeLayout(False)
        pnlEtapa2.ResumeLayout(False)
        pnlEtapa3.ResumeLayout(False)
        pnlEtapa4.ResumeLayout(False)
        pnlEtapa5.ResumeLayout(False)
        pnlEtapa6.ResumeLayout(False)
        pnlTrabajo.ResumeLayout(False)
        pnlPieLineas.ResumeLayout(False)
        pnlAvisoMecanico.ResumeLayout(False)
        pnlNotas.ResumeLayout(False)
        pnlNotas.PerformLayout()
        pnlEditor.ResumeLayout(False)
        pnlEditor.PerformLayout()
        pnlCabeceraEtapa.ResumeLayout(False)
        pnlCabeceraEtapa.PerformLayout()
        pnlDatos.ResumeLayout(False)
        pnlDatos.PerformLayout()
        pnlHistorial.ResumeLayout(False)
        pnlHistorial.PerformLayout()
        CType(dgvDetalle, ComponentModel.ISupportInitialize).EndInit()
        CType(picAvisoMecanico, ComponentModel.ISupportInitialize).EndInit()
        CType(nudCantidad, ComponentModel.ISupportInitialize).EndInit()
        CType(picMecanico, ComponentModel.ISupportInitialize).EndInit()
        CType(picKm, ComponentModel.ISupportInitialize).EndInit()
        CType(picRecepcion, ComponentModel.ISupportInitialize).EndInit()
        CType(picFinalizacion, ComponentModel.ISupportInitialize).EndInit()
        CType(picEntrega, ComponentModel.ISupportInitialize).EndInit()
        CType(picSintoma, ComponentModel.ISupportInitialize).EndInit()
        CType(picHistorial, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvHistorial, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents tlpEtapas As TableLayoutPanel
    Friend WithEvents pnlEtapa1 As Panel
    Friend WithEvents lblNomEtapa1 As Label
    Friend WithEvents lblMarcaEtapa1 As Label
    Friend WithEvents pnlEtapa2 As Panel
    Friend WithEvents lblNomEtapa2 As Label
    Friend WithEvents lblMarcaEtapa2 As Label
    Friend WithEvents pnlEtapa3 As Panel
    Friend WithEvents lblNomEtapa3 As Label
    Friend WithEvents lblMarcaEtapa3 As Label
    Friend WithEvents pnlEtapa4 As Panel
    Friend WithEvents lblNomEtapa4 As Label
    Friend WithEvents lblMarcaEtapa4 As Label
    Friend WithEvents pnlEtapa5 As Panel
    Friend WithEvents lblNomEtapa5 As Label
    Friend WithEvents lblMarcaEtapa5 As Label
    Friend WithEvents pnlEtapa6 As Panel
    Friend WithEvents lblNomEtapa6 As Label
    Friend WithEvents lblMarcaEtapa6 As Label
    Friend WithEvents lblCierre As Label
    Friend WithEvents pnlTrabajo As Panel
    Friend WithEvents dgvDetalle As DataGridView
    Friend WithEvents colMarca As DataGridViewImageColumn
    Friend WithEvents colDescripcion As DataGridViewTextBoxColumn
    Friend WithEvents colCantidad As DataGridViewTextBoxColumn
    Friend WithEvents colPrecio As DataGridViewTextBoxColumn
    Friend WithEvents colSubtotal As DataGridViewTextBoxColumn
    Friend WithEvents colAprobado As DataGridViewCheckBoxColumn
    Friend WithEvents colHoras As DataGridViewTextBoxColumn
    Friend WithEvents colIdDetalle As DataGridViewTextBoxColumn
    Friend WithEvents pnlPieLineas As Panel
    Friend WithEvents btnActualizar As Button
    Friend WithEvents btnQuitar As Button
    Friend WithEvents lblTotalPresupuestado As Label
    Friend WithEvents lblTotalAprobado As Label
    Friend WithEvents pnlAvisoMecanico As Panel
    Friend WithEvents picAvisoMecanico As PictureBox
    Friend WithEvents lblAvisoMecanico As Label
    Friend WithEvents pnlNotas As Panel
    Friend WithEvents lblNotas As Label
    Friend WithEvents txtNotasTecnicas As TextBox
    Friend WithEvents pnlEditor As Panel
    Friend WithEvents lblServicio As Label
    Friend WithEvents cboServicio As ComboBox
    Friend WithEvents lblCantidad As Label
    Friend WithEvents nudCantidad As NumericUpDown
    Friend WithEvents btnAgregar As Button
    Friend WithEvents pnlCabeceraEtapa As Panel
    Friend WithEvents lblEtapaTitulo As Label
    Friend WithEvents lblConteo As Label
    Friend WithEvents lblEtapaAyuda As Label
    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblDatosTitulo As Label
    Friend WithEvents picMecanico As PictureBox
    Friend WithEvents lblMecanicoTit As Label
    Friend WithEvents cboMecanico As ComboBox
    Friend WithEvents lblMecanico As Label
    Friend WithEvents btnGuardarMecanico As Button
    Friend WithEvents picKm As PictureBox
    Friend WithEvents lblKmTit As Label
    Friend WithEvents lblKm As Label
    Friend WithEvents picRecepcion As PictureBox
    Friend WithEvents lblRecepcionTit As Label
    Friend WithEvents lblRecepcion As Label
    Friend WithEvents picFinalizacion As PictureBox
    Friend WithEvents lblFinalizacionTit As Label
    Friend WithEvents lblFinalizacion As Label
    Friend WithEvents picEntrega As PictureBox
    Friend WithEvents lblEntregaTit As Label
    Friend WithEvents lblEntrega As Label
    Friend WithEvents picSintoma As PictureBox
    Friend WithEvents lblSintomaTit As Label
    Friend WithEvents txtSintoma As TextBox
    Friend WithEvents lblObsRecepcionTit As Label
    Friend WithEvents txtObsRecepcion As TextBox
    Friend WithEvents pnlHistorial As Panel
    Friend WithEvents picHistorial As PictureBox
    Friend WithEvents lblHistorialTitulo As Label
    Friend WithEvents dgvHistorial As DataGridView
    Friend WithEvents colHistorial As DataGridViewTextBoxColumn
    Friend WithEvents btnAnular As Button
    Friend WithEvents lblProximo As Label
    Friend WithEvents btnSecundario As Button
    Friend WithEvents btnPrimario As Button
End Class

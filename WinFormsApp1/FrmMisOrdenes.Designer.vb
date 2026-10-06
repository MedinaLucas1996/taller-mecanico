<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMisOrdenes
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
        tlpGrupos = New TableLayoutPanel()
        pnlGrupo1 = New Panel()
        lblNomGrupo1 = New Label()
        lblCantGrupo1 = New Label()
        pnlGrupo2 = New Panel()
        lblNomGrupo2 = New Label()
        lblCantGrupo2 = New Label()
        pnlGrupo3 = New Panel()
        lblNomGrupo3 = New Label()
        lblCantGrupo3 = New Label()
        pnlLista = New Panel()
        dgvOrdenes = New DataGridView()
        pnlResumen = New Panel()
        lblAyuda = New Label()
        pnlOrden = New Panel()
        lblNumero = New Label()
        lblEstado = New Label()
        tlpResumen = New TableLayoutPanel()
        picVehiculo = New PictureBox()
        lblVehiculo = New Label()
        picSintoma = New PictureBox()
        lblSintomaTit = New Label()
        txtSintoma = New TextBox()
        lblObsTit = New Label()
        txtObsRecepcion = New TextBox()
        lblTrabajoTit = New Label()
        txtTrabajo = New TextBox()
        pnlFotos = New Panel()
        lblSinFrente = New Label()
        picFrente = New PictureBox()
        lblSinTrasera = New Label()
        picTrasera = New PictureBox()
        lblSinLateralIzq = New Label()
        picLateralIzq = New PictureBox()
        lblSinLateralDer = New Label()
        picLateralDer = New PictureBox()
        lblSinTablero = New Label()
        picTablero = New PictureBox()
        btnAbrir = New Button()
        tlpGrupos.SuspendLayout()
        pnlGrupo1.SuspendLayout()
        pnlGrupo2.SuspendLayout()
        pnlGrupo3.SuspendLayout()
        pnlLista.SuspendLayout()
        pnlResumen.SuspendLayout()
        pnlOrden.SuspendLayout()
        tlpResumen.SuspendLayout()
        pnlFotos.SuspendLayout()
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).BeginInit()
        CType(picVehiculo, ComponentModel.ISupportInitialize).BeginInit()
        CType(picSintoma, ComponentModel.ISupportInitialize).BeginInit()
        CType(picFrente, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTrasera, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralIzq, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralDer, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTablero, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(700, 60)
        lblTitulo.TabIndex = 3
        lblTitulo.Text = "Mis órdenes asignadas"
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
        ' tlpGrupos
        '
        tlpGrupos.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tlpGrupos.ColumnCount = 3
        tlpGrupos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        tlpGrupos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        tlpGrupos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        tlpGrupos.Controls.Add(pnlGrupo1, 0, 0)
        tlpGrupos.Controls.Add(pnlGrupo2, 1, 0)
        tlpGrupos.Controls.Add(pnlGrupo3, 2, 0)
        tlpGrupos.Location = New Point(30, 104)
        tlpGrupos.Name = "tlpGrupos"
        tlpGrupos.RowCount = 1
        tlpGrupos.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpGrupos.Size = New Size(810, 76)
        tlpGrupos.TabIndex = 0
        '
        ' pnlGrupo1
        '
        pnlGrupo1.BackColor = Color.White
        pnlGrupo1.Controls.Add(lblNomGrupo1)
        pnlGrupo1.Controls.Add(lblCantGrupo1)
        pnlGrupo1.Cursor = Cursors.Hand
        pnlGrupo1.Dock = DockStyle.Fill
        pnlGrupo1.Location = New Point(0, 0)
        pnlGrupo1.Margin = New Padding(0, 0, 6, 0)
        pnlGrupo1.Name = "pnlGrupo1"
        pnlGrupo1.Size = New Size(264, 76)
        pnlGrupo1.TabIndex = 0
        '
        ' lblNomGrupo1
        '
        lblNomGrupo1.Dock = DockStyle.Fill
        lblNomGrupo1.Font = New Font("Segoe UI", 8.25F)
        lblNomGrupo1.ForeColor = Color.DimGray
        lblNomGrupo1.Location = New Point(0, 46)
        lblNomGrupo1.Name = "lblNomGrupo1"
        lblNomGrupo1.Size = New Size(264, 30)
        lblNomGrupo1.TabIndex = 1
        lblNomGrupo1.Text = "En curso"
        lblNomGrupo1.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCantGrupo1
        '
        lblCantGrupo1.Dock = DockStyle.Top
        lblCantGrupo1.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCantGrupo1.Location = New Point(0, 0)
        lblCantGrupo1.Name = "lblCantGrupo1"
        lblCantGrupo1.Size = New Size(264, 46)
        lblCantGrupo1.TabIndex = 0
        lblCantGrupo1.Text = "0"
        lblCantGrupo1.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlGrupo2
        '
        pnlGrupo2.BackColor = Color.White
        pnlGrupo2.Controls.Add(lblNomGrupo2)
        pnlGrupo2.Controls.Add(lblCantGrupo2)
        pnlGrupo2.Cursor = Cursors.Hand
        pnlGrupo2.Dock = DockStyle.Fill
        pnlGrupo2.Location = New Point(0, 0)
        pnlGrupo2.Margin = New Padding(0, 0, 6, 0)
        pnlGrupo2.Name = "pnlGrupo2"
        pnlGrupo2.Size = New Size(264, 76)
        pnlGrupo2.TabIndex = 1
        '
        ' lblNomGrupo2
        '
        lblNomGrupo2.Dock = DockStyle.Fill
        lblNomGrupo2.Font = New Font("Segoe UI", 8.25F)
        lblNomGrupo2.ForeColor = Color.DimGray
        lblNomGrupo2.Location = New Point(0, 46)
        lblNomGrupo2.Name = "lblNomGrupo2"
        lblNomGrupo2.Size = New Size(264, 30)
        lblNomGrupo2.TabIndex = 1
        lblNomGrupo2.Text = "Para empezar"
        lblNomGrupo2.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCantGrupo2
        '
        lblCantGrupo2.Dock = DockStyle.Top
        lblCantGrupo2.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCantGrupo2.Location = New Point(0, 0)
        lblCantGrupo2.Name = "lblCantGrupo2"
        lblCantGrupo2.Size = New Size(264, 46)
        lblCantGrupo2.TabIndex = 0
        lblCantGrupo2.Text = "0"
        lblCantGrupo2.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlGrupo3
        '
        pnlGrupo3.BackColor = Color.White
        pnlGrupo3.Controls.Add(lblNomGrupo3)
        pnlGrupo3.Controls.Add(lblCantGrupo3)
        pnlGrupo3.Cursor = Cursors.Hand
        pnlGrupo3.Dock = DockStyle.Fill
        pnlGrupo3.Location = New Point(0, 0)
        pnlGrupo3.Margin = New Padding(0, 0, 0, 0)
        pnlGrupo3.Name = "pnlGrupo3"
        pnlGrupo3.Size = New Size(264, 76)
        pnlGrupo3.TabIndex = 2
        '
        ' lblNomGrupo3
        '
        lblNomGrupo3.Dock = DockStyle.Fill
        lblNomGrupo3.Font = New Font("Segoe UI", 8.25F)
        lblNomGrupo3.ForeColor = Color.DimGray
        lblNomGrupo3.Location = New Point(0, 46)
        lblNomGrupo3.Name = "lblNomGrupo3"
        lblNomGrupo3.Size = New Size(264, 30)
        lblNomGrupo3.TabIndex = 1
        lblNomGrupo3.Text = "Terminadas"
        lblNomGrupo3.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCantGrupo3
        '
        lblCantGrupo3.Dock = DockStyle.Top
        lblCantGrupo3.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblCantGrupo3.Location = New Point(0, 0)
        lblCantGrupo3.Name = "lblCantGrupo3"
        lblCantGrupo3.Size = New Size(264, 46)
        lblCantGrupo3.TabIndex = 0
        lblCantGrupo3.Text = "0"
        lblCantGrupo3.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlLista
        '
        pnlLista.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlLista.BackColor = Color.White
        pnlLista.Controls.Add(dgvOrdenes)
        pnlLista.Location = New Point(30, 190)
        pnlLista.Name = "pnlLista"
        pnlLista.Size = New Size(420, 430)
        pnlLista.TabIndex = 1
        '
        ' dgvOrdenes
        '
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.White
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.DimGray
        DataGridViewCellStyle1.SelectionBackColor = Color.White
        DataGridViewCellStyle1.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        dgvOrdenes.AllowUserToAddRows = False
        dgvOrdenes.AllowUserToDeleteRows = False
        dgvOrdenes.AllowUserToResizeRows = False
        dgvOrdenes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOrdenes.BackgroundColor = Color.White
        dgvOrdenes.BorderStyle = BorderStyle.None
        dgvOrdenes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvOrdenes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvOrdenes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvOrdenes.ColumnHeadersHeight = 40
        dgvOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvOrdenes.EnableHeadersVisualStyles = False
        dgvOrdenes.GridColor = Color.Gainsboro
        dgvOrdenes.Location = New Point(16, 8)
        dgvOrdenes.MultiSelect = False
        dgvOrdenes.Name = "dgvOrdenes"
        dgvOrdenes.ReadOnly = True
        dgvOrdenes.RowHeadersVisible = False
        dgvOrdenes.RowHeadersWidth = 51
        dgvOrdenes.RowTemplate.Height = 26
        dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrdenes.Size = New Size(388, 406)
        dgvOrdenes.StandardTab = True
        dgvOrdenes.TabIndex = 0
        '
        ' pnlResumen
        '
        pnlResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pnlResumen.BackColor = Color.White
        pnlResumen.Controls.Add(lblAyuda)
        pnlResumen.Controls.Add(pnlOrden)
        pnlResumen.Location = New Point(460, 190)
        pnlResumen.Name = "pnlResumen"
        pnlResumen.Size = New Size(380, 430)
        pnlResumen.TabIndex = 2
        '
        ' lblAyuda
        '
        lblAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(16, 16)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(348, 48)
        lblAyuda.TabIndex = 1
        lblAyuda.Text = "Seleccioná una orden de la lista para ver el trabajo a realizar."
        '
        ' pnlOrden
        '
        pnlOrden.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlOrden.Controls.Add(lblNumero)
        pnlOrden.Controls.Add(lblEstado)
        pnlOrden.Controls.Add(tlpResumen)
        pnlOrden.Controls.Add(btnAbrir)
        pnlOrden.Location = New Point(0, 0)
        pnlOrden.Name = "pnlOrden"
        pnlOrden.Size = New Size(380, 430)
        pnlOrden.TabIndex = 0
        pnlOrden.Visible = False
        '
        ' lblNumero
        '
        lblNumero.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblNumero.AutoEllipsis = True
        lblNumero.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblNumero.Location = New Point(16, 10)
        lblNumero.Name = "lblNumero"
        lblNumero.Size = New Size(198, 30)
        lblNumero.TabIndex = 2
        lblNumero.Text = "Orden N.º"
        lblNumero.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblEstado
        '
        lblEstado.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblEstado.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        lblEstado.Location = New Point(224, 12)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(140, 26)
        lblEstado.TabIndex = 3
        lblEstado.Text = "-"
        lblEstado.TextAlign = ContentAlignment.MiddleCenter
        '
        ' tlpResumen
        '
        tlpResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpResumen.ColumnCount = 2
        tlpResumen.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 30F))
        tlpResumen.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpResumen.Controls.Add(picVehiculo, 0, 0)
        tlpResumen.Controls.Add(lblVehiculo, 1, 0)
        tlpResumen.Controls.Add(picSintoma, 0, 1)
        tlpResumen.Controls.Add(lblSintomaTit, 1, 1)
        tlpResumen.Controls.Add(txtSintoma, 0, 2)
        tlpResumen.Controls.Add(lblObsTit, 0, 3)
        tlpResumen.Controls.Add(txtObsRecepcion, 0, 4)
        tlpResumen.Controls.Add(lblTrabajoTit, 0, 5)
        tlpResumen.Controls.Add(txtTrabajo, 0, 6)
        tlpResumen.Controls.Add(pnlFotos, 0, 7)
        tlpResumen.Location = New Point(16, 46)
        tlpResumen.Name = "tlpResumen"
        tlpResumen.RowCount = 8
        tlpResumen.RowStyles.Add(New RowStyle())
        tlpResumen.RowStyles.Add(New RowStyle())
        tlpResumen.RowStyles.Add(New RowStyle())
        tlpResumen.RowStyles.Add(New RowStyle())
        tlpResumen.RowStyles.Add(New RowStyle())
        tlpResumen.RowStyles.Add(New RowStyle())
        tlpResumen.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpResumen.RowStyles.Add(New RowStyle())
        tlpResumen.Size = New Size(348, 318)
        tlpResumen.TabIndex = 0
        tlpResumen.SetColumnSpan(txtSintoma, 2)
        tlpResumen.SetColumnSpan(lblObsTit, 2)
        tlpResumen.SetColumnSpan(txtObsRecepcion, 2)
        tlpResumen.SetColumnSpan(lblTrabajoTit, 2)
        tlpResumen.SetColumnSpan(txtTrabajo, 2)
        tlpResumen.SetColumnSpan(pnlFotos, 2)
        '
        ' picVehiculo
        '
        picVehiculo.Location = New Point(0, 0)
        picVehiculo.Margin = New Padding(0, 3, 0, 0)
        picVehiculo.Name = "picVehiculo"
        picVehiculo.Size = New Size(24, 24)
        picVehiculo.SizeMode = PictureBoxSizeMode.Zoom
        picVehiculo.TabIndex = 10
        picVehiculo.TabStop = False
        '
        ' lblVehiculo
        '
        lblVehiculo.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        lblVehiculo.AutoEllipsis = True
        lblVehiculo.Location = New Point(30, 0)
        lblVehiculo.Margin = New Padding(0, 4, 0, 4)
        lblVehiculo.Name = "lblVehiculo"
        lblVehiculo.Size = New Size(318, 22)
        lblVehiculo.TabIndex = 11
        lblVehiculo.Text = "-"
        lblVehiculo.TextAlign = ContentAlignment.MiddleLeft
        '
        ' picSintoma
        '
        picSintoma.Location = New Point(0, 0)
        picSintoma.Margin = New Padding(0, 3, 0, 0)
        picSintoma.Name = "picSintoma"
        picSintoma.Size = New Size(24, 24)
        picSintoma.SizeMode = PictureBoxSizeMode.Zoom
        picSintoma.TabIndex = 12
        picSintoma.TabStop = False
        '
        ' lblSintomaTit
        '
        lblSintomaTit.AutoSize = True
        lblSintomaTit.Font = New Font("Segoe UI", 8.25F)
        lblSintomaTit.ForeColor = Color.DimGray
        lblSintomaTit.Location = New Point(0, 0)
        lblSintomaTit.Margin = New Padding(0, 8, 0, 2)
        lblSintomaTit.Name = "lblSintomaTit"
        lblSintomaTit.Size = New Size(100, 19)
        lblSintomaTit.TabIndex = 13
        lblSintomaTit.Text = "El cliente dice"
        '
        ' txtSintoma
        '
        txtSintoma.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtSintoma.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtSintoma.BorderStyle = BorderStyle.None
        txtSintoma.Location = New Point(0, 0)
        txtSintoma.Margin = New Padding(0, 0, 0, 2)
        txtSintoma.Multiline = True
        txtSintoma.Name = "txtSintoma"
        txtSintoma.ReadOnly = True
        txtSintoma.ScrollBars = ScrollBars.Vertical
        txtSintoma.Size = New Size(348, 44)
        txtSintoma.TabIndex = 14
        txtSintoma.TabStop = False
        '
        ' lblObsTit
        '
        lblObsTit.AutoSize = True
        lblObsTit.Font = New Font("Segoe UI", 8.25F)
        lblObsTit.ForeColor = Color.DimGray
        lblObsTit.Location = New Point(0, 0)
        lblObsTit.Margin = New Padding(0, 6, 0, 2)
        lblObsTit.Name = "lblObsTit"
        lblObsTit.Size = New Size(100, 19)
        lblObsTit.TabIndex = 15
        lblObsTit.Text = "Observaciones de recepción"
        '
        ' txtObsRecepcion
        '
        txtObsRecepcion.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtObsRecepcion.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtObsRecepcion.BorderStyle = BorderStyle.None
        txtObsRecepcion.Location = New Point(0, 0)
        txtObsRecepcion.Margin = New Padding(0, 0, 0, 2)
        txtObsRecepcion.Multiline = True
        txtObsRecepcion.Name = "txtObsRecepcion"
        txtObsRecepcion.ReadOnly = True
        txtObsRecepcion.ScrollBars = ScrollBars.Vertical
        txtObsRecepcion.Size = New Size(348, 44)
        txtObsRecepcion.TabIndex = 16
        txtObsRecepcion.TabStop = False
        '
        ' lblTrabajoTit
        '
        lblTrabajoTit.AutoSize = True
        lblTrabajoTit.Font = New Font("Segoe UI", 8.25F)
        lblTrabajoTit.ForeColor = Color.DimGray
        lblTrabajoTit.Location = New Point(0, 0)
        lblTrabajoTit.Margin = New Padding(0, 6, 0, 2)
        lblTrabajoTit.Name = "lblTrabajoTit"
        lblTrabajoTit.Size = New Size(100, 19)
        lblTrabajoTit.TabIndex = 17
        lblTrabajoTit.Text = "Trabajo a realizar"
        '
        ' txtTrabajo
        '
        txtTrabajo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtTrabajo.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtTrabajo.BorderStyle = BorderStyle.None
        txtTrabajo.Location = New Point(0, 0)
        txtTrabajo.Margin = New Padding(0, 0, 0, 2)
        txtTrabajo.MinimumSize = New Size(0, 40)
        txtTrabajo.Multiline = True
        txtTrabajo.Name = "txtTrabajo"
        txtTrabajo.ReadOnly = True
        txtTrabajo.ScrollBars = ScrollBars.Vertical
        txtTrabajo.Size = New Size(348, 60)
        txtTrabajo.TabIndex = 18
        txtTrabajo.TabStop = False
        '
        ' pnlFotos
        '
        pnlFotos.Controls.Add(lblSinFrente)
        pnlFotos.Controls.Add(picFrente)
        pnlFotos.Controls.Add(lblSinTrasera)
        pnlFotos.Controls.Add(picTrasera)
        pnlFotos.Controls.Add(lblSinLateralIzq)
        pnlFotos.Controls.Add(picLateralIzq)
        pnlFotos.Controls.Add(lblSinLateralDer)
        pnlFotos.Controls.Add(picLateralDer)
        pnlFotos.Controls.Add(lblSinTablero)
        pnlFotos.Controls.Add(picTablero)
        pnlFotos.Location = New Point(0, 0)
        pnlFotos.Margin = New Padding(0, 8, 0, 0)
        pnlFotos.Name = "pnlFotos"
        pnlFotos.Size = New Size(348, 58)
        pnlFotos.TabIndex = 19
        '
        ' lblSinFrente
        '
        lblSinFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinFrente.Font = New Font("Segoe UI", 7F)
        lblSinFrente.ForeColor = Color.Gray
        lblSinFrente.Location = New Point(2, 18)
        lblSinFrente.Name = "lblSinFrente"
        lblSinFrente.Size = New Size(52, 20)
        lblSinFrente.TabIndex = 0
        lblSinFrente.Text = "Sin foto"
        lblSinFrente.TextAlign = ContentAlignment.MiddleCenter
        lblSinFrente.Visible = False
        '
        ' picFrente
        '
        picFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picFrente.BorderStyle = BorderStyle.FixedSingle
        picFrente.Location = New Point(0, 0)
        picFrente.Name = "picFrente"
        picFrente.Size = New Size(56, 56)
        picFrente.SizeMode = PictureBoxSizeMode.Zoom
        picFrente.TabIndex = 10
        picFrente.TabStop = False
        '
        ' lblSinTrasera
        '
        lblSinTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinTrasera.Font = New Font("Segoe UI", 7F)
        lblSinTrasera.ForeColor = Color.Gray
        lblSinTrasera.Location = New Point(66, 18)
        lblSinTrasera.Name = "lblSinTrasera"
        lblSinTrasera.Size = New Size(52, 20)
        lblSinTrasera.TabIndex = 1
        lblSinTrasera.Text = "Sin foto"
        lblSinTrasera.TextAlign = ContentAlignment.MiddleCenter
        lblSinTrasera.Visible = False
        '
        ' picTrasera
        '
        picTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTrasera.BorderStyle = BorderStyle.FixedSingle
        picTrasera.Location = New Point(64, 0)
        picTrasera.Name = "picTrasera"
        picTrasera.Size = New Size(56, 56)
        picTrasera.SizeMode = PictureBoxSizeMode.Zoom
        picTrasera.TabIndex = 11
        picTrasera.TabStop = False
        '
        ' lblSinLateralIzq
        '
        lblSinLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinLateralIzq.Font = New Font("Segoe UI", 7F)
        lblSinLateralIzq.ForeColor = Color.Gray
        lblSinLateralIzq.Location = New Point(130, 18)
        lblSinLateralIzq.Name = "lblSinLateralIzq"
        lblSinLateralIzq.Size = New Size(52, 20)
        lblSinLateralIzq.TabIndex = 2
        lblSinLateralIzq.Text = "Sin foto"
        lblSinLateralIzq.TextAlign = ContentAlignment.MiddleCenter
        lblSinLateralIzq.Visible = False
        '
        ' picLateralIzq
        '
        picLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralIzq.BorderStyle = BorderStyle.FixedSingle
        picLateralIzq.Location = New Point(128, 0)
        picLateralIzq.Name = "picLateralIzq"
        picLateralIzq.Size = New Size(56, 56)
        picLateralIzq.SizeMode = PictureBoxSizeMode.Zoom
        picLateralIzq.TabIndex = 12
        picLateralIzq.TabStop = False
        '
        ' lblSinLateralDer
        '
        lblSinLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinLateralDer.Font = New Font("Segoe UI", 7F)
        lblSinLateralDer.ForeColor = Color.Gray
        lblSinLateralDer.Location = New Point(194, 18)
        lblSinLateralDer.Name = "lblSinLateralDer"
        lblSinLateralDer.Size = New Size(52, 20)
        lblSinLateralDer.TabIndex = 3
        lblSinLateralDer.Text = "Sin foto"
        lblSinLateralDer.TextAlign = ContentAlignment.MiddleCenter
        lblSinLateralDer.Visible = False
        '
        ' picLateralDer
        '
        picLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralDer.BorderStyle = BorderStyle.FixedSingle
        picLateralDer.Location = New Point(192, 0)
        picLateralDer.Name = "picLateralDer"
        picLateralDer.Size = New Size(56, 56)
        picLateralDer.SizeMode = PictureBoxSizeMode.Zoom
        picLateralDer.TabIndex = 13
        picLateralDer.TabStop = False
        '
        ' lblSinTablero
        '
        lblSinTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinTablero.Font = New Font("Segoe UI", 7F)
        lblSinTablero.ForeColor = Color.Gray
        lblSinTablero.Location = New Point(258, 18)
        lblSinTablero.Name = "lblSinTablero"
        lblSinTablero.Size = New Size(52, 20)
        lblSinTablero.TabIndex = 4
        lblSinTablero.Text = "Sin foto"
        lblSinTablero.TextAlign = ContentAlignment.MiddleCenter
        lblSinTablero.Visible = False
        '
        ' picTablero
        '
        picTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTablero.BorderStyle = BorderStyle.FixedSingle
        picTablero.Location = New Point(256, 0)
        picTablero.Name = "picTablero"
        picTablero.Size = New Size(56, 56)
        picTablero.SizeMode = PictureBoxSizeMode.Zoom
        picTablero.TabIndex = 14
        picTablero.TabStop = False
        '
        ' btnAbrir
        '
        btnAbrir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnAbrir.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnAbrir.Cursor = Cursors.Hand
        btnAbrir.FlatAppearance.BorderSize = 0
        btnAbrir.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnAbrir.FlatStyle = FlatStyle.Flat
        btnAbrir.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnAbrir.ForeColor = Color.White
        btnAbrir.Location = New Point(16, 374)
        btnAbrir.Name = "btnAbrir"
        btnAbrir.Size = New Size(348, 40)
        btnAbrir.TabIndex = 1
        btnAbrir.Text = " Ver orden"
        btnAbrir.TextImageRelation = TextImageRelation.ImageBeforeText
        btnAbrir.UseVisualStyleBackColor = False
        '
        ' FrmMisOrdenes
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(pnlResumen)
        Controls.Add(pnlLista)
        Controls.Add(tlpGrupos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmMisOrdenes"
        Text = "Mis órdenes"
        tlpGrupos.ResumeLayout(False)
        pnlGrupo1.ResumeLayout(False)
        pnlGrupo2.ResumeLayout(False)
        pnlGrupo3.ResumeLayout(False)
        pnlLista.ResumeLayout(False)
        pnlResumen.ResumeLayout(False)
        pnlOrden.ResumeLayout(False)
        tlpResumen.ResumeLayout(False)
        tlpResumen.PerformLayout()
        pnlFotos.ResumeLayout(False)
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).EndInit()
        CType(picVehiculo, ComponentModel.ISupportInitialize).EndInit()
        CType(picSintoma, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents tlpGrupos As TableLayoutPanel
    Friend WithEvents pnlGrupo1 As Panel
    Friend WithEvents lblNomGrupo1 As Label
    Friend WithEvents lblCantGrupo1 As Label
    Friend WithEvents pnlGrupo2 As Panel
    Friend WithEvents lblNomGrupo2 As Label
    Friend WithEvents lblCantGrupo2 As Label
    Friend WithEvents pnlGrupo3 As Panel
    Friend WithEvents lblNomGrupo3 As Label
    Friend WithEvents lblCantGrupo3 As Label
    Friend WithEvents pnlLista As Panel
    Friend WithEvents dgvOrdenes As DataGridView
    Friend WithEvents pnlResumen As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents pnlOrden As Panel
    Friend WithEvents lblNumero As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents tlpResumen As TableLayoutPanel
    Friend WithEvents picVehiculo As PictureBox
    Friend WithEvents lblVehiculo As Label
    Friend WithEvents picSintoma As PictureBox
    Friend WithEvents lblSintomaTit As Label
    Friend WithEvents txtSintoma As TextBox
    Friend WithEvents lblObsTit As Label
    Friend WithEvents txtObsRecepcion As TextBox
    Friend WithEvents lblTrabajoTit As Label
    Friend WithEvents txtTrabajo As TextBox
    Friend WithEvents pnlFotos As Panel
    Friend WithEvents lblSinFrente As Label
    Friend WithEvents picFrente As PictureBox
    Friend WithEvents lblSinTrasera As Label
    Friend WithEvents picTrasera As PictureBox
    Friend WithEvents lblSinLateralIzq As Label
    Friend WithEvents picLateralIzq As PictureBox
    Friend WithEvents lblSinLateralDer As Label
    Friend WithEvents picLateralDer As PictureBox
    Friend WithEvents lblSinTablero As Label
    Friend WithEvents picTablero As PictureBox
    Friend WithEvents btnAbrir As Button
End Class

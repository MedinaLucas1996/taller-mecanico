<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHistorial
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
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        lblTitulo = New Label()
        lblSubtitulo = New Label()
        pnlLista = New Panel()
        picBuscar = New PictureBox()
        txtFiltro = New TextBox()
        dgvVehiculos = New DataGridView()
        pnlDetalle = New Panel()
        lblAyuda = New Label()
        pnlVehiculo = New Panel()
        lblPatente = New Label()
        lblBaja = New Label()
        picVehiculo = New PictureBox()
        lblVehiculo = New Label()
        picCliente = New PictureBox()
        lblTitular = New Label()
        picKm = New PictureBox()
        lblKm = New Label()
        lblSinOrdenes = New Label()
        tlpHistorial = New TableLayoutPanel()
        lblOrdenesTit = New Label()
        dgvOrdenes = New DataGridView()
        lblServiciosTit = New Label()
        dgvServicios = New DataGridView()
        tlpNotas = New TableLayoutPanel()
        lblSintomaTit = New Label()
        lblObsTit = New Label()
        txtSintoma = New TextBox()
        txtObservaciones = New TextBox()
        pnlLista.SuspendLayout()
        pnlDetalle.SuspendLayout()
        pnlVehiculo.SuspendLayout()
        tlpHistorial.SuspendLayout()
        tlpNotas.SuspendLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvVehiculos, ComponentModel.ISupportInitialize).BeginInit()
        CType(picVehiculo, ComponentModel.ISupportInitialize).BeginInit()
        CType(picCliente, ComponentModel.ISupportInitialize).BeginInit()
        CType(picKm, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvServicios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(700, 60)
        lblTitulo.TabIndex = 2
        lblTitulo.Text = "Historial por patente"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 72)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(480, 25)
        lblSubtitulo.TabIndex = 3
        lblSubtitulo.Text = "Consultá las órdenes de trabajo que tuvo cada vehículo"
        '
        ' pnlLista
        '
        pnlLista.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        pnlLista.BackColor = Color.White
        pnlLista.Controls.Add(picBuscar)
        pnlLista.Controls.Add(txtFiltro)
        pnlLista.Controls.Add(dgvVehiculos)
        pnlLista.Location = New Point(30, 110)
        pnlLista.Name = "pnlLista"
        pnlLista.Size = New Size(380, 510)
        pnlLista.TabIndex = 0
        '
        ' picBuscar
        '
        picBuscar.Location = New Point(16, 18)
        picBuscar.Name = "picBuscar"
        picBuscar.Size = New Size(24, 28)
        picBuscar.SizeMode = PictureBoxSizeMode.Normal
        picBuscar.TabIndex = 2
        picBuscar.TabStop = False
        '
        ' txtFiltro
        '
        txtFiltro.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtFiltro.CharacterCasing = CharacterCasing.Upper
        txtFiltro.Location = New Point(46, 16)
        txtFiltro.MaxLength = 10
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por patente"
        txtFiltro.Size = New Size(318, 27)
        txtFiltro.TabIndex = 0
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
        dgvVehiculos.Location = New Point(16, 56)
        dgvVehiculos.MultiSelect = False
        dgvVehiculos.Name = "dgvVehiculos"
        dgvVehiculos.ReadOnly = True
        dgvVehiculos.RowHeadersVisible = False
        dgvVehiculos.RowHeadersWidth = 51
        dgvVehiculos.RowTemplate.Height = 26
        dgvVehiculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvVehiculos.Size = New Size(348, 438)
        dgvVehiculos.StandardTab = True
        dgvVehiculos.TabIndex = 1
        '
        ' pnlDetalle
        '
        pnlDetalle.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlDetalle.BackColor = Color.White
        pnlDetalle.Controls.Add(lblAyuda)
        pnlDetalle.Controls.Add(pnlVehiculo)
        pnlDetalle.Location = New Point(420, 110)
        pnlDetalle.Name = "pnlDetalle"
        pnlDetalle.Size = New Size(420, 510)
        pnlDetalle.TabIndex = 1
        '
        ' lblAyuda
        '
        lblAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(16, 16)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(388, 48)
        lblAyuda.TabIndex = 1
        lblAyuda.Text = "Buscá un vehículo por su patente y seleccionalo en la lista para ver su historial."
        '
        ' pnlVehiculo
        '
        pnlVehiculo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlVehiculo.Controls.Add(lblPatente)
        pnlVehiculo.Controls.Add(lblBaja)
        pnlVehiculo.Controls.Add(picVehiculo)
        pnlVehiculo.Controls.Add(lblVehiculo)
        pnlVehiculo.Controls.Add(picCliente)
        pnlVehiculo.Controls.Add(lblTitular)
        pnlVehiculo.Controls.Add(picKm)
        pnlVehiculo.Controls.Add(lblKm)
        pnlVehiculo.Controls.Add(lblSinOrdenes)
        pnlVehiculo.Controls.Add(tlpHistorial)
        pnlVehiculo.Location = New Point(0, 0)
        pnlVehiculo.Name = "pnlVehiculo"
        pnlVehiculo.Size = New Size(420, 510)
        pnlVehiculo.TabIndex = 0
        pnlVehiculo.Visible = False
        '
        ' lblPatente
        '
        lblPatente.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblPatente.AutoEllipsis = True
        lblPatente.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblPatente.Location = New Point(16, 10)
        lblPatente.Name = "lblPatente"
        lblPatente.Size = New Size(238, 30)
        lblPatente.TabIndex = 2
        lblPatente.Text = "-"
        lblPatente.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblBaja
        '
        lblBaja.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblBaja.BackColor = Color.Gainsboro
        lblBaja.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        lblBaja.Location = New Point(264, 12)
        lblBaja.Name = "lblBaja"
        lblBaja.Size = New Size(140, 26)
        lblBaja.TabIndex = 3
        lblBaja.Text = "Dado de baja"
        lblBaja.TextAlign = ContentAlignment.MiddleCenter
        lblBaja.Visible = False
        '
        ' picVehiculo
        '
        picVehiculo.Location = New Point(16, 44)
        picVehiculo.Name = "picVehiculo"
        picVehiculo.Size = New Size(24, 24)
        picVehiculo.SizeMode = PictureBoxSizeMode.Zoom
        picVehiculo.TabIndex = 4
        picVehiculo.TabStop = False
        '
        ' lblVehiculo
        '
        lblVehiculo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblVehiculo.AutoEllipsis = True
        lblVehiculo.Location = New Point(46, 44)
        lblVehiculo.Name = "lblVehiculo"
        lblVehiculo.Size = New Size(358, 24)
        lblVehiculo.TabIndex = 5
        lblVehiculo.Text = "-"
        lblVehiculo.TextAlign = ContentAlignment.MiddleLeft
        '
        ' picCliente
        '
        picCliente.Location = New Point(16, 70)
        picCliente.Name = "picCliente"
        picCliente.Size = New Size(24, 24)
        picCliente.SizeMode = PictureBoxSizeMode.Zoom
        picCliente.TabIndex = 6
        picCliente.TabStop = False
        '
        ' lblTitular
        '
        lblTitular.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblTitular.AutoEllipsis = True
        lblTitular.Location = New Point(46, 70)
        lblTitular.Name = "lblTitular"
        lblTitular.Size = New Size(358, 24)
        lblTitular.TabIndex = 7
        lblTitular.Text = "-"
        lblTitular.TextAlign = ContentAlignment.MiddleLeft
        '
        ' picKm
        '
        picKm.Location = New Point(16, 96)
        picKm.Name = "picKm"
        picKm.Size = New Size(24, 24)
        picKm.SizeMode = PictureBoxSizeMode.Zoom
        picKm.TabIndex = 8
        picKm.TabStop = False
        '
        ' lblKm
        '
        lblKm.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblKm.AutoEllipsis = True
        lblKm.Location = New Point(46, 96)
        lblKm.Name = "lblKm"
        lblKm.Size = New Size(358, 24)
        lblKm.TabIndex = 9
        lblKm.Text = "-"
        lblKm.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblSinOrdenes
        '
        lblSinOrdenes.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblSinOrdenes.ForeColor = Color.DimGray
        lblSinOrdenes.Location = New Point(16, 132)
        lblSinOrdenes.Name = "lblSinOrdenes"
        lblSinOrdenes.Size = New Size(388, 48)
        lblSinOrdenes.TabIndex = 1
        lblSinOrdenes.Text = "Este vehículo todavía no tiene órdenes de trabajo."
        lblSinOrdenes.Visible = False
        '
        ' tlpHistorial
        '
        tlpHistorial.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpHistorial.ColumnCount = 1
        tlpHistorial.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpHistorial.Controls.Add(lblOrdenesTit, 0, 0)
        tlpHistorial.Controls.Add(dgvOrdenes, 0, 1)
        tlpHistorial.Controls.Add(lblServiciosTit, 0, 2)
        tlpHistorial.Controls.Add(dgvServicios, 0, 3)
        tlpHistorial.Controls.Add(tlpNotas, 0, 4)
        tlpHistorial.Location = New Point(16, 128)
        tlpHistorial.Name = "tlpHistorial"
        tlpHistorial.RowCount = 5
        tlpHistorial.RowStyles.Add(New RowStyle())
        tlpHistorial.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpHistorial.RowStyles.Add(New RowStyle())
        tlpHistorial.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpHistorial.RowStyles.Add(New RowStyle(SizeType.Absolute, 96F))
        tlpHistorial.Size = New Size(388, 366)
        tlpHistorial.TabIndex = 0
        '
        ' lblOrdenesTit
        '
        lblOrdenesTit.AutoSize = True
        lblOrdenesTit.Font = New Font("Segoe UI", 8.25F)
        lblOrdenesTit.ForeColor = Color.DimGray
        lblOrdenesTit.Location = New Point(0, 0)
        lblOrdenesTit.Margin = New Padding(0, 0, 0, 4)
        lblOrdenesTit.Name = "lblOrdenesTit"
        lblOrdenesTit.Size = New Size(130, 19)
        lblOrdenesTit.TabIndex = 3
        lblOrdenesTit.Text = "Órdenes de trabajo"
        '
        ' dgvOrdenes
        '
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.White
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = Color.DimGray
        DataGridViewCellStyle2.SelectionBackColor = Color.White
        DataGridViewCellStyle2.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        dgvOrdenes.AllowUserToAddRows = False
        dgvOrdenes.AllowUserToDeleteRows = False
        dgvOrdenes.AllowUserToResizeRows = False
        dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOrdenes.BackgroundColor = Color.White
        dgvOrdenes.BorderStyle = BorderStyle.None
        dgvOrdenes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvOrdenes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvOrdenes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvOrdenes.ColumnHeadersHeight = 40
        dgvOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvOrdenes.Dock = DockStyle.Fill
        dgvOrdenes.EnableHeadersVisualStyles = False
        dgvOrdenes.GridColor = Color.Gainsboro
        dgvOrdenes.Location = New Point(0, 23)
        dgvOrdenes.Margin = New Padding(0)
        dgvOrdenes.MultiSelect = False
        dgvOrdenes.Name = "dgvOrdenes"
        dgvOrdenes.ReadOnly = True
        dgvOrdenes.RowHeadersVisible = False
        dgvOrdenes.RowHeadersWidth = 51
        dgvOrdenes.RowTemplate.Height = 26
        dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrdenes.Size = New Size(388, 110)
        dgvOrdenes.StandardTab = True
        dgvOrdenes.TabIndex = 0
        '
        ' lblServiciosTit
        '
        lblServiciosTit.AutoSize = True
        lblServiciosTit.Font = New Font("Segoe UI", 8.25F)
        lblServiciosTit.ForeColor = Color.DimGray
        lblServiciosTit.Location = New Point(0, 143)
        lblServiciosTit.Margin = New Padding(0, 10, 0, 4)
        lblServiciosTit.Name = "lblServiciosTit"
        lblServiciosTit.Size = New Size(150, 19)
        lblServiciosTit.TabIndex = 4
        lblServiciosTit.Text = "Servicios de la orden"
        '
        ' dgvServicios
        '
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.White
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle3.ForeColor = Color.DimGray
        DataGridViewCellStyle3.SelectionBackColor = Color.White
        DataGridViewCellStyle3.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvServicios.AllowUserToAddRows = False
        dgvServicios.AllowUserToDeleteRows = False
        dgvServicios.AllowUserToResizeRows = False
        dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvServicios.BackgroundColor = Color.White
        dgvServicios.BorderStyle = BorderStyle.None
        dgvServicios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvServicios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvServicios.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        dgvServicios.ColumnHeadersHeight = 40
        dgvServicios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvServicios.Dock = DockStyle.Fill
        dgvServicios.EnableHeadersVisualStyles = False
        dgvServicios.GridColor = Color.Gainsboro
        dgvServicios.Location = New Point(0, 166)
        dgvServicios.Margin = New Padding(0)
        dgvServicios.MultiSelect = False
        dgvServicios.Name = "dgvServicios"
        dgvServicios.ReadOnly = True
        dgvServicios.RowHeadersVisible = False
        dgvServicios.RowHeadersWidth = 51
        dgvServicios.RowTemplate.Height = 26
        dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvServicios.Size = New Size(388, 104)
        dgvServicios.StandardTab = True
        dgvServicios.TabIndex = 1
        '
        ' tlpNotas
        '
        tlpNotas.ColumnCount = 2
        tlpNotas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpNotas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpNotas.Controls.Add(lblSintomaTit, 0, 0)
        tlpNotas.Controls.Add(lblObsTit, 1, 0)
        tlpNotas.Controls.Add(txtSintoma, 0, 1)
        tlpNotas.Controls.Add(txtObservaciones, 1, 1)
        tlpNotas.Dock = DockStyle.Fill
        tlpNotas.Location = New Point(0, 280)
        tlpNotas.Margin = New Padding(0, 10, 0, 0)
        tlpNotas.Name = "tlpNotas"
        tlpNotas.RowCount = 2
        tlpNotas.RowStyles.Add(New RowStyle())
        tlpNotas.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpNotas.Size = New Size(388, 86)
        tlpNotas.TabIndex = 2
        '
        ' lblSintomaTit
        '
        lblSintomaTit.AutoSize = True
        lblSintomaTit.Font = New Font("Segoe UI", 8.25F)
        lblSintomaTit.ForeColor = Color.DimGray
        lblSintomaTit.Location = New Point(0, 0)
        lblSintomaTit.Margin = New Padding(0, 0, 5, 2)
        lblSintomaTit.Name = "lblSintomaTit"
        lblSintomaTit.Size = New Size(100, 19)
        lblSintomaTit.TabIndex = 2
        lblSintomaTit.Text = "El cliente dijo"
        '
        ' lblObsTit
        '
        lblObsTit.AutoSize = True
        lblObsTit.Font = New Font("Segoe UI", 8.25F)
        lblObsTit.ForeColor = Color.DimGray
        lblObsTit.Location = New Point(199, 0)
        lblObsTit.Margin = New Padding(5, 0, 0, 2)
        lblObsTit.Name = "lblObsTit"
        lblObsTit.Size = New Size(180, 19)
        lblObsTit.TabIndex = 3
        lblObsTit.Text = "Observaciones del mecánico"
        '
        ' txtSintoma
        '
        txtSintoma.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtSintoma.BorderStyle = BorderStyle.None
        txtSintoma.Dock = DockStyle.Fill
        txtSintoma.Location = New Point(0, 21)
        txtSintoma.Margin = New Padding(0, 0, 5, 0)
        txtSintoma.Multiline = True
        txtSintoma.Name = "txtSintoma"
        txtSintoma.ReadOnly = True
        txtSintoma.ScrollBars = ScrollBars.Vertical
        txtSintoma.Size = New Size(189, 65)
        txtSintoma.TabIndex = 0
        txtSintoma.TabStop = False
        '
        ' txtObservaciones
        '
        txtObservaciones.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtObservaciones.BorderStyle = BorderStyle.None
        txtObservaciones.Dock = DockStyle.Fill
        txtObservaciones.Location = New Point(199, 21)
        txtObservaciones.Margin = New Padding(5, 0, 0, 0)
        txtObservaciones.Multiline = True
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.ReadOnly = True
        txtObservaciones.ScrollBars = ScrollBars.Vertical
        txtObservaciones.Size = New Size(189, 65)
        txtObservaciones.TabIndex = 1
        txtObservaciones.TabStop = False
        '
        ' FrmHistorial
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(pnlDetalle)
        Controls.Add(pnlLista)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmHistorial"
        Text = "Historial"
        pnlLista.ResumeLayout(False)
        pnlLista.PerformLayout()
        pnlDetalle.ResumeLayout(False)
        pnlVehiculo.ResumeLayout(False)
        tlpHistorial.ResumeLayout(False)
        tlpHistorial.PerformLayout()
        tlpNotas.ResumeLayout(False)
        tlpNotas.PerformLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvVehiculos, ComponentModel.ISupportInitialize).EndInit()
        CType(picVehiculo, ComponentModel.ISupportInitialize).EndInit()
        CType(picCliente, ComponentModel.ISupportInitialize).EndInit()
        CType(picKm, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvServicios, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlLista As Panel
    Friend WithEvents picBuscar As PictureBox
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents dgvVehiculos As DataGridView
    Friend WithEvents pnlDetalle As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents pnlVehiculo As Panel
    Friend WithEvents lblPatente As Label
    Friend WithEvents lblBaja As Label
    Friend WithEvents picVehiculo As PictureBox
    Friend WithEvents lblVehiculo As Label
    Friend WithEvents picCliente As PictureBox
    Friend WithEvents lblTitular As Label
    Friend WithEvents picKm As PictureBox
    Friend WithEvents lblKm As Label
    Friend WithEvents lblSinOrdenes As Label
    Friend WithEvents tlpHistorial As TableLayoutPanel
    Friend WithEvents lblOrdenesTit As Label
    Friend WithEvents dgvOrdenes As DataGridView
    Friend WithEvents lblServiciosTit As Label
    Friend WithEvents dgvServicios As DataGridView
    Friend WithEvents tlpNotas As TableLayoutPanel
    Friend WithEvents lblSintomaTit As Label
    Friend WithEvents lblObsTit As Label
    Friend WithEvents txtSintoma As TextBox
    Friend WithEvents txtObservaciones As TextBox
End Class

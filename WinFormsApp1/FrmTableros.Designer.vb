<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTableros
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
        pnlFiltros = New Panel()
        lblDesde = New Label()
        dtpDesde = New DateTimePicker()
        lblHasta = New Label()
        dtpHasta = New DateTimePicker()
        btnActualizar = New Button()
        tlpIndicadores = New TableLayoutPanel()
        pnlIngresadas = New Panel()
        lblNomIngresadas = New Label()
        lblIngresadas = New Label()
        pnlEnTaller = New Panel()
        lblNomEnTaller = New Label()
        lblEnTaller = New Label()
        pnlFacturado = New Panel()
        lblNomFacturado = New Label()
        lblFacturado = New Label()
        pnlTicket = New Panel()
        lblNomTicket = New Label()
        lblTicket = New Label()
        pnlAprobacion = New Panel()
        lblNomAprobacion = New Label()
        lblAprobacion = New Label()
        pnlDemoradas = New Panel()
        lblNomDemoradas = New Label()
        lblDemoradas = New Label()
        tlpGraficos = New TableLayoutPanel()
        fpFacturacion = New ScottPlot.WinForms.FormsPlot()
        fpServicios = New ScottPlot.WinForms.FormsPlot()
        fpHoras = New ScottPlot.WinForms.FormsPlot()
        fpEstados = New ScottPlot.WinForms.FormsPlot()
        pnlFiltros.SuspendLayout()
        tlpIndicadores.SuspendLayout()
        pnlIngresadas.SuspendLayout()
        pnlEnTaller.SuspendLayout()
        pnlFacturado.SuspendLayout()
        pnlTicket.SuspendLayout()
        pnlAprobacion.SuspendLayout()
        pnlDemoradas.SuspendLayout()
        tlpGraficos.SuspendLayout()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(178, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Tableros"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 64)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(300, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Indicadores y gráficos del taller"
        '
        ' pnlFiltros
        '
        pnlFiltros.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnlFiltros.Controls.Add(lblDesde)
        pnlFiltros.Controls.Add(dtpDesde)
        pnlFiltros.Controls.Add(lblHasta)
        pnlFiltros.Controls.Add(dtpHasta)
        pnlFiltros.Controls.Add(btnActualizar)
        pnlFiltros.Location = New Point(370, 22)
        pnlFiltros.Name = "pnlFiltros"
        pnlFiltros.Size = New Size(470, 40)
        pnlFiltros.TabIndex = 2
        '
        ' lblDesde
        '
        lblDesde.Font = New Font("Segoe UI", 10F)
        lblDesde.ForeColor = Color.DimGray
        lblDesde.Location = New Point(0, 0)
        lblDesde.Name = "lblDesde"
        lblDesde.Size = New Size(52, 40)
        lblDesde.TabIndex = 0
        lblDesde.Text = "Desde"
        lblDesde.TextAlign = ContentAlignment.MiddleRight
        '
        ' dtpDesde
        '
        dtpDesde.Font = New Font("Segoe UI", 10F)
        dtpDesde.Format = DateTimePickerFormat.Short
        dtpDesde.Location = New Point(56, 6)
        dtpDesde.Name = "dtpDesde"
        dtpDesde.Size = New Size(120, 30)
        dtpDesde.TabIndex = 1
        '
        ' lblHasta
        '
        lblHasta.Font = New Font("Segoe UI", 10F)
        lblHasta.ForeColor = Color.DimGray
        lblHasta.Location = New Point(180, 0)
        lblHasta.Name = "lblHasta"
        lblHasta.Size = New Size(52, 40)
        lblHasta.TabIndex = 2
        lblHasta.Text = "Hasta"
        lblHasta.TextAlign = ContentAlignment.MiddleRight
        '
        ' dtpHasta
        '
        dtpHasta.Font = New Font("Segoe UI", 10F)
        dtpHasta.Format = DateTimePickerFormat.Short
        dtpHasta.Location = New Point(236, 6)
        dtpHasta.Name = "dtpHasta"
        dtpHasta.Size = New Size(120, 30)
        dtpHasta.TabIndex = 3
        '
        ' btnActualizar
        '
        btnActualizar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnActualizar.Cursor = Cursors.Hand
        btnActualizar.FlatAppearance.BorderSize = 0
        btnActualizar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnActualizar.FlatStyle = FlatStyle.Flat
        btnActualizar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnActualizar.ForeColor = Color.White
        btnActualizar.Location = New Point(366, 2)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Size = New Size(104, 36)
        btnActualizar.TabIndex = 4
        btnActualizar.Text = "Actualizar"
        btnActualizar.UseVisualStyleBackColor = False
        '
        ' tlpIndicadores
        '
        tlpIndicadores.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tlpIndicadores.ColumnCount = 6
        tlpIndicadores.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.66667F))
        tlpIndicadores.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.66667F))
        tlpIndicadores.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.66667F))
        tlpIndicadores.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.66667F))
        tlpIndicadores.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.66667F))
        tlpIndicadores.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 16.66667F))
        tlpIndicadores.Controls.Add(pnlIngresadas, 0, 0)
        tlpIndicadores.Controls.Add(pnlEnTaller, 1, 0)
        tlpIndicadores.Controls.Add(pnlFacturado, 2, 0)
        tlpIndicadores.Controls.Add(pnlTicket, 3, 0)
        tlpIndicadores.Controls.Add(pnlAprobacion, 4, 0)
        tlpIndicadores.Controls.Add(pnlDemoradas, 5, 0)
        tlpIndicadores.Location = New Point(30, 94)
        tlpIndicadores.Name = "tlpIndicadores"
        tlpIndicadores.RowCount = 1
        tlpIndicadores.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpIndicadores.Size = New Size(810, 76)
        tlpIndicadores.TabIndex = 3
        '
        ' pnlIngresadas
        '
        pnlIngresadas.BackColor = Color.White
        pnlIngresadas.Controls.Add(lblNomIngresadas)
        pnlIngresadas.Controls.Add(lblIngresadas)
        pnlIngresadas.Dock = DockStyle.Fill
        pnlIngresadas.Location = New Point(0, 0)
        pnlIngresadas.Margin = New Padding(0, 0, 6, 0)
        pnlIngresadas.Name = "pnlIngresadas"
        pnlIngresadas.Size = New Size(129, 76)
        pnlIngresadas.TabIndex = 0
        '
        ' lblNomIngresadas
        '
        lblNomIngresadas.Dock = DockStyle.Fill
        lblNomIngresadas.Font = New Font("Segoe UI", 8.25F)
        lblNomIngresadas.ForeColor = Color.DimGray
        lblNomIngresadas.Location = New Point(0, 46)
        lblNomIngresadas.Name = "lblNomIngresadas"
        lblNomIngresadas.Size = New Size(129, 30)
        lblNomIngresadas.TabIndex = 1
        lblNomIngresadas.Text = "Órdenes ingresadas"
        lblNomIngresadas.TextAlign = ContentAlignment.TopCenter
        '
        ' lblIngresadas
        '
        lblIngresadas.Dock = DockStyle.Top
        lblIngresadas.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblIngresadas.Location = New Point(0, 0)
        lblIngresadas.Name = "lblIngresadas"
        lblIngresadas.Size = New Size(129, 46)
        lblIngresadas.TabIndex = 0
        lblIngresadas.Text = "0"
        lblIngresadas.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlEnTaller
        '
        pnlEnTaller.BackColor = Color.White
        pnlEnTaller.Controls.Add(lblNomEnTaller)
        pnlEnTaller.Controls.Add(lblEnTaller)
        pnlEnTaller.Dock = DockStyle.Fill
        pnlEnTaller.Location = New Point(135, 0)
        pnlEnTaller.Margin = New Padding(0, 0, 6, 0)
        pnlEnTaller.Name = "pnlEnTaller"
        pnlEnTaller.Size = New Size(129, 76)
        pnlEnTaller.TabIndex = 1
        '
        ' lblNomEnTaller
        '
        lblNomEnTaller.Dock = DockStyle.Fill
        lblNomEnTaller.Font = New Font("Segoe UI", 8.25F)
        lblNomEnTaller.ForeColor = Color.DimGray
        lblNomEnTaller.Location = New Point(0, 46)
        lblNomEnTaller.Name = "lblNomEnTaller"
        lblNomEnTaller.Size = New Size(129, 30)
        lblNomEnTaller.TabIndex = 1
        lblNomEnTaller.Text = "Vehículos en el taller"
        lblNomEnTaller.TextAlign = ContentAlignment.TopCenter
        '
        ' lblEnTaller
        '
        lblEnTaller.Dock = DockStyle.Top
        lblEnTaller.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblEnTaller.Location = New Point(0, 0)
        lblEnTaller.Name = "lblEnTaller"
        lblEnTaller.Size = New Size(129, 46)
        lblEnTaller.TabIndex = 0
        lblEnTaller.Text = "0"
        lblEnTaller.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlFacturado
        '
        pnlFacturado.BackColor = Color.White
        pnlFacturado.Controls.Add(lblNomFacturado)
        pnlFacturado.Controls.Add(lblFacturado)
        pnlFacturado.Dock = DockStyle.Fill
        pnlFacturado.Location = New Point(270, 0)
        pnlFacturado.Margin = New Padding(0, 0, 6, 0)
        pnlFacturado.Name = "pnlFacturado"
        pnlFacturado.Size = New Size(129, 76)
        pnlFacturado.TabIndex = 2
        '
        ' lblNomFacturado
        '
        lblNomFacturado.Dock = DockStyle.Fill
        lblNomFacturado.Font = New Font("Segoe UI", 8.25F)
        lblNomFacturado.ForeColor = Color.DimGray
        lblNomFacturado.Location = New Point(0, 46)
        lblNomFacturado.Name = "lblNomFacturado"
        lblNomFacturado.Size = New Size(129, 30)
        lblNomFacturado.TabIndex = 1
        lblNomFacturado.Text = "Facturado"
        lblNomFacturado.TextAlign = ContentAlignment.TopCenter
        '
        ' lblFacturado
        '
        lblFacturado.Dock = DockStyle.Top
        lblFacturado.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblFacturado.Location = New Point(0, 0)
        lblFacturado.Name = "lblFacturado"
        lblFacturado.Size = New Size(129, 46)
        lblFacturado.TabIndex = 0
        lblFacturado.Text = "0"
        lblFacturado.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlTicket
        '
        pnlTicket.BackColor = Color.White
        pnlTicket.Controls.Add(lblNomTicket)
        pnlTicket.Controls.Add(lblTicket)
        pnlTicket.Dock = DockStyle.Fill
        pnlTicket.Location = New Point(405, 0)
        pnlTicket.Margin = New Padding(0, 0, 6, 0)
        pnlTicket.Name = "pnlTicket"
        pnlTicket.Size = New Size(129, 76)
        pnlTicket.TabIndex = 3
        '
        ' lblNomTicket
        '
        lblNomTicket.Dock = DockStyle.Fill
        lblNomTicket.Font = New Font("Segoe UI", 8.25F)
        lblNomTicket.ForeColor = Color.DimGray
        lblNomTicket.Location = New Point(0, 46)
        lblNomTicket.Name = "lblNomTicket"
        lblNomTicket.Size = New Size(129, 30)
        lblNomTicket.TabIndex = 1
        lblNomTicket.Text = "Ticket promedio"
        lblNomTicket.TextAlign = ContentAlignment.TopCenter
        '
        ' lblTicket
        '
        lblTicket.Dock = DockStyle.Top
        lblTicket.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTicket.Location = New Point(0, 0)
        lblTicket.Name = "lblTicket"
        lblTicket.Size = New Size(129, 46)
        lblTicket.TabIndex = 0
        lblTicket.Text = "0"
        lblTicket.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlAprobacion
        '
        pnlAprobacion.BackColor = Color.White
        pnlAprobacion.Controls.Add(lblNomAprobacion)
        pnlAprobacion.Controls.Add(lblAprobacion)
        pnlAprobacion.Dock = DockStyle.Fill
        pnlAprobacion.Location = New Point(540, 0)
        pnlAprobacion.Margin = New Padding(0, 0, 6, 0)
        pnlAprobacion.Name = "pnlAprobacion"
        pnlAprobacion.Size = New Size(129, 76)
        pnlAprobacion.TabIndex = 4
        '
        ' lblNomAprobacion
        '
        lblNomAprobacion.Dock = DockStyle.Fill
        lblNomAprobacion.Font = New Font("Segoe UI", 8.25F)
        lblNomAprobacion.ForeColor = Color.DimGray
        lblNomAprobacion.Location = New Point(0, 46)
        lblNomAprobacion.Name = "lblNomAprobacion"
        lblNomAprobacion.Size = New Size(129, 30)
        lblNomAprobacion.TabIndex = 1
        lblNomAprobacion.Text = "Presupuestos aprobados"
        lblNomAprobacion.TextAlign = ContentAlignment.TopCenter
        '
        ' lblAprobacion
        '
        lblAprobacion.Dock = DockStyle.Top
        lblAprobacion.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblAprobacion.Location = New Point(0, 0)
        lblAprobacion.Name = "lblAprobacion"
        lblAprobacion.Size = New Size(129, 46)
        lblAprobacion.TabIndex = 0
        lblAprobacion.Text = "0"
        lblAprobacion.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlDemoradas
        '
        pnlDemoradas.BackColor = Color.White
        pnlDemoradas.Controls.Add(lblNomDemoradas)
        pnlDemoradas.Controls.Add(lblDemoradas)
        pnlDemoradas.Dock = DockStyle.Fill
        pnlDemoradas.Location = New Point(675, 0)
        pnlDemoradas.Margin = New Padding(0, 0, 0, 0)
        pnlDemoradas.Name = "pnlDemoradas"
        pnlDemoradas.Size = New Size(135, 76)
        pnlDemoradas.TabIndex = 5
        '
        ' lblNomDemoradas
        '
        lblNomDemoradas.Dock = DockStyle.Fill
        lblNomDemoradas.Font = New Font("Segoe UI", 8.25F)
        lblNomDemoradas.ForeColor = Color.DimGray
        lblNomDemoradas.Location = New Point(0, 46)
        lblNomDemoradas.Name = "lblNomDemoradas"
        lblNomDemoradas.Size = New Size(135, 30)
        lblNomDemoradas.TabIndex = 1
        lblNomDemoradas.Text = "Órdenes demoradas"
        lblNomDemoradas.TextAlign = ContentAlignment.TopCenter
        '
        ' lblDemoradas
        '
        lblDemoradas.Dock = DockStyle.Top
        lblDemoradas.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblDemoradas.Location = New Point(0, 0)
        lblDemoradas.Name = "lblDemoradas"
        lblDemoradas.Size = New Size(135, 46)
        lblDemoradas.TabIndex = 0
        lblDemoradas.Text = "0"
        lblDemoradas.TextAlign = ContentAlignment.BottomCenter
        '
        ' tlpGraficos
        '
        tlpGraficos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpGraficos.ColumnCount = 2
        tlpGraficos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpGraficos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpGraficos.Controls.Add(fpFacturacion, 0, 0)
        tlpGraficos.Controls.Add(fpServicios, 1, 0)
        tlpGraficos.Controls.Add(fpHoras, 0, 1)
        tlpGraficos.Controls.Add(fpEstados, 1, 1)
        tlpGraficos.Location = New Point(30, 182)
        tlpGraficos.Name = "tlpGraficos"
        tlpGraficos.RowCount = 2
        tlpGraficos.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpGraficos.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpGraficos.Size = New Size(810, 436)
        tlpGraficos.TabIndex = 4
        '
        ' fpFacturacion
        '
        fpFacturacion.Dock = DockStyle.Fill
        fpFacturacion.Location = New Point(0, 0)
        fpFacturacion.Margin = New Padding(0, 0, 5, 5)
        fpFacturacion.Name = "fpFacturacion"
        fpFacturacion.Size = New Size(400, 213)
        fpFacturacion.TabIndex = 0
        '
        ' fpServicios
        '
        fpServicios.Dock = DockStyle.Fill
        fpServicios.Location = New Point(410, 0)
        fpServicios.Margin = New Padding(5, 0, 0, 5)
        fpServicios.Name = "fpServicios"
        fpServicios.Size = New Size(400, 213)
        fpServicios.TabIndex = 1
        '
        ' fpHoras
        '
        fpHoras.Dock = DockStyle.Fill
        fpHoras.Location = New Point(0, 223)
        fpHoras.Margin = New Padding(0, 5, 5, 0)
        fpHoras.Name = "fpHoras"
        fpHoras.Size = New Size(400, 213)
        fpHoras.TabIndex = 2
        '
        ' fpEstados
        '
        fpEstados.Dock = DockStyle.Fill
        fpEstados.Location = New Point(410, 223)
        fpEstados.Margin = New Padding(5, 5, 0, 0)
        fpEstados.Name = "fpEstados"
        fpEstados.Size = New Size(400, 213)
        fpEstados.TabIndex = 3
        '
        ' FrmTableros
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(tlpGraficos)
        Controls.Add(tlpIndicadores)
        Controls.Add(pnlFiltros)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmTableros"
        Text = "Tableros"
        pnlFiltros.ResumeLayout(False)
        tlpIndicadores.ResumeLayout(False)
        pnlIngresadas.ResumeLayout(False)
        pnlEnTaller.ResumeLayout(False)
        pnlFacturado.ResumeLayout(False)
        pnlTicket.ResumeLayout(False)
        pnlAprobacion.ResumeLayout(False)
        pnlDemoradas.ResumeLayout(False)
        tlpGraficos.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlFiltros As Panel
    Friend WithEvents lblDesde As Label
    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents btnActualizar As Button
    Friend WithEvents tlpIndicadores As TableLayoutPanel
    Friend WithEvents pnlIngresadas As Panel
    Friend WithEvents lblNomIngresadas As Label
    Friend WithEvents lblIngresadas As Label
    Friend WithEvents pnlEnTaller As Panel
    Friend WithEvents lblNomEnTaller As Label
    Friend WithEvents lblEnTaller As Label
    Friend WithEvents pnlFacturado As Panel
    Friend WithEvents lblNomFacturado As Label
    Friend WithEvents lblFacturado As Label
    Friend WithEvents pnlTicket As Panel
    Friend WithEvents lblNomTicket As Label
    Friend WithEvents lblTicket As Label
    Friend WithEvents pnlAprobacion As Panel
    Friend WithEvents lblNomAprobacion As Label
    Friend WithEvents lblAprobacion As Label
    Friend WithEvents pnlDemoradas As Panel
    Friend WithEvents lblNomDemoradas As Label
    Friend WithEvents lblDemoradas As Label
    Friend WithEvents tlpGraficos As TableLayoutPanel
    Friend WithEvents fpFacturacion As ScottPlot.WinForms.FormsPlot
    Friend WithEvents fpServicios As ScottPlot.WinForms.FormsPlot
    Friend WithEvents fpHoras As ScottPlot.WinForms.FormsPlot
    Friend WithEvents fpEstados As ScottPlot.WinForms.FormsPlot
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInicio
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
        tlpTarjetas = New TableLayoutPanel()
        pnlHoy = New Panel()
        lblNomHoy = New Label()
        lblCantHoy = New Label()
        pnlDemoradas = New Panel()
        lblNomDemoradas = New Label()
        lblCantDemoradas = New Label()
        pnlListas = New Panel()
        lblNomListas = New Label()
        lblCantListas = New Label()
        pnlServices = New Panel()
        lblNomServices = New Label()
        lblCantServices = New Label()
        pnlTurnos = New Panel()
        lblNomTurnos = New Label()
        lblCantTurnos = New Label()
        dgvOrdenes = New DataGridView()
        lblVacio = New Label()
        btnIrOrdenes = New Button()
        tlpTarjetas.SuspendLayout()
        pnlHoy.SuspendLayout()
        pnlDemoradas.SuspendLayout()
        pnlListas.SuspendLayout()
        pnlServices.SuspendLayout()
        pnlTurnos.SuspendLayout()
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(110, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Inicio"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 72)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(20, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "-"
        '
        ' tlpTarjetas
        '
        tlpTarjetas.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tlpTarjetas.ColumnCount = 5
        tlpTarjetas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpTarjetas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpTarjetas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpTarjetas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpTarjetas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        tlpTarjetas.Controls.Add(pnlHoy, 0, 0)
        tlpTarjetas.Controls.Add(pnlDemoradas, 1, 0)
        tlpTarjetas.Controls.Add(pnlListas, 2, 0)
        tlpTarjetas.Controls.Add(pnlTurnos, 3, 0)
        tlpTarjetas.Controls.Add(pnlServices, 4, 0)
        tlpTarjetas.Location = New Point(30, 100)
        tlpTarjetas.Name = "tlpTarjetas"
        tlpTarjetas.RowCount = 1
        tlpTarjetas.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpTarjetas.Size = New Size(810, 90)
        tlpTarjetas.TabIndex = 2
        '
        ' pnlHoy
        '
        pnlHoy.BackColor = Color.White
        pnlHoy.Controls.Add(lblNomHoy)
        pnlHoy.Controls.Add(lblCantHoy)
        pnlHoy.Dock = DockStyle.Fill
        pnlHoy.Location = New Point(0, 0)
        pnlHoy.Margin = New Padding(0, 0, 8, 0)
        pnlHoy.Name = "pnlHoy"
        pnlHoy.Size = New Size(261, 90)
        pnlHoy.TabIndex = 0
        '
        ' lblNomHoy
        '
        lblNomHoy.Dock = DockStyle.Fill
        lblNomHoy.Font = New Font("Segoe UI", 9.5F)
        lblNomHoy.ForeColor = Color.DimGray
        lblNomHoy.Location = New Point(0, 56)
        lblNomHoy.Name = "lblNomHoy"
        lblNomHoy.Size = New Size(261, 34)
        lblNomHoy.TabIndex = 1
        lblNomHoy.Text = "Prometidas para hoy"
        lblNomHoy.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCantHoy
        '
        lblCantHoy.Dock = DockStyle.Top
        lblCantHoy.Font = New Font("Segoe UI", 22F, FontStyle.Bold)
        lblCantHoy.Location = New Point(0, 0)
        lblCantHoy.Name = "lblCantHoy"
        lblCantHoy.Size = New Size(261, 56)
        lblCantHoy.TabIndex = 0
        lblCantHoy.Text = "0"
        lblCantHoy.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlDemoradas
        '
        pnlDemoradas.BackColor = Color.White
        pnlDemoradas.Controls.Add(lblNomDemoradas)
        pnlDemoradas.Controls.Add(lblCantDemoradas)
        pnlDemoradas.Dock = DockStyle.Fill
        pnlDemoradas.Location = New Point(269, 0)
        pnlDemoradas.Margin = New Padding(0, 0, 8, 0)
        pnlDemoradas.Name = "pnlDemoradas"
        pnlDemoradas.Size = New Size(261, 90)
        pnlDemoradas.TabIndex = 1
        '
        ' lblNomDemoradas
        '
        lblNomDemoradas.Dock = DockStyle.Fill
        lblNomDemoradas.Font = New Font("Segoe UI", 9.5F)
        lblNomDemoradas.ForeColor = Color.DimGray
        lblNomDemoradas.Location = New Point(0, 56)
        lblNomDemoradas.Name = "lblNomDemoradas"
        lblNomDemoradas.Size = New Size(261, 34)
        lblNomDemoradas.TabIndex = 1
        lblNomDemoradas.Text = "Demoradas"
        lblNomDemoradas.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCantDemoradas
        '
        lblCantDemoradas.Dock = DockStyle.Top
        lblCantDemoradas.Font = New Font("Segoe UI", 22F, FontStyle.Bold)
        lblCantDemoradas.Location = New Point(0, 0)
        lblCantDemoradas.Name = "lblCantDemoradas"
        lblCantDemoradas.Size = New Size(261, 56)
        lblCantDemoradas.TabIndex = 0
        lblCantDemoradas.Text = "0"
        lblCantDemoradas.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlListas
        '
        pnlListas.BackColor = Color.White
        pnlListas.Controls.Add(lblNomListas)
        pnlListas.Controls.Add(lblCantListas)
        pnlListas.Dock = DockStyle.Fill
        pnlListas.Location = New Point(538, 0)
        pnlListas.Margin = New Padding(0, 0, 8, 0)
        pnlListas.Name = "pnlListas"
        pnlListas.Size = New Size(272, 90)
        pnlListas.TabIndex = 2
        '
        ' lblNomListas
        '
        lblNomListas.Dock = DockStyle.Fill
        lblNomListas.Font = New Font("Segoe UI", 9.5F)
        lblNomListas.ForeColor = Color.DimGray
        lblNomListas.Location = New Point(0, 56)
        lblNomListas.Name = "lblNomListas"
        lblNomListas.Size = New Size(272, 34)
        lblNomListas.TabIndex = 1
        lblNomListas.Text = "Listas para entregar"
        lblNomListas.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCantListas
        '
        lblCantListas.Dock = DockStyle.Top
        lblCantListas.Font = New Font("Segoe UI", 22F, FontStyle.Bold)
        lblCantListas.Location = New Point(0, 0)
        lblCantListas.Name = "lblCantListas"
        lblCantListas.Size = New Size(272, 56)
        lblCantListas.TabIndex = 0
        lblCantListas.Text = "0"
        lblCantListas.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlTurnos
        '
        pnlTurnos.BackColor = Color.White
        pnlTurnos.Controls.Add(lblNomTurnos)
        pnlTurnos.Controls.Add(lblCantTurnos)
        pnlTurnos.Cursor = Cursors.Hand
        pnlTurnos.Dock = DockStyle.Fill
        pnlTurnos.Location = New Point(486, 0)
        pnlTurnos.Margin = New Padding(0, 0, 8, 0)
        pnlTurnos.Name = "pnlTurnos"
        pnlTurnos.Size = New Size(154, 90)
        pnlTurnos.TabIndex = 4
        '
        ' lblNomTurnos
        '
        lblNomTurnos.Cursor = Cursors.Hand
        lblNomTurnos.Dock = DockStyle.Fill
        lblNomTurnos.Font = New Font("Segoe UI", 9.5F)
        lblNomTurnos.ForeColor = Color.DimGray
        lblNomTurnos.Location = New Point(0, 56)
        lblNomTurnos.Name = "lblNomTurnos"
        lblNomTurnos.Size = New Size(154, 34)
        lblNomTurnos.TabIndex = 1
        lblNomTurnos.Text = "Turnos de hoy"
        lblNomTurnos.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCantTurnos
        '
        lblCantTurnos.Cursor = Cursors.Hand
        lblCantTurnos.Dock = DockStyle.Top
        lblCantTurnos.Font = New Font("Segoe UI", 22F, FontStyle.Bold)
        lblCantTurnos.Location = New Point(0, 0)
        lblCantTurnos.Name = "lblCantTurnos"
        lblCantTurnos.Size = New Size(154, 56)
        lblCantTurnos.TabIndex = 0
        lblCantTurnos.Text = "0"
        lblCantTurnos.TextAlign = ContentAlignment.BottomCenter
        '
        ' pnlServices
        '
        pnlServices.BackColor = Color.White
        pnlServices.Controls.Add(lblNomServices)
        pnlServices.Controls.Add(lblCantServices)
        pnlServices.Cursor = Cursors.Hand
        pnlServices.Dock = DockStyle.Fill
        pnlServices.Location = New Point(609, 0)
        pnlServices.Margin = New Padding(0, 0, 0, 0)
        pnlServices.Name = "pnlServices"
        pnlServices.Size = New Size(201, 90)
        pnlServices.TabIndex = 3
        '
        ' lblNomServices
        '
        lblNomServices.Cursor = Cursors.Hand
        lblNomServices.Dock = DockStyle.Fill
        lblNomServices.Font = New Font("Segoe UI", 9.5F)
        lblNomServices.ForeColor = Color.DimGray
        lblNomServices.Location = New Point(0, 56)
        lblNomServices.Name = "lblNomServices"
        lblNomServices.Size = New Size(201, 34)
        lblNomServices.TabIndex = 1
        lblNomServices.Text = "Services vencidos sin avisar"
        lblNomServices.TextAlign = ContentAlignment.TopCenter
        '
        ' lblCantServices
        '
        lblCantServices.Cursor = Cursors.Hand
        lblCantServices.Dock = DockStyle.Top
        lblCantServices.Font = New Font("Segoe UI", 22F, FontStyle.Bold)
        lblCantServices.Location = New Point(0, 0)
        lblCantServices.Name = "lblCantServices"
        lblCantServices.Size = New Size(201, 56)
        lblCantServices.TabIndex = 0
        lblCantServices.Text = "0"
        lblCantServices.TextAlign = ContentAlignment.BottomCenter
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
        dgvOrdenes.Location = New Point(30, 206)
        dgvOrdenes.MultiSelect = False
        dgvOrdenes.Name = "dgvOrdenes"
        dgvOrdenes.ReadOnly = True
        dgvOrdenes.RowHeadersVisible = False
        dgvOrdenes.RowHeadersWidth = 51
        dgvOrdenes.RowTemplate.Height = 24
        dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrdenes.Size = New Size(810, 350)
        dgvOrdenes.TabIndex = 3
        '
        ' lblVacio
        '
        lblVacio.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblVacio.BackColor = Color.White
        lblVacio.Font = New Font("Segoe UI", 12F)
        lblVacio.ForeColor = Color.DimGray
        lblVacio.Location = New Point(30, 206)
        lblVacio.Name = "lblVacio"
        lblVacio.Size = New Size(810, 350)
        lblVacio.TabIndex = 4
        lblVacio.Text = "No hay órdenes que requieran atención hoy."
        lblVacio.TextAlign = ContentAlignment.MiddleCenter
        lblVacio.Visible = False
        '
        ' btnIrOrdenes
        '
        btnIrOrdenes.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnIrOrdenes.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnIrOrdenes.Cursor = Cursors.Hand
        btnIrOrdenes.FlatAppearance.BorderSize = 0
        btnIrOrdenes.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnIrOrdenes.FlatStyle = FlatStyle.Flat
        btnIrOrdenes.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnIrOrdenes.ForeColor = Color.White
        btnIrOrdenes.Location = New Point(610, 572)
        btnIrOrdenes.Name = "btnIrOrdenes"
        btnIrOrdenes.Size = New Size(230, 40)
        btnIrOrdenes.TabIndex = 5
        btnIrOrdenes.Text = "Ir a órdenes de trabajo"
        btnIrOrdenes.UseVisualStyleBackColor = False
        '
        ' FrmInicio
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(btnIrOrdenes)
        Controls.Add(lblVacio)
        Controls.Add(dgvOrdenes)
        Controls.Add(tlpTarjetas)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmInicio"
        Text = "Inicio"
        tlpTarjetas.ResumeLayout(False)
        pnlHoy.ResumeLayout(False)
        pnlDemoradas.ResumeLayout(False)
        pnlListas.ResumeLayout(False)
        pnlServices.ResumeLayout(False)
        pnlTurnos.ResumeLayout(False)
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents tlpTarjetas As TableLayoutPanel
    Friend WithEvents pnlHoy As Panel
    Friend WithEvents lblNomHoy As Label
    Friend WithEvents lblCantHoy As Label
    Friend WithEvents pnlDemoradas As Panel
    Friend WithEvents lblNomDemoradas As Label
    Friend WithEvents lblCantDemoradas As Label
    Friend WithEvents pnlListas As Panel
    Friend WithEvents lblNomListas As Label
    Friend WithEvents lblCantListas As Label
    Friend WithEvents pnlServices As Panel
    Friend WithEvents lblNomServices As Label
    Friend WithEvents lblCantServices As Label
    Friend WithEvents pnlTurnos As Panel
    Friend WithEvents lblNomTurnos As Label
    Friend WithEvents lblCantTurnos As Label
    Friend WithEvents dgvOrdenes As DataGridView
    Friend WithEvents lblVacio As Label
    Friend WithEvents btnIrOrdenes As Button
End Class

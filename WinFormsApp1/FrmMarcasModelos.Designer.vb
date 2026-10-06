<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMarcasModelos
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
        lblTitulo = New Label()
        lblSubtitulo = New Label()
        tlpTarjetas = New TableLayoutPanel()
        pnlMarcas = New Panel()
        lblTituloMarcas = New Label()
        btnNuevaMarca = New Button()
        dgvMarcas = New DataGridView()
        lblMarca = New Label()
        txtMarca = New TextBox()
        btnGuardarMarca = New Button()
        btnEliminarMarca = New Button()
        pnlModelos = New Panel()
        lblTituloModelos = New Label()
        btnNuevoModelo = New Button()
        dgvModelos = New DataGridView()
        lblModelo = New Label()
        txtModelo = New TextBox()
        btnGuardarModelo = New Button()
        btnEliminarModelo = New Button()
        lblAyudaModelos = New Label()
        tlpTarjetas.SuspendLayout()
        pnlMarcas.SuspendLayout()
        pnlModelos.SuspendLayout()
        CType(dgvMarcas, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvModelos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(520, 60)
        lblTitulo.TabIndex = 1
        lblTitulo.Text = "Marcas y modelos"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 72)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(160, 25)
        lblSubtitulo.TabIndex = 2
        lblSubtitulo.Text = "-"
        '
        ' tlpTarjetas
        '
        tlpTarjetas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpTarjetas.ColumnCount = 2
        tlpTarjetas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpTarjetas.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpTarjetas.Controls.Add(pnlMarcas, 0, 0)
        tlpTarjetas.Controls.Add(pnlModelos, 1, 0)
        tlpTarjetas.Location = New Point(30, 110)
        tlpTarjetas.Name = "tlpTarjetas"
        tlpTarjetas.RowCount = 1
        tlpTarjetas.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpTarjetas.Size = New Size(810, 510)
        tlpTarjetas.TabIndex = 0
        '
        ' pnlMarcas
        '
        pnlMarcas.BackColor = Color.White
        pnlMarcas.Controls.Add(lblTituloMarcas)
        pnlMarcas.Controls.Add(btnNuevaMarca)
        pnlMarcas.Controls.Add(dgvMarcas)
        pnlMarcas.Controls.Add(lblMarca)
        pnlMarcas.Controls.Add(txtMarca)
        pnlMarcas.Controls.Add(btnEliminarMarca)
        pnlMarcas.Controls.Add(btnGuardarMarca)
        pnlMarcas.Dock = DockStyle.Fill
        pnlMarcas.Location = New Point(0, 0)
        pnlMarcas.Margin = New Padding(0, 0, 5, 0)
        pnlMarcas.Name = "pnlMarcas"
        pnlMarcas.Size = New Size(400, 510)
        pnlMarcas.TabIndex = 0
        '
        ' lblTituloMarcas
        '
        lblTituloMarcas.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblTituloMarcas.AutoEllipsis = True
        lblTituloMarcas.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblTituloMarcas.Location = New Point(16, 12)
        lblTituloMarcas.Name = "lblTituloMarcas"
        lblTituloMarcas.Size = New Size(198, 30)
        lblTituloMarcas.TabIndex = 6
        lblTituloMarcas.Text = "Marcas"
        lblTituloMarcas.TextAlign = ContentAlignment.MiddleLeft
        '
        ' btnNuevaMarca
        '
        btnNuevaMarca.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnNuevaMarca.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnNuevaMarca.Cursor = Cursors.Hand
        btnNuevaMarca.FlatAppearance.BorderSize = 0
        btnNuevaMarca.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnNuevaMarca.FlatStyle = FlatStyle.Flat
        btnNuevaMarca.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnNuevaMarca.ForeColor = Color.White
        btnNuevaMarca.Location = New Point(224, 10)
        btnNuevaMarca.Name = "btnNuevaMarca"
        btnNuevaMarca.Size = New Size(160, 34)
        btnNuevaMarca.TabIndex = 0
        btnNuevaMarca.Text = " Nueva marca"
        btnNuevaMarca.TextImageRelation = TextImageRelation.ImageBeforeText
        btnNuevaMarca.UseVisualStyleBackColor = False
        '
        ' dgvMarcas
        '
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.White
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.DimGray
        DataGridViewCellStyle1.SelectionBackColor = Color.White
        DataGridViewCellStyle1.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        dgvMarcas.AllowUserToAddRows = False
        dgvMarcas.AllowUserToDeleteRows = False
        dgvMarcas.AllowUserToResizeRows = False
        dgvMarcas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMarcas.BackgroundColor = Color.White
        dgvMarcas.BorderStyle = BorderStyle.None
        dgvMarcas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvMarcas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvMarcas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvMarcas.ColumnHeadersHeight = 40
        dgvMarcas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvMarcas.EnableHeadersVisualStyles = False
        dgvMarcas.GridColor = Color.Gainsboro
        dgvMarcas.Location = New Point(16, 54)
        dgvMarcas.MultiSelect = False
        dgvMarcas.Name = "dgvMarcas"
        dgvMarcas.ReadOnly = True
        dgvMarcas.RowHeadersVisible = False
        dgvMarcas.RowHeadersWidth = 51
        dgvMarcas.RowTemplate.Height = 26
        dgvMarcas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMarcas.Size = New Size(368, 328)
        dgvMarcas.StandardTab = True
        dgvMarcas.TabIndex = 1
        '
        ' lblMarca
        '
        lblMarca.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblMarca.AutoEllipsis = True
        lblMarca.Font = New Font("Segoe UI", 8.25F)
        lblMarca.ForeColor = Color.DimGray
        lblMarca.Location = New Point(16, 394)
        lblMarca.Name = "lblMarca"
        lblMarca.Size = New Size(368, 19)
        lblMarca.TabIndex = 7
        lblMarca.Text = "Nombre de la marca nueva (*)"
        '
        ' txtMarca
        '
        txtMarca.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtMarca.Location = New Point(16, 415)
        txtMarca.MaxLength = 50
        txtMarca.Name = "txtMarca"
        txtMarca.Size = New Size(368, 27)
        txtMarca.TabIndex = 2
        '
        ' btnGuardarMarca
        '
        btnGuardarMarca.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnGuardarMarca.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGuardarMarca.Cursor = Cursors.Hand
        btnGuardarMarca.FlatAppearance.BorderSize = 0
        btnGuardarMarca.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGuardarMarca.FlatStyle = FlatStyle.Flat
        btnGuardarMarca.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGuardarMarca.ForeColor = Color.White
        btnGuardarMarca.Location = New Point(194, 454)
        btnGuardarMarca.Name = "btnGuardarMarca"
        btnGuardarMarca.Size = New Size(190, 40)
        btnGuardarMarca.TabIndex = 3
        btnGuardarMarca.Text = " Guardar"
        btnGuardarMarca.TextImageRelation = TextImageRelation.ImageBeforeText
        btnGuardarMarca.UseVisualStyleBackColor = False
        '
        ' btnEliminarMarca
        '
        btnEliminarMarca.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnEliminarMarca.BackColor = Color.White
        btnEliminarMarca.Cursor = Cursors.Hand
        btnEliminarMarca.FlatAppearance.BorderColor = Color.Silver
        btnEliminarMarca.FlatStyle = FlatStyle.Flat
        btnEliminarMarca.Font = New Font("Segoe UI", 10F)
        btnEliminarMarca.ForeColor = Color.Firebrick
        btnEliminarMarca.Location = New Point(16, 454)
        btnEliminarMarca.Name = "btnEliminarMarca"
        btnEliminarMarca.Size = New Size(146, 40)
        btnEliminarMarca.TabIndex = 4
        btnEliminarMarca.Text = " Eliminar"
        btnEliminarMarca.TextImageRelation = TextImageRelation.ImageBeforeText
        btnEliminarMarca.UseVisualStyleBackColor = False
        btnEliminarMarca.Visible = False
        '
        ' pnlModelos
        '
        pnlModelos.BackColor = Color.White
        pnlModelos.Controls.Add(lblTituloModelos)
        pnlModelos.Controls.Add(btnNuevoModelo)
        pnlModelos.Controls.Add(lblAyudaModelos)
        pnlModelos.Controls.Add(dgvModelos)
        pnlModelos.Controls.Add(lblModelo)
        pnlModelos.Controls.Add(txtModelo)
        pnlModelos.Controls.Add(btnEliminarModelo)
        pnlModelos.Controls.Add(btnGuardarModelo)
        pnlModelos.Dock = DockStyle.Fill
        pnlModelos.Location = New Point(0, 0)
        pnlModelos.Margin = New Padding(5, 0, 0, 0)
        pnlModelos.Name = "pnlModelos"
        pnlModelos.Size = New Size(400, 510)
        pnlModelos.TabIndex = 1
        '
        ' lblTituloModelos
        '
        lblTituloModelos.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblTituloModelos.AutoEllipsis = True
        lblTituloModelos.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblTituloModelos.Location = New Point(16, 12)
        lblTituloModelos.Name = "lblTituloModelos"
        lblTituloModelos.Size = New Size(198, 30)
        lblTituloModelos.TabIndex = 6
        lblTituloModelos.Text = "Modelos"
        lblTituloModelos.TextAlign = ContentAlignment.MiddleLeft
        '
        ' btnNuevoModelo
        '
        btnNuevoModelo.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnNuevoModelo.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnNuevoModelo.Cursor = Cursors.Hand
        btnNuevoModelo.FlatAppearance.BorderSize = 0
        btnNuevoModelo.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnNuevoModelo.FlatStyle = FlatStyle.Flat
        btnNuevoModelo.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnNuevoModelo.ForeColor = Color.White
        btnNuevoModelo.Location = New Point(224, 10)
        btnNuevoModelo.Name = "btnNuevoModelo"
        btnNuevoModelo.Size = New Size(160, 34)
        btnNuevoModelo.TabIndex = 0
        btnNuevoModelo.Text = " Nuevo modelo"
        btnNuevoModelo.TextImageRelation = TextImageRelation.ImageBeforeText
        btnNuevoModelo.UseVisualStyleBackColor = False
        '
        ' dgvModelos
        '
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.White
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = Color.DimGray
        DataGridViewCellStyle2.SelectionBackColor = Color.White
        DataGridViewCellStyle2.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        dgvModelos.AllowUserToAddRows = False
        dgvModelos.AllowUserToDeleteRows = False
        dgvModelos.AllowUserToResizeRows = False
        dgvModelos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvModelos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvModelos.BackgroundColor = Color.White
        dgvModelos.BorderStyle = BorderStyle.None
        dgvModelos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvModelos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvModelos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvModelos.ColumnHeadersHeight = 40
        dgvModelos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvModelos.EnableHeadersVisualStyles = False
        dgvModelos.GridColor = Color.Gainsboro
        dgvModelos.Location = New Point(16, 54)
        dgvModelos.MultiSelect = False
        dgvModelos.Name = "dgvModelos"
        dgvModelos.ReadOnly = True
        dgvModelos.RowHeadersVisible = False
        dgvModelos.RowHeadersWidth = 51
        dgvModelos.RowTemplate.Height = 26
        dgvModelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvModelos.Size = New Size(368, 328)
        dgvModelos.StandardTab = True
        dgvModelos.TabIndex = 1
        '
        ' lblModelo
        '
        lblModelo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblModelo.AutoEllipsis = True
        lblModelo.Font = New Font("Segoe UI", 8.25F)
        lblModelo.ForeColor = Color.DimGray
        lblModelo.Location = New Point(16, 394)
        lblModelo.Name = "lblModelo"
        lblModelo.Size = New Size(368, 19)
        lblModelo.TabIndex = 7
        lblModelo.Text = "Nombre del modelo nuevo (*)"
        '
        ' txtModelo
        '
        txtModelo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtModelo.Location = New Point(16, 415)
        txtModelo.MaxLength = 80
        txtModelo.Name = "txtModelo"
        txtModelo.Size = New Size(368, 27)
        txtModelo.TabIndex = 2
        '
        ' btnGuardarModelo
        '
        btnGuardarModelo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnGuardarModelo.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGuardarModelo.Cursor = Cursors.Hand
        btnGuardarModelo.FlatAppearance.BorderSize = 0
        btnGuardarModelo.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGuardarModelo.FlatStyle = FlatStyle.Flat
        btnGuardarModelo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGuardarModelo.ForeColor = Color.White
        btnGuardarModelo.Location = New Point(194, 454)
        btnGuardarModelo.Name = "btnGuardarModelo"
        btnGuardarModelo.Size = New Size(190, 40)
        btnGuardarModelo.TabIndex = 3
        btnGuardarModelo.Text = " Guardar"
        btnGuardarModelo.TextImageRelation = TextImageRelation.ImageBeforeText
        btnGuardarModelo.UseVisualStyleBackColor = False
        '
        ' btnEliminarModelo
        '
        btnEliminarModelo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnEliminarModelo.BackColor = Color.White
        btnEliminarModelo.Cursor = Cursors.Hand
        btnEliminarModelo.FlatAppearance.BorderColor = Color.Silver
        btnEliminarModelo.FlatStyle = FlatStyle.Flat
        btnEliminarModelo.Font = New Font("Segoe UI", 10F)
        btnEliminarModelo.ForeColor = Color.Firebrick
        btnEliminarModelo.Location = New Point(16, 454)
        btnEliminarModelo.Name = "btnEliminarModelo"
        btnEliminarModelo.Size = New Size(146, 40)
        btnEliminarModelo.TabIndex = 4
        btnEliminarModelo.Text = " Eliminar"
        btnEliminarModelo.TextImageRelation = TextImageRelation.ImageBeforeText
        btnEliminarModelo.UseVisualStyleBackColor = False
        btnEliminarModelo.Visible = False
        '
        ' lblAyudaModelos
        '
        lblAyudaModelos.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyudaModelos.ForeColor = Color.DimGray
        lblAyudaModelos.Location = New Point(16, 54)
        lblAyudaModelos.Name = "lblAyudaModelos"
        lblAyudaModelos.Size = New Size(368, 48)
        lblAyudaModelos.TabIndex = 8
        lblAyudaModelos.Text = "Seleccione una marca de la lista para ver y cargar sus modelos."
        '
        ' FrmMarcasModelos
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(tlpTarjetas)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmMarcasModelos"
        Text = "Marcas y modelos"
        tlpTarjetas.ResumeLayout(False)
        pnlMarcas.ResumeLayout(False)
        pnlMarcas.PerformLayout()
        pnlModelos.ResumeLayout(False)
        pnlModelos.PerformLayout()
        CType(dgvMarcas, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvModelos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents tlpTarjetas As TableLayoutPanel
    Friend WithEvents pnlMarcas As Panel
    Friend WithEvents lblTituloMarcas As Label
    Friend WithEvents btnNuevaMarca As Button
    Friend WithEvents dgvMarcas As DataGridView
    Friend WithEvents lblMarca As Label
    Friend WithEvents txtMarca As TextBox
    Friend WithEvents btnGuardarMarca As Button
    Friend WithEvents btnEliminarMarca As Button
    Friend WithEvents pnlModelos As Panel
    Friend WithEvents lblTituloModelos As Label
    Friend WithEvents btnNuevoModelo As Button
    Friend WithEvents dgvModelos As DataGridView
    Friend WithEvents lblModelo As Label
    Friend WithEvents txtModelo As TextBox
    Friend WithEvents btnGuardarModelo As Button
    Friend WithEvents btnEliminarModelo As Button
    Friend WithEvents lblAyudaModelos As Label
End Class

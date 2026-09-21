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
        lblTitulo = New Label()
        lblSubtitulo = New Label()
        pnlMarcas = New Panel()
        lblSeccionMarcas = New Label()
        lblIdMarca = New Label()
        txtIdMarca = New TextBox()
        lblMarca = New Label()
        txtMarca = New TextBox()
        btnGuardarMarca = New Button()
        btnModificarMarca = New Button()
        btnEliminarMarca = New Button()
        btnLimpiarMarca = New Button()
        dgvMarcas = New DataGridView()
        pnlModelos = New Panel()
        lblSeccionModelos = New Label()
        lblMarcaSeleccionada = New Label()
        lblIdModelo = New Label()
        txtIdModelo = New TextBox()
        lblModelo = New Label()
        txtModelo = New TextBox()
        btnGuardarModelo = New Button()
        btnModificarModelo = New Button()
        btnEliminarModelo = New Button()
        btnLimpiarModelo = New Button()
        dgvModelos = New DataGridView()
        pnlMarcas.SuspendLayout()
        CType(dgvMarcas, ComponentModel.ISupportInitialize).BeginInit()
        pnlModelos.SuspendLayout()
        CType(dgvModelos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(40, 25)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(338, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Marcas y modelos"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(43, 78)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(329, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Catálogo de marcas y modelos de vehículos"
        '
        ' pnlMarcas
        '
        pnlMarcas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        pnlMarcas.BackColor = Color.White
        pnlMarcas.Controls.Add(lblSeccionMarcas)
        pnlMarcas.Controls.Add(lblIdMarca)
        pnlMarcas.Controls.Add(txtIdMarca)
        pnlMarcas.Controls.Add(lblMarca)
        pnlMarcas.Controls.Add(txtMarca)
        pnlMarcas.Controls.Add(btnGuardarMarca)
        pnlMarcas.Controls.Add(btnModificarMarca)
        pnlMarcas.Controls.Add(btnEliminarMarca)
        pnlMarcas.Controls.Add(btnLimpiarMarca)
        pnlMarcas.Controls.Add(dgvMarcas)
        pnlMarcas.Location = New Point(40, 120)
        pnlMarcas.Name = "pnlMarcas"
        pnlMarcas.Size = New Size(450, 570)
        pnlMarcas.TabIndex = 2
        '
        ' lblSeccionMarcas
        '
        lblSeccionMarcas.AutoSize = True
        lblSeccionMarcas.Font = New Font("Segoe UI", 13F, FontStyle.Bold)
        lblSeccionMarcas.Location = New Point(20, 15)
        lblSeccionMarcas.Name = "lblSeccionMarcas"
        lblSeccionMarcas.Size = New Size(79, 30)
        lblSeccionMarcas.TabIndex = 0
        lblSeccionMarcas.Text = "Marcas"
        '
        ' lblIdMarca
        '
        lblIdMarca.AutoSize = True
        lblIdMarca.ForeColor = Color.DimGray
        lblIdMarca.Location = New Point(20, 63)
        lblIdMarca.Name = "lblIdMarca"
        lblIdMarca.Size = New Size(24, 20)
        lblIdMarca.TabIndex = 1
        lblIdMarca.Text = "ID"
        '
        ' txtIdMarca
        '
        txtIdMarca.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtIdMarca.Location = New Point(120, 60)
        txtIdMarca.Name = "txtIdMarca"
        txtIdMarca.ReadOnly = True
        txtIdMarca.Size = New Size(80, 27)
        txtIdMarca.TabIndex = 2
        txtIdMarca.TabStop = False
        '
        ' lblMarca
        '
        lblMarca.AutoSize = True
        lblMarca.Location = New Point(20, 103)
        lblMarca.Name = "lblMarca"
        lblMarca.Size = New Size(72, 20)
        lblMarca.TabIndex = 3
        lblMarca.Text = "Marca (*)"
        '
        ' txtMarca
        '
        txtMarca.Location = New Point(120, 100)
        txtMarca.MaxLength = 50
        txtMarca.Name = "txtMarca"
        txtMarca.Size = New Size(310, 27)
        txtMarca.TabIndex = 4
        '
        ' btnGuardarMarca
        '
        btnGuardarMarca.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGuardarMarca.Cursor = Cursors.Hand
        btnGuardarMarca.FlatAppearance.BorderSize = 0
        btnGuardarMarca.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGuardarMarca.FlatStyle = FlatStyle.Flat
        btnGuardarMarca.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGuardarMarca.ForeColor = Color.White
        btnGuardarMarca.Location = New Point(20, 145)
        btnGuardarMarca.Name = "btnGuardarMarca"
        btnGuardarMarca.Size = New Size(95, 38)
        btnGuardarMarca.TabIndex = 5
        btnGuardarMarca.Text = "Guardar"
        btnGuardarMarca.UseVisualStyleBackColor = False
        '
        ' btnModificarMarca
        '
        btnModificarMarca.BackColor = Color.White
        btnModificarMarca.Cursor = Cursors.Hand
        btnModificarMarca.FlatAppearance.BorderColor = Color.Silver
        btnModificarMarca.FlatStyle = FlatStyle.Flat
        btnModificarMarca.Font = New Font("Segoe UI", 10F)
        btnModificarMarca.Location = New Point(122, 145)
        btnModificarMarca.Name = "btnModificarMarca"
        btnModificarMarca.Size = New Size(95, 38)
        btnModificarMarca.TabIndex = 6
        btnModificarMarca.Text = "Modificar"
        btnModificarMarca.UseVisualStyleBackColor = False
        '
        ' btnEliminarMarca
        '
        btnEliminarMarca.BackColor = Color.White
        btnEliminarMarca.Cursor = Cursors.Hand
        btnEliminarMarca.FlatAppearance.BorderColor = Color.Silver
        btnEliminarMarca.FlatStyle = FlatStyle.Flat
        btnEliminarMarca.Font = New Font("Segoe UI", 10F)
        btnEliminarMarca.ForeColor = Color.Firebrick
        btnEliminarMarca.Location = New Point(224, 145)
        btnEliminarMarca.Name = "btnEliminarMarca"
        btnEliminarMarca.Size = New Size(95, 38)
        btnEliminarMarca.TabIndex = 7
        btnEliminarMarca.Text = "Eliminar"
        btnEliminarMarca.UseVisualStyleBackColor = False
        '
        ' btnLimpiarMarca
        '
        btnLimpiarMarca.BackColor = Color.White
        btnLimpiarMarca.Cursor = Cursors.Hand
        btnLimpiarMarca.FlatAppearance.BorderColor = Color.Silver
        btnLimpiarMarca.FlatStyle = FlatStyle.Flat
        btnLimpiarMarca.Font = New Font("Segoe UI", 10F)
        btnLimpiarMarca.Location = New Point(326, 145)
        btnLimpiarMarca.Name = "btnLimpiarMarca"
        btnLimpiarMarca.Size = New Size(95, 38)
        btnLimpiarMarca.TabIndex = 8
        btnLimpiarMarca.Text = "Limpiar"
        btnLimpiarMarca.UseVisualStyleBackColor = False
        '
        ' dgvMarcas
        '
        dgvMarcas.AllowUserToAddRows = False
        dgvMarcas.AllowUserToDeleteRows = False
        dgvMarcas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMarcas.BackgroundColor = Color.White
        dgvMarcas.BorderStyle = BorderStyle.FixedSingle
        dgvMarcas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMarcas.Location = New Point(20, 200)
        dgvMarcas.MultiSelect = False
        dgvMarcas.Name = "dgvMarcas"
        dgvMarcas.ReadOnly = True
        dgvMarcas.RowHeadersVisible = False
        dgvMarcas.RowHeadersWidth = 51
        dgvMarcas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMarcas.Size = New Size(410, 350)
        dgvMarcas.TabIndex = 9
        '
        ' pnlModelos
        '
        pnlModelos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlModelos.BackColor = Color.White
        pnlModelos.Controls.Add(lblSeccionModelos)
        pnlModelos.Controls.Add(lblMarcaSeleccionada)
        pnlModelos.Controls.Add(lblIdModelo)
        pnlModelos.Controls.Add(txtIdModelo)
        pnlModelos.Controls.Add(lblModelo)
        pnlModelos.Controls.Add(txtModelo)
        pnlModelos.Controls.Add(btnGuardarModelo)
        pnlModelos.Controls.Add(btnModificarModelo)
        pnlModelos.Controls.Add(btnEliminarModelo)
        pnlModelos.Controls.Add(btnLimpiarModelo)
        pnlModelos.Controls.Add(dgvModelos)
        pnlModelos.Location = New Point(510, 120)
        pnlModelos.Name = "pnlModelos"
        pnlModelos.Size = New Size(460, 570)
        pnlModelos.TabIndex = 3
        '
        ' lblSeccionModelos
        '
        lblSeccionModelos.AutoSize = True
        lblSeccionModelos.Font = New Font("Segoe UI", 13F, FontStyle.Bold)
        lblSeccionModelos.Location = New Point(20, 15)
        lblSeccionModelos.Name = "lblSeccionModelos"
        lblSeccionModelos.Size = New Size(91, 30)
        lblSeccionModelos.TabIndex = 0
        lblSeccionModelos.Text = "Modelos"
        '
        ' lblMarcaSeleccionada
        '
        lblMarcaSeleccionada.AutoSize = True
        lblMarcaSeleccionada.Font = New Font("Segoe UI", 10F)
        lblMarcaSeleccionada.ForeColor = Color.DimGray
        lblMarcaSeleccionada.Location = New Point(120, 21)
        lblMarcaSeleccionada.Name = "lblMarcaSeleccionada"
        lblMarcaSeleccionada.Size = New Size(236, 23)
        lblMarcaSeleccionada.TabIndex = 1
        lblMarcaSeleccionada.Text = "Seleccione una marca de la lista"
        '
        ' lblIdModelo
        '
        lblIdModelo.AutoSize = True
        lblIdModelo.ForeColor = Color.DimGray
        lblIdModelo.Location = New Point(20, 63)
        lblIdModelo.Name = "lblIdModelo"
        lblIdModelo.Size = New Size(24, 20)
        lblIdModelo.TabIndex = 2
        lblIdModelo.Text = "ID"
        '
        ' txtIdModelo
        '
        txtIdModelo.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtIdModelo.Location = New Point(120, 60)
        txtIdModelo.Name = "txtIdModelo"
        txtIdModelo.ReadOnly = True
        txtIdModelo.Size = New Size(80, 27)
        txtIdModelo.TabIndex = 3
        txtIdModelo.TabStop = False
        '
        ' lblModelo
        '
        lblModelo.AutoSize = True
        lblModelo.Location = New Point(20, 103)
        lblModelo.Name = "lblModelo"
        lblModelo.Size = New Size(80, 20)
        lblModelo.TabIndex = 4
        lblModelo.Text = "Modelo (*)"
        '
        ' txtModelo
        '
        txtModelo.Location = New Point(120, 100)
        txtModelo.MaxLength = 80
        txtModelo.Name = "txtModelo"
        txtModelo.Size = New Size(320, 27)
        txtModelo.TabIndex = 5
        '
        ' btnGuardarModelo
        '
        btnGuardarModelo.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGuardarModelo.Cursor = Cursors.Hand
        btnGuardarModelo.FlatAppearance.BorderSize = 0
        btnGuardarModelo.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGuardarModelo.FlatStyle = FlatStyle.Flat
        btnGuardarModelo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGuardarModelo.ForeColor = Color.White
        btnGuardarModelo.Location = New Point(20, 145)
        btnGuardarModelo.Name = "btnGuardarModelo"
        btnGuardarModelo.Size = New Size(95, 38)
        btnGuardarModelo.TabIndex = 6
        btnGuardarModelo.Text = "Guardar"
        btnGuardarModelo.UseVisualStyleBackColor = False
        '
        ' btnModificarModelo
        '
        btnModificarModelo.BackColor = Color.White
        btnModificarModelo.Cursor = Cursors.Hand
        btnModificarModelo.FlatAppearance.BorderColor = Color.Silver
        btnModificarModelo.FlatStyle = FlatStyle.Flat
        btnModificarModelo.Font = New Font("Segoe UI", 10F)
        btnModificarModelo.Location = New Point(122, 145)
        btnModificarModelo.Name = "btnModificarModelo"
        btnModificarModelo.Size = New Size(95, 38)
        btnModificarModelo.TabIndex = 7
        btnModificarModelo.Text = "Modificar"
        btnModificarModelo.UseVisualStyleBackColor = False
        '
        ' btnEliminarModelo
        '
        btnEliminarModelo.BackColor = Color.White
        btnEliminarModelo.Cursor = Cursors.Hand
        btnEliminarModelo.FlatAppearance.BorderColor = Color.Silver
        btnEliminarModelo.FlatStyle = FlatStyle.Flat
        btnEliminarModelo.Font = New Font("Segoe UI", 10F)
        btnEliminarModelo.ForeColor = Color.Firebrick
        btnEliminarModelo.Location = New Point(224, 145)
        btnEliminarModelo.Name = "btnEliminarModelo"
        btnEliminarModelo.Size = New Size(95, 38)
        btnEliminarModelo.TabIndex = 8
        btnEliminarModelo.Text = "Eliminar"
        btnEliminarModelo.UseVisualStyleBackColor = False
        '
        ' btnLimpiarModelo
        '
        btnLimpiarModelo.BackColor = Color.White
        btnLimpiarModelo.Cursor = Cursors.Hand
        btnLimpiarModelo.FlatAppearance.BorderColor = Color.Silver
        btnLimpiarModelo.FlatStyle = FlatStyle.Flat
        btnLimpiarModelo.Font = New Font("Segoe UI", 10F)
        btnLimpiarModelo.Location = New Point(326, 145)
        btnLimpiarModelo.Name = "btnLimpiarModelo"
        btnLimpiarModelo.Size = New Size(95, 38)
        btnLimpiarModelo.TabIndex = 9
        btnLimpiarModelo.Text = "Limpiar"
        btnLimpiarModelo.UseVisualStyleBackColor = False
        '
        ' dgvModelos
        '
        dgvModelos.AllowUserToAddRows = False
        dgvModelos.AllowUserToDeleteRows = False
        dgvModelos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvModelos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvModelos.BackgroundColor = Color.White
        dgvModelos.BorderStyle = BorderStyle.FixedSingle
        dgvModelos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvModelos.Location = New Point(20, 200)
        dgvModelos.MultiSelect = False
        dgvModelos.Name = "dgvModelos"
        dgvModelos.ReadOnly = True
        dgvModelos.RowHeadersVisible = False
        dgvModelos.RowHeadersWidth = 51
        dgvModelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvModelos.Size = New Size(420, 350)
        dgvModelos.TabIndex = 10
        '
        ' FrmMarcasModelos
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1010, 720)
        Controls.Add(pnlModelos)
        Controls.Add(pnlMarcas)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmMarcasModelos"
        Text = "Marcas y modelos"
        pnlMarcas.ResumeLayout(False)
        pnlMarcas.PerformLayout()
        CType(dgvMarcas, ComponentModel.ISupportInitialize).EndInit()
        pnlModelos.ResumeLayout(False)
        pnlModelos.PerformLayout()
        CType(dgvModelos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlMarcas As Panel
    Friend WithEvents lblSeccionMarcas As Label
    Friend WithEvents lblIdMarca As Label
    Friend WithEvents txtIdMarca As TextBox
    Friend WithEvents lblMarca As Label
    Friend WithEvents txtMarca As TextBox
    Friend WithEvents btnGuardarMarca As Button
    Friend WithEvents btnModificarMarca As Button
    Friend WithEvents btnEliminarMarca As Button
    Friend WithEvents btnLimpiarMarca As Button
    Friend WithEvents dgvMarcas As DataGridView
    Friend WithEvents pnlModelos As Panel
    Friend WithEvents lblSeccionModelos As Label
    Friend WithEvents lblMarcaSeleccionada As Label
    Friend WithEvents lblIdModelo As Label
    Friend WithEvents txtIdModelo As TextBox
    Friend WithEvents lblModelo As Label
    Friend WithEvents txtModelo As TextBox
    Friend WithEvents btnGuardarModelo As Button
    Friend WithEvents btnModificarModelo As Button
    Friend WithEvents btnEliminarModelo As Button
    Friend WithEvents btnLimpiarModelo As Button
    Friend WithEvents dgvModelos As DataGridView
End Class

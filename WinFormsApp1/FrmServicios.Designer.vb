<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmServicios
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
        lblID = New Label()
        txtID = New TextBox()
        lblEstado = New Label()
        lblCodigo = New Label()
        txtCodigo = New TextBox()
        lblCategoria = New Label()
        cboCategoria = New ComboBox()
        lblDescripcion = New Label()
        txtDescripcion = New TextBox()
        lblPrecio = New Label()
        nudPrecio = New NumericUpDown()
        chkTiempo = New CheckBox()
        nudTiempo = New NumericUpDown()
        btnGuardar = New Button()
        btnModificar = New Button()
        btnEliminar = New Button()
        btnLimpiar = New Button()
        txtFiltro = New TextBox()
        dgvServicios = New DataGridView()
        pnlDatos.SuspendLayout()
        CType(nudPrecio, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudTiempo, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvServicios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(40, 25)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(175, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Servicios"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(43, 78)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(290, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Administración del catálogo de servicios"
        '
        ' pnlDatos
        '
        pnlDatos.BackColor = Color.White
        pnlDatos.Controls.Add(lblID)
        pnlDatos.Controls.Add(txtID)
        pnlDatos.Controls.Add(lblEstado)
        pnlDatos.Controls.Add(lblCodigo)
        pnlDatos.Controls.Add(txtCodigo)
        pnlDatos.Controls.Add(lblCategoria)
        pnlDatos.Controls.Add(cboCategoria)
        pnlDatos.Controls.Add(lblDescripcion)
        pnlDatos.Controls.Add(txtDescripcion)
        pnlDatos.Controls.Add(lblPrecio)
        pnlDatos.Controls.Add(nudPrecio)
        pnlDatos.Controls.Add(chkTiempo)
        pnlDatos.Controls.Add(nudTiempo)
        pnlDatos.Controls.Add(btnGuardar)
        pnlDatos.Controls.Add(btnModificar)
        pnlDatos.Controls.Add(btnEliminar)
        pnlDatos.Controls.Add(btnLimpiar)
        pnlDatos.Location = New Point(40, 120)
        pnlDatos.Name = "pnlDatos"
        pnlDatos.Size = New Size(930, 220)
        pnlDatos.TabIndex = 2
        '
        ' lblID
        '
        lblID.AutoSize = True
        lblID.ForeColor = Color.DimGray
        lblID.Location = New Point(20, 21)
        lblID.Name = "lblID"
        lblID.Size = New Size(24, 20)
        lblID.TabIndex = 0
        lblID.Text = "ID"
        '
        ' txtID
        '
        txtID.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        txtID.Location = New Point(140, 18)
        txtID.Name = "txtID"
        txtID.ReadOnly = True
        txtID.Size = New Size(80, 27)
        txtID.TabIndex = 1
        txtID.TabStop = False
        '
        ' lblEstado
        '
        lblEstado.ForeColor = Color.DimGray
        lblEstado.Location = New Point(235, 21)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(400, 20)
        lblEstado.TabIndex = 2
        '
        ' lblCodigo
        '
        lblCodigo.AutoSize = True
        lblCodigo.Location = New Point(20, 63)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Size = New Size(78, 20)
        lblCodigo.TabIndex = 3
        lblCodigo.Text = "Código (*)"
        '
        ' txtCodigo
        '
        txtCodigo.CharacterCasing = CharacterCasing.Upper
        txtCodigo.Location = New Point(140, 60)
        txtCodigo.MaxLength = 30
        txtCodigo.Name = "txtCodigo"
        txtCodigo.Size = New Size(180, 27)
        txtCodigo.TabIndex = 4
        '
        ' lblCategoria
        '
        lblCategoria.AutoSize = True
        lblCategoria.Location = New Point(345, 63)
        lblCategoria.Name = "lblCategoria"
        lblCategoria.Size = New Size(96, 20)
        lblCategoria.TabIndex = 5
        lblCategoria.Text = "Categoría (*)"
        '
        ' cboCategoria
        '
        cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList
        cboCategoria.FormattingEnabled = True
        cboCategoria.Location = New Point(450, 60)
        cboCategoria.Name = "cboCategoria"
        cboCategoria.Size = New Size(310, 28)
        cboCategoria.TabIndex = 6
        '
        ' lblDescripcion
        '
        lblDescripcion.AutoSize = True
        lblDescripcion.Location = New Point(20, 103)
        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Size = New Size(109, 20)
        lblDescripcion.TabIndex = 7
        lblDescripcion.Text = "Descripción (*)"
        '
        ' txtDescripcion
        '
        txtDescripcion.Location = New Point(140, 100)
        txtDescripcion.MaxLength = 200
        txtDescripcion.Name = "txtDescripcion"
        txtDescripcion.Size = New Size(620, 27)
        txtDescripcion.TabIndex = 8
        '
        ' lblPrecio
        '
        lblPrecio.AutoSize = True
        lblPrecio.Location = New Point(20, 143)
        lblPrecio.Name = "lblPrecio"
        lblPrecio.Size = New Size(72, 20)
        lblPrecio.TabIndex = 9
        lblPrecio.Text = "Precio (*)"
        '
        ' nudPrecio
        '
        nudPrecio.DecimalPlaces = 2
        nudPrecio.Location = New Point(140, 140)
        nudPrecio.Maximum = New Decimal(New Integer() {-727379969, 232, 0, 131072})
        nudPrecio.Name = "nudPrecio"
        nudPrecio.Size = New Size(180, 27)
        nudPrecio.TabIndex = 10
        nudPrecio.TextAlign = HorizontalAlignment.Right
        nudPrecio.ThousandsSeparator = True
        '
        ' chkTiempo
        '
        chkTiempo.AutoSize = True
        chkTiempo.Location = New Point(345, 141)
        chkTiempo.Name = "chkTiempo"
        chkTiempo.Size = New Size(205, 24)
        chkTiempo.TabIndex = 11
        chkTiempo.Text = "Tiempo estimado (horas)"
        chkTiempo.UseVisualStyleBackColor = True
        '
        ' nudTiempo
        '
        nudTiempo.DecimalPlaces = 2
        nudTiempo.Enabled = False
        nudTiempo.Location = New Point(570, 140)
        nudTiempo.Maximum = New Decimal(New Integer() {99999, 0, 0, 131072})
        nudTiempo.Name = "nudTiempo"
        nudTiempo.Size = New Size(100, 27)
        nudTiempo.TabIndex = 12
        nudTiempo.TextAlign = HorizontalAlignment.Right
        '
        ' btnGuardar
        '
        btnGuardar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGuardar.Cursor = Cursors.Hand
        btnGuardar.FlatAppearance.BorderSize = 0
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGuardar.FlatStyle = FlatStyle.Flat
        btnGuardar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGuardar.ForeColor = Color.White
        btnGuardar.Location = New Point(790, 18)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(120, 40)
        btnGuardar.TabIndex = 13
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = False
        '
        ' btnModificar
        '
        btnModificar.BackColor = Color.White
        btnModificar.Cursor = Cursors.Hand
        btnModificar.FlatAppearance.BorderColor = Color.Silver
        btnModificar.FlatStyle = FlatStyle.Flat
        btnModificar.Font = New Font("Segoe UI", 10F)
        btnModificar.Location = New Point(790, 66)
        btnModificar.Name = "btnModificar"
        btnModificar.Size = New Size(120, 40)
        btnModificar.TabIndex = 14
        btnModificar.Text = "Modificar"
        btnModificar.UseVisualStyleBackColor = False
        '
        ' btnEliminar
        '
        btnEliminar.BackColor = Color.White
        btnEliminar.Cursor = Cursors.Hand
        btnEliminar.FlatAppearance.BorderColor = Color.Silver
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnEliminar.Font = New Font("Segoe UI", 10F)
        btnEliminar.ForeColor = Color.Firebrick
        btnEliminar.Location = New Point(790, 114)
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Size = New Size(120, 40)
        btnEliminar.TabIndex = 15
        btnEliminar.Text = "Dar de baja"
        btnEliminar.UseVisualStyleBackColor = False
        '
        ' btnLimpiar
        '
        btnLimpiar.BackColor = Color.White
        btnLimpiar.Cursor = Cursors.Hand
        btnLimpiar.FlatAppearance.BorderColor = Color.Silver
        btnLimpiar.FlatStyle = FlatStyle.Flat
        btnLimpiar.Font = New Font("Segoe UI", 10F)
        btnLimpiar.Location = New Point(790, 162)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New Size(120, 40)
        btnLimpiar.TabIndex = 16
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        ' txtFiltro
        '
        txtFiltro.Font = New Font("Segoe UI", 11F)
        txtFiltro.Location = New Point(40, 360)
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por código, descripción o categoría..."
        txtFiltro.Size = New Size(420, 32)
        txtFiltro.TabIndex = 3
        '
        ' dgvServicios
        '
        dgvServicios.AllowUserToAddRows = False
        dgvServicios.AllowUserToDeleteRows = False
        dgvServicios.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvServicios.BackgroundColor = Color.White
        dgvServicios.BorderStyle = BorderStyle.FixedSingle
        dgvServicios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvServicios.Location = New Point(40, 410)
        dgvServicios.MultiSelect = False
        dgvServicios.Name = "dgvServicios"
        dgvServicios.ReadOnly = True
        dgvServicios.RowHeadersVisible = False
        dgvServicios.RowHeadersWidth = 51
        dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvServicios.Size = New Size(930, 250)
        dgvServicios.TabIndex = 4
        '
        ' FrmServicios
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1010, 690)
        Controls.Add(dgvServicios)
        Controls.Add(txtFiltro)
        Controls.Add(pnlDatos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmServicios"
        Text = "Servicios"
        pnlDatos.ResumeLayout(False)
        pnlDatos.PerformLayout()
        CType(nudPrecio, ComponentModel.ISupportInitialize).EndInit()
        CType(nudTiempo, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvServicios, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblID As Label
    Friend WithEvents txtID As TextBox
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents lblCategoria As Label
    Friend WithEvents cboCategoria As ComboBox
    Friend WithEvents lblDescripcion As Label
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents lblPrecio As Label
    Friend WithEvents nudPrecio As NumericUpDown
    Friend WithEvents chkTiempo As CheckBox
    Friend WithEvents nudTiempo As NumericUpDown
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents dgvServicios As DataGridView
End Class

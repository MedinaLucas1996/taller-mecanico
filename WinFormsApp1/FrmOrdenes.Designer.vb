<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOrdenes
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
        lblResumen = New Label()
        dgvResumen = New DataGridView()
        pnlFiltros = New Panel()
        lblEstado = New Label()
        cboEstado = New ComboBox()
        chkDemoradas = New CheckBox()
        lblTexto = New Label()
        txtTexto = New TextBox()
        btnBuscar = New Button()
        btnLimpiar = New Button()
        dgvOrdenes = New DataGridView()
        btnGestionar = New Button()
        lblFotos = New Label()
        lblCantidad = New Label()
        pnlFotos = New Panel()
        lblFrente = New Label()
        lblSinFrente = New Label()
        picFrente = New PictureBox()
        lblTrasera = New Label()
        lblSinTrasera = New Label()
        picTrasera = New PictureBox()
        lblLateralIzq = New Label()
        lblSinLateralIzq = New Label()
        picLateralIzq = New PictureBox()
        lblLateralDer = New Label()
        lblSinLateralDer = New Label()
        picLateralDer = New PictureBox()
        lblTablero = New Label()
        lblSinTablero = New Label()
        picTablero = New PictureBox()
        CType(dgvResumen, ComponentModel.ISupportInitialize).BeginInit()
        pnlFiltros.SuspendLayout()
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).BeginInit()
        pnlFotos.SuspendLayout()
        CType(picFrente, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTrasera, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralIzq, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLateralDer, ComponentModel.ISupportInitialize).BeginInit()
        CType(picTablero, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(372, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Órdenes de trabajo"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(33, 62)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(345, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Estado de todas las órdenes de trabajo"
        '
        ' lblResumen
        '
        lblResumen.AutoSize = True
        lblResumen.Font = New Font("Segoe UI", 8.5F)
        lblResumen.ForeColor = Color.DimGray
        lblResumen.Location = New Point(570, 5)
        lblResumen.Name = "lblResumen"
        lblResumen.Size = New Size(262, 20)
        lblResumen.TabIndex = 2
        lblResumen.Text = "Órdenes por estado (todas, sin filtros)"
        '
        ' dgvResumen
        '
        dgvResumen.AllowUserToAddRows = False
        dgvResumen.AllowUserToDeleteRows = False
        dgvResumen.AllowUserToResizeRows = False
        dgvResumen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResumen.BackgroundColor = Color.White
        dgvResumen.BorderStyle = BorderStyle.FixedSingle
        dgvResumen.ColumnHeadersHeight = 26
        dgvResumen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvResumen.Location = New Point(570, 26)
        dgvResumen.MultiSelect = False
        dgvResumen.Name = "dgvResumen"
        dgvResumen.ReadOnly = True
        dgvResumen.RowHeadersVisible = False
        dgvResumen.RowHeadersWidth = 51
        dgvResumen.RowTemplate.Height = 22
        dgvResumen.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvResumen.Size = New Size(270, 204)
        dgvResumen.TabIndex = 3
        dgvResumen.TabStop = False
        '
        ' pnlFiltros
        '
        pnlFiltros.BackColor = Color.White
        pnlFiltros.Controls.Add(lblEstado)
        pnlFiltros.Controls.Add(cboEstado)
        pnlFiltros.Controls.Add(chkDemoradas)
        pnlFiltros.Controls.Add(lblTexto)
        pnlFiltros.Controls.Add(txtTexto)
        pnlFiltros.Controls.Add(btnBuscar)
        pnlFiltros.Controls.Add(btnLimpiar)
        pnlFiltros.Location = New Point(30, 96)
        pnlFiltros.Name = "pnlFiltros"
        pnlFiltros.Size = New Size(525, 134)
        pnlFiltros.TabIndex = 4
        '
        ' lblEstado
        '
        lblEstado.AutoSize = True
        lblEstado.Location = New Point(20, 19)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(54, 20)
        lblEstado.TabIndex = 0
        lblEstado.Text = "Estado"
        '
        ' cboEstado
        '
        cboEstado.DropDownStyle = ComboBoxStyle.DropDownList
        cboEstado.FormattingEnabled = True
        cboEstado.Location = New Point(150, 16)
        cboEstado.Name = "cboEstado"
        cboEstado.Size = New Size(200, 28)
        cboEstado.TabIndex = 1
        '
        ' chkDemoradas
        '
        chkDemoradas.AutoSize = True
        chkDemoradas.Location = New Point(370, 18)
        chkDemoradas.Name = "chkDemoradas"
        chkDemoradas.Size = New Size(135, 24)
        chkDemoradas.TabIndex = 2
        chkDemoradas.Text = "Solo demoradas"
        chkDemoradas.UseVisualStyleBackColor = True
        '
        ' lblTexto
        '
        lblTexto.AutoSize = True
        lblTexto.Location = New Point(20, 57)
        lblTexto.Name = "lblTexto"
        lblTexto.Size = New Size(122, 20)
        lblTexto.TabIndex = 3
        lblTexto.Text = "Patente o cliente"
        '
        ' txtTexto
        '
        txtTexto.Location = New Point(150, 54)
        txtTexto.MaxLength = 150
        txtTexto.Name = "txtTexto"
        txtTexto.PlaceholderText = "Parte de la patente o del nombre del cliente"
        txtTexto.Size = New Size(355, 27)
        txtTexto.TabIndex = 4
        '
        ' btnBuscar
        '
        btnBuscar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnBuscar.Cursor = Cursors.Hand
        btnBuscar.FlatAppearance.BorderSize = 0
        btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnBuscar.FlatStyle = FlatStyle.Flat
        btnBuscar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnBuscar.ForeColor = Color.White
        btnBuscar.Location = New Point(150, 91)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New Size(110, 34)
        btnBuscar.TabIndex = 5
        btnBuscar.Text = "Buscar"
        btnBuscar.UseVisualStyleBackColor = False
        '
        ' btnLimpiar
        '
        btnLimpiar.BackColor = Color.White
        btnLimpiar.Cursor = Cursors.Hand
        btnLimpiar.FlatAppearance.BorderColor = Color.Silver
        btnLimpiar.FlatStyle = FlatStyle.Flat
        btnLimpiar.Font = New Font("Segoe UI", 10F)
        btnLimpiar.Location = New Point(270, 91)
        btnLimpiar.Name = "btnLimpiar"
        btnLimpiar.Size = New Size(110, 34)
        btnLimpiar.TabIndex = 6
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        ' dgvOrdenes
        '
        dgvOrdenes.AllowUserToAddRows = False
        dgvOrdenes.AllowUserToDeleteRows = False
        dgvOrdenes.AllowUserToResizeRows = False
        dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvOrdenes.BackgroundColor = Color.White
        dgvOrdenes.BorderStyle = BorderStyle.FixedSingle
        dgvOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvOrdenes.Location = New Point(30, 238)
        dgvOrdenes.MultiSelect = False
        dgvOrdenes.Name = "dgvOrdenes"
        dgvOrdenes.ReadOnly = True
        dgvOrdenes.RowHeadersVisible = False
        dgvOrdenes.RowHeadersWidth = 51
        dgvOrdenes.RowTemplate.Height = 22
        dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrdenes.Size = New Size(810, 150)
        dgvOrdenes.TabIndex = 5
        '
        ' btnGestionar
        '
        btnGestionar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGestionar.Cursor = Cursors.Hand
        btnGestionar.Enabled = False
        btnGestionar.FlatAppearance.BorderSize = 0
        btnGestionar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGestionar.FlatStyle = FlatStyle.Flat
        btnGestionar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGestionar.ForeColor = Color.White
        btnGestionar.Location = New Point(30, 393)
        btnGestionar.Name = "btnGestionar"
        btnGestionar.Size = New Size(170, 32)
        btnGestionar.TabIndex = 9
        btnGestionar.Text = "Gestionar orden"
        btnGestionar.UseVisualStyleBackColor = False
        '
        ' lblFotos
        '
        lblFotos.AutoSize = True
        lblFotos.Location = New Point(30, 430)
        lblFotos.Name = "lblFotos"
        lblFotos.Size = New Size(432, 20)
        lblFotos.TabIndex = 6
        lblFotos.Text = "Seleccione una orden de la grilla para ver sus fotos de recepción."
        '
        ' lblCantidad
        '
        lblCantidad.ForeColor = Color.DimGray
        lblCantidad.Location = New Point(590, 430)
        lblCantidad.Name = "lblCantidad"
        lblCantidad.Size = New Size(250, 20)
        lblCantidad.TabIndex = 7
        lblCantidad.Text = "0 órdenes listadas"
        lblCantidad.TextAlign = ContentAlignment.TopRight
        '
        ' pnlFotos
        '
        pnlFotos.BackColor = Color.White
        pnlFotos.Controls.Add(lblFrente)
        pnlFotos.Controls.Add(lblSinFrente)
        pnlFotos.Controls.Add(picFrente)
        pnlFotos.Controls.Add(lblTrasera)
        pnlFotos.Controls.Add(lblSinTrasera)
        pnlFotos.Controls.Add(picTrasera)
        pnlFotos.Controls.Add(lblLateralIzq)
        pnlFotos.Controls.Add(lblSinLateralIzq)
        pnlFotos.Controls.Add(picLateralIzq)
        pnlFotos.Controls.Add(lblLateralDer)
        pnlFotos.Controls.Add(lblSinLateralDer)
        pnlFotos.Controls.Add(picLateralDer)
        pnlFotos.Controls.Add(lblTablero)
        pnlFotos.Controls.Add(lblSinTablero)
        pnlFotos.Controls.Add(picTablero)
        pnlFotos.Location = New Point(30, 454)
        pnlFotos.Name = "pnlFotos"
        pnlFotos.Size = New Size(810, 168)
        pnlFotos.TabIndex = 8
        '
        ' lblFrente
        '
        lblFrente.AutoSize = True
        lblFrente.Location = New Point(20, 8)
        lblFrente.Name = "lblFrente"
        lblFrente.Size = New Size(50, 20)
        lblFrente.TabIndex = 0
        lblFrente.Text = "Frente"
        '
        ' lblSinFrente
        '
        lblSinFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinFrente.ForeColor = Color.Gray
        lblSinFrente.Location = New Point(33, 85)
        lblSinFrente.Name = "lblSinFrente"
        lblSinFrente.Size = New Size(120, 20)
        lblSinFrente.TabIndex = 1
        lblSinFrente.Text = "Sin foto"
        lblSinFrente.TextAlign = ContentAlignment.MiddleCenter
        lblSinFrente.Visible = False
        '
        ' picFrente
        '
        picFrente.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picFrente.BorderStyle = BorderStyle.FixedSingle
        picFrente.Location = New Point(20, 30)
        picFrente.Name = "picFrente"
        picFrente.Size = New Size(146, 130)
        picFrente.SizeMode = PictureBoxSizeMode.Zoom
        picFrente.TabIndex = 2
        picFrente.TabStop = False
        '
        ' lblTrasera
        '
        lblTrasera.AutoSize = True
        lblTrasera.Location = New Point(176, 8)
        lblTrasera.Name = "lblTrasera"
        lblTrasera.Size = New Size(56, 20)
        lblTrasera.TabIndex = 3
        lblTrasera.Text = "Trasera"
        '
        ' lblSinTrasera
        '
        lblSinTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinTrasera.ForeColor = Color.Gray
        lblSinTrasera.Location = New Point(189, 85)
        lblSinTrasera.Name = "lblSinTrasera"
        lblSinTrasera.Size = New Size(120, 20)
        lblSinTrasera.TabIndex = 4
        lblSinTrasera.Text = "Sin foto"
        lblSinTrasera.TextAlign = ContentAlignment.MiddleCenter
        lblSinTrasera.Visible = False
        '
        ' picTrasera
        '
        picTrasera.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTrasera.BorderStyle = BorderStyle.FixedSingle
        picTrasera.Location = New Point(176, 30)
        picTrasera.Name = "picTrasera"
        picTrasera.Size = New Size(146, 130)
        picTrasera.SizeMode = PictureBoxSizeMode.Zoom
        picTrasera.TabIndex = 5
        picTrasera.TabStop = False
        '
        ' lblLateralIzq
        '
        lblLateralIzq.AutoSize = True
        lblLateralIzq.Location = New Point(332, 8)
        lblLateralIzq.Name = "lblLateralIzq"
        lblLateralIzq.Size = New Size(119, 20)
        lblLateralIzq.TabIndex = 6
        lblLateralIzq.Text = "Lateral izquierdo"
        '
        ' lblSinLateralIzq
        '
        lblSinLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinLateralIzq.ForeColor = Color.Gray
        lblSinLateralIzq.Location = New Point(345, 85)
        lblSinLateralIzq.Name = "lblSinLateralIzq"
        lblSinLateralIzq.Size = New Size(120, 20)
        lblSinLateralIzq.TabIndex = 7
        lblSinLateralIzq.Text = "Sin foto"
        lblSinLateralIzq.TextAlign = ContentAlignment.MiddleCenter
        lblSinLateralIzq.Visible = False
        '
        ' picLateralIzq
        '
        picLateralIzq.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralIzq.BorderStyle = BorderStyle.FixedSingle
        picLateralIzq.Location = New Point(332, 30)
        picLateralIzq.Name = "picLateralIzq"
        picLateralIzq.Size = New Size(146, 130)
        picLateralIzq.SizeMode = PictureBoxSizeMode.Zoom
        picLateralIzq.TabIndex = 8
        picLateralIzq.TabStop = False
        '
        ' lblLateralDer
        '
        lblLateralDer.AutoSize = True
        lblLateralDer.Location = New Point(488, 8)
        lblLateralDer.Name = "lblLateralDer"
        lblLateralDer.Size = New Size(111, 20)
        lblLateralDer.TabIndex = 9
        lblLateralDer.Text = "Lateral derecho"
        '
        ' lblSinLateralDer
        '
        lblSinLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinLateralDer.ForeColor = Color.Gray
        lblSinLateralDer.Location = New Point(501, 85)
        lblSinLateralDer.Name = "lblSinLateralDer"
        lblSinLateralDer.Size = New Size(120, 20)
        lblSinLateralDer.TabIndex = 10
        lblSinLateralDer.Text = "Sin foto"
        lblSinLateralDer.TextAlign = ContentAlignment.MiddleCenter
        lblSinLateralDer.Visible = False
        '
        ' picLateralDer
        '
        picLateralDer.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picLateralDer.BorderStyle = BorderStyle.FixedSingle
        picLateralDer.Location = New Point(488, 30)
        picLateralDer.Name = "picLateralDer"
        picLateralDer.Size = New Size(146, 130)
        picLateralDer.SizeMode = PictureBoxSizeMode.Zoom
        picLateralDer.TabIndex = 11
        picLateralDer.TabStop = False
        '
        ' lblTablero
        '
        lblTablero.AutoSize = True
        lblTablero.Location = New Point(644, 8)
        lblTablero.Name = "lblTablero"
        lblTablero.Size = New Size(58, 20)
        lblTablero.TabIndex = 12
        lblTablero.Text = "Tablero"
        '
        ' lblSinTablero
        '
        lblSinTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        lblSinTablero.ForeColor = Color.Gray
        lblSinTablero.Location = New Point(657, 85)
        lblSinTablero.Name = "lblSinTablero"
        lblSinTablero.Size = New Size(120, 20)
        lblSinTablero.TabIndex = 13
        lblSinTablero.Text = "Sin foto"
        lblSinTablero.TextAlign = ContentAlignment.MiddleCenter
        lblSinTablero.Visible = False
        '
        ' picTablero
        '
        picTablero.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        picTablero.BorderStyle = BorderStyle.FixedSingle
        picTablero.Location = New Point(644, 30)
        picTablero.Name = "picTablero"
        picTablero.Size = New Size(146, 130)
        picTablero.SizeMode = PictureBoxSizeMode.Zoom
        picTablero.TabIndex = 14
        picTablero.TabStop = False
        '
        ' FrmOrdenes
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(pnlFotos)
        Controls.Add(lblCantidad)
        Controls.Add(lblFotos)
        Controls.Add(btnGestionar)
        Controls.Add(dgvOrdenes)
        Controls.Add(pnlFiltros)
        Controls.Add(dgvResumen)
        Controls.Add(lblResumen)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmOrdenes"
        Text = "Órdenes de trabajo"
        CType(dgvResumen, ComponentModel.ISupportInitialize).EndInit()
        pnlFiltros.ResumeLayout(False)
        pnlFiltros.PerformLayout()
        CType(dgvOrdenes, ComponentModel.ISupportInitialize).EndInit()
        pnlFotos.ResumeLayout(False)
        pnlFotos.PerformLayout()
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
    Friend WithEvents lblResumen As Label
    Friend WithEvents dgvResumen As DataGridView
    Friend WithEvents pnlFiltros As Panel
    Friend WithEvents lblEstado As Label
    Friend WithEvents cboEstado As ComboBox
    Friend WithEvents chkDemoradas As CheckBox
    Friend WithEvents lblTexto As Label
    Friend WithEvents txtTexto As TextBox
    Friend WithEvents btnBuscar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents dgvOrdenes As DataGridView
    Friend WithEvents btnGestionar As Button
    Friend WithEvents lblFotos As Label
    Friend WithEvents lblCantidad As Label
    Friend WithEvents pnlFotos As Panel
    Friend WithEvents lblFrente As Label
    Friend WithEvents lblSinFrente As Label
    Friend WithEvents picFrente As PictureBox
    Friend WithEvents lblTrasera As Label
    Friend WithEvents lblSinTrasera As Label
    Friend WithEvents picTrasera As PictureBox
    Friend WithEvents lblLateralIzq As Label
    Friend WithEvents lblSinLateralIzq As Label
    Friend WithEvents picLateralIzq As PictureBox
    Friend WithEvents lblLateralDer As Label
    Friend WithEvents lblSinLateralDer As Label
    Friend WithEvents picLateralDer As PictureBox
    Friend WithEvents lblTablero As Label
    Friend WithEvents lblSinTablero As Label
    Friend WithEvents picTablero As PictureBox
End Class

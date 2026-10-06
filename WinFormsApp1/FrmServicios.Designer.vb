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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        lblTitulo = New Label()
        lblSubtitulo = New Label()
        btnNuevo = New Button()
        pnlLista = New Panel()
        picBuscar = New PictureBox()
        txtFiltro = New TextBox()
        chkBajas = New CheckBox()
        dgvServicios = New DataGridView()
        pnlFicha = New Panel()
        lblAyuda = New Label()
        pnlRegistro = New Panel()
        lblRegistroTitulo = New Label()
        lblEstadoRegistro = New Label()
        tlpCampos = New TableLayoutPanel()
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
        lblContexto = New Label()
        btnGuardar = New Button()
        btnBaja = New Button()
        btnCancelar = New Button()
        pnlLista.SuspendLayout()
        pnlFicha.SuspendLayout()
        pnlRegistro.SuspendLayout()
        tlpCampos.SuspendLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvServicios, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudPrecio, ComponentModel.ISupportInitialize).BeginInit()
        CType(nudTiempo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(520, 60)
        lblTitulo.TabIndex = 3
        lblTitulo.Text = "Servicios"
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
        ' btnNuevo
        '
        btnNuevo.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnNuevo.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnNuevo.Cursor = Cursors.Hand
        btnNuevo.FlatAppearance.BorderSize = 0
        btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnNuevo.FlatStyle = FlatStyle.Flat
        btnNuevo.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnNuevo.ForeColor = Color.White
        btnNuevo.Location = New Point(640, 34)
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Size = New Size(200, 40)
        btnNuevo.TabIndex = 0
        btnNuevo.Text = " Nuevo servicio"
        btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText
        btnNuevo.UseVisualStyleBackColor = False
        '
        ' pnlLista
        '
        pnlLista.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlLista.BackColor = Color.White
        pnlLista.Controls.Add(picBuscar)
        pnlLista.Controls.Add(txtFiltro)
        pnlLista.Controls.Add(chkBajas)
        pnlLista.Controls.Add(dgvServicios)
        pnlLista.Location = New Point(30, 110)
        pnlLista.Name = "pnlLista"
        pnlLista.Size = New Size(420, 510)
        pnlLista.TabIndex = 1
        '
        ' picBuscar
        '
        picBuscar.Location = New Point(16, 18)
        picBuscar.Name = "picBuscar"
        picBuscar.Size = New Size(24, 28)
        picBuscar.SizeMode = PictureBoxSizeMode.Normal
        picBuscar.TabIndex = 3
        picBuscar.TabStop = False
        '
        ' txtFiltro
        '
        txtFiltro.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtFiltro.Location = New Point(46, 16)
        txtFiltro.MaxLength = 150
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por código, descripción o categoría"
        txtFiltro.Size = New Size(358, 27)
        txtFiltro.TabIndex = 0
        '
        ' chkBajas
        '
        chkBajas.AutoSize = True
        chkBajas.Location = New Point(16, 52)
        chkBajas.Name = "chkBajas"
        chkBajas.Size = New Size(190, 24)
        chkBajas.TabIndex = 1
        chkBajas.Text = "Mostrar dados de baja"
        chkBajas.UseVisualStyleBackColor = True
        '
        ' dgvServicios
        '
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.White
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.DimGray
        DataGridViewCellStyle1.SelectionBackColor = Color.White
        DataGridViewCellStyle1.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        dgvServicios.AllowUserToAddRows = False
        dgvServicios.AllowUserToDeleteRows = False
        dgvServicios.AllowUserToResizeRows = False
        dgvServicios.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvServicios.BackgroundColor = Color.White
        dgvServicios.BorderStyle = BorderStyle.None
        dgvServicios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvServicios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvServicios.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvServicios.ColumnHeadersHeight = 40
        dgvServicios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvServicios.EnableHeadersVisualStyles = False
        dgvServicios.GridColor = Color.Gainsboro
        dgvServicios.Location = New Point(16, 86)
        dgvServicios.MultiSelect = False
        dgvServicios.Name = "dgvServicios"
        dgvServicios.ReadOnly = True
        dgvServicios.RowHeadersVisible = False
        dgvServicios.RowHeadersWidth = 51
        dgvServicios.RowTemplate.Height = 26
        dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvServicios.Size = New Size(388, 408)
        dgvServicios.StandardTab = True
        dgvServicios.TabIndex = 2
        '
        ' pnlFicha
        '
        pnlFicha.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pnlFicha.BackColor = Color.White
        pnlFicha.Controls.Add(lblAyuda)
        pnlFicha.Controls.Add(pnlRegistro)
        pnlFicha.Location = New Point(460, 110)
        pnlFicha.Name = "pnlFicha"
        pnlFicha.Size = New Size(380, 510)
        pnlFicha.TabIndex = 2
        '
        ' lblAyuda
        '
        lblAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(16, 16)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(348, 48)
        lblAyuda.TabIndex = 1
        lblAyuda.Text = "Seleccione un servicio de la lista o cree uno nuevo."
        '
        ' pnlRegistro
        '
        pnlRegistro.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlRegistro.Controls.Add(lblRegistroTitulo)
        pnlRegistro.Controls.Add(lblEstadoRegistro)
        pnlRegistro.Controls.Add(tlpCampos)
        pnlRegistro.Controls.Add(btnGuardar)
        pnlRegistro.Controls.Add(btnBaja)
        pnlRegistro.Controls.Add(btnCancelar)
        pnlRegistro.Location = New Point(0, 0)
        pnlRegistro.Name = "pnlRegistro"
        pnlRegistro.Size = New Size(380, 510)
        pnlRegistro.TabIndex = 0
        pnlRegistro.Visible = False
        '
        ' lblRegistroTitulo
        '
        lblRegistroTitulo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblRegistroTitulo.AutoEllipsis = True
        lblRegistroTitulo.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblRegistroTitulo.Location = New Point(16, 12)
        lblRegistroTitulo.Name = "lblRegistroTitulo"
        lblRegistroTitulo.Size = New Size(348, 30)
        lblRegistroTitulo.TabIndex = 3
        lblRegistroTitulo.Text = "Nuevo servicio"
        lblRegistroTitulo.TextAlign = ContentAlignment.MiddleLeft
        '
        ' lblEstadoRegistro
        '
        lblEstadoRegistro.AutoSize = True
        lblEstadoRegistro.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        lblEstadoRegistro.Location = New Point(18, 46)
        lblEstadoRegistro.Name = "lblEstadoRegistro"
        lblEstadoRegistro.Padding = New Padding(8, 3, 8, 3)
        lblEstadoRegistro.Size = New Size(64, 25)
        lblEstadoRegistro.TabIndex = 4
        lblEstadoRegistro.Text = "Activo"
        '
        ' tlpCampos
        '
        tlpCampos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpCampos.ColumnCount = 2
        tlpCampos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpCampos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpCampos.Controls.Add(lblCodigo, 0, 0)
        tlpCampos.Controls.Add(txtCodigo, 0, 1)
        tlpCampos.Controls.Add(lblCategoria, 1, 0)
        tlpCampos.Controls.Add(cboCategoria, 1, 1)
        tlpCampos.Controls.Add(lblDescripcion, 0, 2)
        tlpCampos.Controls.Add(txtDescripcion, 0, 3)
        tlpCampos.Controls.Add(lblPrecio, 0, 4)
        tlpCampos.Controls.Add(nudPrecio, 0, 5)
        tlpCampos.Controls.Add(chkTiempo, 1, 4)
        tlpCampos.Controls.Add(nudTiempo, 1, 5)
        tlpCampos.Controls.Add(lblContexto, 0, 6)
        tlpCampos.Location = New Point(16, 78)
        tlpCampos.Name = "tlpCampos"
        tlpCampos.RowCount = 8
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpCampos.Size = New Size(348, 364)
        tlpCampos.TabIndex = 0
        tlpCampos.SetColumnSpan(lblDescripcion, 2)
        tlpCampos.SetColumnSpan(txtDescripcion, 2)
        tlpCampos.SetColumnSpan(lblContexto, 2)
        '
        ' lblCodigo
        '
        lblCodigo.AutoSize = True
        lblCodigo.Font = New Font("Segoe UI", 8.25F)
        lblCodigo.ForeColor = Color.DimGray
        lblCodigo.Location = New Point(0, 0)
        lblCodigo.Margin = New Padding(0, 8, 6, 2)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Size = New Size(100, 19)
        lblCodigo.TabIndex = 0
        lblCodigo.Text = "Código (*)"
        '
        ' txtCodigo
        '
        txtCodigo.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtCodigo.CharacterCasing = CharacterCasing.Upper
        txtCodigo.Location = New Point(0, 0)
        txtCodigo.Margin = New Padding(0, 0, 6, 0)
        txtCodigo.MaxLength = 30
        txtCodigo.Name = "txtCodigo"
        txtCodigo.Size = New Size(168, 27)
        txtCodigo.TabIndex = 1
        '
        ' lblCategoria
        '
        lblCategoria.AutoSize = True
        lblCategoria.Font = New Font("Segoe UI", 8.25F)
        lblCategoria.ForeColor = Color.DimGray
        lblCategoria.Location = New Point(0, 0)
        lblCategoria.Margin = New Padding(6, 8, 0, 2)
        lblCategoria.Name = "lblCategoria"
        lblCategoria.Size = New Size(100, 19)
        lblCategoria.TabIndex = 2
        lblCategoria.Text = "Categoría (*)"
        '
        ' cboCategoria
        '
        cboCategoria.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList
        cboCategoria.FormattingEnabled = True
        cboCategoria.Location = New Point(0, 0)
        cboCategoria.Margin = New Padding(6, 0, 0, 0)
        cboCategoria.Name = "cboCategoria"
        cboCategoria.Size = New Size(168, 28)
        cboCategoria.TabIndex = 3
        '
        ' lblDescripcion
        '
        lblDescripcion.AutoSize = True
        lblDescripcion.Font = New Font("Segoe UI", 8.25F)
        lblDescripcion.ForeColor = Color.DimGray
        lblDescripcion.Location = New Point(0, 0)
        lblDescripcion.Margin = New Padding(0, 8, 0, 2)
        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Size = New Size(100, 19)
        lblDescripcion.TabIndex = 4
        lblDescripcion.Text = "Descripción (*)"
        '
        ' txtDescripcion
        '
        txtDescripcion.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtDescripcion.Location = New Point(0, 0)
        txtDescripcion.Margin = New Padding(0, 0, 0, 0)
        txtDescripcion.MaxLength = 200
        txtDescripcion.Name = "txtDescripcion"
        txtDescripcion.Size = New Size(348, 27)
        txtDescripcion.TabIndex = 5
        '
        ' lblPrecio
        '
        lblPrecio.AutoSize = True
        lblPrecio.Font = New Font("Segoe UI", 8.25F)
        lblPrecio.ForeColor = Color.DimGray
        lblPrecio.Location = New Point(0, 0)
        lblPrecio.Margin = New Padding(0, 8, 6, 2)
        lblPrecio.Name = "lblPrecio"
        lblPrecio.Size = New Size(100, 19)
        lblPrecio.TabIndex = 6
        lblPrecio.Text = "Precio (*)"
        '
        ' nudPrecio
        '
        nudPrecio.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        nudPrecio.DecimalPlaces = 2
        nudPrecio.Location = New Point(0, 0)
        nudPrecio.Margin = New Padding(0, 0, 6, 0)
        nudPrecio.Maximum = New Decimal(New Integer() {-727379969, 232, 0, 131072})
        nudPrecio.Name = "nudPrecio"
        nudPrecio.Size = New Size(168, 27)
        nudPrecio.TabIndex = 7
        nudPrecio.TextAlign = HorizontalAlignment.Right
        nudPrecio.ThousandsSeparator = True
        '
        ' chkTiempo
        '
        chkTiempo.AutoSize = True
        chkTiempo.Font = New Font("Segoe UI", 8.25F)
        chkTiempo.ForeColor = Color.DimGray
        chkTiempo.Location = New Point(0, 0)
        chkTiempo.Margin = New Padding(6, 6, 0, 0)
        chkTiempo.Name = "chkTiempo"
        chkTiempo.Size = New Size(100, 23)
        chkTiempo.TabIndex = 8
        chkTiempo.Text = "Tiempo estimado (h)"
        chkTiempo.UseVisualStyleBackColor = True
        '
        ' nudTiempo
        '
        nudTiempo.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        nudTiempo.DecimalPlaces = 2
        nudTiempo.Enabled = False
        nudTiempo.Location = New Point(0, 0)
        nudTiempo.Margin = New Padding(6, 0, 0, 0)
        nudTiempo.Maximum = New Decimal(New Integer() {99999, 0, 0, 131072})
        nudTiempo.Name = "nudTiempo"
        nudTiempo.Size = New Size(168, 27)
        nudTiempo.TabIndex = 9
        nudTiempo.TextAlign = HorizontalAlignment.Right
        '
        ' lblContexto
        '
        lblContexto.AutoSize = True
        lblContexto.Font = New Font("Segoe UI", 8.25F)
        lblContexto.ForeColor = Color.DimGray
        lblContexto.Location = New Point(0, 0)
        lblContexto.Margin = New Padding(0, 10, 0, 0)
        lblContexto.Name = "lblContexto"
        lblContexto.Size = New Size(100, 19)
        lblContexto.TabIndex = 10
        lblContexto.Text = "-"
        '
        ' btnGuardar
        '
        btnGuardar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnGuardar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnGuardar.Cursor = Cursors.Hand
        btnGuardar.FlatAppearance.BorderSize = 0
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnGuardar.FlatStyle = FlatStyle.Flat
        btnGuardar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnGuardar.ForeColor = Color.White
        btnGuardar.Location = New Point(174, 454)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(190, 40)
        btnGuardar.TabIndex = 1
        btnGuardar.Text = " Guardar"
        btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnGuardar.UseVisualStyleBackColor = False
        '
        ' btnBaja
        '
        btnBaja.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnBaja.BackColor = Color.White
        btnBaja.Cursor = Cursors.Hand
        btnBaja.FlatAppearance.BorderColor = Color.Silver
        btnBaja.FlatStyle = FlatStyle.Flat
        btnBaja.Font = New Font("Segoe UI", 10F)
        btnBaja.ForeColor = Color.Firebrick
        btnBaja.Location = New Point(16, 454)
        btnBaja.Name = "btnBaja"
        btnBaja.Size = New Size(146, 40)
        btnBaja.TabIndex = 2
        btnBaja.Text = " Dar de baja"
        btnBaja.TextImageRelation = TextImageRelation.ImageBeforeText
        btnBaja.UseVisualStyleBackColor = False
        '
        ' btnCancelar
        '
        btnCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCancelar.BackColor = Color.White
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.FlatAppearance.BorderColor = Color.Silver
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 10F)
        btnCancelar.Location = New Point(16, 454)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(146, 40)
        btnCancelar.TabIndex = 3
        btnCancelar.Text = " Cancelar"
        btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCancelar.UseVisualStyleBackColor = False
        btnCancelar.Visible = False
        '
        ' FrmServicios
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(pnlFicha)
        Controls.Add(pnlLista)
        Controls.Add(btnNuevo)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmServicios"
        Text = "Servicios"
        pnlLista.ResumeLayout(False)
        pnlLista.PerformLayout()
        pnlFicha.ResumeLayout(False)
        pnlRegistro.ResumeLayout(False)
        pnlRegistro.PerformLayout()
        tlpCampos.ResumeLayout(False)
        tlpCampos.PerformLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvServicios, ComponentModel.ISupportInitialize).EndInit()
        CType(nudPrecio, ComponentModel.ISupportInitialize).EndInit()
        CType(nudTiempo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents btnNuevo As Button
    Friend WithEvents pnlLista As Panel
    Friend WithEvents picBuscar As PictureBox
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents chkBajas As CheckBox
    Friend WithEvents dgvServicios As DataGridView
    Friend WithEvents pnlFicha As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents pnlRegistro As Panel
    Friend WithEvents lblRegistroTitulo As Label
    Friend WithEvents lblEstadoRegistro As Label
    Friend WithEvents tlpCampos As TableLayoutPanel
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
    Friend WithEvents lblContexto As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnBaja As Button
    Friend WithEvents btnCancelar As Button
End Class

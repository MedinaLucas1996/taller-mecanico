<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTurnos
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
        btnNuevo = New Button()
        btnDiaAnterior = New Button()
        dtpDia = New DateTimePicker()
        btnDiaSiguiente = New Button()
        btnHoy = New Button()
        pnlLista = New Panel()
        lblCerrado = New Label()
        dgvAgenda = New DataGridView()
        pnlFicha = New Panel()
        lblAyuda = New Label()
        pnlRegistro = New Panel()
        lblRegistroTitulo = New Label()
        lblEstadoRegistro = New Label()
        tlpCampos = New TableLayoutPanel()
        lblPatente = New Label()
        txtPatente = New TextBox()
        btnBuscarPatente = New Button()
        lblVehiculo = New Label()
        lblHora = New Label()
        cboHora = New ComboBox()
        lblMotivo = New Label()
        txtMotivo = New TextBox()
        lblDetalle = New Label()
        btnCancelarAlta = New Button()
        btnGuardar = New Button()
        btnCancelarTurno = New Button()
        btnAusente = New Button()
        pnlLista.SuspendLayout()
        CType(dgvAgenda, ComponentModel.ISupportInitialize).BeginInit()
        pnlFicha.SuspendLayout()
        pnlRegistro.SuspendLayout()
        tlpCampos.SuspendLayout()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(140, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Turnos"
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
        btnNuevo.TabIndex = 2
        btnNuevo.Text = " Nuevo turno"
        btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText
        btnNuevo.UseVisualStyleBackColor = False
        '
        ' btnDiaAnterior
        '
        btnDiaAnterior.BackColor = Color.White
        btnDiaAnterior.Cursor = Cursors.Hand
        btnDiaAnterior.FlatAppearance.BorderColor = Color.Silver
        btnDiaAnterior.FlatStyle = FlatStyle.Flat
        btnDiaAnterior.Font = New Font("Segoe UI", 10F)
        btnDiaAnterior.Location = New Point(30, 106)
        btnDiaAnterior.Name = "btnDiaAnterior"
        btnDiaAnterior.Size = New Size(40, 32)
        btnDiaAnterior.TabIndex = 3
        btnDiaAnterior.Text = "<"
        btnDiaAnterior.UseVisualStyleBackColor = False
        '
        ' dtpDia
        '
        dtpDia.Font = New Font("Segoe UI", 10F)
        dtpDia.Format = DateTimePickerFormat.Long
        dtpDia.Location = New Point(76, 108)
        dtpDia.Name = "dtpDia"
        dtpDia.Size = New Size(280, 30)
        dtpDia.TabIndex = 4
        '
        ' btnDiaSiguiente
        '
        btnDiaSiguiente.BackColor = Color.White
        btnDiaSiguiente.Cursor = Cursors.Hand
        btnDiaSiguiente.FlatAppearance.BorderColor = Color.Silver
        btnDiaSiguiente.FlatStyle = FlatStyle.Flat
        btnDiaSiguiente.Font = New Font("Segoe UI", 10F)
        btnDiaSiguiente.Location = New Point(362, 106)
        btnDiaSiguiente.Name = "btnDiaSiguiente"
        btnDiaSiguiente.Size = New Size(40, 32)
        btnDiaSiguiente.TabIndex = 5
        btnDiaSiguiente.Text = ">"
        btnDiaSiguiente.UseVisualStyleBackColor = False
        '
        ' btnHoy
        '
        btnHoy.BackColor = Color.White
        btnHoy.Cursor = Cursors.Hand
        btnHoy.FlatAppearance.BorderColor = Color.Silver
        btnHoy.FlatStyle = FlatStyle.Flat
        btnHoy.Font = New Font("Segoe UI", 10F)
        btnHoy.Location = New Point(408, 106)
        btnHoy.Name = "btnHoy"
        btnHoy.Size = New Size(70, 32)
        btnHoy.TabIndex = 6
        btnHoy.Text = "Hoy"
        btnHoy.UseVisualStyleBackColor = False
        '
        ' pnlLista
        '
        pnlLista.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlLista.BackColor = Color.White
        pnlLista.Controls.Add(lblCerrado)
        pnlLista.Controls.Add(dgvAgenda)
        pnlLista.Location = New Point(30, 150)
        pnlLista.Name = "pnlLista"
        pnlLista.Size = New Size(470, 470)
        pnlLista.TabIndex = 7
        '
        ' lblCerrado
        '
        lblCerrado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblCerrado.BackColor = Color.White
        lblCerrado.Font = New Font("Segoe UI", 12F)
        lblCerrado.ForeColor = Color.DimGray
        lblCerrado.Location = New Point(16, 16)
        lblCerrado.Name = "lblCerrado"
        lblCerrado.Size = New Size(438, 438)
        lblCerrado.TabIndex = 1
        lblCerrado.Text = "El taller no atiende los domingos."
        lblCerrado.TextAlign = ContentAlignment.MiddleCenter
        lblCerrado.Visible = False
        '
        ' dgvAgenda
        '
        dgvAgenda.AllowUserToAddRows = False
        dgvAgenda.AllowUserToDeleteRows = False
        dgvAgenda.AllowUserToResizeRows = False
        dgvAgenda.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvAgenda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAgenda.BackgroundColor = Color.White
        dgvAgenda.BorderStyle = BorderStyle.None
        dgvAgenda.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvAgenda.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvAgenda.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAgenda.EnableHeadersVisualStyles = False
        dgvAgenda.GridColor = Color.Gainsboro
        dgvAgenda.Location = New Point(16, 16)
        dgvAgenda.MultiSelect = False
        dgvAgenda.Name = "dgvAgenda"
        dgvAgenda.ReadOnly = True
        dgvAgenda.RowHeadersVisible = False
        dgvAgenda.RowHeadersWidth = 51
        dgvAgenda.RowTemplate.Height = 28
        dgvAgenda.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAgenda.Size = New Size(438, 438)
        dgvAgenda.TabIndex = 0
        '
        ' pnlFicha
        '
        pnlFicha.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pnlFicha.BackColor = Color.White
        pnlFicha.Controls.Add(lblAyuda)
        pnlFicha.Controls.Add(pnlRegistro)
        pnlFicha.Location = New Point(510, 150)
        pnlFicha.Name = "pnlFicha"
        pnlFicha.Size = New Size(330, 470)
        pnlFicha.TabIndex = 8
        '
        ' lblAyuda
        '
        lblAyuda.Font = New Font("Segoe UI", 10F)
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(16, 16)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(298, 90)
        lblAyuda.TabIndex = 0
        lblAyuda.Text = "Elija un lugar libre de la agenda para dar un turno, o un turno para verlo, cancelarlo o marcarlo como ausente."
        '
        ' pnlRegistro
        '
        pnlRegistro.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlRegistro.Controls.Add(lblRegistroTitulo)
        pnlRegistro.Controls.Add(lblEstadoRegistro)
        pnlRegistro.Controls.Add(tlpCampos)
        pnlRegistro.Controls.Add(lblDetalle)
        pnlRegistro.Controls.Add(btnCancelarAlta)
        pnlRegistro.Controls.Add(btnGuardar)
        pnlRegistro.Controls.Add(btnCancelarTurno)
        pnlRegistro.Controls.Add(btnAusente)
        pnlRegistro.Location = New Point(0, 0)
        pnlRegistro.Name = "pnlRegistro"
        pnlRegistro.Size = New Size(330, 470)
        pnlRegistro.TabIndex = 1
        pnlRegistro.Visible = False
        '
        ' lblRegistroTitulo
        '
        lblRegistroTitulo.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblRegistroTitulo.Location = New Point(16, 12)
        lblRegistroTitulo.Name = "lblRegistroTitulo"
        lblRegistroTitulo.Size = New Size(298, 30)
        lblRegistroTitulo.TabIndex = 0
        lblRegistroTitulo.Text = "-"
        '
        ' lblEstadoRegistro
        '
        lblEstadoRegistro.AutoSize = True
        lblEstadoRegistro.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblEstadoRegistro.Location = New Point(18, 46)
        lblEstadoRegistro.Name = "lblEstadoRegistro"
        lblEstadoRegistro.Padding = New Padding(6, 3, 6, 3)
        lblEstadoRegistro.Size = New Size(64, 25)
        lblEstadoRegistro.TabIndex = 1
        lblEstadoRegistro.Text = "-"
        '
        ' tlpCampos
        '
        tlpCampos.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        tlpCampos.ColumnCount = 2
        tlpCampos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpCampos.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 96F))
        tlpCampos.Controls.Add(lblPatente, 0, 0)
        tlpCampos.Controls.Add(txtPatente, 0, 1)
        tlpCampos.Controls.Add(btnBuscarPatente, 1, 1)
        tlpCampos.Controls.Add(lblVehiculo, 0, 2)
        tlpCampos.Controls.Add(lblHora, 0, 3)
        tlpCampos.Controls.Add(cboHora, 0, 4)
        tlpCampos.Controls.Add(lblMotivo, 0, 5)
        tlpCampos.Controls.Add(txtMotivo, 0, 6)
        tlpCampos.Location = New Point(16, 52)
        tlpCampos.Name = "tlpCampos"
        tlpCampos.RowCount = 7
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.Size = New Size(298, 330)
        tlpCampos.TabIndex = 2
        tlpCampos.SetColumnSpan(lblPatente, 2)
        tlpCampos.SetColumnSpan(lblVehiculo, 2)
        tlpCampos.SetColumnSpan(lblHora, 2)
        tlpCampos.SetColumnSpan(cboHora, 2)
        tlpCampos.SetColumnSpan(lblMotivo, 2)
        tlpCampos.SetColumnSpan(txtMotivo, 2)
        '
        ' lblPatente
        '
        lblPatente.AutoSize = True
        lblPatente.Font = New Font("Segoe UI", 8.25F)
        lblPatente.ForeColor = Color.DimGray
        lblPatente.Location = New Point(0, 0)
        lblPatente.Margin = New Padding(0, 8, 0, 2)
        lblPatente.Name = "lblPatente"
        lblPatente.Size = New Size(100, 19)
        lblPatente.TabIndex = 0
        lblPatente.Text = "Patente (*)"
        '
        ' txtPatente
        '
        txtPatente.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtPatente.CharacterCasing = CharacterCasing.Upper
        txtPatente.Location = New Point(0, 0)
        txtPatente.Margin = New Padding(0, 0, 6, 0)
        txtPatente.MaxLength = 10
        txtPatente.Name = "txtPatente"
        txtPatente.Size = New Size(196, 27)
        txtPatente.TabIndex = 1
        '
        ' btnBuscarPatente
        '
        btnBuscarPatente.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnBuscarPatente.BackColor = Color.White
        btnBuscarPatente.Cursor = Cursors.Hand
        btnBuscarPatente.FlatAppearance.BorderColor = Color.Silver
        btnBuscarPatente.FlatStyle = FlatStyle.Flat
        btnBuscarPatente.Location = New Point(0, 0)
        btnBuscarPatente.Margin = New Padding(0)
        btnBuscarPatente.Name = "btnBuscarPatente"
        btnBuscarPatente.Size = New Size(96, 30)
        btnBuscarPatente.TabIndex = 2
        btnBuscarPatente.Text = " Buscar"
        btnBuscarPatente.TextImageRelation = TextImageRelation.ImageBeforeText
        btnBuscarPatente.UseVisualStyleBackColor = False
        '
        ' lblVehiculo
        '
        lblVehiculo.AutoSize = True
        lblVehiculo.Font = New Font("Segoe UI", 9F)
        lblVehiculo.Location = New Point(0, 0)
        lblVehiculo.Margin = New Padding(0, 6, 0, 0)
        lblVehiculo.MaximumSize = New Size(298, 0)
        lblVehiculo.Name = "lblVehiculo"
        lblVehiculo.Size = New Size(100, 20)
        lblVehiculo.TabIndex = 3
        lblVehiculo.Text = "-"
        '
        ' lblHora
        '
        lblHora.AutoSize = True
        lblHora.Font = New Font("Segoe UI", 8.25F)
        lblHora.ForeColor = Color.DimGray
        lblHora.Location = New Point(0, 0)
        lblHora.Margin = New Padding(0, 10, 0, 2)
        lblHora.Name = "lblHora"
        lblHora.Size = New Size(100, 19)
        lblHora.TabIndex = 4
        lblHora.Text = "Hora (*)"
        '
        ' cboHora
        '
        cboHora.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cboHora.DropDownStyle = ComboBoxStyle.DropDownList
        cboHora.Location = New Point(0, 0)
        cboHora.Margin = New Padding(0)
        cboHora.Name = "cboHora"
        cboHora.Size = New Size(298, 28)
        cboHora.TabIndex = 5
        '
        ' lblMotivo
        '
        lblMotivo.AutoSize = True
        lblMotivo.Font = New Font("Segoe UI", 8.25F)
        lblMotivo.ForeColor = Color.DimGray
        lblMotivo.Location = New Point(0, 0)
        lblMotivo.Margin = New Padding(0, 10, 0, 2)
        lblMotivo.Name = "lblMotivo"
        lblMotivo.Size = New Size(100, 19)
        lblMotivo.TabIndex = 6
        lblMotivo.Text = "Motivo"
        '
        ' txtMotivo
        '
        txtMotivo.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtMotivo.Location = New Point(0, 0)
        txtMotivo.Margin = New Padding(0)
        txtMotivo.MaxLength = 200
        txtMotivo.Multiline = True
        txtMotivo.Name = "txtMotivo"
        txtMotivo.ScrollBars = ScrollBars.Vertical
        txtMotivo.Size = New Size(298, 80)
        txtMotivo.TabIndex = 7
        '
        ' lblDetalle
        '
        lblDetalle.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblDetalle.Font = New Font("Segoe UI", 10F)
        lblDetalle.Location = New Point(16, 84)
        lblDetalle.Name = "lblDetalle"
        lblDetalle.Size = New Size(298, 310)
        lblDetalle.TabIndex = 3
        lblDetalle.Text = "-"
        lblDetalle.Visible = False
        '
        ' btnCancelarAlta
        '
        btnCancelarAlta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCancelarAlta.BackColor = Color.White
        btnCancelarAlta.Cursor = Cursors.Hand
        btnCancelarAlta.FlatAppearance.BorderColor = Color.Silver
        btnCancelarAlta.FlatStyle = FlatStyle.Flat
        btnCancelarAlta.Font = New Font("Segoe UI", 10F)
        btnCancelarAlta.Location = New Point(16, 414)
        btnCancelarAlta.Name = "btnCancelarAlta"
        btnCancelarAlta.Size = New Size(140, 40)
        btnCancelarAlta.TabIndex = 4
        btnCancelarAlta.Text = " Cancelar"
        btnCancelarAlta.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCancelarAlta.UseVisualStyleBackColor = False
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
        btnGuardar.Location = New Point(164, 414)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(150, 40)
        btnGuardar.TabIndex = 5
        btnGuardar.Text = " Dar turno"
        btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnGuardar.UseVisualStyleBackColor = False
        '
        ' btnCancelarTurno
        '
        btnCancelarTurno.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCancelarTurno.BackColor = Color.White
        btnCancelarTurno.Cursor = Cursors.Hand
        btnCancelarTurno.FlatAppearance.BorderColor = Color.Silver
        btnCancelarTurno.FlatStyle = FlatStyle.Flat
        btnCancelarTurno.Font = New Font("Segoe UI", 10F)
        btnCancelarTurno.ForeColor = Color.Firebrick
        btnCancelarTurno.Location = New Point(16, 414)
        btnCancelarTurno.Name = "btnCancelarTurno"
        btnCancelarTurno.Size = New Size(140, 40)
        btnCancelarTurno.TabIndex = 6
        btnCancelarTurno.Text = " Cancelar turno"
        btnCancelarTurno.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCancelarTurno.UseVisualStyleBackColor = False
        '
        ' btnAusente
        '
        btnAusente.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnAusente.BackColor = Color.White
        btnAusente.Cursor = Cursors.Hand
        btnAusente.FlatAppearance.BorderColor = Color.Silver
        btnAusente.FlatStyle = FlatStyle.Flat
        btnAusente.Font = New Font("Segoe UI", 10F)
        btnAusente.Location = New Point(164, 414)
        btnAusente.Name = "btnAusente"
        btnAusente.Size = New Size(150, 40)
        btnAusente.TabIndex = 7
        btnAusente.Text = "Marcar ausente"
        btnAusente.UseVisualStyleBackColor = False
        '
        ' FrmTurnos
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(pnlFicha)
        Controls.Add(pnlLista)
        Controls.Add(btnHoy)
        Controls.Add(btnDiaSiguiente)
        Controls.Add(dtpDia)
        Controls.Add(btnDiaAnterior)
        Controls.Add(btnNuevo)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmTurnos"
        Text = "Turnos"
        pnlLista.ResumeLayout(False)
        CType(dgvAgenda, ComponentModel.ISupportInitialize).EndInit()
        pnlFicha.ResumeLayout(False)
        pnlRegistro.ResumeLayout(False)
        pnlRegistro.PerformLayout()
        tlpCampos.ResumeLayout(False)
        tlpCampos.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnDiaAnterior As Button
    Friend WithEvents dtpDia As DateTimePicker
    Friend WithEvents btnDiaSiguiente As Button
    Friend WithEvents btnHoy As Button
    Friend WithEvents pnlLista As Panel
    Friend WithEvents lblCerrado As Label
    Friend WithEvents dgvAgenda As DataGridView
    Friend WithEvents pnlFicha As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents pnlRegistro As Panel
    Friend WithEvents lblRegistroTitulo As Label
    Friend WithEvents lblEstadoRegistro As Label
    Friend WithEvents tlpCampos As TableLayoutPanel
    Friend WithEvents lblPatente As Label
    Friend WithEvents txtPatente As TextBox
    Friend WithEvents btnBuscarPatente As Button
    Friend WithEvents lblVehiculo As Label
    Friend WithEvents lblHora As Label
    Friend WithEvents cboHora As ComboBox
    Friend WithEvents lblMotivo As Label
    Friend WithEvents txtMotivo As TextBox
    Friend WithEvents lblDetalle As Label
    Friend WithEvents btnCancelarAlta As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnCancelarTurno As Button
    Friend WithEvents btnAusente As Button
End Class

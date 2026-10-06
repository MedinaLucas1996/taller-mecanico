<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUsuarios
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
        dgvUsuarios = New DataGridView()
        pnlFicha = New Panel()
        lblAyuda = New Label()
        pnlRegistro = New Panel()
        lblRegistroTitulo = New Label()
        lblEstadoRegistro = New Label()
        tlpCampos = New TableLayoutPanel()
        lblUsuario = New Label()
        txtUsuario = New TextBox()
        lblNombreCompleto = New Label()
        txtNombreCompleto = New TextBox()
        lblRol = New Label()
        cboRol = New ComboBox()
        lblMecanico = New Label()
        cboMecanico = New ComboBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        lblAyudaPassword = New Label()
        lblContexto = New Label()
        btnGuardar = New Button()
        btnBaja = New Button()
        btnCancelar = New Button()
        pnlLista.SuspendLayout()
        pnlFicha.SuspendLayout()
        pnlRegistro.SuspendLayout()
        tlpCampos.SuspendLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvUsuarios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(520, 60)
        lblTitulo.TabIndex = 3
        lblTitulo.Text = "Usuarios"
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
        btnNuevo.Text = " Nuevo usuario"
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
        pnlLista.Controls.Add(dgvUsuarios)
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
        txtFiltro.PlaceholderText = "Buscar por usuario o nombre completo"
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
        ' dgvUsuarios
        '
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.White
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.DimGray
        DataGridViewCellStyle1.SelectionBackColor = Color.White
        DataGridViewCellStyle1.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        dgvUsuarios.AllowUserToAddRows = False
        dgvUsuarios.AllowUserToDeleteRows = False
        dgvUsuarios.AllowUserToResizeRows = False
        dgvUsuarios.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvUsuarios.BackgroundColor = Color.White
        dgvUsuarios.BorderStyle = BorderStyle.None
        dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvUsuarios.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvUsuarios.ColumnHeadersHeight = 40
        dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvUsuarios.EnableHeadersVisualStyles = False
        dgvUsuarios.GridColor = Color.Gainsboro
        dgvUsuarios.Location = New Point(16, 86)
        dgvUsuarios.MultiSelect = False
        dgvUsuarios.Name = "dgvUsuarios"
        dgvUsuarios.ReadOnly = True
        dgvUsuarios.RowHeadersVisible = False
        dgvUsuarios.RowHeadersWidth = 51
        dgvUsuarios.RowTemplate.Height = 26
        dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvUsuarios.Size = New Size(388, 408)
        dgvUsuarios.StandardTab = True
        dgvUsuarios.TabIndex = 2
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
        lblAyuda.Text = "Seleccione un usuario de la lista o cree uno nuevo."
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
        lblRegistroTitulo.Text = "Nuevo usuario"
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
        tlpCampos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 40F))
        tlpCampos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 60F))
        tlpCampos.Controls.Add(lblUsuario, 0, 0)
        tlpCampos.Controls.Add(txtUsuario, 0, 1)
        tlpCampos.Controls.Add(lblNombreCompleto, 0, 2)
        tlpCampos.Controls.Add(txtNombreCompleto, 0, 3)
        tlpCampos.Controls.Add(lblRol, 0, 4)
        tlpCampos.Controls.Add(cboRol, 0, 5)
        tlpCampos.Controls.Add(lblMecanico, 1, 4)
        tlpCampos.Controls.Add(cboMecanico, 1, 5)
        tlpCampos.Controls.Add(lblPassword, 0, 6)
        tlpCampos.Controls.Add(txtPassword, 0, 7)
        tlpCampos.Controls.Add(lblAyudaPassword, 0, 9)
        tlpCampos.Controls.Add(lblContexto, 0, 10)
        tlpCampos.Location = New Point(16, 78)
        tlpCampos.Name = "tlpCampos"
        tlpCampos.RowCount = 12
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.RowStyles.Add(New RowStyle())
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
        tlpCampos.SetColumnSpan(lblUsuario, 2)
        tlpCampos.SetColumnSpan(txtUsuario, 2)
        tlpCampos.SetColumnSpan(lblNombreCompleto, 2)
        tlpCampos.SetColumnSpan(txtNombreCompleto, 2)
        tlpCampos.SetColumnSpan(lblPassword, 2)
        tlpCampos.SetColumnSpan(txtPassword, 2)
        tlpCampos.SetColumnSpan(lblAyudaPassword, 2)
        tlpCampos.SetColumnSpan(lblContexto, 2)
        '
        ' lblUsuario
        '
        lblUsuario.AutoSize = True
        lblUsuario.Font = New Font("Segoe UI", 8.25F)
        lblUsuario.ForeColor = Color.DimGray
        lblUsuario.Location = New Point(0, 0)
        lblUsuario.Margin = New Padding(0, 8, 0, 2)
        lblUsuario.Name = "lblUsuario"
        lblUsuario.Size = New Size(100, 19)
        lblUsuario.TabIndex = 0
        lblUsuario.Text = "Usuario (*)"
        '
        ' txtUsuario
        '
        txtUsuario.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtUsuario.Location = New Point(0, 0)
        txtUsuario.Margin = New Padding(0, 0, 0, 0)
        txtUsuario.MaxLength = 50
        txtUsuario.Name = "txtUsuario"
        txtUsuario.Size = New Size(348, 27)
        txtUsuario.TabIndex = 1
        '
        ' lblNombreCompleto
        '
        lblNombreCompleto.AutoSize = True
        lblNombreCompleto.Font = New Font("Segoe UI", 8.25F)
        lblNombreCompleto.ForeColor = Color.DimGray
        lblNombreCompleto.Location = New Point(0, 0)
        lblNombreCompleto.Margin = New Padding(0, 8, 0, 2)
        lblNombreCompleto.Name = "lblNombreCompleto"
        lblNombreCompleto.Size = New Size(100, 19)
        lblNombreCompleto.TabIndex = 2
        lblNombreCompleto.Text = "Nombre completo (*)"
        '
        ' txtNombreCompleto
        '
        txtNombreCompleto.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtNombreCompleto.Location = New Point(0, 0)
        txtNombreCompleto.Margin = New Padding(0, 0, 0, 0)
        txtNombreCompleto.MaxLength = 100
        txtNombreCompleto.Name = "txtNombreCompleto"
        txtNombreCompleto.Size = New Size(348, 27)
        txtNombreCompleto.TabIndex = 3
        '
        ' lblRol
        '
        lblRol.AutoSize = True
        lblRol.Font = New Font("Segoe UI", 8.25F)
        lblRol.ForeColor = Color.DimGray
        lblRol.Location = New Point(0, 0)
        lblRol.Margin = New Padding(0, 8, 6, 2)
        lblRol.Name = "lblRol"
        lblRol.Size = New Size(100, 19)
        lblRol.TabIndex = 4
        lblRol.Text = "Rol (*)"
        '
        ' cboRol
        '
        cboRol.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cboRol.DropDownStyle = ComboBoxStyle.DropDownList
        cboRol.FormattingEnabled = True
        cboRol.Items.AddRange(New Object() {"Seleccione un rol", "ADMINISTRADOR", "OPERADOR", "MECANICO"})
        cboRol.Location = New Point(0, 0)
        cboRol.Margin = New Padding(0, 0, 6, 0)
        cboRol.Name = "cboRol"
        cboRol.Size = New Size(133, 28)
        cboRol.TabIndex = 5
        '
        ' lblMecanico
        '
        lblMecanico.AutoSize = True
        lblMecanico.Font = New Font("Segoe UI", 8.25F)
        lblMecanico.ForeColor = Color.DimGray
        lblMecanico.Location = New Point(0, 0)
        lblMecanico.Margin = New Padding(6, 8, 0, 2)
        lblMecanico.Name = "lblMecanico"
        lblMecanico.Size = New Size(100, 19)
        lblMecanico.TabIndex = 6
        lblMecanico.Text = "Mecánico (*)"
        '
        ' cboMecanico
        '
        cboMecanico.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cboMecanico.DropDownStyle = ComboBoxStyle.DropDownList
        cboMecanico.FormattingEnabled = True
        cboMecanico.Location = New Point(0, 0)
        cboMecanico.Margin = New Padding(6, 0, 0, 0)
        cboMecanico.Name = "cboMecanico"
        cboMecanico.Size = New Size(203, 28)
        cboMecanico.TabIndex = 7
        '
        ' lblPassword
        '
        lblPassword.AutoSize = True
        lblPassword.Font = New Font("Segoe UI", 8.25F)
        lblPassword.ForeColor = Color.DimGray
        lblPassword.Location = New Point(0, 0)
        lblPassword.Margin = New Padding(0, 8, 0, 2)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(100, 19)
        lblPassword.TabIndex = 8
        lblPassword.Text = "Contraseña (*)"
        '
        ' txtPassword
        '
        txtPassword.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtPassword.Location = New Point(0, 0)
        txtPassword.Margin = New Padding(0, 0, 0, 0)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(348, 27)
        txtPassword.TabIndex = 9
        txtPassword.UseSystemPasswordChar = True
        '
        ' lblAyudaPassword
        '
        lblAyudaPassword.AutoSize = True
        lblAyudaPassword.Font = New Font("Segoe UI", 8.25F)
        lblAyudaPassword.ForeColor = Color.DimGray
        lblAyudaPassword.Location = New Point(0, 0)
        lblAyudaPassword.Margin = New Padding(0, 4, 0, 0)
        lblAyudaPassword.Name = "lblAyudaPassword"
        lblAyudaPassword.Size = New Size(348, 19)
        lblAyudaPassword.TabIndex = 10
        lblAyudaPassword.Text = "Dejar vacía para conservar la contraseña actual."
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
        lblContexto.TabIndex = 11
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
        ' FrmUsuarios
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
        Name = "FrmUsuarios"
        Text = "Usuarios"
        pnlLista.ResumeLayout(False)
        pnlLista.PerformLayout()
        pnlFicha.ResumeLayout(False)
        pnlRegistro.ResumeLayout(False)
        pnlRegistro.PerformLayout()
        tlpCampos.ResumeLayout(False)
        tlpCampos.PerformLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvUsuarios, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents dgvUsuarios As DataGridView
    Friend WithEvents pnlFicha As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents pnlRegistro As Panel
    Friend WithEvents lblRegistroTitulo As Label
    Friend WithEvents lblEstadoRegistro As Label
    Friend WithEvents tlpCampos As TableLayoutPanel
    Friend WithEvents lblUsuario As Label
    Friend WithEvents txtUsuario As TextBox
    Friend WithEvents lblNombreCompleto As Label
    Friend WithEvents txtNombreCompleto As TextBox
    Friend WithEvents lblRol As Label
    Friend WithEvents cboRol As ComboBox
    Friend WithEvents lblMecanico As Label
    Friend WithEvents cboMecanico As ComboBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblAyudaPassword As Label
    Friend WithEvents lblContexto As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnBaja As Button
    Friend WithEvents btnCancelar As Button
End Class

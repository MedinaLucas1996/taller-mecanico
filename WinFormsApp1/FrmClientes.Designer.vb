<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmClientes
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
        dgvClientes = New DataGridView()
        pnlFicha = New Panel()
        lblAyuda = New Label()
        pnlRegistro = New Panel()
        lblRegistroTitulo = New Label()
        lblEstadoRegistro = New Label()
        tlpCampos = New TableLayoutPanel()
        lblRazonSocial = New Label()
        txtRazonSocial = New TextBox()
        lblDocumento = New Label()
        txtDocumento = New TextBox()
        lblTelefono = New Label()
        txtTelefono = New TextBox()
        lblDomicilio = New Label()
        txtDomicilio = New TextBox()
        lblLocalidad = New Label()
        txtLocalidad = New TextBox()
        lblEmail = New Label()
        txtEmail = New TextBox()
        lblObservaciones = New Label()
        txtObservaciones = New TextBox()
        lblContexto = New Label()
        btnGuardar = New Button()
        btnBaja = New Button()
        btnCancelar = New Button()
        pnlLista.SuspendLayout()
        pnlFicha.SuspendLayout()
        pnlRegistro.SuspendLayout()
        tlpCampos.SuspendLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvClientes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(520, 60)
        lblTitulo.TabIndex = 3
        lblTitulo.Text = "Clientes"
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
        lblSubtitulo.Text = "0 clientes activos"
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
        btnNuevo.Text = " Nuevo cliente"
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
        pnlLista.Controls.Add(dgvClientes)
        pnlLista.Location = New Point(30, 110)
        pnlLista.Name = "pnlLista"
        pnlLista.Size = New Size(420, 510)
        pnlLista.TabIndex = 0
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
        txtFiltro.PlaceholderText = "Buscar por nombre o documento"
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
        ' dgvClientes
        '
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.White
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.DimGray
        DataGridViewCellStyle1.SelectionBackColor = Color.White
        DataGridViewCellStyle1.SelectionForeColor = Color.DimGray
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        dgvClientes.AllowUserToAddRows = False
        dgvClientes.AllowUserToDeleteRows = False
        dgvClientes.AllowUserToResizeRows = False
        dgvClientes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvClientes.BackgroundColor = Color.White
        dgvClientes.BorderStyle = BorderStyle.None
        dgvClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvClientes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvClientes.ColumnHeadersHeight = 40
        dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvClientes.EnableHeadersVisualStyles = False
        dgvClientes.GridColor = Color.Gainsboro
        dgvClientes.Location = New Point(16, 86)
        dgvClientes.MultiSelect = False
        dgvClientes.Name = "dgvClientes"
        dgvClientes.ReadOnly = True
        dgvClientes.RowHeadersVisible = False
        dgvClientes.RowHeadersWidth = 51
        dgvClientes.RowTemplate.Height = 26
        dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvClientes.Size = New Size(388, 408)
        dgvClientes.TabIndex = 2
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
        pnlFicha.TabIndex = 1
        '
        ' lblAyuda
        '
        lblAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(16, 16)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(348, 48)
        lblAyuda.TabIndex = 1
        lblAyuda.Text = "Seleccione un cliente de la lista o cree uno nuevo."
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
        lblRegistroTitulo.Text = "Nuevo cliente"
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
        tlpCampos.Controls.Add(lblRazonSocial, 0, 0)
        tlpCampos.Controls.Add(txtRazonSocial, 0, 1)
        tlpCampos.Controls.Add(lblDocumento, 0, 2)
        tlpCampos.Controls.Add(txtDocumento, 0, 3)
        tlpCampos.Controls.Add(lblTelefono, 1, 2)
        tlpCampos.Controls.Add(txtTelefono, 1, 3)
        tlpCampos.Controls.Add(lblDomicilio, 0, 4)
        tlpCampos.Controls.Add(txtDomicilio, 0, 5)
        tlpCampos.Controls.Add(lblLocalidad, 0, 6)
        tlpCampos.Controls.Add(txtLocalidad, 0, 7)
        tlpCampos.Controls.Add(lblEmail, 1, 6)
        tlpCampos.Controls.Add(txtEmail, 1, 7)
        tlpCampos.Controls.Add(lblObservaciones, 0, 8)
        tlpCampos.Controls.Add(txtObservaciones, 0, 9)
        tlpCampos.Controls.Add(lblContexto, 0, 10)
        tlpCampos.Location = New Point(16, 78)
        tlpCampos.Name = "tlpCampos"
        tlpCampos.RowCount = 11
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
        tlpCampos.RowStyles.Add(New RowStyle())
        tlpCampos.Size = New Size(348, 364)
        tlpCampos.TabIndex = 0
        tlpCampos.SetColumnSpan(lblRazonSocial, 2)
        tlpCampos.SetColumnSpan(txtRazonSocial, 2)
        tlpCampos.SetColumnSpan(lblDomicilio, 2)
        tlpCampos.SetColumnSpan(txtDomicilio, 2)
        tlpCampos.SetColumnSpan(lblObservaciones, 2)
        tlpCampos.SetColumnSpan(txtObservaciones, 2)
        tlpCampos.SetColumnSpan(lblContexto, 2)
        '
        ' lblRazonSocial
        '
        lblRazonSocial.AutoSize = True
        lblRazonSocial.Font = New Font("Segoe UI", 8.25F)
        lblRazonSocial.ForeColor = Color.DimGray
        lblRazonSocial.Location = New Point(0, 0)
        lblRazonSocial.Margin = New Padding(0, 8, 0, 2)
        lblRazonSocial.Name = "lblRazonSocial"
        lblRazonSocial.Size = New Size(100, 19)
        lblRazonSocial.TabIndex = 0
        lblRazonSocial.Text = "Nombre o razón social (*)"
        '
        ' txtRazonSocial
        '
        txtRazonSocial.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtRazonSocial.Location = New Point(0, 0)
        txtRazonSocial.Margin = New Padding(0, 0, 0, 0)
        txtRazonSocial.MaxLength = 150
        txtRazonSocial.Name = "txtRazonSocial"
        txtRazonSocial.Size = New Size(348, 27)
        txtRazonSocial.TabIndex = 1
        '
        ' lblDocumento
        '
        lblDocumento.AutoSize = True
        lblDocumento.Font = New Font("Segoe UI", 8.25F)
        lblDocumento.ForeColor = Color.DimGray
        lblDocumento.Location = New Point(0, 0)
        lblDocumento.Margin = New Padding(0, 8, 6, 2)
        lblDocumento.Name = "lblDocumento"
        lblDocumento.Size = New Size(100, 19)
        lblDocumento.TabIndex = 2
        lblDocumento.Text = "Documento (*)"
        '
        ' txtDocumento
        '
        txtDocumento.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtDocumento.Location = New Point(0, 0)
        txtDocumento.Margin = New Padding(0, 0, 6, 0)
        txtDocumento.MaxLength = 20
        txtDocumento.Name = "txtDocumento"
        txtDocumento.Size = New Size(133, 27)
        txtDocumento.TabIndex = 3
        '
        ' lblTelefono
        '
        lblTelefono.AutoSize = True
        lblTelefono.Font = New Font("Segoe UI", 8.25F)
        lblTelefono.ForeColor = Color.DimGray
        lblTelefono.Location = New Point(0, 0)
        lblTelefono.Margin = New Padding(6, 8, 0, 2)
        lblTelefono.Name = "lblTelefono"
        lblTelefono.Size = New Size(100, 19)
        lblTelefono.TabIndex = 4
        lblTelefono.Text = "Teléfono"
        '
        ' txtTelefono
        '
        txtTelefono.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtTelefono.Location = New Point(0, 0)
        txtTelefono.Margin = New Padding(6, 0, 0, 0)
        txtTelefono.MaxLength = 30
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Size = New Size(203, 27)
        txtTelefono.TabIndex = 5
        '
        ' lblDomicilio
        '
        lblDomicilio.AutoSize = True
        lblDomicilio.Font = New Font("Segoe UI", 8.25F)
        lblDomicilio.ForeColor = Color.DimGray
        lblDomicilio.Location = New Point(0, 0)
        lblDomicilio.Margin = New Padding(0, 8, 0, 2)
        lblDomicilio.Name = "lblDomicilio"
        lblDomicilio.Size = New Size(100, 19)
        lblDomicilio.TabIndex = 6
        lblDomicilio.Text = "Domicilio"
        '
        ' txtDomicilio
        '
        txtDomicilio.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtDomicilio.Location = New Point(0, 0)
        txtDomicilio.Margin = New Padding(0, 0, 0, 0)
        txtDomicilio.MaxLength = 200
        txtDomicilio.Name = "txtDomicilio"
        txtDomicilio.Size = New Size(348, 27)
        txtDomicilio.TabIndex = 7
        '
        ' lblLocalidad
        '
        lblLocalidad.AutoSize = True
        lblLocalidad.Font = New Font("Segoe UI", 8.25F)
        lblLocalidad.ForeColor = Color.DimGray
        lblLocalidad.Location = New Point(0, 0)
        lblLocalidad.Margin = New Padding(0, 8, 6, 2)
        lblLocalidad.Name = "lblLocalidad"
        lblLocalidad.Size = New Size(100, 19)
        lblLocalidad.TabIndex = 8
        lblLocalidad.Text = "Localidad"
        '
        ' txtLocalidad
        '
        txtLocalidad.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtLocalidad.Location = New Point(0, 0)
        txtLocalidad.Margin = New Padding(0, 0, 6, 0)
        txtLocalidad.MaxLength = 100
        txtLocalidad.Name = "txtLocalidad"
        txtLocalidad.Size = New Size(133, 27)
        txtLocalidad.TabIndex = 9
        '
        ' lblEmail
        '
        lblEmail.AutoSize = True
        lblEmail.Font = New Font("Segoe UI", 8.25F)
        lblEmail.ForeColor = Color.DimGray
        lblEmail.Location = New Point(0, 0)
        lblEmail.Margin = New Padding(6, 8, 0, 2)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New Size(100, 19)
        lblEmail.TabIndex = 10
        lblEmail.Text = "Correo"
        '
        ' txtEmail
        '
        txtEmail.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        txtEmail.Location = New Point(0, 0)
        txtEmail.Margin = New Padding(6, 0, 0, 0)
        txtEmail.MaxLength = 100
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(203, 27)
        txtEmail.TabIndex = 11
        '
        ' lblObservaciones
        '
        lblObservaciones.AutoSize = True
        lblObservaciones.Font = New Font("Segoe UI", 8.25F)
        lblObservaciones.ForeColor = Color.DimGray
        lblObservaciones.Location = New Point(0, 0)
        lblObservaciones.Margin = New Padding(0, 8, 0, 2)
        lblObservaciones.Name = "lblObservaciones"
        lblObservaciones.Size = New Size(100, 19)
        lblObservaciones.TabIndex = 12
        lblObservaciones.Text = "Observaciones"
        '
        ' txtObservaciones
        '
        txtObservaciones.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtObservaciones.Location = New Point(0, 0)
        txtObservaciones.Margin = New Padding(0, 0, 0, 0)
        txtObservaciones.MinimumSize = New Size(0, 64)
        txtObservaciones.Multiline = True
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.ScrollBars = ScrollBars.Vertical
        txtObservaciones.Size = New Size(348, 64)
        txtObservaciones.TabIndex = 13
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
        lblContexto.TabIndex = 14
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
        ' FrmClientes
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
        Name = "FrmClientes"
        Text = "Clientes"
        pnlLista.ResumeLayout(False)
        pnlLista.PerformLayout()
        pnlFicha.ResumeLayout(False)
        pnlRegistro.ResumeLayout(False)
        pnlRegistro.PerformLayout()
        tlpCampos.ResumeLayout(False)
        tlpCampos.PerformLayout()
        CType(picBuscar, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvClientes, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents dgvClientes As DataGridView
    Friend WithEvents pnlFicha As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents pnlRegistro As Panel
    Friend WithEvents lblRegistroTitulo As Label
    Friend WithEvents lblEstadoRegistro As Label
    Friend WithEvents tlpCampos As TableLayoutPanel
    Friend WithEvents lblRazonSocial As Label
    Friend WithEvents txtRazonSocial As TextBox
    Friend WithEvents lblDocumento As Label
    Friend WithEvents txtDocumento As TextBox
    Friend WithEvents lblTelefono As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents lblDomicilio As Label
    Friend WithEvents txtDomicilio As TextBox
    Friend WithEvents lblLocalidad As Label
    Friend WithEvents txtLocalidad As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblObservaciones As Label
    Friend WithEvents txtObservaciones As TextBox
    Friend WithEvents lblContexto As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnBaja As Button
    Friend WithEvents btnCancelar As Button
End Class

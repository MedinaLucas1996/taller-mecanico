<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCategorias
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
        lblDescripcion = New Label()
        txtDescripcion = New TextBox()
        btnGuardar = New Button()
        btnModificar = New Button()
        btnEliminar = New Button()
        btnLimpiar = New Button()
        txtFiltro = New TextBox()
        dgvCategorias = New DataGridView()
        pnlDatos.SuspendLayout()
        CType(dgvCategorias, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(40, 25)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(210, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Categorías"
        '
        ' lblSubtitulo
        '
        lblSubtitulo.AutoSize = True
        lblSubtitulo.Font = New Font("Segoe UI", 11F)
        lblSubtitulo.ForeColor = Color.DimGray
        lblSubtitulo.Location = New Point(43, 78)
        lblSubtitulo.Name = "lblSubtitulo"
        lblSubtitulo.Size = New Size(330, 25)
        lblSubtitulo.TabIndex = 1
        lblSubtitulo.Text = "Administración de las categorías de servicio"
        '
        ' pnlDatos
        '
        pnlDatos.BackColor = Color.White
        pnlDatos.Controls.Add(lblID)
        pnlDatos.Controls.Add(txtID)
        pnlDatos.Controls.Add(lblEstado)
        pnlDatos.Controls.Add(lblDescripcion)
        pnlDatos.Controls.Add(txtDescripcion)
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
        ' lblDescripcion
        '
        lblDescripcion.AutoSize = True
        lblDescripcion.Location = New Point(20, 63)
        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Size = New Size(109, 20)
        lblDescripcion.TabIndex = 3
        lblDescripcion.Text = "Descripción (*)"
        '
        ' txtDescripcion
        '
        txtDescripcion.Location = New Point(140, 60)
        txtDescripcion.MaxLength = 60
        txtDescripcion.Name = "txtDescripcion"
        txtDescripcion.Size = New Size(420, 27)
        txtDescripcion.TabIndex = 4
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
        btnGuardar.TabIndex = 5
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
        btnModificar.TabIndex = 6
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
        btnEliminar.TabIndex = 7
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
        btnLimpiar.TabIndex = 8
        btnLimpiar.Text = "Limpiar"
        btnLimpiar.UseVisualStyleBackColor = False
        '
        ' txtFiltro
        '
        txtFiltro.Font = New Font("Segoe UI", 11F)
        txtFiltro.Location = New Point(40, 360)
        txtFiltro.Name = "txtFiltro"
        txtFiltro.PlaceholderText = "Buscar por descripción..."
        txtFiltro.Size = New Size(420, 32)
        txtFiltro.TabIndex = 3
        '
        ' dgvCategorias
        '
        dgvCategorias.AllowUserToAddRows = False
        dgvCategorias.AllowUserToDeleteRows = False
        dgvCategorias.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCategorias.BackgroundColor = Color.White
        dgvCategorias.BorderStyle = BorderStyle.FixedSingle
        dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCategorias.Location = New Point(40, 410)
        dgvCategorias.MultiSelect = False
        dgvCategorias.Name = "dgvCategorias"
        dgvCategorias.ReadOnly = True
        dgvCategorias.RowHeadersVisible = False
        dgvCategorias.RowHeadersWidth = 51
        dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCategorias.Size = New Size(930, 250)
        dgvCategorias.TabIndex = 4
        '
        ' FrmCategorias
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(1010, 690)
        Controls.Add(dgvCategorias)
        Controls.Add(txtFiltro)
        Controls.Add(pnlDatos)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmCategorias"
        Text = "Categorías"
        pnlDatos.ResumeLayout(False)
        pnlDatos.PerformLayout()
        CType(dgvCategorias, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlDatos As Panel
    Friend WithEvents lblID As Label
    Friend WithEvents txtID As TextBox
    Friend WithEvents lblEstado As Label
    Friend WithEvents lblDescripcion As Label
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents txtFiltro As TextBox
    Friend WithEvents dgvCategorias As DataGridView
End Class

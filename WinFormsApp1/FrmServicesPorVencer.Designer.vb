<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmServicesPorVencer
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
        btnExportar = New Button()
        rdbVencidos = New RadioButton()
        rdbProximos = New RadioButton()
        dgvServicios = New DataGridView()
        lblVacio = New Label()
        lblAyuda = New Label()
        btnCopiar = New Button()
        CType(dgvServicios, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblTitulo.Location = New Point(30, 10)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(380, 54)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Services por vencer"
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
        ' btnExportar
        '
        btnExportar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnExportar.BackColor = Color.White
        btnExportar.Cursor = Cursors.Hand
        btnExportar.FlatAppearance.BorderColor = Color.Silver
        btnExportar.FlatStyle = FlatStyle.Flat
        btnExportar.Font = New Font("Segoe UI", 10F)
        btnExportar.Location = New Point(700, 34)
        btnExportar.Name = "btnExportar"
        btnExportar.Size = New Size(140, 40)
        btnExportar.TabIndex = 2
        btnExportar.Text = " Exportar"
        btnExportar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnExportar.UseVisualStyleBackColor = False
        '
        ' rdbVencidos
        '
        rdbVencidos.AutoSize = True
        rdbVencidos.Checked = True
        rdbVencidos.Cursor = Cursors.Hand
        rdbVencidos.Font = New Font("Segoe UI", 10F)
        rdbVencidos.Location = New Point(30, 110)
        rdbVencidos.Name = "rdbVencidos"
        rdbVencidos.Size = New Size(91, 27)
        rdbVencidos.TabIndex = 3
        rdbVencidos.TabStop = True
        rdbVencidos.Text = "Vencidos"
        rdbVencidos.UseVisualStyleBackColor = True
        '
        ' rdbProximos
        '
        rdbProximos.AutoSize = True
        rdbProximos.Cursor = Cursors.Hand
        rdbProximos.Font = New Font("Segoe UI", 10F)
        rdbProximos.Location = New Point(150, 110)
        rdbProximos.Name = "rdbProximos"
        rdbProximos.Size = New Size(270, 27)
        rdbProximos.TabIndex = 4
        rdbProximos.Text = "Vencen en los próximos 30 días"
        rdbProximos.UseVisualStyleBackColor = True
        '
        ' dgvServicios
        '
        dgvServicios.AllowUserToAddRows = False
        dgvServicios.AllowUserToDeleteRows = False
        dgvServicios.AllowUserToResizeRows = False
        dgvServicios.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvServicios.BackgroundColor = Color.White
        dgvServicios.BorderStyle = BorderStyle.FixedSingle
        dgvServicios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvServicios.Location = New Point(30, 146)
        dgvServicios.MultiSelect = False
        dgvServicios.Name = "dgvServicios"
        dgvServicios.ReadOnly = True
        dgvServicios.RowHeadersVisible = False
        dgvServicios.RowHeadersWidth = 51
        dgvServicios.RowTemplate.Height = 24
        dgvServicios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvServicios.Size = New Size(810, 410)
        dgvServicios.TabIndex = 5
        '
        ' lblVacio
        '
        lblVacio.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblVacio.BackColor = Color.White
        lblVacio.Font = New Font("Segoe UI", 12F)
        lblVacio.ForeColor = Color.DimGray
        lblVacio.Location = New Point(30, 146)
        lblVacio.Name = "lblVacio"
        lblVacio.Size = New Size(810, 410)
        lblVacio.TabIndex = 6
        lblVacio.Text = "-"
        lblVacio.TextAlign = ContentAlignment.MiddleCenter
        lblVacio.Visible = False
        '
        ' lblAyuda
        '
        lblAyuda.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyuda.Font = New Font("Segoe UI", 9F)
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(30, 568)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(560, 48)
        lblAyuda.TabIndex = 7
        lblAyuda.Text = "Elija un service y copie el mensaje para pegarlo en WhatsApp. Si el vehículo tiene otros services en la lista, van en el mismo mensaje."
        lblAyuda.TextAlign = ContentAlignment.MiddleLeft
        '
        ' btnCopiar
        '
        btnCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnCopiar.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnCopiar.Cursor = Cursors.Hand
        btnCopiar.FlatAppearance.BorderSize = 0
        btnCopiar.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnCopiar.FlatStyle = FlatStyle.Flat
        btnCopiar.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnCopiar.ForeColor = Color.White
        btnCopiar.Location = New Point(610, 572)
        btnCopiar.Name = "btnCopiar"
        btnCopiar.Size = New Size(230, 40)
        btnCopiar.TabIndex = 8
        btnCopiar.Text = "Copiar mensaje"
        btnCopiar.UseVisualStyleBackColor = False
        '
        ' FrmServicesPorVencer
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        ClientSize = New Size(870, 630)
        Controls.Add(btnCopiar)
        Controls.Add(lblAyuda)
        Controls.Add(lblVacio)
        Controls.Add(dgvServicios)
        Controls.Add(rdbProximos)
        Controls.Add(rdbVencidos)
        Controls.Add(btnExportar)
        Controls.Add(lblSubtitulo)
        Controls.Add(lblTitulo)
        Name = "FrmServicesPorVencer"
        Text = "Services por vencer"
        CType(dgvServicios, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents btnExportar As Button
    Friend WithEvents rdbVencidos As RadioButton
    Friend WithEvents rdbProximos As RadioButton
    Friend WithEvents dgvServicios As DataGridView
    Friend WithEvents lblVacio As Label
    Friend WithEvents lblAyuda As Label
    Friend WithEvents btnCopiar As Button
End Class

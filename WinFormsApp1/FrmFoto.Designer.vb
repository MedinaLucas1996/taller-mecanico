<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFoto
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
        picFoto = New PictureBox()
        pnlAcciones = New Panel()
        lblAyuda = New Label()
        btnReemplazar = New Button()
        btnQuitar = New Button()
        dlgFoto = New OpenFileDialog()
        CType(picFoto, ComponentModel.ISupportInitialize).BeginInit()
        pnlAcciones.SuspendLayout()
        SuspendLayout()
        '
        ' picFoto
        '
        picFoto.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        picFoto.Dock = DockStyle.Fill
        picFoto.Location = New Point(0, 0)
        picFoto.Name = "picFoto"
        picFoto.Size = New Size(960, 656)
        picFoto.SizeMode = PictureBoxSizeMode.Zoom
        picFoto.TabIndex = 0
        picFoto.TabStop = False
        '
        ' pnlAcciones
        '
        pnlAcciones.BackColor = Color.White
        pnlAcciones.Controls.Add(lblAyuda)
        pnlAcciones.Controls.Add(btnReemplazar)
        pnlAcciones.Controls.Add(btnQuitar)
        pnlAcciones.Dock = DockStyle.Bottom
        pnlAcciones.Location = New Point(0, 656)
        pnlAcciones.Name = "pnlAcciones"
        pnlAcciones.Size = New Size(960, 64)
        pnlAcciones.TabIndex = 1
        pnlAcciones.Visible = False
        '
        ' lblAyuda
        '
        lblAyuda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblAyuda.AutoEllipsis = True
        lblAyuda.ForeColor = Color.DimGray
        lblAyuda.Location = New Point(20, 12)
        lblAyuda.Name = "lblAyuda"
        lblAyuda.Size = New Size(500, 40)
        lblAyuda.TabIndex = 0
        lblAyuda.Text = "La orden está recién recepcionada: la foto todavía se puede reemplazar o quitar."
        lblAyuda.TextAlign = ContentAlignment.MiddleLeft
        '
        ' btnReemplazar
        '
        btnReemplazar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnReemplazar.BackColor = Color.White
        btnReemplazar.Cursor = Cursors.Hand
        btnReemplazar.FlatAppearance.BorderColor = Color.Silver
        btnReemplazar.FlatStyle = FlatStyle.Flat
        btnReemplazar.Font = New Font("Segoe UI", 10F)
        btnReemplazar.Location = New Point(540, 12)
        btnReemplazar.Name = "btnReemplazar"
        btnReemplazar.Size = New Size(210, 40)
        btnReemplazar.TabIndex = 1
        btnReemplazar.Text = " Reemplazar foto"
        btnReemplazar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnReemplazar.UseVisualStyleBackColor = False
        '
        ' btnQuitar
        '
        btnQuitar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnQuitar.BackColor = Color.White
        btnQuitar.Cursor = Cursors.Hand
        btnQuitar.FlatAppearance.BorderColor = Color.Silver
        btnQuitar.FlatStyle = FlatStyle.Flat
        btnQuitar.Font = New Font("Segoe UI", 10F)
        btnQuitar.ForeColor = Color.Firebrick
        btnQuitar.Location = New Point(760, 12)
        btnQuitar.Name = "btnQuitar"
        btnQuitar.Size = New Size(180, 40)
        btnQuitar.TabIndex = 2
        btnQuitar.Text = " Quitar foto"
        btnQuitar.TextImageRelation = TextImageRelation.ImageBeforeText
        btnQuitar.UseVisualStyleBackColor = False
        '
        ' dlgFoto
        '
        dlgFoto.Filter = "Imágenes (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
        dlgFoto.Title = "Seleccionar foto"
        '
        ' FrmFoto
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        ClientSize = New Size(960, 720)
        Controls.Add(picFoto)
        Controls.Add(pnlAcciones)
        KeyPreview = True
        MinimizeBox = False
        Name = "FrmFoto"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Foto de recepción"
        CType(picFoto, ComponentModel.ISupportInitialize).EndInit()
        pnlAcciones.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents picFoto As PictureBox
    Friend WithEvents pnlAcciones As Panel
    Friend WithEvents lblAyuda As Label
    Friend WithEvents btnReemplazar As Button
    Friend WithEvents btnQuitar As Button
    Friend WithEvents dlgFoto As OpenFileDialog
End Class

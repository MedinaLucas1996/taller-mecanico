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
        CType(picFoto, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        ' picFoto
        '
        picFoto.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        picFoto.Dock = DockStyle.Fill
        picFoto.Location = New Point(0, 0)
        picFoto.Name = "picFoto"
        picFoto.Size = New Size(960, 720)
        picFoto.SizeMode = PictureBoxSizeMode.Zoom
        picFoto.TabIndex = 0
        picFoto.TabStop = False
        '
        ' FrmFoto
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        ClientSize = New Size(960, 720)
        Controls.Add(picFoto)
        MinimizeBox = False
        Name = "FrmFoto"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Foto de recepción"
        CType(picFoto, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents picFoto As PictureBox
End Class

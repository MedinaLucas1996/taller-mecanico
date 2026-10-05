<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAnularOrden
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
        lblOrden = New Label()
        lblExplicacion = New Label()
        lblMotivo = New Label()
        txtMotivo = New TextBox()
        lblContador = New Label()
        btnAnular = New Button()
        btnCancelar = New Button()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblTitulo.Location = New Point(24, 16)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(160, 32)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Anular orden"
        '
        ' lblOrden
        '
        lblOrden.AutoEllipsis = True
        lblOrden.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblOrden.Location = New Point(26, 56)
        lblOrden.Name = "lblOrden"
        lblOrden.Size = New Size(548, 24)
        lblOrden.TabIndex = 1
        lblOrden.Text = "-"
        '
        ' lblExplicacion
        '
        lblExplicacion.ForeColor = Color.DimGray
        lblExplicacion.Location = New Point(26, 84)
        lblExplicacion.Name = "lblExplicacion"
        lblExplicacion.Size = New Size(548, 64)
        lblExplicacion.TabIndex = 2
        lblExplicacion.Text = "La orden queda cerrada como anulada y no se puede volver atrás. No se borra nada: la orden, sus líneas y su historial se conservan. El motivo queda registrado en el historial."
        '
        ' lblMotivo
        '
        lblMotivo.AutoSize = True
        lblMotivo.Location = New Point(26, 156)
        lblMotivo.Name = "lblMotivo"
        lblMotivo.Size = New Size(190, 20)
        lblMotivo.TabIndex = 3
        lblMotivo.Text = "Motivo de la anulación (*)"
        '
        ' txtMotivo
        '
        txtMotivo.Location = New Point(26, 182)
        txtMotivo.MaxLength = 255
        txtMotivo.Multiline = True
        txtMotivo.Name = "txtMotivo"
        txtMotivo.ScrollBars = ScrollBars.Vertical
        txtMotivo.Size = New Size(548, 100)
        txtMotivo.TabIndex = 4
        '
        ' lblContador
        '
        lblContador.Font = New Font("Segoe UI", 8.5F)
        lblContador.ForeColor = Color.DimGray
        lblContador.Location = New Point(374, 286)
        lblContador.Name = "lblContador"
        lblContador.Size = New Size(200, 20)
        lblContador.TabIndex = 5
        lblContador.Text = "0 de 255 caracteres"
        lblContador.TextAlign = ContentAlignment.TopRight
        '
        ' btnAnular
        '
        btnAnular.BackColor = Color.White
        btnAnular.Cursor = Cursors.Hand
        btnAnular.FlatAppearance.BorderColor = Color.Firebrick
        btnAnular.FlatStyle = FlatStyle.Flat
        btnAnular.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnAnular.ForeColor = Color.Firebrick
        btnAnular.Location = New Point(394, 320)
        btnAnular.Name = "btnAnular"
        btnAnular.Size = New Size(180, 42)
        btnAnular.TabIndex = 6
        btnAnular.Text = " Anular orden"
        btnAnular.TextImageRelation = TextImageRelation.ImageBeforeText
        btnAnular.UseVisualStyleBackColor = False
        '
        ' btnCancelar
        '
        btnCancelar.BackColor = Color.White
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.DialogResult = DialogResult.Cancel
        btnCancelar.FlatAppearance.BorderColor = Color.Silver
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 10F)
        btnCancelar.Location = New Point(254, 320)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(130, 42)
        btnCancelar.TabIndex = 7
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = False
        '
        ' FrmAnularOrden
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        CancelButton = btnCancelar
        ClientSize = New Size(600, 382)
        Controls.Add(btnCancelar)
        Controls.Add(btnAnular)
        Controls.Add(lblContador)
        Controls.Add(txtMotivo)
        Controls.Add(lblMotivo)
        Controls.Add(lblExplicacion)
        Controls.Add(lblOrden)
        Controls.Add(lblTitulo)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "FrmAnularOrden"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Anular orden de trabajo"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblOrden As Label
    Friend WithEvents lblExplicacion As Label
    Friend WithEvents lblMotivo As Label
    Friend WithEvents txtMotivo As TextBox
    Friend WithEvents lblContador As Label
    Friend WithEvents btnAnular As Button
    Friend WithEvents btnCancelar As Button
End Class

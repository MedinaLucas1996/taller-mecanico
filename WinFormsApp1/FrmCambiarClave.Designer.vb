<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCambiarClave
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
        lblExplicacion = New Label()
        lblActual = New Label()
        txtActual = New TextBox()
        lblNueva = New Label()
        txtNueva = New TextBox()
        lblRepetir = New Label()
        txtRepetir = New TextBox()
        btnGuardar = New Button()
        btnCancelar = New Button()
        SuspendLayout()
        '
        ' lblTitulo
        '
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblTitulo.Location = New Point(24, 16)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(220, 32)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Cambiar contraseña"
        '
        ' lblExplicacion
        '
        lblExplicacion.ForeColor = Color.DimGray
        lblExplicacion.Location = New Point(26, 56)
        lblExplicacion.Name = "lblExplicacion"
        lblExplicacion.Size = New Size(428, 44)
        lblExplicacion.TabIndex = 1
        lblExplicacion.Text = "Escriba su contraseña actual y la nueva. La nueva debe tener al menos 6 caracteres."
        '
        ' lblActual
        '
        lblActual.AutoSize = True
        lblActual.Location = New Point(26, 108)
        lblActual.Name = "lblActual"
        lblActual.Size = New Size(160, 20)
        lblActual.TabIndex = 2
        lblActual.Text = "Contraseña actual (*)"
        '
        ' txtActual
        '
        txtActual.Location = New Point(26, 132)
        txtActual.Name = "txtActual"
        txtActual.Size = New Size(428, 27)
        txtActual.TabIndex = 3
        txtActual.UseSystemPasswordChar = True
        '
        ' lblNueva
        '
        lblNueva.AutoSize = True
        lblNueva.Location = New Point(26, 170)
        lblNueva.Name = "lblNueva"
        lblNueva.Size = New Size(160, 20)
        lblNueva.TabIndex = 4
        lblNueva.Text = "Contraseña nueva (*)"
        '
        ' txtNueva
        '
        txtNueva.Location = New Point(26, 194)
        txtNueva.Name = "txtNueva"
        txtNueva.Size = New Size(428, 27)
        txtNueva.TabIndex = 5
        txtNueva.UseSystemPasswordChar = True
        '
        ' lblRepetir
        '
        lblRepetir.AutoSize = True
        lblRepetir.Location = New Point(26, 232)
        lblRepetir.Name = "lblRepetir"
        lblRepetir.Size = New Size(210, 20)
        lblRepetir.TabIndex = 6
        lblRepetir.Text = "Repetir contraseña nueva (*)"
        '
        ' txtRepetir
        '
        txtRepetir.Location = New Point(26, 256)
        txtRepetir.Name = "txtRepetir"
        txtRepetir.Size = New Size(428, 27)
        txtRepetir.TabIndex = 7
        txtRepetir.UseSystemPasswordChar = True
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
        btnGuardar.Location = New Point(304, 306)
        btnGuardar.Name = "btnGuardar"
        btnGuardar.Size = New Size(150, 42)
        btnGuardar.TabIndex = 8
        btnGuardar.Text = "Guardar"
        btnGuardar.UseVisualStyleBackColor = False
        '
        ' btnCancelar
        '
        btnCancelar.BackColor = Color.White
        btnCancelar.Cursor = Cursors.Hand
        btnCancelar.DialogResult = DialogResult.Cancel
        btnCancelar.FlatAppearance.BorderColor = Color.Silver
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.Font = New Font("Segoe UI", 10F)
        btnCancelar.Location = New Point(164, 306)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New Size(130, 42)
        btnCancelar.TabIndex = 9
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = False
        '
        ' FrmCambiarClave
        '
        AcceptButton = btnGuardar
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        CancelButton = btnCancelar
        ClientSize = New Size(480, 368)
        Controls.Add(btnCancelar)
        Controls.Add(btnGuardar)
        Controls.Add(txtRepetir)
        Controls.Add(lblRepetir)
        Controls.Add(txtNueva)
        Controls.Add(lblNueva)
        Controls.Add(txtActual)
        Controls.Add(lblActual)
        Controls.Add(lblExplicacion)
        Controls.Add(lblTitulo)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "FrmCambiarClave"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Cambiar contraseña"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblExplicacion As Label
    Friend WithEvents lblActual As Label
    Friend WithEvents txtActual As TextBox
    Friend WithEvents lblNueva As Label
    Friend WithEvents txtNueva As TextBox
    Friend WithEvents lblRepetir As Label
    Friend WithEvents txtRepetir As TextBox
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnCancelar As Button
End Class

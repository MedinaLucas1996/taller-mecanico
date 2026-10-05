Public Class FrmAnularOrden

    'pide el motivo de la anulacion de una orden y confirma la accion
    'no toca la base: la gestion de la orden hace la anulacion con el motivo que devuelve

    'texto que identifica la orden, lo carga la gestion de la orden antes de abrir la ventana
    Public Orden As String = ""

    'motivo escrito, sin espacios sobrantes; vale solo cuando la ventana se cierra con "Anular orden"
    Public Motivo As String = ""

    Private Sub FrmAnularOrden_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblOrden.Text = Orden
        'icono del boton, si falta el boton queda solo con el texto
        btnAnular.Image = LeerIcono("anular-rojo.png")
    End Sub

    Private Sub txtMotivo_TextChanged(sender As Object, e As EventArgs) Handles txtMotivo.TextChanged
        'muestro cuanto queda del limite, que es el largo de la observacion del historial
        lblContador.Text = txtMotivo.Text.Length & " de 255 caracteres"
    End Sub

    Private Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        'el motivo es obligatorio
        If txtMotivo.Text.Trim = "" Then
            MessageBox.Show("Falta el motivo de la anulación")
            txtMotivo.Focus()
            Exit Sub
        End If

        'devuelvo el motivo y cierro: la anulacion la hace la gestion de la orden
        Motivo = txtMotivo.Text.Trim
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class

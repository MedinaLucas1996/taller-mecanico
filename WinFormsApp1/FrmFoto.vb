Public Class FrmFoto

    'muestra en grande una foto de recepcion
    'el tablero de ordenes carga la imagen en picFoto y el angulo en el titulo antes de abrirlo

    Private Sub FrmFoto_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        'Escape cierra la ventana
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

End Class

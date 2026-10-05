Imports MySqlConnector

Public Class FrmFoto

    'muestra en grande una foto de recepcion
    'el tablero de ordenes carga la imagen en picFoto y el titulo, y estos datos, antes de abrirlo

    'orden y angulo de la foto que se muestra, tal como los guarda la base
    Public IdOrdenTrabajo As Integer = 0
    Public Angulo As String = ""

    'el tablero lo pone en True cuando la orden esta RECEPCIONADA: ahi la foto se puede reemplazar o quitar
    Public PermiteCambios As Boolean = False

    'queda en True cuando el tablero tiene que volver a cargar: la foto cambio o la orden ya no estaba como se veia
    Public HuboCambios As Boolean = False

    Private Sub FrmFoto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'la barra con los botones solo aparece cuando se permite cambiar la foto
        pnlAcciones.Visible = PermiteCambios

        If PermiteCambios Then
            'iconos de los botones, si alguno falta ese boton queda sin icono
            btnReemplazar.Image = LeerIcono("foto-agregar-oscuro.png")
            btnQuitar.Image = LeerIcono("quitar-oscuro.png")
        End If
    End Sub

    Private Sub FrmFoto_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        'Escape cierra la ventana
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub btnReemplazar_Click(sender As Object, e As EventArgs) Handles btnReemplazar.Click
        'reemplazo la foto de este angulo por otra elegida del disco
        If dlgFoto.ShowDialog() <> DialogResult.OK Then Exit Sub

        'mismo proceso que en la recepcion: reducida, enderezada y en JPEG
        Dim bytes() As Byte = LeerFotoComoJpeg(dlgFoto.FileName)
        If bytes Is Nothing Then
            MessageBox.Show("El archivo elegido no es una imagen válida. Elija una foto JPG o PNG.")
            Exit Sub
        End If

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion del estado y el guardado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'bloqueo solo la fila de la orden y leo su estado actual
                    Dim idEstado As Integer = 0
                    Dim consulta As String =
                        "SELECT id_estado_ot FROM orden_trabajo WHERE id_orden_trabajo = @id_orden_trabajo FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then idEstado = Convert.ToInt32(resultado)
                    End Using

                    'el codigo del estado se lee aparte, sin bloquear el catalogo de estados
                    Dim codigoEstado As String = ""
                    consulta = "SELECT codigo FROM estado_ot WHERE id_estado_ot = @id_estado_ot;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then codigoEstado = resultado.ToString()
                    End Using

                    'una foto cargada solo se reemplaza con la orden recien recepcionada
                    If codigoEstado <> "RECEPCIONADA" Then
                        aviso = "La orden ya no está recepcionada: sus fotos ya no se pueden reemplazar."
                    End If

                    If aviso = "" Then
                        'guardo la foto nueva con quien la cargo y el momento de esta carga
                        consulta =
                            "UPDATE ot_foto SET imagen = @imagen, id_usuario = @id_usuario, fecha_hora = NOW() " &
                            "WHERE id_orden_trabajo = @id_orden_trabajo AND angulo = @angulo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@imagen", bytes)
                            cmd.Parameters.AddWithValue("@id_usuario", Sesion.IdUsuario)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.Parameters.AddWithValue("@angulo", Angulo)
                            'si no se actualizo ninguna fila, otro puesto quito la foto
                            If cmd.ExecuteNonQuery() = 0 Then
                                aviso = "La foto ya no existe: otro puesto la quitó. No se guardó nada."
                            End If
                        End Using
                    End If
                Catch ex As Exception
                    'algo fallo antes de confirmar: deshago todo y dejo que el error llegue al mensaje de abajo
                    transaccion.Rollback()
                    Throw
                End Try

                'confirmo fuera del Try de arriba: si el Commit falla no se intenta deshacer una transaccion ya cerrada
                If aviso = "" Then
                    transaccion.Commit()
                Else
                    transaccion.Rollback()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("No se pudo reemplazar la foto: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then MessageBox.Show(aviso)

        'cierro la ventana: el tablero vuelve a cargar la orden y sus fotos
        HuboCambios = True
        Me.Close()
    End Sub

    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        'quito la foto de este angulo, la orden queda sin esa foto

        'pido confirmacion antes de quitar
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Quitar esta foto de la orden? No se puede deshacer.",
            "Quitar foto",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion del estado y el borrado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'bloqueo solo la fila de la orden y leo su estado actual
                    Dim idEstado As Integer = 0
                    Dim consulta As String =
                        "SELECT id_estado_ot FROM orden_trabajo WHERE id_orden_trabajo = @id_orden_trabajo FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then idEstado = Convert.ToInt32(resultado)
                    End Using

                    'el codigo del estado se lee aparte, sin bloquear el catalogo de estados
                    Dim codigoEstado As String = ""
                    consulta = "SELECT codigo FROM estado_ot WHERE id_estado_ot = @id_estado_ot;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then codigoEstado = resultado.ToString()
                    End Using

                    'una foto cargada solo se quita con la orden recien recepcionada
                    If codigoEstado <> "RECEPCIONADA" Then
                        aviso = "La orden ya no está recepcionada: sus fotos ya no se pueden quitar."
                    End If

                    If aviso = "" Then
                        'borro solo la foto de este angulo
                        consulta =
                            "DELETE FROM ot_foto WHERE id_orden_trabajo = @id_orden_trabajo AND angulo = @angulo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.Parameters.AddWithValue("@angulo", Angulo)
                            'si no se borro ninguna fila, otro puesto ya la habia quitado
                            If cmd.ExecuteNonQuery() = 0 Then
                                aviso = "La foto ya no existe: otro puesto la quitó antes."
                            End If
                        End Using
                    End If
                Catch ex As Exception
                    'algo fallo antes de confirmar: deshago todo y dejo que el error llegue al mensaje de abajo
                    transaccion.Rollback()
                    Throw
                End Try

                'confirmo fuera del Try de arriba: si el Commit falla no se intenta deshacer una transaccion ya cerrada
                If aviso = "" Then
                    transaccion.Commit()
                Else
                    transaccion.Rollback()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("No se pudo quitar la foto: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then MessageBox.Show(aviso)

        'cierro la ventana: el tablero vuelve a cargar la orden y sus fotos
        HuboCambios = True
        Me.Close()
    End Sub

End Class

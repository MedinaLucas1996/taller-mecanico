Imports MySqlConnector

Public Class FrmCambiarClave

    'el usuario que inicio sesion cambia su propia contraseña
    'verifica la actual contra el hash guardado y guarda la nueva con salt y hash nuevos, igual que el alta de usuarios

    Private Sub FrmCambiarClave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'el largo minimo sale de la constante compartida, asi el texto nunca queda desactualizado
        lblExplicacion.Text = "Escriba su contraseña actual y la nueva. La nueva debe tener al menos " &
                              LARGO_MINIMO_CLAVE & " caracteres."
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'valido los tres campos antes de ir a la base
        If txtActual.Text = "" Then
            MessageBox.Show("Falta la contraseña actual")
            txtActual.Focus()
            Exit Sub
        End If

        'la clave nueva tiene que tener el largo minimo y no ser solo espacios (la misma regla que el alta de usuarios)
        If txtNueva.Text.Trim.Length < LARGO_MINIMO_CLAVE Then
            MessageBox.Show("La contraseña debe tener al menos " & LARGO_MINIMO_CLAVE & " caracteres.")
            txtNueva.Focus()
            Exit Sub
        End If

        If txtNueva.Text <> txtRepetir.Text Then
            MessageBox.Show("Las contraseñas no coinciden.")
            txtRepetir.Clear()
            txtRepetir.Focus()
            Exit Sub
        End If

        'calcular los hash lleva un momento: muestro el reloj de espera mientras tanto
        Cursor.Current = Cursors.WaitCursor

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'leo el hash y el salt guardados del usuario de la sesion para verificar la clave actual
                Dim hashGuardado As String = ""
                Dim saltGuardado As String = ""
                Dim encontrado As Boolean = False
                Dim consulta As String =
                    "SELECT hash_contrasena, salt FROM usuario WHERE id_usuario = @id AND activo = 1;"
                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@id", Sesion.IdUsuario)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If lector.Read() Then
                            hashGuardado = lector("hash_contrasena").ToString()
                            saltGuardado = lector("salt").ToString()
                            encontrado = True
                        End If
                    End Using
                End Using

                'el usuario pudo ser dado de baja desde otro puesto mientras tenia la sesion abierta
                If Not encontrado Then
                    MessageBox.Show("No se encontró el usuario de la sesión. Cierre la sesión y vuelva a ingresar.")
                    Exit Sub
                End If

                If Not VerificarClave(txtActual.Text, hashGuardado, saltGuardado) Then
                    MessageBox.Show("La contraseña actual no es correcta.")
                    txtActual.Clear()
                    txtActual.Focus()
                    Exit Sub
                End If

                'genero un salt nuevo y calculo el hash de la clave nueva, nunca guardo la clave en texto plano
                Dim salt As String = GenerarSalt()
                Dim hash As String = HashearClave(txtNueva.Text, salt)

                'cambio el hash y el salt, las mismas columnas que toca el alta de usuarios al cambiar una clave
                consulta =
                    "UPDATE usuario SET hash_contrasena=@hash_contrasena, salt=@salt " &
                    "WHERE id_usuario=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@hash_contrasena", hash)
                    cmd.Parameters.AddWithValue("@salt", salt)
                    cmd.Parameters.AddWithValue("@id", Sesion.IdUsuario)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            'cualquier falla (base apagada, datos ilegibles) se avisa con un mensaje claro y queda registrada
            MostrarError("No se pudo cambiar la contraseña.", ex)
            Exit Sub
        Finally
            'pase lo que pase, el cursor vuelve a la normalidad
            Cursor.Current = Cursors.Default
        End Try

        MessageBox.Show("Contraseña cambiada.")
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class

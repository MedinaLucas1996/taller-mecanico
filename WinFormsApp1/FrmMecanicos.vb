Imports MySqlConnector

Public Class FrmMecanicos

    'indica si el mecanico seleccionado esta activo, decide si el boton da de baja o reactiva
    Private mecanicoActivo As Boolean = True

    Sub CargarMecanicos(Optional filtro As String = "")
        'creo subrutina para cargar grilla
        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql, traigo todos los mecanicos: activos y dados de baja
                Dim consulta As String =
                    "SELECT id_mecanico, nombre_completo, especialidad, telefono, " &
                    "CASE WHEN activo = 1 THEN 'Sí' ELSE 'No' END AS esta_activo " &
                    "FROM mecanico "

                'aplico filtro por nombre o especialidad
                If filtro <> "" Then
                    consulta = consulta &
                        "WHERE nombre_completo LIKE @filtro OR especialidad LIKE @filtro "
                End If

                'primero los activos, despues los dados de baja
                consulta = consulta & "ORDER BY activo DESC, nombre_completo;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvMecanicos.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvMecanicos.Columns("id_mecanico").HeaderText = "ID"
                    'la columna ID se ajusta al contenido, no ocupa lo mismo que las demas
                    dgvMecanicos.Columns("id_mecanico").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvMecanicos.Columns("nombre_completo").HeaderText = "Nombre completo"
                    dgvMecanicos.Columns("especialidad").HeaderText = "Especialidad"
                    dgvMecanicos.Columns("telefono").HeaderText = "Teléfono"
                    dgvMecanicos.Columns("esta_activo").HeaderText = "Activo"
                    dgvMecanicos.Columns("esta_activo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                End Using
            End Using
        Catch ex As Exception
            'muestro mensaje de error
            MessageBox.Show("Error al cargar los mecánicos: " & ex.Message)
        End Try
    End Sub

    Sub LimpiarFormu()
        'limpio los campos del formulario
        txtID.Clear()
        txtNombreCompleto.Clear()
        txtEspecialidad.Clear()
        txtTelefono.Clear()
        'sin mecanico seleccionado el boton vuelve a ser el de la baja
        mecanicoActivo = True
        lblEstado.Text = ""
        btnEliminar.Text = "Dar de baja"
        dgvMecanicos.ClearSelection()
        txtNombreCompleto.Focus()
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If txtNombreCompleto.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre completo")
            txtNombreCompleto.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmMecanicos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'solo el administrador puede gestionar mecanicos (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede gestionar mecánicos.")
            pnlDatos.Enabled = False
            txtFiltro.Enabled = False
            dgvMecanicos.Enabled = False
            Exit Sub
        End If

        'cargo la grilla de mecanicos al abrir el formulario
        CargarMecanicos()
        LimpiarFormu()
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito
        CargarMecanicos(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un mecanico nuevo

        'si hay un ID cargado, el usuario quiere modificar, no guardar
        If txtID.Text.Trim <> "" Then
            MessageBox.Show("Hay un mecánico seleccionado. Use MODIFICAR o presione LIMPIAR para cargar uno nuevo.")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        'la especialidad y el telefono vacios se guardan como NULL
        Dim especialidad As Object = DBNull.Value
        If txtEspecialidad.Text.Trim <> "" Then especialidad = txtEspecialidad.Text.Trim
        Dim telefono As Object = DBNull.Value
        If txtTelefono.Text.Trim <> "" Then telefono = txtTelefono.Text.Trim

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'no puede haber dos mecanicos activos con el mismo nombre, sin mirar mayusculas ni espacios
                Dim consulta As String =
                    "SELECT COUNT(*) FROM mecanico " &
                    "WHERE activo = 1 AND LOWER(TRIM(nombre_completo)) = LOWER(@nombre_completo);"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                    If CInt(cmd.ExecuteScalar()) > 0 Then
                        MessageBox.Show("Ya existe un mecánico activo con ese nombre.")
                        txtNombreCompleto.Focus()
                        Exit Sub
                    End If
                End Using

                'armo mi consulta sql
                consulta =
                    "INSERT INTO mecanico (nombre_completo, especialidad, telefono) " &
                    "VALUES (@nombre_completo, @especialidad, @telefono);"

                Using cmd As New MySqlCommand(consulta, cn)
                    'cargo valores en los parametros
                    cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                    cmd.Parameters.AddWithValue("@especialidad", especialidad)
                    cmd.Parameters.AddWithValue("@telefono", telefono)

                    'ejecuto la consulta y obtengo los registros afectados
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros agregados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarMecanicos(txtFiltro.Text.Trim)

        Catch ex As Exception
            MessageBox.Show("Error al guardar " & ex.Message)
        End Try
    End Sub

    Private Sub dgvMecanicos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMecanicos.CellClick
        'traigo los datos de la fila seleccionada a los textbox
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvMecanicos.Rows(e.RowIndex)
        txtID.Text = fila.Cells("id_mecanico").Value.ToString()
        txtNombreCompleto.Text = fila.Cells("nombre_completo").Value.ToString()
        txtEspecialidad.Text = fila.Cells("especialidad").Value.ToString()
        txtTelefono.Text = fila.Cells("telefono").Value.ToString()

        'segun el estado del mecanico, el mismo boton da de baja o reactiva
        mecanicoActivo = (fila.Cells("esta_activo").Value.ToString() = "Sí")
        If mecanicoActivo Then
            lblEstado.Text = "Activo"
            btnEliminar.Text = "Dar de baja"
        Else
            lblEstado.Text = "Dado de baja"
            btnEliminar.Text = "Reactivar"
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        'valido que haya un mecanico seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un mecánico para modificar")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        'la especialidad y el telefono vacios se guardan como NULL
        Dim especialidad As Object = DBNull.Value
        If txtEspecialidad.Text.Trim <> "" Then especialidad = txtEspecialidad.Text.Trim
        Dim telefono As Object = DBNull.Value
        If txtTelefono.Text.Trim <> "" Then telefono = txtTelefono.Text.Trim

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'no puede quedar con el nombre de otro mecanico activo, sin mirar mayusculas ni espacios
                Dim consulta As String =
                    "SELECT COUNT(*) FROM mecanico " &
                    "WHERE activo = 1 AND LOWER(TRIM(nombre_completo)) = LOWER(@nombre_completo) " &
                    "AND id_mecanico <> @id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))
                    If CInt(cmd.ExecuteScalar()) > 0 Then
                        MessageBox.Show("Ya existe otro mecánico activo con ese nombre.")
                        txtNombreCompleto.Focus()
                        Exit Sub
                    End If
                End Using

                'uso update para modificar y where para indicar que registro
                consulta =
                    "UPDATE mecanico SET nombre_completo=@nombre_completo, especialidad=@especialidad, " &
                    "telefono=@telefono " &
                    "WHERE id_mecanico=@id;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'uso parametros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                    cmd.Parameters.AddWithValue("@especialidad", especialidad)
                    cmd.Parameters.AddWithValue("@telefono", telefono)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))

                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros actualizados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarMecanicos(txtFiltro.Text.Trim)

        Catch ex As Exception
            MessageBox.Show("Error al modificar " & ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'doy de baja al mecanico seleccionado, o lo reactivo si ya estaba dado de baja

        'valido que haya un mecanico seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un mecánico para dar de baja o reactivar")
            Exit Sub
        End If

        Dim idMecanico As Integer = CInt(txtID.Text)

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If mecanicoActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja al mecánico " & txtNombreCompleto.Text & "?",
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar al mecánico " & txtNombreCompleto.Text & "?",
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""

        'baja logica: no borro el registro porque puede tener ordenes de trabajo
        'solo lo marco como inactivo, y con el mismo boton se lo puede volver a activar
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el mecanico y lo bloqueo hasta terminar
                    Dim nombreGuardado As String = ""
                    Dim existe As Boolean = False
                    Dim consulta As String =
                        "SELECT nombre_completo FROM mecanico WHERE id_mecanico = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idMecanico)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then
                            existe = True
                            nombreGuardado = resultado.ToString()
                        End If
                    End Using

                    If Not existe Then aviso = "El mecánico seleccionado ya no existe."

                    If aviso = "" AndAlso mecanicoActivo Then
                        'no se da de baja a un mecanico con ordenes de trabajo sin cerrar
                        consulta =
                            "SELECT COUNT(*) FROM orden_trabajo AS ot " &
                            "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                            "WHERE ot.id_mecanico = @id AND e.es_estado_final = 0;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idMecanico)
                            Dim abiertas As Integer = CInt(cmd.ExecuteScalar())
                            If abiertas > 0 Then
                                aviso = "No se puede dar de baja: el mecánico tiene " & abiertas &
                                        " orden(es) de trabajo sin cerrar. Asígnelas a otro mecánico o ciérrelas primero."
                            End If
                        End Using
                    End If

                    If aviso = "" AndAlso Not mecanicoActivo Then
                        'al reactivar no puede quedar con el nombre de otro mecanico activo
                        consulta =
                            "SELECT COUNT(*) FROM mecanico " &
                            "WHERE activo = 1 AND LOWER(TRIM(nombre_completo)) = LOWER(TRIM(@nombre_completo)) " &
                            "AND id_mecanico <> @id;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@nombre_completo", nombreGuardado)
                            cmd.Parameters.AddWithValue("@id", idMecanico)
                            If CInt(cmd.ExecuteScalar()) > 0 Then
                                aviso = "No se puede reactivar: ya existe otro mecánico activo con ese nombre."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE mecanico SET activo = @activo WHERE id_mecanico = @id;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@activo", Not mecanicoActivo)
                            cmd.Parameters.AddWithValue("@id", idMecanico)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    'confirmo solo si se pudo guardar
                    If aviso = "" Then
                        transaccion.Commit()
                    Else
                        transaccion.Rollback()
                    End If
                Catch ex As Exception
                    'algo fallo: deshago todo y dejo que el error llegue al mensaje de abajo
                    transaccion.Rollback()
                    Throw
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cambiar el estado del mecánico: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
            Exit Sub
        End If

        If mecanicoActivo Then
            MessageBox.Show("Mecánico dado de baja.")
        Else
            MessageBox.Show("Mecánico reactivado.")
        End If

        LimpiarFormu()
        CargarMecanicos(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'limpio el formulario para cargar un mecanico nuevo
        LimpiarFormu()
    End Sub

End Class

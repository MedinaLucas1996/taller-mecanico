Imports MySqlConnector

Public Class FrmUsuarios

    Sub CargarUsuarios(Optional filtro As String = "")
        'creo subrutina para cargar grilla
        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql, solo traigo usuarios activos
                'nunca traigo el hash ni el salt de la clave
                Dim consulta As String =
                    "SELECT u.id_usuario, u.nombre_usuario, u.nombre_completo, u.rol, " &
                    "m.nombre_completo AS mecanico, u.id_mecanico " &
                    "FROM usuario AS u " &
                    "LEFT JOIN mecanico AS m ON m.id_mecanico = u.id_mecanico " &
                    "WHERE u.activo = 1 "

                'aplico filtro por usuario o nombre completo
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (u.nombre_usuario LIKE @filtro OR u.nombre_completo LIKE @filtro) "
                End If

                consulta = consulta & "ORDER BY u.nombre_usuario;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvUsuarios.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvUsuarios.Columns("id_usuario").HeaderText = "ID"
                    'la columna ID se ajusta al contenido, no ocupa lo mismo que las demas
                    dgvUsuarios.Columns("id_usuario").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvUsuarios.Columns("nombre_usuario").HeaderText = "Usuario"
                    dgvUsuarios.Columns("nombre_completo").HeaderText = "Nombre completo"
                    dgvUsuarios.Columns("rol").HeaderText = "Rol"
                    dgvUsuarios.Columns("mecanico").HeaderText = "Mecánico"
                    'la clave del mecanico se usa al seleccionar la fila, no se muestra
                    dgvUsuarios.Columns("id_mecanico").Visible = False
                End Using
            End Using
        Catch ex As Exception
            'muestro mensaje de error
            MessageBox.Show("Error al cargar los usuarios: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboMecanicos()
        'cargo los mecanicos activos en el combo
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_mecanico, nombre_completo FROM mecanico WHERE activo = 1 ORDER BY nombre_completo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'agrego una primera opcion con clave 0 que funciona como texto de ayuda
                    Dim filaAyuda As DataRow = tabla.NewRow()
                    filaAyuda("id_mecanico") = 0
                    filaAyuda("nombre_completo") = "Seleccione un mecánico"
                    tabla.Rows.InsertAt(filaAyuda, 0)

                    'el usuario ve el nombre...
                    cboMecanico.DisplayMember = "nombre_completo"
                    '...pero el programa guarda la clave numerica
                    cboMecanico.ValueMember = "id_mecanico"
                    cboMecanico.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los mecánicos: " & ex.Message)
        End Try
    End Sub

    Sub LimpiarFormu()
        'limpio los campos del formulario
        txtID.Clear()
        txtUsuario.Clear()
        txtNombreCompleto.Clear()
        txtPassword.Clear()
        'vuelvo a la opcion "Seleccione un rol", eso tambien deshabilita el combo de mecanicos
        cboRol.SelectedIndex = 0
        If cboMecanico.Items.Count > 0 Then cboMecanico.SelectedIndex = 0
        dgvUsuarios.ClearSelection()
        txtUsuario.Focus()
    End Sub

    Function ValidarCampos(passwordObligatoria As Boolean) As Boolean
        'valido los campos obligatorios
        If txtUsuario.Text.Trim = "" Then
            MessageBox.Show("Falta el usuario")
            txtUsuario.Focus()
            Return False
        End If

        If txtNombreCompleto.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre completo")
            txtNombreCompleto.Focus()
            Return False
        End If

        'la clave solo es obligatoria al crear, al modificar puede quedar vacia
        If passwordObligatoria AndAlso txtPassword.Text = "" Then
            MessageBox.Show("Falta la contraseña")
            txtPassword.Focus()
            Return False
        End If

        'la opcion 0 es el texto de ayuda, no un rol real
        If cboRol.SelectedIndex <= 0 Then
            MessageBox.Show("Seleccione un rol")
            cboRol.Focus()
            Return False
        End If

        'solo el rol MECANICO necesita un mecanico asociado
        If cboRol.Text = "MECANICO" Then
            'la clave 0 corresponde a la opcion de ayuda, no a un mecanico real
            If Not TypeOf cboMecanico.SelectedValue Is Integer OrElse CInt(cboMecanico.SelectedValue) = 0 Then
                MessageBox.Show("Seleccione un mecánico")
                cboMecanico.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub FrmUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'solo el administrador puede gestionar usuarios (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede gestionar usuarios.")
            pnlDatos.Enabled = False
            txtFiltro.Enabled = False
            dgvUsuarios.Enabled = False
            Exit Sub
        End If

        'cargo combos y grilla al abrir el formulario
        CargarComboMecanicos()
        CargarUsuarios()
        LimpiarFormu()
    End Sub

    Private Sub cboRol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRol.SelectedIndexChanged
        'el mecanico solo se elige cuando el rol es MECANICO
        If cboRol.Text = "MECANICO" Then
            cboMecanico.Enabled = True
        Else
            cboMecanico.Enabled = False
            'vuelvo a la opcion "Seleccione un mecánico"
            If cboMecanico.Items.Count > 0 Then cboMecanico.SelectedIndex = 0
        End If
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito
        CargarUsuarios(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un usuario nuevo

        'si hay un ID cargado, el usuario quiere modificar, no guardar
        If txtID.Text.Trim <> "" Then
            MessageBox.Show("Hay un usuario seleccionado. Use MODIFICAR o presione LIMPIAR para cargar uno nuevo.")
            Exit Sub
        End If

        If Not ValidarCampos(True) Then Exit Sub

        'el mecanico solo se guarda para el rol MECANICO, en los demas va NULL
        Dim idMecanico As Object = DBNull.Value
        If cboRol.Text = "MECANICO" Then idMecanico = CInt(cboMecanico.SelectedValue)

        'genero el salt y calculo el hash, nunca guardo la clave en texto plano
        Dim salt As String = GenerarSalt()
        Dim hash As String = HashearClave(txtPassword.Text, salt)

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql
                Dim consulta As String =
                    "INSERT INTO usuario (nombre_usuario, hash_contrasena, salt, nombre_completo, rol, id_mecanico) " &
                    "VALUES (@nombre_usuario, @hash_contrasena, @salt, @nombre_completo, @rol, @id_mecanico);"

                Using cmd As New MySqlCommand(consulta, cn)
                    'cargo valores en los parametros
                    cmd.Parameters.AddWithValue("@nombre_usuario", txtUsuario.Text.Trim)
                    cmd.Parameters.AddWithValue("@hash_contrasena", hash)
                    cmd.Parameters.AddWithValue("@salt", salt)
                    cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                    cmd.Parameters.AddWithValue("@rol", cboRol.Text)
                    cmd.Parameters.AddWithValue("@id_mecanico", idMecanico)

                    'ejecuto la consulta y obtengo los registros afectados
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros agregados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarUsuarios()

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: el usuario ya existe (restriccion unica)
            MessageBox.Show("Ya existe un usuario con ese nombre de usuario. Puede corresponder a un usuario dado de baja.")
            txtUsuario.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al guardar " & ex.Message)
        End Try
    End Sub

    Private Sub dgvUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsuarios.CellClick
        'traigo los datos de la fila seleccionada al formulario
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvUsuarios.Rows(e.RowIndex)
        txtID.Text = fila.Cells("id_usuario").Value.ToString()
        txtUsuario.Text = fila.Cells("nombre_usuario").Value.ToString()
        txtNombreCompleto.Text = fila.Cells("nombre_completo").Value.ToString()
        'la clave nunca se carga en el formulario
        txtPassword.Clear()

        'primero elijo el rol: eso habilita o deshabilita el combo de mecanicos
        cboRol.SelectedItem = fila.Cells("rol").Value.ToString()

        'el mecanico es NULL cuando el rol no es MECANICO
        If IsDBNull(fila.Cells("id_mecanico").Value) Then
            If cboMecanico.Items.Count > 0 Then cboMecanico.SelectedIndex = 0
        Else
            cboMecanico.SelectedValue = CInt(fila.Cells("id_mecanico").Value)
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        'valido que haya un usuario seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un usuario para modificar")
            Exit Sub
        End If

        'al modificar la clave puede quedar vacia
        If Not ValidarCampos(False) Then Exit Sub

        'el administrador logueado no puede quitarse su propio rol de administrador
        If CInt(txtID.Text) = Sesion.IdUsuario AndAlso cboRol.Text <> "ADMINISTRADOR" Then
            MessageBox.Show("No puede cambiar su propio rol.")
            cboRol.Focus()
            Exit Sub
        End If

        'el mecanico solo se guarda para el rol MECANICO, en los demas va NULL
        Dim idMecanico As Object = DBNull.Value
        If cboRol.Text = "MECANICO" Then idMecanico = CInt(cboMecanico.SelectedValue)

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'uso update para modificar y where para indicar que registro
                Dim consulta As String
                If txtPassword.Text = "" Then
                    'clave vacia: dejo el hash y el salt como estan
                    consulta =
                        "UPDATE usuario SET nombre_usuario=@nombre_usuario, nombre_completo=@nombre_completo, " &
                        "rol=@rol, id_mecanico=@id_mecanico " &
                        "WHERE id_usuario=@id;"
                Else
                    'clave nueva: cambio tambien el hash y el salt
                    consulta =
                        "UPDATE usuario SET nombre_usuario=@nombre_usuario, nombre_completo=@nombre_completo, " &
                        "rol=@rol, id_mecanico=@id_mecanico, hash_contrasena=@hash_contrasena, salt=@salt " &
                        "WHERE id_usuario=@id;"
                End If

                Using cmd As New MySqlCommand(consulta, cn)
                    'uso parametros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@nombre_usuario", txtUsuario.Text.Trim)
                    cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                    cmd.Parameters.AddWithValue("@rol", cboRol.Text)
                    cmd.Parameters.AddWithValue("@id_mecanico", idMecanico)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))

                    If txtPassword.Text <> "" Then
                        'genero un salt nuevo y calculo el hash de la clave nueva
                        Dim salt As String = GenerarSalt()
                        cmd.Parameters.AddWithValue("@hash_contrasena", HashearClave(txtPassword.Text, salt))
                        cmd.Parameters.AddWithValue("@salt", salt)
                    End If

                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros actualizados: " & Resultado)
                End Using
            End Using

            'si modifico mi propio usuario, actualizo los datos de la sesion
            If CInt(txtID.Text) = Sesion.IdUsuario Then
                Sesion.NombreUsuario = txtUsuario.Text.Trim
                Sesion.NombreCompleto = txtNombreCompleto.Text.Trim
            End If

            LimpiarFormu()
            CargarUsuarios(txtFiltro.Text.Trim)

        Catch ex As MySqlException When ex.Number = 1062
            MessageBox.Show("Ya existe un usuario con ese nombre de usuario. Puede corresponder a un usuario dado de baja.")
            txtUsuario.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al modificar " & ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'valido que haya un usuario seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un usuario para dar de baja")
            Exit Sub
        End If

        'el usuario logueado no puede darse de baja a si mismo
        If CInt(txtID.Text) = Sesion.IdUsuario Then
            MessageBox.Show("No puede dar de baja su propio usuario.")
            Exit Sub
        End If

        'pido confirmacion antes de dar de baja
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Dar de baja al usuario " & txtUsuario.Text & "?",
            "Dar de baja",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        'baja logica: el usuario puede tener ordenes de trabajo asociadas
        'solo lo marco como inactivo y ya no puede iniciar sesion
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "UPDATE usuario SET activo = 0 WHERE id_usuario=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Usuarios dados de baja: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarUsuarios(txtFiltro.Text.Trim)

        Catch ex As Exception
            MessageBox.Show("Error al dar de baja " & ex.Message)
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'limpio el formulario para cargar un usuario nuevo
        LimpiarFormu()
    End Sub

End Class

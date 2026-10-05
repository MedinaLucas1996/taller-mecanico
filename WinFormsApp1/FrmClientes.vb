Imports MySqlConnector

Public Class FrmClientes

    'indica si el registro seleccionado esta activo, decide si el boton da de baja o reactiva
    Private registroActivo As Boolean = True

    Sub CargarClientes(Optional filtro As String = "")
        'creo subrutina para cargar grilla
        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql
                Dim consulta As String =
                    "SELECT id_cliente, razon_social, documento, domicilio, localidad, " &
                    "telefono, email, observaciones, " &
                    "CASE WHEN activo = 1 THEN 'Sí' ELSE 'No' END AS esta_activo " &
                    "FROM cliente " &
                    "WHERE 1 = 1 "

                'solo traigo clientes activos, salvo que se pida ver tambien los dados de baja
                If Not chkBajas.Checked Then
                    consulta = consulta & "AND activo = 1 "
                End If

                'aplico filtro por nombre o documento
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (razon_social LIKE @filtro OR documento LIKE @filtro) "
                End If

                'primero los activos, despues los dados de baja
                consulta = consulta & "ORDER BY activo DESC, razon_social;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvClientes.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvClientes.Columns("id_cliente").HeaderText = "ID"
                    'la columna ID se ajusta al contenido, no ocupa lo mismo que las demas
                    dgvClientes.Columns("id_cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvClientes.Columns("razon_social").HeaderText = "Nombre / Razón social"
                    dgvClientes.Columns("documento").HeaderText = "Documento"
                    dgvClientes.Columns("domicilio").HeaderText = "Domicilio"
                    dgvClientes.Columns("localidad").HeaderText = "Localidad"
                    dgvClientes.Columns("telefono").HeaderText = "Teléfono"
                    dgvClientes.Columns("email").HeaderText = "Correo"
                    'las observaciones se ven en el formulario, no en la grilla
                    dgvClientes.Columns("observaciones").Visible = False
                    'la columna Activo solo se ve cuando tambien se listan los dados de baja
                    dgvClientes.Columns("esta_activo").HeaderText = "Activo"
                    dgvClientes.Columns("esta_activo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvClientes.Columns("esta_activo").Visible = chkBajas.Checked
                End Using
            End Using
        Catch ex As Exception
            'muestro mensaje de error
            MessageBox.Show("Error al cargar los clientes: " & ex.Message)
        End Try
    End Sub

    Sub LimpiarFormu()
        'limpio los campos del formulario
        txtID.Clear()
        txtRazonSocial.Clear()
        txtDocumento.Clear()
        txtDomicilio.Clear()
        txtLocalidad.Clear()
        txtTelefono.Clear()
        txtEmail.Clear()
        txtObservaciones.Clear()
        'sin registro seleccionado el boton vuelve a ser el de la baja
        registroActivo = True
        btnEliminar.Text = "Dar de baja"
        dgvClientes.ClearSelection()
        txtRazonSocial.Focus()
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If txtRazonSocial.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre o razón social")
            txtRazonSocial.Focus()
            Return False
        End If

        If txtDocumento.Text.Trim = "" Then
            MessageBox.Show("Falta el documento")
            txtDocumento.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'cargo la grilla de clientes al abrir el formulario
        CargarClientes()
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito
        CargarClientes(txtFiltro.Text.Trim)
    End Sub

    Private Sub chkBajas_CheckedChanged(sender As Object, e As EventArgs) Handles chkBajas.CheckedChanged
        'muestro o dejo de mostrar los clientes dados de baja, el formulario queda limpio
        LimpiarFormu()
        CargarClientes(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un cliente nuevo

        'si hay un ID cargado, el usuario quiere modificar, no guardar
        If txtID.Text.Trim <> "" Then
            MessageBox.Show("Hay un cliente seleccionado. Use MODIFICAR o presione LIMPIAR para cargar uno nuevo.")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql
                Dim consulta As String =
                    "INSERT INTO cliente (razon_social, documento, domicilio, localidad, telefono, email, observaciones) " &
                    "VALUES (@razon_social, @documento, @domicilio, @localidad, @telefono, @email, @observaciones);"

                Using cmd As New MySqlCommand(consulta, cn)
                    'cargo valores en los parametros
                    cmd.Parameters.AddWithValue("@razon_social", txtRazonSocial.Text.Trim)
                    cmd.Parameters.AddWithValue("@documento", txtDocumento.Text.Trim)
                    cmd.Parameters.AddWithValue("@domicilio", txtDomicilio.Text.Trim)
                    cmd.Parameters.AddWithValue("@localidad", txtLocalidad.Text.Trim)
                    cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text.Trim)
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim)
                    cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text.Trim)

                    'ejecuto la consulta y obtengo los registros afectados
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros agregados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarClientes()

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: el documento ya existe (restriccion unica)
            MessageBox.Show("Ya existe un cliente con ese documento.")
            txtDocumento.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al guardar " & ex.Message)
        End Try
    End Sub

    Private Sub dgvClientes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientes.CellClick
        'traigo los datos de la fila seleccionada a los textbox
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvClientes.Rows(e.RowIndex)
        txtID.Text = fila.Cells("id_cliente").Value.ToString()
        txtRazonSocial.Text = fila.Cells("razon_social").Value.ToString()
        txtDocumento.Text = fila.Cells("documento").Value.ToString()
        txtDomicilio.Text = fila.Cells("domicilio").Value.ToString()
        txtLocalidad.Text = fila.Cells("localidad").Value.ToString()
        txtTelefono.Text = fila.Cells("telefono").Value.ToString()
        txtEmail.Text = fila.Cells("email").Value.ToString()
        txtObservaciones.Text = fila.Cells("observaciones").Value.ToString()

        'segun el estado del registro, el mismo boton da de baja o reactiva
        registroActivo = (fila.Cells("esta_activo").Value.ToString() = "Sí")
        If registroActivo Then
            btnEliminar.Text = "Dar de baja"
        Else
            btnEliminar.Text = "Reactivar"
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        'valido que haya un cliente seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un cliente para modificar")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'uso update para modificar y where para indicar que registro
                Dim consulta As String =
                    "UPDATE cliente SET razon_social=@razon_social, documento=@documento, " &
                    "domicilio=@domicilio, localidad=@localidad, telefono=@telefono, " &
                    "email=@email, observaciones=@observaciones " &
                    "WHERE id_cliente=@id;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'uso parametros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@razon_social", txtRazonSocial.Text.Trim)
                    cmd.Parameters.AddWithValue("@documento", txtDocumento.Text.Trim)
                    cmd.Parameters.AddWithValue("@domicilio", txtDomicilio.Text.Trim)
                    cmd.Parameters.AddWithValue("@localidad", txtLocalidad.Text.Trim)
                    cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text.Trim)
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim)
                    cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text.Trim)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))

                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros actualizados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarClientes(txtFiltro.Text.Trim)

        Catch ex As MySqlException When ex.Number = 1062
            MessageBox.Show("Ya existe otro cliente con ese documento.")
            txtDocumento.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al modificar " & ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'doy de baja al cliente seleccionado, o lo reactivo si ya estaba dado de baja

        'valido que haya un cliente seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un cliente para dar de baja")
            Exit Sub
        End If

        Dim idRegistro As Integer = CInt(txtID.Text)

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If registroActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja al cliente " & txtRazonSocial.Text & "?",
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar al cliente " & txtRazonSocial.Text & "?",
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'estado del cliente leido en la base dentro de la transaccion
        Dim activoEnBase As Boolean = False
        'queda en True cuando lo que hay en pantalla ya no coincide con la base
        Dim desactualizado As Boolean = False

        'baja logica: no borro el registro porque puede tener ordenes de trabajo
        'solo lo marco como inactivo (regla 8.7 de la especificacion), y con el mismo boton se lo puede reactivar
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el cliente y bloqueo solo su fila hasta terminar
                    Dim existe As Boolean = False
                    Dim consulta As String = "SELECT activo FROM cliente WHERE id_cliente = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idRegistro)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then
                            existe = True
                            activoEnBase = Convert.ToBoolean(resultado)
                        End If
                    End Using

                    If Not existe Then
                        aviso = "El cliente seleccionado ya no existe."
                        desactualizado = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activoEnBase <> registroActivo Then
                        aviso = "El estado del cliente fue cambiado desde otro puesto. Se actualizó la lista: revíselo y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso activoEnBase Then
                        'no se da de baja a un cliente que tiene vehiculos activos
                        consulta = "SELECT COUNT(*) FROM vehiculo WHERE id_cliente = @id AND activo = 1;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
                            Dim vehiculosActivos As Integer = CInt(cmd.ExecuteScalar())
                            If vehiculosActivos > 0 Then
                                aviso = "No se puede dar de baja: el cliente tiene " & vehiculosActivos &
                                        " vehículo(s) activo(s). Cámbieles el titular o delos de baja primero."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE cliente SET activo = @activo WHERE id_cliente = @id;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@activo", Not activoEnBase)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
                            cmd.ExecuteNonQuery()
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
            MessageBox.Show("Error al dar de baja " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
            'si el cliente cambio desde otro puesto, limpio el formulario y muestro el estado real
            If desactualizado Then
                LimpiarFormu()
                CargarClientes(txtFiltro.Text.Trim)
            End If
            Exit Sub
        End If

        If activoEnBase Then
            MessageBox.Show("Clientes dados de baja: 1")
        Else
            MessageBox.Show("Clientes reactivados: 1")
        End If

        LimpiarFormu()
        CargarClientes(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'limpio el formulario para cargar un cliente nuevo
        LimpiarFormu()
    End Sub

End Class

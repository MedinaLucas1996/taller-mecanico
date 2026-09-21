Imports MySqlConnector

Public Class FrmClientes

    Sub CargarClientes(Optional filtro As String = "")
        'creo subrutina para cargar grilla
        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql, solo traigo clientes activos
                Dim consulta As String =
                    "SELECT id_cliente, razon_social, documento, domicilio, localidad, " &
                    "telefono, email, observaciones " &
                    "FROM cliente " &
                    "WHERE activo = 1 "

                'aplico filtro por nombre o documento
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (razon_social LIKE @filtro OR documento LIKE @filtro) "
                End If

                consulta = consulta & "ORDER BY razon_social;"

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
        'valido que haya un cliente seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un cliente para dar de baja")
            Exit Sub
        End If

        'pido confirmacion antes de dar de baja
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Dar de baja al cliente " & txtRazonSocial.Text & "?",
            "Dar de baja",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        'baja logica: no borro el registro porque puede tener ordenes de trabajo
        'solo lo marco como inactivo (regla 8.7 de la especificacion)
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "UPDATE cliente SET activo = 0 WHERE id_cliente=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Clientes dados de baja: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarClientes(txtFiltro.Text.Trim)

        Catch ex As Exception
            MessageBox.Show("Error al dar de baja " & ex.Message)
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'limpio el formulario para cargar un cliente nuevo
        LimpiarFormu()
    End Sub

End Class

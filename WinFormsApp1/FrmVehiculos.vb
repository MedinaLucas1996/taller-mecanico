Imports MySqlConnector

Public Class FrmVehiculos

    Sub CargarVehiculos(Optional filtro As String = "")
        'creo subrutina para cargar grilla
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql uniendo vehiculo con cliente, modelo y marca
                'solo traigo vehiculos activos
                Dim consulta As String =
                    "SELECT v.id_vehiculo, v.patente, c.razon_social AS titular, " &
                    "ma.descripcion AS marca, mo.descripcion AS modelo, v.anio, v.color, " &
                    "v.nro_motor, v.nro_chasis, v.km_actual, v.observaciones, " &
                    "v.id_cliente, mo.id_marca, v.id_modelo " &
                    "FROM vehiculo AS v " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "WHERE v.activo = 1 "

                'aplico filtro por patente, titular, marca o modelo
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (v.patente LIKE @filtro OR c.razon_social LIKE @filtro " &
                        "OR ma.descripcion LIKE @filtro OR mo.descripcion LIKE @filtro) "
                End If

                consulta = consulta & "ORDER BY v.patente;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvVehiculos.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvVehiculos.Columns("id_vehiculo").HeaderText = "ID"
                    dgvVehiculos.Columns("id_vehiculo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvVehiculos.Columns("patente").HeaderText = "Patente"
                    dgvVehiculos.Columns("titular").HeaderText = "Titular"
                    dgvVehiculos.Columns("marca").HeaderText = "Marca"
                    dgvVehiculos.Columns("modelo").HeaderText = "Modelo"
                    dgvVehiculos.Columns("anio").HeaderText = "Año"
                    dgvVehiculos.Columns("color").HeaderText = "Color"
                    dgvVehiculos.Columns("nro_motor").HeaderText = "N.º motor"
                    dgvVehiculos.Columns("nro_chasis").HeaderText = "N.º chasis"
                    dgvVehiculos.Columns("km_actual").HeaderText = "Kilometraje"
                    dgvVehiculos.Columns("km_actual").DefaultCellStyle.Format = "N0"

                    'oculto las claves y las observaciones, se usan al seleccionar la fila
                    dgvVehiculos.Columns("observaciones").Visible = False
                    dgvVehiculos.Columns("id_cliente").Visible = False
                    dgvVehiculos.Columns("id_marca").Visible = False
                    dgvVehiculos.Columns("id_modelo").Visible = False
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los vehículos: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboTitulares()
        'cargo los clientes activos en el combo de titular
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_cliente, razon_social FROM cliente WHERE activo = 1 ORDER BY razon_social;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'el usuario ve el nombre...
                    cboTitular.DisplayMember = "razon_social"
                    '...pero el programa guarda la clave numerica
                    cboTitular.ValueMember = "id_cliente"
                    cboTitular.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los titulares: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboMarcas()
        'cargo todas las marcas en el combo
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "SELECT id_marca, descripcion FROM marca ORDER BY descripcion;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'agrego una primera opcion con clave 0 que funciona como texto de ayuda
                    Dim filaAyuda As DataRow = tabla.NewRow()
                    filaAyuda("id_marca") = 0
                    filaAyuda("descripcion") = "Seleccione una marca"
                    tabla.Rows.InsertAt(filaAyuda, 0)

                    cboMarca.DisplayMember = "descripcion"
                    cboMarca.ValueMember = "id_marca"
                    cboMarca.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las marcas: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboModelos(idMarca As Integer)
        'cargo solo los modelos de la marca elegida (combo en cascada)
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_modelo, descripcion FROM modelo WHERE id_marca = @id_marca ORDER BY descripcion;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_marca", idMarca)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'primera opcion de ayuda, para no elegir un modelo sin querer
                    Dim filaAyuda As DataRow = tabla.NewRow()
                    filaAyuda("id_modelo") = 0
                    filaAyuda("descripcion") = "Seleccione un modelo"
                    tabla.Rows.InsertAt(filaAyuda, 0)

                    cboModelo.DisplayMember = "descripcion"
                    cboModelo.ValueMember = "id_modelo"
                    cboModelo.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los modelos: " & ex.Message)
        End Try
    End Sub

    Sub LimpiarFormu()
        'limpio los campos del formulario
        txtID.Clear()
        cboTitular.SelectedIndex = -1
        cboTitular.Text = ""
        txtPatente.Clear()
        'vuelvo a la opcion "Seleccione una marca", eso tambien vacia los modelos
        If cboMarca.Items.Count > 0 Then cboMarca.SelectedIndex = 0
        cboModelo.DataSource = Nothing
        nudAnio.Value = Date.Now.Year
        txtColor.Clear()
        txtMotor.Clear()
        txtChasis.Clear()
        nudKilometraje.Value = 0
        txtObservaciones.Clear()
        dgvVehiculos.ClearSelection()
        cboTitular.Focus()
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If cboTitular.SelectedValue Is Nothing Then
            MessageBox.Show("Seleccione un titular de la lista")
            cboTitular.Focus()
            Return False
        End If

        If txtPatente.Text.Trim = "" Then
            MessageBox.Show("Falta la patente")
            txtPatente.Focus()
            Return False
        End If

        'la clave 0 corresponde a la opcion de ayuda, no a una marca real
        If Not TypeOf cboMarca.SelectedValue Is Integer OrElse CInt(cboMarca.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione una marca")
            cboMarca.Focus()
            Return False
        End If

        If Not TypeOf cboModelo.SelectedValue Is Integer OrElse CInt(cboModelo.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione un modelo")
            cboModelo.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmVehiculos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'cargo combos y grilla al abrir el formulario
        CargarComboTitulares()
        CargarComboMarcas()
        CargarVehiculos()
        LimpiarFormu()
    End Sub

    Private Sub cboMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMarca.SelectedIndexChanged
        'cuando cambia la marca, recargo los modelos de esa marca
        If TypeOf cboMarca.SelectedValue Is Integer AndAlso CInt(cboMarca.SelectedValue) > 0 Then
            CargarComboModelos(CInt(cboMarca.SelectedValue))
        Else
            'con "Seleccione una marca" no hay modelos para mostrar
            cboModelo.DataSource = Nothing
        End If
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito
        CargarVehiculos(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un vehiculo nuevo

        'si hay un ID cargado, el usuario quiere modificar, no guardar
        If txtID.Text.Trim <> "" Then
            MessageBox.Show("Hay un vehículo seleccionado. Use MODIFICAR o presione LIMPIAR para cargar uno nuevo.")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "INSERT INTO vehiculo (patente, id_cliente, id_modelo, anio, color, nro_motor, nro_chasis, km_actual, observaciones) " &
                    "VALUES (@patente, @id_cliente, @id_modelo, @anio, @color, @nro_motor, @nro_chasis, @km_actual, @observaciones);"

                Using cmd As New MySqlCommand(consulta, cn)
                    'cargo valores en los parametros
                    cmd.Parameters.AddWithValue("@patente", txtPatente.Text.Trim)
                    'SelectedValue contiene la clave del elemento elegido
                    cmd.Parameters.AddWithValue("@id_cliente", CInt(cboTitular.SelectedValue))
                    cmd.Parameters.AddWithValue("@id_modelo", CInt(cboModelo.SelectedValue))
                    cmd.Parameters.AddWithValue("@anio", CInt(nudAnio.Value))
                    cmd.Parameters.AddWithValue("@color", txtColor.Text.Trim)
                    cmd.Parameters.AddWithValue("@nro_motor", txtMotor.Text.Trim)
                    cmd.Parameters.AddWithValue("@nro_chasis", txtChasis.Text.Trim)
                    cmd.Parameters.AddWithValue("@km_actual", CInt(nudKilometraje.Value))
                    cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text.Trim)

                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros agregados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarVehiculos()

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: la patente ya existe (restriccion unica)
            MessageBox.Show("Ya existe un vehículo con esa patente.")
            txtPatente.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al guardar " & ex.Message)
        End Try
    End Sub

    Private Sub dgvVehiculos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVehiculos.CellClick
        'traigo los datos de la fila seleccionada al formulario
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvVehiculos.Rows(e.RowIndex)
        txtID.Text = fila.Cells("id_vehiculo").Value.ToString()
        txtPatente.Text = fila.Cells("patente").Value.ToString()
        cboTitular.SelectedValue = CInt(fila.Cells("id_cliente").Value)

        'primero elijo la marca: eso recarga los modelos de esa marca
        cboMarca.SelectedValue = CInt(fila.Cells("id_marca").Value)
        'despues elijo el modelo dentro de la lista ya cargada
        cboModelo.SelectedValue = CInt(fila.Cells("id_modelo").Value)

        'el año puede estar vacio en la base
        If IsDBNull(fila.Cells("anio").Value) Then
            nudAnio.Value = Date.Now.Year
        Else
            nudAnio.Value = CInt(fila.Cells("anio").Value)
        End If

        txtColor.Text = fila.Cells("color").Value.ToString()
        txtMotor.Text = fila.Cells("nro_motor").Value.ToString()
        txtChasis.Text = fila.Cells("nro_chasis").Value.ToString()
        nudKilometraje.Value = CInt(fila.Cells("km_actual").Value)
        txtObservaciones.Text = fila.Cells("observaciones").Value.ToString()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        'valido que haya un vehiculo seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un vehículo para modificar")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'cambiar el titular aca no pierde el historial:
                'cada orden de trabajo guarda su propio cliente (regla 8.4)
                Dim consulta As String =
                    "UPDATE vehiculo SET patente=@patente, id_cliente=@id_cliente, id_modelo=@id_modelo, " &
                    "anio=@anio, color=@color, nro_motor=@nro_motor, nro_chasis=@nro_chasis, " &
                    "km_actual=@km_actual, observaciones=@observaciones " &
                    "WHERE id_vehiculo=@id;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@patente", txtPatente.Text.Trim)
                    cmd.Parameters.AddWithValue("@id_cliente", CInt(cboTitular.SelectedValue))
                    cmd.Parameters.AddWithValue("@id_modelo", CInt(cboModelo.SelectedValue))
                    cmd.Parameters.AddWithValue("@anio", CInt(nudAnio.Value))
                    cmd.Parameters.AddWithValue("@color", txtColor.Text.Trim)
                    cmd.Parameters.AddWithValue("@nro_motor", txtMotor.Text.Trim)
                    cmd.Parameters.AddWithValue("@nro_chasis", txtChasis.Text.Trim)
                    cmd.Parameters.AddWithValue("@km_actual", CInt(nudKilometraje.Value))
                    cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text.Trim)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))

                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros actualizados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarVehiculos(txtFiltro.Text.Trim)

        Catch ex As MySqlException When ex.Number = 1062
            MessageBox.Show("Ya existe otro vehículo con esa patente.")
            txtPatente.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al modificar " & ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'valido que haya un vehiculo seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un vehículo para dar de baja")
            Exit Sub
        End If

        'pido confirmacion antes de dar de baja
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Dar de baja el vehículo " & txtPatente.Text & "?",
            "Dar de baja",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        'baja logica: el vehiculo puede tener ordenes de trabajo asociadas
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "UPDATE vehiculo SET activo = 0 WHERE id_vehiculo=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Vehículos dados de baja: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarVehiculos(txtFiltro.Text.Trim)

        Catch ex As Exception
            MessageBox.Show("Error al dar de baja " & ex.Message)
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'limpio el formulario para cargar un vehiculo nuevo
        LimpiarFormu()
    End Sub

End Class

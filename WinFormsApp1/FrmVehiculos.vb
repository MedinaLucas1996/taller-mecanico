Imports MySqlConnector

Public Class FrmVehiculos

    'indica si el registro seleccionado esta activo, decide si el boton da de baja o reactiva
    Private registroActivo As Boolean = True

    Sub CargarVehiculos(Optional filtro As String = "")
        'creo subrutina para cargar grilla
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql uniendo vehiculo con cliente, modelo y marca
                Dim consulta As String =
                    "SELECT v.id_vehiculo, v.patente, c.razon_social AS titular, " &
                    "ma.descripcion AS marca, mo.descripcion AS modelo, v.anio, v.color, " &
                    "v.nro_motor, v.nro_chasis, v.km_actual, v.observaciones, " &
                    "v.id_cliente, mo.id_marca, v.id_modelo, " &
                    "CASE WHEN v.activo = 1 THEN 'Sí' ELSE 'No' END AS esta_activo " &
                    "FROM vehiculo AS v " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "WHERE 1 = 1 "

                'solo traigo vehiculos activos, salvo que se pida ver tambien los dados de baja
                If Not chkBajas.Checked Then
                    consulta = consulta & "AND v.activo = 1 "
                End If

                'aplico filtro por patente, titular, marca o modelo
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (v.patente LIKE @filtro OR c.razon_social LIKE @filtro " &
                        "OR ma.descripcion LIKE @filtro OR mo.descripcion LIKE @filtro) "
                End If

                'primero los activos, despues los dados de baja
                consulta = consulta & "ORDER BY v.activo DESC, v.patente;"

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
                    'la columna Activo solo se ve cuando tambien se listan los dados de baja
                    dgvVehiculos.Columns("esta_activo").HeaderText = "Activo"
                    dgvVehiculos.Columns("esta_activo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvVehiculos.Columns("esta_activo").Visible = chkBajas.Checked
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
        'sin registro seleccionado el boton vuelve a ser el de la baja
        registroActivo = True
        btnEliminar.Text = "Dar de baja"
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

    Private Sub chkBajas_CheckedChanged(sender As Object, e As EventArgs) Handles chkBajas.CheckedChanged
        'muestro o dejo de mostrar los vehiculos dados de baja, el formulario queda limpio
        LimpiarFormu()
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

        'segun el estado del registro, el mismo boton da de baja o reactiva
        registroActivo = (fila.Cells("esta_activo").Value.ToString() = "Sí")
        If registroActivo Then
            btnEliminar.Text = "Dar de baja"
        Else
            btnEliminar.Text = "Reactivar"
        End If
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
        'doy de baja el vehiculo seleccionado, o lo reactivo si ya estaba dado de baja

        'valido que haya un vehiculo seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un vehículo para dar de baja")
            Exit Sub
        End If

        Dim idRegistro As Integer = CInt(txtID.Text)

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If registroActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja el vehículo " & txtPatente.Text & "?",
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar el vehículo " & txtPatente.Text & "?",
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'estado del vehiculo leido en la base dentro de la transaccion
        Dim activoEnBase As Boolean = False
        'queda en True cuando lo que hay en pantalla ya no coincide con la base
        Dim desactualizado As Boolean = False

        'baja logica: el vehiculo puede tener ordenes de trabajo asociadas
        'solo lo marco como inactivo, y con el mismo boton se lo puede reactivar
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el vehiculo y bloqueo solo su fila hasta terminar
                    Dim existe As Boolean = False
                    Dim idTitular As Integer = 0
                    Dim consulta As String = "SELECT activo, id_cliente FROM vehiculo WHERE id_vehiculo = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idRegistro)
                        Using lector As MySqlDataReader = cmd.ExecuteReader
                            If lector.Read() Then
                                existe = True
                                activoEnBase = Convert.ToBoolean(lector("activo"))
                                idTitular = CInt(lector("id_cliente"))
                            End If
                        End Using
                    End Using

                    If Not existe Then
                        aviso = "El vehículo seleccionado ya no existe."
                        desactualizado = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activoEnBase <> registroActivo Then
                        aviso = "El estado del vehículo fue cambiado desde otro puesto. Se actualizó la lista: revíselo y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso activoEnBase Then
                        'no se da de baja un vehiculo con una orden de trabajo sin cerrar (estado no final)
                        consulta =
                            "SELECT COUNT(*) AS abiertas, MAX(ot.nro_orden) AS ultima " &
                            "FROM orden_trabajo AS ot " &
                            "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                            "WHERE ot.id_vehiculo = @id AND e.es_estado_final = 0;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
                            Using lector As MySqlDataReader = cmd.ExecuteReader
                                If lector.Read() AndAlso CInt(lector("abiertas")) > 0 Then
                                    aviso = "No se puede dar de baja: el vehículo tiene " & lector("abiertas").ToString() &
                                            " orden(es) de trabajo sin cerrar (la N.º " & lector("ultima").ToString() &
                                            "). Ciérrela o anúlela primero."
                                End If
                            End Using
                        End Using
                    End If

                    If aviso = "" AndAlso Not activoEnBase Then
                        'no se reactiva un vehiculo cuyo titular esta dado de baja
                        consulta = "SELECT razon_social, activo FROM cliente WHERE id_cliente = @id_cliente;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_cliente", idTitular)
                            Using lector As MySqlDataReader = cmd.ExecuteReader
                                If lector.Read() AndAlso Not Convert.ToBoolean(lector("activo")) Then
                                    aviso = "No se puede reactivar: su titular, " & lector("razon_social").ToString() &
                                            ", está dado de baja. Reactive primero al cliente en ""Clientes""."
                                End If
                            End Using
                        End Using
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE vehiculo SET activo = @activo WHERE id_vehiculo = @id;"
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
            'si el vehiculo cambio desde otro puesto, limpio el formulario y muestro el estado real
            If desactualizado Then
                LimpiarFormu()
                CargarVehiculos(txtFiltro.Text.Trim)
            End If
            Exit Sub
        End If

        If activoEnBase Then
            MessageBox.Show("Vehículos dados de baja: 1")
        Else
            MessageBox.Show("Vehículos reactivados: 1")
        End If

        LimpiarFormu()
        CargarVehiculos(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'limpio el formulario para cargar un vehiculo nuevo
        LimpiarFormu()
    End Sub

End Class

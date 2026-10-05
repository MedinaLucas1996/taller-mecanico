Imports MySqlConnector

Public Class FrmAltaRapida

    'alta rapida de un vehiculo, y de su titular si hace falta, sin salir de la recepcion
    'la recepcion carga la patente buscada antes de abrir la ventana y la lee de nuevo al cerrarse:
    'si se cierra con "Registrar y continuar" trae la patente que quedo guardada
    Public Patente As String = ""

    'cliente elegido en la grilla como titular, queda en 0 mientras no haya uno
    Private idClienteElegido As Integer = 0

    Sub CargarClientes(Optional filtro As String = "")
        'cargo la grilla con los clientes activos, para elegir al titular
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_cliente, razon_social, documento, telefono " &
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

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    dgvClientes.DataSource = tabla

                    'la clave se usa al elegir la fila, no se muestra
                    dgvClientes.Columns("id_cliente").Visible = False
                    dgvClientes.Columns("razon_social").HeaderText = "Nombre / Razón social"
                    dgvClientes.Columns("documento").HeaderText = "Documento"
                    dgvClientes.Columns("documento").FillWeight = 40
                    dgvClientes.Columns("telefono").HeaderText = "Teléfono"
                    dgvClientes.Columns("telefono").FillWeight = 40
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los clientes: " & ex.Message)
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

    Function ValidarCampos() As Boolean
        'valido el titular y el vehiculo con las mismas reglas de Clientes y de Vehiculos

        If rbNuevo.Checked Then
            'cliente nuevo: nombre y documento obligatorios
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
        Else
            'cliente ya registrado: tiene que haber uno elegido en la grilla
            If idClienteElegido = 0 Then
                MessageBox.Show("Seleccione un titular de la lista, o elija ""Cliente nuevo"" para cargarlo.")
                txtBuscarCliente.Focus()
                Return False
            End If
        End If

        If txtPatente.Text.Trim = "" Then
            MessageBox.Show("Falta la patente")
            txtPatente.Focus()
            Return False
        End If

        'la clave 0 corresponde a la opcion de ayuda, no a una marca real
        If cboMarca.SelectedValue Is Nothing OrElse Convert.ToInt32(cboMarca.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione una marca")
            cboMarca.Focus()
            Return False
        End If

        If cboModelo.SelectedValue Is Nothing OrElse Convert.ToInt32(cboModelo.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione un modelo")
            cboModelo.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmAltaRapida_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'registran clientes y vehiculos el administrador y el operador, igual que en la recepcion
        If Sesion.Rol <> "ADMINISTRADOR" AndAlso Sesion.Rol <> "OPERADOR" Then
            MessageBox.Show("Solo el administrador y el operador pueden registrar clientes y vehículos.")
            Me.Close()
            Exit Sub
        End If

        'icono del boton principal, si falta queda solo con el texto
        btnRegistrar.Image = LeerIcono("confirmar.png")

        'la patente llega cargada con la que se busco en la recepcion
        txtPatente.Text = Patente.Trim.ToUpper
        nudAnio.Value = Date.Now.Year

        CargarClientes()
        CargarComboMarcas()
    End Sub

    Private Sub rbExistente_CheckedChanged(sender As Object, e As EventArgs) Handles rbExistente.CheckedChanged
        'muestro la busqueda de clientes o los campos del cliente nuevo, uno de los dos
        pnlExistente.Visible = rbExistente.Checked
        pnlNuevo.Visible = Not rbExistente.Checked

        If rbExistente.Checked Then
            txtBuscarCliente.Focus()
        Else
            txtRazonSocial.Focus()
        End If
    End Sub

    Private Sub txtBuscarCliente_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCliente.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito, el titular ya elegido se conserva
        CargarClientes(txtBuscarCliente.Text.Trim)
    End Sub

    Private Sub dgvClientes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvClientes.DataBindingComplete
        'la grilla selecciona sola la primera fila, la desmarco: el titular se elige con un click
        dgvClientes.ClearSelection()
    End Sub

    Private Sub dgvClientes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientes.CellClick
        'el cliente de la fila elegida queda como titular del vehiculo
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvClientes.Rows(e.RowIndex)
        idClienteElegido = CInt(fila.Cells("id_cliente").Value)
        lblClienteElegido.Text = "Titular elegido: " & fila.Cells("razon_social").Value.ToString() &
                                 " (documento " & fila.Cells("documento").Value.ToString() & ")"
    End Sub

    Private Sub cboMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMarca.SelectedIndexChanged
        'al cambiar la marca recargo los modelos de esa marca
        If TypeOf cboMarca.SelectedValue Is Integer AndAlso CInt(cboMarca.SelectedValue) > 0 Then
            CargarComboModelos(CInt(cboMarca.SelectedValue))
        Else
            'sin marca elegida no hay modelos para mostrar
            cboModelo.DataSource = Nothing
        End If
    End Sub

    Private Sub chkAnio_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnio.CheckedChanged
        'el año es opcional: solo se carga con la casilla marcada
        nudAnio.Enabled = chkAnio.Checked
    End Sub

    Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        'guardo el cliente, si es nuevo, y el vehiculo con ese cliente como titular

        If Not ValidarCampos() Then Exit Sub

        'la patente se guarda sin espacios y en mayusculas, como la busca la recepcion
        Dim patenteNueva As String = txtPatente.Text.Trim.ToUpper
        Dim idMarca As Integer = Convert.ToInt32(cboMarca.SelectedValue)
        Dim idModelo As Integer = Convert.ToInt32(cboModelo.SelectedValue)

        'los datos opcionales vacios se guardan como NULL
        Dim telefono As Object = DBNull.Value
        If txtTelefono.Text.Trim <> "" Then telefono = txtTelefono.Text.Trim
        Dim anio As Object = DBNull.Value
        If chkAnio.Checked Then anio = CInt(nudAnio.Value)
        Dim colorVehiculo As Object = DBNull.Value
        If txtColor.Text.Trim <> "" Then colorVehiculo = txtColor.Text.Trim

        Dim aviso As String = ""
        'queda en True cuando el cliente elegido en la grilla dejo de estar activo
        Dim clienteInvalido As Boolean = False

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'el cliente nuevo y el vehiculo se guardan los dos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    Dim idTitular As Integer = idClienteElegido
                    Dim consulta As String

                    If Not rbNuevo.Checked Then
                        'vuelvo a leer el cliente elegido: otro puesto pudo darlo de baja
                        Dim sigueActivo As Boolean = False
                        consulta = "SELECT activo FROM cliente WHERE id_cliente = @id_cliente FOR UPDATE;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_cliente", idClienteElegido)
                            Dim resultado As Object = cmd.ExecuteScalar()
                            If resultado IsNot Nothing Then sigueActivo = Convert.ToBoolean(resultado)
                        End Using

                        If Not sigueActivo Then
                            aviso = "El cliente elegido ya no está activo. Búsquelo de nuevo o cargue un cliente nuevo."
                            clienteInvalido = True
                        End If
                    End If

                    'el modelo elegido tiene que seguir existiendo, y ser de la marca elegida
                    If aviso = "" Then
                        consulta = "SELECT COUNT(*) FROM modelo WHERE id_modelo = @id_modelo AND id_marca = @id_marca;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_modelo", idModelo)
                            cmd.Parameters.AddWithValue("@id_marca", idMarca)
                            If CInt(cmd.ExecuteScalar()) = 0 Then
                                aviso = "El modelo elegido ya no existe. Vuelva a elegir la marca y el modelo."
                            End If
                        End Using
                    End If

                    If aviso = "" AndAlso rbNuevo.Checked Then
                        'guardo el cliente nuevo y me quedo con la clave que le dio la base
                        consulta =
                            "INSERT INTO cliente (razon_social, documento, telefono) " &
                            "VALUES (@razon_social, @documento, @telefono);"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@razon_social", txtRazonSocial.Text.Trim)
                            cmd.Parameters.AddWithValue("@documento", txtDocumento.Text.Trim)
                            cmd.Parameters.AddWithValue("@telefono", telefono)
                            cmd.ExecuteNonQuery()
                            idTitular = CInt(cmd.LastInsertedId)
                        End Using
                    End If

                    If aviso = "" Then
                        'guardo el vehiculo con ese titular, el kilometraje arranca en 0
                        consulta =
                            "INSERT INTO vehiculo (patente, id_cliente, id_modelo, anio, color, km_actual) " &
                            "VALUES (@patente, @id_cliente, @id_modelo, @anio, @color, @km_actual);"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@patente", patenteNueva)
                            cmd.Parameters.AddWithValue("@id_cliente", idTitular)
                            cmd.Parameters.AddWithValue("@id_modelo", idModelo)
                            cmd.Parameters.AddWithValue("@anio", anio)
                            cmd.Parameters.AddWithValue("@color", colorVehiculo)
                            cmd.Parameters.AddWithValue("@km_actual", 0)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                Catch ex As Exception
                    'algo fallo antes de confirmar: deshago todo y dejo que el error llegue a los mensajes de abajo
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

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: se repite el documento del cliente o la patente del vehiculo
            'el mensaje de la base nombra la restriccion unica que fallo
            If ex.Message.Contains("un_cliente_documento") Then
                MessageBox.Show("Ya existe un cliente con ese documento. No se guardó nada." & vbCrLf &
                                "Elija ""Cliente ya registrado"" y búsquelo por su documento. " &
                                "Si no aparece, puede estar dado de baja.")
                txtDocumento.Focus()
            ElseIf ex.Message.Contains("un_vehiculo_patente") Then
                MessageBox.Show("Ya existe un vehículo con esa patente. No se guardó nada." & vbCrLf &
                                "Puede estar dado de baja: revíselo en ""Vehículos"".")
                txtPatente.Focus()
            Else
                MessageBox.Show("Ya existe un registro con esos datos y no se guardó nada." & vbCrLf &
                                "Detalle: " & ex.Message)
            End If
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("No se pudo registrar el vehículo y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
            'recargo la grilla y olvido al titular elegido, que ya no sirve
            If clienteInvalido Then
                idClienteElegido = 0
                lblClienteElegido.Text = "Titular elegido: ninguno"
                CargarClientes(txtBuscarCliente.Text.Trim)
            End If
            Exit Sub
        End If

        'devuelvo la patente guardada: la recepcion la busca y sigue como con cualquier vehiculo
        Patente = patenteNueva
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class

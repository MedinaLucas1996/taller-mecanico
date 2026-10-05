Imports MySqlConnector

Public Class FrmOrdenGestion

    'orden que se gestiona, la carga el tablero antes de abrir el formulario
    Public IdOrdenTrabajo As Integer = 0

    'datos del estado actual de la orden, se vuelven a leer en cada carga
    Private codigoEstado As String = ""
    Private permiteEdicion As Boolean = False
    Private esEstadoFinal As Boolean = True

    'linea del presupuesto elegida en la grilla, queda en 0 mientras no haya una seleccionada
    Private idDetalleSeleccionado As Integer = 0

    Sub CargarComboMecanicos()
        'cargo los mecanicos activos en el combo
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'traigo tambien al mecanico de la orden aunque este dado de baja, para poder mostrarlo
                Dim consulta As String =
                    "SELECT id_mecanico, nombre_completo FROM mecanico " &
                    "WHERE activo = 1 OR id_mecanico = " &
                    "(SELECT id_mecanico FROM orden_trabajo WHERE id_orden_trabajo = @id_orden_trabajo) " &
                    "ORDER BY nombre_completo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'agrego una primera opcion con clave 0 para dejar la orden sin mecanico
                    Dim filaSinAsignar As DataRow = tabla.NewRow()
                    filaSinAsignar("id_mecanico") = 0
                    filaSinAsignar("nombre_completo") = "(sin asignar)"
                    tabla.Rows.InsertAt(filaSinAsignar, 0)

                    cboMecanico.DisplayMember = "nombre_completo"
                    cboMecanico.ValueMember = "id_mecanico"
                    cboMecanico.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los mecánicos: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboServicios()
        'cargo los servicios activos en el combo, cada uno con su precio vigente
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_servicio, descripcion, precio FROM servicio " &
                    "WHERE activo = 1 ORDER BY descripcion;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'armo el texto que ve el usuario: descripcion y precio
                    tabla.Columns.Add("texto", GetType(String))
                    For Each fila As DataRow In tabla.Rows
                        fila("texto") = fila("descripcion").ToString() & " - " & CDec(fila("precio")).ToString("C2")
                    Next

                    'agrego una primera opcion con clave 0 que obliga a elegir un servicio
                    Dim filaElegir As DataRow = tabla.NewRow()
                    filaElegir("id_servicio") = 0
                    filaElegir("descripcion") = ""
                    filaElegir("precio") = 0D
                    filaElegir("texto") = "(seleccione un servicio)"
                    tabla.Rows.InsertAt(filaElegir, 0)

                    cboServicio.DisplayMember = "texto"
                    cboServicio.ValueMember = "id_servicio"
                    cboServicio.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los servicios: " & ex.Message)
        End Try
    End Sub

    Sub CargarDetalle()
        'cargo la grilla con las lineas del presupuesto de la orden

        'al recargar la grilla ya no hay una linea seleccionada
        idDetalleSeleccionado = 0

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_ot_detalle, descripcion, cantidad, precio_unitario, subtotal, aprobado " &
                    "FROM ot_detalle " &
                    "WHERE id_orden_trabajo = @id_orden_trabajo " &
                    "ORDER BY id_ot_detalle;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    dgvDetalle.DataSource = tabla

                    'la clave de la linea se usa al seleccionar la fila, no se muestra
                    dgvDetalle.Columns("id_ot_detalle").Visible = False

                    'pongo titulos legibles en las columnas
                    dgvDetalle.Columns("descripcion").HeaderText = "Descripción"
                    dgvDetalle.Columns("cantidad").HeaderText = "Cantidad"
                    dgvDetalle.Columns("cantidad").DefaultCellStyle.Format = "N2"
                    dgvDetalle.Columns("precio_unitario").HeaderText = "Precio unitario"
                    dgvDetalle.Columns("precio_unitario").DefaultCellStyle.Format = "C2"
                    dgvDetalle.Columns("subtotal").HeaderText = "Subtotal"
                    dgvDetalle.Columns("subtotal").DefaultCellStyle.Format = "C2"
                    dgvDetalle.Columns("aprobado").HeaderText = "Aprobado"

                    'los numeros van alineados a la derecha
                    dgvDetalle.Columns("cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvDetalle.Columns("precio_unitario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvDetalle.Columns("subtotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    'las columnas de datos cortos se ajustan al contenido, la descripcion ocupa el resto
                    dgvDetalle.Columns("cantidad").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvDetalle.Columns("precio_unitario").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvDetalle.Columns("subtotal").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvDetalle.Columns("aprobado").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar el presupuesto: " & ex.Message)
        End Try
    End Sub

    Sub CargarHistorial()
        'cargo la grilla con los cambios de estado de la orden, el mas nuevo primero
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT h.fecha_hora, e.descripcion AS estado, u.nombre_completo AS usuario, h.observacion " &
                    "FROM ot_historial_estado AS h " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = h.id_estado_ot " &
                    "JOIN usuario AS u ON u.id_usuario = h.id_usuario " &
                    "WHERE h.id_orden_trabajo = @id_orden_trabajo " &
                    "ORDER BY h.fecha_hora DESC, h.id_ot_historial DESC;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    dgvHistorial.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvHistorial.Columns("fecha_hora").HeaderText = "Fecha y hora"
                    dgvHistorial.Columns("fecha_hora").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
                    dgvHistorial.Columns("estado").HeaderText = "Estado"
                    dgvHistorial.Columns("usuario").HeaderText = "Usuario"
                    dgvHistorial.Columns("observacion").HeaderText = "Observación"

                    'la fecha y el estado se ajustan al contenido, la observacion ocupa mas lugar
                    dgvHistorial.Columns("fecha_hora").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvHistorial.Columns("estado").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvHistorial.Columns("observacion").FillWeight = 200
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar el historial de estados: " & ex.Message)
        End Try
    End Sub

    Sub CargarOrden()
        'cargo los datos de la orden y habilito los controles segun su estado actual
        'se llama al abrir el formulario y despues de cada guardado

        'hasta leer el estado, la orden se trata como cerrada: no se puede tocar nada
        codigoEstado = ""
        permiteEdicion = False
        esEstadoFinal = True

        Dim encontrada As Boolean = False
        Dim idMecanico As Integer = 0

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'el cliente es el de la orden (titular al recepcionar), no el titular actual del vehiculo
                Dim consulta As String =
                    "SELECT ot.nro_orden, ot.fecha_recepcion, ot.fecha_prometida, ot.km_ingreso, " &
                    "ot.sintoma_reportado, ot.observaciones_recepcion, ot.id_mecanico, " &
                    "ot.total_presupuestado, ot.total_aprobado, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "c.razon_social AS cliente, " &
                    "e.codigo, e.descripcion AS estado, e.permite_edicion_detalle, e.es_estado_final " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = ot.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN cliente AS c ON c.id_cliente = ot.id_cliente " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.id_orden_trabajo = @id_orden_trabajo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If lector.Read() Then
                            encontrada = True

                            'datos de cabecera, solo lectura
                            lblNroOrden.Text = "N.º " & lector("nro_orden").ToString()
                            lblEstado.Text = lector("estado").ToString()
                            lblVehiculo.Text = lector("patente").ToString() & " - " & lector("vehiculo").ToString()
                            lblCliente.Text = lector("cliente").ToString()
                            lblFechaRecepcion.Text = CDate(lector("fecha_recepcion")).ToString("dd/MM/yyyy HH:mm")
                            lblKmIngreso.Text = CInt(lector("km_ingreso")).ToString("N0")
                            txtSintoma.Text = lector("sintoma_reportado").ToString()
                            txtObsRecepcion.Text = lector("observaciones_recepcion").ToString()

                            'la fecha prometida es opcional
                            If IsDBNull(lector("fecha_prometida")) Then
                                lblFechaPrometida.Text = "Sin fecha"
                            Else
                                lblFechaPrometida.Text = CDate(lector("fecha_prometida")).ToString("dd/MM/yyyy")
                            End If

                            'la orden puede no tener mecanico asignado
                            If Not IsDBNull(lector("id_mecanico")) Then idMecanico = CInt(lector("id_mecanico"))

                            'totales guardados en la cabecera de la orden
                            lblTotalPresupuestado.Text = "Total presupuestado: " & CDec(lector("total_presupuestado")).ToString("C2")
                            lblTotalAprobado.Text = "Total aprobado: " & CDec(lector("total_aprobado")).ToString("C2")

                            'datos del estado que deciden que se puede hacer
                            codigoEstado = lector("codigo").ToString()
                            permiteEdicion = Convert.ToBoolean(lector("permite_edicion_detalle"))
                            esEstadoFinal = Convert.ToBoolean(lector("es_estado_final"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar la orden de trabajo: " & ex.Message)
        End Try

        If encontrada Then
            'marco en el combo al mecanico de la orden, la clave 0 es "(sin asignar)"
            If cboMecanico.DataSource IsNot Nothing Then cboMecanico.SelectedValue = idMecanico

            CargarDetalle()
            CargarHistorial()
        End If

        'el mecanico se puede cambiar mientras el estado no sea final
        cboMecanico.Enabled = Not esEstadoFinal
        btnGuardarMecanico.Enabled = Not esEstadoFinal

        'el presupuesto se edita solo en los estados que lo permiten
        cboServicio.Enabled = permiteEdicion
        nudCantidad.Enabled = permiteEdicion
        btnAgregar.Enabled = permiteEdicion
        btnActualizar.Enabled = permiteEdicion
        btnQuitar.Enabled = permiteEdicion

        'cada boton de cambio de estado se habilita solo en su estado de origen
        btnPresupuestar.Enabled = (codigoEstado = "RECEPCIONADA")
    End Sub

    Function PermiteEditarDetalle(cn As MySqlConnection, transaccion As MySqlTransaction) As Boolean
        'vuelvo a leer el estado de la orden dentro de la transaccion: otro puesto pudo cambiarlo
        'FOR UPDATE bloquea la orden hasta terminar, asi nadie le cambia el estado en el medio
        Dim consulta As String =
            "SELECT e.permite_edicion_detalle " &
            "FROM orden_trabajo AS ot " &
            "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
            "WHERE ot.id_orden_trabajo = @id_orden_trabajo FOR UPDATE;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
            Dim resultado As Object = cmd.ExecuteScalar()
            If resultado Is Nothing Then Return False
            Return Convert.ToBoolean(resultado)
        End Using
    End Function

    Sub RecalcularTotales(cn As MySqlConnection, transaccion As MySqlTransaction)
        'recalculo los dos totales de la cabecera a partir de las lineas
        'van en un solo UPDATE porque la tabla exige total_aprobado <= total_presupuestado
        Dim consulta As String =
            "UPDATE orden_trabajo SET " &
            "total_presupuestado = (SELECT COALESCE(SUM(d.subtotal), 0) FROM ot_detalle AS d " &
            "WHERE d.id_orden_trabajo = @id_orden_trabajo), " &
            "total_aprobado = (SELECT COALESCE(SUM(d.subtotal), 0) FROM ot_detalle AS d " &
            "WHERE d.id_orden_trabajo = @id_orden_trabajo AND d.aprobado = 1) " &
            "WHERE id_orden_trabajo = @id_orden_trabajo;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub FrmOrdenGestion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'solo el administrador y el operador gestionan las ordenes
        If Sesion.Rol <> "ADMINISTRADOR" AndAlso Sesion.Rol <> "OPERADOR" Then
            MessageBox.Show("Solo el administrador y el operador pueden gestionar las órdenes de trabajo.")
            Me.Close()
            Exit Sub
        End If

        'el tablero tiene que indicar la orden antes de abrir el formulario
        If IdOrdenTrabajo = 0 Then
            MessageBox.Show("No se indicó la orden de trabajo a gestionar.")
            Me.Close()
            Exit Sub
        End If

        'cargo los combos y despues la orden, que marca su mecanico en el combo
        CargarComboMecanicos()
        CargarComboServicios()
        CargarOrden()

        'sin estado leido la orden no existe o no se pudo cargar
        If codigoEstado = "" Then
            MessageBox.Show("No se pudo abrir la orden de trabajo seleccionada.")
            Me.Close()
        End If
    End Sub

    Private Sub dgvDetalle_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDetalle.DataBindingComplete
        'la grilla selecciona sola la primera fila, la desmarco
        dgvDetalle.ClearSelection()

        'si se reordeno la grilla, vuelvo a marcar la linea elegida
        If idDetalleSeleccionado = 0 Then Exit Sub
        For Each fila As DataGridViewRow In dgvDetalle.Rows
            If CInt(fila.Cells("id_ot_detalle").Value) = idDetalleSeleccionado Then
                fila.Selected = True
            End If
        Next
    End Sub

    Private Sub dgvHistorial_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvHistorial.DataBindingComplete
        'el historial es solo de consulta, no dejo ninguna fila marcada
        dgvHistorial.ClearSelection()
    End Sub

    Private Sub dgvDetalle_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalle.CellClick
        'recuerdo la linea elegida y traigo su cantidad para poder cambiarla
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvDetalle.Rows(e.RowIndex)
        idDetalleSeleccionado = CInt(fila.Cells("id_ot_detalle").Value)

        Dim cantidad As Decimal = CDec(fila.Cells("cantidad").Value)
        If cantidad <= nudCantidad.Maximum Then nudCantidad.Value = cantidad
    End Sub

    Private Sub btnGuardarMecanico_Click(sender As Object, e As EventArgs) Handles btnGuardarMecanico.Click
        'guardo el mecanico elegido en la orden

        'la clave 0 es "(sin asignar)" y se guarda como NULL
        Dim idMecanico As Object = DBNull.Value
        If TypeOf cboMecanico.SelectedValue Is Integer AndAlso CInt(cboMecanico.SelectedValue) > 0 Then
            idMecanico = CInt(cboMecanico.SelectedValue)
        End If

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el estado: en un estado final ya no se cambia el mecanico
                    Dim esFinal As Boolean = True
                    Dim consulta As String =
                        "SELECT e.es_estado_final " &
                        "FROM orden_trabajo AS ot " &
                        "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                        "WHERE ot.id_orden_trabajo = @id_orden_trabajo FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then esFinal = Convert.ToBoolean(resultado)
                    End Using

                    If esFinal Then
                        aviso = "La orden ya está en un estado final y no se puede cambiar su mecánico."
                    End If

                    If aviso = "" Then
                        consulta = "UPDATE orden_trabajo SET id_mecanico = @id_mecanico " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_mecanico", idMecanico)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
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
            MessageBox.Show("No se pudo guardar el mecánico: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Mecánico guardado.")
        End If

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        'agrego un servicio al presupuesto de la orden

        'la clave 0 es "(seleccione un servicio)"
        If Not TypeOf cboServicio.SelectedValue Is Integer OrElse CInt(cboServicio.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione un servicio")
            cboServicio.Focus()
            Exit Sub
        End If
        Dim idServicio As Integer = CInt(cboServicio.SelectedValue)

        'la cantidad admite dos decimales y tiene que ser mayor a cero
        Dim cantidad As Decimal = Math.Round(nudCantidad.Value, 2, MidpointRounding.AwayFromZero)
        If cantidad <= 0 Then
            MessageBox.Show("La cantidad debe ser mayor a cero")
            nudCantidad.Focus()
            Exit Sub
        End If

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la linea y los totales de la orden se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If Not PermiteEditarDetalle(cn, transaccion) Then
                        aviso = "El estado actual de la orden ya no permite modificar el presupuesto."
                    End If

                    Dim consulta As String

                    'un servicio va una sola vez por orden
                    If aviso = "" Then
                        consulta = "SELECT COUNT(*) FROM ot_detalle " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo AND id_servicio = @id_servicio;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.Parameters.AddWithValue("@id_servicio", idServicio)
                            If CInt(cmd.ExecuteScalar()) > 0 Then
                                aviso = "Ese servicio ya está en el presupuesto. " &
                                        "Seleccione la línea y use ""Actualizar cantidad""."
                            End If
                        End Using
                    End If

                    'copio la descripcion y el precio vigentes del servicio
                    Dim descripcion As String = ""
                    Dim precio As Decimal = 0D
                    If aviso = "" Then
                        consulta = "SELECT descripcion, precio FROM servicio " &
                                   "WHERE id_servicio = @id_servicio AND activo = 1;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_servicio", idServicio)
                            Using lector As MySqlDataReader = cmd.ExecuteReader
                                If lector.Read() Then
                                    descripcion = lector("descripcion").ToString()
                                    precio = CDec(lector("precio"))
                                Else
                                    aviso = "El servicio elegido ya no está activo."
                                End If
                            End Using
                        End Using
                    End If

                    If aviso = "" Then
                        Dim subtotal As Decimal = Math.Round(cantidad * precio, 2, MidpointRounding.AwayFromZero)

                        consulta =
                            "INSERT INTO ot_detalle (id_orden_trabajo, id_servicio, descripcion, cantidad, " &
                            "precio_unitario, subtotal) " &
                            "VALUES (@id_orden_trabajo, @id_servicio, @descripcion, @cantidad, " &
                            "@precio_unitario, @subtotal);"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.Parameters.AddWithValue("@id_servicio", idServicio)
                            cmd.Parameters.AddWithValue("@descripcion", descripcion)
                            cmd.Parameters.AddWithValue("@cantidad", cantidad)
                            cmd.Parameters.AddWithValue("@precio_unitario", precio)
                            cmd.Parameters.AddWithValue("@subtotal", subtotal)
                            cmd.ExecuteNonQuery()
                        End Using

                        RecalcularTotales(cn, transaccion)
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
            MessageBox.Show("No se pudo agregar el servicio y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then MessageBox.Show(aviso)

        'dejo listo el alta de otro servicio solo si este se guardo
        If aviso = "" Then
            cboServicio.SelectedIndex = 0
            nudCantidad.Value = 1
        End If

        'vuelvo a cargar la orden con sus lineas y totales actuales
        CargarOrden()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        'cambio la cantidad de la linea seleccionada
        If idDetalleSeleccionado = 0 Then
            MessageBox.Show("Debe seleccionar una línea del presupuesto para cambiar su cantidad")
            Exit Sub
        End If

        'la cantidad admite dos decimales y tiene que ser mayor a cero
        Dim cantidad As Decimal = Math.Round(nudCantidad.Value, 2, MidpointRounding.AwayFromZero)
        If cantidad <= 0 Then
            MessageBox.Show("La cantidad debe ser mayor a cero")
            nudCantidad.Focus()
            Exit Sub
        End If

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la linea y los totales de la orden se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If Not PermiteEditarDetalle(cn, transaccion) Then
                        aviso = "El estado actual de la orden ya no permite modificar el presupuesto."
                    End If

                    If aviso = "" Then
                        'el subtotal se recalcula con el precio que quedo guardado en la linea
                        Dim consulta As String =
                            "UPDATE ot_detalle SET cantidad = @cantidad, " &
                            "subtotal = ROUND(@cantidad * precio_unitario, 2) " &
                            "WHERE id_ot_detalle = @id_ot_detalle AND id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@cantidad", cantidad)
                            cmd.Parameters.AddWithValue("@id_ot_detalle", idDetalleSeleccionado)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.ExecuteNonQuery()
                        End Using

                        RecalcularTotales(cn, transaccion)
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
            MessageBox.Show("No se pudo cambiar la cantidad y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then MessageBox.Show(aviso)

        'vuelvo a cargar la orden con sus lineas y totales actuales
        CargarOrden()
    End Sub

    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        'quito del presupuesto la linea seleccionada
        If idDetalleSeleccionado = 0 Then
            MessageBox.Show("Debe seleccionar una línea del presupuesto para quitarla")
            Exit Sub
        End If

        'pido confirmacion antes de quitar
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Quitar del presupuesto la línea seleccionada?",
            "Quitar línea",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la linea y los totales de la orden se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If Not PermiteEditarDetalle(cn, transaccion) Then
                        aviso = "El estado actual de la orden ya no permite modificar el presupuesto."
                    End If

                    If aviso = "" Then
                        Dim consulta As String =
                            "DELETE FROM ot_detalle " &
                            "WHERE id_ot_detalle = @id_ot_detalle AND id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_ot_detalle", idDetalleSeleccionado)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.ExecuteNonQuery()
                        End Using

                        RecalcularTotales(cn, transaccion)
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
            MessageBox.Show("No se pudo quitar la línea y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then MessageBox.Show(aviso)

        'vuelvo a cargar la orden con sus lineas y totales actuales
        CargarOrden()
    End Sub

    Private Sub btnPresupuestar_Click(sender As Object, e As EventArgs) Handles btnPresupuestar.Click
        'paso la orden de RECEPCIONADA a PRESUPUESTADA

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'el cambio de estado y su fila de historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el estado de la orden: otro puesto pudo cambiarlo
                    Dim codigoActual As String = ""
                    Dim consulta As String =
                        "SELECT e.codigo " &
                        "FROM orden_trabajo AS ot " &
                        "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                        "WHERE ot.id_orden_trabajo = @id_orden_trabajo FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then codigoActual = resultado.ToString()
                    End Using

                    If codigoActual <> "RECEPCIONADA" Then
                        aviso = "La orden ya no está recepcionada, no se puede presupuestar."
                    End If

                    'no se presupuesta una orden sin lineas
                    If aviso = "" Then
                        consulta = "SELECT COUNT(*) FROM ot_detalle WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            If CInt(cmd.ExecuteScalar()) = 0 Then
                                aviso = "Agregue al menos un servicio antes de presupuestar la orden."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'busco la clave del estado PRESUPUESTADA por su codigo
                        Dim idEstado As Integer
                        consulta = "SELECT id_estado_ot FROM estado_ot WHERE codigo = 'PRESUPUESTADA';"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            Dim resultado As Object = cmd.ExecuteScalar()
                            If resultado Is Nothing Then
                                Throw New Exception("no existe el estado PRESUPUESTADA en la tabla estado_ot.")
                            End If
                            idEstado = CInt(resultado)
                        End Using

                        'cambio el estado de la orden
                        consulta = "UPDATE orden_trabajo SET id_estado_ot = @id_estado_ot " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.ExecuteNonQuery()
                        End Using

                        'guardo la fila del historial de estados
                        consulta =
                            "INSERT INTO ot_historial_estado (id_orden_trabajo, id_estado_ot, id_usuario, observacion) " &
                            "VALUES (@id_orden_trabajo, @id_estado_ot, @id_usuario, @observacion);"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
                            cmd.Parameters.AddWithValue("@id_usuario", Sesion.IdUsuario)
                            cmd.Parameters.AddWithValue("@observacion", "Presupuesto cargado")
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
            MessageBox.Show("No se pudo presupuestar la orden y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Orden presupuestada.")
        End If

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

End Class

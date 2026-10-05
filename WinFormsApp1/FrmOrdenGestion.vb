Imports MySqlConnector

Public Class FrmOrdenGestion

    'orden que se gestiona, la carga el tablero antes de abrir el formulario
    Public IdOrdenTrabajo As Integer = 0

    'datos del estado actual de la orden, se vuelven a leer en cada carga
    Private codigoEstado As String = ""
    Private permiteEdicion As Boolean = False
    Private esEstadoFinal As Boolean = True

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
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_ot_detalle, descripcion, cantidad, precio_unitario, subtotal, aprobado, " &
                    "cantidad_real, horas_reales " &
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
                    dgvDetalle.Columns("precio_unitario").HeaderText = "Precio"
                    dgvDetalle.Columns("precio_unitario").DefaultCellStyle.Format = "C2"
                    dgvDetalle.Columns("subtotal").HeaderText = "Subtotal"
                    dgvDetalle.Columns("subtotal").DefaultCellStyle.Format = "C2"
                    dgvDetalle.Columns("aprobado").HeaderText = "Aprobado"
                    dgvDetalle.Columns("cantidad_real").HeaderText = "Cant. real"
                    dgvDetalle.Columns("cantidad_real").DefaultCellStyle.Format = "N2"
                    dgvDetalle.Columns("horas_reales").HeaderText = "Horas"
                    dgvDetalle.Columns("horas_reales").DefaultCellStyle.Format = "N2"

                    'los numeros van alineados a la derecha
                    dgvDetalle.Columns("cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvDetalle.Columns("precio_unitario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvDetalle.Columns("subtotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvDetalle.Columns("cantidad_real").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvDetalle.Columns("horas_reales").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    'las columnas de datos cortos se ajustan al contenido, la descripcion ocupa el resto
                    dgvDetalle.Columns("cantidad").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvDetalle.Columns("precio_unitario").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvDetalle.Columns("subtotal").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvDetalle.Columns("aprobado").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvDetalle.Columns("cantidad_real").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvDetalle.Columns("horas_reales").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                    'la aprobacion se marca linea por linea solo con la orden presupuestada,
                    'el resto de las columnas nunca se edita en la grilla
                    dgvDetalle.ReadOnly = (codigoEstado <> "PRESUPUESTADA")
                    If codigoEstado = "PRESUPUESTADA" Then
                        For Each columna As DataGridViewColumn In dgvDetalle.Columns
                            columna.ReadOnly = (columna.Name <> "aprobado")
                        Next
                    End If
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

                    'el historial queda siempre en el orden de la consulta: no se reordena con un click en el titulo
                    For Each columna As DataGridViewColumn In dgvHistorial.Columns
                        columna.SortMode = DataGridViewColumnSortMode.NotSortable
                    Next
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
        Dim notasGuardadas As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'el cliente es el de la orden (titular al recepcionar), no el titular actual del vehiculo
                Dim consulta As String =
                    "SELECT ot.nro_orden, ot.fecha_recepcion, ot.fecha_prometida, ot.km_ingreso, " &
                    "ot.fecha_finalizacion, ot.fecha_entrega, " &
                    "ot.sintoma_reportado, ot.observaciones_recepcion, ot.observaciones_mecanico, " &
                    "ot.id_mecanico, ot.total_presupuestado, ot.total_aprobado, v.patente, " &
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

                            'las fechas de cierre y de entrega existen recien cuando la orden llega a esos estados
                            If IsDBNull(lector("fecha_finalizacion")) Then
                                lblFechaFinalizacion.Text = "-"
                            Else
                                lblFechaFinalizacion.Text = CDate(lector("fecha_finalizacion")).ToString("dd/MM/yyyy HH:mm")
                            End If
                            If IsDBNull(lector("fecha_entrega")) Then
                                lblFechaEntrega.Text = "-"
                            Else
                                lblFechaEntrega.Text = CDate(lector("fecha_entrega")).ToString("dd/MM/yyyy HH:mm")
                            End If

                            'la orden puede no tener mecanico asignado
                            If Not IsDBNull(lector("id_mecanico")) Then idMecanico = CInt(lector("id_mecanico"))

                            'observaciones del mecanico, se guardan al finalizar
                            notasGuardadas = lector("observaciones_mecanico").ToString()

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

            'con el trabajo en proceso las observaciones se estan escribiendo y todavia no se guardaron:
            'no las piso con lo que hay en la base
            If codigoEstado <> "EN_PROCESO" Then txtNotasTecnicas.Text = notasGuardadas

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

        'la ejecucion real y las observaciones del mecanico se cargan con el trabajo en proceso
        nudCantidadReal.Enabled = (codigoEstado = "EN_PROCESO")
        nudHorasReales.Enabled = (codigoEstado = "EN_PROCESO")
        btnGuardarEjecucion.Enabled = (codigoEstado = "EN_PROCESO")
        txtNotasTecnicas.ReadOnly = (codigoEstado <> "EN_PROCESO")

        'cada boton de cambio de estado se habilita solo en su estado de origen
        btnPresupuestar.Enabled = (codigoEstado = "RECEPCIONADA")
        btnAprobar.Enabled = (codigoEstado = "PRESUPUESTADA")
        btnRechazar.Enabled = (codigoEstado = "PRESUPUESTADA")
        btnIniciar.Enabled = (codigoEstado = "APROBADA")
        btnFinalizar.Enabled = (codigoEstado = "EN_PROCESO")
        btnEntregar.Enabled = (codigoEstado = "FINALIZADA")

        'la anulacion es solo del administrador y vale en cualquier estado que no sea final
        txtMotivoAnulacion.Visible = (Sesion.Rol = "ADMINISTRADOR")
        btnAnular.Visible = (Sesion.Rol = "ADMINISTRADOR")
        txtMotivoAnulacion.Enabled = Not esEstadoFinal
        btnAnular.Enabled = Not esEstadoFinal
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

    Function LeerCodigoEstado(cn As MySqlConnection, transaccion As MySqlTransaction) As String
        'vuelvo a leer el codigo del estado de la orden dentro de la transaccion: otro puesto pudo cambiarlo
        'FOR UPDATE bloquea la orden hasta terminar, asi nadie le cambia el estado en el medio
        Dim consulta As String =
            "SELECT e.codigo " &
            "FROM orden_trabajo AS ot " &
            "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
            "WHERE ot.id_orden_trabajo = @id_orden_trabajo FOR UPDATE;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
            Dim resultado As Object = cmd.ExecuteScalar()
            If resultado Is Nothing Then Return ""
            Return resultado.ToString()
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

    Sub CambiarEstado(cn As MySqlConnection, transaccion As MySqlTransaction, codigoDestino As String, observacion As String)
        'paso la orden al estado indicado y dejo su fila en el historial, dentro de la transaccion del boton

        'busco la clave del estado por su codigo
        Dim idEstado As Integer
        Dim consulta As String = "SELECT id_estado_ot FROM estado_ot WHERE codigo = @codigo;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@codigo", codigoDestino)
            Dim resultado As Object = cmd.ExecuteScalar()
            If resultado Is Nothing Then
                Throw New Exception("no existe el estado " & codigoDestino & " en la tabla estado_ot.")
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
            cmd.Parameters.AddWithValue("@observacion", observacion)
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
        'al cargar o reordenar la grilla no dejo ninguna linea marcada: hay que elegirla
        'al tildar una aprobacion la grilla tambien pasa por aca, ahi la seleccion no se toca
        If e.ListChangedType = System.ComponentModel.ListChangedType.Reset Then
            dgvDetalle.ClearSelection()
        End If
    End Sub

    Private Sub dgvHistorial_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvHistorial.DataBindingComplete
        'el historial es solo de consulta, no dejo ninguna fila marcada
        dgvHistorial.ClearSelection()

        'dejo a la vista la primera fila, que es el cambio de estado mas nuevo
        If dgvHistorial.Rows.Count > 0 AndAlso dgvHistorial.DisplayedRowCount(True) > 0 Then
            dgvHistorial.FirstDisplayedScrollingRowIndex = 0
        End If
    End Sub

    Private Sub dgvDetalle_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDetalle.SelectionChanged
        'traigo los valores de la linea elegida con el mouse o con el teclado para poder cambiarlos
        'al recargar la grilla el foco esta en otro control y los campos no se tocan
        If Not dgvDetalle.Focused Then Exit Sub
        If dgvDetalle.SelectedRows.Count = 0 Then Exit Sub

        Dim fila As DataGridViewRow = dgvDetalle.SelectedRows(0)

        Dim cantidad As Decimal = CDec(fila.Cells("cantidad").Value)
        If cantidad <= nudCantidad.Maximum Then nudCantidad.Value = cantidad

        'si la linea todavia no tiene cantidad real propongo la presupuestada
        Dim cantidadReal As Decimal = cantidad
        If Not IsDBNull(fila.Cells("cantidad_real").Value) Then cantidadReal = CDec(fila.Cells("cantidad_real").Value)
        If cantidadReal <= nudCantidadReal.Maximum Then nudCantidadReal.Value = cantidadReal

        Dim horasReales As Decimal = 0D
        If Not IsDBNull(fila.Cells("horas_reales").Value) Then horasReales = CDec(fila.Cells("horas_reales").Value)
        If horasReales <= nudHorasReales.Maximum Then nudHorasReales.Value = horasReales
    End Sub

    Private Sub btnGuardarMecanico_Click(sender As Object, e As EventArgs) Handles btnGuardarMecanico.Click
        'guardo el mecanico elegido en la orden

        'si el combo no se pudo cargar no hay nada confiable para guardar
        If cboMecanico.DataSource Is Nothing OrElse cboMecanico.SelectedValue Is Nothing Then
            MessageBox.Show("No se pudo leer el mecánico elegido. Cierre la ventana y vuelva a abrir la orden.")
            Exit Sub
        End If

        'la clave 0 es "(sin asignar)" y se guarda como NULL
        Dim idElegido As Integer = Convert.ToInt32(cboMecanico.SelectedValue)
        Dim idMecanico As Object = DBNull.Value
        If idElegido > 0 Then idMecanico = idElegido

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el estado: otro puesto pudo cambiarlo
                    Dim codigoActual As String = ""
                    Dim esFinal As Boolean = True
                    Dim consulta As String =
                        "SELECT e.codigo, e.es_estado_final " &
                        "FROM orden_trabajo AS ot " &
                        "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                        "WHERE ot.id_orden_trabajo = @id_orden_trabajo FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                        Using lector As MySqlDataReader = cmd.ExecuteReader
                            If lector.Read() Then
                                codigoActual = lector("codigo").ToString()
                                esFinal = Convert.ToBoolean(lector("es_estado_final"))
                            End If
                        End Using
                    End Using

                    'en un estado final ya no se cambia el mecanico
                    If esFinal Then
                        aviso = "La orden ya está en un estado final y no se puede cambiar su mecánico."
                    End If

                    'desde que el trabajo se inicia la orden no puede quedar sin mecanico
                    If aviso = "" AndAlso idElegido = 0 Then
                        If codigoActual = "EN_PROCESO" OrElse codigoActual = "FINALIZADA" Then
                            aviso = "El trabajo ya se inició: la orden no puede quedar sin mecánico."
                        End If
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
        If cboServicio.SelectedValue Is Nothing OrElse Convert.ToInt32(cboServicio.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione un servicio")
            cboServicio.Focus()
            Exit Sub
        End If
        Dim idServicio As Integer = Convert.ToInt32(cboServicio.SelectedValue)

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

        'tomo la linea marcada en la grilla en este momento
        If dgvDetalle.SelectedRows.Count = 0 Then
            MessageBox.Show("Debe seleccionar una línea del presupuesto para cambiar su cantidad")
            Exit Sub
        End If
        Dim idDetalle As Integer = CInt(dgvDetalle.SelectedRows(0).Cells("id_ot_detalle").Value)

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
                            cmd.Parameters.AddWithValue("@id_ot_detalle", idDetalle)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            'si no se actualizo ninguna fila, otro puesto quito la linea
                            If cmd.ExecuteNonQuery() = 0 Then
                                aviso = "La línea seleccionada ya no existe en el presupuesto."
                            End If
                        End Using
                    End If

                    If aviso = "" Then RecalcularTotales(cn, transaccion)

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

        'tomo la linea marcada en la grilla en este momento
        If dgvDetalle.SelectedRows.Count = 0 Then
            MessageBox.Show("Debe seleccionar una línea del presupuesto para quitarla")
            Exit Sub
        End If
        Dim fila As DataGridViewRow = dgvDetalle.SelectedRows(0)
        Dim idDetalle As Integer = CInt(fila.Cells("id_ot_detalle").Value)

        'pido confirmacion antes de quitar
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Quitar del presupuesto la línea """ & fila.Cells("descripcion").Value.ToString() & """?",
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
                            cmd.Parameters.AddWithValue("@id_ot_detalle", idDetalle)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            'si no se borro ninguna fila, otro puesto ya habia quitado la linea
                            If cmd.ExecuteNonQuery() = 0 Then
                                aviso = "La línea seleccionada ya no existe en el presupuesto."
                            End If
                        End Using
                    End If

                    If aviso = "" Then RecalcularTotales(cn, transaccion)

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
                    If LeerCodigoEstado(cn, transaccion) <> "RECEPCIONADA" Then
                        aviso = "La orden ya no está recepcionada, no se puede presupuestar."
                    End If

                    'no se presupuesta una orden sin lineas
                    If aviso = "" Then
                        Dim consulta As String =
                            "SELECT COUNT(*) FROM ot_detalle WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            If CInt(cmd.ExecuteScalar()) = 0 Then
                                aviso = "Agregue al menos un servicio antes de presupuestar la orden."
                            End If
                        End Using
                    End If

                    If aviso = "" Then CambiarEstado(cn, transaccion, "PRESUPUESTADA", "Presupuesto cargado")

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

    Private Sub btnAprobar_Click(sender As Object, e As EventArgs) Handles btnAprobar.Click
        'registro la aprobacion del cliente: paso la orden de PRESUPUESTADA a APROBADA
        'con las lineas que quedaron tildadas en la grilla

        'cierro la edicion de la celda para que la ultima tilde quede en la grilla
        dgvDetalle.EndEdit()

        'cuento las lineas aprobadas y sumo su importe
        Dim lineasAprobadas As Integer = 0
        Dim totalAprobado As Decimal = 0D
        For Each fila As DataGridViewRow In dgvDetalle.Rows
            If Convert.ToBoolean(fila.Cells("aprobado").Value) Then
                lineasAprobadas = lineasAprobadas + 1
                totalAprobado = totalAprobado + CDec(fila.Cells("subtotal").Value)
            End If
        Next

        If lineasAprobadas = 0 Then
            MessageBox.Show("Marque como aprobada al menos una línea del presupuesto." & vbCrLf &
                            "Si el cliente no aprueba ninguna, use ""Rechazar"".")
            Exit Sub
        End If

        'pido confirmacion mostrando lo que aprueba el cliente
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Registrar la aprobación del cliente?" & vbCrLf &
            "Líneas aprobadas: " & lineasAprobadas & " de " & dgvDetalle.Rows.Count & vbCrLf &
            "Total aprobado: " & totalAprobado.ToString("C2"),
            "Registrar aprobación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'las aprobaciones, los totales, el estado y el historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If LeerCodigoEstado(cn, transaccion) <> "PRESUPUESTADA" Then
                        aviso = "La orden ya no está presupuestada, no se puede registrar la aprobación."
                    End If

                    Dim consulta As String

                    'si otro puesto agrego o quito lineas, lo que se confirmo ya no es el presupuesto actual
                    If aviso = "" Then
                        consulta = "SELECT COUNT(*) FROM ot_detalle WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            If CInt(cmd.ExecuteScalar()) <> dgvDetalle.Rows.Count Then
                                aviso = "El presupuesto fue modificado desde otro puesto. " &
                                        "Revise las líneas y vuelva a registrar la aprobación."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'guardo la aprobacion de cada linea tal como quedo en la grilla
                        consulta = "UPDATE ot_detalle SET aprobado = @aprobado " &
                                   "WHERE id_ot_detalle = @id_ot_detalle AND id_orden_trabajo = @id_orden_trabajo;"
                        For Each fila As DataGridViewRow In dgvDetalle.Rows
                            Using cmd As New MySqlCommand(consulta, cn, transaccion)
                                cmd.Parameters.AddWithValue("@aprobado", Convert.ToBoolean(fila.Cells("aprobado").Value))
                                cmd.Parameters.AddWithValue("@id_ot_detalle", CInt(fila.Cells("id_ot_detalle").Value))
                                cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        'compruebo en la base que quedo al menos una linea aprobada
                        consulta = "SELECT COUNT(*) FROM ot_detalle " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo AND aprobado = 1;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            If CInt(cmd.ExecuteScalar()) = 0 Then
                                aviso = "No quedó ninguna línea aprobada. Si el cliente no aprueba ninguna, use ""Rechazar""."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        RecalcularTotales(cn, transaccion)
                        CambiarEstado(cn, transaccion, "APROBADA", "Aprobación del cliente registrada")
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
            MessageBox.Show("No se pudo registrar la aprobación y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Aprobación registrada.")
        End If

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

    Private Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        'el cliente no aprueba el presupuesto: paso la orden de PRESUPUESTADA a RECHAZADA

        'pido confirmacion, la orden queda cerrada
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Registrar que el cliente rechazó el presupuesto? La orden queda cerrada.",
            "Rechazar presupuesto",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'las lineas, los totales, el estado y el historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If LeerCodigoEstado(cn, transaccion) <> "PRESUPUESTADA" Then
                        aviso = "La orden ya no está presupuestada, no se puede rechazar."
                    End If

                    If aviso = "" Then
                        'ninguna linea queda aprobada, las lineas no se borran
                        Dim consulta As String =
                            "UPDATE ot_detalle SET aprobado = 0 WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.ExecuteNonQuery()
                        End Using

                        RecalcularTotales(cn, transaccion)
                        CambiarEstado(cn, transaccion, "RECHAZADA", "Presupuesto rechazado por el cliente")
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
            MessageBox.Show("No se pudo rechazar el presupuesto y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Presupuesto rechazado.")
        End If

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

    Private Sub btnIniciar_Click(sender As Object, e As EventArgs) Handles btnIniciar.Click
        'paso la orden de APROBADA a EN_PROCESO

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'el cambio de estado y su fila de historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If LeerCodigoEstado(cn, transaccion) <> "APROBADA" Then
                        aviso = "La orden ya no está aprobada, no se puede iniciar el trabajo."
                    End If

                    'el trabajo no se inicia sin un mecanico asignado
                    If aviso = "" Then
                        Dim consulta As String =
                            "SELECT id_mecanico FROM orden_trabajo WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            Dim resultado As Object = cmd.ExecuteScalar()
                            If resultado Is Nothing OrElse IsDBNull(resultado) Then
                                aviso = "Asigne un mecánico a la orden y presione ""Guardar mecánico"" antes de iniciar el trabajo."
                            End If
                        End Using
                    End If

                    If aviso = "" Then CambiarEstado(cn, transaccion, "EN_PROCESO", "Inicio del trabajo")

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
            MessageBox.Show("No se pudo iniciar el trabajo y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Trabajo iniciado.")
        End If

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

    Private Sub btnGuardarEjecucion_Click(sender As Object, e As EventArgs) Handles btnGuardarEjecucion.Click
        'guardo la cantidad real y las horas reales de la linea seleccionada
        'no cambia el estado de la orden ni deja fila en el historial

        'tomo la linea marcada en la grilla en este momento
        If dgvDetalle.SelectedRows.Count = 0 Then
            MessageBox.Show("Debe seleccionar una línea del presupuesto para cargar su ejecución")
            Exit Sub
        End If
        Dim fila As DataGridViewRow = dgvDetalle.SelectedRows(0)
        Dim idDetalle As Integer = CInt(fila.Cells("id_ot_detalle").Value)

        'solo se ejecutan las lineas que aprobo el cliente
        If Not Convert.ToBoolean(fila.Cells("aprobado").Value) Then
            MessageBox.Show("La línea seleccionada no fue aprobada por el cliente, no se ejecuta.")
            Exit Sub
        End If

        'los dos valores admiten dos decimales y no pueden ser negativos
        Dim cantidadReal As Decimal = Math.Round(nudCantidadReal.Value, 2, MidpointRounding.AwayFromZero)
        Dim horasReales As Decimal = Math.Round(nudHorasReales.Value, 2, MidpointRounding.AwayFromZero)
        If cantidadReal < 0 Then
            MessageBox.Show("La cantidad real no puede ser negativa")
            nudCantidadReal.Focus()
            Exit Sub
        End If
        If horasReales < 0 OrElse horasReales > 999.99D Then
            MessageBox.Show("Las horas reales deben estar entre 0 y 999,99")
            nudHorasReales.Focus()
            Exit Sub
        End If

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If LeerCodigoEstado(cn, transaccion) <> "EN_PROCESO" Then
                        aviso = "La orden ya no está en proceso, no se puede cargar la ejecución."
                    End If

                    If aviso = "" Then
                        Dim consulta As String =
                            "UPDATE ot_detalle SET cantidad_real = @cantidad_real, horas_reales = @horas_reales " &
                            "WHERE id_ot_detalle = @id_ot_detalle AND id_orden_trabajo = @id_orden_trabajo " &
                            "AND aprobado = 1;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@cantidad_real", cantidadReal)
                            cmd.Parameters.AddWithValue("@horas_reales", horasReales)
                            cmd.Parameters.AddWithValue("@id_ot_detalle", idDetalle)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            If cmd.ExecuteNonQuery() = 0 Then
                                aviso = "La línea seleccionada ya no existe o no está aprobada."
                            End If
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
            MessageBox.Show("No se pudo guardar la ejecución y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then MessageBox.Show(aviso)

        'vuelvo a cargar la orden con sus lineas actuales
        CargarOrden()
    End Sub

    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        'cierre tecnico: paso la orden de EN_PROCESO a FINALIZADA

        'las observaciones del mecanico son obligatorias para cerrar el trabajo
        Dim notas As String = txtNotasTecnicas.Text.Trim
        If notas = "" Then
            MessageBox.Show("Faltan las observaciones del mecánico")
            txtNotasTecnicas.Focus()
            Exit Sub
        End If

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'las observaciones, la fecha, el estado y el historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If LeerCodigoEstado(cn, transaccion) <> "EN_PROCESO" Then
                        aviso = "La orden ya no está en proceso, no se puede finalizar."
                    End If

                    Dim consulta As String

                    'toda linea aprobada tiene que tener cargada su ejecucion real
                    If aviso = "" Then
                        consulta = "SELECT COUNT(*) FROM ot_detalle " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo AND aprobado = 1 " &
                                   "AND (cantidad_real IS NULL OR horas_reales IS NULL);"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            Dim faltantes As Integer = CInt(cmd.ExecuteScalar())
                            If faltantes > 0 Then
                                aviso = "Falta cargar la cantidad real y las horas reales en " & faltantes &
                                        " línea(s) aprobada(s). Seleccione cada una y use ""Guardar ejecución""."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'la fecha de finalizacion es la fecha y hora del servidor
                        consulta = "UPDATE orden_trabajo SET observaciones_mecanico = @observaciones_mecanico, " &
                                   "fecha_finalizacion = NOW() " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@observaciones_mecanico", notas)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.ExecuteNonQuery()
                        End Using

                        CambiarEstado(cn, transaccion, "FINALIZADA", "Trabajo finalizado")
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
            MessageBox.Show("No se pudo finalizar la orden y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Orden finalizada.")
        End If

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

    Private Sub btnEntregar_Click(sender As Object, e As EventArgs) Handles btnEntregar.Click
        'entrego el vehiculo: paso la orden de FINALIZADA a ENTREGADA

        'pido confirmacion, la orden queda cerrada
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Registrar la entrega del vehículo al cliente? La orden queda cerrada.",
            "Entregar vehículo",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la fecha, el kilometraje del vehiculo, el estado y el historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If LeerCodigoEstado(cn, transaccion) <> "FINALIZADA" Then
                        aviso = "La orden ya no está finalizada, no se puede entregar."
                    End If

                    If aviso = "" Then
                        'leo el vehiculo de la orden y el kilometraje con el que ingreso
                        Dim idVehiculo As Integer
                        Dim kmIngreso As Integer
                        Dim consulta As String =
                            "SELECT id_vehiculo, km_ingreso FROM orden_trabajo " &
                            "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            Using lector As MySqlDataReader = cmd.ExecuteReader
                                If Not lector.Read() Then
                                    Throw New Exception("no se encontró la orden de trabajo.")
                                End If
                                idVehiculo = CInt(lector("id_vehiculo"))
                                kmIngreso = CInt(lector("km_ingreso"))
                            End Using
                        End Using

                        'la fecha de entrega es la fecha y hora del servidor
                        consulta = "UPDATE orden_trabajo SET fecha_entrega = NOW() " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.ExecuteNonQuery()
                        End Using

                        'el kilometraje del vehiculo sube al de la orden, nunca baja
                        consulta = "UPDATE vehiculo SET km_actual = @km " &
                                   "WHERE id_vehiculo = @id_vehiculo AND km_actual < @km;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@km", kmIngreso)
                            cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo)
                            cmd.ExecuteNonQuery()
                        End Using

                        CambiarEstado(cn, transaccion, "ENTREGADA", "Vehículo entregado al cliente")
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
            MessageBox.Show("No se pudo registrar la entrega y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Vehículo entregado.")
        End If

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

    Private Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        'anulo la orden desde cualquier estado que no sea final

        'el boton solo lo ve el administrador, igual vuelvo a comprobar el rol
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede anular órdenes de trabajo.")
            Exit Sub
        End If

        'el motivo es obligatorio y queda en el historial de estados
        Dim motivo As String = txtMotivoAnulacion.Text.Trim
        If motivo = "" Then
            MessageBox.Show("Falta el motivo de la anulación")
            txtMotivoAnulacion.Focus()
            Exit Sub
        End If

        'pido confirmacion, la anulacion no tiene vuelta atras
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Anular la orden de trabajo? Esta acción no se puede deshacer." & vbCrLf &
            "Motivo: " & motivo,
            "Anular orden",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'el cambio de estado y su fila de historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el estado de la orden: otro puesto pudo cerrarla
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
                        aviso = "La orden ya está en un estado final y no se puede anular."
                    End If

                    'no se borra ninguna fila: la orden, sus lineas y su historial se conservan
                    If aviso = "" Then CambiarEstado(cn, transaccion, "ANULADA", motivo)

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
            MessageBox.Show("No se pudo anular la orden y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Orden anulada.")
            txtMotivoAnulacion.Clear()
        End If

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

End Class

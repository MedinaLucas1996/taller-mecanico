Imports MySqlConnector

Public Class FrmHistorial

    'pantalla de solo consulta: ningun boton ni rutina de este formulario escribe en la base

    'vehiculo que se ve en la tarjeta de la derecha, queda en 0 mientras no haya uno (el ID no se muestra)
    Private idVehiculo As Integer = 0
    'orden elegida del vehiculo, sus servicios se ven debajo de la lista de ordenes
    Private idOrden As Integer = 0

    'el mecanico no ve importes: sus consultas nunca traen totales, precios ni subtotales
    Private esMecanico As Boolean = False

    'queda en True mientras el programa carga una grilla, para no tomarlo como una eleccion del usuario
    Private cargando As Boolean = False

    Function ColorEstado(codigo As String) As Color
        'color de fondo de cada estado, los mismos que usa el tablero de ordenes
        If codigo = "RECEPCIONADA" Then Return Color.Lavender
        If codigo = "PRESUPUESTADA" Then Return Color.LightBlue
        If codigo = "APROBADA" Then Return Color.LightSkyBlue
        If codigo = "EN_PROCESO" Then Return Color.Khaki
        If codigo = "FINALIZADA" Then Return Color.PaleTurquoise
        If codigo = "ENTREGADA" Then Return Color.LightGreen
        If codigo = "RECHAZADA" Then Return Color.LightPink
        If codigo = "ANULADA" Then Return Color.Gainsboro
        Return Color.White
    End Function

    Sub SinOrdenar(grilla As DataGridView)
        'las listas del historial no se ordenan con un click en el titulo: asi la fila elegida nunca cambia sola
        For Each columna As DataGridViewColumn In grilla.Columns
            columna.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Function MarcarFila(grilla As DataGridView, columnaId As String, columnaVisible As String, id As Integer) As DataGridViewRow
        'dejo marcada en la grilla la fila con esa clave: celda actual y seleccion juntas
        'con clave 0, o si la fila no esta listada, no queda nada marcado; devuelve la fila o Nothing
        Dim encontrada As DataGridViewRow = Nothing
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If id <> 0 AndAlso grilla.Columns.Contains(columnaId) Then
                For Each fila As DataGridViewRow In grilla.Rows
                    If CInt(fila.Cells(columnaId).Value) = id Then encontrada = fila
                Next
            End If

            grilla.ClearSelection()
            If encontrada Is Nothing Then
                grilla.CurrentCell = Nothing
            Else
                grilla.CurrentCell = encontrada.Cells(columnaVisible)
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    Sub CargarVehiculos()
        'cargo la lista de vehiculos cuya patente contiene lo escrito; tambien los dados de baja, su historial sigue valiendo
        Dim filtro As String = txtFiltro.Text.Trim

        cargando = True

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'traigo tambien los datos que se ven en la tarjeta, asi elegir un vehiculo no vuelve a consultar
                Dim consulta As String =
                    "SELECT v.id_vehiculo, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "c.razon_social AS titular, " &
                    "CASE WHEN v.activo = 1 THEN '' ELSE 'Baja' END AS estado, " &
                    "v.anio, v.color, v.km_actual, c.documento, c.telefono, v.activo " &
                    "FROM vehiculo AS v " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "WHERE v.patente LIKE @filtro " &
                    "ORDER BY v.activo DESC, v.patente;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    dgvVehiculos.DataSource = tabla

                    'en la lista se ven cuatro columnas, el resto se usa en la tarjeta
                    dgvVehiculos.Columns("id_vehiculo").Visible = False
                    dgvVehiculos.Columns("anio").Visible = False
                    dgvVehiculos.Columns("color").Visible = False
                    dgvVehiculos.Columns("km_actual").Visible = False
                    dgvVehiculos.Columns("documento").Visible = False
                    dgvVehiculos.Columns("telefono").Visible = False
                    dgvVehiculos.Columns("activo").Visible = False

                    'pongo titulos legibles en las columnas y reparto el ancho, con un minimo para cada una
                    dgvVehiculos.Columns("patente").HeaderText = "Patente"
                    dgvVehiculos.Columns("patente").FillWeight = 22
                    dgvVehiculos.Columns("patente").MinimumWidth = 65
                    dgvVehiculos.Columns("vehiculo").HeaderText = "Vehículo"
                    dgvVehiculos.Columns("vehiculo").FillWeight = 34
                    dgvVehiculos.Columns("vehiculo").MinimumWidth = 75
                    dgvVehiculos.Columns("titular").HeaderText = "Titular"
                    dgvVehiculos.Columns("titular").FillWeight = 28
                    dgvVehiculos.Columns("titular").MinimumWidth = 70
                    dgvVehiculos.Columns("estado").HeaderText = "Estado"
                    dgvVehiculos.Columns("estado").FillWeight = 16
                    dgvVehiculos.Columns("estado").MinimumWidth = 55

                    SinOrdenar(dgvVehiculos)

                    'sin vehiculo elegido, la ayuda dice si la busqueda no encontro nada
                    If tabla.Rows.Count = 0 Then
                        lblAyuda.Text = "No hay vehículos con una patente que contenga """ & filtro & """."
                    Else
                        lblAyuda.Text = "Buscá un vehículo por su patente y seleccionalo en la lista para ver su historial."
                    End If
                End Using
            End Using

            'el vehiculo que se estaba viendo queda en la tarjeta; si sigue en la lista, queda marcado
            MarcarFila(dgvVehiculos, "id_vehiculo", "patente", idVehiculo)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los vehículos: " & ex.Message)
        Finally
            'pase lo que pase, la grilla vuelve a atender al usuario
            cargando = False
        End Try
    End Sub

    Sub CargarOrdenes()
        'cargo todas las ordenes del vehiculo que se ve, en cualquier estado, la mas reciente primero
        cargando = True
        idOrden = 0

        'la grilla se muestra antes de cargarla: una grilla oculta vuelve a enlazar sus datos al mostrarse
        lblSinOrdenes.Visible = False
        tlpHistorial.Visible = True

        Dim tabla As New DataTable

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String

                If esMecanico Then
                    'el mecanico: las mismas columnas sin ningun importe
                    consulta =
                        "SELECT ot.id_orden_trabajo, ot.nro_orden, ot.fecha_recepcion, " &
                        "e.codigo AS codigo_estado, e.descripcion AS estado, ot.km_ingreso, " &
                        "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, " &
                        "ot.sintoma_reportado, ot.observaciones_mecanico "
                Else
                    'administrador y operador: tambien el total, el aprobado o, si no hubo aprobacion, el presupuestado
                    consulta =
                        "SELECT ot.id_orden_trabajo, ot.nro_orden, ot.fecha_recepcion, " &
                        "e.codigo AS codigo_estado, e.descripcion AS estado, ot.km_ingreso, " &
                        "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, " &
                        "CASE WHEN ot.total_aprobado > 0 THEN ot.total_aprobado ELSE ot.total_presupuestado END AS total, " &
                        "ot.sintoma_reportado, ot.observaciones_mecanico "
                End If

                consulta = consulta &
                    "FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "LEFT JOIN mecanico AS m ON m.id_mecanico = ot.id_mecanico " &
                    "WHERE ot.id_vehiculo = @id_vehiculo " &
                    "ORDER BY ot.fecha_recepcion DESC, ot.nro_orden DESC;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                End Using
            End Using

            dgvOrdenes.DataSource = tabla

            'en la lista se ven seis columnas (cinco para el mecanico), el resto se usa debajo
            dgvOrdenes.Columns("id_orden_trabajo").Visible = False
            dgvOrdenes.Columns("codigo_estado").Visible = False
            dgvOrdenes.Columns("sintoma_reportado").Visible = False
            dgvOrdenes.Columns("observaciones_mecanico").Visible = False

            dgvOrdenes.Columns("nro_orden").HeaderText = "N.º"
            dgvOrdenes.Columns("nro_orden").FillWeight = 10
            dgvOrdenes.Columns("nro_orden").MinimumWidth = 40
            dgvOrdenes.Columns("fecha_recepcion").HeaderText = "Ingreso"
            dgvOrdenes.Columns("fecha_recepcion").DefaultCellStyle.Format = "dd/MM/yyyy"
            dgvOrdenes.Columns("fecha_recepcion").FillWeight = 18
            dgvOrdenes.Columns("fecha_recepcion").MinimumWidth = 75
            dgvOrdenes.Columns("estado").HeaderText = "Estado"
            dgvOrdenes.Columns("estado").FillWeight = 20
            dgvOrdenes.Columns("estado").MinimumWidth = 80
            dgvOrdenes.Columns("km_ingreso").HeaderText = "Km"
            dgvOrdenes.Columns("km_ingreso").DefaultCellStyle.Format = "N0"
            dgvOrdenes.Columns("km_ingreso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvOrdenes.Columns("km_ingreso").FillWeight = 14
            dgvOrdenes.Columns("km_ingreso").MinimumWidth = 55
            dgvOrdenes.Columns("mecanico").HeaderText = "Mecánico"
            dgvOrdenes.Columns("mecanico").FillWeight = 22
            dgvOrdenes.Columns("mecanico").MinimumWidth = 75

            If Not esMecanico Then
                dgvOrdenes.Columns("total").HeaderText = "Total"
                dgvOrdenes.Columns("total").DefaultCellStyle.Format = "N2"
                dgvOrdenes.Columns("total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvOrdenes.Columns("total").FillWeight = 16
                dgvOrdenes.Columns("total").MinimumWidth = 75
            End If

            SinOrdenar(dgvOrdenes)
        Catch ex As Exception
            MessageBox.Show("Error al cargar las órdenes del vehículo: " & ex.Message)
        Finally
            cargando = False
        End Try

        If tabla.Rows.Count = 0 Then
            'vehiculo sin ordenes: en lugar de las grillas, un aviso
            tlpHistorial.Visible = False
            lblSinOrdenes.Visible = True
            Exit Sub
        End If

        'al elegir un vehiculo se ve su orden mas reciente, la primera de la lista
        Dim primera As DataGridViewRow = MarcarFila(dgvOrdenes, "id_orden_trabajo", "nro_orden", CInt(tabla.Rows(0)("id_orden_trabajo")))
        If primera IsNot Nothing Then MostrarOrden(primera)
    End Sub

    Sub CargarServicios()
        'cargo las lineas de la orden elegida: las aprobadas y las que el cliente no aprobo
        Dim tabla As New DataTable

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String

                If esMecanico Then
                    'el mecanico: sin precio ni subtotal
                    consulta =
                        "SELECT d.descripcion, d.cantidad, " &
                        "CASE WHEN d.aprobado = 1 THEN 'Sí' ELSE 'No' END AS aprobado, d.horas_reales "
                Else
                    consulta =
                        "SELECT d.descripcion, d.cantidad, " &
                        "CASE WHEN d.aprobado = 1 THEN 'Sí' ELSE 'No' END AS aprobado, d.horas_reales, d.subtotal "
                End If

                consulta = consulta &
                    "FROM ot_detalle AS d " &
                    "WHERE d.id_orden_trabajo = @id_orden_trabajo " &
                    "ORDER BY d.id_ot_detalle;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", idOrden)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                End Using
            End Using

            dgvServicios.DataSource = tabla

            dgvServicios.Columns("descripcion").HeaderText = "Servicio"
            dgvServicios.Columns("descripcion").FillWeight = 46
            dgvServicios.Columns("descripcion").MinimumWidth = 120
            dgvServicios.Columns("cantidad").HeaderText = "Cant."
            dgvServicios.Columns("cantidad").DefaultCellStyle.Format = "0.##"
            dgvServicios.Columns("cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvServicios.Columns("cantidad").FillWeight = 10
            dgvServicios.Columns("cantidad").MinimumWidth = 45
            dgvServicios.Columns("aprobado").HeaderText = "Aprobado"
            dgvServicios.Columns("aprobado").FillWeight = 14
            dgvServicios.Columns("aprobado").MinimumWidth = 70
            'las horas pueden no estar cargadas todavia: la celda queda vacia
            dgvServicios.Columns("horas_reales").HeaderText = "Horas"
            dgvServicios.Columns("horas_reales").DefaultCellStyle.Format = "0.00"
            dgvServicios.Columns("horas_reales").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvServicios.Columns("horas_reales").FillWeight = 12
            dgvServicios.Columns("horas_reales").MinimumWidth = 50

            If Not esMecanico Then
                dgvServicios.Columns("subtotal").HeaderText = "Subtotal"
                dgvServicios.Columns("subtotal").DefaultCellStyle.Format = "N2"
                dgvServicios.Columns("subtotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvServicios.Columns("subtotal").FillWeight = 18
                dgvServicios.Columns("subtotal").MinimumWidth = 75
            End If

            SinOrdenar(dgvServicios)

            'la lista de servicios es solo para leer: no dejo ninguna fila marcada
            dgvServicios.ClearSelection()
            dgvServicios.CurrentCell = Nothing
        Catch ex As Exception
            MessageBox.Show("Error al cargar los servicios de la orden: " & ex.Message)
        End Try

        If tabla.Rows.Count = 0 Then
            'una orden recien recibida todavia no tiene presupuesto
            lblServiciosTit.Text = "La orden todavía no tiene servicios cargados"
        End If
    End Sub

    Sub MostrarAyuda()
        'sin vehiculo elegido, la tarjeta de la derecha muestra solo la ayuda
        idVehiculo = 0
        idOrden = 0
        pnlVehiculo.Visible = False
        lblAyuda.Visible = True
        'sin vehiculo no hay ordenes que exportar
        btnExportar.Enabled = False
    End Sub

    Sub MostrarVehiculo(fila As DataGridViewRow)
        'cargo en la tarjeta el vehiculo de la fila, su titular actual y su historial de ordenes
        idVehiculo = CInt(fila.Cells("id_vehiculo").Value)

        lblPatente.Text = fila.Cells("patente").Value.ToString()
        lblBaja.Visible = Not Convert.ToBoolean(fila.Cells("activo").Value)

        'marca y modelo, y el año y el color cuando estan cargados
        Dim vehiculo As String = fila.Cells("vehiculo").Value.ToString()
        If Not IsDBNull(fila.Cells("anio").Value) Then vehiculo = vehiculo & " · " & fila.Cells("anio").Value.ToString()
        Dim colorVehiculo As String = fila.Cells("color").Value.ToString().Trim
        If colorVehiculo <> "" Then vehiculo = vehiculo & " · " & colorVehiculo
        lblVehiculo.Text = vehiculo

        'titular actual con su documento y, si tiene, su telefono
        Dim titular As String = fila.Cells("titular").Value.ToString() & " · " & fila.Cells("documento").Value.ToString()
        Dim telefono As String = fila.Cells("telefono").Value.ToString().Trim
        If telefono <> "" Then titular = titular & " · Tel. " & telefono
        lblTitular.Text = titular

        lblKm.Text = CInt(fila.Cells("km_actual").Value).ToString("N0") & " km actuales"

        'la tarjeta se muestra antes de cargar las grillas que tiene adentro
        lblAyuda.Visible = False
        pnlVehiculo.Visible = True
        'con un vehiculo elegido ya se pueden exportar sus ordenes
        btnExportar.Enabled = True

        CargarOrdenes()
    End Sub

    Sub MostrarOrden(fila As DataGridViewRow)
        'muestro debajo los servicios, lo que dijo el cliente y las observaciones del mecanico de la orden de la fila
        idOrden = CInt(fila.Cells("id_orden_trabajo").Value)

        lblServiciosTit.Text = "Servicios de la orden N.º " & fila.Cells("nro_orden").Value.ToString()
        txtSintoma.Text = fila.Cells("sintoma_reportado").Value.ToString()

        Dim observaciones As String = fila.Cells("observaciones_mecanico").Value.ToString().Trim
        If observaciones = "" Then observaciones = "Sin observaciones."
        txtObservaciones.Text = observaciones

        CargarServicios()
    End Sub

    Private Sub FrmHistorial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'iconos, si alguno falta ese control queda sin icono y el sistema sigue
        picBuscar.Image = LeerIcono("buscar-oscuro.png")
        picVehiculo.Image = LeerIcono("vehiculo-oscuro.png")
        picCliente.Image = LeerIcono("cliente-oscuro.png")
        picKm.Image = LeerIcono("km-oscuro.png")
        btnExportar.Image = LeerIcono("exportar.png")

        'los encabezados de las listas llevan solo una linea clara debajo, como las filas
        dgvVehiculos.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single
        dgvOrdenes.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single
        dgvServicios.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'los tres roles abren esta pantalla; el rol solo decide si se ven los importes
        esMecanico = (Sesion.Rol = ROL_MECANICO)

        MostrarAyuda()
        CargarVehiculos()
    End Sub

    Private Sub FrmHistorial_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(dgvVehiculos, "id_vehiculo", "patente", idVehiculo)
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la lista con lo escrito; la tarjeta no se toca, solo se vuelve a marcar su fila
        If cargando Then Exit Sub
        CargarVehiculos()
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        'guardo en un CSV las ordenes del vehiculo que se esta viendo
        ExportarGrilla(dgvOrdenes, "historial-" & lblPatente.Text)
    End Sub

    Private Sub dgvVehiculos_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvVehiculos.DataBindingComplete
        'la grilla termino de cargar sus filas
        If Not dgvVehiculos.Columns.Contains("activo") Then Exit Sub

        For Each fila As DataGridViewRow In dgvVehiculos.Rows
            'los vehiculos dados de baja van en gris, tambien cuando la fila esta seleccionada
            If Not Convert.ToBoolean(fila.Cells("activo").Value) Then
                fila.DefaultCellStyle.ForeColor = Color.Gray
                fila.DefaultCellStyle.SelectionForeColor = Color.Gainsboro
            End If
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcado solo el vehiculo que se esta viendo
        MarcarFila(dgvVehiculos, "id_vehiculo", "patente", idVehiculo)
    End Sub

    Private Sub dgvVehiculos_SelectionChanged(sender As Object, e As EventArgs) Handles dgvVehiculos.SelectionChanged
        'muestro el vehiculo que el usuario elige con el mouse o con el teclado
        If cargando Then Exit Sub
        If Not dgvVehiculos.Focused Then Exit Sub
        If dgvVehiculos.SelectedRows.Count = 0 Then Exit Sub

        Dim fila As DataGridViewRow = dgvVehiculos.SelectedRows(0)
        'si ya se esta viendo ese vehiculo no vuelvo a traer sus ordenes
        If CInt(fila.Cells("id_vehiculo").Value) = idVehiculo Then Exit Sub
        MostrarVehiculo(fila)
    End Sub

    Private Sub dgvVehiculos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVehiculos.CellClick
        'un click sobre una fila siempre la muestra, aunque la grilla ya la tuviera marcada
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        Dim fila As DataGridViewRow = dgvVehiculos.Rows(e.RowIndex)
        If CInt(fila.Cells("id_vehiculo").Value) = idVehiculo Then Exit Sub
        MostrarVehiculo(fila)
    End Sub

    Private Sub dgvOrdenes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvOrdenes.DataBindingComplete
        'pinto la celda del estado con su color, como en el tablero de ordenes
        If Not dgvOrdenes.Columns.Contains("codigo_estado") Then Exit Sub

        For Each fila As DataGridViewRow In dgvOrdenes.Rows
            Dim colorEstadoFila As Color = ColorEstado(fila.Cells("codigo_estado").Value.ToString())
            fila.Cells("estado").Style.BackColor = colorEstadoFila
            fila.Cells("estado").Style.SelectionBackColor = colorEstadoFila
            fila.Cells("estado").Style.SelectionForeColor = Color.Black
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcada solo la orden que se esta viendo
        MarcarFila(dgvOrdenes, "id_orden_trabajo", "nro_orden", idOrden)
    End Sub

    Private Sub dgvOrdenes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOrdenes.SelectionChanged
        'muestro los servicios de la orden que el usuario elige con el mouse o con el teclado
        If cargando Then Exit Sub
        If Not dgvOrdenes.Focused Then Exit Sub
        If dgvOrdenes.SelectedRows.Count = 0 Then Exit Sub

        Dim fila As DataGridViewRow = dgvOrdenes.SelectedRows(0)
        If CInt(fila.Cells("id_orden_trabajo").Value) = idOrden Then Exit Sub
        MostrarOrden(fila)
    End Sub

    Private Sub dgvOrdenes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellClick
        'un click sobre una fila siempre la muestra, aunque la grilla ya la tuviera marcada
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        Dim fila As DataGridViewRow = dgvOrdenes.Rows(e.RowIndex)
        If CInt(fila.Cells("id_orden_trabajo").Value) = idOrden Then Exit Sub
        MostrarOrden(fila)
    End Sub

End Class

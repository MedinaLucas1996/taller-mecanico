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
        'si habia una busqueda pendiente, esta carga ya usa el texto actual
        tmrFiltro.Stop()

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
            MostrarError("Error al cargar los vehículos", ex)
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
            MostrarError("Error al cargar las órdenes del vehículo", ex)
        Finally
            cargando = False
        End Try

        If tabla.Rows.Count = 0 Then
            'vehiculo sin ordenes: en lugar de las grillas, un aviso
            tlpHistorial.Visible = False
            lblSinOrdenes.Text = "Este vehículo todavía no tiene órdenes de trabajo."
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
            MostrarError("Error al cargar los servicios de la orden", ex)
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
        'sin vehiculo no hay ordenes que exportar ni historial que imprimir
        btnExportar.Enabled = False
        btnImprimir.Enabled = False
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
        'imprimir lee el historial de la base, asi que ya se puede; exportar guarda la grilla de ordenes,
        'que recien se llena cuando el usuario se detiene en el vehiculo
        btnExportar.Enabled = False
        btnImprimir.Enabled = True

        'espero un momento antes de traer sus ordenes: al bajar con las flechas solo se lee el vehiculo donde se detiene
        'mientras tanto no muestro las ordenes del vehiculo anterior
        idOrden = 0
        tmrServicios.Stop()
        tlpHistorial.Visible = False
        lblSinOrdenes.Text = "Cargando las órdenes del vehículo..."
        lblSinOrdenes.Visible = True
        tmrOrdenes.Stop()
        tmrOrdenes.Start()
    End Sub

    Private Sub tmrOrdenes_Tick(sender As Object, e As EventArgs) Handles tmrOrdenes.Tick
        'el usuario se detuvo en un vehiculo: ahora si traigo sus ordenes
        tmrOrdenes.Stop()
        If idVehiculo = 0 Then Exit Sub
        btnExportar.Enabled = True
        CargarOrdenes()
    End Sub

    Sub MostrarOrden(fila As DataGridViewRow, Optional diferido As Boolean = False)
        'muestro debajo los servicios, lo que dijo el cliente y las observaciones del mecanico de la orden de la fila
        idOrden = CInt(fila.Cells("id_orden_trabajo").Value)

        lblServiciosTit.Text = "Servicios de la orden N.º " & fila.Cells("nro_orden").Value.ToString()
        txtSintoma.Text = fila.Cells("sintoma_reportado").Value.ToString()

        Dim observaciones As String = fila.Cells("observaciones_mecanico").Value.ToString().Trim
        If observaciones = "" Then observaciones = "Sin observaciones."
        txtObservaciones.Text = observaciones

        If diferido Then
            'elegida en la grilla: espero un momento antes de traer sus servicios, sin mostrar los de la orden anterior
            dgvServicios.DataSource = Nothing
            tmrServicios.Stop()
            tmrServicios.Start()
        Else
            tmrServicios.Stop()
            CargarServicios()
        End If
    End Sub

    Private Sub tmrServicios_Tick(sender As Object, e As EventArgs) Handles tmrServicios.Tick
        'el usuario se detuvo en una orden: ahora si traigo sus servicios
        tmrServicios.Stop()
        If idOrden = 0 Then Exit Sub
        CargarServicios()
    End Sub

    Private Sub FrmHistorial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'las grillas se dibujan con doble bufer para que no parpadeen ni se traben al desplazarse
        ActivarDobleBufer(Me)

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
        'no consulto en cada tecla: vuelvo a contar el tiempo desde la ultima y cargo cuando el usuario se detiene
        If cargando Then Exit Sub
        tmrFiltro.Stop()
        tmrFiltro.Start()
    End Sub

    Private Sub tmrFiltro_Tick(sender As Object, e As EventArgs) Handles tmrFiltro.Tick
        'el usuario dejo de escribir: vuelvo a cargar la lista con lo escrito; la tarjeta no se toca, solo se vuelve a marcar su fila
        tmrFiltro.Stop()
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
        MostrarOrden(fila, True)
    End Sub

    Private Sub dgvOrdenes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellClick
        'un click sobre una fila siempre la muestra, aunque la grilla ya la tuviera marcada
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        Dim fila As DataGridViewRow = dgvOrdenes.Rows(e.RowIndex)
        If CInt(fila.Cells("id_orden_trabajo").Value) = idOrden Then Exit Sub
        MostrarOrden(fila, True)
    End Sub

    '----- historial del vehiculo impreso -----

    'datos de la hoja: se leen de la base al pedir la impresion, no de las grillas, y el evento PrintPage solo dibuja
    Private vehiculoHoja As DataRow
    Private ordenesHoja As DataTable
    Private serviciosHoja As DataTable
    Private fechaHoja As DateTime

    'bloques de la hoja en el orden en que se imprimen
    'un numero >= 0 es una parte de una orden: la orden es paso \ PARTES_POR_ORDEN (su fila en ordenesHoja) y la parte es paso Mod PARTES_POR_ORDEN
    Const PASO_VEHICULO As Integer = -1
    Const PASO_TITULAR As Integer = -2
    Const PASO_ORDENES As Integer = -3
    Const PARTE_CABECERA As Integer = 0
    Const PARTE_SERVICIOS As Integer = 1
    Const PARTE_OBSERVACIONES As Integer = 2
    'cantidad de partes de cada orden; si se agrega una parte, este numero sube con ella
    Const PARTES_POR_ORDEN As Integer = 3
    Private pasosHoja As New List(Of Integer)

    'por donde va la impresion: hoja, bloque y fila de la tabla; se reinician en BeginPrint
    Private hojaHistorial As Integer = 0
    Private pasoHistorial As Integer = 0
    Private filaHistorial As Integer = 0
    Private errorHistorial As Boolean = False

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        'imprimo el historial del vehiculo que se esta viendo
        If idVehiculo = 0 Then Exit Sub

        If Not CargarDatosHistorial() Then Exit Sub

        'abro la vista previa maximizada; desde su boton de impresora se imprime
        'esta pantalla va dentro de la ventana principal: la vista previa se abre sobre esa ventana
        Try
            prdHistorial.DefaultPageSettings.Margins = New Printing.Margins(MARGEN_HOJA, MARGEN_HOJA, MARGEN_HOJA, MARGEN_HOJA)
            ppdVistaPrevia.Document = prdHistorial
            ppdVistaPrevia.WindowState = FormWindowState.Maximized
            ppdVistaPrevia.ShowDialog(Me.TopLevelControl)
        Catch ex As Exception
            MostrarError("No se pudo preparar la impresión", ex)
        End Try
    End Sub

    Function CargarDatosHistorial() As Boolean
        'leo el vehiculo con su titular actual, todas sus ordenes y sus servicios aprobados; armo la lista de bloques
        Dim consultaVehiculo As String =
            "SELECT v.patente, CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
            "v.anio, v.color, v.km_actual, v.activo, c.razon_social, c.documento, c.telefono " &
            "FROM vehiculo AS v " &
            "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
            "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
            "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
            "WHERE v.id_vehiculo = @id_vehiculo;"

        'ordenes en cualquier estado, la mas reciente primero; el mecanico no trae ningun importe
        Dim consultaOrdenes As String =
            "SELECT ot.id_orden_trabajo, ot.nro_orden, ot.fecha_recepcion, e.descripcion AS estado, ot.km_ingreso, " &
            "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, ot.fecha_entrega, " &
            "ot.sintoma_reportado, ot.diagnostico, ot.observaciones_mecanico "
        If Not esMecanico Then consultaOrdenes = consultaOrdenes & ", ot.total_aprobado "
        consultaOrdenes = consultaOrdenes &
            "FROM orden_trabajo AS ot " &
            "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
            "LEFT JOIN mecanico AS m ON m.id_mecanico = ot.id_mecanico " &
            "WHERE ot.id_vehiculo = @id_vehiculo " &
            "ORDER BY ot.fecha_recepcion DESC, ot.nro_orden DESC;"

        'solo las lineas que el cliente aprobo, sin precios
        Dim consultaServicios As String =
            "SELECT d.id_orden_trabajo, d.descripcion, d.cantidad, d.horas_reales " &
            "FROM ot_detalle AS d " &
            "JOIN orden_trabajo AS ot ON ot.id_orden_trabajo = d.id_orden_trabajo " &
            "WHERE ot.id_vehiculo = @id_vehiculo AND d.aprobado = 1 " &
            "ORDER BY d.id_orden_trabajo, d.id_ot_detalle;"

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim vehiculo As DataTable = LeerTablaHistorial(cn, consultaVehiculo)
                If vehiculo.Rows.Count = 0 Then
                    MessageBox.Show("No se encontró el vehículo para imprimir su historial.")
                    Return False
                End If
                vehiculoHoja = vehiculo.Rows(0)
                ordenesHoja = LeerTablaHistorial(cn, consultaOrdenes)
                serviciosHoja = LeerTablaHistorial(cn, consultaServicios)
            End Using
        Catch ex As Exception
            MostrarError("Error al leer los datos del historial", ex)
            Return False
        End Try

        ArmarPasosHistorial()
        fechaHoja = DateTime.Now
        Return True
    End Function

    Function LeerTablaHistorial(cn As MySqlConnection, consulta As String) As DataTable
        'ejecuto una consulta del vehiculo y devuelvo sus filas
        Using cmd As New MySqlCommand(consulta, cn)
            cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo)
            Dim tabla As New DataTable
            Using lector As MySqlDataReader = cmd.ExecuteReader
                tabla.Load(lector)
            End Using
            Return tabla
        End Using
    End Function

    Sub ArmarPasosHistorial()
        'orden de la hoja: vehiculo, titular, tabla de ordenes y despues cada orden con sus partes
        pasosHoja.Clear()
        pasosHoja.Add(PASO_VEHICULO)
        pasosHoja.Add(PASO_TITULAR)
        pasosHoja.Add(PASO_ORDENES)

        For i As Integer = 0 To ordenesHoja.Rows.Count - 1
            pasosHoja.Add(i * PARTES_POR_ORDEN + PARTE_CABECERA)
            pasosHoja.Add(i * PARTES_POR_ORDEN + PARTE_SERVICIOS)
            'las observaciones del mecanico salen solo cuando estan cargadas
            If ordenesHoja.Rows(i)("observaciones_mecanico").ToString().Trim <> "" Then pasosHoja.Add(i * PARTES_POR_ORDEN + PARTE_OBSERVACIONES)
        Next
    End Sub

    Function ServiciosDeOrden(indice As Integer) As DataRow()
        'servicios aprobados de la orden que esta en esa fila de ordenesHoja
        Dim idOrdenHoja As Integer = CInt(ordenesHoja.Rows(indice)("id_orden_trabajo"))
        Return serviciosHoja.Select("id_orden_trabajo = " & idOrdenHoja.ToString())
    End Function

    Private Sub prdHistorial_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles prdHistorial.BeginPrint
        'la vista previa y la impresion real recorren las hojas cada una desde el principio
        hojaHistorial = 0
        pasoHistorial = 0
        filaHistorial = 0
        errorHistorial = False
    End Sub

    Private Sub prdHistorial_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles prdHistorial.PrintPage
        'un error al dibujar corta el historial con un aviso, sin cerrar el sistema
        Try
            DibujarHojaHistorial(e)
        Catch ex As Exception
            e.HasMorePages = False
            If Not errorHistorial Then
                errorHistorial = True
                MostrarError("No se pudo dibujar el historial", ex)
            End If
        End Try
    End Sub

    Sub DibujarHojaHistorial(e As Printing.PrintPageEventArgs)
        'dibujo los bloques que entran en la hoja; lo que no entra sigue en la siguiente
        Dim g As Graphics = e.Graphics
        Dim area As Rectangle = e.MarginBounds
        Dim x As Integer = area.Left
        Dim ancho As Integer = area.Width
        Dim limite As Integer = area.Bottom - ALTO_PIE
        Dim patente As String = vehiculoHoja("patente").ToString()

        hojaHistorial += 1

        Dim y As Integer = DibujarCabecera(g, x, area.Top, ancho, "Historial del vehículo", patente)
        Dim inicioHoja As Integer = y

        Do While pasoHistorial < pasosHoja.Count
            Dim paso As Integer = pasosHoja(pasoHistorial)
            Dim termino As Boolean

            If paso = PASO_ORDENES AndAlso ordenesHoja.Rows.Count > 0 Then
                'tabla de ordenes: se puede partir entre hojas
                termino = ImprimirTablaOrdenes(g, x, y, ancho, limite, y = inicioHoja)
            ElseIf paso >= 0 AndAlso paso Mod PARTES_POR_ORDEN = PARTE_SERVICIOS AndAlso ServiciosDeOrden(paso \ PARTES_POR_ORDEN).Length > 0 Then
                'servicios de una orden: tabla que tambien se puede partir
                termino = ImprimirServiciosOrden(g, paso \ PARTES_POR_ORDEN, x, y, ancho, limite, y = inicioHoja)
            Else
                'los demas bloques no se parten: si no entran pasan enteros a la hoja siguiente
                Dim fin As Integer = DibujarBloqueHistorial(g, paso, x, y, ancho, False)
                termino = (fin <= limite OrElse y = inicioHoja)
                If termino Then y = DibujarBloqueHistorial(g, paso, x, y, ancho, True)
            End If

            If Not termino Then Exit Do
            pasoHistorial += 1
            filaHistorial = 0
        Loop

        DibujarPie(g, area, "Historial del vehículo · " & patente & " · Impreso el " & fechaHoja.ToString("dd/MM/yyyy HH:mm"), hojaHistorial)
        e.HasMorePages = (pasoHistorial < pasosHoja.Count)
    End Sub

    Function DibujarBloqueHistorial(g As Graphics, paso As Integer, x As Integer, y As Integer, ancho As Integer,
                                    dibujar As Boolean) As Integer
        'dibujo (o solo mido, con dibujar en False) un bloque de la hoja y devuelvo el y donde termina
        Const ANCHO_ETIQUETA_ORDEN As Integer = 190

        Select Case paso
            Case PASO_VEHICULO
                Return DibujarCajaVehiculo(g, x, y, ancho, dibujar)

            Case PASO_TITULAR
                Return DibujarCajaTitular(g, x, y, ancho, dibujar)

            Case PASO_ORDENES
                'vehiculo sin ordenes: el titulo de la tabla y un aviso
                y = DibujarTituloSeccion(g, "Órdenes de trabajo", x, y, ancho, dibujar)
                Return DibujarParrafo(g, "Sin órdenes de trabajo registradas.", x, y, ancho, dibujar) + 14
        End Select

        Dim orden As DataRow = ordenesHoja.Rows(paso \ PARTES_POR_ORDEN)

        Select Case paso Mod PARTES_POR_ORDEN
            Case PARTE_CABECERA
                'franja con el numero y el estado de la orden, lo que dijo el cliente y el diagnostico si se cargo
                Dim titulo As String = "Orden N.º " & orden("nro_orden").ToString() & " · " & orden("estado").ToString()
                Dim yFin As Integer = DibujarTituloCaja(g, titulo, x, y, ancho, dibujar)
                yFin = DibujarDato(g, "Síntoma", orden("sintoma_reportado").ToString(), x + 8, yFin, ancho - 16, ANCHO_ETIQUETA_ORDEN, dibujar)
                If orden("diagnostico").ToString().Trim <> "" Then
                    yFin = DibujarDato(g, "Diagnóstico", orden("diagnostico").ToString().Trim, x + 8, yFin, ancho - 16, ANCHO_ETIQUETA_ORDEN, dibujar)
                End If
                yFin = yFin + 6
                'al medir reservo lugar para el comienzo de los servicios, asi la franja no queda sola al pie de la hoja
                If Not dibujar Then Return yFin + 80
                Return yFin

            Case PARTE_SERVICIOS
                'orden sin servicios aprobados
                Return DibujarParrafo(g, "Sin servicios aprobados.", x + 8, y, ancho - 16, dibujar) + 16

            Case Else
                Return DibujarDato(g, "Observaciones del mecánico", orden("observaciones_mecanico").ToString().Trim,
                                   x + 8, y, ancho - 16, ANCHO_ETIQUETA_ORDEN, dibujar) + 16
        End Select
    End Function

    Function DibujarCajaVehiculo(g As Graphics, x As Integer, y As Integer, ancho As Integer, dibujar As Boolean) As Integer
        'caja con los datos del vehiculo en dos columnas
        Const ANCHO_ETIQUETA As Integer = 130
        Dim fila As DataRow = vehiculoHoja
        Dim anchoColumna As Integer = (ancho - 32) \ 2
        Dim xDerecha As Integer = x + 16 + anchoColumna + 16

        Dim yInicio As Integer = DibujarTituloCaja(g, "Vehículo", x, y, ancho, dibujar)

        Dim yIzquierda As Integer = yInicio
        yIzquierda = DibujarDato(g, "Patente", fila("patente").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yIzquierda = DibujarDato(g, "Marca y modelo", fila("vehiculo").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yIzquierda = DibujarDato(g, "Año", fila("anio").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)

        Dim yDerecha As Integer = yInicio
        yDerecha = DibujarDato(g, "Color", fila("color").ToString(), xDerecha, yDerecha, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yDerecha = DibujarDato(g, "Kilometraje actual", CInt(fila("km_actual")).ToString("N0") & " km", xDerecha, yDerecha, anchoColumna, ANCHO_ETIQUETA, dibujar)
        'el estado sale solo cuando el vehiculo esta dado de baja
        If Not Convert.ToBoolean(fila("activo")) Then
            yDerecha = DibujarDato(g, "Estado", "Dado de baja", xDerecha, yDerecha, anchoColumna, ANCHO_ETIQUETA, dibujar)
        End If

        Dim yFin As Integer = Math.Max(yIzquierda, yDerecha) + 4
        If dibujar Then DibujarBordeCaja(g, x, y, ancho, yFin)
        Return yFin + 14
    End Function

    Function DibujarCajaTitular(g As Graphics, x As Integer, y As Integer, ancho As Integer, dibujar As Boolean) As Integer
        'caja con el titular actual del vehiculo
        Const ANCHO_ETIQUETA As Integer = 130
        Dim fila As DataRow = vehiculoHoja
        Dim anchoColumna As Integer = (ancho - 32) \ 2
        Dim xDerecha As Integer = x + 16 + anchoColumna + 16

        Dim yInicio As Integer = DibujarTituloCaja(g, "Titular actual", x, y, ancho, dibujar)

        Dim yIzquierda As Integer = yInicio
        yIzquierda = DibujarDato(g, "Razón social", fila("razon_social").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yIzquierda = DibujarDato(g, "Documento", fila("documento").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)

        Dim yDerecha As Integer = yInicio
        yDerecha = DibujarDato(g, "Teléfono", fila("telefono").ToString(), xDerecha, yDerecha, anchoColumna, ANCHO_ETIQUETA, dibujar)

        Dim yFin As Integer = Math.Max(yIzquierda, yDerecha) + 4
        If dibujar Then DibujarBordeCaja(g, x, y, ancho, yFin)
        Return yFin + 18
    End Function

    Function ImprimirTablaOrdenes(g As Graphics, x As Integer, ByRef y As Integer, ancho As Integer,
                                  limite As Integer, inicioHoja As Boolean) As Boolean
        'tabla con todas las ordenes del vehiculo; el mecanico la ve sin la columna del total
        Dim columnas() As String = {"N.º", "Recepción", "Estado", "Km ingreso", "Mecánico", "Entrega", "Total aprobado"}
        Dim pesos() As Single = {0.8F, 1.3F, 1.6F, 1.3F, 2.3F, 1.3F, 1.6F}
        Dim alineaciones() As StringAlignment = {StringAlignment.Near, StringAlignment.Near, StringAlignment.Near,
                                                 StringAlignment.Far, StringAlignment.Near, StringAlignment.Near,
                                                 StringAlignment.Far}
        If esMecanico Then
            ReDim Preserve columnas(5)
            ReDim Preserve pesos(5)
            ReDim Preserve alineaciones(5)
        End If

        Dim filas As New List(Of String())
        For Each orden As DataRow In ordenesHoja.Rows
            Dim valores() As String = {orden("nro_orden").ToString(),
                                       CDate(orden("fecha_recepcion")).ToString("dd/MM/yyyy"),
                                       orden("estado").ToString(),
                                       CInt(orden("km_ingreso")).ToString("N0"),
                                       orden("mecanico").ToString(),
                                       TextoFecha(orden("fecha_entrega"), "dd/MM/yyyy")}
            If Not esMecanico Then
                ReDim Preserve valores(6)
                'una orden sin aprobar todavia no tiene total: se imprime un guion en lugar de cortar la hoja
                If IsDBNull(orden("total_aprobado")) Then
                    valores(6) = "-"
                Else
                    valores(6) = CDec(orden("total_aprobado")).ToString("C2")
                End If
            End If
            filas.Add(valores)
        Next

        Return DibujarTabla(g, "Órdenes de trabajo", columnas, pesos, alineaciones, filas,
                            x, y, ancho, limite, filaHistorial, inicioHoja)
    End Function

    Function ImprimirServiciosOrden(g As Graphics, indice As Integer, x As Integer, ByRef y As Integer, ancho As Integer,
                                    limite As Integer, inicioHoja As Boolean) As Boolean
        'tabla con los servicios aprobados de una orden, sin precios; las horas pueden no estar cargadas
        Dim columnas() As String = {"Servicio", "Cantidad", "Horas trabajadas"}
        Dim pesos() As Single = {5.0F, 1.3F, 1.7F}
        Dim alineaciones() As StringAlignment = {StringAlignment.Near, StringAlignment.Far, StringAlignment.Far}

        Dim filas As New List(Of String())
        For Each servicio As DataRow In ServiciosDeOrden(indice)
            Dim horas As String = "-"
            If Not IsDBNull(servicio("horas_reales")) Then horas = CDec(servicio("horas_reales")).ToString("0.00")
            filas.Add({servicio("descripcion").ToString(),
                       CDec(servicio("cantidad")).ToString("0.##"),
                       horas})
        Next

        Dim titulo As String = "Servicios aprobados de la orden N.º " & ordenesHoja.Rows(indice)("nro_orden").ToString()
        Dim termino As Boolean = DibujarTabla(g, titulo, columnas, pesos, alineaciones, filas,
                                              x, y, ancho, limite, filaHistorial, inicioHoja)
        'separo la orden siguiente un poco mas que una tabla comun
        If termino Then y += 4
        Return termino
    End Function

    Private Sub FrmHistorial_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'la vista previa y el documento de impresion no estan en components: los libero al cerrar
        ppdVistaPrevia.Dispose()
        prdHistorial.Dispose()
    End Sub

End Class

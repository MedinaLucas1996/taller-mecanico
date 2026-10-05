Imports System.IO
Imports MySqlConnector

Public Class FrmOrdenes

    'orden cuyo detalle se esta mostrando, queda en 0 mientras no haya una seleccionada
    Private idOrdenSeleccionada As Integer = 0

    'estado elegido en las tarjetas, la clave 0 no filtra por estado
    Private idEstadoFiltro As Integer = 0

    'las ocho tarjetas de estado y sus etiquetas, en el orden en que se ven
    Private tarjetas() As Panel
    Private etiquetasCantidad() As Label
    Private etiquetasNombre() As Label

    'estado que muestra cada tarjeta y cuantas ordenes tiene
    Private idsEstado(7) As Integer
    Private cantidadesEstado(7) As Integer

    'evita reaccionar a los cambios de filtros y de seleccion mientras se recarga la grilla
    Private cargandoOrdenes As Boolean = False

    Function ColorEstado(codigo As String) As Color
        'color de fondo de cada estado, se decide por el codigo y no por la descripcion
        'los estados abiertos van en azules, el trabajo en proceso se distingue en amarillo
        If codigo = "RECEPCIONADA" Then Return Color.Lavender
        If codigo = "PRESUPUESTADA" Then Return Color.LightBlue
        If codigo = "APROBADA" Then Return Color.LightSkyBlue
        If codigo = "EN_PROCESO" Then Return Color.Khaki
        If codigo = "FINALIZADA" Then Return Color.PaleTurquoise
        'los estados finales: entregada en verde, rechazada en rosa y anulada en gris
        If codigo = "ENTREGADA" Then Return Color.LightGreen
        If codigo = "RECHAZADA" Then Return Color.LightPink
        If codigo = "ANULADA" Then Return Color.Gainsboro
        Return Color.White
    End Function

    Sub PintarTarjetas()
        'resalto la tarjeta del estado elegido y dejo las demas en blanco
        For i As Integer = 0 To 7
            If idsEstado(i) <> 0 AndAlso idsEstado(i) = idEstadoFiltro Then
                tarjetas(i).BackColor = Color.FromArgb(30, 39, 46)
                etiquetasCantidad(i).ForeColor = Color.White
                etiquetasNombre(i).ForeColor = Color.White
            Else
                tarjetas(i).BackColor = Color.White
                etiquetasNombre(i).ForeColor = Color.DimGray
                'un estado sin ordenes muestra su numero apagado
                If cantidadesEstado(i) = 0 Then
                    etiquetasCantidad(i).ForeColor = Color.Silver
                Else
                    etiquetasCantidad(i).ForeColor = Color.Black
                End If
            End If
        Next
    End Sub

    Sub CargarTarjetas()
        'cargo en las tarjetas la cantidad de ordenes que hay en cada estado
        'cuentan siempre todas las ordenes, no miran los filtros
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'LEFT JOIN para que tambien aparezcan los estados que no tienen ordenes
                Dim consulta As String =
                    "SELECT e.id_estado_ot, e.descripcion, COUNT(ot.id_orden_trabajo) AS cantidad " &
                    "FROM estado_ot AS e " &
                    "LEFT JOIN orden_trabajo AS ot ON ot.id_estado_ot = e.id_estado_ot " &
                    "GROUP BY e.id_estado_ot, e.descripcion, e.orden_flujo " &
                    "ORDER BY e.orden_flujo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        'cada estado va a la tarjeta que le toca por su orden en el flujo
                        Dim i As Integer = 0
                        While lector.Read() AndAlso i <= 7
                            idsEstado(i) = CInt(lector("id_estado_ot"))
                            cantidadesEstado(i) = CInt(lector("cantidad"))
                            etiquetasNombre(i).Text = lector("descripcion").ToString()
                            etiquetasCantidad(i).Text = cantidadesEstado(i).ToString()
                            tarjetas(i).Visible = True
                            i = i + 1
                        End While

                        'si la base tiene menos de ocho estados, las tarjetas que sobran no se muestran
                        While i <= 7
                            idsEstado(i) = 0
                            cantidadesEstado(i) = 0
                            tarjetas(i).Visible = False
                            i = i + 1
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las cantidades por estado: " & ex.Message)
        End Try

        PintarTarjetas()
    End Sub

    Sub CargarOrdenes()
        'cargo la grilla de ordenes con los filtros elegidos, la mas nueva primero
        'si la orden que se estaba mostrando sigue en la lista, queda seleccionada

        Dim texto As String = txtTexto.Text.Trim

        'mientras cargo, los cambios de seleccion de la grilla no cuentan como una eleccion del usuario
        cargandoOrdenes = True

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'nunca traigo las fotos de ot_foto: se cargan recien al elegir una orden
                'el cliente es el de la orden (titular al recepcionar), no el titular actual del vehiculo
                'una orden esta demorada si su fecha prometida ya paso y su estado no es final
                Dim consulta As String =
                    "SELECT ot.id_orden_trabajo, ot.nro_orden, ot.fecha_recepcion, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "c.razon_social AS cliente, " &
                    "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, " &
                    "e.descripcion AS estado, ot.fecha_prometida, " &
                    "CASE WHEN ot.fecha_prometida < @hoy AND e.es_estado_final = 0 " &
                    "THEN 'Demorada' ELSE '' END AS situacion, " &
                    "e.codigo AS codigo_estado, ot.total_presupuestado " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = ot.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN cliente AS c ON c.id_cliente = ot.id_cliente " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "LEFT JOIN mecanico AS m ON m.id_mecanico = ot.id_mecanico " &
                    "WHERE 1 = 1 "

                'aplico el filtro por el estado de la tarjeta elegida
                If idEstadoFiltro > 0 Then
                    consulta = consulta & "AND ot.id_estado_ot = @id_estado_ot "
                End If

                'aplico el filtro por patente o nombre del cliente
                If texto <> "" Then
                    consulta = consulta & "AND (v.patente LIKE @texto OR c.razon_social LIKE @texto) "
                End If

                'aplico el filtro de solo demoradas, con la misma regla de la columna situacion
                If chkDemoradas.Checked Then
                    consulta = consulta & "AND ot.fecha_prometida < @hoy AND e.es_estado_final = 0 "
                End If

                consulta = consulta & "ORDER BY ot.fecha_recepcion DESC, ot.nro_orden DESC;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@hoy", Date.Today)
                    If idEstadoFiltro > 0 Then cmd.Parameters.AddWithValue("@id_estado_ot", idEstadoFiltro)
                    If texto <> "" Then cmd.Parameters.AddWithValue("@texto", "%" & texto & "%")

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'al cargar la tabla la grilla dispara DataBindingComplete, que pinta las filas
                    dgvOrdenes.DataSource = tabla

                    'la clave, el codigo del estado y el total se usan en el detalle, no se muestran
                    dgvOrdenes.Columns("id_orden_trabajo").Visible = False
                    dgvOrdenes.Columns("codigo_estado").Visible = False
                    dgvOrdenes.Columns("total_presupuestado").Visible = False

                    'pongo titulos legibles en las columnas
                    dgvOrdenes.Columns("nro_orden").HeaderText = "N.º"
                    dgvOrdenes.Columns("fecha_recepcion").HeaderText = "Recepción"
                    dgvOrdenes.Columns("fecha_recepcion").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
                    dgvOrdenes.Columns("patente").HeaderText = "Patente"
                    dgvOrdenes.Columns("vehiculo").HeaderText = "Vehículo"
                    dgvOrdenes.Columns("cliente").HeaderText = "Cliente"
                    dgvOrdenes.Columns("mecanico").HeaderText = "Mecánico"
                    dgvOrdenes.Columns("estado").HeaderText = "Estado"
                    dgvOrdenes.Columns("fecha_prometida").HeaderText = "Prometida"
                    dgvOrdenes.Columns("fecha_prometida").DefaultCellStyle.Format = "dd/MM/yyyy"
                    dgvOrdenes.Columns("situacion").HeaderText = "Situación"

                    'las columnas de datos cortos se ajustan al contenido
                    dgvOrdenes.Columns("nro_orden").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("fecha_recepcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("patente").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("estado").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("fecha_prometida").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("situacion").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                    'el vehiculo, el cliente y el mecanico se reparten el resto del ancho
                    'con un ancho minimo: si la ventana es chica aparece la barra horizontal
                    dgvOrdenes.Columns("vehiculo").FillWeight = 110
                    dgvOrdenes.Columns("vehiculo").MinimumWidth = 110
                    dgvOrdenes.Columns("cliente").FillWeight = 140
                    dgvOrdenes.Columns("cliente").MinimumWidth = 120
                    dgvOrdenes.Columns("mecanico").FillWeight = 110
                    dgvOrdenes.Columns("mecanico").MinimumWidth = 110

                    'en el subtitulo muestro cuantas ordenes quedaron listadas y cuantas estan demoradas
                    Dim demoradas As Integer = 0
                    For Each registro As DataRow In tabla.Rows
                        If registro("situacion").ToString() = "Demorada" Then demoradas = demoradas + 1
                    Next

                    Dim resumen As String
                    If tabla.Rows.Count = 1 Then
                        resumen = "1 orden"
                    Else
                        resumen = tabla.Rows.Count & " órdenes"
                    End If
                    If demoradas = 1 Then
                        resumen = resumen & " · 1 demorada"
                    Else
                        resumen = resumen & " · " & demoradas & " demoradas"
                    End If
                    lblSubtitulo.Text = resumen
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las órdenes de trabajo: " & ex.Message)
        End Try

        'busco en la lista nueva la orden que se estaba mostrando
        Dim sigueListada As Boolean = False
        If idOrdenSeleccionada <> 0 AndAlso dgvOrdenes.Columns.Contains("id_orden_trabajo") Then
            For Each fila As DataGridViewRow In dgvOrdenes.Rows
                If CInt(fila.Cells("id_orden_trabajo").Value) = idOrdenSeleccionada Then
                    'la vuelvo a marcar y actualizo sus datos, que pudieron cambiar
                    dgvOrdenes.CurrentCell = fila.Cells("nro_orden")
                    fila.Selected = True
                    MostrarDatosOrden(fila)
                    sigueListada = True
                End If
            Next
        End If

        'si ya no esta en la lista, el detalle vuelve a mostrar solo la ayuda
        If Not sigueListada Then OcultarDetalle()

        cargandoOrdenes = False
    End Sub

    Sub VaciarCuadro(pic As PictureBox, aviso As Label, mostrarAviso As Boolean)
        'saco la foto del cuadro y libero su memoria
        If pic.Image IsNot Nothing Then
            Dim anterior As Image = pic.Image
            pic.Image = Nothing
            anterior.Dispose()
        End If

        'un cuadro sin foto no se puede ampliar
        pic.Cursor = Cursors.Default

        'el aviso "Sin foto" solo se muestra cuando hay una orden seleccionada
        aviso.Text = "Sin foto"
        aviso.Visible = mostrarAviso
    End Sub

    Sub MostrarFoto(pic As PictureBox, aviso As Label, bytes() As Byte)
        'paso los bytes guardados en la base a una foto y la muestro en el cuadro
        Try
            Using memoria As New MemoryStream(bytes)
                Using original As Image = Image.FromStream(memoria)
                    'copio la foto a un bitmap nuevo, que ya no depende de la memoria leida
                    pic.Image = New Bitmap(original)
                End Using
            End Using
            aviso.Visible = False
            'la mano indica que la foto se puede ampliar con un click
            pic.Cursor = Cursors.Hand
        Catch ex As Exception
            'los bytes guardados no son una foto valida: el cuadro queda vacio y lo aviso
            aviso.Text = "Ilegible"
            aviso.Visible = True
        End Try
    End Sub

    Sub OcultarDetalle()
        'olvido la orden seleccionada: la tarjeta de detalle muestra solo la ayuda
        idOrdenSeleccionada = 0
        pnlDatosOrden.Visible = False
        lblAyuda.Visible = True

        VaciarCuadro(picFrente, lblSinFrente, False)
        VaciarCuadro(picTrasera, lblSinTrasera, False)
        VaciarCuadro(picLateralIzq, lblSinLateralIzq, False)
        VaciarCuadro(picLateralDer, lblSinLateralDer, False)
        VaciarCuadro(picTablero, lblSinTablero, False)
    End Sub

    Sub MostrarDatosOrden(fila As DataGridViewRow)
        'paso a la tarjeta de detalle los datos de la orden, que ya estan en su fila de la grilla
        lblDetNumero.Text = "Orden N.º " & fila.Cells("nro_orden").Value.ToString()
        lblDetEstado.Text = fila.Cells("estado").Value.ToString()
        lblDetEstado.BackColor = ColorEstado(fila.Cells("codigo_estado").Value.ToString())
        lblDetVehiculo.Text = fila.Cells("patente").Value.ToString() & " - " & fila.Cells("vehiculo").Value.ToString()
        lblDetCliente.Text = fila.Cells("cliente").Value.ToString()
        lblDetMecanico.Text = fila.Cells("mecanico").Value.ToString()
        lblDetRecepcion.Text = CDate(fila.Cells("fecha_recepcion").Value).ToString("dd/MM/yyyy HH:mm")

        'la fecha prometida es opcional
        If IsDBNull(fila.Cells("fecha_prometida").Value) Then
            lblDetPrometida.Text = "Sin fecha"
        Else
            lblDetPrometida.Text = CDate(fila.Cells("fecha_prometida").Value).ToString("dd/MM/yyyy")
        End If

        'una orden demorada lo indica al lado de su fecha prometida
        If fila.Cells("situacion").Value.ToString() = "Demorada" Then
            lblDetPrometida.Text = lblDetPrometida.Text & " (demorada)"
        End If

        lblDetTotal.Text = CDec(fila.Cells("total_presupuestado").Value).ToString("C2")

        'muestro el contenido de la tarjeta, con el boton para gestionar la orden
        lblAyuda.Visible = False
        pnlDatosOrden.Visible = True
    End Sub

    Sub CargarFotos(idOrden As Integer)
        'cargo las fotos de recepcion de la orden en las cinco miniaturas

        'vacio los cinco cuadros, los que no tengan foto quedan con el aviso "Sin foto"
        VaciarCuadro(picFrente, lblSinFrente, True)
        VaciarCuadro(picTrasera, lblSinTrasera, True)
        VaciarCuadro(picLateralIzq, lblSinLateralIzq, True)
        VaciarCuadro(picLateralDer, lblSinLateralDer, True)
        VaciarCuadro(picTablero, lblSinTablero, True)

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'traigo solo las fotos de esta orden, hay como mucho una por angulo
                Dim consulta As String =
                    "SELECT angulo, imagen FROM ot_foto WHERE id_orden_trabajo = @id_orden_trabajo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", idOrden)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        While lector.Read()
                            Dim angulo As String = lector("angulo").ToString()
                            Dim bytes() As Byte = CType(lector("imagen"), Byte())

                            'cada angulo va a su cuadro
                            If angulo = "FRENTE" Then MostrarFoto(picFrente, lblSinFrente, bytes)
                            If angulo = "TRASERA" Then MostrarFoto(picTrasera, lblSinTrasera, bytes)
                            If angulo = "LATERAL_IZQUIERDO" Then MostrarFoto(picLateralIzq, lblSinLateralIzq, bytes)
                            If angulo = "LATERAL_DERECHO" Then MostrarFoto(picLateralDer, lblSinLateralDer, bytes)
                            If angulo = "TABLERO" Then MostrarFoto(picTablero, lblSinTablero, bytes)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las fotos de la orden: " & ex.Message)
        End Try
    End Sub

    Private Sub FrmOrdenes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'junto las tarjetas y sus etiquetas en el orden en que se ven, para recorrerlas con un For
        tarjetas = {pnlEstado1, pnlEstado2, pnlEstado3, pnlEstado4, pnlEstado5, pnlEstado6, pnlEstado7, pnlEstado8}
        etiquetasCantidad = {lblCant1, lblCant2, lblCant3, lblCant4, lblCant5, lblCant6, lblCant7, lblCant8}
        etiquetasNombre = {lblNom1, lblNom2, lblNom3, lblNom4, lblNom5, lblNom6, lblNom7, lblNom8}

        'solo el administrador y el operador consultan las ordenes (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" AndAlso Sesion.Rol <> "OPERADOR" Then
            MessageBox.Show("Solo el administrador y el operador pueden consultar las órdenes de trabajo.")
            tlpEstados.Enabled = False
            pnlFiltros.Enabled = False
            dgvOrdenes.Enabled = False
            Exit Sub
        End If

        'cargo las tarjetas y la grilla al abrir el formulario
        CargarTarjetas()
        CargarOrdenes()
    End Sub

    Private Sub TarjetaEstado_Click(sender As Object, e As EventArgs) Handles _
        pnlEstado1.Click, lblCant1.Click, lblNom1.Click,
        pnlEstado2.Click, lblCant2.Click, lblNom2.Click,
        pnlEstado3.Click, lblCant3.Click, lblNom3.Click,
        pnlEstado4.Click, lblCant4.Click, lblNom4.Click,
        pnlEstado5.Click, lblCant5.Click, lblNom5.Click,
        pnlEstado6.Click, lblCant6.Click, lblNom6.Click,
        pnlEstado7.Click, lblCant7.Click, lblNom7.Click,
        pnlEstado8.Click, lblCant8.Click, lblNom8.Click

        'filtro la lista por el estado de la tarjeta, o saco el filtro si ya estaba elegida

        'el click puede venir de la tarjeta o de una de sus dos etiquetas
        Dim origen As Control = CType(sender, Control)
        If TypeOf origen Is Label Then origen = origen.Parent

        For i As Integer = 0 To 7
            If origen Is tarjetas(i) AndAlso idsEstado(i) <> 0 Then
                If idEstadoFiltro = idsEstado(i) Then
                    idEstadoFiltro = 0
                Else
                    idEstadoFiltro = idsEstado(i)
                End If
            End If
        Next

        'vuelvo a contar y a listar: asi tambien se ven los cambios hechos desde otro puesto
        CargarTarjetas()
        CargarOrdenes()
    End Sub

    Private Sub txtTexto_TextChanged(sender As Object, e As EventArgs) Handles txtTexto.TextChanged
        'vuelvo a cargar la grilla con el texto escrito
        If cargandoOrdenes Then Exit Sub
        CargarOrdenes()
    End Sub

    Private Sub chkDemoradas_CheckedChanged(sender As Object, e As EventArgs) Handles chkDemoradas.CheckedChanged
        'vuelvo a cargar la grilla con o sin el filtro de demoradas
        If cargandoOrdenes Then Exit Sub
        CargarOrdenes()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'saco los tres filtros: el texto, las demoradas y el estado de la tarjeta

        'mientras los limpio no dejo que cada cambio recargue la grilla por su cuenta
        cargandoOrdenes = True
        txtTexto.Clear()
        chkDemoradas.Checked = False
        idEstadoFiltro = 0
        cargandoOrdenes = False

        CargarTarjetas()
        CargarOrdenes()
        txtTexto.Focus()
    End Sub

    Private Sub dgvOrdenes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvOrdenes.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas: las vuelvo a pintar
        'se hace en este evento porque al ordenar por una columna las filas pierden su color
        If Not dgvOrdenes.Columns.Contains("situacion") Then Exit Sub

        'la grilla selecciona sola la primera fila, la desmarco
        dgvOrdenes.ClearSelection()

        For Each fila As DataGridViewRow In dgvOrdenes.Rows
            'fondo rojo suave para las ordenes demoradas
            If fila.Cells("situacion").Value.ToString() = "Demorada" Then
                fila.DefaultCellStyle.BackColor = Color.MistyRose
            End If

            'la celda del estado lleva el color de su estado, tambien con la fila seleccionada
            Dim colorDelEstado As Color = ColorEstado(fila.Cells("codigo_estado").Value.ToString())
            fila.Cells("estado").Style.BackColor = colorDelEstado
            fila.Cells("estado").Style.ForeColor = Color.Black
            fila.Cells("estado").Style.SelectionBackColor = colorDelEstado
            fila.Cells("estado").Style.SelectionForeColor = Color.Black

            'si se reordeno la grilla, vuelvo a marcar la orden que se esta mostrando
            If idOrdenSeleccionada <> 0 AndAlso CInt(fila.Cells("id_orden_trabajo").Value) = idOrdenSeleccionada Then
                fila.Selected = True
            End If
        Next
    End Sub

    Private Sub dgvOrdenes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOrdenes.SelectionChanged
        'muestro el detalle de la orden que el usuario elige con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargandoOrdenes Then Exit Sub
        If Not dgvOrdenes.Focused Then Exit Sub
        If dgvOrdenes.SelectedRows.Count = 0 Then Exit Sub

        Dim fila As DataGridViewRow = dgvOrdenes.SelectedRows(0)
        Dim idOrden As Integer = CInt(fila.Cells("id_orden_trabajo").Value)

        'si ya se esta mostrando esa orden no vuelvo a traer sus fotos
        If idOrden = idOrdenSeleccionada Then Exit Sub

        idOrdenSeleccionada = idOrden
        MostrarDatosOrden(fila)
        CargarFotos(idOrden)
    End Sub

    Private Sub Foto_Click(sender As Object, e As EventArgs) Handles _
        picFrente.Click, picTrasera.Click, picLateralIzq.Click, picLateralDer.Click, picTablero.Click

        'amplio la foto de la miniatura en una ventana aparte
        Dim pic As PictureBox = CType(sender, PictureBox)

        'una miniatura sin foto no hace nada
        If pic.Image Is Nothing Then Exit Sub

        'el titulo de la ventana es el angulo de la foto
        Dim titulo As String = "Foto de recepción"
        If pic Is picFrente Then titulo = "Frente"
        If pic Is picTrasera Then titulo = "Trasera"
        If pic Is picLateralIzq Then titulo = "Lateral izquierdo"
        If pic Is picLateralDer Then titulo = "Lateral derecho"
        If pic Is picTablero Then titulo = "Tablero"

        Using formulario As New FrmFoto
            formulario.Text = titulo & " - " & lblDetNumero.Text
            'la ventana muestra la misma foto de la miniatura, no la libera al cerrarse
            formulario.picFoto.Image = pic.Image
            formulario.ShowDialog()
        End Using
    End Sub

    Private Sub btnGestionar_Click(sender As Object, e As EventArgs) Handles btnGestionar.Click
        'abro la gestion de la orden seleccionada en una ventana aparte
        If idOrdenSeleccionada = 0 Then
            MessageBox.Show("Debe seleccionar una orden de la grilla para gestionarla")
            Exit Sub
        End If

        Using formulario As New FrmOrdenGestion
            'le indico al formulario que orden tiene que cargar
            formulario.IdOrdenTrabajo = idOrdenSeleccionada
            formulario.ShowDialog()
        End Using

        'al cerrar la gestion la orden pudo cambiar de estado: recargo las tarjetas y la grilla
        'la orden queda seleccionada si sigue en la lista
        CargarTarjetas()
        CargarOrdenes()
    End Sub

End Class

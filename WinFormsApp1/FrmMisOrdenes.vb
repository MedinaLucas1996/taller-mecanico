Imports System.IO
Imports MySqlConnector

Public Class FrmMisOrdenes

    'grupo elegido en las tarjetas de arriba: 1 en curso, 2 para empezar, 3 terminadas; siempre hay uno
    Private grupoElegido As Integer = 1
    'cantidad de ordenes del mecanico en cada grupo (el lugar 0 no se usa)
    Private cantidades(3) As Integer

    'orden que se ve en la tarjeta del resumen, queda en 0 mientras no haya una (el ID no se muestra)
    Private idOrden As Integer = 0
    'codigo del estado de esa orden, decide que dice el boton
    Private codigoEstadoOrden As String = ""

    'nombre del mecanico que inicio la sesion, para el subtitulo
    Private nombreMecanico As String = ""

    'queda en True mientras el programa carga u ordena la grilla, para no tomarlo como una eleccion del usuario
    Private cargando As Boolean = False

    'las tarjetas de grupo y sus etiquetas, para recorrerlas con un For (el lugar 0 no se usa)
    Private tarjetas() As Panel
    Private etiquetasCantidad() As Label
    Private etiquetasNombre() As Label

    'iconos del boton, que cambia segun el estado de la orden
    Private iconoIniciar As Image
    Private iconoFinalizar As Image
    Private iconoAbrir As Image

    Function ColorEstado(codigo As String) As Color
        'color de fondo de cada estado, los mismos que usa el tablero de ordenes
        If codigo = "APROBADA" Then Return Color.LightSkyBlue
        If codigo = "EN_PROCESO" Then Return Color.Khaki
        If codigo = "FINALIZADA" Then Return Color.PaleTurquoise
        If codigo = "ENTREGADA" Then Return Color.LightGreen
        Return Color.White
    End Function

    Function GrupoDeEstado(codigo As String) As Integer
        'grupo al que pertenece cada estado; 0 para los estados que esta pantalla no lista
        If codigo = "EN_PROCESO" Then Return 1
        If codigo = "APROBADA" Then Return 2
        If codigo = "FINALIZADA" OrElse codigo = "ENTREGADA" Then Return 3
        Return 0
    End Function

    Sub CargarGrupos()
        'cuento las ordenes del mecanico en cada grupo y armo el subtitulo
        cantidades(1) = 0
        cantidades(2) = 0
        cantidades(3) = 0

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'solo las ordenes asignadas al mecanico que inicio la sesion
                Dim consulta As String =
                    "SELECT e.codigo, COUNT(*) AS cantidad " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.id_mecanico = @id_mecanico " &
                    "AND e.codigo IN ('EN_PROCESO', 'APROBADA', 'FINALIZADA', 'ENTREGADA') " &
                    "GROUP BY e.codigo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_mecanico", Sesion.IdMecanico)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        While lector.Read()
                            'finalizadas y entregadas se suman en el mismo grupo
                            Dim grupo As Integer = GrupoDeEstado(lector("codigo").ToString())
                            If grupo > 0 Then cantidades(grupo) = cantidades(grupo) + CInt(lector("cantidad"))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al contar sus órdenes de trabajo: " & ex.Message)
        End Try

        For i As Integer = 1 To 3
            etiquetasCantidad(i).Text = cantidades(i).ToString()
        Next

        lblSubtitulo.Text = nombreMecanico & " · " & cantidades(1) & " en curso · " & cantidades(2) & " para empezar"

        PintarGrupos()
    End Sub

    Sub PintarGrupos()
        'resalto la tarjeta del grupo elegido y dejo las demas en blanco
        For i As Integer = 1 To 3
            If i = grupoElegido Then
                tarjetas(i).BackColor = Color.FromArgb(30, 39, 46)
                etiquetasCantidad(i).ForeColor = Color.White
                etiquetasNombre(i).ForeColor = Color.White
            Else
                tarjetas(i).BackColor = Color.White
                etiquetasNombre(i).ForeColor = Color.DimGray
                'un grupo sin ordenes muestra su numero apagado
                If cantidades(i) = 0 Then
                    etiquetasCantidad(i).ForeColor = Color.Silver
                Else
                    etiquetasCantidad(i).ForeColor = Color.Black
                End If
            End If
        Next
    End Sub

    Sub CargarOrdenes()
        'cargo en la grilla las ordenes del mecanico que estan en el grupo elegido
        'si la orden que se estaba viendo sigue en la lista, queda marcada
        cargando = True

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'nunca traigo las fotos de ot_foto ni ningun importe: las fotos se cargan recien al elegir una orden
                'lineas y con_horas cuentan los servicios aprobados y los que ya tienen sus horas cargadas
                'una orden esta demorada si su fecha prometida ya paso y su estado no es final
                Dim consulta As String =
                    "SELECT ot.id_orden_trabajo, ot.nro_orden, " &
                    "CONCAT(v.patente, ' · ', ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "ot.fecha_prometida, " &
                    "(SELECT COUNT(*) FROM ot_detalle AS d " &
                    "WHERE d.id_orden_trabajo = ot.id_orden_trabajo AND d.aprobado = 1) AS lineas, " &
                    "(SELECT COUNT(*) FROM ot_detalle AS d " &
                    "WHERE d.id_orden_trabajo = ot.id_orden_trabajo AND d.aprobado = 1 " &
                    "AND d.horas_reales IS NOT NULL) AS con_horas, " &
                    "e.codigo AS codigo_estado, e.descripcion AS estado, " &
                    "ot.km_ingreso, ot.sintoma_reportado, ot.observaciones_recepcion, " &
                    "CASE WHEN ot.fecha_prometida < @hoy AND e.es_estado_final = 0 THEN 1 ELSE 0 END AS demorada " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = ot.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.id_mecanico = @id_mecanico "

                If grupoElegido = 1 Then
                    'en curso: primero la que se prometio antes, las que no tienen fecha al final
                    consulta = consulta & "AND e.codigo = 'EN_PROCESO' " &
                               "ORDER BY ot.fecha_prometida IS NULL, ot.fecha_prometida, ot.nro_orden;"
                ElseIf grupoElegido = 2 Then
                    'para empezar: con el mismo orden
                    consulta = consulta & "AND e.codigo = 'APROBADA' " &
                               "ORDER BY ot.fecha_prometida IS NULL, ot.fecha_prometida, ot.nro_orden;"
                Else
                    'terminadas: la mas reciente primero, por su entrega o por su finalizacion
                    consulta = consulta & "AND e.codigo IN ('FINALIZADA', 'ENTREGADA') " &
                               "ORDER BY COALESCE(ot.fecha_entrega, ot.fecha_finalizacion) DESC, ot.nro_orden DESC;"
                End If

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_mecanico", Sesion.IdMecanico)
                    cmd.Parameters.AddWithValue("@hoy", Date.Today)

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'columna "n de m": servicios con horas cargadas sobre servicios aprobados
                    'en "Para empezar" queda vacia, todavia no se cargo nada
                    tabla.Columns.Add("horas", GetType(String))
                    For Each registro As DataRow In tabla.Rows
                        If grupoElegido = 2 Then
                            registro("horas") = ""
                        Else
                            registro("horas") = registro("con_horas").ToString() & " de " & registro("lineas").ToString()
                        End If
                    Next

                    'al cargar la tabla la grilla dispara DataBindingComplete, que pinta las demoradas
                    dgvOrdenes.DataSource = tabla

                    'en la lista se ven cuatro columnas, el resto se usa en el resumen
                    dgvOrdenes.Columns("id_orden_trabajo").Visible = False
                    dgvOrdenes.Columns("lineas").Visible = False
                    dgvOrdenes.Columns("con_horas").Visible = False
                    dgvOrdenes.Columns("codigo_estado").Visible = False
                    dgvOrdenes.Columns("estado").Visible = False
                    dgvOrdenes.Columns("km_ingreso").Visible = False
                    dgvOrdenes.Columns("sintoma_reportado").Visible = False
                    dgvOrdenes.Columns("observaciones_recepcion").Visible = False
                    dgvOrdenes.Columns("demorada").Visible = False

                    'pongo titulos legibles en las columnas y reparto el ancho, con un minimo para cada una
                    dgvOrdenes.Columns("nro_orden").HeaderText = "N.º"
                    dgvOrdenes.Columns("nro_orden").FillWeight = 12
                    dgvOrdenes.Columns("nro_orden").MinimumWidth = 45
                    dgvOrdenes.Columns("vehiculo").HeaderText = "Vehículo"
                    dgvOrdenes.Columns("vehiculo").FillWeight = 50
                    dgvOrdenes.Columns("vehiculo").MinimumWidth = 150
                    dgvOrdenes.Columns("fecha_prometida").HeaderText = "Prometida"
                    dgvOrdenes.Columns("fecha_prometida").DefaultCellStyle.Format = "dd/MM/yyyy"
                    dgvOrdenes.Columns("fecha_prometida").FillWeight = 22
                    dgvOrdenes.Columns("fecha_prometida").MinimumWidth = 85
                    dgvOrdenes.Columns("horas").HeaderText = "Horas"
                    dgvOrdenes.Columns("horas").FillWeight = 16
                    dgvOrdenes.Columns("horas").MinimumWidth = 60

                    'el orden por columna lo hace el formulario al hacer click en el titulo (ver ColumnHeaderMouseClick)
                    dgvOrdenes.Columns("nro_orden").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvOrdenes.Columns("vehiculo").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvOrdenes.Columns("fecha_prometida").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvOrdenes.Columns("horas").SortMode = DataGridViewColumnSortMode.Programmatic

                    'sin ordenes en el grupo, la ayuda lo dice
                    If tabla.Rows.Count = 0 Then
                        lblAyuda.Text = "No tenés órdenes en este grupo."
                    Else
                        lblAyuda.Text = "Seleccioná una orden de la lista para ver el trabajo a realizar."
                    End If
                End Using
            End Using

            'en la lista nueva dejo marcada la orden que se estaba viendo
            Dim filaActual As DataGridViewRow = MarcarFila(idOrden)
            If filaActual Is Nothing Then
                'ya no esta en este grupo: el resumen vuelve a la ayuda
                MostrarAyuda()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar sus órdenes de trabajo: " & ex.Message)
        Finally
            'pase lo que pase, la grilla vuelve a atender al usuario
            cargando = False
        End Try
    End Sub

    Function MarcarFila(id As Integer) As DataGridViewRow
        'dejo marcada en la lista la fila de la orden con esa clave: celda actual y seleccion juntas
        'con clave 0, o si la orden no esta listada, no queda nada marcado; devuelve la fila o Nothing
        Dim encontrada As DataGridViewRow = Nothing
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If id <> 0 AndAlso dgvOrdenes.Columns.Contains("id_orden_trabajo") Then
                For Each fila As DataGridViewRow In dgvOrdenes.Rows
                    If CInt(fila.Cells("id_orden_trabajo").Value) = id Then encontrada = fila
                Next
            End If

            dgvOrdenes.ClearSelection()
            If encontrada Is Nothing Then
                dgvOrdenes.CurrentCell = Nothing
            Else
                dgvOrdenes.CurrentCell = encontrada.Cells("nro_orden")
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    Sub VaciarCuadro(pic As PictureBox, aviso As Label, mostrarAviso As Boolean)
        'saco la foto del cuadro y libero su memoria
        If pic.Image IsNot Nothing Then
            Dim anterior As Image = pic.Image
            pic.Image = Nothing
            anterior.Dispose()
        End If

        'un cuadro sin foto no se puede ampliar; aca nunca se ofrece cargar una
        pic.Cursor = Cursors.Default
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

    Sub VaciarFotos(mostrarAviso As Boolean)
        'vacio las cinco miniaturas
        VaciarCuadro(picFrente, lblSinFrente, mostrarAviso)
        VaciarCuadro(picTrasera, lblSinTrasera, mostrarAviso)
        VaciarCuadro(picLateralIzq, lblSinLateralIzq, mostrarAviso)
        VaciarCuadro(picLateralDer, lblSinLateralDer, mostrarAviso)
        VaciarCuadro(picTablero, lblSinTablero, mostrarAviso)
    End Sub

    Sub CargarFotos()
        'cargo las fotos de recepcion de la orden que se ve en las cinco miniaturas
        'los cuadros que no tengan foto quedan con el aviso "Sin foto"
        VaciarFotos(True)

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'solo las fotos de esta orden, y solo si la orden es del mecanico; hay como mucho una por angulo
                Dim consulta As String =
                    "SELECT f.angulo, f.imagen " &
                    "FROM ot_foto AS f " &
                    "JOIN orden_trabajo AS ot ON ot.id_orden_trabajo = f.id_orden_trabajo " &
                    "WHERE f.id_orden_trabajo = @id_orden_trabajo AND ot.id_mecanico = @id_mecanico;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", idOrden)
                    cmd.Parameters.AddWithValue("@id_mecanico", Sesion.IdMecanico)
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

    Sub CargarTrabajo()
        'armo la lista de servicios aprobados de la orden que se ve: cantidad, descripcion y horas, sin importes
        Dim texto As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'solo las lineas que aprobo el cliente, y solo si la orden es del mecanico
                Dim consulta As String =
                    "SELECT d.descripcion, d.cantidad, d.horas_reales " &
                    "FROM ot_detalle AS d " &
                    "JOIN orden_trabajo AS ot ON ot.id_orden_trabajo = d.id_orden_trabajo " &
                    "WHERE d.id_orden_trabajo = @id_orden_trabajo AND ot.id_mecanico = @id_mecanico " &
                    "AND d.aprobado = 1 " &
                    "ORDER BY d.id_ot_detalle;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", idOrden)
                    cmd.Parameters.AddWithValue("@id_mecanico", Sesion.IdMecanico)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        While lector.Read()
                            'las horas pueden no estar cargadas todavia
                            Dim horas As String = "sin horas"
                            If Not IsDBNull(lector("horas_reales")) Then
                                horas = CDec(lector("horas_reales")).ToString("N2") & " h"
                            End If

                            If texto <> "" Then texto = texto & vbCrLf
                            texto = texto & CDec(lector("cantidad")).ToString("0.##") & " × " &
                                    lector("descripcion").ToString() & " · " & horas
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los servicios de la orden: " & ex.Message)
        End Try

        If texto = "" Then texto = "La orden no tiene servicios aprobados."
        txtTrabajo.Text = texto
    End Sub

    Sub MostrarAyuda()
        'sin orden elegida, la tarjeta del resumen muestra solo la ayuda
        idOrden = 0
        codigoEstadoOrden = ""
        pnlOrden.Visible = False
        lblAyuda.Visible = True
        VaciarFotos(False)
    End Sub

    Sub MostrarOrden(fila As DataGridViewRow)
        'cargo en la tarjeta del resumen la orden de la fila: lo que el mecanico necesita para trabajar
        idOrden = CInt(fila.Cells("id_orden_trabajo").Value)
        codigoEstadoOrden = fila.Cells("codigo_estado").Value.ToString()

        lblNumero.Text = "Orden N.º " & fila.Cells("nro_orden").Value.ToString()
        lblEstado.Text = fila.Cells("estado").Value.ToString()
        lblEstado.BackColor = ColorEstado(codigoEstadoOrden)

        'patente, vehiculo y kilometraje con el que ingreso
        lblVehiculo.Text = fila.Cells("vehiculo").Value.ToString() & " · " &
                           CInt(fila.Cells("km_ingreso").Value).ToString("N0") & " km"

        txtSintoma.Text = fila.Cells("sintoma_reportado").Value.ToString()

        'las observaciones de recepcion son opcionales: sin texto, sus dos renglones no se muestran
        Dim observaciones As String = fila.Cells("observaciones_recepcion").Value.ToString().Trim
        txtObsRecepcion.Text = observaciones
        lblObsTit.Visible = (observaciones <> "")
        txtObsRecepcion.Visible = (observaciones <> "")

        CargarTrabajo()
        CargarFotos()

        'el boton dice el proximo paso del mecanico; la gestion de la orden hace el resto
        If codigoEstadoOrden = "APROBADA" Then
            btnAbrir.Text = " Iniciar trabajo"
            btnAbrir.Image = iconoIniciar
        ElseIf codigoEstadoOrden = "EN_PROCESO" Then
            btnAbrir.Text = " Cargar horas y finalizar"
            btnAbrir.Image = iconoFinalizar
        Else
            btnAbrir.Text = " Ver orden"
            btnAbrir.Image = iconoAbrir
        End If

        lblAyuda.Visible = False
        pnlOrden.Visible = True
    End Sub

    Sub ElegirFila(fila As DataGridViewRow)
        'el mecanico eligio una orden de la lista: la muestro en el resumen
        Dim idElegido As Integer = CInt(fila.Cells("id_orden_trabajo").Value)

        'si ya se esta viendo esa orden no vuelvo a traer sus fotos
        If idElegido = idOrden Then Exit Sub

        MostrarOrden(fila)
    End Sub

    Function AnguloDe(pic As PictureBox) As String
        'angulo de la miniatura tal como lo guarda la base
        If pic Is picFrente Then Return "FRENTE"
        If pic Is picTrasera Then Return "TRASERA"
        If pic Is picLateralIzq Then Return "LATERAL_IZQUIERDO"
        If pic Is picLateralDer Then Return "LATERAL_DERECHO"
        Return "TABLERO"
    End Function

    Function NombreAnguloDe(pic As PictureBox) As String
        'angulo de la miniatura como lo lee el usuario
        If pic Is picFrente Then Return "Frente"
        If pic Is picTrasera Then Return "Trasera"
        If pic Is picLateralIzq Then Return "Lateral izquierdo"
        If pic Is picLateralDer Then Return "Lateral derecho"
        Return "Tablero"
    End Function

    Private Sub FrmMisOrdenes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'junto las tarjetas y sus etiquetas para recorrerlas con un For (el lugar 0 no se usa)
        tarjetas = {Nothing, pnlGrupo1, pnlGrupo2, pnlGrupo3}
        etiquetasCantidad = {Nothing, lblCantGrupo1, lblCantGrupo2, lblCantGrupo3}
        etiquetasNombre = {Nothing, lblNomGrupo1, lblNomGrupo2, lblNomGrupo3}

        'iconos, si alguno falta ese control queda sin icono y el sistema sigue
        iconoIniciar = LeerIcono("iniciar.png")
        iconoFinalizar = LeerIcono("finalizar.png")
        iconoAbrir = LeerIcono("abrir.png")
        picVehiculo.Image = LeerIcono("vehiculo-oscuro.png")
        picSintoma.Image = LeerIcono("sintoma-oscuro.png")

        'el encabezado de la lista lleva solo una linea clara debajo, como las filas
        dgvOrdenes.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'esta pantalla es del mecanico (el menu solo le muestra el boton a ese rol)
        If Sesion.Rol <> "MECANICO" Then
            MessageBox.Show("Esta pantalla es solo para el mecánico.")
            tlpGrupos.Enabled = False
            pnlLista.Enabled = False
            pnlResumen.Enabled = False
            Exit Sub
        End If

        'un usuario mecanico sin mecanico asociado no tiene ordenes que mostrar
        If Sesion.IdMecanico = 0 Then
            MessageBox.Show("Su usuario no tiene un mecánico asociado.")
            tlpGrupos.Enabled = False
            pnlLista.Enabled = False
            pnlResumen.Enabled = False
            Exit Sub
        End If

        'nombre del mecanico, para el subtitulo
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "SELECT nombre_completo FROM mecanico WHERE id_mecanico = @id_mecanico;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_mecanico", Sesion.IdMecanico)
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado IsNot Nothing Then nombreMecanico = resultado.ToString()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al leer los datos del mecánico: " & ex.Message)
        End Try

        'cuento los grupos y entro por el primero que tenga ordenes: en curso, para empezar o terminadas
        MostrarAyuda()
        CargarGrupos()
        If cantidades(1) > 0 Then
            grupoElegido = 1
        ElseIf cantidades(2) > 0 Then
            grupoElegido = 2
        Else
            grupoElegido = 3
        End If
        PintarGrupos()
        CargarOrdenes()
    End Sub

    Private Sub FrmMisOrdenes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(idOrden)
    End Sub

    Private Sub FrmMisOrdenes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'libero la memoria de las fotos que quedaron cargadas
        VaciarFotos(False)
    End Sub

    Private Sub Grupo_Click(sender As Object, e As EventArgs) Handles _
        pnlGrupo1.Click, lblCantGrupo1.Click, lblNomGrupo1.Click,
        pnlGrupo2.Click, lblCantGrupo2.Click, lblNomGrupo2.Click,
        pnlGrupo3.Click, lblCantGrupo3.Click, lblNomGrupo3.Click

        'listo las ordenes del grupo de la tarjeta; siempre queda un grupo elegido

        'el click puede venir de la tarjeta o de una de sus dos etiquetas
        Dim origen As Control = CType(sender, Control)
        If TypeOf origen Is Label Then origen = origen.Parent

        For i As Integer = 1 To 3
            If origen Is tarjetas(i) Then grupoElegido = i
        Next

        'vuelvo a contar y a listar: asi tambien se ven los cambios hechos desde otro puesto
        CargarGrupos()
        CargarOrdenes()
    End Sub

    Private Sub dgvOrdenes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvOrdenes.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas: las vuelvo a pintar
        If Not dgvOrdenes.Columns.Contains("demorada") Then Exit Sub

        For Each fila As DataGridViewRow In dgvOrdenes.Rows
            'fondo rojo suave para las ordenes demoradas, igual que en el tablero
            If Convert.ToInt32(fila.Cells("demorada").Value) = 1 Then
                fila.DefaultCellStyle.BackColor = Color.MistyRose
            End If
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcada solo la orden que se esta viendo
        MarcarFila(idOrden)
    End Sub

    Private Sub dgvOrdenes_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvOrdenes.ColumnHeaderMouseClick
        'ordeno la lista por la columna del titulo que se toco; otro click en la misma columna invierte el orden
        'ordenar nunca cambia la orden que se esta viendo
        If e.Button <> MouseButtons.Left Then Exit Sub
        If cargando Then Exit Sub

        Dim columna As DataGridViewColumn = dgvOrdenes.Columns(e.ColumnIndex)
        Dim sentido As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        If dgvOrdenes.SortedColumn Is columna AndAlso dgvOrdenes.SortOrder = SortOrder.Ascending Then
            sentido = System.ComponentModel.ListSortDirection.Descending
        End If

        'mientras se ordena la grilla mueve sola su seleccion: eso no es una eleccion del usuario
        cargando = True
        Try
            dgvOrdenes.Sort(columna, sentido)

            'flecha del titulo, para que se vea por que columna quedo ordenada
            If sentido = System.ComponentModel.ListSortDirection.Ascending Then
                columna.HeaderCell.SortGlyphDirection = SortOrder.Ascending
            Else
                columna.HeaderCell.SortGlyphDirection = SortOrder.Descending
            End If
        Finally
            cargando = False
        End Try
    End Sub

    Private Sub dgvOrdenes_Sorted(sender As Object, e As EventArgs) Handles dgvOrdenes.Sorted
        'las filas cambiaron de lugar: vuelvo a marcar la orden que se esta viendo
        MarcarFila(idOrden)
    End Sub

    Private Sub dgvOrdenes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOrdenes.SelectionChanged
        'muestro el resumen de la orden que el mecanico elige con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargando Then Exit Sub
        If Not dgvOrdenes.Focused Then Exit Sub
        If dgvOrdenes.SelectedRows.Count = 0 Then Exit Sub

        ElegirFila(dgvOrdenes.SelectedRows(0))
    End Sub

    Private Sub dgvOrdenes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellClick
        'un click sobre una fila siempre la muestra, aunque la grilla ya la tuviera marcada
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirFila(dgvOrdenes.Rows(e.RowIndex))
    End Sub

    Private Sub Foto_Click(sender As Object, e As EventArgs) Handles _
        picFrente.Click, picTrasera.Click, picLateralIzq.Click, picLateralDer.Click, picTablero.Click

        'una miniatura con foto se amplia en una ventana aparte; el mecanico no carga, cambia ni quita fotos
        Dim pic As PictureBox = CType(sender, PictureBox)
        If idOrden = 0 Then Exit Sub
        If pic.Image Is Nothing Then Exit Sub

        Using formulario As New FrmFoto
            'el titulo de la ventana es el angulo de la foto y el numero de la orden
            formulario.Text = NombreAnguloDe(pic) & " - " & lblNumero.Text
            'la ventana muestra la misma foto de la miniatura, no la libera al cerrarse
            formulario.picFoto.Image = pic.Image
            formulario.IdOrdenTrabajo = idOrden
            formulario.Angulo = AnguloDe(pic)
            'solo para mirar
            formulario.PermiteCambios = False
            formulario.ShowDialog()
        End Using
    End Sub

    Private Sub btnAbrir_Click(sender As Object, e As EventArgs) Handles btnAbrir.Click
        'abro la gestion de la orden que se ve, en una ventana aparte: ahi se inicia, se cargan las horas y se finaliza
        If idOrden = 0 Then
            MessageBox.Show("Debe seleccionar una orden de la lista")
            Exit Sub
        End If

        Using formulario As New FrmOrdenGestion
            'le indico al formulario que orden tiene que cargar; el vuelve a comprobar que sea del mecanico
            formulario.IdOrdenTrabajo = idOrden
            formulario.ShowDialog()
        End Using

        'al cerrar la gestion la orden pudo cambiar de estado: miro en que grupo quedo
        Dim grupoDeLaOrden As Integer = 0
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'si la orden ya no es del mecanico no devuelve nada, y deja de verse
                Dim consulta As String =
                    "SELECT e.codigo FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.id_orden_trabajo = @id_orden_trabajo AND ot.id_mecanico = @id_mecanico;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", idOrden)
                    cmd.Parameters.AddWithValue("@id_mecanico", Sesion.IdMecanico)
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado IsNot Nothing Then grupoDeLaOrden = GrupoDeEstado(resultado.ToString())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al leer el estado de la orden: " & ex.Message)
        End Try

        'si se inicio o se finalizo, paso al grupo donde quedo para seguir viendola
        If grupoDeLaOrden > 0 Then grupoElegido = grupoDeLaOrden

        'recargo las cantidades y la lista; la orden queda marcada si sigue en el grupo elegido
        CargarGrupos()
        CargarOrdenes()

        'vuelvo a mostrar su resumen con los datos de ahora: estado, horas y boton
        Dim fila As DataGridViewRow = MarcarFila(idOrden)
        If fila IsNot Nothing Then MostrarOrden(fila)
    End Sub

End Class

Imports MySqlConnector

Public Class FrmOrdenGestion

    'orden que se gestiona, la carga el tablero antes de abrir el formulario
    Public IdOrdenTrabajo As Integer = 0

    'datos del estado actual de la orden, se vuelven a leer en cada carga
    Private codigoEstado As String = ""
    Private permiteEdicion As Boolean = False
    Private esEstadoFinal As Boolean = True

    'mecanico que la orden tiene guardado, 0 si no tiene
    Private idMecanicoOrden As Integer = 0

    'etapa mas avanzada del flujo a la que llego la orden (1 a 6), sale del historial
    Private etapaAlcanzada As Integer = 0
    'motivo con el que se rechazo o anulo la orden, sale del historial
    Private motivoCierre As String = ""

    'queda en True mientras se cargan los datos, para no tomar esos cambios como del usuario
    Private cargando As Boolean = False
    'queda en True cuando hay cantidades, horas u observaciones escritas y todavia sin guardar
    Private avanceSinGuardar As Boolean = False

    'fuente del primer renglon de cada fila del historial
    Private fuenteNegrita As Font

    'iconos que cambian segun el estado o la linea
    Private iconoCheckVerde As Image
    Private iconoPendiente As Image
    Private iconoReloj As Image
    Private iconoMecanico As Image
    Private iconoPresupuestar As Image
    Private iconoConfirmar As Image
    Private iconoIniciar As Image
    Private iconoFinalizar As Image
    Private iconoEntregar As Image
    Private iconoRechazar As Image
    Private iconoGuardar As Image

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

    Function OrdenEtapa(codigo As String) As Integer
        'lugar de cada estado en la barra de etapas, 0 para los que no estan en la barra
        If codigo = "RECEPCIONADA" Then Return 1
        If codigo = "PRESUPUESTADA" Then Return 2
        If codigo = "APROBADA" Then Return 3
        If codigo = "EN_PROCESO" Then Return 4
        If codigo = "FINALIZADA" Then Return 5
        If codigo = "ENTREGADA" Then Return 6
        Return 0
    End Function

    Sub CargarIconos()
        'cargo los iconos, si alguno falta ese control queda sin icono y el sistema sigue
        iconoCheckVerde = LeerIcono("check-verde.png")
        iconoPendiente = LeerIcono("pendiente-gris.png")
        iconoReloj = LeerIcono("reloj-ambar.png")
        iconoMecanico = LeerIcono("mecanico-oscuro.png")
        iconoPresupuestar = LeerIcono("presupuestar.png")
        iconoConfirmar = LeerIcono("confirmar.png")
        iconoIniciar = LeerIcono("iniciar.png")
        iconoFinalizar = LeerIcono("finalizar.png")
        iconoEntregar = LeerIcono("entregar.png")
        iconoRechazar = LeerIcono("rechazar-rojo.png")
        iconoGuardar = LeerIcono("guardar-oscuro.png")

        btnAgregar.Image = LeerIcono("agregar.png")
        btnActualizar.Image = LeerIcono("editar-oscuro.png")
        btnQuitar.Image = LeerIcono("quitar-oscuro.png")
        btnGuardarMecanico.Image = LeerIcono("guardar-oscuro.png")
        btnAnular.Image = LeerIcono("anular-rojo.png")

        picMecanico.Image = iconoMecanico
        picKm.Image = LeerIcono("km-oscuro.png")
        picRecepcion.Image = LeerIcono("calendario-oscuro.png")
        picFinalizacion.Image = LeerIcono("check-oscuro.png")
        picEntrega.Image = LeerIcono("vehiculo-oscuro.png")
        picSintoma.Image = LeerIcono("sintoma-oscuro.png")
        picHistorial.Image = LeerIcono("historial-oscuro.png")
    End Sub

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
        'cargo la grilla con las lineas de la orden y la acomodo segun la etapa
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_ot_detalle, descripcion, cantidad, precio_unitario, subtotal, aprobado, " &
                    "horas_reales " &
                    "FROM ot_detalle " &
                    "WHERE id_orden_trabajo = @id_orden_trabajo "

                'desde que el trabajo se inicia solo cuentan las lineas que aprobo el cliente
                If codigoEstado = "EN_PROCESO" OrElse codigoEstado = "FINALIZADA" OrElse codigoEstado = "ENTREGADA" Then
                    consulta = consulta & "AND aprobado = 1 "
                End If

                consulta = consulta & "ORDER BY id_ot_detalle;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'las columnas de la grilla estan declaradas en el disenador, cada una con su campo
                    dgvDetalle.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las líneas de la orden: " & ex.Message)
        End Try

        'en proceso y en los resumenes se ven las horas trabajadas de cada linea
        Dim conEjecucion As Boolean =
            (codigoEstado = "EN_PROCESO" OrElse codigoEstado = "FINALIZADA" OrElse codigoEstado = "ENTREGADA")

        'la marca de listo o pendiente es solo para cargar las horas
        colMarca.Visible = (codigoEstado = "EN_PROCESO")

        'con el trabajo en proceso los importes no hacen falta, se cargan las horas
        colPrecio.Visible = (codigoEstado <> "EN_PROCESO")
        colSubtotal.Visible = (codigoEstado <> "EN_PROCESO")

        'la aprobacion por linea se ve mientras se decide y en las ordenes que no siguieron
        colAprobado.Visible = (codigoEstado = "PRESUPUESTADA" OrElse codigoEstado = "APROBADA" OrElse
                               codigoEstado = "RECHAZADA" OrElse codigoEstado = "ANULADA")

        'las horas se ven desde que el trabajo se inicia, y en una orden anulada por si llegaron a cargarse
        colHoras.Visible = (conEjecucion OrElse codigoEstado = "ANULADA")

        'en la grilla se edita solo la aprobacion (presupuestada) o las horas (en proceso)
        dgvDetalle.ReadOnly = Not (codigoEstado = "PRESUPUESTADA" OrElse codigoEstado = "EN_PROCESO")
        colMarca.ReadOnly = True
        colDescripcion.ReadOnly = True
        colCantidad.ReadOnly = True
        colPrecio.ReadOnly = True
        colSubtotal.ReadOnly = True
        colAprobado.ReadOnly = (codigoEstado <> "PRESUPUESTADA")
        colHoras.ReadOnly = (codigoEstado <> "EN_PROCESO")
    End Sub

    Sub CargarHistorial()
        'cargo la lista con los cambios de estado de la orden, el mas nuevo primero
        etapaAlcanzada = 0
        motivoCierre = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'titulo es el primer renglon de cada fila de la lista y detalle el segundo
                Dim consulta As String =
                    "SELECT e.descripcion AS titulo, " &
                    "CONCAT(DATE_FORMAT(h.fecha_hora, '%d/%m/%Y %H:%i'), ' · ', u.nombre_completo, " &
                    "COALESCE(CONCAT(' · ', h.observacion), '')) AS detalle, " &
                    "e.codigo, h.observacion " &
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

                    For Each registro As DataRow In tabla.Rows
                        'la etapa mas avanzada por la que paso la orden marca la barra de etapas
                        Dim etapa As Integer = OrdenEtapa(registro("codigo").ToString())
                        If etapa > etapaAlcanzada Then etapaAlcanzada = etapa

                        'la fila mas nueva del estado actual trae el motivo del rechazo o de la anulacion
                        If motivoCierre = "" AndAlso registro("codigo").ToString() = codigoEstado Then
                            motivoCierre = registro("observacion").ToString()
                        End If
                    Next

                    dgvHistorial.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar el historial de estados: " & ex.Message)
        End Try
    End Sub

    Sub PintarEtapa(celda As Panel, marca As Label, nombre As Label, etapa As Integer)
        'dibujo una celda de la barra: etapa cumplida, etapa actual o etapa que falta
        Dim etapaActual As Integer = OrdenEtapa(codigoEstado)

        marca.Image = Nothing
        marca.Text = ""

        If etapaActual > 0 AndAlso etapa = etapaActual Then
            'etapa actual: resaltada en oscuro, con su numero
            celda.BackColor = Color.FromArgb(30, 39, 46)
            marca.ForeColor = Color.White
            nombre.ForeColor = Color.White
            marca.Text = etapa.ToString()
        ElseIf etapa <= etapaAlcanzada Then
            'etapa cumplida: con la marca verde
            celda.BackColor = Color.White
            marca.ForeColor = Color.SeaGreen
            nombre.ForeColor = Color.FromArgb(30, 39, 46)
            If iconoCheckVerde IsNot Nothing Then
                marca.Image = iconoCheckVerde
            Else
                marca.Text = "✓"
            End If
        Else
            'etapa que falta: apagada
            celda.BackColor = Color.White
            marca.ForeColor = Color.Silver
            nombre.ForeColor = Color.Silver
            If iconoPendiente IsNot Nothing Then
                marca.Image = iconoPendiente
            Else
                marca.Text = etapa.ToString()
            End If
        End If
    End Sub

    Sub MostrarEtapa()
        'acomodo la barra de etapas, el area de trabajo y los botones del pie segun el estado de la orden

        PintarEtapa(pnlEtapa1, lblMarcaEtapa1, lblNomEtapa1, 1)
        PintarEtapa(pnlEtapa2, lblMarcaEtapa2, lblNomEtapa2, 2)
        PintarEtapa(pnlEtapa3, lblMarcaEtapa3, lblNomEtapa3, 3)
        PintarEtapa(pnlEtapa4, lblMarcaEtapa4, lblNomEtapa4, 4)
        PintarEtapa(pnlEtapa5, lblMarcaEtapa5, lblNomEtapa5, 5)
        PintarEtapa(pnlEtapa6, lblMarcaEtapa6, lblNomEtapa6, 6)

        'una orden rechazada o anulada lo dice debajo de la barra, con su motivo si lo tiene
        lblCierre.Text = ""
        If codigoEstado = "RECHAZADA" Then lblCierre.Text = "Orden rechazada"
        If codigoEstado = "ANULADA" Then lblCierre.Text = "Orden anulada"
        If lblCierre.Text <> "" AndAlso motivoCierre <> "" Then
            lblCierre.Text = lblCierre.Text & ": " & motivoCierre
        End If

        'partes del area de trabajo: cada etapa muestra solo las que usa
        pnlEditor.Visible = permiteEdicion
        btnActualizar.Visible = permiteEdicion
        btnQuitar.Visible = permiteEdicion
        pnlAvisoMecanico.Visible = (codigoEstado = "APROBADA")
        pnlNotas.Visible = (codigoEstado = "EN_PROCESO" OrElse codigoEstado = "FINALIZADA" OrElse
                            codigoEstado = "ENTREGADA" OrElse codigoEstado = "ANULADA")
        txtNotasTecnicas.ReadOnly = (codigoEstado <> "EN_PROCESO")
        lblTotalAprobado.Visible = (codigoEstado <> "RECEPCIONADA")

        'el mecanico se cambia mientras el estado no sea final, despues solo se lee
        cboMecanico.Visible = Not esEstadoFinal
        btnGuardarMecanico.Visible = Not esEstadoFinal
        lblMecanico.Visible = esEstadoFinal

        'en el pie hay un solo boton principal, con el nombre del proximo paso
        lblProximo.Visible = Not esEstadoFinal
        btnSecundario.Visible = False
        btnSecundario.ForeColor = Color.Black
        btnPrimario.Image = Nothing

        If codigoEstado = "RECEPCIONADA" Then
            lblEtapaTitulo.Text = "Armar el presupuesto"
            lblEtapaAyuda.Text = "Agregá los servicios que necesita el vehículo y, cuando esté completo, presupuestá la orden."
            btnPrimario.Text = " Presupuestar"
            btnPrimario.Image = iconoPresupuestar
        End If

        If codigoEstado = "PRESUPUESTADA" Then
            lblEtapaTitulo.Text = "Aprobación del cliente"
            lblEtapaAyuda.Text = "Tildá las líneas que el cliente aprueba. Todavía podés ajustar el presupuesto."
            btnPrimario.Text = " Registrar aprobación"
            btnPrimario.Image = iconoConfirmar
            btnSecundario.Visible = True
            btnSecundario.Text = " Rechazar"
            btnSecundario.Image = iconoRechazar
            btnSecundario.ForeColor = Color.Firebrick
        End If

        If codigoEstado = "APROBADA" Then
            lblEtapaTitulo.Text = "Listo para iniciar"
            lblEtapaAyuda.Text = "El cliente aprobó el presupuesto. Revisá el mecánico asignado e iniciá el trabajo."
            btnPrimario.Text = " Iniciar trabajo"
            btnPrimario.Image = iconoIniciar

            'el mecanico es obligatorio para iniciar: lo digo claro, y aviso si falta
            If idMecanicoOrden = 0 Then
                lblAvisoMecanico.Text = "Falta asignar un mecánico: elegilo en ""Datos de la orden"" y guardalo."
                lblAvisoMecanico.ForeColor = Color.Firebrick
                picAvisoMecanico.Image = iconoReloj
            Else
                lblAvisoMecanico.Text = "Mecánico asignado: " & cboMecanico.Text
                lblAvisoMecanico.ForeColor = Color.FromArgb(30, 39, 46)
                picAvisoMecanico.Image = iconoMecanico
            End If
        End If

        If codigoEstado = "EN_PROCESO" Then
            lblEtapaTitulo.Text = "Trabajo en proceso"
            lblEtapaAyuda.Text = "Cargá las horas trabajadas de cada servicio aprobado."
            btnPrimario.Text = " Finalizar trabajo"
            btnPrimario.Image = iconoFinalizar
            btnSecundario.Visible = True
            btnSecundario.Text = " Guardar avance"
            btnSecundario.Image = iconoGuardar
        End If

        If codigoEstado = "FINALIZADA" Then
            lblEtapaTitulo.Text = "Trabajo finalizado"
            lblEtapaAyuda.Text = "El vehículo está listo. Entregalo al cliente para cerrar la orden."
            btnPrimario.Text = " Entregar vehículo"
            btnPrimario.Image = iconoEntregar
        End If

        If esEstadoFinal Then
            lblEtapaTitulo.Text = "Resumen de la orden"
            lblEtapaAyuda.Text = "La orden está cerrada. Estos datos son solo de consulta."
            btnPrimario.Text = "Cerrar"
        End If

        'la anulacion es solo del administrador y vale en cualquier estado que no sea final
        btnAnular.Visible = (Sesion.Rol = "ADMINISTRADOR" AndAlso Not esEstadoFinal)

        ActualizarConteo()
    End Sub

    Sub ActualizarConteo()
        'actualizo el contador de la etapa: aprobadas (presupuestada) o sin horas cargadas (en proceso)
        lblConteo.Text = ""

        If codigoEstado = "PRESUPUESTADA" Then
            'cuento las lineas tildadas y sumo su importe, todavia sin guardar
            Dim aprobadas As Integer = 0
            Dim total As Decimal = 0D
            For Each fila As DataGridViewRow In dgvDetalle.Rows
                If Convert.ToBoolean(fila.Cells("colAprobado").Value) Then
                    aprobadas = aprobadas + 1
                    total = total + CDec(fila.Cells("colSubtotal").Value)
                End If
            Next
            lblConteo.Text = "Aprobadas " & aprobadas & " de " & dgvDetalle.Rows.Count
            lblTotalAprobado.Text = "Total aprobado: " & total.ToString("C2")
        End If

        If codigoEstado = "EN_PROCESO" Then
            'una linea esta lista cuando tiene cargadas sus horas trabajadas
            Dim faltan As Integer = 0
            For Each fila As DataGridViewRow In dgvDetalle.Rows
                If IsDBNull(fila.Cells("colHoras").Value) Then
                    faltan = faltan + 1
                End If
            Next
            If faltan = 0 Then
                lblConteo.Text = "Horas cargadas en " & dgvDetalle.Rows.Count & " de " & dgvDetalle.Rows.Count
            Else
                lblConteo.Text = "Faltan horas en " & faltan & " de " & dgvDetalle.Rows.Count
            End If
        End If
    End Sub

    Sub CargarOrden()
        'cargo los datos de la orden y muestro el area de trabajo de su estado actual
        'se llama al abrir el formulario y despues de cada guardado

        'si la orden estaba presupuestada, recuerdo las lineas tildadas para no perderlas al recargar
        Dim tildadas As New List(Of Integer)
        If codigoEstado = "PRESUPUESTADA" Then
            dgvDetalle.EndEdit()
            For Each fila As DataGridViewRow In dgvDetalle.Rows
                If Convert.ToBoolean(fila.Cells("colAprobado").Value) Then
                    tildadas.Add(CInt(fila.Cells("colIdDetalle").Value))
                End If
            Next
        End If

        cargando = True

        'hasta leer el estado, la orden se trata como cerrada: no se puede tocar nada
        codigoEstado = ""
        permiteEdicion = False
        esEstadoFinal = True
        idMecanicoOrden = 0

        Dim encontrada As Boolean = False

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
                    "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, " &
                    "e.codigo, e.descripcion AS estado, e.permite_edicion_detalle, e.es_estado_final " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = ot.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN cliente AS c ON c.id_cliente = ot.id_cliente " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "LEFT JOIN mecanico AS m ON m.id_mecanico = ot.id_mecanico " &
                    "WHERE ot.id_orden_trabajo = @id_orden_trabajo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If lector.Read() Then
                            encontrada = True

                            'cabecera: numero, patente y vehiculo, con el estado a la derecha
                            lblTitulo.Text = "Orden N.º " & lector("nro_orden").ToString() & " · " &
                                             lector("patente").ToString() & " " & lector("vehiculo").ToString()
                            lblEstado.Text = lector("estado").ToString()
                            lblEstado.BackColor = ColorEstado(lector("codigo").ToString())

                            'subtitulo: cliente y fecha prometida, que es opcional
                            Dim prometida As String = "sin fecha"
                            If Not IsDBNull(lector("fecha_prometida")) Then
                                prometida = CDate(lector("fecha_prometida")).ToString("dd/MM/yyyy")
                            End If
                            lblSubtitulo.Text = "Cliente: " & lector("cliente").ToString() &
                                                " · Entrega prometida: " & prometida

                            'datos de la orden, solo lectura
                            lblKm.Text = CInt(lector("km_ingreso")).ToString("N0") & " km"
                            lblRecepcion.Text = CDate(lector("fecha_recepcion")).ToString("dd/MM/yyyy HH:mm")
                            txtSintoma.Text = lector("sintoma_reportado").ToString()
                            txtObsRecepcion.Text = lector("observaciones_recepcion").ToString()
                            lblMecanico.Text = lector("mecanico").ToString()

                            'las fechas de cierre y de entrega se muestran recien cuando existen
                            Dim finalizada As Boolean = Not IsDBNull(lector("fecha_finalizacion"))
                            picFinalizacion.Visible = finalizada
                            lblFinalizacionTit.Visible = finalizada
                            lblFinalizacion.Visible = finalizada
                            If finalizada Then
                                lblFinalizacion.Text = CDate(lector("fecha_finalizacion")).ToString("dd/MM/yyyy HH:mm")
                            End If

                            Dim entregada As Boolean = Not IsDBNull(lector("fecha_entrega"))
                            picEntrega.Visible = entregada
                            lblEntregaTit.Visible = entregada
                            lblEntrega.Visible = entregada
                            If entregada Then
                                lblEntrega.Text = CDate(lector("fecha_entrega")).ToString("dd/MM/yyyy HH:mm")
                            End If

                            'la orden puede no tener mecanico asignado
                            If Not IsDBNull(lector("id_mecanico")) Then idMecanicoOrden = CInt(lector("id_mecanico"))

                            'observaciones del mecanico: se guardan con el avance y al finalizar
                            txtNotasTecnicas.Text = lector("observaciones_mecanico").ToString()

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
            If cboMecanico.DataSource IsNot Nothing Then cboMecanico.SelectedValue = idMecanicoOrden

            CargarDetalle()

            'vuelvo a tildar las lineas que estaban tildadas antes de recargar, si la orden sigue presupuestada
            If codigoEstado = "PRESUPUESTADA" Then
                For Each fila As DataGridViewRow In dgvDetalle.Rows
                    If tildadas.Contains(CInt(fila.Cells("colIdDetalle").Value)) Then
                        fila.Cells("colAprobado").Value = True
                    End If
                Next
            End If

            CargarHistorial()
        End If

        MostrarEtapa()

        'lo que se ve es lo que esta guardado
        avanceSinGuardar = False
        cargando = False
    End Sub

    Function LeerCodigoEstado(cn As MySqlConnection, transaccion As MySqlTransaction) As String
        'vuelvo a leer el estado de la orden dentro de la transaccion: otro puesto pudo cambiarlo
        'FOR UPDATE bloquea solo la fila de la orden hasta terminar, asi nadie le cambia el estado en el medio
        Dim idEstado As Integer = 0
        Dim consulta As String =
            "SELECT id_estado_ot FROM orden_trabajo WHERE id_orden_trabajo = @id_orden_trabajo FOR UPDATE;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
            Dim resultado As Object = cmd.ExecuteScalar()
            If resultado Is Nothing Then Return ""
            idEstado = Convert.ToInt32(resultado)
        End Using

        'el codigo se lee aparte, sin bloquear el catalogo de estados
        consulta = "SELECT codigo FROM estado_ot WHERE id_estado_ot = @id_estado_ot;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
            Dim resultado As Object = cmd.ExecuteScalar()
            If resultado Is Nothing Then Return ""
            Return resultado.ToString()
        End Using
    End Function

    Function PermiteEditarDetalle(cn As MySqlConnection, transaccion As MySqlTransaction) As Boolean
        'bloqueo la orden y miro si su estado actual deja editar el presupuesto
        Dim codigoActual As String = LeerCodigoEstado(cn, transaccion)

        Dim consulta As String = "SELECT permite_edicion_detalle FROM estado_ot WHERE codigo = @codigo;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@codigo", codigoActual)
            Dim resultado As Object = cmd.ExecuteScalar()
            If resultado Is Nothing Then Return False
            Return Convert.ToBoolean(resultado)
        End Using
    End Function

    Function EstadoEsFinal(cn As MySqlConnection, transaccion As MySqlTransaction, codigo As String) As Boolean
        'indica si un estado es final; un codigo que no existe se trata como final
        Dim consulta As String = "SELECT es_estado_final FROM estado_ot WHERE codigo = @codigo;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@codigo", codigo)
            Dim resultado As Object = cmd.ExecuteScalar()
            If resultado Is Nothing Then Return True
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

    Sub GuardarEjecucion(cn As MySqlConnection, transaccion As MySqlTransaction, notas As String)
        'guardo lo que esta en pantalla del trabajo en proceso: las horas de cada linea aprobada y las observaciones
        'lo usan "Guardar avance" y "Finalizar trabajo", cada uno dentro de su transaccion

        'solo se escriben las horas: ningun otro dato de la linea se toca
        Dim consulta As String =
            "UPDATE ot_detalle SET horas_reales = @horas_reales " &
            "WHERE id_ot_detalle = @id_ot_detalle AND id_orden_trabajo = @id_orden_trabajo AND aprobado = 1;"

        For Each fila As DataGridViewRow In dgvDetalle.Rows
            'una celda vacia se guarda como NULL, las demas con dos decimales
            Dim horasReales As Object = DBNull.Value
            If Not IsDBNull(fila.Cells("colHoras").Value) Then
                horasReales = Math.Round(CDec(fila.Cells("colHoras").Value), 2, MidpointRounding.AwayFromZero)
            End If

            Using cmd As New MySqlCommand(consulta, cn, transaccion)
                cmd.Parameters.AddWithValue("@horas_reales", horasReales)
                cmd.Parameters.AddWithValue("@id_ot_detalle", CInt(fila.Cells("colIdDetalle").Value))
                cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                cmd.ExecuteNonQuery()
            End Using
        Next

        'las observaciones vacias se guardan como NULL
        Dim observaciones As Object = DBNull.Value
        If notas <> "" Then observaciones = notas

        consulta = "UPDATE orden_trabajo SET observaciones_mecanico = @observaciones_mecanico " &
                   "WHERE id_orden_trabajo = @id_orden_trabajo;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@observaciones_mecanico", observaciones)
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

        CargarIconos()
        fuenteNegrita = New Font(dgvHistorial.Font, FontStyle.Bold)

        'las grillas usan las columnas declaradas en el disenador, no las arman solas
        dgvDetalle.AutoGenerateColumns = False
        dgvHistorial.AutoGenerateColumns = False

        'formato de las columnas numericas, alineadas a la derecha
        colCantidad.DefaultCellStyle.Format = "N2"
        colCantidad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        colPrecio.DefaultCellStyle.Format = "C2"
        colPrecio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        colSubtotal.DefaultCellStyle.Format = "C2"
        colSubtotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        colHoras.DefaultCellStyle.Format = "N2"
        colHoras.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'una linea sin marca no muestra el dibujo de imagen faltante
        colMarca.DefaultCellStyle.NullValue = Nothing

        'el historial es una lista de consulta: la fila elegida no se resalta
        dgvHistorial.DefaultCellStyle.SelectionBackColor = Color.White
        dgvHistorial.DefaultCellStyle.SelectionForeColor = Color.Black

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

    Private Sub FrmOrdenGestion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'si hay avance del trabajo sin guardar, aviso antes de cerrar para no perderlo
        If Not avanceSinGuardar Then Exit Sub

        Dim respuesta As DialogResult = MessageBox.Show(
            "Hay cantidades, horas u observaciones sin guardar." & vbCrLf &
            "¿Cerrar igual? Para conservarlas, elija No y use ""Guardar avance"".",
            "Avance sin guardar",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then e.Cancel = True
    End Sub

    Private Sub dgvDetalle_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvDetalle.DataBindingComplete
        'al cargar o reordenar la grilla no dejo ninguna linea marcada: hay que elegirla
        'al tildar una aprobacion la grilla tambien pasa por aca, ahi la seleccion no se toca
        If e.ListChangedType = System.ComponentModel.ListChangedType.Reset Then
            dgvDetalle.ClearSelection()
        End If
    End Sub

    Private Sub dgvDetalle_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDetalle.SelectionChanged
        'traigo la cantidad de la linea elegida con el mouse o con el teclado para poder cambiarla
        'al recargar la grilla el foco esta en otro control y el campo no se toca
        If Not permiteEdicion Then Exit Sub
        If Not dgvDetalle.Focused Then Exit Sub
        If dgvDetalle.SelectedRows.Count = 0 Then Exit Sub

        Dim cantidad As Decimal = CDec(dgvDetalle.SelectedRows(0).Cells("colCantidad").Value)
        If cantidad <= nudCantidad.Maximum Then nudCantidad.Value = cantidad
    End Sub

    Private Sub dgvDetalle_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDetalle.CellFormatting
        'la primera columna marca cada linea como lista o pendiente segun tenga cargadas sus horas
        If e.RowIndex < 0 Then Exit Sub
        If dgvDetalle.Columns(e.ColumnIndex) IsNot colMarca Then Exit Sub

        Dim fila As DataGridViewRow = dgvDetalle.Rows(e.RowIndex)
        If IsDBNull(fila.Cells("colHoras").Value) Then
            e.Value = iconoReloj
        Else
            e.Value = iconoCheckVerde
        End If
        e.FormattingApplied = True
    End Sub

    Private Sub dgvDetalle_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvDetalle.CurrentCellDirtyStateChanged
        'una tilde de aprobacion queda en la grilla apenas se hace, sin esperar a salir de la celda
        If dgvDetalle.CurrentCell Is Nothing Then Exit Sub
        If dgvDetalle.CurrentCell.OwningColumn IsNot colAprobado Then Exit Sub

        If dgvDetalle.IsCurrentCellDirty Then dgvDetalle.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub dgvDetalle_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgvDetalle.CellValidating
        'valido lo que se escribe en "Horas trabajadas" antes de que quede en la grilla
        If codigoEstado <> "EN_PROCESO" Then Exit Sub
        If Not dgvDetalle.IsCurrentCellDirty Then Exit Sub

        Dim columna As DataGridViewColumn = dgvDetalle.Columns(e.ColumnIndex)
        If columna IsNot colHoras Then Exit Sub

        'una celda vacia es valida: la linea queda pendiente
        Dim texto As String = e.FormattedValue.ToString().Trim
        If texto = "" Then Exit Sub

        'el separador de miles no se acepta: con la configuracion regional en español
        'un "1.5" se leeria como 15 sin avisar
        Dim formato As Globalization.NumberFormatInfo = Globalization.CultureInfo.CurrentCulture.NumberFormat

        Dim aviso As String = ""
        Dim valor As Decimal
        If formato.NumberGroupSeparator <> "" AndAlso texto.Contains(formato.NumberGroupSeparator) Then
            aviso = "Escriba el número sin separador de miles. Para los decimales use """ &
                    formato.NumberDecimalSeparator & """."
        ElseIf Not Decimal.TryParse(texto, valor) Then
            aviso = "Ingrese un número, con hasta dos decimales."
        ElseIf valor < 0 Then
            aviso = "El valor no puede ser negativo."
        ElseIf valor > 999.99D Then
            aviso = "Las horas trabajadas no pueden superar 999,99."
        End If

        If aviso <> "" Then
            'rechazo lo escrito: la celda vuelve al valor que tenia
            MessageBox.Show(aviso)
            dgvDetalle.CancelEdit()
            e.Cancel = True
        End If
    End Sub

    Private Sub dgvDetalle_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvDetalle.DataError
        'la grilla no pudo convertir lo escrito al tipo de la columna: aviso y dejo el valor anterior
        MessageBox.Show("El valor escrito no es válido para esa columna. Se conserva el valor anterior.")
        dgvDetalle.CancelEdit()
        e.ThrowException = False
    End Sub

    Private Sub dgvDetalle_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalle.CellValueChanged
        'al tildar una aprobacion o cargar las horas actualizo el contador de la etapa
        If cargando Then Exit Sub
        If e.RowIndex < 0 Then Exit Sub

        Dim columna As DataGridViewColumn = dgvDetalle.Columns(e.ColumnIndex)

        If codigoEstado = "EN_PROCESO" AndAlso columna Is colHoras Then
            'hay avance escrito que todavia no se guardo
            avanceSinGuardar = True
            'vuelvo a dibujar la fila para que cambie su marca de lista o pendiente
            dgvDetalle.InvalidateRow(e.RowIndex)
        End If

        ActualizarConteo()
    End Sub

    Private Sub txtNotasTecnicas_TextChanged(sender As Object, e As EventArgs) Handles txtNotasTecnicas.TextChanged
        'las observaciones escritas con el trabajo en proceso cuentan como avance sin guardar
        If cargando Then Exit Sub
        If codigoEstado = "EN_PROCESO" Then avanceSinGuardar = True
    End Sub

    Private Sub dgvHistorial_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvHistorial.DataBindingComplete
        'el historial es solo de consulta, no dejo ninguna fila marcada
        dgvHistorial.ClearSelection()

        'dejo a la vista la primera fila, que es el cambio de estado mas nuevo
        If dgvHistorial.Rows.Count > 0 AndAlso dgvHistorial.DisplayedRowCount(True) > 0 Then
            dgvHistorial.FirstDisplayedScrollingRowIndex = 0
        End If
    End Sub

    Private Sub dgvHistorial_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvHistorial.CellPainting
        'cada fila del historial se dibuja en dos renglones: el estado, y debajo fecha, usuario y nota en gris
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

        Dim vista As DataRowView = TryCast(dgvHistorial.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If vista Is Nothing Then Exit Sub

        e.PaintBackground(e.CellBounds, False)

        Dim mitad As Integer = e.CellBounds.Height \ 2
        Dim renglon1 As New Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Width - 4, mitad - 2)
        Dim renglon2 As New Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + mitad, e.CellBounds.Width - 4, mitad - 2)
        Dim formato As TextFormatFlags = TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis

        TextRenderer.DrawText(e.Graphics, vista("titulo").ToString(), fuenteNegrita, renglon1, Color.FromArgb(30, 39, 46), formato)
        TextRenderer.DrawText(e.Graphics, vista("detalle").ToString(), dgvHistorial.Font, renglon2, Color.Gray, formato)

        e.Handled = True
    End Sub

    Private Sub dgvHistorial_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles dgvHistorial.CellToolTipTextNeeded
        'el texto completo de la fila se ve al pasar el mouse, por si la nota no entra en el ancho
        If e.RowIndex < 0 Then Exit Sub

        Dim vista As DataRowView = TryCast(dgvHistorial.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If vista Is Nothing Then Exit Sub

        e.ToolTipText = vista("titulo").ToString() & vbCrLf & vista("detalle").ToString()
    End Sub

    Private Sub btnPrimario_Click(sender As Object, e As EventArgs) Handles btnPrimario.Click
        'el boton principal hace el proximo paso de la orden, segun su estado
        If esEstadoFinal Then
            Me.Close()
            Exit Sub
        End If

        'tomo el estado que se ve en pantalla al hacer clic: cada paso recarga la orden y cambia
        'codigoEstado, y un clic tiene que hacer un solo paso, nunca encadenar el siguiente
        Dim estadoAlClic As String = codigoEstado

        If estadoAlClic = "RECEPCIONADA" Then
            Presupuestar()
        ElseIf estadoAlClic = "PRESUPUESTADA" Then
            RegistrarAprobacion()
        ElseIf estadoAlClic = "APROBADA" Then
            IniciarTrabajo()
        ElseIf estadoAlClic = "EN_PROCESO" Then
            FinalizarTrabajo()
        ElseIf estadoAlClic = "FINALIZADA" Then
            EntregarVehiculo()
        End If
    End Sub

    Private Sub btnSecundario_Click(sender As Object, e As EventArgs) Handles btnSecundario.Click
        'el boton secundario es la otra accion de la etapa: rechazar el presupuesto o guardar el avance
        'igual que en el principal, un clic hace una sola accion
        Dim estadoAlClic As String = codigoEstado

        If estadoAlClic = "PRESUPUESTADA" Then
            RechazarPresupuesto()
        ElseIf estadoAlClic = "EN_PROCESO" Then
            GuardarAvance()
        End If
    End Sub

    Private Sub btnGuardarMecanico_Click(sender As Object, e As EventArgs) Handles btnGuardarMecanico.Click
        'guardo el mecanico elegido en la orden

        'si el combo no se pudo cargar no hay nada confiable para guardar
        If cboMecanico.DataSource Is Nothing OrElse cboMecanico.SelectedValue Is Nothing Then
            MessageBox.Show("No se pudo leer el mecánico elegido. Cierre la ventana y vuelva a abrir la orden.")
            Exit Sub
        End If

        'guardar el mecanico recarga la orden: antes hay que guardar el avance escrito
        If avanceSinGuardar Then
            MessageBox.Show("Hay cantidades, horas u observaciones sin guardar. Use ""Guardar avance"" antes de cambiar el mecánico.")
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
                    Dim codigoActual As String = LeerCodigoEstado(cn, transaccion)

                    'en un estado final ya no se cambia el mecanico
                    If EstadoEsFinal(cn, transaccion, codigoActual) Then
                        aviso = "La orden ya está en un estado final y no se puede cambiar su mecánico."
                    End If

                    'desde que el trabajo se inicia la orden no puede quedar sin mecanico
                    If aviso = "" AndAlso idElegido = 0 Then
                        If codigoActual = "EN_PROCESO" OrElse codigoActual = "FINALIZADA" Then
                            aviso = "El trabajo ya se inició: la orden no puede quedar sin mecánico."
                        End If
                    End If

                    If aviso = "" Then
                        Dim consulta As String = "UPDATE orden_trabajo SET id_mecanico = @id_mecanico " &
                                                 "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_mecanico", idMecanico)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
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
        Dim idDetalle As Integer = CInt(dgvDetalle.SelectedRows(0).Cells("colIdDetalle").Value)

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
        Dim idDetalle As Integer = CInt(fila.Cells("colIdDetalle").Value)

        'pido confirmacion antes de quitar
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Quitar del presupuesto la línea """ & fila.Cells("colDescripcion").Value.ToString() & """?",
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
            MessageBox.Show("No se pudo quitar la línea y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then MessageBox.Show(aviso)

        'vuelvo a cargar la orden con sus lineas y totales actuales
        CargarOrden()
    End Sub

    Sub Presupuestar()
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

    Sub RegistrarAprobacion()
        'registro la aprobacion del cliente: paso la orden de PRESUPUESTADA a APROBADA
        'con las lineas que quedaron tildadas en la grilla

        'cierro la edicion de la celda para que la ultima tilde quede en la grilla
        dgvDetalle.EndEdit()

        'cuento las lineas aprobadas y sumo su importe
        Dim lineasAprobadas As Integer = 0
        Dim totalAprobado As Decimal = 0D
        For Each fila As DataGridViewRow In dgvDetalle.Rows
            If Convert.ToBoolean(fila.Cells("colAprobado").Value) Then
                lineasAprobadas = lineasAprobadas + 1
                totalAprobado = totalAprobado + CDec(fila.Cells("colSubtotal").Value)
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
                                cmd.Parameters.AddWithValue("@aprobado", Convert.ToBoolean(fila.Cells("colAprobado").Value))
                                cmd.Parameters.AddWithValue("@id_ot_detalle", CInt(fila.Cells("colIdDetalle").Value))
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

    Sub RechazarPresupuesto()
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

    Sub IniciarTrabajo()
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

    Sub GuardarAvance()
        'guardo lo cargado hasta ahora del trabajo en proceso: las horas de cada linea y las observaciones
        'no cambia el estado de la orden ni deja fila en el historial

        'cierro la edicion de la celda para que el ultimo valor escrito quede en la grilla
        If Not dgvDetalle.EndEdit() Then Exit Sub

        Dim notas As String = txtNotasTecnicas.Text.Trim
        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'las lineas y las observaciones se guardan juntas o ninguna
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If LeerCodigoEstado(cn, transaccion) <> "EN_PROCESO" Then
                        aviso = "La orden ya no está en proceso, no se puede guardar el avance."
                    End If

                    If aviso = "" Then GuardarEjecucion(cn, transaccion, notas)
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
            MessageBox.Show("No se pudo guardar el avance y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Avance guardado.")
        End If

        'vuelvo a cargar la orden con lo que quedo guardado
        CargarOrden()
    End Sub

    Sub FinalizarTrabajo()
        'cierre tecnico: guardo el avance y paso la orden de EN_PROCESO a FINALIZADA, todo junto

        'cierro la edicion de la celda para que el ultimo valor escrito quede en la grilla
        If Not dgvDetalle.EndEdit() Then Exit Sub

        'toda linea aprobada tiene que tener cargadas sus horas trabajadas
        'lo compruebo antes de guardar, asi lo ya escrito sigue en pantalla
        Dim faltantes As Integer = 0
        For Each fila As DataGridViewRow In dgvDetalle.Rows
            If IsDBNull(fila.Cells("colHoras").Value) Then
                faltantes = faltantes + 1
            End If
        Next
        If faltantes > 0 Then
            MessageBox.Show("Falta cargar las horas trabajadas en " & faltantes &
                            " línea(s) aprobada(s). Complete la columna ""Horas trabajadas"".")
            dgvDetalle.Focus()
            Exit Sub
        End If

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

                'las horas de cada linea, las observaciones, la fecha, el estado y el historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    If LeerCodigoEstado(cn, transaccion) <> "EN_PROCESO" Then
                        aviso = "La orden ya no está en proceso, no se puede finalizar."
                    End If

                    Dim consulta As String

                    If aviso = "" Then
                        'el mismo guardado que hace "Guardar avance"
                        GuardarEjecucion(cn, transaccion, notas)

                        'vuelvo a comprobar en la base que no quede ninguna linea aprobada sin sus horas
                        consulta = "SELECT COUNT(*) FROM ot_detalle " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo AND aprobado = 1 " &
                                   "AND horas_reales IS NULL;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            Dim sinEjecucion As Integer = CInt(cmd.ExecuteScalar())
                            If sinEjecucion > 0 Then
                                aviso = "Falta cargar las horas trabajadas en " & sinEjecucion &
                                        " línea(s) aprobada(s). Se recargó la orden: revísela y vuelva a intentar."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'la fecha de finalizacion es la fecha y hora del servidor
                        consulta = "UPDATE orden_trabajo SET fecha_finalizacion = NOW() " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.ExecuteNonQuery()
                        End Using

                        CambiarEstado(cn, transaccion, "FINALIZADA", "Trabajo finalizado")
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

    Sub EntregarVehiculo()
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

        'el motivo es obligatorio y queda en el historial: lo pide una ventana aparte, que tambien confirma
        Dim motivo As String = ""
        Using formulario As New FrmAnularOrden
            formulario.Orden = lblTitulo.Text
            If formulario.ShowDialog() <> DialogResult.OK Then Exit Sub
            motivo = formulario.Motivo
        End Using

        Dim aviso As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'el cambio de estado y su fila de historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el estado de la orden: otro puesto pudo cerrarla
                    Dim codigoActual As String = LeerCodigoEstado(cn, transaccion)

                    If EstadoEsFinal(cn, transaccion, codigoActual) Then
                        aviso = "La orden ya está en un estado final y no se puede anular."
                    End If

                    'no se borra ninguna fila: la orden, sus lineas y su historial se conservan
                    If aviso = "" Then CambiarEstado(cn, transaccion, "ANULADA", motivo)
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
            MessageBox.Show("No se pudo anular la orden y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Orden anulada.")
        End If

        'lo que estaba escrito y sin guardar ya no cuenta: la orden quedo cerrada o cambio desde otro puesto
        avanceSinGuardar = False

        'vuelvo a cargar la orden con su estado actual
        CargarOrden()
    End Sub

End Class

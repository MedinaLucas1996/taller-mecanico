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

    'queda en True cuando quien abre la orden es un mecanico: ve solo el trabajo, sin importes,
    'y puede iniciar, cargar horas y finalizar unicamente sus ordenes; se decide una vez, en Load
    Private modoMecanico As Boolean = False

    'etapa mas avanzada del flujo a la que llego la orden (1 a 6), sale del historial
    Private etapaAlcanzada As Integer = 0
    'motivo con el que se rechazo o anulo la orden, sale del historial
    Private motivoCierre As String = ""

    'queda en True mientras se cargan los datos, para no tomar esos cambios como del usuario
    Private cargando As Boolean = False
    'queda en True cuando hay cantidades, horas u observaciones escritas y todavia sin guardar
    Private avanceSinGuardar As Boolean = False
    'queda en True cuando el mecanico escribio el diagnostico y todavia no lo guardo
    Private diagnosticoSinGuardar As Boolean = False

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

    'datos del reporte de la orden: se leen al pedir la impresion y el evento PrintPage solo dibuja
    Private datosImpresion As DataRow
    Private lineasImpresion As DataTable
    Private historialImpresion As DataTable
    Private fechaImpresion As DateTime

    'por donde va la impresion: hoja, seccion y fila de la tabla; se reinician en BeginPrint
    Private hojaImpresa As Integer = 0
    Private seccionImpresa As Integer = 0
    Private filaImpresa As Integer = 0

    'reporte que se esta imprimiendo, y si ya se aviso un error al dibujarlo
    Private reporteImpreso As Integer = 0
    Private errorImpresion As Boolean = False

    'fotos de la recepcion para el acta, por angulo; se liberan al volver a leerlas y al cerrar
    Private fotosImpresion As New Dictionary(Of String, Image)

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
        btnImprimir.Image = LeerIcono("reportes.png")
        btnAvisar.Image = LeerIcono("cliente-oscuro.png")
        btnGuardarDiagnostico.Image = LeerIcono("guardar-oscuro.png")

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
        'el mecanico no ve importes en ninguna etapa
        colPrecio.Visible = (codigoEstado <> "EN_PROCESO" AndAlso Not modoMecanico)
        colSubtotal.Visible = (codigoEstado <> "EN_PROCESO" AndAlso Not modoMecanico)

        'la aprobacion por linea se ve mientras se decide y en las ordenes que no siguieron
        colAprobado.Visible = (codigoEstado = "PRESUPUESTADA" OrElse codigoEstado = "APROBADA" OrElse
                               codigoEstado = "RECHAZADA" OrElse codigoEstado = "ANULADA")

        'las horas se ven desde que el trabajo se inicia, y en una orden anulada por si llegaron a cargarse
        colHoras.Visible = (conEjecucion OrElse codigoEstado = "ANULADA")

        'en la grilla se edita solo la aprobacion (presupuestada) o las horas (en proceso)
        'el mecanico no registra la aprobacion: para el la grilla solo se edita en proceso
        dgvDetalle.ReadOnly = Not ((codigoEstado = "PRESUPUESTADA" AndAlso Not modoMecanico) OrElse codigoEstado = "EN_PROCESO")
        colMarca.ReadOnly = True
        colDescripcion.ReadOnly = True
        colCantidad.ReadOnly = True
        colPrecio.ReadOnly = True
        colSubtotal.ReadOnly = True
        colAprobado.ReadOnly = (codigoEstado <> "PRESUPUESTADA" OrElse modoMecanico)
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

        'el diagnostico del mecanico se lee encima de las lineas mientras se arma el presupuesto,
        'y mientras el cliente lo decide si esta cargado; aca es solo de lectura, lo escribe el mecanico
        pnlDiagnostico.Visible = (codigoEstado = "RECEPCIONADA" OrElse
                                  (codigoEstado = "PRESUPUESTADA" AndAlso txtDiagnostico.Text.Trim <> ""))
        txtDiagnostico.ReadOnly = True
        txtDiagnostico.PlaceholderText = "El mecánico todavía no cargó el diagnóstico."
        btnGuardarDiagnostico.Visible = False

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
        btnAnular.Visible = (Sesion.Rol = ROL_ADMINISTRADOR AndAlso Not esEstadoFinal)

        'el aviso al cliente sale cuando el trabajo esta finalizado; lo dan el administrador y el operador
        btnAvisar.Visible = (codigoEstado = "FINALIZADA" AndAlso
                             (Sesion.Rol = ROL_ADMINISTRADOR OrElse Sesion.Rol = ROL_OPERADOR))

        '"Anular" va pegado a "Imprimir" salvo cuando el aviso al cliente ocupa ese lugar: asi en el ancho minimo
        'de la ventana no se pisa con el texto del proximo paso, que esta anclado a la derecha
        If btnAvisar.Visible Then
            btnAnular.Left = btnAvisar.Right + 8
        Else
            btnAnular.Left = btnAvisar.Left
        End If

        'el mecanico ve la misma pantalla con menos cosas: lo ajusto al final, sobre lo ya armado
        If modoMecanico Then MostrarEtapaMecanico()

        ActualizarConteo()
    End Sub

    Sub MostrarEtapaMecanico()
        'ajusto la etapa para el mecanico: sin importes, sin presupuesto, sin cambio de mecanico y sin anulacion
        'solo puede iniciar el trabajo (aprobada) y cargar horas, guardar y finalizar (en proceso)

        'nada de importes
        lblTotalPresupuestado.Visible = False
        lblTotalAprobado.Visible = False

        'el presupuesto no se toca
        pnlEditor.Visible = False
        btnActualizar.Visible = False
        btnQuitar.Visible = False

        'el mecanico asignado se lee, no se cambia
        cboMecanico.Visible = False
        btnGuardarMecanico.Visible = False
        lblMecanico.Visible = True
        btnAnular.Visible = False
        'al cliente le avisa la recepcion, no el mecanico
        btnAvisar.Visible = False

        If codigoEstado = "RECEPCIONADA" Then
            'antes del presupuesto el mecanico revisa el vehiculo y escribe su diagnostico
            lblEtapaTitulo.Text = "Diagnóstico del vehículo"
            lblEtapaAyuda.Text = "Revisá el vehículo y escribí lo que encontraste. La recepción arma el presupuesto con tu diagnóstico."
            txtDiagnostico.ReadOnly = False
            txtDiagnostico.PlaceholderText = "Qué se encontró al revisar el vehículo y qué hace falta reparar."
            btnGuardarDiagnostico.Visible = True
        End If

        If codigoEstado = "PRESUPUESTADA" Then
            lblEtapaTitulo.Text = "Esperando la aprobación del cliente"
            lblEtapaAyuda.Text = "El presupuesto está armado y falta que el cliente lo apruebe. Vas a poder iniciar el trabajo cuando lo haga."
        End If

        If codigoEstado = "APROBADA" Then
            lblEtapaAyuda.Text = "El cliente aprobó el presupuesto. Cuando empieces con el vehículo, iniciá el trabajo."
            'el combo de mecanicos no se carga en este modo: el nombre sale de los datos de la orden
            lblAvisoMecanico.Text = "Mecánico asignado: " & lblMecanico.Text
        End If

        If codigoEstado = "FINALIZADA" Then
            lblEtapaAyuda.Text = "El trabajo está terminado. La entrega del vehículo al cliente la registra la recepción."
        End If

        'fuera de aprobada y en proceso no hay ningun paso para el mecanico: el boton principal solo cierra
        If codigoEstado <> "APROBADA" AndAlso codigoEstado <> "EN_PROCESO" Then
            lblProximo.Visible = False
            btnSecundario.Visible = False
            btnPrimario.Image = Nothing
            btnPrimario.Text = "Cerrar"
        End If
    End Sub

    Sub ActualizarConteo()
        'actualizo el contador de la etapa: aprobadas (presupuestada) o sin horas cargadas (en proceso)
        lblConteo.Text = ""

        If codigoEstado = "PRESUPUESTADA" AndAlso Not modoMecanico Then
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
                    "ot.sintoma_reportado, ot.observaciones_recepcion, ot.observaciones_mecanico, ot.diagnostico, " &
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

                            'diagnostico del mecanico: es opcional y se escribe antes del presupuesto
                            txtDiagnostico.Text = lector("diagnostico").ToString()

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
        diagnosticoSinGuardar = False
        cargando = False
    End Sub

    Function OrdenEsDelMecanico(cn As MySqlConnection, transaccion As MySqlTransaction) As Boolean
        'indica si la orden esta asignada al mecanico que inicio la sesion
        'se lee de la base, nunca de lo que hay en pantalla: la orden pudo reasignarse desde otro puesto
        'dentro de una transaccion se llama despues de LeerCodigoEstado, que ya dejo bloqueada la fila
        If Sesion.IdMecanico = 0 Then Return False

        Dim consulta As String =
            "SELECT id_mecanico FROM orden_trabajo WHERE id_orden_trabajo = @id_orden_trabajo;"
        Using cmd As New MySqlCommand(consulta, cn, transaccion)
            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
            Dim resultado As Object = cmd.ExecuteScalar()
            If resultado Is Nothing OrElse IsDBNull(resultado) Then Return False
            Return Convert.ToInt32(resultado) = Sesion.IdMecanico
        End Using
    End Function

    Function RechazaMecanico() As Boolean
        'las acciones de la recepcion (presupuesto, aprobacion, entrega, mecanico asignado) no son del mecanico
        'sus botones estan ocultos en ese modo, pero cada accion lo vuelve a comprobar al empezar
        If Not modoMecanico Then Return False

        MessageBox.Show("Esta acción no está disponible para el mecánico.")
        Return True
    End Function

    Sub CerrarPorOrdenAjena()
        'la orden dejo de ser del mecanico mientras la tenia abierta: aviso y cierro, sin guardar nada
        MessageBox.Show("La orden ya no está asignada a usted: fue reasignada desde otro puesto." & vbCrLf &
                        "No se guardó ningún dato.")
        'lo escrito no se puede guardar, no tiene sentido preguntar por el al cerrar
        avanceSinGuardar = False
        diagnosticoSinGuardar = False
        Me.Close()
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
        'el administrador y el operador gestionan las ordenes; el mecanico entra en un modo propio,
        'solo para sus ordenes; el modo se decide aca, una sola vez
        If Sesion.Rol = ROL_MECANICO Then
            modoMecanico = True
        ElseIf Sesion.Rol <> ROL_ADMINISTRADOR AndAlso Sesion.Rol <> ROL_OPERADOR Then
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

        If modoMecanico Then
            'el mecanico abre solo las ordenes que tiene asignadas: lo compruebo en la base,
            'no con lo que diga la pantalla que abrio este formulario
            If Sesion.IdMecanico = 0 Then
                MessageBox.Show("Su usuario no tiene un mecánico asociado.")
                Me.Close()
                Exit Sub
            End If

            Dim esSuya As Boolean = False
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    esSuya = OrdenEsDelMecanico(cn, Nothing)
                End Using
            Catch ex As Exception
                MessageBox.Show("No se pudo comprobar la orden de trabajo: " & ex.Message)
                Me.Close()
                Exit Sub
            End Try

            If Not esSuya Then
                MessageBox.Show("Esta orden de trabajo no está asignada a usted.")
                Me.Close()
                Exit Sub
            End If
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
        'el mecanico no usa ninguno de los dos, y el de servicios muestra precios: en su modo no se cargan
        If Not modoMecanico Then
            CargarComboMecanicos()
            CargarComboServicios()
        End If
        CargarOrden()

        'sin estado leido la orden no existe o no se pudo cargar
        If codigoEstado = "" Then
            MessageBox.Show("No se pudo abrir la orden de trabajo seleccionada.")
            Me.Close()
        End If
    End Sub

    Private Sub FrmOrdenGestion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'si hay un diagnostico escrito sin guardar, aviso antes de cerrar para no perderlo
        If diagnosticoSinGuardar Then
            Dim respuestaDiagnostico As DialogResult = MessageBox.Show(
                "Hay un diagnóstico escrito sin guardar." & vbCrLf &
                "¿Cerrar igual? Para conservarlo, elija No y use ""Guardar diagnóstico"".",
                "Diagnóstico sin guardar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If respuestaDiagnostico = DialogResult.No Then e.Cancel = True
            Exit Sub
        End If

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

    Private Sub txtDiagnostico_TextChanged(sender As Object, e As EventArgs) Handles txtDiagnostico.TextChanged
        'lo que el mecanico escribe en el diagnostico queda sin guardar hasta usar su boton
        If cargando Then Exit Sub
        If modoMecanico AndAlso codigoEstado = "RECEPCIONADA" Then diagnosticoSinGuardar = True
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

        'para el mecanico el boton solo hace un paso en aprobada y en proceso; en el resto dice "Cerrar"
        If modoMecanico AndAlso codigoEstado <> "APROBADA" AndAlso codigoEstado <> "EN_PROCESO" Then
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

        'accion de la recepcion: un mecanico no la hace aunque llegue hasta aca
        If RechazaMecanico() Then Exit Sub

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

    Private Sub btnGuardarDiagnostico_Click(sender As Object, e As EventArgs) Handles btnGuardarDiagnostico.Click
        'guardo el diagnostico que escribio el mecanico; la orden sigue recepcionada
        'el boton solo lo ve el mecanico de la orden, igual lo compruebo
        If Not modoMecanico Then Exit Sub

        Dim diagnostico As String = txtDiagnostico.Text.Trim
        If diagnostico = "" Then
            MessageBox.Show("Escriba el diagnóstico antes de guardarlo")
            txtDiagnostico.Focus()
            Exit Sub
        End If

        Dim aviso As String = ""
        'queda en True cuando la orden dejo de ser del mecanico que la tiene abierta
        Dim ordenAjena As Boolean = False

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'el diagnostico y su fila de historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'leo el estado, lo que deja bloqueada la fila de la orden
                    Dim codigoActual As String = LeerCodigoEstado(cn, transaccion)

                    'un mecanico solo diagnostica sus ordenes: lo compruebo con la fila ya bloqueada
                    If Not OrdenEsDelMecanico(cn, transaccion) Then
                        ordenAjena = True
                        aviso = "ajena"
                    ElseIf codigoActual <> "RECEPCIONADA" Then
                        aviso = "La orden ya no está recepcionada: el diagnóstico ya no se puede cambiar."
                    End If

                    'leo el diagnostico guardado para saber si es el primero o una correccion
                    Dim anterior As String = ""
                    Dim consulta As String
                    If aviso = "" Then
                        consulta = "SELECT diagnostico FROM orden_trabajo WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            Dim resultado As Object = cmd.ExecuteScalar()
                            If resultado IsNot Nothing AndAlso Not IsDBNull(resultado) Then anterior = resultado.ToString().Trim
                        End Using

                        If anterior = diagnostico Then aviso = "El diagnóstico no tiene cambios para guardar."
                    End If

                    If aviso = "" Then
                        consulta = "UPDATE orden_trabajo SET diagnostico = @diagnostico " &
                                   "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@diagnostico", diagnostico)
                            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                            cmd.ExecuteNonQuery()
                        End Using

                        'dejo constancia en el historial con el estado actual de la orden, que no cambia
                        Dim observacion As String = "Diagnóstico cargado"
                        If anterior <> "" Then observacion = "Diagnóstico corregido"
                        consulta =
                            "INSERT INTO ot_historial_estado (id_orden_trabajo, id_estado_ot, id_usuario, observacion) " &
                            "SELECT id_orden_trabajo, id_estado_ot, @id_usuario, @observacion FROM orden_trabajo " &
                            "WHERE id_orden_trabajo = @id_orden_trabajo;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_usuario", Sesion.IdUsuario)
                            cmd.Parameters.AddWithValue("@observacion", observacion)
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
            MessageBox.Show("No se pudo guardar el diagnóstico y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        If ordenAjena Then
            CerrarPorOrdenAjena()
            Exit Sub
        End If

        If aviso <> "" Then
            MessageBox.Show(aviso)
        Else
            MessageBox.Show("Diagnóstico guardado.")
        End If

        'vuelvo a cargar la orden con lo que quedo guardado
        CargarOrden()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        'agrego un servicio al presupuesto de la orden

        'accion de la recepcion: un mecanico no la hace aunque llegue hasta aca
        If RechazaMecanico() Then Exit Sub

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

        'accion de la recepcion: un mecanico no la hace aunque llegue hasta aca
        If RechazaMecanico() Then Exit Sub

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

        'accion de la recepcion: un mecanico no la hace aunque llegue hasta aca
        If RechazaMecanico() Then Exit Sub

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

        'accion de la recepcion: un mecanico no la hace aunque llegue hasta aca
        If RechazaMecanico() Then Exit Sub

        'el diagnostico del mecanico es opcional: si falta, aviso y dejo seguir
        'lo leo de la base, el mecanico pudo guardarlo despues de abrir esta ventana
        Dim diagnosticoGuardado As String = ""
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "SELECT diagnostico FROM orden_trabajo WHERE id_orden_trabajo = @id_orden_trabajo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                    Dim resultado As Object = cmd.ExecuteScalar()
                    If resultado IsNot Nothing AndAlso Not IsDBNull(resultado) Then diagnosticoGuardado = resultado.ToString().Trim
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al leer el diagnóstico de la orden: " & ex.Message)
            Exit Sub
        End Try

        If diagnosticoGuardado = "" Then
            Dim respuesta As DialogResult = MessageBox.Show(
                "La orden no tiene el diagnóstico del mecánico." & vbCrLf &
                "¿Presupuestar igual?",
                "Sin diagnóstico",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If respuesta = DialogResult.No Then Exit Sub
        End If

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

        'accion de la recepcion: un mecanico no la hace aunque llegue hasta aca
        If RechazaMecanico() Then Exit Sub

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

        'accion de la recepcion: un mecanico no la hace aunque llegue hasta aca
        If RechazaMecanico() Then Exit Sub

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
        'queda en True cuando la orden dejo de ser del mecanico que la tiene abierta
        Dim ordenAjena As Boolean = False

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'el cambio de estado y su fila de historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'leo el estado, lo que deja bloqueada la fila de la orden
                    Dim codigoActual As String = LeerCodigoEstado(cn, transaccion)

                    'un mecanico solo inicia sus ordenes: lo compruebo con la fila ya bloqueada
                    If modoMecanico AndAlso Not OrdenEsDelMecanico(cn, transaccion) Then
                        ordenAjena = True
                        aviso = "ajena"
                    ElseIf codigoActual <> "APROBADA" Then
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

        If ordenAjena Then
            CerrarPorOrdenAjena()
            Exit Sub
        End If

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
        'queda en True cuando la orden dejo de ser del mecanico que la tiene abierta
        Dim ordenAjena As Boolean = False

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'las lineas y las observaciones se guardan juntas o ninguna
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'leo el estado, lo que deja bloqueada la fila de la orden
                    Dim codigoActual As String = LeerCodigoEstado(cn, transaccion)

                    'un mecanico solo guarda avance en sus ordenes: lo compruebo con la fila ya bloqueada
                    If modoMecanico AndAlso Not OrdenEsDelMecanico(cn, transaccion) Then
                        ordenAjena = True
                        aviso = "ajena"
                    ElseIf codigoActual <> "EN_PROCESO" Then
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

        If ordenAjena Then
            CerrarPorOrdenAjena()
            Exit Sub
        End If

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
        'queda en True cuando la orden dejo de ser del mecanico que la tiene abierta
        Dim ordenAjena As Boolean = False

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'las horas de cada linea, las observaciones, la fecha, el estado y el historial se guardan juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'leo el estado, lo que deja bloqueada la fila de la orden
                    Dim codigoActual As String = LeerCodigoEstado(cn, transaccion)

                    'un mecanico solo finaliza sus ordenes: lo compruebo con la fila ya bloqueada
                    If modoMecanico AndAlso Not OrdenEsDelMecanico(cn, transaccion) Then
                        ordenAjena = True
                        aviso = "ajena"
                    ElseIf codigoActual <> "EN_PROCESO" Then
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

        If ordenAjena Then
            CerrarPorOrdenAjena()
            Exit Sub
        End If

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

        'accion de la recepcion: un mecanico no la hace aunque llegue hasta aca
        If RechazaMecanico() Then Exit Sub

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
        If Sesion.Rol <> ROL_ADMINISTRADOR Then
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

    Private Sub btnAvisar_Click(sender As Object, e As EventArgs) Handles btnAvisar.Click
        'armo el aviso de vehiculo listo, lo copio al portapapeles y dejo constancia en el historial
        'el estado de la orden no cambia: la entrega se registra con su propio boton
        'el mecanico no avisa a los clientes: el boton ya se le oculta, igual lo compruebo
        If Sesion.Rol <> ROL_ADMINISTRADOR AndAlso Sesion.Rol <> ROL_OPERADOR Then Exit Sub

        Dim nroOrden As String = ""
        Dim codigoActual As String = ""
        Dim idEstado As Integer = 0
        Dim cliente As String = ""
        Dim telefono As String = ""
        Dim vehiculo As String = ""
        Dim patente As String = ""
        Dim total As Decimal = 0D
        Dim servicios As New List(Of String)
        'ultimo aviso ya registrado para esta orden, vacio si nunca se aviso
        Dim avisoAnterior As String = ""

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'leo la orden de la base: otro puesto pudo cambiarla desde que se abrio esta pantalla
                'el total es el aprobado que guarda la orden, el mismo que muestra el pie de las lineas
                Dim consulta As String =
                    "SELECT ot.nro_orden, ot.id_estado_ot, ot.total_aprobado, e.codigo, " &
                    "c.razon_social AS cliente, c.telefono, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo " &
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
                            nroOrden = lector("nro_orden").ToString()
                            idEstado = CInt(lector("id_estado_ot"))
                            codigoActual = lector("codigo").ToString()
                            'el total puede venir NULL en datos migrados: el aviso sale igual, con 0
                            If IsDBNull(lector("total_aprobado")) Then
                                total = 0
                            Else
                                total = CDec(lector("total_aprobado"))
                            End If
                            cliente = lector("cliente").ToString()
                            telefono = lector("telefono").ToString().Trim()
                            patente = lector("patente").ToString()
                            vehiculo = lector("vehiculo").ToString()
                        End If
                    End Using
                End Using

                'los servicios realizados son las lineas aprobadas, en el orden en que se cargaron
                consulta =
                    "SELECT descripcion FROM ot_detalle " &
                    "WHERE id_orden_trabajo = @id_orden_trabajo AND aprobado = 1 " &
                    "ORDER BY id_ot_detalle;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        Do While lector.Read()
                            servicios.Add(lector("descripcion").ToString())
                        Loop
                    End Using
                End Using

                'busco si ya se aviso al cliente, para no anotar dos avisos por un doble click
                consulta =
                    "SELECT h.fecha_hora, u.nombre_completo FROM ot_historial_estado AS h " &
                    "JOIN usuario AS u ON u.id_usuario = h.id_usuario " &
                    "WHERE h.id_orden_trabajo = @id_orden_trabajo AND h.observacion LIKE @observacion " &
                    "ORDER BY h.fecha_hora DESC LIMIT 1;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                    cmd.Parameters.AddWithValue("@observacion", "Cliente avisado%")
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If lector.Read() Then
                            avisoAnterior = CDate(lector("fecha_hora")).ToString("dd/MM/yyyy HH:mm") &
                                            " por " & lector("nombre_completo").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al leer los datos de la orden para el aviso: " & ex.Message)
            Exit Sub
        End Try

        'el aviso es solo de una orden finalizada
        If codigoActual <> "FINALIZADA" Then
            MessageBox.Show("La orden ya no está finalizada: el aviso al cliente no corresponde.")
            CargarOrden()
            Exit Sub
        End If

        'si ya se aviso, pregunto antes de copiar y anotar otro aviso
        If avisoAnterior <> "" Then
            Dim respuesta As DialogResult = MessageBox.Show(
                "Este cliente ya fue avisado el " & avisoAnterior & "." & vbCrLf &
                "¿Copiar el aviso otra vez y anotarlo de nuevo en el historial?",
                "Aviso al cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If respuesta = DialogResult.No Then Exit Sub
        End If

        Dim lista As String = String.Join(", ", servicios)
        If lista = "" Then lista = "-"
        Dim aviso As String =
            "Hola " & cliente & ", su vehículo " & vehiculo & " patente " & patente &
            " ya está listo para retirar. Orden de trabajo N.º " & nroOrden &
            ". Servicios realizados: " & lista & ". Total: $ " & total.ToString("N2") &
            ". ¡Gracias por confiar en nosotros!"

        'el portapapeles es el canal: desde ahi se pega en WhatsApp
        Try
            Clipboard.SetText(aviso)
        Catch ex As Exception
            MessageBox.Show("No se pudo copiar el aviso al portapapeles: " & ex.Message)
            Exit Sub
        End Try

        'dejo la fila en el historial con el estado actual de la orden, como las demas filas
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "INSERT INTO ot_historial_estado (id_orden_trabajo, id_estado_ot, id_usuario, observacion) " &
                    "VALUES (@id_orden_trabajo, @id_estado_ot, @id_usuario, @observacion);"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                    cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
                    cmd.Parameters.AddWithValue("@id_usuario", Sesion.IdUsuario)
                    cmd.Parameters.AddWithValue("@observacion", "Cliente avisado (texto copiado para WhatsApp)")
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("El aviso quedó copiado, pero no se pudo anotar en el historial: " & ex.Message)
        End Try

        CargarHistorial()

        'si el cliente no tiene telefono cargado, el aviso se le lee igual
        Dim destino As String = telefono
        If destino = "" Then destino = "cliente"
        MessageBox.Show("El aviso quedó copiado. Péguelo en WhatsApp o léalo por teléfono al " & destino & ".",
                        "Aviso al cliente", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    '----- reportes impresos -----

    'reportes que se imprimen desde esta ventana
    Const REPORTE_ORDEN As Integer = 1
    Const REPORTE_PRESUPUESTO As Integer = 2
    Const REPORTE_ACTA As Integer = 3
    Const REPORTE_COLGANTE As Integer = 4

    'secciones de los reportes; cada reporte usa algunas, en el orden de SeccionesReporte
    Const SECCION_DATOS As Integer = 0
    Const SECCION_RECEPCION As Integer = 1
    Const SECCION_SERVICIOS As Integer = 2
    Const SECCION_TOTALES As Integer = 3
    Const SECCION_TRABAJO As Integer = 4
    Const SECCION_MOTIVO As Integer = 5
    Const SECCION_HISTORIAL As Integer = 6
    Const SECCION_SINTOMA As Integer = 7
    Const SECCION_VALIDEZ As Integer = 8
    Const SECCION_CONFORMIDAD As Integer = 9
    Const SECCION_FOTOS As Integer = 10
    Const SECCION_FIRMAS_ACTA As Integer = 11

    'angulos de las fotos como los guarda la base, y el titulo que sale debajo de cada una
    ReadOnly angulosFoto() As String = {"FRENTE", "TRASERA", "LATERAL_IZQUIERDO", "LATERAL_DERECHO", "TABLERO"}
    ReadOnly titulosFoto() As String = {"Frente", "Trasera", "Lateral izquierdo", "Lateral derecho", "Tablero"}

    Function SeccionesReporte(reporte As Integer) As Integer()
        'secciones de cada reporte, en el orden en que se imprimen
        If reporte = REPORTE_PRESUPUESTO Then
            Return {SECCION_DATOS, SECCION_SINTOMA, SECCION_SERVICIOS, SECCION_TOTALES, SECCION_VALIDEZ, SECCION_CONFORMIDAD}
        End If
        If reporte = REPORTE_ACTA Then
            Return {SECCION_DATOS, SECCION_RECEPCION, SECCION_FOTOS, SECCION_FIRMAS_ACTA}
        End If
        Return {SECCION_DATOS, SECCION_RECEPCION, SECCION_SERVICIOS, SECCION_TOTALES, SECCION_TRABAJO, SECCION_MOTIVO, SECCION_HISTORIAL}
    End Function

    Function TituloReporte(reporte As Integer) As String
        'titulo de la banda de cabecera de cada reporte
        If reporte = REPORTE_PRESUPUESTO Then Return "Presupuesto"
        If reporte = REPORTE_ACTA Then Return "Acta de recepción"
        If reporte = REPORTE_COLGANTE Then Return "Colgante de llaves"
        Return "Orden de trabajo"
    End Function

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        'el mecanico solo imprime la orden de trabajo, sin importes
        mnuPresupuesto.Visible = Not modoMecanico
        mnuActa.Visible = Not modoMecanico

        'el presupuesto necesita lineas y no se imprime de una orden anulada
        Dim conLineas As Boolean = False
        Dim lineas As DataTable = TryCast(dgvDetalle.DataSource, DataTable)
        If lineas IsNot Nothing Then conLineas = (lineas.Rows.Count > 0)
        mnuPresupuesto.Enabled = conLineas AndAlso codigoEstado <> "ANULADA"

        'muestro el menu de reportes encima del boton, que esta al pie de la ventana
        cmsImprimir.Show(btnImprimir, New Point(0, 0), ToolStripDropDownDirection.AboveRight)
    End Sub

    Function ConfirmarAvanceSinGuardar() As Boolean
        'el reporte sale de lo guardado en la base: si hay avance o diagnostico sin guardar, aviso que no va a aparecer
        If Not avanceSinGuardar AndAlso Not diagnosticoSinGuardar Then Return True

        Dim mensaje As String = "Hay cantidades, horas u observaciones sin guardar y no van a salir en el reporte."
        Dim titulo As String = "Avance sin guardar"
        If diagnosticoSinGuardar Then
            mensaje = "Hay un diagnóstico escrito sin guardar y no va a salir en el reporte."
            titulo = "Diagnóstico sin guardar"
        End If

        Dim respuesta As DialogResult = MessageBox.Show(
            mensaje & vbCrLf &
            "¿Imprimir igual?",
            titulo,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)
        Return (respuesta = DialogResult.Yes)
    End Function

    Private Sub mnuOrdenTrabajo_Click(sender As Object, e As EventArgs) Handles mnuOrdenTrabajo.Click
        If Not ConfirmarAvanceSinGuardar() Then Exit Sub
        If Not CargarDatosImpresion() Then Exit Sub
        MostrarVistaPrevia(prdOrdenTrabajo)
    End Sub

    Private Sub mnuPresupuesto_Click(sender As Object, e As EventArgs) Handles mnuPresupuesto.Click
        'el menu ya lo oculta para el mecanico; igual lo compruebo, el presupuesto lleva importes
        If modoMecanico Then Exit Sub
        If Not ConfirmarAvanceSinGuardar() Then Exit Sub
        If Not CargarDatosImpresion() Then Exit Sub

        If lineasImpresion.Rows.Count = 0 OrElse datosImpresion("codigo").ToString() = "ANULADA" Then
            MessageBox.Show("El presupuesto se imprime solo de una orden con líneas que no esté anulada.")
            Exit Sub
        End If
        MostrarVistaPrevia(prdPresupuesto)
    End Sub

    Private Sub mnuActa_Click(sender As Object, e As EventArgs) Handles mnuActa.Click
        'el acta muestra lo que se registro al recibir el vehiculo, con sus cinco fotos
        If modoMecanico Then Exit Sub
        If Not CargarDatosImpresion() Then Exit Sub
        If Not CargarFotosImpresion() Then Exit Sub
        MostrarVistaPrevia(prdActa)
    End Sub

    Private Sub mnuColgante_Click(sender As Object, e As EventArgs) Handles mnuColgante.Click
        'el colgante no lleva importes ni avance del trabajo: lo imprime cualquier rol, en cualquier estado
        If Not CargarDatosImpresion() Then Exit Sub
        MostrarVistaPrevia(prdColgante)
    End Sub

    Function CargarFotosImpresion() As Boolean
        'leo las fotos de la recepcion; las anteriores se liberan antes de leer las nuevas
        LiberarFotosImpresion()

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "SELECT angulo, imagen FROM ot_foto WHERE id_orden_trabajo = @id_orden_trabajo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        Do While lector.Read()
                            'una foto ilegible queda guardada como Nothing: el acta la marca como ilegible
                            fotosImpresion(lector("angulo").ToString()) = BytesAImagen(CType(lector("imagen"), Byte()))
                        Loop
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al leer las fotos de la recepción: " & ex.Message)
            LiberarFotosImpresion()
            Return False
        End Try

        Return True
    End Function

    Function BytesAImagen(bytes() As Byte) As Image
        'paso los bytes guardados en la base a una foto, como en el tablero de ordenes
        Try
            Using memoria As New System.IO.MemoryStream(bytes)
                Using original As Image = Image.FromStream(memoria)
                    'copio la foto a un bitmap nuevo, que ya no depende de la memoria leida
                    Return New Bitmap(original)
                End Using
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Sub LiberarFotosImpresion()
        'libero las fotos leidas para el acta
        For Each foto As Image In fotosImpresion.Values
            If foto IsNot Nothing Then foto.Dispose()
        Next
        fotosImpresion.Clear()
    End Sub

    Private Sub FrmOrdenGestion_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'la vista previa y los documentos de impresion no estan en components: los libero aca, con las fotos
        LiberarFotosImpresion()
        ppdVistaPrevia.Dispose()
        prdOrdenTrabajo.Dispose()
        prdPresupuesto.Dispose()
        prdActa.Dispose()
        prdColgante.Dispose()
    End Sub

    Sub MostrarVistaPrevia(documento As Printing.PrintDocument)
        'abro la vista previa maximizada; desde su boton de impresora se imprime
        'uso ShowDialog porque este formulario ya se abre como dialogo: asi la vista previa queda encima
        'y no sigue abierta si se cierra la orden
        Try
            documento.DefaultPageSettings.Margins = New Printing.Margins(MARGEN_HOJA, MARGEN_HOJA, MARGEN_HOJA, MARGEN_HOJA)
            ppdVistaPrevia.Document = documento
            ppdVistaPrevia.WindowState = FormWindowState.Maximized
            ppdVistaPrevia.ShowDialog(Me)
        Catch ex As Exception
            MessageBox.Show("No se pudo preparar la impresión: " & ex.Message)
        End Try
    End Sub

    Function CargarDatosImpresion() As Boolean
        'leo de la base todo lo que muestra el reporte de la orden: cabecera, lineas e historial
        'el mecanico no ve importes: sus consultas no traen ningun total ni precio
        Dim consultaCabecera As String
        Dim consultaLineas As String

        If modoMecanico Then
            consultaCabecera =
                "SELECT ot.nro_orden, ot.fecha_recepcion, ot.fecha_prometida, ot.km_ingreso, ot.nivel_combustible, " &
                "ot.fecha_finalizacion, ot.fecha_entrega, " &
                "ot.sintoma_reportado, ot.observaciones_recepcion, ot.observaciones_mecanico, ot.diagnostico, " &
                "v.patente, CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, v.anio, v.color, " &
                "c.razon_social AS cliente, c.documento, c.telefono, " &
                "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, " &
                "e.codigo, e.descripcion AS estado " &
                "FROM orden_trabajo AS ot " &
                "JOIN vehiculo AS v ON v.id_vehiculo = ot.id_vehiculo " &
                "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                "JOIN cliente AS c ON c.id_cliente = ot.id_cliente " &
                "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                "LEFT JOIN mecanico AS m ON m.id_mecanico = ot.id_mecanico " &
                "WHERE ot.id_orden_trabajo = @id_orden_trabajo;"
            consultaLineas =
                "SELECT descripcion, cantidad, aprobado, horas_reales " &
                "FROM ot_detalle " &
                "WHERE id_orden_trabajo = @id_orden_trabajo " &
                "ORDER BY id_ot_detalle;"
        Else
            consultaCabecera =
                "SELECT ot.nro_orden, ot.fecha_recepcion, ot.fecha_prometida, ot.km_ingreso, ot.nivel_combustible, " &
                "ot.fecha_finalizacion, ot.fecha_entrega, " &
                "ot.sintoma_reportado, ot.observaciones_recepcion, ot.observaciones_mecanico, ot.diagnostico, " &
                "ot.total_presupuestado, ot.total_aprobado, " &
                "v.patente, CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, v.anio, v.color, " &
                "c.razon_social AS cliente, c.documento, c.telefono, " &
                "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, " &
                "e.codigo, e.descripcion AS estado " &
                "FROM orden_trabajo AS ot " &
                "JOIN vehiculo AS v ON v.id_vehiculo = ot.id_vehiculo " &
                "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                "JOIN cliente AS c ON c.id_cliente = ot.id_cliente " &
                "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                "LEFT JOIN mecanico AS m ON m.id_mecanico = ot.id_mecanico " &
                "WHERE ot.id_orden_trabajo = @id_orden_trabajo;"
            consultaLineas =
                "SELECT descripcion, cantidad, precio_unitario, subtotal, aprobado, horas_reales " &
                "FROM ot_detalle " &
                "WHERE id_orden_trabajo = @id_orden_trabajo " &
                "ORDER BY id_ot_detalle;"
        End If

        'en papel el historial se lee de arriba hacia abajo: el cambio mas viejo primero
        Dim consultaHistorial As String =
            "SELECT h.fecha_hora, e.codigo, e.descripcion AS estado, u.nombre_completo AS usuario, h.observacion " &
            "FROM ot_historial_estado AS h " &
            "JOIN estado_ot AS e ON e.id_estado_ot = h.id_estado_ot " &
            "JOIN usuario AS u ON u.id_usuario = h.id_usuario " &
            "WHERE h.id_orden_trabajo = @id_orden_trabajo " &
            "ORDER BY h.fecha_hora, h.id_ot_historial;"

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim cabecera As DataTable = LeerTablaImpresion(cn, consultaCabecera)
                If cabecera.Rows.Count = 0 Then
                    MessageBox.Show("No se encontró la orden de trabajo para imprimir.")
                    Return False
                End If
                datosImpresion = cabecera.Rows(0)
                lineasImpresion = LeerTablaImpresion(cn, consultaLineas)
                historialImpresion = LeerTablaImpresion(cn, consultaHistorial)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al leer los datos del reporte: " & ex.Message)
            Return False
        End Try

        fechaImpresion = DateTime.Now
        Return True
    End Function

    Function LeerTablaImpresion(cn As MySqlConnection, consulta As String) As DataTable
        'ejecuto una consulta de la orden y devuelvo sus filas
        Using cmd As New MySqlCommand(consulta, cn)
            cmd.Parameters.AddWithValue("@id_orden_trabajo", IdOrdenTrabajo)
            Dim tabla As New DataTable
            Using lector As MySqlDataReader = cmd.ExecuteReader
                tabla.Load(lector)
            End Using
            Return tabla
        End Using
    End Function

    Function EtapaImpresa() As Integer
        'etapa mas avanzada por la que paso la orden, segun el historial leido para el reporte
        Dim etapa As Integer = 0
        For Each registro As DataRow In historialImpresion.Rows
            Dim etapaRegistro As Integer = OrdenEtapa(registro("codigo").ToString())
            If etapaRegistro > etapa Then etapa = etapaRegistro
        Next
        Return etapa
    End Function

    Function MotivoImpreso() As String
        'motivo del rechazo o de la anulacion: la observacion del ultimo paso al estado actual
        Dim motivo As String = ""
        Dim codigo As String = datosImpresion("codigo").ToString()
        For Each registro As DataRow In historialImpresion.Rows
            If registro("codigo").ToString() = codigo Then motivo = registro("observacion").ToString()
        Next
        Return motivo
    End Function

    Function MuestraAprobacion() As Boolean
        'la columna Aprobado tiene sentido cuando la orden ya paso por el presupuesto y se decidio
        Dim codigo As String = datosImpresion("codigo").ToString()
        Return codigo <> "RECEPCIONADA" AndAlso codigo <> "PRESUPUESTADA" AndAlso EtapaImpresa() >= 2
    End Function

    Sub EmpezarReporte(reporte As Integer)
        'la vista previa y la impresion real recorren las hojas cada una desde el principio
        reporteImpreso = reporte
        hojaImpresa = 0
        seccionImpresa = 0
        filaImpresa = 0
        errorImpresion = False
    End Sub

    Private Sub prdOrdenTrabajo_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles prdOrdenTrabajo.BeginPrint
        EmpezarReporte(REPORTE_ORDEN)
    End Sub

    Private Sub prdPresupuesto_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles prdPresupuesto.BeginPrint
        EmpezarReporte(REPORTE_PRESUPUESTO)
    End Sub

    Private Sub prdActa_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles prdActa.BeginPrint
        EmpezarReporte(REPORTE_ACTA)
    End Sub

    Private Sub prdColgante_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles prdColgante.BeginPrint
        EmpezarReporte(REPORTE_COLGANTE)
    End Sub

    Private Sub prdOrdenTrabajo_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles prdOrdenTrabajo.PrintPage
        ImprimirHoja(e)
    End Sub

    Private Sub prdPresupuesto_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles prdPresupuesto.PrintPage
        ImprimirHoja(e)
    End Sub

    Private Sub prdActa_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles prdActa.PrintPage
        ImprimirHoja(e)
    End Sub

    Private Sub prdColgante_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles prdColgante.PrintPage
        ImprimirHoja(e)
    End Sub

    Sub ImprimirHoja(e As Printing.PrintPageEventArgs)
        'dibujo una hoja del reporte en curso; un error corta el reporte con un aviso, sin cerrar el sistema
        Try
            If reporteImpreso = REPORTE_COLGANTE Then
                DibujarHojaColgantes(e)
            Else
                DibujarHoja(e)
            End If
        Catch ex As Exception
            e.HasMorePages = False
            If Not errorImpresion Then
                errorImpresion = True
                MessageBox.Show("No se pudo dibujar el reporte: " & ex.Message)
            End If
        End Try
    End Sub

    Sub DibujarHojaColgantes(e As Printing.PrintPageEventArgs)
        'dos colgantes iguales en una sola hoja, arriba a la izquierda: uno para el llavero y otro para el tablero
        'medidas en centesimos de pulgada: cada colgante es de 90 x 50 mm
        Const ANCHO_COLGANTE As Integer = 354
        Const ALTO_COLGANTE As Integer = 197
        Const SEPARACION As Integer = 40

        Dim fila As DataRow = datosImpresion
        Dim prometida As String = TextoFecha(fila("fecha_prometida"), "dd/MM/yyyy")
        If prometida = "-" Then prometida = "sin fecha"

        hojaImpresa += 1

        For i As Integer = 0 To 1
            Dim caja As New Rectangle(e.MarginBounds.Left, e.MarginBounds.Top + i * (ALTO_COLGANTE + SEPARACION),
                                      ANCHO_COLGANTE, ALTO_COLGANTE)
            DibujarColgante(e.Graphics, caja, fila("patente").ToString(), "OT N.º " & fila("nro_orden").ToString(),
                            fila("vehiculo").ToString(), fila("cliente").ToString(),
                            TextoFecha(fila("fecha_recepcion"), "dd/MM/yyyy"), prometida)
        Next

        e.HasMorePages = False
    End Sub

    Sub DibujarHoja(e As Printing.PrintPageEventArgs)
        'dibujo las secciones del reporte que entran en la hoja; lo que no entra sigue en la siguiente
        Dim g As Graphics = e.Graphics
        Dim area As Rectangle = e.MarginBounds
        Dim x As Integer = area.Left
        Dim ancho As Integer = area.Width
        Dim limite As Integer = area.Bottom - ALTO_PIE
        Dim titulo As String = TituloReporte(reporteImpreso)
        Dim numero As String = "N.º " & datosImpresion("nro_orden").ToString()
        Dim secciones() As Integer = SeccionesReporte(reporteImpreso)

        hojaImpresa += 1

        'la orden anulada lleva la marca de agua detras de todo
        If datosImpresion("codigo").ToString() = "ANULADA" Then DibujarMarcaAgua(g, area, "ANULADA")

        Dim y As Integer = DibujarCabecera(g, x, area.Top, ancho, titulo, numero)
        Dim inicioHoja As Integer = y

        Do While seccionImpresa < secciones.Length
            Dim seccion As Integer = secciones(seccionImpresa)
            Dim termino As Boolean
            If seccion = SECCION_SERVICIOS Then
                termino = ImprimirServicios(g, x, y, ancho, limite, y = inicioHoja)
            ElseIf seccion = SECCION_HISTORIAL Then
                termino = ImprimirHistorial(g, x, y, ancho, limite, y = inicioHoja)
            Else
                'las demas secciones son bloques que no se parten: si no entran pasan enteros a la hoja siguiente
                Dim fin As Integer = DibujarSeccionOrden(g, seccion, x, y, ancho, False)
                termino = (fin <= limite OrElse y = inicioHoja)
                If termino Then y = DibujarSeccionOrden(g, seccion, x, y, ancho, True)
            End If

            If Not termino Then Exit Do
            seccionImpresa += 1
            filaImpresa = 0
        Loop

        DibujarPie(g, area, titulo & " " & numero & " · Impreso el " & fechaImpresion.ToString("dd/MM/yyyy HH:mm"), hojaImpresa)
        e.HasMorePages = (seccionImpresa < secciones.Length)
    End Sub

    Function DibujarSeccionOrden(g As Graphics, seccion As Integer, x As Integer, y As Integer, ancho As Integer,
                                 dibujar As Boolean) As Integer
        'dibujo (o solo mido, con dibujar en False) una seccion del reporte y devuelvo el y donde termina
        'una seccion que no corresponde al estado de la orden no ocupa lugar
        Dim fila As DataRow = datosImpresion
        Dim codigo As String = fila("codigo").ToString()

        Select Case seccion
            Case SECCION_DATOS
                'renglon del vehiculo con el estado a la derecha
                Dim yRenglon As Integer = y + 4
                If dibujar Then
                    DibujarTextoChico(g, "Recibida el " & TextoFecha(fila("fecha_recepcion"), "dd/MM/yyyy HH:mm") &
                                      " · Patente " & fila("patente").ToString(), x, yRenglon)
                End If
                'el acta registra la recepcion: no lleva el estado actual de la orden
                Dim yInsignia As Integer = y + 24
                If dibujar AndAlso reporteImpreso <> REPORTE_ACTA Then
                    yInsignia = DibujarInsignia(g, fila("estado").ToString(), ColorEstado(codigo), x + ancho, y)
                End If
                y = yInsignia + 12

                'dos cajas lado a lado: cliente y vehiculo
                Const ANCHO_ETIQUETA As Integer = 105
                Dim anchoCaja As Integer = (ancho - 16) \ 2
                Dim xVehiculo As Integer = x + anchoCaja + 16

                Dim yCliente As Integer = DibujarTituloCaja(g, "Cliente", x, y, anchoCaja, dibujar)
                yCliente = DibujarDato(g, "Nombre", fila("cliente").ToString(), x + 8, yCliente, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)
                yCliente = DibujarDato(g, "Documento", fila("documento").ToString(), x + 8, yCliente, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)
                yCliente = DibujarDato(g, "Teléfono", fila("telefono").ToString(), x + 8, yCliente, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)

                Dim yVehiculo As Integer = DibujarTituloCaja(g, "Vehículo", xVehiculo, y, anchoCaja, dibujar)
                yVehiculo = DibujarDato(g, "Patente", fila("patente").ToString(), xVehiculo + 8, yVehiculo, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)
                yVehiculo = DibujarDato(g, "Marca y modelo", fila("vehiculo").ToString(), xVehiculo + 8, yVehiculo, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)
                yVehiculo = DibujarDato(g, "Año", fila("anio").ToString(), xVehiculo + 8, yVehiculo, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)
                yVehiculo = DibujarDato(g, "Color", fila("color").ToString(), xVehiculo + 8, yVehiculo, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)
                yVehiculo = DibujarDato(g, "Km de ingreso", CInt(fila("km_ingreso")).ToString("N0") & " km", xVehiculo + 8, yVehiculo, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)
                yVehiculo = DibujarDato(g, "Combustible", NombreCombustible(fila("nivel_combustible")), xVehiculo + 8, yVehiculo, anchoCaja - 16, ANCHO_ETIQUETA, dibujar)

                'las dos cajas terminan a la misma altura
                Dim yFin As Integer = Math.Max(yCliente, yVehiculo) + 4
                If dibujar Then
                    DibujarBordeCaja(g, x, y, anchoCaja, yFin)
                    DibujarBordeCaja(g, xVehiculo, y, anchoCaja, yFin)
                End If
                Return yFin + 16

            Case SECCION_RECEPCION
                Const ANCHO_ETIQUETA As Integer = 175
                Dim prometida As String = TextoFecha(fila("fecha_prometida"), "dd/MM/yyyy")
                If prometida = "-" Then prometida = "Sin fecha"

                y = DibujarTituloSeccion(g, "Recepción", x, y, ancho, dibujar)
                y = DibujarDato(g, "Fecha de recepción", TextoFecha(fila("fecha_recepcion"), "dd/MM/yyyy HH:mm"), x, y, ancho, ANCHO_ETIQUETA, dibujar)
                y = DibujarDato(g, "Entrega prometida", prometida, x, y, ancho, ANCHO_ETIQUETA, dibujar)
                y = DibujarDato(g, "Síntoma reportado", fila("sintoma_reportado").ToString(), x, y, ancho, ANCHO_ETIQUETA, dibujar)
                y = DibujarDato(g, "Observaciones", fila("observaciones_recepcion").ToString(), x, y, ancho, ANCHO_ETIQUETA, dibujar)
                'la orden de trabajo suma el diagnostico del mecanico si esta cargado; el acta registra solo la recepcion
                If reporteImpreso = REPORTE_ORDEN AndAlso fila("diagnostico").ToString().Trim <> "" Then
                    y = DibujarDato(g, "Diagnóstico", fila("diagnostico").ToString().Trim, x, y, ancho, ANCHO_ETIQUETA, dibujar)
                End If
                Return y + 12

            Case SECCION_TOTALES
                'solo para quien ve importes y cuando la orden tiene lineas
                If modoMecanico OrElse lineasImpresion.Rows.Count = 0 Then Return y

                Dim derecha As Integer = x + ancho - 4
                Dim conAprobado As Boolean = MuestraAprobacion()
                y = DibujarImporte(g, "Total presupuestado:", CDec(fila("total_presupuestado")).ToString("C2"), derecha, y,
                                   Not conAprobado, dibujar)
                If conAprobado Then
                    y = DibujarImporte(g, "Total aprobado:", CDec(fila("total_aprobado")).ToString("C2"), derecha, y, True, dibujar)
                End If
                Return y + 14

            Case SECCION_TRABAJO
                'el trabajo del mecanico se muestra desde que la orden llego a estar en proceso
                If EtapaImpresa() < 4 Then Return y

                Const ANCHO_ETIQUETA As Integer = 175
                Dim horas As Decimal = 0
                For Each linea As DataRow In lineasImpresion.Rows
                    If Not IsDBNull(linea("horas_reales")) Then horas += CDec(linea("horas_reales"))
                Next

                y = DibujarTituloSeccion(g, "Trabajo realizado", x, y, ancho, dibujar)
                y = DibujarDato(g, "Mecánico", fila("mecanico").ToString(), x, y, ancho, ANCHO_ETIQUETA, dibujar)
                y = DibujarDato(g, "Horas trabajadas", horas.ToString("N2") & " h", x, y, ancho, ANCHO_ETIQUETA, dibujar)
                If Not IsDBNull(fila("fecha_finalizacion")) Then
                    y = DibujarDato(g, "Fecha de finalización", TextoFecha(fila("fecha_finalizacion"), "dd/MM/yyyy HH:mm"), x, y, ancho, ANCHO_ETIQUETA, dibujar)
                End If
                If Not IsDBNull(fila("fecha_entrega")) Then
                    y = DibujarDato(g, "Fecha de entrega", TextoFecha(fila("fecha_entrega"), "dd/MM/yyyy HH:mm"), x, y, ancho, ANCHO_ETIQUETA, dibujar)
                End If
                y = DibujarDato(g, "Observaciones del mecánico", fila("observaciones_mecanico").ToString(), x, y, ancho, ANCHO_ETIQUETA, dibujar)
                Return y + 12

            Case SECCION_MOTIVO
                'una orden rechazada o anulada lleva el motivo que quedo en el historial
                Dim titulo As String = ""
                If codigo = "RECHAZADA" Then titulo = "Motivo del rechazo"
                If codigo = "ANULADA" Then titulo = "Motivo de la anulación"
                If titulo = "" Then Return y

                Dim motivo As String = MotivoImpreso()
                If motivo = "" Then motivo = "Sin motivo registrado."
                Return DibujarRecuadro(g, titulo, motivo, x, y, ancho, dibujar)

            Case SECCION_SINTOMA
                'presupuesto: lo que pidio el cliente al dejar el vehiculo
                y = DibujarTituloSeccion(g, "Motivo de la consulta", x, y, ancho, dibujar)
                y = DibujarDato(g, "Síntoma reportado", fila("sintoma_reportado").ToString(), x, y, ancho, 175, dibujar)
                'y lo que encontro el mecanico al revisarlo, si lo cargo
                If fila("diagnostico").ToString().Trim <> "" Then
                    y = DibujarDato(g, "Diagnóstico", fila("diagnostico").ToString().Trim, x, y, ancho, 175, dibujar)
                End If
                Return y + 12

            Case SECCION_VALIDEZ
                'el presupuesto vale una cantidad de dias desde que se cargo; los importes son precios finales
                Dim emision As DateTime = FechaPresupuesto()
                Dim nota As String =
                    "Presupuesto emitido el " & emision.ToString("dd/MM/yyyy") & ", válido por " &
                    DIAS_VALIDEZ_PRESUPUESTO.ToString() & " días (hasta el " &
                    emision.AddDays(DIAS_VALIDEZ_PRESUPUESTO).ToString("dd/MM/yyyy") & "). " &
                    "Los importes son precios finales."
                Return DibujarParrafo(g, nota, x, y, ancho, dibujar) + 16

            Case SECCION_CONFORMIDAD
                'espacio para que el cliente firme que acepta el presupuesto
                y = DibujarTituloSeccion(g, "Conformidad del cliente", x, y, ancho, dibujar)
                y = DibujarParrafo(g, "Acepto el presupuesto detallado y autorizo la realización de los trabajos.", x, y, ancho, dibujar)
                Return DibujarFirmas(g, {"Firma", "Aclaración", "Fecha"}, x, y, ancho, dibujar)

            Case SECCION_FOTOS
                Return DibujarFotosRecepcion(g, x, y, ancho, dibujar)

            Case SECCION_FIRMAS_ACTA
                'firma del cliente que deja el vehiculo y de quien lo recibio en el taller
                y = DibujarParrafo(g, "El cliente deja el vehículo en el estado descripto en esta acta.", x, y + 4, ancho, dibujar)
                Return DibujarFirmas(g, {"Cliente" & vbLf & fila("cliente").ToString(),
                                         "Recibió" & vbLf & UsuarioRecepcion()}, x, y, ancho, dibujar)
        End Select

        Return y
    End Function

    Function FechaPresupuesto() As DateTime
        'fecha en que la orden paso a presupuestada (la ultima vez); si todavia no paso, la de impresion
        Dim fecha As DateTime = fechaImpresion
        For Each registro As DataRow In historialImpresion.Rows
            If registro("codigo").ToString() = "PRESUPUESTADA" Then fecha = CDate(registro("fecha_hora"))
        Next
        Return fecha
    End Function

    Function UsuarioRecepcion() As String
        'quien recibio el vehiculo: el usuario de la fila de historial que recepciono la orden
        For Each registro As DataRow In historialImpresion.Rows
            If registro("codigo").ToString() = "RECEPCIONADA" Then Return registro("usuario").ToString()
        Next
        Return ""
    End Function

    Function DibujarFotosRecepcion(g As Graphics, x As Integer, y As Integer, ancho As Integer, dibujar As Boolean) As Integer
        'las cinco fotos de la recepcion en una grilla de tres columnas, cada una con su titulo
        Const COLUMNAS As Integer = 3
        Const SEPARACION As Integer = 12

        y = DibujarTituloSeccion(g, "Fotos de la recepción", x, y, ancho, dibujar)

        Dim anchoFoto As Integer = (ancho - SEPARACION * (COLUMNAS - 1)) \ COLUMNAS
        Dim altoFoto As Integer = anchoFoto * 2 \ 3
        Dim altoCelda As Integer = altoFoto + ALTO_TITULO_FOTO

        For i As Integer = 0 To angulosFoto.Length - 1
            Dim columna As Integer = i Mod COLUMNAS
            Dim renglon As Integer = i \ COLUMNAS
            Dim caja As New Rectangle(x + columna * (anchoFoto + SEPARACION), y + renglon * (altoCelda + SEPARACION), anchoFoto, altoFoto)

            If dibujar Then
                'sin fila en ot_foto no hay foto; con fila pero bytes ilegibles queda guardada como Nothing
                Dim foto As Image = Nothing
                Dim aviso As String = "Sin foto"
                If fotosImpresion.ContainsKey(angulosFoto(i)) Then
                    foto = fotosImpresion(angulosFoto(i))
                    aviso = "Foto ilegible"
                End If
                DibujarFoto(g, foto, titulosFoto(i), aviso, caja)
            End If
        Next

        Dim renglones As Integer = (angulosFoto.Length + COLUMNAS - 1) \ COLUMNAS
        Return y + renglones * (altoCelda + SEPARACION) + 8
    End Function

    Function ImprimirServicios(g As Graphics, x As Integer, ByRef y As Integer, ancho As Integer, limite As Integer,
                               inicioHoja As Boolean) As Boolean
        'tabla de servicios de la orden; las columnas dependen del estado y de quien imprime
        If lineasImpresion.Rows.Count = 0 Then Return True

        Dim conAprobado As Boolean = MuestraAprobacion()
        'las horas son del trabajo: van solo en la orden de trabajo, no en el presupuesto
        Dim conHoras As Boolean = (reporteImpreso = REPORTE_ORDEN AndAlso EtapaImpresa() >= 4)

        Dim columnas As New List(Of String) From {"Servicio", "Cant."}
        Dim pesos As New List(Of Single) From {5.0F, 1.1F}
        Dim alineaciones As New List(Of StringAlignment) From {StringAlignment.Near, StringAlignment.Far}
        If conAprobado Then
            columnas.Add("Aprobado")
            pesos.Add(1.3F)
            alineaciones.Add(StringAlignment.Center)
        End If
        If conHoras Then
            columnas.Add("Horas")
            pesos.Add(1.1F)
            alineaciones.Add(StringAlignment.Far)
        End If
        If Not modoMecanico Then
            columnas.Add("P. unitario")
            pesos.Add(1.8F)
            alineaciones.Add(StringAlignment.Far)
            columnas.Add("Subtotal")
            pesos.Add(1.8F)
            alineaciones.Add(StringAlignment.Far)
        End If

        Dim filas As New List(Of String())
        For Each linea As DataRow In lineasImpresion.Rows
            Dim valores As New List(Of String) From {linea("descripcion").ToString(), CDec(linea("cantidad")).ToString("N2")}
            If conAprobado Then
                If CBool(linea("aprobado")) Then valores.Add("Sí") Else valores.Add("No")
            End If
            If conHoras Then
                If IsDBNull(linea("horas_reales")) Then valores.Add("-") Else valores.Add(CDec(linea("horas_reales")).ToString("N2"))
            End If
            If Not modoMecanico Then
                valores.Add(CDec(linea("precio_unitario")).ToString("C2"))
                valores.Add(CDec(linea("subtotal")).ToString("C2"))
            End If
            filas.Add(valores.ToArray())
        Next

        Return DibujarTabla(g, "Servicios", columnas.ToArray(), pesos.ToArray(), alineaciones.ToArray(), filas,
                            x, y, ancho, limite, filaImpresa, inicioHoja)
    End Function

    Function ImprimirHistorial(g As Graphics, x As Integer, ByRef y As Integer, ancho As Integer, limite As Integer,
                               inicioHoja As Boolean) As Boolean
        'tabla con los cambios de estado de la orden, al final del reporte
        If historialImpresion.Rows.Count = 0 Then Return True

        Dim columnas() As String = {"Fecha y hora", "Estado", "Usuario", "Observación"}
        Dim pesos() As Single = {2.0F, 1.8F, 2.9F, 3.8F}
        Dim alineaciones() As StringAlignment = {StringAlignment.Near, StringAlignment.Near, StringAlignment.Near, StringAlignment.Near}

        Dim filas As New List(Of String())
        For Each registro As DataRow In historialImpresion.Rows
            filas.Add({CDate(registro("fecha_hora")).ToString("dd/MM/yyyy HH:mm"),
                       registro("estado").ToString(),
                       registro("usuario").ToString(),
                       registro("observacion").ToString()})
        Next

        Return DibujarTabla(g, "Historial de estados", columnas, pesos, alineaciones, filas,
                            x, y, ancho, limite, filaImpresa, inicioHoja)
    End Function

End Class

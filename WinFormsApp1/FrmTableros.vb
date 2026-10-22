'importo la clase para conectar a BD
Imports MySqlConnector

'no importo ScottPlot: su clase Color se confundiria con System.Drawing.Color
Public Class FrmTableros

    'meses abreviados para las etiquetas del grafico de facturacion
    Private ReadOnly nombresMes() As String = {"ene", "feb", "mar", "abr", "may", "jun",
                                               "jul", "ago", "sep", "oct", "nov", "dic"}

    Function ColorEstado(codigo As String) As ScottPlot.Color
        'los mismos colores de los estados que usa el tablero de ordenes,
        'un poco mas oscuros porque sobre fondo blanco quedan muy palidos
        Dim color As Color = Color.White
        If codigo = "RECEPCIONADA" Then color = Color.Lavender
        If codigo = "PRESUPUESTADA" Then color = Color.LightBlue
        If codigo = "APROBADA" Then color = Color.LightSkyBlue
        If codigo = "EN_PROCESO" Then color = Color.Khaki
        If codigo = "FINALIZADA" Then color = Color.PaleTurquoise
        If codigo = "ENTREGADA" Then color = Color.LightGreen
        If codigo = "RECHAZADA" Then color = Color.LightPink
        If codigo = "ANULADA" Then color = Color.Gainsboro
        Return ScottPlot.Color.FromColor(color).Darken(0.15)
    End Function

    Sub MostrarSinDatos(grafico As ScottPlot.WinForms.FormsPlot, titulo As String)
        'grafico vacio: solo el titulo y el aviso, sin ejes
        grafico.Plot.Add.Annotation("Sin datos para el período", ScottPlot.Alignment.MiddleCenter)
        'el titulo va despues de ocultar los ejes, porque ocultarlos tambien lo oculta
        grafico.Plot.HideAxesAndGrid()
        grafico.Plot.Title(titulo, 13)
        grafico.Refresh()
    End Sub

    Sub PrepararGrafico(grafico As ScottPlot.WinForms.FormsPlot)
        'Reset deja el grafico como nuevo: ademas de limpiar los datos (Plot.Clear)
        'borra las etiquetas manuales de los ejes, la leyenda y los ejes ocultos
        grafico.Reset()
        grafico.Plot.Font.Set("Segoe UI")
        'fondo blanco en todo el grafico, como las tarjetas de las otras pantallas
        grafico.Plot.FigureBackground.Color = ScottPlot.Colors.White
        'el tablero solo se mira: sin zoom ni arrastre con el mouse
        grafico.UserInputProcessor.Disable()
        'el titulo centrado en todo el grafico y no solo sobre las barras,
        'asi no se corta cuando las etiquetas de la izquierda son largas
        grafico.Plot.Axes.Title.FullFigureCenter = True
    End Sub

    Sub AgregarParametros(cmd As MySqlCommand)
        'el periodo elegido: hasta incluye todo ese dia, por eso se compara con el dia siguiente
        cmd.Parameters.AddWithValue("@desde", dtpDesde.Value.Date)
        cmd.Parameters.AddWithValue("@hasta", dtpHasta.Value.Date.AddDays(1))
    End Sub

    Sub CargarIndicadores()
        'los seis numeros de arriba
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'ordenes recibidas en el periodo
                Dim sql As String = "SELECT COUNT(*) FROM orden_trabajo " &
                    "WHERE fecha_recepcion >= @desde AND fecha_recepcion < @hasta;"
                Using cmd As New MySqlCommand(sql, cn)
                    AgregarParametros(cmd)
                    lblIngresadas.Text = CInt(cmd.ExecuteScalar()).ToString()
                End Using

                'vehiculos en el taller ahora: ordenes que todavia no se cerraron
                sql = "SELECT COUNT(*) FROM orden_trabajo AS o " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = o.id_estado_ot " &
                    "WHERE e.codigo NOT IN ('ENTREGADA', 'RECHAZADA', 'ANULADA');"
                Using cmd As New MySqlCommand(sql, cn)
                    lblEnTaller.Text = CInt(cmd.ExecuteScalar()).ToString()
                End Using

                'facturado y ticket promedio: ordenes entregadas en el periodo, por su total aprobado
                sql = "SELECT COUNT(*) AS entregadas, COALESCE(SUM(o.total_aprobado), 0) AS facturado " &
                    "FROM orden_trabajo AS o " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = o.id_estado_ot " &
                    "WHERE e.codigo = 'ENTREGADA' " &
                    "AND o.fecha_entrega >= @desde AND o.fecha_entrega < @hasta;"
                Using cmd As New MySqlCommand(sql, cn)
                    AgregarParametros(cmd)
                    Using rd = cmd.ExecuteReader()
                        rd.Read()
                        Dim entregadas As Integer = CInt(rd("entregadas"))
                        Dim facturado As Decimal = CDec(rd("facturado"))
                        lblFacturado.Text = facturado.ToString("$ #,##0")
                        If entregadas > 0 Then
                            lblTicket.Text = (facturado / entregadas).ToString("$ #,##0")
                        Else
                            lblTicket.Text = "$ 0"
                        End If
                    End Using
                End Using

                'aprobacion: de las ordenes presupuestadas en el periodo, cuantas aprobo el cliente
                sql = "SELECT COUNT(DISTINCT hp.id_orden_trabajo) AS presupuestadas, " &
                    "COUNT(DISTINCT ha.id_orden_trabajo) AS aprobadas " &
                    "FROM ot_historial_estado AS hp " &
                    "JOIN estado_ot AS ep ON ep.id_estado_ot = hp.id_estado_ot AND ep.codigo = 'PRESUPUESTADA' " &
                    "LEFT JOIN (SELECT h.id_orden_trabajo FROM ot_historial_estado AS h " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = h.id_estado_ot " &
                    "WHERE e.codigo = 'APROBADA') AS ha ON ha.id_orden_trabajo = hp.id_orden_trabajo " &
                    "WHERE hp.fecha_hora >= @desde AND hp.fecha_hora < @hasta;"
                Using cmd As New MySqlCommand(sql, cn)
                    AgregarParametros(cmd)
                    Using rd = cmd.ExecuteReader()
                        rd.Read()
                        Dim presupuestadas As Integer = CInt(rd("presupuestadas"))
                        Dim aprobadas As Integer = CInt(rd("aprobadas"))
                        If presupuestadas > 0 Then
                            lblAprobacion.Text = Math.Round(aprobadas * 100.0 / presupuestadas).ToString() & " %"
                        Else
                            lblAprobacion.Text = "0 %"
                        End If
                    End Using
                End Using

                'demoradas ahora: paso la fecha prometida y el trabajo no esta terminado
                sql = "SELECT COUNT(*) FROM orden_trabajo AS o " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = o.id_estado_ot " &
                    "WHERE o.fecha_prometida < @hoy " &
                    "AND e.codigo NOT IN ('FINALIZADA', 'ENTREGADA', 'RECHAZADA', 'ANULADA');"
                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@hoy", Date.Today)
                    Dim demoradas As Integer = CInt(cmd.ExecuteScalar())
                    lblDemoradas.Text = demoradas.ToString()
                    'en rojo cuando hay alguna
                    If demoradas > 0 Then
                        lblDemoradas.ForeColor = Color.Firebrick
                    Else
                        lblDemoradas.ForeColor = Color.Black
                    End If
                End Using
            End Using
        Catch ex As Exception
            MostrarError("No se pudieron calcular los indicadores.", ex)
        End Try
    End Sub

    Sub GraficoFacturacion()
        'facturado por mes: siempre los ultimos 6 meses, terminando en el mes de "hasta"
        Dim titulo As String = "Facturación por mes"
        Try
            Dim primerMes As New Date(dtpHasta.Value.Year, dtpHasta.Value.Month, 1)
            primerMes = primerMes.AddMonths(-5)

            'un lugar por mes, en cero hasta que la consulta diga otra cosa
            Dim importes(5) As Double
            Dim etiquetas(5) As String
            Dim posiciones(5) As Double
            For i As Integer = 0 To 5
                Dim mes As Date = primerMes.AddMonths(i)
                'el mes arriba y el año abajo, para que entren en el grafico angosto
                etiquetas(i) = nombresMes(mes.Month - 1) & vbLf & mes.ToString("yy")
                posiciones(i) = i
            Next

            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                Dim sql As String = "SELECT YEAR(o.fecha_entrega) AS anio, MONTH(o.fecha_entrega) AS mes, " &
                    "SUM(o.total_aprobado) AS facturado " &
                    "FROM orden_trabajo AS o " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = o.id_estado_ot " &
                    "WHERE e.codigo = 'ENTREGADA' " &
                    "AND o.fecha_entrega >= @inicio AND o.fecha_entrega < @fin " &
                    "GROUP BY YEAR(o.fecha_entrega), MONTH(o.fecha_entrega);"

                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@inicio", primerMes)
                    cmd.Parameters.AddWithValue("@fin", primerMes.AddMonths(6))
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            'ubico cada mes en su lugar del arreglo
                            Dim mes As New Date(CInt(rd("anio")), CInt(rd("mes")), 1)
                            Dim indice As Integer = (mes.Year - primerMes.Year) * 12 + mes.Month - primerMes.Month
                            importes(indice) = CDbl(rd("facturado"))
                        End While
                    End Using
                End Using
            End Using

            PrepararGrafico(fpFacturacion)

            If importes.Sum() = 0 Then
                MostrarSinDatos(fpFacturacion, titulo)
                Exit Sub
            End If

            Dim barras = fpFacturacion.Plot.Add.Bars(importes)
            barras.Color = ScottPlot.Color.FromColor(Color.FromArgb(52, 73, 94))
            'el mes como etiqueta debajo de cada barra
            fpFacturacion.Plot.Axes.Bottom.SetTicks(posiciones, etiquetas)
            'las barras arrancan desde el cero, sin margen abajo
            fpFacturacion.Plot.Axes.Margins(bottom:=0)
            fpFacturacion.Plot.Title(titulo, 13)
            fpFacturacion.Plot.HideGrid()
            fpFacturacion.Refresh()
        Catch ex As Exception
            MostrarError("No se pudo armar el gráfico de facturación.", ex)
        End Try
    End Sub

    Sub GraficoServicios()
        'los 8 servicios mas pedidos: lineas aprobadas de las ordenes recibidas en el periodo
        Dim titulo As String = "Servicios más pedidos"
        Try
            Dim cantidades As New List(Of Double)
            Dim nombres As New List(Of String)

            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                Dim sql As String = "SELECT s.descripcion, SUM(d.cantidad) AS cantidad " &
                    "FROM ot_detalle AS d " &
                    "JOIN orden_trabajo AS o ON o.id_orden_trabajo = d.id_orden_trabajo " &
                    "JOIN servicio AS s ON s.id_servicio = d.id_servicio " &
                    "WHERE d.aprobado = 1 " &
                    "AND o.fecha_recepcion >= @desde AND o.fecha_recepcion < @hasta " &
                    "GROUP BY s.id_servicio, s.descripcion " &
                    "ORDER BY cantidad DESC, s.descripcion " &
                    "LIMIT 8;"

                Using cmd As New MySqlCommand(sql, cn)
                    AgregarParametros(cmd)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            'los nombres largos se acortan para que entren al costado
                            Dim nombre As String = rd("descripcion").ToString()
                            If nombre.Length > 28 Then nombre = nombre.Substring(0, 27) & "…"
                            nombres.Add(nombre)
                            cantidades.Add(CDbl(rd("cantidad")))
                        End While
                    End Using
                End Using
            End Using

            PrepararGrafico(fpServicios)

            If cantidades.Count = 0 Then
                MostrarSinDatos(fpServicios, titulo)
                Exit Sub
            End If

            'el mas pedido arriba: invierto las posiciones
            Dim barras As New List(Of ScottPlot.Bar)
            Dim posiciones As New List(Of Double)
            For i As Integer = 0 To cantidades.Count - 1
                Dim posicion As Double = cantidades.Count - 1 - i
                barras.Add(New ScottPlot.Bar With {
                    .Position = posicion,
                    .Value = cantidades(i),
                    .FillColor = ScottPlot.Color.FromColor(Color.SteelBlue),
                    .Label = cantidades(i).ToString("0.##")
                })
                posiciones.Add(posicion)
            Next

            Dim grafico = fpServicios.Plot.Add.Bars(barras)
            'barras horizontales con el nombre del servicio a la izquierda
            grafico.Horizontal = True
            fpServicios.Plot.Axes.Left.SetTicks(posiciones.ToArray(), nombres.ToArray())
            'lugar a la derecha para el numero de la barra mas larga
            fpServicios.Plot.Axes.Margins(left:=0, right:=0.25)
            fpServicios.Plot.Title(titulo, 13)
            fpServicios.Plot.HideGrid()
            fpServicios.Refresh()
        Catch ex As Exception
            MostrarError("No se pudo armar el gráfico de servicios.", ex)
        End Try
    End Sub

    Sub GraficoHoras()
        'horas estimadas contra reales de cada mecanico, en las ordenes finalizadas en el periodo
        Dim titulo As String = "Horas por mecánico"
        Try
            Dim nombres As New List(Of String)
            Dim estimadas As New List(Of Double)
            Dim reales As New List(Of Double)

            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                Dim sql As String = "SELECT m.nombre_completo, " &
                    "SUM(COALESCE(s.tiempo_estimado_horas, 0) * d.cantidad) AS estimadas, " &
                    "SUM(COALESCE(d.horas_reales, 0)) AS reales " &
                    "FROM orden_trabajo AS o " &
                    "JOIN mecanico AS m ON m.id_mecanico = o.id_mecanico " &
                    "JOIN ot_detalle AS d ON d.id_orden_trabajo = o.id_orden_trabajo " &
                    "JOIN servicio AS s ON s.id_servicio = d.id_servicio " &
                    "WHERE d.aprobado = 1 " &
                    "AND o.fecha_finalizacion >= @desde AND o.fecha_finalizacion < @hasta " &
                    "GROUP BY m.id_mecanico, m.nombre_completo " &
                    "ORDER BY m.nombre_completo;"

                Using cmd As New MySqlCommand(sql, cn)
                    AgregarParametros(cmd)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            'nombre y apellido en renglones separados, para que no se pisen
                            nombres.Add(rd("nombre_completo").ToString().Replace(" ", vbLf))
                            estimadas.Add(CDbl(rd("estimadas")))
                            reales.Add(CDbl(rd("reales")))
                        End While
                    End Using
                End Using
            End Using

            PrepararGrafico(fpHoras)

            If nombres.Count = 0 Then
                MostrarSinDatos(fpHoras, titulo)
                Exit Sub
            End If

            Dim colorEstimadas As ScottPlot.Color = ScottPlot.Color.FromColor(Color.LightSteelBlue)
            Dim colorReales As ScottPlot.Color = ScottPlot.Color.FromColor(Color.FromArgb(52, 73, 94))

            'dos barras juntas por mecanico y un espacio entre mecanicos
            Dim barras As New List(Of ScottPlot.Bar)
            Dim posiciones As New List(Of Double)
            For i As Integer = 0 To nombres.Count - 1
                barras.Add(New ScottPlot.Bar With {.Position = i * 3, .Value = estimadas(i), .FillColor = colorEstimadas})
                barras.Add(New ScottPlot.Bar With {.Position = i * 3 + 1, .Value = reales(i), .FillColor = colorReales})
                posiciones.Add(i * 3 + 0.5)
            Next

            fpHoras.Plot.Add.Bars(barras)
            fpHoras.Plot.Axes.Bottom.SetTicks(posiciones.ToArray(), nombres.ToArray())
            'lugar arriba para que la leyenda no tape las barras
            fpHoras.Plot.Axes.Margins(bottom:=0, top:=0.3)

            'referencias de los dos colores
            fpHoras.Plot.Legend.ManualItems.Add(New ScottPlot.LegendItem With {.LabelText = "Estimadas", .FillColor = colorEstimadas})
            fpHoras.Plot.Legend.ManualItems.Add(New ScottPlot.LegendItem With {.LabelText = "Reales", .FillColor = colorReales})
            fpHoras.Plot.ShowLegend(ScottPlot.Alignment.UpperRight)

            fpHoras.Plot.Title(titulo, 13)
            fpHoras.Plot.HideGrid()
            fpHoras.Refresh()
        Catch ex As Exception
            MostrarError("No se pudo armar el gráfico de horas.", ex)
        End Try
    End Sub

    Sub GraficoEstados()
        'ordenes recibidas en el periodo segun su estado actual
        Dim titulo As String = "Órdenes por estado"
        Try
            Dim porciones As New List(Of ScottPlot.PieSlice)

            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                Dim sql As String = "SELECT e.codigo, e.descripcion, COUNT(*) AS cantidad " &
                    "FROM orden_trabajo AS o " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = o.id_estado_ot " &
                    "WHERE o.fecha_recepcion >= @desde AND o.fecha_recepcion < @hasta " &
                    "GROUP BY e.id_estado_ot, e.codigo, e.descripcion " &
                    "ORDER BY e.orden_flujo;"

                Using cmd As New MySqlCommand(sql, cn)
                    AgregarParametros(cmd)
                    Using rd = cmd.ExecuteReader()
                        While rd.Read()
                            Dim cantidad As Double = CDbl(rd("cantidad"))
                            'creo la porcion con el color del estado
                            Dim porcion As New ScottPlot.PieSlice
                            porcion.Value = cantidad
                            porcion.Label = cantidad.ToString()
                            porcion.LegendText = rd("descripcion").ToString()
                            porcion.LabelBold = True
                            porcion.FillColor = ColorEstado(rd("codigo").ToString())
                            porciones.Add(porcion)
                        End While
                    End Using
                End Using
            End Using

            PrepararGrafico(fpEstados)

            If porciones.Count = 0 Then
                MostrarSinDatos(fpEstados, titulo)
                Exit Sub
            End If

            Dim torta = fpEstados.Plot.Add.Pie(porciones)
            torta.ExplodeFraction = 0.05
            torta.SliceLabelDistance = 0.6

            'la torta mide 1 de radio (un poco mas con las porciones separadas): muestro solo ese cuadrado,
            'asi ocupa todo el alto del grafico sin el margen que agrega el ajuste automatico
            fpEstados.Plot.Axes.SetLimits(-1.08, 1.08, -1.08, 1.08)
            'y siempre redonda: la misma escala en los dos ejes, alejando el lado que sobra (el ancho)
            'para que entre entera en cualquier tamaño de ventana
            fpEstados.Plot.Axes.Rules.Add(New ScottPlot.AxisRules.SquareZoomOut(fpEstados.Plot.Axes.Bottom, fpEstados.Plot.Axes.Left))

            'la leyenda va a la derecha, donde sobra ancho
            fpEstados.Plot.ShowLegend(ScottPlot.Edge.Right)
            'el titulo va despues de ocultar los ejes, porque ocultarlos tambien lo oculta
            fpEstados.Plot.HideAxesAndGrid()
            fpEstados.Plot.Title(titulo, 13)
            fpEstados.Refresh()
        Catch ex As Exception
            MostrarError("No se pudo armar el gráfico de estados.", ex)
        End Try
    End Sub

    'tiempos por etapa del periodo: los usan el grafico, la grilla y la hoja impresa
    Private tablaEtapas As DataTable = Nothing
    'periodo con el que se leyo la tabla, para imprimirlo aunque despues cambien las fechas
    Private desdeEtapas As Date
    Private hastaEtapas As Date
    'fecha de impresion de la hoja y aviso de error una sola vez
    Private fechaTiempos As DateTime
    Private errorTiempos As Boolean = False

    Function TextoDuracion(minutos As Double) As String
        'menos de una hora se muestra en minutos, menos de un dia en horas, si no en dias con un decimal
        'asi una etapa corta no aparece como "0 h"; redondeo antes de elegir la unidad para no mostrar "60 min" ni "24 h"
        Dim enMinutos As Double = Math.Round(minutos)
        If enMinutos < 60 Then Return enMinutos.ToString("0") & " min"
        Dim enHoras As Double = Math.Round(minutos / 60)
        If enHoras < 24 Then Return enHoras.ToString("0") & " h"
        Return (minutos / 1440).ToString("0.0") & " días"
    End Function

    Function TextosEtapa(fila As DataRow) As String()
        'los cinco textos de una etapa, iguales en la grilla y en la hoja impresa
        Return {fila("descripcion").ToString(),
                CInt(fila("ordenes")).ToString(),
                TextoDuracion(CDbl(fila("promedio"))),
                TextoDuracion(CDbl(fila("minimo"))),
                TextoDuracion(CDbl(fila("maximo")))}
    End Function

    Sub TiemposPorEtapa()
        'una etapa empieza cuando la orden entra a un estado distinto del anterior
        'y termina en el siguiente cambio de estado; las etapas en curso no se cuentan
        Try
            Dim sql As String = "WITH marcadas AS (" &
                "SELECT h.id_ot_historial, h.id_orden_trabajo, h.id_estado_ot, h.fecha_hora, " &
                "LAG(h.id_estado_ot) OVER (PARTITION BY h.id_orden_trabajo " &
                "ORDER BY h.fecha_hora, h.id_ot_historial) AS estado_anterior " &
                "FROM ot_historial_estado AS h), " &
                "etapas AS (" &
                "SELECT id_orden_trabajo, id_estado_ot, fecha_hora AS inicio, " &
                "LEAD(fecha_hora) OVER (PARTITION BY id_orden_trabajo " &
                "ORDER BY fecha_hora, id_ot_historial) AS fin " &
                "FROM marcadas " &
                "WHERE estado_anterior IS NULL OR estado_anterior <> id_estado_ot) " &
                "SELECT e.codigo, e.descripcion, COUNT(*) AS ordenes, " &
                "AVG(TIMESTAMPDIFF(MINUTE, t.inicio, t.fin)) AS promedio, " &
                "MIN(TIMESTAMPDIFF(MINUTE, t.inicio, t.fin)) AS minimo, " &
                "MAX(TIMESTAMPDIFF(MINUTE, t.inicio, t.fin)) AS maximo " &
                "FROM etapas AS t " &
                "JOIN estado_ot AS e ON e.id_estado_ot = t.id_estado_ot " &
                "WHERE t.fin IS NOT NULL AND e.es_estado_final = 0 " &
                "AND t.inicio >= @desde AND t.inicio < @hasta " &
                "GROUP BY e.id_estado_ot, e.codigo, e.descripcion, e.orden_flujo " &
                "ORDER BY e.orden_flujo;"

            Dim tabla As New DataTable
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Using cmd As New MySqlCommand(sql, cn)
                    AgregarParametros(cmd)
                    Using rd = cmd.ExecuteReader()
                        tabla.Load(rd)
                    End Using
                End Using
            End Using

            tablaEtapas = tabla
            desdeEtapas = dtpDesde.Value.Date
            hastaEtapas = dtpHasta.Value.Date
        Catch ex As Exception
            tablaEtapas = Nothing
            MostrarError("No se pudieron calcular los tiempos por etapa.", ex)
        End Try

        GraficoEtapas()
        GrillaEtapas()
    End Sub

    Sub GraficoEtapas()
        'duracion promedio de cada etapa, en el orden del flujo de la orden
        'el largo de la barra va en dias y el rotulo con la misma unidad que la grilla (min, h o dias)
        Dim titulo As String = "Duración promedio por etapa"
        Try
            PrepararGrafico(fpEtapas)

            If tablaEtapas Is Nothing OrElse tablaEtapas.Rows.Count = 0 Then
                MostrarSinDatos(fpEtapas, titulo)
                Exit Sub
            End If

            'la primera etapa del flujo arriba: invierto las posiciones
            Dim barras As New List(Of ScottPlot.Bar)
            Dim posiciones As New List(Of Double)
            Dim nombres As New List(Of String)
            Dim cantidad As Integer = tablaEtapas.Rows.Count
            For i As Integer = 0 To cantidad - 1
                Dim fila As DataRow = tablaEtapas.Rows(i)
                Dim dias As Double = CDbl(fila("promedio")) / 1440
                Dim posicion As Double = cantidad - 1 - i
                barras.Add(New ScottPlot.Bar With {
                    .Position = posicion,
                    .Value = dias,
                    .FillColor = ColorEstado(fila("codigo").ToString()),
                    .Label = TextoDuracion(CDbl(fila("promedio")))
                })
                posiciones.Add(posicion)
                nombres.Add(fila("descripcion").ToString())
            Next

            Dim grafico = fpEtapas.Plot.Add.Bars(barras)
            'barras horizontales con el nombre de la etapa a la izquierda
            grafico.Horizontal = True
            fpEtapas.Plot.Axes.Left.SetTicks(posiciones.ToArray(), nombres.ToArray())
            'lugar a la derecha para el numero de la barra mas larga
            fpEtapas.Plot.Axes.Margins(left:=0, right:=0.2)
            fpEtapas.Plot.Title(titulo, 13)
            fpEtapas.Plot.Axes.Bottom.Label.Text = "días"
            fpEtapas.Plot.HideGrid()
            fpEtapas.Refresh()
        Catch ex As Exception
            MostrarError("No se pudo armar el gráfico de tiempos por etapa.", ex)
        End Try
    End Sub

    Sub GrillaEtapas()
        'un renglon por etapa con la cantidad y los tiempos promedio, minimo y maximo
        dgvEtapas.Rows.Clear()
        If tablaEtapas Is Nothing Then Exit Sub

        For Each fila As DataRow In tablaEtapas.Rows
            dgvEtapas.Rows.Add(TextosEtapa(fila))
        Next
        dgvEtapas.ClearSelection()
    End Sub

    Sub Actualizar()
        'recalculo todo el tablero con el periodo elegido
        lblSubtitulo.Text = "Período del " & dtpDesde.Value.ToString("dd/MM/yyyy") &
                            " al " & dtpHasta.Value.ToString("dd/MM/yyyy")

        'si la base no responde aviso una sola vez, en lugar de un mensaje por cada indicador y grafico
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
            End Using
        Catch ex As Exception
            MostrarError("No se pudo actualizar el tablero.", ex)
            Exit Sub
        End Try

        'mientras se recalculan los indicadores y los graficos muestro el reloj de espera
        Cursor.Current = Cursors.WaitCursor
        Try
            CargarIndicadores()
            GraficoFacturacion()
            GraficoServicios()
            GraficoHoras()
            GraficoEstados()
            TiemposPorEtapa()
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub FrmTableros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'las grillas se dibujan con doble bufer para que no parpadeen ni se traben al desplazarse
        ActivarDobleBufer(Me)

        'solo el administrador puede ver los tableros (el menu ya oculta el boton)
        If Sesion.Rol <> ROL_ADMINISTRADOR Then
            MessageBox.Show("Solo el administrador puede ver los tableros.")
            pnlFiltros.Enabled = False
            Exit Sub
        End If

        'el periodo arranca en el mes actual: del dia 1 hasta hoy
        dtpDesde.Value = New Date(Date.Today.Year, Date.Today.Month, 1)
        dtpHasta.Value = Date.Today

        Actualizar()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If dtpDesde.Value.Date > dtpHasta.Value.Date Then
            MessageBox.Show("La fecha desde no puede ser posterior a la fecha hasta.")
            Exit Sub
        End If

        Actualizar()
    End Sub

    Private Sub btnImprimirTiempos_Click(sender As Object, e As EventArgs) Handles btnImprimirTiempos.Click
        'la hoja sale de los tiempos que se ven en el tablero
        If tablaEtapas Is Nothing Then
            MessageBox.Show("No hay tiempos por etapa para imprimir. Actualice el tablero y vuelva a intentar.")
            Exit Sub
        End If

        fechaTiempos = DateTime.Now

        'abro la vista previa maximizada; desde su boton de impresora se imprime
        'esta pantalla va dentro de la ventana principal: la vista previa se abre sobre esa ventana
        Try
            prdTiempos.DefaultPageSettings.Margins = New Printing.Margins(MARGEN_HOJA, MARGEN_HOJA, MARGEN_HOJA, MARGEN_HOJA)
            ppdVistaPrevia.Document = prdTiempos
            ppdVistaPrevia.WindowState = FormWindowState.Maximized
            ppdVistaPrevia.ShowDialog(Me.TopLevelControl)
        Catch ex As Exception
            MostrarError("No se pudo preparar la impresión", ex)
        End Try
    End Sub

    Private Sub prdTiempos_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles prdTiempos.BeginPrint
        'la vista previa y la impresion real avisan cada una de su propio error
        errorTiempos = False
    End Sub

    Private Sub prdTiempos_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles prdTiempos.PrintPage
        'un error al dibujar corta la hoja con un aviso, sin cerrar el sistema
        Try
            DibujarHojaTiempos(e)
        Catch ex As Exception
            If Not errorTiempos Then
                errorTiempos = True
                MostrarError("No se pudo dibujar la hoja de tiempos", ex)
            End If
        End Try
        'la hoja es una sola
        e.HasMorePages = False
    End Sub

    Sub DibujarHojaTiempos(e As Printing.PrintPageEventArgs)
        'cabecera con el periodo, la tabla de etapas, las barras y la nota
        Dim g As Graphics = e.Graphics
        Dim area As Rectangle = e.MarginBounds
        Dim x As Integer = area.Left
        Dim ancho As Integer = area.Width
        Dim limite As Integer = area.Bottom - ALTO_PIE
        Dim periodo As String = "Período del " & desdeEtapas.ToString("dd/MM/yyyy") &
                                " al " & hastaEtapas.ToString("dd/MM/yyyy")

        Dim y As Integer = DibujarCabecera(g, x, area.Top, ancho, "Tiempos por etapa", periodo)

        If tablaEtapas.Rows.Count = 0 Then
            DibujarParrafo(g, "No hay etapas terminadas en el período.", x, y, ancho)
        Else
            'tabla con un renglon por etapa
            Dim columnas() As String = {"Etapa", "Órdenes", "Promedio", "Mínimo", "Máximo"}
            Dim pesos() As Single = {2.4F, 1.2F, 1.5F, 1.5F, 1.5F}
            Dim alineaciones() As StringAlignment = {StringAlignment.Near, StringAlignment.Far, StringAlignment.Far,
                                                     StringAlignment.Far, StringAlignment.Far}
            Dim filas As New List(Of String())
            For Each fila As DataRow In tablaEtapas.Rows
                filas.Add(TextosEtapa(fila))
            Next
            Dim filaActual As Integer = 0
            DibujarTabla(g, "Detalle por etapa", columnas, pesos, alineaciones, filas,
                         x, y, ancho, limite, filaActual, True)

            y = DibujarBarrasTiempos(g, x, y + 6, ancho)

            DibujarParrafo(g, "Cada etapa se mide desde que la orden entra al estado hasta que pasa al siguiente. " &
                              "No se cuentan las etapas en curso.", x, y, ancho)
        End If

        DibujarPie(g, area, "Tiempos por etapa · Impreso el " & fechaTiempos.ToString("dd/MM/yyyy HH:mm"), 1)
    End Sub

    Function DibujarBarrasTiempos(g As Graphics, x As Integer, y As Integer, ancho As Integer) As Integer
        'una barra por etapa, larga en proporcion al mayor promedio, con el nombre y el promedio
        Const ANCHO_NOMBRE As Integer = 150
        Const ANCHO_VALOR As Integer = 90
        Const ALTO_BARRA As Integer = 20

        y = DibujarTituloSeccion(g, "Duración promedio por etapa", x, y, ancho)

        Dim mayor As Double = 0
        For Each fila As DataRow In tablaEtapas.Rows
            mayor = Math.Max(mayor, CDbl(fila("promedio")))
        Next

        Dim anchoBarras As Integer = ancho - ANCHO_NOMBRE - ANCHO_VALOR

        Using fuente As New Font("Segoe UI", 9),
              borde As New Pen(Color.Gray),
              centrado As New StringFormat()

            centrado.LineAlignment = StringAlignment.Center
            For Each fila As DataRow In tablaEtapas.Rows
                Dim promedio As Double = CDbl(fila("promedio"))
                'una etapa de pocos minutos igual deja ver una barra finita
                Dim largo As Integer = 2
                If mayor > 0 Then largo = Math.Max(2, CInt(anchoBarras * promedio / mayor))

                'el color de la etapa, el mismo del grafico
                Dim colorEtapa As ScottPlot.Color = ColorEstado(fila("codigo").ToString())

                g.DrawString(fila("descripcion").ToString(), fuente, Brushes.Black,
                             New RectangleF(x, y, ANCHO_NOMBRE, ALTO_BARRA), centrado)
                Using relleno As New SolidBrush(Color.FromArgb(colorEtapa.Alpha, colorEtapa.Red, colorEtapa.Green, colorEtapa.Blue))
                    g.FillRectangle(relleno, x + ANCHO_NOMBRE, y, largo, ALTO_BARRA)
                End Using
                g.DrawRectangle(borde, x + ANCHO_NOMBRE, y, largo, ALTO_BARRA)
                g.DrawString(TextoDuracion(promedio), fuente, Brushes.Black,
                             New RectangleF(x + ANCHO_NOMBRE + largo + 6, y, ANCHO_VALOR, ALTO_BARRA), centrado)
                y += ALTO_BARRA + 8
            Next
        End Using

        Return y + 8
    End Function

    Private Sub FrmTableros_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'la vista previa y el documento de impresion no estan en components: los libero al cerrar
        ppdVistaPrevia.Dispose()
        prdTiempos.Dispose()
    End Sub

End Class

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
            MessageBox.Show("No se pudieron calcular los indicadores." & vbCrLf & "Detalle: " & ex.Message)
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
                etiquetas(i) = nombresMes(mes.Month - 1) & " " & mes.ToString("yy")
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
            MessageBox.Show("No se pudo armar el gráfico de facturación." & vbCrLf & "Detalle: " & ex.Message)
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
            fpServicios.Plot.Axes.Margins(left:=0)
            fpServicios.Plot.Title(titulo, 13)
            fpServicios.Plot.HideGrid()
            fpServicios.Refresh()
        Catch ex As Exception
            MessageBox.Show("No se pudo armar el gráfico de servicios." & vbCrLf & "Detalle: " & ex.Message)
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
                            nombres.Add(rd("nombre_completo").ToString())
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
            MessageBox.Show("No se pudo armar el gráfico de horas." & vbCrLf & "Detalle: " & ex.Message)
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
            MessageBox.Show("No se pudo armar el gráfico de estados." & vbCrLf & "Detalle: " & ex.Message)
        End Try
    End Sub

    Sub Actualizar()
        'recalculo todo el tablero con el periodo elegido
        lblSubtitulo.Text = "Período del " & dtpDesde.Value.ToString("dd/MM/yyyy") &
                            " al " & dtpHasta.Value.ToString("dd/MM/yyyy")
        CargarIndicadores()
        GraficoFacturacion()
        GraficoServicios()
        GraficoHoras()
        GraficoEstados()
    End Sub

    Private Sub FrmTableros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

End Class

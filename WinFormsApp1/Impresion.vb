Imports System.Drawing.Drawing2D

Module Impresion
    'rutinas de dibujo compartidas por los reportes impresos
    'aca no hay consultas: cada formulario lee sus datos y usa estas rutinas en su evento PrintPage
    'todas las medidas son las de la hoja (centesimos de pulgada) y cada rutina devuelve el y siguiente

    'datos del taller que salen en la cabecera de cada reporte; la base no los guarda
    Public Const NOMBRE_TALLER As String = "Taller Mecánico"
    Public Const LEMA_TALLER As String = "Servicio y reparación de vehículos"

    'margen de las hojas impresas, en centesimos de pulgada
    Public Const MARGEN_HOJA As Integer = 60

    'alto que se reserva abajo de cada hoja para el pie
    Public Const ALTO_PIE As Integer = 34

    'dias que vale un presupuesto desde que se emite
    Public Const DIAS_VALIDEZ_PRESUPUESTO As Integer = 15

    'alto del titulo que va debajo de cada foto
    Public Const ALTO_TITULO_FOTO As Integer = 22

    'colores de la impresion: se leen bien tambien en blanco y negro
    ReadOnly colorOscuro As Color = Color.FromArgb(30, 39, 46)
    ReadOnly colorEtiqueta As Color = Color.DimGray
    ReadOnly colorLinea As Color = Color.Gainsboro
    ReadOnly colorFondoTitulo As Color = Color.FromArgb(235, 235, 235)
    ReadOnly colorCebra As Color = Color.FromArgb(247, 247, 247)

    'separacion interna de las celdas y las cajas
    Const RELLENO As Integer = 4

    Function NombreCombustible(codigo As Object) As String
        'paso el valor que guarda la base al texto que lee el usuario, como en la recepcion
        Dim valor As String = ""
        If codigo IsNot Nothing AndAlso Not IsDBNull(codigo) Then valor = codigo.ToString()

        If valor = "VACIO" Then Return "Vacío"
        If valor = "UN_CUARTO" Then Return "Un cuarto"
        If valor = "MEDIO" Then Return "Medio"
        If valor = "TRES_CUARTOS" Then Return "Tres cuartos"
        If valor = "LLENO" Then Return "Lleno"
        Return "Sin registrar"
    End Function

    Function TextoFecha(valor As Object, formato As String) As String
        'fecha opcional de la base: un guion cuando no esta cargada
        If valor Is Nothing OrElse IsDBNull(valor) Then Return "-"
        Return CDate(valor).ToString(formato)
    End Function

    Function AltoTexto(g As Graphics, texto As String, fuente As Font, ancho As Integer) As Integer
        'alto que ocupa un texto partido en renglones dentro de un ancho
        If texto = "" Then texto = " "
        Return CInt(Math.Ceiling(g.MeasureString(texto, fuente, ancho).Height))
    End Function

    Function DibujarCabecera(g As Graphics, x As Integer, y As Integer, ancho As Integer,
                             titulo As String, numero As String) As Integer
        'banda oscura con el nombre del taller a la izquierda y el reporte a la derecha
        Const ALTO_BANDA As Integer = 64

        Using fondo As New SolidBrush(colorOscuro),
              fuenteTaller As New Font("Segoe UI", 16, FontStyle.Bold),
              fuenteLema As New Font("Segoe UI", 8.5F),
              fuenteTitulo As New Font("Segoe UI", 13, FontStyle.Bold),
              fuenteNumero As New Font("Segoe UI", 10),
              claro As New SolidBrush(Color.Gainsboro),
              derecha As New StringFormat()

            g.FillRectangle(fondo, x, y, ancho, ALTO_BANDA)
            g.DrawString(NOMBRE_TALLER, fuenteTaller, Brushes.White, x + 12, y + 8)
            g.DrawString(LEMA_TALLER, fuenteLema, claro, x + 14, y + 38)

            derecha.Alignment = StringAlignment.Far
            Dim bordeDerecho As Integer = x + ancho - 12
            g.DrawString(titulo, fuenteTitulo, Brushes.White, bordeDerecho, y + 9, derecha)
            g.DrawString(numero, fuenteNumero, claro, bordeDerecho, y + 36, derecha)
        End Using

        Return y + ALTO_BANDA + 10
    End Function

    Function DibujarInsignia(g As Graphics, texto As String, colorFondo As Color,
                             derecha As Integer, y As Integer) As Integer
        'etiqueta del estado con su color de fondo claro y borde oscuro, alineada a la derecha
        Using fuente As New Font("Segoe UI", 9.5F, FontStyle.Bold),
              fondo As New SolidBrush(colorFondo),
              borde As New Pen(colorOscuro),
              centrado As New StringFormat()

            Dim medida As SizeF = g.MeasureString(texto, fuente)
            Dim caja As New Rectangle(derecha - CInt(medida.Width) - 20, y, CInt(medida.Width) + 20, 24)

            centrado.Alignment = StringAlignment.Center
            centrado.LineAlignment = StringAlignment.Center
            g.FillRectangle(fondo, caja)
            g.DrawRectangle(borde, caja)
            g.DrawString(texto, fuente, Brushes.Black, caja, centrado)

            Return caja.Bottom
        End Using
    End Function

    Function DibujarTextoChico(g As Graphics, texto As String, x As Integer, y As Integer) As Integer
        'renglon en gris chico, por ejemplo la fecha de impresion
        Using fuente As New Font("Segoe UI", 8.5F),
              pincel As New SolidBrush(colorEtiqueta)
            g.DrawString(texto, fuente, pincel, x, y)
            Return y + CInt(Math.Ceiling(fuente.GetHeight(g)))
        End Using
    End Function

    Function DibujarTituloSeccion(g As Graphics, texto As String, x As Integer, y As Integer, ancho As Integer,
                                  Optional dibujar As Boolean = True) As Integer
        'titulo de una seccion con una linea oscura debajo
        Using fuente As New Font("Segoe UI", 11, FontStyle.Bold)
            Dim alto As Integer = CInt(Math.Ceiling(fuente.GetHeight(g)))
            If dibujar Then
                Using pincel As New SolidBrush(colorOscuro),
                      linea As New Pen(colorOscuro, 1.5F)
                    g.DrawString(texto, fuente, pincel, x, y)
                    g.DrawLine(linea, x, y + alto + 2, x + ancho, y + alto + 2)
                End Using
            End If
            Return y + alto + 8
        End Using
    End Function

    Function DibujarDato(g As Graphics, etiqueta As String, valor As String, x As Integer, y As Integer,
                         ancho As Integer, anchoEtiqueta As Integer, Optional dibujar As Boolean = True) As Integer
        'renglon etiqueta: valor; el valor se parte en renglones si no entra en el ancho
        If valor Is Nothing OrElse valor.Trim() = "" Then valor = "-"

        Using fuenteEtiqueta As New Font("Segoe UI", 9),
              fuenteValor As New Font("Segoe UI", 9.5F)

            Dim anchoValor As Integer = ancho - anchoEtiqueta
            Dim alto As Integer = Math.Max(AltoTexto(g, etiqueta, fuenteEtiqueta, anchoEtiqueta),
                                           AltoTexto(g, valor, fuenteValor, anchoValor))
            If dibujar Then
                Using gris As New SolidBrush(colorEtiqueta)
                    g.DrawString(etiqueta, fuenteEtiqueta, gris, New RectangleF(x, y + 1, anchoEtiqueta, alto))
                    g.DrawString(valor, fuenteValor, Brushes.Black, New RectangleF(x + anchoEtiqueta, y, anchoValor, alto))
                End Using
            End If
            Return y + alto + 3
        End Using
    End Function

    Function DibujarTituloCaja(g As Graphics, titulo As String, x As Integer, y As Integer, ancho As Integer,
                               Optional dibujar As Boolean = True) As Integer
        'franja gris con el titulo de una caja; el borde lo dibuja DibujarBordeCaja al final
        Const ALTO_TITULO As Integer = 24
        If dibujar Then
            Using fondo As New SolidBrush(colorFondoTitulo),
                  fuente As New Font("Segoe UI", 9.5F, FontStyle.Bold),
                  centrado As New StringFormat()
                centrado.LineAlignment = StringAlignment.Center
                g.FillRectangle(fondo, x, y, ancho, ALTO_TITULO)
                g.DrawString(titulo, fuente, Brushes.Black, New RectangleF(x + RELLENO * 2, y, ancho, ALTO_TITULO), centrado)
            End Using
        End If
        Return y + ALTO_TITULO + RELLENO * 2
    End Function

    Sub DibujarBordeCaja(g As Graphics, x As Integer, yInicio As Integer, ancho As Integer, yFin As Integer)
        'borde fino alrededor de una caja ya dibujada
        Using borde As New Pen(Color.DarkGray)
            g.DrawRectangle(borde, x, yInicio, ancho, yFin - yInicio)
        End Using
    End Sub

    Function DibujarRecuadro(g As Graphics, titulo As String, texto As String, x As Integer, y As Integer,
                             ancho As Integer, Optional dibujar As Boolean = True) As Integer
        'caja con titulo y un texto libre adentro, por ejemplo el motivo de una anulacion
        Dim yTexto As Integer = DibujarTituloCaja(g, titulo, x, y, ancho, dibujar)
        Using fuente As New Font("Segoe UI", 9.5F)
            Dim alto As Integer = AltoTexto(g, texto, fuente, ancho - RELLENO * 4)
            If dibujar Then
                g.DrawString(texto, fuente, Brushes.Black, New RectangleF(x + RELLENO * 2, yTexto, ancho - RELLENO * 4, alto))
            End If
            Dim yFin As Integer = yTexto + alto + RELLENO * 2
            If dibujar Then DibujarBordeCaja(g, x, y, ancho, yFin)
            Return yFin + 12
        End Using
    End Function

    Function DibujarImporte(g As Graphics, etiqueta As String, importe As String, xDerecha As Integer, y As Integer,
                            Optional destacado As Boolean = False, Optional dibujar As Boolean = True) As Integer
        'renglon de total: la etiqueta y el importe alineados a la derecha, el importe en su columna
        Const ANCHO_IMPORTE As Integer = 130
        Dim estilo As FontStyle = FontStyle.Regular
        If destacado Then estilo = FontStyle.Bold

        Using fuente As New Font("Segoe UI", 10, estilo),
              derecha As New StringFormat()
            Dim alto As Integer = CInt(Math.Ceiling(fuente.GetHeight(g)))
            If dibujar Then
                derecha.Alignment = StringAlignment.Far
                g.DrawString(etiqueta, fuente, Brushes.Black, xDerecha - ANCHO_IMPORTE, y, derecha)
                g.DrawString(importe, fuente, Brushes.Black, xDerecha, y, derecha)
            End If
            Return y + alto + 2
        End Using
    End Function

    Function AnchosColumnas(pesos() As Single, ancho As Integer) As Integer()
        'reparto el ancho de una tabla entre sus columnas segun el peso de cada una
        Dim total As Single = 0
        For Each peso As Single In pesos
            total += peso
        Next

        Dim anchos(pesos.Length - 1) As Integer
        Dim usado As Integer = 0
        For i As Integer = 0 To pesos.Length - 1
            anchos(i) = CInt(ancho * pesos(i) / total)
            usado += anchos(i)
        Next
        'lo que sobra por redondeo va a la primera columna
        anchos(0) += ancho - usado
        Return anchos
    End Function

    Function DibujarFila(g As Graphics, valores() As String, anchos() As Integer, alineaciones() As StringAlignment,
                         x As Integer, y As Integer, encabezado As Boolean, cebra As Boolean,
                         Optional dibujar As Boolean = True) As Integer
        'una fila de tabla: el encabezado va en negrita sobre gris, las demas con cebra muy clara
        Dim estilo As FontStyle = FontStyle.Regular
        If encabezado Then estilo = FontStyle.Bold

        Using fuente As New Font("Segoe UI", 9, estilo),
              formato As New StringFormat()

            formato.Trimming = StringTrimming.EllipsisWord

            'el alto de la fila es el de la celda mas alta
            Dim alto As Integer = 0
            For i As Integer = 0 To valores.Length - 1
                alto = Math.Max(alto, AltoTexto(g, valores(i), fuente, anchos(i) - RELLENO * 2))
            Next
            alto += RELLENO * 2

            If dibujar Then
                Dim anchoTotal As Integer = 0
                For Each anchoColumna As Integer In anchos
                    anchoTotal += anchoColumna
                Next

                If encabezado OrElse cebra Then
                    Dim colorFondo As Color = colorCebra
                    If encabezado Then colorFondo = colorFondoTitulo
                    Using fondo As New SolidBrush(colorFondo)
                        g.FillRectangle(fondo, x, y, anchoTotal, alto)
                    End Using
                End If

                Dim xCelda As Integer = x
                For i As Integer = 0 To valores.Length - 1
                    formato.Alignment = alineaciones(i)
                    g.DrawString(valores(i), fuente, Brushes.Black,
                                 New RectangleF(xCelda + RELLENO, y + RELLENO, anchos(i) - RELLENO * 2, alto - RELLENO * 2),
                                 formato)
                    xCelda += anchos(i)
                Next

                'linea de cierre de la fila, mas marcada debajo del encabezado
                Dim colorRaya As Color = colorLinea
                If encabezado Then colorRaya = Color.Gray
                Using raya As New Pen(colorRaya)
                    g.DrawLine(raya, x, y + alto, x + anchoTotal, y + alto)
                End Using
            End If

            Return y + alto
        End Using
    End Function

    Function DibujarTabla(g As Graphics, titulo As String, columnas() As String, pesos() As Single,
                          alineaciones() As StringAlignment, filas As List(Of String()),
                          x As Integer, ByRef y As Integer, ancho As Integer, limite As Integer,
                          ByRef filaActual As Integer, inicioHoja As Boolean) As Boolean
        'tabla con titulo y encabezado que sigue en la hoja siguiente si no entra
        'filaActual es la primera fila que falta imprimir; devuelve True cuando la tabla termino
        'si la hoja esta empezada y no entra ni el encabezado con una fila, no dibuja nada y devuelve False
        Dim anchos() As Integer = AnchosColumnas(pesos, ancho)

        Dim textoTitulo As String = titulo
        If filaActual > 0 Then textoTitulo = titulo & " (continuación)"

        'compruebo que entre el titulo, el encabezado y la primera fila pendiente
        Dim yPrueba As Integer = DibujarTituloSeccion(g, textoTitulo, x, y, ancho, False)
        yPrueba = DibujarFila(g, columnas, anchos, alineaciones, x, yPrueba, True, False, False)
        If filaActual < filas.Count Then
            yPrueba = DibujarFila(g, filas(filaActual), anchos, alineaciones, x, yPrueba, False, False, False)
        End If
        If yPrueba > limite AndAlso Not inicioHoja Then Return False

        y = DibujarTituloSeccion(g, textoTitulo, x, y, ancho)
        y = DibujarFila(g, columnas, anchos, alineaciones, x, y, True, False)

        Dim primera As Boolean = True
        Do While filaActual < filas.Count
            Dim yFila As Integer = DibujarFila(g, filas(filaActual), anchos, alineaciones, x, y, False, False, False)
            'la primera fila de la hoja se dibuja siempre, asi la tabla avanza aunque la fila sea muy alta
            If yFila > limite AndAlso Not primera Then Return False

            y = DibujarFila(g, filas(filaActual), anchos, alineaciones, x, y, False, filaActual Mod 2 = 1)
            filaActual += 1
            primera = False
        Loop

        y += 12
        Return True
    End Function

    Sub DibujarPie(g As Graphics, area As Rectangle, textoIzquierda As String, hoja As Integer)
        'pie de cada hoja: una linea, un texto a la izquierda y el numero de hoja a la derecha
        Dim y As Integer = area.Bottom - ALTO_PIE + 10

        Using linea As New Pen(colorLinea),
              fuente As New Font("Segoe UI", 8.5F),
              gris As New SolidBrush(colorEtiqueta),
              derecha As New StringFormat()

            derecha.Alignment = StringAlignment.Far
            g.DrawLine(linea, area.Left, y, area.Right, y)
            g.DrawString(textoIzquierda, fuente, gris, area.Left, y + 4)
            g.DrawString("Hoja " & hoja.ToString(), fuente, gris, area.Right, y + 4, derecha)
        End Using
    End Sub

    Sub DibujarMarcaAgua(g As Graphics, area As Rectangle, texto As String)
        'texto grande, gris muy claro y en diagonal, detras de todo lo demas de la hoja
        Dim estado As GraphicsState = g.Save()

        Using fuente As New Font("Segoe UI", 80, FontStyle.Bold),
              pincel As New SolidBrush(Color.FromArgb(228, 228, 228)),
              centrado As New StringFormat()

            centrado.Alignment = StringAlignment.Center
            centrado.LineAlignment = StringAlignment.Center
            g.TranslateTransform(area.Left + area.Width / 2.0F, area.Top + area.Height / 2.0F)
            g.RotateTransform(-40)
            g.DrawString(texto, fuente, pincel, 0, 0, centrado)
        End Using

        g.Restore(estado)
    End Sub

    Function DibujarParrafo(g As Graphics, texto As String, x As Integer, y As Integer, ancho As Integer,
                            Optional dibujar As Boolean = True) As Integer
        'texto corrido en gris, partido en renglones, por ejemplo una nota al pie de un presupuesto
        Using fuente As New Font("Segoe UI", 9)
            Dim alto As Integer = AltoTexto(g, texto, fuente, ancho)
            If dibujar Then
                Using gris As New SolidBrush(Color.FromArgb(70, 70, 70))
                    g.DrawString(texto, fuente, gris, New RectangleF(x, y, ancho, alto))
                End Using
            End If
            Return y + alto + 4
        End Using
    End Function

    Function DibujarFirmas(g As Graphics, leyendas() As String, x As Integer, y As Integer, ancho As Integer,
                           Optional dibujar As Boolean = True) As Integer
        'lineas de firma repartidas en el ancho, con su leyenda centrada debajo (puede tener dos renglones)
        Const ESPACIO_FIRMA As Integer = 60
        Const SEPARACION As Integer = 30

        Dim anchoFirma As Integer = (ancho - SEPARACION * (leyendas.Length - 1)) \ leyendas.Length
        Dim yLinea As Integer = y + ESPACIO_FIRMA

        Using fuente As New Font("Segoe UI", 9),
              linea As New Pen(Color.Black),
              centrado As New StringFormat()

            centrado.Alignment = StringAlignment.Center
            Dim altoLeyenda As Integer = 0
            For i As Integer = 0 To leyendas.Length - 1
                Dim xFirma As Integer = x + i * (anchoFirma + SEPARACION)
                Dim alto As Integer = AltoTexto(g, leyendas(i), fuente, anchoFirma)
                altoLeyenda = Math.Max(altoLeyenda, alto)
                If dibujar Then
                    g.DrawLine(linea, xFirma, yLinea, xFirma + anchoFirma, yLinea)
                    g.DrawString(leyendas(i), fuente, Brushes.Black, New RectangleF(xFirma, yLinea + 4, anchoFirma, alto), centrado)
                End If
            Next
            Return yLinea + 4 + altoLeyenda + 16
        End Using
    End Function

    Sub DibujarFoto(g As Graphics, foto As Image, titulo As String, aviso As String, caja As Rectangle)
        'cuadro de una foto con borde y su titulo debajo; sin foto muestra el aviso en gris
        Using borde As New Pen(Color.DarkGray),
              fuente As New Font("Segoe UI", 9, FontStyle.Bold),
              fuenteAviso As New Font("Segoe UI", 9),
              gris As New SolidBrush(colorEtiqueta),
              fondo As New SolidBrush(colorCebra),
              centrado As New StringFormat()

            centrado.Alignment = StringAlignment.Center
            centrado.LineAlignment = StringAlignment.Center

            If foto Is Nothing Then
                g.FillRectangle(fondo, caja)
                g.DrawString(aviso, fuenteAviso, gris, caja, centrado)
            Else
                Dim adentro As Rectangle = caja
                adentro.Inflate(-RELLENO, -RELLENO)
                DibujarImagenAjustada(g, foto, adentro)
            End If
            g.DrawRectangle(borde, caja)

            g.DrawString(titulo, fuente, Brushes.Black,
                         New RectangleF(caja.Left, caja.Bottom, caja.Width, ALTO_TITULO_FOTO), centrado)
        End Using
    End Sub

    Sub DibujarImagenAjustada(g As Graphics, imagen As Image, caja As Rectangle)
        'dibujo una imagen lo mas grande posible dentro de la caja, sin deformarla y centrada
        If imagen Is Nothing OrElse imagen.Width = 0 OrElse imagen.Height = 0 Then Exit Sub

        Dim escala As Double = Math.Min(caja.Width / imagen.Width, caja.Height / imagen.Height)
        Dim ancho As Integer = CInt(imagen.Width * escala)
        Dim alto As Integer = CInt(imagen.Height * escala)
        Dim x As Integer = caja.Left + (caja.Width - ancho) \ 2
        Dim y As Integer = caja.Top + (caja.Height - alto) \ 2

        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.DrawImage(imagen, x, y, ancho, alto)
    End Sub

End Module

Imports System.IO
Imports System.Text

Module Exportar
    'rutinas compartidas para exportar una grilla a un archivo CSV
    'las usan el tablero de ordenes, el historial por patente y mis ordenes

    Sub ExportarGrilla(grilla As DataGridView, nombreSugerido As String)
        'pido donde guardar y escribo lo que se ve en la grilla; con la grilla vacia no abro el dialogo
        If ContarFilas(grilla) = 0 Then
            MessageBox.Show("No hay filas para exportar.")
            Exit Sub
        End If

        'nombre sugerido con la fecha de hoy, el usuario puede cambiarlo
        Dim ruta As String = ""
        Using dialogo As New SaveFileDialog()
            dialogo.Filter = "Archivo CSV (*.csv)|*.csv"
            dialogo.FileName = nombreSugerido & "-" & Date.Today.ToString("yyyy-MM-dd") & ".csv"
            If dialogo.ShowDialog() <> DialogResult.OK Then Exit Sub
            ruta = dialogo.FileName
        End Using

        'escribo primero un archivo temporal al lado y recien al terminar lo paso al nombre elegido:
        'si la escritura falla a mitad de camino, el archivo que ya existia no queda reemplazado por uno cortado
        'el temporal tiene un nombre al azar en la misma carpeta, asi nunca pisa otro archivo del usuario
        Dim temporal As String = Path.Combine(Path.GetDirectoryName(ruta), Path.GetRandomFileName())
        Try
            Dim filas As Integer = EscribirCsv(grilla, temporal)
            File.Move(temporal, ruta, True)
            MessageBox.Show("Se exportaron " & filas & " filas a " & ruta)
        Catch ex As Exception
            'disco lleno, carpeta sin permiso o archivo abierto en otro programa
            Try
                If File.Exists(temporal) Then File.Delete(temporal)
            Catch
                'si no se puede borrar el temporal, queda junto al archivo; no cambia nada para el usuario
            End Try
            MostrarError("No se pudo guardar el archivo", ex)
        End Try
    End Sub

    Function ContarFilas(grilla As DataGridView) As Integer
        'cuento las filas con datos, sin la fila nueva que algunas grillas muestran al final
        Dim filas As Integer = 0
        For Each fila As DataGridViewRow In grilla.Rows
            If Not fila.IsNewRow Then filas += 1
        Next
        Return filas
    End Function

    Function EscribirCsv(grilla As DataGridView, ruta As String) As Integer
        'escribo el archivo: UTF-8 con BOM para que Excel lea bien los acentos, separador ; y fin de linea CRLF
        'primera linea los titulos de las columnas visibles, despues una linea por fila con el texto tal como se ve
        'devuelve la cantidad de filas escritas, sin contar la de los titulos
        Dim filas As Integer = 0

        Using escritor As New StreamWriter(ruta, False, New UTF8Encoding(True))
            escritor.NewLine = vbCrLf

            'titulos, en el orden en que se ven las columnas
            Dim linea As New StringBuilder
            Dim columna As DataGridViewColumn = grilla.Columns.GetFirstColumn(DataGridViewElementStates.Visible)
            While columna IsNot Nothing
                If linea.Length > 0 Then linea.Append(";")
                linea.Append(Campo(columna.HeaderText))
                columna = grilla.Columns.GetNextColumn(columna, DataGridViewElementStates.Visible, DataGridViewElementStates.None)
            End While
            escritor.WriteLine(linea.ToString())

            'una linea por fila, con el valor formateado de cada celda visible (fechas e importes como en pantalla)
            For Each fila As DataGridViewRow In grilla.Rows
                If fila.IsNewRow Then Continue For

                linea.Clear()
                columna = grilla.Columns.GetFirstColumn(DataGridViewElementStates.Visible)
                While columna IsNot Nothing
                    If linea.Length > 0 Then linea.Append(";")

                    Dim valor As Object = fila.Cells(columna.Index).FormattedValue
                    Dim texto As String = ""
                    If valor IsNot Nothing Then texto = valor.ToString()
                    linea.Append(Campo(texto))

                    columna = grilla.Columns.GetNextColumn(columna, DataGridViewElementStates.Visible, DataGridViewElementStates.None)
                End While
                escritor.WriteLine(linea.ToString())
                filas += 1
            Next
        End Using

        Return filas
    End Function

    Function Campo(texto As String) As String
        'cada campo va entre comillas; una comilla adentro del texto se escribe doble, como pide el formato CSV
        'un texto que empieza con = + - @, tabulador o retorno se antepone con un apostrofo:
        'Excel lo tomaria como formula y lo ejecutaria (algunas planillas ignoran el tabulador o el retorno inicial)
        Dim seguro As String = texto
        If seguro <> "" AndAlso ("=+-@" & vbTab & vbCr).Contains(seguro(0)) Then seguro = "'" & seguro
        Return """" & seguro.Replace("""", """""") & """"
    End Function

End Module

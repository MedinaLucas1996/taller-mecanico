Imports MySqlConnector

Public Class FrmMarcasModelos

    'marca elegida en la lista de la izquierda, queda en 0 mientras se carga una nueva (el ID no se muestra)
    Private idMarca As Integer = 0
    'nombre guardado de la marca elegida, para los titulos y los mensajes
    Private nombreMarca As String = ""
    'modelo elegido en la lista de la derecha, queda en 0 mientras se carga uno nuevo
    Private idModelo As Integer = 0
    'nombre guardado del modelo elegido
    Private nombreModelo As String = ""

    'queda en True mientras el programa carga u ordena una grilla, para no tomarlo como una eleccion del usuario
    Private cargando As Boolean = False

    ' =========================================================
    ' RUTINAS COMUNES A LAS DOS LISTAS
    ' =========================================================

    Function MarcarFila(grilla As DataGridView, clave As String, id As Integer) As DataGridViewRow
        'dejo marcada en esa lista la fila con esa clave: celda actual y seleccion juntas
        'con clave 0, o si no esta listada, no queda nada marcado; devuelve la fila o Nothing
        Dim encontrada As DataGridViewRow = Nothing
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If id <> 0 AndAlso grilla.Columns.Contains(clave) Then
                For Each fila As DataGridViewRow In grilla.Rows
                    If CInt(fila.Cells(clave).Value) = id Then encontrada = fila
                Next
            End If

            grilla.ClearSelection()
            If encontrada Is Nothing Then
                grilla.CurrentCell = Nothing
            Else
                grilla.CurrentCell = encontrada.Cells("descripcion")
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    ' =========================================================
    ' MARCAS
    ' =========================================================

    Sub CargarMarcas()
        'cargo las marcas con la cantidad de modelos de cada una y dejo marcada la que se estaba viendo
        cargando = True

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'uso LEFT JOIN para que aparezcan tambien las marcas sin modelos
                Dim consulta As String =
                    "SELECT ma.id_marca, ma.descripcion, COUNT(mo.id_modelo) AS modelos " &
                    "FROM marca AS ma " &
                    "LEFT JOIN modelo AS mo ON mo.id_marca = ma.id_marca " &
                    "GROUP BY ma.id_marca, ma.descripcion " &
                    "ORDER BY ma.descripcion;"

                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    dgvMarcas.DataSource = tabla

                    'la clave se usa al elegir la fila, no se muestra
                    dgvMarcas.Columns("id_marca").Visible = False

                    'pongo titulos legibles en las columnas; la cantidad va alineada a la derecha, con su titulo
                    dgvMarcas.Columns("descripcion").HeaderText = "Marca"
                    dgvMarcas.Columns("descripcion").FillWeight = 70
                    dgvMarcas.Columns("modelos").HeaderText = "Modelos"
                    dgvMarcas.Columns("modelos").FillWeight = 30
                    dgvMarcas.Columns("modelos").MinimumWidth = 80
                    dgvMarcas.Columns("modelos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvMarcas.Columns("modelos").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

                    'el orden por columna lo hace el formulario al hacer click en el titulo (ver ColumnHeaderMouseClick)
                    dgvMarcas.Columns("descripcion").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvMarcas.Columns("modelos").SortMode = DataGridViewColumnSortMode.Programmatic

                    'en el subtitulo cuento las marcas cargadas
                    If tabla.Rows.Count = 1 Then
                        lblSubtitulo.Text = "1 marca"
                    Else
                        lblSubtitulo.Text = tabla.Rows.Count & " marcas"
                    End If
                End Using
            End Using

            'en la lista nueva dejo marcada la marca que se estaba viendo
            MarcarFila(dgvMarcas, "id_marca", idMarca)
        Catch ex As Exception
            MessageBox.Show("Error al cargar las marcas: " & ex.Message)
        Finally
            'pase lo que pase, la grilla vuelve a atender al usuario
            cargando = False
        End Try
    End Sub

    Sub MostrarMarca(fila As DataGridViewRow)
        'la marca de la fila queda elegida: se puede cambiar su nombre o eliminarla, y a la derecha se ven sus modelos
        idMarca = CInt(fila.Cells("id_marca").Value)
        nombreMarca = fila.Cells("descripcion").Value.ToString()

        txtMarca.Text = nombreMarca
        lblMarca.Text = "Nombre de la marca " & nombreMarca & " (*)"
        btnGuardarMarca.Text = " Guardar cambios"
        btnEliminarMarca.Visible = True

        'los modelos siempre pertenecen a una marca: la tarjeta de la derecha muestra los de esta
        lblTituloModelos.Text = "Modelos de " & nombreMarca
        lblAyudaModelos.Visible = False
        btnNuevoModelo.Visible = True
        dgvModelos.Visible = True
        lblModelo.Visible = True
        txtModelo.Visible = True
        btnGuardarModelo.Visible = True

        idModelo = 0
        CargarModelos()
        NuevoModelo()
    End Sub

    Sub NuevaMarca()
        'dejo la tarjeta de marcas lista para cargar una nueva; sin marca elegida no hay modelos para mostrar
        idMarca = 0
        nombreMarca = ""

        txtMarca.Clear()
        lblMarca.Text = "Nombre de la marca nueva (*)"
        btnGuardarMarca.Text = " Guardar"
        btnEliminarMarca.Visible = False
        MarcarFila(dgvMarcas, "id_marca", 0)

        idModelo = 0
        nombreModelo = ""
        lblTituloModelos.Text = "Modelos"
        dgvModelos.DataSource = Nothing
        lblAyudaModelos.Visible = True
        btnNuevoModelo.Visible = False
        dgvModelos.Visible = False
        lblModelo.Visible = False
        txtModelo.Visible = False
        btnGuardarModelo.Visible = False
        btnEliminarModelo.Visible = False
    End Sub

    Sub ElegirMarca(fila As DataGridViewRow)
        'el usuario eligio una marca de la lista
        Dim idElegido As Integer = CInt(fila.Cells("id_marca").Value)

        'si ya es la marca elegida no hay nada que hacer
        If idElegido = idMarca Then Exit Sub

        MostrarMarca(fila)
    End Sub

    ' =========================================================
    ' MODELOS (siempre de la marca elegida)
    ' =========================================================

    Sub CargarModelos()
        'cargo los modelos de la marca elegida y dejo marcado el que se estaba viendo
        cargando = True

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_modelo, descripcion FROM modelo WHERE id_marca = @id_marca ORDER BY descripcion;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_marca", idMarca)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    dgvModelos.DataSource = tabla

                    'la clave se usa al elegir la fila, no se muestra
                    dgvModelos.Columns("id_modelo").Visible = False
                    dgvModelos.Columns("descripcion").HeaderText = "Modelo"
                    dgvModelos.Columns("descripcion").SortMode = DataGridViewColumnSortMode.Programmatic
                End Using
            End Using

            MarcarFila(dgvModelos, "id_modelo", idModelo)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los modelos: " & ex.Message)
        Finally
            cargando = False
        End Try
    End Sub

    Sub MostrarModelo(fila As DataGridViewRow)
        'el modelo de la fila queda elegido: se puede cambiar su nombre o eliminarlo
        idModelo = CInt(fila.Cells("id_modelo").Value)
        nombreModelo = fila.Cells("descripcion").Value.ToString()

        txtModelo.Text = nombreModelo
        lblModelo.Text = "Nombre del modelo " & nombreModelo & " (*)"
        btnGuardarModelo.Text = " Guardar cambios"
        btnEliminarModelo.Visible = True
    End Sub

    Sub NuevoModelo()
        'dejo la tarjeta de modelos lista para cargar uno nuevo de la marca elegida
        idModelo = 0
        nombreModelo = ""

        txtModelo.Clear()
        lblModelo.Text = "Nombre del modelo nuevo de " & nombreMarca & " (*)"
        btnGuardarModelo.Text = " Guardar"
        btnEliminarModelo.Visible = False
        MarcarFila(dgvModelos, "id_modelo", 0)
    End Sub

    Sub ElegirModelo(fila As DataGridViewRow)
        'el usuario eligio un modelo de la lista
        Dim idElegido As Integer = CInt(fila.Cells("id_modelo").Value)

        'si ya es el modelo elegido no hay nada que hacer
        If idElegido = idModelo Then Exit Sub

        MostrarModelo(fila)
    End Sub

    ' =========================================================
    ' EVENTOS DEL FORMULARIO Y DE LAS LISTAS
    ' =========================================================

    Private Sub FrmMarcasModelos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnNuevaMarca.Image = LeerIcono("agregar.png")
        btnNuevoModelo.Image = LeerIcono("agregar.png")
        btnGuardarMarca.Image = LeerIcono("guardar.png")
        btnGuardarModelo.Image = LeerIcono("guardar.png")
        btnEliminarMarca.Image = LeerIcono("baja-rojo.png")
        btnEliminarModelo.Image = LeerIcono("baja-rojo.png")

        'el encabezado de cada lista lleva solo una linea clara debajo, como las filas
        dgvMarcas.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single
        dgvModelos.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'cargo la grilla de marcas al abrir el formulario, sin marca elegida
        NuevaMarca()
        CargarMarcas()
    End Sub

    Private Sub FrmMarcasModelos_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(dgvMarcas, "id_marca", idMarca)
    End Sub

    Private Sub dgvMarcas_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvMarcas.DataBindingComplete
        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcada solo la marca elegida
        MarcarFila(dgvMarcas, "id_marca", idMarca)
    End Sub

    Private Sub dgvModelos_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvModelos.DataBindingComplete
        'lo mismo para los modelos
        MarcarFila(dgvModelos, "id_modelo", idModelo)
    End Sub

    Private Sub Grilla_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles _
        dgvMarcas.ColumnHeaderMouseClick, dgvModelos.ColumnHeaderMouseClick

        'ordeno la lista por la columna del titulo que se toco; otro click en la misma columna invierte el orden
        'ordenar nunca cambia la marca ni el modelo elegidos
        If e.Button <> MouseButtons.Left Then Exit Sub
        If cargando Then Exit Sub

        'sender es la grilla en la que se hizo click
        Dim grilla As DataGridView = DirectCast(sender, DataGridView)
        Dim columna As DataGridViewColumn = grilla.Columns(e.ColumnIndex)
        Dim sentido As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        If grilla.SortedColumn Is columna AndAlso grilla.SortOrder = SortOrder.Ascending Then
            sentido = System.ComponentModel.ListSortDirection.Descending
        End If

        'mientras se ordena la grilla mueve sola su seleccion: eso no es una eleccion del usuario
        cargando = True
        Try
            grilla.Sort(columna, sentido)

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

    Private Sub dgvMarcas_Sorted(sender As Object, e As EventArgs) Handles dgvMarcas.Sorted
        'las filas cambiaron de lugar: vuelvo a marcar la marca elegida
        MarcarFila(dgvMarcas, "id_marca", idMarca)
    End Sub

    Private Sub dgvModelos_Sorted(sender As Object, e As EventArgs) Handles dgvModelos.Sorted
        MarcarFila(dgvModelos, "id_modelo", idModelo)
    End Sub

    Private Sub dgvMarcas_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMarcas.SelectionChanged
        'elijo la marca que el usuario marca con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargando Then Exit Sub
        If Not dgvMarcas.Focused Then Exit Sub
        If dgvMarcas.SelectedRows.Count = 0 Then Exit Sub

        ElegirMarca(dgvMarcas.SelectedRows(0))
    End Sub

    Private Sub dgvMarcas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMarcas.CellClick
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirMarca(dgvMarcas.Rows(e.RowIndex))
    End Sub

    Private Sub dgvModelos_SelectionChanged(sender As Object, e As EventArgs) Handles dgvModelos.SelectionChanged
        'elijo el modelo que el usuario marca con el mouse o con el teclado
        If cargando Then Exit Sub
        If Not dgvModelos.Focused Then Exit Sub
        If dgvModelos.SelectedRows.Count = 0 Then Exit Sub

        ElegirModelo(dgvModelos.SelectedRows(0))
    End Sub

    Private Sub dgvModelos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvModelos.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirModelo(dgvModelos.Rows(e.RowIndex))
    End Sub

    ' =========================================================
    ' BOTONES DE MARCAS
    ' =========================================================

    Private Sub btnNuevaMarca_Click(sender As Object, e As EventArgs) Handles btnNuevaMarca.Click
        'dejo de ver la marca elegida para cargar una nueva
        NuevaMarca()
        txtMarca.Focus()
    End Sub

    Private Sub btnGuardarMarca_Click(sender As Object, e As EventArgs) Handles btnGuardarMarca.Click
        'guardo una marca nueva, o el nombre nuevo de la marca elegida
        If txtMarca.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre de la marca")
            txtMarca.Focus()
            Exit Sub
        End If

        If idMarca = 0 Then
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    Dim consulta As String = "INSERT INTO marca (descripcion) VALUES (@descripcion);"
                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@descripcion", txtMarca.Text.Trim)
                        Dim Resultado As Integer = cmd.ExecuteNonQuery()

                        'la marca nueva queda elegida, lista para cargarle modelos
                        idMarca = CInt(cmd.LastInsertedId)

                        MessageBox.Show("Registros agregados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: la marca ya existe (restriccion unica)
                MessageBox.Show("Ya existe una marca con ese nombre.")
                txtMarca.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al guardar " & ex.Message)
                Exit Sub
            End Try
        Else
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    Dim consulta As String = "UPDATE marca SET descripcion=@descripcion WHERE id_marca=@id;"
                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@descripcion", txtMarca.Text.Trim)
                        cmd.Parameters.AddWithValue("@id", idMarca)
                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        MessageBox.Show("Registros actualizados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                MessageBox.Show("Ya existe otra marca con ese nombre.")
                txtMarca.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al modificar " & ex.Message)
                Exit Sub
            End Try
        End If

        'recargo la lista y vuelvo a mostrar la marca guardada, con sus modelos
        CargarMarcas()
        Dim fila As DataGridViewRow = MarcarFila(dgvMarcas, "id_marca", idMarca)
        If fila IsNot Nothing Then MostrarMarca(fila)
    End Sub

    Private Sub btnEliminarMarca_Click(sender As Object, e As EventArgs) Handles btnEliminarMarca.Click
        'elimino la marca elegida
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Eliminar la marca " & nombreMarca & "?",
            "Eliminar marca",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "DELETE FROM marca WHERE id_marca=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", idMarca)
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros eliminados: " & Resultado)
                End Using
            End Using

        Catch ex As MySqlException When ex.Number = 1451
            'error 1451: la base no deja borrar porque hay registros que dependen de esta marca
            MessageBox.Show("No se puede eliminar: la marca tiene modelos cargados. Elimine primero sus modelos.")
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("Error al eliminar " & ex.Message)
            Exit Sub
        End Try

        'la marca ya no existe: ninguna queda elegida
        NuevaMarca()
        CargarMarcas()
    End Sub

    ' =========================================================
    ' BOTONES DE MODELOS
    ' =========================================================

    Private Sub btnNuevoModelo_Click(sender As Object, e As EventArgs) Handles btnNuevoModelo.Click
        'dejo de ver el modelo elegido para cargar uno nuevo de la misma marca
        NuevoModelo()
        txtModelo.Focus()
    End Sub

    Private Sub btnGuardarModelo_Click(sender As Object, e As EventArgs) Handles btnGuardarModelo.Click
        'guardo un modelo nuevo de la marca elegida, o el nombre nuevo del modelo elegido
        If txtModelo.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre del modelo")
            txtModelo.Focus()
            Exit Sub
        End If

        If idModelo = 0 Then
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    Dim consulta As String =
                        "INSERT INTO modelo (id_marca, descripcion) VALUES (@id_marca, @descripcion);"
                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@id_marca", idMarca)
                        cmd.Parameters.AddWithValue("@descripcion", txtModelo.Text.Trim)
                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        MessageBox.Show("Registros agregados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: esta marca ya tiene un modelo con ese nombre
                MessageBox.Show("La marca " & nombreMarca & " ya tiene un modelo con ese nombre.")
                txtModelo.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al guardar " & ex.Message)
                Exit Sub
            End Try

            'recargo las marcas para actualizar la cantidad de modelos
            CargarMarcas()
        Else
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    Dim consulta As String = "UPDATE modelo SET descripcion=@descripcion WHERE id_modelo=@id;"
                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@descripcion", txtModelo.Text.Trim)
                        cmd.Parameters.AddWithValue("@id", idModelo)
                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        MessageBox.Show("Registros actualizados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                MessageBox.Show("La marca " & nombreMarca & " ya tiene otro modelo con ese nombre.")
                txtModelo.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al modificar " & ex.Message)
                Exit Sub
            End Try
        End If

        'recargo los modelos y dejo la tarjeta lista para cargar otro, como antes
        CargarModelos()
        NuevoModelo()
        txtModelo.Focus()
    End Sub

    Private Sub btnEliminarModelo_Click(sender As Object, e As EventArgs) Handles btnEliminarModelo.Click
        'elimino el modelo elegido
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Eliminar el modelo " & nombreModelo & "?",
            "Eliminar modelo",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "DELETE FROM modelo WHERE id_modelo=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", idModelo)
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros eliminados: " & Resultado)
                End Using
            End Using

        Catch ex As MySqlException When ex.Number = 1451
            'error 1451: hay vehiculos cargados con este modelo
            MessageBox.Show("No se puede eliminar: hay vehículos cargados con ese modelo.")
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("Error al eliminar " & ex.Message)
            Exit Sub
        End Try

        'el modelo ya no existe: recargo las dos listas (la de marcas, por la cantidad de modelos)
        idModelo = 0
        CargarModelos()
        CargarMarcas()
        NuevoModelo()
    End Sub

End Class

Imports MySqlConnector

Public Class FrmMarcasModelos

    ' =========================================================
    ' MARCAS
    ' =========================================================

    Sub CargarMarcas()
        'cargo las marcas con la cantidad de modelos de cada una
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

                    'pongo titulos legibles en las columnas
                    dgvMarcas.Columns("id_marca").HeaderText = "ID"
                    dgvMarcas.Columns("id_marca").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvMarcas.Columns("descripcion").HeaderText = "Marca"
                    dgvMarcas.Columns("modelos").HeaderText = "Modelos"
                    dgvMarcas.Columns("modelos").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las marcas: " & ex.Message)
        End Try
    End Sub

    Sub LimpiarMarca()
        'limpio la marca y, como ya no hay marca elegida, tambien los modelos
        txtIdMarca.Clear()
        txtMarca.Clear()
        dgvMarcas.ClearSelection()
        lblMarcaSeleccionada.Text = "Seleccione una marca de la lista"
        dgvModelos.DataSource = Nothing
        LimpiarModelo()
        txtMarca.Focus()
    End Sub

    Private Sub FrmMarcasModelos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'cargo la grilla de marcas al abrir el formulario
        CargarMarcas()
        LimpiarMarca()
    End Sub

    Private Sub dgvMarcas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMarcas.CellClick
        'traigo la marca seleccionada y cargo sus modelos
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvMarcas.Rows(e.RowIndex)
        txtIdMarca.Text = fila.Cells("id_marca").Value.ToString()
        txtMarca.Text = fila.Cells("descripcion").Value.ToString()

        lblMarcaSeleccionada.Text = "de " & txtMarca.Text
        CargarModelos(CInt(txtIdMarca.Text))
        LimpiarModelo()
    End Sub

    Private Sub btnGuardarMarca_Click(sender As Object, e As EventArgs) Handles btnGuardarMarca.Click
        'guardo una marca nueva
        If txtIdMarca.Text.Trim <> "" Then
            MessageBox.Show("Hay una marca seleccionada. Use MODIFICAR o presione LIMPIAR para cargar una nueva.")
            Exit Sub
        End If

        If txtMarca.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre de la marca")
            txtMarca.Focus()
            Exit Sub
        End If

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "INSERT INTO marca (descripcion) VALUES (@descripcion);"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@descripcion", txtMarca.Text.Trim)
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros agregados: " & Resultado)
                End Using
            End Using

            CargarMarcas()
            LimpiarMarca()

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: la marca ya existe (restriccion unica)
            MessageBox.Show("Ya existe una marca con ese nombre.")
            txtMarca.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al guardar " & ex.Message)
        End Try
    End Sub

    Private Sub btnModificarMarca_Click(sender As Object, e As EventArgs) Handles btnModificarMarca.Click
        'valido que haya una marca seleccionada
        If txtIdMarca.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar una marca para modificar")
            Exit Sub
        End If

        If txtMarca.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre de la marca")
            txtMarca.Focus()
            Exit Sub
        End If

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "UPDATE marca SET descripcion=@descripcion WHERE id_marca=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@descripcion", txtMarca.Text.Trim)
                    cmd.Parameters.AddWithValue("@id", CInt(txtIdMarca.Text))
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros actualizados: " & Resultado)
                End Using
            End Using

            CargarMarcas()
            LimpiarMarca()

        Catch ex As MySqlException When ex.Number = 1062
            MessageBox.Show("Ya existe otra marca con ese nombre.")
            txtMarca.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al modificar " & ex.Message)
        End Try
    End Sub

    Private Sub btnEliminarMarca_Click(sender As Object, e As EventArgs) Handles btnEliminarMarca.Click
        'valido que haya una marca seleccionada
        If txtIdMarca.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar una marca para eliminar")
            Exit Sub
        End If

        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Eliminar la marca " & txtMarca.Text & "?",
            "Eliminar marca",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "DELETE FROM marca WHERE id_marca=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", CInt(txtIdMarca.Text))
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros eliminados: " & Resultado)
                End Using
            End Using

            CargarMarcas()
            LimpiarMarca()

        Catch ex As MySqlException When ex.Number = 1451
            'error 1451: la base no deja borrar porque hay registros que dependen de esta marca
            MessageBox.Show("No se puede eliminar: la marca tiene modelos cargados. Elimine primero sus modelos.")
        Catch ex As Exception
            MessageBox.Show("Error al eliminar " & ex.Message)
        End Try
    End Sub

    Private Sub btnLimpiarMarca_Click(sender As Object, e As EventArgs) Handles btnLimpiarMarca.Click
        LimpiarMarca()
    End Sub

    ' =========================================================
    ' MODELOS (siempre de la marca seleccionada)
    ' =========================================================

    Sub CargarModelos(idMarca As Integer)
        'cargo los modelos de la marca seleccionada
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

                    dgvModelos.Columns("id_modelo").HeaderText = "ID"
                    dgvModelos.Columns("id_modelo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvModelos.Columns("descripcion").HeaderText = "Modelo"
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los modelos: " & ex.Message)
        End Try
    End Sub

    Sub LimpiarModelo()
        'limpio solo los campos del modelo, la marca queda elegida
        txtIdModelo.Clear()
        txtModelo.Clear()
        dgvModelos.ClearSelection()
    End Sub

    Function HayMarcaSeleccionada() As Boolean
        'los modelos siempre pertenecen a una marca
        If txtIdMarca.Text.Trim = "" Then
            MessageBox.Show("Primero seleccione una marca de la lista")
            Return False
        End If
        Return True
    End Function

    Private Sub dgvModelos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvModelos.CellClick
        'traigo el modelo seleccionado a los textbox
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvModelos.Rows(e.RowIndex)
        txtIdModelo.Text = fila.Cells("id_modelo").Value.ToString()
        txtModelo.Text = fila.Cells("descripcion").Value.ToString()
    End Sub

    Private Sub btnGuardarModelo_Click(sender As Object, e As EventArgs) Handles btnGuardarModelo.Click
        'guardo un modelo nuevo para la marca seleccionada
        If Not HayMarcaSeleccionada() Then Exit Sub

        If txtIdModelo.Text.Trim <> "" Then
            MessageBox.Show("Hay un modelo seleccionado. Use MODIFICAR o presione LIMPIAR para cargar uno nuevo.")
            Exit Sub
        End If

        If txtModelo.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre del modelo")
            txtModelo.Focus()
            Exit Sub
        End If

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "INSERT INTO modelo (id_marca, descripcion) VALUES (@id_marca, @descripcion);"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_marca", CInt(txtIdMarca.Text))
                    cmd.Parameters.AddWithValue("@descripcion", txtModelo.Text.Trim)
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros agregados: " & Resultado)
                End Using
            End Using

            CargarModelos(CInt(txtIdMarca.Text))
            'recargo las marcas para actualizar la cantidad de modelos
            CargarMarcas()
            LimpiarModelo()

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: esta marca ya tiene un modelo con ese nombre
            MessageBox.Show("La marca " & txtMarca.Text & " ya tiene un modelo con ese nombre.")
            txtModelo.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al guardar " & ex.Message)
        End Try
    End Sub

    Private Sub btnModificarModelo_Click(sender As Object, e As EventArgs) Handles btnModificarModelo.Click
        If Not HayMarcaSeleccionada() Then Exit Sub

        If txtIdModelo.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un modelo para modificar")
            Exit Sub
        End If

        If txtModelo.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre del modelo")
            txtModelo.Focus()
            Exit Sub
        End If

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "UPDATE modelo SET descripcion=@descripcion WHERE id_modelo=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@descripcion", txtModelo.Text.Trim)
                    cmd.Parameters.AddWithValue("@id", CInt(txtIdModelo.Text))
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros actualizados: " & Resultado)
                End Using
            End Using

            CargarModelos(CInt(txtIdMarca.Text))
            LimpiarModelo()

        Catch ex As MySqlException When ex.Number = 1062
            MessageBox.Show("La marca " & txtMarca.Text & " ya tiene otro modelo con ese nombre.")
            txtModelo.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al modificar " & ex.Message)
        End Try
    End Sub

    Private Sub btnEliminarModelo_Click(sender As Object, e As EventArgs) Handles btnEliminarModelo.Click
        If Not HayMarcaSeleccionada() Then Exit Sub

        If txtIdModelo.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un modelo para eliminar")
            Exit Sub
        End If

        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Eliminar el modelo " & txtModelo.Text & "?",
            "Eliminar modelo",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If respuesta = DialogResult.No Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "DELETE FROM modelo WHERE id_modelo=@id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", CInt(txtIdModelo.Text))
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros eliminados: " & Resultado)
                End Using
            End Using

            CargarModelos(CInt(txtIdMarca.Text))
            CargarMarcas()
            LimpiarModelo()

        Catch ex As MySqlException When ex.Number = 1451
            'error 1451: hay vehiculos cargados con este modelo
            MessageBox.Show("No se puede eliminar: hay vehículos cargados con ese modelo.")
        Catch ex As Exception
            MessageBox.Show("Error al eliminar " & ex.Message)
        End Try
    End Sub

    Private Sub btnLimpiarModelo_Click(sender As Object, e As EventArgs) Handles btnLimpiarModelo.Click
        LimpiarModelo()
    End Sub

End Class

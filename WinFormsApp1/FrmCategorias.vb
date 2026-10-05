Imports MySqlConnector

Public Class FrmCategorias

    'indica si la categoria seleccionada esta activa, decide si el boton da de baja o reactiva
    Private categoriaActiva As Boolean = True

    Sub CargarCategorias(Optional filtro As String = "")
        'creo subrutina para cargar grilla
        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql, traigo todas las categorias: activas y dadas de baja
                'cada una con la cantidad de servicios activos que tiene
                Dim consulta As String =
                    "SELECT c.id_categoria_servicio, c.descripcion, " &
                    "(SELECT COUNT(*) FROM servicio AS s " &
                    "WHERE s.id_categoria_servicio = c.id_categoria_servicio AND s.activo = 1) AS servicios_activos, " &
                    "CASE WHEN c.activo = 1 THEN 'Sí' ELSE 'No' END AS esta_activa " &
                    "FROM categoria_servicio AS c "

                'aplico filtro por descripcion
                If filtro <> "" Then
                    consulta = consulta & "WHERE c.descripcion LIKE @filtro "
                End If

                'primero las activas, despues las dadas de baja
                consulta = consulta & "ORDER BY c.activo DESC, c.descripcion;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvCategorias.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvCategorias.Columns("id_categoria_servicio").HeaderText = "ID"
                    'las columnas de datos cortos se ajustan al contenido, la descripcion ocupa el resto
                    dgvCategorias.Columns("id_categoria_servicio").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvCategorias.Columns("descripcion").HeaderText = "Descripción"
                    dgvCategorias.Columns("servicios_activos").HeaderText = "Servicios activos"
                    dgvCategorias.Columns("servicios_activos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvCategorias.Columns("servicios_activos").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvCategorias.Columns("esta_activa").HeaderText = "Activo"
                    dgvCategorias.Columns("esta_activa").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                End Using
            End Using
        Catch ex As Exception
            'muestro mensaje de error
            MessageBox.Show("Error al cargar las categorías: " & ex.Message)
        End Try
    End Sub

    Sub LimpiarFormu()
        'limpio los campos del formulario
        txtID.Clear()
        txtDescripcion.Clear()
        'sin categoria seleccionada el boton vuelve a ser el de la baja
        categoriaActiva = True
        lblEstado.Text = ""
        btnEliminar.Text = "Dar de baja"
        dgvCategorias.ClearSelection()
        txtDescripcion.Focus()
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If txtDescripcion.Text.Trim = "" Then
            MessageBox.Show("Falta la descripción")
            txtDescripcion.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmCategorias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'solo el administrador puede gestionar categorias (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede gestionar categorías.")
            pnlDatos.Enabled = False
            txtFiltro.Enabled = False
            dgvCategorias.Enabled = False
            Exit Sub
        End If

        'cargo la grilla de categorias al abrir el formulario
        CargarCategorias()
        LimpiarFormu()
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito
        CargarCategorias(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo una categoria nueva

        'si hay un ID cargado, el usuario quiere modificar, no guardar
        If txtID.Text.Trim <> "" Then
            MessageBox.Show("Hay una categoría seleccionada. Use MODIFICAR o presione LIMPIAR para cargar una nueva.")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql
                Dim consulta As String = "INSERT INTO categoria_servicio (descripcion) VALUES (@descripcion);"

                Using cmd As New MySqlCommand(consulta, cn)
                    'cargo valores en los parametros
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim)

                    'ejecuto la consulta y obtengo los registros afectados
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros agregados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarCategorias(txtFiltro.Text.Trim)

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: la descripcion ya existe (restriccion unica un_categoria_servicio_descripcion)
            MessageBox.Show("Ya existe una categoría con esa descripción. Puede corresponder a una categoría dada de baja.")
            txtDescripcion.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al guardar " & ex.Message)
        End Try
    End Sub

    Private Sub dgvCategorias_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCategorias.CellClick
        'traigo los datos de la fila seleccionada al formulario
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvCategorias.Rows(e.RowIndex)
        txtID.Text = fila.Cells("id_categoria_servicio").Value.ToString()
        txtDescripcion.Text = fila.Cells("descripcion").Value.ToString()

        'segun el estado de la categoria, el mismo boton da de baja o reactiva
        categoriaActiva = (fila.Cells("esta_activa").Value.ToString() = "Sí")
        If categoriaActiva Then
            lblEstado.Text = "Activa"
            btnEliminar.Text = "Dar de baja"
        Else
            lblEstado.Text = "Dada de baja"
            btnEliminar.Text = "Reactivar"
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        'valido que haya una categoria seleccionada
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar una categoría para modificar")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'uso update para modificar y where para indicar que registro
                Dim consulta As String =
                    "UPDATE categoria_servicio SET descripcion=@descripcion WHERE id_categoria_servicio=@id;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'uso parametros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))

                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros actualizados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarCategorias(txtFiltro.Text.Trim)

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: la descripcion ya existe (restriccion unica un_categoria_servicio_descripcion)
            MessageBox.Show("Ya existe otra categoría con esa descripción. Puede corresponder a una categoría dada de baja.")
            txtDescripcion.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al modificar " & ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'doy de baja la categoria seleccionada, o la reactivo si ya estaba dada de baja

        'valido que haya una categoria seleccionada
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar una categoría para dar de baja o reactivar")
            Exit Sub
        End If

        Dim idCategoria As Integer = CInt(txtID.Text)

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If categoriaActiva Then
            respuesta = MessageBox.Show(
                "¿Dar de baja la categoría " & txtDescripcion.Text & "?",
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar la categoría " & txtDescripcion.Text & "?",
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'estado de la categoria leido en la base dentro de la transaccion
        Dim activaEnBase As Boolean = False
        'queda en True cuando lo que hay en pantalla ya no coincide con la base
        Dim desactualizado As Boolean = False

        'baja logica: no borro el registro porque los servicios la referencian
        'solo la marco como inactiva, y con el mismo boton se la puede volver a activar
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer la categoria y la bloqueo hasta terminar
                    Dim existe As Boolean = False
                    Dim consulta As String =
                        "SELECT activo FROM categoria_servicio WHERE id_categoria_servicio = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idCategoria)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then
                            existe = True
                            activaEnBase = Convert.ToBoolean(resultado)
                        End If
                    End Using

                    If Not existe Then
                        aviso = "La categoría seleccionada ya no existe."
                        desactualizado = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activaEnBase <> categoriaActiva Then
                        aviso = "El estado de la categoría fue cambiado desde otro puesto. Se actualizó la lista: revísela y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso activaEnBase Then
                        'no se da de baja una categoria que tiene servicios activos
                        consulta = "SELECT COUNT(*) FROM servicio " &
                                   "WHERE id_categoria_servicio = @id AND activo = 1;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idCategoria)
                            Dim serviciosActivos As Integer = CInt(cmd.ExecuteScalar())
                            If serviciosActivos > 0 Then
                                aviso = "No se puede dar de baja: la categoría tiene " & serviciosActivos &
                                        " servicio(s) activo(s). Páselos a otra categoría o delos de baja primero."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE categoria_servicio SET activo = @activo WHERE id_categoria_servicio = @id;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@activo", Not activaEnBase)
                            cmd.Parameters.AddWithValue("@id", idCategoria)
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
            MessageBox.Show("Error al cambiar el estado de la categoría: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
            'si la categoria cambio desde otro puesto, limpio el formulario
            If desactualizado Then LimpiarFormu()
            'recargo la grilla: muestra el estado real y la cantidad actual de servicios activos
            CargarCategorias(txtFiltro.Text.Trim)
            Exit Sub
        End If

        If activaEnBase Then
            MessageBox.Show("Categoría dada de baja.")
        Else
            MessageBox.Show("Categoría reactivada.")
        End If

        LimpiarFormu()
        CargarCategorias(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'limpio el formulario para cargar una categoria nueva
        LimpiarFormu()
    End Sub

End Class

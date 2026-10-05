Imports MySqlConnector

Public Class FrmServicios

    'indica si el servicio seleccionado esta activo, decide si el boton da de baja o reactiva
    Private servicioActivo As Boolean = True

    Sub CargarServicios(Optional filtro As String = "")
        'creo subrutina para cargar grilla
        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql, traigo todos los servicios: activos y dados de baja
                Dim consulta As String =
                    "SELECT s.id_servicio, s.codigo, s.descripcion, c.descripcion AS categoria, " &
                    "s.precio, s.tiempo_estimado_horas, " &
                    "CASE WHEN s.activo = 1 THEN 'Sí' ELSE 'No' END AS esta_activo, " &
                    "s.id_categoria_servicio " &
                    "FROM servicio AS s " &
                    "JOIN categoria_servicio AS c ON c.id_categoria_servicio = s.id_categoria_servicio "

                'aplico filtro por codigo, descripcion o categoria
                If filtro <> "" Then
                    consulta = consulta &
                        "WHERE s.codigo LIKE @filtro OR s.descripcion LIKE @filtro OR c.descripcion LIKE @filtro "
                End If

                'primero los activos, despues los dados de baja
                consulta = consulta & "ORDER BY s.activo DESC, s.descripcion;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvServicios.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvServicios.Columns("id_servicio").HeaderText = "ID"
                    'las columnas de datos cortos se ajustan al contenido, la descripcion ocupa el resto
                    dgvServicios.Columns("id_servicio").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvServicios.Columns("codigo").HeaderText = "Código"
                    dgvServicios.Columns("codigo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvServicios.Columns("descripcion").HeaderText = "Descripción"
                    dgvServicios.Columns("categoria").HeaderText = "Categoría"
                    dgvServicios.Columns("categoria").FillWeight = 50
                    dgvServicios.Columns("precio").HeaderText = "Precio"
                    dgvServicios.Columns("precio").DefaultCellStyle.Format = "C2"
                    dgvServicios.Columns("precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvServicios.Columns("precio").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvServicios.Columns("tiempo_estimado_horas").HeaderText = "Horas"
                    dgvServicios.Columns("tiempo_estimado_horas").DefaultCellStyle.Format = "N2"
                    dgvServicios.Columns("tiempo_estimado_horas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvServicios.Columns("tiempo_estimado_horas").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvServicios.Columns("esta_activo").HeaderText = "Activo"
                    dgvServicios.Columns("esta_activo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    'la clave de la categoria se usa al seleccionar la fila, no se muestra
                    dgvServicios.Columns("id_categoria_servicio").Visible = False
                End Using
            End Using
        Catch ex As Exception
            'muestro mensaje de error
            MessageBox.Show("Error al cargar los servicios: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboCategorias(Optional idCategoriaIncluida As Integer = 0)
        'cargo las categorias activas en el combo, ordenadas por nombre
        'si el servicio seleccionado tiene una categoria dada de baja, la incluyo para poder mostrarla
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_categoria_servicio, " &
                    "CASE WHEN activo = 1 THEN descripcion ELSE CONCAT(descripcion, ' (inactiva)') END AS nombre " &
                    "FROM categoria_servicio " &
                    "WHERE activo = 1 OR id_categoria_servicio = @id_incluida " &
                    "ORDER BY descripcion;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_incluida", idCategoriaIncluida)

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'agrego una primera opcion con clave 0 que funciona como texto de ayuda
                    Dim filaAyuda As DataRow = tabla.NewRow()
                    filaAyuda("id_categoria_servicio") = 0
                    filaAyuda("nombre") = "(seleccione una categoría)"
                    tabla.Rows.InsertAt(filaAyuda, 0)

                    'el usuario ve el nombre...
                    cboCategoria.DisplayMember = "nombre"
                    '...pero el programa guarda la clave numerica
                    cboCategoria.ValueMember = "id_categoria_servicio"
                    cboCategoria.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las categorías: " & ex.Message)
        End Try
    End Sub

    Sub LimpiarFormu()
        'limpio los campos del formulario
        txtID.Clear()
        txtCodigo.Clear()
        txtDescripcion.Clear()
        nudPrecio.Value = 0
        'el tiempo estimado es opcional, arranca sin marcar
        chkTiempo.Checked = False
        nudTiempo.Value = 0
        'vuelvo a dejar en el combo solo las categorias activas, en la opcion de ayuda
        CargarComboCategorias()
        'sin servicio seleccionado el boton vuelve a ser el de la baja
        servicioActivo = True
        lblEstado.Text = ""
        btnEliminar.Text = "Dar de baja"
        dgvServicios.ClearSelection()
        txtCodigo.Focus()
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If txtCodigo.Text.Trim = "" Then
            MessageBox.Show("Falta el código")
            txtCodigo.Focus()
            Return False
        End If

        If txtDescripcion.Text.Trim = "" Then
            MessageBox.Show("Falta la descripción")
            txtDescripcion.Focus()
            Return False
        End If

        'la clave 0 corresponde a la opcion de ayuda, no a una categoria real
        If cboCategoria.SelectedValue Is Nothing OrElse Convert.ToInt32(cboCategoria.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione una categoría")
            cboCategoria.Focus()
            Return False
        End If

        'el precio es obligatorio pero puede ser cero, nunca negativo
        If nudPrecio.Value < 0 Then
            MessageBox.Show("El precio no puede ser negativo")
            nudPrecio.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmServicios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'solo el administrador puede gestionar servicios (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede gestionar servicios.")
            pnlDatos.Enabled = False
            txtFiltro.Enabled = False
            dgvServicios.Enabled = False
            Exit Sub
        End If

        'cargo la grilla de servicios al abrir el formulario, el combo lo carga la limpieza
        CargarServicios()
        LimpiarFormu()
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito
        CargarServicios(txtFiltro.Text.Trim)
    End Sub

    Private Sub chkTiempo_CheckedChanged(sender As Object, e As EventArgs) Handles chkTiempo.CheckedChanged
        'las horas solo se cargan cuando el servicio tiene tiempo estimado
        nudTiempo.Enabled = chkTiempo.Checked
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un servicio nuevo

        'si hay un ID cargado, el usuario quiere modificar, no guardar
        If txtID.Text.Trim <> "" Then
            MessageBox.Show("Hay un servicio seleccionado. Use MODIFICAR o presione LIMPIAR para cargar uno nuevo.")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        'el precio y las horas van como decimales con dos lugares
        Dim precio As Decimal = Math.Round(nudPrecio.Value, 2, MidpointRounding.AwayFromZero)

        'el tiempo estimado sin marcar se guarda como NULL
        Dim tiempo As Object = DBNull.Value
        If chkTiempo.Checked Then tiempo = Math.Round(nudTiempo.Value, 2, MidpointRounding.AwayFromZero)

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql
                Dim consulta As String =
                    "INSERT INTO servicio (codigo, descripcion, id_categoria_servicio, precio, tiempo_estimado_horas) " &
                    "VALUES (@codigo, @descripcion, @id_categoria_servicio, @precio, @tiempo_estimado_horas);"

                Using cmd As New MySqlCommand(consulta, cn)
                    'cargo valores en los parametros, el codigo se guarda sin espacios y en mayusculas
                    cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim.ToUpper)
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim)
                    cmd.Parameters.AddWithValue("@id_categoria_servicio", Convert.ToInt32(cboCategoria.SelectedValue))
                    cmd.Parameters.AddWithValue("@precio", precio)
                    cmd.Parameters.AddWithValue("@tiempo_estimado_horas", tiempo)

                    'ejecuto la consulta y obtengo los registros afectados
                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros agregados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarServicios(txtFiltro.Text.Trim)

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: el codigo ya existe (restriccion unica un_servicio_codigo)
            MessageBox.Show("Ya existe un servicio con ese código. Puede corresponder a un servicio dado de baja.")
            txtCodigo.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al guardar " & ex.Message)
        End Try
    End Sub

    Private Sub dgvServicios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvServicios.CellClick
        'traigo los datos de la fila seleccionada al formulario
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvServicios.Rows(e.RowIndex)
        txtID.Text = fila.Cells("id_servicio").Value.ToString()
        txtCodigo.Text = fila.Cells("codigo").Value.ToString()
        txtDescripcion.Text = fila.Cells("descripcion").Value.ToString()

        'recargo el combo incluyendo la categoria del servicio, por si esta dada de baja
        Dim idCategoria As Integer = CInt(fila.Cells("id_categoria_servicio").Value)
        CargarComboCategorias(idCategoria)
        If cboCategoria.DataSource IsNot Nothing Then cboCategoria.SelectedValue = idCategoria

        Dim precio As Decimal = CDec(fila.Cells("precio").Value)
        If precio <= nudPrecio.Maximum Then nudPrecio.Value = precio

        'el tiempo estimado es NULL cuando el servicio no lo tiene cargado
        If IsDBNull(fila.Cells("tiempo_estimado_horas").Value) Then
            chkTiempo.Checked = False
            nudTiempo.Value = 0
        Else
            chkTiempo.Checked = True
            Dim tiempo As Decimal = CDec(fila.Cells("tiempo_estimado_horas").Value)
            If tiempo <= nudTiempo.Maximum Then nudTiempo.Value = tiempo
        End If

        'segun el estado del servicio, el mismo boton da de baja o reactiva
        servicioActivo = (fila.Cells("esta_activo").Value.ToString() = "Sí")
        If servicioActivo Then
            lblEstado.Text = "Activo"
            btnEliminar.Text = "Dar de baja"
        Else
            lblEstado.Text = "Dado de baja"
            btnEliminar.Text = "Reactivar"
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        'valido que haya un servicio seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un servicio para modificar")
            Exit Sub
        End If

        If Not ValidarCampos() Then Exit Sub

        'el precio y las horas van como decimales con dos lugares
        Dim precio As Decimal = Math.Round(nudPrecio.Value, 2, MidpointRounding.AwayFromZero)

        'el tiempo estimado sin marcar se guarda como NULL
        Dim tiempo As Object = DBNull.Value
        If chkTiempo.Checked Then tiempo = Math.Round(nudTiempo.Value, 2, MidpointRounding.AwayFromZero)

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'uso update para modificar y where para indicar que registro
                'el precio nuevo no cambia los presupuestos ya cargados: cada linea guarda su copia
                Dim consulta As String =
                    "UPDATE servicio SET codigo=@codigo, descripcion=@descripcion, " &
                    "id_categoria_servicio=@id_categoria_servicio, precio=@precio, " &
                    "tiempo_estimado_horas=@tiempo_estimado_horas " &
                    "WHERE id_servicio=@id;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'uso parametros para evitar SQL Injection
                    cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim.ToUpper)
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim)
                    cmd.Parameters.AddWithValue("@id_categoria_servicio", Convert.ToInt32(cboCategoria.SelectedValue))
                    cmd.Parameters.AddWithValue("@precio", precio)
                    cmd.Parameters.AddWithValue("@tiempo_estimado_horas", tiempo)
                    cmd.Parameters.AddWithValue("@id", CInt(txtID.Text))

                    Dim Resultado As Integer = cmd.ExecuteNonQuery()
                    MessageBox.Show("Registros actualizados: " & Resultado)
                End Using
            End Using

            LimpiarFormu()
            CargarServicios(txtFiltro.Text.Trim)

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: el codigo ya existe (restriccion unica un_servicio_codigo)
            MessageBox.Show("Ya existe otro servicio con ese código. Puede corresponder a un servicio dado de baja.")
            txtCodigo.Focus()
        Catch ex As Exception
            MessageBox.Show("Error al modificar " & ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'doy de baja al servicio seleccionado, o lo reactivo si ya estaba dado de baja

        'valido que haya un servicio seleccionado
        If txtID.Text.Trim = "" Then
            MessageBox.Show("Debe seleccionar un servicio para dar de baja o reactivar")
            Exit Sub
        End If

        Dim idServicio As Integer = CInt(txtID.Text)

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If servicioActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja el servicio " & txtDescripcion.Text & "?",
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar el servicio " & txtDescripcion.Text & "?",
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'estado del servicio leido en la base dentro de la transaccion
        Dim activoEnBase As Boolean = False
        'queda en True cuando lo que hay en pantalla ya no coincide con la base
        Dim desactualizado As Boolean = False

        'baja logica: no borro el registro, solo lo marco como inactivo
        'no hace falta comprobar si esta en uso: cada linea de presupuesto guarda su copia
        'de la descripcion y del precio, y la gestion de la orden solo ofrece servicios activos
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el servicio con su categoria y lo bloqueo hasta terminar
                    Dim existe As Boolean = False
                    Dim categoriaActiva As Boolean = False
                    Dim categoria As String = ""
                    Dim consulta As String =
                        "SELECT s.activo, c.activo AS categoria_activa, c.descripcion AS categoria " &
                        "FROM servicio AS s " &
                        "JOIN categoria_servicio AS c ON c.id_categoria_servicio = s.id_categoria_servicio " &
                        "WHERE s.id_servicio = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idServicio)
                        Using lector As MySqlDataReader = cmd.ExecuteReader
                            If lector.Read() Then
                                existe = True
                                activoEnBase = Convert.ToBoolean(lector("activo"))
                                categoriaActiva = Convert.ToBoolean(lector("categoria_activa"))
                                categoria = lector("categoria").ToString()
                            End If
                        End Using
                    End Using

                    If Not existe Then
                        aviso = "El servicio seleccionado ya no existe."
                        desactualizado = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activoEnBase <> servicioActivo Then
                        aviso = "El estado del servicio fue cambiado desde otro puesto. Se actualizó la lista: revíselo y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso Not activoEnBase AndAlso Not categoriaActiva Then
                        'no se reactiva un servicio cuya categoria esta dada de baja
                        aviso = "No se puede reactivar: la categoría """ & categoria &
                                """ está dada de baja. Cambie la categoría del servicio con MODIFICAR y vuelva a intentar."
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE servicio SET activo = @activo WHERE id_servicio = @id;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@activo", Not activoEnBase)
                            cmd.Parameters.AddWithValue("@id", idServicio)
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
            MessageBox.Show("Error al cambiar el estado del servicio: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
            'si el servicio cambio desde otro puesto, limpio el formulario y muestro el estado real
            If desactualizado Then
                LimpiarFormu()
                CargarServicios(txtFiltro.Text.Trim)
            End If
            Exit Sub
        End If

        If activoEnBase Then
            MessageBox.Show("Servicio dado de baja.")
        Else
            MessageBox.Show("Servicio reactivado.")
        End If

        LimpiarFormu()
        CargarServicios(txtFiltro.Text.Trim)
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'limpio el formulario para cargar un servicio nuevo
        LimpiarFormu()
    End Sub

End Class

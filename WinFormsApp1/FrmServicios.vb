Imports MySqlConnector

Public Class FrmServicios

    'servicio que se ve en la tarjeta del registro, queda en 0 mientras no haya (el ID no se muestra)
    Private idServicio As Integer = 0
    'indica que la tarjeta esta cargando un registro nuevo
    Private creando As Boolean = False
    'indica si el registro cargado esta activo, decide si el boton da de baja o reactiva
    Private registroActivo As Boolean = True

    'queda en True cuando el usuario cambio algun campo y todavia no guardo
    Private hayCambios As Boolean = False
    'queda en True mientras el programa carga la grilla o los campos, para no tomarlo como un cambio del usuario
    Private cargando As Boolean = False

    'iconos de los botones que cambian segun el registro
    Private iconoBaja As Image
    Private iconoReactivar As Image

    Sub CargarServicios()
        'cargo la grilla con el filtro escrito y dejo marcado el servicio que se estaba viendo
        Dim filtro As String = txtFiltro.Text.Trim

        cargando = True

        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql, cada servicio con su categoria
                Dim consulta As String =
                    "SELECT s.id_servicio, s.codigo, s.descripcion, c.descripcion AS categoria, " &
                    "s.precio, s.tiempo_estimado_horas, s.activo, " &
                    "s.id_categoria_servicio, c.activo AS categoria_activa " &
                    "FROM servicio AS s " &
                    "JOIN categoria_servicio AS c ON c.id_categoria_servicio = s.id_categoria_servicio " &
                    "WHERE 1 = 1 "

                'solo traigo servicios activos, salvo que se pida ver tambien los dados de baja
                If Not chkBajas.Checked Then
                    consulta = consulta & "AND s.activo = 1 "
                End If

                'aplico filtro por codigo, descripcion o categoria
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (s.codigo LIKE @filtro OR s.descripcion LIKE @filtro OR c.descripcion LIKE @filtro) "
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

                    'las claves y los estados se usan al elegir la fila, no se muestran
                    dgvServicios.Columns("id_servicio").Visible = False
                    dgvServicios.Columns("activo").Visible = False
                    dgvServicios.Columns("id_categoria_servicio").Visible = False
                    dgvServicios.Columns("categoria_activa").Visible = False

                    'pongo titulos legibles en las columnas y reparto el ancho, con un minimo para cada una
                    dgvServicios.Columns("codigo").HeaderText = "Código"
                    dgvServicios.Columns("codigo").FillWeight = 16
                    dgvServicios.Columns("codigo").MinimumWidth = 75
                    dgvServicios.Columns("descripcion").HeaderText = "Descripción"
                    dgvServicios.Columns("descripcion").FillWeight = 36
                    dgvServicios.Columns("descripcion").MinimumWidth = 130
                    dgvServicios.Columns("categoria").HeaderText = "Categoría"
                    dgvServicios.Columns("categoria").FillWeight = 22
                    dgvServicios.Columns("categoria").MinimumWidth = 90
                    'el precio y las horas van alineados a la derecha, con su titulo
                    dgvServicios.Columns("precio").HeaderText = "Precio"
                    dgvServicios.Columns("precio").DefaultCellStyle.Format = "C2"
                    dgvServicios.Columns("precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvServicios.Columns("precio").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvServicios.Columns("precio").FillWeight = 16
                    dgvServicios.Columns("precio").MinimumWidth = 85
                    dgvServicios.Columns("tiempo_estimado_horas").HeaderText = "Horas"
                    dgvServicios.Columns("tiempo_estimado_horas").DefaultCellStyle.Format = "N2"
                    dgvServicios.Columns("tiempo_estimado_horas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvServicios.Columns("tiempo_estimado_horas").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvServicios.Columns("tiempo_estimado_horas").FillWeight = 10
                    dgvServicios.Columns("tiempo_estimado_horas").MinimumWidth = 55

                    'el orden por columna lo hace el formulario al hacer click en el titulo (ver ColumnHeaderMouseClick)
                    dgvServicios.Columns("codigo").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvServicios.Columns("descripcion").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvServicios.Columns("categoria").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvServicios.Columns("precio").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvServicios.Columns("tiempo_estimado_horas").SortMode = DataGridViewColumnSortMode.Programmatic

                    'en el subtitulo cuento lo que quedo listado
                    Dim activos As Integer = 0
                    Dim bajas As Integer = 0
                    For Each registro As DataRow In tabla.Rows
                        If Convert.ToBoolean(registro("activo")) Then
                            activos = activos + 1
                        Else
                            bajas = bajas + 1
                        End If
                    Next

                    If chkBajas.Checked Then
                        lblSubtitulo.Text = activos & " activos · " & bajas & " dados de baja"
                    ElseIf activos = 1 Then
                        lblSubtitulo.Text = "1 servicio activo"
                    Else
                        lblSubtitulo.Text = activos & " servicios activos"
                    End If
                End Using
            End Using

            'en la lista nueva dejo marcado el registro que se estaba viendo
            Dim filaActual As DataGridViewRow = MarcarFila(idServicio)
            If filaActual Is Nothing AndAlso idServicio <> 0 AndAlso Not hayCambios Then
                'ya no esta en la lista (por el filtro o por una baja): la tarjeta vuelve a la ayuda
                MostrarAyuda()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar los servicios: " & ex.Message)
        Finally
            'pase lo que pase, la grilla vuelve a atender al usuario
            cargando = False
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

    Function MarcarFila(id As Integer) As DataGridViewRow
        'dejo marcada en la lista la fila con esa clave: celda actual y seleccion juntas
        'con clave 0, o si el registro no esta listado, no queda nada marcado; devuelve la fila o Nothing
        Dim encontrada As DataGridViewRow = Nothing
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If id <> 0 AndAlso dgvServicios.Columns.Contains("id_servicio") Then
                For Each fila As DataGridViewRow In dgvServicios.Rows
                    If CInt(fila.Cells("id_servicio").Value) = id Then encontrada = fila
                Next
            End If

            dgvServicios.ClearSelection()
            If encontrada Is Nothing Then
                dgvServicios.CurrentCell = Nothing
            Else
                dgvServicios.CurrentCell = encontrada.Cells("codigo")
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    Sub RefrescarRegistro()
        'vuelvo a mostrar en la tarjeta los datos guardados del registro que se esta viendo
        Dim fila As DataGridViewRow = MarcarFila(idServicio)
        If fila IsNot Nothing Then MostrarRegistro(fila)
    End Sub

    Sub MostrarAyuda()
        'sin registro elegido ni alta en curso, la tarjeta muestra solo la ayuda
        idServicio = 0
        creando = False
        hayCambios = False
        pnlRegistro.Visible = False
        lblAyuda.Visible = True
    End Sub

    Sub CargarCampos(fila As DataGridViewRow)
        'paso a los campos los datos de la fila; con fila en Nothing quedan vacios para un alta
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If fila Is Nothing Then
                txtCodigo.Clear()
                txtDescripcion.Clear()
                nudPrecio.Value = 0
                'el tiempo estimado es opcional, arranca sin marcar
                chkTiempo.Checked = False
                nudTiempo.Value = 0
                'dejo en el combo solo las categorias activas, en la opcion de ayuda
                CargarComboCategorias()
            Else
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
            End If

            'lo que se ve es lo que esta guardado
            hayCambios = False
        Finally
            cargando = estabaCargando
        End Try
    End Sub

    Sub MostrarRegistro(fila As DataGridViewRow)
        'cargo en la tarjeta el servicio de la fila, para verlo y modificarlo
        idServicio = CInt(fila.Cells("id_servicio").Value)
        creando = False
        registroActivo = Convert.ToBoolean(fila.Cells("activo").Value)

        CargarCampos(fila)
        lblRegistroTitulo.Text = fila.Cells("descripcion").Value.ToString()

        'etiqueta de estado y boton de la izquierda segun el registro este activo o dado de baja
        lblEstadoRegistro.Visible = True
        If registroActivo Then
            lblEstadoRegistro.Text = "Activo"
            lblEstadoRegistro.BackColor = Color.FromArgb(223, 240, 216)
            lblEstadoRegistro.ForeColor = Color.DarkGreen
            btnBaja.Text = " Dar de baja"
            btnBaja.ForeColor = Color.Firebrick
            btnBaja.Image = iconoBaja
        Else
            lblEstadoRegistro.Text = "Dado de baja"
            lblEstadoRegistro.BackColor = Color.Gainsboro
            lblEstadoRegistro.ForeColor = Color.DimGray
            btnBaja.Text = " Reactivar"
            btnBaja.ForeColor = Color.Black
            btnBaja.Image = iconoReactivar
        End If

        btnBaja.Visible = True
        btnCancelar.Visible = False
        btnGuardar.Text = " Guardar cambios"

        'linea de contexto: aviso cuando la categoria del servicio esta dada de baja
        If Convert.ToBoolean(fila.Cells("categoria_activa").Value) Then
            lblContexto.Visible = False
        Else
            lblContexto.Text = "Su categoría está dada de baja: elija otra para poder reactivarlo."
            lblContexto.Visible = True
        End If

        lblAyuda.Visible = False
        pnlRegistro.Visible = True
    End Sub

    Sub NuevoRegistro()
        'dejo la tarjeta lista para cargar un nuevo registro
        idServicio = 0
        creando = True
        registroActivo = True

        CargarCampos(Nothing)
        lblRegistroTitulo.Text = "Nuevo servicio"

        'un registro nuevo no tiene estado ni contexto, y sus botones son Cancelar y Guardar
        lblEstadoRegistro.Visible = False
        lblContexto.Visible = False
        btnBaja.Visible = False
        btnCancelar.Visible = True
        btnGuardar.Text = " Guardar"

        lblAyuda.Visible = False
        pnlRegistro.Visible = True

        'durante un alta no queda ninguna fila marcada en la lista
        MarcarFila(0)

        txtCodigo.Focus()
    End Sub

    Function DescartarCambios() As Boolean
        'si hay cambios sin guardar pregunto antes de perderlos; devuelve True cuando se puede seguir
        If Not hayCambios Then Return True

        Dim respuesta As DialogResult = MessageBox.Show(
            "Hay cambios sin guardar. ¿Descartarlos?",
            "Cambios sin guardar",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        Return respuesta = DialogResult.Yes
    End Function

    Sub ElegirFila(fila As DataGridViewRow)
        'el usuario eligio una fila de la lista: la cargo en la tarjeta del registro
        Dim idElegido As Integer = CInt(fila.Cells("id_servicio").Value)

        'si ya se esta viendo ese registro no hay nada que hacer
        If idElegido = idServicio AndAlso Not creando Then Exit Sub

        If Not DescartarCambios() Then
            'el usuario prefirio seguir con lo que estaba cargando: la lista vuelve a marcar ese registro
            '(en un alta no hay registro, y no queda nada marcado)
            MarcarFila(idServicio)
            Exit Sub
        End If

        MostrarRegistro(fila)
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If txtCodigo.Text.Trim = "" Then
            MessageBox.Show("Falta el código")
            txtCodigo.Focus()
            Return False
        End If

        'la clave 0 corresponde a la opcion de ayuda, no a una categoria real
        If cboCategoria.SelectedValue Is Nothing OrElse Convert.ToInt32(cboCategoria.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione una categoría")
            cboCategoria.Focus()
            Return False
        End If

        If txtDescripcion.Text.Trim = "" Then
            MessageBox.Show("Falta la descripción")
            txtDescripcion.Focus()
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
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnNuevo.Image = LeerIcono("agregar.png")
        btnGuardar.Image = LeerIcono("guardar.png")
        btnCancelar.Image = LeerIcono("cancelar-oscuro.png")
        picBuscar.Image = LeerIcono("buscar-oscuro.png")
        iconoBaja = LeerIcono("baja-rojo.png")
        iconoReactivar = LeerIcono("reactivar-oscuro.png")

        'el encabezado de la lista lleva solo una linea clara debajo, como las filas
        dgvServicios.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'la tarjeta arranca con la ayuda
        MostrarAyuda()

        'solo el administrador puede gestionar servicios (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede gestionar servicios.")
            btnNuevo.Enabled = False
            pnlLista.Enabled = False
            pnlFicha.Enabled = False
            Exit Sub
        End If

        'cargo la grilla de servicios al abrir el formulario, el combo se carga al abrir un registro
        CargarServicios()
    End Sub

    Private Sub FrmServicios_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(idServicio)
    End Sub

    Private Sub FrmServicios_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'al salir de la pantalla pregunto antes de perder cambios sin guardar
        If Not DescartarCambios() Then
            e.Cancel = True
            Exit Sub
        End If

        'ya se acepto perderlos: si el cierre sigue, no se vuelve a preguntar
        hayCambios = False
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito; la tarjeta no se toca, solo se vuelve a marcar su fila
        If cargando Then Exit Sub
        CargarServicios()
    End Sub

    Private Sub chkBajas_CheckedChanged(sender As Object, e As EventArgs) Handles chkBajas.CheckedChanged
        'muestro o dejo de mostrar los registros dados de baja
        CargarServicios()
    End Sub

    Private Sub dgvServicios_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvServicios.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas
        If Not dgvServicios.Columns.Contains("activo") Then Exit Sub

        For Each fila As DataGridViewRow In dgvServicios.Rows
            'los registros dados de baja van en gris, tambien cuando la fila esta seleccionada
            If Not Convert.ToBoolean(fila.Cells("activo").Value) Then
                fila.DefaultCellStyle.ForeColor = Color.Gray
                fila.DefaultCellStyle.SelectionForeColor = Color.Gainsboro
            End If
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcado solo el registro que se esta viendo
        MarcarFila(idServicio)
    End Sub

    Private Sub dgvServicios_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvServicios.ColumnHeaderMouseClick
        'ordeno la lista por la columna del titulo que se toco; otro click en la misma columna invierte el orden
        'ordenar nunca cambia el registro abierto ni pregunta nada
        If e.Button <> MouseButtons.Left Then Exit Sub
        If cargando Then Exit Sub

        Dim columna As DataGridViewColumn = dgvServicios.Columns(e.ColumnIndex)
        Dim sentido As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        If dgvServicios.SortedColumn Is columna AndAlso dgvServicios.SortOrder = SortOrder.Ascending Then
            sentido = System.ComponentModel.ListSortDirection.Descending
        End If

        'mientras se ordena la grilla mueve sola su seleccion: eso no es una eleccion del usuario
        cargando = True
        Try
            dgvServicios.Sort(columna, sentido)

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

    Private Sub dgvServicios_Sorted(sender As Object, e As EventArgs) Handles dgvServicios.Sorted
        'las filas cambiaron de lugar: vuelvo a marcar el registro que se esta viendo
        MarcarFila(idServicio)
    End Sub

    Private Sub dgvServicios_SelectionChanged(sender As Object, e As EventArgs) Handles dgvServicios.SelectionChanged
        'cargo en la tarjeta el registro que el usuario elige con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargando Then Exit Sub
        If Not dgvServicios.Focused Then Exit Sub
        If dgvServicios.SelectedRows.Count = 0 Then Exit Sub

        ElegirFila(dgvServicios.SelectedRows(0))
    End Sub

    Private Sub dgvServicios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvServicios.CellClick
        'un click sobre una fila siempre la carga, aunque la grilla ya la tuviera marcada
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirFila(dgvServicios.Rows(e.RowIndex))
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        'empiezo la carga de un registro nuevo
        If Not DescartarCambios() Then Exit Sub
        NuevoRegistro()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        'dejo de cargar el registro nuevo, la tarjeta vuelve a la ayuda
        If Not DescartarCambios() Then Exit Sub
        MostrarAyuda()
    End Sub

    Private Sub chkTiempo_CheckedChanged(sender As Object, e As EventArgs) Handles chkTiempo.CheckedChanged
        'las horas solo se cargan cuando el servicio tiene tiempo estimado
        nudTiempo.Enabled = chkTiempo.Checked

        'si lo cambio el usuario, hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub Campo_Changed(sender As Object, e As EventArgs) Handles _
        txtCodigo.TextChanged, cboCategoria.SelectedIndexChanged, txtDescripcion.TextChanged,
        nudPrecio.ValueChanged, nudTiempo.ValueChanged

        'el usuario cambio un campo: hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un servicio nuevo, o los cambios del servicio que se esta viendo
        If Not ValidarCampos() Then Exit Sub

        'recuerdo si era un alta, porque al guardar deja de serlo
        Dim eraAlta As Boolean = creando

        'el precio y las horas van como decimales con dos lugares
        Dim precio As Decimal = Math.Round(nudPrecio.Value, 2, MidpointRounding.AwayFromZero)

        'el tiempo estimado sin marcar se guarda como NULL
        Dim tiempo As Object = DBNull.Value
        If chkTiempo.Checked Then tiempo = Math.Round(nudTiempo.Value, 2, MidpointRounding.AwayFromZero)

        If creando Then
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

                        'el servicio nuevo queda como el que se esta viendo, con la clave que le dio la base
                        idServicio = CInt(cmd.LastInsertedId)
                        creando = False
                        hayCambios = False

                        MessageBox.Show("Registros agregados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: el codigo ya existe (restriccion unica un_servicio_codigo)
                MessageBox.Show("Ya existe un servicio con ese código. Puede corresponder a un servicio dado de baja.")
                txtCodigo.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al guardar " & ex.Message)
                Exit Sub
            End Try
        Else
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
                        cmd.Parameters.AddWithValue("@id", idServicio)

                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        hayCambios = False
                        MessageBox.Show("Registros actualizados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: el codigo ya existe (restriccion unica un_servicio_codigo)
                MessageBox.Show("Ya existe otro servicio con ese código. Puede corresponder a un servicio dado de baja.")
                txtCodigo.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al modificar " & ex.Message)
                Exit Sub
            End Try
        End If

        'si se creo un registro, limpio la busqueda para que quede listado aunque no coincida con lo escrito
        If eraAlta AndAlso txtFiltro.Text <> "" Then
            cargando = True
            Try
                txtFiltro.Clear()
            Finally
                cargando = False
            End Try
        End If

        'recargo la lista: el registro guardado queda marcado y la tarjeta muestra lo que quedo en la base
        CargarServicios()
        RefrescarRegistro()
    End Sub

    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        'doy de baja el servicio que se esta viendo, o lo reactivo si ya estaba dado de baja
        Dim idRegistro As Integer = idServicio

        'la baja y la reactivacion no guardan los campos: si hay cambios lo aviso en la misma pregunta
        Dim avisoCambios As String = ""
        If hayCambios Then
            avisoCambios = vbCrLf & vbCrLf & "Los cambios sin guardar de la tarjeta se van a perder."
        End If

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If registroActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja el servicio " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar el servicio " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        'si no confirma no se hace nada, y lo que estaba escribiendo queda como estaba
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
                        cmd.Parameters.AddWithValue("@id", idRegistro)
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
                    If aviso = "" AndAlso activoEnBase <> registroActivo Then
                        aviso = "El estado del servicio fue cambiado desde otro puesto. Se actualizó la lista: revíselo y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso Not activoEnBase AndAlso Not categoriaActiva Then
                        'no se reactiva un servicio cuya categoria esta dada de baja
                        aviso = "No se puede reactivar: la categoría """ & categoria &
                                """ está dada de baja. Cambie la categoría del servicio, guarde los cambios y vuelva a intentar."
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE servicio SET activo = @activo WHERE id_servicio = @id;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@activo", Not activoEnBase)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
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
        ElseIf activoEnBase Then
            MessageBox.Show("Servicio dado de baja.")
        Else
            MessageBox.Show("Servicio reactivado.")
        End If

        'los cambios sin guardar se pierden recien ahora: cuando el estado cambio, o cuando la tarjeta
        'mostraba un estado que ya no es el de la base y hay que volver a leerla
        If aviso = "" OrElse desactualizado Then hayCambios = False

        'recargo la lista: muestra el estado real, y el registro sigue marcado si todavia esta listado
        CargarServicios()
        'si la baja se rechazo y habia cambios sin guardar, la tarjeta queda como estaba
        If Not hayCambios Then RefrescarRegistro()
    End Sub

End Class

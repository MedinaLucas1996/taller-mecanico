Imports MySqlConnector

Public Class FrmCategorias

    'categoria que se ve en la tarjeta del registro, queda en 0 mientras no haya (el ID no se muestra)
    Private idCategoria As Integer = 0
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

    Sub CargarCategorias()
        'cargo la grilla con el filtro escrito y dejo marcada la categoria que se estaba viendo
        Dim filtro As String = txtFiltro.Text.Trim

        cargando = True

        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql, cada categoria con la cantidad de servicios activos que tiene
                Dim consulta As String =
                    "SELECT c.id_categoria_servicio, c.descripcion, " &
                    "(SELECT COUNT(*) FROM servicio AS s " &
                    "WHERE s.id_categoria_servicio = c.id_categoria_servicio AND s.activo = 1) AS servicios_activos, " &
                    "c.activo " &
                    "FROM categoria_servicio AS c " &
                    "WHERE 1 = 1 "

                'solo traigo categorias activas, salvo que se pida ver tambien las dadas de baja
                If Not chkBajas.Checked Then
                    consulta = consulta & "AND c.activo = 1 "
                End If

                'aplico filtro por descripcion
                If filtro <> "" Then
                    consulta = consulta & "AND c.descripcion LIKE @filtro "
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

                    'la clave y el estado se usan al elegir la fila, no se muestran
                    dgvCategorias.Columns("id_categoria_servicio").Visible = False
                    dgvCategorias.Columns("activo").Visible = False

                    'pongo titulos legibles en las columnas y reparto el ancho, con un minimo para cada una
                    dgvCategorias.Columns("descripcion").HeaderText = "Descripción"
                    dgvCategorias.Columns("descripcion").FillWeight = 70
                    dgvCategorias.Columns("descripcion").MinimumWidth = 160
                    'la cantidad va alineada a la derecha, con su titulo
                    dgvCategorias.Columns("servicios_activos").HeaderText = "Servicios activos"
                    dgvCategorias.Columns("servicios_activos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvCategorias.Columns("servicios_activos").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvCategorias.Columns("servicios_activos").FillWeight = 30
                    dgvCategorias.Columns("servicios_activos").MinimumWidth = 120

                    'el orden por columna lo hace el formulario al hacer click en el titulo (ver ColumnHeaderMouseClick)
                    dgvCategorias.Columns("descripcion").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvCategorias.Columns("servicios_activos").SortMode = DataGridViewColumnSortMode.Programmatic

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
                        lblSubtitulo.Text = activos & " activas · " & bajas & " dadas de baja"
                    ElseIf activos = 1 Then
                        lblSubtitulo.Text = "1 categoría activa"
                    Else
                        lblSubtitulo.Text = activos & " categorías activas"
                    End If
                End Using
            End Using

            'en la lista nueva dejo marcado el registro que se estaba viendo
            Dim filaActual As DataGridViewRow = MarcarFila(idCategoria)
            If filaActual Is Nothing AndAlso idCategoria <> 0 AndAlso Not hayCambios Then
                'ya no esta en la lista (por el filtro o por una baja): la tarjeta vuelve a la ayuda
                MostrarAyuda()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar las categorías: " & ex.Message)
        Finally
            'pase lo que pase, la grilla vuelve a atender al usuario
            cargando = False
        End Try
    End Sub

    Function MarcarFila(id As Integer) As DataGridViewRow
        'dejo marcada en la lista la fila con esa clave: celda actual y seleccion juntas
        'con clave 0, o si el registro no esta listado, no queda nada marcado; devuelve la fila o Nothing
        Dim encontrada As DataGridViewRow = Nothing
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If id <> 0 AndAlso dgvCategorias.Columns.Contains("id_categoria_servicio") Then
                For Each fila As DataGridViewRow In dgvCategorias.Rows
                    If CInt(fila.Cells("id_categoria_servicio").Value) = id Then encontrada = fila
                Next
            End If

            dgvCategorias.ClearSelection()
            If encontrada Is Nothing Then
                dgvCategorias.CurrentCell = Nothing
            Else
                dgvCategorias.CurrentCell = encontrada.Cells("descripcion")
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    Sub RefrescarRegistro()
        'vuelvo a mostrar en la tarjeta los datos guardados del registro que se esta viendo
        Dim fila As DataGridViewRow = MarcarFila(idCategoria)
        If fila IsNot Nothing Then MostrarRegistro(fila)
    End Sub

    Sub MostrarAyuda()
        'sin registro elegido ni alta en curso, la tarjeta muestra solo la ayuda
        idCategoria = 0
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
                txtDescripcion.Clear()
            Else
                txtDescripcion.Text = fila.Cells("descripcion").Value.ToString()
            End If

            'lo que se ve es lo que esta guardado
            hayCambios = False
        Finally
            cargando = estabaCargando
        End Try
    End Sub

    Sub MostrarRegistro(fila As DataGridViewRow)
        'cargo en la tarjeta la categoria de la fila, para verla y modificarla
        idCategoria = CInt(fila.Cells("id_categoria_servicio").Value)
        creando = False
        registroActivo = Convert.ToBoolean(fila.Cells("activo").Value)

        CargarCampos(fila)
        lblRegistroTitulo.Text = fila.Cells("descripcion").Value.ToString()

        'etiqueta de estado y boton de la izquierda segun el registro este activo o dado de baja
        lblEstadoRegistro.Visible = True
        If registroActivo Then
            lblEstadoRegistro.Text = "Activa"
            lblEstadoRegistro.BackColor = Color.FromArgb(223, 240, 216)
            lblEstadoRegistro.ForeColor = Color.DarkGreen
            btnBaja.Text = " Dar de baja"
            btnBaja.ForeColor = Color.Firebrick
            btnBaja.Image = iconoBaja
        Else
            lblEstadoRegistro.Text = "Dada de baja"
            lblEstadoRegistro.BackColor = Color.Gainsboro
            lblEstadoRegistro.ForeColor = Color.DimGray
            btnBaja.Text = " Reactivar"
            btnBaja.ForeColor = Color.Black
            btnBaja.Image = iconoReactivar
        End If

        btnBaja.Visible = True
        btnCancelar.Visible = False
        btnGuardar.Text = " Guardar cambios"

        'linea de contexto: cuantos servicios activos tiene, ya viene contado en la fila
        Dim servicios As Integer = Convert.ToInt32(fila.Cells("servicios_activos").Value)
        If servicios = 0 Then
            lblContexto.Text = "Sin servicios activos"
        ElseIf servicios = 1 Then
            lblContexto.Text = "1 servicio activo"
        Else
            lblContexto.Text = servicios & " servicios activos"
        End If
        lblContexto.Visible = True

        lblAyuda.Visible = False
        pnlRegistro.Visible = True
    End Sub

    Sub NuevoRegistro()
        'dejo la tarjeta lista para cargar un nuevo registro
        idCategoria = 0
        creando = True
        registroActivo = True

        CargarCampos(Nothing)
        lblRegistroTitulo.Text = "Nueva categoría"

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

        txtDescripcion.Focus()
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
        Dim idElegido As Integer = CInt(fila.Cells("id_categoria_servicio").Value)

        'si ya se esta viendo ese registro no hay nada que hacer
        If idElegido = idCategoria AndAlso Not creando Then Exit Sub

        If Not DescartarCambios() Then
            'el usuario prefirio seguir con lo que estaba cargando: la lista vuelve a marcar ese registro
            '(en un alta no hay registro, y no queda nada marcado)
            MarcarFila(idCategoria)
            Exit Sub
        End If

        MostrarRegistro(fila)
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
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnNuevo.Image = LeerIcono("agregar.png")
        btnGuardar.Image = LeerIcono("guardar.png")
        btnCancelar.Image = LeerIcono("cancelar-oscuro.png")
        picBuscar.Image = LeerIcono("buscar-oscuro.png")
        iconoBaja = LeerIcono("baja-rojo.png")
        iconoReactivar = LeerIcono("reactivar-oscuro.png")

        'el encabezado de la lista lleva solo una linea clara debajo, como las filas
        dgvCategorias.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'la tarjeta arranca con la ayuda
        MostrarAyuda()

        'solo el administrador puede gestionar categorias (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede gestionar categorías.")
            btnNuevo.Enabled = False
            pnlLista.Enabled = False
            pnlFicha.Enabled = False
            Exit Sub
        End If

        'cargo la grilla de categorias al abrir el formulario
        CargarCategorias()
    End Sub

    Private Sub FrmCategorias_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(idCategoria)
    End Sub

    Private Sub FrmCategorias_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
        CargarCategorias()
    End Sub

    Private Sub chkBajas_CheckedChanged(sender As Object, e As EventArgs) Handles chkBajas.CheckedChanged
        'muestro o dejo de mostrar los registros dados de baja
        CargarCategorias()
    End Sub

    Private Sub dgvCategorias_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvCategorias.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas
        If Not dgvCategorias.Columns.Contains("activo") Then Exit Sub

        For Each fila As DataGridViewRow In dgvCategorias.Rows
            'los registros dados de baja van en gris, tambien cuando la fila esta seleccionada
            If Not Convert.ToBoolean(fila.Cells("activo").Value) Then
                fila.DefaultCellStyle.ForeColor = Color.Gray
                fila.DefaultCellStyle.SelectionForeColor = Color.Gainsboro
            End If
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcado solo el registro que se esta viendo
        MarcarFila(idCategoria)
    End Sub

    Private Sub dgvCategorias_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvCategorias.ColumnHeaderMouseClick
        'ordeno la lista por la columna del titulo que se toco; otro click en la misma columna invierte el orden
        'ordenar nunca cambia el registro abierto ni pregunta nada
        If e.Button <> MouseButtons.Left Then Exit Sub
        If cargando Then Exit Sub

        Dim columna As DataGridViewColumn = dgvCategorias.Columns(e.ColumnIndex)
        Dim sentido As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        If dgvCategorias.SortedColumn Is columna AndAlso dgvCategorias.SortOrder = SortOrder.Ascending Then
            sentido = System.ComponentModel.ListSortDirection.Descending
        End If

        'mientras se ordena la grilla mueve sola su seleccion: eso no es una eleccion del usuario
        cargando = True
        Try
            dgvCategorias.Sort(columna, sentido)

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

    Private Sub dgvCategorias_Sorted(sender As Object, e As EventArgs) Handles dgvCategorias.Sorted
        'las filas cambiaron de lugar: vuelvo a marcar el registro que se esta viendo
        MarcarFila(idCategoria)
    End Sub

    Private Sub dgvCategorias_SelectionChanged(sender As Object, e As EventArgs) Handles dgvCategorias.SelectionChanged
        'cargo en la tarjeta el registro que el usuario elige con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargando Then Exit Sub
        If Not dgvCategorias.Focused Then Exit Sub
        If dgvCategorias.SelectedRows.Count = 0 Then Exit Sub

        ElegirFila(dgvCategorias.SelectedRows(0))
    End Sub

    Private Sub dgvCategorias_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCategorias.CellClick
        'un click sobre una fila siempre la carga, aunque la grilla ya la tuviera marcada
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirFila(dgvCategorias.Rows(e.RowIndex))
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

    Private Sub Campo_Changed(sender As Object, e As EventArgs) Handles txtDescripcion.TextChanged
        'el usuario cambio un campo: hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo una categoria nueva, o los cambios de la categoria que se esta viendo
        If Not ValidarCampos() Then Exit Sub

        'recuerdo si era un alta, porque al guardar deja de serlo
        Dim eraAlta As Boolean = creando

        If creando Then
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

                        'la categoria nueva queda como la que se esta viendo, con la clave que le dio la base
                        idCategoria = CInt(cmd.LastInsertedId)
                        creando = False
                        hayCambios = False

                        MessageBox.Show("Registros agregados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: la descripcion ya existe (restriccion unica un_categoria_servicio_descripcion)
                MessageBox.Show("Ya existe una categoría con esa descripción. Puede corresponder a una categoría dada de baja.")
                txtDescripcion.Focus()
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
                    Dim consulta As String =
                        "UPDATE categoria_servicio SET descripcion=@descripcion WHERE id_categoria_servicio=@id;"

                    Using cmd As New MySqlCommand(consulta, cn)
                        'uso parametros para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim)
                        cmd.Parameters.AddWithValue("@id", idCategoria)

                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        hayCambios = False
                        MessageBox.Show("Registros actualizados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: la descripcion ya existe (restriccion unica un_categoria_servicio_descripcion)
                MessageBox.Show("Ya existe otra categoría con esa descripción. Puede corresponder a una categoría dada de baja.")
                txtDescripcion.Focus()
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
        CargarCategorias()
        RefrescarRegistro()
    End Sub

    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        'doy de baja la categoria que se esta viendo, o la reactivo si ya estaba dada de baja
        Dim idRegistro As Integer = idCategoria

        'la baja y la reactivacion no guardan los campos: si hay cambios lo aviso en la misma pregunta
        Dim avisoCambios As String = ""
        If hayCambios Then
            avisoCambios = vbCrLf & vbCrLf & "Los cambios sin guardar de la tarjeta se van a perder."
        End If

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If registroActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja la categoría " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar la categoría " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        'si no confirma no se hace nada, y lo que estaba escribiendo queda como estaba
        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'estado de la categoria leido en la base dentro de la transaccion
        Dim activoEnBase As Boolean = False
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
                        cmd.Parameters.AddWithValue("@id", idRegistro)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then
                            existe = True
                            activoEnBase = Convert.ToBoolean(resultado)
                        End If
                    End Using

                    If Not existe Then
                        aviso = "La categoría seleccionada ya no existe."
                        desactualizado = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activoEnBase <> registroActivo Then
                        aviso = "El estado de la categoría fue cambiado desde otro puesto. Se actualizó la lista: revísela y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso activoEnBase Then
                        'no se da de baja una categoria que tiene servicios activos
                        consulta = "SELECT COUNT(*) FROM servicio " &
                                   "WHERE id_categoria_servicio = @id AND activo = 1;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
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
            MessageBox.Show("Error al cambiar el estado de la categoría: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        ElseIf activoEnBase Then
            MessageBox.Show("Categoría dada de baja.")
        Else
            MessageBox.Show("Categoría reactivada.")
        End If

        'los cambios sin guardar se pierden recien ahora: cuando el estado cambio, o cuando la tarjeta
        'mostraba un estado que ya no es el de la base y hay que volver a leerla
        If aviso = "" OrElse desactualizado Then hayCambios = False

        'recargo la lista: muestra el estado real, y el registro sigue marcado si todavia esta listado
        CargarCategorias()
        'si la baja se rechazo y habia cambios sin guardar, la tarjeta queda como estaba
        If Not hayCambios Then RefrescarRegistro()
    End Sub

End Class

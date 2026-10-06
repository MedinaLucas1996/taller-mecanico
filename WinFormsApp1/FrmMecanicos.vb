Imports MySqlConnector

Public Class FrmMecanicos

    'mecanico que se ve en la tarjeta del registro, queda en 0 mientras no haya (el ID no se muestra)
    Private idMecanico As Integer = 0
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

    Sub CargarMecanicos()
        'cargo la grilla con el filtro escrito y dejo marcado al mecanico que se estaba viendo
        Dim filtro As String = txtFiltro.Text.Trim

        cargando = True

        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql
                Dim consulta As String =
                    "SELECT id_mecanico, nombre_completo, especialidad, telefono, activo " &
                    "FROM mecanico " &
                    "WHERE 1 = 1 "

                'solo traigo mecanicos activos, salvo que se pida ver tambien los dados de baja
                If Not chkBajas.Checked Then
                    consulta = consulta & "AND activo = 1 "
                End If

                'aplico filtro por nombre o especialidad
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (nombre_completo LIKE @filtro OR especialidad LIKE @filtro) "
                End If

                'primero los activos, despues los dados de baja
                consulta = consulta & "ORDER BY activo DESC, nombre_completo;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvMecanicos.DataSource = tabla

                    'la clave y el estado se usan al elegir la fila, no se muestran
                    dgvMecanicos.Columns("id_mecanico").Visible = False
                    dgvMecanicos.Columns("activo").Visible = False

                    'pongo titulos legibles en las columnas y reparto el ancho, con un minimo para cada una
                    dgvMecanicos.Columns("nombre_completo").HeaderText = "Nombre"
                    dgvMecanicos.Columns("nombre_completo").FillWeight = 40
                    dgvMecanicos.Columns("nombre_completo").MinimumWidth = 150
                    dgvMecanicos.Columns("especialidad").HeaderText = "Especialidad"
                    dgvMecanicos.Columns("especialidad").FillWeight = 35
                    dgvMecanicos.Columns("especialidad").MinimumWidth = 120
                    dgvMecanicos.Columns("telefono").HeaderText = "Teléfono"
                    dgvMecanicos.Columns("telefono").FillWeight = 25
                    dgvMecanicos.Columns("telefono").MinimumWidth = 95

                    'el orden por columna lo hace el formulario al hacer click en el titulo (ver ColumnHeaderMouseClick)
                    dgvMecanicos.Columns("nombre_completo").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvMecanicos.Columns("especialidad").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvMecanicos.Columns("telefono").SortMode = DataGridViewColumnSortMode.Programmatic

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
                        lblSubtitulo.Text = "1 mecánico activo"
                    Else
                        lblSubtitulo.Text = activos & " mecánicos activos"
                    End If
                End Using
            End Using

            'en la lista nueva dejo marcado el registro que se estaba viendo
            Dim filaActual As DataGridViewRow = MarcarFila(idMecanico)
            If filaActual Is Nothing AndAlso idMecanico <> 0 AndAlso Not hayCambios Then
                'ya no esta en la lista (por el filtro o por una baja): la tarjeta vuelve a la ayuda
                MostrarAyuda()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar los mecánicos: " & ex.Message)
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
            If id <> 0 AndAlso dgvMecanicos.Columns.Contains("id_mecanico") Then
                For Each fila As DataGridViewRow In dgvMecanicos.Rows
                    If CInt(fila.Cells("id_mecanico").Value) = id Then encontrada = fila
                Next
            End If

            dgvMecanicos.ClearSelection()
            If encontrada Is Nothing Then
                dgvMecanicos.CurrentCell = Nothing
            Else
                dgvMecanicos.CurrentCell = encontrada.Cells("nombre_completo")
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    Sub RefrescarRegistro()
        'vuelvo a mostrar en la tarjeta los datos guardados del registro que se esta viendo
        Dim fila As DataGridViewRow = MarcarFila(idMecanico)
        If fila IsNot Nothing Then MostrarRegistro(fila)
    End Sub

    Sub MostrarAyuda()
        'sin registro elegido ni alta en curso, la tarjeta muestra solo la ayuda
        idMecanico = 0
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
                txtNombreCompleto.Clear()
                txtEspecialidad.Clear()
                txtTelefono.Clear()
            Else
                'la especialidad y el telefono pueden estar vacios en la base: ToString los deja en blanco
                txtNombreCompleto.Text = fila.Cells("nombre_completo").Value.ToString()
                txtEspecialidad.Text = fila.Cells("especialidad").Value.ToString()
                txtTelefono.Text = fila.Cells("telefono").Value.ToString()
            End If

            'lo que se ve es lo que esta guardado
            hayCambios = False
        Finally
            cargando = estabaCargando
        End Try
    End Sub

    Sub MostrarRegistro(fila As DataGridViewRow)
        'cargo en la tarjeta al mecanico de la fila, para verlo y modificarlo
        idMecanico = CInt(fila.Cells("id_mecanico").Value)
        creando = False
        registroActivo = Convert.ToBoolean(fila.Cells("activo").Value)

        CargarCampos(fila)
        lblRegistroTitulo.Text = fila.Cells("nombre_completo").Value.ToString()

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

        'linea de contexto: cuantas ordenes de trabajo sin cerrar tiene asignadas
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT COUNT(*) FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.id_mecanico = @id AND e.es_estado_final = 0;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", idMecanico)
                    Dim abiertas As Integer = CInt(cmd.ExecuteScalar())
                    If abiertas = 0 Then
                        lblContexto.Text = "Sin órdenes de trabajo abiertas"
                    ElseIf abiertas = 1 Then
                        lblContexto.Text = "1 orden de trabajo abierta asignada"
                    Else
                        lblContexto.Text = abiertas & " órdenes de trabajo abiertas asignadas"
                    End If
                End Using
            End Using
            lblContexto.Visible = True
        Catch ex As Exception
            'si no se pudo contar, la tarjeta sirve igual sin esa linea
            lblContexto.Visible = False
        End Try

        lblAyuda.Visible = False
        pnlRegistro.Visible = True
    End Sub

    Sub NuevoRegistro()
        'dejo la tarjeta lista para cargar un nuevo registro
        idMecanico = 0
        creando = True
        registroActivo = True

        CargarCampos(Nothing)
        lblRegistroTitulo.Text = "Nuevo mecánico"

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

        txtNombreCompleto.Focus()
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
        Dim idElegido As Integer = CInt(fila.Cells("id_mecanico").Value)

        'si ya se esta viendo ese registro no hay nada que hacer
        If idElegido = idMecanico AndAlso Not creando Then Exit Sub

        If Not DescartarCambios() Then
            'el usuario prefirio seguir con lo que estaba cargando: la lista vuelve a marcar ese registro
            '(en un alta no hay registro, y no queda nada marcado)
            MarcarFila(idMecanico)
            Exit Sub
        End If

        MostrarRegistro(fila)
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If txtNombreCompleto.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre completo")
            txtNombreCompleto.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmMecanicos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnNuevo.Image = LeerIcono("agregar.png")
        btnGuardar.Image = LeerIcono("guardar.png")
        btnCancelar.Image = LeerIcono("cancelar-oscuro.png")
        picBuscar.Image = LeerIcono("buscar-oscuro.png")
        iconoBaja = LeerIcono("baja-rojo.png")
        iconoReactivar = LeerIcono("reactivar-oscuro.png")

        'el encabezado de la lista lleva solo una linea clara debajo, como las filas
        dgvMecanicos.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'la tarjeta arranca con la ayuda
        MostrarAyuda()

        'solo el administrador puede gestionar mecanicos (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede gestionar mecánicos.")
            btnNuevo.Enabled = False
            pnlLista.Enabled = False
            pnlFicha.Enabled = False
            Exit Sub
        End If

        'cargo la grilla de mecanicos al abrir el formulario
        CargarMecanicos()
    End Sub

    Private Sub FrmMecanicos_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(idMecanico)
    End Sub

    Private Sub FrmMecanicos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
        CargarMecanicos()
    End Sub

    Private Sub chkBajas_CheckedChanged(sender As Object, e As EventArgs) Handles chkBajas.CheckedChanged
        'muestro o dejo de mostrar los registros dados de baja
        CargarMecanicos()
    End Sub

    Private Sub dgvMecanicos_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvMecanicos.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas
        If Not dgvMecanicos.Columns.Contains("activo") Then Exit Sub

        For Each fila As DataGridViewRow In dgvMecanicos.Rows
            'los registros dados de baja van en gris, tambien cuando la fila esta seleccionada
            If Not Convert.ToBoolean(fila.Cells("activo").Value) Then
                fila.DefaultCellStyle.ForeColor = Color.Gray
                fila.DefaultCellStyle.SelectionForeColor = Color.Gainsboro
            End If
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcado solo el registro que se esta viendo
        MarcarFila(idMecanico)
    End Sub

    Private Sub dgvMecanicos_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvMecanicos.ColumnHeaderMouseClick
        'ordeno la lista por la columna del titulo que se toco; otro click en la misma columna invierte el orden
        'ordenar nunca cambia el registro abierto ni pregunta nada
        If e.Button <> MouseButtons.Left Then Exit Sub
        If cargando Then Exit Sub

        Dim columna As DataGridViewColumn = dgvMecanicos.Columns(e.ColumnIndex)
        Dim sentido As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        If dgvMecanicos.SortedColumn Is columna AndAlso dgvMecanicos.SortOrder = SortOrder.Ascending Then
            sentido = System.ComponentModel.ListSortDirection.Descending
        End If

        'mientras se ordena la grilla mueve sola su seleccion: eso no es una eleccion del usuario
        cargando = True
        Try
            dgvMecanicos.Sort(columna, sentido)

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

    Private Sub dgvMecanicos_Sorted(sender As Object, e As EventArgs) Handles dgvMecanicos.Sorted
        'las filas cambiaron de lugar: vuelvo a marcar el registro que se esta viendo
        MarcarFila(idMecanico)
    End Sub

    Private Sub dgvMecanicos_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMecanicos.SelectionChanged
        'cargo en la tarjeta el registro que el usuario elige con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargando Then Exit Sub
        If Not dgvMecanicos.Focused Then Exit Sub
        If dgvMecanicos.SelectedRows.Count = 0 Then Exit Sub

        ElegirFila(dgvMecanicos.SelectedRows(0))
    End Sub

    Private Sub dgvMecanicos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMecanicos.CellClick
        'un click sobre una fila siempre la carga, aunque la grilla ya la tuviera marcada
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirFila(dgvMecanicos.Rows(e.RowIndex))
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

    Private Sub Campo_Changed(sender As Object, e As EventArgs) Handles _
        txtNombreCompleto.TextChanged, txtEspecialidad.TextChanged, txtTelefono.TextChanged

        'el usuario cambio un campo: hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un mecanico nuevo, o los cambios del mecanico que se esta viendo
        If Not ValidarCampos() Then Exit Sub

        'recuerdo si era un alta, porque al guardar deja de serlo
        Dim eraAlta As Boolean = creando

        'la especialidad y el telefono vacios se guardan como NULL
        Dim especialidad As Object = DBNull.Value
        If txtEspecialidad.Text.Trim <> "" Then especialidad = txtEspecialidad.Text.Trim
        Dim telefono As Object = DBNull.Value
        If txtTelefono.Text.Trim <> "" Then telefono = txtTelefono.Text.Trim

        If creando Then
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()

                    'no puede haber dos mecanicos activos con el mismo nombre, sin mirar mayusculas ni espacios
                    Dim consulta As String =
                        "SELECT COUNT(*) FROM mecanico " &
                        "WHERE activo = 1 AND LOWER(TRIM(nombre_completo)) = LOWER(@nombre_completo);"
                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                        If CInt(cmd.ExecuteScalar()) > 0 Then
                            MessageBox.Show("Ya existe un mecánico activo con ese nombre.")
                            txtNombreCompleto.Focus()
                            Exit Sub
                        End If
                    End Using

                    'armo mi consulta sql
                    consulta =
                        "INSERT INTO mecanico (nombre_completo, especialidad, telefono) " &
                        "VALUES (@nombre_completo, @especialidad, @telefono);"

                    Using cmd As New MySqlCommand(consulta, cn)
                        'cargo valores en los parametros
                        cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                        cmd.Parameters.AddWithValue("@especialidad", especialidad)
                        cmd.Parameters.AddWithValue("@telefono", telefono)

                        'ejecuto la consulta y obtengo los registros afectados
                        Dim Resultado As Integer = cmd.ExecuteNonQuery()

                        'el mecanico nuevo queda como el que se esta viendo, con la clave que le dio la base
                        idMecanico = CInt(cmd.LastInsertedId)
                        creando = False
                        hayCambios = False

                        MessageBox.Show("Registros agregados: " & Resultado)
                    End Using
                End Using

            Catch ex As Exception
                MessageBox.Show("Error al guardar " & ex.Message)
                Exit Sub
            End Try
        Else
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()

                    'no puede quedar con el nombre de otro mecanico activo, sin mirar mayusculas ni espacios
                    Dim consulta As String =
                        "SELECT COUNT(*) FROM mecanico " &
                        "WHERE activo = 1 AND LOWER(TRIM(nombre_completo)) = LOWER(@nombre_completo) " &
                        "AND id_mecanico <> @id;"
                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                        cmd.Parameters.AddWithValue("@id", idMecanico)
                        If CInt(cmd.ExecuteScalar()) > 0 Then
                            MessageBox.Show("Ya existe otro mecánico activo con ese nombre.")
                            txtNombreCompleto.Focus()
                            Exit Sub
                        End If
                    End Using

                    'uso update para modificar y where para indicar que registro
                    consulta =
                        "UPDATE mecanico SET nombre_completo=@nombre_completo, especialidad=@especialidad, " &
                        "telefono=@telefono " &
                        "WHERE id_mecanico=@id;"

                    Using cmd As New MySqlCommand(consulta, cn)
                        'uso parametros para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                        cmd.Parameters.AddWithValue("@especialidad", especialidad)
                        cmd.Parameters.AddWithValue("@telefono", telefono)
                        cmd.Parameters.AddWithValue("@id", idMecanico)

                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        hayCambios = False
                        MessageBox.Show("Registros actualizados: " & Resultado)
                    End Using
                End Using

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
        CargarMecanicos()
        RefrescarRegistro()
    End Sub

    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        'doy de baja al mecanico que se esta viendo, o lo reactivo si ya estaba dado de baja
        Dim idRegistro As Integer = idMecanico

        'la baja y la reactivacion no guardan los campos: si hay cambios lo aviso en la misma pregunta
        Dim avisoCambios As String = ""
        If hayCambios Then
            avisoCambios = vbCrLf & vbCrLf & "Los cambios sin guardar de la tarjeta se van a perder."
        End If

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If registroActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja al mecánico " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar al mecánico " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        'si no confirma no se hace nada, y lo que estaba escribiendo queda como estaba
        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'estado del mecanico leido en la base dentro de la transaccion
        Dim activoEnBase As Boolean = False
        'queda en True cuando lo que hay en pantalla ya no coincide con la base
        Dim desactualizado As Boolean = False

        'baja logica: no borro el registro porque puede tener ordenes de trabajo
        'solo lo marco como inactivo, y con el mismo boton se lo puede volver a activar
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el mecanico y bloqueo solo su fila hasta terminar
                    Dim nombreGuardado As String = ""
                    Dim existe As Boolean = False
                    Dim consulta As String =
                        "SELECT nombre_completo, activo FROM mecanico WHERE id_mecanico = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idRegistro)
                        Using lector As MySqlDataReader = cmd.ExecuteReader
                            If lector.Read() Then
                                existe = True
                                nombreGuardado = lector("nombre_completo").ToString()
                                activoEnBase = Convert.ToBoolean(lector("activo"))
                            End If
                        End Using
                    End Using

                    If Not existe Then
                        aviso = "El mecánico seleccionado ya no existe."
                        desactualizado = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activoEnBase <> registroActivo Then
                        aviso = "El estado del mecánico fue cambiado desde otro puesto. Se actualizó la lista: revíselo y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso activoEnBase Then
                        'no se da de baja a un mecanico con ordenes de trabajo sin cerrar
                        consulta =
                            "SELECT COUNT(*) FROM orden_trabajo AS ot " &
                            "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                            "WHERE ot.id_mecanico = @id AND e.es_estado_final = 0;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
                            Dim abiertas As Integer = CInt(cmd.ExecuteScalar())
                            If abiertas > 0 Then
                                aviso = "No se puede dar de baja: el mecánico tiene " & abiertas &
                                        " orden(es) de trabajo sin cerrar. Asígnelas a otro mecánico o ciérrelas primero."
                            End If
                        End Using
                    End If

                    If aviso = "" AndAlso Not activoEnBase Then
                        'al reactivar no puede quedar con el nombre de otro mecanico activo
                        consulta =
                            "SELECT COUNT(*) FROM mecanico " &
                            "WHERE activo = 1 AND LOWER(TRIM(nombre_completo)) = LOWER(TRIM(@nombre_completo)) " &
                            "AND id_mecanico <> @id;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@nombre_completo", nombreGuardado)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
                            If CInt(cmd.ExecuteScalar()) > 0 Then
                                aviso = "No se puede reactivar: ya existe otro mecánico activo con ese nombre."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE mecanico SET activo = @activo WHERE id_mecanico = @id;"
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
            MessageBox.Show("Error al cambiar el estado del mecánico: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        ElseIf activoEnBase Then
            MessageBox.Show("Mecánico dado de baja.")
        Else
            MessageBox.Show("Mecánico reactivado.")
        End If

        'los cambios sin guardar se pierden recien ahora: cuando el estado cambio, o cuando la tarjeta
        'mostraba un estado que ya no es el de la base y hay que volver a leerla
        If aviso = "" OrElse desactualizado Then hayCambios = False

        'recargo la lista: muestra el estado real, y el registro sigue marcado si todavia esta listado
        CargarMecanicos()
        'si la baja se rechazo y habia cambios sin guardar, la tarjeta queda como estaba
        If Not hayCambios Then RefrescarRegistro()
    End Sub

End Class

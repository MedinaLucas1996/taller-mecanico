Imports MySqlConnector

Public Class FrmUsuarios

    'usuario que se ve en la tarjeta del registro, queda en 0 mientras no haya (el ID no se muestra)
    Private idUsuario As Integer = 0
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

    Sub CargarUsuarios()
        'cargo la grilla con el filtro escrito y dejo marcado al usuario que se estaba viendo
        Dim filtro As String = txtFiltro.Text.Trim

        cargando = True

        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql
                'nunca traigo el hash ni el salt de la clave
                Dim consulta As String =
                    "SELECT u.id_usuario, u.nombre_usuario, u.nombre_completo, u.rol, " &
                    "m.nombre_completo AS mecanico, u.id_mecanico, u.activo " &
                    "FROM usuario AS u " &
                    "LEFT JOIN mecanico AS m ON m.id_mecanico = u.id_mecanico " &
                    "WHERE 1 = 1 "

                'solo traigo usuarios activos, salvo que se pida ver tambien los dados de baja
                If Not chkBajas.Checked Then
                    consulta = consulta & "AND u.activo = 1 "
                End If

                'aplico filtro por usuario o nombre completo
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (u.nombre_usuario LIKE @filtro OR u.nombre_completo LIKE @filtro) "
                End If

                'primero los activos, despues los dados de baja
                consulta = consulta & "ORDER BY u.activo DESC, u.nombre_usuario;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvUsuarios.DataSource = tabla

                    'las claves y el estado se usan al elegir la fila, no se muestran
                    dgvUsuarios.Columns("id_usuario").Visible = False
                    dgvUsuarios.Columns("id_mecanico").Visible = False
                    dgvUsuarios.Columns("activo").Visible = False

                    'pongo titulos legibles en las columnas y reparto el ancho, con un minimo para cada una
                    dgvUsuarios.Columns("nombre_usuario").HeaderText = "Usuario"
                    dgvUsuarios.Columns("nombre_usuario").FillWeight = 22
                    dgvUsuarios.Columns("nombre_usuario").MinimumWidth = 90
                    dgvUsuarios.Columns("nombre_completo").HeaderText = "Nombre completo"
                    dgvUsuarios.Columns("nombre_completo").FillWeight = 32
                    dgvUsuarios.Columns("nombre_completo").MinimumWidth = 130
                    dgvUsuarios.Columns("rol").HeaderText = "Rol"
                    dgvUsuarios.Columns("rol").FillWeight = 22
                    dgvUsuarios.Columns("rol").MinimumWidth = 110
                    dgvUsuarios.Columns("mecanico").HeaderText = "Mecánico"
                    dgvUsuarios.Columns("mecanico").FillWeight = 24
                    dgvUsuarios.Columns("mecanico").MinimumWidth = 100

                    'el orden por columna lo hace el formulario al hacer click en el titulo (ver ColumnHeaderMouseClick)
                    dgvUsuarios.Columns("nombre_usuario").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvUsuarios.Columns("nombre_completo").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvUsuarios.Columns("rol").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvUsuarios.Columns("mecanico").SortMode = DataGridViewColumnSortMode.Programmatic

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
                        lblSubtitulo.Text = "1 usuario activo"
                    Else
                        lblSubtitulo.Text = activos & " usuarios activos"
                    End If
                End Using
            End Using

            'en la lista nueva dejo marcado el registro que se estaba viendo
            Dim filaActual As DataGridViewRow = MarcarFila(idUsuario)
            If filaActual Is Nothing AndAlso idUsuario <> 0 AndAlso Not hayCambios Then
                'ya no esta en la lista (por el filtro o por una baja): la tarjeta vuelve a la ayuda
                MostrarAyuda()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar los usuarios: " & ex.Message)
        Finally
            'pase lo que pase, la grilla vuelve a atender al usuario
            cargando = False
        End Try
    End Sub

    Sub CargarComboMecanicos()
        'cargo los mecanicos activos en el combo
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_mecanico, nombre_completo FROM mecanico WHERE activo = 1 ORDER BY nombre_completo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'agrego una primera opcion con clave 0 que funciona como texto de ayuda
                    Dim filaAyuda As DataRow = tabla.NewRow()
                    filaAyuda("id_mecanico") = 0
                    filaAyuda("nombre_completo") = "Seleccione un mecánico"
                    tabla.Rows.InsertAt(filaAyuda, 0)

                    'el usuario ve el nombre...
                    cboMecanico.DisplayMember = "nombre_completo"
                    '...pero el programa guarda la clave numerica
                    cboMecanico.ValueMember = "id_mecanico"
                    cboMecanico.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los mecánicos: " & ex.Message)
        End Try
    End Sub

    Function MarcarFila(id As Integer) As DataGridViewRow
        'dejo marcada en la lista la fila con esa clave: celda actual y seleccion juntas
        'con clave 0, o si el registro no esta listado, no queda nada marcado; devuelve la fila o Nothing
        Dim encontrada As DataGridViewRow = Nothing
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If id <> 0 AndAlso dgvUsuarios.Columns.Contains("id_usuario") Then
                For Each fila As DataGridViewRow In dgvUsuarios.Rows
                    If CInt(fila.Cells("id_usuario").Value) = id Then encontrada = fila
                Next
            End If

            dgvUsuarios.ClearSelection()
            If encontrada Is Nothing Then
                dgvUsuarios.CurrentCell = Nothing
            Else
                dgvUsuarios.CurrentCell = encontrada.Cells("nombre_usuario")
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    Sub RefrescarRegistro()
        'vuelvo a mostrar en la tarjeta los datos guardados del registro que se esta viendo
        Dim fila As DataGridViewRow = MarcarFila(idUsuario)
        If fila IsNot Nothing Then MostrarRegistro(fila)
    End Sub

    Sub MostrarAyuda()
        'sin registro elegido ni alta en curso, la tarjeta muestra solo la ayuda
        idUsuario = 0
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
            'la clave nunca se carga en el formulario
            txtPassword.Clear()

            If fila Is Nothing Then
                txtUsuario.Clear()
                txtNombreCompleto.Clear()
                'vuelvo a la opcion "Seleccione un rol", eso tambien deshabilita el combo de mecanicos
                cboRol.SelectedIndex = 0
                If cboMecanico.Items.Count > 0 Then cboMecanico.SelectedIndex = 0
            Else
                txtUsuario.Text = fila.Cells("nombre_usuario").Value.ToString()
                txtNombreCompleto.Text = fila.Cells("nombre_completo").Value.ToString()

                'primero elijo el rol: eso habilita o deshabilita el combo de mecanicos
                cboRol.SelectedItem = fila.Cells("rol").Value.ToString()

                'el mecanico es NULL cuando el rol no es MECANICO
                If IsDBNull(fila.Cells("id_mecanico").Value) Then
                    If cboMecanico.Items.Count > 0 Then cboMecanico.SelectedIndex = 0
                Else
                    cboMecanico.SelectedValue = CInt(fila.Cells("id_mecanico").Value)
                End If
            End If

            'lo que se ve es lo que esta guardado
            hayCambios = False
        Finally
            cargando = estabaCargando
        End Try
    End Sub

    Sub MostrarRegistro(fila As DataGridViewRow)
        'cargo en la tarjeta al usuario de la fila, para verlo y modificarlo
        idUsuario = CInt(fila.Cells("id_usuario").Value)
        creando = False
        registroActivo = Convert.ToBoolean(fila.Cells("activo").Value)

        CargarCampos(fila)
        lblRegistroTitulo.Text = fila.Cells("nombre_usuario").Value.ToString()

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

        'al modificar, la contraseña es opcional: vacia conserva la actual
        lblPassword.Text = "Contraseña nueva"
        lblAyudaPassword.Visible = True

        'linea de contexto: aviso cuando el usuario que se ve es el que inicio la sesion
        If idUsuario = Sesion.IdUsuario Then
            lblContexto.Text = "Es su propio usuario: no puede darlo de baja ni cambiarle el rol."
            lblContexto.Visible = True
        Else
            lblContexto.Visible = False
        End If

        lblAyuda.Visible = False
        pnlRegistro.Visible = True
    End Sub

    Sub NuevoRegistro()
        'dejo la tarjeta lista para cargar un nuevo registro
        idUsuario = 0
        creando = True
        registroActivo = True

        CargarCampos(Nothing)
        lblRegistroTitulo.Text = "Nuevo usuario"

        'un registro nuevo no tiene estado ni contexto, y sus botones son Cancelar y Guardar
        lblEstadoRegistro.Visible = False
        lblContexto.Visible = False
        btnBaja.Visible = False
        btnCancelar.Visible = True
        btnGuardar.Text = " Guardar"

        'en un alta la contraseña es obligatoria
        lblPassword.Text = "Contraseña (*)"
        lblAyudaPassword.Visible = False

        lblAyuda.Visible = False
        pnlRegistro.Visible = True

        'durante un alta no queda ninguna fila marcada en la lista
        MarcarFila(0)

        txtUsuario.Focus()
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
        Dim idElegido As Integer = CInt(fila.Cells("id_usuario").Value)

        'si ya se esta viendo ese registro no hay nada que hacer
        If idElegido = idUsuario AndAlso Not creando Then Exit Sub

        If Not DescartarCambios() Then
            'el usuario prefirio seguir con lo que estaba cargando: la lista vuelve a marcar ese registro
            '(en un alta no hay registro, y no queda nada marcado)
            MarcarFila(idUsuario)
            Exit Sub
        End If

        MostrarRegistro(fila)
    End Sub

    Function ValidarCampos(passwordObligatoria As Boolean) As Boolean
        'valido los campos obligatorios
        If txtUsuario.Text.Trim = "" Then
            MessageBox.Show("Falta el usuario")
            txtUsuario.Focus()
            Return False
        End If

        If txtNombreCompleto.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre completo")
            txtNombreCompleto.Focus()
            Return False
        End If

        'la opcion 0 es el texto de ayuda, no un rol real
        If cboRol.SelectedIndex <= 0 Then
            MessageBox.Show("Seleccione un rol")
            cboRol.Focus()
            Return False
        End If

        'solo el rol MECANICO necesita un mecanico asociado
        If cboRol.Text = "MECANICO" Then
            'la clave 0 corresponde a la opcion de ayuda, no a un mecanico real
            If Not TypeOf cboMecanico.SelectedValue Is Integer OrElse CInt(cboMecanico.SelectedValue) = 0 Then
                MessageBox.Show("Seleccione un mecánico")
                cboMecanico.Focus()
                Return False
            End If
        End If

        'la clave solo es obligatoria al crear, al modificar puede quedar vacia
        If passwordObligatoria AndAlso txtPassword.Text = "" Then
            MessageBox.Show("Falta la contraseña")
            txtPassword.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnNuevo.Image = LeerIcono("agregar.png")
        btnGuardar.Image = LeerIcono("guardar.png")
        btnCancelar.Image = LeerIcono("cancelar-oscuro.png")
        picBuscar.Image = LeerIcono("buscar-oscuro.png")
        iconoBaja = LeerIcono("baja-rojo.png")
        iconoReactivar = LeerIcono("reactivar-oscuro.png")

        'el encabezado de la lista lleva solo una linea clara debajo, como las filas
        dgvUsuarios.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'la tarjeta arranca con la ayuda
        MostrarAyuda()

        'solo el administrador puede gestionar usuarios (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" Then
            MessageBox.Show("Solo el administrador puede gestionar usuarios.")
            btnNuevo.Enabled = False
            pnlLista.Enabled = False
            pnlFicha.Enabled = False
            Exit Sub
        End If

        'cargo combos y grilla al abrir el formulario
        cargando = True
        Try
            CargarComboMecanicos()
            cboRol.SelectedIndex = 0
        Finally
            cargando = False
        End Try

        CargarUsuarios()
    End Sub

    Private Sub FrmUsuarios_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(idUsuario)
    End Sub

    Private Sub FrmUsuarios_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
        CargarUsuarios()
    End Sub

    Private Sub chkBajas_CheckedChanged(sender As Object, e As EventArgs) Handles chkBajas.CheckedChanged
        'muestro o dejo de mostrar los registros dados de baja
        CargarUsuarios()
    End Sub

    Private Sub dgvUsuarios_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvUsuarios.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas
        If Not dgvUsuarios.Columns.Contains("activo") Then Exit Sub

        For Each fila As DataGridViewRow In dgvUsuarios.Rows
            'los registros dados de baja van en gris, tambien cuando la fila esta seleccionada
            If Not Convert.ToBoolean(fila.Cells("activo").Value) Then
                fila.DefaultCellStyle.ForeColor = Color.Gray
                fila.DefaultCellStyle.SelectionForeColor = Color.Gainsboro
            End If
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcado solo el registro que se esta viendo
        MarcarFila(idUsuario)
    End Sub

    Private Sub dgvUsuarios_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvUsuarios.ColumnHeaderMouseClick
        'ordeno la lista por la columna del titulo que se toco; otro click en la misma columna invierte el orden
        'ordenar nunca cambia el registro abierto ni pregunta nada
        If e.Button <> MouseButtons.Left Then Exit Sub
        If cargando Then Exit Sub

        Dim columna As DataGridViewColumn = dgvUsuarios.Columns(e.ColumnIndex)
        Dim sentido As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        If dgvUsuarios.SortedColumn Is columna AndAlso dgvUsuarios.SortOrder = SortOrder.Ascending Then
            sentido = System.ComponentModel.ListSortDirection.Descending
        End If

        'mientras se ordena la grilla mueve sola su seleccion: eso no es una eleccion del usuario
        cargando = True
        Try
            dgvUsuarios.Sort(columna, sentido)

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

    Private Sub dgvUsuarios_Sorted(sender As Object, e As EventArgs) Handles dgvUsuarios.Sorted
        'las filas cambiaron de lugar: vuelvo a marcar el registro que se esta viendo
        MarcarFila(idUsuario)
    End Sub

    Private Sub dgvUsuarios_SelectionChanged(sender As Object, e As EventArgs) Handles dgvUsuarios.SelectionChanged
        'cargo en la tarjeta el registro que el usuario elige con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargando Then Exit Sub
        If Not dgvUsuarios.Focused Then Exit Sub
        If dgvUsuarios.SelectedRows.Count = 0 Then Exit Sub

        ElegirFila(dgvUsuarios.SelectedRows(0))
    End Sub

    Private Sub dgvUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsuarios.CellClick
        'un click sobre una fila siempre la carga, aunque la grilla ya la tuviera marcada
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirFila(dgvUsuarios.Rows(e.RowIndex))
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

    Private Sub cboRol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRol.SelectedIndexChanged
        'el mecanico solo se elige cuando el rol es MECANICO
        If cboRol.Text = "MECANICO" Then
            cboMecanico.Enabled = True
        Else
            cboMecanico.Enabled = False
            'vuelvo a la opcion "Seleccione un mecánico"
            If cboMecanico.Items.Count > 0 Then cboMecanico.SelectedIndex = 0
        End If

        'si lo cambio el usuario, hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub Campo_Changed(sender As Object, e As EventArgs) Handles _
        txtUsuario.TextChanged, txtNombreCompleto.TextChanged, cboMecanico.SelectedIndexChanged, txtPassword.TextChanged

        'el usuario cambio un campo: hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un usuario nuevo, o los cambios del usuario que se esta viendo

        'la clave es obligatoria solo en un alta
        If Not ValidarCampos(creando) Then Exit Sub

        'recuerdo si era un alta, porque al guardar deja de serlo
        Dim eraAlta As Boolean = creando

        'el mecanico solo se guarda para el rol MECANICO, en los demas va NULL
        Dim idMecanico As Object = DBNull.Value
        If cboRol.Text = "MECANICO" Then idMecanico = CInt(cboMecanico.SelectedValue)

        If creando Then
            'genero el salt y calculo el hash, nunca guardo la clave en texto plano
            Dim salt As String = GenerarSalt()
            Dim hash As String = HashearClave(txtPassword.Text, salt)

            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    'armo mi consulta sql
                    Dim consulta As String =
                        "INSERT INTO usuario (nombre_usuario, hash_contrasena, salt, nombre_completo, rol, id_mecanico) " &
                        "VALUES (@nombre_usuario, @hash_contrasena, @salt, @nombre_completo, @rol, @id_mecanico);"

                    Using cmd As New MySqlCommand(consulta, cn)
                        'cargo valores en los parametros
                        cmd.Parameters.AddWithValue("@nombre_usuario", txtUsuario.Text.Trim)
                        cmd.Parameters.AddWithValue("@hash_contrasena", hash)
                        cmd.Parameters.AddWithValue("@salt", salt)
                        cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                        cmd.Parameters.AddWithValue("@rol", cboRol.Text)
                        cmd.Parameters.AddWithValue("@id_mecanico", idMecanico)

                        'ejecuto la consulta y obtengo los registros afectados
                        Dim Resultado As Integer = cmd.ExecuteNonQuery()

                        'el usuario nuevo queda como el que se esta viendo, con la clave que le dio la base
                        idUsuario = CInt(cmd.LastInsertedId)
                        creando = False
                        hayCambios = False

                        MessageBox.Show("Registros agregados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: el usuario ya existe (restriccion unica)
                MessageBox.Show("Ya existe un usuario con ese nombre de usuario. Puede corresponder a un usuario dado de baja.")
                txtUsuario.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al guardar " & ex.Message)
                Exit Sub
            End Try
        Else
            'el administrador logueado no puede quitarse su propio rol de administrador
            If idUsuario = Sesion.IdUsuario AndAlso cboRol.Text <> "ADMINISTRADOR" Then
                MessageBox.Show("No puede cambiar su propio rol.")
                cboRol.Focus()
                Exit Sub
            End If

            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    'uso update para modificar y where para indicar que registro
                    Dim consulta As String
                    If txtPassword.Text = "" Then
                        'clave vacia: dejo el hash y el salt como estan
                        consulta =
                            "UPDATE usuario SET nombre_usuario=@nombre_usuario, nombre_completo=@nombre_completo, " &
                            "rol=@rol, id_mecanico=@id_mecanico " &
                            "WHERE id_usuario=@id;"
                    Else
                        'clave nueva: cambio tambien el hash y el salt
                        consulta =
                            "UPDATE usuario SET nombre_usuario=@nombre_usuario, nombre_completo=@nombre_completo, " &
                            "rol=@rol, id_mecanico=@id_mecanico, hash_contrasena=@hash_contrasena, salt=@salt " &
                            "WHERE id_usuario=@id;"
                    End If

                    Using cmd As New MySqlCommand(consulta, cn)
                        'uso parametros para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@nombre_usuario", txtUsuario.Text.Trim)
                        cmd.Parameters.AddWithValue("@nombre_completo", txtNombreCompleto.Text.Trim)
                        cmd.Parameters.AddWithValue("@rol", cboRol.Text)
                        cmd.Parameters.AddWithValue("@id_mecanico", idMecanico)
                        cmd.Parameters.AddWithValue("@id", idUsuario)

                        If txtPassword.Text <> "" Then
                            'genero un salt nuevo y calculo el hash de la clave nueva
                            Dim salt As String = GenerarSalt()
                            cmd.Parameters.AddWithValue("@hash_contrasena", HashearClave(txtPassword.Text, salt))
                            cmd.Parameters.AddWithValue("@salt", salt)
                        End If

                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        hayCambios = False
                        MessageBox.Show("Registros actualizados: " & Resultado)
                    End Using
                End Using

                'si modifico mi propio usuario, actualizo los datos de la sesion
                If idUsuario = Sesion.IdUsuario Then
                    Sesion.NombreUsuario = txtUsuario.Text.Trim
                    Sesion.NombreCompleto = txtNombreCompleto.Text.Trim
                End If

            Catch ex As MySqlException When ex.Number = 1062
                MessageBox.Show("Ya existe un usuario con ese nombre de usuario. Puede corresponder a un usuario dado de baja.")
                txtUsuario.Focus()
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
        CargarUsuarios()
        RefrescarRegistro()
    End Sub

    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        'doy de baja al usuario que se esta viendo, o lo reactivo si ya estaba dado de baja
        Dim idRegistro As Integer = idUsuario

        'el usuario logueado no puede darse de baja a si mismo
        If idRegistro = Sesion.IdUsuario Then
            MessageBox.Show("No puede dar de baja su propio usuario.")
            Exit Sub
        End If

        'la baja y la reactivacion no guardan los campos: si hay cambios lo aviso en la misma pregunta
        Dim avisoCambios As String = ""
        If hayCambios Then
            avisoCambios = vbCrLf & vbCrLf & "Los cambios sin guardar de la tarjeta se van a perder."
        End If

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If registroActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja al usuario " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar al usuario " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        'si no confirma no se hace nada, y lo que estaba escribiendo queda como estaba
        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'estado del usuario leido en la base dentro de la transaccion
        Dim activoEnBase As Boolean = False
        'queda en True cuando lo que hay en pantalla ya no coincide con la base
        Dim desactualizado As Boolean = False

        'baja logica: el usuario puede tener ordenes de trabajo asociadas
        'solo lo marco como inactivo y ya no puede iniciar sesion; con el mismo boton se lo puede reactivar
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el usuario y bloqueo solo su fila hasta terminar
                    Dim existe As Boolean = False
                    Dim rolEnBase As String = ""
                    Dim idMecanicoEnBase As Integer = 0
                    Dim consulta As String = "SELECT activo, rol, id_mecanico FROM usuario WHERE id_usuario = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idRegistro)
                        Using lector As MySqlDataReader = cmd.ExecuteReader
                            If lector.Read() Then
                                existe = True
                                activoEnBase = Convert.ToBoolean(lector("activo"))
                                rolEnBase = lector("rol").ToString()
                                If Not IsDBNull(lector("id_mecanico")) Then idMecanicoEnBase = CInt(lector("id_mecanico"))
                            End If
                        End Using
                    End Using

                    If Not existe Then
                        aviso = "El usuario seleccionado ya no existe."
                        desactualizado = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activoEnBase <> registroActivo Then
                        aviso = "El estado del usuario fue cambiado desde otro puesto. Se actualizó la lista: revíselo y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso activoEnBase AndAlso rolEnBase = "ADMINISTRADOR" Then
                        'el sistema no puede quedar sin ningun administrador activo
                        'puede pasar si dos administradores se dan de baja uno al otro desde dos puestos
                        'aca si bloqueo a los demas administradores, para que las dos bajas no pasen a la vez
                        Dim otrosAdministradores As Integer = 0
                        consulta = "SELECT id_usuario FROM usuario " &
                                   "WHERE rol = 'ADMINISTRADOR' AND activo = 1 AND id_usuario <> @id FOR UPDATE;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
                            Using lector As MySqlDataReader = cmd.ExecuteReader
                                While lector.Read()
                                    otrosAdministradores = otrosAdministradores + 1
                                End While
                            End Using
                        End Using

                        If otrosAdministradores = 0 Then
                            aviso = "No se puede dar de baja: es el único administrador activo y el sistema quedaría sin administrador."
                        End If
                    End If

                    If aviso = "" AndAlso Not activoEnBase AndAlso rolEnBase = "MECANICO" Then
                        'no se reactiva un usuario mecanico cuyo mecanico asociado esta dado de baja
                        consulta = "SELECT nombre_completo, activo FROM mecanico WHERE id_mecanico = @id_mecanico;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_mecanico", idMecanicoEnBase)
                            Using lector As MySqlDataReader = cmd.ExecuteReader
                                If lector.Read() AndAlso Not Convert.ToBoolean(lector("activo")) Then
                                    aviso = "No se puede reactivar: su mecánico asociado, " & lector("nombre_completo").ToString() &
                                            ", está dado de baja. Reactive primero al mecánico en ""Mecánicos""."
                                End If
                            End Using
                        End Using
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE usuario SET activo = @activo WHERE id_usuario = @id;"
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
            MessageBox.Show("Error al dar de baja " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
        ElseIf activoEnBase Then
            MessageBox.Show("Usuarios dados de baja: 1")
        Else
            MessageBox.Show("Usuarios reactivados: 1")
        End If

        'los cambios sin guardar se pierden recien ahora: cuando el estado cambio, o cuando la tarjeta
        'mostraba un estado que ya no es el de la base y hay que volver a leerla
        If aviso = "" OrElse desactualizado Then hayCambios = False

        'recargo la lista: muestra el estado real, y el registro sigue marcado si todavia esta listado
        CargarUsuarios()
        'si la baja se rechazo y habia cambios sin guardar, la tarjeta queda como estaba
        If Not hayCambios Then RefrescarRegistro()
    End Sub

End Class

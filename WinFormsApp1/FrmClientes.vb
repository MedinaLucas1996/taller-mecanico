Imports MySqlConnector

Public Class FrmClientes

    'cliente cargado en la tarjeta del registro, queda en 0 mientras no haya uno (el ID no se muestra)
    Private idCliente As Integer = 0
    'indica que la tarjeta esta cargando un cliente nuevo
    Private creando As Boolean = False
    'indica si el cliente cargado esta activo, decide si el boton da de baja o reactiva
    Private registroActivo As Boolean = True

    'queda en True cuando el usuario cambio algun campo y todavia no guardo
    Private hayCambios As Boolean = False
    'queda en True mientras el programa carga la grilla o los campos, para no tomarlo como un cambio del usuario
    Private cargando As Boolean = False

    'iconos de los botones que cambian segun el registro
    Private iconoBaja As Image
    Private iconoReactivar As Image

    Sub CargarClientes()
        'cargo la grilla con el filtro escrito y dejo marcado al cliente que se estaba viendo
        Dim filtro As String = txtFiltro.Text.Trim

        cargando = True

        Try
            'conecto a la bd para cargar la grilla
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql
                Dim consulta As String =
                    "SELECT id_cliente, razon_social, documento, telefono, localidad, " &
                    "domicilio, email, observaciones, activo, fecha_alta " &
                    "FROM cliente " &
                    "WHERE 1 = 1 "

                'solo traigo clientes activos, salvo que se pida ver tambien los dados de baja
                If Not chkBajas.Checked Then
                    consulta = consulta & "AND activo = 1 "
                End If

                'aplico filtro por nombre o documento
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (razon_social LIKE @filtro OR documento LIKE @filtro) "
                End If

                'primero los activos, despues los dados de baja
                consulta = consulta & "ORDER BY activo DESC, razon_social;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvClientes.DataSource = tabla

                    'en la lista se ven cuatro columnas, el resto de los datos se ve en la tarjeta del registro
                    dgvClientes.Columns("id_cliente").Visible = False
                    dgvClientes.Columns("domicilio").Visible = False
                    dgvClientes.Columns("email").Visible = False
                    dgvClientes.Columns("observaciones").Visible = False
                    dgvClientes.Columns("activo").Visible = False
                    dgvClientes.Columns("fecha_alta").Visible = False

                    'pongo titulos legibles en las columnas, con un ancho minimo para cada una
                    dgvClientes.Columns("razon_social").HeaderText = "Nombre"
                    dgvClientes.Columns("razon_social").MinimumWidth = 150
                    dgvClientes.Columns("documento").HeaderText = "Documento"
                    dgvClientes.Columns("documento").MinimumWidth = 95
                    dgvClientes.Columns("telefono").HeaderText = "Teléfono"
                    dgvClientes.Columns("telefono").MinimumWidth = 95
                    dgvClientes.Columns("localidad").HeaderText = "Localidad"
                    dgvClientes.Columns("localidad").MinimumWidth = 95

                    'reparto el ancho recien despues de los minimos: un minimo que agranda una columna le cambia
                    'a la grilla el reparto de las otras, y la lista se movia al recargarla
                    dgvClientes.Columns("razon_social").FillWeight = 40
                    dgvClientes.Columns("documento").FillWeight = 20
                    dgvClientes.Columns("telefono").FillWeight = 20
                    dgvClientes.Columns("localidad").FillWeight = 20

                    'el orden por columna lo hace el formulario al hacer click en el titulo (ver ColumnHeaderMouseClick)
                    dgvClientes.Columns("razon_social").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvClientes.Columns("documento").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvClientes.Columns("telefono").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvClientes.Columns("localidad").SortMode = DataGridViewColumnSortMode.Programmatic

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
                        lblSubtitulo.Text = "1 cliente activo"
                    Else
                        lblSubtitulo.Text = activos & " clientes activos"
                    End If
                End Using
            End Using

            'en la lista nueva dejo marcado al cliente que se estaba viendo
            Dim filaActual As DataGridViewRow = MarcarFila(idCliente)
            If filaActual Is Nothing AndAlso idCliente <> 0 AndAlso Not hayCambios Then
                'el cliente ya no esta en la lista (por el filtro o por una baja): la tarjeta vuelve a la ayuda
                MostrarAyuda()
            End If
        Catch ex As Exception
            'muestro mensaje de error
            MessageBox.Show("Error al cargar los clientes: " & ex.Message)
        Finally
            'pase lo que pase, la grilla vuelve a atender al usuario
            cargando = False
        End Try
    End Sub

    Function MarcarFila(id As Integer) As DataGridViewRow
        'dejo marcada en la lista la fila del cliente con esa clave: celda actual y seleccion juntas
        'con clave 0, o si el cliente no esta listado, no queda nada marcado; devuelve la fila o Nothing
        Dim encontrada As DataGridViewRow = Nothing
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If id <> 0 AndAlso dgvClientes.Columns.Contains("id_cliente") Then
                For Each fila As DataGridViewRow In dgvClientes.Rows
                    If CInt(fila.Cells("id_cliente").Value) = id Then encontrada = fila
                Next
            End If

            dgvClientes.ClearSelection()
            If encontrada Is Nothing Then
                dgvClientes.CurrentCell = Nothing
            Else
                dgvClientes.CurrentCell = encontrada.Cells("razon_social")
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    Sub RefrescarRegistro()
        'vuelvo a mostrar en la tarjeta los datos guardados del cliente que se esta viendo
        Dim fila As DataGridViewRow = MarcarFila(idCliente)
        If fila IsNot Nothing Then MostrarRegistro(fila)
    End Sub

    Sub MostrarAyuda()
        'sin cliente elegido ni alta en curso, la tarjeta muestra solo la ayuda
        idCliente = 0
        creando = False
        hayCambios = False
        pnlRegistro.Visible = False
        lblAyuda.Visible = True

        'la ficha se imprime solo de un cliente guardado
        btnImprimirFicha.Enabled = False
    End Sub

    Sub CargarCampos(fila As DataGridViewRow)
        'paso a los campos los datos de la fila; con fila en Nothing quedan vacios para un alta
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If fila Is Nothing Then
                txtRazonSocial.Clear()
                txtDocumento.Clear()
                txtTelefono.Clear()
                txtDomicilio.Clear()
                txtLocalidad.Clear()
                txtEmail.Clear()
                txtObservaciones.Clear()
            Else
                txtRazonSocial.Text = fila.Cells("razon_social").Value.ToString()
                txtDocumento.Text = fila.Cells("documento").Value.ToString()
                txtTelefono.Text = fila.Cells("telefono").Value.ToString()
                txtDomicilio.Text = fila.Cells("domicilio").Value.ToString()
                txtLocalidad.Text = fila.Cells("localidad").Value.ToString()
                txtEmail.Text = fila.Cells("email").Value.ToString()
                txtObservaciones.Text = fila.Cells("observaciones").Value.ToString()
            End If

            'lo que se ve es lo que esta guardado
            hayCambios = False
        Finally
            cargando = estabaCargando
        End Try
    End Sub

    Sub MostrarRegistro(fila As DataGridViewRow)
        'cargo en la tarjeta al cliente de la fila, para verlo y modificarlo
        idCliente = CInt(fila.Cells("id_cliente").Value)
        creando = False
        registroActivo = Convert.ToBoolean(fila.Cells("activo").Value)

        CargarCampos(fila)
        lblRegistroTitulo.Text = fila.Cells("razon_social").Value.ToString()

        'etiqueta de estado y boton de la izquierda segun el cliente este activo o dado de baja
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

        'linea de contexto: cuantos vehiculos activos tiene y desde cuando es cliente
        Dim vehiculos As Integer = 0
        Dim desde As String = CDate(fila.Cells("fecha_alta").Value).ToString("dd/MM/yyyy")
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "SELECT COUNT(*) FROM vehiculo WHERE id_cliente = @id AND activo = 1;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", idCliente)
                    vehiculos = CInt(cmd.ExecuteScalar())
                End Using
            End Using

            Dim textoVehiculos As String = vehiculos & " vehículos"
            If vehiculos = 1 Then textoVehiculos = "1 vehículo"
            lblContexto.Text = textoVehiculos & " · cliente desde " & desde
        Catch ex As Exception
            'si no se pudo contar, muestro solo la fecha: el resto de la tarjeta sirve igual
            lblContexto.Text = "Cliente desde " & desde
        End Try
        lblContexto.Visible = True

        lblAyuda.Visible = False
        pnlRegistro.Visible = True

        'un cliente guardado ya tiene su ficha para imprimir
        btnImprimirFicha.Enabled = True
    End Sub

    Sub NuevoRegistro()
        'dejo la tarjeta lista para cargar un cliente nuevo
        idCliente = 0
        creando = True
        registroActivo = True

        CargarCampos(Nothing)
        lblRegistroTitulo.Text = "Nuevo cliente"

        'un cliente que todavia no se guardo no tiene ficha
        btnImprimirFicha.Enabled = False

        'un cliente nuevo no tiene estado ni contexto, y sus botones son Cancelar y Guardar
        lblEstadoRegistro.Visible = False
        lblContexto.Visible = False
        btnBaja.Visible = False
        btnCancelar.Visible = True
        btnGuardar.Text = " Guardar"

        lblAyuda.Visible = False
        pnlRegistro.Visible = True

        'durante un alta no queda ninguna fila marcada en la lista
        MarcarFila(0)

        txtRazonSocial.Focus()
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
        Dim idElegido As Integer = CInt(fila.Cells("id_cliente").Value)

        'si ya se esta viendo ese cliente no hay nada que hacer
        If idElegido = idCliente AndAlso Not creando Then Exit Sub

        If Not DescartarCambios() Then
            'el usuario prefirio seguir con lo que estaba cargando: la lista vuelve a marcar ese registro
            '(en un alta no hay registro, y no queda nada marcado)
            MarcarFila(idCliente)
            Exit Sub
        End If

        MostrarRegistro(fila)
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If txtRazonSocial.Text.Trim = "" Then
            MessageBox.Show("Falta el nombre o razón social")
            txtRazonSocial.Focus()
            Return False
        End If

        If txtDocumento.Text.Trim = "" Then
            MessageBox.Show("Falta el documento")
            txtDocumento.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnNuevo.Image = LeerIcono("agregar.png")
        btnGuardar.Image = LeerIcono("guardar.png")
        btnCancelar.Image = LeerIcono("cancelar-oscuro.png")
        picBuscar.Image = LeerIcono("buscar-oscuro.png")
        iconoBaja = LeerIcono("baja-rojo.png")
        iconoReactivar = LeerIcono("reactivar-oscuro.png")

        'el encabezado de la lista lleva solo una linea clara debajo, como las filas
        dgvClientes.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'la tarjeta arranca con la ayuda; la grilla se carga al mostrarse la pantalla (ver Shown)
        MostrarAyuda()

        'solo el administrador y el operador gestionan clientes (el menu ya oculta el boton)
        If Sesion.Rol <> ROL_ADMINISTRADOR AndAlso Sesion.Rol <> ROL_OPERADOR Then
            MessageBox.Show("Solo el administrador y el operador pueden gestionar clientes.")
            btnNuevo.Enabled = False
            btnImprimirFicha.Enabled = False
            pnlLista.Enabled = False
            pnlFicha.Enabled = False
        End If
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        'vuelvo a cargar la grilla con el filtro escrito; la tarjeta no se toca, solo se vuelve a marcar su fila
        If cargando Then Exit Sub
        CargarClientes()
    End Sub

    Private Sub FrmClientes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'al salir de la pantalla pregunto antes de perder cambios sin guardar
        If Not DescartarCambios() Then
            e.Cancel = True
            Exit Sub
        End If

        'ya se acepto perderlos: si el cierre sigue, no se vuelve a preguntar
        hayCambios = False
    End Sub

    Private Sub chkBajas_CheckedChanged(sender As Object, e As EventArgs) Handles chkBajas.CheckedChanged
        'muestro o dejo de mostrar los clientes dados de baja
        CargarClientes()
    End Sub

    Private Sub dgvClientes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvClientes.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas
        If Not dgvClientes.Columns.Contains("activo") Then Exit Sub

        For Each fila As DataGridViewRow In dgvClientes.Rows
            'los clientes dados de baja van en gris, tambien cuando la fila esta seleccionada
            If Not Convert.ToBoolean(fila.Cells("activo").Value) Then
                fila.DefaultCellStyle.ForeColor = Color.Gray
                fila.DefaultCellStyle.SelectionForeColor = Color.Gainsboro
            End If
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcado solo al cliente que se esta viendo
        MarcarFila(idCliente)
    End Sub

    Private Sub dgvClientes_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvClientes.ColumnHeaderMouseClick
        'ordeno la lista por la columna del titulo que se toco; otro click en la misma columna invierte el orden
        'ordenar nunca cambia el registro abierto ni pregunta nada
        If e.Button <> MouseButtons.Left Then Exit Sub
        If cargando Then Exit Sub

        Dim columna As DataGridViewColumn = dgvClientes.Columns(e.ColumnIndex)
        Dim sentido As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        If dgvClientes.SortedColumn Is columna AndAlso dgvClientes.SortOrder = SortOrder.Ascending Then
            sentido = System.ComponentModel.ListSortDirection.Descending
        End If

        'mientras se ordena la grilla mueve sola su seleccion: eso no es una eleccion del usuario
        cargando = True
        Try
            dgvClientes.Sort(columna, sentido)

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

    Private Sub dgvClientes_Sorted(sender As Object, e As EventArgs) Handles dgvClientes.Sorted
        'las filas cambiaron de lugar: vuelvo a marcar al cliente que se esta viendo
        MarcarFila(idCliente)
    End Sub

    Private Sub FrmClientes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'sin permiso la lista quedo deshabilitada en Load y no se carga
        If Not pnlLista.Enabled Then Exit Sub

        'cargo la grilla de clientes recien ahora, cuando la pantalla ya tiene su tamaño final dentro de la ventana:
        'cargada en Load se repartia el ancho con la grilla mas angosta y las columnas se movian en la primera recarga
        CargarClientes()
    End Sub

    Private Sub dgvClientes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvClientes.SelectionChanged
        'cargo en la tarjeta al cliente que el usuario elige con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargando Then Exit Sub
        If Not dgvClientes.Focused Then Exit Sub
        If dgvClientes.SelectedRows.Count = 0 Then Exit Sub

        ElegirFila(dgvClientes.SelectedRows(0))
    End Sub

    Private Sub dgvClientes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientes.CellClick
        'un click sobre una fila siempre la carga, aunque la grilla ya la tuviera marcada
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirFila(dgvClientes.Rows(e.RowIndex))
    End Sub

    Private Sub Campo_TextChanged(sender As Object, e As EventArgs) Handles _
        txtRazonSocial.TextChanged, txtDocumento.TextChanged, txtTelefono.TextChanged, txtDomicilio.TextChanged,
        txtLocalidad.TextChanged, txtEmail.TextChanged, txtObservaciones.TextChanged

        'el usuario cambio un campo: hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        'empiezo la carga de un cliente nuevo
        If Not DescartarCambios() Then Exit Sub
        NuevoRegistro()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        'dejo de cargar el cliente nuevo, la tarjeta vuelve a la ayuda
        If Not DescartarCambios() Then Exit Sub
        MostrarAyuda()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un cliente nuevo, o los cambios del cliente que se esta viendo
        If Not ValidarCampos() Then Exit Sub

        'recuerdo si era un alta, porque al guardar deja de serlo
        Dim eraAlta As Boolean = creando

        If creando Then
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    'armo mi consulta sql
                    Dim consulta As String =
                        "INSERT INTO cliente (razon_social, documento, domicilio, localidad, telefono, email, observaciones) " &
                        "VALUES (@razon_social, @documento, @domicilio, @localidad, @telefono, @email, @observaciones);"

                    Using cmd As New MySqlCommand(consulta, cn)
                        'cargo valores en los parametros
                        cmd.Parameters.AddWithValue("@razon_social", txtRazonSocial.Text.Trim)
                        cmd.Parameters.AddWithValue("@documento", txtDocumento.Text.Trim)
                        cmd.Parameters.AddWithValue("@domicilio", txtDomicilio.Text.Trim)
                        cmd.Parameters.AddWithValue("@localidad", txtLocalidad.Text.Trim)
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text.Trim)
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim)
                        cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text.Trim)

                        'ejecuto la consulta y obtengo los registros afectados
                        Dim Resultado As Integer = cmd.ExecuteNonQuery()

                        'el cliente nuevo queda como el que se esta viendo, con la clave que le dio la base
                        idCliente = CInt(cmd.LastInsertedId)
                        creando = False
                        hayCambios = False

                        MessageBox.Show("Registros agregados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: el documento ya existe (restriccion unica)
                MessageBox.Show("Ya existe un cliente con ese documento.")
                txtDocumento.Focus()
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
                        "UPDATE cliente SET razon_social=@razon_social, documento=@documento, " &
                        "domicilio=@domicilio, localidad=@localidad, telefono=@telefono, " &
                        "email=@email, observaciones=@observaciones " &
                        "WHERE id_cliente=@id;"

                    Using cmd As New MySqlCommand(consulta, cn)
                        'uso parametros para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@razon_social", txtRazonSocial.Text.Trim)
                        cmd.Parameters.AddWithValue("@documento", txtDocumento.Text.Trim)
                        cmd.Parameters.AddWithValue("@domicilio", txtDomicilio.Text.Trim)
                        cmd.Parameters.AddWithValue("@localidad", txtLocalidad.Text.Trim)
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text.Trim)
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim)
                        cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text.Trim)
                        cmd.Parameters.AddWithValue("@id", idCliente)

                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        hayCambios = False
                        MessageBox.Show("Registros actualizados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                MessageBox.Show("Ya existe otro cliente con ese documento.")
                txtDocumento.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al modificar " & ex.Message)
                Exit Sub
            End Try
        End If

        'si se creo un cliente, limpio la busqueda para que quede listado aunque no coincida con lo escrito
        If eraAlta AndAlso txtFiltro.Text <> "" Then
            cargando = True
            Try
                txtFiltro.Clear()
            Finally
                cargando = False
            End Try
        End If

        'recargo la lista: el cliente guardado queda marcado y la tarjeta muestra lo que quedo en la base
        CargarClientes()
        RefrescarRegistro()
    End Sub

    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        'doy de baja al cliente que se esta viendo, o lo reactivo si ya estaba dado de baja

        Dim idRegistro As Integer = idCliente

        'la baja y la reactivacion no guardan los campos: si hay cambios lo aviso en la misma pregunta
        Dim avisoCambios As String = ""
        If hayCambios Then
            avisoCambios = vbCrLf & vbCrLf & "Los cambios sin guardar de la tarjeta se van a perder."
        End If

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If registroActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja al cliente " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar al cliente " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        'si no confirma no se hace nada, y lo que estaba escribiendo queda como estaba
        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'queda en True cuando el estado que se ve en la tarjeta ya no es el de la base
        Dim estadoViejo As Boolean = False
        'estado del cliente leido en la base dentro de la transaccion
        Dim activoEnBase As Boolean = False

        'baja logica: no borro el registro porque puede tener ordenes de trabajo
        'solo lo marco como inactivo (regla 8.7 de la especificacion), y con el mismo boton se lo puede reactivar
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el cliente y bloqueo solo su fila hasta terminar
                    Dim existe As Boolean = False
                    Dim consulta As String = "SELECT activo FROM cliente WHERE id_cliente = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idRegistro)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado IsNot Nothing Then
                            existe = True
                            activoEnBase = Convert.ToBoolean(resultado)
                        End If
                    End Using

                    If Not existe Then
                        aviso = "El cliente seleccionado ya no existe."
                        estadoViejo = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activoEnBase <> registroActivo Then
                        aviso = "El estado del cliente fue cambiado desde otro puesto. Se actualizó la lista: revíselo y vuelva a intentar."
                        estadoViejo = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso activoEnBase Then
                        'no se da de baja a un cliente que tiene vehiculos activos
                        consulta = "SELECT COUNT(*) FROM vehiculo WHERE id_cliente = @id AND activo = 1;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
                            Dim vehiculosActivos As Integer = CInt(cmd.ExecuteScalar())
                            If vehiculosActivos > 0 Then
                                aviso = "No se puede dar de baja: el cliente tiene " & vehiculosActivos &
                                        " vehículo(s) activo(s). Cámbieles el titular o delos de baja primero."
                            End If
                        End Using
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE cliente SET activo = @activo WHERE id_cliente = @id;"
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
            MessageBox.Show("Clientes dados de baja: 1")
        Else
            MessageBox.Show("Clientes reactivados: 1")
        End If

        'los cambios sin guardar se pierden recien ahora: cuando el estado cambio, o cuando la tarjeta
        'mostraba un estado que ya no es el de la base y hay que volver a leerla
        If aviso = "" OrElse estadoViejo Then hayCambios = False

        'recargo la lista: muestra el estado real, y el cliente sigue marcado si todavia esta listado
        CargarClientes()
        'si la baja se rechazo y habia cambios sin guardar, la tarjeta queda como estaba
        If Not hayCambios Then RefrescarRegistro()
    End Sub

    '----- ficha del cliente impresa -----

    'datos de la ficha: se leen al pedir la impresion y el evento PrintPage solo dibuja
    Private clienteFicha As DataRow
    Private vehiculosFicha As DataTable
    Private ordenesFicha As DataTable
    Private fechaFicha As DateTime

    'bloques de la ficha en el orden en que se imprimen: un numero >= 0 es la fila del vehiculo en vehiculosFicha
    Const PASO_CLIENTE As Integer = -1
    Const PASO_PROPIOS As Integer = -2
    Const PASO_VENDIDOS As Integer = -3
    Const PASO_RESUMEN As Integer = -4
    Private pasosFicha As New List(Of Integer)

    'por donde va la impresion: hoja, bloque y fila de la tabla; se reinician en BeginPrint
    Private hojaFicha As Integer = 0
    Private pasoFicha As Integer = 0
    Private filaFicha As Integer = 0
    Private errorFicha As Boolean = False

    Private Sub btnImprimirFicha_Click(sender As Object, e As EventArgs) Handles btnImprimirFicha.Click
        'imprimo la ficha del cliente guardado que se esta viendo
        If idCliente = 0 OrElse creando Then Exit Sub

        'la ficha sale de lo guardado en la base: aviso si hay cambios que no van a aparecer
        If hayCambios Then
            Dim respuesta As DialogResult = MessageBox.Show(
                "Hay cambios sin guardar y no van a salir en la ficha, que muestra solo los datos guardados." & vbCrLf &
                "¿Imprimir igual?",
                "Cambios sin guardar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
            If respuesta = DialogResult.No Then Exit Sub
        End If

        If Not CargarDatosFicha() Then Exit Sub

        'abro la vista previa maximizada; desde su boton de impresora se imprime
        'esta pantalla va dentro de la ventana principal: la vista previa se abre sobre esa ventana
        Try
            prdFicha.DefaultPageSettings.Margins = New Printing.Margins(MARGEN_HOJA, MARGEN_HOJA, MARGEN_HOJA, MARGEN_HOJA)
            ppdVistaPrevia.Document = prdFicha
            ppdVistaPrevia.WindowState = FormWindowState.Maximized
            ppdVistaPrevia.ShowDialog(Me.TopLevelControl)
        Catch ex As Exception
            MessageBox.Show("No se pudo preparar la impresión: " & ex.Message)
        End Try
    End Sub

    Function CargarDatosFicha() As Boolean
        'leo el cliente, sus vehiculos y sus ordenes; armo la lista de bloques de la ficha
        Dim consultaCliente As String =
            "SELECT razon_social, documento, telefono, email, domicilio, localidad, fecha_alta, activo, observaciones " &
            "FROM cliente WHERE id_cliente = @id_cliente;"

        'vehiculos que hoy son del cliente (tambien los dados de baja) y, despues, los que ya no son suyos
        'pero que trajo al taller: propio = 1 o 0
        Dim consultaVehiculos As String =
            "SELECT v.id_vehiculo, v.patente, CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
            "v.anio, v.color, v.km_actual, v.activo, 1 AS propio " &
            "FROM vehiculo AS v " &
            "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
            "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
            "WHERE v.id_cliente = @id_cliente " &
            "UNION " &
            "SELECT v.id_vehiculo, v.patente, CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
            "v.anio, v.color, v.km_actual, v.activo, 0 AS propio " &
            "FROM vehiculo AS v " &
            "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
            "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
            "WHERE v.id_cliente <> @id_cliente " &
            "AND v.id_vehiculo IN (SELECT ot.id_vehiculo FROM orden_trabajo AS ot WHERE ot.id_cliente = @id_cliente) " &
            "ORDER BY propio DESC, activo DESC, patente;"

        'ordenes del cliente; el total es el aprobado si hay, si no el presupuestado
        Dim consultaOrdenes As String =
            "SELECT ot.id_vehiculo, ot.nro_orden, ot.fecha_recepcion, e.codigo, e.descripcion AS estado, " &
            "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, " &
            "CASE WHEN ot.total_aprobado > 0 THEN ot.total_aprobado ELSE ot.total_presupuestado END AS total " &
            "FROM orden_trabajo AS ot " &
            "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
            "LEFT JOIN mecanico AS m ON m.id_mecanico = ot.id_mecanico " &
            "WHERE ot.id_cliente = @id_cliente " &
            "ORDER BY ot.fecha_recepcion, ot.nro_orden;"

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim cliente As DataTable = LeerTablaFicha(cn, consultaCliente)
                If cliente.Rows.Count = 0 Then
                    MessageBox.Show("No se encontró el cliente para imprimir su ficha.")
                    Return False
                End If
                clienteFicha = cliente.Rows(0)
                vehiculosFicha = LeerTablaFicha(cn, consultaVehiculos)
                ordenesFicha = LeerTablaFicha(cn, consultaOrdenes)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al leer los datos de la ficha: " & ex.Message)
            Return False
        End Try

        ArmarPasosFicha()
        fechaFicha = DateTime.Now
        Return True
    End Function

    Function LeerTablaFicha(cn As MySqlConnection, consulta As String) As DataTable
        'ejecuto una consulta del cliente y devuelvo sus filas
        Using cmd As New MySqlCommand(consulta, cn)
            cmd.Parameters.AddWithValue("@id_cliente", idCliente)
            Dim tabla As New DataTable
            Using lector As MySqlDataReader = cmd.ExecuteReader
                tabla.Load(lector)
            End Using
            Return tabla
        End Using
    End Function

    Sub ArmarPasosFicha()
        'orden de la ficha: cliente, sus vehiculos, los que ya no son suyos (si hay) y el resumen
        pasosFicha.Clear()
        pasosFicha.Add(PASO_CLIENTE)
        pasosFicha.Add(PASO_PROPIOS)
        For i As Integer = 0 To vehiculosFicha.Rows.Count - 1
            If CInt(vehiculosFicha.Rows(i)("propio")) = 1 Then pasosFicha.Add(i)
        Next

        Dim primeraVendida As Boolean = True
        For i As Integer = 0 To vehiculosFicha.Rows.Count - 1
            If CInt(vehiculosFicha.Rows(i)("propio")) = 0 Then
                If primeraVendida Then pasosFicha.Add(PASO_VENDIDOS)
                primeraVendida = False
                pasosFicha.Add(i)
            End If
        Next

        pasosFicha.Add(PASO_RESUMEN)
    End Sub

    Function OrdenesDelVehiculo(fila As DataRow) As DataRow()
        'ordenes del cliente hechas con ese vehiculo
        Return ordenesFicha.Select("id_vehiculo = " & CInt(fila("id_vehiculo")).ToString())
    End Function

    Function TituloVehiculo(fila As DataRow) As String
        'renglon de cabecera de cada vehiculo: patente, modelo, año, color y km
        Dim titulo As String = fila("patente").ToString() & " · " & fila("vehiculo").ToString()
        If Not IsDBNull(fila("anio")) Then titulo = titulo & " · " & fila("anio").ToString()
        If fila("color").ToString() <> "" Then titulo = titulo & " · " & fila("color").ToString()
        titulo = titulo & " · " & CInt(fila("km_actual")).ToString("N0") & " km"
        If CInt(fila("propio")) = 1 AndAlso Not Convert.ToBoolean(fila("activo")) Then titulo = titulo & " (Baja)"
        Return titulo
    End Function

    Private Sub prdFicha_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles prdFicha.BeginPrint
        'la vista previa y la impresion real recorren las hojas cada una desde el principio
        hojaFicha = 0
        pasoFicha = 0
        filaFicha = 0
        errorFicha = False
    End Sub

    Private Sub prdFicha_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles prdFicha.PrintPage
        'un error al dibujar corta la ficha con un aviso, sin cerrar el sistema
        Try
            DibujarHojaFicha(e)
        Catch ex As Exception
            e.HasMorePages = False
            If Not errorFicha Then
                errorFicha = True
                MessageBox.Show("No se pudo dibujar la ficha: " & ex.Message)
            End If
        End Try
    End Sub

    Sub DibujarHojaFicha(e As Printing.PrintPageEventArgs)
        'dibujo los bloques que entran en la hoja; lo que no entra sigue en la siguiente
        Dim g As Graphics = e.Graphics
        Dim area As Rectangle = e.MarginBounds
        Dim x As Integer = area.Left
        Dim ancho As Integer = area.Width
        Dim limite As Integer = area.Bottom - ALTO_PIE
        Dim nombre As String = clienteFicha("razon_social").ToString()

        hojaFicha += 1

        Dim y As Integer = DibujarCabecera(g, x, area.Top, ancho, "Ficha del cliente", nombre)
        Dim inicioHoja As Integer = y

        Do While pasoFicha < pasosFicha.Count
            Dim paso As Integer = pasosFicha(pasoFicha)
            Dim termino As Boolean

            If paso >= 0 AndAlso OrdenesDelVehiculo(vehiculosFicha.Rows(paso)).Length > 0 Then
                'vehiculo con ordenes: tabla que se puede partir entre hojas
                termino = ImprimirOrdenesVehiculo(g, vehiculosFicha.Rows(paso), x, y, ancho, limite, y = inicioHoja)
            Else
                'los demas bloques no se parten: si no entran pasan enteros a la hoja siguiente
                Dim fin As Integer = DibujarBloqueFicha(g, paso, x, y, ancho, False)
                termino = (fin <= limite OrElse y = inicioHoja)
                If termino Then y = DibujarBloqueFicha(g, paso, x, y, ancho, True)
            End If

            If Not termino Then Exit Do
            pasoFicha += 1
            filaFicha = 0
        Loop

        DibujarPie(g, area, "Ficha del cliente · " & nombre & " · Impreso el " & fechaFicha.ToString("dd/MM/yyyy HH:mm"), hojaFicha)
        e.HasMorePages = (pasoFicha < pasosFicha.Count)
    End Sub

    Function DibujarBloqueFicha(g As Graphics, paso As Integer, x As Integer, y As Integer, ancho As Integer,
                                dibujar As Boolean) As Integer
        'dibujo (o solo mido, con dibujar en False) un bloque de la ficha y devuelvo el y donde termina
        Select Case paso
            Case PASO_CLIENTE
                Return DibujarCajaCliente(g, x, y, ancho, dibujar)

            Case PASO_PROPIOS, PASO_VENDIDOS
                'titulo de grupo; reservo lugar para que no quede solo al pie de la hoja
                Dim titulo As String = "Vehículos del cliente"
                Dim nota As String = "Vehículos que hoy están a nombre del cliente, cada uno con sus órdenes de trabajo."
                If paso = PASO_VENDIDOS Then
                    titulo = "Vehículos que ya no son del cliente"
                    nota = "Órdenes que el cliente trajo al taller con vehículos que hoy tienen otro titular."
                ElseIf vehiculosFicha.Select("propio = 1").Length = 0 Then
                    nota = "El cliente no tiene vehículos a su nombre."
                End If

                Dim yFin As Integer = DibujarTituloCaja(g, titulo, x, y, ancho, dibujar)
                yFin = DibujarParrafo(g, nota, x + 8, yFin - 4, ancho - 16, dibujar) + 10
                If Not dibujar Then Return yFin + 90
                Return yFin

            Case PASO_RESUMEN
                Return DibujarResumenFicha(g, x, y, ancho, dibujar)
        End Select

        'vehiculo sin ordenes: su renglon de cabecera y un aviso
        Dim vehiculo As DataRow = vehiculosFicha.Rows(paso)
        y = DibujarTituloSeccion(g, TituloVehiculo(vehiculo), x, y, ancho, dibujar)
        Return DibujarParrafo(g, "Sin órdenes de trabajo registradas.", x, y, ancho, dibujar) + 14
    End Function

    Function DibujarCajaCliente(g As Graphics, x As Integer, y As Integer, ancho As Integer, dibujar As Boolean) As Integer
        'caja con los datos del cliente en dos columnas y las observaciones debajo
        Const ANCHO_ETIQUETA As Integer = 105
        Dim fila As DataRow = clienteFicha
        Dim anchoColumna As Integer = (ancho - 32) \ 2
        Dim xDerecha As Integer = x + 16 + anchoColumna + 16

        Dim domicilio As String = fila("domicilio").ToString()
        If fila("localidad").ToString() <> "" Then
            If domicilio <> "" Then domicilio = domicilio & ", "
            domicilio = domicilio & fila("localidad").ToString()
        End If

        Dim estado As String = "Activo"
        If Not Convert.ToBoolean(fila("activo")) Then estado = "Dado de baja"

        Dim yInicio As Integer = DibujarTituloCaja(g, "Cliente", x, y, ancho, dibujar)

        Dim yIzquierda As Integer = yInicio
        yIzquierda = DibujarDato(g, "Razón social", fila("razon_social").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yIzquierda = DibujarDato(g, "Documento", fila("documento").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yIzquierda = DibujarDato(g, "Teléfono", fila("telefono").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yIzquierda = DibujarDato(g, "Email", fila("email").ToString(), x + 8, yIzquierda, anchoColumna, ANCHO_ETIQUETA, dibujar)

        Dim yDerecha As Integer = yInicio
        yDerecha = DibujarDato(g, "Domicilio", domicilio, xDerecha, yDerecha, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yDerecha = DibujarDato(g, "Cliente desde", TextoFecha(fila("fecha_alta"), "dd/MM/yyyy"), xDerecha, yDerecha, anchoColumna, ANCHO_ETIQUETA, dibujar)
        yDerecha = DibujarDato(g, "Estado", estado, xDerecha, yDerecha, anchoColumna, ANCHO_ETIQUETA, dibujar)

        Dim yFin As Integer = Math.Max(yIzquierda, yDerecha)
        yFin = DibujarDato(g, "Observaciones", fila("observaciones").ToString(), x + 8, yFin + 2, ancho - 16, ANCHO_ETIQUETA, dibujar) + 4

        If dibujar Then DibujarBordeCaja(g, x, y, ancho, yFin)
        Return yFin + 18
    End Function

    Function ImprimirOrdenesVehiculo(g As Graphics, vehiculo As DataRow, x As Integer, ByRef y As Integer, ancho As Integer,
                                     limite As Integer, inicioHoja As Boolean) As Boolean
        'tabla con las ordenes de un vehiculo, con su renglon de cabecera como titulo
        Dim columnas() As String = {"N.º", "Ingreso", "Estado", "Mecánico", "Total"}
        Dim pesos() As Single = {0.9F, 1.6F, 1.8F, 2.6F, 1.7F}
        Dim alineaciones() As StringAlignment = {StringAlignment.Near, StringAlignment.Near, StringAlignment.Near,
                                                 StringAlignment.Near, StringAlignment.Far}

        Dim filas As New List(Of String())
        For Each orden As DataRow In OrdenesDelVehiculo(vehiculo)
            filas.Add({orden("nro_orden").ToString(),
                       CDate(orden("fecha_recepcion")).ToString("dd/MM/yyyy"),
                       orden("estado").ToString(),
                       orden("mecanico").ToString(),
                       CDec(orden("total")).ToString("C2")})
        Next

        Return DibujarTabla(g, TituloVehiculo(vehiculo), columnas, pesos, alineaciones, filas,
                            x, y, ancho, limite, filaFicha, inicioHoja)
    End Function

    Function DibujarResumenFicha(g As Graphics, x As Integer, y As Integer, ancho As Integer, dibujar As Boolean) As Integer
        'totales de la ficha: vehiculos a nombre del cliente, ordenes y lo facturado en ordenes entregadas
        Const ANCHO_ETIQUETA As Integer = 300
        Dim facturado As Decimal = 0
        For Each orden As DataRow In ordenesFicha.Rows
            If orden("codigo").ToString() = "ENTREGADA" Then facturado += CDec(orden("total"))
        Next

        y = DibujarTituloSeccion(g, "Resumen", x, y, ancho, dibujar)
        y = DibujarDato(g, "Vehículos a nombre del cliente", vehiculosFicha.Select("propio = 1").Length.ToString(), x, y, ancho, ANCHO_ETIQUETA, dibujar)
        y = DibujarDato(g, "Órdenes de trabajo (todas)", ordenesFicha.Rows.Count.ToString(), x, y, ancho, ANCHO_ETIQUETA, dibujar)
        y = DibujarDato(g, "Total facturado (solo órdenes entregadas)", facturado.ToString("C2"), x, y, ancho, ANCHO_ETIQUETA, dibujar)
        Return y + 12
    End Function

    Private Sub FrmClientes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'la vista previa y el documento de impresion no estan en components: los libero al cerrar
        ppdVistaPrevia.Dispose()
        prdFicha.Dispose()
    End Sub

End Class

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

                    'pongo titulos legibles en las columnas y reparto el ancho, con un minimo para cada una
                    dgvClientes.Columns("razon_social").HeaderText = "Nombre"
                    dgvClientes.Columns("razon_social").FillWeight = 40
                    dgvClientes.Columns("razon_social").MinimumWidth = 150
                    dgvClientes.Columns("documento").HeaderText = "Documento"
                    dgvClientes.Columns("documento").FillWeight = 20
                    dgvClientes.Columns("documento").MinimumWidth = 95
                    dgvClientes.Columns("telefono").HeaderText = "Teléfono"
                    dgvClientes.Columns("telefono").FillWeight = 20
                    dgvClientes.Columns("telefono").MinimumWidth = 95
                    dgvClientes.Columns("localidad").HeaderText = "Localidad"
                    dgvClientes.Columns("localidad").FillWeight = 20
                    dgvClientes.Columns("localidad").MinimumWidth = 95

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
    End Sub

    Sub NuevoRegistro()
        'dejo la tarjeta lista para cargar un cliente nuevo
        idCliente = 0
        creando = True
        registroActivo = True

        CargarCampos(Nothing)
        lblRegistroTitulo.Text = "Nuevo cliente"

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

        'cargo la grilla de clientes al abrir el formulario, la tarjeta arranca con la ayuda
        MostrarAyuda()
        CargarClientes()
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
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(idCliente)
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

End Class

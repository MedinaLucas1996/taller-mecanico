Imports MySqlConnector

Public Class FrmVehiculos

    'vehiculo cargado en la tarjeta del registro, queda en 0 mientras no haya uno (el ID no se muestra)
    Private idVehiculo As Integer = 0
    'indica que la tarjeta esta cargando un vehiculo nuevo
    Private creando As Boolean = False
    'indica si el vehiculo cargado esta activo, decide si el boton da de baja o reactiva
    Private registroActivo As Boolean = True

    'queda en True cuando el usuario cambio algun campo y todavia no guardo
    Private hayCambios As Boolean = False
    'queda en True mientras el programa carga la grilla o los campos, para no tomarlo como un cambio del usuario
    Private cargando As Boolean = False

    'iconos de los botones que cambian segun el registro
    Private iconoBaja As Image
    Private iconoReactivar As Image

    Sub CargarVehiculos()
        'cargo la grilla con el filtro escrito y dejo marcado al vehiculo que se estaba viendo
        Dim filtro As String = txtFiltro.Text.Trim

        cargando = True

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'armo mi consulta sql uniendo vehiculo con cliente, modelo y marca
                Dim consulta As String =
                    "SELECT v.id_vehiculo, v.patente, CONCAT(ma.descripcion, ' ', mo.descripcion) AS marca_modelo, " &
                    "c.razon_social AS titular, v.anio, v.color, " &
                    "v.nro_motor, v.nro_chasis, v.km_actual, v.observaciones, " &
                    "v.id_cliente, mo.id_marca, v.id_modelo, v.activo " &
                    "FROM vehiculo AS v " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "WHERE 1 = 1 "

                'solo traigo vehiculos activos, salvo que se pida ver tambien los dados de baja
                If Not chkBajas.Checked Then
                    consulta = consulta & "AND v.activo = 1 "
                End If

                'aplico filtro por patente, titular, marca o modelo
                If filtro <> "" Then
                    consulta = consulta &
                        "AND (v.patente LIKE @filtro OR c.razon_social LIKE @filtro " &
                        "OR ma.descripcion LIKE @filtro OR mo.descripcion LIKE @filtro) "
                End If

                'primero los activos, despues los dados de baja
                consulta = consulta & "ORDER BY v.activo DESC, v.patente;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    'uso datatable para guardar el select
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'cargo la tabla en la grilla
                    dgvVehiculos.DataSource = tabla

                    'en la lista se ven cuatro columnas, el resto de los datos se ve en la tarjeta del registro
                    dgvVehiculos.Columns("id_vehiculo").Visible = False
                    dgvVehiculos.Columns("color").Visible = False
                    dgvVehiculos.Columns("nro_motor").Visible = False
                    dgvVehiculos.Columns("nro_chasis").Visible = False
                    dgvVehiculos.Columns("km_actual").Visible = False
                    dgvVehiculos.Columns("observaciones").Visible = False
                    dgvVehiculos.Columns("id_cliente").Visible = False
                    dgvVehiculos.Columns("id_marca").Visible = False
                    dgvVehiculos.Columns("id_modelo").Visible = False
                    dgvVehiculos.Columns("activo").Visible = False

                    'pongo titulos legibles en las columnas y reparto el ancho, con un minimo para cada una
                    dgvVehiculos.Columns("patente").HeaderText = "Patente"
                    dgvVehiculos.Columns("patente").FillWeight = 18
                    dgvVehiculos.Columns("patente").MinimumWidth = 85
                    dgvVehiculos.Columns("marca_modelo").HeaderText = "Marca y modelo"
                    dgvVehiculos.Columns("marca_modelo").FillWeight = 34
                    dgvVehiculos.Columns("marca_modelo").MinimumWidth = 130
                    dgvVehiculos.Columns("titular").HeaderText = "Titular"
                    dgvVehiculos.Columns("titular").FillWeight = 36
                    dgvVehiculos.Columns("titular").MinimumWidth = 130
                    dgvVehiculos.Columns("anio").HeaderText = "Año"
                    dgvVehiculos.Columns("anio").FillWeight = 12
                    dgvVehiculos.Columns("anio").MinimumWidth = 55

                    'el orden por columna lo hace el formulario al hacer click en el titulo (ver ColumnHeaderMouseClick)
                    dgvVehiculos.Columns("patente").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvVehiculos.Columns("marca_modelo").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvVehiculos.Columns("titular").SortMode = DataGridViewColumnSortMode.Programmatic
                    dgvVehiculos.Columns("anio").SortMode = DataGridViewColumnSortMode.Programmatic

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
                        lblSubtitulo.Text = "1 vehículo activo"
                    Else
                        lblSubtitulo.Text = activos & " vehículos activos"
                    End If
                End Using
            End Using

            'en la lista nueva dejo marcado al vehiculo que se estaba viendo
            Dim filaActual As DataGridViewRow = MarcarFila(idVehiculo)
            If filaActual Is Nothing AndAlso idVehiculo <> 0 AndAlso Not hayCambios Then
                'el vehiculo ya no esta en la lista (por el filtro o por una baja): la tarjeta vuelve a la ayuda
                MostrarAyuda()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar los vehículos: " & ex.Message)
        Finally
            'pase lo que pase, la grilla vuelve a atender al usuario
            cargando = False
        End Try
    End Sub

    Sub CargarComboTitulares(idPropio As Integer)
        'cargo los clientes activos en el combo de titular
        'idPropio es el titular del vehiculo que se esta viendo: si esta dado de baja se lista igual,
        'marcado como tal, para que el combo no quede vacio; con 0 solo se listan los activos
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_cliente, " &
                    "CASE WHEN activo = 1 THEN razon_social ELSE CONCAT(razon_social, ' (dado de baja)') END AS nombre " &
                    "FROM cliente WHERE activo = 1 OR id_cliente = @propio " &
                    "ORDER BY activo DESC, razon_social;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@propio", idPropio)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'el usuario ve el nombre...
                    cboTitular.DisplayMember = "nombre"
                    '...pero el programa guarda la clave numerica
                    cboTitular.ValueMember = "id_cliente"
                    cboTitular.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los titulares: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboMarcas()
        'cargo todas las marcas en el combo
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String = "SELECT id_marca, descripcion FROM marca ORDER BY descripcion;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'agrego una primera opcion con clave 0 que funciona como texto de ayuda
                    Dim filaAyuda As DataRow = tabla.NewRow()
                    filaAyuda("id_marca") = 0
                    filaAyuda("descripcion") = "Seleccione una marca"
                    tabla.Rows.InsertAt(filaAyuda, 0)

                    cboMarca.DisplayMember = "descripcion"
                    cboMarca.ValueMember = "id_marca"
                    cboMarca.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las marcas: " & ex.Message)
        End Try
    End Sub

    Sub CargarComboModelos(idMarca As Integer)
        'cargo solo los modelos de la marca elegida (combo en cascada)
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
                    'primera opcion de ayuda, para no elegir un modelo sin querer
                    Dim filaAyuda As DataRow = tabla.NewRow()
                    filaAyuda("id_modelo") = 0
                    filaAyuda("descripcion") = "Seleccione un modelo"
                    tabla.Rows.InsertAt(filaAyuda, 0)

                    cboModelo.DisplayMember = "descripcion"
                    cboModelo.ValueMember = "id_modelo"
                    cboModelo.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los modelos: " & ex.Message)
        End Try
    End Sub

    Function MarcarFila(id As Integer) As DataGridViewRow
        'dejo marcada en la lista la fila del vehiculo con esa clave: celda actual y seleccion juntas
        'con clave 0, o si el vehiculo no esta listado, no queda nada marcado; devuelve la fila o Nothing
        Dim encontrada As DataGridViewRow = Nothing
        Dim estabaCargando As Boolean = cargando
        cargando = True

        Try
            If id <> 0 AndAlso dgvVehiculos.Columns.Contains("id_vehiculo") Then
                For Each fila As DataGridViewRow In dgvVehiculos.Rows
                    If CInt(fila.Cells("id_vehiculo").Value) = id Then encontrada = fila
                Next
            End If

            dgvVehiculos.ClearSelection()
            If encontrada Is Nothing Then
                dgvVehiculos.CurrentCell = Nothing
            Else
                dgvVehiculos.CurrentCell = encontrada.Cells("patente")
                encontrada.Selected = True
            End If
        Finally
            cargando = estabaCargando
        End Try

        Return encontrada
    End Function

    Sub RefrescarRegistro()
        'vuelvo a mostrar en la tarjeta los datos guardados del vehiculo que se esta viendo
        Dim fila As DataGridViewRow = MarcarFila(idVehiculo)
        If fila IsNot Nothing Then MostrarRegistro(fila)
    End Sub

    Sub MostrarAyuda()
        'sin vehiculo elegido ni alta en curso, la tarjeta muestra solo la ayuda
        idVehiculo = 0
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
                'para un vehiculo nuevo solo se puede elegir un cliente activo
                CargarComboTitulares(0)
                cboTitular.SelectedIndex = -1
                cboTitular.Text = ""
                txtPatente.Clear()
                'vuelvo a la opcion "Seleccione una marca", eso tambien vacia los modelos
                If cboMarca.Items.Count > 0 Then cboMarca.SelectedIndex = 0
                cboModelo.DataSource = Nothing
                nudAnio.Value = Date.Now.Year
                txtColor.Clear()
                txtMotor.Clear()
                txtChasis.Clear()
                nudKilometraje.Value = 0
                txtObservaciones.Clear()
            Else
                txtPatente.Text = fila.Cells("patente").Value.ToString()

                'el combo lista los clientes activos y tambien al titular de este vehiculo, aunque este dado de baja
                CargarComboTitulares(CInt(fila.Cells("id_cliente").Value))
                cboTitular.SelectedValue = CInt(fila.Cells("id_cliente").Value)

                'primero elijo la marca: eso recarga los modelos de esa marca
                cboMarca.SelectedValue = CInt(fila.Cells("id_marca").Value)
                'despues elijo el modelo dentro de la lista ya cargada
                cboModelo.SelectedValue = CInt(fila.Cells("id_modelo").Value)

                'el año puede estar vacio en la base
                If IsDBNull(fila.Cells("anio").Value) Then
                    nudAnio.Value = Date.Now.Year
                Else
                    nudAnio.Value = CInt(fila.Cells("anio").Value)
                End If

                txtColor.Text = fila.Cells("color").Value.ToString()
                txtMotor.Text = fila.Cells("nro_motor").Value.ToString()
                txtChasis.Text = fila.Cells("nro_chasis").Value.ToString()
                nudKilometraje.Value = CInt(fila.Cells("km_actual").Value)
                txtObservaciones.Text = fila.Cells("observaciones").Value.ToString()
            End If

            'lo que se ve es lo que esta guardado
            hayCambios = False
        Finally
            cargando = estabaCargando
        End Try
    End Sub

    Sub MostrarRegistro(fila As DataGridViewRow)
        'cargo en la tarjeta al vehiculo de la fila, para verlo y modificarlo
        idVehiculo = CInt(fila.Cells("id_vehiculo").Value)
        creando = False
        registroActivo = Convert.ToBoolean(fila.Cells("activo").Value)

        CargarCampos(fila)
        lblRegistroTitulo.Text = fila.Cells("patente").Value.ToString()

        'etiqueta de estado y boton de la izquierda segun el vehiculo este activo o dado de baja
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

        'linea de contexto: cuantas ordenes de trabajo tiene el vehiculo y cuando fue la ultima
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT COUNT(*) AS cantidad, MAX(fecha_recepcion) AS ultima " &
                    "FROM orden_trabajo WHERE id_vehiculo = @id;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id", idVehiculo)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        lblContexto.Text = "Sin órdenes de trabajo"
                        'sin ordenes la fecha viene vacia, por eso la miro antes de usarla
                        If lector.Read() AndAlso CInt(lector("cantidad")) > 0 AndAlso Not IsDBNull(lector("ultima")) Then
                            Dim cantidad As Integer = CInt(lector("cantidad"))
                            Dim textoOrdenes As String = cantidad & " órdenes de trabajo"
                            If cantidad = 1 Then textoOrdenes = "1 orden de trabajo"
                            lblContexto.Text = textoOrdenes & " · última el " & CDate(lector("ultima")).ToString("dd/MM/yyyy")
                        End If
                    End Using
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
        'dejo la tarjeta lista para cargar un vehiculo nuevo
        idVehiculo = 0
        creando = True
        registroActivo = True

        CargarCampos(Nothing)
        lblRegistroTitulo.Text = "Nuevo vehículo"

        'un vehiculo nuevo no tiene estado ni contexto, y sus botones son Cancelar y Guardar
        lblEstadoRegistro.Visible = False
        lblContexto.Visible = False
        btnBaja.Visible = False
        btnCancelar.Visible = True
        btnGuardar.Text = " Guardar"

        lblAyuda.Visible = False
        pnlRegistro.Visible = True

        'durante un alta no queda ninguna fila marcada en la lista
        MarcarFila(0)

        txtPatente.Focus()
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
        Dim idElegido As Integer = CInt(fila.Cells("id_vehiculo").Value)

        'si ya se esta viendo ese vehiculo no hay nada que hacer
        If idElegido = idVehiculo AndAlso Not creando Then Exit Sub

        If Not DescartarCambios() Then
            'el usuario prefirio seguir con lo que estaba cargando: la lista vuelve a marcar ese registro
            '(en un alta no hay registro, y no queda nada marcado)
            MarcarFila(idVehiculo)
            Exit Sub
        End If

        MostrarRegistro(fila)
    End Sub

    Function ValidarCampos() As Boolean
        'valido los campos obligatorios
        If txtPatente.Text.Trim = "" Then
            MessageBox.Show("Falta la patente")
            txtPatente.Focus()
            Return False
        End If

        If cboTitular.SelectedValue Is Nothing Then
            MessageBox.Show("Seleccione un titular de la lista")
            cboTitular.Focus()
            Return False
        End If

        'la clave 0 corresponde a la opcion de ayuda, no a una marca real
        If Not TypeOf cboMarca.SelectedValue Is Integer OrElse CInt(cboMarca.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione una marca")
            cboMarca.Focus()
            Return False
        End If

        If Not TypeOf cboModelo.SelectedValue Is Integer OrElse CInt(cboModelo.SelectedValue) = 0 Then
            MessageBox.Show("Seleccione un modelo")
            cboModelo.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub FrmVehiculos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnNuevo.Image = LeerIcono("agregar.png")
        btnGuardar.Image = LeerIcono("guardar.png")
        btnCancelar.Image = LeerIcono("cancelar-oscuro.png")
        picBuscar.Image = LeerIcono("buscar-oscuro.png")
        iconoBaja = LeerIcono("baja-rojo.png")
        iconoReactivar = LeerIcono("reactivar-oscuro.png")

        'el encabezado de la lista lleva solo una linea clara debajo, como las filas
        dgvVehiculos.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'cargo combos y grilla al abrir el formulario, la tarjeta arranca con la ayuda
        cargando = True
        Try
            CargarComboTitulares(0)
            CargarComboMarcas()
        Finally
            cargando = False
        End Try

        MostrarAyuda()
        CargarVehiculos()
    End Sub

    Private Sub FrmVehiculos_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'al mostrarse por primera vez la grilla toma sola la primera fila como celda actual: la suelto
        MarcarFila(idVehiculo)
    End Sub

    Private Sub FrmVehiculos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
        CargarVehiculos()
    End Sub

    Private Sub chkBajas_CheckedChanged(sender As Object, e As EventArgs) Handles chkBajas.CheckedChanged
        'muestro o dejo de mostrar los vehiculos dados de baja
        CargarVehiculos()
    End Sub

    Private Sub dgvVehiculos_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvVehiculos.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas
        If Not dgvVehiculos.Columns.Contains("activo") Then Exit Sub

        For Each fila As DataGridViewRow In dgvVehiculos.Rows
            'los vehiculos dados de baja van en gris, tambien cuando la fila esta seleccionada
            If Not Convert.ToBoolean(fila.Cells("activo").Value) Then
                fila.DefaultCellStyle.ForeColor = Color.Gray
                fila.DefaultCellStyle.SelectionForeColor = Color.Gainsboro
            End If
        Next

        'al terminar de enlazar la grilla marca sola la primera fila: dejo marcado solo al vehiculo que se esta viendo
        MarcarFila(idVehiculo)
    End Sub

    Private Sub dgvVehiculos_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvVehiculos.ColumnHeaderMouseClick
        'ordeno la lista por la columna del titulo que se toco; otro click en la misma columna invierte el orden
        'ordenar nunca cambia el registro abierto ni pregunta nada
        If e.Button <> MouseButtons.Left Then Exit Sub
        If cargando Then Exit Sub

        Dim columna As DataGridViewColumn = dgvVehiculos.Columns(e.ColumnIndex)
        Dim sentido As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        If dgvVehiculos.SortedColumn Is columna AndAlso dgvVehiculos.SortOrder = SortOrder.Ascending Then
            sentido = System.ComponentModel.ListSortDirection.Descending
        End If

        'mientras se ordena la grilla mueve sola su seleccion: eso no es una eleccion del usuario
        cargando = True
        Try
            dgvVehiculos.Sort(columna, sentido)

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

    Private Sub dgvVehiculos_Sorted(sender As Object, e As EventArgs) Handles dgvVehiculos.Sorted
        'las filas cambiaron de lugar: vuelvo a marcar al vehiculo que se esta viendo
        MarcarFila(idVehiculo)
    End Sub

    Private Sub dgvVehiculos_SelectionChanged(sender As Object, e As EventArgs) Handles dgvVehiculos.SelectionChanged
        'cargo en la tarjeta al vehiculo que el usuario elige con el mouse o con el teclado

        'al recargar la grilla la seleccion cambia sola, eso no es una eleccion del usuario
        If cargando Then Exit Sub
        If Not dgvVehiculos.Focused Then Exit Sub
        If dgvVehiculos.SelectedRows.Count = 0 Then Exit Sub

        ElegirFila(dgvVehiculos.SelectedRows(0))
    End Sub

    Private Sub dgvVehiculos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVehiculos.CellClick
        'un click sobre una fila siempre la carga, aunque la grilla ya la tuviera marcada
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        ElegirFila(dgvVehiculos.Rows(e.RowIndex))
    End Sub

    Private Sub cboMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMarca.SelectedIndexChanged
        'cuando cambia la marca, recargo los modelos de esa marca
        If TypeOf cboMarca.SelectedValue Is Integer AndAlso CInt(cboMarca.SelectedValue) > 0 Then
            CargarComboModelos(CInt(cboMarca.SelectedValue))
        Else
            'con "Seleccione una marca" no hay modelos para mostrar
            cboModelo.DataSource = Nothing
        End If

        'si la cambio el usuario, hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub Campo_Changed(sender As Object, e As EventArgs) Handles _
        txtPatente.TextChanged, cboTitular.TextChanged, cboTitular.SelectedIndexChanged, cboModelo.SelectedIndexChanged,
        nudAnio.ValueChanged, txtColor.TextChanged, txtMotor.TextChanged, txtChasis.TextChanged,
        nudKilometraje.ValueChanged, txtObservaciones.TextChanged

        'el usuario cambio un campo: hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        'empiezo la carga de un vehiculo nuevo
        If Not DescartarCambios() Then Exit Sub
        NuevoRegistro()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        'dejo de cargar el vehiculo nuevo, la tarjeta vuelve a la ayuda
        If Not DescartarCambios() Then Exit Sub
        MostrarAyuda()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo un vehiculo nuevo, o los cambios del vehiculo que se esta viendo
        If Not ValidarCampos() Then Exit Sub

        'recuerdo si era un alta, porque al guardar deja de serlo
        Dim eraAlta As Boolean = creando

        If creando Then
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    Dim consulta As String =
                        "INSERT INTO vehiculo (patente, id_cliente, id_modelo, anio, color, nro_motor, nro_chasis, km_actual, observaciones) " &
                        "VALUES (@patente, @id_cliente, @id_modelo, @anio, @color, @nro_motor, @nro_chasis, @km_actual, @observaciones);"

                    Using cmd As New MySqlCommand(consulta, cn)
                        'cargo valores en los parametros
                        cmd.Parameters.AddWithValue("@patente", txtPatente.Text.Trim)
                        'SelectedValue contiene la clave del elemento elegido
                        cmd.Parameters.AddWithValue("@id_cliente", CInt(cboTitular.SelectedValue))
                        cmd.Parameters.AddWithValue("@id_modelo", CInt(cboModelo.SelectedValue))
                        cmd.Parameters.AddWithValue("@anio", CInt(nudAnio.Value))
                        cmd.Parameters.AddWithValue("@color", txtColor.Text.Trim)
                        cmd.Parameters.AddWithValue("@nro_motor", txtMotor.Text.Trim)
                        cmd.Parameters.AddWithValue("@nro_chasis", txtChasis.Text.Trim)
                        cmd.Parameters.AddWithValue("@km_actual", CInt(nudKilometraje.Value))
                        cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text.Trim)

                        Dim Resultado As Integer = cmd.ExecuteNonQuery()

                        'el vehiculo nuevo queda como el que se esta viendo, con la clave que le dio la base
                        idVehiculo = CInt(cmd.LastInsertedId)
                        creando = False
                        hayCambios = False

                        MessageBox.Show("Registros agregados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                'error 1062: la patente ya existe (restriccion unica)
                MessageBox.Show("Ya existe un vehículo con esa patente.")
                txtPatente.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al guardar " & ex.Message)
                Exit Sub
            End Try
        Else
            Try
                Using cn As New MySqlConnection(CADENA)
                    cn.Open()
                    'cambiar el titular aca no pierde el historial:
                    'cada orden de trabajo guarda su propio cliente (regla 8.4)
                    Dim consulta As String =
                        "UPDATE vehiculo SET patente=@patente, id_cliente=@id_cliente, id_modelo=@id_modelo, " &
                        "anio=@anio, color=@color, nro_motor=@nro_motor, nro_chasis=@nro_chasis, " &
                        "km_actual=@km_actual, observaciones=@observaciones " &
                        "WHERE id_vehiculo=@id;"

                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@patente", txtPatente.Text.Trim)
                        cmd.Parameters.AddWithValue("@id_cliente", CInt(cboTitular.SelectedValue))
                        cmd.Parameters.AddWithValue("@id_modelo", CInt(cboModelo.SelectedValue))
                        cmd.Parameters.AddWithValue("@anio", CInt(nudAnio.Value))
                        cmd.Parameters.AddWithValue("@color", txtColor.Text.Trim)
                        cmd.Parameters.AddWithValue("@nro_motor", txtMotor.Text.Trim)
                        cmd.Parameters.AddWithValue("@nro_chasis", txtChasis.Text.Trim)
                        cmd.Parameters.AddWithValue("@km_actual", CInt(nudKilometraje.Value))
                        cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text.Trim)
                        cmd.Parameters.AddWithValue("@id", idVehiculo)

                        Dim Resultado As Integer = cmd.ExecuteNonQuery()
                        hayCambios = False
                        MessageBox.Show("Registros actualizados: " & Resultado)
                    End Using
                End Using

            Catch ex As MySqlException When ex.Number = 1062
                MessageBox.Show("Ya existe otro vehículo con esa patente.")
                txtPatente.Focus()
                Exit Sub
            Catch ex As Exception
                MessageBox.Show("Error al modificar " & ex.Message)
                Exit Sub
            End Try
        End If

        'si se creo un vehiculo, limpio la busqueda para que quede listado aunque no coincida con lo escrito
        If eraAlta AndAlso txtFiltro.Text <> "" Then
            cargando = True
            Try
                txtFiltro.Clear()
            Finally
                cargando = False
            End Try
        End If

        'recargo la lista: el vehiculo guardado queda marcado y la tarjeta muestra lo que quedo en la base
        CargarVehiculos()
        RefrescarRegistro()
    End Sub

    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        'doy de baja el vehiculo que se esta viendo, o lo reactivo si ya estaba dado de baja
        Dim idRegistro As Integer = idVehiculo

        'la baja y la reactivacion no guardan los campos: si hay cambios lo aviso en la misma pregunta
        Dim avisoCambios As String = ""
        If hayCambios Then
            avisoCambios = vbCrLf & vbCrLf & "Los cambios sin guardar de la tarjeta se van a perder."
        End If

        'pido confirmacion antes de cambiar el estado
        Dim respuesta As DialogResult
        If registroActivo Then
            respuesta = MessageBox.Show(
                "¿Dar de baja el vehículo " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Dar de baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
        Else
            respuesta = MessageBox.Show(
                "¿Reactivar el vehículo " & lblRegistroTitulo.Text & "?" & avisoCambios,
                "Reactivar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
        End If

        'si no confirma no se hace nada, y lo que estaba escribiendo queda como estaba
        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        'estado del vehiculo leido en la base dentro de la transaccion
        Dim activoEnBase As Boolean = False
        'queda en True cuando lo que hay en pantalla ya no coincide con la base
        Dim desactualizado As Boolean = False

        'baja logica: el vehiculo puede tener ordenes de trabajo asociadas
        'solo lo marco como inactivo, y con el mismo boton se lo puede reactivar
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la comprobacion y el cambio de estado van juntos en una transaccion
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el vehiculo y bloqueo solo su fila hasta terminar
                    Dim existe As Boolean = False
                    Dim idTitular As Integer = 0
                    Dim consulta As String = "SELECT activo, id_cliente FROM vehiculo WHERE id_vehiculo = @id FOR UPDATE;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id", idRegistro)
                        Using lector As MySqlDataReader = cmd.ExecuteReader
                            If lector.Read() Then
                                existe = True
                                activoEnBase = Convert.ToBoolean(lector("activo"))
                                idTitular = CInt(lector("id_cliente"))
                            End If
                        End Using
                    End Using

                    If Not existe Then
                        aviso = "El vehículo seleccionado ya no existe."
                        desactualizado = True
                    End If

                    'si otro puesto ya le cambio el estado, lo que se confirmo en pantalla no vale
                    If aviso = "" AndAlso activoEnBase <> registroActivo Then
                        aviso = "El estado del vehículo fue cambiado desde otro puesto. Se actualizó la lista: revíselo y vuelva a intentar."
                        desactualizado = True
                    End If

                    'de aca en mas decido con el estado leido en la base, no con el de la pantalla
                    If aviso = "" AndAlso activoEnBase Then
                        'no se da de baja un vehiculo con una orden de trabajo sin cerrar (estado no final)
                        consulta =
                            "SELECT COUNT(*) AS abiertas, MAX(ot.nro_orden) AS ultima " &
                            "FROM orden_trabajo AS ot " &
                            "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                            "WHERE ot.id_vehiculo = @id AND e.es_estado_final = 0;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id", idRegistro)
                            Using lector As MySqlDataReader = cmd.ExecuteReader
                                If lector.Read() AndAlso CInt(lector("abiertas")) > 0 Then
                                    aviso = "No se puede dar de baja: el vehículo tiene " & lector("abiertas").ToString() &
                                            " orden(es) de trabajo sin cerrar (la N.º " & lector("ultima").ToString() &
                                            "). Ciérrela o anúlela primero."
                                End If
                            End Using
                        End Using
                    End If

                    If aviso = "" AndAlso Not activoEnBase Then
                        'no se reactiva un vehiculo cuyo titular esta dado de baja
                        consulta = "SELECT razon_social, activo FROM cliente WHERE id_cliente = @id_cliente;"
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_cliente", idTitular)
                            Using lector As MySqlDataReader = cmd.ExecuteReader
                                If lector.Read() AndAlso Not Convert.ToBoolean(lector("activo")) Then
                                    aviso = "No se puede reactivar: su titular, " & lector("razon_social").ToString() &
                                            ", está dado de baja. Reactive primero al cliente en ""Clientes""."
                                End If
                            End Using
                        End Using
                    End If

                    If aviso = "" Then
                        'activo pasa a FALSE en la baja y a TRUE en la reactivacion
                        consulta = "UPDATE vehiculo SET activo = @activo WHERE id_vehiculo = @id;"
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
            MessageBox.Show("Vehículos dados de baja: 1")
        Else
            MessageBox.Show("Vehículos reactivados: 1")
        End If

        'los cambios sin guardar se pierden recien ahora: cuando el estado cambio, o cuando la tarjeta
        'mostraba un estado que ya no es el de la base y hay que volver a leerla
        If aviso = "" OrElse desactualizado Then hayCambios = False

        'recargo la lista: muestra el estado real, y el vehiculo sigue marcado si todavia esta listado
        CargarVehiculos()
        'si la baja se rechazo y habia cambios sin guardar, la tarjeta queda como estaba
        If Not hayCambios Then RefrescarRegistro()
    End Sub

End Class

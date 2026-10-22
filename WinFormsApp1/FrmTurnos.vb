Imports MySqlConnector

Public Class FrmTurnos

    'agenda de turnos del taller: un dia por vez, una fila por turno y otra por los lugares libres de cada franja
    'un turno reserva un horario del taller para un vehiculo, no un mecanico

    'dia que muestra la agenda
    Private dia As Date = Date.Today

    'vehiculo y motivo de un turno pedido desde otra pantalla (por ejemplo, Services por vencer)
    'se cargan antes de mostrar la pantalla; vacios, la pantalla abre normal
    Public PatenteInicial As String = ""
    Public MotivoInicial As String = ""
    'turno que se ve en la tarjeta, 0 mientras no haya uno elegido
    Private idTurno As Integer = 0
    'indica que la tarjeta esta cargando un turno nuevo
    Private creando As Boolean = False
    'vehiculo encontrado por patente para el turno nuevo, 0 mientras no haya
    Private idVehiculo As Integer = 0
    'queda en True cuando el usuario escribio algo del turno nuevo y todavia no lo guardo
    Private hayCambios As Boolean = False
    'queda en True mientras el programa carga la agenda o los campos, para no tomarlo como un cambio del usuario
    Private cargando As Boolean = False
    'queda en True cuando la ultima lectura de las horas libres fallo, para no confundirlo con un dia lleno
    Private errorLeyendoHoras As Boolean = False

    Sub CargarAgenda()
        'armo la agenda del dia: por cada franja, sus turnos y una fila con los lugares que quedan libres
        'los turnos cancelados no se muestran y no ocupan lugar
        cargando = True
        Try
            Dim cultura As New Globalization.CultureInfo("es-AR")
            Dim franjas As List(Of Date) = FranjasDelDia(dia)

            'el domingo no hay franjas: en lugar de la agenda se ve el aviso
            If franjas.Count = 0 Then
                lblCerrado.Visible = True
                dgvAgenda.Visible = False
                lblSubtitulo.Text = dia.ToString("dddd d 'de' MMMM 'de' yyyy", cultura) & " · cerrado"
                Exit Sub
            End If

            Dim turnos As New DataTable
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'el cliente es el titular actual del vehiculo
                Dim consulta As String =
                    "SELECT t.id_turno, t.fecha_hora, t.estado, t.motivo, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "c.razon_social AS cliente, c.telefono " &
                    "FROM turno AS t " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = t.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "WHERE t.fecha_hora >= @desde AND t.fecha_hora < @hasta AND t.estado <> 'CANCELADO' " &
                    "ORDER BY t.fecha_hora, t.id_turno;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@desde", dia.Date)
                    cmd.Parameters.AddWithValue("@hasta", dia.Date.AddDays(1))
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        turnos.Load(lector)
                    End Using
                End Using
            End Using

            'tabla de la agenda: las filas de turnos y las de lugares libres tienen las mismas columnas
            Dim agenda As New DataTable
            agenda.Columns.Add("id_turno", GetType(Integer))
            agenda.Columns.Add("fecha_hora", GetType(Date))
            agenda.Columns.Add("hora", GetType(String))
            agenda.Columns.Add("patente", GetType(String))
            agenda.Columns.Add("vehiculo", GetType(String))
            agenda.Columns.Add("cliente", GetType(String))
            agenda.Columns.Add("telefono", GetType(String))
            agenda.Columns.Add("motivo", GetType(String))
            agenda.Columns.Add("estado", GetType(String))

            Dim totalTurnos As Integer = 0
            Dim totalLibres As Integer = 0
            Dim usados As New List(Of Integer)

            For Each franja As Date In franjas
                Dim ocupados As Integer = 0
                For Each turno As DataRow In turnos.Rows
                    If CDate(turno("fecha_hora")) = franja Then
                        AgregarFilaTurno(agenda, turno)
                        usados.Add(CInt(turno("id_turno")))
                        ocupados = ocupados + 1
                    End If
                Next
                totalTurnos = totalTurnos + ocupados

                'los lugares libres se ofrecen mientras la franja no termino
                Dim libres As Integer = CUPO_POR_FRANJA - ocupados
                If libres > 0 AndAlso franja.AddHours(1) > Date.Now Then
                    Dim fila As DataRow = agenda.NewRow()
                    fila("id_turno") = 0
                    fila("fecha_hora") = franja
                    fila("hora") = franja.ToString("HH:mm")
                    'el texto invita a usar el lugar: un clic en la fila abre el turno nuevo a esa hora
                    If libres = 1 Then
                        fila("vehiculo") = "+ Dar turno (1 lugar libre)"
                    Else
                        fila("vehiculo") = "+ Dar turno (" & libres & " lugares libres)"
                    End If
                    fila("estado") = "Libre"
                    agenda.Rows.Add(fila)
                    totalLibres = totalLibres + libres
                End If
            Next

            'un turno fuera del horario actual (por ejemplo, si el horario cambio) se lista igual, al final
            For Each turno As DataRow In turnos.Rows
                If Not usados.Contains(CInt(turno("id_turno"))) Then
                    AgregarFilaTurno(agenda, turno)
                    totalTurnos = totalTurnos + 1
                End If
            Next

            'la grilla se muestra antes de enlazarla: oculta no arma sus columnas
            lblCerrado.Visible = False
            dgvAgenda.Visible = True
            dgvAgenda.DataSource = agenda

            dgvAgenda.Columns("id_turno").Visible = False
            dgvAgenda.Columns("fecha_hora").Visible = False
            dgvAgenda.Columns("hora").HeaderText = "Hora"
            dgvAgenda.Columns("patente").HeaderText = "Patente"
            dgvAgenda.Columns("vehiculo").HeaderText = "Vehículo"
            dgvAgenda.Columns("cliente").HeaderText = "Cliente"
            dgvAgenda.Columns("telefono").HeaderText = "Teléfono"
            dgvAgenda.Columns("motivo").HeaderText = "Motivo"
            dgvAgenda.Columns("estado").HeaderText = "Estado"

            'la hora, la patente, el telefono y el estado tienen un ancho fijo, suficiente para su contenido
            dgvAgenda.Columns("hora").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvAgenda.Columns("hora").Width = 64
            dgvAgenda.Columns("hora").DefaultCellStyle.Font = New Font(dgvAgenda.Font, FontStyle.Bold)
            dgvAgenda.Columns("patente").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvAgenda.Columns("patente").Width = 92
            dgvAgenda.Columns("telefono").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvAgenda.Columns("telefono").Width = 110
            dgvAgenda.Columns("estado").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvAgenda.Columns("estado").Width = 96

            'el vehiculo, el cliente y el motivo se reparten el resto del ancho
            dgvAgenda.Columns("vehiculo").FillWeight = 110
            dgvAgenda.Columns("vehiculo").MinimumWidth = 120
            dgvAgenda.Columns("cliente").FillWeight = 100
            dgvAgenda.Columns("cliente").MinimumWidth = 90
            dgvAgenda.Columns("motivo").FillWeight = 120
            dgvAgenda.Columns("motivo").MinimumWidth = 90

            'las filas no se ordenan por columna: el orden de la agenda es el de las franjas
            For Each columna As DataGridViewColumn In dgvAgenda.Columns
                columna.SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            Dim textoTurnos As String = totalTurnos & " turnos"
            If totalTurnos = 1 Then textoTurnos = "1 turno"
            Dim textoLibres As String = totalLibres & " lugares libres"
            If totalLibres = 1 Then textoLibres = "1 lugar libre"
            lblSubtitulo.Text = dia.ToString("dddd d 'de' MMMM 'de' yyyy", cultura) & " · " & textoTurnos & " · " & textoLibres
        Catch ex As Exception
            MostrarError("No se pudo cargar la agenda de turnos.", ex)
        Finally
            cargando = False
        End Try

        'en la agenda nueva dejo marcado el turno que se esta viendo
        MarcarFila()
    End Sub

    Sub AgregarFilaTurno(agenda As DataTable, turno As DataRow)
        'paso un turno de la consulta a una fila de la agenda
        Dim fila As DataRow = agenda.NewRow()
        fila("id_turno") = CInt(turno("id_turno"))
        fila("fecha_hora") = CDate(turno("fecha_hora"))
        fila("hora") = CDate(turno("fecha_hora")).ToString("HH:mm")
        fila("patente") = turno("patente").ToString()
        fila("vehiculo") = turno("vehiculo").ToString()
        fila("cliente") = turno("cliente").ToString()
        fila("telefono") = turno("telefono").ToString()
        fila("motivo") = turno("motivo").ToString()
        fila("estado") = TextoEstadoTurno(turno("estado").ToString())
        agenda.Rows.Add(fila)
    End Sub

    Sub MarcarFila()
        'dejo marcada en la agenda la fila del turno que se ve en la tarjeta; sin turno no queda nada marcado
        Dim estabaCargando As Boolean = cargando
        cargando = True
        Try
            dgvAgenda.ClearSelection()
            dgvAgenda.CurrentCell = Nothing
            If idTurno = 0 OrElse Not dgvAgenda.Columns.Contains("id_turno") Then Exit Sub
            For Each fila As DataGridViewRow In dgvAgenda.Rows
                If CInt(fila.Cells("id_turno").Value) = idTurno Then
                    dgvAgenda.CurrentCell = fila.Cells("hora")
                    fila.Selected = True
                End If
            Next
        Finally
            cargando = estabaCargando
        End Try
    End Sub

    Sub CambiarDia(nuevoDia As Date)
        'muestro la agenda de otro dia; la tarjeta vuelve a la ayuda
        If Not DescartarCambios() Then Exit Sub
        dia = nuevoDia.Date
        cargando = True
        Try
            dtpDia.Value = dia
        Finally
            cargando = False
        End Try
        MostrarAyuda()
        CargarAgenda()
    End Sub

    Sub MostrarAyuda()
        'sin turno elegido ni alta en curso, la tarjeta muestra solo la ayuda
        idTurno = 0
        idVehiculo = 0
        creando = False
        hayCambios = False
        pnlRegistro.Visible = False
        lblAyuda.Visible = True
        MarcarFila()
    End Sub

    Function DescartarCambios() As Boolean
        'si hay un turno nuevo a medio cargar pregunto antes de perderlo; devuelve True cuando se puede seguir
        If Not hayCambios Then Return True

        Dim respuesta As DialogResult = MessageBox.Show(
            "El turno nuevo no se guardó. ¿Descartarlo?",
            "Turno sin guardar",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        Return respuesta = DialogResult.Yes
    End Function

    Function CargarComboHoras(Optional horaElegida As Date = Nothing) As Boolean
        'cargo en el combo las franjas del dia que todavia no terminaron y tienen lugar
        'devuelve False si no queda ninguna, o si la base no respondio (en ese caso errorLeyendoHoras queda en True)
        errorLeyendoHoras = False
        Dim tabla As New DataTable
        tabla.Columns.Add("fecha_hora", GetType(Date))
        tabla.Columns.Add("texto", GetType(String))

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                For Each franja As Date In FranjasDelDia(dia)
                    If franja.AddHours(1) <= Date.Now Then Continue For

                    Dim ocupados As Integer
                    Dim consulta As String =
                        "SELECT COUNT(*) FROM turno WHERE fecha_hora = @fecha_hora AND estado <> 'CANCELADO';"
                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@fecha_hora", franja)
                        ocupados = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    Dim libres As Integer = CUPO_POR_FRANJA - ocupados
                    If libres > 0 Then
                        Dim texto As String = franja.ToString("HH:mm") & " (" & libres & " libre"
                        If libres > 1 Then texto = texto & "s"
                        tabla.Rows.Add(franja, texto & ")")
                    End If
                Next
            End Using
        Catch ex As Exception
            MostrarError("No se pudieron leer los lugares libres.", ex)
            errorLeyendoHoras = True
            Return False
        End Try

        If tabla.Rows.Count = 0 Then Return False

        cboHora.DisplayMember = "texto"
        cboHora.ValueMember = "fecha_hora"
        cboHora.DataSource = tabla

        'si se eligio un lugar libre de la agenda, el combo arranca en esa hora
        cboHora.SelectedIndex = 0
        If horaElegida <> Nothing Then
            For i As Integer = 0 To tabla.Rows.Count - 1
                If CDate(tabla.Rows(i)("fecha_hora")) = horaElegida Then cboHora.SelectedIndex = i
            Next
        End If
        Return True
    End Function

    Sub NuevoTurno(Optional horaElegida As Date = Nothing)
        'dejo la tarjeta lista para dar un turno en el dia que muestra la agenda
        If FranjasDelDia(dia).Count = 0 Then
            MessageBox.Show("El taller no atiende los domingos.")
            Exit Sub
        End If

        cargando = True
        Try
            If Not CargarComboHoras(horaElegida) Then
                'si la base no respondio, el mensaje del error ya se mostro: no digo que el dia esta lleno
                If Not errorLeyendoHoras Then MessageBox.Show("No quedan lugares libres en este día.")
                Exit Sub
            End If

            idTurno = 0
            idVehiculo = 0
            creando = True
            txtPatente.Clear()
            txtMotivo.Clear()
            lblVehiculo.Text = "Busque la patente del vehículo."
            lblVehiculo.ForeColor = Color.DimGray
            hayCambios = False
        Finally
            cargando = False
        End Try

        lblRegistroTitulo.Text = "Nuevo turno"
        lblEstadoRegistro.Visible = False
        tlpCampos.Visible = True
        lblDetalle.Visible = False
        btnCancelarAlta.Visible = True
        btnGuardar.Visible = True
        btnCancelarTurno.Visible = False
        btnAusente.Visible = False
        btnConfirmacion.Visible = False

        lblAyuda.Visible = False
        pnlRegistro.Visible = True
        MarcarFila()
        txtPatente.Focus()
    End Sub

    Sub MostrarTurno(id As Integer)
        'cargo en la tarjeta un turno de la agenda, para verlo, cancelarlo o marcarlo como ausente
        Dim cultura As New Globalization.CultureInfo("es-AR")
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT t.fecha_hora, t.estado, t.motivo, t.fecha_alta, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "c.razon_social AS cliente, c.telefono, u.nombre_completo AS usuario, ot.nro_orden " &
                    "FROM turno AS t " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = t.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "JOIN usuario AS u ON u.id_usuario = t.id_usuario " &
                    "LEFT JOIN orden_trabajo AS ot ON ot.id_orden_trabajo = t.id_orden_trabajo " &
                    "WHERE t.id_turno = @id_turno;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_turno", id)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If Not lector.Read() Then
                            MessageBox.Show("El turno ya no existe.")
                            MostrarAyuda()
                            CargarAgenda()
                            Exit Sub
                        End If

                        Dim fechaHora As Date = CDate(lector("fecha_hora"))
                        Dim estado As String = lector("estado").ToString()

                        idTurno = id
                        idVehiculo = 0
                        creando = False
                        hayCambios = False

                        lblRegistroTitulo.Text = "Turno de las " & fechaHora.ToString("HH:mm")
                        lblEstadoRegistro.Text = TextoEstadoTurno(estado)
                        lblEstadoRegistro.Visible = True
                        If estado = "PENDIENTE" Then
                            lblEstadoRegistro.BackColor = Color.LemonChiffon
                            lblEstadoRegistro.ForeColor = Color.DarkGoldenrod
                        ElseIf estado = "ATENDIDO" Then
                            lblEstadoRegistro.BackColor = Color.FromArgb(223, 240, 216)
                            lblEstadoRegistro.ForeColor = Color.DarkGreen
                        Else
                            lblEstadoRegistro.BackColor = Color.MistyRose
                            lblEstadoRegistro.ForeColor = Color.Firebrick
                        End If

                        'el detalle va en un solo texto, de solo lectura
                        Dim motivo As String = lector("motivo").ToString()
                        If motivo = "" Then motivo = "-"
                        Dim telefono As String = lector("telefono").ToString()
                        If telefono = "" Then telefono = "sin teléfono"
                        Dim texto As String =
                            fechaHora.ToString("dddd d 'de' MMMM, HH:mm", cultura) & " hs" & vbCrLf & vbCrLf &
                            lector("patente").ToString() & " · " & lector("vehiculo").ToString() & vbCrLf &
                            lector("cliente").ToString() & " · " & telefono & vbCrLf & vbCrLf &
                            "Motivo: " & motivo & vbCrLf & vbCrLf &
                            "Turno dado por " & lector("usuario").ToString() & " el " &
                            CDate(lector("fecha_alta")).ToString("dd/MM/yyyy HH:mm") & "."
                        If Not IsDBNull(lector("nro_orden")) Then
                            texto = texto & vbCrLf & "Recepcionado: orden de trabajo N.º " & lector("nro_orden").ToString() & "."
                        End If
                        lblDetalle.Text = texto

                        'solo un turno pendiente se cancela; se marca ausente recien cuando su franja empezo
                        btnCancelarTurno.Visible = (estado = "PENDIENTE")
                        btnAusente.Visible = (estado = "PENDIENTE" AndAlso fechaHora <= Date.Now)
                        'la confirmacion se puede volver a copiar mientras el turno sigue pendiente
                        btnConfirmacion.Visible = (estado = "PENDIENTE")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MostrarError("No se pudo leer el turno.", ex)
            Exit Sub
        End Try

        tlpCampos.Visible = False
        lblDetalle.Visible = True
        btnCancelarAlta.Visible = False
        btnGuardar.Visible = False
        lblAyuda.Visible = False
        pnlRegistro.Visible = True
        MarcarFila()
    End Sub

    Sub CambiarEstado(estadoNuevo As String, pregunta As String, titulo As String, hecho As String)
        'paso el turno que se esta viendo de PENDIENTE al estado nuevo, si sigue pendiente en la base
        Dim respuesta As DialogResult = MessageBox.Show(pregunta, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If respuesta = DialogResult.No Then Exit Sub

        Dim aviso As String = ""
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'vuelvo a leer el turno bloqueandolo: otro puesto pudo cambiarlo despues de abrirlo
                    Dim estado As Object
                    Using cmd As New MySqlCommand("SELECT estado FROM turno WHERE id_turno = @id_turno FOR UPDATE;", cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_turno", idTurno)
                        estado = cmd.ExecuteScalar()
                    End Using

                    If estado Is Nothing Then
                        aviso = "El turno ya no existe."
                    ElseIf estado.ToString() <> "PENDIENTE" Then
                        aviso = "El turno ya no está pendiente: quedó " & TextoEstadoTurno(estado.ToString()).ToLower() & " desde otro puesto."
                    Else
                        Using cmd As New MySqlCommand("UPDATE turno SET estado = @estado WHERE id_turno = @id_turno;", cn, transaccion)
                            cmd.Parameters.AddWithValue("@estado", estadoNuevo)
                            cmd.Parameters.AddWithValue("@id_turno", idTurno)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                Catch ex As Exception
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
            MessageBox.Show("Error al cambiar el turno: " & ex.Message)
            Exit Sub
        End Try

        If aviso = "" Then
            MessageBox.Show(hecho)
        Else
            MessageBox.Show(aviso)
        End If

        'recargo la agenda; un turno cancelado ya no figura y la tarjeta vuelve a la ayuda
        Dim idVisto As Integer = idTurno
        CargarAgenda()
        If estadoNuevo = "CANCELADO" AndAlso aviso = "" Then
            MostrarAyuda()
        Else
            MostrarTurno(idVisto)
        End If
    End Sub

    Private Sub FrmTurnos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnNuevo.Image = LeerIcono("agregar.png")
        btnBuscarPatente.Image = LeerIcono("buscar.png")
        btnCancelarAlta.Image = LeerIcono("cancelar-oscuro.png")
        btnGuardar.Image = LeerIcono("guardar.png")
        btnCancelarTurno.Image = LeerIcono("baja-rojo.png")
        btnConfirmacion.Image = LeerIcono("check-oscuro.png")
        btnAusente.Image = LeerIcono("reloj-oscuro.png")

        'el encabezado de la agenda lleva solo una linea clara debajo, como las filas
        dgvAgenda.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single
        'encabezado blanco con titulos grises en negrita, y un poco de aire a los costados de cada celda
        dgvAgenda.ColumnHeadersDefaultCellStyle.BackColor = Color.White
        dgvAgenda.ColumnHeadersDefaultCellStyle.ForeColor = Color.DimGray
        dgvAgenda.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAgenda.Font, FontStyle.Bold)
        dgvAgenda.ColumnHeadersDefaultCellStyle.Padding = New Padding(4, 6, 4, 6)
        'el titulo de la columna de la celda actual no se pinta como seleccionado: queda igual que los demas
        dgvAgenda.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White
        dgvAgenda.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.DimGray
        dgvAgenda.DefaultCellStyle.Padding = New Padding(4, 0, 4, 0)
        dgvAgenda.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 73, 94)

        MostrarAyuda()

        'solo el administrador y el operador dan turnos (el menu ya oculta el boton)
        If Sesion.Rol <> ROL_ADMINISTRADOR AndAlso Sesion.Rol <> ROL_OPERADOR Then
            MessageBox.Show("Solo el administrador y el operador pueden gestionar los turnos.")
            btnNuevo.Enabled = False
            pnlLista.Enabled = False
            pnlFicha.Enabled = False
            btnDiaAnterior.Enabled = False
            btnDiaSiguiente.Enabled = False
            btnHoy.Enabled = False
            dtpDia.Enabled = False
            Exit Sub
        End If

        cargando = True
        Try
            dtpDia.Value = dia
        Finally
            cargando = False
        End Try
    End Sub

    Private Sub FrmTurnos_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'sin permiso la agenda quedo deshabilitada en Load y no se carga
        If Not pnlLista.Enabled Then Exit Sub

        'cargo la agenda recien ahora, cuando la pantalla ya tiene su tamaño final dentro de la ventana
        CargarAgenda()

        'si otra pantalla pidio un turno para un vehiculo, lo dejo listo para guardar
        If PatenteInicial <> "" Then IniciarTurnoPedido()
    End Sub

    Private Sub FrmTurnos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'al salir de la pantalla pregunto antes de perder un turno nuevo sin guardar
        If Not DescartarCambios() Then
            e.Cancel = True
            Exit Sub
        End If
        hayCambios = False
    End Sub

    Private Sub btnDiaAnterior_Click(sender As Object, e As EventArgs) Handles btnDiaAnterior.Click
        CambiarDia(dia.AddDays(-1))
    End Sub

    Private Sub btnDiaSiguiente_Click(sender As Object, e As EventArgs) Handles btnDiaSiguiente.Click
        CambiarDia(dia.AddDays(1))
    End Sub

    Private Sub btnHoy_Click(sender As Object, e As EventArgs) Handles btnHoy.Click
        CambiarDia(Date.Today)
    End Sub

    Private Sub dtpDia_ValueChanged(sender As Object, e As EventArgs) Handles dtpDia.ValueChanged
        'el usuario eligio otro dia en el calendario
        If cargando Then Exit Sub
        If dtpDia.Value.Date = dia Then Exit Sub

        Dim elegido As Date = dtpDia.Value.Date
        If Not DescartarCambios() Then
            'sigue con el turno a medio cargar: el calendario vuelve al dia de la agenda
            cargando = True
            Try
                dtpDia.Value = dia
            Finally
                cargando = False
            End Try
            Exit Sub
        End If

        hayCambios = False
        CambiarDia(elegido)
    End Sub

    Private Sub dgvAgenda_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvAgenda.DataBindingComplete
        'pinto las filas segun su estado; las franjas se alternan con un fondo apenas gris para separarlas
        If Not dgvAgenda.Columns.Contains("estado") Then Exit Sub

        Dim franjaAnterior As Date = Nothing
        Dim franjaPar As Boolean = True
        For Each fila As DataGridViewRow In dgvAgenda.Rows
            Dim franja As Date = CDate(fila.Cells("fecha_hora").Value)
            If franja <> franjaAnterior Then franjaPar = Not franjaPar
            franjaAnterior = franja
            If franjaPar Then fila.DefaultCellStyle.BackColor = Color.FromArgb(248, 249, 251)

            Dim estado As String = fila.Cells("estado").Value.ToString()
            If estado = "Libre" Then
                'los lugares libres van en azul, como un enlace para dar el turno
                fila.DefaultCellStyle.ForeColor = Color.FromArgb(0, 102, 204)
                fila.DefaultCellStyle.SelectionForeColor = Color.White
            ElseIf estado = "Atendido" Then
                fila.DefaultCellStyle.BackColor = Color.FromArgb(223, 240, 216)
            ElseIf estado = "Ausente" Then
                fila.DefaultCellStyle.BackColor = Color.MistyRose
            ElseIf CDate(fila.Cells("fecha_hora").Value) <= Date.Now Then
                'pendiente con la franja ya empezada: el vehiculo todavia no llego
                fila.DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next

        MarcarFila()
    End Sub

    Private Sub dgvAgenda_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvAgenda.CellFormatting
        'la hora se escribe solo en la primera fila de cada franja, como en una agenda de papel
        If e.RowIndex <= 0 Then Exit Sub
        If dgvAgenda.Columns(e.ColumnIndex).Name <> "hora" Then Exit Sub

        Dim anterior As Object = dgvAgenda.Rows(e.RowIndex - 1).Cells("fecha_hora").Value
        Dim actual As Object = dgvAgenda.Rows(e.RowIndex).Cells("fecha_hora").Value
        If anterior IsNot Nothing AndAlso actual IsNot Nothing AndAlso CDate(anterior) = CDate(actual) Then
            e.Value = ""
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub dgvAgenda_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAgenda.CellClick
        'un lugar libre abre el turno nuevo en esa hora; un turno se muestra en la tarjeta
        If e.RowIndex < 0 Then Exit Sub
        If cargando Then Exit Sub

        Dim fila As DataGridViewRow = dgvAgenda.Rows(e.RowIndex)
        Dim id As Integer = CInt(fila.Cells("id_turno").Value)

        If id = 0 Then
            'si ya se esta cargando un turno nuevo, el lugar elegido solo cambia la hora
            If creando Then
                Dim franja As Date = CDate(fila.Cells("fecha_hora").Value)
                For i As Integer = 0 To cboHora.Items.Count - 1
                    If CDate(CType(cboHora.Items(i), DataRowView)("fecha_hora")) = franja Then cboHora.SelectedIndex = i
                Next
                Exit Sub
            End If
            NuevoTurno(CDate(fila.Cells("fecha_hora").Value))
        Else
            If id = idTurno AndAlso Not creando Then Exit Sub
            If Not DescartarCambios() Then
                MarcarFila()
                Exit Sub
            End If
            MostrarTurno(id)
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        If Not DescartarCambios() Then Exit Sub
        NuevoTurno()
    End Sub

    Private Sub btnCancelarAlta_Click(sender As Object, e As EventArgs) Handles btnCancelarAlta.Click
        If Not DescartarCambios() Then Exit Sub
        MostrarAyuda()
    End Sub

    Private Sub Campo_Changed(sender As Object, e As EventArgs) Handles txtPatente.TextChanged, txtMotivo.TextChanged
        'el usuario escribio algo del turno nuevo: hay cambios sin guardar
        If cargando Then Exit Sub
        hayCambios = True
    End Sub

    Private Sub txtPatente_TextChanged(sender As Object, e As EventArgs) Handles txtPatente.TextChanged
        'si cambia la patente, el vehiculo encontrado ya no vale
        If cargando Then Exit Sub
        If idVehiculo <> 0 Then
            idVehiculo = 0
            lblVehiculo.Text = "Busque la patente del vehículo."
            lblVehiculo.ForeColor = Color.DimGray
        End If
    End Sub

    Private Sub txtPatente_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPatente.KeyDown
        'Enter en la patente busca el vehiculo
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnBuscarPatente.PerformClick()
        End If
    End Sub

    Private Sub btnBuscarPatente_Click(sender As Object, e As EventArgs) Handles btnBuscarPatente.Click
        'busco el vehiculo del turno por patente exacta
        Dim patente As String = txtPatente.Text.Trim.ToUpper
        If patente = "" Then
            MessageBox.Show("Ingrese la patente del vehículo")
            txtPatente.Focus()
            Exit Sub
        End If

        Dim encontrado As Boolean = False
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT v.id_vehiculo, v.activo, c.activo AS cliente_activo, c.razon_social, c.telefono, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo " &
                    "FROM vehiculo AS v " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "WHERE v.patente = @patente;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@patente", patente)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If lector.Read() Then
                            encontrado = True
                            If Not Convert.ToBoolean(lector("activo")) Then
                                MessageBox.Show("El vehículo con la patente " & patente & " está dado de baja.")
                                Exit Sub
                            End If
                            If Not Convert.ToBoolean(lector("cliente_activo")) Then
                                MessageBox.Show("El titular del vehículo " & patente & " está dado de baja.")
                                Exit Sub
                            End If

                            Dim telefono As String = lector("telefono").ToString()
                            If telefono = "" Then telefono = "sin teléfono"
                            idVehiculo = CInt(lector("id_vehiculo"))
                            'el vehiculo en la primera linea y su titular con el telefono en la segunda
                            lblVehiculo.Text = lector("vehiculo").ToString() & vbCrLf &
                                               lector("razon_social").ToString() & " · " & telefono
                            lblVehiculo.ForeColor = Color.Black
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MostrarError("No se pudo buscar el vehículo.", ex)
            Exit Sub
        End Try

        If encontrado Then
            txtMotivo.Focus()
            Exit Sub
        End If

        'la patente no existe: ofrezco registrarla sin salir de la pantalla, igual que en la recepcion
        Dim respuesta As DialogResult = MessageBox.Show(
            "La patente " & patente & " no está registrada. ¿Registrar el vehículo ahora?",
            "Patente no registrada", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If respuesta = DialogResult.No Then Exit Sub

        Dim patenteRegistrada As String = ""
        Using formulario As New FrmAltaRapida
            formulario.Patente = patente
            If formulario.ShowDialog() <> DialogResult.OK Then Exit Sub
            patenteRegistrada = formulario.Patente
        End Using

        'busco la patente recien registrada con el boton de siempre
        txtPatente.Text = patenteRegistrada
        btnBuscarPatente.PerformClick()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'guardo el turno nuevo, comprobando en la base el cupo de la franja y que el vehiculo no tenga otro turno pendiente
        If idVehiculo = 0 Then
            MessageBox.Show("Busque la patente del vehículo antes de dar el turno.")
            txtPatente.Focus()
            Exit Sub
        End If

        If cboHora.SelectedValue Is Nothing Then
            MessageBox.Show("Elija la hora del turno")
            cboHora.Focus()
            Exit Sub
        End If

        Dim franja As Date = CDate(cboHora.SelectedValue)
        If franja.AddHours(1) <= Date.Now Then
            MessageBox.Show("Esa franja ya terminó. Elija otra hora.")
            Exit Sub
        End If

        'el motivo vacio se guarda como NULL
        Dim motivo As Object = DBNull.Value
        If txtMotivo.Text.Trim <> "" Then motivo = txtMotivo.Text.Trim

        Dim aviso As String = ""
        Dim idNuevo As Integer = 0
        'nombre del bloqueo de la franja: los puestos que dan un turno en la misma franja pasan de a uno
        'sin el, con la franja vacia los bloqueos de la consulta del cupo no se excluyen y los guardados chocaban
        Dim nombreBloqueo As String = "taller_turno_" & franja.ToString("yyyyMMddHH")
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'espero a lo sumo 10 segundos a que otro puesto termine de dar un turno en esta franja
                Using cmd As New MySqlCommand("SELECT GET_LOCK(@nombre, 10);", cn)
                    cmd.Parameters.AddWithValue("@nombre", nombreBloqueo)
                    Dim obtenido As Object = cmd.ExecuteScalar()
                    If IsDBNull(obtenido) OrElse Convert.ToInt32(obtenido) <> 1 Then
                        MessageBox.Show("Otro puesto está dando un turno en esa franja. Espere unos segundos y vuelva a intentar.")
                        Exit Sub
                    End If
                End Using

                Try
                    GuardarTurno(cn, franja, motivo, aviso, idNuevo)
                Catch ex As MySqlException When ex.Number = 1213
                    'error 1213: otro puesto daba un turno en otra franja al mismo tiempo y la base freno uno de los dos
                    'no se guardo nada; el turno sigue cargado para volver a intentar
                    aviso = "Otro puesto estaba dando un turno al mismo tiempo y no se guardó este. Presione ""Dar turno"" otra vez."
                Finally
                    'libero el bloqueo de la franja pase lo que pase
                    Using cmd As New MySqlCommand("SELECT RELEASE_LOCK(@nombre);", cn)
                        cmd.Parameters.AddWithValue("@nombre", nombreBloqueo)
                        cmd.ExecuteScalar()
                    End Using
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al guardar el turno: " & ex.Message)
            Exit Sub
        End Try

        If aviso <> "" Then
            MessageBox.Show(aviso)
            'el cupo pudo cambiar: recargo la agenda y las horas libres, el turno nuevo sigue cargado
            Dim horaElegida As Date = franja
            CargarAgenda()
            cargando = True
            Try
                If Not CargarComboHoras(horaElegida) Then cboHora.DataSource = Nothing
            Finally
                cargando = False
            End Try
            Exit Sub
        End If

        'el turno guardado queda como el que se esta viendo
        hayCambios = False
        creando = False
        idTurno = idNuevo
        CargarAgenda()
        MostrarTurno(idNuevo)

        'ofrezco la confirmacion para WhatsApp en el mismo momento, que es cuando se le avisa al cliente
        Dim respuesta As DialogResult = MessageBox.Show(
            "Turno dado para el " & franja.ToString("dd/MM/yyyy 'a las' HH:mm") & "." & vbCrLf &
            "¿Copiar la confirmación para enviarla por WhatsApp?",
            "Turno dado", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If respuesta = DialogResult.Yes Then CopiarConfirmacion(idNuevo)
    End Sub

    Function ArmarConfirmacion(id As Integer, ByRef telefono As String) As String
        'armo el mensaje de confirmacion del turno para WhatsApp; devuelve "" si el turno no se pudo leer
        Dim cultura As New Globalization.CultureInfo("es-AR")
        Dim mano As String = Char.ConvertFromUtf32(&H1F44B)
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT t.fecha_hora, t.motivo, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "c.razon_social AS cliente, c.telefono " &
                    "FROM turno AS t " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = t.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "WHERE t.id_turno = @id_turno;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_turno", id)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If Not lector.Read() Then Return ""

                        telefono = lector("telefono").ToString()
                        Dim fechaHora As Date = CDate(lector("fecha_hora"))
                        Dim texto As String =
                            "¡Hola " & lector("cliente").ToString() & "! " & mano &
                            " Le escribimos de " & NOMBRE_TALLER & " para confirmar su turno del " &
                            fechaHora.ToString("dddd d 'de' MMMM", cultura) & " a las " & fechaHora.ToString("HH:mm") &
                            " para su " & lector("vehiculo").ToString() & " (patente " & lector("patente").ToString() & ")."
                        Dim motivo As String = lector("motivo").ToString()
                        If motivo <> "" Then texto = texto & vbCrLf & "Motivo: " & motivo & "."
                        texto = texto & vbCrLf &
                            "Le pedimos llegar unos minutos antes. Si no puede venir, avísenos por este medio así le damos el lugar a otro cliente." & vbCrLf &
                            "¡Lo esperamos! Saludos cordiales, el equipo de " & NOMBRE_TALLER & "."
                        Return texto
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MostrarError("No se pudo leer el turno para la confirmación.", ex)
            Return ""
        End Try
    End Function

    Sub CopiarConfirmacion(id As Integer)
        'copio la confirmacion del turno al portapapeles: desde ahi se pega en WhatsApp
        Dim telefono As String = ""
        Dim mensaje As String = ArmarConfirmacion(id, telefono)
        If mensaje = "" Then Exit Sub

        Try
            Clipboard.SetText(mensaje)
        Catch ex As Exception
            MessageBox.Show("No se pudo copiar la confirmación al portapapeles: " & ex.Message)
            Exit Sub
        End Try

        Dim destino As String = "al " & telefono
        If telefono = "" Then destino = "al cliente (no tiene teléfono cargado)"
        MessageBox.Show("La confirmación quedó copiada. Péguela en WhatsApp y envíela " & destino & ".",
                        "Confirmación del turno", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Function PrimerDiaConLugar() As Date
        'busco desde hoy, por los proximos 30 dias, el primer dia con una franja libre que no termino
        'devuelve Nothing si no hay ninguno
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                For i As Integer = 0 To 30
                    Dim candidato As Date = Date.Today.AddDays(i)
                    Dim franjas As List(Of Date) = FranjasDelDia(candidato)
                    If franjas.Count = 0 Then Continue For

                    'turnos que ocupan lugar en cada franja del dia
                    Dim ocupados As New Dictionary(Of Date, Integer)
                    Dim consulta As String =
                        "SELECT fecha_hora, COUNT(*) AS cantidad FROM turno " &
                        "WHERE fecha_hora >= @desde AND fecha_hora < @hasta AND estado <> 'CANCELADO' " &
                        "GROUP BY fecha_hora;"
                    Using cmd As New MySqlCommand(consulta, cn)
                        cmd.Parameters.AddWithValue("@desde", candidato)
                        cmd.Parameters.AddWithValue("@hasta", candidato.AddDays(1))
                        Using lector As MySqlDataReader = cmd.ExecuteReader
                            While lector.Read()
                                ocupados(CDate(lector("fecha_hora"))) = Convert.ToInt32(lector("cantidad"))
                            End While
                        End Using
                    End Using

                    For Each franja As Date In franjas
                        If franja.AddHours(1) <= Date.Now Then Continue For
                        Dim cantidad As Integer = 0
                        If ocupados.ContainsKey(franja) Then cantidad = ocupados(franja)
                        If cantidad < CUPO_POR_FRANJA Then Return candidato
                    Next
                Next
            End Using
        Catch ex As Exception
            MostrarError("No se pudieron buscar los lugares libres.", ex)
        End Try
        Return Nothing
    End Function

    Sub IniciarTurnoPedido()
        'otra pantalla pidio un turno para un vehiculo: abro el turno nuevo en el primer dia con lugar,
        'con la patente buscada y el motivo cargado
        Dim patente As String = PatenteInicial
        Dim motivo As String = MotivoInicial
        PatenteInicial = ""
        MotivoInicial = ""

        Dim diaLibre As Date = PrimerDiaConLugar()
        If diaLibre = Nothing Then
            MessageBox.Show("No hay lugares libres en los próximos 30 días.")
            Exit Sub
        End If

        If diaLibre <> dia Then CambiarDia(diaLibre)
        NuevoTurno()
        If Not creando Then Exit Sub

        txtPatente.Text = patente
        btnBuscarPatente.PerformClick()
        txtMotivo.Text = motivo
    End Sub

    Private Sub btnConfirmacion_Click(sender As Object, e As EventArgs) Handles btnConfirmacion.Click
        CopiarConfirmacion(idTurno)
    End Sub

    Sub GuardarTurno(cn As MySqlConnection, franja As Date, motivo As Object, ByRef aviso As String, ByRef idNuevo As Integer)
        'compruebo y guardo el turno nuevo en una transaccion; si no se puede, dejo el motivo en aviso
        'la llama btnGuardar con el bloqueo de la franja ya tomado
        Dim transaccion As MySqlTransaction = cn.BeginTransaction()
        Try
            'bloqueo el vehiculo: dos puestos no le dan dos turnos a la vez
            Dim activo As Object
            Using cmd As New MySqlCommand("SELECT activo FROM vehiculo WHERE id_vehiculo = @id_vehiculo FOR UPDATE;", cn, transaccion)
                cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo)
                activo = cmd.ExecuteScalar()
            End Using
            If activo Is Nothing OrElse Not Convert.ToBoolean(activo) Then
                aviso = "El vehículo fue dado de baja desde otro puesto."
            End If

            'un vehiculo tiene a lo sumo un turno pendiente de aca en adelante
            If aviso = "" Then
                Dim consulta As String =
                    "SELECT fecha_hora FROM turno " &
                    "WHERE id_vehiculo = @id_vehiculo AND estado = 'PENDIENTE' AND fecha_hora >= @desde " &
                    "ORDER BY fecha_hora LIMIT 1;"
                Using cmd As New MySqlCommand(consulta, cn, transaccion)
                    cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo)
                    'la franja en curso todavia cuenta como pendiente
                    cmd.Parameters.AddWithValue("@desde", Date.Now.AddHours(-1))
                    Dim otro As Object = cmd.ExecuteScalar()
                    If otro IsNot Nothing Then
                        aviso = "El vehículo ya tiene un turno pendiente el " &
                                CDate(otro).ToString("dd/MM/yyyy 'a las' HH:mm") & ". Cancélelo antes de dar otro."
                    End If
                End Using
            End If

            'cuento el cupo con los turnos de la franja bloqueados
            If aviso = "" Then
                Dim consulta As String =
                    "SELECT COUNT(*) FROM turno WHERE fecha_hora = @fecha_hora AND estado <> 'CANCELADO' FOR UPDATE;"
                Using cmd As New MySqlCommand(consulta, cn, transaccion)
                    cmd.Parameters.AddWithValue("@fecha_hora", franja)
                    If Convert.ToInt32(cmd.ExecuteScalar()) >= CUPO_POR_FRANJA Then
                        aviso = "La franja de las " & franja.ToString("HH:mm") & " se llenó desde otro puesto. Elija otra hora."
                    End If
                End Using
            End If

            If aviso = "" Then
                Dim consulta As String =
                    "INSERT INTO turno (id_vehiculo, fecha_hora, motivo, id_usuario) " &
                    "VALUES (@id_vehiculo, @fecha_hora, @motivo, @id_usuario);"
                Using cmd As New MySqlCommand(consulta, cn, transaccion)
                    cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo)
                    cmd.Parameters.AddWithValue("@fecha_hora", franja)
                    cmd.Parameters.AddWithValue("@motivo", motivo)
                    cmd.Parameters.AddWithValue("@id_usuario", Sesion.IdUsuario)
                    cmd.ExecuteNonQuery()
                    idNuevo = CInt(cmd.LastInsertedId)
                End Using
            End If
        Catch ex As Exception
            'algo fallo: deshago todo y dejo que el error llegue al mensaje de btnGuardar
            transaccion.Rollback()
            Throw
        End Try

        'confirmo fuera del Try de arriba: si el Commit falla no se intenta deshacer una transaccion ya cerrada
        If aviso = "" Then
            transaccion.Commit()
        Else
            transaccion.Rollback()
        End If
    End Sub

    Private Sub btnCancelarTurno_Click(sender As Object, e As EventArgs) Handles btnCancelarTurno.Click
        CambiarEstado("CANCELADO", "¿Cancelar este turno? El lugar queda libre para otro vehículo.",
                      "Cancelar turno", "Turno cancelado.")
    End Sub

    Private Sub btnAusente_Click(sender As Object, e As EventArgs) Handles btnAusente.Click
        CambiarEstado("AUSENTE", "¿Marcar el turno como ausente? El vehículo no vino a su turno.",
                      "Marcar ausente", "Turno marcado como ausente.")
    End Sub

End Class

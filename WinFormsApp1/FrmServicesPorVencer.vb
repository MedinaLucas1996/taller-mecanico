Imports MySqlConnector

Public Class FrmServicesPorVencer

    'lista los services que vencen (o ya vencieron) y arma el mensaje para avisarle al cliente por WhatsApp
    'el mensaje se copia al portapapeles y cada aviso queda registrado en aviso_service

    'indica si la lista cargada es la de vencidos o la de los que vencen pronto: cambia el texto del mensaje
    Private listaVencidos As Boolean = True

    Sub CargarServices()
        'cargo la grilla con los services del filtro elegido
        listaVencidos = rdbVencidos.Checked

        'vencidos: hasta hoy inclusive; proximos: desde manana hasta dentro de DIAS_PROXIMOS dias
        Dim desde As Date = New Date(1900, 1, 1)
        Dim hasta As Date = Date.Today
        If Not listaVencidos Then
            desde = Date.Today.AddDays(1)
            hasta = Date.Today.AddDays(DIAS_PROXIMOS)
        End If

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'primero los que no se avisaron hace poco, por fecha de vencimiento
                Dim consulta As String = ConsultaServices() &
                    "ORDER BY avisado_reciente, vence, v.patente, s.descripcion;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@desde", desde)
                    cmd.Parameters.AddWithValue("@hasta", hasta)
                    cmd.Parameters.AddWithValue("@limite_aviso", Date.Now.AddDays(-DIAS_AVISO_RECIENTE))

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'la grilla se muestra antes de enlazarla: oculta no arma sus columnas, y una carga anterior
                    'sin services la pudo dejar oculta
                    dgvServicios.Visible = True

                    'al cargar la tabla la grilla dispara DataBindingComplete, que pinta las filas
                    dgvServicios.DataSource = tabla

                    'las claves y la marca de aviso reciente sirven para el mensaje, no se muestran
                    dgvServicios.Columns("id_vehiculo").Visible = False
                    dgvServicios.Columns("id_servicio").Visible = False
                    dgvServicios.Columns("avisado_reciente").Visible = False

                    'pongo titulos legibles en las columnas
                    dgvServicios.Columns("cliente").HeaderText = "Cliente"
                    dgvServicios.Columns("telefono").HeaderText = "Teléfono"
                    dgvServicios.Columns("patente").HeaderText = "Patente"
                    dgvServicios.Columns("vehiculo").HeaderText = "Vehículo"
                    dgvServicios.Columns("servicio").HeaderText = "Service"
                    dgvServicios.Columns("ultimo").HeaderText = "Último"
                    dgvServicios.Columns("ultimo").DefaultCellStyle.Format = "dd/MM/yyyy"
                    dgvServicios.Columns("ultimo_km").HeaderText = "Km último"
                    dgvServicios.Columns("ultimo_km").DefaultCellStyle.Format = "N0"
                    dgvServicios.Columns("ultimo_km").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvServicios.Columns("proximo_km").HeaderText = "Próximo km"
                    dgvServicios.Columns("proximo_km").DefaultCellStyle.Format = "N0"
                    dgvServicios.Columns("proximo_km").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvServicios.Columns("vence").HeaderText = "Vence"
                    dgvServicios.Columns("vence").DefaultCellStyle.Format = "dd/MM/yyyy"
                    dgvServicios.Columns("aviso").HeaderText = "Aviso"

                    'las columnas de datos cortos se ajustan al contenido
                    dgvServicios.Columns("telefono").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvServicios.Columns("patente").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvServicios.Columns("ultimo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvServicios.Columns("vence").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                    'el resto de las columnas se reparte el ancho, con un minimo que entra en la pantalla mas chica
                    dgvServicios.Columns("cliente").FillWeight = 120
                    dgvServicios.Columns("cliente").MinimumWidth = 100
                    dgvServicios.Columns("vehiculo").FillWeight = 90
                    dgvServicios.Columns("vehiculo").MinimumWidth = 80
                    dgvServicios.Columns("servicio").FillWeight = 130
                    dgvServicios.Columns("servicio").MinimumWidth = 110
                    dgvServicios.Columns("ultimo_km").FillWeight = 55
                    dgvServicios.Columns("ultimo_km").MinimumWidth = 70
                    dgvServicios.Columns("proximo_km").FillWeight = 55
                    dgvServicios.Columns("proximo_km").MinimumWidth = 70
                    dgvServicios.Columns("aviso").FillWeight = 110
                    dgvServicios.Columns("aviso").MinimumWidth = 60

                    'en el subtitulo cuento los services, los vehiculos y los que ya se avisaron hace poco
                    Dim vehiculos As New List(Of Integer)
                    Dim recientes As Integer = 0
                    For Each registro As DataRow In tabla.Rows
                        Dim idVehiculo As Integer = CInt(registro("id_vehiculo"))
                        If Not vehiculos.Contains(idVehiculo) Then vehiculos.Add(idVehiculo)
                        If CInt(registro("avisado_reciente")) = 1 Then recientes = recientes + 1
                    Next

                    Dim textoServices As String = tabla.Rows.Count & " services"
                    If tabla.Rows.Count = 1 Then textoServices = "1 service"
                    Dim textoVehiculos As String = vehiculos.Count & " vehículos"
                    If vehiculos.Count = 1 Then textoVehiculos = "1 vehículo"
                    lblSubtitulo.Text = textoServices & " · " & textoVehiculos & " · " &
                                        recientes & " avisados en los últimos " & DIAS_AVISO_RECIENTE & " días"

                    'sin services, en lugar de la grilla vacia se ve el aviso
                    If listaVencidos Then
                        lblVacio.Text = "No hay services vencidos."
                    Else
                        lblVacio.Text = "No hay services que venzan en los próximos " & DIAS_PROXIMOS & " días."
                    End If
                    dgvServicios.Visible = tabla.Rows.Count > 0
                    lblVacio.Visible = tabla.Rows.Count = 0
                End Using
            End Using
        Catch ex As Exception
            MostrarError("No se pudieron cargar los services por vencer.", ex)
        End Try
    End Sub

    Function ArmarMensaje(filas As List(Of DataGridViewRow)) As String
        'armo el mensaje para WhatsApp con los services del vehiculo; todas las filas son del mismo vehiculo
        Dim cultura As New Globalization.CultureInfo("es-AR")
        Dim primera As DataGridViewRow = filas(0)
        Dim mano As String = Char.ConvertFromUtf32(&H1F44B)

        Dim texto As String =
            "¡Hola " & primera.Cells("cliente").Value.ToString() & "! " & mano &
            " Le escribimos de " & NOMBRE_TALLER & " para recordarle que a su " &
            primera.Cells("vehiculo").Value.ToString() & " (patente " & primera.Cells("patente").Value.ToString() & ") "

        If filas.Count = 1 Then
            'un solo service: va en la misma oracion, con la primera letra en minuscula
            Dim servicio As String = primera.Cells("servicio").Value.ToString()
            servicio = servicio.Substring(0, 1).ToLower() & servicio.Substring(1)
            If listaVencidos Then
                texto = texto & "ya le corresponde el service de " & servicio & ": " & UltimoService(primera, cultura) & "."
            Else
                texto = texto & "se acerca el service de " & servicio & ", que vence el " &
                        CDate(primera.Cells("vence").Value).ToString("dd/MM/yyyy") & ": " &
                        UltimoService(primera, cultura) & "."
            End If
        Else
            'varios services: uno por renglon, como una lista
            If listaVencidos Then
                texto = texto & "ya le corresponden estos services:"
            Else
                texto = texto & "se acercan estos services:"
            End If
            For Each fila As DataGridViewRow In filas
                texto = texto & vbCrLf & "• " & fila.Cells("servicio").Value.ToString() & ": "
                If listaVencidos Then
                    texto = texto & UltimoService(fila, cultura) & "."
                Else
                    texto = texto & "vence el " & CDate(fila.Cells("vence").Value).ToString("dd/MM/yyyy") &
                            " (" & UltimoService(fila, cultura) & ")."
                End If
            Next
        End If

        texto = texto & vbCrLf &
            "Mantener el service al día cuida el motor y evita sorpresas en la ruta. " &
            "Si le parece, le reservamos un turno cuando le quede cómodo." & vbCrLf &
            "¡Muchas gracias por confiar en nosotros! Saludos cordiales, el equipo de " & NOMBRE_TALLER & "."
        Return texto
    End Function

    Function UltimoService(fila As DataGridViewRow, cultura As Globalization.CultureInfo) As String
        'cuando se hizo el ultimo service y con cuantos km entro el auto
        Return "el último lo hicimos el " & CDate(fila.Cells("ultimo").Value).ToString("dd/MM/yyyy") &
               ", a los " & CInt(fila.Cells("ultimo_km").Value).ToString("N0", cultura) & " km"
    End Function

    Private Sub FrmServicesPorVencer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'iconos de los botones, si alguno falta ese boton queda solo con el texto
        btnExportar.Image = LeerIcono("exportar.png")

        'el encabezado de la lista lleva solo una linea clara debajo, como las filas
        dgvServicios.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single

        'solo el administrador y el operador avisan a los clientes (el menu ya oculta el boton)
        If Sesion.Rol <> ROL_ADMINISTRADOR AndAlso Sesion.Rol <> ROL_OPERADOR Then
            MessageBox.Show("Solo el administrador y el operador pueden avisar los services.")
            rdbVencidos.Enabled = False
            rdbProximos.Enabled = False
            btnCopiar.Enabled = False
            btnExportar.Enabled = False
            dgvServicios.Visible = False
            lblSubtitulo.Text = ""
        End If
    End Sub

    Private Sub FrmServicesPorVencer_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'sin permiso los botones quedaron deshabilitados en Load y la grilla no se carga
        If Not btnCopiar.Enabled Then Exit Sub

        'cargo la grilla recien ahora, cuando la pantalla ya tiene su tamaño final dentro de la ventana
        CargarServices()
    End Sub

    Private Sub Filtro_CheckedChanged(sender As Object, e As EventArgs) Handles _
        rdbVencidos.CheckedChanged, rdbProximos.CheckedChanged

        'al cambiar de opcion se disparan las dos: cargo una sola vez, con la que quedo marcada
        If Not CType(sender, RadioButton).Checked Then Exit Sub
        'el diseñador marca "Vencidos" al armar el formulario: esa vez no cargo, la primera carga es la de Shown
        If Not Me.Visible Then Exit Sub
        If Not btnCopiar.Enabled Then Exit Sub
        CargarServices()
    End Sub

    Private Sub dgvServicios_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvServicios.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas: las vuelvo a pintar
        If Not dgvServicios.Columns.Contains("avisado_reciente") Then Exit Sub

        'la grilla selecciona sola la primera fila, la desmarco
        dgvServicios.ClearSelection()

        For Each fila As DataGridViewRow In dgvServicios.Rows
            'los services avisados hace poco van en gris, tambien cuando la fila esta seleccionada
            If CInt(fila.Cells("avisado_reciente").Value) = 1 Then
                fila.DefaultCellStyle.ForeColor = Color.Gray
                fila.DefaultCellStyle.SelectionForeColor = Color.Gainsboro
            End If
        Next
    End Sub

    Private Sub dgvServicios_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvServicios.CellDoubleClick
        'un doble click sobre una fila copia su mensaje, igual que el boton
        If e.RowIndex < 0 Then Exit Sub
        btnCopiar.PerformClick()
    End Sub

    Private Sub btnCopiar_Click(sender As Object, e As EventArgs) Handles btnCopiar.Click
        'copio el mensaje del vehiculo elegido y registro un aviso por cada service que nombra
        If dgvServicios.SelectedRows.Count = 0 Then
            MessageBox.Show("Elija un service de la lista.")
            Exit Sub
        End If

        'junto todos los services listados del mismo vehiculo: van en un solo mensaje
        Dim elegida As DataGridViewRow = dgvServicios.SelectedRows(0)
        Dim idVehiculo As Integer = CInt(elegida.Cells("id_vehiculo").Value)
        Dim filas As New List(Of DataGridViewRow)
        Dim avisoAnterior As String = ""
        For Each fila As DataGridViewRow In dgvServicios.Rows
            If CInt(fila.Cells("id_vehiculo").Value) = idVehiculo Then
                filas.Add(fila)
                If avisoAnterior = "" Then avisoAnterior = fila.Cells("aviso").Value.ToString()
            End If
        Next

        'si ya se le aviso por alguno de estos services, pregunto antes de copiar y registrar otro aviso
        If avisoAnterior <> "" Then
            Dim respuesta As DialogResult = MessageBox.Show(
                avisoAnterior & "." & vbCrLf &
                "¿Copiar el mensaje otra vez y registrar un nuevo aviso?",
                "Aviso de service", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If respuesta = DialogResult.No Then Exit Sub
        End If

        Dim mensaje As String = ArmarMensaje(filas)

        'el portapapeles es el canal: desde ahi se pega en WhatsApp
        Try
            Clipboard.SetText(mensaje)
        Catch ex As Exception
            MessageBox.Show("No se pudo copiar el mensaje al portapapeles: " & ex.Message)
            Exit Sub
        End Try

        'registro un aviso por cada service del mensaje, todos juntos en una transaccion
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    Dim consulta As String =
                        "INSERT INTO aviso_service (id_vehiculo, id_servicio, id_usuario) " &
                        "VALUES (@id_vehiculo, @id_servicio, @id_usuario);"
                    For Each fila As DataGridViewRow In filas
                        Using cmd As New MySqlCommand(consulta, cn, transaccion)
                            cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo)
                            cmd.Parameters.AddWithValue("@id_servicio", CInt(fila.Cells("id_servicio").Value))
                            cmd.Parameters.AddWithValue("@id_usuario", Sesion.IdUsuario)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                Catch ex As Exception
                    'algo fallo antes de confirmar: deshago todo y dejo que el error llegue al mensaje de abajo
                    transaccion.Rollback()
                    Throw
                End Try

                'confirmo fuera del Try de arriba: si el Commit falla no se intenta deshacer una transaccion ya cerrada
                transaccion.Commit()
            End Using
        Catch ex As Exception
            MessageBox.Show("El mensaje quedó copiado, pero no se pudo registrar el aviso: " & ex.Message)
        End Try

        'si el cliente no tiene telefono cargado, el mensaje se le lee igual
        Dim telefono As String = elegida.Cells("telefono").Value.ToString()
        Dim destino As String = "al " & telefono
        If telefono = "" Then destino = "al cliente (no tiene teléfono cargado)"
        MessageBox.Show("El mensaje quedó copiado. Péguelo en WhatsApp y envíelo " & destino & ".",
                        "Aviso de service", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'recargo la lista: el vehiculo avisado pasa al final, en gris
        CargarServices()
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        'exporto lo que se ve en la grilla a un archivo CSV
        If listaVencidos Then
            ExportarGrilla(dgvServicios, "services-vencidos")
        Else
            ExportarGrilla(dgvServicios, "services-proximos")
        End If
    End Sub

    Private Sub FrmServicesPorVencer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'no hay campos que se editen en esta pantalla: el cierre nunca se cancela ni pregunta nada
    End Sub

End Class

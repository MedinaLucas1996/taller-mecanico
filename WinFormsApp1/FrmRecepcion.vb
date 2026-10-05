Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports MySqlConnector

Public Class FrmRecepcion

    'lado mayor maximo de las fotos guardadas, en pixeles
    Private Const LADO_MAXIMO As Integer = 1280

    'paso del asistente que se esta mostrando (1 a 4)
    Private pasoActual As Integer = 1

    'datos del vehiculo encontrado, quedan en 0 mientras no haya uno cargado
    Private idVehiculo As Integer = 0
    'titular actual del vehiculo, es el cliente que se guarda en la orden
    Private idCliente As Integer = 0
    'kilometraje minimo que acepta la orden nueva
    Private kmMinimo As Integer = 0

    'fuentes para marcar el paso actual en la fila de pasos
    Private fuentePasoActual As New Font("Segoe UI", 10.0F, FontStyle.Bold)
    Private fuentePasoNormal As New Font("Segoe UI", 10.0F, FontStyle.Regular)

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
                    'agrego una primera opcion con clave 0: la orden puede quedar sin mecanico
                    Dim filaSinAsignar As DataRow = tabla.NewRow()
                    filaSinAsignar("id_mecanico") = 0
                    filaSinAsignar("nombre_completo") = "Sin asignar"
                    tabla.Rows.InsertAt(filaSinAsignar, 0)

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

    Sub MostrarPaso(paso As Integer)
        'muestro el panel del paso pedido y acomodo los botones
        pasoActual = paso

        'el resumen se arma de nuevo cada vez que se llega al paso 4
        If paso = 4 Then ArmarResumen()

        'oculto todos los paneles y dejo visible solo el del paso actual
        pnlPaso1.Visible = False
        pnlPaso2.Visible = False
        pnlPaso3.Visible = False
        pnlPaso4.Visible = False
        If paso = 1 Then pnlPaso1.Visible = True
        If paso = 2 Then pnlPaso2.Visible = True
        If paso = 3 Then pnlPaso3.Visible = True
        If paso = 4 Then pnlPaso4.Visible = True

        'dejo todas las etiquetas de paso en gris...
        lblPaso1.Font = fuentePasoNormal
        lblPaso1.ForeColor = Color.Gray
        lblPaso2.Font = fuentePasoNormal
        lblPaso2.ForeColor = Color.Gray
        lblPaso3.Font = fuentePasoNormal
        lblPaso3.ForeColor = Color.Gray
        lblPaso4.Font = fuentePasoNormal
        lblPaso4.ForeColor = Color.Gray

        '...y marco en negrita y oscuro la del paso actual
        If paso = 1 Then
            lblPaso1.Font = fuentePasoActual
            lblPaso1.ForeColor = Color.FromArgb(30, 39, 46)
        End If
        If paso = 2 Then
            lblPaso2.Font = fuentePasoActual
            lblPaso2.ForeColor = Color.FromArgb(30, 39, 46)
        End If
        If paso = 3 Then
            lblPaso3.Font = fuentePasoActual
            lblPaso3.ForeColor = Color.FromArgb(30, 39, 46)
        End If
        If paso = 4 Then
            lblPaso4.Font = fuentePasoActual
            lblPaso4.ForeColor = Color.FromArgb(30, 39, 46)
        End If

        'en el primer paso no hay a donde volver
        If paso = 1 Then
            btnAnterior.Visible = False
        Else
            btnAnterior.Visible = True
        End If

        'en el ultimo paso se confirma, en los demas se avanza
        If paso = 4 Then
            btnSiguiente.Visible = False
            btnConfirmar.Visible = True
        Else
            btnSiguiente.Visible = True
            btnConfirmar.Visible = False
        End If

        'dejo el cursor en el primer campo del paso
        If paso = 1 Then txtPatente.Focus()
        If paso = 2 Then nudKmIngreso.Focus()
    End Sub

    Sub LimpiarVehiculo()
        'olvido el vehiculo cargado, la patente escrita no se toca
        idVehiculo = 0
        idCliente = 0
        kmMinimo = 0
        txtMarcaModelo.Clear()
        txtAnio.Clear()
        txtColor.Clear()
        txtKmActual.Clear()
        txtTitular.Clear()
        txtDocumento.Clear()
        dgvAnteriores.DataSource = Nothing
        lblKmMinimo.Text = "Mínimo: 0 km"
    End Sub

    Sub ReiniciarAsistente()
        'dejo todo vacio y vuelvo al paso 1

        'paso 1
        txtPatente.Clear()
        LimpiarVehiculo()

        'paso 2
        nudKmIngreso.Value = 0
        cboCombustible.SelectedIndex = 0
        If cboMecanico.Items.Count > 0 Then cboMecanico.SelectedIndex = 0
        txtSintoma.Clear()
        txtObservaciones.Clear()
        'al cambiar la fecha la casilla se marca sola, por eso la desmarco despues
        dtpFechaPrometida.Value = Date.Today
        dtpFechaPrometida.Checked = False

        'paso 3
        QuitarFoto(picFrente)
        QuitarFoto(picTrasera)
        QuitarFoto(picLateralIzq)
        QuitarFoto(picLateralDer)
        QuitarFoto(picTablero)

        'paso 4
        txtResumen.Clear()

        MostrarPaso(1)
    End Sub

    Function HayDatosCargados() As Boolean
        'indica si el operador ya cargo algo, para pedir confirmacion al cancelar
        If idVehiculo <> 0 Then Return True
        If txtPatente.Text.Trim <> "" Then Return True
        If cboCombustible.SelectedIndex > 0 Then Return True
        If cboMecanico.SelectedIndex > 0 Then Return True
        If txtSintoma.Text.Trim <> "" Then Return True
        If txtObservaciones.Text.Trim <> "" Then Return True
        If dtpFechaPrometida.Checked Then Return True
        If ListarFotos(True) <> "" Then Return True

        Return False
    End Function

    Function ValidarVehiculo() As Boolean
        'paso 1: tiene que haber un vehiculo cargado
        If idVehiculo = 0 Then
            MessageBox.Show("Busque un vehículo por su patente antes de continuar.")
            txtPatente.Focus()
            Return False
        End If

        Return True
    End Function

    Function ValidarDatosIngreso() As Boolean
        'paso 2: valido los datos de ingreso

        'el campo numerico no tiene decimales, por eso el valor siempre es un numero entero
        Dim km As Integer = CInt(nudKmIngreso.Value)

        'el kilometraje no puede ser menor al ultimo registrado (regla 8.3)
        If km < kmMinimo Then
            MessageBox.Show("El kilometraje de ingreso no puede ser menor a " & kmMinimo.ToString("N0") &
                            " km, que es el último registrado para este vehículo.")
            nudKmIngreso.Focus()
            Return False
        End If

        If txtSintoma.Text.Trim = "" Then
            MessageBox.Show("Falta el síntoma reportado por el cliente")
            txtSintoma.Focus()
            Return False
        End If

        'la fecha prometida es opcional, pero si se marca no puede ser anterior a hoy
        If dtpFechaPrometida.Checked AndAlso dtpFechaPrometida.Value.Date < Date.Today Then
            MessageBox.Show("La fecha prometida de entrega no puede ser anterior a hoy")
            dtpFechaPrometida.Focus()
            Return False
        End If

        Return True
    End Function

    Function CombustibleElegido() As Object
        'paso del texto del combo al valor que guarda la base
        If cboCombustible.SelectedIndex = 1 Then Return "VACIO"
        If cboCombustible.SelectedIndex = 2 Then Return "UN_CUARTO"
        If cboCombustible.SelectedIndex = 3 Then Return "MEDIO"
        If cboCombustible.SelectedIndex = 4 Then Return "TRES_CUARTOS"
        If cboCombustible.SelectedIndex = 5 Then Return "LLENO"

        'la opcion "Sin registrar" se guarda como NULL
        Return DBNull.Value
    End Function

    Function ListarFotos(cargadas As Boolean) As String
        'devuelve los nombres de las fotos cargadas (True) o de las que faltan (False)
        Dim lista As String = ""

        If (picFrente.Image IsNot Nothing) = cargadas Then lista = lista & "Frente, "
        If (picTrasera.Image IsNot Nothing) = cargadas Then lista = lista & "Trasera, "
        If (picLateralIzq.Image IsNot Nothing) = cargadas Then lista = lista & "Lateral izquierdo, "
        If (picLateralDer.Image IsNot Nothing) = cargadas Then lista = lista & "Lateral derecho, "
        If (picTablero.Image IsNot Nothing) = cargadas Then lista = lista & "Tablero, "

        'saco la coma y el espacio del final
        If lista <> "" Then lista = lista.Substring(0, lista.Length - 2)

        Return lista
    End Function

    Sub ArmarResumen()
        'armo el texto de solo lectura del paso 4 con todo lo cargado
        Dim texto As String = ""

        texto = texto & "VEHÍCULO" & vbCrLf
        texto = texto & "Patente: " & txtPatente.Text.Trim & vbCrLf
        texto = texto & "Marca y modelo: " & txtMarcaModelo.Text & vbCrLf
        texto = texto & "Año: " & txtAnio.Text & "    Color: " & txtColor.Text & vbCrLf
        texto = texto & "Titular: " & txtTitular.Text & " (documento " & txtDocumento.Text & ")" & vbCrLf
        texto = texto & vbCrLf

        texto = texto & "DATOS DE INGRESO" & vbCrLf
        texto = texto & "Km de ingreso: " & CInt(nudKmIngreso.Value).ToString("N0") & " km" & vbCrLf
        texto = texto & "Nivel de combustible: " & cboCombustible.Text & vbCrLf
        texto = texto & "Mecánico: " & cboMecanico.Text & vbCrLf

        If dtpFechaPrometida.Checked Then
            texto = texto & "Fecha prometida de entrega: " & dtpFechaPrometida.Value.ToString("dd/MM/yyyy") & vbCrLf
        Else
            texto = texto & "Fecha prometida de entrega: sin fecha" & vbCrLf
        End If

        texto = texto & "Síntoma reportado: " & txtSintoma.Text.Trim & vbCrLf

        If txtObservaciones.Text.Trim = "" Then
            texto = texto & "Observaciones de recepción: sin observaciones" & vbCrLf
        Else
            texto = texto & "Observaciones de recepción: " & txtObservaciones.Text.Trim & vbCrLf
        End If
        texto = texto & vbCrLf

        texto = texto & "FOTOS" & vbCrLf

        Dim cargadas As String = ListarFotos(True)
        If cargadas = "" Then cargadas = "ninguna"
        texto = texto & "Cargadas: " & cargadas & vbCrLf

        Dim faltantes As String = ListarFotos(False)
        If faltantes = "" Then faltantes = "ninguna"
        texto = texto & "Faltantes: " & faltantes

        txtResumen.Text = texto
    End Sub

    Function CargarImagenReducida(ruta As String) As Bitmap
        'leo una foto del disco y la devuelvo reducida a LADO_MAXIMO pixeles en su lado mayor
        'devuelve Nothing cuando el archivo no es una imagen valida
        Try
            'leo todo el archivo a memoria, asi no queda bloqueado en el disco
            Dim bytes() As Byte = File.ReadAllBytes(ruta)

            Using memoria As New MemoryStream(bytes)
                Using original As Image = Image.FromStream(memoria)
                    'las fotos de celular guardan la orientacion aparte (dato EXIF 274): la enderezo
                    If Array.IndexOf(original.PropertyIdList, &H112) >= 0 Then
                        Dim orientacion As Integer = original.GetPropertyItem(&H112).Value(0)
                        If orientacion = 3 Then original.RotateFlip(RotateFlipType.Rotate180FlipNone)
                        If orientacion = 6 Then original.RotateFlip(RotateFlipType.Rotate90FlipNone)
                        If orientacion = 8 Then original.RotateFlip(RotateFlipType.Rotate270FlipNone)
                    End If

                    'calculo el tamaño nuevo, la foto nunca se agranda
                    Dim ancho As Integer = original.Width
                    Dim alto As Integer = original.Height
                    Dim ladoMayor As Integer = ancho
                    If alto > ancho Then ladoMayor = alto

                    If ladoMayor > LADO_MAXIMO Then
                        ancho = CInt(original.Width * LADO_MAXIMO / ladoMayor)
                        alto = CInt(original.Height * LADO_MAXIMO / ladoMayor)
                        If ancho < 1 Then ancho = 1
                        If alto < 1 Then alto = 1
                    End If

                    'dibujo la foto en un bitmap nuevo, que ya no depende del archivo
                    Dim reducida As New Bitmap(ancho, alto, PixelFormat.Format24bppRgb)
                    Using dibujo As Graphics = Graphics.FromImage(reducida)
                        'fondo blanco para los PNG con transparencia
                        dibujo.Clear(Color.White)
                        dibujo.InterpolationMode = InterpolationMode.HighQualityBicubic
                        dibujo.DrawImage(original, 0, 0, ancho, alto)
                    End Using

                    Return reducida
                End Using
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Function ImagenABytes(imagen As Image) As Byte()
        'convierto la foto a JPEG para guardarla en la base
        Using memoria As New MemoryStream()
            imagen.Save(memoria, ImageFormat.Jpeg)
            Return memoria.ToArray()
        End Using
    End Function

    Sub CargarFoto(pic As PictureBox)
        'el operador elige una foto del disco y la muestro en el cuadro indicado
        If dlgFoto.ShowDialog() <> DialogResult.OK Then Exit Sub

        Dim foto As Bitmap = CargarImagenReducida(dlgFoto.FileName)

        'si el archivo no es una imagen, el cuadro queda como estaba
        If foto Is Nothing Then
            MessageBox.Show("El archivo elegido no es una imagen válida. Elija una foto JPG o PNG.")
            Exit Sub
        End If

        'libero la foto anterior antes de reemplazarla
        QuitarFoto(pic)
        pic.Image = foto
    End Sub

    Sub QuitarFoto(pic As PictureBox)
        'saco la foto del cuadro y libero su memoria
        If pic.Image Is Nothing Then Exit Sub

        Dim anterior As Image = pic.Image
        pic.Image = Nothing
        anterior.Dispose()
    End Sub

    Private Sub FrmRecepcion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'solo el administrador y el operador recepcionan vehiculos (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" AndAlso Sesion.Rol <> "OPERADOR" Then
            MessageBox.Show("Solo el administrador y el operador pueden recepcionar vehículos.")
            pnlTarjeta.Enabled = False
            btnCancelar.Enabled = False
            btnSiguiente.Enabled = False
            Exit Sub
        End If

        'cargo el combo y arranco en el paso 1 con todo vacio
        CargarComboMecanicos()
        ReiniciarAsistente()
    End Sub

    Private Sub txtPatente_TextChanged(sender As Object, e As EventArgs) Handles txtPatente.TextChanged
        'si cambian la patente despues de buscar, el vehiculo cargado ya no vale
        If idVehiculo <> 0 Then LimpiarVehiculo()
    End Sub

    Private Sub txtPatente_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPatente.KeyDown
        'Enter en la patente hace lo mismo que el boton Buscar
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnBuscar.PerformClick()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        'busco el vehiculo por patente

        'la patente se busca sin espacios y en mayusculas
        Dim patente As String = txtPatente.Text.Trim.ToUpper
        txtPatente.Text = patente

        'olvido el vehiculo de una busqueda anterior
        LimpiarVehiculo()

        If patente = "" Then
            MessageBox.Show("Ingrese la patente del vehículo")
            txtPatente.Focus()
            Exit Sub
        End If

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'datos del vehiculo, los guardo recien cuando pasan todas las validaciones
                Dim idVehiculoEncontrado As Integer
                Dim idClienteEncontrado As Integer
                Dim kmActual As Integer
                Dim marcaModelo As String
                Dim anio As String
                Dim colorVehiculo As String
                Dim titular As String
                Dim documento As String

                'busco el vehiculo activo con esa patente exacta, con su marca, modelo y titular
                Dim consulta As String =
                    "SELECT v.id_vehiculo, v.id_cliente, v.anio, v.color, v.km_actual, " &
                    "ma.descripcion AS marca, mo.descripcion AS modelo, c.razon_social, c.documento " &
                    "FROM vehiculo AS v " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "WHERE v.patente = @patente AND v.activo = 1;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@patente", patente)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If Not lector.Read() Then
                            MessageBox.Show("No hay un vehículo activo con la patente " & patente & "." & vbCrLf &
                                            "Regístrelo primero en ""Vehículos"" y vuelva a buscarlo.")
                            txtPatente.Focus()
                            Exit Sub
                        End If

                        idVehiculoEncontrado = CInt(lector("id_vehiculo"))
                        idClienteEncontrado = CInt(lector("id_cliente"))
                        kmActual = CInt(lector("km_actual"))
                        marcaModelo = lector("marca").ToString() & " " & lector("modelo").ToString()
                        'el año y el color pueden estar vacios en la base
                        anio = lector("anio").ToString()
                        colorVehiculo = lector("color").ToString()
                        titular = lector("razon_social").ToString()
                        documento = lector("documento").ToString()
                    End Using
                End Using

                'un vehiculo con una orden abierta (estado no final) no se recepciona de nuevo
                consulta =
                    "SELECT ot.nro_orden, e.descripcion " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.id_vehiculo = @id_vehiculo AND e.es_estado_final = 0 " &
                    "ORDER BY ot.nro_orden DESC LIMIT 1;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculoEncontrado)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If lector.Read() Then
                            MessageBox.Show("El vehículo " & patente & " ya tiene una orden de trabajo abierta: la N.º " &
                                            lector("nro_orden").ToString() & ", en estado " &
                                            lector("descripcion").ToString() & "." & vbCrLf &
                                            "No se puede recepcionar de nuevo hasta que esa orden se cierre.")
                            txtPatente.Focus()
                            Exit Sub
                        End If
                    End Using
                End Using

                'busco el kilometraje mas alto de las ordenes del vehiculo
                Dim kmUltimaOrden As Integer
                consulta = "SELECT COALESCE(MAX(km_ingreso), 0) FROM orden_trabajo WHERE id_vehiculo = @id_vehiculo;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculoEncontrado)
                    kmUltimaOrden = CInt(cmd.ExecuteScalar())
                End Using

                'cargo la grilla con las ordenes anteriores, la mas nueva primero
                consulta =
                    "SELECT ot.nro_orden, ot.fecha_recepcion, e.descripcion AS estado, ot.km_ingreso " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.id_vehiculo = @id_vehiculo " &
                    "ORDER BY ot.fecha_recepcion DESC, ot.nro_orden DESC;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculoEncontrado)

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    dgvAnteriores.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvAnteriores.Columns("nro_orden").HeaderText = "N.º de orden"
                    dgvAnteriores.Columns("fecha_recepcion").HeaderText = "Fecha de recepción"
                    dgvAnteriores.Columns("fecha_recepcion").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
                    dgvAnteriores.Columns("estado").HeaderText = "Estado"
                    dgvAnteriores.Columns("km_ingreso").HeaderText = "Km de ingreso"
                    dgvAnteriores.Columns("km_ingreso").DefaultCellStyle.Format = "N0"
                    dgvAnteriores.ClearSelection()
                End Using

                'muestro los datos del vehiculo y de su titular
                txtMarcaModelo.Text = marcaModelo
                txtAnio.Text = anio
                txtColor.Text = colorVehiculo
                txtKmActual.Text = kmActual.ToString("N0")
                txtTitular.Text = titular
                txtDocumento.Text = documento

                'el minimo es el mayor entre el km del vehiculo y el de su orden mas alta (regla 8.3)
                kmMinimo = kmActual
                If kmUltimaOrden > kmMinimo Then kmMinimo = kmUltimaOrden
                lblKmMinimo.Text = "Mínimo: " & kmMinimo.ToString("N0") & " km"
                'dejo el km de ingreso precargado con el minimo
                nudKmIngreso.Value = kmMinimo

                'recien ahora el vehiculo queda cargado y se puede avanzar
                idCliente = idClienteEncontrado
                idVehiculo = idVehiculoEncontrado
            End Using
        Catch ex As Exception
            'si algo fallo a mitad de camino no dejo un vehiculo a medio cargar
            LimpiarVehiculo()
            MessageBox.Show("Error al buscar el vehículo: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCargarFrente_Click(sender As Object, e As EventArgs) Handles btnCargarFrente.Click
        CargarFoto(picFrente)
    End Sub

    Private Sub btnQuitarFrente_Click(sender As Object, e As EventArgs) Handles btnQuitarFrente.Click
        QuitarFoto(picFrente)
    End Sub

    Private Sub btnCargarTrasera_Click(sender As Object, e As EventArgs) Handles btnCargarTrasera.Click
        CargarFoto(picTrasera)
    End Sub

    Private Sub btnQuitarTrasera_Click(sender As Object, e As EventArgs) Handles btnQuitarTrasera.Click
        QuitarFoto(picTrasera)
    End Sub

    Private Sub btnCargarLateralIzq_Click(sender As Object, e As EventArgs) Handles btnCargarLateralIzq.Click
        CargarFoto(picLateralIzq)
    End Sub

    Private Sub btnQuitarLateralIzq_Click(sender As Object, e As EventArgs) Handles btnQuitarLateralIzq.Click
        QuitarFoto(picLateralIzq)
    End Sub

    Private Sub btnCargarLateralDer_Click(sender As Object, e As EventArgs) Handles btnCargarLateralDer.Click
        CargarFoto(picLateralDer)
    End Sub

    Private Sub btnQuitarLateralDer_Click(sender As Object, e As EventArgs) Handles btnQuitarLateralDer.Click
        QuitarFoto(picLateralDer)
    End Sub

    Private Sub btnCargarTablero_Click(sender As Object, e As EventArgs) Handles btnCargarTablero.Click
        CargarFoto(picTablero)
    End Sub

    Private Sub btnQuitarTablero_Click(sender As Object, e As EventArgs) Handles btnQuitarTablero.Click
        QuitarFoto(picTablero)
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        'valido el paso actual antes de avanzar
        If pasoActual = 1 Then
            If Not ValidarVehiculo() Then Exit Sub
        End If

        If pasoActual = 2 Then
            If Not ValidarDatosIngreso() Then Exit Sub
        End If

        'las fotos del paso 3 son opcionales, nunca frenan el avance

        If pasoActual < 4 Then MostrarPaso(pasoActual + 1)
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        'vuelvo al paso anterior sin perder nada de lo cargado
        If pasoActual > 1 Then MostrarPaso(pasoActual - 1)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        'si ya hay algo cargado pido confirmacion antes de descartarlo
        If HayDatosCargados() Then
            Dim respuesta As DialogResult = MessageBox.Show(
                "¿Cancelar la recepción? Se pierden los datos y las fotos cargadas.",
                "Cancelar recepción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If respuesta = DialogResult.No Then Exit Sub
        End If

        ReiniciarAsistente()
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        'guardo la orden de trabajo con su historial y sus fotos

        'vuelvo a validar los pasos 1 y 2 por las dudas
        If Not ValidarVehiculo() Then
            MostrarPaso(1)
            Exit Sub
        End If

        If Not ValidarDatosIngreso() Then
            MostrarPaso(2)
            Exit Sub
        End If

        'las fotos no son obligatorias, pero aviso cuales faltan
        Dim faltantes As String = ListarFotos(False)
        If faltantes <> "" Then
            Dim respuesta As DialogResult = MessageBox.Show(
                "Faltan las fotos de: " & faltantes & "." & vbCrLf & "¿Continuar igual?",
                "Fotos faltantes",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If respuesta = DialogResult.No Then Exit Sub
        End If

        'el mecanico es opcional: la clave 0 es "Sin asignar" y se guarda NULL
        Dim idMecanico As Object = DBNull.Value
        If TypeOf cboMecanico.SelectedValue Is Integer AndAlso CInt(cboMecanico.SelectedValue) > 0 Then
            idMecanico = CInt(cboMecanico.SelectedValue)
        End If

        'las observaciones vacias se guardan como NULL
        Dim observaciones As Object = DBNull.Value
        If txtObservaciones.Text.Trim <> "" Then observaciones = txtObservaciones.Text.Trim

        'la fecha prometida sin marcar se guarda como NULL
        Dim fechaPrometida As Object = DBNull.Value
        If dtpFechaPrometida.Checked Then fechaPrometida = dtpFechaPrometida.Value.Date

        'angulos como los guarda la base y la foto cargada en cada uno, en el mismo orden
        Dim angulos() As String = {"FRENTE", "TRASERA", "LATERAL_IZQUIERDO", "LATERAL_DERECHO", "TABLERO"}
        Dim fotos() As Image = {picFrente.Image, picTrasera.Image, picLateralIzq.Image, picLateralDer.Image, picTablero.Image}

        Dim nroOrden As Integer

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'la orden, su historial y sus fotos se guardan todos juntos o ninguno
                Dim transaccion As MySqlTransaction = cn.BeginTransaction()
                Try
                    'busco la clave del estado RECEPCIONADA por su codigo
                    Dim idEstado As Integer
                    Dim consulta As String = "SELECT id_estado_ot FROM estado_ot WHERE codigo = 'RECEPCIONADA';"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        Dim resultado As Object = cmd.ExecuteScalar()
                        If resultado Is Nothing Then
                            Throw New Exception("no existe el estado RECEPCIONADA en la tabla estado_ot.")
                        End If
                        idEstado = CInt(resultado)
                    End Using

                    'calculo el proximo numero de orden
                    consulta = "SELECT COALESCE(MAX(nro_orden), 0) + 1 FROM orden_trabajo;"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        nroOrden = CInt(cmd.ExecuteScalar())
                    End Using

                    'guardo la orden de trabajo, la fecha de recepcion la pone la base
                    Dim idOrden As Integer
                    consulta =
                        "INSERT INTO orden_trabajo (nro_orden, id_vehiculo, id_cliente, id_mecanico, id_estado_ot, " &
                        "fecha_prometida, km_ingreso, nivel_combustible, sintoma_reportado, " &
                        "observaciones_recepcion, id_usuario_alta) " &
                        "VALUES (@nro_orden, @id_vehiculo, @id_cliente, @id_mecanico, @id_estado_ot, " &
                        "@fecha_prometida, @km_ingreso, @nivel_combustible, @sintoma_reportado, " &
                        "@observaciones_recepcion, @id_usuario_alta);"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@nro_orden", nroOrden)
                        cmd.Parameters.AddWithValue("@id_vehiculo", idVehiculo)
                        'el cliente de la orden es el titular en este momento (regla 8.4)
                        cmd.Parameters.AddWithValue("@id_cliente", idCliente)
                        cmd.Parameters.AddWithValue("@id_mecanico", idMecanico)
                        cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
                        cmd.Parameters.AddWithValue("@fecha_prometida", fechaPrometida)
                        cmd.Parameters.AddWithValue("@km_ingreso", CInt(nudKmIngreso.Value))
                        cmd.Parameters.AddWithValue("@nivel_combustible", CombustibleElegido())
                        cmd.Parameters.AddWithValue("@sintoma_reportado", txtSintoma.Text.Trim)
                        cmd.Parameters.AddWithValue("@observaciones_recepcion", observaciones)
                        cmd.Parameters.AddWithValue("@id_usuario_alta", Sesion.IdUsuario)
                        cmd.ExecuteNonQuery()

                        'clave que la base le dio a la orden nueva
                        idOrden = CInt(cmd.LastInsertedId)
                    End Using

                    'guardo la primera fila del historial de estados
                    consulta =
                        "INSERT INTO ot_historial_estado (id_orden_trabajo, id_estado_ot, id_usuario, observacion) " &
                        "VALUES (@id_orden_trabajo, @id_estado_ot, @id_usuario, @observacion);"
                    Using cmd As New MySqlCommand(consulta, cn, transaccion)
                        cmd.Parameters.AddWithValue("@id_orden_trabajo", idOrden)
                        cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
                        cmd.Parameters.AddWithValue("@id_usuario", Sesion.IdUsuario)
                        cmd.Parameters.AddWithValue("@observacion", "Recepción del vehículo")
                        cmd.ExecuteNonQuery()
                    End Using

                    'guardo una fila por cada foto cargada, la imagen va como JPEG
                    consulta =
                        "INSERT INTO ot_foto (id_orden_trabajo, angulo, imagen, id_usuario) " &
                        "VALUES (@id_orden_trabajo, @angulo, @imagen, @id_usuario);"
                    For i As Integer = 0 To 4
                        If fotos(i) IsNot Nothing Then
                            Using cmd As New MySqlCommand(consulta, cn, transaccion)
                                cmd.Parameters.AddWithValue("@id_orden_trabajo", idOrden)
                                cmd.Parameters.AddWithValue("@angulo", angulos(i))
                                cmd.Parameters.AddWithValue("@imagen", ImagenABytes(fotos(i)))
                                cmd.Parameters.AddWithValue("@id_usuario", Sesion.IdUsuario)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Next

                    'todo salio bien, confirmo los cambios
                    transaccion.Commit()
                Catch ex As Exception
                    'algo fallo: deshago todo y dejo que el error llegue a los mensajes de abajo
                    transaccion.Rollback()
                    Throw
                End Try
            End Using

        Catch ex As MySqlException When ex.Number = 1062
            'error 1062: otro puesto genero una orden con el mismo numero al mismo tiempo
            MessageBox.Show("Otro puesto registró una orden con el mismo número al mismo tiempo. " &
                            "No se guardó nada: presione ""Confirmar recepción"" nuevamente.")
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("No se pudo registrar la recepción y no se guardó ningún dato." & vbCrLf &
                            "Detalle: " & ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("Recepción registrada. Orden de trabajo N.º " & nroOrden & ".")

        'dejo el asistente vacio para recibir otro vehiculo
        ReiniciarAsistente()
    End Sub

End Class

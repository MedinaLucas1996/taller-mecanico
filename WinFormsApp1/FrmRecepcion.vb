Imports System.IO
Imports MySqlConnector

Public Class FrmRecepcion

    'paso del asistente que se esta mostrando (1 a 4)
    Private pasoActual As Integer = 1

    'datos del vehiculo encontrado, quedan en 0 mientras no haya uno cargado
    Private idVehiculo As Integer = 0
    'titular actual del vehiculo, es el cliente que se guarda en la orden
    Private idCliente As Integer = 0
    'kilometraje minimo que acepta la orden nueva
    Private kmMinimo As Integer = 0

    'paso mas avanzado al que se llego, decide que datos ya figuran en el resumen
    Private pasoAlcanzado As Integer = 1

    'iconos que se usan mas de una vez: la marca de paso completo y la camara del cuadro vacio
    Private iconoCheck As Image
    Private iconoCamara As Image

    Sub CargarIconos()
        'pongo los iconos de los botones, de las fotos y de las filas del resumen
        btnBuscar.Image = LeerIcono("buscar.png")
        btnRegistrarVehiculo.Image = LeerIcono("agregar.png")
        btnCancelar.Image = LeerIcono("cancelar-oscuro.png")
        btnAnterior.Image = LeerIcono("anterior-oscuro.png")
        btnSiguiente.Image = LeerIcono("siguiente.png")
        btnConfirmar.Image = LeerIcono("confirmar.png")

        btnCargarFrente.Image = LeerIcono("foto-agregar-oscuro.png")
        btnCargarTrasera.Image = LeerIcono("foto-agregar-oscuro.png")
        btnCargarLateralIzq.Image = LeerIcono("foto-agregar-oscuro.png")
        btnCargarLateralDer.Image = LeerIcono("foto-agregar-oscuro.png")
        btnCargarTablero.Image = LeerIcono("foto-agregar-oscuro.png")
        btnQuitarFrente.Image = LeerIcono("quitar-oscuro.png")
        btnQuitarTrasera.Image = LeerIcono("quitar-oscuro.png")
        btnQuitarLateralIzq.Image = LeerIcono("quitar-oscuro.png")
        btnQuitarLateralDer.Image = LeerIcono("quitar-oscuro.png")
        btnQuitarTablero.Image = LeerIcono("quitar-oscuro.png")

        picResVehiculo.Image = LeerIcono("vehiculo-oscuro.png")
        picResCliente.Image = LeerIcono("cliente-oscuro.png")
        picResKm.Image = LeerIcono("km-oscuro.png")
        picResCombustible.Image = LeerIcono("combustible-oscuro.png")
        picResMecanico.Image = LeerIcono("mecanico-oscuro.png")
        picResFecha.Image = LeerIcono("calendario-oscuro.png")
        picResFotos.Image = LeerIcono("camara-oscuro.png")
        picResSintoma.Image = LeerIcono("sintoma-oscuro.png")

        iconoCheck = LeerIcono("check-oscuro.png")
        iconoCamara = LeerIcono("camara-oscuro.png")
    End Sub

    Sub PintarPaso(celda As Panel, numero As Label, nombre As Label, paso As Integer)
        'dibujo una celda de la barra de pasos segun sea el paso actual, uno completo o uno que falta
        numero.Image = Nothing
        numero.Text = paso.ToString()

        If paso = pasoActual Then
            'paso actual: resaltado en oscuro
            celda.BackColor = Color.FromArgb(30, 39, 46)
            numero.ForeColor = Color.White
            nombre.ForeColor = Color.White
            celda.Cursor = Cursors.Default
        ElseIf paso < pasoActual Then
            'paso completo: lleva la marca en lugar del numero y se puede volver a el con un click
            celda.BackColor = Color.White
            numero.ForeColor = Color.SeaGreen
            nombre.ForeColor = Color.FromArgb(30, 39, 46)
            celda.Cursor = Cursors.Hand
            If iconoCheck IsNot Nothing Then
                numero.Image = iconoCheck
                numero.Text = ""
            Else
                numero.Text = "✓"
            End If
        Else
            'paso que falta: apagado, no responde al click
            celda.BackColor = Color.White
            numero.ForeColor = Color.Silver
            nombre.ForeColor = Color.Silver
            celda.Cursor = Cursors.Default
        End If
    End Sub

    Sub LimpiarVistasPrevias()
        'las miniaturas del paso 4 muestran las mismas fotos del paso 3: las suelto sin liberarlas
        picVistaFrente.Image = Nothing
        picVistaTrasera.Image = Nothing
        picVistaLateralIzq.Image = Nothing
        picVistaLateralDer.Image = Nothing
        picVistaTablero.Image = Nothing
    End Sub

    Function ContarFotos() As Integer
        'cuento cuantos de los cinco angulos tienen foto cargada
        Dim cantidad As Integer = 0
        If picFrente.Image IsNot Nothing Then cantidad = cantidad + 1
        If picTrasera.Image IsNot Nothing Then cantidad = cantidad + 1
        If picLateralIzq.Image IsNot Nothing Then cantidad = cantidad + 1
        If picLateralDer.Image IsNot Nothing Then cantidad = cantidad + 1
        If picTablero.Image IsNot Nothing Then cantidad = cantidad + 1
        Return cantidad
    End Function

    Sub ActualizarFotos()
        'despues de cargar o quitar una foto acomodo los cuadros del paso 3 y el resumen
        lblAyudaFotos.Text = "Son opcionales. Cargadas " & ContarFotos() & " de 5."

        '"Quitar" solo se ve en los angulos que tienen foto
        btnQuitarFrente.Visible = (picFrente.Image IsNot Nothing)
        btnQuitarTrasera.Visible = (picTrasera.Image IsNot Nothing)
        btnQuitarLateralIzq.Visible = (picLateralIzq.Image IsNot Nothing)
        btnQuitarLateralDer.Visible = (picLateralDer.Image IsNot Nothing)
        btnQuitarTablero.Visible = (picTablero.Image IsNot Nothing)

        ActualizarResumen()
    End Sub

    Sub ActualizarResumen()
        'lleno la tarjeta de resumen con lo cargado hasta ahora, lo que falta queda con un guion

        'el vehiculo y su titular figuran desde que se encuentra la patente
        If idVehiculo <> 0 Then
            lblResVehiculo.Text = txtPatente.Text.Trim & " - " & txtMarcaModelo.Text
            lblResCliente.Text = txtTitular.Text
        Else
            lblResVehiculo.Text = "-"
            lblResCliente.Text = "-"
        End If

        'los datos de ingreso figuran una vez que se paso el paso 2
        If pasoAlcanzado >= 3 Then
            lblResKm.Text = CInt(nudKmIngreso.Value).ToString("N0") & " km"
            lblResCombustible.Text = cboCombustible.Text
            lblResMecanico.Text = cboMecanico.Text
            If dtpFechaPrometida.Checked Then
                lblResFecha.Text = dtpFechaPrometida.Value.ToString("dd/MM/yyyy")
            Else
                lblResFecha.Text = "Sin fecha"
            End If
            lblResSintoma.Text = txtSintoma.Text.Trim
        Else
            lblResKm.Text = "-"
            lblResCombustible.Text = "-"
            lblResMecanico.Text = "-"
            lblResFecha.Text = "-"
            lblResSintoma.Text = "-"
        End If

        lblResFotos.Text = ContarFotos() & " de 5"
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
        If paso > pasoAlcanzado Then pasoAlcanzado = paso
        lblSubtitulo.Text = "Paso " & paso & " de 4"

        'el detalle se arma de nuevo cada vez que se llega al paso 4, con las miniaturas de las fotos
        If paso = 4 Then
            ArmarResumen()
            picVistaFrente.Image = picFrente.Image
            picVistaTrasera.Image = picTrasera.Image
            picVistaLateralIzq.Image = picLateralIzq.Image
            picVistaLateralDer.Image = picLateralDer.Image
            picVistaTablero.Image = picTablero.Image
        Else
            LimpiarVistasPrevias()
        End If

        'oculto todos los paneles y dejo visible solo el del paso actual
        pnlPaso1.Visible = False
        pnlPaso2.Visible = False
        pnlPaso3.Visible = False
        pnlPaso4.Visible = False
        If paso = 1 Then pnlPaso1.Visible = True
        If paso = 2 Then pnlPaso2.Visible = True
        If paso = 3 Then pnlPaso3.Visible = True
        If paso = 4 Then pnlPaso4.Visible = True

        'dibujo la barra de pasos: el actual resaltado, los completos con su marca y los que faltan apagados
        PintarPaso(pnlBarra1, lblNumPaso1, lblNomPaso1, 1)
        PintarPaso(pnlBarra2, lblNumPaso2, lblNomPaso2, 2)
        PintarPaso(pnlBarra3, lblNumPaso3, lblNomPaso3, 3)
        PintarPaso(pnlBarra4, lblNumPaso4, lblNomPaso4, 4)

        'al dejar un paso el resumen y los cuadros de fotos quedan al dia
        ActualizarFotos()

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

        'sin vehiculo se ve la ayuda en lugar de la tarjeta con sus datos y sus ordenes anteriores
        pnlVehiculo.Visible = False
        lblAnteriores.Visible = False
        dgvAnteriores.Visible = False
        lblAyudaPatente.Visible = True
        pnlNoRegistrada.Visible = False

        'los datos de ingreso eran de ese vehiculo: salen del resumen hasta volver a pasar por el paso 2
        pasoAlcanzado = 1
        ActualizarResumen()
    End Sub

    Sub ReiniciarAsistente()
        'dejo todo vacio y vuelvo al paso 1

        'las miniaturas del paso 4 dejan de apuntar a las fotos antes de liberarlas
        LimpiarVistasPrevias()

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
        ActualizarFotos()
    End Sub

    Sub QuitarFoto(pic As PictureBox)
        'saco la foto del cuadro y libero su memoria
        If pic.Image Is Nothing Then Exit Sub

        Dim anterior As Image = pic.Image
        pic.Image = Nothing
        anterior.Dispose()
        ActualizarFotos()
    End Sub

    Private Sub FrmRecepcion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'solo el administrador y el operador recepcionan vehiculos (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" AndAlso Sesion.Rol <> "OPERADOR" Then
            MessageBox.Show("Solo el administrador y el operador pueden recepcionar vehículos.")
            tlpPasos.Enabled = False
            pnlTarjeta.Enabled = False
            btnCancelar.Enabled = False
            btnSiguiente.Enabled = False
            Exit Sub
        End If

        'cargo los iconos, si alguno falta ese control queda sin icono
        CargarIconos()

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

                'busco el vehiculo con esa patente exacta, activo o no, con su marca, modelo y titular
                Dim consulta As String =
                    "SELECT v.id_vehiculo, v.id_cliente, v.anio, v.color, v.km_actual, v.activo, " &
                    "ma.descripcion AS marca, mo.descripcion AS modelo, c.razon_social, c.documento " &
                    "FROM vehiculo AS v " &
                    "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "WHERE v.patente = @patente;"

                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@patente", patente)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        If Not lector.Read() Then
                            'la patente no existe: aviso en el mismo paso y ofrezco registrarla sin salir de la recepcion
                            lblNoRegistrada.Text = "La patente " & patente & " no está registrada."
                            lblAyudaPatente.Visible = False
                            pnlNoRegistrada.Visible = True
                            btnRegistrarVehiculo.Focus()
                            Exit Sub
                        End If

                        'el vehiculo existe pero esta dado de baja: no se recepciona ni se registra de nuevo
                        If Not Convert.ToBoolean(lector("activo")) Then
                            MessageBox.Show("El vehículo con la patente " & patente & " está dado de baja." & vbCrLf &
                                            "Puede reactivarlo en ""Vehículos"", marcando ""Mostrar dados de baja"", y volver a buscarlo.")
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

                'muestro la tarjeta del vehiculo con sus ordenes anteriores y lo paso al resumen
                lblAyudaPatente.Visible = False
                pnlVehiculo.Visible = True
                lblAnteriores.Visible = True
                dgvAnteriores.Visible = True
                ActualizarResumen()
            End Using
        Catch ex As Exception
            'si algo fallo a mitad de camino no dejo un vehiculo a medio cargar
            LimpiarVehiculo()
            MessageBox.Show("Error al buscar el vehículo: " & ex.Message)
        End Try
    End Sub

    Private Sub btnRegistrarVehiculo_Click(sender As Object, e As EventArgs) Handles btnRegistrarVehiculo.Click
        'registro el vehiculo, y su titular si hace falta, sin salir de la recepcion
        Dim patenteRegistrada As String = ""

        Using formulario As New FrmAltaRapida
            'la ventana arranca con la patente que se busco
            formulario.Patente = txtPatente.Text.Trim
            If formulario.ShowDialog() <> DialogResult.OK Then Exit Sub
            patenteRegistrada = formulario.Patente
        End Using

        'busco la patente recien registrada con el boton de siempre: el paso sigue igual que con cualquier vehiculo
        txtPatente.Text = patenteRegistrada
        btnBuscar.PerformClick()
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

    Private Sub Foto_Click(sender As Object, e As EventArgs) Handles _
        picFrente.Click, picTrasera.Click, picLateralIzq.Click, picLateralDer.Click, picTablero.Click

        'un click en un cuadro vacio hace lo mismo que su boton "Cargar foto"
        Dim pic As PictureBox = CType(sender, PictureBox)
        If pic.Image Is Nothing Then CargarFoto(pic)
    End Sub

    Private Sub Foto_Paint(sender As Object, e As PaintEventArgs) Handles _
        picFrente.Paint, picTrasera.Paint, picLateralIzq.Paint, picLateralDer.Paint, picTablero.Paint

        'un cuadro sin foto se dibuja como un lugar para cargarla: borde punteado, camara y texto
        Dim pic As PictureBox = CType(sender, PictureBox)
        If pic.Image IsNot Nothing Then Exit Sub

        ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle, Color.DarkGray, ButtonBorderStyle.Dashed)

        'la camara va centrada, un poco mas arriba que el texto
        Dim centroX As Integer = pic.ClientSize.Width \ 2
        Dim centroY As Integer = pic.ClientSize.Height \ 2
        If iconoCamara IsNot Nothing Then
            e.Graphics.DrawImage(iconoCamara, centroX - iconoCamara.Width \ 2, centroY - iconoCamara.Height - 2)
        End If

        Dim zonaTexto As New Rectangle(0, centroY + 2, pic.ClientSize.Width, 20)
        TextRenderer.DrawText(e.Graphics, "Cargar foto", pic.Font, zonaTexto, Color.Gray,
                              TextFormatFlags.HorizontalCenter Or TextFormatFlags.Top)
    End Sub

    Private Sub BarraPaso_Click(sender As Object, e As EventArgs) Handles _
        pnlBarra1.Click, lblNumPaso1.Click, lblNomPaso1.Click,
        pnlBarra2.Click, lblNumPaso2.Click, lblNomPaso2.Click,
        pnlBarra3.Click, lblNumPaso3.Click, lblNomPaso3.Click,
        pnlBarra4.Click, lblNumPaso4.Click, lblNomPaso4.Click

        'un click en un paso ya completo vuelve a ese paso sin perder nada de lo cargado
        'hacia adelante no se salta: siempre se avanza con "Siguiente", que valida cada paso

        'el click puede venir de la celda o de una de sus dos etiquetas
        Dim origen As Control = CType(sender, Control)
        If TypeOf origen Is Label Then origen = origen.Parent

        Dim paso As Integer = 0
        If origen Is pnlBarra1 Then paso = 1
        If origen Is pnlBarra2 Then paso = 2
        If origen Is pnlBarra3 Then paso = 3
        If origen Is pnlBarra4 Then paso = 4

        If paso >= 1 AndAlso paso < pasoActual Then MostrarPaso(paso)
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

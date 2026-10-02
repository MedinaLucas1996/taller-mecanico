Imports System.IO
Imports MySqlConnector

Public Class FrmOrdenes

    'orden cuyas fotos se estan mostrando, queda en 0 mientras no haya una seleccionada
    Private idOrdenSeleccionada As Integer = 0

    Sub CargarComboEstados()
        'cargo los estados de la orden de trabajo en el combo, en el orden del flujo
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                Dim consulta As String =
                    "SELECT id_estado_ot, descripcion FROM estado_ot ORDER BY orden_flujo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using
                    'agrego una primera opcion con clave 0 que no filtra por estado
                    Dim filaTodos As DataRow = tabla.NewRow()
                    filaTodos("id_estado_ot") = 0
                    filaTodos("descripcion") = "Todos los estados"
                    tabla.Rows.InsertAt(filaTodos, 0)

                    'el usuario ve la descripcion...
                    cboEstado.DisplayMember = "descripcion"
                    '...pero el programa filtra por la clave numerica
                    cboEstado.ValueMember = "id_estado_ot"
                    cboEstado.DataSource = tabla
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar los estados: " & ex.Message)
        End Try
    End Sub

    Sub CargarResumen()
        'cargo la grilla chica con la cantidad de ordenes que hay en cada estado
        'el resumen cuenta siempre todas las ordenes, no mira los filtros
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'LEFT JOIN para que tambien aparezcan los estados que no tienen ordenes
                Dim consulta As String =
                    "SELECT e.descripcion AS estado, COUNT(ot.id_orden_trabajo) AS cantidad " &
                    "FROM estado_ot AS e " &
                    "LEFT JOIN orden_trabajo AS ot ON ot.id_estado_ot = e.id_estado_ot " &
                    "GROUP BY e.id_estado_ot, e.descripcion, e.orden_flujo " &
                    "ORDER BY e.orden_flujo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    dgvResumen.DataSource = tabla

                    'pongo titulos legibles en las columnas
                    dgvResumen.Columns("estado").HeaderText = "Estado"
                    dgvResumen.Columns("cantidad").HeaderText = "Cantidad"
                    'la cantidad es un numero corto, le dejo mas lugar al estado
                    dgvResumen.Columns("cantidad").FillWeight = 50
                    dgvResumen.ClearSelection()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar el resumen por estado: " & ex.Message)
        End Try
    End Sub

    Sub CargarOrdenes()
        'cargo la grilla de ordenes con los filtros elegidos, la mas nueva primero

        'al recargar la grilla ya no hay una orden seleccionada
        LimpiarFotos()
        dgvOrdenes.ClearSelection()

        'la clave 0 es "Todos los estados" y no filtra
        Dim idEstado As Integer = 0
        If cboEstado.SelectedIndex > 0 Then idEstado = CInt(cboEstado.SelectedValue)

        Dim texto As String = txtTexto.Text.Trim

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'nunca traigo las imagenes de ot_foto: se cargan recien al elegir una orden
                'el cliente es el de la orden (titular al recepcionar), no el titular actual del vehiculo
                'una orden esta demorada si su fecha prometida ya paso y su estado no es final
                Dim consulta As String =
                    "SELECT ot.id_orden_trabajo, ot.nro_orden, ot.fecha_recepcion, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "c.razon_social AS cliente, " &
                    "COALESCE(m.nombre_completo, 'Sin asignar') AS mecanico, " &
                    "e.descripcion AS estado, ot.fecha_prometida, " &
                    "CASE WHEN ot.fecha_prometida < @hoy AND e.es_estado_final = 0 " &
                    "THEN 'Demorada' ELSE '' END AS situacion " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = ot.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN cliente AS c ON c.id_cliente = ot.id_cliente " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "LEFT JOIN mecanico AS m ON m.id_mecanico = ot.id_mecanico " &
                    "WHERE 1 = 1 "

                'aplico el filtro por estado
                If idEstado > 0 Then
                    consulta = consulta & "AND ot.id_estado_ot = @id_estado_ot "
                End If

                'aplico el filtro por patente o nombre del cliente
                If texto <> "" Then
                    consulta = consulta & "AND (v.patente LIKE @texto OR c.razon_social LIKE @texto) "
                End If

                'aplico el filtro de solo demoradas, con la misma regla de la columna situacion
                If chkDemoradas.Checked Then
                    consulta = consulta & "AND ot.fecha_prometida < @hoy AND e.es_estado_final = 0 "
                End If

                consulta = consulta & "ORDER BY ot.fecha_recepcion DESC, ot.nro_orden DESC;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@hoy", Date.Today)
                    If idEstado > 0 Then cmd.Parameters.AddWithValue("@id_estado_ot", idEstado)
                    If texto <> "" Then cmd.Parameters.AddWithValue("@texto", "%" & texto & "%")

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'al cargar la tabla la grilla dispara DataBindingComplete, que pinta las demoradas
                    dgvOrdenes.DataSource = tabla

                    'la clave de la orden se usa al seleccionar la fila, no se muestra
                    dgvOrdenes.Columns("id_orden_trabajo").Visible = False

                    'pongo titulos legibles en las columnas
                    dgvOrdenes.Columns("nro_orden").HeaderText = "N.º"
                    dgvOrdenes.Columns("fecha_recepcion").HeaderText = "Recepción"
                    dgvOrdenes.Columns("fecha_recepcion").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
                    dgvOrdenes.Columns("patente").HeaderText = "Patente"
                    dgvOrdenes.Columns("vehiculo").HeaderText = "Vehículo"
                    dgvOrdenes.Columns("cliente").HeaderText = "Cliente"
                    dgvOrdenes.Columns("mecanico").HeaderText = "Mecánico"
                    dgvOrdenes.Columns("estado").HeaderText = "Estado"
                    dgvOrdenes.Columns("fecha_prometida").HeaderText = "Prometida"
                    dgvOrdenes.Columns("fecha_prometida").DefaultCellStyle.Format = "dd/MM/yyyy"
                    dgvOrdenes.Columns("situacion").HeaderText = "Situación"

                    'las columnas de datos cortos se ajustan al contenido,
                    'el vehiculo, el cliente y el mecanico se reparten el resto del ancho
                    dgvOrdenes.Columns("nro_orden").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("fecha_recepcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("patente").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("estado").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("fecha_prometida").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("situacion").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                    'muestro cuantas ordenes quedaron listadas
                    If tabla.Rows.Count = 1 Then
                        lblCantidad.Text = "1 orden listada"
                    Else
                        lblCantidad.Text = tabla.Rows.Count & " órdenes listadas"
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las órdenes de trabajo: " & ex.Message)
        End Try
    End Sub

    Sub VaciarCuadro(pic As PictureBox, aviso As Label, mostrarAviso As Boolean)
        'saco la foto del cuadro y libero su memoria
        If pic.Image IsNot Nothing Then
            Dim anterior As Image = pic.Image
            pic.Image = Nothing
            anterior.Dispose()
        End If

        'el aviso "Sin foto" solo se muestra cuando hay una orden seleccionada
        aviso.Text = "Sin foto"
        aviso.Visible = mostrarAviso
    End Sub

    Sub LimpiarFotos()
        'olvido la orden seleccionada y dejo los cinco cuadros vacios
        idOrdenSeleccionada = 0
        lblFotos.Text = "Seleccione una orden de la grilla para ver sus fotos de recepción."

        VaciarCuadro(picFrente, lblSinFrente, False)
        VaciarCuadro(picTrasera, lblSinTrasera, False)
        VaciarCuadro(picLateralIzq, lblSinLateralIzq, False)
        VaciarCuadro(picLateralDer, lblSinLateralDer, False)
        VaciarCuadro(picTablero, lblSinTablero, False)
    End Sub

    Sub MostrarFoto(pic As PictureBox, aviso As Label, bytes() As Byte)
        'paso los bytes guardados en la base a una imagen y la muestro en el cuadro
        Try
            Using memoria As New MemoryStream(bytes)
                Using original As Image = Image.FromStream(memoria)
                    'copio la foto a un bitmap nuevo, que ya no depende de la memoria leida
                    pic.Image = New Bitmap(original)
                End Using
            End Using
            aviso.Visible = False
        Catch ex As Exception
            'los bytes guardados no son una imagen valida: el cuadro queda vacio y lo aviso
            aviso.Text = "Foto ilegible"
            aviso.Visible = True
        End Try
    End Sub

    Private Sub FrmOrdenes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'solo el administrador y el operador consultan las ordenes (el menu ya oculta el boton)
        If Sesion.Rol <> "ADMINISTRADOR" AndAlso Sesion.Rol <> "OPERADOR" Then
            MessageBox.Show("Solo el administrador y el operador pueden consultar las órdenes de trabajo.")
            pnlFiltros.Enabled = False
            dgvResumen.Enabled = False
            dgvOrdenes.Enabled = False
            Exit Sub
        End If

        'cargo el combo, el resumen y la grilla al abrir el formulario
        CargarComboEstados()
        CargarResumen()
        CargarOrdenes()
    End Sub

    Private Sub txtTexto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTexto.KeyDown
        'Enter en el texto hace lo mismo que el boton Buscar
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnBuscar.PerformClick()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        'vuelvo a cargar el resumen y la grilla con los filtros elegidos
        CargarResumen()
        CargarOrdenes()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        'dejo los tres filtros como al abrir el formulario y recargo todo
        If cboEstado.Items.Count > 0 Then cboEstado.SelectedIndex = 0
        txtTexto.Clear()
        chkDemoradas.Checked = False

        CargarResumen()
        CargarOrdenes()
        txtTexto.Focus()
    End Sub

    Private Sub dgvOrdenes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvOrdenes.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas: marco las demoradas
        'se hace en este evento porque al ordenar por una columna las filas pierden su color
        If Not dgvOrdenes.Columns.Contains("situacion") Then Exit Sub

        'la grilla selecciona sola la primera fila, la desmarco
        dgvOrdenes.ClearSelection()

        For Each fila As DataGridViewRow In dgvOrdenes.Rows
            'fondo rojo suave para las ordenes demoradas
            If fila.Cells("situacion").Value.ToString() = "Demorada" Then
                fila.DefaultCellStyle.BackColor = Color.MistyRose
            End If

            'si se reordeno la grilla, vuelvo a marcar la orden cuyas fotos se estan mostrando
            If idOrdenSeleccionada <> 0 AndAlso CInt(fila.Cells("id_orden_trabajo").Value) = idOrdenSeleccionada Then
                fila.Selected = True
            End If
        Next
    End Sub

    Private Sub dgvOrdenes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellClick
        'cargo las fotos de la orden de la fila seleccionada
        'si e.RowIndex es mayor o igual a 0, se hizo click en una fila valida
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = dgvOrdenes.Rows(e.RowIndex)
        Dim idOrden As Integer = CInt(fila.Cells("id_orden_trabajo").Value)

        'si ya se estan mostrando las fotos de esa orden no las vuelvo a traer
        If idOrden = idOrdenSeleccionada Then Exit Sub

        'vacio los cinco cuadros, los que no tengan foto quedan con el aviso "Sin foto"
        VaciarCuadro(picFrente, lblSinFrente, True)
        VaciarCuadro(picTrasera, lblSinTrasera, True)
        VaciarCuadro(picLateralIzq, lblSinLateralIzq, True)
        VaciarCuadro(picLateralDer, lblSinLateralDer, True)
        VaciarCuadro(picTablero, lblSinTablero, True)

        idOrdenSeleccionada = idOrden
        lblFotos.Text = "Fotos de recepción de la orden N.º " & fila.Cells("nro_orden").Value.ToString()

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'traigo solo las fotos de esta orden, hay como mucho una por angulo
                Dim consulta As String =
                    "SELECT angulo, imagen FROM ot_foto WHERE id_orden_trabajo = @id_orden_trabajo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@id_orden_trabajo", idOrden)
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        While lector.Read()
                            Dim angulo As String = lector("angulo").ToString()
                            Dim bytes() As Byte = CType(lector("imagen"), Byte())

                            'cada angulo va a su cuadro
                            If angulo = "FRENTE" Then MostrarFoto(picFrente, lblSinFrente, bytes)
                            If angulo = "TRASERA" Then MostrarFoto(picTrasera, lblSinTrasera, bytes)
                            If angulo = "LATERAL_IZQUIERDO" Then MostrarFoto(picLateralIzq, lblSinLateralIzq, bytes)
                            If angulo = "LATERAL_DERECHO" Then MostrarFoto(picLateralDer, lblSinLateralDer, bytes)
                            If angulo = "TABLERO" Then MostrarFoto(picTablero, lblSinTablero, bytes)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            'si fallo la carga no dejo cuadros a medio llenar ni una orden marcada
            LimpiarFotos()
            dgvOrdenes.ClearSelection()
            MessageBox.Show("Error al cargar las fotos de la orden: " & ex.Message)
        End Try
    End Sub

End Class

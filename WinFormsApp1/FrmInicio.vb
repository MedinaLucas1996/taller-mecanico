Imports MySqlConnector

Public Class FrmInicio

    'pantalla de inicio del administrador y del operador: resume las ordenes que piden atencion hoy
    'es de solo consulta: ningun boton ni rutina de este formulario escribe en la base

    Sub PintarCantidad(etiqueta As Label, cantidad As Integer, colorConOrdenes As Color)
        'un contador en cero se ve apagado; con ordenes toma el color que le toca a su tarjeta
        etiqueta.Text = cantidad.ToString()
        If cantidad = 0 Then
            etiqueta.ForeColor = Color.Silver
        Else
            etiqueta.ForeColor = colorConOrdenes
        End If
    End Sub

    Sub CargarTarjetas()
        'cuento las ordenes de cada tarjeta; las tres reglas son las mismas que usa la columna situacion de la grilla
        Dim hoy As Integer = 0
        Dim demoradas As Integer = 0
        Dim listas As Integer = 0

        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()

                'prometidas para hoy: fecha prometida de hoy y estado no final
                Dim consulta As String =
                    "SELECT COUNT(*) FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.fecha_prometida = @hoy AND e.es_estado_final = 0;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@hoy", Date.Today)
                    hoy = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                'demoradas: fecha prometida ya pasada y estado no final, la misma regla que el tablero de ordenes
                consulta =
                    "SELECT COUNT(*) FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE ot.fecha_prometida < @hoy AND e.es_estado_final = 0;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@hoy", Date.Today)
                    demoradas = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                'listas para entregar: el trabajo termino y el vehiculo espera al cliente
                consulta =
                    "SELECT COUNT(*) FROM orden_trabajo AS ot " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE e.codigo = @codigo;"
                Using cmd As New MySqlCommand(consulta, cn)
                    cmd.Parameters.AddWithValue("@codigo", "FINALIZADA")
                    listas = Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar el resumen de órdenes: " & ex.Message)
        End Try

        PintarCantidad(lblCantHoy, hoy, Color.Black)
        PintarCantidad(lblCantDemoradas, demoradas, Color.Firebrick)
        PintarCantidad(lblCantListas, listas, Color.Black)
    End Sub

    Sub CargarOrdenes()
        'listo las ordenes de las tres tarjetas: primero las demoradas, despues las de hoy y al final las listas
        Try
            Using cn As New MySqlConnection(CADENA)
                cn.Open()
                'el cliente es el de la orden (titular al recepcionar), no el titular actual del vehiculo
                'una orden que cumple dos reglas (lista para entregar con fecha prometida vencida) aparece una sola vez,
                'con la situacion mas urgente
                Dim consulta As String =
                    "SELECT ot.id_orden_trabajo, ot.nro_orden, v.patente, " &
                    "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, " &
                    "c.razon_social AS cliente, e.descripcion AS estado, ot.fecha_prometida, " &
                    "CASE WHEN ot.fecha_prometida < @hoy AND e.es_estado_final = 0 THEN 'Demorada' " &
                    "WHEN ot.fecha_prometida = @hoy AND e.es_estado_final = 0 THEN 'Prometida hoy' " &
                    "ELSE 'Lista para entregar' END AS situacion, " &
                    "CASE WHEN ot.fecha_prometida < @hoy AND e.es_estado_final = 0 THEN 1 " &
                    "WHEN ot.fecha_prometida = @hoy AND e.es_estado_final = 0 THEN 2 " &
                    "ELSE 3 END AS orden_situacion " &
                    "FROM orden_trabajo AS ot " &
                    "JOIN vehiculo AS v ON v.id_vehiculo = ot.id_vehiculo " &
                    "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
                    "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
                    "JOIN cliente AS c ON c.id_cliente = ot.id_cliente " &
                    "JOIN estado_ot AS e ON e.id_estado_ot = ot.id_estado_ot " &
                    "WHERE (ot.fecha_prometida < @hoy AND e.es_estado_final = 0) " &
                    "OR (ot.fecha_prometida = @hoy AND e.es_estado_final = 0) " &
                    "OR e.codigo = @codigo " &
                    "ORDER BY orden_situacion, ot.fecha_prometida, ot.nro_orden;"

                Using cmd As New MySqlCommand(consulta, cn)
                    'evito SQL Injection usando parametros
                    cmd.Parameters.AddWithValue("@hoy", Date.Today)
                    cmd.Parameters.AddWithValue("@codigo", "FINALIZADA")

                    Dim tabla As New DataTable
                    Using lector As MySqlDataReader = cmd.ExecuteReader
                        tabla.Load(lector)
                    End Using

                    'al cargar la tabla la grilla dispara DataBindingComplete, que pinta las filas
                    dgvOrdenes.DataSource = tabla

                    'la clave y el orden de la situacion sirven para la lista, no se muestran
                    dgvOrdenes.Columns("id_orden_trabajo").Visible = False
                    dgvOrdenes.Columns("orden_situacion").Visible = False

                    'pongo titulos legibles en las columnas
                    dgvOrdenes.Columns("nro_orden").HeaderText = "N.º"
                    dgvOrdenes.Columns("patente").HeaderText = "Patente"
                    dgvOrdenes.Columns("vehiculo").HeaderText = "Vehículo"
                    dgvOrdenes.Columns("cliente").HeaderText = "Cliente"
                    dgvOrdenes.Columns("estado").HeaderText = "Estado"
                    dgvOrdenes.Columns("fecha_prometida").HeaderText = "Prometida"
                    dgvOrdenes.Columns("fecha_prometida").DefaultCellStyle.Format = "dd/MM/yyyy"
                    dgvOrdenes.Columns("situacion").HeaderText = "Situación"

                    'las columnas de datos cortos se ajustan al contenido
                    dgvOrdenes.Columns("nro_orden").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("patente").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("estado").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("fecha_prometida").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    dgvOrdenes.Columns("situacion").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                    'el vehiculo y el cliente se reparten el resto del ancho
                    dgvOrdenes.Columns("vehiculo").FillWeight = 110
                    dgvOrdenes.Columns("vehiculo").MinimumWidth = 110
                    dgvOrdenes.Columns("cliente").FillWeight = 140
                    dgvOrdenes.Columns("cliente").MinimumWidth = 120

                    'sin ordenes que atender, en lugar de la grilla vacia se ve el aviso
                    dgvOrdenes.Visible = tabla.Rows.Count > 0
                    lblVacio.Visible = tabla.Rows.Count = 0
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar las órdenes que requieren atención: " & ex.Message)
        End Try
    End Sub

    Private Sub FrmInicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'saludo al usuario que ingreso y muestro la fecha de hoy en castellano
        Dim cultura As New Globalization.CultureInfo("es-AR")
        lblSubtitulo.Text = "Hola, " & Sesion.NombreCompleto & " · " &
                            Date.Today.ToString("dddd d 'de' MMMM 'de' yyyy", cultura)
    End Sub

    Private Sub FrmInicio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'cargo las tarjetas y la grilla recien ahora, cuando la pantalla ya tiene su tamaño final dentro de la ventana
        CargarTarjetas()
        CargarOrdenes()
    End Sub

    Private Sub dgvOrdenes_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvOrdenes.DataBindingComplete
        'la grilla termino de cargar o de reordenar sus filas: las vuelvo a pintar segun su situacion
        If Not dgvOrdenes.Columns.Contains("situacion") Then Exit Sub

        'la grilla selecciona sola la primera fila, la desmarco
        dgvOrdenes.ClearSelection()

        For Each fila As DataGridViewRow In dgvOrdenes.Rows
            'rojo suave para las demoradas, amarillo suave para las de hoy y el color de FINALIZADA para las listas
            Dim situacion As String = fila.Cells("situacion").Value.ToString()
            If situacion = "Demorada" Then
                fila.DefaultCellStyle.BackColor = Color.MistyRose
            ElseIf situacion = "Prometida hoy" Then
                fila.DefaultCellStyle.BackColor = Color.LemonChiffon
            Else
                fila.DefaultCellStyle.BackColor = Color.PaleTurquoise
            End If
        Next
    End Sub

    Private Sub btnIrOrdenes_Click(sender As Object, e As EventArgs) Handles btnIrOrdenes.Click
        'abro las ordenes de trabajo a traves del menu principal, asi la opcion queda resaltada como si la hubieran elegido
        DirectCast(Me.ParentForm, FrmPrincipal).btnOrdenes.PerformClick()
    End Sub

    Private Sub FrmInicio_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'pantalla de solo consulta: no hay cambios que perder, el cierre nunca se cancela ni pregunta nada
    End Sub

End Class

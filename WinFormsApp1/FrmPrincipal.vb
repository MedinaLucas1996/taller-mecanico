Public Class FrmPrincipal

    'indica que el formulario se cierra por cerrar sesion y no por salir del sistema
    Private cerrandoSesion As Boolean = False

    Private Sub FrmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'muestro el usuario que inicio sesion
        lblUsuario.Text = "Usuario: " & Sesion.NombreCompleto & " (" & Sesion.Rol & ")"

        'las opciones del menu arrancan ocultas, muestro las que corresponden a cada rol

        'el administrador ve todo el menu
        If Sesion.Rol = "ADMINISTRADOR" Then
            lblTituloOperaciones.Visible = True
            btnRecepcion.Visible = True
            btnOrdenes.Visible = True
            btnHistorial.Visible = True
            lblTituloDatosMaestros.Visible = True
            btnClientes.Visible = True
            btnVehiculos.Visible = True
            btnMarcasModelos.Visible = True
            btnServicios.Visible = True
            btnCategorias.Visible = True
            btnMecanicos.Visible = True
            btnUsuarios.Visible = True
            lblTituloReportes.Visible = True
            btnReportes.Visible = True
        End If

        'el operador ve las operaciones y los datos de clientes y vehiculos
        If Sesion.Rol = "OPERADOR" Then
            lblTituloOperaciones.Visible = True
            btnRecepcion.Visible = True
            btnOrdenes.Visible = True
            btnHistorial.Visible = True
            lblTituloDatosMaestros.Visible = True
            btnClientes.Visible = True
            btnVehiculos.Visible = True
            btnMarcasModelos.Visible = True
        End If

        'el mecanico solo ve el historial
        If Sesion.Rol = "MECANICO" Then
            lblTituloOperaciones.Visible = True
            btnHistorial.Visible = True
        End If
    End Sub

    Private Sub AbrirFormulario(formulario As Form)
        'abro el formulario dentro del panel de contenido
        panelContenido.Controls.Clear()

        formulario.TopLevel = False
        formulario.FormBorderStyle = FormBorderStyle.None
        formulario.Dock = DockStyle.Fill

        panelContenido.Controls.Add(formulario)

        formulario.Show()
    End Sub

    Private Sub btnRecepcion_Click(sender As Object, e As EventArgs) Handles btnRecepcion.Click
        AbrirFormulario(New FrmRecepcion())
    End Sub

    Private Sub btnOrdenes_Click(sender As Object, e As EventArgs) Handles btnOrdenes.Click
        AbrirFormulario(New FrmOrdenes())
    End Sub

    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
        AbrirFormulario(New FrmClientes())
    End Sub

    Private Sub btnVehiculos_Click(sender As Object, e As EventArgs) Handles btnVehiculos.Click
        AbrirFormulario(New FrmVehiculos())
    End Sub

    Private Sub btnMarcasModelos_Click(sender As Object, e As EventArgs) Handles btnMarcasModelos.Click
        AbrirFormulario(New FrmMarcasModelos())
    End Sub

    Private Sub btnUsuarios_Click(sender As Object, e As EventArgs) Handles btnUsuarios.Click
        AbrirFormulario(New FrmUsuarios())
    End Sub

    Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        'pido confirmacion antes de cerrar la sesion
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Cerrar la sesión de " & Sesion.NombreUsuario & "?",
            "Cerrar sesión",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If respuesta = DialogResult.No Then Exit Sub

        'limpio los datos del usuario que estaba logueado
        Sesion.CerrarSesion()

        'muestro el login vacio, listo para que ingrese otro usuario
        cerrandoSesion = True
        FrmLogin.PrepararNuevoIngreso()

        Me.Close()
    End Sub

    Private Sub FrmPrincipal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'si se esta cerrando la sesion, el login ya quedo a la vista y la aplicacion sigue
        If cerrandoSesion Then Exit Sub

        'el login queda oculto al abrir el menu principal,
        'por eso al cerrar el menu termino toda la aplicacion
        Application.Exit()
    End Sub

End Class

Imports System.IO

Public Class FrmPrincipal

    'indica que el formulario se cierra por cerrar sesion y no por salir del sistema
    Private cerrandoSesion As Boolean = False

    'el menu arranca expandido, el boton de arriba lo contrae a una columna de iconos
    Private menuColapsado As Boolean = False

    'secciones del menu que ve el rol que ingreso, deciden que titulos se muestran
    Private verOperaciones As Boolean = False
    Private verReportes As Boolean = False
    Private verDatosMaestros As Boolean = False

    'los botones de opciones del menu, para recorrerlos con un For
    Private botonesMenu() As Button

    'iconos del boton que contrae y expande el menu
    Private iconoContraer As Image
    Private iconoExpandir As Image

    Function LeerIcono(archivo As String) As Image
        'leo un icono de la carpeta Recursos\iconos que esta junto al ejecutable
        Try
            Dim ruta As String = Path.Combine(AppContext.BaseDirectory, "Recursos", "iconos", archivo)
            Using imagen As Image = Image.FromFile(ruta)
                'copio el icono a un bitmap nuevo para no dejar el archivo abierto
                Return New Bitmap(imagen)
            End Using
        Catch ex As Exception
            'si el archivo falta o no se puede leer, la opcion queda sin icono y el sistema sigue
            Return Nothing
        End Try
    End Function

    Sub AplicarMenu()
        'acomodo el menu segun este expandido o contraido
        'el panel de contenido ocupa solo el ancho que el menu deja libre
        panelMenu.SuspendLayout()

        If menuColapsado Then
            'contraido: una columna angosta, sin el nombre del sistema
            panelMenu.Width = 56
            lblLogo.Visible = False
            btnMenu.Image = iconoExpandir
            tipMenu.SetToolTip(btnMenu, "Expandir menú")
        Else
            panelMenu.Width = 230
            lblLogo.Visible = True
            btnMenu.Image = iconoContraer
            tipMenu.SetToolTip(btnMenu, "Contraer menú")
        End If

        'si falta el icono del boton, muestro un simbolo para que se pueda seguir usando
        If btnMenu.Image Is Nothing Then
            btnMenu.Text = "≡"
        Else
            btnMenu.Text = ""
        End If

        'los titulos de seccion solo se ven con el menu expandido y si el rol tiene opciones en esa seccion
        lblTituloOperaciones.Visible = verOperaciones AndAlso Not menuColapsado
        lblTituloReportes.Visible = verReportes AndAlso Not menuColapsado
        lblTituloDatosMaestros.Visible = verDatosMaestros AndAlso Not menuColapsado

        For Each boton As Button In botonesMenu
            'el nombre de cada opcion esta guardado una sola vez, en el Tag del boton
            Dim nombre As String = boton.Tag.ToString()

            If menuColapsado Then
                'contraido: solo el icono centrado, el nombre aparece al pasar el mouse
                boton.Text = ""
                boton.TextImageRelation = TextImageRelation.Overlay
                boton.ImageAlign = ContentAlignment.MiddleCenter
                boton.TextAlign = ContentAlignment.MiddleCenter
                boton.Padding = Padding.Empty
                tipMenu.SetToolTip(boton, nombre)

                'una opcion sin icono muestra su inicial para no quedar vacia
                If boton.Image Is Nothing Then boton.Text = nombre.Substring(0, 1)
            Else
                'expandido: el icono a la izquierda y el nombre a continuacion
                boton.Text = "  " & nombre
                boton.TextImageRelation = TextImageRelation.ImageBeforeText
                boton.ImageAlign = ContentAlignment.MiddleLeft
                boton.TextAlign = ContentAlignment.MiddleLeft
                boton.Padding = New Padding(16, 0, 0, 0)
                tipMenu.SetToolTip(boton, "")
            End If
        Next

        panelMenu.ResumeLayout()
    End Sub

    Private Sub FrmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'muestro el usuario que inicio sesion
        lblUsuario.Text = "Usuario: " & Sesion.NombreCompleto & " (" & Sesion.Rol & ")"

        'junto los botones del menu en el orden en que se ven
        botonesMenu = {btnMisOrdenes, btnRecepcion, btnOrdenes, btnHistorial, btnReportes, btnClientes, btnVehiculos,
                       btnMarcasModelos, btnServicios, btnCategorias, btnMecanicos, btnUsuarios}

        'cargo el icono de cada opcion, si alguno falta ese boton queda sin icono
        btnMisOrdenes.Image = LeerIcono("mis-ordenes.png")
        btnRecepcion.Image = LeerIcono("recepcion.png")
        btnOrdenes.Image = LeerIcono("ordenes.png")
        btnHistorial.Image = LeerIcono("historial.png")
        btnReportes.Image = LeerIcono("reportes.png")
        btnClientes.Image = LeerIcono("clientes.png")
        btnVehiculos.Image = LeerIcono("vehiculos.png")
        btnMarcasModelos.Image = LeerIcono("marcas.png")
        btnServicios.Image = LeerIcono("servicios.png")
        btnCategorias.Image = LeerIcono("categorias.png")
        btnMecanicos.Image = LeerIcono("mecanicos.png")
        btnUsuarios.Image = LeerIcono("usuarios.png")
        btnCerrarSesion.Image = LeerIcono("cerrar-sesion.png")
        iconoContraer = LeerIcono("menu-contraer.png")
        iconoExpandir = LeerIcono("menu-expandir.png")

        'las opciones del menu arrancan ocultas, muestro las que corresponden a cada rol

        'el administrador ve todo el menu
        If Sesion.Rol = "ADMINISTRADOR" Then
            verOperaciones = True
            btnRecepcion.Visible = True
            btnOrdenes.Visible = True
            btnHistorial.Visible = True
            verDatosMaestros = True
            btnClientes.Visible = True
            btnVehiculos.Visible = True
            btnMarcasModelos.Visible = True
            btnServicios.Visible = True
            btnCategorias.Visible = True
            btnMecanicos.Visible = True
            btnUsuarios.Visible = True
            verReportes = True
            btnReportes.Visible = True
        End If

        'el operador ve las operaciones y los datos de clientes y vehiculos
        If Sesion.Rol = "OPERADOR" Then
            verOperaciones = True
            btnRecepcion.Visible = True
            btnOrdenes.Visible = True
            btnHistorial.Visible = True
            verDatosMaestros = True
            btnClientes.Visible = True
            btnVehiculos.Visible = True
            btnMarcasModelos.Visible = True
        End If

        'el mecanico ve sus ordenes asignadas y el historial
        If Sesion.Rol = "MECANICO" Then
            verOperaciones = True
            btnMisOrdenes.Visible = True
            btnHistorial.Visible = True
        End If

        'muestro los titulos de las secciones del rol y dejo el menu expandido
        AplicarMenu()
    End Sub

    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        'contraigo el menu a una columna de iconos, o lo vuelvo a expandir
        menuColapsado = Not menuColapsado
        AplicarMenu()
    End Sub

    Function CerrarPantallaActual() As Boolean
        'cierro la pantalla que esta abierta en el panel de contenido
        'devuelve False cuando esa pantalla no se dejo cerrar (por ejemplo, tiene cambios sin guardar)
        If panelContenido.Controls.Count = 0 Then Return True

        'recorro una copia porque al cerrar o quitar un control la coleccion del panel cambia
        Dim controles(panelContenido.Controls.Count - 1) As Control
        panelContenido.Controls.CopyTo(controles, 0)

        For Each control As Control In controles
            Dim pantalla As Form = TryCast(control, Form)

            If pantalla Is Nothing Then
                'no es una pantalla: es el texto de bienvenida, que se quita al abrir la primera
                panelContenido.Controls.Remove(control)
            Else
                'Close dispara el FormClosing de la pantalla; si nadie lo cancela, la pantalla se libera y sale del panel
                pantalla.Close()

                'si sigue en el panel es porque cancelo el cierre
                If panelContenido.Controls.Contains(pantalla) Then Return False
            End If
        Next

        Return True
    End Function

    Private Sub AbrirFormulario(formulario As Form)
        'abro el formulario dentro del panel de contenido, despues de cerrar la pantalla que estaba abierta
        If Not CerrarPantallaActual() Then
            'la pantalla actual se queda: descarto la que se iba a abrir
            formulario.Dispose()
            Exit Sub
        End If

        formulario.TopLevel = False
        formulario.FormBorderStyle = FormBorderStyle.None
        formulario.Dock = DockStyle.Fill

        panelContenido.Controls.Add(formulario)

        formulario.Show()
    End Sub

    Private Sub btnMisOrdenes_Click(sender As Object, e As EventArgs) Handles btnMisOrdenes.Click
        AbrirFormulario(New FrmMisOrdenes())
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

    Private Sub btnServicios_Click(sender As Object, e As EventArgs) Handles btnServicios.Click
        AbrirFormulario(New FrmServicios())
    End Sub

    Private Sub btnCategorias_Click(sender As Object, e As EventArgs) Handles btnCategorias.Click
        AbrirFormulario(New FrmCategorias())
    End Sub

    Private Sub btnMecanicos_Click(sender As Object, e As EventArgs) Handles btnMecanicos.Click
        AbrirFormulario(New FrmMecanicos())
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

        'cierro la pantalla abierta; si tiene cambios sin guardar y el usuario no los descarta, la sesion sigue
        If Not CerrarPantallaActual() Then Exit Sub

        'limpio los datos del usuario que estaba logueado
        Sesion.CerrarSesion()

        'muestro el login vacio, listo para que ingrese otro usuario
        cerrandoSesion = True
        FrmLogin.PrepararNuevoIngreso()

        Me.Close()
    End Sub

    Private Sub FrmPrincipal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'antes de cerrar la ventana principal cierro la pantalla abierta, que puede pedir que no se cierre
        'al cerrar sesion la pantalla ya se cerro, asi que aca no se vuelve a preguntar
        If Not CerrarPantallaActual() Then e.Cancel = True
    End Sub

    Private Sub FrmPrincipal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'si se esta cerrando la sesion, el login ya quedo a la vista y la aplicacion sigue
        If cerrandoSesion Then Exit Sub

        'el login queda oculto al abrir el menu principal,
        'por eso al cerrar el menu termino toda la aplicacion
        Application.Exit()
    End Sub

End Class

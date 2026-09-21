Public Class FrmPrincipal

    Private panelMenu As Panel
    Private panelSuperior As Panel
    Private panelContenido As Panel

    Public Sub New()
        InitializeComponent()
        ConfigurarPantalla()
    End Sub

    Private Sub ConfigurarPantalla()

        ' ==============================
        ' FORMULARIO PRINCIPAL
        ' ==============================

        Me.Text = "Taller Mecánico - Sistema de Gestión"
        Me.WindowState = FormWindowState.Maximized
        Me.MinimumSize = New Size(1100, 700)

        ' ==============================
        ' MENÚ LATERAL
        ' ==============================

        panelMenu = New Panel With {
            .Dock = DockStyle.Left,
            .Width = 230,
            .BackColor = Color.FromArgb(30, 39, 46)
        }

        ' Logo / título
        Dim panelLogo As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 100
        }

        Dim lblLogo As New Label With {
            .Text = "TALLER" & vbCrLf & "MECÁNICO",
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter
        }

        panelLogo.Controls.Add(lblLogo)
        panelMenu.Controls.Add(panelLogo)

        ' ------------------------------
        ' OPERACIONES
        ' ------------------------------

        AgregarTituloMenu("OPERACIONES")

        AgregarBotonMenu("Recepción")
        AgregarBotonMenu("Órdenes de trabajo")
        AgregarBotonMenu("Historial")

        ' ------------------------------
        ' DATOS MAESTROS
        ' ------------------------------

        AgregarTituloMenu("DATOS MAESTROS")

        AgregarBotonMenu(
            "Clientes",
            AddressOf BtnClientes_Click
        )

        AgregarBotonMenu(
            "Vehículos",
            AddressOf BtnVehiculos_Click
        )

        AgregarBotonMenu(
            "Marcas y modelos",
            AddressOf BtnMarcasModelos_Click
        )
        AgregarBotonMenu("Servicios")
        AgregarBotonMenu("Categorías")
        AgregarBotonMenu("Mecánicos")
        AgregarBotonMenu("Usuarios")

        ' ------------------------------
        ' REPORTES
        ' ------------------------------

        AgregarTituloMenu("REPORTES")

        AgregarBotonMenu("Reportes")

        ' ==============================
        ' BARRA SUPERIOR
        ' ==============================

        panelSuperior = New Panel With {
            .Dock = DockStyle.Top,
            .Height = 65,
            .BackColor = Color.White
        }

        Dim lblSistema As New Label With {
            .Text = "Sistema de Gestión",
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .AutoSize = True,
            .Location = New Point(25, 20)
        }

        ' Esto es solamente visual por ahora.
        Dim lblUsuario As New Label With {
            .Text = "Usuario: Administrador",
            .Font = New Font("Segoe UI", 10),
            .AutoSize = True,
            .Anchor = AnchorStyles.Top Or AnchorStyles.Right
        }

        lblUsuario.Location = New Point(650, 23)

        panelSuperior.Controls.Add(lblSistema)
        panelSuperior.Controls.Add(lblUsuario)

        ' ==============================
        ' ÁREA CENTRAL
        ' ==============================

        panelContenido = New Panel With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.FromArgb(245, 246, 250)
        }

        MostrarInicio()

        ' IMPORTANTE:
        ' primero Fill y Top,
        ' finalmente Left.
        Me.Controls.Add(panelContenido)
        Me.Controls.Add(panelSuperior)
        Me.Controls.Add(panelMenu)

    End Sub


    ' ==========================================
    ' CREACIÓN DE TÍTULOS DEL MENÚ
    ' ==========================================

    Private Sub AgregarTituloMenu(texto As String)

        Dim lbl As New Label With {
            .Text = texto,
            .Dock = DockStyle.Top,
            .Height = 40,
            .ForeColor = Color.FromArgb(150, 160, 170),
            .Font = New Font(
                "Segoe UI",
                9,
                FontStyle.Bold
            ),
            .Padding = New Padding(20, 15, 0, 0)
        }

        panelMenu.Controls.Add(lbl)
        lbl.BringToFront()

    End Sub


    ' ==========================================
    ' BOTÓN SIN EVENTO
    ' ==========================================

    Private Sub AgregarBotonMenu(texto As String)

        AgregarBotonMenu(texto, Nothing)

    End Sub


    ' ==========================================
    ' BOTÓN CON EVENTO
    ' ==========================================

    Private Sub AgregarBotonMenu(
        texto As String,
        evento As EventHandler
    )

        Dim btn As New Button With {
            .Text = "   " & texto,
            .Dock = DockStyle.Top,
            .Height = 46,
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(30, 39, 46),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 10),
            .TextAlign = ContentAlignment.MiddleLeft,
            .Cursor = Cursors.Hand
        }

        btn.FlatAppearance.BorderSize = 0

        btn.FlatAppearance.MouseOverBackColor =
            Color.FromArgb(52, 73, 94)

        If evento IsNot Nothing Then
            AddHandler btn.Click, evento
        End If

        panelMenu.Controls.Add(btn)
        btn.BringToFront()

    End Sub


    ' ==========================================
    ' PANTALLA DE INICIO
    ' ==========================================

    Private Sub MostrarInicio()

        panelContenido.Controls.Clear()

        Dim lblBienvenida As New Label With {
            .Text = "Bienvenido",
            .Font = New Font(
                "Segoe UI",
                28,
                FontStyle.Bold
            ),
            .AutoSize = True,
            .Location = New Point(60, 60)
        }

        Dim lblDescripcion As New Label With {
            .Text =
                "Sistema de Gestión para Taller Mecánico" &
                vbCrLf &
                "Seleccione una opción del menú para comenzar.",
            .Font = New Font("Segoe UI", 12),
            .AutoSize = True,
            .ForeColor = Color.DimGray,
            .Location = New Point(65, 120)
        }

        panelContenido.Controls.Add(lblBienvenida)
        panelContenido.Controls.Add(lblDescripcion)

    End Sub


    ' ==========================================
    ' ABRIR FORMULARIO DENTRO DEL PRINCIPAL
    ' ==========================================

    Private Sub AbrirFormulario(formulario As Form)

        panelContenido.Controls.Clear()

        formulario.TopLevel = False
        formulario.FormBorderStyle =
            FormBorderStyle.None

        formulario.Dock = DockStyle.Fill

        panelContenido.Controls.Add(formulario)

        formulario.Show()

    End Sub


    ' ==========================================
    ' CIERRE DEL SISTEMA
    ' ==========================================

    Private Sub FrmPrincipal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'el login queda oculto al abrir el menu principal,
        'por eso al cerrar el menu termino toda la aplicacion
        Application.Exit()
    End Sub


    ' ==========================================
    ' CLIENTES
    ' ==========================================

    Private Sub BtnClientes_Click(
        sender As Object,
        e As EventArgs
    )

        AbrirFormulario(New FrmClientes())

    End Sub


    ' ==========================================
    ' VEHÍCULOS
    ' ==========================================

    Private Sub BtnVehiculos_Click(
        sender As Object,
        e As EventArgs
    )

        AbrirFormulario(New FrmVehiculos())

    End Sub


    ' ==========================================
    ' MARCAS Y MODELOS
    ' ==========================================

    Private Sub BtnMarcasModelos_Click(
        sender As Object,
        e As EventArgs
    )

        AbrirFormulario(New FrmMarcasModelos())

    End Sub

End Class
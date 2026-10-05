<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrincipal
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        panelMenu = New Panel()
        panelMenuAbajo = New TableLayoutPanel()
        lblTituloDatosMaestros = New Label()
        btnClientes = New Button()
        btnVehiculos = New Button()
        btnMarcasModelos = New Button()
        btnServicios = New Button()
        btnCategorias = New Button()
        btnMecanicos = New Button()
        btnUsuarios = New Button()
        panelMenuArriba = New TableLayoutPanel()
        lblTituloOperaciones = New Label()
        btnRecepcion = New Button()
        btnOrdenes = New Button()
        btnHistorial = New Button()
        lblTituloReportes = New Label()
        btnReportes = New Button()
        panelLogo = New Panel()
        lblLogo = New Label()
        btnMenu = New Button()
        panelSuperior = New Panel()
        lblSistema = New Label()
        lblUsuario = New Label()
        btnCerrarSesion = New Button()
        panelContenido = New Panel()
        lblBienvenida = New Label()
        lblDescripcion = New Label()
        tipMenu = New ToolTip(components)
        panelMenu.SuspendLayout()
        panelMenuAbajo.SuspendLayout()
        panelMenuArriba.SuspendLayout()
        panelLogo.SuspendLayout()
        panelSuperior.SuspendLayout()
        panelContenido.SuspendLayout()
        SuspendLayout()
        '
        ' panelMenu
        '
        panelMenu.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        panelMenu.Controls.Add(panelMenuAbajo)
        panelMenu.Controls.Add(panelMenuArriba)
        panelMenu.Controls.Add(panelLogo)
        panelMenu.Dock = DockStyle.Left
        panelMenu.Location = New Point(0, 0)
        panelMenu.Name = "panelMenu"
        panelMenu.Size = New Size(230, 761)
        panelMenu.TabIndex = 0
        '
        ' panelMenuAbajo
        '
        panelMenuAbajo.AutoSize = True
        panelMenuAbajo.ColumnCount = 1
        panelMenuAbajo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        panelMenuAbajo.Controls.Add(lblTituloDatosMaestros, 0, 0)
        panelMenuAbajo.Controls.Add(btnClientes, 0, 1)
        panelMenuAbajo.Controls.Add(btnVehiculos, 0, 2)
        panelMenuAbajo.Controls.Add(btnMarcasModelos, 0, 3)
        panelMenuAbajo.Controls.Add(btnServicios, 0, 4)
        panelMenuAbajo.Controls.Add(btnCategorias, 0, 5)
        panelMenuAbajo.Controls.Add(btnMecanicos, 0, 6)
        panelMenuAbajo.Controls.Add(btnUsuarios, 0, 7)
        panelMenuAbajo.Dock = DockStyle.Bottom
        panelMenuAbajo.Location = New Point(0, 445)
        panelMenuAbajo.Name = "panelMenuAbajo"
        panelMenuAbajo.RowCount = 8
        panelMenuAbajo.RowStyles.Add(New RowStyle())
        panelMenuAbajo.RowStyles.Add(New RowStyle())
        panelMenuAbajo.RowStyles.Add(New RowStyle())
        panelMenuAbajo.RowStyles.Add(New RowStyle())
        panelMenuAbajo.RowStyles.Add(New RowStyle())
        panelMenuAbajo.RowStyles.Add(New RowStyle())
        panelMenuAbajo.RowStyles.Add(New RowStyle())
        panelMenuAbajo.RowStyles.Add(New RowStyle())
        panelMenuAbajo.Size = New Size(230, 316)
        panelMenuAbajo.TabIndex = 2
        '
        ' lblTituloDatosMaestros
        '
        lblTituloDatosMaestros.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        lblTituloDatosMaestros.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTituloDatosMaestros.ForeColor = Color.FromArgb(CByte(150), CByte(160), CByte(170))
        lblTituloDatosMaestros.Location = New Point(0, 0)
        lblTituloDatosMaestros.Margin = New Padding(0)
        lblTituloDatosMaestros.Name = "lblTituloDatosMaestros"
        lblTituloDatosMaestros.Padding = New Padding(20, 13, 0, 0)
        lblTituloDatosMaestros.Size = New Size(230, 36)
        lblTituloDatosMaestros.TabIndex = 0
        lblTituloDatosMaestros.Text = "DATOS MAESTROS"
        lblTituloDatosMaestros.Visible = False
        '
        ' btnClientes
        '
        btnClientes.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnClientes.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnClientes.Cursor = Cursors.Hand
        btnClientes.FlatAppearance.BorderSize = 0
        btnClientes.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnClientes.FlatStyle = FlatStyle.Flat
        btnClientes.Font = New Font("Segoe UI", 10F)
        btnClientes.ForeColor = Color.White
        btnClientes.ImageAlign = ContentAlignment.MiddleLeft
        btnClientes.Location = New Point(0, 36)
        btnClientes.Margin = New Padding(0)
        btnClientes.Name = "btnClientes"
        btnClientes.Padding = New Padding(16, 0, 0, 0)
        btnClientes.Size = New Size(230, 40)
        btnClientes.TabIndex = 1
        btnClientes.Tag = "Clientes"
        btnClientes.Text = "  Clientes"
        btnClientes.TextAlign = ContentAlignment.MiddleLeft
        btnClientes.TextImageRelation = TextImageRelation.ImageBeforeText
        btnClientes.UseVisualStyleBackColor = False
        btnClientes.Visible = False
        '
        ' btnVehiculos
        '
        btnVehiculos.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnVehiculos.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnVehiculos.Cursor = Cursors.Hand
        btnVehiculos.FlatAppearance.BorderSize = 0
        btnVehiculos.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnVehiculos.FlatStyle = FlatStyle.Flat
        btnVehiculos.Font = New Font("Segoe UI", 10F)
        btnVehiculos.ForeColor = Color.White
        btnVehiculos.ImageAlign = ContentAlignment.MiddleLeft
        btnVehiculos.Location = New Point(0, 76)
        btnVehiculos.Margin = New Padding(0)
        btnVehiculos.Name = "btnVehiculos"
        btnVehiculos.Padding = New Padding(16, 0, 0, 0)
        btnVehiculos.Size = New Size(230, 40)
        btnVehiculos.TabIndex = 2
        btnVehiculos.Tag = "Vehículos"
        btnVehiculos.Text = "  Vehículos"
        btnVehiculos.TextAlign = ContentAlignment.MiddleLeft
        btnVehiculos.TextImageRelation = TextImageRelation.ImageBeforeText
        btnVehiculos.UseVisualStyleBackColor = False
        btnVehiculos.Visible = False
        '
        ' btnMarcasModelos
        '
        btnMarcasModelos.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnMarcasModelos.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnMarcasModelos.Cursor = Cursors.Hand
        btnMarcasModelos.FlatAppearance.BorderSize = 0
        btnMarcasModelos.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnMarcasModelos.FlatStyle = FlatStyle.Flat
        btnMarcasModelos.Font = New Font("Segoe UI", 10F)
        btnMarcasModelos.ForeColor = Color.White
        btnMarcasModelos.ImageAlign = ContentAlignment.MiddleLeft
        btnMarcasModelos.Location = New Point(0, 116)
        btnMarcasModelos.Margin = New Padding(0)
        btnMarcasModelos.Name = "btnMarcasModelos"
        btnMarcasModelos.Padding = New Padding(16, 0, 0, 0)
        btnMarcasModelos.Size = New Size(230, 40)
        btnMarcasModelos.TabIndex = 3
        btnMarcasModelos.Tag = "Marcas y modelos"
        btnMarcasModelos.Text = "  Marcas y modelos"
        btnMarcasModelos.TextAlign = ContentAlignment.MiddleLeft
        btnMarcasModelos.TextImageRelation = TextImageRelation.ImageBeforeText
        btnMarcasModelos.UseVisualStyleBackColor = False
        btnMarcasModelos.Visible = False
        '
        ' btnServicios
        '
        btnServicios.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnServicios.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnServicios.Cursor = Cursors.Hand
        btnServicios.FlatAppearance.BorderSize = 0
        btnServicios.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnServicios.FlatStyle = FlatStyle.Flat
        btnServicios.Font = New Font("Segoe UI", 10F)
        btnServicios.ForeColor = Color.White
        btnServicios.ImageAlign = ContentAlignment.MiddleLeft
        btnServicios.Location = New Point(0, 156)
        btnServicios.Margin = New Padding(0)
        btnServicios.Name = "btnServicios"
        btnServicios.Padding = New Padding(16, 0, 0, 0)
        btnServicios.Size = New Size(230, 40)
        btnServicios.TabIndex = 4
        btnServicios.Tag = "Servicios"
        btnServicios.Text = "  Servicios"
        btnServicios.TextAlign = ContentAlignment.MiddleLeft
        btnServicios.TextImageRelation = TextImageRelation.ImageBeforeText
        btnServicios.UseVisualStyleBackColor = False
        btnServicios.Visible = False
        '
        ' btnCategorias
        '
        btnCategorias.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnCategorias.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnCategorias.Cursor = Cursors.Hand
        btnCategorias.FlatAppearance.BorderSize = 0
        btnCategorias.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnCategorias.FlatStyle = FlatStyle.Flat
        btnCategorias.Font = New Font("Segoe UI", 10F)
        btnCategorias.ForeColor = Color.White
        btnCategorias.ImageAlign = ContentAlignment.MiddleLeft
        btnCategorias.Location = New Point(0, 196)
        btnCategorias.Margin = New Padding(0)
        btnCategorias.Name = "btnCategorias"
        btnCategorias.Padding = New Padding(16, 0, 0, 0)
        btnCategorias.Size = New Size(230, 40)
        btnCategorias.TabIndex = 5
        btnCategorias.Tag = "Categorías"
        btnCategorias.Text = "  Categorías"
        btnCategorias.TextAlign = ContentAlignment.MiddleLeft
        btnCategorias.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCategorias.UseVisualStyleBackColor = False
        btnCategorias.Visible = False
        '
        ' btnMecanicos
        '
        btnMecanicos.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnMecanicos.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnMecanicos.Cursor = Cursors.Hand
        btnMecanicos.FlatAppearance.BorderSize = 0
        btnMecanicos.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnMecanicos.FlatStyle = FlatStyle.Flat
        btnMecanicos.Font = New Font("Segoe UI", 10F)
        btnMecanicos.ForeColor = Color.White
        btnMecanicos.ImageAlign = ContentAlignment.MiddleLeft
        btnMecanicos.Location = New Point(0, 236)
        btnMecanicos.Margin = New Padding(0)
        btnMecanicos.Name = "btnMecanicos"
        btnMecanicos.Padding = New Padding(16, 0, 0, 0)
        btnMecanicos.Size = New Size(230, 40)
        btnMecanicos.TabIndex = 6
        btnMecanicos.Tag = "Mecánicos"
        btnMecanicos.Text = "  Mecánicos"
        btnMecanicos.TextAlign = ContentAlignment.MiddleLeft
        btnMecanicos.TextImageRelation = TextImageRelation.ImageBeforeText
        btnMecanicos.UseVisualStyleBackColor = False
        btnMecanicos.Visible = False
        '
        ' btnUsuarios
        '
        btnUsuarios.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnUsuarios.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnUsuarios.Cursor = Cursors.Hand
        btnUsuarios.FlatAppearance.BorderSize = 0
        btnUsuarios.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnUsuarios.FlatStyle = FlatStyle.Flat
        btnUsuarios.Font = New Font("Segoe UI", 10F)
        btnUsuarios.ForeColor = Color.White
        btnUsuarios.ImageAlign = ContentAlignment.MiddleLeft
        btnUsuarios.Location = New Point(0, 276)
        btnUsuarios.Margin = New Padding(0)
        btnUsuarios.Name = "btnUsuarios"
        btnUsuarios.Padding = New Padding(16, 0, 0, 0)
        btnUsuarios.Size = New Size(230, 40)
        btnUsuarios.TabIndex = 7
        btnUsuarios.Tag = "Usuarios"
        btnUsuarios.Text = "  Usuarios"
        btnUsuarios.TextAlign = ContentAlignment.MiddleLeft
        btnUsuarios.TextImageRelation = TextImageRelation.ImageBeforeText
        btnUsuarios.UseVisualStyleBackColor = False
        btnUsuarios.Visible = False
        '
        ' panelMenuArriba
        '
        panelMenuArriba.AutoSize = True
        panelMenuArriba.ColumnCount = 1
        panelMenuArriba.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        panelMenuArriba.Controls.Add(lblTituloOperaciones, 0, 0)
        panelMenuArriba.Controls.Add(btnRecepcion, 0, 1)
        panelMenuArriba.Controls.Add(btnOrdenes, 0, 2)
        panelMenuArriba.Controls.Add(btnHistorial, 0, 3)
        panelMenuArriba.Controls.Add(lblTituloReportes, 0, 4)
        panelMenuArriba.Controls.Add(btnReportes, 0, 5)
        panelMenuArriba.Dock = DockStyle.Top
        panelMenuArriba.Location = New Point(0, 76)
        panelMenuArriba.Name = "panelMenuArriba"
        panelMenuArriba.RowCount = 6
        panelMenuArriba.RowStyles.Add(New RowStyle())
        panelMenuArriba.RowStyles.Add(New RowStyle())
        panelMenuArriba.RowStyles.Add(New RowStyle())
        panelMenuArriba.RowStyles.Add(New RowStyle())
        panelMenuArriba.RowStyles.Add(New RowStyle())
        panelMenuArriba.RowStyles.Add(New RowStyle())
        panelMenuArriba.Size = New Size(230, 232)
        panelMenuArriba.TabIndex = 1
        '
        ' lblTituloOperaciones
        '
        lblTituloOperaciones.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        lblTituloOperaciones.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTituloOperaciones.ForeColor = Color.FromArgb(CByte(150), CByte(160), CByte(170))
        lblTituloOperaciones.Location = New Point(0, 0)
        lblTituloOperaciones.Margin = New Padding(0)
        lblTituloOperaciones.Name = "lblTituloOperaciones"
        lblTituloOperaciones.Padding = New Padding(20, 13, 0, 0)
        lblTituloOperaciones.Size = New Size(230, 36)
        lblTituloOperaciones.TabIndex = 0
        lblTituloOperaciones.Text = "OPERACIONES"
        lblTituloOperaciones.Visible = False
        '
        ' btnRecepcion
        '
        btnRecepcion.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnRecepcion.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnRecepcion.Cursor = Cursors.Hand
        btnRecepcion.FlatAppearance.BorderSize = 0
        btnRecepcion.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnRecepcion.FlatStyle = FlatStyle.Flat
        btnRecepcion.Font = New Font("Segoe UI", 10F)
        btnRecepcion.ForeColor = Color.White
        btnRecepcion.ImageAlign = ContentAlignment.MiddleLeft
        btnRecepcion.Location = New Point(0, 36)
        btnRecepcion.Margin = New Padding(0)
        btnRecepcion.Name = "btnRecepcion"
        btnRecepcion.Padding = New Padding(16, 0, 0, 0)
        btnRecepcion.Size = New Size(230, 40)
        btnRecepcion.TabIndex = 1
        btnRecepcion.Tag = "Recepción"
        btnRecepcion.Text = "  Recepción"
        btnRecepcion.TextAlign = ContentAlignment.MiddleLeft
        btnRecepcion.TextImageRelation = TextImageRelation.ImageBeforeText
        btnRecepcion.UseVisualStyleBackColor = False
        btnRecepcion.Visible = False
        '
        ' btnOrdenes
        '
        btnOrdenes.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnOrdenes.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnOrdenes.Cursor = Cursors.Hand
        btnOrdenes.FlatAppearance.BorderSize = 0
        btnOrdenes.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnOrdenes.FlatStyle = FlatStyle.Flat
        btnOrdenes.Font = New Font("Segoe UI", 10F)
        btnOrdenes.ForeColor = Color.White
        btnOrdenes.ImageAlign = ContentAlignment.MiddleLeft
        btnOrdenes.Location = New Point(0, 76)
        btnOrdenes.Margin = New Padding(0)
        btnOrdenes.Name = "btnOrdenes"
        btnOrdenes.Padding = New Padding(16, 0, 0, 0)
        btnOrdenes.Size = New Size(230, 40)
        btnOrdenes.TabIndex = 2
        btnOrdenes.Tag = "Órdenes de trabajo"
        btnOrdenes.Text = "  Órdenes de trabajo"
        btnOrdenes.TextAlign = ContentAlignment.MiddleLeft
        btnOrdenes.TextImageRelation = TextImageRelation.ImageBeforeText
        btnOrdenes.UseVisualStyleBackColor = False
        btnOrdenes.Visible = False
        '
        ' btnHistorial
        '
        btnHistorial.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnHistorial.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnHistorial.Cursor = Cursors.Hand
        btnHistorial.FlatAppearance.BorderSize = 0
        btnHistorial.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnHistorial.FlatStyle = FlatStyle.Flat
        btnHistorial.Font = New Font("Segoe UI", 10F)
        btnHistorial.ForeColor = Color.White
        btnHistorial.ImageAlign = ContentAlignment.MiddleLeft
        btnHistorial.Location = New Point(0, 116)
        btnHistorial.Margin = New Padding(0)
        btnHistorial.Name = "btnHistorial"
        btnHistorial.Padding = New Padding(16, 0, 0, 0)
        btnHistorial.Size = New Size(230, 40)
        btnHistorial.TabIndex = 3
        btnHistorial.Tag = "Historial"
        btnHistorial.Text = "  Historial"
        btnHistorial.TextAlign = ContentAlignment.MiddleLeft
        btnHistorial.TextImageRelation = TextImageRelation.ImageBeforeText
        btnHistorial.UseVisualStyleBackColor = False
        btnHistorial.Visible = False
        '
        ' lblTituloReportes
        '
        lblTituloReportes.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        lblTituloReportes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTituloReportes.ForeColor = Color.FromArgb(CByte(150), CByte(160), CByte(170))
        lblTituloReportes.Location = New Point(0, 156)
        lblTituloReportes.Margin = New Padding(0)
        lblTituloReportes.Name = "lblTituloReportes"
        lblTituloReportes.Padding = New Padding(20, 13, 0, 0)
        lblTituloReportes.Size = New Size(230, 36)
        lblTituloReportes.TabIndex = 4
        lblTituloReportes.Text = "REPORTES"
        lblTituloReportes.Visible = False
        '
        ' btnReportes
        '
        btnReportes.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        btnReportes.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnReportes.Cursor = Cursors.Hand
        btnReportes.FlatAppearance.BorderSize = 0
        btnReportes.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnReportes.FlatStyle = FlatStyle.Flat
        btnReportes.Font = New Font("Segoe UI", 10F)
        btnReportes.ForeColor = Color.White
        btnReportes.ImageAlign = ContentAlignment.MiddleLeft
        btnReportes.Location = New Point(0, 192)
        btnReportes.Margin = New Padding(0)
        btnReportes.Name = "btnReportes"
        btnReportes.Padding = New Padding(16, 0, 0, 0)
        btnReportes.Size = New Size(230, 40)
        btnReportes.TabIndex = 5
        btnReportes.Tag = "Reportes"
        btnReportes.Text = "  Reportes"
        btnReportes.TextAlign = ContentAlignment.MiddleLeft
        btnReportes.TextImageRelation = TextImageRelation.ImageBeforeText
        btnReportes.UseVisualStyleBackColor = False
        btnReportes.Visible = False
        '
        ' panelLogo
        '
        panelLogo.Controls.Add(lblLogo)
        panelLogo.Controls.Add(btnMenu)
        panelLogo.Dock = DockStyle.Top
        panelLogo.Location = New Point(0, 0)
        panelLogo.Name = "panelLogo"
        panelLogo.Size = New Size(230, 76)
        panelLogo.TabIndex = 0
        '
        ' lblLogo
        '
        lblLogo.Dock = DockStyle.Fill
        lblLogo.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblLogo.ForeColor = Color.White
        lblLogo.Location = New Point(56, 0)
        lblLogo.Name = "lblLogo"
        lblLogo.Size = New Size(174, 76)
        lblLogo.TabIndex = 1
        lblLogo.Text = "TALLER" & vbCrLf & "MECÁNICO"
        lblLogo.TextAlign = ContentAlignment.MiddleLeft
        '
        ' btnMenu
        '
        btnMenu.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnMenu.Cursor = Cursors.Hand
        btnMenu.Dock = DockStyle.Left
        btnMenu.FlatAppearance.BorderSize = 0
        btnMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnMenu.FlatStyle = FlatStyle.Flat
        btnMenu.Font = New Font("Segoe UI", 14F)
        btnMenu.ForeColor = Color.White
        btnMenu.Location = New Point(0, 0)
        btnMenu.Name = "btnMenu"
        btnMenu.Size = New Size(56, 76)
        btnMenu.TabIndex = 0
        btnMenu.UseVisualStyleBackColor = False
        '
        ' panelSuperior
        '
        panelSuperior.BackColor = Color.White
        panelSuperior.Controls.Add(lblSistema)
        panelSuperior.Controls.Add(lblUsuario)
        panelSuperior.Controls.Add(btnCerrarSesion)
        panelSuperior.Dock = DockStyle.Top
        panelSuperior.Location = New Point(230, 0)
        panelSuperior.Name = "panelSuperior"
        panelSuperior.Padding = New Padding(0, 14, 20, 14)
        panelSuperior.Size = New Size(954, 65)
        panelSuperior.TabIndex = 1
        '
        ' lblSistema
        '
        lblSistema.AutoSize = True
        lblSistema.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblSistema.Location = New Point(25, 20)
        lblSistema.Name = "lblSistema"
        lblSistema.Size = New Size(180, 25)
        lblSistema.TabIndex = 0
        lblSistema.Text = "Sistema de Gestión"
        '
        ' lblUsuario
        '
        lblUsuario.Dock = DockStyle.Right
        lblUsuario.Font = New Font("Segoe UI", 10F)
        lblUsuario.Location = New Point(354, 14)
        lblUsuario.Name = "lblUsuario"
        lblUsuario.Padding = New Padding(0, 0, 15, 0)
        lblUsuario.Size = New Size(420, 37)
        lblUsuario.TabIndex = 1
        lblUsuario.Text = "Usuario:"
        lblUsuario.TextAlign = ContentAlignment.MiddleRight
        '
        ' btnCerrarSesion
        '
        btnCerrarSesion.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnCerrarSesion.Cursor = Cursors.Hand
        btnCerrarSesion.Dock = DockStyle.Right
        btnCerrarSesion.FlatAppearance.BorderSize = 0
        btnCerrarSesion.FlatStyle = FlatStyle.Flat
        btnCerrarSesion.Font = New Font("Segoe UI", 10F)
        btnCerrarSesion.ForeColor = Color.White
        btnCerrarSesion.Location = New Point(774, 14)
        btnCerrarSesion.Name = "btnCerrarSesion"
        btnCerrarSesion.Size = New Size(160, 37)
        btnCerrarSesion.TabIndex = 2
        btnCerrarSesion.Text = " Cerrar sesión"
        btnCerrarSesion.TextImageRelation = TextImageRelation.ImageBeforeText
        btnCerrarSesion.UseVisualStyleBackColor = False
        '
        ' panelContenido
        '
        panelContenido.BackColor = Color.FromArgb(CByte(245), CByte(246), CByte(250))
        panelContenido.Controls.Add(lblBienvenida)
        panelContenido.Controls.Add(lblDescripcion)
        panelContenido.Dock = DockStyle.Fill
        panelContenido.Location = New Point(230, 65)
        panelContenido.Name = "panelContenido"
        panelContenido.Size = New Size(954, 696)
        panelContenido.TabIndex = 2
        '
        ' lblBienvenida
        '
        lblBienvenida.AutoSize = True
        lblBienvenida.Font = New Font("Segoe UI", 28F, FontStyle.Bold)
        lblBienvenida.Location = New Point(60, 60)
        lblBienvenida.Name = "lblBienvenida"
        lblBienvenida.Size = New Size(224, 51)
        lblBienvenida.TabIndex = 0
        lblBienvenida.Text = "Bienvenido"
        '
        ' lblDescripcion
        '
        lblDescripcion.AutoSize = True
        lblDescripcion.Font = New Font("Segoe UI", 12F)
        lblDescripcion.ForeColor = Color.DimGray
        lblDescripcion.Location = New Point(65, 120)
        lblDescripcion.Name = "lblDescripcion"
        lblDescripcion.Size = New Size(343, 42)
        lblDescripcion.TabIndex = 1
        lblDescripcion.Text = "Sistema de Gestión para Taller Mecánico" & vbCrLf & "Seleccione una opción del menú para comenzar."
        '
        ' FrmPrincipal
        '
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1184, 761)
        Controls.Add(panelContenido)
        Controls.Add(panelSuperior)
        Controls.Add(panelMenu)
        MinimumSize = New Size(1100, 700)
        Name = "FrmPrincipal"
        Text = "Taller Mecánico - Sistema de Gestión"
        WindowState = FormWindowState.Maximized
        panelMenu.ResumeLayout(False)
        panelMenuAbajo.ResumeLayout(False)
        panelMenuAbajo.PerformLayout()
        panelMenuArriba.ResumeLayout(False)
        panelMenuArriba.PerformLayout()
        panelLogo.ResumeLayout(False)
        panelSuperior.ResumeLayout(False)
        panelSuperior.PerformLayout()
        panelContenido.ResumeLayout(False)
        panelContenido.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents panelMenu As Panel
    Friend WithEvents panelMenuAbajo As TableLayoutPanel
    Friend WithEvents lblTituloDatosMaestros As Label
    Friend WithEvents btnClientes As Button
    Friend WithEvents btnVehiculos As Button
    Friend WithEvents btnMarcasModelos As Button
    Friend WithEvents btnServicios As Button
    Friend WithEvents btnCategorias As Button
    Friend WithEvents btnMecanicos As Button
    Friend WithEvents btnUsuarios As Button
    Friend WithEvents panelMenuArriba As TableLayoutPanel
    Friend WithEvents lblTituloOperaciones As Label
    Friend WithEvents btnRecepcion As Button
    Friend WithEvents btnOrdenes As Button
    Friend WithEvents btnHistorial As Button
    Friend WithEvents lblTituloReportes As Label
    Friend WithEvents btnReportes As Button
    Friend WithEvents panelLogo As Panel
    Friend WithEvents lblLogo As Label
    Friend WithEvents btnMenu As Button
    Friend WithEvents panelSuperior As Panel
    Friend WithEvents lblSistema As Label
    Friend WithEvents lblUsuario As Label
    Friend WithEvents btnCerrarSesion As Button
    Friend WithEvents panelContenido As Panel
    Friend WithEvents lblBienvenida As Label
    Friend WithEvents lblDescripcion As Label
    Friend WithEvents tipMenu As ToolTip
End Class

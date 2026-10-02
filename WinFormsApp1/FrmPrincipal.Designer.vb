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
        panelMenu = New Panel()
        btnReportes = New Button()
        lblTituloReportes = New Label()
        btnUsuarios = New Button()
        btnMecanicos = New Button()
        btnCategorias = New Button()
        btnServicios = New Button()
        btnMarcasModelos = New Button()
        btnVehiculos = New Button()
        btnClientes = New Button()
        lblTituloDatosMaestros = New Label()
        btnHistorial = New Button()
        btnOrdenes = New Button()
        btnRecepcion = New Button()
        lblTituloOperaciones = New Label()
        panelLogo = New Panel()
        lblLogo = New Label()
        panelSuperior = New Panel()
        lblSistema = New Label()
        lblUsuario = New Label()
        btnCerrarSesion = New Button()
        panelContenido = New Panel()
        lblBienvenida = New Label()
        lblDescripcion = New Label()
        panelMenu.SuspendLayout()
        panelLogo.SuspendLayout()
        panelSuperior.SuspendLayout()
        panelContenido.SuspendLayout()
        SuspendLayout()
        '
        ' panelMenu
        '
        panelMenu.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        panelMenu.Controls.Add(btnReportes)
        panelMenu.Controls.Add(lblTituloReportes)
        panelMenu.Controls.Add(btnUsuarios)
        panelMenu.Controls.Add(btnMecanicos)
        panelMenu.Controls.Add(btnCategorias)
        panelMenu.Controls.Add(btnServicios)
        panelMenu.Controls.Add(btnMarcasModelos)
        panelMenu.Controls.Add(btnVehiculos)
        panelMenu.Controls.Add(btnClientes)
        panelMenu.Controls.Add(lblTituloDatosMaestros)
        panelMenu.Controls.Add(btnHistorial)
        panelMenu.Controls.Add(btnOrdenes)
        panelMenu.Controls.Add(btnRecepcion)
        panelMenu.Controls.Add(lblTituloOperaciones)
        panelMenu.Controls.Add(panelLogo)
        panelMenu.Dock = DockStyle.Left
        panelMenu.Location = New Point(0, 0)
        panelMenu.Name = "panelMenu"
        panelMenu.Size = New Size(230, 761)
        panelMenu.TabIndex = 0
        '
        ' btnReportes
        '
        btnReportes.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnReportes.Cursor = Cursors.Hand
        btnReportes.Dock = DockStyle.Top
        btnReportes.FlatAppearance.BorderSize = 0
        btnReportes.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnReportes.FlatStyle = FlatStyle.Flat
        btnReportes.Font = New Font("Segoe UI", 10F)
        btnReportes.ForeColor = Color.White
        btnReportes.Location = New Point(0, 680)
        btnReportes.Name = "btnReportes"
        btnReportes.Size = New Size(230, 46)
        btnReportes.TabIndex = 14
        btnReportes.Text = "   Reportes"
        btnReportes.TextAlign = ContentAlignment.MiddleLeft
        btnReportes.UseVisualStyleBackColor = False
        '
        ' lblTituloReportes
        '
        lblTituloReportes.Dock = DockStyle.Top
        lblTituloReportes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTituloReportes.ForeColor = Color.FromArgb(CByte(150), CByte(160), CByte(170))
        lblTituloReportes.Location = New Point(0, 640)
        lblTituloReportes.Name = "lblTituloReportes"
        lblTituloReportes.Padding = New Padding(20, 15, 0, 0)
        lblTituloReportes.Size = New Size(230, 40)
        lblTituloReportes.TabIndex = 13
        lblTituloReportes.Text = "REPORTES"
        '
        ' btnUsuarios
        '
        btnUsuarios.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnUsuarios.Cursor = Cursors.Hand
        btnUsuarios.Dock = DockStyle.Top
        btnUsuarios.FlatAppearance.BorderSize = 0
        btnUsuarios.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnUsuarios.FlatStyle = FlatStyle.Flat
        btnUsuarios.Font = New Font("Segoe UI", 10F)
        btnUsuarios.ForeColor = Color.White
        btnUsuarios.Location = New Point(0, 594)
        btnUsuarios.Name = "btnUsuarios"
        btnUsuarios.Size = New Size(230, 46)
        btnUsuarios.TabIndex = 12
        btnUsuarios.Text = "   Usuarios"
        btnUsuarios.TextAlign = ContentAlignment.MiddleLeft
        btnUsuarios.UseVisualStyleBackColor = False
        btnUsuarios.Visible = False
        '
        ' btnMecanicos
        '
        btnMecanicos.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnMecanicos.Cursor = Cursors.Hand
        btnMecanicos.Dock = DockStyle.Top
        btnMecanicos.FlatAppearance.BorderSize = 0
        btnMecanicos.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnMecanicos.FlatStyle = FlatStyle.Flat
        btnMecanicos.Font = New Font("Segoe UI", 10F)
        btnMecanicos.ForeColor = Color.White
        btnMecanicos.Location = New Point(0, 548)
        btnMecanicos.Name = "btnMecanicos"
        btnMecanicos.Size = New Size(230, 46)
        btnMecanicos.TabIndex = 11
        btnMecanicos.Text = "   Mecánicos"
        btnMecanicos.TextAlign = ContentAlignment.MiddleLeft
        btnMecanicos.UseVisualStyleBackColor = False
        '
        ' btnCategorias
        '
        btnCategorias.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnCategorias.Cursor = Cursors.Hand
        btnCategorias.Dock = DockStyle.Top
        btnCategorias.FlatAppearance.BorderSize = 0
        btnCategorias.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnCategorias.FlatStyle = FlatStyle.Flat
        btnCategorias.Font = New Font("Segoe UI", 10F)
        btnCategorias.ForeColor = Color.White
        btnCategorias.Location = New Point(0, 502)
        btnCategorias.Name = "btnCategorias"
        btnCategorias.Size = New Size(230, 46)
        btnCategorias.TabIndex = 10
        btnCategorias.Text = "   Categorías"
        btnCategorias.TextAlign = ContentAlignment.MiddleLeft
        btnCategorias.UseVisualStyleBackColor = False
        '
        ' btnServicios
        '
        btnServicios.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnServicios.Cursor = Cursors.Hand
        btnServicios.Dock = DockStyle.Top
        btnServicios.FlatAppearance.BorderSize = 0
        btnServicios.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnServicios.FlatStyle = FlatStyle.Flat
        btnServicios.Font = New Font("Segoe UI", 10F)
        btnServicios.ForeColor = Color.White
        btnServicios.Location = New Point(0, 456)
        btnServicios.Name = "btnServicios"
        btnServicios.Size = New Size(230, 46)
        btnServicios.TabIndex = 9
        btnServicios.Text = "   Servicios"
        btnServicios.TextAlign = ContentAlignment.MiddleLeft
        btnServicios.UseVisualStyleBackColor = False
        '
        ' btnMarcasModelos
        '
        btnMarcasModelos.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnMarcasModelos.Cursor = Cursors.Hand
        btnMarcasModelos.Dock = DockStyle.Top
        btnMarcasModelos.FlatAppearance.BorderSize = 0
        btnMarcasModelos.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnMarcasModelos.FlatStyle = FlatStyle.Flat
        btnMarcasModelos.Font = New Font("Segoe UI", 10F)
        btnMarcasModelos.ForeColor = Color.White
        btnMarcasModelos.Location = New Point(0, 410)
        btnMarcasModelos.Name = "btnMarcasModelos"
        btnMarcasModelos.Size = New Size(230, 46)
        btnMarcasModelos.TabIndex = 8
        btnMarcasModelos.Text = "   Marcas y modelos"
        btnMarcasModelos.TextAlign = ContentAlignment.MiddleLeft
        btnMarcasModelos.UseVisualStyleBackColor = False
        '
        ' btnVehiculos
        '
        btnVehiculos.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnVehiculos.Cursor = Cursors.Hand
        btnVehiculos.Dock = DockStyle.Top
        btnVehiculos.FlatAppearance.BorderSize = 0
        btnVehiculos.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnVehiculos.FlatStyle = FlatStyle.Flat
        btnVehiculos.Font = New Font("Segoe UI", 10F)
        btnVehiculos.ForeColor = Color.White
        btnVehiculos.Location = New Point(0, 364)
        btnVehiculos.Name = "btnVehiculos"
        btnVehiculos.Size = New Size(230, 46)
        btnVehiculos.TabIndex = 7
        btnVehiculos.Text = "   Vehículos"
        btnVehiculos.TextAlign = ContentAlignment.MiddleLeft
        btnVehiculos.UseVisualStyleBackColor = False
        '
        ' btnClientes
        '
        btnClientes.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnClientes.Cursor = Cursors.Hand
        btnClientes.Dock = DockStyle.Top
        btnClientes.FlatAppearance.BorderSize = 0
        btnClientes.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnClientes.FlatStyle = FlatStyle.Flat
        btnClientes.Font = New Font("Segoe UI", 10F)
        btnClientes.ForeColor = Color.White
        btnClientes.Location = New Point(0, 318)
        btnClientes.Name = "btnClientes"
        btnClientes.Size = New Size(230, 46)
        btnClientes.TabIndex = 6
        btnClientes.Text = "   Clientes"
        btnClientes.TextAlign = ContentAlignment.MiddleLeft
        btnClientes.UseVisualStyleBackColor = False
        '
        ' lblTituloDatosMaestros
        '
        lblTituloDatosMaestros.Dock = DockStyle.Top
        lblTituloDatosMaestros.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTituloDatosMaestros.ForeColor = Color.FromArgb(CByte(150), CByte(160), CByte(170))
        lblTituloDatosMaestros.Location = New Point(0, 278)
        lblTituloDatosMaestros.Name = "lblTituloDatosMaestros"
        lblTituloDatosMaestros.Padding = New Padding(20, 15, 0, 0)
        lblTituloDatosMaestros.Size = New Size(230, 40)
        lblTituloDatosMaestros.TabIndex = 5
        lblTituloDatosMaestros.Text = "DATOS MAESTROS"
        '
        ' btnHistorial
        '
        btnHistorial.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnHistorial.Cursor = Cursors.Hand
        btnHistorial.Dock = DockStyle.Top
        btnHistorial.FlatAppearance.BorderSize = 0
        btnHistorial.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnHistorial.FlatStyle = FlatStyle.Flat
        btnHistorial.Font = New Font("Segoe UI", 10F)
        btnHistorial.ForeColor = Color.White
        btnHistorial.Location = New Point(0, 232)
        btnHistorial.Name = "btnHistorial"
        btnHistorial.Size = New Size(230, 46)
        btnHistorial.TabIndex = 4
        btnHistorial.Text = "   Historial"
        btnHistorial.TextAlign = ContentAlignment.MiddleLeft
        btnHistorial.UseVisualStyleBackColor = False
        '
        ' btnOrdenes
        '
        btnOrdenes.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnOrdenes.Cursor = Cursors.Hand
        btnOrdenes.Dock = DockStyle.Top
        btnOrdenes.FlatAppearance.BorderSize = 0
        btnOrdenes.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnOrdenes.FlatStyle = FlatStyle.Flat
        btnOrdenes.Font = New Font("Segoe UI", 10F)
        btnOrdenes.ForeColor = Color.White
        btnOrdenes.Location = New Point(0, 186)
        btnOrdenes.Name = "btnOrdenes"
        btnOrdenes.Size = New Size(230, 46)
        btnOrdenes.TabIndex = 3
        btnOrdenes.Text = "   Órdenes de trabajo"
        btnOrdenes.TextAlign = ContentAlignment.MiddleLeft
        btnOrdenes.UseVisualStyleBackColor = False
        '
        ' btnRecepcion
        '
        btnRecepcion.BackColor = Color.FromArgb(CByte(30), CByte(39), CByte(46))
        btnRecepcion.Cursor = Cursors.Hand
        btnRecepcion.Dock = DockStyle.Top
        btnRecepcion.FlatAppearance.BorderSize = 0
        btnRecepcion.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(52), CByte(73), CByte(94))
        btnRecepcion.FlatStyle = FlatStyle.Flat
        btnRecepcion.Font = New Font("Segoe UI", 10F)
        btnRecepcion.ForeColor = Color.White
        btnRecepcion.Location = New Point(0, 140)
        btnRecepcion.Name = "btnRecepcion"
        btnRecepcion.Size = New Size(230, 46)
        btnRecepcion.TabIndex = 2
        btnRecepcion.Text = "   Recepción"
        btnRecepcion.TextAlign = ContentAlignment.MiddleLeft
        btnRecepcion.UseVisualStyleBackColor = False
        '
        ' lblTituloOperaciones
        '
        lblTituloOperaciones.Dock = DockStyle.Top
        lblTituloOperaciones.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTituloOperaciones.ForeColor = Color.FromArgb(CByte(150), CByte(160), CByte(170))
        lblTituloOperaciones.Location = New Point(0, 100)
        lblTituloOperaciones.Name = "lblTituloOperaciones"
        lblTituloOperaciones.Padding = New Padding(20, 15, 0, 0)
        lblTituloOperaciones.Size = New Size(230, 40)
        lblTituloOperaciones.TabIndex = 1
        lblTituloOperaciones.Text = "OPERACIONES"
        '
        ' panelLogo
        '
        panelLogo.Controls.Add(lblLogo)
        panelLogo.Dock = DockStyle.Top
        panelLogo.Location = New Point(0, 0)
        panelLogo.Name = "panelLogo"
        panelLogo.Size = New Size(230, 100)
        panelLogo.TabIndex = 0
        '
        ' lblLogo
        '
        lblLogo.Dock = DockStyle.Fill
        lblLogo.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        lblLogo.ForeColor = Color.White
        lblLogo.Location = New Point(0, 0)
        lblLogo.Name = "lblLogo"
        lblLogo.Size = New Size(230, 100)
        lblLogo.TabIndex = 0
        lblLogo.Text = "TALLER" & vbCrLf & "MECÁNICO"
        lblLogo.TextAlign = ContentAlignment.MiddleCenter
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
        lblSistema.Size = New Size(187, 25)
        lblSistema.TabIndex = 0
        lblSistema.Text = "Sistema de Gestión"
        '
        ' lblUsuario
        '
        lblUsuario.Dock = DockStyle.Right
        lblUsuario.Font = New Font("Segoe UI", 10F)
        lblUsuario.Location = New Point(374, 14)
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
        btnCerrarSesion.Location = New Point(794, 14)
        btnCerrarSesion.Name = "btnCerrarSesion"
        btnCerrarSesion.Size = New Size(140, 37)
        btnCerrarSesion.TabIndex = 2
        btnCerrarSesion.Text = "Cerrar sesión"
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
        lblBienvenida.Size = New Size(216, 51)
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
        lblDescripcion.Size = New Size(354, 42)
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
        panelLogo.ResumeLayout(False)
        panelSuperior.ResumeLayout(False)
        panelSuperior.PerformLayout()
        panelContenido.ResumeLayout(False)
        panelContenido.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents panelMenu As Panel
    Friend WithEvents btnReportes As Button
    Friend WithEvents lblTituloReportes As Label
    Friend WithEvents btnUsuarios As Button
    Friend WithEvents btnMecanicos As Button
    Friend WithEvents btnCategorias As Button
    Friend WithEvents btnServicios As Button
    Friend WithEvents btnMarcasModelos As Button
    Friend WithEvents btnVehiculos As Button
    Friend WithEvents btnClientes As Button
    Friend WithEvents lblTituloDatosMaestros As Label
    Friend WithEvents btnHistorial As Button
    Friend WithEvents btnOrdenes As Button
    Friend WithEvents btnRecepcion As Button
    Friend WithEvents lblTituloOperaciones As Label
    Friend WithEvents panelLogo As Panel
    Friend WithEvents lblLogo As Label
    Friend WithEvents panelSuperior As Panel
    Friend WithEvents lblSistema As Label
    Friend WithEvents lblUsuario As Label
    Friend WithEvents btnCerrarSesion As Button
    Friend WithEvents panelContenido As Panel
    Friend WithEvents lblBienvenida As Label
    Friend WithEvents lblDescripcion As Label
End Class

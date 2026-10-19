Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed. This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    ' **NEW** ApplyApplicationDefaults: Raised when the application queries default values to be set for the application.

    ' Example:
    ' Private Sub MyApplication_ApplyApplicationDefaults(sender As Object, e As ApplyApplicationDefaultsEventArgs) Handles Me.ApplyApplicationDefaults
    '
    '   ' Setting the application-wide default Font:
    '   e.Font = New Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular)
    '
    '   ' Setting the HighDpiMode for the Application:
    '   e.HighDpiMode = HighDpiMode.PerMonitorV2
    '
    '   ' If a splash dialog is used, this sets the minimum display time:
    '   e.MinimumSplashScreenDisplayTime = 4000
    ' End Sub

    Partial Friend Class MyApplication

        'cuando fue el ultimo error no atrapado y cuantos seguidos hubo, para cortar una cadena de errores que se repiten
        Private ultimoErrorInesperado As Date = Date.MinValue
        Private erroresSeguidos As Integer = 0

        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            'un error que ninguna pantalla atrapo: lo registro y le doy al usuario la opcion de seguir o salir
            Dim pudoRegistrar As Boolean = RegistrarError("Error no atrapado por ninguna pantalla", e.Exception)

            'si los errores vienen uno atras de otro (por ejemplo al redibujar una pantalla), seguir solo repite el dialogo: cierro
            If (Date.Now - ultimoErrorInesperado).TotalSeconds < 10 Then
                erroresSeguidos += 1
            Else
                erroresSeguidos = 1
            End If
            ultimoErrorInesperado = Date.Now

            Dim detalle As String
            If pudoRegistrar Then
                detalle = "El detalle quedó registrado en: " & RutaRegistro
            Else
                'no hubo donde guardarlo: muestro el detalle para que no se pierda
                detalle = "No se pudo escribir el registro de errores. Detalle: " & e.Exception.ToString()
            End If

            If erroresSeguidos >= 3 Then
                MessageBox.Show("El error se repite y el sistema se va a cerrar." & vbCrLf & vbCrLf & detalle,
                                "Taller Mecánico", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.ExitApplication = True
                Exit Sub
            End If

            Dim respuesta As DialogResult = MessageBox.Show(
                "Ocurrió un error inesperado y la operación no se completó." & vbCrLf & detalle & vbCrLf & vbCrLf &
                "¿Desea seguir trabajando? Si elige No, el sistema se cierra.",
                "Taller Mecánico", MessageBoxButtons.YesNo, MessageBoxIcon.Error)

            e.ExitApplication = (respuesta = DialogResult.No)
        End Sub

    End Class
End Namespace

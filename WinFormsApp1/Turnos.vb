Module Turnos
    'horario de atencion del taller y cupo de cada franja de los turnos

    'de lunes a viernes las franjas van de HORA_APERTURA a HORA_CIERRE (la ultima empieza una hora antes)
    Public Const HORA_APERTURA As Integer = 8
    Public Const HORA_CIERRE As Integer = 17
    'los sabados se cierra antes; los domingos no se atiende
    Public Const HORA_CIERRE_SABADO As Integer = 12
    'cuantos vehiculos se reciben como maximo en cada franja de una hora
    Public Const CUPO_POR_FRANJA As Integer = 2

    Function FranjasDelDia(dia As Date) As List(Of Date)
        'devuelvo el inicio de cada franja de ese dia; un domingo no tiene franjas
        Dim franjas As New List(Of Date)
        If dia.DayOfWeek = DayOfWeek.Sunday Then Return franjas

        Dim cierre As Integer = HORA_CIERRE
        If dia.DayOfWeek = DayOfWeek.Saturday Then cierre = HORA_CIERRE_SABADO

        For hora As Integer = HORA_APERTURA To cierre - 1
            franjas.Add(dia.Date.AddHours(hora))
        Next
        Return franjas
    End Function

    Function TextoEstadoTurno(estado As String) As String
        'el estado como se guarda en la base, escrito para la pantalla
        If estado = "PENDIENTE" Then Return "Pendiente"
        If estado = "ATENDIDO" Then Return "Atendido"
        If estado = "CANCELADO" Then Return "Cancelado"
        If estado = "AUSENTE" Then Return "Ausente"
        Return estado
    End Function

End Module

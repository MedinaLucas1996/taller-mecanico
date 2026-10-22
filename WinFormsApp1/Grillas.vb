'importo Reflection para activar una propiedad que DataGridView no deja cambiar desde afuera
Imports System.Reflection

Module Grillas
    'rutinas compartidas para las grillas de las pantallas
    'cada pantalla con grilla la llama al empezar su Load

    Sub ActivarDobleBufer(contenedor As Control)
        'activo el doble bufer en cada grilla del contenedor y de los paneles que tenga adentro
        'sin doble bufer la grilla se dibuja directo en pantalla mientras se desplaza: parpadea y se traba
        'con doble bufer se dibuja primero en memoria y se muestra de una sola vez
        For Each control As Control In contenedor.Controls
            If TypeOf control Is DataGridView Then
                'DoubleBuffered es una propiedad protegida de Control: la activo por reflexion
                Dim propiedad As PropertyInfo =
                    GetType(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
                propiedad.SetValue(control, True)
            ElseIf control.HasChildren Then
                'las grillas suelen estar dentro de paneles: las busco tambien adentro
                ActivarDobleBufer(control)
            End If
        Next
    End Sub

End Module

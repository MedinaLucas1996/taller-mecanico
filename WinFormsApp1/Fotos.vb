Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO

Module Fotos
    'rutinas compartidas para las fotos de recepcion y los iconos
    'las usan el asistente de recepcion, el tablero de ordenes y la ventana de la foto ampliada

    'lado mayor maximo de las fotos guardadas, en pixeles
    Public Const LADO_MAXIMO As Integer = 1280

    Function CargarImagenReducida(ruta As String) As Bitmap
        'leo una foto del disco y la devuelvo reducida a LADO_MAXIMO pixeles en su lado mayor
        'devuelve Nothing cuando el archivo no es una imagen valida
        Try
            'leo todo el archivo a memoria, asi no queda bloqueado en el disco
            Dim bytes() As Byte = File.ReadAllBytes(ruta)

            Using memoria As New MemoryStream(bytes)
                Using original As Image = Image.FromStream(memoria)
                    'las fotos de celular guardan la orientacion aparte (dato EXIF 274): la enderezo
                    If Array.IndexOf(original.PropertyIdList, &H112) >= 0 Then
                        Dim orientacion As Integer = original.GetPropertyItem(&H112).Value(0)
                        If orientacion = 3 Then original.RotateFlip(RotateFlipType.Rotate180FlipNone)
                        If orientacion = 6 Then original.RotateFlip(RotateFlipType.Rotate90FlipNone)
                        If orientacion = 8 Then original.RotateFlip(RotateFlipType.Rotate270FlipNone)
                    End If

                    'calculo el tamaño nuevo, la foto nunca se agranda
                    Dim ancho As Integer = original.Width
                    Dim alto As Integer = original.Height
                    Dim ladoMayor As Integer = ancho
                    If alto > ancho Then ladoMayor = alto

                    If ladoMayor > LADO_MAXIMO Then
                        ancho = CInt(original.Width * LADO_MAXIMO / ladoMayor)
                        alto = CInt(original.Height * LADO_MAXIMO / ladoMayor)
                        If ancho < 1 Then ancho = 1
                        If alto < 1 Then alto = 1
                    End If

                    'dibujo la foto en un bitmap nuevo, que ya no depende del archivo
                    Dim reducida As New Bitmap(ancho, alto, PixelFormat.Format24bppRgb)
                    Using dibujo As Graphics = Graphics.FromImage(reducida)
                        'fondo blanco para los PNG con transparencia
                        dibujo.Clear(Color.White)
                        dibujo.InterpolationMode = InterpolationMode.HighQualityBicubic
                        dibujo.DrawImage(original, 0, 0, ancho, alto)
                    End Using

                    Return reducida
                End Using
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Function ImagenABytes(imagen As Image) As Byte()
        'convierto la foto a JPEG para guardarla en la base
        Using memoria As New MemoryStream()
            imagen.Save(memoria, ImageFormat.Jpeg)
            Return memoria.ToArray()
        End Using
    End Function

    Function LeerFotoComoJpeg(ruta As String) As Byte()
        'leo una foto del disco y devuelvo los bytes JPEG listos para guardar en ot_foto
        'usa las dos rutinas de arriba, igual que la recepcion; devuelve Nothing si el archivo no es una imagen
        Using foto As Bitmap = CargarImagenReducida(ruta)
            If foto Is Nothing Then Return Nothing
            Return ImagenABytes(foto)
        End Using
    End Function

    Function LeerIcono(archivo As String) As Image
        'leo un icono de la carpeta Recursos\iconos que esta junto al ejecutable
        Try
            Dim ruta As String = Path.Combine(AppContext.BaseDirectory, "Recursos", "iconos", archivo)
            Using imagen As Image = Image.FromFile(ruta)
                'copio el icono a un bitmap nuevo para no dejar el archivo abierto
                Return New Bitmap(imagen)
            End Using
        Catch ex As Exception
            'si el archivo falta o no se puede leer, el control queda sin icono y el sistema sigue
            Return Nothing
        End Try
    End Function

End Module

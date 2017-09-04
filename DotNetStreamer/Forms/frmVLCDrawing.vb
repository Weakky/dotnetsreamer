Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D

Public Class frmVLCDrawing


#Region "Properties"

    Private _DrawSubtitles As Boolean = False 'Temporary property
    Public Property DrawSubtitles As Boolean
        Get
            Return _DrawSubtitles
        End Get
        Set(value As Boolean)
            _DrawSubtitles = value
            Invalidate()
        End Set
    End Property

    Private _DrawNotification As Boolean = False
    Public Property DrawNotification As Boolean
        Get
            Return _DrawNotification
        End Get
        Set(value As Boolean)
            _DrawNotification = value
            Invalidate()
        End Set
    End Property

    Private _DrawExitButton As Boolean = False
    Public Property DrawExitButton As Boolean
        Get
            Return _DrawExitButton
        End Get
        Set(value As Boolean)
            _DrawExitButton = value

            If value Then
                tmrExitButtonTimeOut.Stop()
                tmrExitButtonTimeOut.Start()
            Else
                tmrExitButtonTimeOut.Stop()
            End If
            Invalidate()
        End Set
    End Property

    Private _DrawVideoStateButton As PlayPauseBufferButtonStyles = PlayPauseBufferButtonStyles.Play
    Public Property DrawVideoStateButton As PlayPauseBufferButtonStyles
        Get
            Return _DrawVideoStateButton
        End Get
        Set(value As PlayPauseBufferButtonStyles)
            _DrawVideoStateButton = value
            Invalidate()
        End Set
    End Property

    Enum PlayPauseBufferButtonStyles
        Play = 0
        Pause = 1
        Buffering = 2
    End Enum

    Private _BufferingValue As Integer = 0
    Public Property BufferingValue As Integer
        Get
            Return _BufferingValue
        End Get
        Set(value As Integer)
            If value <> _BufferingValue Then
                _BufferingValue = value
                Invalidate()
            End If
        End Set
    End Property

    Private _SubtitleText As String = String.Empty
    Public Property SubtitleText As String
        Get
            Return _SubtitleText
        End Get
        Set(value As String)
            _SubtitleText = value
        End Set
    End Property

#End Region

#Region "Private Fields"
    Private WithEvents tmrExitButtonTimeOut As New Timer With {.Enabled = False, .Interval = 3000}
#End Region

    Public Event CrossClicked()

    Protected Overrides Sub OnShown(e As EventArgs)

        AddHandler frmMain.Move, Sub()
                                     MakeFormFitOnVLCPlayer()
                                 End Sub

        AddHandler frmMain.Resize, Sub()
                                       MakeFormFitOnVLCPlayer()
                                   End Sub

        AddHandler frmMain.pnl_VideoPlayback.Resize, Sub()
                                                         MakeFormFitOnVLCPlayer()
                                                     End Sub

        AddHandler frmMain.pnl_VideoPlayback.Move, Sub()
                                                       MakeFormFitOnVLCPlayer()
                                                   End Sub
        MakeFormFitOnVLCPlayer()

        MyBase.OnShown(e)
    End Sub

    Private _ResetDrawing As Boolean = False
    Public Sub ResetDrawing()
        _SubtitleText = String.Empty
        _DrawExitButton = False
        _DrawSubtitles = False
        _DrawNotification = False
        _DrawVideoStateButton = PlayPauseBufferButtonStyles.Play
        PaintHook()
    End Sub

    Public NotificationText As String = String.Empty

    Public Sub PaintHook()
        Using B As New Bitmap(Width, Height)
            Using G As Graphics = Graphics.FromImage(B)

                If _DrawSubtitles Then
                    DrawSubtitleToSubtitlesForm(G, B, _SubtitleText, Settings.Data.SubFont, Settings.Data.SubColor)
                End If

                If _DrawExitButton Then
                    DrawExitButtonToVideo(G, B)
                End If

                If _DrawNotification AndAlso _DrawVideoStateButton <> PlayPauseBufferButtonStyles.Buffering Then
                    PaintNotification(G, B, Bounds.Size, NotificationText)
                End If

                Select Case _DrawVideoStateButton
                    Case PlayPauseBufferButtonStyles.Pause
                        PaintPauseButton(G, B, Bounds.Size)
                    Case PlayPauseBufferButtonStyles.Buffering
                        PaintBufferingProgressBar(G, B, Bounds.Size)
                    Case PlayPauseBufferButtonStyles.Play
                End Select

                UpdateLayeredForm(B)
            End Using
        End Using
    End Sub

#Region "Form Location"
    Private Sub MakeFormFitOnVLCPlayer()
        Size = frmMain.pnl_VideoPlayback.Size
        Location = frmMain.PointToScreen(frmMain.pnl_VideoPlayback.Location)
    End Sub
#End Region


#Region "Subtitle Drawing"

    Private Sub DrawSubtitleToSubtitlesForm(G As Graphics, B As Bitmap, SubtitleText As String, TextFont As Font, TextColor As Color, Optional ByVal ShadowColor As Color = Nothing)
        Try
            If ShadowColor = Nothing Then ShadowColor = Color.Black

            Dim TextBounds As Rectangle = Rectangle.Round(B.GetBounds(GraphicsUnit.Pixel))
            TextBounds = New Rectangle(TextBounds.X + 10, TextBounds.Y + 5, TextBounds.Width - 20, TextBounds.Height - 35)

            Using SF As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Far}

                Dim originalSize As SizeF = New SizeF(849, 527)
                TextFont = AppropriateFont(G, originalSize, ClientRectangle.Size, TextFont) 'Autoscale font

                If SubtitleText.Contains("<i>") AndAlso SubtitleText.Contains("</i>") Then
                    SubtitleText = SubtitleText.Replace("<i>", String.Empty).Replace("</i>", String.Empty)
                    TextFont = New Font(TextFont.FontFamily.Name, TextFont.Size, FontStyle.Italic)
                End If

                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), TextBounds, SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 1, TextBounds.Y + 0), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 0, TextBounds.Y + 1), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 1, TextBounds.Y + 1), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 1, TextBounds.Y + 2), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 2, TextBounds.Y + 1), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 0, TextBounds.Y + 2), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 1, TextBounds.Y + 2), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 2, TextBounds.Y + 2), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(TextBounds.X + 2, TextBounds.Y + 0), TextBounds.Size), SF)
                G.DrawString(SubtitleText, TextFont, New SolidBrush(TextColor), New Rectangle(New Point(TextBounds.X + 1, TextBounds.Y + 1), TextBounds.Size), SF)

                TextBounds = Nothing
                TextFont.Dispose()
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private _oldfont As Font = Settings.Data.SubFont
    Private Function AppropriateFont(g As Graphics, defaultSize As SizeF, changedSize As SizeF, f As Font) As Font
        Dim wRatio As Single = changedSize.Width / defaultSize.Width
        Dim hRatio As Single = changedSize.Height / defaultSize.Height
        Dim ratio As Single = If((hRatio < wRatio), hRatio, wRatio)
        Dim newSize As Single = f.Size * ratio

        If newSize = _oldfont.Size Then
            Return f
        Else
            Return New Font(f.FontFamily.Name, newSize, f.Style)
        End If

    End Function
#End Region
#Region "Cross Drawing"

    Private Sub DrawExitButtonToVideo(G As Graphics, B As Bitmap)
        If _DrawExitButton AndAlso Not frmHint.Visible Then
            If frmMain.FullscreenModeToolStripMenuItem.Checked OrElse frmMain.PictureInPictureToolStripMenuItem.Checked Then
                Using crossFont As New Font("Marlett", 16.0F)
                    Dim R As New Rectangle(New Point(Width - 29, 6), New Size(30, 24))
                    G.DrawString("r", crossFont, Brushes.White, R) 'Draw a cross
                End Using
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        If IsMouseOverCross(e.Location) Then RaiseEvent CrossClicked()
        MyBase.OnMouseClick(e)
    End Sub

    Public Function IsMouseOverCross(mouseLoc As Point) As Boolean
        Dim R As New Rectangle(PointToScreen(New Point(Width - 29, 6)), New Size(30, 24))
        mouseLoc = PointToScreen(mouseLoc)

        If R.Contains(mouseLoc) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private oldMouseLoc As Point = Point.Empty
    Public Function IsMouseOverVLC() As Boolean
        Dim R As Rectangle = frmMain.Bounds
        Dim pcin As New CURSORINFO() With {.cbSize = Marshal.SizeOf(pcin)}

        If GetCursorInfo(pcin) Then
            Dim mouseLocation As Point = New Point(pcin.ptScreenPos.x, pcin.ptScreenPos.y)
            If R.Contains(New Point(pcin.ptScreenPos.x, pcin.ptScreenPos.y)) AndAlso mouseLocation <> oldMouseLoc Then
                '  Debug.WriteLine("Old:{0} - Current:{1}", oldMouseLoc, mouseLocation)
                oldMouseLoc = mouseLocation
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function


    Private Sub tmrExitButtonTimeOut_Tick(sender As Object, e As EventArgs) Handles tmrExitButtonTimeOut.Tick
        _DrawExitButton = False
    End Sub

#End Region
#Region "Video State Drawing"

    Private Sub PaintPlayButton(G As Graphics, B As Bitmap, size As Size)

        Dim buttonBounds As New Rectangle(Width / 2 - (size.Width / 2), Height / 2 - (size.Height / 2), size.Width, size.Height)
        Dim p1 = New Point(((buttonBounds.Width / 5) * 2) + buttonBounds.X, (buttonBounds.Height / 4) + buttonBounds.Y)
        Dim p2 = New Point(((buttonBounds.Width / 5) * 2) + buttonBounds.X, (buttonBounds.Height - (buttonBounds.Height / 4) - 2) + buttonBounds.Y)
        Dim p3 = New Point(((buttonBounds.Width / 9) * 6) + buttonBounds.X, ((buttonBounds.Height - 1) / 2) + buttonBounds.Y)
        Dim p = {p1, p2, p3}

        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        G.FillPolygon(Brushes.White, p)
    End Sub
    Private Sub PaintPauseButton(G As Graphics, B As Bitmap, size As Size)
        G.SmoothingMode = Drawing2D.SmoothingMode.None
        size = GetProportionalSize(size)
        Dim buttonBounds As New Rectangle(Width / 2 - (size.Width / 2), Height / 2 - (size.Height / 2), size.Width, size.Height)

        Dim R1 As New Rectangle(((buttonBounds.Width / 7) * 2) + buttonBounds.X, (buttonBounds.Height / 4) + buttonBounds.Y, buttonBounds.Width / 7, (buttonBounds.Height) - 2 * (buttonBounds.Height / 4))
        G.FillRectangle(Brushes.White, R1)
        Dim R2 As New Rectangle((buttonBounds.Width - ((buttonBounds.Width / 7) * 3)) + buttonBounds.X, (buttonBounds.Height / 4) + buttonBounds.Y, buttonBounds.Width / 7, (buttonBounds.Height) - 2 * (buttonBounds.Height / 4))
        G.FillRectangle(Brushes.White, R2)
        G.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
    End Sub

    Dim _Maximum As Integer = 100
    Dim Thickness As Single = 3
    Dim oldValue As Integer = -1

    Private Sub PaintBufferingProgressBar(G As Graphics, B As Bitmap, size As Size)
        If _BufferingValue <= _Maximum AndAlso _BufferingValue > 0 Then

            size = GetRadialBarProportionalSize(size)

            Dim buttonBounds As New Rectangle((Me.Width / 2) - (size.Width / 2), (Me.Height / 2) - (size.Height / 2), size.Width, size.Height)
            Dim SF As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center, .Trimming = StringTrimming.EllipsisCharacter}
            Dim BufferingText As String = String.Format("Buffering: {0}%", CInt((100 / _Maximum) * _BufferingValue))
            Dim TextFont As New Font("Segoe UI Light", 15)
            Dim ShadowColor As Color = Color.Black

            Dim Width As Integer = buttonBounds.Width
            Dim Height As Integer = buttonBounds.Height

            'Enable anti-aliasing to prevent rough edges
            G.SmoothingMode = SmoothingMode.HighQuality

            'Draw progress
            Using P As New Pen(Brushes.White, Thickness)
                P.StartCap = LineCap.Round : P.EndCap = LineCap.Round
                Try
                    G.DrawArc(P, CInt(buttonBounds.X + (Thickness / 2)), CInt(buttonBounds.Y + (Thickness / 2)), Width - Thickness - 1, Height - Thickness - 1, -90, CInt((360 / _Maximum) * _BufferingValue))
                Catch ex As Exception
                End Try
            End Using


            'Draw progress string
            Dim S As SizeF = G.MeasureString(BufferingText, TextFont, buttonBounds.Width)


            'Draw round rectangle behind the text
            Dim centreRectangle As New Rectangle(buttonBounds.X + (buttonBounds.Width / 2) - ((S.Width + 9) / 2), buttonBounds.Y + (buttonBounds.Height / 2) - ((S.Height + 3) / 2), S.Width + 9, S.Height + 4)
            G.FillPath(New SolidBrush(Color.FromArgb(13, 250, 250, 250)), CreateRound(centreRectangle, 5))


            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), buttonBounds, SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 0), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 0, buttonBounds.Y + 1), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 1), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 2), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 2, buttonBounds.Y + 1), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 0, buttonBounds.Y + 2), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 2), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 2, buttonBounds.Y + 2), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 2, buttonBounds.Y + 0), buttonBounds.Size), SF)
            G.DrawString(BufferingText, TextFont, New SolidBrush(Color.White), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 1), buttonBounds.Size), SF)
            'oldValue = _BufferingValue
        End If
    End Sub

    Private Sub PaintNotification(G As Graphics, B As Bitmap, size As Size, Text As String)

            size = GetRadialBarProportionalSize(size)

            Dim buttonBounds As New Rectangle((Me.Width / 2) - (size.Width / 2), (Me.Height / 2) - (size.Height / 2), size.Width, size.Height)
            Dim SF As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center, .Trimming = StringTrimming.EllipsisCharacter}
            Dim ErrorText As String = Text
            Dim TextFont As New Font("Segoe UI Light", 15)
            Dim ShadowColor As Color = Color.Black

            Dim Width As Integer = buttonBounds.Width
            Dim Height As Integer = buttonBounds.Height

            'Enable anti-aliasing to prevent rough edges
            G.SmoothingMode = SmoothingMode.HighQuality

            'Draw progress string
            Dim S As SizeF = G.MeasureString(ErrorText, TextFont, buttonBounds.Width)


            'Draw round rectangle behind the text
            Dim centreRectangle As New Rectangle(buttonBounds.X + (buttonBounds.Width / 2) - ((S.Width + 9) / 2), buttonBounds.Y + (buttonBounds.Height / 2) - ((S.Height + 3) / 2), S.Width + 9, S.Height + 4)
            G.FillPath(New SolidBrush(Color.FromArgb(13, 250, 250, 250)), CreateRound(centreRectangle, 5))


            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), buttonBounds, SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 0), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 0, buttonBounds.Y + 1), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 1), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 2), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 2, buttonBounds.Y + 1), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 0, buttonBounds.Y + 2), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 2), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 2, buttonBounds.Y + 2), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(ShadowColor), New Rectangle(New Point(buttonBounds.X + 2, buttonBounds.Y + 0), buttonBounds.Size), SF)
            G.DrawString(ErrorText, TextFont, New SolidBrush(Color.White), New Rectangle(New Point(buttonBounds.X + 1, buttonBounds.Y + 1), buttonBounds.Size), SF)
    End Sub

#End Region


#Region "Helper Methods"

    Private CreateRoundPath As GraphicsPath
    Private CreateCreateRoundangle As Rectangle
    Function CreateRound(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal slope As Integer) As GraphicsPath
        CreateCreateRoundangle = New Rectangle(x, y, width, height)
        Return CreateRound(CreateCreateRoundangle, slope)
    End Function

    Function CreateRound(ByVal r As Rectangle, ByVal slope As Integer) As GraphicsPath
        CreateRoundPath = New GraphicsPath(FillMode.Winding)
        slope = slope * 4
        CreateRoundPath.AddArc(r.X, r.Y, slope, slope, 180.0F, 90.0F)
        CreateRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270.0F, 90.0F)
        CreateRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0.0F, 90.0F)
        CreateRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90.0F, 90.0F)
        CreateRoundPath.CloseFigure()
        Return CreateRoundPath
    End Function

    Private Function GetRadialBarProportionalSize(size As Size) As Size
        size = New Size(size.Width * 0.45, size.Height * 0.45)
        Dim originalSize As Size = New Size(250, 250)
        Dim wRatio As Single = size.Width / originalSize.Width
        Dim hRatio As Single = size.Height / originalSize.Height
        Dim ratio As Single = If((hRatio < wRatio), hRatio, wRatio)
        Dim newSize As Size = New Size(originalSize.Width * ratio, originalSize.Height * ratio)
        Return newSize
    End Function
    Private Function GetProportionalSize(size As Size) As Size
        size = New Size(size.Width * 0.6, size.Height * 0.6)
        Dim originalSize As Size = New Size(250, 250)
        Dim wRatio As Single = size.Width / originalSize.Width
        Dim hRatio As Single = size.Height / originalSize.Height
        Dim ratio As Single = If((hRatio < wRatio), hRatio, wRatio)
        Dim newSize As Size = New Size(originalSize.Width * ratio, originalSize.Height * ratio)
        Return newSize
    End Function

#End Region

    Private Sub DrawLoadingNyanToVideo(G As Graphics, B As Bitmap)
        Dim rectangle As New Rectangle(Width / 2 - (Size.Width / 2), Height / 2 - (Size.Height / 2), Size.Width, Size.Height)
        G.DrawImage(My.Resources.tumblr_mjphnqLpNy1s5jjtzo1_400, rectangle)
    End Sub

End Class
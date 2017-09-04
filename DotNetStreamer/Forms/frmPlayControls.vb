Public Class frmPlayControls

#Region "Declarations"
    Dim btnPrevious As New Rectangle(8, 30, 27, 27)
    Dim btnPlayPause As New Rectangle(40, 22, 35, 35)
    Dim btnNext As New Rectangle(80, 30, 27, 27)
    '__________________________________________________
    Dim prgH As New Rectangle(117, 37, 462, 7)
    Dim prgV As New Rectangle(btnNext.Right + 4, 20, 7, 40)
    Dim time As New Point(115, 53)
    Dim timeleft As New Point(587, 53)
    Dim shows As New Point(115, 28)
    Dim btnPreviousState, btnPlayPauseState, btnNextState, btnLoveState As Boolean
#End Region
#Region "Structures & custom events"
    Enum PlayPauseButtonStyles
        Play = 0
        Pause = 1
    End Enum

    Public Event Previous_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event PlayPause_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Next_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Love_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Progressbar_Clicked(ByVal Sender As Object, ByVal ClickedAtValue As Double)
    Public Event MouseMoveProgress(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseDownProgress(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseUpProgress(ByVal e As System.Windows.Forms.MouseEventArgs)
#End Region
#Region "Properties"

    Dim _PPBS As PlayPauseButtonStyles
    Property PlayPauseButton As PlayPauseButtonStyles
        Get
            Return _PPBS
        End Get
        Set(value As PlayPauseButtonStyles)
            _PPBS = value
            Invalidate()
        End Set
    End Property

    Dim _Seconds As String
    Property Seconds As String
        Get
            Return _Seconds
        End Get
        Set(value As String)
            _Seconds = value
            Invalidate()
        End Set
    End Property

    Dim _SecondsLeft As String
    Property SecondsLeft As String
        Get
            Return _SecondsLeft
        End Get
        Set(value As String)
            _SecondsLeft = value
            Invalidate()
        End Set
    End Property

    Dim _ShowName As String
    Property ShowName As String
        Get
            Return _ShowName
        End Get
        Set(value As String)
            _ShowName = value
            Invalidate()
        End Set
    End Property

    Dim _ProgressValue As Integer
    Property ProgressValue As Integer
        Get
            Return _ProgressValue
        End Get
        Set(value As Integer)
            _ProgressValue = value
            Invalidate()
        End Set
    End Property

    Dim _Max As Integer = 100
    Property ProgressMaximum As Integer
        Get
            Return _Max
        End Get
        Set(value As Integer)
            If value >= _ProgressValue Then _Max = value
        End Set
    End Property

    Dim _Val As Integer = 0
    Property Value As Integer
        Get
            Return _Val
        End Get
        Set(v As Integer)
            If v <= _Max Then _Val = v
            Invalidate()
        End Set
    End Property

    Dim _MaxV As Integer = 100
    Property MaximumV As Integer
        Get
            Return _MaxV
        End Get
        Set(value As Integer)
            If value >= _Val Then _MaxV = value
        End Set
    End Property
#End Region
#Region "Events"

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        PaintControl()
        timeleft = New Point(Width - CreateGraphics.MeasureString(_SecondsLeft, New Font("Segoe UI", 7)).Width + 37, 53)
        prgH.Width = Width - 180
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If btnPrevious.Contains(e.Location) Then btnPreviousState = True Else btnPreviousState = False
        If btnPlayPause.Contains(e.Location) Then btnPlayPauseState = True Else btnPlayPauseState = False
        If btnNext.Contains(e.Location) Then btnNextState = True Else btnNextState = False
        If prgV.Contains(e.Location) Then RaiseEvent MouseMoveProgress(e)

        If e.Button = Windows.Forms.MouseButtons.Left Then
            If prgH.Contains(e.Location) Then
                Dim X As Integer = e.Location.X - prgH.X
                RaiseEvent Progressbar_Clicked(Me, (X / prgH.Width) * _Max)
            End If
        End If

        Invalidate()
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If btnPrevious.Contains(e.Location) Then
            RaiseEvent Previous_Clicked(Me, Nothing)
        End If
        If btnPlayPause.Contains(e.Location) Then
            RaiseEvent PlayPause_Clicked(Me, Nothing)
        End If
        If btnNext.Contains(e.Location) Then
            RaiseEvent Next_Clicked(Me, Nothing)
        End If
        If prgH.Contains(e.Location) Then
            Dim X As Integer = e.Location.X - prgH.X
            RaiseEvent Progressbar_Clicked(Me, (X / prgH.Width) * _Max)
        End If
        If prgV.Contains(e.Location) Then RaiseEvent MouseDownProgress(e)
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        If prgV.Contains(e.Location) Then RaiseEvent MouseUpProgress(e)
        Invalidate()
        MyBase.OnMouseUp(e)
    End Sub

#End Region
#Region "Mouse Handling"

    Private Sub PlayControls_PlayPause_Clicked(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PlayPause_Clicked
        'Select Case PlayPauseButton
        '    Case SpectrumPanel.PlayPauseButtonStyles.Pause
        '        frmMain.AxWindowsMediaPlayer1.Ctlcontrols.pause()
        '        UpdateControl()
        '    Case SpectrumPanel.PlayPauseButtonStyles.Play
        '        frmMain.AxWindowsMediaPlayer1.Ctlcontrols.play()
        '        UpdateControl()
        'End Select
    End Sub

    Private Sub PlayControls_Next_Clicked(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Next_Clicked
        On Error Resume Next
        'If Not frmMain.AxWindowsMediaPlayer1.currentMedia Is Nothing And Not frmMain.AxWindowsMediaPlayer1.currentMedia.duration - frmMain.AxWindowsMediaPlayer1.currentMedia.currentPosition < 30 Then frmMain.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition += 30
    End Sub

    Private Sub PlayControls_Previous_Clicked(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Previous_Clicked
        On Error Resume Next
        ' If Not frmMain.AxWindowsMediaPlayer1.currentMedia Is Nothing And Not frmMain.AxWindowsMediaPlayer1.currentMedia.duration < 30 Then frmMain.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition -= 30
    End Sub

    Private Sub PlayControls_Progressbar_Clicked(ByVal Sender As Object, ByVal ClickedAtValue As Double) Handles Me.Progressbar_Clicked
        ' If Not frmMain.AxWindowsMediaPlayer1.currentMedia Is Nothing Then frmMain.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = ClickedAtValue
    '    Try
    '        For Each srtitem As String In Split(frmMain.srt, " --> ")
    '            Dim StartTimeStr As String = Split(srtitem, vbNewLine)(UBound(Split(srtitem, vbNewLine)))
    '            Dim StartTime As Double = frmMain.ConvertToSeconds(StartTimeStr)
    '            Dim srtn As String = ""
    '            If Not frmMain.PlayControls.Visible Then
    '                ' If StartTime > frmMain.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition Then
    '                srtn = Split(srtitem, vbNewLine)(UBound(Split(srtitem, vbNewLine)) - 1) - 1
    '                frmMain.srtno = srtn
    '                Exit For
    '            End If
    '            ' End If
    '        Next
    '    Catch ex As Exception
    '        frmMain.srtno = 1
    '    End Try
    End Sub

    'Private Sub MetroProgressbarVertical1_MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDownProgress
    '    _Val = CInt((prgV.Height - (e.Y - 20)) * _MaxV) / prgV.Height
    '    frmMain.MetroProgressbarVertical1.Value = _Val
    '    frmMain.AxWindowsMediaPlayer1.settings.volume = _Val
    'End Sub

    'Private Sub MetroProgressbarVertical1_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUpProgress
    '    _Val = CInt((prgV.Height - (e.Y - 20)) * _MaxV) / prgV.Height
    '    Value = _Val
    '    frmMain.MetroProgressbarVertical1.Value = _Val
    '    frmMain.AxWindowsMediaPlayer1.settings.volume = _Val
    '    PaintControl()
    '    Invalidate()
    'End Sub

    'Private Sub MetroProgressbarVertical1_MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMoveProgress
    '    If e.Button = Windows.Forms.MouseButtons.Left Then
    '        _Val = CInt((prgV.Height - e.Y - 20) * _MaxV) / prgV.Height
    '        frmMain.MetroProgressbarVertical1.Value = _Val
    '        frmMain.AxWindowsMediaPlayer1.settings.volume = _Val
    '    End If
    'End Sub

#End Region

    Private _Opacity As Integer = 255
    Private DrawWidth As Integer
    Private Draw As Boolean

    Public Sub UpdateControl()
        PaintControl()
    End Sub

    Public Sub ShowControl()
        Draw = True
        PaintControl()
    End Sub

    Public Sub HideControl()
        Draw = False
        PaintControl()
    End Sub

    Private Sub PaintControl()
        Dim B As New Bitmap(Width, Height)
        Dim e As Graphics = Graphics.FromImage(B)

        Text = frmMain.PlayControls.Text
        ShowName = frmMain.PlayControls.ShowName
        Seconds = frmMain.PlayControls.Seconds
        SecondsLeft = frmMain.PlayControls.SecondsLeft
        _PPBS = frmMain.PlayControls.PlayPauseButton
        ProgressMaximum = frmMain.PlayControls.ProgressMaximum
        ProgressValue = frmMain.PlayControls.ProgressValue
        _Val = frmMain.MetroProgressbarVertical1.Value
        _MaxV = frmMain.MetroProgressbarVertical1.Maximum

        If Draw Then
            With e
                .SmoothingMode = Drawing2D.SmoothingMode.HighQuality

                '//////////////////      RECTANGLE AROUND
                Dim rect As New Rectangle(btnPrevious.Left - 4, btnPlayPause.Top - 5, Width, Height - 1)
                .FillRectangle(New SolidBrush(Color.FromArgb(180, 0, 0, 0)), rect)
                .DrawRectangle(New Pen(Color.FromArgb(150, Color.White)), rect.X, rect.Y, rect.Width - 5, rect.Height - 18)

                Dim btnBrush As Brush

                Dim brushWhite As Brush = New SolidBrush(Color.FromArgb(_Opacity, Color.White))
                Dim brushLightGray As Brush = New SolidBrush(Color.FromArgb(_Opacity, Color.LightGray))

                '//////////////////     PREVIOUS BUTTON
                If btnPreviousState = True Then btnBrush = brushWhite Else btnBrush = brushLightGray
                Dim p1 As New Point(((btnPrevious.Width / 7) * 3) + btnPrevious.X, (btnPrevious.Height / 4) + btnPrevious.Y + 1)
                Dim p2 As New Point(((btnPrevious.Width / 7) * 3) + +btnPrevious.X, (btnPrevious.Height - (btnPrevious.Height / 4) - 1) + +btnPrevious.Y)
                Dim p3 As New Point(((btnPrevious.Width / 9) * 2) + btnPrevious.X, ((btnPrevious.Height - 1) / 2) + btnPrevious.Y)
                Dim p() As Point = {p1, p2, p3}
                .FillPolygon(btnBrush, p)
                p1 = New Point(((btnPrevious.Width / 9) * 6) + btnPrevious.X, (btnPrevious.Height / 4) + btnPrevious.Y + 1)
                p2 = New Point(((btnPrevious.Width / 9) * 6) + btnPrevious.X, (btnPrevious.Height - (btnPrevious.Height / 4) - 1) + btnPrevious.Y)
                p3 = New Point(((btnPrevious.Width / 7) * 3) + btnPrevious.X, ((btnPrevious.Height - 1) / 2) + btnPrevious.Y)
                p = {p1, p2, p3}
                .FillPolygon(btnBrush, p)
                .DrawEllipse(New Pen(btnBrush), btnPrevious)

                '//////////////////     PLAYPAUSE BUTTON
                If btnPlayPauseState = True Then btnBrush = brushWhite Else btnBrush = brushWhite

                Select Case _PPBS
                    Case PlayPauseButtonStyles.Play
                        p1 = New Point(((btnPlayPause.Width / 5) * 2) + btnPlayPause.X, (btnPlayPause.Height / 4) + btnPlayPause.Y)
                        p2 = New Point(((btnPlayPause.Width / 5) * 2) + btnPlayPause.X, (btnPlayPause.Height - (btnPlayPause.Height / 4) - 2) + btnPlayPause.Y)
                        p3 = New Point(((btnPlayPause.Width / 9) * 6) + btnPlayPause.X, ((btnPlayPause.Height - 1) / 2) + btnPlayPause.Y)
                        p = {p1, p2, p3}
                        .FillPolygon(btnBrush, p)
                    Case PlayPauseButtonStyles.Pause
                        .SmoothingMode = Drawing2D.SmoothingMode.None
                        Dim R1 As New Rectangle(((btnPlayPause.Width / 7) * 2) + btnPlayPause.X, (btnPlayPause.Height / 4) + btnPlayPause.Y, btnPlayPause.Width / 7, (btnPlayPause.Height) - 2 * (btnPlayPause.Height / 4))
                        .FillRectangle(btnBrush, R1)
                        Dim R2 As New Rectangle((btnPlayPause.Width - ((btnPlayPause.Width / 7) * 3)) + btnPlayPause.X, (btnPlayPause.Height / 4) + btnPlayPause.Y, btnPlayPause.Width / 7, (btnPlayPause.Height) - 2 * (btnPlayPause.Height / 4))
                        .FillRectangle(btnBrush, R2)
                        .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                End Select
                .DrawEllipse(New Pen(btnBrush), btnPlayPause)

                '//////////////////      NEXT BUTTON
                If btnNextState = True Then btnBrush = brushWhite Else btnBrush = brushLightGray
                p1 = New Point(((btnNext.Width / 7) * 2) + btnNext.X, (btnNext.Height / 4) + btnNext.Y + 1)
                p2 = New Point(((btnNext.Width / 7) * 2) + btnNext.X, (btnNext.Height - (btnNext.Height / 4) - 1) + btnNext.Y)
                p3 = New Point(((btnNext.Width / 11) * 6) + btnNext.X, ((btnNext.Height - 1) / 2) + btnNext.Y)
                p = {p1, p2, p3}
                .FillPolygon(btnBrush, p)
                p1 = New Point(((btnNext.Width / 11) * 6) + btnNext.X, (btnNext.Height / 4) + btnNext.Y + 1)
                p2 = New Point(((btnNext.Width / 11) * 6) + btnNext.X, (btnNext.Height - (btnNext.Height / 4) - 1) + btnNext.Y)
                p3 = New Point(((btnNext.Width / 9) * 7) + btnNext.X, ((btnNext.Height - 1) / 2) + btnNext.Y)
                p = {p1, p2, p3}
                .FillPolygon(btnBrush, p)
                .DrawEllipse(New Pen(btnBrush), btnNext)

                '//////////////////      HORIZONTAL PROGRESSBAR
                .SmoothingMode = Drawing2D.SmoothingMode.None
                Dim Progress As Double = (_ProgressValue / _Max) * (prgH.Width - 1)
                .FillRectangle(New SolidBrush(Color.FromArgb(95, Color.Black)), New Rectangle(prgH.X, prgH.Y, prgH.Width - 1, prgH.Height - 1))
                If Progress > Width Then Progress = 1
                If Progress > 0 Then .FillRectangle(New SolidBrush(Color.FromArgb(150, Color.White)), New Rectangle(prgH.X + 1, prgH.Y + 1, Progress, prgH.Height - 1))
                .DrawRectangle(New Pen(Color.FromArgb(150, Color.White)), prgH)

                '//////////////////      VERTICAL PROGRESSBAR

                'Dim ProgressV As Double = CInt((_Val / _MaxV) * (prgV.Height - 2))
                '.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), New Rectangle(prgV.X, prgV.Y, prgV.Width - 1, prgV.Height - 1))
                'If Progress > 0 Then .FillRectangle(New SolidBrush(Color.FromArgb(255, Color.White)), New Rectangle(prgV.X, prgV.Y, prgV.Width, ProgressV))
                '.DrawRectangle(New Pen(Brushes.White), New Rectangle(prgV.X, prgV.Y, prgV.Width - 1, prgV.Height - 1))

                '//////////////////      TIME & TIME LEFT
                .DrawString(_Seconds, New Font("Segoe UI", 7), Brushes.White, time, New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
                .DrawString(_SecondsLeft, New Font("Segoe UI", 7), Brushes.White, timeleft, New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center})

                '//////////////////      SHOWNAME
                If Not String.IsNullOrEmpty(_ShowName) Then
                    .DrawString(Text & _ShowName, New Font("Segoe UI", 9), Brushes.White, shows, New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
                End If

                UpdateLayeredForm(B)
            End With
        Else
            e.Clear(Color.Transparent)
            UpdateLayeredForm(B)
        End If
    End Sub

End Class
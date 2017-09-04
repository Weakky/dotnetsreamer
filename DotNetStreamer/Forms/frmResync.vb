Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class frmReSync

#Region "GUI"
    Private Sub frmMain_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
        e.Graphics.Clear(Color.DimGray)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(2, 15, 15)), New Rectangle(1, 1, Width - 2, Height - 2))
        e.Graphics.SetClip(New Rectangle(1, 1, Width - 2, Height - 2))
        Dim path As New GraphicsPath() : path.AddEllipse(New Rectangle(-100, -100, Width + 198, Height + 198))
        Dim PGB As New PathGradientBrush(path)
        PGB.CenterColor = Color.FromArgb(4, 35, 35) : PGB.SurroundColors = {Color.FromArgb(2, 15, 15)}
        PGB.FocusScales = New Point(0.1F, 0.3F)
        e.Graphics.FillPath(PGB, path)
        e.Graphics.ResetClip()

        Dim LGB As New LinearGradientBrush(New Rectangle(1, 16, 13, 55), Color.FromArgb(42, 177, 80), Color.FromArgb(49, 149, 178), 90.0F)
        e.Graphics.FillRectangle(LGB, LGB.Rectangle)
    End Sub

    Private Sub lblClose_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lblClose.MouseDown
        lblClose.ForeColor = Color.Gray
        lblClose.BackColor = Color.Transparent
    End Sub

    Private Sub lblClose_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lblClose.MouseUp
        lblClose.ForeColor = Color.FromArgb(224, 224, 224)
    End Sub

    Private Sub lblClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblClose.Click
        Me.Close()
    End Sub

    Private Sub lblMinimize_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lblMinimize.MouseDown
        lblMinimize.ForeColor = Color.Gray
    End Sub

    Private Sub lblMinimize_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lblMinimize.MouseUp
        lblMinimize.ForeColor = Color.LightGray
    End Sub

    Private Sub lblMinimize_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

#End Region
#Region "Movable form"
    Dim IsDraggingForm As Boolean = False
    Private MousePos As New System.Drawing.Point(0, 0)

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left Then
            IsDraggingForm = True
            MousePos = e.Location
        End If
    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = MouseButtons.Left Then IsDraggingForm = False
    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If IsDraggingForm Then
            Dim temp As Point = New Point(Me.Location + (e.Location - MousePos))
            Me.Location = temp
            temp = Nothing
        End If
    End Sub
#End Region

#Region "ReSync Function"
    Public Function ReSyncSRT(ByVal SRTSource As String, ByVal intSeconds As Integer, ByVal intmilliSeconds As Integer, ByVal blnForward As Boolean) As String
        Dim SRTRegex As New Regex("(?<sequence>\d+)\r\n(?<start>\d{2}\:\d{2}\:\d{2},\d{3}) --\> (?<end>\d{2}\:\d{2}\:\d{2},\d{3})\r\n(?<text>[\s\S]*?\r\n\r\n)", RegexOptions.Compiled Or RegexOptions.ECMAScript)
        Dim sequence As Integer = 0
        Dim offset As Double = intSeconds + (intmilliSeconds / 1000)

        If blnForward = False Then
            offset = -offset
        End If

        Return SRTRegex.Replace(SRTSource, Function(m As Match) m.Value.Replace([String].Format("{0}" & vbCr & vbLf & "{1} --> {2}" & vbCr & vbLf, m.Groups("sequence").Value, m.Groups("start").Value, m.Groups("end").Value), [String].Format("{0}" & vbCr & vbLf & "{1:HH\:mm\:ss\,fff} --> {2:HH\:mm\:ss\,fff}" & vbCr & vbLf, System.Math.Max(System.Threading.Interlocked.Increment(sequence), sequence - 1), DateTime.Parse(m.Groups("start").Value.Replace(",", ".")).AddSeconds(offset), DateTime.Parse(m.Groups("end").Value.Replace(",", ".")).AddSeconds(offset))))
    End Function
    Public Sub ReSyncSRT(ByVal SRTSource As List(Of SubtitleParser.SubtitleItem), ByVal intSeconds As Integer, ByVal intmilliSeconds As Integer, ByVal blnForward As Boolean)
        Dim SRTRegex As New Regex("(?<sequence>\d+)\r\n(?<start>\d{2}\:\d{2}\:\d{2},\d{3}) --\> (?<end>\d{2}\:\d{2}\:\d{2},\d{3})\r\n(?<text>[\s\S]*?\r\n\r\n)", RegexOptions.Compiled Or RegexOptions.ECMAScript)
        Dim sequence As Integer = 0
        Dim offset As Double = intSeconds + (intmilliSeconds / 1000)

        If blnForward = False Then
            offset = -offset
        End If


        SRTSource.Iterate(Function(x)
                              x.BeginTime += offset
                              x.EndTime += offset
                              Return x
                          End Function)
    End Sub
#End Region

 
    Private Sub backwardBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles backwardBtn.Click
        frmMain.tmrSubtitlesAndSeekPosition.Stop()

        ReSyncSRT(SRTParsed, secNUD.Value, milliNUD.Value, True)
        SRTIndex = SubtitleParser.GetCurrentSubIndexFromTime()

        frmVLCDrawing.SubtitleText = String.Empty

        frmMain.tmrSubtitlesAndSeekPosition.Start()
    End Sub

    Private Sub forwardBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles forwardBtn.Click
        frmMain.tmrSubtitlesAndSeekPosition.Stop()

        ReSyncSRT(SRTParsed, secNUD.Value, milliNUD.Value, False)
        SRTIndex = SubtitleParser.GetCurrentSubIndexFromTime()

        frmVLCDrawing.SubtitleText = String.Empty

        frmMain.tmrSubtitlesAndSeekPosition.Start()
    End Sub

End Class
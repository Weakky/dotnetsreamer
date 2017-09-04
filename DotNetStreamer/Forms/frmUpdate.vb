Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Net

Public Class frmUpdate

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

    'Dim Client As New WebClient

    'Private Sub frmUpdatevb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    AddHandler Client.DownloadProgressChanged, AddressOf client_ProgressChanged
    '    AddHandler Client.DownloadFileCompleted, AddressOf client_DownloadCompleted

    '    Try
    '        Dim a As String = Client.DownloadString("http://dl.dropbox.com/u/19643954/update.txt")
    '        Client.DownloadFileAsync(New Uri(a.Split("|")(1)), Application.StartupPath & "\updated.exe")
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    'Private Sub client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
    '    Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
    '    Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
    '    Dim percentage As Double = bytesIn / totalBytes * 100
    '    MetroProgressbarHorizontal1.Value = Int32.Parse(Math.Truncate(percentage).ToString())
    '    Label1.Text = String.Format("Downloading update: {0} %", Integer.Parse(Math.Truncate(percentage).ToString()))
    'End Sub

    'Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    '    Dim sb As New System.Text.StringBuilder
    '    With sb
    '        .Append("echo off" & vbCrLf)
    '        .Append(":start" & vbCrLf)
    '        .Append("del " & Chr(34) & Application.ExecutablePath & Chr(34) & vbCrLf)
    '        .Append("if exist """ & Application.ExecutablePath & """ goto start" & vbCrLf)
    '        .Append("ren " & "updated.exe" & " " & System.IO.Path.GetFileName(Application.ExecutablePath).Replace(" ", "_") & vbCrLf)
    '        .Append("start """" " & Chr(34) & System.IO.Path.GetFileName(Application.ExecutablePath).Replace(" ", "_") & Chr(34) & vbCrLf)
    '        .Append("del %0" & vbCrLf)
    '    End With

    '    My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Update.bat", sb.ToString, False)

    '    Dim Proc As New Process
    '    With Proc.StartInfo
    '        .FileName = Application.StartupPath & "\Update.bat"
    '        .WindowStyle = ProcessWindowStyle.Hidden
    '        .CreateNoWindow = True
    '    End With
    '    Proc.Start() : Application.Exit()
    'End Sub
End Class
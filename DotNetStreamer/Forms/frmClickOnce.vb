Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Net
Imports ComponentAce.Compression.ZipForge
Imports System.IO

Public Class frmClickOnce

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


#Region "Auto Updater Events"

    Public WithEvents installer As New ClickOnce(My.Application.Info.Version)
    Private Sub frmClickOnce_Load(sender As Object, e As EventArgs) Handles Me.Load

        Settings.LoadSettings()
        installer.InstallApplication("http://dotnetstreamer.com/dotnetbin/.NET Streamer.application")

        If installer.debug Then
            TBProgress.Text = "100%"
            Dim G As Graphics = Me.CreateGraphics()
            Dim Status As String = "150 MB / 250 MB"
            Dim StatusWidth As Integer = G.MeasureString(Status, TBSpeed.Font).Width
            TBSpeed.Location = New Point(Me.Width - StatusWidth - 10, TBSpeed.Location.Y)
            TBSpeed.Text = Status
        End If
    End Sub

    Private Sub installer_ErrorMsg(Message As String) Handles installer.ErrorMsg
        TBStatus.Text = Message
    End Sub

    Private Sub installer_Updated() Handles installer.Updated
        If Not installer.debug Then
            Settings.Data.IsFirstRun = True
            Settings.SerializeToXML()

            Process.Start(My.Computer.FileSystem.SpecialDirectories.Programs & "\TRANSLU6DE\TRANSLU6DE\.NET Streamer.appref-ms")
            Process.GetCurrentProcess.Kill()
        End If

    End Sub

    Private Sub installer_InformationMsg(Message As String) Handles installer.InformationMsg
        TBStatus.Text = Message
    End Sub

    Private Sub installer_Installed() Handles installer.Installed
        Me.Hide()
        Dim VLCSetup As New SetupVLC()
        If VLCSetup.IsVLCInstalled AndAlso VLCSetup.VLCVersion = "2.1.5" Then frmMain.Show() Else frmVlcFullSetup.Show()
    End Sub

    Private Sub installer_ProgressChanged(Progress As Integer, Downloaded As Long, OutOf As Long) Handles installer.ProgressChanged
        TBProgress.Text = CStr(Progress) & "%"
        Dim G As Graphics = Me.CreateGraphics()
        Dim Status As String = String.Format("{0} MB / {1} MB", {Math.Round(Downloaded / 1024 / 1024, 2), Math.Round(OutOf / 1024 / 1024, 2)})
        Dim StatusWidth As Integer = G.MeasureString(Status, TBSpeed.Font).Width
        TBSpeed.Location = New Point(Me.Width - StatusWidth - 5, TBSpeed.Location.Y)
        TBSpeed.Text = String.Format("{0} MB / {1} MB", {Math.Round(Downloaded / 1024 / 1024, 2), Math.Round(OutOf / 1024 / 1024, 2)})
        Progressbar1.Value = Progress
    End Sub

#End Region
End Class
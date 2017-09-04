Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Net
Imports ComponentAce.Compression.ZipForge
Imports System.IO
Imports Microsoft.Win32

Public Class frmSetupVLC

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


    Private WithEvents VLCSetup As SetupVLCOld

    Private Sub frmUpdatevb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        VLCSetup = New SetupVLCOld()
        VLCSetup.SetupVLC()
    End Sub

    Private Sub VLCSetup_VLCDownloadFinished(e As Exception) Handles VLCSetup.VLCDownloadFinished
        If e Is Nothing Then
            Try
                MessageBox.Show("VLC has finished to download. Click 'OK' to install it, then restart .NET Streamer." & Environment.NewLine & _
                                "Be careful. It does require to launch it as admin.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Process.Start(VLCSetup.LocalPath)
                Application.Exit()
            Catch ex As System.ComponentModel.Win32Exception
               MessageBox.Show("The installer requires admin rights. Accept the UAC dialog or installation will fail.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Process.Start(VLCSetup.LocalPath)
                Application.Exit()
            Catch except As Exception
                MessageBox.Show(except.Message)
                Application.Exit()
            End Try
        Else
            lblSpeed.Text = String.Format("Error: {0}", e.Message)
            If Directory.Exists(Settings.Data.FolderVLCPath) Then Directory.Delete(Settings.Data.FolderVLCPath)
            If File.Exists(VLCSetup.LocalPath) Then File.Delete(VLCSetup.LocalPath)
        End If
    End Sub

    Private Sub VLCSetup_VLCDownloadProgress(Progress As Integer, TimeLeft As String, Speed As String) Handles VLCSetup.VLCDownloadProgress
        lblPercentage.Text = String.Format("{0} %", Integer.Parse(Math.Truncate(Progress).ToString()))
        pbProgress.Value = Progress
        lblSpeed.Text = Speed
        lblTimeLeft.Text = TimeLeft
    End Sub

    Private Sub VLCSetup_VLCRegistered(e As Exception) Handles VLCSetup.VLCRegistered
        If e Is Nothing Then
            Me.Hide() : Me.Opacity = 0 : Me.ShowInTaskbar = False
            frmMain.Show()
        Else
            MessageBox.Show(e.Message)
        End If
    End Sub
End Class
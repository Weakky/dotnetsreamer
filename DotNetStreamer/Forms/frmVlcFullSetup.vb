Imports System.Drawing.Drawing2D

Public Class frmVlcFullSetup

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

    Private WithEvents setupVLC As SetupVLC
    Private Const VLCRequiredVersion As String = "2.1.5"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        setupVLC = New SetupVLC()

        If setupVLC.IsVLCInstalled AndAlso setupVLC.VLCVersion <> VLCRequiredVersion Then 'If installed but wrong version
            PanelManager1.SelectedPanel = PnlWrongVlcVersion
        ElseIf setupVLC.IsVLCInstalled Then 'If installed and everything's fine
            PanelManager1.SelectedPanel = PnlFinishSetup
        ElseIf Not setupVLC.IsVLCInstalled AndAlso setupVLC.IsVLCAlreadyDownload Then 'if vlc's already downloaded
            PanelManager1.SelectedPanel = PnlAlreadyDownload
        Else
            setupVLC.Setup()
            PanelManager1.SelectedPanel = PnlDownloadVLC
        End If


    End Sub


    Private Sub btnWaitInstallNext_Click(sender As Object, e As EventArgs) Handles btnWaitInstallNext.Click
        If btnWaitInstallNext.Text <> "Waiting..." Then PanelManager1.SelectedPanel = PnlFinishSetup
    End Sub

    Private Sub setupVLC_VLCDownloadFinished(e As Exception) Handles setupVLC.VLCDownloadFinished
        If e Is Nothing Then
            PanelManager1.SelectedPanel = PnlLaunchSetup
        Else
            MessageBox.Show(e.Message, ".NET Streamer")
            If IO.File.Exists(setupVLC.LocalPath) Then IO.File.Delete(setupVLC.LocalPath)
        End If
    End Sub

    Private Sub setupVLC_VLCDownloadProgress(Progress As Integer, TimeLeft As String, Speed As String) Handles setupVLC.VLCDownloadProgress
        lblPercentage.Text = String.Format("{0} %", Integer.Parse(Math.Truncate(Progress).ToString()))
        pbProgress.Value = Progress
        lblSpeed.Text = Speed
        lblTimeLeft.Text = TimeLeft
    End Sub

    Private Sub setupVLC_VLCRegistered(path As String, e As Exception) Handles setupVLC.VLCRegistered
        If e Is Nothing Then
            lblPath.Text = String.Format("Path: {0}", path)
            lblVersion.Text = "Version: 2.1.5"
            lblInstalled.Text = "Installed: Successfully"

            btnWaitInstallNext.Enabled = True
            btnWaitInstallNext.Text = "Next"
            btnWaitInstallNext.Invalidate()
        Else
            lblInstalled.Text = String.Format("Error: {0}", e.Message)
            btnWaitInstallNext.Text = "Error."
        End If
    End Sub

    Private Sub btnLaunchSetup_Click(sender As Object, e As EventArgs) Handles btnNextLaunchSetup.Click
            setupVLC.LaunchSetupFile()
            setupVLC.StartCheckForVLCIntallation()
            PanelManager1.SelectedPanel = PnlWaitVlcInstallation
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles btnNextAlreadyDownloaded.Click
        setupVLC.LaunchSetupFile()
        setupVLC.StartCheckForVLCIntallation()
        PanelManager1.SelectedPanel = PnlWaitVlcInstallation
    End Sub

    Private Sub MetroButton2_Click_1(sender As Object, e As EventArgs) Handles MetroButton2.Click
        frmMain.Show() : Me.Close()
    End Sub

    Private Sub btnUninstaller_Click(sender As Object, e As EventArgs) Handles btnUninstaller.Click
        setupVLC.LaunchUninstaller()
        setupVLC.StartCheckForVLCUnintallation()
        PanelManager1.SelectedPanel = PnlWaitToUninstall
    End Sub

    Private Sub setupVLC_VLCUnregistered() Handles setupVLC.VLCUnregistered
        lblUninstallStatus.Text = "Status: Sucessfull"
        btnUninstallNext.Enabled = True
        btnUninstallNext.Text = "Next"
        btnUninstallNext.Invalidate()
    End Sub

    Private Sub btnUninstallNext_Click(sender As Object, e As EventArgs) Handles btnUninstallNext.Click
        setupVLC.Setup()
        If Not setupVLC.IsVLCInstalled AndAlso setupVLC.IsVLCAlreadyDownload Then 'if vlc's already downloaded
            PanelManager1.SelectedPanel = PnlAlreadyDownload
        Else
            PanelManager1.SelectedPanel = PnlDownloadVLC
        End If
    End Sub
End Class

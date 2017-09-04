Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports DotNetStreamer.Settings

Public Class frmPreferences

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

        Dim LGB As New LinearGradientBrush(New Rectangle(1, 24, 13, 55), Color.FromArgb(42, 177, 80), Color.FromArgb(49, 149, 178), 90.0F)
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

    Private fd As New FontDialog With {.ShowColor = True, .Color = Color.White}
    Private VideoDatabase As String = String.Empty

    Private Sub frmPreferences_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Settings.DeserializeFromXML()
        Dim subFont As Font = Settings.Data.SubFont
        Dim subColor As Color = Settings.Data.SubColor
        Dim showHint As Boolean = Settings.Data.ShowHint
        Dim autoDLSub As Boolean = Settings.Data.AutoDownloadSubtitle
        Dim Language As String = Settings.Data.Language
        Dim Host As String = Settings.Data.Host
        Dim NetworkCachingTime As String = CStr(Settings.Data.NetworkCachingTime)
        VideoDatabase = Settings.Data.VideoDatabase
        If VideoDatabase Is Nothing Then VideoDatabase = "Primewire.ag"


        lblPreview.ForeColor = subColor
        lblPreview.Font = New Font(subFont.Name, 9F)
        txtFont.Text = subFont.Name & "," & subFont.SizeInPoints
        cbVideoservice.Text = Host
        chkHint.Checked = showHint
        chkAutoSub.Checked = autoDLSub
        MetroCombobox1.Text = Language
        cbNetworkCaching.Text = NetworkCachingTime
        cbVideoDatabase.Text = VideoDatabase

        lblVersion.Text = "Build: " & My.Application.Info.Version.ToString
    End Sub

    Private Sub btnChooseFont_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChooseFont.Click

        If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFont.Text = fd.Font.Name & "," & CInt(fd.Font.SizeInPoints)
            lblPreview.Font = New Font(fd.Font.Name, 9.0F)
            lblPreview.ForeColor = fd.Color
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        Settings.Data.SubFont = fd.Font
        Settings.Data.SubColor = lblPreview.ForeColor
        Settings.Data.AutoDownloadSubtitle = chkAutoSub.Checked
        Settings.Data.ShowHint = chkHint.Checked
        Settings.Data.Language = MetroCombobox1.Text
        Settings.Data.Host = cbVideoservice.Text
        Settings.Data.NetworkCachingTime = CInt(cbNetworkCaching.Text)
        Settings.Data.VideoDatabase = cbVideoDatabase.Text

        If Not VideoDatabase.Equals(cbVideoDatabase.Text) Then
            MessageBox.Show("All the content preloaded into the treeview must be deleted. Just perform the requests again in order to use the new video database you just selected.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            frmMain.tvSeries.Nodes.Clear()

            Select Case cbVideoDatabase.Text
                Case "Primewire.ag"
                    frmMain.DataFetcher.StreamingServiceUsed = ShowsMoviesFetcher.StreamingService.Primewire
                Case ".NET Streamer API"
                    frmMain.DataFetcher.StreamingServiceUsed = ShowsMoviesFetcher.StreamingService.DotNetStreamerAPI
            End Select

        End If
        ' Settings.SerializeToXML()
        Close()
    End Sub

    Private Sub txtFont_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFont.TextChanged
        If txtFont.Text.Contains(",") Then
            Try
                lblPreview.Font = New Font(txtFont.Text.Split(",")(0), 9.0F)
                fd.Font = New Font(lblPreview.Font.Name, txtFont.Text.Split(",")(1))
            Catch ex As Exception : End Try
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


    Private Sub btnApply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
        Settings.Data.SubFont = fd.Font
        Settings.Data.SubColor = lblPreview.ForeColor
        Settings.Data.AutoDownloadSubtitle = chkAutoSub.Checked
        Settings.Data.ShowHint = chkHint.Checked
        Settings.Data.Language = MetroCombobox1.Text
        Settings.Data.Host = cbVideoservice.Text
        Settings.Data.NetworkCachingTime = CInt(cbNetworkCaching.Text)
        Settings.Data.VideoDatabase = cbVideoDatabase.Text

    End Sub


End Class
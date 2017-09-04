Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class frmDownload

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



    Private Sub lvSeason_DoubleClick(sender As Object, e As EventArgs) Handles lvSeason.DoubleClick
        If lvSeason.SelectedItems(0) Is Nothing Then Exit Sub
        Dim S As DownloadManager.Season = DirectCast(lvSeason.SelectedItems(0).Tag, DownloadManager.Season)
        lvEpisodes.Items.Clear()
        lvEpisodes.Items.AddRange(S.Episodes.ToListViewItemArray)
    End Sub

    Private Sub lvShow_DoubleClick(sender As Object, e As EventArgs) Handles lvShow.DoubleClick
        If lvShow.SelectedItems(0) Is Nothing Then Exit Sub
        Dim S As DownloadManager.Show = DirectCast(lvShow.SelectedItems(0).Tag, DownloadManager.Show)
        lvSeason.Items.Clear() : lvEpisodes.Items.Clear()
        lvSeason.Items.AddRange(S.Seasons.ToListViewItemArray)
    End Sub


    Private WithEvents MC As New ManagerClass With {.AutoDownload = True, .MaxDownloads = 3}

    Private Delegate Sub d_ListViewItemAdd(ByVal FD As FileData)
    Private Sub ListViewItemAdd(ByVal FD As FileData)
        If lvDownload.InvokeRequired Then
            lvDownload.Invoke(New d_ListViewItemAdd(AddressOf ListViewItemAdd), FD)
        Else
            Dim LVI As New ListViewItem(New String() {FD.Episode.Show, FD.Episode.Name, FD.SaveTo, "Queued."})
            lvDownload.Items.Add(LVI)
        End If
    End Sub

    Private Delegate Sub d_ListViewItemEdit(ByVal e As ManagerClass.ProgressEventArgs)
    Private Sub ListViewItemEdit(ByVal e As ManagerClass.ProgressEventArgs)
        If lvDownload.InvokeRequired Then
            lvDownload.Invoke(New d_ListViewItemEdit(AddressOf ListViewItemEdit), e)
        Else
            Dim LVT As ListViewItem = lvDownload.FindItemWithText(e.File.Episode.Name, True, 0, True)
            If LVT IsNot Nothing Then
                LVT.SubItems(3).Text = String.Format("{0}% - {1}", e.Percentage, e.Speed)
            End If
        End If
    End Sub

    Private Sub MC_DownloadStarted(sender As Object, e As FileData) Handles MC.DownloadStarted
        ListViewItemAdd(e)
        If lvEpisodes.Items.Count > 0 Then
            Dim LVI As ListViewItem = lvEpisodes.FindItemWithText(e.Episode.Name, True, 0, True)
            If LVI IsNot Nothing Then
                LVI.SubItems(1).Text = "Queued."
            End If
        End If
    End Sub

    Private Sub MC_ProgressChanged(ByVal sender As Object, ByVal e As ManagerClass.ProgressEventArgs) Handles MC.ProgressChanged
        'ToDo: Handle any additional stuff that should be done here whenever progress changes on a file.
        ListViewItemEdit(e)
        If lvEpisodes.Items.Count > 0 Then
            Dim LVI As ListViewItem = lvEpisodes.FindItemWithText(e.File.Episode.Name, True, 0, True)
            If LVI IsNot Nothing Then
                LVI.SubItems(1).Text = String.Format("{0}% - {1}", e.Percentage, e.Speed)
            End If
        End If
    End Sub

    Private Sub MC_ProgressFinished(ByVal sender As Object, ByVal e As ManagerClass.FileCompletionArgs) Handles MC.ProgressFinished
        Try

            DownloadAllSubtitles(e.File.Episode)
            Dim LVT As New ListViewItem
            LVT = lvDownload.FindItemWithText(e.File.Episode.Name, True, 0, True)
            If LVT IsNot Nothing Then
                LVT.SubItems(3).Text = "Getting Subs.."
            End If

            If lvEpisodes.Items.Count > 0 Then
                Dim LVI As ListViewItem = lvEpisodes.FindItemWithText(e.File.Episode.Name, True, 0, True)
                If LVI IsNot Nothing Then
                    LVI.SubItems(1).Text = "Getting Subs.."
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MC_FileError(ByVal sender As Object, ByVal e As ManagerClass.DownloadErrorArgs) Handles MC.FileError
        Try
            Dim LVT As New ListViewItem
            LVT = lvDownload.FindItemWithText(e.File.Episode.Name, True, 0, True)
            If Not LVT Is Nothing Then
                LVT.SubItems(3).Text = "Error."
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private WithEvents Subtitle As New SubtitlesClass()
    Private Sub DownloadAllSubtitles(Ep As DownloadManager.Episode)
        Subtitle.GetSubtitles(Ep, Settings.Data.Language)
    End Sub


    Private Sub Subtitle_OnSubtitleDownloadedFromEpisode(e As SubtitlesClass.DownloadCompletedArgs) Handles Subtitle.OnSubtitleDownloadedFromEpisode
        If e.Exception Is Nothing Then
            Dim LVT As New ListViewItem
            LVT = lvDownload.FindItemWithText(e.Episode.Name, True, 0, True)
            If LVT IsNot Nothing Then
                LVT.SubItems(3).Text = "Completed."
            End If

            If lvEpisodes.Items.Count > 0 Then
                Dim LVI As ListViewItem = lvEpisodes.FindItemWithText(e.Episode.Name, True, 0, True)
                If LVI IsNot Nothing Then
                    LVI.SubItems(1).Text = "Completed."
                End If
            End If
        End If
    End Sub

    Private Sub Subtitle_SearchCompletedDownload(e As SubtitlesClass.SearchCompletedArgs) Handles Subtitle.SearchCompletedDownload
        If e.exception Is Nothing Then
            For Each URL As String In e.Links
                Subtitle.DownloadSubtitle(e.Episode, URL)
            Next
        End If
    End Sub


    Public Sub AddToQueue(Episode As DownloadManager.Episode, Location As String)
        Episode.Path = Location

        Dim FD As New FileData
        FD.Episode = Episode
        FD.URL = Episode.URL
        FD.SaveTo = Location
        MC.Add(FD)
    End Sub

End Class
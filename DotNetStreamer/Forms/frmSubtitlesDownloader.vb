Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports DotNetStreamer.Settings

Public Class frmSubtitlesDownloader

#Region "GUI"
    Private Sub frmSubtitlesDownloader_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
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
        Me.Hide()
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

    Private lvwColumnSorter As ListViewColumnSorter
    WithEvents Subtitles As New SubtitlesClass

    Private Sub frmSubtitlesDownloader_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        lvwColumnSorter = New ListViewColumnSorter()
        lvSubtitles.ListViewItemSorter = lvwColumnSorter
    End Sub

    Public Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        If frmMain.DataFetcher IsNot Nothing AndAlso frmMain.DataFetcher.IsMovie Then
            SearchSubtitles(txtShowName.Text, Settings.Data.Language, False)
        Else
            SearchSubtitles(txtShowName.Text, txtSeason.Text, txtEpisode.Text, Settings.Data.Language, False)
        End If
    End Sub

    Dim autoDownload As Boolean = False
    Public Sub SearchSubtitles(ByVal ShowName As String, ByVal Season As String, ByVal Episode As String, Language As String, ByVal _AutoDownload As Boolean)
        If Not String.IsNullOrEmpty(ShowName) Then
            txtShowName.Text = ShowName : txtSeason.Text = Season : txtEpisode.Text = Episode
            autoDownload = _AutoDownload
            pcLoading.Show()
            Subtitles.GetSubtitles(ShowName, Season, Episode, Language)
            txtShowName.BorderColor = Color.FromArgb(75, 75, 75)
            txtSeason.BorderColor = Color.FromArgb(75, 75, 75)
            txtEpisode.BorderColor = Color.FromArgb(75, 75, 75)
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            If String.IsNullOrEmpty(ShowName) Then txtShowName.BorderColor = Color.Brown
            If String.IsNullOrEmpty(Season) Then txtSeason.BorderColor = Color.Brown
            If String.IsNullOrEmpty(Episode) Then txtEpisode.BorderColor = Color.Brown
        End If
    End Sub

    Public Sub SearchSubtitles(ByVal Movie As String, Language As String, ByVal _AutoDownload As Boolean)
        If Not String.IsNullOrEmpty(Movie) Then
            txtShowName.Text = Movie
            autoDownload = _AutoDownload
            pcLoading.Show()
            Subtitles.GetSubtitles(Movie, Language)
            txtShowName.BorderColor = Color.FromArgb(75, 75, 75)
            txtSeason.BorderColor = Color.FromArgb(75, 75, 75)
            txtEpisode.BorderColor = Color.FromArgb(75, 75, 75)
        Else
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            If String.IsNullOrEmpty(Movie) Then txtShowName.BorderColor = Color.Brown
        End If
    End Sub

    Private Sub Subtitles_OnSubtitleDownloaded(ByVal srtContent As String) Handles Subtitles.OnSubtitleDownloaded

        SRTText = vbNewLine & srtContent
        SRTParsed = SubtitleParser.Parse(SRTText)
        SRT = Nothing
        frmVLCDrawing.DrawSubtitles = True

        If SRTParsed.Count > 0 Then
            SRTIndex = SubtitleParser.GetCurrentSubIndexFromTime()
        Else
            SRTText = String.Empty
            SRTIndex = 0
            SRTParsed.Clear()
            frmVLCDrawing.SubtitleText = String.Empty
        End If

        If frmMain.VLCPlayer.input.Time > 0 Then SRTIndex = SubtitleParser.GetCurrentSubIndexFromTime()

        frmMain.EnableSubtitlesToolStripMenuItem.Enabled = True : frmMain.EnableSubtitlesToolStripMenuItem.Checked = True
        frmMain.ReSyncSubtitlesToolStripMenuItem1.Enabled = True
        frmMain.tmrSubtitlesAndSeekPosition.Start()
        pcLoading.Hide()
        Hide()
    End Sub

    Public ShowHintMessage As String = String.Empty

    Private Sub Subtitles_SearchCompleted(ByVal ListViewItems() As ListViewItem) Handles Subtitles.SearchCompletedLVI
        Try
            lvSubtitles.Items.Clear()
            If ListViewItems.Length = 0 Then lvSubtitles.Items.Add(New ListViewItem(New String() {"No subtitles available."}))
            lvSubtitles.Items.AddRange(ListViewItems)

            If autoDownload And ListViewItems.Length <> 0 Then

                ShowHintMessage = "Subtitles are being downloaded automatically."
                If frmMain.VLCPlayer.playlist.isPlaying Then frmMain.ShowHint(ShowHintMessage, String.Empty, 4)
                Subtitles.DownloadSubtitle(ListViewItems(0).SubItems(4).Text)

            ElseIf autoDownload = True And ListViewItems.Length = 0 Then
                ShowHintMessage = "No subtitle found in your language."
                If frmMain.VLCPlayer.playlist.isPlaying Then frmMain.ShowHint(ShowHintMessage, String.Empty, 4)
            End If
            autoDownload = False
            If Not IsNothing(lvwColumnSorter) Then
                lvwColumnSorter.SortColumn = 3
                lvwColumnSorter.Order = SortOrder.Ascending
            End If
            lvSubtitles.Sort()
            pcLoading.Hide()
        Catch ex As Exception
            pcLoading.Hide()
        End Try
    End Sub

    Private Sub lvSubtitles_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvSubtitles.DoubleClick
        If Not lvSubtitles.FocusedItem.SubItems(0).Text = "No subtitles available." Then
            pcLoading.Show()
            Subtitles.DownloadSubtitle(lvSubtitles.FocusedItem.SubItems(4).Text)
        End If
    End Sub

    Private Sub txtEpisode_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtEpisode.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Return Then
            btnSearch_Click(sender, Nothing)
        End If
    End Sub

    Private Sub txtSeason_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtSeason.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Return Then
            btnSearch_Click(sender, Nothing)
        End If
    End Sub

    Private Sub txtShowname_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtShowName.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Return Then
            btnSearch_Click(sender, Nothing)
        End If
    End Sub

    Private Sub lblShowNameHeader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblShowNameHeader.Click
        If lvwColumnSorter.SortColumn = 0 Then
            If lvwColumnSorter.Order = SortOrder.Ascending Then lvwColumnSorter.Order = SortOrder.Descending Else lvwColumnSorter.Order = SortOrder.Ascending
        Else
            lvwColumnSorter.SortColumn = 0
            lvwColumnSorter.Order = SortOrder.Ascending
        End If
        lvSubtitles.Sort()
    End Sub

    Private Sub lblSeasonHeader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblSeasonHeader.Click
        If lvwColumnSorter.SortColumn = 1 Then
            If lvwColumnSorter.Order = SortOrder.Ascending Then lvwColumnSorter.Order = SortOrder.Descending Else lvwColumnSorter.Order = SortOrder.Ascending
        Else
            lvwColumnSorter.SortColumn = 1
            lvwColumnSorter.Order = SortOrder.Ascending
        End If
        lvSubtitles.Sort()
    End Sub

    Private Sub lblEpisodeHeader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblEpisodeHeader.Click
        If lvwColumnSorter.SortColumn = 2 Then
            If lvwColumnSorter.Order = SortOrder.Ascending Then lvwColumnSorter.Order = SortOrder.Descending Else lvwColumnSorter.Order = SortOrder.Ascending
        Else
            lvwColumnSorter.SortColumn = 2
            lvwColumnSorter.Order = SortOrder.Ascending
        End If
        lvSubtitles.Sort()
    End Sub

    Private Sub lblLanguageHeader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblLanguageHeader.Click
        If lvwColumnSorter.SortColumn = 3 Then
            If lvwColumnSorter.Order = SortOrder.Ascending Then lvwColumnSorter.Order = SortOrder.Descending Else lvwColumnSorter.Order = SortOrder.Ascending
        Else
            lvwColumnSorter.SortColumn = 3
            lvwColumnSorter.Order = SortOrder.Ascending
        End If
        lvSubtitles.Sort()
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        frmPreferences.Show()
    End Sub

End Class

Public Class ListViewColumnSorter
    Implements System.Collections.IComparer

    Private ColumnToSort As Integer
    Private OrderOfSort As SortOrder
    Private ObjectCompare As CaseInsensitiveComparer

    Public Sub New()
        ColumnToSort = 0
        OrderOfSort = SortOrder.None
        ObjectCompare = New CaseInsensitiveComparer()
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim compareResult As Integer
        Dim listviewX As ListViewItem = CType(x, ListViewItem)
        Dim listviewY As ListViewItem = CType(y, ListViewItem)
        compareResult = ObjectCompare.Compare(listviewX.SubItems(ColumnToSort).Text, listviewY.SubItems(ColumnToSort).Text)
        If (OrderOfSort = SortOrder.Ascending) Then
            Return compareResult
        ElseIf (OrderOfSort = SortOrder.Descending) Then
            Return (-compareResult)
        Else
            Return 0
        End If
    End Function

    Public Property SortColumn() As Integer
        Set(ByVal Value As Integer)
            ColumnToSort = Value
        End Set
        Get
            Return ColumnToSort
        End Get
    End Property

    Public Property Order() As SortOrder
        Set(ByVal Value As SortOrder)
            OrderOfSort = Value
        End Set
        Get
            Return OrderOfSort
        End Get
    End Property
End Class
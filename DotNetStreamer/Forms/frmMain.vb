Imports System.IO
Imports System.Net
Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Text.RegularExpressions
Imports DotNetStreamer.SubtitleParser
Imports DotNetStreamer.ShowsMoviesFetcher
Imports DotNetStreamer.Settings
Imports DotNetStreamer.Service.Captcha
Imports System.Threading

Public Class frmMain

    'TODO: Finish Icefilms implementation

    'TODO: Fix issues with favourites
    'TODO: Disabled sorting (radiobuttons) with primewire.ag
    'TODO: Make error handling on .ns api requests
    'TODO: Fix controls resizing (videoplayer, treeview..)



#Region "GUI"

    Private Sub frmMain_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If SeriesView1.Bounds.Contains(e.Location) Then
            DropDownMenu1.Hide()
        End If
    End Sub

    Private Sub frmMain_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
        e.Graphics.Clear(Color.DimGray)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(2, 15, 15)), New Rectangle(1, 1, Width - 2, Height - 2))
        e.Graphics.SetClip(New Rectangle(1, 1, Width - 2, Height - 2))
        Dim path As New GraphicsPath() : path.AddEllipse(New Rectangle(-100, -100, Width + 198, Height + 198))
        Dim PGB As New PathGradientBrush(path)
        PGB.CenterColor = Color.FromArgb(4, 35, 35) : PGB.SurroundColors = {Color.FromArgb(2, 15, 15)}
        PGB.FocusScales = New Point(CInt(0.1F), CInt(0.3F))
        e.Graphics.FillPath(PGB, path)
        e.Graphics.ResetClip()
        Dim LGB As New LinearGradientBrush(New Rectangle(1, 16, 13, 55), Color.FromArgb(42, 177, 80), Color.FromArgb(49, 149, 178), 90.0F)
        If dontShowBorderGradient = True Then e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(2, 15, 15)), LGB.Rectangle) Else e.Graphics.FillRectangle(LGB, LGB.Rectangle)

    End Sub


    Private Sub lblClose_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lblClose.MouseDown
        lblClose.ForeColor = Color.Gray
        lblClose.BackColor = Color.Transparent
    End Sub

    Private Sub lblClose_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lblClose.MouseUp
        lblClose.ForeColor = Color.FromArgb(224, 224, 224)
    End Sub

    Private Sub lblClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblClose.Click
        Settings.SerializeToXML()
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
#Region "Windows Behaviour"

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
#Region "Resizable form"
    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCHITTEST As UInt32 = &H84
        Const WM_MOUSEMOVE As UInt32 = &H200
        Const HTBOTTOMLEFT As UInt32 = 16
        Const HTBOTTOMRIGHT As UInt32 = 17
        Const RESIZE_HANDLE_SIZE As Integer = 10
        Dim handled As Boolean = False

        If m.Msg = WM_NCHITTEST OrElse m.Msg = WM_MOUSEMOVE Then
            If PictureInPictureToolStripMenuItem.Checked Then

                Dim formSize As Size = Me.Size
                Dim screenPoint As New Point(m.LParam.ToInt32())
                Dim clientPoint As Point = Me.PointToClient(screenPoint)
                Dim hitBox As New Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)
                Dim lefthitBox As New Rectangle(0, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)

                'Dim G As Graphics = Me.CreateGraphics()
                'G.FillRectangle(Brushes.Fuchsia, lefthitBox)
                'G.FillRectangle(Brushes.Fuchsia, hitBox)

                If lefthitBox.Contains(clientPoint) Then
                    m.Result = CType(HTBOTTOMLEFT, IntPtr)
                    handled = True
                End If

                If hitBox.Contains(clientPoint) Then
                    m.Result = CType(HTBOTTOMRIGHT, IntPtr)
                    handled = True
                End If

            End If
        End If

        If Not handled Then
            MyBase.WndProc(m)
        End If
    End Sub
#End Region

#End Region

#Region "Private Fields"

    Private WithEvents WC As New WebClient
    Private WithEvents keyboardHook As New KeyboardHook
    Public WithEvents DataFetcher As ShowsMoviesFetcher
 
    Private PositionSeeker As New SeekPosition()

    Private pictureInPictureSize As Size = Nothing
    Private dontShowBorderGradient, fullscreen As Boolean

    Private WithEvents mDetector As New MouseDetector()

#End Region


    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        Me.ActiveControl = SeriesView1

        DataFetcher = New ShowsMoviesFetcher(Settings.Data.VideoDatabase)


        If Settings.Data.IsFirstRun Then
            Settings.Data.IsFirstRun = False
            WC.DownloadStringAsync(New Uri("http://dotnetstreamer.com/update.txt")) 'Download updatelogs on first launch
        End If

        DataFetcher.FetchEverythingOnSplashScreen()

    End Sub


#Region "Check if Watchseries' online"
    Private Sub CheckOnline()
        Try
            Dim req As HttpWebRequest = CType(WebRequest.Create("http://www.watchseries.biz/"), HttpWebRequest)
            req.Timeout = 10000
            Dim res As HttpWebResponse = CType(req.GetResponse, HttpWebResponse)
        Catch ex As Exception
            MessageBox.Show("Watchseries.biz seems to be offline." & Environment.NewLine & _
                            "Therefore, this application can not currently work." & Environment.NewLine & _
                            "Try later.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(0)
        End Try
    End Sub
#End Region

#Region "UpdateLogs"
    Private Sub WC_DownloadStringCompleted(sender As Object, e As DownloadStringCompletedEventArgs) Handles WC.DownloadStringCompleted
        If Split(e.Result, "|")(0).Contains(Me.ProductVersion) Then
            frmUpdateLog.txtUpdatelog.Text = Split(e.Result, "|")(2).Replace("/n/", vbNewLine)
            frmUpdateLog.Show()
        End If
    End Sub
#End Region

#Region "Search Engine"
    Dim FirstSearch As Boolean = True


    Private Sub Series_SearchCompleted(ByVal e As SearchCompletedArgs) Handles DataFetcher.SearchCompleted

        If e.exception Is Nothing Then

            pcLoading.Hide()
            If e.NoResults Then Exit Sub
            lblShowName.Text = e.Query.Name.ToUpper
            lblDescription.Text = e.Query.Description
            picThumbnail.ImageLocation = e.Query.ThumbnailURL

            Dim TN As TreeNode = ShowToTreeNode(e.Query, DataFetcher.StreamingServiceUsed)

            'If Not DataFetcher.StreamingServiceUsed = StreamingService.DotNetStreamerAPI Then
            Dim foundDuplicates As Boolean = False
            For Each node As TreeNode In tvSeries.Nodes
                If node.Text = TN.Text Then
                    foundDuplicates = True
                    tvSeries.SelectedNode = node
                    tvSeries.SelectedNode.Expand()
                    Exit Sub
                End If
            Next

            'Add '(W)' marker to nodes already watched
            For Each EpisodeInfo As SeriesClass.Episode In Settings.Data.AlreadyWatchedEpisode
                Dim nodeEpisode As TreeNode = TN.Nodes.FlattenTree().Where(Function(n) n.Text = EpisodeInfo.Name)(0)
                If nodeEpisode IsNot Nothing Then nodeEpisode.Text = String.Format("(W) {0}", nodeEpisode.Text)
            Next
            '   End If


            'Add treenode to the treeview
            tvSeries.TopNode = TN
            tvSeries.Nodes.Add(TN)

            Me.ActiveControl = SeriesView1


            If FirstSearch Then
                For i As Integer = 1 To 80 Step 10
                    pnlSeries.Height -= 10
                    pnl_VideoPlayback.Height -= 10
                    SeriesView1.Height -= 10
                Next
                FirstSearch = False
            End If
        Else
            MessageBox.Show(e.exception.Message, ".NET Streamer")
            pcLoading.Hide()
        End If

    End Sub
#End Region

#Region "AutoCompletion"
    Private Sub Series_SearchCompletionCompleted(e As SearchCompletionCompletedArgs) Handles DataFetcher.SearchCompletionCompleted
        pcLoading.Hide()
        If e.Exception Is Nothing Then
            DropDownMenu1.Items.Clear()
            DropDownMenu1.AddItems(e.Result.ToArray)
        Else
            DropDownMenu1.NoQuery = True
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If DropDownMenu1.SelectedItem >= 0 AndAlso DropDownMenu1.SelectedItem <= DropDownMenu1.Items.Count - 1 Then DropDownMenu1_ItemClicked(DropDownMenu1.SelectedItem)
                DropDownMenu1.Items.Clear()
                Me.ActiveControl = tvSeries
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If FullscreenModeToolStripMenuItem.Checked Then Exit Sub

        DropDownMenu1.BringToFront() : DropDownMenu1.Show()
        If String.IsNullOrEmpty(txtSearch.Text) Then DropDownMenu1.NoQuery = True Else DropDownMenu1.NoQuery = False


        DataFetcher.QueryText = txtSearch.Text

        Select Case DataFetcher.StreamingServiceUsed
            Case StreamingService.Primewire, StreamingService.Icefilms
                DataFetcher.GetAutoCompletion(txtSearch.Text, Nothing)

            Case StreamingService.DotNetStreamerAPI
                If rbMovies.Checked Then DataFetcher.GetAutoCompletion(txtSearch.Text, Categories.Movies)
                If rbTvShows.Checked Then DataFetcher.GetAutoCompletion(txtSearch.Text, Categories.Series)

        End Select

        pcLoading.Show()

    End Sub

    Private Sub DropDownMenu1_ItemClicked(Index As Integer) Handles DropDownMenu1.ItemClicked
        Try
            If txtSearch.Text <> String.Empty And DropDownMenu1.Items.Count > 0 Then
                pcLoading.Show()
                PlayControls.SendToBack()
                DropDownMenu1.SendToBack() : DropDownMenu1.Hide()
                Dim showURL As String = DropDownMenu1.Items(Index).Link
                If Index >= 0 AndAlso Index <= DropDownMenu1.Items.Count - 1 Then

                    Select Case DataFetcher.StreamingServiceUsed
                        Case StreamingService.Primewire
                            DataFetcher.Search(showURL, True)
                        Case StreamingService.Icefilms
                            DataFetcher.Search(DropDownMenu1.Items(Index).Title)
                        Case StreamingService.DotNetStreamerAPI

                            If rbMovies.Checked Then DataFetcher.Search(DropDownMenu1.Items(Index).Title, Nothing, Categories.Movies)
                            If rbTvShows.Checked Then DataFetcher.Search(DropDownMenu1.Items(Index).Title, Nothing, Categories.Series)
                            'DataFetcher.Search(txtSearch.Text)

                    End Select

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Load Movies on TreeViewNode Double Click"

    Private PlayMode As Boolean
    Private CurrentEpisode As SeriesClass.Episode
    Private URLPageEpisode As String = String.Empty
    Private isMovie As Boolean = False
    Private isHDQuality As Boolean = False
    Public Captcha As Captcha = Nothing

    Private Sub tvSeries_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvSeries.NodeMouseClick

        If e.Node.Tag Is Nothing Then Return
        Dim Query As Query = Nothing

        Select Case e.Node.Tag.GetType()

            Case GetType(Query)
                Query = DirectCast(e.Node.Tag, Query)

                If Not Query.Movie Then DataFetcher.GetEpisodeCount(Query.ID, Query.Seasons.Count, e.Node)

        End Select

    End Sub

    Private Sub DataFetcher_OnEpisodeCountGrabbed(e As EpisodeCountGrabbedEventArs) Handles DataFetcher.OnEpisodeCountGrabbed

        e.TVNode.Nodes.RemoveAt(0)

        Dim Query As Query = DirectCast(e.TVNode.Tag, Query)
        Dim seasonNumber As String = Split(e.TVNode.Text, "Season ")(1)


        For i As Integer = 1 To CInt(e.EpisodeCount)
            Dim episodeNumber As String = CStr(i)
            Dim episodeLink As String = String.Format("id={0}&season={1}&episode={2}", Query.ID, seasonNumber, episodeNumber)

            Dim Episode As New Episode(Query.Name, episodeLink)
            e.TVNode.Nodes.Add(New TreeNode(String.Format("Episode {0}", i)) With {.Tag = Episode})
        Next

    End Sub


    Private Sub tvSeries_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvSeries.NodeMouseDoubleClick
        Try
            Dim Query As Query = Nothing
            If e.Node.Tag Is Nothing Then Return
            Select Case e.Node.Tag.GetType()
                Case GetType(Query)
                    Query = TryCast(e.Node.Tag, Query)

                    If DataFetcher.StreamingServiceUsed = StreamingService.DotNetStreamerAPI Then Query.URL = "id=" & Query.ID

                    If Query IsNot Nothing AndAlso Query.Movie Then URLPageEpisode = Query.URL
                    DataFetcher.IsMovie = True

                Case GetType(Episode)
                    Dim Episode As Episode = TryCast(e.Node.Tag, Episode)
                    If Episode IsNot Nothing Then URLPageEpisode = Episode.Link

            End Select


            Select Case DataFetcher.StreamingServiceUsed

                Case StreamingService.Primewire

                    If Query Is Nothing Then
                        CurrentEpisode = New SeriesClass.Episode(e.Node.Text, e.Node.Parent.Text, e.Node.Parent.Parent.Text, Nothing) 'Create new structure of the current show to get multiple info about it easily
                    Else
                        CurrentEpisode = New SeriesClass.Episode(e.Node.Text, "", "", Query.URL)
                    End If

                    Select Case Settings.Data.Host
                        Case "Automatic"
                            DataFetcher.GetLinksFromPrimewire(URLPageEpisode, PrimewireHost.Automatic)
                        Case "Thefile.me"
                            DataFetcher.GetLinksFromPrimewire(URLPageEpisode, PrimewireHost.TheFile)
                        Case "Thevideo.me"
                            DataFetcher.GetLinksFromPrimewire(URLPageEpisode, PrimewireHost.TheVideo)
                        Case "Bestreams.net"
                            DataFetcher.GetLinksFromPrimewire(URLPageEpisode, PrimewireHost.Bestreams)
                        Case "Sharesix.com"
                            DataFetcher.GetLinksFromPrimewire(URLPageEpisode, PrimewireHost.Sharesix)
                        Case "Gorillavid.in"
                            DataFetcher.GetLinksFromPrimewire(URLPageEpisode, PrimewireHost.Gorillavid)
                        Case "Promptfile.com"
                            DataFetcher.GetLinksFromPrimewire(URLPageEpisode, PrimewireHost.Promptfile)
                    End Select


                Case StreamingService.Icefilms

                    'THIS IS JUST FOR TESTING PURPOSE AND MAY NEVER BE IMPLEMENTED

                    If Query Is Nothing Then
                        CurrentEpisode = New SeriesClass.Episode(e.Node.Text, e.Node.Parent.Parent.Text, e.Node.Parent.Parent.Parent.Text, Nothing) 'Create new structure of the current show to get multiple info about it easily
                    Else
                        CurrentEpisode = New SeriesClass.Episode(e.Node.Text, "", "", Query.URL)
                    End If

                    isHDQuality = e.Node.Parent.Text.Contains("HD")

                    'GrabNewCaptchaFromRecaptcha()
                    DataFetcher.GetLinksFromIcefilms(URLPageEpisode, IcefilmsHost.BillionsUpload, isHDQuality, Captcha)


                Case StreamingService.DotNetStreamerAPI
                    If Query Is Nothing Then
                        CurrentEpisode = New SeriesClass.Episode(e.Node.Text, e.Node.Parent.Text, e.Node.Parent.Parent.Text, Nothing) 'Create new structure of the current show to get multiple info about it easily
                    Else
                        CurrentEpisode = New SeriesClass.Episode(e.Node.Text, "", "", Query.URL)
                    End If

                    DataFetcher.GetLinksFromDotNetStreamerAPI(URLPageEpisode, DataFetcher.IsMovie)

            End Select


            pcLoading.Show()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Series_LinkCompleted(e As LinkDownloadedEventArgs) Handles DataFetcher.OnVideoLinkDownloaded
        Try

            If e.Exception Is Nothing Then

                Dim URL As String = e.Link

                'If there's no URL then show msgbox
                If URL = String.Empty AndAlso Not e.AutomaticHost Then
                    pcLoading.Hide()
                    MessageBox.Show("No video link has been found for this video hoster. Sorry." & Environment.NewLine & _
                                   "We're currently searching for another host.", ".NET Streamer")

                    If DataFetcher.StreamingServiceUsed = StreamingService.Primewire Then DataFetcher.GetLinksFromPrimewire(URLPageEpisode, PrimewireHost.Automatic)
                    If DataFetcher.StreamingServiceUsed = StreamingService.Icefilms Then DataFetcher.GetLinksFromIcefilms(URLPageEpisode, IcefilmsHost.Automatic, isHDQuality)
                    Exit Sub
                End If

                SeriesView1.Hide() : pcLoading.Show()
                'tmrUnconnected.Stop() : tmrUnconnected.Start()
                CurrentEpisode.Link = URL

                If Not DataFetcher.IsMovie Then
                    'Search for subtitles automatically

                    Dim ShowName As String = String.Empty
                    Dim Season As String = String.Empty
                    Dim Episode As String = String.Empty

                    Select Case DataFetcher.StreamingServiceUsed

                        Case StreamingService.Primewire, StreamingService.Icefilms
                            ShowName = Split(CurrentEpisode.Show, "(")(0).Trim
                            Season = Split(CurrentEpisode.Season, " ")(1).Trim
                            Episode = Split(Split(CurrentEpisode.Name, "E")(1), " -")(0).Trim

                        Case StreamingService.DotNetStreamerAPI
                            ShowName = CurrentEpisode.Show.Trim
                            Season = Split(CurrentEpisode.Season, " ")(1).Trim
                            Episode = Split(CurrentEpisode.Name, " ")(1).Trim

                    End Select


                    'Grab subtitles
                    frmSubtitlesDownloader.SearchSubtitles(ShowName, Season, Episode, Settings.Data.Language, Settings.Data.AutoDownloadSubtitle)
                Else
                    Dim MovieName As String = CurrentEpisode.Name.Split("(")(0).Trim()
                    frmSubtitlesDownloader.SearchSubtitles(MovieName, Settings.Data.Language, Settings.Data.AutoDownloadSubtitle)
                End If


                'Mark episode as already watched
                Dim NodeEpisode As TreeNode = tvSeries.FlattenTree().Where(Function(n) n.Text = CurrentEpisode.Name)(0)

                If Not NodeEpisode.Text.Contains("(W)") Then
                    NodeEpisode.Text = String.Format("(W) {0}", NodeEpisode.Text) 'Add '(W)' marker (= Watched)
                    Settings.Data.AlreadyWatchedEpisode.Add(CurrentEpisode)
                    '  Settings.SerializeToXML()
                End If

                LoadVideoBuffered(URL, Settings.Data.NetworkCachingTime) 'Actually loads and play the video

                If DataFetcher.StreamingServiceUsed = StreamingService.Primewire Then
                    PlayControls.ShowName = String.Format(" {0} ({1})", CurrentEpisode.Name, PrimewireFetcher.Hosts(e.PrimewireHost))
                Else
                    PlayControls.ShowName = String.Format(" {0} ({1})", CurrentEpisode.Name, IcefilmsFetcher.Hosts(e.IcefilmsHost))
                End If

                If Not PlayMode Then
                    pnlShowInfo.Hide()
                    Application.DoEvents()

                    For i As Integer = 1 To 20
                        pnl_VideoPlayback.Height += 1
                        pnlSeries.Height += 1
                    Next
                    For i As Integer = 1 To 55
                        pnlSeries.Height += 1
                    Next

                    PlayControls.BringToFront()
                    PlayControls.Show()
                End If
                PlayMode = True
                backBtn.Visible = True

                ' tmrUnconnected.Start()
                tmrSubtitlesAndSeekPosition.Start()
                pcLoading.Show()

                frmVLCDrawing.DrawVideoStateButton = frmVLCDrawing.PlayPauseBufferButtonStyles.Play : PlayControls.Text = "Loading:" : frmVLCDrawing.DrawNotification = True : frmVLCDrawing.NotificationText = "Loading Video.. May take a few minutes."

            Else
                pcLoading.Hide()
                MessageBox.Show("No video link has been found for this video hoster. Sorry." & Environment.NewLine & _
                                "We're currently searching for another host.", ".NET Streamer")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub LoadVideoBuffered(URL As String, seconds As Integer)
        If VLCPlayer.playlist.items.count <> 0 Then
            VLCPlayer.playlist.stop()
            VLCPlayer.playlist.items.clear()
        End If

        Dim Options As String() = New String() {String.Format(":network-caching={0}", CStr(seconds * 1000))}
        VLCPlayer.playlist.add(URL, Nothing, Options)
        VLCPlayer.Volume = 50
        VLCPlayer.playlist.play()
    End Sub
    Private Sub LoadVideoAt(URL As String, secondsToStart As Integer)
        If VLCPlayer.playlist.items.count <> 0 Then
            VLCPlayer.playlist.items.clear()
            VLCPlayer.playlist.stop()
        End If

        Dim Options As String() = New String() {String.Format(":network-caching={0}", CStr(Settings.Data.NetworkCachingTime * 1000)), String.Format(":start-time={0}", secondsToStart)}
        VLCPlayer.playlist.add(URL, Nothing, Options)
        VLCPlayer.playlist.play()
        Options = Nothing
    End Sub

    Public Sub SolveNewCaptcha()
        Captcha = SolveMedia.Request("NC4YrdltTKcQ1MjApF5Nx1bvC32DgGzt")
        frmCaptcha.PictureBox1.Image = Captcha.image
        frmCaptcha.Show()

        While (Not captchaSolved)
            Application.DoEvents()
        End While

        frmCaptcha.Hide()
        captchaSolved = False
    End Sub

    Public Sub GrabNewCaptchaFromRecaptcha()
        Captcha = Recaptcha.Request("")
        frmCaptcha.PictureBox1.Image = Captcha.image
        frmCaptcha.Show()

        While (Not captchaSolved)
            Application.DoEvents()
        End While

        frmCaptcha.Hide()
        captchaSolved = False
    End Sub

#End Region

#Region "Current Video Time"

    Private Sub VLCPlayer_MediaPlayerOpening(sender As Object, e As EventArgs) Handles VLCPlayer.MediaPlayerOpening
        PictureInPictureToolStripMenuItem.Enabled = True
        FullscreenModeToolStripMenuItem.Enabled = True

        frmVLCDrawing.Show(Me)

        If Settings.Data.ShowHint AndAlso Settings.Data.AutoDownloadSubtitle = False Then
            ShowHint("Did you know you could download subtitles for this episode?", "Download subtitles", 8)
        End If
        If Settings.Data.ShowHint AndAlso Settings.Data.AutoDownloadSubtitle AndAlso Not String.IsNullOrEmpty(frmSubtitlesDownloader.ShowHintMessage) Then
            ShowHint(frmSubtitlesDownloader.ShowHintMessage, String.Empty, 4)
        End If
    End Sub

    Private Sub VLCPlayer_MediaPlayerTimeChanged(sender As Object, e As AxAXVLC.DVLCEvents_MediaPlayerTimeChangedEvent) Handles VLCPlayer.MediaPlayerTimeChanged
        If VLCPlayer.playlist.items.count = 0 Then Exit Sub




        PlayControls.ProgressMaximum = VLCPlayer.input.Length
        PlayControls.ProgressValue = CDbl(e.time)
    End Sub

    Private Sub PlayControls_Progressbar_HoverCurrentTime(sender As Object, currentTime As TimeSpan) Handles PlayControls.Progressbar_HoverCurrentTime
        PlayControls.Seconds = currentTime.Hours.ToString("D2") & ":" & currentTime.Minutes.ToString("D2") & ":" & currentTime.Seconds.ToString("D2")
    End Sub

    Private Sub tmrSubtitlesAndSeekPosition_Tick(sender As Object, e As EventArgs) Handles tmrSubtitlesAndSeekPosition.Tick

        If Not PlayControls.MouseOnProgressBar Then
            Dim TS1 As New TimeSpan(0, 0, 0, 0, CInt(PositionSeeker.MilliSecondsElapsed))
            PlayControls.Seconds = TS1.Hours.ToString("D2") & ":" & TS1.Minutes.ToString("D2") & ":" & TS1.Seconds.ToString("D2")

            Dim TS2 As New TimeSpan(0, 0, 0, 0, CInt(VLCPlayer.input.Length - PositionSeeker.MilliSecondsElapsed))
            PlayControls.SecondsLeft = "-" & TS2.Hours.ToString("D2") & ":" & TS2.Minutes.ToString("D2") & ":" & TS2.Seconds.ToString("D2")
        End If

        If frmVLCDrawing.IsMouseOverVLC Then frmVLCDrawing.DrawExitButton = True
        frmVLCDrawing.PaintHook()
        If SRTParsed.Count > 0 Then ParseSubtitle()
    End Sub

#End Region

#Region "Play/Pause/Next/Previous/Fullscreen/Duration ProgressBar"

    Private Sub PlayControls_Next_Clicked(ByVal sender As Object, ByVal e As EventArgs) Handles PlayControls.Next_Clicked
        On Error Resume Next
        If Not VLCPlayer.playlist.items.count = 0 And Not VLCPlayer.input.Length - VLCPlayer.input.Time < 30 Then VLCPlayer.input.Time += 30
    End Sub

    Private Sub PlayControls_Previous_Clicked(ByVal sender As Object, ByVal e As EventArgs) Handles PlayControls.Previous_Clicked
        On Error Resume Next
        If Not VLCPlayer.playlist.items.count = 0 And Not VLCPlayer.input.Length - VLCPlayer.input.Time < 30 Then VLCPlayer.input.Time -= 30
    End Sub

    Private Sub PlayControls_Progressbar_Clicked(ByVal Sender As Object, ByVal clickedTimeMilliseconds As Double) Handles PlayControls.Progressbar_Clicked
        If Not VLCPlayer.playlist.items.count = 0 Then VLCPlayer.input.Time = clickedTimeMilliseconds
        PositionSeeker.ResetAndStartAt(clickedTimeMilliseconds)
        SRTIndex = SubtitleParser.GetCurrentSubIndexFromTime() 'SubtitleParser.GetCurrentSubFromTime(New TimeSpan(0, 0, 0, 0, VLCPlayer.input.Time).TotalSeconds, SRTParsed).ID
        frmVLCDrawing.SubtitleText = String.Empty
    End Sub

    Private Sub PlayControls_FullScreen_Clicked(sender As Object, e As EventArgs) Handles PlayControls.FullScreen_Clicked
        ShowHint("Press 'Escape' to leave/enter fullscreen mode.", String.Empty, 6)
        MakeFullScreen()
    End Sub

#End Region
#Region "Shortcuts Play/Pause ~ Leave/Go FullScreen"

    Private Declare Function GetForegroundWindow Lib "user32.dll" () As Int32
    Private Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As Int32, ByVal lpString As String, ByVal cch As Int32) As Int32
    Private Function GetActiveWindowTitle() As String
        Dim MyStr As String
        MyStr = New String(Chr(0), 100)
        GetWindowText(GetForegroundWindow, MyStr, 100)
        MyStr = MyStr.Substring(0, InStr(MyStr, Chr(0)) - 1)
        Return MyStr
    End Function

    Private Sub keyboard_KeyDown(Key As Keys) Handles keyboardHook.KeyDown
        If GetActiveWindowTitle.Contains("DotNet Streamer") Then 'Make sure dotnetstreamer is focused
            Debug.WriteLine(Key)
            Dim itemsCount As Integer = VLCPlayer.playlist.items.count
            If itemsCount = 0 AndAlso Not Me.ActiveControl.ToString = String.Format("System.Windows.Forms.TextBox, Text: {0}", txtSearch.Text) Then Return

            If Key = Keys.Escape Then
                MakeFullScreen()

            ElseIf Key = Keys.Space And Not Me.ActiveControl.ToString = String.Format("System.Windows.Forms.TextBox, Text: {0}", txtSearch.Text) Then 'Avoid the shortcut to be working when the window isn't on top
                If itemsCount = 0 Then Return

                If VLCPlayer.playlist.isPlaying Then
                    VLCPlayer.playlist.togglePause()
                Else
                    VLCPlayer.playlist.play()
                End If

            ElseIf Key = Keys.Down AndAlso Me.ActiveControl.ToString = String.Format("System.Windows.Forms.TextBox, Text: {0}", txtSearch.Text) Then

                If DropDownMenu1.SelectedItem = -1 Then
                    DropDownMenu1.SelectedItem = 0
                Else
                    If Not DropDownMenu1.SelectedItem >= DropDownMenu1.Items.Count - 1 Then
                        DropDownMenu1.SelectedItem += 1
                    End If
                End If
                DropDownMenu1.Invalidate()

            ElseIf Key = Keys.Up AndAlso Me.ActiveControl.ToString = String.Format("System.Windows.Forms.TextBox, Text: {0}", txtSearch.Text) Then

                If DropDownMenu1.SelectedItem = -1 Then
                    DropDownMenu1.SelectedItem = 0
                Else
                    If Not DropDownMenu1.SelectedItem <= 0 Then
                        DropDownMenu1.SelectedItem -= 1
                    End If
                End If
                DropDownMenu1.Invalidate()

            ElseIf Key = Keys.Up Then
                MetroProgressbarVertical1.Value += 10
                VLCPlayer.Volume = MetroProgressbarVertical1.Value

            ElseIf Key = Keys.Down Then
                MetroProgressbarVertical1.Value -= 10
                VLCPlayer.Volume = MetroProgressbarVertical1.Value

            End If
        Else
            Debug.WriteLine(GetActiveWindowTitle())
        End If
    End Sub

#Region "Old shortcut method"
    'Protected Overloads Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
    '    Dim itemsCount As Integer = VLCPlayer.playlist.items.count
    '    If itemsCount = 0 AndAlso Not Me.ActiveControl.ToString = String.Format("System.Windows.Forms.TextBox, Text: {0}", txtSearch.Text) Then Return False

    '    If keyData = Keys.Escape Then
    '        MakeFullScreen()
    '        Return True
    '    ElseIf keyData = Keys.Space And Not Me.ActiveControl.ToString = String.Format("System.Windows.Forms.TextBox, Text: {0}", txtSearch.Text) Then 'Avoid the shortcut to be working when the window isn't on top
    '        If itemsCount = 0 Then Return False

    '        If VLCPlayer.playlist.isPlaying Then
    '            VLCPlayer.playlist.togglePause()
    '        Else
    '            VLCPlayer.playlist.play()
    '        End If

    '        Return True
    '    ElseIf keyData = Keys.Down And Me.ActiveControl.ToString = String.Format("System.Windows.Forms.TextBox, Text: {0}", txtSearch.Text) Then

    '        If DropDownMenu1.SelectedItem = -1 Then
    '            DropDownMenu1.SelectedItem = 0
    '        Else
    '            If Not DropDownMenu1.SelectedItem >= DropDownMenu1.Items.Count - 1 Then
    '                DropDownMenu1.SelectedItem += 1
    '            End If
    '        End If
    '        DropDownMenu1.Invalidate()
    '        Return True
    '    ElseIf keyData = Keys.Up And Me.ActiveControl.ToString = String.Format("System.Windows.Forms.TextBox, Text: {0}", txtSearch.Text) Then

    '        If DropDownMenu1.SelectedItem = -1 Then
    '            DropDownMenu1.SelectedItem = 0
    '        Else
    '            If Not DropDownMenu1.SelectedItem <= 0 Then
    '                DropDownMenu1.SelectedItem -= 1
    '            End If
    '        End If
    '        DropDownMenu1.Invalidate()
    '        Return True

    '    ElseIf keyData = Keys.Up Then
    '        MetroProgressbarVertical1.Value += 10
    '        VLCPlayer.Volume += 10
    '        Return True

    '    ElseIf keyData = Keys.Down Then
    '        MetroProgressbarVertical1.Value -= 10
    '        VLCPlayer.Volume -= 10
    '        Return True
    '    Else
    '        Return MyBase.ProcessCmdKey(msg, keyData)
    '    End If
    'End Function
#End Region

#End Region

#Region "Video State"

    Private Sub PlayControls_PlayPause_Clicked(ByVal sender As Object, ByVal e As EventArgs) Handles PlayControls.PlayPause_Clicked
        Select Case PlayControls.PlayPauseButton
            Case SpectrumPanel.PlayPauseButtonStyles.Pause
                VLCPlayer.playlist.pause()
            Case SpectrumPanel.PlayPauseButtonStyles.Play
                VLCPlayer.playlist.play()
        End Select
    End Sub

    Private _IsBuffering As Boolean = False
    Private _oldCache As Integer = 0
    Private WithEvents timerBufferSync As New System.Windows.Forms.Timer() With {.Interval = 2000}
    Private Sub VLCPlayer_MediaPlayerBuffering(sender As Object, e As AxAXVLC.DVLCEvents_MediaPlayerBufferingEvent) Handles VLCPlayer.MediaPlayerBuffering
        If e.cache = 100 Then
            _IsBuffering = False : PlayControls.Text = "Now playing: " : frmVLCDrawing.DrawVideoStateButton = frmVLCDrawing.PlayPauseBufferButtonStyles.Play

            timerBufferSync.Start()

        ElseIf _oldCache <> e.cache AndAlso e.cache > 0 Then

            _IsBuffering = True
            PositionSeeker.StopSeeking()
            pcLoading.Hide()
            PlayControls.Text = String.Format("Buffering: {0}%", e.cache)
            frmVLCDrawing.DrawNotification = False
            frmVLCDrawing.DrawVideoStateButton = frmVLCDrawing.PlayPauseBufferButtonStyles.Buffering
            frmVLCDrawing.BufferingValue = e.cache
            End If
            _oldCache = e.cache
    End Sub

    Private Sub VLCPlayer_MediaPlayerStopped(sender As Object, e As EventArgs) Handles VLCPlayer.MediaPlayerStopped
        PositionSeeker.StopSeeking()
        PlayControls.PlayPauseButton = SpectrumPanel.PlayPauseButtonStyles.Play
        PlayControls.Text = ".NET Streamer"
    End Sub
    Private Sub VLCPlayer_MediaPlayerEndReached(sender As Object, e As EventArgs) Handles VLCPlayer.MediaPlayerEndReached
        Dim CurrentSeconds As Double = New TimeSpan(0, 0, 0, 0, VLCPlayer.input.Time).TotalSeconds
        Dim VideoLength As Double = New TimeSpan(0, 0, 0, 0, VLCPlayer.input.Length).TotalSeconds

        If CurrentSeconds + 5 < VideoLength AndAlso Not _IsBuffering Then
            frmHint.ShowHint("Reconnecting..", String.Empty, 5)
            PlayControls.PlayPauseButton = SpectrumPanel.PlayPauseButtonStyles.Play
            PlayControls.Text = "Reconnecting: "
            frmVLCDrawing.DrawNotification = True : frmVLCDrawing.NotificationText = "VLC Error: Reconnecting.. Wait a few seconds.."
            LoadVideoAt(CurrentEpisode.Link, New TimeSpan(0, 0, 0, 0, VLCPlayer.input.Time).TotalSeconds)
            Debug.WriteLine("RECONNECTING VIDEO FAILED")
            Debug.WriteLine("RECONNECTING VIDEO FAILED")
            Debug.WriteLine("RECONNECTING VIDEO FAILED")
            Debug.WriteLine("RECONNECTING VIDEO FAILED")
        ElseIf _IsBuffering And CurrentSeconds = 0 Then
            MessageBox.Show("File does not exist on remote server. Try using a specific host under preferences.", ".NET Streamer")
            backBtn_Click(Me, Nothing)
            frmPreferences.Show()
        Else
            SetPlayerBackToOriginalState()
            Debug.WriteLine("video ended properly")
        End If
    End Sub
    Private Sub VLCPlayer_MediaPlayerPlaying(sender As Object, e As EventArgs) Handles VLCPlayer.MediaPlayerPlaying

        PositionSeeker.ResetAndStartAt(New TimeSpan(0, 0, 0, 0, VLCPlayer.input.Time).TotalMilliseconds) : PositionSeeker.Start()
        Debug.WriteLine("State: Playing ; Event fired | PS:{0} - VLCTime: {1}", PositionSeeker.MilliSecondsElapsed, VLCPlayer.input.Time)
        PlayControls.PlayPauseButton = SpectrumPanel.PlayPauseButtonStyles.Pause
        frmVLCDrawing.DrawVideoStateButton = frmVLCDrawing.PlayPauseBufferButtonStyles.Play
        PlayControls.Text = "Now Playing: "

    End Sub
    Private Sub VLCPlayer_MediaPlayerPaused(sender As Object, e As EventArgs) Handles VLCPlayer.MediaPlayerPaused
        PositionSeeker.StopSeeking()
        PlayControls.PlayPauseButton = SpectrumPanel.PlayPauseButtonStyles.Play
        frmVLCDrawing.DrawVideoStateButton = frmVLCDrawing.PlayPauseBufferButtonStyles.Pause
        PlayControls.Text = "Paused: "
        Debug.WriteLine("State: Paused ; Event fired | PS:{0} - VLCTime: {1}", PositionSeeker.MilliSecondsElapsed, VLCPlayer.input.Time)
    End Sub

    Private Sub SetPlayerBackToOriginalState()
        FullscreenModeToolStripMenuItem.Enabled = False : PictureInPictureToolStripMenuItem.Enabled = False
        PositionSeeker.StopSeeking()
        PlayControls.PlayPauseButton = SpectrumPanel.PlayPauseButtonStyles.Play
        PlayControls.Text = ".NET Streamer "
        PlayControls.ShowName = "- Player"
        PlayControls.Seconds = "00:00:00"
        PlayControls.SecondsLeft = "-00:00:00"
    End Sub

#End Region

#Region "Subtitles"

#Region "Subtitles Engine"
    Private ID As Integer = 0
    Private ShowOnlyOnce As Boolean = False

    Private Sub ParseSubtitle()
        If VLCPlayer.playlist.items.count = 0 Then Exit Sub
        If SRTParsed.Count = 0 Then Exit Sub

        'If subtitle is ended
        If SRTIndex = SRTParsed.Count Then
            frmVLCDrawing.SubtitleText = String.Empty
            Exit Sub
        End If

        If SRT Is Nothing Then SRT = SRTParsed(SRTIndex)

        If SRT IsNot Nothing AndAlso SRT.ID - 1 <> SRTIndex Then
            SRT = SRTParsed(SRTIndex)
        End If

        Dim TS As New TimeSpan(0, 0, 0, 0, CInt(PositionSeeker.MilliSecondsElapsed))

        If TS.TotalSeconds >= SRT.BeginTime AndAlso TS.TotalSeconds <= SRT.EndTime Then
            frmVLCDrawing.SubtitleText = SRT.Text
        ElseIf TS.TotalSeconds > SRT.EndTime Then
            SRTIndex += 1
            Exit Sub
        Else
            frmVLCDrawing.SubtitleText = String.Empty
        End If

        TS = Nothing
    End Sub

#End Region

#Region "Subtitle Menu"

    Private Sub lblSubtitlesMenu_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblSubtitlesMenu.Click
        cmsSubtitles.Show(PointToScreen(New Point(lblSubtitlesMenu.Location.X, lblSubtitlesMenu.Location.Y + lblSubtitlesMenu.Height)))
    End Sub

    Private Sub EnableSubtitlesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EnableSubtitlesToolStripMenuItem.Click
        If EnableSubtitlesToolStripMenuItem.Checked Then
            frmVLCDrawing.SubtitleText = String.Empty
            frmVLCDrawing.DrawSubtitles = False
            EnableSubtitlesToolStripMenuItem.Checked = False
        Else
            EnableSubtitlesToolStripMenuItem.Checked = True
            frmVLCDrawing.DrawSubtitles = True
        End If
    End Sub

    Private Sub DownloadSubtitlesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DownloadSubtitlesToolStripMenuItem.Click
        frmSubtitlesDownloader.Show()
    End Sub

    Private Sub LoadsrtToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadsrtToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "Subtitles Files (*.srt)|*.srt|All Files (*.*)|*.*"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim sr As New IO.StreamReader(ofd.FileName)
            If Not sr.CurrentEncoding.ToString = "System.Text.UTF8Encoding" Then
                sr = New IO.StreamReader(ofd.FileName, System.Text.Encoding.UTF8)
                SRTText = vbNewLine & sr.ReadToEnd() : sr.Close()
                SRTParsed = SubtitleParser.Parse(SRTText)
                SRTIndex = SubtitleParser.GetCurrentSubIndexFromTime() 'SubtitleParser.GetCurrentSubFromTime(New TimeSpan(0, 0, 0, 0, VLCPlayer.input.Time).TotalSeconds, SRTParsed).ID
            Else
                sr = New IO.StreamReader(ofd.FileName, System.Text.Encoding.Default)
                SRTText = vbNewLine & sr.ReadToEnd() : sr.Close()
                SRTParsed = SubtitleParser.Parse(SRTText)
                SRTIndex = SubtitleParser.GetCurrentSubIndexFromTime()
                'SRT = SRTParsed(SRTIndex)
            End If
            tmrSubtitlesAndSeekPosition.Start()
            EnableSubtitlesToolStripMenuItem.Checked = True : EnableSubtitlesToolStripMenuItem.Enabled = True : ReSyncSubtitlesToolStripMenuItem1.Enabled = True
            frmVLCDrawing.DrawSubtitles = True
        End If
    End Sub

#End Region

#End Region

#Region "Video Menu"
    Private Sub FullscreenModeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles FullscreenModeToolStripMenuItem.Click
        If VLCPlayer.playlist.items.count = 0 Then Exit Sub
        MakeFullScreen()
    End Sub

    Private Sub PictureInPictureToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles PictureInPictureToolStripMenuItem.CheckedChanged
        If Not PictureInPictureToolStripMenuItem.Checked Then
            Settings.Data.PictureInPictureSize = pictureInPictureSize
            ' Settings.SerializeToXML()
        End If
    End Sub


    Private Sub PictureInPictureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureInPictureToolStripMenuItem.Click
        If VLCPlayer.playlist.items.count = 0 Then Exit Sub
        MakePictureInPictureMode()
    End Sub

    Private Sub MakePictureInPictureMode()
        If Not PictureInPictureToolStripMenuItem.Checked Then
            dontShowBorderGradient = True : VLCPlayer.Toolbar = True : PictureInPictureToolStripMenuItem.Checked = True

            Me.TopMost = True
            Me.MinimumSize = New Size(150, 150)
            Me.Size = Settings.Data.PictureInPictureSize
            Me.Top = Screen.FromControl(Me).WorkingArea.Top
            Me.Left = Screen.FromControl(Me).WorkingArea.Right - Me.Width

            pnl_VideoPlayback.BringToFront()
            pnl_VideoPlayback.Location = New Point(2, 2)
            pnl_VideoPlayback.BorderStyle = BorderStyle.None
            pnl_VideoPlayback.Size = New Size(Width - 4, Height - 4)
        End If
    End Sub

    Private Sub MakeFullScreen()
        If Not FullscreenModeToolStripMenuItem.Checked And PictureInPictureToolStripMenuItem.Checked = False Then
            VLCPlayer.Toolbar = True : FullscreenModeToolStripMenuItem.Checked = True : Me.TopMost = True : dontShowBorderGradient = True

            Me.Size = Screen.FromControl(Me).Bounds.Size
            Me.Location = Screen.FromControl(Me).Bounds.Location

            pnl_VideoPlayback.BorderStyle = BorderStyle.None
            pnl_VideoPlayback.Location = New Point(0, 0)
            pnl_VideoPlayback.Size = New Size(Width, Height)
            pnl_VideoPlayback.BringToFront()

        ElseIf FullscreenModeToolStripMenuItem.Checked Or PictureInPictureToolStripMenuItem.Checked Then
            VLCPlayer.Toolbar = False : FullscreenModeToolStripMenuItem.Checked = False : PictureInPictureToolStripMenuItem.Checked = False
            TopMost = False : dontShowBorderGradient = False

            Me.Size = New Size(849, 527)
            Me.MinimumSize = Me.Size
            Me.Left = CInt((Screen.FromControl(Me).Bounds.Width - Me.Width) / 2)
            Me.Top = CInt((Screen.FromControl(Me).Bounds.Height - Me.Height) / 2)

            pnl_VideoPlayback.BorderStyle = BorderStyle.FixedSingle
            pnl_VideoPlayback.Location = New Point(221, 103)
            pnl_VideoPlayback.Size = New Size(Width - tvSeries.Width - 40, Height - 170)
        End If

    End Sub

#End Region

#Region "Progressbar Volume"
    Private Sub MetroProgressbarVertical1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MetroProgressbarVertical1.MouseDown
        MetroProgressbarVertical1.Value = CInt(((MetroProgressbarVertical1.Height - e.Y) * MetroProgressbarVertical1.Maximum) / MetroProgressbarVertical1.Height)
        VLCPlayer.Volume = MetroProgressbarVertical1.Value
    End Sub

    Private Sub MetroProgressbarVertical1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MetroProgressbarVertical1.MouseUp
        MetroProgressbarVertical1.Value = CInt(((MetroProgressbarVertical1.Height - e.Y) * MetroProgressbarVertical1.Maximum) / MetroProgressbarVertical1.Height)
        VLCPlayer.Volume = MetroProgressbarVertical1.Value
    End Sub

    Private Sub MetroProgressbarVertical1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MetroProgressbarVertical1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            MetroProgressbarVertical1.Value = CInt(((MetroProgressbarVertical1.Height - e.Y) * MetroProgressbarVertical1.Maximum) / MetroProgressbarVertical1.Height)
            VLCPlayer.Volume = MetroProgressbarVertical1.Value
        End If
    End Sub

#End Region

#Region "Custom Control SeriesView"

    Private MoviesList As New List(Of SeriesView.Item)
    Private SeriesList As New List(Of SeriesView.Item)
    Private UpdateList As List(Of String())
    Private MoviesCompleted As Boolean = False
    Private SeriesCompleted As Boolean = False
    Private MoviesException As Boolean = False
    Private SeriesException As Boolean = False
    Private Sub Series_SearchTopAndNewSeriesCompleted(e As ShowsMoviesFetcher.SplashScreenDataArgs) Handles DataFetcher.SplashScreenShowsMoviesFetched
        If e.exception Is Nothing Then

            Select Case e.Categories
                Case SeriesClass.Categories.Movies
                    MoviesList = e.Result
                    MoviesCompleted = True

                Case SeriesClass.Categories.Series
                    SeriesList = e.Result
                    UpdateList = e.UpdatedList
                    SeriesCompleted = True
            End Select


            If SeriesView1.SelectedTab = SeriesView.Categories.Series AndAlso SeriesCompleted Then
                SeriesView1.State = SeriesView.States.Loaded
                SeriesView1.AddItems(SeriesList, UpdateList)
                SeriesCompleted = False

            ElseIf SeriesView1.SelectedTab = SeriesView.Categories.Movies AndAlso MoviesCompleted Then
                SeriesView1.State = SeriesView.States.Loaded
                SeriesView1.AddItems(MoviesList, UpdateList)
                MoviesCompleted = False
            End If


        Else
            If e.Categories = Categories.Movies Then MoviesException = True
            If e.Categories = Categories.Series Then SeriesException = True
            If SeriesView1.SelectedTab = e.Categories Then SeriesView1.State = SeriesView.States.GotException
        End If
    End Sub

#Region "Click Item Event"
    Dim selectedIndex As Integer
    Private Sub SeriesView1_ItemClicked(selectedItem As Integer, click As MouseButtons) Handles SeriesView1.ItemClicked
        Try
            If click = Windows.Forms.MouseButtons.Right Then
                selectedIndex = selectedItem
                cmsViewControl.Show(New Point(MousePosition.X, MousePosition.Y))
            ElseIf click = Windows.Forms.MouseButtons.Left Then
                pcLoading.Show()
                PlayControls.SendToBack()
                SeriesView1.ScrollingMaximum = 58

                If DataFetcher.StreamingServiceUsed = StreamingService.DotNetStreamerAPI Then
                    DataFetcher.Search(CType(SeriesView1.Items(selectedItem).Tag, String), False, SeriesView1.SelectedTab)
                Else
                    DataFetcher.Search(CType(SeriesView1.Items(selectedItem).Tag, String), False)
                End If


                Me.ActiveControl = tvSeries
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Select Tab (Top/New/Favorites)"
    Private Sub SeriesView1_LabelClicked(SelectedTab As SeriesView.Categories) Handles SeriesView1.LabelClicked
        Try
            Select Case SelectedTab
                Case SeriesView.Categories.Movies
                    If MoviesException Then
                        SeriesView1.State = SeriesView.States.GotException
                        SeriesView1.Items.Clear()
                        Exit Sub
                    End If
                    If MoviesList.Count = 0 Then
                        SeriesView1.State = SeriesView.States.Loading
                        SeriesView1.Items.Clear()
                        Exit Sub
                    Else
                        SeriesView1.State = SeriesView.States.Loaded
                    End If


                    SeriesView1.Items.Clear()
                    SeriesView1.ScrollingMaximum = 58
                    If MoviesList IsNot Nothing Then
                        SeriesView1.AddItems(MoviesList, UpdateList)
                    End If

                Case SeriesView.Categories.Series

                    If SeriesList.Count = 0 AndAlso Not SeriesCompleted AndAlso Not SeriesException Then
                        SeriesView1.State = SeriesView.States.Loading
                        SeriesView1.Items.Clear()
                        Exit Sub
                    ElseIf SeriesList.Count = 0 AndAlso SeriesException Then
                        SeriesView1.State = SeriesView.States.GotException
                        SeriesView1.Items.Clear()
                        Exit Sub
                    Else
                        SeriesView1.State = SeriesView.States.Loaded
                    End If

                    SeriesView1.Items.Clear()
                    SeriesView1.ScrollingMaximum = 58
                    If SeriesList IsNot Nothing Then
                        SeriesView1.AddItems(SeriesList, UpdateList)
                    End If
                Case SeriesView.Categories.Favorites
                    SeriesView1.Items.Clear()
                    ' Settings.DeserializeFromXML()
                    If Settings.Data.FavoriteList.Count = 0 Then
                        SeriesView1.NoFavorite = True
                        Exit Select
                    Else
                        SeriesView1.NoFavorite = False
                        SeriesView1.AddItems(Settings.Data.FavoriteList, UpdateList)
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "ContextMenuStrips Add/Delete from Favorite"

    'Show DeleteItem only on Favorite Tab
    Private Sub cmsViewControl_Opening(sender As Object, e As CancelEventArgs) Handles cmsViewControl.Opening
        If Not SeriesView1.SelectedTab = SeriesView.Categories.Favorites Then
            DeleteShowFromFavoriteToolStripMenuItem.Visible = False
        Else
            DeleteShowFromFavoriteToolStripMenuItem.Visible = True
        End If

    End Sub


    'Treeview one
    Private Sub AddTVShowToFavoriteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddTVShowToFavoriteToolStripMenuItem.Click
        Try
            If Not New Regex("E[0-9]+").IsMatch(tvSeries.SelectedNode.Text) Then
                Dim Duplicates As Boolean = False
                ' Settings.DeserializeFromXML()

                Dim EpisodeInfo As Query = DirectCast(tvSeries.SelectedNode.Tag, Query)

                If Settings.Data.FavoriteList.Count > 0 Then
                    For Each item As SeriesView.ItemFavorite In Settings.Data.FavoriteList
                        If GetTVShowNameOnly(tvSeries.SelectedNode.Text) = item.Title Then
                            Duplicates = True
                            Exit For
                        End If
                    Next
                    If Not Duplicates Then AddItemsToFavorite(GetTVShowNameOnly(EpisodeInfo.Name), EpisodeInfo.ImdbRate, EpisodeInfo.Length, EpisodeInfo.ThumbnailURL)
                Else
                    AddItemsToFavorite(GetTVShowNameOnly(EpisodeInfo.Name), EpisodeInfo.ImdbRate, EpisodeInfo.Length, EpisodeInfo.ThumbnailURL)
                End If

                If SeriesView1.SelectedTab = SeriesView.Categories.Favorites Then
                    SeriesView1.NoFavorite = False
                    SeriesView1.Items.Clear()
                    '  Settings.DeserializeFromXML()
                    SeriesView1.AddItems(Settings.Data.FavoriteList)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    'Custom control one
    Private Sub AddTVShowToFavoriteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddTVShowToFavoriteToolStripMenuItem1.Click
        Try
            Dim Duplicates As Boolean = False
            Dim EpisodeInformation As SeriesView.Item = SeriesView1.Items(selectedIndex)

            '  Settings.DeserializeFromXML()
            If Settings.Data.FavoriteList.Count > 0 Then
                For Each item As SeriesView.ItemFavorite In Settings.Data.FavoriteList
                    If item.Title.Contains(DirectCast(SeriesView1.Items(selectedIndex).Tag, String)) Then
                        Duplicates = True
                        Exit For
                    End If
                Next
                If Not Duplicates Then AddItemsToFavorite(EpisodeInformation.Title, EpisodeInformation.ImdbRating, EpisodeInformation.EpisodeLength, EpisodeInformation.ThumbnailURL)
            Else
                AddItemsToFavorite(EpisodeInformation.Title, EpisodeInformation.ImdbRating, EpisodeInformation.EpisodeLength, EpisodeInformation.ThumbnailURL)
            End If


            If SeriesView1.SelectedTab = SeriesView.Categories.Favorites Then
                SeriesView1.NoFavorite = False
                SeriesView1.Items.Clear()
                '   Settings.DeserializeFromXML()
                SeriesView1.AddItems(Settings.Data.FavoriteList)
            End If
        Catch ex As Exception
        End Try
    End Sub
    'Delete from favorite
    Private Sub DeleteShowFromFavoriteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteShowFromFavoriteToolStripMenuItem.Click

        ' Settings.DeserializeFromXML()

        If Settings.Data.FavoriteList.Count = 1 Then
            SeriesView1.Items.Clear()
            Settings.Data.FavoriteList.Clear()
            SeriesView1.NoFavorite = True
        Else
            Settings.Data.FavoriteList.RemoveAll(Function(item) item.Title = CStr(SeriesView1.Items(selectedIndex).Tag))
        End If

        'Settings.SerializeToXML()

        If SeriesView1.SelectedTab = SeriesView.Categories.Favorites Then
            SeriesView1.Items.Clear()
            SeriesView1.AddItems(Settings.Data.FavoriteList)
        End If

    End Sub


    Private Function GetTVShowNameOnly(Showname As String) As String
        If Showname.Contains("(") Then Return Split(Showname, " (")(0) Else Return String.Empty
    End Function

#End Region

#Region "Others functions"
    Public Shared Function ImageToBase64String(ByVal image As Image, ByVal imageFormat As ImageFormat) As String
        Using memStream As New MemoryStream
            image.Save(memStream, imageFormat)
            Dim result As String = Convert.ToBase64String(memStream.ToArray())
            memStream.Close()
            Return result
        End Using
    End Function

    'Private http As New Utility.Http
    Public Sub AddItemsToFavorite(ByVal ShowName As String, ByVal Imdb As String, ByVal Length As String, ByVal ThumbnailURL As String)
        Try

            Dim ImageStream As New System.IO.MemoryStream(New WebClient().DownloadData(ThumbnailURL))
            Dim bmp As New Bitmap(ImageStream)

            '  Settings.DeserializeFromXML()
            Settings.Data.FavoriteList.Add(New SeriesView.ItemFavorite(ShowName, Imdb, Length, bmp, ShowName))
            ' Settings.SerializeToXML()

        Catch ex As Exception
            MessageBox.Show("Unexpected Error. Can not add this TV Show to the favorites", ".NET Streamer")
        End Try
    End Sub
#End Region

#End Region

#Region "Back to Custom Control Button"
    Private Sub backBtn_Click(sender As Object, e As EventArgs) Handles backBtn.Click

        pcLoading.Hide()

        VLCPlayer.playlist.stop()
        VLCPlayer.playlist.items.clear()

        frmVLCDrawing.ResetDrawing()

        tmrSubtitlesAndSeekPosition.Stop()
        '  tmrUnconnected.Stop()
        PictureInPictureToolStripMenuItem.Enabled = False : FullscreenModeToolStripMenuItem.Enabled = False
        PlayControls.SendToBack()
        PlayMode = False
        frmHint.HideHint()
        backBtn.Visible = False
        SeriesView1.Height = 335 : pnl_VideoPlayback.Height = 335

        For i As Integer = 1 To 75 'resize treeview
            pnlSeries.Height -= 1
        Next

        SeriesView1.Show()
        SeriesView1.BringToFront()
        pnlShowInfo.Show()
        pnlShowInfo.BringToFront()
    End Sub
#End Region

#Region "Other"
    Public Sub HintButtonClicked()
        If showPreferences Then
            frmPreferences.Show()
            showPreferences = False
        Else
            frmSubtitlesDownloader.Show()
        End If
    End Sub

    Public Sub ShowHint(hintText As String, buttonText As String, timeOut As Integer)
        If frmHint.Visible = False Then
            frmHint.Show(Me)
            frmHint.Width = pnl_VideoPlayback.Width
            frmHint.Height = 33
            frmHint.Location = PointToScreen(pnl_VideoPlayback.Location)
            frmHint.ShowHint(hintText, buttonText, timeOut)
        End If
    End Sub

    'Private Sub tmrUnconnected_Tick(sender As Object, e As EventArgs) Handles tmrUnconnected.Tick
    '    If PlayControls.Text.Contains("Connecting") Or PlayControls.Text.Contains("Buffering") And frmHint.Visible = False Then
    '        showPreferences = True
    '        frmHint.Visible = False
    '        frmHint.Show(Me)
    '        frmHint.Height = 36
    '        frmHint.ShowHint("Trouble connecting? Try setting another streaming service manually.", "Preferences", 15)
    '        tmrUnconnected.Stop()
    '    End If
    'End Sub

    Private Sub lblPreferencesMenu_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblPreferencesMenu.Click
        frmPreferences.Show()
    End Sub

    Private Sub lblVideoMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblVideoMenu.Click
        cmsVideomode.Show(PointToScreen(New Point(lblVideoMenu.Location.X, lblVideoMenu.Location.Y + lblVideoMenu.Height)))
    End Sub

    Private Sub btnDownloadSubtitles_Click(ByVal sender As Object, ByVal e As EventArgs)
        frmSubtitlesDownloader.Show()
    End Sub

    Private Sub ReSyncSubtitlesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReSyncSubtitlesToolStripMenuItem1.Click
        frmReSync.Show()
    End Sub


    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        'frmSubtitles.Show(Me)
        'frmSubtitles.Width = pnl_VideoPlayback.Width
        'frmSubtitles.Height = pnl_VideoPlayback.Height
        'frmSubtitles.Location = PointToScreen(pnl_VideoPlayback.Location)

        AddHandler frmHint.HintButtonClicked, AddressOf HintButtonClicked
        AddHandler frmVLCDrawing.CrossClicked, AddressOf CrossClickedHandler

    End Sub
    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        pcLoading.Left = CInt((Width / 2) - (pcLoading.Width / 2))

        'If Not frmSubtitles Is Nothing Then
        '    frmSubtitles.Location = PointToScreen(pnl_VideoPlayback.Location)
        '    frmSubtitles.Width = pnl_VideoPlayback.Width
        '    frmSubtitles.Height = pnl_VideoPlayback.Height
        'End If

        If Not frmHint Is Nothing Then
            frmHint.Location = PointToScreen(pnl_VideoPlayback.Location)
            frmHint.Width = pnl_VideoPlayback.Width
        End If

        If PictureInPictureToolStripMenuItem.Checked Then
            pictureInPictureSize = Me.Size
            Invalidate()
        End If
    End Sub
    Private Sub frmMain_Move(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Move

        'If Not frmSubtitles Is Nothing Then
        '    '    frmSubtitles.Location = PointToScreen(pnl_VideoPlayback.Location)
        'End If
        If Not frmHint Is Nothing Then
            frmHint.Location = PointToScreen(pnl_VideoPlayback.Location)
        End If

        'If Not frmPlayControls Is Nothing Then
        'frmPlayControls.Location = PointToScreen(New Point(pnl_VideoPlayback.Location.X + 10, pnl_VideoPlayback.Bottom - frmPlayControls.Height - 10))
        ' End If
    End Sub
    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub MetroRadioButton1_CheckedChanged() Handles rbTvShows.CheckedChanged
        If rbMovies.Checked Then rbMovies.Checked = False
        rbTvShows.Checked = True

    End Sub
    Private Sub MetroRadioButton2_CheckedChanged() Handles rbMovies.CheckedChanged
        If rbTvShows.Checked Then rbTvShows.Checked = False
        rbMovies.Checked = True
    End Sub

    Private Sub CrossClickedHandler()
        MakeFullScreen()
    End Sub

    Private Sub pnl_VideoPlayback_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles pnl_VideoPlayback.Resize

        'If Not frmSubtitles Is Nothing Then
        '    frmSubtitles.Location = PointToScreen(pnl_VideoPlayback.Location)
        '    frmSubtitles.Width = pnl_VideoPlayback.Width
        '    frmSubtitles.Height = pnl_VideoPlayback.Height
        'End If

        If Not frmHint Is Nothing Then
            frmHint.Location = PointToScreen(pnl_VideoPlayback.Location)
            frmHint.Width = pnl_VideoPlayback.Width
            frmHint.Height = 33
        End If

    End Sub

#End Region

#Region "Mark/UnMark episode as Already Watch '(W)'"

    Private Sub MarkShowAsWatchedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MarkShowAsWatchedToolStripMenuItem.Click
        MarkEpisodeAsWatched(tvSeries.SelectedNode)
    End Sub
    Private Sub cmsTreeView_Opening(sender As Object, e As CancelEventArgs) Handles cmsTreeView.Opening
        If tvSeries.SelectedNode IsNot Nothing AndAlso tvSeries.SelectedNode.Text.Contains("(W)") Then
            MarkShowAsWatchedToolStripMenuItem.Visible = False
            UnMarkShowAsWatchedToolStripMenuItem.Visible = True
        Else
            MarkShowAsWatchedToolStripMenuItem.Visible = True
            UnMarkShowAsWatchedToolStripMenuItem.Visible = False
        End If

    End Sub
    Private Sub UnMarkShowAsWatchedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnMarkShowAsWatchedToolStripMenuItem.Click
        UnMarkEpisodeAsWatched(tvSeries.SelectedNode)
    End Sub

    Private Sub MarkEpisodeAsWatched(node As TreeNode)
        If node Is Nothing Then Exit Sub
        If Not node.Text.Contains("Episode") Then Exit Sub
        Dim WatchedEpisode As New SeriesClass.Episode(node.Text.Replace("(W) ", String.Empty), node.Parent.Text, node.Parent.Parent.Text, Nothing)

        For Each item As SeriesClass.Episode In Settings.Data.AlreadyWatchedEpisode
            If WatchedEpisode.Name = item.Name Then
                Exit Sub
            End If
        Next
        Settings.Data.AlreadyWatchedEpisode.Add(WatchedEpisode)
        '  Settings.SerializeToXML()
        node.Text = String.Format("(W) {0}", node.Text)
    End Sub
    Private Sub UnMarkEpisodeAsWatched(node As TreeNode)
        If node Is Nothing Then Exit Sub
        If Not node.Text.Contains("Episode") Then Exit Sub

        Dim WatchedEpisode As New SeriesClass.Episode(node.Text.Replace("(W) ", String.Empty), node.Parent.Text, node.Parent.Parent.Text, Nothing)

        Settings.Data.AlreadyWatchedEpisode.RemoveAll(Function(x) x.Name = WatchedEpisode.Name)
        ' Settings.SerializeToXML()
        node.Text = node.Text.Replace("(W) ", String.Empty)
    End Sub

#End Region


    'Public DM As New DownloadManager()
    'Private Sub DownloadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadToolStripMenuItem.Click
    '    DM.SetupDownload(tvSeries.SelectedNode)
    'End Sub

    'Private Sub MetroRadioButton2_CheckedChanged()
    '    If MetroRadioButton2.Checked AndAlso MetroRadioButton1.Checked Then
    '        MetroRadioButton2.Checked = True
    '        MetroRadioButton1.Checked = False
    '    End If
    'End Sub

    'Private Sub MetroRadioButton1_CheckedChanged()
    '    If MetroRadioButton1.Checked AndAlso MetroRadioButton2.Checked Then
    '        MetroRadioButton1.Checked = True
    '        MetroRadioButton2.Checked = False
    '    End If
    'End Sub

    Private Sub timerBufferSync_Tick(sender As Object, e As EventArgs) Handles timerBufferSync.Tick

        PositionSeeker.ResetAndStartAt(New TimeSpan(0, 0, 0, 0, VLCPlayer.input.Time).TotalMilliseconds) : PositionSeeker.Start()
        If SRTParsed.Count > 0 AndAlso VLCPlayer.input.Time > 0 Then SRTIndex = SubtitleParser.GetCurrentSubIndexFromTime()
        Debug.WriteLine("State: Buffering completed ; Event fired | PS:{0} - VLCTime: {1}", New TimeSpan(0, 0, 0, 0, PositionSeeker.MilliSecondsElapsed), New TimeSpan(0, 0, 0, 0, VLCPlayer.input.Time))

        timerBufferSync.Stop()

    End Sub

    Private Sub mDetector_MouseLeftButtonClick(sender As Object, e As MouseEventArgs) Handles mDetector.MouseLeftButtonClick

        Dim loc As Point = Me.PointToClient(e.Location)

        If Not txtSearch.Bounds.Contains(loc) Then
            If DropDownMenu1.Visible = True AndAlso Not DropDownMenu1.Bounds.Contains(loc) Then
                DropDownMenu1.Hide()
            End If
        ElseIf txtSearch.Bounds.Contains(loc) AndAlso DropDownMenu1.Visible = False Then
            DropDownMenu1.Show()
        End If

    End Sub



End Class


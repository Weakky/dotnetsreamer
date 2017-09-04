<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.lblMinimize = New System.Windows.Forms.Label()
        Me.lblClose = New System.Windows.Forms.Label()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblDotNet = New System.Windows.Forms.Label()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.lblPreferencesMenu = New System.Windows.Forms.Label()
        Me.lblSubtitlesMenu = New System.Windows.Forms.Label()
        Me.lblVideoMenu = New System.Windows.Forms.Label()
        Me.pnl_VideoPlayback = New System.Windows.Forms.Panel()
        Me.VLCPlayer = New AxAXVLC.AxVLCPlugin2()
        Me.pnlShowInfo = New System.Windows.Forms.Panel()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lblShowName = New System.Windows.Forms.Label()
        Me.picThumbnail = New System.Windows.Forms.PictureBox()
        Me.lblDevelopButton = New System.Windows.Forms.Label()
        Me.cmsSubtitles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LoadsrtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadSubtitlesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EnableSubtitlesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReSyncSubtitlesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrSubtitlesAndSeekPosition = New System.Windows.Forms.Timer(Me.components)
        Me.cmsVideomode = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FullscreenModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureInPictureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvSeries = New System.Windows.Forms.TreeView()
        Me.cmsTreeView = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddTVShowToFavoriteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MarkShowAsWatchedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnMarkShowAsWatchedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlSeries = New System.Windows.Forms.Panel()
        Me.ReSyncSubtitlesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsViewControl = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddTVShowToFavoriteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteShowFromFavoriteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pcLoading = New System.Windows.Forms.PictureBox()
        Me.backBtn = New System.Windows.Forms.PictureBox()
        Me.SeriesView1 = New DotNetStreamer.SeriesView()
        Me.rbMovies = New DotNetStreamer.MetroRadioButton()
        Me.rbTvShows = New DotNetStreamer.MetroRadioButton()
        Me.DropDownMenu1 = New DotNetStreamer.DropDownMenu()
        Me.PlayControls = New DotNetStreamer.SpectrumPanel()
        Me.MetroProgressbarVertical1 = New DotNetStreamer.MetroProgressbarVertical()
        Me.txtSearch = New DotNetStreamer.MetroTextBox()
        Me.pnlTitle.SuspendLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_VideoPlayback.SuspendLayout()
        CType(Me.VLCPlayer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlShowInfo.SuspendLayout()
        CType(Me.picThumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsSubtitles.SuspendLayout()
        Me.cmsVideomode.SuspendLayout()
        Me.cmsTreeView.SuspendLayout()
        Me.pnlSeries.SuspendLayout()
        Me.cmsViewControl.SuspendLayout()
        CType(Me.pcLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.backBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PlayControls.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMinimize
        '
        Me.lblMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMinimize.BackColor = System.Drawing.Color.Transparent
        Me.lblMinimize.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblMinimize.ForeColor = System.Drawing.Color.LightGray
        Me.lblMinimize.Location = New System.Drawing.Point(804, 9)
        Me.lblMinimize.Name = "lblMinimize"
        Me.lblMinimize.Size = New System.Drawing.Size(17, 16)
        Me.lblMinimize.TabIndex = 22
        Me.lblMinimize.Text = "0"
        Me.lblMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClose
        '
        Me.lblClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClose.BackColor = System.Drawing.Color.Transparent
        Me.lblClose.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblClose.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose.Location = New System.Drawing.Point(823, 9)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(16, 16)
        Me.lblClose.TabIndex = 21
        Me.lblClose.Text = "r"
        Me.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlBlock
        '
        Me.pnlBlock.BackColor = System.Drawing.Color.SeaGreen
        Me.pnlBlock.Location = New System.Drawing.Point(1, 16)
        Me.pnlBlock.Name = "pnlBlock"
        Me.pnlBlock.Size = New System.Drawing.Size(13, 55)
        Me.pnlBlock.TabIndex = 23
        Me.pnlBlock.Visible = False
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.Transparent
        Me.pnlTitle.Controls.Add(Me.lblDotNet)
        Me.pnlTitle.Controls.Add(Me.lblStreamer)
        Me.pnlTitle.Controls.Add(Me.pcIcon)
        Me.pnlTitle.Location = New System.Drawing.Point(23, 16)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(80, 53)
        Me.pnlTitle.TabIndex = 26
        '
        'lblDotNet
        '
        Me.lblDotNet.AutoSize = True
        Me.lblDotNet.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblDotNet.ForeColor = System.Drawing.Color.LightGray
        Me.lblDotNet.Location = New System.Drawing.Point(34, 15)
        Me.lblDotNet.Name = "lblDotNet"
        Me.lblDotNet.Size = New System.Drawing.Size(39, 20)
        Me.lblDotNet.TabIndex = 29
        Me.lblDotNet.Text = ".NET"
        '
        'lblStreamer
        '
        Me.lblStreamer.AutoSize = True
        Me.lblStreamer.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblStreamer.ForeColor = System.Drawing.Color.LightGray
        Me.lblStreamer.Location = New System.Drawing.Point(-4, 32)
        Me.lblStreamer.Name = "lblStreamer"
        Me.lblStreamer.Size = New System.Drawing.Size(77, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "STREAMER"
        '
        'pcIcon
        '
        Me.pcIcon.Image = Global.DotNetStreamer.My.Resources.Resources.NS
        Me.pcIcon.Location = New System.Drawing.Point(-8, -5)
        Me.pcIcon.Name = "pcIcon"
        Me.pcIcon.Size = New System.Drawing.Size(48, 48)
        Me.pcIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pcIcon.TabIndex = 28
        Me.pcIcon.TabStop = False
        '
        'lblPreferencesMenu
        '
        Me.lblPreferencesMenu.AutoSize = True
        Me.lblPreferencesMenu.BackColor = System.Drawing.Color.Transparent
        Me.lblPreferencesMenu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblPreferencesMenu.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.lblPreferencesMenu.ForeColor = System.Drawing.Color.LightGray
        Me.lblPreferencesMenu.Location = New System.Drawing.Point(112, 15)
        Me.lblPreferencesMenu.Name = "lblPreferencesMenu"
        Me.lblPreferencesMenu.Size = New System.Drawing.Size(105, 25)
        Me.lblPreferencesMenu.TabIndex = 29
        Me.lblPreferencesMenu.Text = "preferences"
        '
        'lblSubtitlesMenu
        '
        Me.lblSubtitlesMenu.AutoSize = True
        Me.lblSubtitlesMenu.BackColor = System.Drawing.Color.Transparent
        Me.lblSubtitlesMenu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSubtitlesMenu.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.lblSubtitlesMenu.ForeColor = System.Drawing.Color.LightGray
        Me.lblSubtitlesMenu.Location = New System.Drawing.Point(245, 15)
        Me.lblSubtitlesMenu.Name = "lblSubtitlesMenu"
        Me.lblSubtitlesMenu.Size = New System.Drawing.Size(77, 25)
        Me.lblSubtitlesMenu.TabIndex = 30
        Me.lblSubtitlesMenu.Text = "subtitles"
        '
        'lblVideoMenu
        '
        Me.lblVideoMenu.AutoSize = True
        Me.lblVideoMenu.BackColor = System.Drawing.Color.Transparent
        Me.lblVideoMenu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblVideoMenu.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.lblVideoMenu.ForeColor = System.Drawing.Color.LightGray
        Me.lblVideoMenu.Location = New System.Drawing.Point(354, 15)
        Me.lblVideoMenu.Name = "lblVideoMenu"
        Me.lblVideoMenu.Size = New System.Drawing.Size(57, 25)
        Me.lblVideoMenu.TabIndex = 31
        Me.lblVideoMenu.Text = "video"
        '
        'pnl_VideoPlayback
        '
        Me.pnl_VideoPlayback.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl_VideoPlayback.BackColor = System.Drawing.Color.Black
        Me.pnl_VideoPlayback.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_VideoPlayback.Controls.Add(Me.VLCPlayer)
        Me.pnl_VideoPlayback.Location = New System.Drawing.Point(221, 109)
        Me.pnl_VideoPlayback.Name = "pnl_VideoPlayback"
        Me.pnl_VideoPlayback.Size = New System.Drawing.Size(612, 406)
        Me.pnl_VideoPlayback.TabIndex = 36
        '
        'VLCPlayer
        '
        Me.VLCPlayer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLCPlayer.Enabled = True
        Me.VLCPlayer.Location = New System.Drawing.Point(0, 0)
        Me.VLCPlayer.Name = "VLCPlayer"
        Me.VLCPlayer.OcxState = CType(resources.GetObject("VLCPlayer.OcxState"), System.Windows.Forms.AxHost.State)
        Me.VLCPlayer.Size = New System.Drawing.Size(610, 404)
        Me.VLCPlayer.TabIndex = 0
        '
        'pnlShowInfo
        '
        Me.pnlShowInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlShowInfo.BackColor = System.Drawing.Color.Transparent
        Me.pnlShowInfo.Controls.Add(Me.lblDescription)
        Me.pnlShowInfo.Controls.Add(Me.lblShowName)
        Me.pnlShowInfo.Controls.Add(Me.picThumbnail)
        Me.pnlShowInfo.Location = New System.Drawing.Point(16, 440)
        Me.pnlShowInfo.Name = "pnlShowInfo"
        Me.pnlShowInfo.Size = New System.Drawing.Size(817, 76)
        Me.pnlShowInfo.TabIndex = 37
        '
        'lblDescription
        '
        Me.lblDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDescription.ForeColor = System.Drawing.Color.LightGray
        Me.lblDescription.Location = New System.Drawing.Point(52, 29)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(772, 44)
        Me.lblDescription.TabIndex = 38
        '
        'lblShowName
        '
        Me.lblShowName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblShowName.BackColor = System.Drawing.Color.Transparent
        Me.lblShowName.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.lblShowName.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblShowName.Location = New System.Drawing.Point(51, 8)
        Me.lblShowName.Name = "lblShowName"
        Me.lblShowName.Size = New System.Drawing.Size(297, 21)
        Me.lblShowName.TabIndex = 37
        '
        'picThumbnail
        '
        Me.picThumbnail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picThumbnail.BackColor = System.Drawing.Color.Transparent
        Me.picThumbnail.Location = New System.Drawing.Point(0, 9)
        Me.picThumbnail.Name = "picThumbnail"
        Me.picThumbnail.Size = New System.Drawing.Size(46, 65)
        Me.picThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picThumbnail.TabIndex = 36
        Me.picThumbnail.TabStop = False
        '
        'lblDevelopButton
        '
        Me.lblDevelopButton.AutoSize = True
        Me.lblDevelopButton.BackColor = System.Drawing.Color.Transparent
        Me.lblDevelopButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblDevelopButton.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.lblDevelopButton.ForeColor = System.Drawing.Color.LightGray
        Me.lblDevelopButton.Location = New System.Drawing.Point(443, 15)
        Me.lblDevelopButton.Name = "lblDevelopButton"
        Me.lblDevelopButton.Size = New System.Drawing.Size(132, 25)
        Me.lblDevelopButton.TabIndex = 39
        Me.lblDevelopButton.Text = "developbutton"
        Me.lblDevelopButton.Visible = False
        '
        'cmsSubtitles
        '
        Me.cmsSubtitles.BackColor = System.Drawing.SystemColors.Control
        Me.cmsSubtitles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadsrtToolStripMenuItem, Me.DownloadSubtitlesToolStripMenuItem, Me.EnableSubtitlesToolStripMenuItem, Me.ToolStripSeparator1, Me.ReSyncSubtitlesToolStripMenuItem1})
        Me.cmsSubtitles.Name = "cmsSubtitles"
        Me.cmsSubtitles.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.cmsSubtitles.Size = New System.Drawing.Size(177, 98)
        '
        'LoadsrtToolStripMenuItem
        '
        Me.LoadsrtToolStripMenuItem.Image = CType(resources.GetObject("LoadsrtToolStripMenuItem.Image"), System.Drawing.Image)
        Me.LoadsrtToolStripMenuItem.Name = "LoadsrtToolStripMenuItem"
        Me.LoadsrtToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.LoadsrtToolStripMenuItem.Text = "Load .srt"
        '
        'DownloadSubtitlesToolStripMenuItem
        '
        Me.DownloadSubtitlesToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DownloadSubtitlesToolStripMenuItem.Image = Global.DotNetStreamer.My.Resources.Resources.download
        Me.DownloadSubtitlesToolStripMenuItem.Name = "DownloadSubtitlesToolStripMenuItem"
        Me.DownloadSubtitlesToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.DownloadSubtitlesToolStripMenuItem.Text = "Download Subtitles"
        '
        'EnableSubtitlesToolStripMenuItem
        '
        Me.EnableSubtitlesToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.EnableSubtitlesToolStripMenuItem.Enabled = False
        Me.EnableSubtitlesToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.EnableSubtitlesToolStripMenuItem.Image = Global.DotNetStreamer.My.Resources.Resources.enable_sub
        Me.EnableSubtitlesToolStripMenuItem.Name = "EnableSubtitlesToolStripMenuItem"
        Me.EnableSubtitlesToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.EnableSubtitlesToolStripMenuItem.Text = "Enable Subtitles"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(173, 6)
        '
        'ReSyncSubtitlesToolStripMenuItem1
        '
        Me.ReSyncSubtitlesToolStripMenuItem1.BackColor = System.Drawing.SystemColors.Control
        Me.ReSyncSubtitlesToolStripMenuItem1.Enabled = False
        Me.ReSyncSubtitlesToolStripMenuItem1.Image = Global.DotNetStreamer.My.Resources.Resources.synchonize
        Me.ReSyncSubtitlesToolStripMenuItem1.Name = "ReSyncSubtitlesToolStripMenuItem1"
        Me.ReSyncSubtitlesToolStripMenuItem1.Size = New System.Drawing.Size(176, 22)
        Me.ReSyncSubtitlesToolStripMenuItem1.Text = "ReSync Subtitles"
        '
        'tmrSubtitlesAndSeekPosition
        '
        Me.tmrSubtitlesAndSeekPosition.Interval = 250
        '
        'cmsVideomode
        '
        Me.cmsVideomode.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FullscreenModeToolStripMenuItem, Me.PictureInPictureToolStripMenuItem})
        Me.cmsVideomode.Name = "cmsVideomode"
        Me.cmsVideomode.Size = New System.Drawing.Size(165, 48)
        '
        'FullscreenModeToolStripMenuItem
        '
        Me.FullscreenModeToolStripMenuItem.Enabled = False
        Me.FullscreenModeToolStripMenuItem.Image = Global.DotNetStreamer.My.Resources.Resources.fullscreeniconcms
        Me.FullscreenModeToolStripMenuItem.Name = "FullscreenModeToolStripMenuItem"
        Me.FullscreenModeToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.FullscreenModeToolStripMenuItem.Text = "Fullscreen Mode"
        '
        'PictureInPictureToolStripMenuItem
        '
        Me.PictureInPictureToolStripMenuItem.Enabled = False
        Me.PictureInPictureToolStripMenuItem.Image = Global.DotNetStreamer.My.Resources.Resources._1397337532_Streamline_46
        Me.PictureInPictureToolStripMenuItem.Name = "PictureInPictureToolStripMenuItem"
        Me.PictureInPictureToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.PictureInPictureToolStripMenuItem.Text = "Picture In Picture"
        '
        'tvSeries
        '
        Me.tvSeries.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tvSeries.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.tvSeries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvSeries.ContextMenuStrip = Me.cmsTreeView
        Me.tvSeries.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.tvSeries.ForeColor = System.Drawing.Color.LightGray
        Me.tvSeries.ItemHeight = 19
        Me.tvSeries.Location = New System.Drawing.Point(-1, -1)
        Me.tvSeries.Name = "tvSeries"
        Me.tvSeries.Size = New System.Drawing.Size(198, 407)
        Me.tvSeries.TabIndex = 32
        '
        'cmsTreeView
        '
        Me.cmsTreeView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddTVShowToFavoriteToolStripMenuItem, Me.MarkShowAsWatchedToolStripMenuItem, Me.UnMarkShowAsWatchedToolStripMenuItem, Me.DownloadToolStripMenuItem})
        Me.cmsTreeView.Name = "cmsTreeView"
        Me.cmsTreeView.Size = New System.Drawing.Size(234, 92)
        '
        'AddTVShowToFavoriteToolStripMenuItem
        '
        Me.AddTVShowToFavoriteToolStripMenuItem.Image = CType(resources.GetObject("AddTVShowToFavoriteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AddTVShowToFavoriteToolStripMenuItem.Name = "AddTVShowToFavoriteToolStripMenuItem"
        Me.AddTVShowToFavoriteToolStripMenuItem.Size = New System.Drawing.Size(233, 22)
        Me.AddTVShowToFavoriteToolStripMenuItem.Text = "Add TV Show To Favorite"
        '
        'MarkShowAsWatchedToolStripMenuItem
        '
        Me.MarkShowAsWatchedToolStripMenuItem.Image = CType(resources.GetObject("MarkShowAsWatchedToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MarkShowAsWatchedToolStripMenuItem.Name = "MarkShowAsWatchedToolStripMenuItem"
        Me.MarkShowAsWatchedToolStripMenuItem.Size = New System.Drawing.Size(233, 22)
        Me.MarkShowAsWatchedToolStripMenuItem.Text = "Mark Episode As Watched (W)"
        '
        'UnMarkShowAsWatchedToolStripMenuItem
        '
        Me.UnMarkShowAsWatchedToolStripMenuItem.Name = "UnMarkShowAsWatchedToolStripMenuItem"
        Me.UnMarkShowAsWatchedToolStripMenuItem.Size = New System.Drawing.Size(233, 22)
        Me.UnMarkShowAsWatchedToolStripMenuItem.Text = "UnMark Episode As Watched"
        '
        'DownloadToolStripMenuItem
        '
        Me.DownloadToolStripMenuItem.Enabled = False
        Me.DownloadToolStripMenuItem.Image = Global.DotNetStreamer.My.Resources.Resources.download
        Me.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem"
        Me.DownloadToolStripMenuItem.Size = New System.Drawing.Size(233, 22)
        Me.DownloadToolStripMenuItem.Text = "Download (Coming Soon)"
        '
        'pnlSeries
        '
        Me.pnlSeries.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlSeries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSeries.Controls.Add(Me.tvSeries)
        Me.pnlSeries.Location = New System.Drawing.Point(17, 109)
        Me.pnlSeries.Name = "pnlSeries"
        Me.pnlSeries.Size = New System.Drawing.Size(198, 407)
        Me.pnlSeries.TabIndex = 40
        '
        'ReSyncSubtitlesToolStripMenuItem
        '
        Me.ReSyncSubtitlesToolStripMenuItem.Enabled = False
        Me.ReSyncSubtitlesToolStripMenuItem.Name = "ReSyncSubtitlesToolStripMenuItem"
        Me.ReSyncSubtitlesToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ReSyncSubtitlesToolStripMenuItem.Text = "ReSync Subtitles"
        '
        'cmsViewControl
        '
        Me.cmsViewControl.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddTVShowToFavoriteToolStripMenuItem1, Me.DeleteShowFromFavoriteToolStripMenuItem})
        Me.cmsViewControl.Name = "cmsViewControl"
        Me.cmsViewControl.Size = New System.Drawing.Size(219, 48)
        '
        'AddTVShowToFavoriteToolStripMenuItem1
        '
        Me.AddTVShowToFavoriteToolStripMenuItem1.Image = CType(resources.GetObject("AddTVShowToFavoriteToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.AddTVShowToFavoriteToolStripMenuItem1.Name = "AddTVShowToFavoriteToolStripMenuItem1"
        Me.AddTVShowToFavoriteToolStripMenuItem1.Size = New System.Drawing.Size(218, 22)
        Me.AddTVShowToFavoriteToolStripMenuItem1.Text = "Add Show to Favorites"
        '
        'DeleteShowFromFavoriteToolStripMenuItem
        '
        Me.DeleteShowFromFavoriteToolStripMenuItem.Image = Global.DotNetStreamer.My.Resources.Resources._1378240352_Error
        Me.DeleteShowFromFavoriteToolStripMenuItem.Name = "DeleteShowFromFavoriteToolStripMenuItem"
        Me.DeleteShowFromFavoriteToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.DeleteShowFromFavoriteToolStripMenuItem.Text = "Delete Show from Favorites"
        '
        'pcLoading
        '
        Me.pcLoading.BackColor = System.Drawing.Color.Transparent
        Me.pcLoading.Image = Global.DotNetStreamer.My.Resources.Resources.animated_loader
        Me.pcLoading.Location = New System.Drawing.Point(269, 1)
        Me.pcLoading.Name = "pcLoading"
        Me.pcLoading.Size = New System.Drawing.Size(288, 4)
        Me.pcLoading.TabIndex = 2
        Me.pcLoading.TabStop = False
        Me.pcLoading.Visible = False
        '
        'backBtn
        '
        Me.backBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.backBtn.BackColor = System.Drawing.Color.Transparent
        Me.backBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.backBtn.Image = Global.DotNetStreamer.My.Resources.Resources.TgDwcOS
        Me.backBtn.Location = New System.Drawing.Point(802, 52)
        Me.backBtn.Name = "backBtn"
        Me.backBtn.Size = New System.Drawing.Size(30, 30)
        Me.backBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.backBtn.TabIndex = 44
        Me.backBtn.TabStop = False
        Me.backBtn.Visible = False
        '
        'SeriesView1
        '
        Me.SeriesView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SeriesView1.BackColor = System.Drawing.Color.Transparent
        Me.SeriesView1.Location = New System.Drawing.Point(219, 103)
        Me.SeriesView1.Name = "SeriesView1"
        Me.SeriesView1.NoFavorite = False
        Me.SeriesView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SeriesView1.ScrollingMaximum = 58
        Me.SeriesView1.ScrollValue = 0
        Me.SeriesView1.SelectedTab = DotNetStreamer.SeriesView.Categories.Series
        Me.SeriesView1.Size = New System.Drawing.Size(614, 413)
        Me.SeriesView1.State = DotNetStreamer.SeriesView.States.Loading
        Me.SeriesView1.TabIndex = 43
        Me.SeriesView1.Text = "SeriesView2"
        '
        'rbMovies
        '
        Me.rbMovies.BackColor = System.Drawing.Color.Transparent
        Me.rbMovies.BorderColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.rbMovies.CheckColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.rbMovies.Checked = False
        Me.rbMovies.FillColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.rbMovies.Location = New System.Drawing.Point(208, 80)
        Me.rbMovies.Name = "rbMovies"
        Me.rbMovies.Size = New System.Drawing.Size(98, 23)
        Me.rbMovies.TabIndex = 47
        Me.rbMovies.Text = "Movies"
        '
        'rbTvShows
        '
        Me.rbTvShows.BackColor = System.Drawing.Color.Transparent
        Me.rbTvShows.BorderColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.rbTvShows.CheckColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.rbTvShows.Checked = True
        Me.rbTvShows.FillColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.rbTvShows.Location = New System.Drawing.Point(117, 80)
        Me.rbTvShows.Name = "rbTvShows"
        Me.rbTvShows.Size = New System.Drawing.Size(98, 23)
        Me.rbTvShows.TabIndex = 46
        Me.rbTvShows.Text = "TV Shows"
        '
        'DropDownMenu1
        '
        Me.DropDownMenu1.Location = New System.Drawing.Point(117, 78)
        Me.DropDownMenu1.Name = "DropDownMenu1"
        Me.DropDownMenu1.NoQuery = False
        Me.DropDownMenu1.SelectedItem = -1
        Me.DropDownMenu1.Size = New System.Drawing.Size(372, 0)
        Me.DropDownMenu1.TabIndex = 45
        Me.DropDownMenu1.Text = "DropDownMenu1"
        '
        'PlayControls
        '
        Me.PlayControls.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PlayControls.BackColor = System.Drawing.Color.Transparent
        Me.PlayControls.Controls.Add(Me.MetroProgressbarVertical1)
        Me.PlayControls.Location = New System.Drawing.Point(220, 468)
        Me.PlayControls.Name = "PlayControls"
        Me.PlayControls.PlayPauseButton = DotNetStreamer.SpectrumPanel.PlayPauseButtonStyles.Play
        Me.PlayControls.ProgressMaximum = 100.0R
        Me.PlayControls.ProgressValue = 0.0R
        Me.PlayControls.Seconds = "00:00"
        Me.PlayControls.SecondsLeft = "-00:00:00"
        Me.PlayControls.ShowName = ""
        Me.PlayControls.Size = New System.Drawing.Size(613, 43)
        Me.PlayControls.TabIndex = 38
        Me.PlayControls.Text = "Now playing:"
        '
        'MetroProgressbarVertical1
        '
        Me.MetroProgressbarVertical1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroProgressbarVertical1.Location = New System.Drawing.Point(144, 1)
        Me.MetroProgressbarVertical1.Maximum = 100
        Me.MetroProgressbarVertical1.Name = "MetroProgressbarVertical1"
        Me.MetroProgressbarVertical1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroProgressbarVertical1.Size = New System.Drawing.Size(8, 38)
        Me.MetroProgressbarVertical1.TabIndex = 0
        Me.MetroProgressbarVertical1.Text = "MetroProgressbarVertical1"
        Me.MetroProgressbarVertical1.Value = 80
        '
        'txtSearch
        '
        Me.txtSearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtSearch.Location = New System.Drawing.Point(117, 46)
        Me.txtSearch.MaxLength = 32767
        Me.txtSearch.Multiline = False
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.NumericOnly = False
        Me.txtSearch.ReadOnly = False
        Me.txtSearch.Size = New System.Drawing.Size(372, 27)
        Me.txtSearch.TabIndex = 27
        Me.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSearch.UseSystemPasswordChar = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(849, 527)
        Me.ControlBox = False
        Me.Controls.Add(Me.SeriesView1)
        Me.Controls.Add(Me.rbMovies)
        Me.Controls.Add(Me.rbTvShows)
        Me.Controls.Add(Me.pnlSeries)
        Me.Controls.Add(Me.DropDownMenu1)
        Me.Controls.Add(Me.backBtn)
        Me.Controls.Add(Me.PlayControls)
        Me.Controls.Add(Me.pnlShowInfo)
        Me.Controls.Add(Me.pnl_VideoPlayback)
        Me.Controls.Add(Me.lblDevelopButton)
        Me.Controls.Add(Me.pcLoading)
        Me.Controls.Add(Me.lblVideoMenu)
        Me.Controls.Add(Me.lblSubtitlesMenu)
        Me.Controls.Add(Me.lblPreferencesMenu)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlBlock)
        Me.Controls.Add(Me.lblMinimize)
        Me.Controls.Add(Me.lblClose)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(849, 527)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DotNet Streamer"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_VideoPlayback.ResumeLayout(False)
        CType(Me.VLCPlayer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlShowInfo.ResumeLayout(False)
        CType(Me.picThumbnail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsSubtitles.ResumeLayout(False)
        Me.cmsVideomode.ResumeLayout(False)
        Me.cmsTreeView.ResumeLayout(False)
        Me.pnlSeries.ResumeLayout(False)
        Me.cmsViewControl.ResumeLayout(False)
        CType(Me.pcLoading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.backBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PlayControls.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMinimize As System.Windows.Forms.Label
    Friend WithEvents lblClose As System.Windows.Forms.Label
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents txtSearch As MetroTextBox
    Friend WithEvents lblPreferencesMenu As System.Windows.Forms.Label
    Friend WithEvents lblSubtitlesMenu As System.Windows.Forms.Label
    Friend WithEvents lblVideoMenu As System.Windows.Forms.Label
    Friend WithEvents pnl_VideoPlayback As System.Windows.Forms.Panel
    Friend WithEvents pcLoading As System.Windows.Forms.PictureBox
    Friend WithEvents pnlShowInfo As System.Windows.Forms.Panel
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblShowName As System.Windows.Forms.Label
    Friend WithEvents picThumbnail As System.Windows.Forms.PictureBox
    Friend WithEvents PlayControls As SpectrumPanel
    Friend WithEvents lblDevelopButton As System.Windows.Forms.Label
    Friend WithEvents cmsSubtitles As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DownloadSubtitlesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnableSubtitlesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrSubtitlesAndSeekPosition As System.Windows.Forms.Timer
    Friend WithEvents cmsVideomode As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents FullscreenModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents lblDotNet As System.Windows.Forms.Label
    Friend WithEvents tvSeries As System.Windows.Forms.TreeView
    Friend WithEvents pnlSeries As System.Windows.Forms.Panel
    Friend WithEvents LoadsrtToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PictureInPictureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MetroProgressbarVertical1 As MetroProgressbarVertical
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ReSyncSubtitlesToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReSyncSubtitlesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SeriesView1 As DotNetStreamer.SeriesView
    Friend WithEvents cmsTreeView As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddTVShowToFavoriteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsViewControl As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddTVShowToFavoriteToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents backBtn As System.Windows.Forms.PictureBox
    Friend WithEvents DeleteShowFromFavoriteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropDownMenu1 As DotNetStreamer.DropDownMenu
    Friend WithEvents VLCPlayer As AxAXVLC.AxVLCPlugin2
    Friend WithEvents MarkShowAsWatchedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnMarkShowAsWatchedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rbTvShows As DotNetStreamer.MetroRadioButton
    Friend WithEvents rbMovies As DotNetStreamer.MetroRadioButton

End Class
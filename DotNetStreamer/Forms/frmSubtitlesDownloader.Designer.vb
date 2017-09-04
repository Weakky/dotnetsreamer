<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSubtitlesDownloader
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSubtitlesDownloader))
        Me.lblMinimize = New System.Windows.Forms.Label()
        Me.lblClose = New System.Windows.Forms.Label()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.lblShowname = New System.Windows.Forms.Label()
        Me.lblSeason = New System.Windows.Forms.Label()
        Me.lblEpisode = New System.Windows.Forms.Label()
        Me.lblShowNameHeader = New System.Windows.Forms.Label()
        Me.lblSeasonHeader = New System.Windows.Forms.Label()
        Me.lblEpisodeHeader = New System.Windows.Forms.Label()
        Me.lblLanguageHeader = New System.Windows.Forms.Label()
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.pcLoading = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lvSubtitles = New DotNetStreamer.MetroListview()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtEpisode = New DotNetStreamer.MetroTextBox()
        Me.txtSeason = New DotNetStreamer.MetroTextBox()
        Me.btnSearch = New DotNetStreamer.MetroButton()
        Me.txtShowName = New DotNetStreamer.MetroTextBox()
        Me.pnlTitle.SuspendLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMinimize
        '
        Me.lblMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMinimize.BackColor = System.Drawing.Color.Transparent
        Me.lblMinimize.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblMinimize.ForeColor = System.Drawing.Color.LightGray
        Me.lblMinimize.Location = New System.Drawing.Point(546, 9)
        Me.lblMinimize.Name = "lblMinimize"
        Me.lblMinimize.Size = New System.Drawing.Size(17, 16)
        Me.lblMinimize.TabIndex = 24
        Me.lblMinimize.Text = "0"
        Me.lblMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClose
        '
        Me.lblClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClose.BackColor = System.Drawing.Color.Transparent
        Me.lblClose.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblClose.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose.Location = New System.Drawing.Point(565, 9)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(16, 16)
        Me.lblClose.TabIndex = 23
        Me.lblClose.Text = "r"
        Me.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlBlock
        '
        Me.pnlBlock.BackColor = System.Drawing.Color.SeaGreen
        Me.pnlBlock.Location = New System.Drawing.Point(1, 16)
        Me.pnlBlock.Name = "pnlBlock"
        Me.pnlBlock.Size = New System.Drawing.Size(13, 55)
        Me.pnlBlock.TabIndex = 30
        Me.pnlBlock.Visible = False
        '
        'lblShowname
        '
        Me.lblShowname.AutoSize = True
        Me.lblShowname.BackColor = System.Drawing.Color.Transparent
        Me.lblShowname.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblShowname.ForeColor = System.Drawing.Color.White
        Me.lblShowname.Location = New System.Drawing.Point(104, 25)
        Me.lblShowname.Name = "lblShowname"
        Me.lblShowname.Size = New System.Drawing.Size(69, 15)
        Me.lblShowname.TabIndex = 34
        Me.lblShowname.Text = "Show name"
        '
        'lblSeason
        '
        Me.lblSeason.AutoSize = True
        Me.lblSeason.BackColor = System.Drawing.Color.Transparent
        Me.lblSeason.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSeason.ForeColor = System.Drawing.Color.White
        Me.lblSeason.Location = New System.Drawing.Point(432, 26)
        Me.lblSeason.Name = "lblSeason"
        Me.lblSeason.Size = New System.Drawing.Size(13, 15)
        Me.lblSeason.TabIndex = 37
        Me.lblSeason.Text = "S"
        '
        'lblEpisode
        '
        Me.lblEpisode.AutoSize = True
        Me.lblEpisode.BackColor = System.Drawing.Color.Transparent
        Me.lblEpisode.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblEpisode.ForeColor = System.Drawing.Color.White
        Me.lblEpisode.Location = New System.Drawing.Point(466, 26)
        Me.lblEpisode.Name = "lblEpisode"
        Me.lblEpisode.Size = New System.Drawing.Size(13, 15)
        Me.lblEpisode.TabIndex = 38
        Me.lblEpisode.Text = "E"
        '
        'lblShowNameHeader
        '
        Me.lblShowNameHeader.AutoSize = True
        Me.lblShowNameHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblShowNameHeader.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.lblShowNameHeader.ForeColor = System.Drawing.Color.White
        Me.lblShowNameHeader.Location = New System.Drawing.Point(10, 84)
        Me.lblShowNameHeader.Name = "lblShowNameHeader"
        Me.lblShowNameHeader.Size = New System.Drawing.Size(89, 21)
        Me.lblShowNameHeader.TabIndex = 40
        Me.lblShowNameHeader.Text = "Show name"
        '
        'lblSeasonHeader
        '
        Me.lblSeasonHeader.AutoSize = True
        Me.lblSeasonHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblSeasonHeader.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.lblSeasonHeader.ForeColor = System.Drawing.Color.White
        Me.lblSeasonHeader.Location = New System.Drawing.Point(278, 84)
        Me.lblSeasonHeader.Name = "lblSeasonHeader"
        Me.lblSeasonHeader.Size = New System.Drawing.Size(58, 21)
        Me.lblSeasonHeader.TabIndex = 41
        Me.lblSeasonHeader.Text = "Season"
        '
        'lblEpisodeHeader
        '
        Me.lblEpisodeHeader.AutoSize = True
        Me.lblEpisodeHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblEpisodeHeader.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.lblEpisodeHeader.ForeColor = System.Drawing.Color.White
        Me.lblEpisodeHeader.Location = New System.Drawing.Point(349, 84)
        Me.lblEpisodeHeader.Name = "lblEpisodeHeader"
        Me.lblEpisodeHeader.Size = New System.Drawing.Size(62, 21)
        Me.lblEpisodeHeader.TabIndex = 42
        Me.lblEpisodeHeader.Text = "Episode"
        '
        'lblLanguageHeader
        '
        Me.lblLanguageHeader.AutoSize = True
        Me.lblLanguageHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblLanguageHeader.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.lblLanguageHeader.ForeColor = System.Drawing.Color.White
        Me.lblLanguageHeader.Location = New System.Drawing.Point(419, 84)
        Me.lblLanguageHeader.Name = "lblLanguageHeader"
        Me.lblLanguageHeader.Size = New System.Drawing.Size(77, 21)
        Me.lblLanguageHeader.TabIndex = 43
        Me.lblLanguageHeader.Text = "Language"
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.Transparent
        Me.pnlTitle.Controls.Add(Me.lblStreamer)
        Me.pnlTitle.Controls.Add(Me.pcIcon)
        Me.pnlTitle.Location = New System.Drawing.Point(23, 16)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(76, 53)
        Me.pnlTitle.TabIndex = 47
        '
        'lblStreamer
        '
        Me.lblStreamer.AutoSize = True
        Me.lblStreamer.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblStreamer.ForeColor = System.Drawing.Color.LightGray
        Me.lblStreamer.Location = New System.Drawing.Point(-4, 32)
        Me.lblStreamer.Name = "lblStreamer"
        Me.lblStreamer.Size = New System.Drawing.Size(75, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "SUBTITLES"
        '
        'pcIcon
        '
        Me.pcIcon.Image = Global.DotNetStreamer.My.Resources.Resources.NS
        Me.pcIcon.Location = New System.Drawing.Point(-3, -5)
        Me.pcIcon.Name = "pcIcon"
        Me.pcIcon.Size = New System.Drawing.Size(48, 48)
        Me.pcIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pcIcon.TabIndex = 28
        Me.pcIcon.TabStop = False
        '
        'pcLoading
        '
        Me.pcLoading.BackColor = System.Drawing.Color.Transparent
        Me.pcLoading.Image = Global.DotNetStreamer.My.Resources.Resources.animated_loader
        Me.pcLoading.Location = New System.Drawing.Point(151, 1)
        Me.pcLoading.Name = "pcLoading"
        Me.pcLoading.Size = New System.Drawing.Size(288, 4)
        Me.pcLoading.TabIndex = 45
        Me.pcLoading.TabStop = False
        Me.pcLoading.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(10, 379)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(572, 21)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "If there are no subtitles available, try changing your language under preferences" & _
    " tab."
        '
        'lvSubtitles
        '
        Me.lvSubtitles.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.lvSubtitles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvSubtitles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lvSubtitles.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lvSubtitles.ForeColor = System.Drawing.Color.LightGray
        Me.lvSubtitles.FullRowSelect = True
        Me.lvSubtitles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvSubtitles.Location = New System.Drawing.Point(12, 110)
        Me.lvSubtitles.Name = "lvSubtitles"
        Me.lvSubtitles.Size = New System.Drawing.Size(566, 263)
        Me.lvSubtitles.TabIndex = 39
        Me.lvSubtitles.UseCompatibleStateImageBehavior = False
        Me.lvSubtitles.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Show"
        Me.ColumnHeader1.Width = 258
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Season"
        Me.ColumnHeader2.Width = 70
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Episode"
        Me.ColumnHeader3.Width = 85
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Language"
        Me.ColumnHeader4.Width = 150
        '
        'txtEpisode
        '
        Me.txtEpisode.BorderColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.txtEpisode.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtEpisode.ForeColor = System.Drawing.Color.Gainsboro
        Me.txtEpisode.Location = New System.Drawing.Point(461, 43)
        Me.txtEpisode.MaxLength = 32767
        Me.txtEpisode.Multiline = False
        Me.txtEpisode.Name = "txtEpisode"
        Me.txtEpisode.NumericOnly = True
        Me.txtEpisode.ReadOnly = False
        Me.txtEpisode.Size = New System.Drawing.Size(23, 27)
        Me.txtEpisode.TabIndex = 3
        Me.txtEpisode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtEpisode.UseSystemPasswordChar = False
        '
        'txtSeason
        '
        Me.txtSeason.BorderColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.txtSeason.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtSeason.ForeColor = System.Drawing.Color.Gainsboro
        Me.txtSeason.Location = New System.Drawing.Point(427, 43)
        Me.txtSeason.MaxLength = 32767
        Me.txtSeason.Multiline = False
        Me.txtSeason.Name = "txtSeason"
        Me.txtSeason.NumericOnly = True
        Me.txtSeason.ReadOnly = False
        Me.txtSeason.Size = New System.Drawing.Size(23, 27)
        Me.txtSeason.TabIndex = 2
        Me.txtSeason.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSeason.UseSystemPasswordChar = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.Transparent
        Me.btnSearch.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSearch.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearch.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(497, 44)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(79, 27)
        Me.btnSearch.TabIndex = 33
        Me.btnSearch.Text = "SEARCH"
        '
        'txtShowName
        '
        Me.txtShowName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.txtShowName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtShowName.ForeColor = System.Drawing.Color.Gainsboro
        Me.txtShowName.Location = New System.Drawing.Point(107, 43)
        Me.txtShowName.MaxLength = 32767
        Me.txtShowName.Multiline = False
        Me.txtShowName.Name = "txtShowName"
        Me.txtShowName.NumericOnly = False
        Me.txtShowName.ReadOnly = False
        Me.txtShowName.Size = New System.Drawing.Size(309, 27)
        Me.txtShowName.TabIndex = 1
        Me.txtShowName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtShowName.UseSystemPasswordChar = False
        '
        'frmSubtitlesDownloader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(590, 410)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pcLoading)
        Me.Controls.Add(Me.lblLanguageHeader)
        Me.Controls.Add(Me.lblEpisodeHeader)
        Me.Controls.Add(Me.lblSeasonHeader)
        Me.Controls.Add(Me.lblShowNameHeader)
        Me.Controls.Add(Me.lvSubtitles)
        Me.Controls.Add(Me.lblEpisode)
        Me.Controls.Add(Me.lblSeason)
        Me.Controls.Add(Me.txtEpisode)
        Me.Controls.Add(Me.txtSeason)
        Me.Controls.Add(Me.lblShowname)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtShowName)
        Me.Controls.Add(Me.pnlBlock)
        Me.Controls.Add(Me.lblMinimize)
        Me.Controls.Add(Me.lblClose)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSubtitlesDownloader"
        Me.Text = "Download Subtitles"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcLoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMinimize As System.Windows.Forms.Label
    Friend WithEvents lblClose As System.Windows.Forms.Label
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents btnSearch As MetroButton
    Friend WithEvents txtShowName As MetroTextBox
    Friend WithEvents lblShowname As System.Windows.Forms.Label
    Friend WithEvents txtSeason As MetroTextBox
    Friend WithEvents txtEpisode As MetroTextBox
    Friend WithEvents lblSeason As System.Windows.Forms.Label
    Friend WithEvents lblEpisode As System.Windows.Forms.Label
    Friend WithEvents lvSubtitles As MetroListview
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblShowNameHeader As System.Windows.Forms.Label
    Friend WithEvents lblSeasonHeader As System.Windows.Forms.Label
    Friend WithEvents lblEpisodeHeader As System.Windows.Forms.Label
    Friend WithEvents lblLanguageHeader As System.Windows.Forms.Label
    Friend WithEvents pcLoading As System.Windows.Forms.PictureBox
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class

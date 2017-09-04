<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDownload
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDownload))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.lblMinimize = New System.Windows.Forms.Label()
        Me.lblClose = New System.Windows.Forms.Label()
        Me.lvEpisodes = New DotNetStreamer.MetroListview()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvDownload = New DotNetStreamer.MetroListview()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvSeason = New DotNetStreamer.MetroListview()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvShow = New DotNetStreamer.MetroListview()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlTitle.SuspendLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.Transparent
        Me.pnlTitle.Controls.Add(Me.lblStreamer)
        Me.pnlTitle.Controls.Add(Me.pcIcon)
        Me.pnlTitle.Location = New System.Drawing.Point(22, 12)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(88, 53)
        Me.pnlTitle.TabIndex = 49
        '
        'lblStreamer
        '
        Me.lblStreamer.AutoSize = True
        Me.lblStreamer.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblStreamer.ForeColor = System.Drawing.Color.LightGray
        Me.lblStreamer.Location = New System.Drawing.Point(-4, 32)
        Me.lblStreamer.Name = "lblStreamer"
        Me.lblStreamer.Size = New System.Drawing.Size(90, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "DOWNLOAD"
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
        'pnlBlock
        '
        Me.pnlBlock.BackColor = System.Drawing.Color.SeaGreen
        Me.pnlBlock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBlock.Location = New System.Drawing.Point(0, 12)
        Me.pnlBlock.Name = "pnlBlock"
        Me.pnlBlock.Size = New System.Drawing.Size(13, 55)
        Me.pnlBlock.TabIndex = 48
        Me.pnlBlock.Visible = False
        '
        'lblMinimize
        '
        Me.lblMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMinimize.BackColor = System.Drawing.Color.Transparent
        Me.lblMinimize.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblMinimize.ForeColor = System.Drawing.Color.LightGray
        Me.lblMinimize.Location = New System.Drawing.Point(807, 10)
        Me.lblMinimize.Name = "lblMinimize"
        Me.lblMinimize.Size = New System.Drawing.Size(17, 16)
        Me.lblMinimize.TabIndex = 66
        Me.lblMinimize.Text = "0"
        Me.lblMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClose
        '
        Me.lblClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClose.BackColor = System.Drawing.Color.Transparent
        Me.lblClose.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblClose.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose.Location = New System.Drawing.Point(826, 10)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(16, 16)
        Me.lblClose.TabIndex = 65
        Me.lblClose.Text = "r"
        Me.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lvEpisodes
        '
        Me.lvEpisodes.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.lvEpisodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvEpisodes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader8})
        Me.lvEpisodes.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lvEpisodes.ForeColor = System.Drawing.Color.LightGray
        Me.lvEpisodes.FullRowSelect = True
        Me.lvEpisodes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvEpisodes.Location = New System.Drawing.Point(578, 89)
        Me.lvEpisodes.MultiSelect = False
        Me.lvEpisodes.Name = "lvEpisodes"
        Me.lvEpisodes.Size = New System.Drawing.Size(277, 214)
        Me.lvEpisodes.TabIndex = 71
        Me.lvEpisodes.UseCompatibleStateImageBehavior = False
        Me.lvEpisodes.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Show"
        Me.ColumnHeader3.Width = 139
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "State"
        Me.ColumnHeader8.Width = 133
        '
        'lvDownload
        '
        Me.lvDownload.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.lvDownload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvDownload.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lvDownload.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lvDownload.ForeColor = System.Drawing.Color.LightGray
        Me.lvDownload.FullRowSelect = True
        Me.lvDownload.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvDownload.Location = New System.Drawing.Point(12, 310)
        Me.lvDownload.Name = "lvDownload"
        Me.lvDownload.Size = New System.Drawing.Size(843, 139)
        Me.lvDownload.TabIndex = 70
        Me.lvDownload.UseCompatibleStateImageBehavior = False
        Me.lvDownload.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Show"
        Me.ColumnHeader4.Width = 177
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Episode"
        Me.ColumnHeader5.Width = 188
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Location"
        Me.ColumnHeader6.Width = 362
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Progress"
        Me.ColumnHeader7.Width = 108
        '
        'lvSeason
        '
        Me.lvSeason.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.lvSeason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvSeason.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lvSeason.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lvSeason.ForeColor = System.Drawing.Color.LightGray
        Me.lvSeason.FullRowSelect = True
        Me.lvSeason.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvSeason.Location = New System.Drawing.Point(295, 89)
        Me.lvSeason.MultiSelect = False
        Me.lvSeason.Name = "lvSeason"
        Me.lvSeason.Size = New System.Drawing.Size(277, 214)
        Me.lvSeason.TabIndex = 68
        Me.lvSeason.UseCompatibleStateImageBehavior = False
        Me.lvSeason.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Show"
        Me.ColumnHeader2.Width = 258
        '
        'lvShow
        '
        Me.lvShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.lvShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvShow.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvShow.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lvShow.ForeColor = System.Drawing.Color.LightGray
        Me.lvShow.FullRowSelect = True
        Me.lvShow.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvShow.Location = New System.Drawing.Point(12, 89)
        Me.lvShow.Name = "lvShow"
        Me.lvShow.Size = New System.Drawing.Size(277, 214)
        Me.lvShow.TabIndex = 67
        Me.lvShow.UseCompatibleStateImageBehavior = False
        Me.lvShow.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Show"
        Me.ColumnHeader1.Width = 258
        '
        'frmDownload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(867, 456)
        Me.Controls.Add(Me.lvEpisodes)
        Me.Controls.Add(Me.lvDownload)
        Me.Controls.Add(Me.lvSeason)
        Me.Controls.Add(Me.lvShow)
        Me.Controls.Add(Me.lblMinimize)
        Me.Controls.Add(Me.lblClose)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlBlock)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDownload"
        Me.Text = ".NET Streamer - SRT ReSync"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents lblMinimize As System.Windows.Forms.Label
    Friend WithEvents lblClose As System.Windows.Forms.Label
    Friend WithEvents lvShow As DotNetStreamer.MetroListview
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvSeason As DotNetStreamer.MetroListview
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvDownload As DotNetStreamer.MetroListview
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvEpisodes As DotNetStreamer.MetroListview
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
End Class

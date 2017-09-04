<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreferences
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreferences))
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.lblMinimize = New System.Windows.Forms.Label()
        Me.lblClose = New System.Windows.Forms.Label()
        Me.lblPreview = New System.Windows.Forms.Label()
        Me.lblSUBTITLES = New System.Windows.Forms.Label()
        Me.lblPreviewText = New System.Windows.Forms.Label()
        Me.lblFontAndColor = New System.Windows.Forms.Label()
        Me.lblSTREAMING = New System.Windows.Forms.Label()
        Me.lblVideoService = New System.Windows.Forms.Label()
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.lblAbout = New System.Windows.Forms.Label()
        Me.lblAboutTrans = New System.Windows.Forms.Label()
        Me.lblAboutMava = New System.Windows.Forms.Label()
        Me.lblTranslu6de = New System.Windows.Forms.Label()
        Me.lblMavamaarten = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbVideoDatabase = New DotNetStreamer.MetroCombobox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbNetworkCaching = New DotNetStreamer.MetroCombobox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkAutoSub = New DotNetStreamer.MetroCheckbox()
        Me.btnOK = New DotNetStreamer.MetroButton()
        Me.btnCancel = New DotNetStreamer.MetroButton()
        Me.MetroCombobox1 = New DotNetStreamer.MetroCombobox()
        Me.cbVideoservice = New DotNetStreamer.MetroCombobox()
        Me.chkHint = New DotNetStreamer.MetroCheckbox()
        Me.btnApply = New DotNetStreamer.MetroButton()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.txtFont = New DotNetStreamer.MetroTextBox()
        Me.btnChooseFont = New DotNetStreamer.MetroButton()
        Me.pnlTitle.SuspendLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBlock
        '
        Me.pnlBlock.BackColor = System.Drawing.Color.SeaGreen
        Me.pnlBlock.Location = New System.Drawing.Point(1, 24)
        Me.pnlBlock.Name = "pnlBlock"
        Me.pnlBlock.Size = New System.Drawing.Size(13, 55)
        Me.pnlBlock.TabIndex = 27
        Me.pnlBlock.Visible = False
        '
        'lblMinimize
        '
        Me.lblMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMinimize.BackColor = System.Drawing.Color.Transparent
        Me.lblMinimize.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblMinimize.ForeColor = System.Drawing.Color.LightGray
        Me.lblMinimize.Location = New System.Drawing.Point(374, 3)
        Me.lblMinimize.Name = "lblMinimize"
        Me.lblMinimize.Size = New System.Drawing.Size(17, 16)
        Me.lblMinimize.TabIndex = 30
        Me.lblMinimize.Text = "0"
        Me.lblMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClose
        '
        Me.lblClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClose.BackColor = System.Drawing.Color.Transparent
        Me.lblClose.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblClose.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose.Location = New System.Drawing.Point(393, 3)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(16, 16)
        Me.lblClose.TabIndex = 29
        Me.lblClose.Text = "r"
        Me.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPreview
        '
        Me.lblPreview.BackColor = System.Drawing.Color.Black
        Me.lblPreview.ForeColor = System.Drawing.Color.White
        Me.lblPreview.Location = New System.Drawing.Point(4, 198)
        Me.lblPreview.Name = "lblPreview"
        Me.lblPreview.Size = New System.Drawing.Size(405, 60)
        Me.lblPreview.TabIndex = 31
        Me.lblPreview.Text = "This is subtitle line number 1." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This is line number two!"
        Me.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSUBTITLES
        '
        Me.lblSUBTITLES.AutoSize = True
        Me.lblSUBTITLES.BackColor = System.Drawing.Color.Transparent
        Me.lblSUBTITLES.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.lblSUBTITLES.ForeColor = System.Drawing.Color.LightGray
        Me.lblSUBTITLES.Location = New System.Drawing.Point(7, 91)
        Me.lblSUBTITLES.Name = "lblSUBTITLES"
        Me.lblSUBTITLES.Size = New System.Drawing.Size(91, 25)
        Me.lblSUBTITLES.TabIndex = 28
        Me.lblSUBTITLES.Text = "SUBTITLES"
        '
        'lblPreviewText
        '
        Me.lblPreviewText.AutoSize = True
        Me.lblPreviewText.BackColor = System.Drawing.Color.Transparent
        Me.lblPreviewText.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblPreviewText.ForeColor = System.Drawing.Color.LightGray
        Me.lblPreviewText.Location = New System.Drawing.Point(6, 179)
        Me.lblPreviewText.Name = "lblPreviewText"
        Me.lblPreviewText.Size = New System.Drawing.Size(49, 13)
        Me.lblPreviewText.TabIndex = 32
        Me.lblPreviewText.Text = "Preview:"
        '
        'lblFontAndColor
        '
        Me.lblFontAndColor.AutoSize = True
        Me.lblFontAndColor.BackColor = System.Drawing.Color.Transparent
        Me.lblFontAndColor.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblFontAndColor.ForeColor = System.Drawing.Color.LightGray
        Me.lblFontAndColor.Location = New System.Drawing.Point(7, 126)
        Me.lblFontAndColor.Name = "lblFontAndColor"
        Me.lblFontAndColor.Size = New System.Drawing.Size(86, 13)
        Me.lblFontAndColor.TabIndex = 35
        Me.lblFontAndColor.Text = "Font and color:"
        '
        'lblSTREAMING
        '
        Me.lblSTREAMING.AutoSize = True
        Me.lblSTREAMING.BackColor = System.Drawing.Color.Transparent
        Me.lblSTREAMING.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.lblSTREAMING.ForeColor = System.Drawing.Color.LightGray
        Me.lblSTREAMING.Location = New System.Drawing.Point(6, 165)
        Me.lblSTREAMING.Name = "lblSTREAMING"
        Me.lblSTREAMING.Size = New System.Drawing.Size(104, 25)
        Me.lblSTREAMING.TabIndex = 37
        Me.lblSTREAMING.Text = "STREAMING"
        '
        'lblVideoService
        '
        Me.lblVideoService.AutoSize = True
        Me.lblVideoService.BackColor = System.Drawing.Color.Transparent
        Me.lblVideoService.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblVideoService.ForeColor = System.Drawing.Color.Silver
        Me.lblVideoService.Location = New System.Drawing.Point(8, 195)
        Me.lblVideoService.Name = "lblVideoService"
        Me.lblVideoService.Size = New System.Drawing.Size(188, 15)
        Me.lblVideoService.TabIndex = 39
        Me.lblVideoService.Text = "Preferred video streaming service: "
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.Transparent
        Me.pnlTitle.Controls.Add(Me.lblStreamer)
        Me.pnlTitle.Controls.Add(Me.pcIcon)
        Me.pnlTitle.Location = New System.Drawing.Point(23, 24)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(97, 53)
        Me.pnlTitle.TabIndex = 43
        '
        'lblStreamer
        '
        Me.lblStreamer.AutoSize = True
        Me.lblStreamer.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblStreamer.ForeColor = System.Drawing.Color.LightGray
        Me.lblStreamer.Location = New System.Drawing.Point(-4, 32)
        Me.lblStreamer.Name = "lblStreamer"
        Me.lblStreamer.Size = New System.Drawing.Size(99, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "PREFERENCES"
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
        'lblAbout
        '
        Me.lblAbout.AutoSize = True
        Me.lblAbout.BackColor = System.Drawing.Color.Transparent
        Me.lblAbout.Font = New System.Drawing.Font("Segoe UI Light", 17.0!)
        Me.lblAbout.ForeColor = System.Drawing.Color.LightGray
        Me.lblAbout.Location = New System.Drawing.Point(123, 14)
        Me.lblAbout.Name = "lblAbout"
        Me.lblAbout.Size = New System.Drawing.Size(211, 31)
        Me.lblAbout.TabIndex = 44
        Me.lblAbout.Text = "About .Net streamer"
        '
        'lblAboutTrans
        '
        Me.lblAboutTrans.AutoSize = True
        Me.lblAboutTrans.BackColor = System.Drawing.Color.Transparent
        Me.lblAboutTrans.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblAboutTrans.ForeColor = System.Drawing.Color.DarkGray
        Me.lblAboutTrans.Location = New System.Drawing.Point(222, 48)
        Me.lblAboutTrans.Name = "lblAboutTrans"
        Me.lblAboutTrans.Size = New System.Drawing.Size(177, 13)
        Me.lblAboutTrans.TabIndex = 45
        Me.lblAboutTrans.Text = "Streaming, subtitle downloading"
        '
        'lblAboutMava
        '
        Me.lblAboutMava.AutoSize = True
        Me.lblAboutMava.BackColor = System.Drawing.Color.Transparent
        Me.lblAboutMava.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblAboutMava.ForeColor = System.Drawing.Color.DarkGray
        Me.lblAboutMava.Location = New System.Drawing.Point(222, 64)
        Me.lblAboutMava.Name = "lblAboutMava"
        Me.lblAboutMava.Size = New System.Drawing.Size(176, 13)
        Me.lblAboutMava.TabIndex = 46
        Me.lblAboutMava.Text = "GUI, threading, subtitle playback"
        '
        'lblTranslu6de
        '
        Me.lblTranslu6de.AutoSize = True
        Me.lblTranslu6de.BackColor = System.Drawing.Color.Transparent
        Me.lblTranslu6de.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTranslu6de.ForeColor = System.Drawing.Color.DarkGray
        Me.lblTranslu6de.Location = New System.Drawing.Point(126, 48)
        Me.lblTranslu6de.Name = "lblTranslu6de"
        Me.lblTranslu6de.Size = New System.Drawing.Size(73, 13)
        Me.lblTranslu6de.TabIndex = 47
        Me.lblTranslu6de.Text = "TRANSLU6DE"
        '
        'lblMavamaarten
        '
        Me.lblMavamaarten.AutoSize = True
        Me.lblMavamaarten.BackColor = System.Drawing.Color.Transparent
        Me.lblMavamaarten.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMavamaarten.ForeColor = System.Drawing.Color.DarkGray
        Me.lblMavamaarten.Location = New System.Drawing.Point(126, 64)
        Me.lblMavamaarten.Name = "lblMavamaarten"
        Me.lblMavamaarten.Size = New System.Drawing.Size(84, 13)
        Me.lblMavamaarten.TabIndex = 48
        Me.lblMavamaarten.Text = "Mavamaarten~"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label1.ForeColor = System.Drawing.Color.DarkGray
        Me.Label1.Location = New System.Drawing.Point(125, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Idb"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label2.ForeColor = System.Drawing.Color.DarkGray
        Me.Label2.Location = New System.Drawing.Point(222, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Http Utility Class"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.ForeColor = System.Drawing.Color.Silver
        Me.Label3.Location = New System.Drawing.Point(9, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(209, 15)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Filter language for subtitles download:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.cbVideoDatabase)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cbNetworkCaching)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.chkAutoSub)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.btnOK)
        Me.Panel1.Controls.Add(Me.lblSTREAMING)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.MetroCombobox1)
        Me.Panel1.Controls.Add(Me.lblVideoService)
        Me.Panel1.Controls.Add(Me.cbVideoservice)
        Me.Panel1.Controls.Add(Me.chkHint)
        Me.Panel1.Controls.Add(Me.btnApply)
        Me.Panel1.Location = New System.Drawing.Point(4, 267)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(404, 289)
        Me.Panel1.TabIndex = 54
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label6.ForeColor = System.Drawing.Color.Silver
        Me.Label6.Location = New System.Drawing.Point(8, 134)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 15)
        Me.Label6.TabIndex = 58
        Me.Label6.Text = "Content database: "
        '
        'cbVideoDatabase
        '
        Me.cbVideoDatabase.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.cbVideoDatabase.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.cbVideoDatabase.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cbVideoDatabase.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cbVideoDatabase.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cbVideoDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVideoDatabase.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cbVideoDatabase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cbVideoDatabase.FormattingEnabled = True
        Me.cbVideoDatabase.ItemHeight = 16
        Me.cbVideoDatabase.Items.AddRange(New Object() {"Primewire.ag", ".NET Streamer API"})
        Me.cbVideoDatabase.Location = New System.Drawing.Point(116, 131)
        Me.cbVideoDatabase.Name = "cbVideoDatabase"
        Me.cbVideoDatabase.Size = New System.Drawing.Size(184, 22)
        Me.cbVideoDatabase.TabIndex = 59
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.Label5.ForeColor = System.Drawing.Color.LightGray
        Me.Label5.Location = New System.Drawing.Point(4, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 25)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "DATABASE"
        '
        'cbNetworkCaching
        '
        Me.cbNetworkCaching.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.cbNetworkCaching.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.cbNetworkCaching.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cbNetworkCaching.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cbNetworkCaching.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cbNetworkCaching.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbNetworkCaching.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cbNetworkCaching.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cbNetworkCaching.FormattingEnabled = True
        Me.cbNetworkCaching.ItemHeight = 16
        Me.cbNetworkCaching.Items.AddRange(New Object() {"1", "5", "10", "15", "30", "60"})
        Me.cbNetworkCaching.Location = New System.Drawing.Point(247, 225)
        Me.cbNetworkCaching.Name = "cbNetworkCaching"
        Me.cbNetworkCaching.Size = New System.Drawing.Size(146, 22)
        Me.cbNetworkCaching.TabIndex = 56
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label4.ForeColor = System.Drawing.Color.Silver
        Me.Label4.Location = New System.Drawing.Point(8, 227)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(228, 15)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Seconds to buffer videos before they play:"
        '
        'chkAutoSub
        '
        Me.chkAutoSub.BackColor = System.Drawing.Color.Transparent
        Me.chkAutoSub.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.chkAutoSub.CheckColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.chkAutoSub.Checked = True
        Me.chkAutoSub.FillColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.chkAutoSub.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.chkAutoSub.ForeColor = System.Drawing.Color.Silver
        Me.chkAutoSub.Location = New System.Drawing.Point(12, 45)
        Me.chkAutoSub.Name = "chkAutoSub"
        Me.chkAutoSub.Size = New System.Drawing.Size(232, 19)
        Me.chkAutoSub.TabIndex = 54
        Me.chkAutoSub.Text = "Automatically download subtitles"
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Transparent
        Me.btnOK.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnOK.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(275, 259)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(118, 22)
        Me.btnOK.TabIndex = 36
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCancel.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCancel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(10, 259)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(118, 22)
        Me.btnCancel.TabIndex = 38
        Me.btnCancel.Text = "CANCEL"
        '
        'MetroCombobox1
        '
        Me.MetroCombobox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.MetroCombobox1.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.MetroCombobox1.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.MetroCombobox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.MetroCombobox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.MetroCombobox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.MetroCombobox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MetroCombobox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.MetroCombobox1.FormattingEnabled = True
        Me.MetroCombobox1.ItemHeight = 16
        Me.MetroCombobox1.Items.AddRange(New Object() {"None", "English", "French", "Dutch", "German", "Danish", "Brazilian", "Spanish", "Italian", "Croatian", "Swedish"})
        Me.MetroCombobox1.Location = New System.Drawing.Point(218, 9)
        Me.MetroCombobox1.Name = "MetroCombobox1"
        Me.MetroCombobox1.Size = New System.Drawing.Size(178, 22)
        Me.MetroCombobox1.TabIndex = 53
        '
        'cbVideoservice
        '
        Me.cbVideoservice.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.cbVideoservice.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.cbVideoservice.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.cbVideoservice.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.cbVideoservice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cbVideoservice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVideoservice.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cbVideoservice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cbVideoservice.FormattingEnabled = True
        Me.cbVideoservice.ItemHeight = 16
        Me.cbVideoservice.Items.AddRange(New Object() {"Automatic", "Thefile.me", "Thevideo.me", "Promptfile.com", "Bestreams.net", "Gorillavid.in"})
        Me.cbVideoservice.Location = New System.Drawing.Point(202, 192)
        Me.cbVideoservice.Name = "cbVideoservice"
        Me.cbVideoservice.Size = New System.Drawing.Size(191, 22)
        Me.cbVideoservice.TabIndex = 41
        '
        'chkHint
        '
        Me.chkHint.BackColor = System.Drawing.Color.Transparent
        Me.chkHint.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.chkHint.CheckColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.chkHint.Checked = True
        Me.chkHint.FillColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer))
        Me.chkHint.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.chkHint.ForeColor = System.Drawing.Color.Silver
        Me.chkHint.Location = New System.Drawing.Point(13, 70)
        Me.chkHint.Name = "chkHint"
        Me.chkHint.Size = New System.Drawing.Size(232, 19)
        Me.chkHint.TabIndex = 42
        Me.chkHint.Text = "Show ""Did you know"" subtitles hint"
        '
        'btnApply
        '
        Me.btnApply.BackColor = System.Drawing.Color.Transparent
        Me.btnApply.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnApply.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApply.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApply.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApply.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnApply.ForeColor = System.Drawing.Color.White
        Me.btnApply.Location = New System.Drawing.Point(142, 259)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(118, 22)
        Me.btnApply.TabIndex = 49
        Me.btnApply.Text = "APPLY"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.lblVersion.ForeColor = System.Drawing.Color.Silver
        Me.lblVersion.Location = New System.Drawing.Point(9, 8)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(46, 12)
        Me.lblVersion.TabIndex = 55
        Me.lblVersion.Text = "Build: /"
        '
        'txtFont
        '
        Me.txtFont.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.txtFont.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtFont.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtFont.ForeColor = System.Drawing.Color.White
        Me.txtFont.Location = New System.Drawing.Point(9, 143)
        Me.txtFont.MaxLength = 32767
        Me.txtFont.Multiline = False
        Me.txtFont.Name = "txtFont"
        Me.txtFont.NumericOnly = False
        Me.txtFont.ReadOnly = False
        Me.txtFont.Size = New System.Drawing.Size(295, 27)
        Me.txtFont.TabIndex = 33
        Me.txtFont.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtFont.UseSystemPasswordChar = False
        '
        'btnChooseFont
        '
        Me.btnChooseFont.BackColor = System.Drawing.Color.Transparent
        Me.btnChooseFont.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnChooseFont.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnChooseFont.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnChooseFont.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnChooseFont.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnChooseFont.ForeColor = System.Drawing.Color.White
        Me.btnChooseFont.Location = New System.Drawing.Point(314, 142)
        Me.btnChooseFont.Name = "btnChooseFont"
        Me.btnChooseFont.Size = New System.Drawing.Size(79, 27)
        Me.btnChooseFont.TabIndex = 34
        Me.btnChooseFont.Text = "Choose..."
        '
        'frmPreferences
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(414, 559)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblFontAndColor)
        Me.Controls.Add(Me.lblPreview)
        Me.Controls.Add(Me.lblPreviewText)
        Me.Controls.Add(Me.txtFont)
        Me.Controls.Add(Me.btnChooseFont)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblAboutMava)
        Me.Controls.Add(Me.lblMavamaarten)
        Me.Controls.Add(Me.lblTranslu6de)
        Me.Controls.Add(Me.lblAboutTrans)
        Me.Controls.Add(Me.lblAbout)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.lblSUBTITLES)
        Me.Controls.Add(Me.lblMinimize)
        Me.Controls.Add(Me.lblClose)
        Me.Controls.Add(Me.pnlBlock)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPreferences"
        Me.Text = "Preferences"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents lblMinimize As System.Windows.Forms.Label
    Friend WithEvents lblClose As System.Windows.Forms.Label
    Friend WithEvents lblPreview As System.Windows.Forms.Label
    Friend WithEvents lblSUBTITLES As System.Windows.Forms.Label
    Friend WithEvents lblPreviewText As System.Windows.Forms.Label
    Friend WithEvents btnChooseFont As MetroButton
    Friend WithEvents txtFont As MetroTextBox
    Friend WithEvents lblFontAndColor As System.Windows.Forms.Label
    Friend WithEvents btnOK As MetroButton
    Friend WithEvents lblSTREAMING As System.Windows.Forms.Label
    Friend WithEvents btnCancel As MetroButton
    Friend WithEvents lblVideoService As System.Windows.Forms.Label
    Friend WithEvents cbVideoservice As MetroCombobox
    Friend WithEvents chkHint As MetroCheckbox
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents lblAbout As System.Windows.Forms.Label
    Friend WithEvents lblAboutTrans As System.Windows.Forms.Label
    Friend WithEvents lblAboutMava As System.Windows.Forms.Label
    Friend WithEvents lblTranslu6de As System.Windows.Forms.Label
    Friend WithEvents lblMavamaarten As System.Windows.Forms.Label
    Friend WithEvents btnApply As MetroButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents MetroCombobox1 As MetroCombobox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkAutoSub As DotNetStreamer.MetroCheckbox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents cbNetworkCaching As DotNetStreamer.MetroCombobox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbVideoDatabase As DotNetStreamer.MetroCombobox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class

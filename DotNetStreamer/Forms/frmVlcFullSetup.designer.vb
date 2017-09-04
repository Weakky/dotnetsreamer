<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVlcFullSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVlcFullSetup))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.PanelManager1 = New DotNetStreamer.Controls.PanelManager()
        Me.PnlWelcome = New DotNetStreamer.Controls.ManagedPanel()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.MetroButton1 = New DotNetStreamer.MetroButton()
        Me.PnlAlreadyInstalled = New DotNetStreamer.Controls.ManagedPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PnlDownloadVLC = New DotNetStreamer.Controls.ManagedPanel()
        Me.lblPercentage = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.lblTimeLeft = New System.Windows.Forms.Label()
        Me.pbProgress = New DotNetStreamer.MetroProgressbarHorizontal()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PnlLaunchSetup = New DotNetStreamer.Controls.ManagedPanel()
        Me.btnNextLaunchSetup = New DotNetStreamer.MetroButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PnlWaitVlcInstallation = New DotNetStreamer.Controls.ManagedPanel()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblPath = New System.Windows.Forms.Label()
        Me.lblInstalled = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnWaitInstallNext = New DotNetStreamer.MetroButton()
        Me.PnlFinishSetup = New DotNetStreamer.Controls.ManagedPanel()
        Me.MetroButton2 = New DotNetStreamer.MetroButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PnlWrongVlcVersion = New DotNetStreamer.Controls.ManagedPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnUninstaller = New DotNetStreamer.MetroButton()
        Me.PnlAlreadyDownload = New DotNetStreamer.Controls.ManagedPanel()
        Me.btnNextAlreadyDownloaded = New DotNetStreamer.MetroButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PnlWaitToUninstall = New DotNetStreamer.Controls.ManagedPanel()
        Me.btnUninstallNext = New DotNetStreamer.MetroButton()
        Me.lblUninstallStatus = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlTitle.SuspendLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelManager1.SuspendLayout()
        Me.PnlWelcome.SuspendLayout()
        Me.PnlAlreadyInstalled.SuspendLayout()
        Me.PnlDownloadVLC.SuspendLayout()
        Me.PnlLaunchSetup.SuspendLayout()
        Me.PnlWaitVlcInstallation.SuspendLayout()
        Me.PnlFinishSetup.SuspendLayout()
        Me.PnlWrongVlcVersion.SuspendLayout()
        Me.PnlAlreadyDownload.SuspendLayout()
        Me.PnlWaitToUninstall.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.Transparent
        Me.pnlTitle.Controls.Add(Me.lblStreamer)
        Me.pnlTitle.Controls.Add(Me.pcIcon)
        Me.pnlTitle.Location = New System.Drawing.Point(19, 14)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(82, 53)
        Me.pnlTitle.TabIndex = 50
        '
        'lblStreamer
        '
        Me.lblStreamer.AutoSize = True
        Me.lblStreamer.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblStreamer.ForeColor = System.Drawing.Color.LightGray
        Me.lblStreamer.Location = New System.Drawing.Point(-4, 32)
        Me.lblStreamer.Name = "lblStreamer"
        Me.lblStreamer.Size = New System.Drawing.Size(78, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "VLC SETUP"
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
        Me.pnlBlock.TabIndex = 51
        Me.pnlBlock.Visible = False
        '
        'PanelManager1
        '
        Me.PanelManager1.BackColor = System.Drawing.Color.Transparent
        Me.PanelManager1.Controls.Add(Me.PnlWelcome)
        Me.PanelManager1.Controls.Add(Me.PnlAlreadyInstalled)
        Me.PanelManager1.Controls.Add(Me.PnlDownloadVLC)
        Me.PanelManager1.Controls.Add(Me.PnlLaunchSetup)
        Me.PanelManager1.Controls.Add(Me.PnlWaitVlcInstallation)
        Me.PanelManager1.Controls.Add(Me.PnlFinishSetup)
        Me.PanelManager1.Controls.Add(Me.PnlWrongVlcVersion)
        Me.PanelManager1.Controls.Add(Me.PnlAlreadyDownload)
        Me.PanelManager1.Controls.Add(Me.PnlWaitToUninstall)
        Me.PanelManager1.Location = New System.Drawing.Point(11, 70)
        Me.PanelManager1.Name = "PanelManager1"
        Me.PanelManager1.SelectedIndex = 0
        Me.PanelManager1.SelectedPanel = Me.PnlWelcome
        Me.PanelManager1.Size = New System.Drawing.Size(532, 125)
        Me.PanelManager1.TabIndex = 54
        '
        'PnlWelcome
        '
        Me.PnlWelcome.Controls.Add(Me.lblWelcome)
        Me.PnlWelcome.Controls.Add(Me.MetroButton1)
        Me.PnlWelcome.Location = New System.Drawing.Point(0, 0)
        Me.PnlWelcome.Name = "PnlWelcome"
        Me.PnlWelcome.Size = New System.Drawing.Size(532, 125)
        Me.PnlWelcome.Text = "ManagedPanel1"
        '
        'lblWelcome
        '
        Me.lblWelcome.AutoSize = True
        Me.lblWelcome.BackColor = System.Drawing.Color.Transparent
        Me.lblWelcome.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblWelcome.ForeColor = System.Drawing.Color.LightGray
        Me.lblWelcome.Location = New System.Drawing.Point(6, 8)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(340, 80)
        Me.lblWelcome.TabIndex = 52
        Me.lblWelcome.Text = "Welcome on the .NET Streamer VLC setup system." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "In order to get the application p" & _
    "roperly working, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "we need to install and register VLC on your computer." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Just f" & _
    "ollow the few steps."
        '
        'MetroButton1
        '
        Me.MetroButton1.BackColor = System.Drawing.Color.Transparent
        Me.MetroButton1.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MetroButton1.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroButton1.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroButton1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroButton1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MetroButton1.ForeColor = System.Drawing.Color.White
        Me.MetroButton1.Location = New System.Drawing.Point(420, 89)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(105, 27)
        Me.MetroButton1.TabIndex = 53
        Me.MetroButton1.Text = "Next"
        '
        'PnlAlreadyInstalled
        '
        Me.PnlAlreadyInstalled.Controls.Add(Me.Label1)
        Me.PnlAlreadyInstalled.Location = New System.Drawing.Point(0, 0)
        Me.PnlAlreadyInstalled.Name = "PnlAlreadyInstalled"
        Me.PnlAlreadyInstalled.Size = New System.Drawing.Size(0, 0)
        Me.PnlAlreadyInstalled.Text = "ManagedPanel2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label1.ForeColor = System.Drawing.Color.LightGray
        Me.Label1.Location = New System.Drawing.Point(16, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 20)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "VLC Already installed."
        '
        'PnlDownloadVLC
        '
        Me.PnlDownloadVLC.Controls.Add(Me.lblPercentage)
        Me.PnlDownloadVLC.Controls.Add(Me.lblSpeed)
        Me.PnlDownloadVLC.Controls.Add(Me.lblTimeLeft)
        Me.PnlDownloadVLC.Controls.Add(Me.pbProgress)
        Me.PnlDownloadVLC.Controls.Add(Me.Label2)
        Me.PnlDownloadVLC.Location = New System.Drawing.Point(0, 0)
        Me.PnlDownloadVLC.Name = "PnlDownloadVLC"
        Me.PnlDownloadVLC.Size = New System.Drawing.Size(0, 0)
        Me.PnlDownloadVLC.Text = "ManagedPanel3"
        '
        'lblPercentage
        '
        Me.lblPercentage.AutoSize = True
        Me.lblPercentage.BackColor = System.Drawing.Color.Transparent
        Me.lblPercentage.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblPercentage.ForeColor = System.Drawing.Color.LightGray
        Me.lblPercentage.Location = New System.Drawing.Point(487, 40)
        Me.lblPercentage.Name = "lblPercentage"
        Me.lblPercentage.Size = New System.Drawing.Size(37, 20)
        Me.lblPercentage.TabIndex = 57
        Me.lblPercentage.Text = "00%"
        '
        'lblSpeed
        '
        Me.lblSpeed.AutoSize = True
        Me.lblSpeed.BackColor = System.Drawing.Color.Transparent
        Me.lblSpeed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblSpeed.ForeColor = System.Drawing.Color.LightGray
        Me.lblSpeed.Location = New System.Drawing.Point(406, 93)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(91, 20)
        Me.lblSpeed.TabIndex = 56
        Me.lblSpeed.Text = "Speed: 0KB/S"
        '
        'lblTimeLeft
        '
        Me.lblTimeLeft.AutoSize = True
        Me.lblTimeLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeLeft.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblTimeLeft.ForeColor = System.Drawing.Color.LightGray
        Me.lblTimeLeft.Location = New System.Drawing.Point(9, 93)
        Me.lblTimeLeft.Name = "lblTimeLeft"
        Me.lblTimeLeft.Size = New System.Drawing.Size(146, 20)
        Me.lblTimeLeft.TabIndex = 54
        Me.lblTimeLeft.Text = "Time Left: Estimating..."
        '
        'pbProgress
        '
        Me.pbProgress.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pbProgress.Location = New System.Drawing.Point(12, 63)
        Me.pbProgress.Maximum = 100
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pbProgress.Size = New System.Drawing.Size(512, 23)
        Me.pbProgress.TabIndex = 55
        Me.pbProgress.Text = "MetroProgressbarHorizontal1"
        Me.pbProgress.Value = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label2.ForeColor = System.Drawing.Color.LightGray
        Me.Label2.Location = New System.Drawing.Point(11, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(489, 20)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "VLC is not installed on your system. .NET Streamer is currently downloading it."
        '
        'PnlLaunchSetup
        '
        Me.PnlLaunchSetup.Controls.Add(Me.btnNextLaunchSetup)
        Me.PnlLaunchSetup.Controls.Add(Me.Label5)
        Me.PnlLaunchSetup.Location = New System.Drawing.Point(0, 0)
        Me.PnlLaunchSetup.Name = "PnlLaunchSetup"
        Me.PnlLaunchSetup.Size = New System.Drawing.Size(0, 0)
        Me.PnlLaunchSetup.Text = "ManagedPanel1"
        '
        'btnNextLaunchSetup
        '
        Me.btnNextLaunchSetup.BackColor = System.Drawing.Color.Transparent
        Me.btnNextLaunchSetup.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNextLaunchSetup.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNextLaunchSetup.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNextLaunchSetup.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNextLaunchSetup.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnNextLaunchSetup.ForeColor = System.Drawing.Color.White
        Me.btnNextLaunchSetup.Location = New System.Drawing.Point(373, 88)
        Me.btnNextLaunchSetup.Name = "btnNextLaunchSetup"
        Me.btnNextLaunchSetup.Size = New System.Drawing.Size(148, 27)
        Me.btnNextLaunchSetup.TabIndex = 54
        Me.btnNextLaunchSetup.Text = "Launch Setup (As Admin)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label5.ForeColor = System.Drawing.Color.LightGray
        Me.Label5.Location = New System.Drawing.Point(5, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(497, 80)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = resources.GetString("Label5.Text")
        '
        'PnlWaitVlcInstallation
        '
        Me.PnlWaitVlcInstallation.Controls.Add(Me.lblVersion)
        Me.PnlWaitVlcInstallation.Controls.Add(Me.lblPath)
        Me.PnlWaitVlcInstallation.Controls.Add(Me.lblInstalled)
        Me.PnlWaitVlcInstallation.Controls.Add(Me.Label3)
        Me.PnlWaitVlcInstallation.Controls.Add(Me.btnWaitInstallNext)
        Me.PnlWaitVlcInstallation.Location = New System.Drawing.Point(0, 0)
        Me.PnlWaitVlcInstallation.Name = "PnlWaitVlcInstallation"
        Me.PnlWaitVlcInstallation.Size = New System.Drawing.Size(532, 125)
        Me.PnlWaitVlcInstallation.Text = "ManagedPanel1"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblVersion.ForeColor = System.Drawing.Color.LightGray
        Me.lblVersion.Location = New System.Drawing.Point(9, 76)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(71, 20)
        Me.lblVersion.TabIndex = 58
        Me.lblVersion.Text = "Version: ..."
        '
        'lblPath
        '
        Me.lblPath.AutoSize = True
        Me.lblPath.BackColor = System.Drawing.Color.Transparent
        Me.lblPath.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblPath.ForeColor = System.Drawing.Color.LightGray
        Me.lblPath.Location = New System.Drawing.Point(10, 54)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(51, 20)
        Me.lblPath.TabIndex = 57
        Me.lblPath.Text = "Path: ..."
        '
        'lblInstalled
        '
        Me.lblInstalled.AutoSize = True
        Me.lblInstalled.BackColor = System.Drawing.Color.Transparent
        Me.lblInstalled.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblInstalled.ForeColor = System.Drawing.Color.LightGray
        Me.lblInstalled.Location = New System.Drawing.Point(10, 99)
        Me.lblInstalled.Name = "lblInstalled"
        Me.lblInstalled.Size = New System.Drawing.Size(75, 20)
        Me.lblInstalled.TabIndex = 56
        Me.lblInstalled.Text = "Installed: ..."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label3.ForeColor = System.Drawing.Color.LightGray
        Me.Label3.Location = New System.Drawing.Point(8, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(489, 40)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "We're now waiting for the setup to install VLC. Everything is silent. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "It may ta" & _
    "kes a few minutes. It will automatically detects when it will be finished." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btnWaitInstallNext
        '
        Me.btnWaitInstallNext.BackColor = System.Drawing.Color.Transparent
        Me.btnWaitInstallNext.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnWaitInstallNext.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnWaitInstallNext.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnWaitInstallNext.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnWaitInstallNext.Enabled = False
        Me.btnWaitInstallNext.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnWaitInstallNext.ForeColor = System.Drawing.Color.White
        Me.btnWaitInstallNext.Location = New System.Drawing.Point(416, 90)
        Me.btnWaitInstallNext.Name = "btnWaitInstallNext"
        Me.btnWaitInstallNext.Size = New System.Drawing.Size(105, 27)
        Me.btnWaitInstallNext.TabIndex = 54
        Me.btnWaitInstallNext.Text = "Waiting..."
        '
        'PnlFinishSetup
        '
        Me.PnlFinishSetup.BackColor = System.Drawing.Color.Transparent
        Me.PnlFinishSetup.Controls.Add(Me.MetroButton2)
        Me.PnlFinishSetup.Controls.Add(Me.Label4)
        Me.PnlFinishSetup.Location = New System.Drawing.Point(0, 0)
        Me.PnlFinishSetup.Name = "PnlFinishSetup"
        Me.PnlFinishSetup.Size = New System.Drawing.Size(0, 0)
        Me.PnlFinishSetup.Text = "ManagedPanel4"
        '
        'MetroButton2
        '
        Me.MetroButton2.BackColor = System.Drawing.Color.Transparent
        Me.MetroButton2.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MetroButton2.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroButton2.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroButton2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroButton2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MetroButton2.ForeColor = System.Drawing.Color.White
        Me.MetroButton2.Location = New System.Drawing.Point(421, 90)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(105, 27)
        Me.MetroButton2.TabIndex = 57
        Me.MetroButton2.Text = "Finish."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label4.ForeColor = System.Drawing.Color.LightGray
        Me.Label4.Location = New System.Drawing.Point(3, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(460, 40)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "Congratulations!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "You properly installed VLC. Click 'Finish' to start enjoying .N" & _
    "ET Streamer :)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'PnlWrongVlcVersion
        '
        Me.PnlWrongVlcVersion.Controls.Add(Me.Label6)
        Me.PnlWrongVlcVersion.Controls.Add(Me.btnUninstaller)
        Me.PnlWrongVlcVersion.Location = New System.Drawing.Point(0, 0)
        Me.PnlWrongVlcVersion.Name = "PnlWrongVlcVersion"
        Me.PnlWrongVlcVersion.Size = New System.Drawing.Size(0, 0)
        Me.PnlWrongVlcVersion.Text = "ManagedPanel1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label6.ForeColor = System.Drawing.Color.LightGray
        Me.Label6.Location = New System.Drawing.Point(3, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(493, 80)
        Me.Label6.TabIndex = 54
        Me.Label6.Text = resources.GetString("Label6.Text")
        '
        'btnUninstaller
        '
        Me.btnUninstaller.BackColor = System.Drawing.Color.Transparent
        Me.btnUninstaller.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUninstaller.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnUninstaller.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnUninstaller.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnUninstaller.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnUninstaller.ForeColor = System.Drawing.Color.White
        Me.btnUninstaller.Location = New System.Drawing.Point(411, 91)
        Me.btnUninstaller.Name = "btnUninstaller"
        Me.btnUninstaller.Size = New System.Drawing.Size(111, 27)
        Me.btnUninstaller.TabIndex = 55
        Me.btnUninstaller.Text = "Launch Uninstaller"
        '
        'PnlAlreadyDownload
        '
        Me.PnlAlreadyDownload.Controls.Add(Me.btnNextAlreadyDownloaded)
        Me.PnlAlreadyDownload.Controls.Add(Me.Label8)
        Me.PnlAlreadyDownload.Location = New System.Drawing.Point(0, 0)
        Me.PnlAlreadyDownload.Name = "PnlAlreadyDownload"
        Me.PnlAlreadyDownload.Size = New System.Drawing.Size(0, 0)
        Me.PnlAlreadyDownload.Text = "ManagedPanel1"
        '
        'btnNextAlreadyDownloaded
        '
        Me.btnNextAlreadyDownloaded.BackColor = System.Drawing.Color.Transparent
        Me.btnNextAlreadyDownloaded.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnNextAlreadyDownloaded.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNextAlreadyDownloaded.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNextAlreadyDownloaded.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNextAlreadyDownloaded.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnNextAlreadyDownloaded.ForeColor = System.Drawing.Color.White
        Me.btnNextAlreadyDownloaded.Location = New System.Drawing.Point(375, 90)
        Me.btnNextAlreadyDownloaded.Name = "btnNextAlreadyDownloaded"
        Me.btnNextAlreadyDownloaded.Size = New System.Drawing.Size(148, 27)
        Me.btnNextAlreadyDownloaded.TabIndex = 55
        Me.btnNextAlreadyDownloaded.Text = "Launch Setup (As Admin)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label8.ForeColor = System.Drawing.Color.LightGray
        Me.Label8.Location = New System.Drawing.Point(7, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(436, 80)
        Me.Label8.TabIndex = 54
        Me.Label8.Text = resources.GetString("Label8.Text")
        '
        'PnlWaitToUninstall
        '
        Me.PnlWaitToUninstall.Controls.Add(Me.btnUninstallNext)
        Me.PnlWaitToUninstall.Controls.Add(Me.lblUninstallStatus)
        Me.PnlWaitToUninstall.Controls.Add(Me.Label9)
        Me.PnlWaitToUninstall.Location = New System.Drawing.Point(0, 0)
        Me.PnlWaitToUninstall.Name = "PnlWaitToUninstall"
        Me.PnlWaitToUninstall.Size = New System.Drawing.Size(0, 0)
        Me.PnlWaitToUninstall.Text = "ManagedPanel1"
        '
        'btnUninstallNext
        '
        Me.btnUninstallNext.BackColor = System.Drawing.Color.Transparent
        Me.btnUninstallNext.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnUninstallNext.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnUninstallNext.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnUninstallNext.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnUninstallNext.Enabled = False
        Me.btnUninstallNext.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnUninstallNext.ForeColor = System.Drawing.Color.White
        Me.btnUninstallNext.Location = New System.Drawing.Point(421, 85)
        Me.btnUninstallNext.Name = "btnUninstallNext"
        Me.btnUninstallNext.Size = New System.Drawing.Size(105, 27)
        Me.btnUninstallNext.TabIndex = 59
        Me.btnUninstallNext.Text = "Wating..."
        '
        'lblUninstallStatus
        '
        Me.lblUninstallStatus.AutoSize = True
        Me.lblUninstallStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblUninstallStatus.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblUninstallStatus.ForeColor = System.Drawing.Color.LightGray
        Me.lblUninstallStatus.Location = New System.Drawing.Point(4, 92)
        Me.lblUninstallStatus.Name = "lblUninstallStatus"
        Me.lblUninstallStatus.Size = New System.Drawing.Size(61, 20)
        Me.lblUninstallStatus.TabIndex = 58
        Me.lblUninstallStatus.Text = "Status: ..."
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label9.ForeColor = System.Drawing.Color.LightGray
        Me.Label9.Location = New System.Drawing.Point(4, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(381, 40)
        Me.Label9.TabIndex = 57
        Me.Label9.Text = "We're now waiting for you to finish the VLC uninstall process." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " It will automati" & _
    "cally detects when it gets finished."
        '
        'frmVlcFullSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(549, 199)
        Me.Controls.Add(Me.PanelManager1)
        Me.Controls.Add(Me.pnlBlock)
        Me.Controls.Add(Me.pnlTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVlcFullSetup"
        Me.Text = "Form1"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelManager1.ResumeLayout(False)
        Me.PnlWelcome.ResumeLayout(False)
        Me.PnlWelcome.PerformLayout()
        Me.PnlAlreadyInstalled.ResumeLayout(False)
        Me.PnlAlreadyInstalled.PerformLayout()
        Me.PnlDownloadVLC.ResumeLayout(False)
        Me.PnlDownloadVLC.PerformLayout()
        Me.PnlLaunchSetup.ResumeLayout(False)
        Me.PnlLaunchSetup.PerformLayout()
        Me.PnlWaitVlcInstallation.ResumeLayout(False)
        Me.PnlWaitVlcInstallation.PerformLayout()
        Me.PnlFinishSetup.ResumeLayout(False)
        Me.PnlFinishSetup.PerformLayout()
        Me.PnlWrongVlcVersion.ResumeLayout(False)
        Me.PnlWrongVlcVersion.PerformLayout()
        Me.PnlAlreadyDownload.ResumeLayout(False)
        Me.PnlAlreadyDownload.PerformLayout()
        Me.PnlWaitToUninstall.ResumeLayout(False)
        Me.PnlWaitToUninstall.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents lblWelcome As System.Windows.Forms.Label
    Friend WithEvents MetroButton1 As DotNetStreamer.MetroButton
    Friend WithEvents PanelManager1 As Controls.PanelManager
    Friend WithEvents PnlWelcome As Controls.ManagedPanel
    Friend WithEvents PnlAlreadyInstalled As Controls.ManagedPanel
    Friend WithEvents PnlDownloadVLC As Controls.ManagedPanel
    Friend WithEvents PnlFinishSetup As Controls.ManagedPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PnlWrongVlcVersion As Controls.ManagedPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPercentage As System.Windows.Forms.Label
    Friend WithEvents lblSpeed As System.Windows.Forms.Label
    Friend WithEvents lblTimeLeft As System.Windows.Forms.Label
    Friend WithEvents pbProgress As DotNetStreamer.MetroProgressbarHorizontal
    Friend WithEvents PnlLaunchSetup As Controls.ManagedPanel
    Friend WithEvents btnNextLaunchSetup As DotNetStreamer.MetroButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PnlWaitVlcInstallation As Controls.ManagedPanel
    Friend WithEvents btnWaitInstallNext As DotNetStreamer.MetroButton
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblPath As System.Windows.Forms.Label
    Friend WithEvents lblInstalled As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PnlAlreadyDownload As Controls.ManagedPanel
    Friend WithEvents btnNextAlreadyDownloaded As DotNetStreamer.MetroButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents MetroButton2 As DotNetStreamer.MetroButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnUninstaller As DotNetStreamer.MetroButton
    Friend WithEvents PnlWaitToUninstall As Controls.ManagedPanel
    Friend WithEvents btnUninstallNext As DotNetStreamer.MetroButton
    Friend WithEvents lblUninstallStatus As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label

End Class

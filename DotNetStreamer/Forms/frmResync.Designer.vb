<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReSync
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReSync))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.secNUD = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.milliNUD = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.forwardBtn = New DotNetStreamer.MetroButton()
        Me.backwardBtn = New DotNetStreamer.MetroButton()
        Me.lblMinimize = New System.Windows.Forms.Label()
        Me.lblClose = New System.Windows.Forms.Label()
        Me.pnlTitle.SuspendLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.secNUD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.milliNUD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.Transparent
        Me.pnlTitle.Controls.Add(Me.lblStreamer)
        Me.pnlTitle.Controls.Add(Me.pcIcon)
        Me.pnlTitle.Location = New System.Drawing.Point(22, 12)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(76, 53)
        Me.pnlTitle.TabIndex = 49
        '
        'lblStreamer
        '
        Me.lblStreamer.AutoSize = True
        Me.lblStreamer.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblStreamer.ForeColor = System.Drawing.Color.LightGray
        Me.lblStreamer.Location = New System.Drawing.Point(-4, 32)
        Me.lblStreamer.Name = "lblStreamer"
        Me.lblStreamer.Size = New System.Drawing.Size(60, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "RESYNC"
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Silver
        Me.Label4.Location = New System.Drawing.Point(13, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Seconds:"
        '
        'secNUD
        '
        Me.secNUD.BackColor = System.Drawing.Color.Silver
        Me.secNUD.Location = New System.Drawing.Point(87, 31)
        Me.secNUD.Name = "secNUD"
        Me.secNUD.Size = New System.Drawing.Size(120, 20)
        Me.secNUD.TabIndex = 61
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.milliNUD)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.forwardBtn)
        Me.GroupBox1.Controls.Add(Me.backwardBtn)
        Me.GroupBox1.Controls.Add(Me.secNUD)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Silver
        Me.GroupBox1.Location = New System.Drawing.Point(13, 76)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(223, 128)
        Me.GroupBox1.TabIndex = 64
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SRT Syncer"
        '
        'milliNUD
        '
        Me.milliNUD.BackColor = System.Drawing.Color.Silver
        Me.milliNUD.Location = New System.Drawing.Point(87, 60)
        Me.milliNUD.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.milliNUD.Name = "milliNUD"
        Me.milliNUD.Size = New System.Drawing.Size(120, 20)
        Me.milliNUD.TabIndex = 66
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Silver
        Me.Label1.Location = New System.Drawing.Point(13, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 65
        Me.Label1.Text = "Milliseconds:"
        '
        'forwardBtn
        '
        Me.forwardBtn.BackColor = System.Drawing.Color.Transparent
        Me.forwardBtn.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.forwardBtn.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.forwardBtn.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.forwardBtn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.forwardBtn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.forwardBtn.ForeColor = System.Drawing.Color.White
        Me.forwardBtn.Location = New System.Drawing.Point(115, 92)
        Me.forwardBtn.Name = "forwardBtn"
        Me.forwardBtn.Size = New System.Drawing.Size(95, 22)
        Me.forwardBtn.TabIndex = 64
        Me.forwardBtn.Text = "Forward >"
        '
        'backwardBtn
        '
        Me.backwardBtn.BackColor = System.Drawing.Color.Transparent
        Me.backwardBtn.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.backwardBtn.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.backwardBtn.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.backwardBtn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.backwardBtn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.backwardBtn.ForeColor = System.Drawing.Color.White
        Me.backwardBtn.Location = New System.Drawing.Point(9, 92)
        Me.backwardBtn.Name = "backwardBtn"
        Me.backwardBtn.Size = New System.Drawing.Size(95, 22)
        Me.backwardBtn.TabIndex = 63
        Me.backwardBtn.Text = "< Backward"
        '
        'lblMinimize
        '
        Me.lblMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMinimize.BackColor = System.Drawing.Color.Transparent
        Me.lblMinimize.Font = New System.Drawing.Font("Marlett", 11.0!)
        Me.lblMinimize.ForeColor = System.Drawing.Color.LightGray
        Me.lblMinimize.Location = New System.Drawing.Point(206, 10)
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
        Me.lblClose.Location = New System.Drawing.Point(225, 10)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(16, 16)
        Me.lblClose.TabIndex = 65
        Me.lblClose.Text = "r"
        Me.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmReSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(249, 216)
        Me.Controls.Add(Me.lblMinimize)
        Me.Controls.Add(Me.lblClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlBlock)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReSync"
        Me.Text = ".NET Streamer - SRT ReSync"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.secNUD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.milliNUD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents secNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents backwardBtn As DotNetStreamer.MetroButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblMinimize As System.Windows.Forms.Label
    Friend WithEvents lblClose As System.Windows.Forms.Label
    Friend WithEvents forwardBtn As DotNetStreamer.MetroButton
    Friend WithEvents milliNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class

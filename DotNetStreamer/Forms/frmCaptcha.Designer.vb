<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCaptcha
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCaptcha))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.lblMinimize = New System.Windows.Forms.Label()
        Me.lblClose = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtSolve = New DotNetStreamer.MetroTextBox()
        Me.btnOK = New DotNetStreamer.MetroButton()
        Me.MetroButton1 = New DotNetStreamer.MetroButton()
        Me.pnlTitle.SuspendLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lblStreamer.Size = New System.Drawing.Size(70, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "CAPTCHA"
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
        Me.lblMinimize.Location = New System.Drawing.Point(286, 10)
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
        Me.lblClose.Location = New System.Drawing.Point(305, 10)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(16, 16)
        Me.lblClose.TabIndex = 65
        Me.lblClose.Text = "r"
        Me.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(12, 83)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(305, 150)
        Me.PictureBox1.TabIndex = 67
        Me.PictureBox1.TabStop = False
        '
        'txtSolve
        '
        Me.txtSolve.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.txtSolve.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.txtSolve.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtSolve.ForeColor = System.Drawing.Color.White
        Me.txtSolve.Location = New System.Drawing.Point(12, 239)
        Me.txtSolve.MaxLength = 32767
        Me.txtSolve.Multiline = False
        Me.txtSolve.Name = "txtSolve"
        Me.txtSolve.NumericOnly = False
        Me.txtSolve.ReadOnly = False
        Me.txtSolve.Size = New System.Drawing.Size(305, 23)
        Me.txtSolve.TabIndex = 68
        Me.txtSolve.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSolve.UseSystemPasswordChar = False
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
        Me.btnOK.Location = New System.Drawing.Point(12, 269)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(118, 22)
        Me.btnOK.TabIndex = 69
        Me.btnOK.Text = "Solve"
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
        Me.MetroButton1.Location = New System.Drawing.Point(259, 269)
        Me.MetroButton1.Name = "MetroButton1"
        Me.MetroButton1.Size = New System.Drawing.Size(58, 22)
        Me.MetroButton1.TabIndex = 70
        Me.MetroButton1.Text = "Refresh"
        '
        'frmCaptcha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(329, 299)
        Me.Controls.Add(Me.MetroButton1)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtSolve)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblMinimize)
        Me.Controls.Add(Me.lblClose)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlBlock)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCaptcha"
        Me.Text = ".NET Streamer - SRT ReSync"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents lblMinimize As System.Windows.Forms.Label
    Friend WithEvents lblClose As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtSolve As DotNetStreamer.MetroTextBox
    Friend WithEvents btnOK As DotNetStreamer.MetroButton
    Friend WithEvents MetroButton1 As DotNetStreamer.MetroButton
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetupVLC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetupVLC))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.lblTimeLeft = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.lblPercentage = New System.Windows.Forms.Label()
        Me.pbProgress = New DotNetStreamer.MetroProgressbarHorizontal()
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
        Me.pnlTitle.Size = New System.Drawing.Size(109, 53)
        Me.pnlTitle.TabIndex = 49
        '
        'lblStreamer
        '
        Me.lblStreamer.AutoSize = True
        Me.lblStreamer.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblStreamer.ForeColor = System.Drawing.Color.LightGray
        Me.lblStreamer.Location = New System.Drawing.Point(-4, 32)
        Me.lblStreamer.Name = "lblStreamer"
        Me.lblStreamer.Size = New System.Drawing.Size(111, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "INSTALLING VLC"
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
        'lblTimeLeft
        '
        Me.lblTimeLeft.AutoSize = True
        Me.lblTimeLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblTimeLeft.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblTimeLeft.ForeColor = System.Drawing.Color.LightGray
        Me.lblTimeLeft.Location = New System.Drawing.Point(13, 112)
        Me.lblTimeLeft.Name = "lblTimeLeft"
        Me.lblTimeLeft.Size = New System.Drawing.Size(146, 20)
        Me.lblTimeLeft.TabIndex = 29
        Me.lblTimeLeft.Text = "Time Left: Estimating..."
        '
        'lblSpeed
        '
        Me.lblSpeed.AutoSize = True
        Me.lblSpeed.BackColor = System.Drawing.Color.Transparent
        Me.lblSpeed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblSpeed.ForeColor = System.Drawing.Color.LightGray
        Me.lblSpeed.Location = New System.Drawing.Point(319, 112)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(91, 20)
        Me.lblSpeed.TabIndex = 51
        Me.lblSpeed.Text = "Speed: 0KB/S"
        '
        'lblPercentage
        '
        Me.lblPercentage.AutoSize = True
        Me.lblPercentage.BackColor = System.Drawing.Color.Transparent
        Me.lblPercentage.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblPercentage.ForeColor = System.Drawing.Color.LightGray
        Me.lblPercentage.Location = New System.Drawing.Point(380, 61)
        Me.lblPercentage.Name = "lblPercentage"
        Me.lblPercentage.Size = New System.Drawing.Size(29, 20)
        Me.lblPercentage.TabIndex = 52
        Me.lblPercentage.Text = "0%"
        '
        'pbProgress
        '
        Me.pbProgress.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pbProgress.Location = New System.Drawing.Point(12, 84)
        Me.pbProgress.Maximum = 100
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pbProgress.Size = New System.Drawing.Size(403, 23)
        Me.pbProgress.TabIndex = 50
        Me.pbProgress.Text = "MetroProgressbarHorizontal1"
        Me.pbProgress.Value = 0
        '
        'frmSetupVLC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(427, 141)
        Me.Controls.Add(Me.lblPercentage)
        Me.Controls.Add(Me.lblSpeed)
        Me.Controls.Add(Me.lblTimeLeft)
        Me.Controls.Add(Me.pbProgress)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlBlock)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSetupVLC"
        Me.Text = "Installing VLC..."
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents pbProgress As MetroProgressbarHorizontal
    Friend WithEvents lblTimeLeft As System.Windows.Forms.Label
    Friend WithEvents lblSpeed As System.Windows.Forms.Label
    Friend WithEvents lblPercentage As System.Windows.Forms.Label
End Class

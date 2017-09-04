<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClickOnce
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClickOnce))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.TBStatus = New System.Windows.Forms.Label()
        Me.TBSpeed = New System.Windows.Forms.Label()
        Me.TBProgress = New System.Windows.Forms.Label()
        Me.Progressbar1 = New DotNetStreamer.MetroProgressbarHorizontal()
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
        Me.lblStreamer.Size = New System.Drawing.Size(76, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "UPDATING"
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
        'TBStatus
        '
        Me.TBStatus.AutoSize = True
        Me.TBStatus.BackColor = System.Drawing.Color.Transparent
        Me.TBStatus.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.TBStatus.ForeColor = System.Drawing.Color.LightGray
        Me.TBStatus.Location = New System.Drawing.Point(10, 112)
        Me.TBStatus.Name = "TBStatus"
        Me.TBStatus.Size = New System.Drawing.Size(57, 20)
        Me.TBStatus.TabIndex = 51
        Me.TBStatus.Text = "Status: /"
        '
        'TBSpeed
        '
        Me.TBSpeed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBSpeed.AutoSize = True
        Me.TBSpeed.BackColor = System.Drawing.Color.Transparent
        Me.TBSpeed.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.TBSpeed.ForeColor = System.Drawing.Color.LightGray
        Me.TBSpeed.Location = New System.Drawing.Point(329, 112)
        Me.TBSpeed.Name = "TBSpeed"
        Me.TBSpeed.Size = New System.Drawing.Size(86, 20)
        Me.TBSpeed.TabIndex = 29
        Me.TBSpeed.Text = "0 MB / 0 MB"
        '
        'TBProgress
        '
        Me.TBProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBProgress.AutoSize = True
        Me.TBProgress.BackColor = System.Drawing.Color.Transparent
        Me.TBProgress.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.TBProgress.ForeColor = System.Drawing.Color.LightGray
        Me.TBProgress.Location = New System.Drawing.Point(375, 61)
        Me.TBProgress.Name = "TBProgress"
        Me.TBProgress.Size = New System.Drawing.Size(29, 20)
        Me.TBProgress.TabIndex = 52
        Me.TBProgress.Text = "0%"
        '
        'Progressbar1
        '
        Me.Progressbar1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Progressbar1.Location = New System.Drawing.Point(12, 84)
        Me.Progressbar1.Maximum = 100
        Me.Progressbar1.Name = "Progressbar1"
        Me.Progressbar1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Progressbar1.Size = New System.Drawing.Size(403, 23)
        Me.Progressbar1.TabIndex = 50
        Me.Progressbar1.Text = "MetroProgressbarHorizontal1"
        Me.Progressbar1.Value = 0
        '
        'frmClickOnce
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(427, 141)
        Me.Controls.Add(Me.TBProgress)
        Me.Controls.Add(Me.TBStatus)
        Me.Controls.Add(Me.TBSpeed)
        Me.Controls.Add(Me.Progressbar1)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlBlock)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmClickOnce"
        Me.Text = "Updating .."
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
    Friend WithEvents Progressbar1 As MetroProgressbarHorizontal
    Friend WithEvents TBStatus As System.Windows.Forms.Label
    Friend WithEvents TBSpeed As System.Windows.Forms.Label
    Friend WithEvents TBProgress As System.Windows.Forms.Label
End Class

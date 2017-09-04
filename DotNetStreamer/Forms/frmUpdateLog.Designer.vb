<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateLog))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblStreamer = New System.Windows.Forms.Label()
        Me.pcIcon = New System.Windows.Forms.PictureBox()
        Me.pnlBlock = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUpdatelog = New System.Windows.Forms.Label()
        Me.PnlButtons = New System.Windows.Forms.Panel()
        Me.btnAccept = New DotNetStreamer.MetroButton()
        Me.PnlUpdateLog = New System.Windows.Forms.Panel()
        Me.pnlTitle.SuspendLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlButtons.SuspendLayout()
        Me.PnlUpdateLog.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.Transparent
        Me.pnlTitle.Controls.Add(Me.lblStreamer)
        Me.pnlTitle.Controls.Add(Me.pcIcon)
        Me.pnlTitle.Location = New System.Drawing.Point(22, 12)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(86, 55)
        Me.pnlTitle.TabIndex = 51
        '
        'lblStreamer
        '
        Me.lblStreamer.AutoSize = True
        Me.lblStreamer.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.lblStreamer.ForeColor = System.Drawing.Color.LightGray
        Me.lblStreamer.Location = New System.Drawing.Point(-4, 32)
        Me.lblStreamer.Name = "lblStreamer"
        Me.lblStreamer.Size = New System.Drawing.Size(87, 20)
        Me.lblStreamer.TabIndex = 27
        Me.lblStreamer.Text = "UPDATELOG"
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
        Me.pnlBlock.TabIndex = 50
        Me.pnlBlock.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Light", 11.0!)
        Me.Label1.ForeColor = System.Drawing.Color.LightGray
        Me.Label1.Location = New System.Drawing.Point(51, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(228, 40)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "An update is available. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The following things have changed:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUpdatelog
        '
        Me.txtUpdatelog.BackColor = System.Drawing.Color.Transparent
        Me.txtUpdatelog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUpdatelog.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtUpdatelog.ForeColor = System.Drawing.Color.LightGray
        Me.txtUpdatelog.Location = New System.Drawing.Point(0, 0)
        Me.txtUpdatelog.Name = "txtUpdatelog"
        Me.txtUpdatelog.Size = New System.Drawing.Size(305, 158)
        Me.txtUpdatelog.TabIndex = 55
        '
        'PnlButtons
        '
        Me.PnlButtons.BackColor = System.Drawing.Color.Transparent
        Me.PnlButtons.Controls.Add(Me.btnAccept)
        Me.PnlButtons.Location = New System.Drawing.Point(43, 306)
        Me.PnlButtons.Name = "PnlButtons"
        Me.PnlButtons.Size = New System.Drawing.Size(247, 31)
        Me.PnlButtons.TabIndex = 58
        '
        'btnAccept
        '
        Me.btnAccept.BackColor = System.Drawing.Color.Transparent
        Me.btnAccept.BackColorDown = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAccept.BackColorNormal = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAccept.BackColorOver = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAccept.BorderColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAccept.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnAccept.ForeColor = System.Drawing.Color.White
        Me.btnAccept.Location = New System.Drawing.Point(3, 5)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(241, 22)
        Me.btnAccept.TabIndex = 57
        Me.btnAccept.Text = "CLOSE"
        '
        'PnlUpdateLog
        '
        Me.PnlUpdateLog.BackColor = System.Drawing.Color.Transparent
        Me.PnlUpdateLog.Controls.Add(Me.txtUpdatelog)
        Me.PnlUpdateLog.Location = New System.Drawing.Point(12, 135)
        Me.PnlUpdateLog.Name = "PnlUpdateLog"
        Me.PnlUpdateLog.Size = New System.Drawing.Size(305, 158)
        Me.PnlUpdateLog.TabIndex = 59
        '
        'frmUpdateLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(329, 347)
        Me.Controls.Add(Me.PnlUpdateLog)
        Me.Controls.Add(Me.PnlButtons)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlBlock)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUpdateLog"
        Me.Text = "Update"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        CType(Me.pcIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlButtons.ResumeLayout(False)
        Me.PnlUpdateLog.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblStreamer As System.Windows.Forms.Label
    Friend WithEvents pcIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBlock As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOK As MetroButton
    Friend WithEvents btnNO As MetroButton
    Friend WithEvents txtUpdatelog As System.Windows.Forms.Label
    Friend WithEvents btnAccept As DotNetStreamer.MetroButton
    Friend WithEvents PnlButtons As System.Windows.Forms.Panel
    Friend WithEvents PnlUpdateLog As System.Windows.Forms.Panel
End Class

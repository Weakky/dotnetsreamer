Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Public Class frmUpdateLog

#Region "GUI"
    Private Sub frmMain_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
        e.Graphics.Clear(Color.DimGray)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(2, 15, 15)), New Rectangle(1, 1, Width - 2, Height - 2))
        e.Graphics.SetClip(New Rectangle(1, 1, Width - 2, Height - 2))
        Dim path As New GraphicsPath() : path.AddEllipse(New Rectangle(-100, -100, Width + 198, Height + 198))
        Dim PGB As New PathGradientBrush(path)
        PGB.CenterColor = Color.FromArgb(4, 35, 35) : PGB.SurroundColors = {Color.FromArgb(2, 15, 15)}
        PGB.FocusScales = New Point(0.1F, 0.3F)
        e.Graphics.FillPath(PGB, path)
        e.Graphics.ResetClip()

        Dim LGB As New LinearGradientBrush(New Rectangle(1, 16, 13, 55), Color.FromArgb(42, 177, 80), Color.FromArgb(49, 149, 178), 90.0F)
        e.Graphics.FillRectangle(LGB, LGB.Rectangle)
    End Sub

#End Region
#Region "Movable form"
    Dim IsDraggingForm As Boolean = False
    Private MousePos As New System.Drawing.Point(0, 0)

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left Then
            IsDraggingForm = True
            MousePos = e.Location
        End If
    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = MouseButtons.Left Then IsDraggingForm = False
    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If IsDraggingForm Then
            Dim temp As Point = New Point(Me.Location + (e.Location - MousePos))
            Me.Location = temp
            temp = Nothing
        End If
    End Sub
#End Region


    Private Sub frmUpdateLog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim G As Graphics = Me.CreateGraphics()
        Dim UpdateLogSize As Size = G.MeasureString(txtUpdatelog.Text, txtUpdatelog.Font, txtUpdatelog.Width).ToSize

        If UpdateLogSize.Height > PnlUpdateLog.Height Then
            Dim HeightToAdd As Integer = UpdateLogSize.Height - PnlUpdateLog.Height
            PnlUpdateLog.Height += HeightToAdd
            PnlButtons.Location = New Point(PnlButtons.Location.X, PnlButtons.Location.Y + HeightToAdd)
            Height += HeightToAdd
        Else
            Dim HeightToRemove As Integer = PnlUpdateLog.Height - UpdateLogSize.Height
            PnlUpdateLog.Height -= HeightToRemove
            PnlButtons.Location = New Point(PnlButtons.Location.X, PnlButtons.Location.Y - HeightToRemove)
            Height -= HeightToRemove
        End If

        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        Me.TopMost = True
    End Sub

    Private Sub btnNO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Me.Close()
    End Sub
End Class
Public Class frmHint

      Public Event HintButtonClicked()
      Public WithEvents tmrTimeOut As New Timer With {.Enabled = False}
    Public WithEvents tmrAnimation As New Timer With {.Interval = 1}

      Public HintText As String, HintButtonText As String, TimeOutInSeconds As Integer
      Private DrawHeight As Integer = 0
    Private Expanding As Boolean = False

#Region "Mouse Handling"

    Enum MouseStates
        None = 0
        Over = 1
        Down = 2
    End Enum

    Private State As MouseStates = MouseStates.None

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseStates.Over
        PaintHint()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseStates.None
        PaintHint()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseStates.Down
        PaintHint()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseStates.Over
        PaintHint()
    End Sub

    Dim MouseLocation As Point
    Dim oldMouseLocation As Point = New Point(-1, -1)
    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        MouseLocation = e.Location

        Dim ButtonRectangle As New Rectangle(Width - 165, -29 + DrawHeight, 130, 24)
        Dim XRectangle As New Rectangle(Width - 27, -25 + DrawHeight, 16, 17)

        If ButtonRectangle.Contains(MouseLocation) <> ButtonRectangle.Contains(oldMouseLocation) Then
            PaintHint()
        ElseIf XRectangle.Contains(MouseLocation) <> XRectangle.Contains(oldMouseLocation) Then
            PaintHint()
        End If

        oldMouseLocation = MouseLocation
    End Sub

    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        MyBase.OnClick(e)

        Dim ButtonRectangle As New Rectangle(Width - 165, -29 + DrawHeight, 130, 24)
        Dim XRectangle As New Rectangle(Width - 27, -25 + DrawHeight, 16, 17)

        If ButtonRectangle.Contains(MouseLocation) Then
            HideHint()
            RaiseEvent HintButtonClicked()
        ElseIf XRectangle.Contains(MouseLocation) Then
            HideHint()
        End If
        showPreferences = False
    End Sub
#End Region

      Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            PaintHint()
      End Sub

      Public Sub ShowHint(_HintText As String, _HintButtonText As String, _TimeOutInSeconds As Integer)
            Expanding = True
            HintText = _HintText
        HintButtonText = _HintButtonText
            tmrAnimation.Start()
            TimeOutInSeconds = _TimeOutInSeconds
            If _TimeOutInSeconds > 0 Then tmrTimeOut.Interval = 1000 * _TimeOutInSeconds Else tmrTimeOut.Interval = 1000
      End Sub

    Private fontHint As New Font("Segoe UI Light", 9)
    Private fontCross As New Font("Marlett", 11)
    Private SF1 As New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center}
    Private SF2 As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}

      Public Sub PaintHint()
        Using B As New Bitmap(Width, Height)
            Using G As Graphics = Graphics.FromImage(B)
                        G.FillRectangle(New SolidBrush(Color.FromArgb(230, 30, 50, 50)), New Rectangle(0, 0, Width, DrawHeight))
                        G.DrawRectangle(Pens.Gray, New Rectangle(0, 0, Width - 1, DrawHeight - 1))

                G.DrawString(HintText, fontHint, Brushes.White, New Rectangle(9, DrawHeight - 23, Width - 180, 12), SF1)

                        If HintButtonText <> String.Empty Then
                            Dim ButtonRectangle As New Rectangle(Width - 165, -29 + DrawHeight, 130, 24)
                            G.FillRectangle(New SolidBrush(Color.FromArgb(60, Color.Black)), ButtonRectangle)
                            If ButtonRectangle.Contains(MouseLocation) And State = MouseStates.Over Then G.FillRectangle(New SolidBrush(Color.FromArgb(7, Color.White)), ButtonRectangle)
                            If ButtonRectangle.Contains(MouseLocation) And State = MouseStates.Down Then G.FillRectangle(New SolidBrush(Color.FromArgb(10, Color.Black)), ButtonRectangle)
                            G.DrawRectangle(New Pen(Color.FromArgb(50, Color.White)), ButtonRectangle)

                    G.DrawString(HintButtonText, fontHint, Brushes.White, ButtonRectangle, SF2)
                        End If

                        Dim XRectangle As New Rectangle(Width - 27, -25 + DrawHeight, 16, 17)
                        Dim XBrush As Brush = Brushes.LightGray
                        If XRectangle.Contains(MouseLocation) And State = MouseStates.Over Then XBrush = Brushes.WhiteSmoke
                        If XRectangle.Contains(MouseLocation) And State = MouseStates.Down Then XBrush = Brushes.Gray
                G.DrawString("r", fontCross, XBrush, New Point(Width - 28, -24 + DrawHeight))

                        UpdateLayeredForm(B)
                    End Using
                End Using
    End Sub

    Public Sub HideHint()
        Expanding = False
        tmrAnimation.Start()
    End Sub

      Private Sub tmrTimeOut_Tick(sender As Object, e As EventArgs) Handles tmrTimeOut.Tick
            tmrTimeOut.Stop()
            HideHint()
      End Sub

      Private Sub tmrAnimation_Tick(sender As Object, e As EventArgs) Handles tmrAnimation.Tick

            If Expanding Then
                  If DrawHeight < 33 Then
                DrawHeight += 2.5
                        PaintHint()
                  Else
                        tmrAnimation.Stop()
                        DrawHeight = 33
                        PaintHint()
                        If TimeOutInSeconds > 0 Then tmrTimeOut.Start()
                  End If
            Else
                  If DrawHeight > 0 Then
                DrawHeight -= 2.5
                        PaintHint()
            Else
                Visible = False
                Hide()
                tmrAnimation.Stop()
                DrawHeight = 0
                PaintHint()
                  End If
            End If

      End Sub

    Private Sub frmHint_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
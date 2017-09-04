Imports System.Runtime.InteropServices

Public Class frmExitVideoMode



#Region " Fields "

    Private CrossShown As Boolean = False
    Private Draw As Boolean = False

    Private WithEvents tmrAnimation As New Timer With {.Enabled = False, .Interval = 500}
    Private WithEvents tmrTimeOut As New Timer With {.Enabled = False, .Interval = 3000}

#End Region
#Region " Events "
    Public Event CrossClicked()
#End Region

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        Dim R = New Rectangle(Width - 25, 0, 40, 40)

        If R.Contains(e.Location) Then
            Cursor = Cursors.Hand
        End If
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)

        Dim R = New Rectangle(Width - 25, 0, 40, 40)

        If R.Contains(e.Location) Then
            RaiseEvent CrossClicked()
        End If
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
    End Sub

    Public Sub DrawExitButton()
        Me.Show(frmMain)
        tmrAnimation.Start()
        Draw = True
    End Sub

    Public Sub StopDrawingExitButton()
        tmrAnimation.Stop()
        tmrTimeOut.Stop()
        Me.Hide()
        Draw = False
        DrawExitButtonToVideo(Draw)
    End Sub

    Private Sub DrawButtonTemporarily()
        If IsMouseMovingOnVlcPlayer() Then
            tmrTimeOut.Stop()
            tmrAnimation.Stop()

            tmrAnimation.Start()
            tmrTimeOut.Start()
            DrawExitButtonToVideo(Draw)
        End If
    End Sub

    Private Sub tmrAnimation_Tick(sender As Object, e As EventArgs) Handles tmrAnimation.Tick
        Location = frmMain.PointToScreen(frmMain.pnl_VideoPlayback.Location)
        Width = frmMain.pnl_VideoPlayback.Width
        DrawButtonTemporarily()
    End Sub

    Private Sub tmrTimeOut_Tick(sender As Object, e As EventArgs) Handles tmrTimeOut.Tick
        tmrAnimation.Stop()
        tmrAnimation.Start()
        tmrTimeOut.Stop()
        DrawExitButtonToVideo(False)
    End Sub

    Private Sub DrawExitButtonToVideo(_Draw As Boolean)

            Using B As New Bitmap(Width, Height)
                Using G As Graphics = Graphics.FromImage(B)

                    If _Draw AndAlso Not frmHint.Visible Then
                        Dim TextBounds As Rectangle = Rectangle.Round(B.GetBounds(GraphicsUnit.Pixel))
                        Dim SF As New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Far}

                        G.DrawString("r", New Font("Marlett", 16.0F), Brushes.White, TextBounds, SF)

                    SF.Dispose()
                    TextBounds = Nothing
                    Else
                        G.Clear(Color.Transparent)
                    End If

                    UpdateLayeredForm(B)
                End Using
            End Using
    End Sub

    Private oldMouseLoc As Point = Point.Empty
    Private Function IsMouseMovingOnVlcPlayer() As Boolean
        Dim R As Rectangle = frmMain.Bounds
        Dim pcin As New CURSORINFO()
        pcin.cbSize = Marshal.SizeOf(pcin)

        If GetCursorInfo(pcin) Then
            Dim mouseLocation As Point = New Point(pcin.ptScreenPos.x, pcin.ptScreenPos.y)
            If R.Contains(New Point(pcin.ptScreenPos.x, pcin.ptScreenPos.y)) AndAlso mouseLocation <> oldMouseLoc Then
                Debug.WriteLine("Old:{0} - Current:{1}", oldMouseLoc, mouseLocation)
                oldMouseLoc = mouseLocation
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

  End Class
Imports System.Runtime.InteropServices

Public Class clsLayeredForm
      Inherits Form

#Region "Native Methods and Structures"

      Const WS_EX_LAYERED As Int32 = &H80000
      Const HTCAPTION As Int32 = &H2
      Const WM_NCHITTEST As Int32 = &H84
      Const ULW_ALPHA As Int32 = &H2
      Const AC_SRC_OVER As Byte = &H0
      Const AC_SRC_ALPHA As Byte = &H1

      <StructLayout(LayoutKind.Sequential)> _
      Private Structure Point
            Public x As Int32
            Public y As Int32

            Public Sub New(x As Int32, y As Int32)
                  Me.x = x
                  Me.y = y
            End Sub
      End Structure

      <StructLayout(LayoutKind.Sequential)> _
      Private Shadows Structure Size
            Public cx As Int32
            Public cy As Int32

            Public Sub New(cx As Int32, cy As Int32)
                  Me.cx = cx
                  Me.cy = cy
            End Sub
      End Structure

      <StructLayout(LayoutKind.Sequential, Pack:=1)> _
      Private Structure ARGB
            Public Blue As Byte
            Public Green As Byte
            Public Red As Byte
            Public Alpha As Byte
      End Structure

      <StructLayout(LayoutKind.Sequential, Pack:=1)> _
      Private Structure BLENDFUNCTION
            Public BlendOp As Byte
            Public BlendFlags As Byte
            Public SourceConstantAlpha As Byte
            Public AlphaFormat As Byte
      End Structure

      <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
      Private Shared Function UpdateLayeredWindow(hwnd As IntPtr, hdcDst As IntPtr, ByRef pptDst As Point, ByRef psize As Size, hdcSrc As IntPtr, ByRef pprSrc As Point, _
      crKey As Int32, ByRef pblend As BLENDFUNCTION, dwFlags As Int32) As <MarshalAs(UnmanagedType.Bool)> Boolean
      End Function

      <DllImport("gdi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
      Private Shared Function CreateCompatibleDC(hDC As IntPtr) As IntPtr
      End Function

      <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
      Private Shared Function GetDC(hWnd As IntPtr) As IntPtr
      End Function

      <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
      Private Shared Function ReleaseDC(hWnd As IntPtr, hDC As IntPtr) As Integer
      End Function

      <DllImport("gdi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
      Private Shared Function DeleteDC(hdc As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
      End Function

      <DllImport("gdi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
      Private Shared Function SelectObject(hDC As IntPtr, hObject As IntPtr) As IntPtr
      End Function

      <DllImport("gdi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
      Private Shared Function DeleteObject(hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
      End Function

#End Region

      Protected Overrides ReadOnly Property CreateParams As CreateParams
            Get
                  Dim createP As CreateParams = MyBase.CreateParams
                  If Not DesignMode Then createP.ExStyle = createP.ExStyle Or WS_EX_LAYERED

                  Return createP
            End Get
      End Property

      Public Sub UpdateLayeredForm(ByVal B As Bitmap)
            Dim screenDc As IntPtr = GetDC(IntPtr.Zero)
            Dim memDc As IntPtr = CreateCompatibleDC(screenDc)
            Dim hBitmap As IntPtr = IntPtr.Zero
            Dim hOldBitmap As IntPtr = IntPtr.Zero

            Try
                  hBitmap = B.GetHbitmap(Color.FromArgb(0))
                  hOldBitmap = SelectObject(memDc, hBitmap)

                  Dim NewSize As New Size(B.Width, B.Height)
                  Dim blend As BLENDFUNCTION = New BLENDFUNCTION()
                  blend.BlendOp = AC_SRC_OVER
                  blend.BlendFlags = 0
                  blend.SourceConstantAlpha = 255
                  blend.AlphaFormat = AC_SRC_ALPHA

                  UpdateLayeredWindow(Handle, screenDc, New Point(Location.X, Location.Y), NewSize, memDc, New Point(0, 0), 0, blend, ULW_ALPHA)

            Catch ex As Exception

            Finally
                  ReleaseDC(IntPtr.Zero, screenDc)
                  If hBitmap <> IntPtr.Zero Then
                        SelectObject(memDc, hOldBitmap)
                        DeleteObject(hBitmap)
                  End If
                  DeleteDC(memDc)
            End Try

      End Sub
End Class

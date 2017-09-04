Imports System.Runtime.InteropServices

Public Class MouseHook
    Private Declare Function SetWindowsHookEx Lib "user32" Alias "SetWindowsHookExA" (ByVal idHook As Integer, ByVal lpfn As MouseProcDelegate, ByVal hmod As Integer, ByVal dwThreadId As Integer) As Integer
    Private Declare Function CallNextHookEx Lib "user32" (ByVal hHook As Integer, ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As MSLLHOOKSTRUCT) As Integer
    Private Declare Function UnhookWindowsHookEx Lib "user32" (ByVal hHook As Integer) As Integer
    Private Delegate Function MouseProcDelegate(ByVal nCode As Integer, ByVal wParam As Integer, ByRef lParam As MSLLHOOKSTRUCT) As Integer

    Private Structure MSLLHOOKSTRUCT
        Public pt As Point
        Public mouseData As Integer
        Public flags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Structure

    Public Enum Wheel_Direction
        WheelUp
        WheelDown
    End Enum

    Private Const HC_ACTION As Integer = 0
    Private Const WH_MOUSE_LL As Integer = 14
    Private Const WM_MOUSEMOVE As Integer = &H200
    Private Const WM_LBUTTONDOWN As Integer = &H201
    Private Const WM_LBUTTONUP As Integer = &H202
    Private Const WM_LBUTTONDBLCLK As Integer = &H203
    Private Const WM_RBUTTONDOWN As Integer = &H204
    Private Const WM_RBUTTONUP As Integer = &H205
    Private Const WM_RBUTTONDBLCLK As Integer = &H206
    Private Const WM_MBUTTONDOWN As Integer = &H207
    Private Const WM_MBUTTONUP As Integer = &H208
    Private Const WM_MBUTTONDBLCLK As Integer = &H209
    Private Const WM_MOUSEWHEEL As Integer = &H20A

    Private MouseHook As Integer
    Private MouseHookDelegate As MouseProcDelegate

    Public Event Mouse_Move(ByVal ptLocat As Point)
    Public Event Mouse_Left_Down(ByVal ptLocat As Point)
    Public Event Mouse_Left_Up(ByVal ptLocat As Point)
    Public Event Mouse_Left_DoubleClick(ByVal ptLocat As Point)
    Public Event Mouse_Right_Down(ByVal ptLocat As Point)
    Public Event Mouse_Right_Up(ByVal ptLocat As Point)
    Public Event Mouse_Right_DoubleClick(ByVal ptLocat As Point)
    Public Event Mouse_Middle_Down(ByVal ptLocat As Point)
    Public Event Mouse_Middle_Up(ByVal ptLocat As Point)
    Public Event Mouse_Middle_DoubleClick(ByVal ptLocat As Point)
    Public Event Mouse_Wheel(ByVal ptLocat As Point, ByVal Direction As Wheel_Direction)

    Public Sub New()
        MouseHookDelegate = New MouseProcDelegate(AddressOf MouseProc)
        MouseHook = SetWindowsHookEx(WH_MOUSE_LL, MouseHookDelegate, System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly.GetModules()(0)).ToInt32, 0)
    End Sub

    Private Function MouseProc(ByVal nCode As Integer, ByVal wParam As Integer, ByRef lParam As MSLLHOOKSTRUCT) As Integer
        If (nCode = HC_ACTION) Then
            Select Case wParam
                Case WM_MOUSEMOVE
                    RaiseEvent Mouse_Move(lParam.pt)
                Case WM_LBUTTONDOWN
                    RaiseEvent Mouse_Left_Down(lParam.pt)
                Case WM_LBUTTONUP
                    RaiseEvent Mouse_Left_Up(lParam.pt)
                Case WM_LBUTTONDBLCLK
                    RaiseEvent Mouse_Left_DoubleClick(lParam.pt)
                Case WM_RBUTTONDOWN
                    RaiseEvent Mouse_Right_Down(lParam.pt)
                Case WM_RBUTTONUP
                    RaiseEvent Mouse_Right_Up(lParam.pt)
                Case WM_RBUTTONDBLCLK
                    RaiseEvent Mouse_Right_DoubleClick(lParam.pt)
                Case WM_MBUTTONDOWN
                    RaiseEvent Mouse_Middle_Down(lParam.pt)
                Case WM_MBUTTONUP
                    RaiseEvent Mouse_Middle_Up(lParam.pt)
                Case WM_MBUTTONDBLCLK
                    RaiseEvent Mouse_Middle_DoubleClick(lParam.pt)
                Case WM_MOUSEWHEEL
                    Dim wDirection As Wheel_Direction
                    If lParam.mouseData < 0 Then
                        wDirection = Wheel_Direction.WheelDown
                    Else
                        wDirection = Wheel_Direction.WheelUp
                    End If
                    RaiseEvent Mouse_Wheel(lParam.pt, wDirection)
            End Select
        End If
        Return CallNextHookEx(MouseHook, nCode, wParam, lParam)
    End Function

    Public Sub DestroyHook()
        Finalize()
    End Sub

    Protected Overrides Sub Finalize()
        UnhookWindowsHookEx(MouseHook)
        MyBase.Finalize()
    End Sub
End Class

Public Class MouseDetector
    Public Event MouseLeftButtonClick(ByVal sender As Object, ByVal e As MouseEventArgs)
    Public Event MouseRightButtonClick(ByVal sender As Object, ByVal e As MouseEventArgs)
    Private Delegate Function MouseHookCallback(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
    Private MouseHookCallbackDelegate As MouseHookCallback
    Private MouseHookID As Integer
    Public Sub New()
        If MouseHookID = 0 Then
            MouseHookCallbackDelegate = AddressOf MouseHookProc
            MouseHookID = SetWindowsHookEx(CInt(14), MouseHookCallbackDelegate, Process.GetCurrentProcess().MainModule.BaseAddress, 0)
            If MouseHookID = 0 Then
                'error
            End If
        End If
    End Sub
    Public Sub Dispose()
        If Not MouseHookID = -1 Then
            UnhookWindowsHookEx(MouseHookID)
            MouseHookCallbackDelegate = Nothing
        End If
        MouseHookID = -1
    End Sub
    Private Enum MouseMessages
        WM_LeftButtonDown = 513
        WM_LeftButtonUp = 514
        WM_LeftDblClick = 515
        WM_RightButtonDown = 516
        WM_RightButtonUp = 517
        WM_RightDblClick = 518
        WM_MOUSEMOVE = &H200
    End Enum
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure Point
        Public x As Integer
        Public y As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure MouseHookStruct
        Public pt As Point
        Public hwnd As Integer
        Public wHitTestCode As Integer
        Public dwExtraInfo As Integer
    End Structure
    <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function CallNextHookEx( _
    ByVal idHook As Integer, _
    ByVal nCode As Integer, _
    ByVal wParam As IntPtr, _
    ByVal lParam As IntPtr) As Integer
    End Function
    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)> _
    Private Shared Function SetWindowsHookEx _
    (ByVal idHook As Integer, ByVal HookProc As MouseHookCallback, _
    ByVal hInstance As IntPtr, ByVal wParam As Integer) As Integer
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)> _
    Private Shared Function UnhookWindowsHookEx(ByVal idHook As Integer) As Integer
    End Function

    Private Function MouseHookProc(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
        If nCode < 0 Then
            Return CallNextHookEx(MouseHookID, nCode, wParam, lParam)
        End If
        Dim MouseData As MouseHookStruct = Marshal.PtrToStructure(lParam, GetType(MouseHookStruct))
        Select Case wParam
            Case MouseMessages.WM_LeftButtonUp
                RaiseEvent MouseLeftButtonClick(Nothing, New MouseEventArgs(MouseButtons.Left, 1, MouseData.pt.x, MouseData.pt.y, 0))
            Case MouseMessages.WM_RightButtonUp
                RaiseEvent MouseRightButtonClick(Nothing, New MouseEventArgs(MouseButtons.Right, 1, MouseData.pt.x, MouseData.pt.y, 0))
            Case MouseMessages.WM_MOUSEMOVE
                'Debug.WriteLine("CA BOUGE PUTAAAAAIN")
        End Select
        Return CallNextHookEx(MouseHookID, nCode, wParam, lParam)
    End Function
End Class


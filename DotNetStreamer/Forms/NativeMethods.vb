Imports System.Runtime.InteropServices

Module NativeMethods

    <DllImport("user32.dll", EntryPoint:="GetCursorInfo")> _
    Public Function GetCursorInfo(ByRef pci As CURSORINFO) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure CURSORINFO
        Public cbSize As Int32
        Public flags As Int32
        Public hCursor As IntPtr
        Public ptScreenPos As POINTAPI
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure POINTAPI
        Public x As Int32
        Public y As Int32
    End Structure


End Module

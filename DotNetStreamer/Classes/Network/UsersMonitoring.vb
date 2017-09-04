Option Strict On

Imports System.Globalization

Public Class UserOnline

    Private Const URL As String = "http://www.dotnetstreamer.com/online.php"
    Public OnlineUsers As Integer = 0
    Public Event OnlineUsersChanged(ByVal sender As System.Object, ByVal e As UserOnlineArgs)

    Public Sub New()
        Dim t As System.Timers.Timer = New System.Timers.Timer(5 * 60 * 1000)
        AddHandler t.Elapsed, AddressOf UpdaterUsersOnline
        t.Start()
    End Sub

    Private Sub UpdaterUsersOnline(sender As Object, e As Timers.ElapsedEventArgs)
        Dim n As Integer
        Dim s As String = String.Empty
        Try 'Just to make sure our whole application doesn't crash if there'll be any problems with the server
            s = New System.Net.WebClient().DownloadString(URL & "?check&online")
        Catch ex As System.Net.WebException
            Debug.WriteLine(ex.Message)
        End Try
        If Integer.TryParse(s, n) AndAlso Not n = OnlineUsers Then
            RaiseEvent OnlineUsersChanged(Me, New UserOnlineArgs(n))
            OnlineUsers = n
        End If
    End Sub

    Public Sub AddPerformedUpdate()
        Try
            Dim R As String = New System.Net.WebClient().DownloadString(URL & "?update=" & RegionInfo.CurrentRegion.EnglishName)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub
    Public Function GetTotalUpdates() As String
        Dim Response As String = String.Empty
        Try
            Response = New System.Net.WebClient().DownloadString(URL & "?cup")
        Catch ex As Exception
            Debug.WriteLine(ex.Message)

        End Try
        Return Response
    End Function
    Public Function GetTotalUsersOnline() As String
        Dim Response As String = String.Empty
        Try
            Response = New System.Net.WebClient().DownloadString(URL & "?c")
        Catch ex As Exception
            Debug.WriteLine(ex.Message)

        End Try
        Return Response
    End Function

End Class

Public Class UserOnlineArgs
    Public NewValue As Integer
    Public Sub New(ByVal i As Integer)
        NewValue = i
    End Sub
End Class
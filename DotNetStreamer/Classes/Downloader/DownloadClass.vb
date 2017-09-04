Imports System.Net
Imports System.ComponentModel

Public Class DownloadClass

#Region " Properties and Variables "

    Public Property Downloading As Boolean = False
    Public Data As New FileData
    Private SpeedCalculate As New Stopwatch
    Private WithEvents WC As New WebClient

#End Region

#Region " Events "

    ''' <summary>
    ''' This event is raised whenever a file just started to download.
    ''' </summary>
    ''' <param name="file">The Name of the file that just started to download.</param>
    ''' <remarks></remarks>
    Public Event DownloadStarted(ByVal file As FileData)

    ''' <summary>
    ''' This event is raised whenever the progress of a file has changed.
    ''' </summary>
    ''' <param name="file">The Name of the file being downloaded.</param>
    ''' <param name="speed">The speed of the file (as a string).</param>
    ''' <param name="percent">The percentage of the file downloaded.</param>
    ''' <remarks></remarks>
    Public Event ProgressChanged(ByVal file As FileData, ByVal speed As String, ByVal percent As Integer)

    ''' <summary>
    ''' This event is raised when the file is finished downloading.
    ''' </summary>
    ''' <param name="file">The name of the file.</param>
    ''' <remarks></remarks>
    Public Event ProgressFinished(ByVal file As FileData)

    ''' <summary>
    ''' This event is raised when there was an error somewhere in the queue.
    ''' </summary>
    ''' <param name="err">The System.Exception that was caught.</param>
    ''' <remarks></remarks>
    Public Event FileError(ByVal file As FileData, ByVal err As System.Exception)

#End Region

#Region " Subs "

    ''' <summary>
    ''' Initialize the Download Class with the new Data.
    ''' </summary>
    ''' <param name="FD">The File Data to download.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal FD As FileData)
        Data = FD
        SpeedCalculate = New Stopwatch
    End Sub

    ''' <summary>
    ''' Initialize the Download Class with the new Data
    ''' </summary>
    ''' <param name="URL">The URL to download from (Must be a DIRECT URL!)</param>
    ''' <param name="SaveLocation">The Absolute Location to save the file to.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal URL As String, ByVal SaveLocation As String)
        Data = New FileData With {.URL = URL, .SaveTo = SaveLocation}
        SpeedCalculate = New Stopwatch
    End Sub

    ''' <summary>
    ''' Start the Download.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Start()
        If Not Downloading Then
            WC.DownloadFileAsync(New Uri(Data.URL), Data.SaveTo) ': Data.Downloading = True
            RaiseEvent DownloadStarted(Data)
            SpeedCalculate.Reset()
            SpeedCalculate.Start()
            Downloading = True
        End If
    End Sub

    ''' <summary>
    ''' Abort the Download. This will throw an exception.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Abort()
        If Downloading Then
            WC.CancelAsync()
            SpeedCalculate.Stop()
            Downloading = False
        End If
    End Sub

    ''' <summary>
    ''' Disposes of the Download Class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose()
        WC.Dispose()
        SpeedCalculate = Nothing
        Data = New FileData
        Downloading = False
    End Sub

#End Region

#Region " WebClient Handlers "

    Private Sub WC_DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles WC.DownloadProgressChanged
        Dim CurBytes As Long = e.BytesReceived
        Dim StreamEnd As Long = e.TotalBytesToReceive
        If SpeedCalculate.Elapsed.TotalSeconds > 0 Then
            Dim Speed As Integer = CInt((CurBytes / SpeedCalculate.Elapsed.TotalSeconds))
            Dim NewSpeed As Integer = Speed
            Dim count As Integer = 0I
            Dim ending As String = String.Empty
            Do Until NewSpeed <= 1024
                NewSpeed = CInt(NewSpeed / 1024)
                count += 1
            Loop
            Select Case count
                Case 0
                    ending = "Bytes/sec"
                Case 1
                    ending = "Kb/sec"
                Case 2
                    ending = "Mb/sec"
                Case 3
                    ending = "Gb/sec"
            End Select
            RaiseEvent ProgressChanged(Data, String.Format("{0}{1}", NewSpeed, ending), e.ProgressPercentage)
        End If
    End Sub

    Private Sub WC_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles WC.DownloadFileCompleted
        If e.Error IsNot Nothing Then
            RaiseEvent FileError(Data, e.Error)
        Else
            RaiseEvent ProgressFinished(Data)
        End If
        Downloading = False
        Try
            SpeedCalculate.Stop()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " Operators "

    ''' <summary>
    ''' Compares two DownloadClass Structures to see if they are the same.
    ''' </summary>
    ''' <param name="DC1">One class to compare.</param>
    ''' <param name="DC2">The other class to compare.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Operator =(ByVal DC1 As DownloadClass, ByVal DC2 As DownloadClass) As Boolean
        Return (DC1.Data.FileName = DC2.Data.FileName) AndAlso (DC1.Data.SaveTo = DC2.Data.SaveTo)
    End Operator

    ''' <summary>
    ''' Compares two DownloadClass Structures to see if they are NOT the same.
    ''' </summary>
    ''' <param name="DC1">One class to compare.</param>
    ''' <param name="DC2">The other class to compare.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Operator <>(ByVal DC1 As DownloadClass, ByVal DC2 As DownloadClass) As Boolean
        Return (DC1.Data.FileName <> DC2.Data.FileName) AndAlso (DC1.Data.SaveTo <> DC2.Data.SaveTo)
    End Operator

#End Region

End Class
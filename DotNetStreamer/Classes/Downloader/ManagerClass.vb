Public Class ManagerClass

#Region " Properties and Variables "
    Private _CurrentDownloads As Integer = 0I
    Private _MaxDownloads As Integer = 1I
    ''' <summary>
    ''' Get or Set the number of Maximum Downloads.
    ''' This number MUST BE GREATER THAN ZERO.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaxDownloads As Integer
        Get
            Return _MaxDownloads
        End Get
        Set(ByVal value As Integer)
            If value <= 0 Then
                Throw New ArgumentException(String.Format("The Maxiumum Downloads must be greater than or equal to zero ({0} was the given value)", value.ToString))
            ElseIf value <> _MaxDownloads Then
                _MaxDownloads = value
                Call Me.ReduceDownloads()
            End If
        End Set
    End Property
    ''' <summary>
    ''' Returns the number of currently active downloads.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentDownloads As Integer
        Get
            Return _CurrentDownloads
        End Get
    End Property
    ''' <summary>
    ''' If True, the ManagerClass will automatically start the items that are added
    ''' if they can be started (if the Maximum Downloads has room for one more).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutoDownload As Boolean = False
    ''' <summary>
    ''' Returns the current DownloadList object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentDownloadList As List(Of DownloadClass)
        Get
            Return DownloadList
        End Get
    End Property
    Private DownloadList As New List(Of DownloadClass)

#End Region

#Region " Subs and Functions "

    ''' <summary>
    ''' Reruns the Download List to start downloads IF the max downloads hasn't been reached or if the item hasn't been started.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RunList()
        SyncLock DownloadList 'Lock the List
            For i As Integer = DownloadList.Count - 1 To 0 Step -1 'Setup a loop
                If Not DownloadList(i).Downloading AndAlso CurrentDownloads < MaxDownloads Then
                    DownloadList(i).Start()
                    _CurrentDownloads += 1
                End If
            Next
        End SyncLock
    End Sub

    ''' <summary>
    ''' Add a new item to the Download List.
    ''' </summary>
    ''' <param name="FD">The new FileData to add.</param>
    ''' <remarks></remarks>
    Public Sub Add(ByVal FD As FileData)
        SyncLock DownloadList
            Dim DC As New DownloadClass(FD)
            If Not CheckForItemInList(FD) Then
                DownloadList.Add(DC)
                AddHandler DC.FileError, AddressOf FileErrorSub
                AddHandler DC.ProgressChanged, AddressOf ProgressChangedSub
                AddHandler DC.ProgressFinished, AddressOf ProgressFinishedSub
                AddHandler DC.DownloadStarted, AddressOf DownloadStartedSub
                If AutoDownload Then RunList()
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' Remove an item from the Download List.
    ''' </summary>
    ''' <param name="FileName">The Filename to remove.</param>
    ''' <remarks></remarks>
    Public Sub Remove(ByVal FileName As String)
        SyncLock DownloadList
            For i As Integer = DownloadList.Count - 1 To 0 Step -1
                If DownloadList(i).Data.FileName = FileName Then
                    If DownloadList(i).Downloading Then
                        DownloadList(i).Abort()
                        _CurrentDownloads -= 1
                    End If
                    DownloadList.RemoveAt(i)
                End If
            Next
        End SyncLock
    End Sub

    ''' <summary>
    ''' Check for an existing item in the queue list.
    ''' </summary>
    ''' <param name="FD">The FileData to use to compare to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckForItemInList(ByVal FD As FileData) As Boolean
        SyncLock DownloadList
            For i As Integer = DownloadList.Count - 1 To 0 Step -1
                If DownloadList(i).Data.URL = FD.URL Then
                    Return True
                End If
            Next
            Return False
        End SyncLock
    End Function

    ''' <summary>
    ''' Internal sub used to stop downloads if necessary.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReduceDownloads()
        'If our Maximum allowed downloads are less than
        'the currently running downloads...
        If MaxDownloads < CurrentDownloads Then
            SyncLock DownloadList 'lock the download list.
                Dim i As Integer = DownloadList.Count - 1 'Get the current count.
                'Setup a loop to run while Current downloads are greater than Max Downloads and 
                'while i is greater than or equal to zero.
                Do While (CurrentDownloads > MaxDownloads) AndAlso (i >= 0)
                    If DownloadList(i).Downloading Then 'If downloading
                        DownloadList(i).Abort() 'Abort
                        _CurrentDownloads -= 1 'Remove one active download
                        i -= 1 'reduce the count by one.
                    End If
                Loop
            End SyncLock
        End If
    End Sub

#End Region

#Region " Property Handles "


    Public Event DownloadStarted(ByVal sender As Object, ByVal e As FileData)
    Private Sub DownloadStartedSub(ByVal file As FileData)
        RaiseEvent DownloadStarted(Me, file)
    End Sub

    ''' <summary>
    ''' This event is raised whenever the progress of a file has changed.
    ''' </summary>
    ''' <param name="sender">The Object that raised the event.</param>
    ''' <param name="e">The ProgressEventArgs Structure.</param>
    ''' <remarks></remarks>
    Public Event ProgressChanged(ByVal sender As Object, ByVal e As ProgressEventArgs)
    Private Sub ProgressChangedSub(ByVal file As FileData, ByVal speed As String, ByVal percent As Integer)
        Me.OnProgressChange(New ProgressEventArgs With {.File = file, .Percentage = percent, .Speed = speed})
    End Sub

    ''' <summary>
    ''' This event is raised when the file is finished downloading.
    ''' </summary>
    ''' <param name="sender">The Object that raised the event.</param>
    ''' <param name="e">The File Completion Structure</param>
    ''' <remarks></remarks>
    Public Event ProgressFinished(ByVal sender As Object, ByVal e As FileCompletionArgs)
    Private Sub ProgressFinishedSub(ByVal file As FileData)
        Dim DC As DownloadClass = FindDownloader(file.FileName)
        Call Remove(file.FileName)

        RemoveHandler DC.ProgressChanged, AddressOf ProgressChangedSub
        RemoveHandler DC.FileError, AddressOf FileErrorSub
        RemoveHandler DC.ProgressFinished, AddressOf ProgressFinishedSub

        Me.OnFileCompletion(New FileCompletionArgs With {.File = file})

        DC.Dispose()
        _CurrentDownloads -= 1

        Call RunList()
    End Sub

    ''' <summary>
    ''' This event is raised when there was an error somewhere in the queue.
    ''' </summary>
    ''' <param name="sender">The Object that raised the event.</param>
    ''' <param name="e">The structure that contains the information.</param>
    ''' <remarks></remarks>
    Public Event FileError(ByVal sender As Object, ByVal e As DownloadErrorArgs)
    Private Sub FileErrorSub(ByVal file As FileData, ByVal err As System.Exception)
        Me.OnFileError(New DownloadErrorArgs With {.File = file, .Exception = err})
    End Sub

    ''' <summary>
    ''' Finds the specific DownloadClass and exposes it.
    ''' </summary>
    ''' <param name="filename">The Filename that the specific DownloadClass is downloading.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FindDownloader(ByVal filename As String) As DownloadClass
        SyncLock DownloadList
            For i As Integer = DownloadList.Count - 1 To 0 Step -1
                If DownloadList(i).Data.FileName = filename Then Return DownloadList(i)
            Next
        End SyncLock
        Return Nothing
    End Function

    ''' <summary>
    ''' Sub used to handle progress changes and raising the event that goes with it.
    ''' </summary>
    ''' <param name="Data">The Progress Event Args Structure.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub OnProgressChange(ByVal Data As ProgressEventArgs)
        RaiseEvent ProgressChanged(Me, Data)
    End Sub

    ''' <summary>
    ''' Called whenever there is an error.
    ''' </summary>
    ''' <param name="Data">The Error Structure.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub OnFileError(ByVal Data As DownloadErrorArgs)
        RaiseEvent FileError(Me, Data)
    End Sub

    ''' <summary>
    ''' Called when a file is finished.
    ''' </summary>
    ''' <param name="Data">The FileCompletionArgs Structure</param>
    ''' <remarks></remarks>
    Protected Overridable Sub OnFileCompletion(ByVal Data As FileCompletionArgs)
        RaiseEvent ProgressFinished(Me, Data)
    End Sub

#End Region

#Region " Structures "
    Public Structure ProgressEventArgs
        ''' <summary>
        ''' The filename of the item being updated.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property File As FileData
        ''' <summary>
        ''' The current speed the item is downloading at.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Speed As String
        ''' <summary>
        ''' The current percent completion of the item.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Percentage As Integer


    End Structure
    Public Structure DownloadErrorArgs
        ''' <summary>
        ''' The filename that had an error.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property File As FileData
        ''' <summary>
        ''' The exception that was raised.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Exception As Exception
    End Structure
    Public Structure FileCompletionArgs
        ''' <summary>
        ''' The name of the file that was completed.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property File As FileData
    End Structure
#End Region

End Class
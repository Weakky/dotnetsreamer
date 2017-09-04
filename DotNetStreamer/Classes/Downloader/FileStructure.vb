Public Structure FileData

#Region " Variables "

    Private DURL As String
    Private DDownloadLocation As String
    Private dFileName As String

#End Region

#Region " Properties "

    Private _Ep As DownloadManager.Episode
    Public Property Episode As DownloadManager.Episode
        Get
            Return _Ep
        End Get
        Set(value As DownloadManager.Episode)
            _Ep = value
        End Set
    End Property

    'Private _Downloading As Boolean
    'Public Property Downloading As Boolean
    '    Get
    '        Return _Downloading
    '    End Get
    '    Set(value As Boolean)
    '        _Downloading = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Get or Set the URL of the Current Download Item.
    ''' </summary>
    ''' <value>The URL to download from.</value>
    ''' <returns>Returns the current URL of the file.</returns>
    ''' <remarks></remarks>
    Public Property URL() As String
        Get
            Return DURL
        End Get
        Set(ByVal value As String)
            DURL = value
        End Set
    End Property

    ''' <summary>
    ''' Get or Set the download location of the item. This is the absolute file location, including the filename.
    ''' </summary>
    ''' <value>The new location to save to (including filename)</value>
    ''' <returns>Returns the direct path to save to (including filename)</returns>
    ''' <remarks></remarks>
    Public Property SaveTo() As String
        Get
            Return DDownloadLocation
        End Get
        Set(ByVal value As String)
            If IO.Directory.Exists(value.Replace(IO.Path.GetFileName(value), String.Empty)) Then
                DDownloadLocation = value
                dFileName = Split(IO.Path.GetFileNameWithoutExtension(DDownloadLocation), " - ")(0)
            Else
                Throw New IO.DirectoryNotFoundException("You need to provide a valid directory!")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns only the filename of the item to download. (this is pulled from the Filepath)
    ''' </summary>
    ''' <value>This is Readonly, you cannot set the FileName.</value>
    ''' <returns>Returns the FileName.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileName() As String
        Get
            Return dFileName
        End Get
    End Property

    ''' <summary>
    ''' Returns the FilePath of the File to be downloaded, excluding the filename.
    ''' </summary>
    ''' <value>ReadOnly Property, cannot set the value.</value>
    ''' <returns>Returns the FilePath of the downloading file.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FilePath() As String
        Get
            Return DDownloadLocation.Replace(dFileName, String.Empty).Trim("\"c)
        End Get
    End Property

#End Region
    
End Structure
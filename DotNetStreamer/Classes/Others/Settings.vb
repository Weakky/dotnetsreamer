Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO.Compression

Public Class Settings

    Public Shared Property Data As New DataSettings 'Contains all your settings
    Private Shared BaseFolderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DotNetStreamer")
    Private Shared SettingsPath As String = Path.Combine(BaseFolderPath, "settings.bin")

    <Serializable> _
    Public Class DataSettings
        Public Property FolderVLCPath As String = String.Empty
        Public Property SubFont As New Font("Segoe UI Light", 11)
        Public Property SubColor As Color = Color.White
        Public Property ShowHint As Boolean = True
        Public Property AutoDownloadSubtitle As Boolean = False
        Public Property Language As String = "None"
        Public Property Host As String = "Automatic"
        Public Property VideoDatabase As String = "Primewire.ag"
        Public Property NetworkCachingTime As Integer = 30
        Public Property FavoriteList As New List(Of SeriesView.ItemFavorite)
        Public Property IsFirstRun As Boolean = True
        Public Property AlreadyWatchedEpisode As New List(Of SeriesClass.Episode)
        Public Property PictureInPictureSize As Size = New Size(650, 400)
    End Class


#Region "Public Shared Methods"

    Public Shared Sub LoadSettings()
        If Not Directory.Exists(BaseFolderPath) Then Directory.CreateDirectory(BaseFolderPath)
        DeserializeFromXML()
    End Sub
    Public Shared Sub SerializeToXML() 'Serialize to the bin file
        Try
            Dim FileStream As Stream = New FileStream(SettingsPath, FileMode.OpenOrCreate) With {.Position = 0} 'Open the filestream to create or open a file
            Dim BinFor As New BinaryFormatter
            FileStream = New GZipStream(FileStream, CompressionMode.Compress)  'Compress the file
            BinFor.Serialize(FileStream, Data) 'Serialize the file
            FileStream.Close() 'Close the filestream
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Shared Sub DeserializeFromXML() 'Deserialize the bin file to the Data property
        Try
            Dim FileStream As Stream = New FileStream(SettingsPath, FileMode.OpenOrCreate) With {.Position = 0} 'Open the filestream to create or open a file
            Dim bf As New BinaryFormatter
            FileStream = New GZipStream(FileStream, CompressionMode.Decompress)  'Decompress the file
            Dim tmp As Object = bf.Deserialize(FileStream) 'Deserialize the file
            FileStream.Close() 'Close the filestream
            Data = DirectCast(tmp, DataSettings) 'Returns the values
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class
'Imports System.IO
'Imports System.Xml.Serialization
'Imports System.Runtime.Serialization.Formatters.Binary
'Imports System.IO.Compression

'Public Class Settings

'    Public Shared Property Data As New DataSettings 'Contains all your settings

'    Public Class Serializable
'        Public Class FontX
'            Public Property FontFamily() As String
'                Get
'                    Return m_FontFamily
'                End Get
'                Set(value As String)
'                    m_FontFamily = value
'                End Set
'            End Property
'            Private m_FontFamily As String
'            Public Property GraphicsUnit() As GraphicsUnit
'                Get
'                    Return m_GraphicsUnit
'                End Get
'                Set(value As GraphicsUnit)
'                    m_GraphicsUnit = value
'                End Set
'            End Property
'            Private m_GraphicsUnit As GraphicsUnit
'            Public Property Size() As Single
'                Get
'                    Return m_Size
'                End Get
'                Set(value As Single)
'                    m_Size = value
'                End Set
'            End Property
'            Private m_Size As Single
'            Public Property Style() As FontStyle
'                Get
'                    Return m_Style
'                End Get
'                Set(value As FontStyle)
'                    m_Style = value
'                End Set
'            End Property
'            Private m_Style As FontStyle

'            ''' <summary>
'            ''' Intended for xml serialization purposes only
'            ''' </summary>
'            Private Sub New()
'            End Sub

'            Public Sub New(f As Font)
'                FontFamily = f.FontFamily.Name
'                GraphicsUnit = f.Unit
'                Size = f.Size
'                Style = f.Style
'            End Sub

'            Public Shared Function FromFont(f As Font) As FontX
'                Return New FontX(f)
'            End Function

'            Public Function ToFont() As Font
'                Return New Font(FontFamily, Size, Style, GraphicsUnit)
'            End Function
'        End Class
'        Public Class ColorX
'            Private m_Color As Color = Drawing.Color.White

'            Public Sub New()
'            End Sub

'            Public Sub New(newColor As Color)
'                m_Color = newColor
'            End Sub

'            <XmlIgnoreAttribute> _
'            Public Property Color() As Color
'                Get
'                    Return m_Color
'                End Get
'                Set(value As Color)
'                    m_Color = value
'                End Set
'            End Property

'            <System.Xml.Serialization.XmlAttribute("Name")> _
'            Public Property Name() As String
'                Get
'                    Return ColorTranslator.ToHtml(m_Color)
'                End Get
'                Set(value As String)
'                    m_Color = ColorTranslator.FromHtml(value)
'                End Set
'            End Property

'            <System.Xml.Serialization.XmlAttribute("Alpha")> _
'            Public Property Alpha() As Integer
'                Get
'                    Return m_Color.A
'                End Get
'                Set(value As Integer)
'                    m_Color = Color.FromArgb(value, m_Color.R, m_Color.G, m_Color.B)
'                End Set
'            End Property

'            <System.Xml.Serialization.XmlAttribute("Red")> _
'            Public Property Red() As Integer
'                Get
'                    Return m_Color.R
'                End Get
'                Set(value As Integer)
'                    m_Color = Color.FromArgb(m_Color.A, value, m_Color.G, m_Color.B)
'                End Set
'            End Property

'            <System.Xml.Serialization.XmlAttribute("Green")> _
'            Public Property Green() As Integer
'                Get
'                    Return m_Color.G
'                End Get
'                Set(value As Integer)
'                    m_Color = Color.FromArgb(m_Color.A, m_Color.R, value, m_Color.B)
'                End Set
'            End Property

'            <System.Xml.Serialization.XmlAttribute("Blue")> _
'            Public Property Blue() As Integer
'                Get
'                    Return m_Color.B
'                End Get
'                Set(value As Integer)
'                    m_Color = Color.FromArgb(m_Color.A, m_Color.R, m_Color.G, value)
'                End Set
'            End Property
'        End Class
'    End Class

'    <Serializable>
'    Public Class DataSettings
'        Public Property FolderVLCPath As String = String.Empty
'        Public Property SubFont As New Serializable.FontX(New Font("Segoe UI Light", 11))
'        Public Property SubColor As New Serializable.ColorX(Color.White)
'        Public Property ShowHint As Boolean = True
'        Public Property AutoDownloadSubtitle As Boolean = False
'        Public Property Language As String = "None"
'        Public Property Host As String = "Automatic"
'        Public Property NetworkCachingTime As Integer = 15
'        Public Property FavoriteList As New List(Of SeriesView.ItemFavorite)
'        Public Property IsFirstRun As Boolean = True
'        Public Property AlreadyWatchedEpisode As New List(Of SeriesClass.Episode)
'        Public Property PictureInPictureSize As Size = New Size(650, 400)
'    End Class

'    Private Shared XMLPath As String = String.Empty

'#Region "Constructor"
'    Public Shared Sub Setup(Path As Environment.SpecialFolder, FolderName As String, Optional FileName As String = "Settings.xml")
'        If Not Directory.Exists(CombinePaths(Environment.GetFolderPath(Path), FolderName)) Then
'            Directory.CreateDirectory(CombinePaths(Environment.GetFolderPath(Path), FolderName))
'        End If

'        XMLPath = CombinePaths(Environment.GetFolderPath(Path), FolderName, FileName)

'        If Not File.Exists(CombinePaths(Environment.GetFolderPath(Path), FolderName, FileName)) Then
'            SerializeToXML()
'            DeserializeFromXML()
'        Else
'            DeserializeFromXML()

'        End If
'    End Sub
'#End Region

'#Region "Public Methods"
'    Public Shared Sub SerializeToXML()
'        With New XmlSerializer(GetType(DataSettings))
'            Using writeStream As New StreamWriter(XMLPath)
'                .Serialize(writeStream, Data)
'                writeStream.Close()
'            End Using
'        End With
'    End Sub
'    Public Shared Sub DeserializeFromXML()
'        Dim settings As DataSettings = Nothing
'        With New XmlSerializer(GetType(DataSettings))
'            Using fs As New FileStream(XMLPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
'                Using readStream As New StreamReader(fs)
'                    settings = CType(.Deserialize(readStream), DataSettings)
'                    readStream.Close()
'                End Using
'            End Using
'        End With
'        Data = settings
'    End Sub

'#End Region

'#Region "Helper Methods"
'    Private Shared Function CombinePaths(first As String, ParamArray others As String()) As String
'        Dim _path As String = first
'        For Each section As String In others
'            _path = Path.Combine(_path, section)
'        Next
'        Return _path
'    End Function

'    Private Function ReadFileWithoutLocking(filePath As String) As StreamReader
'        Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
'            Using sr As New StreamReader(fs)
'                Return sr
'            End Using
'        End Using
'    End Function

'#End Region

'End Class


'Class Serializer
'    Public Shared Sub SaveObject(Of Typ)(ByVal Path As String, ByVal _Object As Typ, Optional ByVal Compress As Boolean = True)
'        Dim FileStream As Stream = New FileStream(Path, FileMode.OpenOrCreate) 'Open the filestream to create or open a file
'        Dim BinFor As New BinaryFormatter
'        If Compress = True Then FileStream = New GZipStream(FileStream, CompressionMode.Compress) 'Compress the file
'        BinFor.Serialize(FileStream, _Object) 'Serialize the file
'        FileStream.Close() 'Close the filestream
'    End Sub
'    Public Shared Function LoadObject(ByVal Path As String, Optional ByVal Compress As Boolean = True) As Object
'        If System.IO.File.Exists(Path) = True Then
'            Dim FileStream As Stream = New FileStream(Path, FileMode.OpenOrCreate) 'Open the filestream to create or open a file
'            Dim bf As New BinaryFormatter
'            If Compress = True Then FileStream = New GZipStream(FileStream, CompressionMode.Decompress) 'Decompress the file
'            Dim tmp As Object = bf.Deserialize(FileStream) 'Deserialize the file
'            FileStream.Close() 'Close the filestream
'            Return tmp 'Returns the values
'        Else
'            Return Nothing
'        End If
'    End Function
'End Class
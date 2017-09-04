Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Drawing
Imports System.Threading
Imports System.Threading.Tasks

Public Class DownloadManager

    'TODO: Continue to fix download system. Season don't seem to add properly ATM.

#Region "Properties"

    Private _ListOfShows As List(Of Show)
    Public Property Shows As List(Of Show)
        Get
            Return _ListOfShows
        End Get
        Set(value As List(Of Show))
            _ListOfShows = value
        End Set
    End Property

#End Region
#Region "Classes"
    Public Class Show
        Public Property Name As String
        Public Property Seasons As List(Of Season)

        Sub New()
            Seasons = New List(Of Season)
        End Sub
    End Class



    Public Class Season
        Private _Name As String
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(value As String)
                _Name = value
            End Set
        End Property

        Private _Episodes As List(Of Episode)
        Public Property Episodes As List(Of Episode)
            Get
                Return _Episodes
            End Get
            Set(value As List(Of Episode))
                _Episodes = value
            End Set
        End Property

        Sub New(SeasonName As String)
            _Name = SeasonName
            _Episodes = New List(Of Episode)
        End Sub

    End Class

    Public Class Episode
        Private _Name As String
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(value As String)
                _Name = value
            End Set
        End Property

        Private _URL As String
        Public Property URL As String
            Get
                Return _URL
            End Get
            Set(value As String)
                _URL = value
            End Set
        End Property

        Private _Host As String
        Public Property Host As String
            Get
                Return _Host
            End Get
            Set(value As String)
                _Host = value
            End Set
        End Property

        Private _Show As String
        Public Property Show As String
            Get
                Return _Show
            End Get
            Set(value As String)
                _Show = value
            End Set
        End Property

        Private _Season As String
        Public Property Season As String
            Get
                Return _Season
            End Get
            Set(value As String)
                _Season = value
            End Set
        End Property

        Private _EpisodePath As String
        Public Property Path As String
            Get
                Return _EpisodePath
            End Get
            Set(value As String)
                _EpisodePath = value
            End Set
        End Property

        Sub New(EpisodeName As String, EpisodeURL As String, EpisodeHost As String)
            _Name = EpisodeName
            _URL = EpisodeURL
            _Host = EpisodeHost
        End Sub

    End Class
#End Region

    Private _CurrentShow As Show
    Private _DownloadType As DownloadType
    Private WithEvents Series As New SeriesClass

    Public Enum DownloadType
        Show = 0
        Season = 1
        Episode = 2
    End Enum

    Public Sub New()
        _ListOfShows = New List(Of Show)
    End Sub

    Public Sub SetupDownload(selectedNode As TreeNode)
        If selectedNode Is Nothing Then Exit Sub
        _DownloadType = GetDownloadType(selectedNode.Text)
        Select Case _DownloadType
            Case DownloadType.Show
            Case DownloadType.Season
                _CurrentShow = GrabInformationsFromEpisode(_DownloadType, selectedNode)
                DownloadWholeSeason(_CurrentShow)
            Case DownloadType.Episode
                _CurrentShow = GrabInformationsFromEpisode(_DownloadType, selectedNode)
                DownloadEpisode(_CurrentShow)
        End Select
    End Sub

    Private Function GrabInformationsFromEpisode(dType As DownloadType, selectedNode As TreeNode) As Show
        Select Case dType
            Case DownloadType.Episode
                Dim S As New Show()
                S.Name = selectedNode.Parent.Parent.Text
                S.Seasons = New List(Of Season) From {New Season(selectedNode.Parent.Text)}
                S.Seasons(0).Episodes = New List(Of Episode) From {New Episode(selectedNode.Text.Replace("(W) ", String.Empty), selectedNode.ImageKey, String.Empty) With {.Show = S.Name, .Season = S.Seasons(0).Name}}
                Return S
            Case DownloadType.Season
                Dim S As New Show()
                S.Name = selectedNode.Parent.Text
                S.Seasons = New List(Of Season) From {New Season(selectedNode.Text)}

                For Each N As TreeNode In selectedNode.Nodes
                    S.Seasons(0).Episodes.Add(New Episode(N.Text.Replace("(W) ", String.Empty), N.ImageKey, String.Empty) With {.Show = S.Name, .Season = S.Seasons(0).Name})
                Next
                Return S
            Case DownloadType.Show
                Return Nothing
            Case Else
                Return Nothing
        End Select
    End Function

    Private Function GetDownloadType(ByVal query As String) As DownloadType
        If query.Contains("(") AndAlso query.Contains(")") AndAlso Not query.Contains("(W)") Then
            Return DownloadType.Show
        ElseIf query.Contains("Season") Then
            Return DownloadType.Season
        Else
            Return DownloadType.Episode
        End If
    End Function

#Region "Private Methods"

#Region "Download Episode"
    Private Sub DownloadEpisode(selectedShow As Show)
        Dim Show As Show = _ListOfShows.Find(Function(x) selectedShow.Name = x.Name)

        'If there's no show existing
        If _ListOfShows.Count = 0 Then
            If Show Is Nothing Then
                _ListOfShows.Add(selectedShow)
                frmDownload.lvShow.Items.Add(New ListViewItem(selectedShow.Name) With {.Tag = selectedShow})
                frmDownload.lvSeason.Items.Add(New ListViewItem(selectedShow.Seasons(0).Name) With {.Tag = selectedShow.Seasons(0)})
                frmDownload.lvEpisodes.Items.Add(New ListViewItem(New String() {selectedShow.FirstEpisode().Name, "Queued."}) With {.Tag = selectedShow})
            Else
                Show.Seasons(0).Episodes.Add(selectedShow.Seasons(0).Episodes(0))
                frmDownload.lvEpisodes.Items.Add(New ListViewItem(New String() {selectedShow.FirstEpisode().Name, "Queued."}) With {.Tag = selectedShow})
            End If
        ElseIf _ListOfShows.Count > 0 Then
            If Show Is Nothing Then

                frmDownload.lvSeason.Items.Clear() : frmDownload.lvEpisodes.Items.Clear()
                _ListOfShows.Add(selectedShow)

                frmDownload.lvShow.Items.Add(New ListViewItem(selectedShow.Name) With {.Tag = selectedShow})
                frmDownload.lvSeason.Items.Add(New ListViewItem(selectedShow.Seasons(0).Name) With {.Tag = selectedShow.Seasons(0)})
                frmDownload.lvEpisodes.Items.Add(New ListViewItem(New String() {selectedShow.FirstEpisode().Name, "Queued."}) With {.Tag = selectedShow})
            Else
                frmDownload.lvSeason.Items.Clear() : frmDownload.lvEpisodes.Items.Clear()

                Dim newSeason As Season = selectedShow.Seasons(0)

                'If the show doesn't exist
                If Show.Seasons.Find(Function(x) newSeason.Name = x.Name) Is Nothing Then
                    Debug.WriteLine("season doesn't exist yet")
                    Show.Seasons.Add(newSeason)
                    frmDownload.lvSeason.Items.AddRange(Show.Seasons.ToListViewItemArray)
                Else
                    Debug.WriteLine("season already exists")
                    frmDownload.lvSeason.Items.AddRange(Show.Seasons.ToListViewItemArray)
                End If

                Dim index As Integer = Show.Seasons.FindIndex(Function(x) newSeason.Name = x.Name)
                If Not Show.Seasons(index).Episodes.Exists(Function(x) x.Name = selectedShow.Seasons(0).Episodes(0).Name) Then Show.Seasons(index).Episodes.Add(selectedShow.Seasons(0).Episodes(0))
                frmDownload.lvEpisodes.Items.AddRange(Show.Seasons(index).Episodes.ToListViewItemArray)
            End If
        End If

        frmDownload.Show()
        GetEpisodeLinkAndStart(selectedShow.Seasons(0).Episodes(0))
    End Sub
    Private Sub GetEpisodeLinkAndStart(E As Episode)
        Series.GetLinkAutomaticAndDownload(E)
    End Sub

#End Region

#Region "Download Season"

    Private Sub DownloadWholeSeason(selectedShow As Show)
        Dim Show As Show = _ListOfShows.Find(Function(x) selectedShow.Name = x.Name)

        'If there's no show existing
        If _ListOfShows.Count = 0 Then
            If Show Is Nothing Then
                _ListOfShows.Add(selectedShow)
                frmDownload.lvShow.Items.Add(New ListViewItem(selectedShow.Name) With {.Tag = selectedShow})
                frmDownload.lvSeason.Items.Add(New ListViewItem(selectedShow.Seasons(0).Name) With {.Tag = selectedShow.Seasons(0)})
                frmDownload.lvEpisodes.Items.AddRange(selectedShow.ToEpisodesLVIArray())
            Else
                Show.Seasons(0).Episodes.Add(selectedShow.Seasons(0).Episodes(0))
                frmDownload.lvEpisodes.Items.AddRange(selectedShow.ToEpisodesLVIArray())
            End If
        ElseIf _ListOfShows.Count > 0 Then
            If Show Is Nothing Then

                frmDownload.lvSeason.Items.Clear() : frmDownload.lvEpisodes.Items.Clear()
                _ListOfShows.Add(selectedShow)

                frmDownload.lvShow.Items.Add(New ListViewItem(selectedShow.Name) With {.Tag = selectedShow})
                frmDownload.lvSeason.Items.Add(New ListViewItem(selectedShow.Seasons(0).Name) With {.Tag = selectedShow.Seasons(0)})
                frmDownload.lvEpisodes.Items.AddRange(selectedShow.ToEpisodesLVIArray())
            Else
                frmDownload.lvSeason.Items.Clear() : frmDownload.lvEpisodes.Items.Clear()

                Dim newSeason As Season = selectedShow.Seasons(0)

                'If the show doesn't exist
                If Show.Seasons.Find(Function(x) newSeason.Name = x.Name) Is Nothing Then
                    Debug.WriteLine("season doesn't exist yet")
                    Show.Seasons.Add(newSeason)
                    frmDownload.lvSeason.Items.AddRange(Show.Seasons.ToListViewItemArray)
                Else
                    Debug.WriteLine("season already exists")
                    frmDownload.lvSeason.Items.AddRange(Show.Seasons.ToListViewItemArray)
                End If

                Dim index As Integer = Show.Seasons.FindIndex(Function(x) newSeason.Name = x.Name)

                _CurrentShow.Seasons(0).Episodes.Clear() 'Define a temporarily show

                'Add only the non-existing episodes if the season already exist
                For i As Integer = 0 To selectedShow.Seasons(0).Episodes().Count - 1
                    If Show.Seasons(index).Episodes(i).Name <> selectedShow.Seasons(0).Episodes(i).Name Then
                        _CurrentShow.Seasons(0).Episodes.Add(selectedShow.Seasons(0).Episodes(i))
                        frmDownload.lvEpisodes.Items.Add(selectedShow.Seasons(0).Episodes(i).ToListViewItem())
                    Else
                        Debug.WriteLine("Episode already found: {0}", i)
                    End If
                Next
                frmDownload.lvEpisodes.Items.AddRange(Show.Seasons(index).Episodes.ToListViewItemArray)
            End If
        End If
        frmDownload.Show()
        GetLinksFromSeason(_CurrentShow)
    End Sub
    Private Sub GetLinksFromSeason(show As Show)
        frmDownload.Show()
        For Each E As Episode In show.Seasons(0).Episodes
            Series.GetLinkAutomaticAndDownload(E)
        Next
    End Sub

    Private Sub Series_DownloadLinkGrabbedAndDownload(Episode As Episode) Handles Series.DownloadLinkGrabbedAndDownload
        Dim VideoPath As String = GetVideoPath(Episode)
        frmDownload.AddToQueue(Episode, VideoPath)
    End Sub

    Private Function GetVideoPath(Episode As Episode) As String
        Dim BaseDir As DirectoryInfo = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ".NET Streamer - Downloaded Shows"))
        Dim ShowDir As DirectoryInfo = Directory.CreateDirectory(Path.Combine(BaseDir.FullName, Episode.Show))
        Dim SeasonDir As DirectoryInfo = Directory.CreateDirectory(Path.Combine(ShowDir.FullName, Episode.Season))
        Dim EpisodeDir As DirectoryInfo = Directory.CreateDirectory(Path.Combine(SeasonDir.FullName, Episode.Name))
        Dim VideoPath As String = String.Empty

        If Episode.URL.Contains(".mp4") Then
            VideoPath = Path.Combine(EpisodeDir.FullName, Episode.Name) & ".mp4"
        ElseIf Episode.URL.Contains(".flv") Then
            VideoPath = Path.Combine(EpisodeDir.FullName, Episode.Name) & ".flv"
        ElseIf Episode.URL.Contains(".mkv") Then
            VideoPath = Path.Combine(EpisodeDir.FullName, Episode.Name) & ".mkv"
        Else
            VideoPath = Path.Combine(EpisodeDir.FullName, Episode.Name) & ".mp4"
        End If
        Return VideoPath
    End Function

#End Region

#End Region


End Class

Imports DotNetStreamer.Service.Captcha

Public Class ShowsMoviesFetcher

    'Centralize all differents website database
    'Watchseries.biz, TMDB API, Primewire.ag, icefilms.info...

    'That class "centralize" all the differents classes that fetch the movies/tv shows
    'I have two for the moment, one for primewire, and icefilms (which isn't implemented yet)
    'So on the constructor I decided what streaming service is gonna be used
    'Then I just use the different classes based on the streaming service defined


#Region "Helpers"
    Public Shared Function ShowToTreeNode(S As Query, StreamingService As ShowsMoviesFetcher.StreamingService) As TreeNode
        Dim TN As New TreeNode(S.Name)

        If S.Movie Then TN.Tag = S

        Select Case StreamingService

            Case ShowsMoviesFetcher.StreamingService.Primewire

                For Each Season As Season In S.Seasons
                    Dim TNSeason As New TreeNode(Season.Name)

                    For Each Episode As Episode In Season.Episodes
                        Dim TNEpisode As New TreeNode(Episode.Name) With {.Tag = Episode}
                        TNSeason.Nodes.Add(TNEpisode)
                    Next
                    TN.Nodes.Add(TNSeason)
                Next

            Case ShowsMoviesFetcher.StreamingService.Icefilms

                For Each Season As Season In S.Seasons
                    Dim TNSeason As New TreeNode(Season.Name)

                    Dim TNHDEpisodes As New TreeNode("HD")
                    Dim TNDVDRiPEpisodes As New TreeNode("DVDRip / Standard Def")

                    Dim HDEpisodes As List(Of Episode) = Season.Episodes.FindAll(Function(x) x.HD = True)

                    For Each Episode As Episode In HDEpisodes
                        Dim TNEpisode As New TreeNode(Episode.Name) With {.Tag = Episode}
                        TNHDEpisodes.Nodes.Add(TNEpisode)
                    Next

                    For Each Episode As Episode In Season.Episodes
                        Dim TNEpisode As New TreeNode(Episode.Name) With {.Tag = Episode}
                        TNDVDRiPEpisodes.Nodes.Add(TNEpisode)
                    Next

                    TNSeason.Nodes.Add(TNHDEpisodes)
                    TNSeason.Nodes.Add(TNDVDRiPEpisodes)
                    TN.Nodes.Add(TNSeason)
                Next

            Case ShowsMoviesFetcher.StreamingService.DotNetStreamerAPI

                For Each Season As Season In S.Seasons
                    Dim TNSeason As New TreeNode(Season.Name) With {.Tag = S}
                    TNSeason.Nodes.Add("Loading...")
                    TN.Nodes.Add(TNSeason)
                Next

        End Select


        Return TN 'This is what actually create the treenode and store all link into each node, it's done here
    End Function
#End Region

#Region "Private Fields"

    Private WithEvents Movies As New TMDB()
    Private WithEvents Series As New SeriesClass()
    Private WithEvents Primewire As New PrimewireFetcher()
    Private WithEvents Icefilms As New IcefilmsFetcher()

#End Region

#Region "Properties"

    Private _StreamingService As StreamingService = StreamingService.Primewire
    Public Property StreamingServiceUsed() As StreamingService
        Get
            Return _StreamingService
        End Get
        Set(ByVal value As StreamingService)
            _StreamingService = value
        End Set
    End Property

    Public WriteOnly Property QueryText() As String
        Set(ByVal value As String)
            Primewire.QueryText = value
        End Set
    End Property

    Private _isMovie As Boolean
    Public Property IsMovie() As Boolean
        Get
            Return _isMovie
        End Get
        Set(ByVal value As Boolean)
            _isMovie = value
        End Set
    End Property

#End Region

#Region "Classes & Structures"

    Public Class Query
        Public Name As String
        Public Description As String
        Public URL As String
        Public ID As String
        Public ImdbRate As String
        Public Length As String
        Public ThumbnailURL As String
        Public Movie As Boolean = False
        Public Seasons As List(Of Season)

        Sub New(_Name As String)
            Name = _Name
            Seasons = New List(Of Season)
        End Sub
    End Class

    Public Class Season
        Public Name As String
        Public Episodes As List(Of Episode)

        Sub New(_Name As String)
            Name = _Name : Episodes = New List(Of Episode)
        End Sub

    End Class
    Public Class Episode
        Public Property Name As String
        Public Property Link As String
        Public Property HD As Boolean

        Sub New(_Name As String, _Link As String)
            Name = _Name : Link = _Link
        End Sub

    End Class

    Public Structure HostEpisode
        Public Name As String
        Public Rating As Double
        Public Votes As Double
        Public ExternalURL As String

        Public ID As String
        Public Key As String
        Public TempHostLink As String
    End Structure

    Public Class SortHostByRating
        Implements IComparer(Of HostEpisode)

        Public Function Compare(x As HostEpisode, y As HostEpisode) As Integer Implements IComparer(Of HostEpisode).Compare

            Dim ratioX As Double = x.Rating * x.Votes
            Dim ratioY As Double = y.Rating * y.Votes

            'If ratioX = ratioY Then Return 0
            'If ratioX < ratioY Then Return 1
            'If ratioX > ratioY Then Return -1

            Return Math.Max(Math.Min(ratioY - ratioX, 1), -1)
        End Function
    End Class

#End Region

#Region "Enums"

    Public Enum StreamingService
        Primewire = 0
        Icefilms = 1
        DotNetStreamerAPI = 2
    End Enum

    Public Enum Categories
        Movies = 1
        Series = 2
    End Enum

    Public Enum PrimewireHost
        Automatic = 0
        Gorillavid = 1
        Promptfile = 2
        TheFile = 3
        TheVideo = 4
        Bestreams = 5
        Sharesix = 6
    End Enum

    Public Enum IcefilmsHost
        Automatic = 0
        Hugefiles = 1
        HundredUpload = 2
        BillionsUpload = 3
    End Enum

    Public Enum DotNetStreamerHost
        VK = 0
    End Enum

#End Region

#Region "Events"

    Public Event SplashScreenShowsMoviesFetched(e As SplashScreenDataArgs)
    Public Event SearchCompleted(ByVal e As SearchCompletedArgs)
    Public Event OnVideoLinkDownloaded(e As LinkDownloadedEventArgs)
    Public Event SearchCompletionCompleted(ByVal e As SearchCompletionCompletedArgs)
    Public Event OnEpisodeCountGrabbed(e As EpisodeCountGrabbedEventArs)

#End Region
#Region "Structures Events"

    Structure SplashScreenDataArgs
        Property exception As Exception
        Property Categories As Categories
        Property Result As List(Of SeriesView.Item)
        Property UpdatedList As List(Of String())
    End Structure

    Structure SearchCompletedArgs
        Property exception As Exception
        Property NoResults As Boolean
        Property Query As Query
    End Structure

    Structure LinkDownloadedEventArgs
        Property PrimewireHost As PrimewireHost
        Property IcefilmsHost As IcefilmsHost
        Property AutomaticHost As Boolean
        Property Link As String
        Property Exception As Exception
    End Structure

    Structure SearchCompletionCompletedArgs
        Property Exception As Exception
        Property Query As String
        Property Result As List(Of DropDownMenu.Item)
    End Structure

    Structure EpisodeCountGrabbedEventArs
        Property EpisodeCount As String
        Property TVNode As TreeNode
    End Structure

#End Region

#Region "Splashscreen Methods (Publics & Privates)"
    Public Sub FetchEverythingOnSplashScreen()
        Movies.GetPopularMovies()
        Series.GetNewSeries()
    End Sub

    Private Sub Movies_GetPopularMoviesCompleted(e As TMDB.GetPopularMoviesCompletedArgs) Handles Movies.GetPopularMoviesCompleted
        If e.exception Is Nothing Then
            Dim args As New SplashScreenDataArgs() With {.Result = e.Movies, .Categories = e.Categories}
            RaiseEvent SplashScreenShowsMoviesFetched(args)
        Else
            RaiseEvent SplashScreenShowsMoviesFetched(New SplashScreenDataArgs() With {.exception = e.exception})
        End If
    End Sub

    Private Sub Series_SearchTopAndNewSeriesCompleted(e As SeriesClass.SearchTopAndNewSeriesCompletedArgs) Handles Series.SearchTopAndNewSeriesCompleted
        If e.exception Is Nothing Then
            Dim args As New SplashScreenDataArgs() With {.Result = e.Result, .Categories = e.Categories, .UpdatedList = e.UpdatedList}
            RaiseEvent SplashScreenShowsMoviesFetched(args)
        Else
            RaiseEvent SplashScreenShowsMoviesFetched(New SplashScreenDataArgs() With {.exception = e.exception, .Categories = e.Categories})
        End If
    End Sub
#End Region

#Region "Constructor"
    Sub New(hostName As String)
        Select Case hostName
            Case "Primewire.ag"
                _StreamingService = StreamingService.Primewire
            Case ".NET Streamer API"
                _StreamingService = StreamingService.DotNetStreamerAPI
        End Select
    End Sub
#End Region

#Region "Public Methods"

    Public Sub Search(query As String, Optional fromCompletion As Boolean = False, Optional searchType As Categories = Categories.Series)
        Select Case _StreamingService

            Case ShowsMoviesFetcher.StreamingService.Primewire
                Primewire.Search(query, fromCompletion)

            Case ShowsMoviesFetcher.StreamingService.Icefilms
                Icefilms.Search(query)

            Case StreamingService.DotNetStreamerAPI
                'DotNetStreamer.Search(query, searchType)


        End Select
    End Sub

    Public Sub GetLinksFromPrimewire(URL As String, PrimewireHost As PrimewireHost)
        Primewire.GetLinks(URL, PrimewireHost)
    End Sub

    Public Sub GetLinksFromIcefilms(URL As String, IcefilmsHost As IcefilmsHost, HDQuality As Boolean, Optional Captcha As Captcha = Nothing)
        Icefilms.GetLinks(URL, IcefilmsHost, HDQuality, Captcha)
    End Sub

    Public Sub GetLinksFromDotNetStreamerAPI(URL As String, isMovie As Boolean)
        ' DotNetStreamer.GetLinks(URL, isMovie)
    End Sub

    Public Sub GetAutoCompletion(query As String, searchType As Categories)
        Select Case StreamingServiceUsed
            Case StreamingService.Primewire, StreamingService.Icefilms
                Primewire.GetAutoCompletion(query)

            Case StreamingService.DotNetStreamerAPI
                ' DotNetStreamer.GetAutoCompletion(query, searchType)
        End Select
    End Sub

    Public Sub GetEpisodeCount(seasonId As String, seasonCount As String, tvNode As TreeNode)
        '  DotNetStreamer.GetEpisodeCount(seasonId, seasonCount, tvNode)
    End Sub

#End Region

#Region "Primewire.ag Callbacks"

    Private Sub Primewire_OnVideoLinkDownloaded(e As LinkDownloadedEventArgs) Handles Primewire.OnVideoLinkDownloaded
        RaiseEvent OnVideoLinkDownloaded(e)
    End Sub

    Private Sub Primewire_SearchCompleted(e As SearchCompletedArgs) Handles Primewire.SearchCompleted
        RaiseEvent SearchCompleted(e)
    End Sub

    Private Sub Primewire_SearchCompletionCompleted(e As SearchCompletionCompletedArgs) Handles Primewire.SearchCompletionCompleted
        RaiseEvent SearchCompletionCompleted(e)
    End Sub

#End Region

#Region "Icefilms.info Callbacks"

    Private Sub Icefilms_OnVideoLinkDownloaded(e As LinkDownloadedEventArgs) Handles Icefilms.OnVideoLinkDownloaded
        RaiseEvent OnVideoLinkDownloaded(e)
    End Sub

    Private Sub Icefilms_SearchCompleted(e As SearchCompletedArgs) Handles Icefilms.SearchCompleted
        RaiseEvent SearchCompleted(e)
    End Sub

#End Region

End Class

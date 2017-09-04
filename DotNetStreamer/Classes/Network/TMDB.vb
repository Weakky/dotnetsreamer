Imports System.Net
Imports DotNetStreamer.Utility.Http
Imports System.Web.Script.Serialization
Imports System.ComponentModel
Imports System.IO

Public Class TMDB

    Private Const API_KEY As String = "d040d1c59f1da6ba74a89f4012b168c1"
    Private Const BASE_URL As String = "https://api.themoviedb.org/3/"
    Private HTTP As New Utility.Http

    Public Structure Movie
        Public Property Title As String
        Public Property Thumbnail As Image
        Public Property Rating As String
    End Structure

    Private Methods As String() = New String() {"discover", "movie/popular"}

    Public Enum APIMethods As Integer
        Discover = 0
        PopularMovies = 1
    End Enum

    Private SortMethods As String() = New String() {"asc", "desc"}

    Public Enum APISortMethods As Integer
        Ascending = 0
        Descending = 1
    End Enum

    Structure GetPopularMoviesCompletedArgs
        Property exception As Exception
        Property Categories As SeriesClass.Categories
        Property Movies As List(Of SeriesView.Item)
    End Structure

    Public Event GetPopularMoviesCompleted(e As GetPopularMoviesCompletedArgs)

    Delegate Sub WorkerDelegate(ByVal asyncOp As AsyncOperation)

    Public Sub GetPopularMovies()
        Dim worker As New WorkerDelegate(AddressOf GetPopularMoviesWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, Nothing, Nothing)
    End Sub

    Public Sub GetPopularMoviesWorker(asyncOP As AsyncOperation)

        Dim URL As String = String.Format("{0}{1}?&api_key={2}", BASE_URL, Methods(APIMethods.PopularMovies), API_KEY)
        Dim response As String = MakeRequest(asyncOP, URL)

        If String.IsNullOrEmpty(response) Then Exit Sub

        Dim jsonDes As JSONPopularMovies.RootObject = New JavaScriptSerializer().Deserialize(Of JSONPopularMovies.RootObject)(response)

        Dim listMovies As New List(Of SeriesView.Item)

        Dim ImageStream As Stream = Nothing
        Dim wc As New WebClient() With {.Proxy = Nothing}
        Dim B As Bitmap


        For Each Result As JSONPopularMovies.Result In jsonDes.results
            ImageStream = New IO.MemoryStream(wc.DownloadData("http://image.tmdb.org/t/p/w185" & Result.poster_path))
            B = New Bitmap(ImageStream)
            listMovies.Add(New SeriesView.Item(Result.original_title, "Imdb Rating: " & Result.vote_average, String.Empty, String.Empty, B, Result.original_title))
        Next

        asyncOP.PostOperationCompleted(AddressOf RaiseGetPopularMoviesEvent, New GetPopularMoviesCompletedArgs() With {.Movies = listMovies, .Categories = SeriesClass.Categories.Movies, .exception = Nothing})

        jsonDes = Nothing
        listMovies = Nothing
        ImageStream.Dispose()
        wc.Dispose()
        '  B.Dispose()
    End Sub

    Private Sub RaiseGetPopularMoviesEvent(e As GetPopularMoviesCompletedArgs)
        RaiseEvent GetPopularMoviesCompleted(e)
    End Sub

    Private Function MakeRequest(asyncOP As AsyncOperation, URL As String) As String
        With HTTP
            Dim response As HttpResponse = .GetResponse(Utility.Http.Verb.GET, URL)

            If IsNothing(response.Error) Then
                Return response.Html
            Else
                asyncOP.PostOperationCompleted(AddressOf RaiseGetPopularMoviesEvent, New GetPopularMoviesCompletedArgs() With {.exception = New Exception()})
                Return String.Empty
            End If
        End With
    End Function

End Class

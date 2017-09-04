Imports System.Collections.Specialized
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.ComponentModel
Imports System.Text
Imports DotNetStreamer.Utility.Http
Imports ComponentAce.Compression.ZipForge
Imports ComponentAce.Compression.Archiver

Public Class SubtitlesClass

#Region "Declarations"
#Region "Structures"
    Public Structure Subtitle
        Public Link As String
        Public Name As String
        Public Season As String
        Public Episode As String
        Public Language As String
        Public LanguageFilter As String
    End Structure
#End Region
#Region "Variables"
    Dim http As New Utility.Http
    Dim _Sub As Subtitle
    Private BASE_URL As String = "http://www.podnapisi.net"
    'Dim sc As New Shell32.Shell()
#End Region
#End Region

#Region "Helping Functions"
    Private Function GetBetweenAll(ByVal Source As String, ByVal Str1 As String, ByVal Str2 As String) As String()
        Dim Results, T As New List(Of String)
        T.AddRange(Regex.Split(Source, Str1))
        T.RemoveAt(0)
        For Each I As String In T
            Results.Add(Regex.Split(I, Str2)(0))
        Next
        Return Results.ToArray
    End Function

    Private Function GetBetween(ByVal Source As String, ByVal str1 As String, ByVal str2 As String) As String
        Try
            Dim a As String = Regex.Split(Source, str1)(1)
            Return Regex.Split(a, str2)(0)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
#End Region

#Region "Main functions"

#Region "Search"

    Structure SearchCompletedArgs
        Property exception As Exception
        Property Episode As DownloadManager.Episode
        Property Links As String()
    End Structure

    Public Event SearchCompletedLVI(ByVal ListViewItems() As ListViewItem)
    Public Event SearchCompletedDownload(e As SearchCompletedArgs)

    Public Sub GetSubtitles(ByVal show As String, ByVal season As String, ByVal episode As String, ByVal language As String)
        Dim worker As New GetSubTitlesLinkDelegate(AddressOf GetSubTitlesLinkWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, show, season, episode, language, Nothing, Nothing)
    End Sub
    Public Sub GetSubtitles(Movie As String, ByVal Language As String)
        Dim worker As New GetSubtitlesMovieLinkDelegate(AddressOf GetSubTitlesLinkWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, Movie, Language, Nothing, Nothing)
    End Sub
    Public Sub GetSubtitles(Episode As DownloadManager.Episode, ByVal Language As String)
        Dim worker As New GetSubtitlesLinkDownloadDelegate(AddressOf GetSubTitlesLinkWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, Episode, Language, Nothing, Nothing)
    End Sub

    Private Sub OnSearchCompleted(ByVal ListViewItems() As ListViewItem)
        RaiseEvent SearchCompletedLVI(ListViewItems.ToArray)
    End Sub
    Private Sub OnSearchCompletedDownload(e As SearchCompletedArgs)
        RaiseEvent SearchCompletedDownload(e)
    End Sub


    Public Shared Function CurrentTimeStamp() As Double
        Return Math.Round(Now.Subtract(Convert.ToDateTime("1.1.1970 00:00:00")).TotalSeconds, 3)
    End Function

    Private Delegate Sub GetSubTitlesLinkDelegate(ByVal AsyncOp As AsyncOperation, ByVal show As String, ByVal season As String, ByVal episode As String, ByVal language As String)
    Private Delegate Sub GetSubtitlesLinkDownloadDelegate(ByVal AsyncOp As AsyncOperation, ByVal Episode As DownloadManager.Episode, ByVal Language As String)
    Private Delegate Sub GetSubtitlesMovieLinkDelegate(ByVal AsyncOp As AsyncOperation, Movie As String, ByVal Language As String)
    Private Sub GetSubTitlesLinkWorker(ByVal AsyncOp As AsyncOperation, ByVal show As String, ByVal season As String, ByVal episode As String, ByVal language As String)
        Try
            With http
                Dim LVI As New List(Of ListViewItem)
                Dim LanguageFilter As String = "en"

                With _Sub
                    Select Case language
                        Case "None"
                            LanguageFilter = String.Empty
                        Case "English"
                            LanguageFilter = "2"
                        Case "French"
                            LanguageFilter = "fr"
                        Case "Dutch"
                            LanguageFilter = "23"
                        Case "German"
                            LanguageFilter = "5"
                        Case "Danish"
                            LanguageFilter = "24"
                        Case "Brazilian"
                            LanguageFilter = "48"
                        Case "Spainish"
                            LanguageFilter = "28"
                        Case "Italian"
                            LanguageFilter = "9"
                        Case "Croatian"
                            LanguageFilter = "38"
                        Case "Swedish"
                            LanguageFilter = "25"
                    End Select
                End With

                Dim response As HttpResponse = .GetResponse(Utility.Http.Verb.GET, String.Format("http://www.podnapisi.net/subtitles/search/advanced?keywords={0}&year=&seasons={1}&episodes={2}&language={3}", show, season, episode, LanguageFilter))

                If Not IsNothing(response.Error) Then
                    Debug.Print("Could not request search information; " & response.Error.Message)
                    Return
                End If

                Dim subListHtml As String() = Split(response.Html, "<tr class=""subtitle-entry""").Skip(1).ToArray()

                For Each subHTML As String In subListHtml

                    Dim link As String = Split(subHTML, "</div>")(0)
                    link = GetBetween(link, "data-href=", ">").Replace(Chr(34), String.Empty)

                    Dim subEntitled As String = GetBetween(subHTML, String.Format("<a href=""{0}"">", link), "</a>").Trim()
                    Dim subName As String = Split(subEntitled, " Season ")(0)
                    Dim subSeason As String = GetBetween(subEntitled, " Season ", " Episode ")
                    Dim subEpisode As String = Split(subEntitled, " Episode ")(1)
                    Dim subLink As String = link

                    LVI.Add(New ListViewItem(New String() {subName, subSeason, subEpisode, language, subLink}))

                Next


                AsyncOp.PostOperationCompleted(AddressOf OnSearchCompleted, LVI.ToArray)
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetSubTitlesLinkWorker(ByVal AsyncOp As AsyncOperation, Ep As DownloadManager.Episode, ByVal Language As String)
        Try
            With http

                Dim Show As String = Split(Ep.Show, " (")(0).Trim
                Dim Season As String = Ep.Season.Split(" ")(1).Trim
                Dim Episode As String = Split(Split(Ep.Name, "Episode")(1), " -")(0).Trim


                Dim Links As New List(Of String)
                Dim Headers As New NameValueCollection
                Headers.Add("X-Requested-With", "XMLHttpRequest")
                Headers.Add("Pragma", "no-cache")
                Headers.Add("Cache-Control", "no-cache")

                .Accept = "*/*"
                .AcceptCharset = String.Empty
                .AcceptEncoding = "gzip, deflate"
                .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"

                .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"

                .Referer = String.Format("http://fr.podnapisi.net/fr/ppodnapisi/search?sT=1&sK={0}&sTS={1}&sTE={2}", Show, Season, Episode)

                With _Sub
                    Select Case Language
                        Case "None"
                            .LanguageFilter = String.Empty
                        Case "English"
                            .LanguageFilter = "2"
                        Case "French"
                            .LanguageFilter = "8"
                        Case "Dutch"
                            .LanguageFilter = "23"
                        Case "German"
                            .LanguageFilter = "5"
                        Case "Danish"
                            .LanguageFilter = "24"
                        Case "Brazilian"
                            .LanguageFilter = "48"
                        Case "Spainish"
                            .LanguageFilter = "28"
                        Case "Italian"
                            .LanguageFilter = "9"
                        Case "Croatian"
                            .LanguageFilter = "38"
                        Case "Swedish"
                            .LanguageFilter = "25"
                    End Select
                End With

                Dim hr As HttpResponse = .GetResponse(Verb.POST, "http://fr.podnapisi.net/pprofil/set_setting", "rpcParams=%5B%22subtitles.languages%22%2C+%5B" & _Sub.LanguageFilter & "%5D%5D&notificationTimestamp=" & CurrentTimeStamp(), Headers)
                If Not IsNothing(hr.Error) Then
                    Debug.Print("Could not submit profile setting information; " & hr.Error.Message)
                    Return
                ElseIf Not hr.Html.Trim.Equals("{""output"":null}") Then
                    Debug.Print("Unexpected response from server.")
                    Return
                End If

                Headers.Clear()
                Headers.Add("Cache-Control", "max-age=0")

                .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"

                hr = .GetResponse(Verb.GET, String.Format("http://fr.podnapisi.net/fr/ppodnapisi/search?sT=1&sK={0}&sTS={1}&sTE={2}", Show, Season, Episode).Replace(" ", "+"), String.Empty, Headers)
                If Not IsNothing(hr.Error) Then
                    Debug.Print("Could not request search information; " & hr.Error.Message)
                    Return
                End If


                For Each m As Match In Regex.Matches(hr.Html, "<a class=""subtitle_page_link"" href=""(.*)"">(.*)<b>.*</b></a>(.*)<b>(.*)</b>(.*)<b>(.*)</b>")
                    _Sub.Link = m.Groups(1).Value
                    _Sub.Name = m.Groups(2).Value
                    _Sub.Season = "Season: " & m.Groups(4).Value
                    _Sub.Episode = m.Groups(5).Value & m.Groups(6).Value
                    _Sub.Language = m.Groups(7).Value
                    Links.Add(_Sub.Link)
                Next

                Dim match As MatchCollection = Regex.Matches(hr.Html, "<div class=""flag"" style="".*"" title=""(.*)"" alt="".*"">")


                AsyncOp.PostOperationCompleted(AddressOf OnSearchCompletedDownload, New SearchCompletedArgs() With {.Episode = Ep, .Links = Links.ToArray, .exception = Nothing})
            End With
        Catch ex As Exception
            AsyncOp.PostOperationCompleted(AddressOf OnSearchCompletedDownload, New SearchCompletedArgs() With {.Episode = Ep, .Links = Nothing, .exception = ex})
        End Try
    End Sub
    Private Sub GetSubTitlesLinkWorker(ByVal AsyncOp As AsyncOperation, movie As String, ByVal Language As String)
        Try
            With http

                Dim LVI As New List(Of ListViewItem)
                Dim Headers As New NameValueCollection
                Headers.Add("X-Requested-With", "XMLHttpRequest")
                Headers.Add("Pragma", "no-cache")
                Headers.Add("Cache-Control", "no-cache")

                .Accept = "*/*"
                .AcceptCharset = String.Empty
                .AcceptEncoding = "gzip, deflate"
                .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"

                .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"

                .Referer = String.Format("http://www.podnapisi.net/fr/ppodnapisi/search?sJ=8&sT=0&sK={0}", movie)

                With _Sub
                    Select Case Language
                        Case "None"
                            .LanguageFilter = String.Empty
                        Case "English"
                            .LanguageFilter = "2"
                        Case "French"
                            .LanguageFilter = "8"
                        Case "Dutch"
                            .LanguageFilter = "23"
                        Case "German"
                            .LanguageFilter = "5"
                        Case "Danish"
                            .LanguageFilter = "24"
                        Case "Brazilian"
                            .LanguageFilter = "48"
                        Case "Spainish"
                            .LanguageFilter = "28"
                        Case "Italian"
                            .LanguageFilter = "9"
                        Case "Croatian"
                            .LanguageFilter = "38"
                        Case "Swedish"
                            .LanguageFilter = "25"
                    End Select
                End With

                Dim hr As HttpResponse = .GetResponse(Verb.POST, "http://fr.podnapisi.net/pprofil/set_setting", "rpcParams=%5B%22subtitles.languages%22%2C+%5B" & _Sub.LanguageFilter & "%5D%5D&notificationTimestamp=" & CurrentTimeStamp(), Headers)
                If Not IsNothing(hr.Error) Then
                    Debug.Print("Could not submit profile setting information; " & hr.Error.Message)
                    Return
                ElseIf Not hr.Html.Trim.Equals("{""output"":null}") Then
                    Debug.Print("Unexpected response from server.")
                    Return
                End If

                Headers.Clear()
                Headers.Add("Cache-Control", "max-age=0")

                .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"

                hr = .GetResponse(Verb.GET, String.Format("http://www.podnapisi.net/fr/ppodnapisi/search?sJ={0}&sT=0&sK={1}", _Sub.LanguageFilter, movie.Replace(" ", "+")), String.Empty, Headers)
                If Not IsNothing(hr.Error) Then
                    Debug.Print("Could not request search information; " & hr.Error.Message)
                    Return
                End If


                For Each m As Match In Regex.Matches(hr.Html, "<a class=""subtitle_page_link"" href=""(.*)"">(.*)<b>(.*)</b></a>")
                    _Sub.Link = m.Groups(1).Value
                    _Sub.Name = m.Groups(2).Value & m.Groups(3).Value
                    _Sub.Language = m.Groups(7).Value
                    LVI.Add(New ListViewItem(New String() {_Sub.Name, "/", "/", _Sub.Language, _Sub.Link}))
                Next

                Dim match As MatchCollection = Regex.Matches(hr.Html, "<div class=""flag"" style="".*"" title=""(.*)"" alt="".*"">")

                For i = 0 To LVI.Count - 1
                    LVI(i).SubItems(3).Text = match(i).Groups(1).Value
                Next

                AsyncOp.PostOperationCompleted(AddressOf OnSearchCompleted, LVI.ToArray)
            End With
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "DownloadSubtitle"
    Public Structure DownloadCompletedArgs
        Property Exception As Exception
        Property Episode As DownloadManager.Episode
    End Structure

    Public Event OnSubtitleDownloaded(ByVal srtContent As String)
    Public Event OnSubtitleDownloadedFromEpisode(ByVal e As DownloadCompletedArgs)

    Public Sub DownloadSubtitle(ByVal URL As String)
        Dim worker As New DownloadSubtitleDelegate(AddressOf DownloadSubtitleWorker)
        Dim asyncop As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncop, URL, Nothing, Nothing)
    End Sub

    Public Sub DownloadSubtitle(ByVal Episode As DownloadManager.Episode, URL As String)
        Dim worker As New DownloadSubtitleFromEpisodeDelegate(AddressOf DownloadSubtitleWorker)
        Dim asyncop As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncop, Episode, URL, Nothing, Nothing)
    End Sub


    Private Delegate Sub DownloadSubtitleDelegate(ByVal AsyncOp As AsyncOperation, ByVal link As String)
    Private Delegate Sub DownloadSubtitleFromEpisodeDelegate(ByVal AsyncOp As AsyncOperation, Episode As DownloadManager.Episode, ByVal link As String)


    Private Sub DownloadSubtitleWorker(ByVal AsyncOp As AsyncOperation, ByVal link As String)
        Try
            With http
                Dim HTML As String = .GetResponse(Verb.GET, "http://www.podnapisi.net" & link).Html
                'TODO: Fix SubDLink here

                ' Dim SubDLink As String = Split(Regex.Match(HTML, "<a rel=""nofollow"" target=""_blank"" class=""button big download"" href=""(.*)"">.*</a>").Groups(1).Value, ChrW(34))(0)
                '<a rel="nofollow" target="_blank" class="button big download" href="/fr/ppodnapisi/predownload/i/2722375/k/0dde838833043f9598ed088d01e7853e32d90d79">Télécharger sous-titres</a>
                Dim DlLink As String = BASE_URL & Regex.Match(HTML, "<form class=""form-inline download-form"" action=""(.*)"">").Groups(1).Value

                Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DotNetStreamer\" & "\Subtitles\" & "sub.zip"
                Dim dirpath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DotNetStreamer\" & "\Subtitles"

                IO.Directory.CreateDirectory(dirpath)

                If IO.File.Exists(path) Then
                    IO.File.Delete(path)
                End If

                Using wc As New System.Net.WebClient()
                    wc.DownloadFile(DlLink, path)
                End Using

                Using archiver As New ZipForge
                    archiver.FileName = path
                    archiver.OpenArchive(System.IO.FileMode.Open)
                    archiver.BaseDir = dirpath
                    archiver.ExtractFiles("*.*")
                    archiver.CloseArchive()
                End Using

                Dim subtitle As String = String.Empty
                Dim subpath As String = Directory.GetFiles(dirpath, "*.srt", SearchOption.TopDirectoryOnly)(0)
                Dim sw As New StreamReader(subpath, Encoding.Default)
                subtitle = sw.ReadToEnd()
                sw.Close()

                If subtitle.Contains("©") Or subtitle.Contains("Ã") Or subtitle.Contains("®") Then
                    Dim FL = New FileInfo(subpath)
                    Dim SR = FL.OpenText()
                    Dim SW1 = New StreamWriter(New FileStream(FL.FullName + ".UTF8.txt", FileMode.OpenOrCreate), Encoding.UTF8)
                    SW1.Write(SR.ReadToEnd())
                    SR.Close()
                    SW1.Close()

                    subtitle = File.ReadAllText(FL.FullName + ".UTF8.txt", Encoding.UTF8)
                    File.Delete(FL.FullName + ".UTF8.txt")
                End If

                File.Delete(Directory.GetFiles(dirpath, "*.srt", SearchOption.TopDirectoryOnly)(0))

                AsyncOp.PostOperationCompleted(AddressOf SubtitleDownloaded, subtitle)
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub DownloadSubtitleWorker(ByVal AsyncOp As AsyncOperation, ByVal Ep As DownloadManager.Episode, ByVal link As String)
        Try
            With http
                Dim HTML As String = .GetResponse(Verb.GET, "http://www.podnapisi.net" & link).Html
                Dim DlLink As String = "http://www.podnapisi.net" & Regex.Match(HTML, "<a rel=""nofollow"" class=""button big download"" href=""(.*)"" title="".*"">.*</a>").Groups(1).Value

                Dim BaseDir As String = IO.Path.GetDirectoryName(Ep.Path)
                Dim SubtitleDir As String = IO.Path.Combine(BaseDir, "Subtitles")
                Dim ZipFileName As String = Path.GetRandomFileName & ".zip"
                Dim SubtitlePath As String = IO.Path.Combine(SubtitleDir, ZipFileName)

                IO.Directory.CreateDirectory(SubtitleDir)

                If IO.File.Exists(SubtitlePath) Then
                    IO.File.Delete(SubtitlePath)
                End If

                Dim WC As New System.Net.WebClient()
                WC.DownloadFile(DlLink, SubtitlePath)

                Using archiver As New ZipForge
                    archiver.FileName = SubtitlePath
                    archiver.OpenArchive(System.IO.FileMode.Open)
                    archiver.BaseDir = SubtitleDir
                    archiver.ExtractFiles("*.*")
                    archiver.CloseArchive()
                End Using

                If File.Exists(SubtitlePath) Then File.Delete(SubtitlePath)

                AsyncOp.PostOperationCompleted(AddressOf SubtitleDownloadedFromEpisode, New DownloadCompletedArgs() With {.Episode = Ep, .Exception = Nothing})
            End With
        Catch ex As Exception
            AsyncOp.PostOperationCompleted(AddressOf SubtitleDownloadedFromEpisode, New DownloadCompletedArgs() With {.Episode = Ep, .Exception = ex})
        End Try
    End Sub

    Private Sub SubtitleDownloaded(ByVal srtContent As String)
        RaiseEvent OnSubtitleDownloaded(srtContent)
    End Sub
    Private Sub SubtitleDownloadedFromEpisode(e As DownloadCompletedArgs)
        RaiseEvent OnSubtitleDownloadedFromEpisode(e)
    End Sub


#End Region

#End Region

End Class

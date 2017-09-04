Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Net
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Web
Imports System.Text
Imports DotNetStreamer.ShowsMoviesFetcher

Public Class PrimewireFetcher

#Region "Helpers"
    Private Function GetBetween(ByVal Source As String, ByVal str1 As String, ByVal str2 As String) As String
        Try
            Dim a As String = Regex.Split(Source, str1)(1)
            Return Regex.Split(a, str2)(0)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Private Function GetBetweenAll(ByVal Source As String, ByVal Str1 As String, ByVal Str2 As String) As String()
        Dim Results, T As New List(Of String)
        T.AddRange(Regex.Split(Source, Str1))
        T.RemoveAt(0)
        For Each I As String In T
            Results.Add(Regex.Split(I, Str2)(0))
        Next
        Return Results.ToArray
    End Function
    Private Function GetRealURL(URL As String) As String
        With HTTP
            Dim Base64Code As String = HttpUtility.ParseQueryString(URL).Get("url")
            Dim RealURL As String = Encoding.UTF8.GetString(Convert.FromBase64String(Base64Code))
            Return RealURL
        End With
    End Function
    Private Function GetRealURLAndResponse(URL As String) As String
        With HTTP
            Dim Base64Code As String = HttpUtility.ParseQueryString(URL).Get("url")
            Dim RealURL As String = Encoding.UTF8.GetString(Convert.FromBase64String(Base64Code))
            Return .GetResponse(Utility.Http.Verb.GET, RealURL).Html
        End With
    End Function
    Private Function GetHostFromName(Name As String) As PrimewireHost
        Select Case Name
            Case "thefile.me"
                Return PrimewireHost.TheFile

            Case "thevideo.me"
                Return PrimewireHost.TheVideo

            Case "promptfile.com"
                Return PrimewireHost.Promptfile

            Case "gorillavid.in"
                Return PrimewireHost.Gorillavid

            Case "sharesix.com"
                Return PrimewireHost.Sharesix

            Case "bestreams.net"
                Return PrimewireHost.Bestreams
            Case Else
                Return PrimewireHost.Automatic
        End Select
    End Function
    Private Function DoesImageExistRemotely(uriToImage As String) As Boolean
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(uriToImage), HttpWebRequest)

        request.Method = "HEAD"
        Try
            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)

                If response.StatusCode = HttpStatusCode.OK Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch generatedExceptionName As WebException
            Return False
        Catch
            Return False
        End Try
    End Function
#End Region

#Region "Fields"

    Private Const BaseURL As String = "http://www.primewire.ag"
    ' Private Key As String = "7bff31456f397846"
    Private HTTP As New Utility.Http
    Public Shared Hosts() As String = {"Automatic", "Gorillavid.in", "Promptfile.com", "Thefile.me", "Thevideo.me", "Bestreams.net", "Sharesix.com"}

#End Region

#Region "Events, Delegates & Callbacks"

    Public Event SearchCompleted(ByVal e As SearchCompletedArgs)
    Private Delegate Sub WorkerDelegate(ByVal asyncOp As AsyncOperation, ByVal query As String, FromLink As Boolean)
    Private Sub OnSearchCompleted(ByVal e As SearchCompletedArgs)
        RaiseEvent SearchCompleted(e)
    End Sub


    Public Event OnVideoLinkDownloaded(e As LinkDownloadedEventArgs)
    Private Delegate Sub DownloadLinkDelegate(ByVal AsyncOp As AsyncOperation, ByVal link As String, Host As PrimewireHost)
    Private Sub LinkDownloaded(e As LinkDownloadedEventArgs)
        RaiseEvent OnVideoLinkDownloaded(e)
    End Sub


    Public Event SearchCompletionCompleted(ByVal e As SearchCompletionCompletedArgs)
    Delegate Sub WorkerDelegateCompletion(ByVal asyncOp As AsyncOperation, query As String)
    Private Sub OnCompletionCompleted(e As SearchCompletionCompletedArgs)
        RaiseEvent SearchCompletionCompleted(e)
    End Sub

#End Region

#Region "Public Methods"

    Public Sub Search(ByVal query As String, FromLink As Boolean)
        Dim worker As New WorkerDelegate(AddressOf SearchWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, query, FromLink, Nothing, Nothing)
    End Sub

    Public Sub GetLinks(ByVal link As String, Host As PrimewireHost)
        Dim worker As New DownloadLinkDelegate(AddressOf GetLinksWorker)
        Dim asyncop As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncop, link, Host, Nothing, Nothing)
    End Sub

    Public Sub GetAutoCompletion(query As String)
        Dim worker As New WorkerDelegateCompletion(AddressOf GetCompletionWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, query, Nothing, Nothing)
    End Sub

#End Region

#Region "Private Methods"

    Private Sub SearchWorker(asyncOp As AsyncOperation, PWquery As String, FromLink As Boolean)
        With HTTP

            PWquery = Split(PWquery, "(")(0).Replace("..", String.Empty)
            Dim HTML As String = String.Empty
            Dim ShowsHTML As String = String.Empty
            Dim showPreURL As String = String.Empty

            If Not FromLink Then
                HTML = .GetResponse(Utility.Http.Verb.GET, String.Format("{0}/index.php?search_keywords={1}&search_section=1", BaseURL, PWquery)).Html

                If HTML.Contains("Sorry but I couldn't find anything like that") Then
                    asyncOp.PostOperationCompleted(AddressOf OnSearchCompleted, New SearchCompletedArgs With {.exception = New Exception("Query does not exist.")})
                    Return
                End If

                ShowsHTML = GetBetweenAll(HTML, "<div class=""clearer""></div>", "<div class=""clearer""></div>")(1)
                showPreURL = BaseURL & Regex.Match(HTML, "<a href=""(.*)"" title="".*""><img src=""(.*)"" border="".*"" width="".*"" height="".*"" alt="".*""><h2>(.*)</h2></a>").Groups(1).Value
            Else
                showPreURL = PWquery
            End If

            Dim showHTML As String = .GetResponse(Utility.Http.Verb.GET, showPreURL).Html

            Dim showDescription As String = GetBetween(showHTML, "<td colspan=""2""><p style=""width:460px; display:block;"">", "<td width=""170"" rowspan=""11"" align=""center"" valign=""top"">").Replace("</p></td>", String.Empty).Trim()

            Dim showTitle As String = GetBetweenAll(showHTML, "<h1 class=""titles""><span>", "</span></h1>")(1)
            showTitle = Regex.Match(showTitle, "<a href="".*"" title="".*"">(.*)</a>").Groups(1).Value
            Dim showImageURL As String = "http:" & Regex.Match(showHTML, "<img src=""(.*)"" width="".*"" border="".*"" style="".*"">").Groups(1).Value

            Dim Query As New Query(showTitle)
            Query.Description = showDescription
            Query.ThumbnailURL = New Uri(showImageURL).AbsoluteUri
            Query.URL = showPreURL

            GetSeasonsAndEpisodes(asyncOp, Query, showHTML)
        End With
    End Sub
    Private Sub GetSeasonsAndEpisodes(asyncOP As AsyncOperation, query As Query, episodeHTML As String)
        With HTTP

            Dim regexSeasons As New Regex("<div data-id.*="".*"" class=""show_season"">")

            If regexSeasons.IsMatch(episodeHTML) Then 'If query is a tv show

                Dim seasonsHTML As String() = Regex.Split(episodeHTML, "<div data-id.*="".*"" class=""show_season"">").Skip(1).ToArray()
                Dim seasonNumber As Integer = 1

                Debug.WriteLine(String.Format("---------------- {0} ----------------", query.Name))

                For Each seasonHTML As String In seasonsHTML

                    Dim Season As New Season(String.Format("Season {0}", seasonNumber))
                    Dim regexEpisodes As MatchCollection = Regex.Matches(seasonHTML, "<div class=""tv_episode_item""> <a href=""(.*)"">(.*) <span class=""tv_episode_name"">(.*)</span>")


                    Debug.WriteLine(String.Format("---------------- {0} ----------------", Season.Name))

                    For Each M As Match In regexEpisodes
                        Dim episodeName As String = M.Groups(2).Value.Replace("                               ", "") & M.Groups(3).Value
                        Dim episodeURL As String = BaseURL & M.Groups(1).Value

                        Season.Episodes.Add(New Episode(episodeName, episodeURL))
                    Next

                    query.Seasons.Add(Season)
                    seasonNumber += 1
                Next

            Else
                query.Movie = True
            End If

            asyncOP.PostOperationCompleted(AddressOf OnSearchCompleted, New SearchCompletedArgs With {.exception = Nothing, .NoResults = False, .query = query})

        End With
    End Sub
    Private Sub GetShows(asyncOp As AsyncOperation, query As String)
        With HTTP

            Dim ShowList As New List(Of Query)
            Dim HTML As String = .GetResponse(Utility.Http.Verb.GET, String.Format("{0}/index.php?search_keywords={1}&search_section=2", BaseURL, query)).Html
            Dim ShowsHTML As String = GetBetweenAll(HTML, "<div class=""clearer""></div>", "<div class=""clearer""></div>")(1)

            Dim regexShow As MatchCollection = Regex.Matches(HTML, "<a href=""(.*)"" title="".*""><img src=""(.*)"" border=""0"" width=""150"" height=""225"" alt="".*""><h2>(.*)</h2></a>")

            For Each M As Match In regexShow
                Dim showTitle As String = M.Groups(3).Value
                Dim showLink As String = BaseURL & M.Groups(1).Value
                Dim showImageURL As String = "http://" & M.Groups(2).Value.Remove(0, 2)

                Dim newShow As New Query(showTitle)
                newShow.URL = showLink
                newShow.ThumbnailURL = showImageURL
                ShowList.Add(newShow)
            Next

            asyncOp.PostOperationCompleted(AddressOf OnSearchCompleted, New SearchCompletedArgs With {.exception = Nothing, .NoResults = False, .Query = ShowList(0)})
        End With
    End Sub
    Private Function GetHosts(episodeURL As String) As HostEpisode()
        With HTTP

            Dim HostList As New List(Of HostEpisode)()
            Dim HTML As String = .GetResponse(Utility.Http.Verb.GET, episodeURL).Html
            Dim episodesHTML As String() = Split(HTML, "<tbody>").Skip(2).ToArray()
            Array.Resize(episodesHTML, episodesHTML.Length - 1)

            For Each episodeHTML In episodesHTML

                Dim hostExternalLink As String = GetBetween(episodeHTML, "<a href=", "onClick").Replace(ChrW(34), String.Empty).Trim()
                Dim hostName As String = Split(Split(episodeHTML, "document.writeln('")(1), "')")(0)
                Dim hostRating As String = Regex.Match(episodeHTML, "<li class=""current-rating"" style=""width:.*;"">Currently (.*)/5</li>").Groups(1).Value
                Dim hostVotes As String = Regex.Match(episodeHTML, "<div class=.*>(.*)</div>").Groups(1).Value.Replace("(", String.Empty).Replace("votes)", String.Empty).Replace("vote)", String.Empty)

                If Not hostName.Equals("Watch HD") Then

                    If hostName.Contains("gorillavid.") Then hostName = "gorillavid.in"

                    Dim Host As New HostEpisode()
                    Host.Name = hostName
                    Host.Rating = Double.Parse(hostRating, CultureInfo.InvariantCulture)
                    Host.ExternalURL = hostExternalLink
                    Host.Votes = Double.Parse(hostVotes, CultureInfo.InvariantCulture)


                    Debug.WriteLine("{0} - {1} - {2}", Host.Name, Host.Rating, Host.ExternalURL)
                    HostList.Add(Host)

                End If
            Next

            HostList.Sort(New SortHostByRating())

            Return HostList.ToArray()
        End With
    End Function


    Private Sub GetLinksWorker(ByVal AsyncOp As AsyncOperation, ByVal link As String, ByVal Host As PrimewireHost)
        Try
            With HTTP

                Dim AvailableHost As List(Of HostEpisode) = GetHosts(link).ToList()
                Dim e As New LinkDownloadedEventArgs With {.PrimewireHost = Host, .Link = String.Empty}
                Dim externalURL As String = String.Empty

                Dim existingHosts As List(Of HostEpisode) = AvailableHost.FindAll(Function(x) x.Name = "thefile.me" Or x.Name = "thevideo.me" Or x.Name = "promptfile.com" Or x.Name = "gorillavid.in")

                If existingHosts.Count = 0 Then
                    e.Exception = New Exception("No hosts available for this tv show/movie.")
                    AsyncOp.PostOperationCompleted(AddressOf LinkDownloaded, e)
                End If


                Select Case Host
                    Case PrimewireHost.Automatic
                        e = GetLinkAutomatic(link, AvailableHost.ToArray())


                    Case PrimewireHost.TheFile
                        externalURL = AvailableHost.Find(Function(x) x.Name = "thefile.me").ExternalURL
                        If externalURL <> String.Empty Then e.Link = GetTheFileMeLink(externalURL)

                    Case PrimewireHost.Bestreams
                        externalURL = AvailableHost.Find(Function(x) x.Name = "bestreams.net").ExternalURL
                        If externalURL <> String.Empty Then e.Link = GetBestStreamsLink(externalURL)

                    Case PrimewireHost.Sharesix
                        externalURL = AvailableHost.Find(Function(x) x.Name = "sharesix.com").ExternalURL
                        If externalURL <> String.Empty Then e.Link = GetShareSixLink(externalURL)

                    Case PrimewireHost.TheVideo
                        externalURL = AvailableHost.Find(Function(x) x.Name = "thevideo.me").ExternalURL
                        If externalURL <> String.Empty Then e.Link = GetTheVideoLink(externalURL)

                    Case PrimewireHost.Promptfile
                        externalURL = AvailableHost.Find(Function(x) x.Name = "promptfile.com").ExternalURL
                        If externalURL <> String.Empty Then e.Link = GetPromptFileLink(externalURL)

                    Case PrimewireHost.Gorillavid
                        externalURL = AvailableHost.Find(Function(x) x.Name = "gorillavid.in").ExternalURL
                        If externalURL <> String.Empty Then e.Link = GetGorriladVidLink(externalURL)
                End Select


                AsyncOp.PostOperationCompleted(AddressOf LinkDownloaded, e)
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetLinkAutomatic(link As String, hostsEpisodes As HostEpisode()) As LinkDownloadedEventArgs
        Dim URL As String = String.Empty

        Dim listHosts As List(Of HostEpisode) = hostsEpisodes.ToList() 'stupid 
            For i As Integer = 1 To 4

                Dim hostIndex As Integer = i

                Dim selectedHost As HostEpisode = listHosts.Find(Function(x) x.Name = Hosts(hostIndex).ToLower)

                If Not IsNothing(selectedHost) AndAlso Not String.IsNullOrEmpty(selectedHost.ExternalURL) Then

                    Select Case selectedHost.Name

                        Case "thefile.me"
                            URL = GetTheFileMeLink(selectedHost.ExternalURL)

                        Case "thevideo.me"
                            URL = GetTheVideoLink(selectedHost.ExternalURL)

                        Case "promptfile.com"
                            URL = GetPromptFileLink(selectedHost.ExternalURL)

                        Case "gorillavid.in"
                            URL = GetGorriladVidLink(selectedHost.ExternalURL)

                    End Select

                    Return New LinkDownloadedEventArgs() With {.AutomaticHost = True, .Link = URL, .PrimewireHost = GetHostFromName(Hosts(hostIndex).ToLower)}
                Else
                    Continue For
                End If
            Next




            'If String.IsNullOrWhiteSpace(URL) Then
            '    For Each Host As HostEpisode In hostsEpisodes
            '        Select Case Host.Name

            '            Case "thefile.me"
            '                URL = GetTheFileMeLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

            '            Case "thevideo.me"
            '                URL = GetTheVideoLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

            '            Case "promptfile.com"
            '                URL = GetPromptFileLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

            '            Case "gorillavid.in"
            '                URL = GetGorriladVidLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

            '                ' Case "sharesix.com"
            '                '    URL = GetShareSixLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

            '                '   Case "bestreams.net"
            '                '    URL = GetBestStreamsLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

            '        End Select

            '        If URL = String.Empty Then Continue For

            '        Debug.WriteLine("=======> " & Host.Name & " " & URL)
            '        Return New LinkDownloadedEventArgs() With {.AutomaticHost = True, .Link = URL, .PrimewireHost = GetHostFromName(Host.Name)}
            '    Next
            'End If

            Return New LinkDownloadedEventArgs() With {.Link = String.Empty}
    End Function
    Public Function GetBestStreamsLink(externalURL As String) As String
        With HTTP


            Dim HTML As String = GetRealURLAndResponse(externalURL)

            Dim id As String = Regex.Match(HTML, "<input type=""hidden"" name=""id"" value=""(.*)"">").Groups(1).Value
            Dim fname As String = Regex.Match(HTML, "<input type=""hidden"" name=""fname"" value=""(.*)"">").Groups(1).Value
            Dim referer As String = Regex.Match(HTML, "<input type=""hidden"" name=""referer"" value=""(.*)"">").Groups(1).Value
            Dim hash As String = Regex.Match(HTML, "<input type=""hidden"" name=""hash"" value=""(.*)"">").Groups(1).Value

            Dim Headers As New NameValueCollection()
            Headers.Add("Connection", "keep-alive")

            .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0"
            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
            .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
            .AcceptEncoding = "gzip, deflate"
            .Referer = "http://bestreams.net/" & id

            Threading.Thread.Sleep(1500)

            Dim postData As String = String.Format("op=download1&usr_login=&id={0}&fname={1}&referer={3}&hash={2}&imhuman=Proceed+to+video", id, fname, hash, referer)
            Dim postRequestHTML As String = .GetResponse(Utility.Http.Verb.POST, String.Format("http://bestreams.net/{0}", id), postData, Headers).Html

            Dim DirectLink As String = Regex.Match(postRequestHTML, "file: ""(.*)""").Groups(1).Value
            Return DirectLink
        End With
    End Function
    Public Function GetShareSixLink(externalURL As String) As String
        With HTTP

            Dim redirectionURL As String = GetRealURL(externalURL)

            Dim Headers As New NameValueCollection()
            Headers.Add("Connection", "keep-alive")

            .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0"
            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
            .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
            .AcceptEncoding = "Accept-Encoding"
            .AcceptCharset = String.Empty
            .ContentType = "application/x-www-form-urlencoded"
            .Referer = redirectionURL

            Dim HTML As String = .GetResponse(Utility.Http.Verb.POST, redirectionURL, "method_free=Free", Headers).Html

            Dim DirectLink As String = GetBetween(HTML, "var lnk1 = '", "';")

            Process.Start(DirectLink)

            Return DirectLink
        End With
    End Function
    Public Function GetTheFileMeLink(externalURL As String) As String
        With HTTP

            Dim HTML As String = GetRealURLAndResponse(externalURL)

            Dim id As String = Regex.Match(HTML, "<input type=""hidden"" name=""id"" value=""(.*)"">").Groups(1).Value
            Dim fname As String = Regex.Match(HTML, "<input type=""hidden"" name=""fname"" value=""(.*)"">").Groups(1).Value

            Dim postData As String = String.Format("op=download1&usr_login=&id={0}&fname={1}&referer=&method_free=Free+Download", id, fname)
            Dim postRequestHTML As String = .GetResponse(Utility.Http.Verb.POST, String.Format("http://thefile.me/{0}", id), postData).Html

            Dim rand As String = Regex.Match(postRequestHTML, "<input type=""hidden"" name=""rand"" value=""(.*)"">").Groups(1).Value

            Dim postDataGenerateLink As String = String.Format("op=download2&id={0}&rand={1}&referer=&method_free=Free+Download&method_premium=&down_direct=1", id, rand)
            Dim postRequesGenerateLinktHTML As String = .GetResponse(Utility.Http.Verb.POST, String.Format("http://thefile.me/{0}", id), postDataGenerateLink).Html

            Dim splitDirectLink As String = GetBetween(postRequesGenerateLinktHTML, "<h3 class=""x"">", "</h3>")
            Dim DirectLink As String = Regex.Match(splitDirectLink, "<a href="".*"">(.*)</a>").Groups(1).Value

            Return DirectLink
        End With
    End Function
    Public Function GetTheVideoLink(externalURL As String) As String
        With HTTP


            Dim Headers As New NameValueCollection()
            Headers.Add("Connection", "keep-alive")

            .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0"
            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
            .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
            .AcceptEncoding = "gzip, deflate"
            .AcceptCharset = String.Empty


            Dim HTML As String = GetRealURLAndResponse(externalURL)


            Dim id As String = Regex.Match(HTML, "<input type=""hidden"" name=""id"" value=""(.*)"">").Groups(1).Value
            Dim hash As String = Regex.Match(HTML, "<input type=""hidden"" name=""hash"" value=""(.*)"">").Groups(1).Value

            Dim requestHTML As String = .GetResponse(Utility.Http.Verb.GET, String.Format("http://www.thevideo.me/download/{0}/n/{1}", id, hash)).Html
            Dim DirectLink As String = Regex.Match(requestHTML, "<a href=""(.*)"">Direct Download Link</a>").Groups(1).Value

            Return DirectLink
        End With
    End Function
    Public Function GetGorriladVidLink(externalURL As String) As String
        With HTTP

            Dim RealURL As String = GetRealURL(externalURL)

            Dim urlID As String = New Uri(RealURL).Segments(1)
            Dim embedURL As String = String.Format("http://gorillavid.in/embed-{0}-960x480.html", urlID)

            Dim HTML As String = .GetResponse(Utility.Http.Verb.GET, embedURL).Html

            Dim DirectLink As String = Regex.Match(HTML, "file: ""(.*)"",").Groups(1).Value

            If Not String.IsNullOrEmpty(DirectLink) Then
                'I dunno what would happen if the .mp4 file exists
                If Not DoesImageExistRemotely(DirectLink) Then
                    DirectLink = String.Empty
                End If
            End If

            Return DirectLink
        End With
    End Function
    Public Function GetPromptFileLink(externalURL As String) As String
        With HTTP

            Dim Headers As New NameValueCollection()
            Headers.Add("Connection", "keep-alive")

            .Referer = externalURL
            .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0"
            .AcceptEncoding = "gzip, deflate"
            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"

            Dim RealURL As String = GetRealURL(externalURL)
            Dim HTML As String = .GetResponse(Utility.Http.Verb.GET, RealURL, String.Empty, Headers).Html

            Dim chash As String = Regex.Match(HTML, "<input type=""hidden"" name=""chash"" value=""(.*)"" />").Groups(1).Value

            Dim postResponse As String = .GetResponse(Utility.Http.Verb.POST, RealURL, "chash=" & chash).Html

            Dim DirectLink As String = Regex.Match(postResponse, "<a href=""(.*)"" target=""_blank"" class=""view_dl_link"">Download File</a>").Groups(1).Value

            'HTTP.AutoRedirect = True
            'Dim ht As Utility.Http.HttpResponse = .GetResponse(Utility.Http.Verb.GET, DirectLink)
            'DirectLink = ht.ResponseUri

            Return DirectLink
            'promptfile.com 

            '<div class="index_item index_item_ie"><a href="/watch-10211-Home-and-Away" title="Watch Home and Away (1988)"><img src="//images.primewire.ag/thumbs/10211_Home_and_Away_1988.jpg" border="0" width="150" height="225" alt="Watch Home and Away"><h2>Home and Away (1988)</h2></a><div class="index_ratings"><div id="unit_long10211"><ul style="width: 100px;" class="unit-rating"><li style="width: 62px;" class="current-rating">Current rating.</li><li class="r1-unit"></li><li class="r2-unit"></li><li class="r3-unit"></li><li class="r4-unit"></li><li class="r5-unit"></li></ul></div></div>
            '<div class="index_item index_item_ie"><a href="/watch-1253-Home-Alone" title="Watch Home Alone (1990)"><img src="//images.primewire.ag/thumbs/1253_Home_Alone_1990.jpg" border="0" width="150" height="225" alt="Watch Home Alone"><h2>Home Alone (1990)</h2></a><div class="index_ratings"><div id="unit_long1253"><ul style="width: 100px;" class="unit-rating"><li style="width: 86px;" class="current-rating">Current rating.</li><li class="r1-unit"></li><li class="r2-unit"></li><li class="r3-unit"></li><li class="r4-unit"></li><li class="r5-unit"></li></ul></div></div>
        End With
    End Function

    Public QueryText As String = String.Empty
    Private Sub GetCompletionWorker(asyncOp As AsyncOperation, query As String)
        Try
            With HTTP

                Dim Items As New List(Of DropDownMenu.Item)

                Dim HTML As String = .GetResponse(Utility.Http.Verb.GET, String.Format("{0}/index.php?search_keywords={1}&search_section=2", BaseURL, query)).Html
                Dim ShowsHTML As String = GetBetweenAll(HTML, "<div class=""clearer""></div>", "<div class=""clearer""></div>")(1)

                Dim regexShow As MatchCollection = Regex.Matches(HTML, "<a href=""(.*)"" title=""Watch (.*)""><img src=""(.*)"" border=""0"" width=""150"" height=""225"" alt="".*""><h2>.*</h2></a>")
                Dim regexGenre As MatchCollection = Regex.Matches(HTML, "<div class=""item_categories""><a href="".*"">(.*)</a> </div></div")

                Dim genreIndex As Integer = 0

                Dim MaxItem As Integer = 5
                Dim ItemCount As Integer = 1

                For Each M As Match In regexShow
                    If ItemCount <= MaxItem Then
                        Dim showTitle As String = M.Groups(2).Value.Split("("c)(0).Trim()
                        Dim showLink As String = BaseURL & M.Groups(1).Value
                        Dim showImageURL As String = "http://" & M.Groups(3).Value.Remove(0, 2)
                        Dim showGenre As String = regexGenre.Item(genreIndex).Groups(1).Value

                        If QueryText <> query Then 'Avoid downloading all images if user has already typed the rest of his request
                            Debug.WriteLine("Request aborted: {0}", query)
                            Return
                        End If

                        If DoesImageExistRemotely(showImageURL) Then
                            Dim ImageStream As New IO.MemoryStream(New WebClient().DownloadData(showImageURL))
                            Items.Add(New DropDownMenu.Item(showTitle, showGenre, New System.Drawing.Bitmap(ImageStream)) With {.Link = showLink})
                        End If

                        If genreIndex + 1 < regexGenre.Count Then genreIndex += 1
                        ItemCount += 1
                    Else
                        Exit For
                    End If
                Next
                asyncOp.PostOperationCompleted(AddressOf OnCompletionCompleted, New SearchCompletionCompletedArgs With {.Result = Items, .Exception = Nothing, .Query = query})
            End With
        Catch ex As Exception
            asyncOp.PostOperationCompleted(AddressOf OnCompletionCompleted, New SearchCompletionCompletedArgs With {.Exception = ex})
        End Try
    End Sub

#End Region

End Class




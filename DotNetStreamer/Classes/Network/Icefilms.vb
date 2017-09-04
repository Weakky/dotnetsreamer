Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports System.Net
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.Web
Imports System.Text
Imports System.Web.Script.Serialization
Imports DotNetStreamer.Service.Captcha.Captcha
Imports DotNetStreamer.Service.Captcha
Imports DotNetStreamer.ShowsMoviesFetcher

Public Class IcefilmsFetcher

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
    'Private Function GetHostFromName(Name As String) As Host
    '    Select Case Name
    '        Case "thefile.me"
    '            Return Host.TheFile

    '        Case "thevideo.me"
    '            Return Host.TheVideo

    '        Case "promptfile.com"
    '            Return Host.Promptfile

    '        Case "gorillavid.in"
    '            Return Host.Gorillavid

    '        Case "sharesix.com"
    '            Return Host.Sharesix

    '        Case "bestreams.net"
    '            Return Host.Bestreams
    '    End Select
    'End Function
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
    Public Shared Function ShowToTreeNode(S As Query) As TreeNode
        Dim TN As New TreeNode(S.Name) With {.Tag = S}

        For Each Season As Season In S.Seasons
            Dim TNSeason As New TreeNode(Season.Name)

            For Each Episode As Episode In Season.Episodes
                Dim TNEpisode As New TreeNode(Episode.Name) With {.Tag = Episode}
                TNSeason.Nodes.Add(TNEpisode)
            Next
            TN.Nodes.Add(TNSeason)
        Next

        Return TN
    End Function
#End Region

#Region "Fields"

    Private Const BaseURL As String = "http://www.icefilms.info"
    ' Private Key As String = "7bff31456f397846"
    Private HTTP As New Utility.Http
    Public Shared Hosts() As String = {"Automatic", "Thefile.me", "Thevideo.me", "Bestreams.net", "Sharesix.com", "Gorillavid.in", "Promptfile.com"}

#End Region


#Region "Events, Delegates & Callbacks"

    Public Event SearchCompleted(ByVal e As SearchCompletedArgs)
    Private Delegate Sub WorkerDelegate(ByVal asyncOp As AsyncOperation, ByVal query As String)
    Private Sub OnSearchCompleted(ByVal e As SearchCompletedArgs)
        RaiseEvent SearchCompleted(e)
    End Sub


    Public Event OnVideoLinkDownloaded(e As LinkDownloadedEventArgs)
    Private Delegate Sub DownloadLinkDelegate(ByVal AsyncOp As AsyncOperation, ByVal link As String, Host As IcefilmsHost, HDQuality As Boolean, Captcha As Captcha)
    Private Sub LinkDownloaded(e As LinkDownloadedEventArgs)
        RaiseEvent OnVideoLinkDownloaded(e)
    End Sub

#End Region


#Region "Public Methods"

    Public Sub Search(ByVal query As String)
        Dim worker As New WorkerDelegate(AddressOf SearchWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, query, Nothing, Nothing)
    End Sub

    Public Sub GetLinks(ByVal link As String, Host As IcefilmsHost, HDQuality As Boolean, Captcha As Captcha)
        Dim worker As New DownloadLinkDelegate(AddressOf GetLinksWorker)
        Dim asyncop As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncop, link, Host, HDQuality, Captcha, Nothing, Nothing)
    End Sub


#End Region

#Region "Private Methods"

    Private Sub SearchWorker(asyncOp As AsyncOperation, PWquery As String)
        With HTTP

            Dim jsonResponse As String = .GetResponse(Utility.Http.Verb.GET, String.Format("https://www.googleapis.com/customsearch/v1element?key=AIzaSyCVAXiUzRYsML1Pv6RwSG1gunmMikTzQqY&rsz=filtered_cse&num=10&hl=fr&prettyPrint=true&source=gcsc&gss=.com&sig=23952f7483f1bca4119a89c020d13def&cx=010591583107216882486:bafpv02vxuq&q={0}", PWquery.Replace(" ", "%20"))).Html

            Dim searchResultLink As String = jsonResponse.Substring(jsonResponse.IndexOf("""unescapedUrl"":") + 17).Split(Chr(34))(0)

            If Not searchResultLink.Contains("tv/series/") Then
                Dim episodeHTMLContent As String = .GetResponse(Utility.Http.Verb.GET, searchResultLink).Html
                Dim preShowLink As String = episodeHTMLContent.Substring(episodeHTMLContent.IndexOf("Previous Episode</a> | ") + 31).Split(">"c)(0)
                searchResultLink = BaseURL & preShowLink
            End If

            Dim showHTMLContent As String = .GetResponse(Utility.Http.Verb.GET, searchResultLink).Html

            Dim showTitle As String = showHTMLContent.Substring(showHTMLContent.IndexOf("<h1>") + 4).Split("<"c)(0)

            Dim Query As New Query(showTitle)

            GetSeasonsAndEpisodes(asyncOp, Query, showHTMLContent)
        End With
    End Sub
    Private Sub GetSeasonsAndEpisodes(asyncOP As AsyncOperation, query As Query, episodeHTML As String)
        With HTTP


            Dim seasonsHTML As String() = Regex.Split(episodeHTML, "<a name=[0-9]+>").Skip(1).ToArray()
            Dim Seasons As Season = Nothing

            For Each seasonHTML As String In seasonsHTML

                Dim seasonName As String = seasonHTML.Substring(4).Split("<"c)(0).Trim()
                Dim episodesHTML As String() = Split(seasonHTML, "<a href=").Skip(2).ToArray()

                Seasons = New Season(seasonName)

                Debug.WriteLine(seasonName)
                Debug.WriteLine("----------")

                For Each episodeSplit As String In episodesHTML

                    If episodeSplit.StartsWith("/ip.php?v=") Then

                        '/ip.php?v=158369&>1x01 Pilot</a><b>HD</b><br><img class=star>

                        Dim episodeLink As String = String.Format("http://www.icefilms.info/membersonly/components/com_iceplayer/video.php?h=377&w=626&vid={0}&img=", episodeSplit.Substring(episodeSplit.IndexOf("v=") + 2).Split("&"c)(0))  'BaseURL & Split(episodeSplit, "&>")(0)
                        Dim episodeName As String = Split(episodeSplit, ">")(1).Split("<"c)(0)


                        Dim episodeNumber As String = Regex.Match(episodeName, "[0-9]+x([0-9]+)").Groups(1).Value
                        episodeName = Regex.Replace(episodeName, "^([0-9]+x[0-9]+)", "E" & episodeNumber & " - ")

                        '1x01 Pilot: Part 1

                        Dim isHD As Boolean = episodeSplit.Contains("<b>HD</b>")

                        Seasons.Episodes.Add(New Episode(episodeName, episodeLink) With {.HD = isHD})

                        Debug.WriteLine(episodeName & "/" & episodeLink)

                    End If
                Next
                query.Seasons.Add(Seasons)
            Next

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


    Private Function GetHosts(episodeURL As String, HDQuality As Boolean) As HostEpisode()
        With HTTP


            Dim HostList As New List(Of HostEpisode)()
            Dim HTML As String = .GetResponse(Utility.Http.Verb.GET, episodeURL).Html


            Dim hostsHTML As String = .GetResponse(Utility.Http.Verb.GET, episodeURL).Html


            Dim qualitiesHost As String() = Split(hostsHTML, "<div class=ripdiv>").Skip(1).ToArray()

            Dim HDHostTest As String = String.Empty

            If Not qualitiesHost(0).StartsWith("<b>DVDRip / Standard Def</b>") Then
                HDHostTest = If(HDQuality, qualitiesHost(0), qualitiesHost(1))
            Else
                HDHostTest = qualitiesHost(0)
            End If

            Dim hostsTest As String() = Split(HDHostTest, "<a rel=").Skip(1).ToArray()

            Dim videoKey As String = HttpUtility.ParseQueryString(episodeURL)("vid")

            For Each hostR As String In hostsTest

                Dim id As String = hostR.Substring(hostR.LastIndexOf("onclick='go(") + 12).Split(")"c)(0)

                If hostR.Contains("orangered") AndAlso hostR.Contains("white") Then
                    HostList.Add(New HostEpisode() With {.Name = "180Upload", .ID = id, .Key = videoKey})

                ElseIf hostR.Contains("orange") AndAlso hostR.Contains("#666") Then
                    HostList.Add(New HostEpisode() With {.Name = "Movreel", .ID = id, .Key = videoKey})

                ElseIf hostR.Contains("#0be") AndAlso hostR.Contains("orangered") Then
                    HostList.Add(New HostEpisode() With {.Name = "BillionUploads.com", .ID = id, .Key = videoKey})

                ElseIf hostR.Contains("lightgreen") AndAlso hostR.Contains("green") Then
                    HostList.Add(New HostEpisode() With {.Name = "Hugefiles.net", .ID = id, .Key = videoKey})
                End If

            Next

            Return HostList.ToArray()
        End With
    End Function

    Private Function GeneratePostData(id As String, t As String) As String
        Dim m As String = (New Random().Next(100, 300) * -1).ToString()
        Dim s As String = (New Random().Next(0, 5)).ToString()

        Dim postDataSB As New StringBuilder()
        postDataSB.Append("id=" & id)
        postDataSB.Append("&s=" & s)
        postDataSB.Append("&iqs=")
        postDataSB.Append("&url=")
        postDataSB.Append("&m=" & m)
        postDataSB.Append("&cap=")
        postDataSB.Append("&sec=" & "37fn8Oklq")
        postDataSB.Append("&t=" & t)
        Return postDataSB.ToString()
    End Function


    Private Function GrabLinkFromHostID(id As String, t As String) As String
        With HTTP
            Dim Headers As New NameValueCollection()
            Headers.Add("Connection", "keep-alive")
            Headers.Add("Pragma", "no-cache")
            Headers.Add("Cache-Control", "no-cache")

            .ContentType = "application/x-www-form-urlencoded"
            .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0"
            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
            .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
            .AcceptEncoding = "gzip, deflate"


            Dim postData As String = String.Empty
            Dim response As String = String.Empty

            Do Until Not String.IsNullOrEmpty(response)
                postData = GeneratePostData(id, t)
                response = .GetResponse(Utility.Http.Verb.POST, "http://www.icefilms.info/membersonly/components/com_iceplayer/video.phpAjaxResp.php", postData, Headers).Html
            Loop

            If String.IsNullOrEmpty(response) Then Debug.WriteLine("LINK NOT GIVEN!!!!!!")

            Dim hostURL As String = HttpUtility.UrlDecode(Split(response, "url=")(1))
            Return hostURL
        End With
    End Function



    Private Sub GetLinksWorker(ByVal AsyncOp As AsyncOperation, ByVal link As String, ByVal Host As IcefilmsHost, HDQuality As Boolean, Captcha As Captcha)
        Try
            With HTTP

                Dim AvailableHost As List(Of HostEpisode) = GetHosts(link, HDQuality).ToList()
                Dim e As New LinkDownloadedEventArgs With {.IcefilmsHost = Host, .Link = String.Empty}
                Dim externalURL As String = String.Empty

                Select Case Host

                    Case IcefilmsHost.Automatic
                        e = GetLinkAutomatic(link, AvailableHost.ToArray())

                    Case IcefilmsHost.Hugefiles
                        Dim hostTemp As HostEpisode = AvailableHost.Find(Function(x) x.Name = "Hugefiles.net")
                        externalURL = GrabLinkFromHostID(hostTemp.ID, hostTemp.Key)

                        If externalURL <> String.Empty Then e.Link = GetHugefileLink(Captcha, externalURL)

                    Case IcefilmsHost.HundredUpload
                        externalURL = AvailableHost.Find(Function(x) x.Name = "bestreams.net").ExternalURL
                        ' If externalURL <> String.Empty Then e.Link = GetBestStreamsLink(externalURL)

                    Case IcefilmsHost.BillionsUpload
                        Dim hostTemp As HostEpisode = AvailableHost.Find(Function(x) x.Name = "BillionUploads.com")
                        externalURL = GrabLinkFromHostID(hostTemp.ID, hostTemp.Key)
                        If externalURL <> String.Empty Then e.Link = GetBillionUploadsLink(Captcha, externalURL)
                End Select


                AsyncOp.PostOperationCompleted(AddressOf LinkDownloaded, e)
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetLinkAutomatic(link As String, hostsEpisodes As HostEpisode()) As LinkDownloadedEventArgs
        Dim URL As String = String.Empty

        Dim listHosts As List(Of HostEpisode) = hostsEpisodes.ToList()

        'URL = GetTheFileMeLink(listHosts.Find(Function(x) x.Name = "thefile.me").ExternalURL)

        If String.IsNullOrWhiteSpace(URL) Then
            For Each Host As HostEpisode In hostsEpisodes
                Select Case Host.Name

                    'Case "thefile.me"
                    '    URL = GetTheFileMeLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

                    'Case "thevideo.me"
                    '    URL = GetTheVideoLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

                    'Case "promptfile.com"
                    '    URL = GetPromptFileLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

                    'Case "gorillavid.in"
                    '    URL = GetGorriladVidLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

                    ' Case "sharesix.com"
                    '    URL = GetShareSixLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

                    '   Case "bestreams.net"
                    '    URL = GetBestStreamsLink(listHosts.Find(Function(x) x.Name = Host.Name).ExternalURL)

                End Select

                If URL = String.Empty Then Continue For

                Debug.WriteLine("=======> " & Host.Name & " " & URL)
                ' Return New LinkDownloadedEventArgs() With {.AutomaticHost = True, .Link = URL, .Host = GetHostFromName(Host.Name)}
            Next
        End If

        Return New LinkDownloadedEventArgs() With {.Link = String.Empty}
    End Function

    Private Function GetHugefileLink(Captcha As Captcha, URL As String) As String

        With HTTP

            Dim htmlContent As String = .GetResponse(Utility.Http.Verb.GET, URL).Html

            Dim rand As String = Regex.Match(htmlContent, "<input type=""hidden"" name=""rand"" value=""(.*)"">").Groups(1).Value
            Dim id As String = Regex.Match(htmlContent, "<input type=""hidden"" name=""id"" value=""(.*)"">").Groups(1).Value
            Dim fname As String = Regex.Match(htmlContent, "<input type=""hidden"" name=""fname"" value=""(.*)"">").Groups(1).Value

            Dim postData As String = Uri.EscapeUriString(String.Format("op=download1&usr_login=&rand={0}&id={1}&fname={2}&referer=&ctype=8&adcopy_response={3}&adcopy_challenge={4}&method_free=tel%3Fchargement+libre+", rand, id, fname, Captcha.Solution, Captcha.challenge))

            Dim Headers As New NameValueCollection()
            Headers.Add("Connection", "keep-alive")

            .ContentType = "application/x-www-form-urlencoded"
            .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:34.0) Gecko/20100101 Firefox/34.0"
            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
            .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
            .AcceptEncoding = "gzip, deflate"

            Dim response As String = .GetResponse(Utility.Http.Verb.POST, URL, postData).Html

            Dim directLink As String = response.Substring(response.IndexOf("var fileUrl = ") + 15).Split(ChrW(34))(0)
            Return directLink
        End With


    End Function

    Private Function GetBillionUploadsLink(Captcha As Captcha, URL As String) As String

        With HTTP

            .UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0"
            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
            .AcceptLanguage = "en-US,en;q=0.5"
            .AcceptEncoding = ""

            Dim captchaURL As String = String.Empty

            Dim html As String = .GetResponse(Utility.Http.Verb.GET, URL).Html

            Dim b As String = Regex.Match(html, "var z="""";var b=""(.*)""").Groups(1).Value.Split(ChrW(34))(0)
            Dim z As New StringBuilder()
            If Not String.IsNullOrEmpty(b) Then
                For i As Integer = 0 To b.Length - 1 Step 2
                    Dim text As String = b.Substring(i, 2)
                    If Not String.IsNullOrEmpty(text) Then
                        Dim number As Integer = Integer.Parse(text, Globalization.NumberStyles.HexNumber).ToString()
                        z.Append(ChrW(number))
                    End If
                Next

                Dim decoded As String = z.ToString()
                captchaURL = "http://billionuploads.com" & decoded.Substring(decoded.IndexOf("GET") + 6).Split(ChrW(34))(0)

                .GetResponse(Utility.Http.Verb.GET, captchaURL)

                html = .GetResponse(Utility.Http.Verb.GET, URL).Html
            End If

            Dim id As String = URL.Split("/"c)(3)
            Dim blader As String = Regex.Match(html, "<textarea source=""self"" style=""display: none;visibility: hidden"">(.*)</textarea>").Groups(1).Value

            Dim postD As String = String.Format("op=download2&id={0}&referer=&method_free=&method_premium=&down_direct=1&submit_btn=&airman=toast&blader={1}", id, blader)

            Dim postResponse As String = .GetResponse(Utility.Http.Verb.POST, URL, postD).Html

            Dim encodedLink As String = Regex.Match(postResponse, "<input type=""hidden"" id=""dl"" value=""(.*)"">").Groups(1).Value

            If String.IsNullOrEmpty(encodedLink) Then MessageBox.Show("DID NOT GRAB THE ENCODED LINK!")
            encodedLink = Split(encodedLink, "GvaZu")(1)

            Dim decodedlink As String = DecryptLink(DecryptLink(encodedLink))

            Return decodedlink

            'captchaURL = "http://billionuploads.com" & html.Substring(html.IndexOf("<iframe src=") + 13).Split(ChrW(34))(0)

            '.Referer = URL
            'Dim htmlContent As String = .GetResponse(Utility.Http.Verb.GET, captchaURL).Html

            'Dim captchaURL2 As String = "http://billionuploads.com" & Regex.Match(htmlContent, "<form action=""(.*)"" method=""post"" id=""captcha-form"">").Groups(1).Value

            'Dim postData As String = String.Format("recaptcha_challenge_field={0}&recaptcha_response_field={1}", Captcha.challenge, Captcha.Solution)
            '.Referer = HttpUtility.UrlEncode(captchaURL)
            'Dim response As String = .GetResponse(Utility.Http.Verb.POST, captchaURL2, postData).Html


            'Dim finalresponse As String = .GetResponse(Utility.Http.Verb.GET, URL).Html
            'Dim directLink As String = response.Substring(response.IndexOf("var file_link = '") + 17).Split("'")(0)
            'Return directLink
        End With

    End Function



    Private Function DecryptLink(link As String) As String
        Dim S As String = String.Empty
        Dim i As New StringBuilder()
        Dim u()() As Integer = {New Integer() {65, 91}, New Integer() {97, 123}, New Integer() {48, 58}, New Integer() {43, 44}, New Integer() {47, 48}}
        Dim t As New Dictionary(Of String, Integer)

        For z As Integer = 0 To u.Length - 1
            For n As Integer = u(z)(0) To u(z)(1)
                i.Append(Chr(n))
            Next
        Next

        i.Replace("[", String.Empty)
        i.Replace("{", String.Empty)

        For n As Integer = 0 To 63
            t.Add(i.Chars(n), n)
        Next

        For n As Integer = 0 To link.Length Step 72
            Dim a, c As Integer

            Dim h As String = String.Empty

            If n + 72 > link.Length Then
                h = link.Substring(n, link.Length - n)
            Else
                h = link.Substring(n, 72)
            End If


            For l As Integer = 0 To h.Length - 1

                Dim f As Integer = 0

                If t.ContainsKey(h(l)) Then
                    f = t(h(l))
                Else
                    Continue For
                End If

                a = (a << 6) + Integer.Parse(f)
                c += 6

                While (c >= 8)
                    c -= 8
                    S = S & Chr((ZFRS(a, c)) Mod 256)
                End While

            Next
        Next
        Return S
    End Function

    'Zero Fill Right Shift
    Private Shared Function ZFRS(i As Integer, j As Integer) As Integer
        Dim maskIt As Boolean = (i < 0)
        i = i >> j
        If maskIt Then
            i = i And &H7FFFFFFF
        End If
        Return i
    End Function

#End Region

End Class

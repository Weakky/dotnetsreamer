Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.IO
Imports System.Threading
Imports System.ComponentModel
Imports System.Collections.Specialized
Imports System.Web
Imports System.Xml.Serialization
Imports System.Reflection

Public Class SeriesClass

    'Developped by TRANSLU6DE and Mavamaarten~
    '28/10/12
    'Released on : 30/10/12
    'Hackforums.net

#Region "Properties"

    Dim _LinkOfCurrentEpisode As String
    Public Property LinkOfCurrentEpisode As String
        Get
            LinkOfCurrentEpisode = _LinkOfCurrentEpisode
        End Get
        Set(value As String)
            _LinkOfCurrentEpisode = value
        End Set
    End Property
#End Region


#Region "Search function"
    Structure SearchCompletedArgs
        Property exception As Exception
        Property NoResults As Boolean
        Property ShowInformations As Show
        Property TreeNode As TreeNode
    End Structure

    Public Structure Show
        Public Name As String
        Public Description As String
        Public ImdbRate As String
        Public Length As String
        Public ThumbnailURL As String
    End Structure
    <Serializable> _
    Public Structure Episode
        Public Property Name As String
        Public Property Season As String
        Public Property Show As String
        Public Property Link As String

        Sub New(_Name As String, _Season As String, _Show As String, _Link As String)
            Name = _Name : Season = _Season : Show = _Show : Link = _Link
        End Sub

    End Structure

    Private fname As String

    Dim HTTP As New Utility.Http
    Public Event SearchCompleted(ByVal e As SearchCompletedArgs)

    Delegate Sub WorkerDelegate(ByVal asyncOp As AsyncOperation, ByVal query As String)
    Public Sub Search(ByVal query As String)
        Dim worker As New WorkerDelegate(AddressOf SearchWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, query, Nothing, Nothing)
    End Sub


    Private Sub SearchWorker(ByVal asyncOp As AsyncOperation, ByVal query As String)
        Try
            With HTTP

                'Quick fix with non working shows ..
                If query.Contains("Grey") Then query = "Grey" 'Fix Grey's Anatomy
                query = query.Replace("...", String.Empty)
                If query.Contains("Agents of S") Then query = "Agents of SHIELD"
                '--------------------------------------------------

                Dim Source As String = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/rpc.php", "queryString=" & query).Html

                Dim linkep As String = Regex.Match(Source, "<span class=""category"">TV SHOWS</span><a href=""(.*)"">").Groups(1).Value
                linkep = linkep.Remove(linkep.IndexOf(Chr(34)))

                Dim showname As String = Split(Source, "searchheading" & ChrW(34) & ">")(1).Split("<")(0)

                Dim sourceEpisode As String = .GetResponse(Utility.Http.Verb.GET, linkep).Html

                Dim parts() As String = GetBetweenAll(sourceEpisode, "<h4  ><a class=", "<h4  ><a class=")
                Dim seasons As String = String.Empty

                Dim description As String = Regex.Match(sourceEpisode, "<meta name=""description"" content=""(.*)"" />").Groups(1).Value 'Description of show
                Dim thumbnailURL As String = Regex.Match(sourceEpisode, "<link rel=""image_src"" href=""(.*)""/>").Groups(1).Value 'URL of thumbnail of image show
                Dim imdb As String = "IMDb " & Regex.Match(sourceEpisode, "<span itemprop=""ratingValue"">(.*)</span>").Groups(1).Value.Split("<"c)(0)
                Dim length As String = Regex.Match(sourceEpisode, "<time itemprop=""duration"" datetime="".*"">TV Series - (.*)</time>").Groups(1).Value

                If description.Contains("href") Then
                    description = Split(description, "<")(0)
                End If

                Dim ShowNode As TreeNode
                Dim ShowInformations As Show = New Show With {.Name = showname, .Description = description, .ThumbnailURL = thumbnailURL, .ImdbRate = imdb, .Length = length}

                If Not showname = String.Empty Then
                    ShowNode = New TreeNode(showname.Trim)
                    ShowNode.Tag = ShowInformations
                Else
                    asyncOp.PostOperationCompleted(AddressOf OnSearchCompleted, New SearchCompletedArgs With {.exception = Nothing, .NoResults = True})
                    Exit Sub
                End If

                Dim listNode As New List(Of TreeNode)
                Dim list As New List(Of String)

                For Each part As String In parts
                    list.Clear()
                    Dim links As MatchCollection = Regex.Matches(part, "<div><em>.*</em><span class='epnomber'><a href='(.*)'>(.*)</a></span><span class='epname'><span class='halfShad'></span> <a href='.*'>(.*)</a></span></div>")
                    seasons = Regex.Match(part, "href="""" onclick="".*"">(.*)</a></h4>").Groups(1).Value

                    ShowNode.Nodes.Add(part, seasons.Trim) 'Adding All seasons into the TreeNodes of Name Show


                    For Each m As Match In links
                        Dim episode As String = String.Format("{0} - {1}", m.Groups(2).Value, m.Groups(3).Value)
                        Dim link As String = m.Groups(1).Value
                        list.Add(episode + "///" + link)  'Adding all elements into an array to reverse the items of the TreeView.
                        'Not the most accurate way to do it though.
                    Next

                    list.Reverse()

                    For i = 0 To list.Count - 1
                        Dim episode As String = Regex.Match(list(i), "(.*)///(.*)").Groups(1).Value
                        Dim link As String = Regex.Match(list(i), "(.*)///(.*)").Groups(2).Value
                        ShowNode.Nodes.Find(part, True)(0).Nodes.Add(link, episode, link)
                    Next

                Next
                asyncOp.PostOperationCompleted(AddressOf OnSearchCompleted, New SearchCompletedArgs With {.exception = Nothing, .NoResults = False, .ShowInformations = ShowInformations, .TreeNode = ShowNode})
            End With
        Catch ex As Exception
            asyncOp.PostOperationCompleted(AddressOf OnSearchCompleted, New SearchCompletedArgs With {.exception = ex, .NoResults = True})
            Exit Sub
        End Try
    End Sub



    Private Sub OnSearchCompleted(ByVal e As SearchCompletedArgs)
        RaiseEvent SearchCompleted(e)
    End Sub
#End Region

#Region "Get All Series on Launch Control"
    Structure SearchTopAndNewSeriesCompletedArgs
        Property exception As Exception
        Property Categories As Categories
        Property Result As List(Of SeriesView.Item)
        Property UpdatedList As List(Of String())
    End Structure

    Public Enum Categories
        Movies = 1
        Series = 2
    End Enum

    Public Event SearchTopAndNewSeriesCompleted(ByVal e As SearchTopAndNewSeriesCompletedArgs)

    Delegate Sub WorkerDelegateTopSeries(ByVal asyncOp As AsyncOperation)
    Public Sub GetTopSeries()
        Dim worker As New WorkerDelegateTopSeries(AddressOf GetTopSeriesWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, Nothing, Nothing)
    End Sub

    Delegate Sub WorkerDelegateNewSeries(ByVal asyncOp As AsyncOperation)
    Public Sub GetNewSeries()
        Dim worker As New WorkerDelegateNewSeries(AddressOf GetNewSeriesWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, Nothing, Nothing)
    End Sub

#Region "Get Top Series"

    Private TopSeriesList As New List(Of SeriesView.Item)

    Private Sub GetTopSeriesWorker(asyncOp As AsyncOperation)
        Try
            Using wc As New WebClient

                Dim HTML As String = wc.DownloadString("http://watchseries.biz/top-series.html")


                Dim movieregex As MatchCollection = Regex.Matches(HTML, "<div class='mov1'><a title='.*' href='(.*)'>(.*)</a>.*</div>")
                Dim thumbnailregex As MatchCollection = Regex.Matches(HTML, " <div class='mov0'><a title='.*' href='.*'><img src='(.*)' width='.*' height='.*' alt='' ></img></a></div>")
                Dim imdbandlength As MatchCollection = Regex.Matches(HTML, "<div class='mov2a'>(.*)</div><div class='mov2b'><a href='(.*)' rel='external nofollow'>(.*) </a></div>")

                For i = 0 To movieregex.Count - 1
                    Dim Showname As String = movieregex.Item(i).Groups(2).Value
                    Dim Thumbnail As String = thumbnailregex.Item(i).Groups(1).Value
                    Dim length As String = imdbandlength.Item(i).Groups(1).Value
                    Dim imdbnote As String = imdbandlength.Item(i).Groups(3).Value

                    Dim ImageStream As New IO.MemoryStream(wc.DownloadData(Thumbnail))

                    TopSeriesList.Add(New SeriesView.Item(HttpUtility.HtmlDecode(Showname), imdbnote, length, Thumbnail, New System.Drawing.Bitmap(ImageStream), HttpUtility.HtmlDecode(Showname)))
                    ImageStream.Dispose()
                Next
            End Using
            asyncOp.PostOperationCompleted(AddressOf SearchTopNewSeriesCompleted, New SearchTopAndNewSeriesCompletedArgs With {.Categories = Categories.Movies, .Result = TopSeriesList, .UpdatedList = Nothing, .exception = Nothing})
        Catch ex As Exception
            asyncOp.PostOperationCompleted(AddressOf SearchTopNewSeriesCompleted, New SearchTopAndNewSeriesCompletedArgs With {.exception = ex})
        End Try
    End Sub
#End Region
#Region "Get New Series"

    Private NewSeriesList As New List(Of SeriesView.Item)
    Private updateList As New List(Of String())

    Private Sub GetNewSeriesWorker(ByVal asyncOp As AsyncOperation)
        With HTTP
            .TimeOut = 30000
            Dim wc As New WebClient()
            Dim response As Utility.Http.HttpResponse = .GetResponse(Utility.Http.Verb.GET, "http://watchseries.biz/")

            If Not IsNothing(response.Error) Then
                asyncOp.PostOperationCompleted(AddressOf SearchTopNewSeriesCompleted, New SearchTopAndNewSeriesCompletedArgs With {.exception = New Exception(), .Categories = Categories.Series})
                Return
            End If

            Dim source As String = response.Html

            Dim movieregex As MatchCollection = Regex.Matches(source, "<div class='mov1'><a title='.*' href='(.*)'>(.*)</a>.*</div>")
            Dim thumbnailregex As MatchCollection = Regex.Matches(source, "<div class='mov0' style='position: relative;'><a title='.*' href='.*'><img src='(.*)' width='.*' height='.*' alt='.*' >(.*)")
            Dim imdbandlength As MatchCollection = Regex.Matches(source, "<div class='mov2a'>(.*)</div><div class='mov2b'><a href='(.*)' rel='external nofollow'>(.*) </a></div>")

            For i = 0 To movieregex.Count - 1

                Dim Showname As String = movieregex.Item(i).Groups(2).Value
                Dim Thumbnail As String = thumbnailregex.Item(i).Groups(1).Value
                Dim updated As String = If(thumbnailregex.Item(i).Groups(2).Value.Contains("updated"), "updated", String.Empty)
                Dim length As String = imdbandlength.Item(i).Groups(1).Value
                Dim imdbnote As String = imdbandlength.Item(i).Groups(3).Value

                Dim ImageStream As Stream = Nothing

                Try
                    ImageStream = New IO.MemoryStream(wc.DownloadData(Thumbnail))
                Catch ex As WebException
                    My.Resources.noimage.Save(ImageStream, Imaging.ImageFormat.Png)
                End Try

                NewSeriesList.Add(New SeriesView.Item(HttpUtility.HtmlDecode(Showname), imdbnote, length, Thumbnail, New System.Drawing.Bitmap(ImageStream), HttpUtility.HtmlDecode(Showname)))
                updateList.Add({HttpUtility.HtmlDecode(Showname), updated})

                ImageStream.Dispose()
                wc.Dispose()
            Next
        End With
        asyncOp.PostOperationCompleted(AddressOf SearchTopNewSeriesCompleted, New SearchTopAndNewSeriesCompletedArgs With {.Categories = Categories.Series, .Result = NewSeriesList, .UpdatedList = updateList, .exception = Nothing})
    End Sub

    Private Sub SearchTopNewSeriesCompleted(e As SearchTopAndNewSeriesCompletedArgs)
        RaiseEvent SearchTopAndNewSeriesCompleted(e)
    End Sub

#End Region

#End Region

#Region "AutoCompletion Search"
    Structure SearchCompletionArgs
        Property exception As Exception
        Property Result As List(Of DropDownMenu.Item)
    End Structure

    Public Event SearchCompletionCompleted(ByVal e As SearchCompletionArgs)

    Delegate Sub WorkerDelegateCompletion(ByVal asyncOp As AsyncOperation, query As String)
    Public Sub GetAutoCompletion(query As String)
        Dim worker As New WorkerDelegateCompletion(AddressOf GetCompletionWorker)
        Dim asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncOp, query, Nothing, Nothing)
    End Sub

    Private Sub GetCompletionWorker(asyncOp As AsyncOperation, query As String)
        With HTTP


            Try
                Dim HTML As String = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/rpc.php", String.Format("queryString={0}", query)).Html

                Dim result() As String = Split(HTML, "<a href=")

                Dim Items As New List(Of DropDownMenu.Item)

                For i As Integer = 1 To result.Length - 2

                    Dim Title As String = GetBetween(result(i), "<span class=""searchheading"">", "<")
                    Dim Auteur As String = GetBetween(result(i), "<span>", "</span></a>")
                    Dim ImageURL As String = GetBetween(result(i), "<img src=" & ChrW(34), ChrW(34)) '<img src="http://watchseries.biz/search_images/701sc.jpg" alt="" />"
                    Dim URL As String = GetBetween(result(i), ChrW(34), ChrW(34))
                    Dim ImageStream As New IO.MemoryStream(New WebClient().DownloadData(ImageURL))

                    Items.Add(New DropDownMenu.Item(Web.HttpUtility.HtmlDecode(Title), Auteur, New System.Drawing.Bitmap(ImageStream)))
                Next

                asyncOp.PostOperationCompleted(AddressOf OnCompletionCompleted, New SearchCompletionArgs With {.Result = Items, .exception = Nothing})
            Catch ex As Exception
                asyncOp.PostOperationCompleted(AddressOf OnCompletionCompleted, New SearchCompletionArgs With {.exception = ex})
            End Try
        End With
    End Sub

    Private Sub OnCompletionCompleted(e As SearchCompletionArgs)
        RaiseEvent SearchCompletionCompleted(e)
    End Sub

    Private Function GetBetween(ByVal Source As String, ByVal str1 As String, ByVal str2 As String) As String
        Try
            Dim a As String = Regex.Split(Source, str1)(1)
            Return Regex.Split(a, str2)(0)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

#End Region

#Region "Get Available Streaming Service"
    Private Function GetAvailableStreamingService() As String()
        With HTTP
            Debug.Print(_LinkOfCurrentEpisode)
            Dim HostsArray As Array = System.Enum.GetValues(GetType(Host))
            For Each Host As Host In HostsArray
                If Host <> SeriesClass.Host.Automatic AndAlso Not String.IsNullOrEmpty(_LinkOfCurrentEpisode) Then
                    Dim LinkID As String = _LinkOfCurrentEpisode.Substring(_LinkOfCurrentEpisode.IndexOf("play-")).Replace("-serie.html", String.Empty).Replace("play-", String.Empty)
                    Dim HTML As String = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host), _LinkOfCurrentEpisode)).Html
                    Debug.Print(String.Format("HOST:{0} >>>>>>> {1}", Hosts(Host), HTML))
                End If
            Next
        End With
        Return Nothing
    End Function
#End Region 'Not completely implemented yet.

#Region "Get Video Link for: Gorillavid, Daclips, Movpod, NowVideo, MovShare, NovaMov Putlocker"

#Region "Fields & Structures"
    Private HTML As String = String.Empty
    Private Embedded_Link As String = String.Empty
    Private LinkID As String = String.Empty

    Private Hosts() As String = {"Automatic", "Movpod", "Gorillavid", "Daclips", "Putlocker", "Nowvideo", "Movshare", "Videoweed", "Vidup", "Moymir", "Flashx"}

    Public Enum Host
        Automatic = 0
        MovPod = 1
        Gorrilavid = 2
        Daclips = 3
        Putlocker = 4
        NowVideo = 5
        Movshare = 6
        Videoweed = 7
        VidUp = 8
        Moymir = 9
        Flashx = 10
    End Enum

    Structure LinkDownloadedEventArgs
        Property Host As Host
        Property AutomaticHost As Boolean
        Property Link As String
    End Structure

#End Region

    Public Event OnVideoLinkDownloaded(e As LinkDownloadedEventArgs)
    Private Delegate Sub DownloadLinkDelegate(ByVal AsyncOp As AsyncOperation, ByVal link As String, Host As Host)

    Public Sub GetLinks(ByVal link As String, Host As Host)
        Dim worker As New DownloadLinkDelegate(AddressOf GetLinksWorker)
        Dim asyncop As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncop, link, Host, Nothing, Nothing)
    End Sub

    Private Sub GetLinksWorker(ByVal AsyncOp As AsyncOperation, ByVal link As String, ByVal Host As Host)
        Try
            With HTTP

                _LinkOfCurrentEpisode = link
                LinkID = link.Substring(link.IndexOf("play-")).Replace("-serie.html", String.Empty).Replace("play-", String.Empty)
                If Host <> Host.Automatic Then HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host), LinkID)).Html

                Dim URL As String = String.Empty
                Dim SelectedHost As Host = Nothing
                Dim e As New LinkDownloadedEventArgs

                Select Case Host
                    Case SeriesClass.Host.Automatic
                        e = GetLinkAutomatic()
                    Case SeriesClass.Host.Videoweed
                        e = GetVideoWeedLink()
                    Case SeriesClass.Host.VidUp
                        e = GetVidUpLink()
                    Case SeriesClass.Host.Moymir
                        e = GetMoymirLink()
                    Case SeriesClass.Host.Putlocker
                        e = GetPutLockerLink()
                    Case SeriesClass.Host.Movshare
                        e = GetMovShareLink()
                    Case SeriesClass.Host.Flashx
                        e = GetFlashxLink()
                    Case SeriesClass.Host.NowVideo
                        e = GetNowVideoLink()
                    Case SeriesClass.Host.Daclips
                        e = GetMGDLink()
                    Case SeriesClass.Host.MovPod
                        e = GetMGDLink()
                    Case SeriesClass.Host.Gorrilavid
                        e = GetMGDLink()
                End Select

                AsyncOp.PostOperationCompleted(AddressOf LinkDownloaded, e)
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LinkDownloaded(e As LinkDownloadedEventArgs)
        RaiseEvent OnVideoLinkDownloaded(e)
    End Sub

    'NEW FUNCTIONS

    Private Function GetFlashxLink() As LinkDownloadedEventArgs
        Try
            Using WC As New System.Net.WebClient() With {.Proxy = Nothing}


                Dim WatchseriesHTML As String = WC.DownloadString(_LinkOfCurrentEpisode)
                Dim FlashxLink As String = Split(WatchseriesHTML, "' target='_blank'>Flashx</a>")(0)
                FlashxLink = Split(FlashxLink.Substring(FlashxLink.LastIndexOf("<a href='")), "<a href='")(1) 'Get flashX link


                Dim flashxEmbeddedLink As String = String.Format("http://play.flashx.tv/player/embed.php?hash={0}&width=620&height=400", FlashxLink.Split("/")(4))

                Dim FlashxHTML As String = WC.DownloadString(flashxEmbeddedLink)

                Dim Hash As String = Regex.Match(FlashxHTML, "<input name=""hash"" type=""hidden"" value=""(.*)"">").Groups(1).Value
                Dim Sechash As String = Regex.Match(FlashxHTML, "<input name=""sechash"" type=""hidden"" value=""(.*)"">").Groups(1).Value
                Dim Width As String = Regex.Match(FlashxHTML, "<input name=""width"" type=""hidden"" value=""(.*)"">").Groups(1).Value
                Dim Height As String = Regex.Match(FlashxHTML, "<input name=""height"" type=""hidden"" value=""(.*)"">").Groups(1).Value

                Dim NVC As New System.Collections.Specialized.NameValueCollection()
                NVC.Add("hash", Hash)
                NVC.Add("sechash", Sechash)
                NVC.Add("width", Width)
                NVC.Add("height", Height)

                Dim POSTCaptchResponse As String = System.Text.Encoding.UTF8.GetString(WC.UploadValues("http://play.flashx.tv/player/captcha.php", NVC))
                Dim linkFromPOSTReponse As String = Regex.Match(POSTCaptchResponse, "<param name=""movie"" value=""(.*)"" />").Groups(1).Value
                Dim linkXML As String = Split(linkFromPOSTReponse, "config=")(1)
                Dim XMLContent As String = WC.DownloadString(linkXML)

                Dim mediaURL As String = Split(Split(XMLContent, "<file>")(1), "</file>")(0)

                Return New LinkDownloadedEventArgs() With {.Host = Host.Flashx, .AutomaticHost = False, .Link = mediaURL}
            End Using
        Catch ex As Exception
            Return New LinkDownloadedEventArgs With {.Host = Host.Videoweed, .Link = String.Empty, .AutomaticHost = False}
        End Try
    End Function

    Private Function GetVideoWeedLink() As LinkDownloadedEventArgs
        Try
            With HTTP
                Embedded_Link = Regex.Match(HTML, "<span>Videoweed</span><iframe width='.*' height='.*' frameborder='0' src='(.*)' scrolling='no'></iframe>").Groups(1).Value
                Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                Dim filekey As String = (Regex.Match(srcpage, "var fkz=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
                Dim MediaURL As String = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.videoweed.es/api/player.api.php?pass=undefined&cid2={0}&file={1}&cid=undefined&user=undefined&key={2}&cid3=undefined&numOfErrors=0", cid2, file, filekey)).Html, "url=")(1).Split("&title")(0)
                Return New LinkDownloadedEventArgs With {.Host = Host.Videoweed, .Link = MediaURL, .AutomaticHost = False}
            End With
        Catch ex As Exception
            Return New LinkDownloadedEventArgs With {.Host = Host.Videoweed, .Link = String.Empty, .AutomaticHost = False}
        End Try
    End Function

    Private Function GetVidUpLink() As LinkDownloadedEventArgs
        Try
            With HTTP
                Embedded_Link = Regex.Match(HTML, "<span>Vidup</span><IFRAME SRC='(.*)' FRAMEBORDER=.* MARGINWIDTH=.* MARGINHEIGHT=.* SCROLLING=NO WIDTH=.* HEIGHT=.*></IFRAME>").Groups(1).Value
                If Not Embedded_Link.Contains("http://www.") Then Embedded_Link = Embedded_Link.Replace("http://", "http://www.")

                Dim SpliitedLink As String = "http://www.vidup.me/" & Split(Embedded_Link, "-")(1)

                Dim HTMLPage As String = .GetResponse(Utility.Http.Verb.GET, SpliitedLink).Html

                Dim id As String = Regex.Match(HTMLPage, "<input type=""hidden"" name=""id"" value=""(.*)"">").Groups(1).Value
                Dim fname As String = Regex.Match(HTMLPage, "<input type=""hidden"" name=""fname"" value=""(.*)"">").Groups(1).Value
                Dim referer As String = Embedded_Link.Replace(":", "%3A").Replace("/", "%2F")

                Dim POSTData As String = String.Format("op=download1&usr_login=&id={0}&fname={1}&referer={2}&method_free_01=tel%3Fchargement+libre+", id, fname, referer)
                Dim HTMLVideoLink As String = .GetResponse(Utility.Http.Verb.POST, SpliitedLink, POSTData).Html


                Dim LinkIP As String() = Split(Split(HTMLVideoLink, "|skin|")(1), "|100||timeslidertooltipplugin|")(0).Split("|") _
                                         .Where(Function(x) Not String.IsNullOrEmpty(x)).ToArray : Array.Reverse(LinkIP) 'Grab IP from the eval crypted function

                Dim LinkID As String = Split(Split(HTMLVideoLink, "|provider|mp4|video|")(1), "|swf|")(0).Split("|")(0)

                Dim MediaURL As String = String.Format("http://{0}:182/d/{1}/video.mp4?start=0", String.Join(".", LinkIP.ToArray), LinkID)
                Return New LinkDownloadedEventArgs With {.Host = Host.VidUp, .Link = MediaURL, .AutomaticHost = False}
            End With
        Catch ex As Exception
            Return New LinkDownloadedEventArgs With {.Host = Host.VidUp, .Link = String.Empty, .AutomaticHost = False}
        End Try
    End Function

    Private Function GetMoymirLink() As LinkDownloadedEventArgs
        Try
            With HTTP
                Embedded_Link = Regex.Match(HTML, "<span>Moymir</span><iframe src='(.*)' width='.*' height='.*' frameborder='0' webkitallowfullscreen='' mozallowfullscreen='' allowfullscreen=''></iframe>").Groups(1).Value
                Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                Dim MediaURL As String = Regex.Match(srcpage, "videoSrc = ""(.*)""").Groups(1).Value
                Return New LinkDownloadedEventArgs With {.Host = Host.Moymir, .Link = MediaURL, .AutomaticHost = False}
            End With
        Catch ex As Exception
            Return New LinkDownloadedEventArgs With {.Host = Host.Moymir, .Link = String.Empty, .AutomaticHost = False}
        End Try

        'videoSrc = "http://cdn40.video.mail.ru/hv/46322793.mp4?sign=f507cc87d4ef563a399e3044abe9cfb922fc3b13&slave[]=s%3Ahttp%3A%2F%2F127.0.0.1%3A5010%2F46322793-hv.mp4&p=0&expire_at=1393790400&touch=1393378267"
    End Function
    Private Function GetPutLockerLink() As LinkDownloadedEventArgs
        With HTTP
            Try
                .TimeOut = 15000
                Embedded_Link = Regex.Match(HTML, "<iframe src='(.*)' width='.*' height='.*' frameborder='.*' scrolling='no'></iframe>").Groups(1).Value.Replace("/embed/", "/file/")

                If Not Embedded_Link.Contains("http://www.") Then Embedded_Link = Embedded_Link.Replace("http://", "http://www.").Replace("putlocker", "firedrive")

                Dim source As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html
                Dim ConfirmID As String = Regex.Match(source, "<input type=""hidden"" name=""confirm"" value=""(.*)""/>").Groups(1).Value.Replace("=", "%3D").Replace("+", "%2B").Replace("/", "%2F")

                Dim Headers As New NameValueCollection
                Headers.Add("Connection", "keep-alive")

                .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
                .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0"
                .AcceptEncoding = "gzip, deflate"
                .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
                .ContentType = "application/x-www-form-urlencoded"


                'confirm=gVvWqQrXvRBzKKaLL2GZe4UOqvYdCx0z8b%2FlEZckeorsCzLqRJ63NQ%2BkJlHji6v3wnVgftCG5ZSBvlT%2B6wg6xw%3D%3D
                'confirm=OiDTwS3fcBprmy41rU56LjjVSKwCtLQYo9hFrifK2nmOOYff6/k9gMZJoiWyu1e0oRIGz5M31yIXSjz3nKoEag==

                Dim ConfirmButton As String = .GetResponse(Utility.Http.Verb.POST, Embedded_Link, String.Format("confirm={0}", ConfirmID), Headers).Html

                Dim ParseHTML As String = Split(Split(ConfirmButton, "<div id='fd_dl_drpdwn'>")(1), "target=""_blank""")(0).Replace("<a href=", String.Empty).Replace(ChrW(34), String.Empty).Trim()
                Return New LinkDownloadedEventArgs With {.Host = Host.Putlocker, .Link = ParseHTML, .AutomaticHost = False}
            Catch ex As Exception
                Return New LinkDownloadedEventArgs With {.Host = Host.Putlocker, .Link = String.Empty, .AutomaticHost = False}
            End Try
        End With
    End Function




    Private Function GetSockShareLink() As LinkDownloadedEventArgs
        With HTTP
            Embedded_Link = Regex.Match(HTML, "<iframe src='(.*)' width='.*' height='.*' frameborder='.*' scrolling='no'></iframe>").Groups(1).Value.Replace("/embed/", "/file/")

            Dim source As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html
            Dim Hash As String = Regex.Match(source, "<input type=""hidden"" value=""(.*)"" name=""hash"">").Groups(1).Value

            Dim Headers As New NameValueCollection
            Headers.Add("Connection", "keep-alive")

            .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
            .UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0"
            .AcceptCharset = String.Empty
            .AcceptEncoding = "gzip, deflate"
            .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
            .ContentType = "application/x-www-form-urlencoded"

            Dim sourceSecondPage As String = .GetResponse(Utility.Http.Verb.POST, Embedded_Link, String.Format("hash={0}&confirm=Continue+as+Free+User", Hash), Headers).Html
            Dim StreamURL As String = Regex.Match(sourceSecondPage, "playlist: \'(.*)\'").Groups(1).Value
            Dim GetPhpScript As String = .GetResponse(Utility.Http.Verb.GET, "http://www.sockshare.com" + StreamURL).Html
            Dim MediaURL As String = Regex.Match(GetPhpScript, "<media:content url=""(.*)"" type="".*""  duration="".*"" />").Groups(1).Value
            Return New LinkDownloadedEventArgs With {.Host = Host.Putlocker, .Link = MediaURL, .AutomaticHost = False}
        End With
    End Function
    Private Function GetNowVideoLink() As LinkDownloadedEventArgs
        Try
            With HTTP
                Embedded_Link = Regex.Match(HTML, "src='(.*)'").Groups(1).Value
                Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                Dim filekey As String = (Regex.Match(srcpage, "var fkzd=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")

                Dim MediaURL As String = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.nowvideo.sx/api/player.api.php?pass=undefined&cid2={0}&key={1}&cid=undefined&user=undefined&file={2}&cid3=undefined&numOfErrors=0", cid2, filekey, file)).Html, "url=")(1).Split("&title")(0)
                Return New LinkDownloadedEventArgs With {.Host = Host.NowVideo, .Link = MediaURL, .AutomaticHost = False}
            End With
        Catch ex As Exception
            Return New LinkDownloadedEventArgs With {.Host = Host.NowVideo, .Link = String.Empty, .AutomaticHost = False}
        End Try
    End Function
    Private Function GetMGDLink() As LinkDownloadedEventArgs
        Try
            With HTTP
                Embedded_Link = Regex.Match(HTML, "<IFRAME SRC='(.*)'").Groups(1).Value
                Dim MediaURL As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html
                MediaURL = (Regex.Match(MediaURL, "{ file: ""(.*)"", type:"".*"" }").Groups(1).Value).Replace(".flv", ".mp4")
                Return New LinkDownloadedEventArgs With {.Host = Host.MovPod, .Link = MediaURL, .AutomaticHost = False}
            End With
        Catch ex As Exception
            Return New LinkDownloadedEventArgs With {.Host = Host.MovPod, .Link = String.Empty, .AutomaticHost = False}
        End Try
    End Function
    Private Function GetMovShareLink() As LinkDownloadedEventArgs
        Try
            With HTTP
                Embedded_Link = Regex.Match(HTML, "src='(.*)'").Groups(1).Value
                Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                Dim filekey As String = (Regex.Match(srcpage, "var fkzd=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
                'http://www.movshare.net/api/player.api.php?pass=undefined&cid2=19487&key=88%2E184%2E87%2E35%2D189917ac69d076f54b46025915df29b2&cid=undefined&user=undefined&file=80c8ae63d5385&cid3=undefined&numOfErrors=0
                Dim MediaURL As String = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.movshare.net/api/player.api.php?pass=undefined&cid2={0}&key={1}&cid=undefined&user=undefined&file={2}&cid3=undefined&numOfErrors=0", cid2, filekey, file)).Html, "url=")(1).Split("&title")(0)
                Return New LinkDownloadedEventArgs With {.Host = Host.Movshare, .Link = MediaURL, .AutomaticHost = False}
            End With
        Catch ex As Exception
            Return New LinkDownloadedEventArgs With {.Host = Host.Movshare, .Link = String.Empty, .AutomaticHost = False}
        End Try
    End Function
    Private Function GetLinkAutomatic() As LinkDownloadedEventArgs
        With HTTP

            '-------------------------------
            '------------PUTLOCKER---------
            '-------------------------------

            Dim source As String = String.Empty
            Dim Hash As String = String.Empty
            Dim Headers As New NameValueCollection
            Dim GetPhpScript As String = String.Empty
            Dim StreamURL As String = String.Empty
            Dim MediaURL As String = String.Empty
            Dim SelectedHost As Host = Nothing


            Try
                .TimeOut = 15000
                HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Putlocker), LinkID)).Html
                Embedded_Link = Regex.Match(HTML, "<iframe src='(.*)' width='.*' height='.*' frameborder='.*' scrolling='no'></iframe>").Groups(1).Value.Replace("/embed/", "/file/")

                If Not Embedded_Link.Contains("http://www.") Then Embedded_Link = Embedded_Link.Replace("http://", "http://www.").Replace("putlocker", "firedrive")

                source = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html
                Dim ConfirmID As String = Regex.Match(source, "<input type=""hidden"" name=""confirm"" value=""(.*)""/>").Groups(1).Value.Replace("=", "%3D").Replace("+", "%2B").Replace("/", "%2F")

                Headers.Clear()
                Headers.Add("Connection", "keep-alive")

                .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
                .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0"
                .AcceptEncoding = "gzip, deflate"
                .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
                .ContentType = "application/x-www-form-urlencoded"

                Dim ConfirmButton As String = .GetResponse(Utility.Http.Verb.POST, Embedded_Link, String.Format("confirm={0}", ConfirmID), Headers).Html

                MediaURL = Split(Split(ConfirmButton, "<div id='fd_dl_drpdwn'>")(1), "target=""_blank""")(0).Replace("<a href=", String.Empty).Replace(ChrW(34), String.Empty).Trim()
                SelectedHost = Host.Putlocker
            Catch ex As Exception
                Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH PUTLOCKER")
                Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH PUTLOCKER")
                Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH PUTLOCKER")
                Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH PUTLOCKER")
                MediaURL = String.Empty
            End Try


            '-------------------------------
            '-------------MOYMIR-------------
            '-------------------------------

            If MediaURL = String.Empty Then
                Try
                    With HTTP
                        HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Moymir), LinkID)).Html
                        Embedded_Link = Regex.Match(HTML, "<span>Moymir</span><iframe src='(.*)' width='.*' height='.*' frameborder='0' webkitallowfullscreen='' mozallowfullscreen='' allowfullscreen=''></iframe>").Groups(1).Value
                        Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html
                        MediaURL = Regex.Match(srcpage, "videoSrc = ""(.*)""").Groups(1).Value
                        SelectedHost = Host.Moymir
                    End With
                Catch ex As Exception
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    MediaURL = String.Empty
                End Try
            End If

            '-------------------------------
            '-------------VIDEOWEED---------
            '-------------------------------
            If MediaURL = String.Empty Then
                Try
                    HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Videoweed), LinkID)).Html
                    Embedded_Link = Regex.Match(HTML, "<span>Videoweed</span><iframe width='.*' height='.*' frameborder='0' src='(.*)' scrolling='no'></iframe>").Groups(1).Value
                    Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                    Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                    Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                    Dim filekey As String = (Regex.Match(srcpage, "var fkz=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
                    'http://www.movshare.net/api/player.api.php?pass=undefined&cid2=19487&key=88%2E184%2E87%2E35%2D189917ac69d076f54b46025915df29b2&cid=undefined&user=undefined&file=80c8ae63d5385&cid3=undefined&numOfErrors=0
                    MediaURL = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.videoweed.es/api/player.api.php?pass=undefined&cid2={0}&file={1}&cid=undefined&user=undefined&key={2}&cid3=undefined&numOfErrors=0", cid2, file, filekey)).Html, "url=")(1).Split("&title")(0)
                    SelectedHost = Host.Videoweed
                Catch ex As Exception
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    MediaURL = String.Empty
                End Try
            End If

            '-------------------------------
            '-------------VIDUP-------------
            '-------------------------------

            'If MediaURL = String.Empty Then
            '    Try
            '        HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.VidUp), LinkID)).Html
            '        Embedded_Link = Regex.Match(HTML, "<span>Vidup</span><IFRAME SRC='(.*)' FRAMEBORDER=.* MARGINWIDTH=.* MARGINHEIGHT=.* SCROLLING=NO WIDTH=.* HEIGHT=.*></IFRAME>").Groups(1).Value
            '        If Not Embedded_Link.Contains("http://www.") Then Embedded_Link = Embedded_Link.Replace("http://", "http://www.")

            '        Dim SpliitedLink As String = "http://www.vidup.me/" & Split(Embedded_Link, "-")(1)

            '        Dim HTMLPage As String = .GetResponse(Utility.Http.Verb.GET, SpliitedLink).Html

            '        Dim id As String = Regex.Match(HTMLPage, "<input type=""hidden"" name=""id"" value=""(.*)"">").Groups(1).Value
            '        Dim fname As String = Regex.Match(HTMLPage, "<input type=""hidden"" name=""fname"" value=""(.*)"">").Groups(1).Value
            '        Dim referer As String = Embedded_Link.Replace(":", "%3A").Replace("/", "%2F")

            '        Dim POSTData As String = String.Format("op=download1&usr_login=&id={0}&fname={1}&referer={2}&method_free_01=tel%3Fchargement+libre+", id, fname, referer)
            '        Dim HTMLVideoLink As String = .GetResponse(Utility.Http.Verb.POST, SpliitedLink, POSTData).Html


            '        Dim LinkIP As String() = Split(Split(HTMLVideoLink, "|skin|")(1), "|100||timeslidertooltipplugin|")(0).Split("|") _
            '                                 .Where(Function(x) Not String.IsNullOrEmpty(x)).ToArray : Array.Reverse(LinkIP) 'Grab IP from the eval crypted function

            '        Dim LinkIdentity As String = Split(Split(HTMLVideoLink, "|provider|mp4|video|")(1), "|swf|")(0).Split("|")(0)

            '        MediaURL = String.Format("http://{0}:182/d/{1}/video.mp4?start=0", String.Join(".", LinkIP.ToArray), LinkIdentity)
            '    Catch ex As Exception
            '        Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDUP")
            '        Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDUP")
            '        Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDUP")
            '        Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDUP")
            '        MediaURL = String.Empty
            '    End Try
            'End If


            '-------------------------------
            '---GORRILAVID/DACLIPS/MOVPOD---
            '-------------------------------

            If MediaURL = String.Empty Then
                Try
                    HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Daclips), LinkID)).Html
                    If HTML = String.Empty Then HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Gorrilavid), LinkID)).Html
                    If HTML = String.Empty Then HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.MovPod), LinkID)).Html
                    Embedded_Link = Regex.Match(HTML, "<IFRAME SRC='(.*)'").Groups(1).Value
                    MediaURL = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html
                    MediaURL = (Regex.Match(MediaURL, "{ file: ""(.*)"", type:"".*"" }").Groups(1).Value).Replace(".flv", ".mp4")
                    SelectedHost = Host.MovPod
                Catch ex As Exception
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH GORRILA/DACLIPS/MOVPOD")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH GORRILA/DACLIPS/MOVPOD")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH GORRILA/DACLIPS/MOVPOD")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH GORRILA/DACLIPS/MOVPOD")
                End Try
            End If

            '-------------------------------
            '------------MOVSHARE-----------
            '-------------------------------

            If MediaURL = String.Empty Then
                Try
                    With HTTP
                        HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Movshare), LinkID)).Html
                        Embedded_Link = Regex.Match(HTML, "src='(.*)'").Groups(1).Value
                        Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                        Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                        Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                        Dim filekey As String = (Regex.Match(srcpage, "var fkzd=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
                        MediaURL = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.movshare.net/api/player.api.php?pass=undefined&cid2={0}&key={1}&cid=undefined&user=undefined&file={2}&cid3=undefined&numOfErrors=0", cid2, filekey, file)).Html, "url=")(1).Split("&title")(0)
                        SelectedHost = Host.Movshare
                    End With
                Catch ex As Exception
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH MOVSHARE")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH MOVSHARE")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH MOVSHARE")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH MOVSHARE")
                    MediaURL = String.Empty
                End Try
            End If

            '--------------------------------
            '----------NOWVIDEO--------------
            '--------------------------------

            If MediaURL = String.Empty Then
                Try
                    HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.NowVideo), LinkID)).Html
                    Embedded_Link = Regex.Match(HTML, "src='(.*)'").Groups(1).Value
                    Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                    Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                    Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                    Dim filekey As String = (Regex.Match(srcpage, "var fkzd=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
                    MediaURL = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.nowvideo.sx/api/player.api.php?pass=undefined&cid2={0}&key={1}&cid=undefined&user=undefined&file={2}&cid3=undefined&numOfErrors=0", cid2, filekey, file)).Html, "url=")(1).Split("&title")(0)
                    SelectedHost = Host.NowVideo
                Catch ex As Exception
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH NOWVIDEO")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH NOWVIDEO")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH NOWVIDEO")
                    Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH NOWVIDEO")
                    MediaURL = String.Empty
                End Try
            End If
            Return New LinkDownloadedEventArgs() With {.Host = SelectedHost, .AutomaticHost = True, .Link = MediaURL}
        End With
    End Function


    Private Delegate Sub DownloadLinkAndDownloadDelegate(AsyncOp As AsyncOperation, Ep As DownloadManager.Episode)
    Public Event DownloadLinkGrabbedAndDownload(Episode As DownloadManager.Episode)
    Public Sub GetLinkAutomaticAndDownload(Ep As DownloadManager.Episode)
        Dim worker As New DownloadLinkAndDownloadDelegate(AddressOf GetLinkAutomaticToDownloadWorker)
        Dim asyncop As AsyncOperation = AsyncOperationManager.CreateOperation(New Object)
        worker.BeginInvoke(asyncop, Ep, Nothing, Nothing)
    End Sub


    Private Sub GetLinkAutomaticToDownloadWorker(AsyncOp As AsyncOperation, Ep As DownloadManager.Episode)
        With HTTP

            Dim link As String = Ep.URL

            _LinkOfCurrentEpisode = link
            LinkID = link.Substring(link.IndexOf("play-")).Replace("-serie.html", String.Empty).Replace("play-", String.Empty)

            '-------------------------------
            '------------PUTLOCKER---------
            '-------------------------------

            Dim source As String = String.Empty
            Dim Hash As String = String.Empty
            Dim Headers As New NameValueCollection
            Dim GetPhpScript As String = String.Empty
            Dim StreamURL As String = String.Empty
            Dim MediaURL As String = String.Empty


            Try
                .TimeOut = 15000
                HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Putlocker), LinkID)).Html
                Embedded_Link = Regex.Match(HTML, "<iframe src='(.*)' width='.*' height='.*' frameborder='.*' scrolling='no'></iframe>").Groups(1).Value.Replace("/embed/", "/file/")

                If Not Embedded_Link.Contains("http://www.") Then Embedded_Link = Embedded_Link.Replace("http://", "http://www.").Replace("putlocker", "firedrive")

                source = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html
                Dim ConfirmID As String = Regex.Match(source, "<input type=""hidden"" name=""confirm"" value=""(.*)""/>").Groups(1).Value.Replace("=", "%3D").Replace("+", "%2B").Replace("/", "%2F")

                Headers.Clear()
                Headers.Add("Connection", "keep-alive")

                .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
                .UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0"
                .AcceptEncoding = "gzip, deflate"
                .AcceptLanguage = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3"
                .ContentType = "application/x-www-form-urlencoded"

                Dim ConfirmButton As String = .GetResponse(Utility.Http.Verb.POST, Embedded_Link, String.Format("confirm={0}", ConfirmID), Headers).Html

                MediaURL = Split(Split(ConfirmButton, "<div id='fd_dl_drpdwn'>")(1), "target=""_blank""")(0).Replace("<a href=", String.Empty).Replace(ChrW(34), String.Empty).Trim()
            Catch ex As Exception
                'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH PUTLOCKER")
                'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH PUTLOCKER")
                'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH PUTLOCKER")
                'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH PUTLOCKER")
                MediaURL = String.Empty
            End Try

            '-------------------------------
            '-------------VIDEOWEED---------
            '-------------------------------
            If MediaURL = String.Empty Then
                Try
                    HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Videoweed), LinkID)).Html
                    Embedded_Link = Regex.Match(HTML, "<span>Videoweed</span><iframe width='.*' height='.*' frameborder='0' src='(.*)' scrolling='no'></iframe>").Groups(1).Value
                    Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                    Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                    Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                    Dim filekey As String = (Regex.Match(srcpage, "var fkz=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
                    'http://www.movshare.net/api/player.api.php?pass=undefined&cid2=19487&key=88%2E184%2E87%2E35%2D189917ac69d076f54b46025915df29b2&cid=undefined&user=undefined&file=80c8ae63d5385&cid3=undefined&numOfErrors=0
                    MediaURL = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.videoweed.es/api/player.api.php?pass=undefined&cid2={0}&file={1}&cid=undefined&user=undefined&key={2}&cid3=undefined&numOfErrors=0", cid2, file, filekey)).Html, "url=")(1).Split("&title")(0)
                Catch ex As Exception
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDEOWEED")
                    MediaURL = String.Empty
                End Try
            End If

            '-------------------------------
            '-------------VIDUP-------------
            '-------------------------------

            'If MediaURL = String.Empty Then
            '    Try
            '        HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.VidUp), LinkID)).Html
            '        Embedded_Link = Regex.Match(HTML, "<span>Vidup</span><IFRAME SRC='(.*)' FRAMEBORDER=.* MARGINWIDTH=.* MARGINHEIGHT=.* SCROLLING=NO WIDTH=.* HEIGHT=.*></IFRAME>").Groups(1).Value
            '        If Not Embedded_Link.Contains("http://www.") Then Embedded_Link = Embedded_Link.Replace("http://", "http://www.")

            '        Dim SpliitedLink As String = "http://www.vidup.me/" & Split(Embedded_Link, "-")(1)

            '        Dim HTMLPage As String = .GetResponse(Utility.Http.Verb.GET, SpliitedLink).Html

            '        Dim id As String = Regex.Match(HTMLPage, "<input type=""hidden"" name=""id"" value=""(.*)"">").Groups(1).Value
            '        Dim fname As String = Regex.Match(HTMLPage, "<input type=""hidden"" name=""fname"" value=""(.*)"">").Groups(1).Value
            '        Dim referer As String = Embedded_Link.Replace(":", "%3A").Replace("/", "%2F")

            '        Dim POSTData As String = String.Format("op=download1&usr_login=&id={0}&fname={1}&referer={2}&method_free_01=tel%3Fchargement+libre+", id, fname, referer)
            '        Dim HTMLVideoLink As String = .GetResponse(Utility.Http.Verb.POST, SpliitedLink, POSTData).Html


            '        Dim LinkIP As String() = Split(Split(HTMLVideoLink, "|skin|")(1), "|100||timeslidertooltipplugin|")(0).Split("|") _
            '                                 .Where(Function(x) Not String.IsNullOrEmpty(x)).ToArray : Array.Reverse(LinkIP) 'Grab IP from the eval crypted function

            '        Dim LinkIdentity As String = Split(Split(HTMLVideoLink, "|provider|mp4|video|")(1), "|swf|")(0).Split("|")(0)

            '        MediaURL = String.Format("http://{0}:182/d/{1}/video.mp4?start=0", String.Join(".", LinkIP.ToArray), LinkIdentity)
            '    Catch ex As Exception
            '        Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDUP")
            '        Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDUP")
            '        Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDUP")
            '        Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH VIDUP")
            '        MediaURL = String.Empty
            '    End Try
            'End If


            '-------------------------------
            '---GORRILAVID/DACLIPS/MOVPOD---
            '-------------------------------

            If MediaURL = String.Empty Then
                Try
                    HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Daclips), LinkID)).Html
                    If HTML = String.Empty Then HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Gorrilavid), LinkID)).Html
                    If HTML = String.Empty Then HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.MovPod), LinkID)).Html
                    Embedded_Link = Regex.Match(HTML, "<IFRAME SRC='(.*)'").Groups(1).Value
                    MediaURL = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html
                    MediaURL = (Regex.Match(MediaURL, "{ file: ""(.*)"", type:"".*"" }").Groups(1).Value).Replace(".flv", ".mp4")
                Catch ex As Exception
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH GORRILA/DACLIPS/MOVPOD")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH GORRILA/DACLIPS/MOVPOD")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH GORRILA/DACLIPS/MOVPOD")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH GORRILA/DACLIPS/MOVPOD")
                End Try
            End If

            '-------------------------------
            '------------MOVSHARE-----------
            '-------------------------------

            If MediaURL = String.Empty Then
                Try
                    With HTTP
                        HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.Movshare), LinkID)).Html
                        Embedded_Link = Regex.Match(HTML, "src='(.*)'").Groups(1).Value
                        Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                        Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                        Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                        Dim filekey As String = (Regex.Match(srcpage, "var fkzd=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
                        MediaURL = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.movshare.net/api/player.api.php?pass=undefined&cid2={0}&key={1}&cid=undefined&user=undefined&file={2}&cid3=undefined&numOfErrors=0", cid2, filekey, file)).Html, "url=")(1).Split("&title")(0)
                    End With
                Catch ex As Exception
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH MOVSHARE")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH MOVSHARE")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH MOVSHARE")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH MOVSHARE")
                    MediaURL = String.Empty
                End Try
            End If

            '--------------------------------
            '----------NOWVIDEO--------------
            '--------------------------------

            If MediaURL = String.Empty Then
                Try
                    HTML = .GetResponse(Utility.Http.Verb.POST, "http://watchseries.biz/veri.php", String.Format("video={0}&seo={1}", Hosts(Host.NowVideo), LinkID)).Html
                    Embedded_Link = Regex.Match(HTML, "src='(.*)'").Groups(1).Value
                    Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, Embedded_Link).Html

                    Dim cid2 As String = Regex.Match(srcpage, "flashvars.cid2=""(.*)"";").Groups(1).Value
                    Dim file As String = Regex.Match(srcpage, "flashvars.file=""(.*)"";").Groups(1).Value
                    Dim filekey As String = (Regex.Match(srcpage, "var fkzd=""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
                    MediaURL = Split(.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.nowvideo.sx/api/player.api.php?pass=undefined&cid2={0}&key={1}&cid=undefined&user=undefined&file={2}&cid3=undefined&numOfErrors=0", cid2, filekey, file)).Html, "url=")(1).Split("&title")(0)
                Catch ex As Exception
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH NOWVIDEO")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH NOWVIDEO")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH NOWVIDEO")
                    'Debug.WriteLine(">>>>>> WARNING: EXCEPTION IN AUTOMATIC FUNCTION WITH NOWVIDEO")
                    MediaURL = String.Empty
                End Try
            End If

            Ep.URL = MediaURL

            AsyncOp.PostOperationCompleted(AddressOf OnDownloadGrabbedAndDownload, Ep)
        End With
    End Sub

    Private Sub OnDownloadGrabbedAndDownload(Ep As DownloadManager.Episode)
        RaiseEvent DownloadLinkGrabbedAndDownload(Ep)
    End Sub


#Region "Old GetMoveshare link, with eval crypting"
    'Dim JSevalfunction As String
    'Private Function MovShare() As String
    '    Try
    '        With http
    '            PreLink = (Regex.Match(a, "<iframe width='.*' height='.*' frameborder='.*' src='(.*)' scrolling='.*'></iframe>").Groups(1).Value).Split("&")(0)
    '            Dim srcpage As String = .GetResponse(Utility.Http.Verb.GET, PreLink).Html

    '            JSevalfunction = Split(srcpage, "eval")(1)
    '            JSevalfunction = "eval" & Split(JSevalfunction, "</script>")(0)
    '            Return Hosts.MovShare
    '        End With
    '    Catch ex As Exception
    '        Return String.Empty
    '    End Try
    'End Function
    'Public Sub GetMovShareLink()
    '    Dim WB As New WebBrowser
    '    WB.ScriptErrorsSuppressed = True
    '    WB.Navigate("http://www.strictly-software.com/unpacker#unpacker")

    '    Dim APIKey As String = String.Empty
    '    Dim searchonce As Boolean = False

    '    AddHandler WB.DocumentCompleted, Sub()
    '                                         If Not searchonce Then
    '                                             searchonce = True
    '                                             Dim textArea As HtmlElement = WB.Document.All("txtPacked")
    '                                             If textArea IsNot Nothing Then
    '                                                 textArea.InnerText = JSevalfunction
    '                                             End If
    '                                             WB.Document.InvokeScript("unpack")

    '                                             Dim result As HtmlElement = WB.Document.All("txtUnpacked")
    '                                             If result IsNot Nothing Then
    '                                                 APIKey = result.InnerText
    '                                                 APIKey = (Regex.Match(APIKey, "var ll = ""(.*)"";").Groups(1).Value).Replace(".", "%2E").Replace("-", "%2D")
    '                                                 Dim file As String = Split(PreLink, "embed.php?v=")(1)
    '                                                 Dim MediaURL As String = Split(http.GetResponse(Utility.Http.Verb.GET, String.Format("http://www.movshare.net/api/player.api.php?pass=undefined&cid2={0}&key={1}&cid=undefined&user=undefined&file={2}&cid3=undefined&numOfErrors=0", String.Empty, APIKey, file)).Html, "url=")(1).Split("&title")(0)
    '                                                 RaiseEvent OnVideoLinkDownloaded(MediaURL)
    '                                                 Exit Sub
    '                                             End If
    '                                         End If
    '                                     End Sub
    'End Sub
#End Region

#End Region

#Region "Help Functions"
    Private Function GetBetweenAll(ByVal Source As String, ByVal Str1 As String, ByVal Str2 As String) As String()
        Dim Results, T As New List(Of String)
        T.AddRange(Regex.Split(Source, Str1))
        T.RemoveAt(0)
        For Each I As String In T
            Results.Add(Regex.Split(I, Str2)(0))
        Next
        Return Results.ToArray
    End Function

#End Region


End Class
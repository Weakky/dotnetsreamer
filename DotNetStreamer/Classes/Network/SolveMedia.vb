Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Namespace Service.Captcha

    Public Class SolveMedia

        Public Shared Function Request(key As String, Optional ref As String = "") As Captcha
            Using wc = New WebClient
                With wc
                    With .Headers
                        .Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0")
                        .Add("Accept", "*/*")
                        .Add("Accept-Language", "en-US,en;q=0.5")
                        If Not String.IsNullOrEmpty(ref) Then .Add("Referer", ref)
                        .Add("Cache-Control", "max-age=0")
                    End With

                    Dim html As String = .DownloadString("http://api.solvemedia.com/papi/challenge.script?k=" & Uri.EscapeUriString(key))
                    html = .DownloadString(String.Format("http://api.solvemedia.com/papi/_challenge.js?k={0}", key))
                    Dim chid As String = Regex.Match(html, """chid""\s*:\s*""(?<value>.*\.\w\S*)""").Groups("value").Value

                    With .Headers
                        .Set("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8")
                        .Remove("Cache-Control")
                    End With

                    Dim content As Byte() = .DownloadData(String.Format("http://api.solvemedia.com/papi/media?c={0}", Uri.EscapeUriString(chid)))

                    Using stream = New MemoryStream(content)
                        Return New Captcha(Image.FromStream(stream), chid)
                    End Using
                End With
            End Using
        End Function

    End Class

    Public Class Recaptcha

        '6LfFBNYSAAAAAJ6UifoTnR0mUEGFPzzuK372a2Vr

        Public Shared Function Request(key As String, Optional ref As String = "") As Captcha
            Using wc = New WebClient
                With wc

                    Dim html As String = wc.DownloadString("http://www.google.com/recaptcha/api/challenge?k=6LfFBNYSAAAAAJ6UifoTnR0mUEGFPzzuK372a2Vr")
                    Dim challenge As String = challengeByHtml(html)

                    Dim content As Byte() = wc.DownloadData("http://www.google.com/recaptcha/api/image?c=" & challenge)

                    Using stream = New MemoryStream(content)
                        Return New Captcha(Image.FromStream(stream), challenge)
                    End Using
                End With
            End Using
        End Function


        Private Shared Function challengeByHtml(javascriptRecaptcha As String) As String
            Dim c As Integer = javascriptRecaptcha.IndexOf("challenge : '")
            Dim d As Integer = javascriptRecaptcha.IndexOf("',", c + 13)
            Dim challenge As String = javascriptRecaptcha.Substring(c + 13, d - (c + 13))
            Return challenge
        End Function
    End Class

    Public Class Captcha
        Implements IDisposable

        Public Property image As Image
        Public Property challenge As String

        Private _solution As String
        Public Property Solution() As String
            Get
                Return _solution
            End Get
            Set(ByVal value As String)
                _solution = value.Replace(" ", "+")
            End Set
        End Property


#Region "Constructor"

        Public Sub New(image As Image, challenge As String)
            Me.image = image
            Me.challenge = challenge
        End Sub

#End Region

#Region "IDisposable"

        Private disposedValue As Boolean
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    If image IsNot Nothing Then image.Dispose()
                End If
            End If
            disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

#End Region

    End Class

End Namespace


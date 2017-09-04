Imports System.Text.RegularExpressions

Public Class SubtitleParser

    Public Class SubtitleItem

        Public Sub New()
        End Sub

        Public Sub New(_ID As Integer, _BeginTime As Double, _EndTime As Double, _Text As String)
            ID = _ID
            BeginTime = _BeginTime
            EndTime = _EndTime
            Text = _Text
        End Sub

        Public Property ID As Integer
            Get
                Return m_Id
            End Get
            Set(value As Integer)
                m_Id = value
            End Set
        End Property
        Private m_Id As Integer

        Public Property BeginTime As Double
            Get
                Return m_BeginTime
            End Get
            Set(value As Double)
                m_BeginTime = value
            End Set
        End Property
        Private m_BeginTime As Double

        Public Property EndTime As Double
            Get
                Return m_EndTime
            End Get
            Set(value As Double)
                m_EndTime = value
            End Set
        End Property
        Private m_EndTime As Double

        Public Property Text As String
            Get
                Return m_Text
            End Get
            Set(value As String)
                m_Text = value
            End Set
        End Property
        Private m_Text As String
    End Class

    Public Sub New()
    End Sub

    Public Shared Function Parse(subtitleText As String) As List(Of SubtitleItem)
        Dim Result As New List(Of SubtitleItem)
        Dim strno As Integer = 0

        Dim SRTRegexCollection As MatchCollection = Regex.Matches(subtitleText, "(?<sequence>\d+)\r\n(?<start>\d{2}\:\d{2}\:\d{2},\d{3}) --\> (?<end>\d{2}\:\d{2}\:\d{2},\d{3})\r\n(?<text>[\s\S]*?\r\n\r\n)", RegexOptions.Compiled Or RegexOptions.ECMAScript)

        If SRTRegexCollection.Count = 0 Then
            MessageBox.Show("Couldn't parse your subtitles.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Result
        End If

            For i As Integer = 0 To SRTRegexCollection.Count - 1
                Dim ID As String = SRTRegexCollection(i).Groups("sequence").Value

                Dim StartTimeStr As String = SRTRegexCollection(i).Groups("start").Value
                Dim StartTime As Double = ConvertToSeconds(StartTimeStr)

                Dim EndTimeStr As String = SRTRegexCollection(i).Groups("end").Value
                Dim EndTime As Double = ConvertToSeconds(EndTimeStr)

                Dim Text As String = SRTRegexCollection(i).Groups("text").Value.TrimEnd()

                Result.Add(New SubtitleItem(ID, StartTime, EndTime, Text))
            Next

        Return CheckForSubtitleIntegrity(Result)

    End Function

    'Assure subtitle list integrity
    Private Shared Function CheckForSubtitleIntegrity(subtitleList As List(Of SubtitleItem)) As List(Of SubtitleItem)

        Dim i As Integer = 0

        While i <> subtitleList.Count

            If i <> subtitleList(i).ID - 1 Then

                Dim subtitleIndex As Integer = subtitleList(i).ID - 1

                Dim endTimePreviousSubtitle As Double = 0
                If i > 1 Then endTimePreviousSubtitle = subtitleList(i - 1).EndTime
                Dim startTimeNextSubtitle As Double = subtitleList(i).BeginTime
                Dim subtitleMissingCount As Integer = subtitleIndex - i

                Dim offsetSubtitle As Double = GetTimeOffsetSubtitle(endTimePreviousSubtitle, startTimeNextSubtitle, subtitleMissingCount)

                Do Until i <= subtitleIndex

                    Dim PreviousSubtitleItem As SubtitleItem = New SubtitleItem(0, 0, 0, String.Empty)
                    If i > 0 Then PreviousSubtitleItem = subtitleList(i - 1)

                    If PreviousSubtitleItem IsNot Nothing AndAlso i < subtitleList.Count Then
                        subtitleList.Insert(i, New SubtitleItem(i, PreviousSubtitleItem.EndTime, PreviousSubtitleItem.EndTime + offsetSubtitle, "<i>MISSING SUBTITLE.</i>" & Environment.NewLine & String.Format("({0} seconds left)", CInt(startTimeNextSubtitle - PreviousSubtitleItem.EndTime))))
                    End If

                    i += 1

                Loop

            End If

            i += 1
        End While


        Return subtitleList
    End Function

    Private Shared Function GetTimeOffsetSubtitle(endTimePrevious As Double, startTimeNext As Double, subtitleCount As Integer) As Double
        Dim timeDifference As Double = startTimeNext - endTimePrevious
        Return CDbl(timeDifference / subtitleCount)
    End Function

    ''Assure subtitle list integrity
    'Private Shared Function CheckForSubtitleIntegrity(subtitleList As List(Of SubtitleItem)) As List(Of SubtitleItem)
    '    For i As Integer = 0 To subtitleList.Count - 1
    '        If i <> subtitleList(i).ID - 1 Then

    '            Dim PreviousSubtitleItem As SubtitleItem = subtitleList(i - 1)
    '            Dim NextSubtitleItem As SubtitleItem = subtitleList(i)

    '            If PreviousSubtitleItem IsNot Nothing AndAlso NextSubtitleItem IsNot Nothing Then
    '                Dim MissingID As Integer = subtitleList(i).ID - 1
    '                If Not MissingID - 1 > subtitleList.Count - 1 Then subtitleList.Insert(MissingID - 1, New SubtitleItem(MissingID, PreviousSubtitleItem.EndTime, NextSubtitleItem.BeginTime, "<i>MISSING SUBTITLE.</i>"))
    '            End If

    '            i += 1
    '        End If
    '    Next
    '    Return subtitleList
    'End Function

    ''' <summary>
    ''' Return a subtitle from the current video time.
    ''' </summary>
    ''' <param name="CurrentTime">Current video time, in SECONDS.</param>
    ''' <param name="SI">List that holds all the subtitles</param>
    ''' <remarks></remarks>
    Private Shared Function GetCurrentSubFromTime(CurrentTime As Double, SI As List(Of SubtitleItem)) As SubtitleItem
        If SI.Count = 0 Then Return New SubtitleItem(1, 0, 0, String.Empty)
        Dim Result As SubtitleItem = SI.Find(Function(x) Math.Round(CurrentTime) >= Math.Round(x.BeginTime) AndAlso Math.Round(CurrentTime) <= Math.Round(x.EndTime))

        Dim Offset As Integer = 5

        Do Until Result IsNot Nothing
            Result = SI.Find(Function(x) Math.Round(CurrentTime) >= Math.Round(x.BeginTime - Offset) AndAlso Math.Round(CurrentTime) <= Math.Round(x.EndTime + Offset))
            Offset += 5
        Loop

        If Result IsNot Nothing Then
            Return Result
        Else
            Debug.WriteLine("No Index found, returning 0")
            Return New SubtitleItem(1, 0, 0, String.Empty)
        End If
    End Function

    Public Shared Function GetCurrentSubIndexFromTime() As Integer
        If SRTParsed Is Nothing Then Return 0

        Dim CurrentTime As Double = New TimeSpan(0, 0, 0, 0, frmMain.VLCPlayer.input.Time).TotalSeconds

        Dim Index As Integer = GetCurrentSubFromTime(CurrentTime, SRTParsed).ID - 1
        Debug.WriteLine("Index found: {0}", Index)
        Return Index
    End Function

    Public Shared Function ConvertToSeconds(ByVal time As String) As Double
        If time = String.Empty Then Return Nothing

        Dim r As Match = Regex.Match(time, "(.*):(.*):(.*),(.*)")

        Dim hours As Integer = CInt(r.Groups(1).Value)
        Dim minutes As Integer = CInt(r.Groups(2).Value)
        Dim seconds As Integer = CInt(r.Groups(3).Value)
        Dim milliseconds As Integer = CInt(r.Groups(4).Value)

        Dim ts As New TimeSpan(0, hours, minutes, seconds, milliseconds)
        Return ts.TotalSeconds
    End Function

End Class
Public Class SeekPosition

    Private SW As Stopwatch
    Private MilliSecondsOffSet As Double = 0

    Public Sub New()
        SW = New Stopwatch()
    End Sub

    Public ReadOnly Property MilliSecondsElapsed As Double
        Get
            Return MilliSecondsOffSet + SW.Elapsed.TotalMilliseconds
        End Get
    End Property

    Public Sub Start()
        SW.Start()
    End Sub

    Public Sub StopSeeking()
        SW.Stop()
    End Sub

    Public Sub ResetAndStartAt(ByVal Offset As Double)
        MilliSecondsOffSet = Offset
        SW.Reset()
    End Sub

End Class

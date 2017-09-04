Imports DotNetStreamer.SubtitleParser

Module SharedContent

    Public captchaSolved As Boolean = False

    Public showPreferences As Boolean

    Public SRTText As String = String.Empty
    Public SRTParsed As New List(Of SubtitleItem)
    Public SRTIndex As Integer = 0
    Public SRT As SubtitleItem

End Module

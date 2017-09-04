Imports System.Runtime.CompilerServices
Imports DotNetStreamer.DownloadManager

Module DownloadExtension

    <Extension()>
    Public Function ToListViewItem(ByVal Seasons As List(Of DownloadManager.Season)) As ListViewItem
        Return New ListViewItem(Seasons(0).Name) With {.Tag = Seasons(0)}
    End Function

    <Extension()>
    Public Function ToListViewItem(ByVal E As Episode) As ListViewItem
        Return New ListViewItem(New String() {E.Name, "Queued"})
    End Function

    <Extension()>
    Public Function ToListViewItemArray(ByVal Seasons As List(Of DownloadManager.Season)) As ListViewItem()
        Dim L As New List(Of ListViewItem)
        For Each S As Season In Seasons
            L.Add(New ListViewItem(S.Name) With {.Tag = S})
        Next
        Return L.ToArray()
    End Function


    <Extension()>
    Public Function ToListViewItemArray(ByVal Episodes As List(Of Episode)) As ListViewItem()
        Dim L As New List(Of ListViewItem)
        For Each E As Episode In Episodes
            L.Add(New ListViewItem(New String() {E.Name, "Queued."}))
        Next
        Return L.ToArray()
    End Function


    <Extension()>
    Public Function ToEpisodesLVIArray(ByVal S As Show) As ListViewItem()
        Dim LVI As New List(Of ListViewItem)
        For Each E As Episode In S.Seasons(0).Episodes

            LVI.Add(New ListViewItem(New String() {E.Name, "Queued."}) With {.Tag = S})
        Next
        Return LVI.ToArray()
    End Function

    <Extension()>
    Public Function FirstEpisode(ByVal Show As Show) As Episode
        Return Show.Seasons(0).Episodes(0)
    End Function

    <Extension()> _
    Public Function Iterate(Of T)(source As IEnumerable(Of T), act As Action(Of T)) As IEnumerable(Of T)
        For Each element As T In source
            act(element)
        Next
        Return source
    End Function

End Module

Module TreeviewExtensions

    <System.Runtime.CompilerServices.Extension> _
    Public Function FlattenTree(tv As TreeView) As IEnumerable(Of TreeNode)
        Return FlattenTree(tv.Nodes)
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Function FlattenTree(coll As TreeNodeCollection) As IEnumerable(Of TreeNode)
        Return coll.Cast(Of TreeNode)().Concat(coll.Cast(Of TreeNode)().SelectMany(Function(x) FlattenTree(x.Nodes)))
    End Function
End Module




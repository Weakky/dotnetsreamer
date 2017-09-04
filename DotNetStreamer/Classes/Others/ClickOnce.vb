Imports System.Deployment.Application

Public Class ClickOnce
    Dim WithEvents iphm As InPlaceHostingManager = Nothing
    Public Event InformationMsg(ByVal Message As String)
    Public Event ErrorMsg(ByVal Message As String)
    Public Event ProgressChanged(ByVal Progress As Integer, ByVal Completed As Long, ByVal OutOf As Long)
    Public Event Updated()
    Public Event Installed()
    Public CurrentVersion As Version
    Dim CanShow As Boolean = False
    Dim ShouldRestart As Boolean = False
    Public debug As Boolean = False ' Set to true to make it fake update

    Public Sub InstallApplication(ByVal deployManifestUriStr As String)
        Try
            Dim deploymentUri As New Uri(deployManifestUriStr)
            iphm = New InPlaceHostingManager(deploymentUri, False)
        Catch uriEx As UriFormatException
            RaiseEvent ErrorMsg("Cannot install the application.")
            Return
        Catch platformEx As PlatformNotSupportedException
            RaiseEvent ErrorMsg("Requires Windows XP or higher.")
            Return
        Catch argumentEx As ArgumentException
            RaiseEvent ErrorMsg("Cannot install the application.")
            Return
        End Try

        iphm.GetManifestAsync()
    End Sub

    Private Sub iphm_GetManifestCompleted(ByVal sender As Object, ByVal e As GetManifestCompletedEventArgs) Handles iphm.GetManifestCompleted
        ' Check for an error. 
        '  If (e.Error IsNot Nothing) Then
        ' Cancel download and install.
        ' Return
        '  End If

        ' Dim isFullTrust As Boolean = CheckForFullTrust(e.ApplicationManifest) 

        ' Verify this application can be installed. 
        Try
            ' the true parameter allows InPlaceHostingManager 
            ' to grant the permissions requested in the application manifest. 
            iphm.AssertApplicationRequirements(True)
        Catch ex As Exception
            RaiseEvent Installed() : Exit Sub
            'RaiseEvent ErrorMsg("An error occurred.")
        End Try

        ' Use the information from GetManifestCompleted() to confirm  
        ' that the user wants to proceed. 

        If e.Version > CurrentVersion Or Debug Then
            RaiseEvent InformationMsg("Updating to " & e.Version.ToString() & "...")
        Else
            RaiseEvent Installed() : Exit Sub
        End If

        ' Download the deployment manifest.  
        ' Usually, this shouldn't throw an exception unless  
        ' AssertApplicationRequirements() failed, or you did not call that method 
        ' before calling this one. 

        Try
            CanShow = True
            iphm.DownloadApplicationAsync()
            ShouldRestart = True
        Catch downloadEx As Exception
            RaiseEvent ErrorMsg("Cannot initiate download.")
            Return
        End Try
    End Sub

    Private Sub iphm_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles iphm.DownloadProgressChanged
        If CanShow = True Then RaiseEvent ProgressChanged(e.ProgressPercentage, e.BytesDownloaded, e.TotalBytesToDownload)
    End Sub

    Private Sub iphm_DownloadApplicationCompleted(ByVal sender As Object, ByVal e As DownloadApplicationCompletedEventArgs) Handles iphm.DownloadApplicationCompleted
        ' Check for an error. 
        If (e.Error IsNot Nothing) Then
            ' Cancel download and install.
            RaiseEvent ErrorMsg(e.Error.Message)
            Return
        End If

        If ShouldRestart = True Then
            RaiseEvent Updated()
        Else
            RaiseEvent Installed()
        End If

    End Sub

    Public Sub New(ByVal CurrentVersion As Version)
        Me.CurrentVersion = CurrentVersion
    End Sub
End Class

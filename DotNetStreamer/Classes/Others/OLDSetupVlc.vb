Option Strict On
Imports System.Net
Imports System.IO
Imports Microsoft.Win32

Public Class SetupVLCOld



#Region "Private Fields"
    Private WithEvents Client As New WebClient
    Private RemotePath As New Uri("https://dl.dropboxusercontent.com/u/19643954/vlc-2.1.3-win32.exe")
    Public LocalPath As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "VLC", "VLC 2.1.0.3.exe")

    Private VLCFolderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "VideoLAN", "VLC")

    Private SW As Stopwatch
    Private VLCRegistrySubKey As RegistryKey
#End Region

#Region "Events"
    Public Event VLCDownloadProgress(Progress As Integer, TimeLeft As String, Speed As String)
    Public Event VLCDownloadFinished(e As Exception)
    Public Event VLCRegistered(e As Exception)
#End Region

#Region "Properties"

    Private _IsVLCInstalled As Boolean
    Public Property IsVLCInstalled As Boolean
        Get
            Return _IsVLCInstalled
        End Get
        Set(value As Boolean)
            _IsVLCInstalled = value
        End Set
    End Property

    Private _VLCInstallPath As String
    Public Property VLCInstallPath As String
        Get
            Return _VLCInstallPath
        End Get
        Set(value As String)
            _VLCInstallPath = value
        End Set
    End Property

    Private _VLCVersion As String
    Public Property VLCVersion As String
        Get
            Return _VLCVersion
        End Get
        Set(value As String)
            _VLCVersion = value
        End Set
    End Property

    Public ReadOnly Property VLCUninstallPath As String
        Get
            Return Path.Combine(_VLCInstallPath, "uninstall.exe")
        End Get
    End Property

    Public ReadOnly Property VLCEXEPath As String
        Get
            Return Path.Combine(_VLCInstallPath, "vlc.exe")
        End Get
    End Property

#End Region


    Public Sub New()
        SW = New Stopwatch()
        VLCRegistrySubKey = GetVLCRegistryKey()

        If IsVLCInstalledOnComputer(VLCRegistrySubKey) Then
            _IsVLCInstalled = True
            _VLCVersion = GetVLCVersion(VLCRegistrySubKey)
            _VLCInstallPath = GetVLCInstallDir(VLCRegistrySubKey)
        Else
            _IsVLCInstalled = False
            _VLCVersion = String.Empty
            _VLCInstallPath = String.Empty
        End If

    End Sub

    Public Sub SetupVLC()

        If _IsVLCInstalled AndAlso Not _VLCVersion = "2.1.5" Then
            MessageBox.Show("VLC is properly installed, but you don't have the required version: 2.1.5." & Environment.NewLine & "Click 'OK' to uninstall VLC, and then restart .NET Streamer.")
            If File.Exists(VLCUninstallPath) Then Process.Start(VLCUninstallPath) Else Throw New Exception("Uninstaller not found.")
            Application.Exit()
        End If

        If _IsVLCInstalled Then
            If Directory.Exists(_VLCInstallPath) AndAlso File.Exists(VLCEXEPath) Then
                CheckVLCVersionAndRegister(VLCEXEPath, VLCUninstallPath)
                Settings.Data.FolderVLCPath = _VLCInstallPath
                ' Settings.SerializeToXML()
            End If
        Else
            Dim VLCPath As New IO.FileInfo(LocalPath)
            VLCPath.Directory.Create()

            If File.Exists(LocalPath) Then
                PromptVLCAlreadyInstalled()
            Else
                StartDownloadVLC()
            End If

        End If
    End Sub

    Private Sub StartDownloadVLC()
        Client.DownloadFileAsync(RemotePath, LocalPath)
        SW.Start()
    End Sub
    Private Sub PromptVLCAlreadyInstalled()
        Try
            MessageBox.Show("You already downloaded VLC but the folder installation hasn't been found." & Environment.NewLine & _
                            "Click 'OK' to install it, then restart .NET Streamer." & Environment.NewLine & _
                                "Be careful. It does require to launch it as admin.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Process.Start(LocalPath)
            Application.Exit()
        Catch ex As System.ComponentModel.Win32Exception
            MessageBox.Show("The installer requires admin rights. Accept the UAC dialog or installation will fail.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Process.Start(LocalPath)
            Application.Exit()
        End Try
    End Sub

    Private Sub CheckVLCVersionAndRegister(VLCEXEPath As String, VLCUninstallPath As String)
        Try
            If Not File.Exists(VLCEXEPath) Then
                Throw New Exception("VLC.exe does not exist.")
            Else
                RegisterDll(Path.Combine(_VLCInstallPath, "axvlc.dll"))
                RaiseEvent VLCRegistered(Nothing)
            End If
        Catch ex As Exception
            RaiseEvent VLCRegistered(ex)
        End Try
    End Sub

    Private Function GetVLCRegistryKey() As RegistryKey
        '32bits: HKEY_LOCAL_MACHINE\SOFTWARE\VideoLan\VLC
        '64bits: HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\VideoLAN\VLC
        Dim keyName As String = If(Environment.Is64BitOperatingSystem, "SOFTWARE\Wow6432Node\VideoLAN\VLC", "SOFTWARE\VideoLan\VLC")
        Dim rSubKey As RegistryKey = Registry.LocalMachine.OpenSubKey(keyName, False)
        Return rSubKey
    End Function
    Private Function IsVLCInstalledOnComputer(registryKey As RegistryKey) As Boolean
        If registryKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetVLCVersion(registryKey As RegistryKey) As String
        If registryKey IsNot Nothing Then Return registryKey.GetValue("Version").ToString() Else Throw New Exception("Registry key does not exist.")
    End Function

    Private Function GetVLCInstallDir(registryKey As RegistryKey) As String
        If registryKey IsNot Nothing Then Return registryKey.GetValue("InstallDir").ToString() Else Throw New Exception("Registry key does not exist.")
    End Function


    Private Sub client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles Client.DownloadProgressChanged
        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100

        Dim progressDownload As Integer
        Dim timeLeftDownload As String
        Dim speedDownload As String

        progressDownload = Int32.Parse(Math.Truncate(percentage).ToString())

        If progressDownload = 0 Then
            timeLeftDownload = "Time Left: Estimating..."
        Else
            Dim secondsRemaining As Double = (100 - progressDownload) * SW.Elapsed.TotalSeconds / progressDownload
            Dim ts As TimeSpan = TimeSpan.FromSeconds(secondsRemaining)
            timeLeftDownload = String.Format("Time Left: {0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds)
            Select Case secondsRemaining
                Case Is < 60
                    timeLeftDownload = String.Format("Time Left: {0} sec", ts.Seconds)
                Case Is < 3600
                    timeLeftDownload = String.Format("Time Left: {0} min {1} sec", ts.Minutes, ts.Seconds)
                Case Is < 86400
                    timeLeftDownload = String.Format("Time Left: {0} hours {1} min {2} sec", ts.Hours, ts.Minutes, ts.Seconds)
                Case Else
                    timeLeftDownload = String.Format("Time Left: {0} days {1} hours {2} min {3} sec", ts.Days, ts.Hours, ts.Minutes, ts.Seconds)
            End Select
        End If

        Dim kbps As Double = bytesIn / (SW.Elapsed.TotalSeconds * 1024)
        If kbps < 1024 Then
            speedDownload = String.Format("Speed: {0:0.00} KB/s", kbps)
        Else
            speedDownload = String.Format("Speed: {0:0.00} MB/s", kbps / 1024)
        End If

        RaiseEvent VLCDownloadProgress(progressDownload, timeLeftDownload, speedDownload)
    End Sub

    Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles Client.DownloadFileCompleted
        SW.Stop()
        If e.Error IsNot Nothing Then SendExceptionToKeepTrackOfIt(e.Error)
        RaiseEvent VLCDownloadFinished(e.Error)
    End Sub

    Private Sub RegisterDll(filePath As String)
        Try
            ''/s' : Specifies regsvr32 to run silently and to not display any message boxes.
            Dim arg_fileinfo As String = (Convert.ToString("/s" + " " + """") & filePath) + """"
            Dim reg As New Process()
            'This file registers .dll files as command components in the registry.
            reg.StartInfo.FileName = "regsvr32.exe"
            reg.StartInfo.Arguments = arg_fileinfo
            reg.StartInfo.UseShellExecute = False
            reg.StartInfo.Verb = "runas"
            reg.StartInfo.CreateNoWindow = True
            reg.StartInfo.RedirectStandardOutput = True
            reg.Start()
            reg.WaitForExit()
            reg.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SendExceptionToKeepTrackOfIt(ByVal e As Exception)
        Dim SB As New System.Text.StringBuilder()

        SB.AppendLine(String.Format("New Unhandled Exception at: {0} - OS: {1}", Now(), New Microsoft.VisualBasic.Devices.Computer().Info.OSFullName))
        SB.Append(e.ToString())
        SB.AppendLine()
        SB.AppendLine("-----------------------------------------------------------------------")
        SB.AppendLine()

        Try
            Using HTTP As New Utility.Http
                HTTP.GetResponse(Utility.Http.Verb.POST, "http://www.dotnetstreamer.com/online.php", String.Format("exception={0}", System.Web.HttpUtility.UrlEncode(SB.ToString)))
            End Using
        Catch exc As Exception
        End Try
    End Sub

End Class

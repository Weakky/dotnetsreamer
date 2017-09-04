Option Strict On
Imports System.Net
Imports System.IO
Imports Microsoft.Win32

Public Class SetupVLC

   

#Region "Private Fields"

    Private WithEvents Client As WebClient
    Private RemotePath As New Uri("http://ftp.free.org/mirrors/videolan/vlc/2.1.5/win32/vlc-2.1.5-win32.exe")
    Public LocalPath As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "VLC", "VLC 2.1.5.exe")

    Private VLCFolderPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "VideoLAN", "VLC")
    Private SW As Stopwatch
    Private VLCRegistrySubKey As RegistryKey
    Private Const setupSizeInBytes As Long = 24743106 'Size in bytes to check if the file isn't corrupted
#End Region

#Region "Events"
    Public Event VLCDownloadProgress(Progress As Integer, TimeLeft As String, Speed As String)
    Public Event VLCDownloadFinished(e As Exception)
    Public Event VLCRegistered(path As String, e As Exception)
    Public Event VLCUnregistered()
#End Region

#Region "Properties"

    Private _IsVLCInstalled As Boolean
    Public ReadOnly Property IsVLCInstalled As Boolean
        Get
            Return _IsVLCInstalled
        End Get
    End Property

    Private _IsVLCAlreadyDownloaded As Boolean
    Public ReadOnly Property IsVLCAlreadyDownload As Boolean
        Get
            Return _IsVLCAlreadyDownloaded
        End Get
    End Property

    Private _VLCInstallPath As String
    Public ReadOnly Property VLCInstallPath As String
        Get
            Return _VLCInstallPath
        End Get
    End Property

    Private _VLCVersion As String
    Public ReadOnly Property VLCVersion As String
        Get
            Return _VLCVersion
        End Get
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
        Client = New WebClient()


        VLCRegistrySubKey = GetVLCRegistryKey()

        If File.Exists(LocalPath) Then _IsVLCAlreadyDownloaded = True


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

#Region " Public Methods "

    Public Sub Setup()

        If _IsVLCInstalled Then
            If Directory.Exists(_VLCInstallPath) AndAlso File.Exists(VLCEXEPath) Then
                CheckVLCVersionAndRegister(VLCEXEPath)
                Settings.Data.FolderVLCPath = _VLCInstallPath
                ' Settings.SerializeToXML()
            End If
        Else
            Dim VLCPath As New FileInfo(LocalPath)
            VLCPath.Directory.Create()

            If Not _IsVLCAlreadyDownloaded Then StartDownloadVLC()
        End If
    End Sub

    Public Sub LaunchSetupFile()

        Try
            If File.Exists(LocalPath) AndAlso New FileInfo(LocalPath).Length = setupSizeInBytes Then
                Process.Start(LocalPath, String.Format("{0} /L=1033 /S", LocalPath))
            Else
                File.Delete(LocalPath)
                MessageBox.Show("VLC Setup is corrupted. Please relaunch .NET Streamer to download it again.", ".NET Streamer")
                Application.Restart()
            End If
        Catch ex As System.ComponentModel.Win32Exception
            MessageBox.Show("The installer requires admin rights. Accept the UAC dialog or installation will fail.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If File.Exists(LocalPath) Then Process.Start(LocalPath, String.Format("{0} /L=1033 /S", LocalPath))
        End Try

    End Sub


    Private tmrCheckInstallation As New Timer With {.Interval = 1000}
    Public Sub StartCheckForVLCIntallation()
        tmrCheckInstallation.Start()
        AddHandler tmrCheckInstallation.Tick, AddressOf CheckInstallationTick
    End Sub

    Private Sub CheckForVlcToBeInstalled()
        Dim rKey As RegistryKey = GetVLCRegistryKey()
        If IsVLCInstalledOnComputer(rKey) Then
            Debug.WriteLine("VLC Installed")
            tmrCheckInstallation.Stop()
            _IsVLCInstalled = True
            _VLCVersion = GetVLCVersion(rKey)
            _VLCInstallPath = GetVLCInstallDir(rKey)
            CheckVLCVersionAndRegister(VLCEXEPath)
        Else
            Debug.WriteLine("Checking..")
        End If
    End Sub

    Private Sub CheckInstallationTick(sender As Object, e As EventArgs)
        CheckForVlcToBeInstalled()
    End Sub
    Public Sub LaunchUninstaller()
        Try
            If File.Exists(VLCUninstallPath) Then
                Process.Start(VLCUninstallPath)
            Else
                MessageBox.Show("Uninstaller hasn't been found. Uninstall VLC by yourself, and launch .NET Streamer again.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End If

        Catch ex As System.ComponentModel.Win32Exception
            MessageBox.Show("The uninstaller requires admin rights. Accept the UAC dialog or uninstallation will fail.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If File.Exists(VLCUninstallPath) Then
                Process.Start(VLCUninstallPath)
            Else
                MessageBox.Show("Uninstaller hasn't been found. Uninstall VLC by yourself, and launch .NET Streamer again.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End If
        End Try
    End Sub

    Private tmrCheckUninstallation As New Timer With {.Interval = 1000}
    Public Sub StartCheckForVLCUnintallation()
        tmrCheckUninstallation.Start()
        AddHandler tmrCheckUninstallation.Tick, AddressOf CheckUninstallationTick
    End Sub

    Private Sub CheckUninstallationTick(sender As Object, e As EventArgs)
        CheckForVlcToBeUninstalled()
    End Sub
    Private Sub CheckForVlcToBeUninstalled()

        If Not IsVLCUninstallerProcessRunning() Then
            tmrCheckUninstallation.Stop()
            If MessageBox.Show("You just closed the VLC Uninstaller. Do you wanna cancel the uninstallation ?" & Environment.NewLine & _
                               "If you don't, then click 'NO' and the uninstaller will launch again. Otherwise, app wil exit.", ".NET Streamer", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
                Application.Exit()
            Else
                LaunchUninstaller()
                tmrCheckUninstallation.Start()
            End If
        End If

        Dim rKey As RegistryKey = GetVLCRegistryKey()
        If Not IsVLCInstalledOnComputer(rKey) Then
            Debug.WriteLine("VLC Uninstalled")
            tmrCheckUninstallation.Stop()
            _IsVLCInstalled = False
            _VLCVersion = String.Empty
            _VLCInstallPath = String.Empty
            RaiseEvent VLCUnregistered()
        Else
            Debug.WriteLine("Checking..")
        End If
    End Sub
    Private Function IsVLCSetupProcessRunning() As Boolean
        If Process.GetProcessesByName("VLC 2.1.5").Length = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function IsVLCUninstallerProcessRunning() As Boolean
        If Process.GetProcessesByName("Au_").Length = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region " Private Methods "
    Private Sub StartDownloadVLC()
        Client.DownloadFileAsync(RemotePath, LocalPath)
        SW.Start()
    End Sub
    Private Sub PromptVLCAlreadyInstalled()
        Try
            MessageBox.Show("You already downloaded VLC but the folder installation hasn't been found." & Environment.NewLine & _
                            "Click 'OK' to install it, then restart .NET Streamer." & Environment.NewLine & _
                                "Be careful. It does require to launch it as admin.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If File.Exists(LocalPath) Then Process.Start(LocalPath)
            Application.Exit()
        Catch ex As System.ComponentModel.Win32Exception
            MessageBox.Show("The installer requires admin rights. Accept the UAC dialog or installation will fail.", ".NET Streamer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If File.Exists(LocalPath) Then Process.Start(LocalPath)
            Application.Exit()
        End Try
    End Sub
    Private Sub CheckVLCVersionAndRegister(VLCEXEPath As String)
        Try
            If Not File.Exists(VLCEXEPath) Then
                Throw New FileNotFoundException("VLC.exe does not exist.")
            Else
                RegisterDll(Path.Combine(_VLCInstallPath, "axvlc.dll"))
                RaiseEvent VLCRegistered(_VLCInstallPath, Nothing)
            End If
        Catch ex As Exception
            RaiseEvent VLCRegistered(String.Empty, ex)
        End Try
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
#End Region
#Region " Private Functions "


    Private Function GetVLCRegistryKey() As RegistryKey
        '32bits: HKEY_LOCAL_MACHINE\SOFTWARE\VideoLan\VLC
        '64bits: HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\VideoLAN\VLC
        Dim keyName As String = If(Environment.Is64BitOperatingSystem, "SOFTWARE\Wow6432Node\VideoLAN\VLC", "SOFTWARE\VideoLan\VLC")
        Dim rSubKey As RegistryKey = Registry.LocalMachine.OpenSubKey(keyName, False)
        Return rSubKey
    End Function
    Private Function IsVLCInstalledOnComputer(registryKey As RegistryKey) As Boolean
        If registryKey IsNot Nothing Then
            If Not File.Exists(Path.Combine(GetVLCInstallDir(registryKey), "axvlc.dll")) Then Return False Else Return True
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

#End Region

#Region " VLC Async Download Callbacks "

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
        RaiseEvent VLCDownloadFinished(e.Error)
    End Sub

#End Region


End Class

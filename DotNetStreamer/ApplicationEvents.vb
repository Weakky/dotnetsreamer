Imports System.Reflection
Imports System.Threading

Namespace My

    ' Les événements suivants sont disponibles pour MyApplication :
    ' 
    ' Startup : déclenché au démarrage de l'application avant la création du formulaire de démarrage.
    ' Shutdown : déclenché après la fermeture de tous les formulaires de l'application. Cet événement n'est pas déclenché si l'application se termine de façon anormale.
    ' UnhandledException : déclenché si l'application rencontre une exception non gérée.
    ' StartupNextInstance : déclenché lors du lancement d'une application à instance unique et si cette application est déjà active. 
    ' NetworkAvailabilityChanged : déclenché lorsque la connexion réseau est connectée ou déconnectée.

    Partial Friend Class MyApplication


        Private Sub MyApplication_UnhandledException(ByVal _
       sender As Object, ByVal e As  _
       Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) _
       Handles Me.UnhandledException

            'Send exception to keep track of it
            SendExceptionToKeepTrackOfIt(e)

            ' If the user clicks No, then exit.
            e.ExitApplication = _
                MessageBox.Show("An unhandled exception has just occured. Click 'OK' to quit .NET Streamer" & Environment.NewLine & _
                                String.Format("Error: {0}", e.Exception.Message), ".NET Streamer - Unhandled Exception", _
                        MessageBoxButtons.OK, _
                        MessageBoxIcon.Error) _
                        = DialogResult.OK
        End Sub

        Private Sub SendExceptionToKeepTrackOfIt(ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs)
            Dim SB As New System.Text.StringBuilder()

            SB.AppendLine(String.Format("New Unhandled Exception at: {0} - PC Name: {1} - OS: {2}", Now(), Environment.MachineName, New Microsoft.VisualBasic.Devices.Computer().Info.OSFullName))
            SB.Append(e.Exception.ToString())
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
End Namespace


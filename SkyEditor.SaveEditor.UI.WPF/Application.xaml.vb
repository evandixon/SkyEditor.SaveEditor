Imports System.Windows.Threading
Imports SkyEditor.UI.WPF

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.
    Private Sub Application_Exit(sender As Object, e As ExitEventArgs) Handles Me.Exit
        StartupHelpers.RunExitSequence()
    End Sub

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        StartupHelpers.EnableErrorDialog()
        Application.Current.Dispatcher.BeginInvoke(Async Sub()
                                                       Dim mainWindow = Await StartupHelpers.RunWPFStartupSequence(New WPFCoreSkyEditorPlugin(New SkyEditorInfo))
                                                       mainWindow.DisplayStatusBar = False
                                                   End Sub)
    End Sub
End Class

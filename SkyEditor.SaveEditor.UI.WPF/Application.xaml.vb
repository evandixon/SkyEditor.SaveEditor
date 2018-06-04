Imports System.Windows.Threading
Imports SkyEditor.UI.WPF

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.
    Private Sub Application_Exit(sender As Object, e As ExitEventArgs) Handles Me.Exit
        StartupHelpers.Cleanup()
    End Sub

    Private Async Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        StartupHelpers.EnableErrorDialog()
        Await StartupHelpers.ShowMainWindow(New WPFCoreSkyEditorPlugin(New SkyEditorInfo))
    End Sub
End Class

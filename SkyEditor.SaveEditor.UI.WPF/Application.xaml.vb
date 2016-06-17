﻿Imports SkyEditor.UI.WPF

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.
    Private Sub Application_Exit(sender As Object, e As ExitEventArgs) Handles Me.Exit
        StartupHelpers.RunExitSequence()
    End Sub

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
#If DEBUG Then
        PresentationTraceSources.Refresh()
        PresentationTraceSources.DataBindingSource.Listeners.Add(New ConsoleTraceListener)
        PresentationTraceSources.DataBindingSource.Listeners.Add(New DebugTraceListener)
        PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Warning Or SourceLevels.Error
#End If
        Application.Current.Dispatcher.BeginInvoke(Sub()
                                                       StartupHelpers.RunWPFStartupSequence(New WPFCoreSkyEditorPlugin(New SkyEditorInfo))
                                                   End Sub)
    End Sub
End Class

#If DEBUG Then
Public Class DebugTraceListener
    Inherits TraceListener

    Public Overrides Sub Write(message As String)
    End Sub

    Public Overrides Sub WriteLine(message As String)
        Debugger.Break()
    End Sub
End Class
#End If

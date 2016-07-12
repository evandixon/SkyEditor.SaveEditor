Imports SkyEditor.Core

Public Class PluginDefinition
    Inherits SkyEditor.Core.SkyEditorPlugin

    Public Overrides ReadOnly Property Credits As String
        Get
            Return My.Resources.Language.PluginCredits
        End Get
    End Property

    Public Overrides ReadOnly Property PluginAuthor As String
        Get
            Return My.Resources.Language.PluginAuthor
        End Get
    End Property

    Public Overrides ReadOnly Property PluginName As String
        Get
            Return My.Resources.Language.PluginName
        End Get
    End Property

    Public Overrides Sub Load(manager As PluginManager)
        MyBase.Load(manager)
        manager.CurrentIOUIManager.RegisterIOFilter("sav", My.Resources.Language.RawSaveFile)
        manager.CurrentIOUIManager.RegisterIOFilter("skypkm", My.Resources.Language.SkyPkmFile)
    End Sub
End Class

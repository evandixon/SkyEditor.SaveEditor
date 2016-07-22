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
        manager.CurrentIOUIManager.RegisterIOFilter("skypkmex", My.Resources.Language.SkyPkmExFile)
        manager.CurrentIOUIManager.RegisterIOFilter("skypkmq", My.Resources.Language.SkyPkmQFile)
        manager.CurrentIOUIManager.RegisterIOFilter("tdpkm", My.Resources.Language.TDPkmFile)
        manager.CurrentIOUIManager.RegisterIOFilter("tdpkmex", My.Resources.Language.TDPkmExFile)
        manager.CurrentIOUIManager.RegisterIOFilter("rbpkm", My.Resources.Language.RBPkmFile)
        manager.CurrentIOUIManager.RegisterIOFilter("game_data", My.Resources.Language.GameDataFile)
    End Sub
End Class

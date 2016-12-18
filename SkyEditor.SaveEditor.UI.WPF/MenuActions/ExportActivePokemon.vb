Imports System.Reflection
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents
Imports SkyEditor.UI.WPF

Namespace MenuActions
    Public Class ExportActivePokemon
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuPokemon, My.Resources.Language.MenuPokemonExportActive})
            DevOnly = True
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(IParty).GetTypeInfo}
        End Function

        Public Overrides Async Function SupportsObject(Obj As Object) As Task(Of Boolean)
            Return (Await MyBase.SupportsObject(Obj)) AndAlso
                DirectCast(Obj, IParty).Party IsNot Nothing AndAlso
                DirectCast(Obj, IParty).SelectedPokemon IsNot Nothing AndAlso
                DirectCast(Obj, IParty).SelectedPokemon.CanSaveAs(CurrentPluginManager)
        End Function

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If Await SupportsObject(item) Then
                    Dim pkm = DirectCast(item, IParty).SelectedPokemon
                    Dim s = CurrentPluginManager.CurrentIOUIManager.GetSaveFileDialog(pkm)
                    If s.ShowDialog = Forms.DialogResult.OK Then
                        pkm.Save(s.FileName, CurrentPluginManager)
                    End If

                    If TypeOf pkm.File Is SkyActivePokemon Then
                        DirectCast(pkm.File, SkyActivePokemon).DumpToConsole(CurrentPluginManager.CurrentConsoleProvider)
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace


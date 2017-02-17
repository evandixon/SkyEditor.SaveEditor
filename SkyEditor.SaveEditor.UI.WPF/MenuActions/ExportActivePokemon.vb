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

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(IParty).GetTypeInfo}
        End Function

        Public Overrides Async Function SupportsObject(Obj As Object) As Task(Of Boolean)
            Return (Await MyBase.SupportsObject(Obj)) AndAlso
                DirectCast(Obj, IParty).Party IsNot Nothing AndAlso
                DirectCast(Obj, IParty).SelectedPokemon IsNot Nothing AndAlso
                DirectCast(Obj, IParty).SelectedPokemon.CanSaveAs(CurrentApplicationViewModel.CurrentPluginManager)
        End Function

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If Await SupportsObject(item) Then
                    Dim pkm = DirectCast(item, IParty).SelectedPokemon
                    Dim s = CurrentApplicationViewModel.GetSaveFileDialog(pkm)
                    If s.ShowDialog = Forms.DialogResult.OK Then
                        Await pkm.Save(s.FileName, CurrentApplicationViewModel.CurrentPluginManager)
                    End If

                    If TypeOf pkm.Model Is SkyActivePokemon Then
                        DirectCast(pkm.Model, SkyActivePokemon).DumpToConsole(CurrentApplicationViewModel.CurrentPluginManager.CurrentConsoleProvider)
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace


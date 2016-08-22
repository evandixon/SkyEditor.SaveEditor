Imports System.Reflection
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents
Imports SkyEditor.UI.WPF

Namespace MenuActions
    Public Class ExportPokemon
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuPokemon, My.Resources.Language.MenuPokemonExport})
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(IPokemonStorage).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            Return MyBase.SupportsObject(Obj) AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox IsNot Nothing AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox.SelectedPokemon IsNot Nothing AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox.SelectedPokemon.CanSaveAs(CurrentPluginManager)
        End Function

        Public Overrides Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If SupportsObject(item) Then
                    Dim pkm = DirectCast(item, IPokemonStorage).SelectedBox.SelectedPokemon
                    Dim s = CurrentPluginManager.CurrentIOUIManager.GetSaveFileDialog(pkm)
                    If s.ShowDialog = Forms.DialogResult.OK Then
                        pkm.Save(s.FileName, CurrentPluginManager)

                        If TypeOf pkm.File Is SkyStoredPokemon Then
                            DirectCast(pkm.File, SkyStoredPokemon).DumpToConsole(CurrentPluginManager.CurrentConsoleProvider)
                        End If
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace


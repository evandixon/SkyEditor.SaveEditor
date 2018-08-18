Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents
Imports SkyEditor.UI.WPF

Namespace MenuActions
    Public Class ExportPokemon
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuPokemon, My.Resources.Language.MenuPokemonExport})

            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

            If applicationViewModel Is Nothing Then
                Throw New ArgumentNullException(NameOf(applicationViewModel))
            End If

            CurrentPluginManager = pluginManager
            CurrentApplicationViewModel = applicationViewModel
        End Sub

        Protected Property CurrentPluginManager As PluginManager

        Protected Property CurrentApplicationViewModel As ApplicationViewModel

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(IPokemonStorage).GetTypeInfo}
        End Function

        Public Overrides Async Function SupportsObject(Obj As Object) As Task(Of Boolean)
            Return (Await MyBase.SupportsObject(Obj)) AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox IsNot Nothing AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox.SelectedPokemon IsNot Nothing AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox.SelectedPokemon.CanSaveAs(CurrentPluginManager)
        End Function

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If Await SupportsObject(item) Then
                    Dim pkm = DirectCast(item, IPokemonStorage).SelectedBox.SelectedPokemon
                    Dim s = CurrentApplicationViewModel.GetSaveFileDialog(pkm, False, CurrentPluginManager)
                    If s.ShowDialog = Forms.DialogResult.OK Then
                        Await pkm.Save(s.FileName, CurrentPluginManager)
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace


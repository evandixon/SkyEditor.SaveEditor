Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.IO.PluginInfrastructure
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents
Imports SkyEditor.UI.WPF

Namespace MenuActions
    Public Class ImportActivePokemon
        Inherits MenuAction

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New({My.Resources.Language.MenuPokemon, My.Resources.Language.MenuPokemonImportActive})

            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

            If applicationViewModel Is Nothing Then
                Throw New ArgumentNullException(NameOf(applicationViewModel))
            End If

            CurrentPluginManager = pluginManager
            CurrentApplicationViewModel = applicationViewModel
            DevOnly = True
        End Sub

        Protected Property CurrentPluginManager As PluginManager

        Protected Property CurrentApplicationViewModel As ApplicationViewModel

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(IParty).GetTypeInfo}
        End Function

        Public Overrides Async Function SupportsObject(Obj As Object) As Task(Of Boolean)
            Return Await MyBase.SupportsObject(Obj) AndAlso
                DirectCast(Obj, IParty).SelectedPokemon IsNot Nothing AndAlso
                DirectCast(Obj, IParty).SelectedPokemon.CanSaveAs(CurrentPluginManager)
        End Function

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If Await SupportsObject(item) Then
                    Dim pkm As FileViewModel = DirectCast(item, IParty).SelectedPokemon
                    Dim o = CurrentApplicationViewModel.GetOpenFileDialog(pkm.GetSupportedExtensions(CurrentPluginManager), False)
                    If o.ShowDialog = Forms.DialogResult.OK Then
                        Dim newModel = Await IOHelper.OpenFile(o.FileName, pkm.Model.GetType.GetTypeInfo, CurrentPluginManager)
                        pkm.Model = newModel
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace

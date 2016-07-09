Imports System.Reflection
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents
Imports SkyEditor.UI.WPF

Namespace MenuActions
    Public Class ImportPokemon
        Inherits MenuAction

        Public Sub New()
            MyBase.New({My.Resources.Language.MenuPokemon, My.Resources.Language.MenuPokemonImport})
        End Sub

        Public Overrides Function SupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(IPokemonStorage).GetTypeInfo}
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            Return MyBase.SupportsObject(Obj) AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox IsNot Nothing AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox.SelectedPokemon IsNot Nothing
        End Function

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If SupportsObject(item) Then
                    Dim pkm As FileViewModel = DirectCast(item, IPokemonStorage).SelectedBox.SelectedPokemon
                    Dim o = CurrentPluginManager.CurrentIOUIManager.GetOpenFileDialog(pkm.GetSupportedExtensions(CurrentPluginManager))
                    If o.ShowDialog = Forms.DialogResult.OK Then
                        Dim newModel = Await IOHelper.OpenFile(o.FileName, pkm.File.GetType.GetTypeInfo, CurrentPluginManager)
                        pkm.File = newModel
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace

﻿Imports System.Reflection
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

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(IPokemonStorage).GetTypeInfo}
        End Function

        Public Overrides Async Function SupportsObject(Obj As Object) As Task(Of Boolean)
            Return (Await MyBase.SupportsObject(Obj)) AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox IsNot Nothing AndAlso
                DirectCast(Obj, IPokemonStorage).SelectedBox.SelectedPokemon IsNot Nothing
        End Function

        Public Overrides Async Sub DoAction(Targets As IEnumerable(Of Object))
            For Each item In Targets
                If Await SupportsObject(item) Then
                    Dim pkm As FileViewModel = DirectCast(item, IPokemonStorage).SelectedBox.SelectedPokemon
                    Dim o = CurrentApplicationViewModel.GetOpenFileDialog(pkm.GetSupportedExtensions(CurrentApplicationViewModel.CurrentPluginManager))
                    If o.ShowDialog = Forms.DialogResult.OK Then
                        Dim newModel = Await IOHelper.OpenFile(o.FileName, pkm.Model.GetType.GetTypeInfo, CurrentApplicationViewModel.CurrentPluginManager)
                        pkm.Model = newModel
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace

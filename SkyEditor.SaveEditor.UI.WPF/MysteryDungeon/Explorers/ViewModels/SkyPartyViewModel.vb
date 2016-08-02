Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class SkyPartyViewModel
        Inherits GenericViewModel(Of SkySave)
        Implements IParty
        Implements INotifyModified


        Public Property Party As IEnumerable Implements IParty.Party

        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified

        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As SkySave = model

            Dim pkm As New List(Of FileViewModel)
            For Each item In s.ActivePokemon
                Dim vm As New FileViewModel(item)
                AddHandler vm.Modified, AddressOf OnModified
                pkm.Add(vm)
            Next

            Party = pkm
        End Sub
    End Class

End Namespace

Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class SkyPartyViewModel
        Inherits GenericViewModel(Of SkySave)
        Implements IParty
        Implements INotifyModified
        Implements INotifyPropertyChanged

        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

        Public Property Party As IEnumerable(Of FileViewModel) Implements IParty.Party

        Public Property SelectedPokemon As FileViewModel Implements IParty.SelectedPokemon
            Get
                Return _selectedPokemon
            End Get
            Set(value As FileViewModel)
                If _selectedPokemon IsNot value Then
                    _selectedPokemon = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedPokemon)))
                    Me.RequestMenuItemRefresh()
                End If
            End Set
        End Property
        Dim _selectedPokemon As FileViewModel

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

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim s As SkySave = model

            s.ActivePokemon.Clear()

            For Each item In Party
                s.ActivePokemon.Add(item.File)
            Next
        End Sub
    End Class

End Namespace

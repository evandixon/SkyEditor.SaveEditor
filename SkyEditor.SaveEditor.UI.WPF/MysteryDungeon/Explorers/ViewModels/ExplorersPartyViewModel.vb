Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Reflection
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class ExplorersPartyViewModel
        Inherits GenericViewModel
        Implements IParty
        Implements INotifyModified
        Implements INotifyPropertyChanged

        Public Sub New()
            StandbyCommand = New RelayCommand(AddressOf StandbySelectedActivePokemon)
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(SkySave).GetTypeInfo, GetType(TDSave).GetTypeInfo}
        End Function

        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event ActivePokemonRemoving(sender As Object, e As ActivePokemonRemoveEventArgs)
        Public Event ActivePokemonRemoved(sender As Object, e As ActivePokemonRemoveEventArgs)

        Private Sub OnModified(sender As Object, e As EventArgs) Handles _party.CollectionChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

        Public Property Party As IEnumerable(Of FileViewModel) Implements IParty.Party
            Get
                Return _party
            End Get
            Set(value As IEnumerable(Of FileViewModel))
                _party = value
            End Set
        End Property
        Private WithEvents _party As ObservableCollection(Of FileViewModel)

        Public Property SelectedPokemon As FileViewModel Implements IParty.SelectedPokemon
            Get
                Return _selectedPokemon
            End Get
            Set(value As FileViewModel)
                If _selectedPokemon IsNot value Then
                    _selectedPokemon = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedPokemon)))
                    Me.RequestMenuItemRefresh()
                    StandbyCommand.IsEnabled = False '(value IsNot Nothing)
                End If
            End Set
        End Property
        Dim _selectedPokemon As FileViewModel

        Public ReadOnly Property StandbyCommand As RelayCommand Implements IParty.StandbyCommand

        Public ReadOnly Property PartyName As String Implements IParty.PartyName
            Get
                Return My.Resources.Language.Party
            End Get
        End Property

        Public ReadOnly Property CanAddActivePokemon As Boolean
            Get
                Return _party.Count < 4
            End Get
        End Property

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            If TypeOf model Is SkySave Then
                Dim s As SkySave = model

                Dim pkm As New ObservableCollection(Of FileViewModel)
                For Each item In s.ActivePokemon
                    Dim vm As New FileViewModel(item)
                    AddHandler vm.Modified, AddressOf OnModified
                    pkm.Add(vm)
                Next

                _party = pkm
            ElseIf TypeOf model Is TDSave Then
                Dim s As TDSave = model

                Dim pkm As New ObservableCollection(Of FileViewModel)
                For Each item In s.ActivePokemon
                    Dim vm As New FileViewModel(item)
                    AddHandler vm.Modified, AddressOf OnModified
                    pkm.Add(vm)
                Next

                _party = pkm
            End If

        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)


            If TypeOf model Is SkySave Then
                Dim s As SkySave = model

                s.ActivePokemon.Clear()

                For Each item In Party
                    s.ActivePokemon.Add(item.File)
                Next
            ElseIf TypeOf model Is TDSave Then
                Dim s As TDSave = model

                s.ActivePokemon.Clear()

                For Each item In Party
                    s.ActivePokemon.Add(item.File)
                Next
            End If

        End Sub

        Public Sub StandbySelectedActivePokemon()
            Dim pkm = SelectedPokemon?.File

            If pkm IsNot Nothing Then
                RaiseEvent ActivePokemonRemoving(Me, New ActivePokemonRemoveEventArgs With {.Pokemon = pkm})

                _party.Remove(SelectedPokemon)
                SelectedPokemon = Nothing

                RaiseEvent ActivePokemonRemoved(Me, New ActivePokemonRemoveEventArgs With {.Pokemon = pkm})
            End If

        End Sub

        Public Sub AddActivePokemon(pkm As IExplorersStoredPokemon, rosterNumber As Integer)
            Dim vm As New FileViewModel(pkm.ToActive(rosterNumber))
            AddHandler vm.Modified, AddressOf OnModified
            _party.Add(vm)
        End Sub

        Public Overrides Function GetSortOrder() As Integer
            Return 3
        End Function
    End Class

End Namespace

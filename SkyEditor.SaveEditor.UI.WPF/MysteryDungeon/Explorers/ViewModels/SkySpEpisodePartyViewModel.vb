Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class SkySpEpisodePartyViewModel
        Inherits GenericViewModel(Of SkySave)
        Implements IParty
        Implements INotifyModified
        Implements INotifyPropertyChanged

        Public Sub New(pluginManager As PluginManager)
            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

            StandbyCommand = New RelayCommand(AddressOf StandbySelectedActivePokemon)
            CurrentPluginManager = pluginManager
        End Sub

        Public Event Modified As EventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Property CurrentPluginManager As PluginManager

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
                Return My.Resources.Language.SpEpisodeParty
            End Get
        End Property

        Public Overrides ReadOnly Property SortOrder As Integer
            Get
                Return 4
            End Get
        End Property

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As SkySave = model

            Dim pkm As New ObservableCollection(Of FileViewModel)
            For Each item In s.SpEpisodeActivePokemon
                Dim vm As New FileViewModel(item, CurrentPluginManager)
                AddHandler vm.Modified, AddressOf OnModified
                pkm.Add(vm)
            Next

            _party = pkm

        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim s As SkySave = model

            s.SpEpisodeActivePokemon.Clear()

            For Each item In Party
                s.SpEpisodeActivePokemon.Add(item.Model)
            Next

        End Sub

        Public Sub StandbySelectedActivePokemon()
            _party.Remove(SelectedPokemon)
            SelectedPokemon = Nothing
        End Sub

    End Class

End Namespace

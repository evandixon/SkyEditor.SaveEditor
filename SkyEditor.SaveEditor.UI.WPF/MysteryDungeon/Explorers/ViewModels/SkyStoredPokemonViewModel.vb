Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class SkyStoredPokemonViewModel
        Inherits GenericViewModel(Of SkySave)
        Implements INotifyModified
        Implements INotifyPropertyChanged
        Implements IPokemonStorage

        Public Sub New(pluginManager As PluginManager)
            MyBase.New
            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

            StoredPlayerPartner = New ObservableCollection(Of FileViewModel)
            StoredSpEpisodePokemon = New ObservableCollection(Of FileViewModel)
            StoredPokemon = New ObservableCollection(Of FileViewModel)
            AllPokemon = New ObservableCollection(Of FileViewModel)

            AddToPartyCommand = New RelayCommand(AddressOf AddActivePokemon)

            CurrentPluginManager = pluginManager
        End Sub

        Public Event Modified As EventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Property CurrentPluginManager As PluginManager

#Region "Event Handlers"
        Private Sub _storedPlayerPartner_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedPlayerPartner.CollectionChanged, _storedSpEpisodePokemon.CollectionChanged, _storedPokemon.CollectionChanged
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub _selectedBox_SelectedPokemonChanged(sender As Object, e As EventArgs) Handles _selectedBox.SelectedPokemonChanged
            RequestMenuItemRefresh()
            AddToPartyCommand.IsEnabled = False '(ActivePokemonViewModel.CanAddActivePokemon < 4 AndAlso SelectedBox IsNot Nothing AndAlso SelectedBox.SelectedPokemon IsNot Nothing)
        End Sub

        'Private Sub _activeVM_ActivePokemonRemoved(sender As Object, e As ActivePokemonRemoveEventArgs) Handles _activeVM.ActivePokemonRemoved
        '    AllPokemon(e.Pokemon.RosterNumber).Model = e.Pokemon.ToStored
        'End Sub
#End Region

        Protected ReadOnly Property ActivePokemonViewModel As ExplorersPartyViewModel
            Get
                If _activeVM Is Nothing Then
                    _activeVM = GetSiblingViewModel(Of ExplorersPartyViewModel)()
                End If
                Return _activeVM
            End Get
        End Property
        Private WithEvents _activeVM As ExplorersPartyViewModel

#Region "Properties"
        Public Property Storage As IEnumerable(Of IPokemonBox) Implements IPokemonStorage.Storage

        Public Property SelectedBox As IPokemonBox Implements IPokemonStorage.SelectedBox
            Get
                Return _selectedBox
            End Get
            Set(value As IPokemonBox)
                If _selectedBox IsNot value Then
                    _selectedBox = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedBox)))
                End If
            End Set
        End Property
        Private WithEvents _selectedBox As IPokemonBox

        Public Property StoredPlayerPartner As IEnumerable(Of FileViewModel)
            Get
                Return _storedPlayerPartner
            End Get
            Set(value As IEnumerable(Of FileViewModel))
                _storedPlayerPartner = value
            End Set
        End Property
        Private WithEvents _storedPlayerPartner As ObservableCollection(Of FileViewModel)

        Public Property StoredSpEpisodePokemon As IEnumerable(Of FileViewModel)
            Get
                Return _storedSpEpisodePokemon
            End Get
            Set(value As IEnumerable(Of FileViewModel))
                _storedSpEpisodePokemon = value
            End Set
        End Property
        Private WithEvents _storedSpEpisodePokemon As ObservableCollection(Of FileViewModel)

        Public Property StoredPokemon As IEnumerable(Of FileViewModel)
            Get
                Return _storedPokemon
            End Get
            Set(value As IEnumerable(Of FileViewModel))
                _storedPokemon = value
            End Set
        End Property
        Private WithEvents _storedPokemon As ObservableCollection(Of FileViewModel)

        Private Property AllPokemon As ObservableCollection(Of FileViewModel)

        Public Property AddToPartyCommand As RelayCommand Implements IPokemonStorage.AddToPartyCommand

        Public Overrides ReadOnly Property SortOrder As Integer
            Get
                Return 2
            End Get
        End Property
#End Region


        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As SkySave = model

            _storedPlayerPartner.Clear()
            _storedSpEpisodePokemon.Clear()
            _storedPokemon.Clear()

            For count = 0 To s.StoredPokemon.Count - 1
                Dim pkm = s.StoredPokemon(count)
                Dim fvm As New FileViewModel(pkm, CurrentPluginManager)
                AddHandler fvm.Modified, AddressOf OnModified

                If count < 2 Then 'Player Partner
                    _storedPlayerPartner.Add(fvm)
                ElseIf count < 5 Then 'Sp. Episode
                    _storedSpEpisodePokemon.Add(fvm)
                Else 'Others
                    _storedPokemon.Add(fvm)
                End If
                AllPokemon.Add(fvm)
            Next

            Dim b = New ObservableCollection(Of IPokemonBox)
            b.Add(New BasicPokemonBox(My.Resources.Language.PlayerPartnerPokemonSlot, StoredPlayerPartner))
            b.Add(New BasicPokemonBox(My.Resources.Language.SpEpisodePokemonSlot, StoredSpEpisodePokemon))
            b.Add(New BasicPokemonBox(My.Resources.Language.StoredPokemonSlot, StoredPokemon))
            Storage = b
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim s As SkySave = model

            s.StoredPokemon.Clear()
            For Each item In StoredPlayerPartner
                s.StoredPokemon.Add(item.Model)
            Next

            For Each item In StoredSpEpisodePokemon
                s.StoredPokemon.Add(item.Model)
            Next

            For Each item In StoredPokemon
                s.StoredPokemon.Add(item.Model)
            Next

        End Sub

        '''' <summary>
        '''' Adds the selected box's selected pokemon to active Pokemon
        '''' </summary>
        Private Sub AddActivePokemon()
            '    If AddToPartyCommand.IsEnabled Then 'Check to see if the selected box or Pokemon is null
            '        ActivePokemonViewModel.AddActivePokemon(SelectedBox.SelectedPokemon.Model, GetPokemonIndex(SelectedBox.SelectedPokemon))
            '    End If
        End Sub

        Private Function GetPokemonIndex(pkm As FileViewModel)
            If _storedPlayerPartner.Contains(pkm) Then
                Return _storedPlayerPartner.IndexOf(pkm)
            ElseIf _storedSpEpisodePokemon.Contains(pkm) Then
                Return _storedSpEpisodePokemon.IndexOf(pkm) + 2
            Else
                Return _storedPokemon.IndexOf(pkm) + 5
            End If
        End Function

    End Class
End Namespace


Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class TDStoredPokemonViewModel
        Inherits GenericViewModel(Of TDSave)
        Implements INotifyModified
        Implements INotifyPropertyChanged
        Implements IPokemonStorage

        Public Sub New(pluginManager As PluginManager)
            MyBase.New
            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

            StoredPlayerPartner = New ObservableCollection(Of FileViewModel)
            StoredPokemon = New ObservableCollection(Of FileViewModel)

            AddToPartyCommand = New RelayCommand(Sub()
                                                     'Do nothing
                                                 End Sub)
            AddToPartyCommand.IsEnabled = False

            CurrentPluginManager = pluginManager
        End Sub

        Public Event Modified As EventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Property CurrentPluginManager As PluginManager

#Region "Event Handlers"
        Private Sub _storedPlayerPartner_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedPlayerPartner.CollectionChanged, _storedPokemon.CollectionChanged
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub _selectedBox_SelectedPokemonChanged(sender As Object, e As EventArgs) Handles _selectedBox.SelectedPokemonChanged
            RequestMenuItemRefresh()
        End Sub
#End Region

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

        Public Property StoredPokemon As IEnumerable(Of FileViewModel)
            Get
                Return _storedPokemon
            End Get
            Set(value As IEnumerable(Of FileViewModel))
                _storedPokemon = value
            End Set
        End Property

        Public Property AddToPartyCommand As RelayCommand Implements IPokemonStorage.AddToPartyCommand

        Private WithEvents _storedPokemon As ObservableCollection(Of FileViewModel)
#End Region


        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As TDSave = model

            _storedPlayerPartner.Clear()
            _storedPokemon.Clear()

            For count = 0 To s.StoredPokemon.Count - 1
                Dim pkm = s.StoredPokemon(count)
                Dim fvm As New FileViewModel(pkm, CurrentPluginManager)
                AddHandler fvm.Modified, AddressOf OnModified

                If count < 2 Then 'Player Partner
                    _storedPlayerPartner.Add(fvm)
                Else 'Others
                    _storedPokemon.Add(fvm)
                End If
            Next

            Dim b = New ObservableCollection(Of IPokemonBox)
            b.Add(New BasicPokemonBox(My.Resources.Language.PlayerPartnerPokemonSlot, StoredPlayerPartner))
            b.Add(New BasicPokemonBox(My.Resources.Language.StoredPokemonSlot, StoredPokemon))
            Storage = b
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim s As TDSave = model

            s.StoredPokemon.Clear()
            For Each item In StoredPlayerPartner
                s.StoredPokemon.Add(item.Model)
            Next
            For Each item In StoredPokemon
                s.StoredPokemon.Add(item.Model)
            Next
        End Sub

    End Class
End Namespace


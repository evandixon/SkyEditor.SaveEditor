Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
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

        Public Sub New()
            MyBase.New

            StoredPlayerPartner = New ObservableCollection(Of FileViewModel)
            StoredSpEpisodePokemon = New ObservableCollection(Of FileViewModel)
            StoredPokemon = New ObservableCollection(Of FileViewModel)
        End Sub

        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Event Handlers"
        Private Sub _storedPlayerPartner_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedPlayerPartner.CollectionChanged, _storedSpEpisodePokemon.CollectionChanged, _storedPokemon.CollectionChanged
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
#End Region


        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As SkySave = model

            _storedPlayerPartner.Clear()
            _storedSpEpisodePokemon.Clear()
            _storedPokemon.Clear()

            For count = 0 To s.StoredPokemon.Count - 1
                Dim pkm = s.StoredPokemon(count)
                Dim fvm As New FileViewModel(pkm)
                AddHandler fvm.Modified, AddressOf OnModified

                If count < 2 Then 'Player Partner
                    _storedPlayerPartner.Add(fvm)
                ElseIf count < 5 Then 'Sp. Episode
                    _storedSpEpisodePokemon.Add(fvm)
                Else 'Others
                    _storedPokemon.Add(fvm)
                End If
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
                s.StoredPokemon.Add(item.File)
            Next

            For Each item In StoredSpEpisodePokemon
                s.StoredPokemon.Add(item.File)
            Next

            For Each item In StoredPokemon
                s.StoredPokemon.Add(item.File)
            Next

        End Sub

    End Class
End Namespace


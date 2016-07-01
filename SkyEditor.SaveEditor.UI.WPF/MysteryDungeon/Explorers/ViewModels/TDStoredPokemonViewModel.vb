Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class TDStoredPokemonViewModel
        Inherits GenericViewModel(Of TDSave)
        Implements INotifyModified
        Implements IPokemonStorage

        Public Sub New()
            MyBase.New

            StoredPlayerPartner = New ObservableCollection(Of TDStoredPokemon)
            StoredPokemon = New ObservableCollection(Of TDStoredPokemon)
        End Sub

        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified

        Private Sub _storedPlayerPartner_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedPlayerPartner.CollectionChanged, _storedPokemon.CollectionChanged
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(Me, e)
        End Sub

#Region "Properties"
        Public Property Storage As IEnumerable(Of IPokemonBox) Implements IPokemonStorage.Storage
        Public Property StoredPlayerPartner As IEnumerable(Of TDStoredPokemon)
            Get
                Return _storedPlayerPartner
            End Get
            Set(value As IEnumerable(Of TDStoredPokemon))
                _storedPlayerPartner = value
            End Set
        End Property
        Private WithEvents _storedPlayerPartner As ObservableCollection(Of TDStoredPokemon)

        Public Property StoredPokemon As IEnumerable(Of TDStoredPokemon)
            Get
                Return _storedPokemon
            End Get
            Set(value As IEnumerable(Of TDStoredPokemon))
                _storedPokemon = value
            End Set
        End Property
        Private WithEvents _storedPokemon As ObservableCollection(Of TDStoredPokemon)
#End Region


        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As TDSave = model

            _storedPlayerPartner.Clear()
            _storedPokemon.Clear()

            For count = 0 To s.StoredPokemon.Count - 1
                Dim pkm = s.StoredPokemon(count)
                AddHandler pkm.Modified, AddressOf OnModified
                AddHandler pkm.PropertyChanged, AddressOf OnModified

                If count < 2 Then 'Player Partner
                    _storedPlayerPartner.Add(pkm)
                Else 'Others
                    _storedPokemon.Add(pkm)
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
                s.StoredPokemon.Add(item)
            Next
            For Each item In StoredPokemon
                s.StoredPokemon.Add(item)
            Next
        End Sub

    End Class
End Namespace


Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Rescue
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class RBStoredPokemonViewModel
        Inherits GenericViewModel(Of RBSave)
        Implements INotifyModified
        Implements IPokemonStorage

        Public Sub New()
            MyBase.New

            StoredPokemon = New ObservableCollection(Of RBStoredPokemon)
        End Sub

        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified

        Private Sub _storedPlayerPartner_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedPokemon.CollectionChanged
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(Me, e)
        End Sub

        Public Property Storage As IEnumerable(Of IPokemonBox) Implements IPokemonStorage.Storage
        Public Property StoredPokemon As IEnumerable(Of RBStoredPokemon)
            Get
                Return _storedPokemon
            End Get
            Set(value As IEnumerable(Of RBStoredPokemon))
                _storedPokemon = value
            End Set
        End Property
        Private WithEvents _storedPokemon As ObservableCollection(Of RBStoredPokemon)

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As RBSave = model
            _storedPokemon.Clear()
            For Each item In s.StoredPokemon
                _storedPokemon.Add(item)
            Next

            Dim b = New ObservableCollection(Of IPokemonBox)
            b.Add(New BasicPokemonBox(My.Resources.Language.AllPokemon, StoredPokemon))
            Dim defs = StoredPokemonSlotDefinition.FromLines(SkyEditor.SaveEditor.My.Resources.ListResources.RBFriendAreaOffsets)
            Dim offset As Integer = 0
            For Each item In defs
                Dim pokemon As New ObservableCollection(Of RBStoredPokemon)

                For count = offset To offset + item.Length - 1
                    Dim p = _storedPokemon(count)
                    AddHandler p.Modified, AddressOf OnModified
                    AddHandler p.PropertyChanged, AddressOf OnModified

                    pokemon.Add(p)
                Next
                offset += item.Length - 1
                b.Add(New BasicPokemonBox(item.Name, pokemon))
            Next

            Storage = b
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim s As RBSave = model

            Dim defs = StoredPokemonSlotDefinition.FromLines(SkyEditor.SaveEditor.My.Resources.ListResources.RBFriendAreaOffsets)
            For i = 0 To defs.Count - 1
                For j = 0 To defs(i).Length - 1
                    _storedPokemon(i + j) = _Storage(i).ItemCollection(j)
                Next
            Next

            s.StoredPokemon.Clear()
            For Each item In StoredPokemon
                s.StoredPokemon.Add(item)
            Next
        End Sub

    End Class
End Namespace


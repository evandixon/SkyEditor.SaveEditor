Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon
Imports SkyEditor.SaveEditor.MysteryDungeon.Rescue
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Rescue.ViewModels
    Public Class RBPokemonStorageViewModel
        Inherits GenericViewModel(Of RBSave)
        Implements INotifyModified
        Implements INotifyPropertyChanged
        Implements IPokemonStorage

        Public Sub New(pluginManager As PluginManager)
            MyBase.New
            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

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
        Private Sub _storedPlayerPartner_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedPokemon.CollectionChanged
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub _selectedBox_SelectedPokemonChanged(sender As Object, e As EventArgs) Handles _selectedBox.SelectedPokemonChanged
            RequestMenuItemRefresh()
        End Sub
#End Region


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

        Public Property StoredPokemon As IEnumerable(Of FileViewModel)
            Get
                Return _storedPokemon
            End Get
            Set(value As IEnumerable(Of FileViewModel))
                _storedPokemon = value
            End Set
        End Property

        Public ReadOnly Property AddToPartyCommand As RelayCommand Implements IPokemonStorage.AddToPartyCommand

        Private WithEvents _storedPokemon As ObservableCollection(Of FileViewModel)

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As RBSave = model
            _storedPokemon.Clear()
            For Each item In s.StoredPokemon
                Dim fmv As New FileViewModel(item, CurrentPluginManager)
                AddHandler fmv.Modified, AddressOf OnModified

                _storedPokemon.Add(fmv)
            Next

            Dim b = New ObservableCollection(Of IPokemonBox)
            b.Add(New BasicPokemonBox(My.Resources.Language.AllPokemon, StoredPokemon))
            Dim defs = StoredPokemonSlotDefinition.FromLines(DataUtil.GetStringResource("RBFriendAreaOffsets"))
            Dim offset As Integer = 0
            For Each item In defs
                Dim pokemon As New ObservableCollection(Of FileViewModel)

                For count = offset To offset + item.Length - 1
                    pokemon.Add(_storedPokemon(count))
                Next
                offset += item.Length
                b.Add(New BasicPokemonBox(item.Name, pokemon))
            Next

            Storage = b
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim s As RBSave = model

            Dim defs = StoredPokemonSlotDefinition.FromLines(DataUtil.GetStringResource("RBFriendAreaOffsets"))
            Dim offset As Integer = 0
            For i = 0 To defs.Count - 1
                For j = 0 To defs(i).Length - 1
                    _storedPokemon(offset) = _Storage(i + 1).ItemCollection(j)
                    offset += 1
                Next
            Next

            s.StoredPokemon.Clear()
            For Each item In StoredPokemon
                s.StoredPokemon.Add(item.Model)
            Next
        End Sub

    End Class
End Namespace


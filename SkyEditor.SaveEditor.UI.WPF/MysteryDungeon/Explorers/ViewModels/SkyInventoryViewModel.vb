Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class SkyInventoryViewModel
        Inherits GenericViewModel(Of SkySave)
        Implements IInventory
        Implements INotifyModified

        Public Sub New(pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            MyBase.New

            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

            If applicationViewModel Is Nothing Then
                Throw New ArgumentNullException(NameOf(applicationViewModel))
            End If

            CurrentPluginManager = pluginManager
            CurrentApplicationViewModel = applicationViewModel
            StoredItems = New ObservableCollection(Of ExplorersItemViewModel)
            HeldItems = New ObservableCollection(Of ExplorersItemViewModel)
            SpEpisodeHeldItems = New ObservableCollection(Of ExplorersItemViewModel)
            FriendRescueHeldItems = New ObservableCollection(Of ExplorersItemViewModel)
        End Sub

        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Public Property CurrentApplicationViewModel As ApplicationViewModel
        Public Property CurrentPluginManager As PluginManager

        Private Sub _heldItems_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedItems.CollectionChanged, _heldItems.CollectionChanged, _spEpisodeItems.CollectionChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

#Region "Properties"
        Public Property ItemSlots As IEnumerable(Of IItemSlot) Implements IInventory.ItemSlots
        Public Property StoredItems As ObservableCollection(Of ExplorersItemViewModel)
            Get
                Return _storedItems
            End Get
            Private Set(value As ObservableCollection(Of ExplorersItemViewModel))
                _storedItems = value
            End Set
        End Property
        Private WithEvents _storedItems As ObservableCollection(Of ExplorersItemViewModel)

        Public Property HeldItems As ObservableCollection(Of ExplorersItemViewModel)
            Get
                Return _heldItems
            End Get
            Private Set(value As ObservableCollection(Of ExplorersItemViewModel))
                _heldItems = value
            End Set
        End Property
        Private WithEvents _heldItems As ObservableCollection(Of ExplorersItemViewModel)

        Public Property SpEpisodeHeldItems As ObservableCollection(Of ExplorersItemViewModel)
            Get
                Return _spEpisodeItems
            End Get
            Private Set(value As ObservableCollection(Of ExplorersItemViewModel))
                _spEpisodeItems = value
            End Set
        End Property
        Private WithEvents _spEpisodeItems As ObservableCollection(Of ExplorersItemViewModel)

        Public Property FriendRescueHeldItems As ObservableCollection(Of ExplorersItemViewModel)
            Get
                Return _friendRescueHeldItems
            End Get
            Private Set(value As ObservableCollection(Of ExplorersItemViewModel))
                _friendRescueHeldItems = value
            End Set
        End Property
        Private WithEvents _friendRescueHeldItems As ObservableCollection(Of ExplorersItemViewModel)

        Public Overrides ReadOnly Property SortOrder As Integer
            Get
                Return 1
            End Get
        End Property
#End Region

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim m As SkySave = model

            'Set the lists
            StoredItems.Clear()
            For Each item In m.StoredItems
                StoredItems.Add(New ExplorersItemViewModel(item, CurrentApplicationViewModel))
            Next

            HeldItems.Clear()
            For Each item In m.HeldItems
                HeldItems.Add(New ExplorersItemViewModel(item, CurrentApplicationViewModel))
            Next

            SpEpisodeHeldItems.Clear()
            For Each item In m.SpEpisodeHeldItems
                SpEpisodeHeldItems.Add(New ExplorersItemViewModel(item, CurrentApplicationViewModel))
            Next

            FriendRescueHeldItems.Clear()
            For Each item In m.FriendRescueHeldItems
                FriendRescueHeldItems.Add(New ExplorersItemViewModel(item, CurrentApplicationViewModel))
            Next

            'Item slots
            Dim slots As New ObservableCollection(Of IItemSlot)
            slots.Add(New ItemSlot(Of ExplorersItemViewModel)(My.Resources.Language.StoredItemsSlot, StoredItems, 1000, CurrentPluginManager, CurrentApplicationViewModel))
            slots.Add(New ItemSlot(Of ExplorersItemViewModel)(My.Resources.Language.HeldItemsSlot, HeldItems, 50, CurrentPluginManager, CurrentApplicationViewModel))
            slots.Add(New ItemSlot(Of ExplorersItemViewModel)(My.Resources.Language.EpisodeHeldItems, SpEpisodeHeldItems, 50, CurrentPluginManager, CurrentApplicationViewModel))
            slots.Add(New ItemSlot(Of ExplorersItemViewModel)(My.Resources.Language.FriendRescueHeldItems, FriendRescueHeldItems, 50, CurrentPluginManager, CurrentApplicationViewModel))
            ItemSlots = slots
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim m As SkySave = model

            m.StoredItems.Clear()
            For Each item In StoredItems
                m.StoredItems.Add(item.Model)
            Next

            m.HeldItems.Clear()
            For Each item In HeldItems
                m.HeldItems.Add(item.Model)
            Next

            m.SpEpisodeHeldItems.Clear()
            For Each item In SpEpisodeHeldItems
                m.SpEpisodeHeldItems.Add(item.Model)
            Next

            m.FriendRescueHeldItems.Clear()
            For Each item In FriendRescueHeldItems
                m.FriendRescueHeldItems.Add(item.Model)
            Next
        End Sub
    End Class
End Namespace


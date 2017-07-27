Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class SkyInventoryViewModel
        Inherits GenericViewModel(Of SkySave)
        Implements IInventory
        Implements INotifyModified

        Public Sub New()
            MyBase.New

            StoredItems = New ObservableCollection(Of SkyItemViewModel)
            HeldItems = New ObservableCollection(Of SkyHeldItemViewModel)
            SpEpisodeHeldItems = New ObservableCollection(Of SkyHeldItemViewModel)
        End Sub

        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Private Sub _heldItems_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedItems.CollectionChanged, _heldItems.CollectionChanged, _spEpisodeItems.CollectionChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

#Region "Properties"
        Public Property ItemSlots As IEnumerable(Of IItemSlot) Implements IInventory.ItemSlots
        Public Property StoredItems As ObservableCollection(Of SkyItemViewModel)
            Get
                Return _storedItems
            End Get
            Private Set(value As ObservableCollection(Of SkyItemViewModel))
                _storedItems = value
            End Set
        End Property
        Private WithEvents _storedItems As ObservableCollection(Of SkyItemViewModel)

        Public Property HeldItems As ObservableCollection(Of SkyHeldItemViewModel)
            Get
                Return _heldItems
            End Get
            Private Set(value As ObservableCollection(Of SkyHeldItemViewModel))
                _heldItems = value
            End Set
        End Property
        Private WithEvents _heldItems As ObservableCollection(Of SkyHeldItemViewModel)

        Public Property SpEpisodeHeldItems As ObservableCollection(Of SkyHeldItemViewModel)
            Get
                Return _spEpisodeItems
            End Get
            Private Set(value As ObservableCollection(Of SkyHeldItemViewModel))
                _spEpisodeItems = value
            End Set
        End Property
        Private WithEvents _spEpisodeItems As ObservableCollection(Of SkyHeldItemViewModel)

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
                StoredItems.Add(New SkyItemViewModel(item, CurrentApplicationViewModel))
            Next

            HeldItems.Clear()
            For Each item In m.HeldItems
                HeldItems.Add(New SkyHeldItemViewModel(item, CurrentApplicationViewModel))
            Next

            SpEpisodeHeldItems.Clear()
            For Each item In m.SpEpisodeHeldItems
                SpEpisodeHeldItems.Add(New SkyHeldItemViewModel(item, CurrentApplicationViewModel))
            Next

            'Item slots
            Dim slots As New ObservableCollection(Of IItemSlot)
            slots.Add(New ItemSlot(Of SkyItemViewModel)(My.Resources.Language.StoredItemsSlot, StoredItems, 1000))
            slots.Add(New ItemSlot(Of SkyHeldItemViewModel)(My.Resources.Language.HeldItemsSlot, HeldItems, 50))
            slots.Add(New ItemSlot(Of SkyHeldItemViewModel)(My.Resources.Language.EpisodeHeldItems, SpEpisodeHeldItems, 50))
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
        End Sub
    End Class
End Namespace


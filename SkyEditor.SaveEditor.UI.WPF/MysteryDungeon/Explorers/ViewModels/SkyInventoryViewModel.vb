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

            StoredItems = New ObservableCollection(Of SkyStoredItem)
            HeldItems = New ObservableCollection(Of SkyHeldItem)
            SpEpisodeHeldItems = New ObservableCollection(Of SkyHeldItem)
        End Sub

        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified

        Private Sub _heldItems_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _storedItems.CollectionChanged, _heldItems.CollectionChanged, _spEpisodeItems.CollectionChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

#Region "Properties"
        Public Property ItemSlots As IEnumerable(Of IItemSlot) Implements IInventory.ItemSlots
        Public Property StoredItems As ObservableCollection(Of SkyStoredItem)
            Get
                Return _storedItems
            End Get
            Private Set(value As ObservableCollection(Of SkyStoredItem))
                _storedItems = value
            End Set
        End Property
        Private WithEvents _storedItems As ObservableCollection(Of SkyStoredItem)

        Public Property HeldItems As ObservableCollection(Of SkyHeldItem)
            Get
                Return _heldItems
            End Get
            Private Set(value As ObservableCollection(Of SkyHeldItem))
                _heldItems = value
            End Set
        End Property
        Private WithEvents _heldItems As ObservableCollection(Of SkyHeldItem)

        Public Property SpEpisodeHeldItems As ObservableCollection(Of SkyHeldItem)
            Get
                Return _spEpisodeItems
            End Get
            Private Set(value As ObservableCollection(Of SkyHeldItem))
                _spEpisodeItems = value
            End Set
        End Property
        Private WithEvents _spEpisodeItems As ObservableCollection(Of SkyHeldItem)
#End Region




        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim m As SkySave = model

            'Set the lists
            StoredItems.Clear()
            For Each item In m.StoredItems
                StoredItems.Add(item)
            Next

            HeldItems.Clear()
            For Each item In m.HeldItems
                HeldItems.Add(item)
            Next

            SpEpisodeHeldItems.Clear()
            For Each item In m.SpEpisodeHeldItems
                SpEpisodeHeldItems.Add(item)
            Next

            'Item slots
            Dim slots As New ObservableCollection(Of IItemSlot)
            slots.Add(New ItemSlot(Of SkyStoredItem)(My.Resources.Language.StoredItemsSlot, StoredItems, m.Offsets.StoredItemNumber))
            slots.Add(New ItemSlot(Of SkyHeldItem)(My.Resources.Language.HeldItemsSlot, HeldItems, m.Offsets.HeldItemNumber))
            slots.Add(New ItemSlot(Of SkyHeldItem)(My.Resources.Language.EpisodeHeldItems, SpEpisodeHeldItems, m.Offsets.HeldItemNumber))
            ItemSlots = slots
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim m As SkySave = model

            m.StoredItems.Clear()
            For Each item In StoredItems
                m.StoredItems.Add(item)
            Next

            m.HeldItems.Clear()
            For Each item In HeldItems
                m.HeldItems.Add(item)
            Next

            m.SpEpisodeHeldItems.Clear()
            For Each item In SpEpisodeHeldItems
                m.SpEpisodeHeldItems.Add(item)
            Next
        End Sub
    End Class
End Namespace


Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Rescue
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Rescue.ViewModels
    Public Class RBInventoryViewModel
        Inherits GenericViewModel(Of RBSave)
        Implements IInventory
        Implements INotifyModified

        Public Sub New()
            MyBase.New

            HeldItems = New ObservableCollection(Of RBHeldItem)
            StoredItems = New ObservableCollection(Of RBStoredItemViewModel)
        End Sub

        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Public Property ItemSlots As IEnumerable(Of IItemSlot) Implements IInventory.ItemSlots
        Public Property HeldItems As ObservableCollection(Of RBHeldItem)
            Get
                Return _heldItems
            End Get
            Private Set(value As ObservableCollection(Of RBHeldItem))
                _heldItems = value
            End Set
        End Property
        Private WithEvents _heldItems As ObservableCollection(Of RBHeldItem)

        Public Property StoredItems As ObservableCollection(Of RBStoredItemViewModel)
            Get
                Return _storedItems
            End Get
            Private Set(value As ObservableCollection(Of RBStoredItemViewModel))
                _storedItems = value
            End Set
        End Property
        Private WithEvents _storedItems As ObservableCollection(Of RBStoredItemViewModel)

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim m As RBSave = model

            'Set the lists

            HeldItems.Clear()
            For Each item In m.HeldItems
                HeldItems.Add(item)
            Next

            StoredItems.Clear()
            For Each item In m.StoredItems
                StoredItems.Add(New RBStoredItemViewModel(item))
            Next

            'Item slots
            Dim slots As New ObservableCollection(Of IItemSlot)
            slots.Add(New ItemSlot(Of RBHeldItem)(My.Resources.Language.HeldItemsSlot, HeldItems, m.Offsets.HeldItemNumber))
            slots.Add(New RBStoredItemSlot(StoredItems))
            ItemSlots = slots
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim m As RBSave = model

            m.HeldItems.Clear()
            For Each item In HeldItems
                m.HeldItems.Add(item)
            Next

            m.StoredItems.Clear()
            For Each item In StoredItems
                m.StoredItems.Add(item.GetStoredItem)
            Next

        End Sub

        Private Sub _heldItems_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _heldItems.CollectionChanged, _storedItems.CollectionChanged
            RaiseEvent Modified(Me, e)

            If sender Is _storedItems Then
                If e.Action = NotifyCollectionChangedAction.Add Then
                    For Each item As RBStoredItemViewModel In e.NewItems
                        AddHandler item.PropertyChanged, AddressOf OnStoredItemPropertyChanged
                    Next
                ElseIf e.Action = NotifyCollectionChangedAction.Remove Then
                    For Each item As RBStoredItemViewModel In e.OldItems
                        RemoveHandler item.PropertyChanged, AddressOf OnStoredItemPropertyChanged
                    Next
                End If
            End If
        End Sub

        Private Sub OnStoredItemPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
            RaiseEvent Modified(Me, e)
        End Sub
    End Class
End Namespace


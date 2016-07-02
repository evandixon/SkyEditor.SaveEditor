Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
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
        End Sub

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

        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim m As RBSave = model

            'Set the lists

            HeldItems.Clear()
            For Each item In m.HeldItems
                HeldItems.Add(item)
            Next

            'Item slots
            Dim slots As New ObservableCollection(Of IItemSlot)
            slots.Add(New ItemSlot(Of RBHeldItem)(My.Resources.Language.HeldItemsSlot, HeldItems, m.Offsets.HeldItemNumber))
            ItemSlots = slots
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim m As RBSave = model

            m.HeldItems.Clear()
            For Each item In HeldItems
                m.HeldItems.Add(item)
            Next

        End Sub

        Private Sub _heldItems_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _heldItems.CollectionChanged
            RaiseEvent Modified(Me, e)
        End Sub
    End Class
End Namespace


Imports System.Collections.ObjectModel
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Rescue.ViewModels
    Public Class RBStoredItemSlot
        Implements IItemSlot

        Public Sub New(items As ObservableCollection(Of RBStoredItemViewModel))
            ItemCollection = items
            AddCommand = New RelayCommand(AddressOf DoAdd)
            AddCommand.IsEnabled = True
            NewItem = New RBStoredItemViewModel
        End Sub

        Public ReadOnly Property AddCommand As RelayCommand Implements IItemSlot.AddCommand

        Public Property ItemCollection As IList Implements IItemSlot.ItemCollection

        Public ReadOnly Property MaxItemCount As Integer Implements IItemSlot.MaxItemCount
            Get
                Return 239
            End Get
        End Property

        Public ReadOnly Property Name As String Implements IItemSlot.Name
            Get
                Return My.Resources.Language.StoredItemsSlot
            End Get
        End Property

        Public Property NewItem As Object Implements IItemSlot.NewItem

        Private Function CanAdd() As Boolean
            Return ItemCollection.Count < MaxItemCount
        End Function

        Private Function DoAdd() As Task
            Dim didAdd As Boolean = False
            Dim newItemVM As RBStoredItemViewModel = NewItem
            For Each item As RBStoredItemViewModel In ItemCollection
                If item.ItemID = newItemVM.ItemID Then
                    item.Quantity = Math.Min(item.Quantity + newItemVM.Quantity, 1024)
                    didAdd = True
                    Exit For
                End If
            Next

            If Not didAdd Then
                Dim vm As New RBStoredItemViewModel
                vm.ItemID = newItemVM.ItemID
                vm.Quantity = newItemVM.Quantity
                ItemCollection.Add(vm)
            End If

            Return Task.FromResult(0)
        End Function

    End Class

End Namespace

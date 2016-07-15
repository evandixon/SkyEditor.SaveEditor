Imports System.ComponentModel
Imports SkyEditor.SaveEditor.MysteryDungeon.Rescue

Namespace MysteryDungeon.Rescue.ViewModels
    Public Class RBStoredItemViewModel
        Implements INotifyPropertyChanged

        Public Sub New()
            ItemID = 1
            Quantity = 1
        End Sub

        Public Sub New(itemID As Integer, quantity As Integer)
            Me.ItemID = itemID
            Me.Quantity = quantity
        End Sub

        Public Sub New(item As RBStoredItem)
            Me.ItemID = item.ItemID
            Me.Quantity = item.Quantity
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property ItemID As Integer
            Get
                Return _itemID
            End Get
            Set(value As Integer)
                If Not _itemID = value Then
                    _itemID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ItemID)))
                End If
            End Set
        End Property
        Dim _itemID As Integer

        Public Property Quantity As Integer
            Get
                Return _quantity
            End Get
            Set(value As Integer)
                If Not _quantity = value Then
                    _quantity = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Quantity)))
                End If
            End Set
        End Property
        Dim _quantity As Integer

        Public ReadOnly Property Name As String
            Get
                Return Lists.RBItems(ItemID)
            End Get
        End Property

        Public Function GetStoredItem() As RBStoredItem
            Dim out As New RBStoredItem
            out.ItemID = Me.ItemID
            out.Quantity = Me.Quantity
            Return out
        End Function

        Public Overrides Function ToString() As String
            Return $"{Lists.RBItems(ItemID)} ({Quantity})"
        End Function

    End Class
End Namespace

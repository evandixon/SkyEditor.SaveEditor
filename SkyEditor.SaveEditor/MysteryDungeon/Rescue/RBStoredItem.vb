Namespace MysteryDungeon.Rescue
    Public Class RBStoredItem
        Public Sub New()
            ItemID = 0
            Quantity = 0
        End Sub
        Public Sub New(itemID As Integer, quantity As Integer)
            Me.ItemID = itemID
            Me.Quantity = quantity
        End Sub

        Public Property ItemID As Integer

        Public Property Quantity As Integer

        Public Overrides Function ToString() As String
            Return $"{Lists.RBItems(ItemID)} ({Quantity})"
        End Function
    End Class
End Namespace

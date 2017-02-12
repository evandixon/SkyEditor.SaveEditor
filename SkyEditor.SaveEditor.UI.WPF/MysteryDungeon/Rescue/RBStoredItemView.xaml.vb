Imports System.Collections.ObjectModel
Imports System.Reflection
Imports SkyEditor.SaveEditor.UI.WPF.MysteryDungeon.Rescue.ViewModels
Imports SkyEditor.UI.WPF

Namespace MysteryDungeon.Rescue
    Public Class RBStoredItemView
        Inherits DataBoundViewControl

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            Return MyBase.SupportsObject(Obj)
        End Function

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(ObservableCollection(Of RBStoredItemViewModel)).GetTypeInfo}
        End Function

        Private Sub gvItems_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles gvItems.SelectionChanged
            menuDelete.IsEnabled = (gvItems.SelectedItems.Count > 0)
        End Sub

        Private Sub menuDelete_Click(sender As Object, e As RoutedEventArgs) Handles menuDelete.Click
            Dim list = DirectCast(Me.DataContext, IList)
            Dim selected As New List(Of Object)
            selected.AddRange(gvItems.SelectedItems)
            For Each item In selected
                list.Remove(item)
            Next
        End Sub
    End Class
End Namespace


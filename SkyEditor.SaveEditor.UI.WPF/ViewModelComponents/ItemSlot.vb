Imports System.Collections.Specialized
Imports System.Reflection
Imports System.Windows.Input
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities

Namespace ViewModelComponents
    Public Class ItemSlot(Of T As GenericViewModel)
        Implements IItemSlot

        Public Sub New(name As String, items As IList(Of T), maxItemCount As Integer, pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

            If applicationViewModel Is Nothing Then
                Throw New ArgumentNullException(NameOf(applicationViewModel))
            End If

            If Not ReflectionHelpers.IsOfType(GetType(T).GetTypeInfo, GetType(IClonable).GetTypeInfo) Then
                Throw New ArgumentException("T must implement IClonable.  If not, use the overload of New that provides a cloner delegate.", NameOf(T))
            End If

            Me.CurrentApplicationViewModel = applicationViewModel
            Me.Name = name
            Me.ItemCollection = items
            Me.MaxItemCount = maxItemCount
            Me.NewItem = pluginManager.CreateInstance(GetType(T))

            AddCommand = New RelayCommand(AddressOf DoAddClonable)
        End Sub

        Public Sub New(name As String, items As IList(Of T), maxItemCount As Integer, cloner As CloneItem, pluginManager As PluginManager, applicationViewModel As ApplicationViewModel)
            If pluginManager Is Nothing Then
                Throw New ArgumentNullException(NameOf(pluginManager))
            End If

            If applicationViewModel Is Nothing Then
                Throw New ArgumentNullException(NameOf(applicationViewModel))
            End If

            Me.CurrentApplicationViewModel = applicationViewModel
            Me.Name = name
            Me.ItemCollection = items
            Me.MaxItemCount = maxItemCount
            Me.NewItem = pluginManager.CreateInstance(GetType(T))
            Me.cloner = cloner

            AddCommand = New RelayCommand(AddressOf DoAddDelegate)
        End Sub

        Public Property CurrentApplicationViewModel As ApplicationViewModel

        Delegate Function CloneItem(item As T) As T

        Public Property ItemCollection As IList Implements IItemSlot.ItemCollection
            Get
                Return _collection
            End Get
            Set(value As IList)
                If _collection IsNot Nothing AndAlso TypeOf _collection Is INotifyCollectionChanged Then
                    RemoveHandler DirectCast(_collection, INotifyCollectionChanged).CollectionChanged, AddressOf OnCollectionChanged
                End If

                _collection = value

                If _collection IsNot Nothing AndAlso TypeOf _collection Is INotifyCollectionChanged Then
                    AddHandler DirectCast(_collection, INotifyCollectionChanged).CollectionChanged, AddressOf OnCollectionChanged
                End If
            End Set
        End Property
        Dim _collection As IList

        Public ReadOnly Property MaxItemCount As Integer Implements IItemSlot.MaxItemCount

        Public ReadOnly Property Name As String Implements IItemSlot.Name

        'Todo: try to make this be of type T
        Public Property NewItem As Object Implements IItemSlot.NewItem

        Public ReadOnly Property AddCommand As RelayCommand Implements IItemSlot.AddCommand

        Private Property cloner As CloneItem

        Private Function CanAdd() As Boolean
            Return ItemCollection.Count < MaxItemCount
        End Function

        Private Function DoAddClonable() As Task
            Dim cloned As T = DirectCast(NewItem, IClonable).Clone
            ItemCollection.Add(cloned)
            Return Task.FromResult(0)
        End Function

        Private Function DoAddDelegate() As Task
            Dim cloned As T = cloner.Invoke(NewItem)
            ItemCollection.Add(cloned)
            Return Task.FromResult(0)
        End Function

        Private Sub OnCollectionChanged(sender As Object, e As EventArgs)
            AddCommand.IsEnabled = CanAdd()
        End Sub

    End Class
End Namespace


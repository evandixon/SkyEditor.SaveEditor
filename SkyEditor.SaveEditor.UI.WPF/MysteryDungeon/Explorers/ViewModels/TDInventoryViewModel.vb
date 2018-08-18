Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class TDInventoryViewModel
        Inherits GenericViewModel(Of TDSave)
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
            HeldItems = New ObservableCollection(Of ExplorersItemViewModel)
        End Sub

        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Public Property CurrentApplicationViewModel As ApplicationViewModel
        Public Property CurrentPluginManager As PluginManager

        Public Property ItemSlots As IEnumerable(Of IItemSlot) Implements IInventory.ItemSlots

        Public Property HeldItems As ObservableCollection(Of ExplorersItemViewModel)
            Get
                Return _heldItems
            End Get
            Private Set(value As ObservableCollection(Of ExplorersItemViewModel))
                _heldItems = value
            End Set
        End Property
        Private WithEvents _heldItems As ObservableCollection(Of ExplorersItemViewModel)

        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim m As TDSave = model

            'Set the lists
            HeldItems.Clear()
            For Each item In m.HeldItems
                HeldItems.Add(New ExplorersItemViewModel(item, CurrentApplicationViewModel))
            Next

            'Item slots
            Dim slots As New ObservableCollection(Of IItemSlot)
            slots.Add(New ItemSlot(Of ExplorersItemViewModel)(My.Resources.Language.HeldItemsSlot, HeldItems, 48, CurrentPluginManager, CurrentApplicationViewModel))
            ItemSlots = slots
        End Sub

        Public Overrides Sub UpdateModel(model As Object)
            MyBase.UpdateModel(model)

            Dim m As TDSave = model

            m.HeldItems.Clear()
            For Each item In HeldItems
                m.HeldItems.Add(item.Model)
            Next
        End Sub

        Private Sub _heldItems_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Handles _heldItems.CollectionChanged
            RaiseEvent Modified(Me, e)
        End Sub
    End Class
End Namespace


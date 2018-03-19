Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class ExplorersItemViewModel
        Inherits GenericViewModel(Of ExplorersItem)
        Implements INotifyPropertyChanged
        Implements IClonable

        Public Sub New()
            SetModel(New ExplorersItem())
        End Sub

        Public Sub New(model As Object, appViewModel As ApplicationViewModel)
            MyBase.New(model, appViewModel)
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property ID As Integer
            Get
                Return Model.ID
            End Get
            Set(value As Integer)
                Model.ID = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ID)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Name)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Quantity)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBox)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsUsedTM)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsStackableItem)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(StackVisibility)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(CanContainItem)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ContainedItemVisibility)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ContainedItemChoices)))
            End Set
        End Property

        Public ReadOnly Property Name As String
            Get
                Return Model.Name
            End Get
        End Property

        Public Property ContainedItemID As Integer
            Get
                Return Model.ContainedItemID
            End Get
            Set(value As Integer)
                Model.ContainedItemID = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ContainedItemID)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ContainedItemName)))
            End Set
        End Property

        Public ReadOnly Property ContainedItemName As String
            Get
                If TypeOf Model Is TDHeldItem Then
                    Return Lists.TDItems(ContainedItemID)
                Else
                    Return Lists.SkyItems(ContainedItemID)
                End If
            End Get
        End Property

        Public Property Quantity As Integer
            Get
                Return Model.Quantity
            End Get
            Set(value As Integer)
                Model.Quantity = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Quantity)))
            End Set
        End Property

        Public ReadOnly Property IsBox As Boolean
            Get
                Return Model.IsBox
            End Get
        End Property

        Public ReadOnly Property IsUsedTM As Boolean
            Get
                Return Model.IsUsedTM
            End Get
        End Property

        Public ReadOnly Property IsStackableItem As Boolean
            Get
                Return Model.IsStackableItem
            End Get
        End Property

        Public ReadOnly Property StackVisibility As Visibility
            Get
                If IsStackableItem Then
                    Return Visibility.Visible
                Else
                    Return Visibility.Collapsed
                End If
            End Get
        End Property

        Public ReadOnly Property CanContainItem As Boolean
            Get
                Return IsBox OrElse IsUsedTM
            End Get
        End Property

        Public ReadOnly Property ContainedItemVisibility As Visibility
            Get
                If CanContainItem Then
                    Return Visibility.Visible
                Else
                    Return Visibility.Collapsed
                End If
            End Get
        End Property

        Public ReadOnly Property ContainedItemChoices As Dictionary(Of Integer, String)
            Get
                If TypeOf Model Is TDHeldItem Then
                    If IsBox Then
                        Return Lists.TDItems
                    ElseIf IsUsedTM Then
                        Return Lists.TDItemsMovesOnly
                    Else
                        Return New Dictionary(Of Integer, String)
                    End If
                Else
                    If IsBox Then
                        Return Lists.SkyItems
                    ElseIf IsUsedTM Then
                        Return Lists.SkyItemsMovesOnly
                    Else
                        Return New Dictionary(Of Integer, String)
                    End If
                End If
            End Get
        End Property

        Public Function Clone() As Object Implements IClonable.Clone
            If TypeOf Model Is IClonable Then
                Return New ExplorersItemViewModel(DirectCast(Model, IClonable).Clone(), CurrentApplicationViewModel)
            Else
                Return New ExplorersItemViewModel(Model, CurrentApplicationViewModel)
            End If
        End Function

        Public Overrides Function ToString() As String
            Return If(Name, MyBase.ToString())
        End Function

    End Class
End Namespace
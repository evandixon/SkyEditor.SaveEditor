Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class TDHeldItemViewModel
        Inherits GenericViewModel(Of TDHeldItem)
        Implements INotifyPropertyChanged
        Implements IClonable

        Public Sub New()
            SetModel(New TDHeldItem())
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
            End Set
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

        Public Function Clone() As Object Implements IClonable.Clone
            If TypeOf Model Is IClonable Then
                Return New TDHeldItemViewModel(DirectCast(Model, IClonable).Clone(), CurrentApplicationViewModel)
            Else
                Return New TDHeldItemViewModel(Model, CurrentApplicationViewModel)
            End If
        End Function

        Public Overrides Function ToString() As String
            Return If(Name, MyBase.ToString())
        End Function
    End Class
End Namespace
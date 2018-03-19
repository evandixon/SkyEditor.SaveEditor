Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Rescue

Namespace MysteryDungeon.Rescue.ViewModels
    Public Class RBHeldItemViewModel
        Inherits GenericViewModel(Of RBHeldItem)
        Implements INotifyPropertyChanged
        Implements IClonable

        Public Sub New()
            SetModel(New RBHeldItem())
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
                Return Model.ToString()
            End Get
        End Property

        Public Property Parameter As Integer
            Get
                Return Model.Parameter
            End Get
            Set(value As Integer)
                Model.Parameter = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Parameter)))
            End Set
        End Property

        Public Function Clone() As Object Implements IClonable.Clone
            If TypeOf Model Is IClonable Then
                Return New RBHeldItemViewModel(DirectCast(Model, IClonable).Clone(), CurrentApplicationViewModel)
            Else
                Return New RBHeldItemViewModel(Model, CurrentApplicationViewModel)
            End If
        End Function

        Public Overrides Function ToString() As String
            Return If(Name, MyBase.ToString())
        End Function
    End Class
End Namespace
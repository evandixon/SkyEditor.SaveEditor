Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon

Namespace MysteryDungeon
    Public Class MDAttackViewModel
        Inherits GenericViewModel(Of IMDAttack)
        Implements INotifyPropertyChanged
        Implements INotifyModified
        Implements IMDAttack

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(model As IMDAttack, appViewModel As ApplicationViewModel)
            MyBase.New(model, appViewModel)
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Protected Sub RaisePropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Private Sub MDAttackViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

        Public Property IsLinked As Boolean Implements IMDAttack.IsLinked
            Get
                Return Model.IsLinked
            End Get
            Set(value As Boolean)
                If Not Model.IsLinked = value Then
                    Model.IsLinked = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsLinked)))
                End If
            End Set
        End Property

        Public Property IsSwitched As Boolean Implements IMDAttack.IsSwitched
            Get
                Return Model.IsSwitched
            End Get
            Set(value As Boolean)
                If Not Model.IsSwitched = value Then
                    Model.IsSwitched = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsSwitched)))
                End If
            End Set
        End Property

        Public Property IsSet As Boolean Implements IMDAttack.IsSet
            Get
                Return Model.IsSet
            End Get
            Set(value As Boolean)
                If Not Model.IsSet = value Then
                    Model.IsSet = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsSet)))
                End If
            End Set
        End Property

        Public Property ID As Integer Implements IMDAttack.ID
            Get
                Return Model.ID
            End Get
            Set(value As Integer)
                If Not Model.ID = value Then
                    Model.ID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ID)))
                End If
            End Set
        End Property

        Public Property Ginseng As Integer Implements IMDAttack.Ginseng
            Get
                Return Model.Ginseng
            End Get
            Set(value As Integer)
                If Not Model.Ginseng = value Then
                    Model.Ginseng = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Ginseng)))
                End If
            End Set
        End Property

        Public ReadOnly Property MoveNames As Dictionary(Of Integer, String) Implements IMDAttack.MoveNames
            Get
                Return Model.MoveNames
            End Get
        End Property

    End Class
End Namespace

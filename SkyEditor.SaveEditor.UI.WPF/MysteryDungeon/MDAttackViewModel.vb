Imports System.ComponentModel
Imports SkyEditor.Core
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon
Imports SkyEditor.SaveEditor.MysteryDungeon.Rescue

Namespace MysteryDungeon
    Public Class MDAttackViewModel
        Inherits GenericViewModel(Of IMysteryDungeonNdsAttack)
        Implements INotifyPropertyChanged
        Implements INotifyModified

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(model As IMysteryDungeonNdsAttack, appViewModel As ApplicationViewModel)
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

        Public Property IsLinked As Boolean
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

        Public Property IsSwitched As Boolean
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

        Public Property IsSet As Boolean
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

        Public Property ID As Integer
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

        Public Property PowerBoost As Integer
            Get
                Return Model.PowerBoost
            End Get
            Set(value As Integer)
                If Not Model.PowerBoost = value Then
                    Model.PowerBoost = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(PowerBoost)))
                End If
            End Set
        End Property

        Public ReadOnly Property MoveNames As Dictionary(Of Integer, String)
            Get
                If TypeOf Model Is RBAttack Then
                    Return Lists.RBMoves
                Else
                    Return Lists.ExplorersMoves
                End If
            End Get
        End Property

    End Class
End Namespace

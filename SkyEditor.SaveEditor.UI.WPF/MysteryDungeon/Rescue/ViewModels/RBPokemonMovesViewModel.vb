Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.MysteryDungeon.Rescue
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Rescue.ViewModels
    Public Class RBPokemonMovesViewModel
        Inherits GenericViewModel(Of RBStoredPokemon)
        Implements I4Moves
        Implements INotifyModified

        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Private Sub _attack1_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _attack1.PropertyChanged, _attack2.PropertyChanged, _attack3.PropertyChanged, _attack4.PropertyChanged
            RaiseEvent Modified(sender, e)
        End Sub

        Public Property Attack1 As Object Implements I4Moves.Attack1
            Get
                Return _attack1
            End Get
            Set(value As Object)
                _attack1 = value
            End Set
        End Property
        Private WithEvents _attack1 As MDAttackViewModel

        Public Property Attack2 As Object Implements I4Moves.Attack2
            Get
                Return _attack2
            End Get
            Set(value As Object)
                _attack2 = value
            End Set
        End Property
        Private WithEvents _attack2 As MDAttackViewModel

        Public Property Attack3 As Object Implements I4Moves.Attack3
            Get
                Return _attack3
            End Get
            Set(value As Object)
                _attack3 = value
            End Set
        End Property
        Private WithEvents _attack3 As MDAttackViewModel

        Public Property Attack4 As Object Implements I4Moves.Attack4
            Get
                Return _attack4
            End Get
            Set(value As Object)
                _attack4 = value
            End Set
        End Property
        Private WithEvents _attack4 As MDAttackViewModel


        Public Overrides Sub SetModel(model As Object)
            MyBase.SetModel(model)

            Dim s As RBStoredPokemon = model

            Dim a1 = New MDAttackViewModel(s.Attack1, CurrentApplicationViewModel)
            Dim a2 = New MDAttackViewModel(s.Attack2, CurrentApplicationViewModel)
            Dim a3 = New MDAttackViewModel(s.Attack3, CurrentApplicationViewModel)
            Dim a4 = New MDAttackViewModel(s.Attack4, CurrentApplicationViewModel)

            Attack1 = a1
            Attack2 = a2
            Attack3 = a3
            Attack4 = a4
        End Sub

    End Class
End Namespace

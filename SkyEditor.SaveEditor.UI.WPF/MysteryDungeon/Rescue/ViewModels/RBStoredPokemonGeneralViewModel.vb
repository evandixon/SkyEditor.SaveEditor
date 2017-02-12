Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Rescue

Namespace MysteryDungeon.Rescue.ViewModels
    Public Class RBStoredPokemonGeneralViewModel
        Inherits GenericViewModel(Of RBStoredPokemon)
        Implements INotifyPropertyChanged
        Implements INotifyModified

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Private Sub RBStoredPokemonGeneralViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, e)
        End Sub

        Public Property Level As Byte
            Get
                Return Model.Level
            End Get
            Set(value As Byte)
                If Not Model.Level = value Then
                    Model.Level = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Level)))
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

        Public Property MetAt As Integer
            Get
                Return Model.MetAt
            End Get
            Set(value As Integer)
                If Not Model.MetAt = value Then
                    Model.MetAt = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(MetAt)))
                End If
            End Set
        End Property

        Public Property IQ As Integer
            Get
                Return Model.IQ
            End Get
            Set(value As Integer)
                If Not Model.IQ = value Then
                    Model.IQ = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IQ)))
                End If
            End Set
        End Property

        Public Property HP As Integer
            Get
                Return Model.HP
            End Get
            Set(value As Integer)
                If Not Model.HP = value Then
                    Model.HP = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(HP)))
                End If
            End Set
        End Property

        Public Property Attack As Byte
            Get
                Return Model.Attack
            End Get
            Set(value As Byte)
                If Not Model.Attack = value Then
                    Model.Attack = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack)))
                End If
            End Set
        End Property

        Public Property Defense As Byte
            Get
                Return Model.Defense
            End Get
            Set(value As Byte)
                If Not Model.Defense = value Then
                    Model.Defense = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Defense)))
                End If
            End Set
        End Property

        Public Property SpAttack As Byte
            Get
                Return Model.SpAttack
            End Get
            Set(value As Byte)
                If Not Model.SpAttack = value Then
                    Model.SpAttack = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SpAttack)))
                End If
            End Set
        End Property

        Public Property SpDefense As Byte
            Get
                Return Model.SpDefense
            End Get
            Set(value As Byte)
                If Not Model.SpDefense = value Then
                    Model.SpDefense = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SpDefense)))
                End If
            End Set
        End Property

        Public Property Exp As Integer
            Get
                Return Model.Exp
            End Get
            Set(value As Integer)
                If Not Model.Exp = value Then
                    Model.Exp = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Exp)))
                End If
            End Set
        End Property

        Public Property Name As String
            Get
                Return Model.Name
            End Get
            Set(value As String)
                If Not Model.Name = value Then
                    Model.Name = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Name)))
                End If
            End Set
        End Property

        Public ReadOnly Property PokemonNames As Dictionary(Of Integer, String)
            Get
                Return Lists.RBPokemon
            End Get
        End Property

        Public ReadOnly Property LocationNames As Dictionary(Of Integer, String)
            Get
                Return Lists.RBLocations
            End Get
        End Property

    End Class

End Namespace

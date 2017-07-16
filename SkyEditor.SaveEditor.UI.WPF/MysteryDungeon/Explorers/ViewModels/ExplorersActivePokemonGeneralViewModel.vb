Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class ExplorersActivePokemonGeneralViewModel
        Inherits GenericViewModel(Of IExplorersActivePokemon)
        Implements INotifyPropertyChanged
        Implements INotifyModified
        Implements INamed

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Private Sub ExplorersActivePokemonGeneralViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

        Public Property Level As Integer
            Get
                Return Model.Level
            End Get
            Set(value As Integer)
                If Not Model.Level = value Then
                    Model.Level = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Level)))
                End If
            End Set
        End Property

        Public Property ID As Integer
            Get
                Return Model.ID.ID
            End Get
            Set(value As Integer)
                If Not Model.ID.ID = value Then
                    Model.ID.ID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ID)))
                End If
            End Set
        End Property

        ''' <summary>
        ''' The index of the Pokemon in storage as stored in the save file.
        ''' </summary>
        ''' <returns></returns>
        Public Property RosterNumber As Integer
            Get
                Return Model.RosterNumber
            End Get
            Set(value As Integer)
                If Not Model.RosterNumber = value Then
                    Model.RosterNumber = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(RosterNumber)))
                End If
            End Set
        End Property

        Public Property IsFemale As Boolean
            Get
                Return Model.ID.IsFemale
            End Get
            Set(value As Boolean)
                If Not Model.ID.IsFemale = value Then
                    Model.ID.IsFemale = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsFemale)))
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

        Public Property MetFloor As Integer
            Get
                Return Model.MetFloor
            End Get
            Set(value As Integer)
                If Not Model.MetFloor = value Then
                    Model.MetFloor = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(MetFloor)))
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

        Public Property CurrentHP As Integer
            Get
                Return Model.CurrentHP
            End Get
            Set(value As Integer)
                If Not Model.CurrentHP = value Then
                    Model.CurrentHP = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(CurrentHP)))
                End If
            End Set
        End Property

        Public Property MaxHP As Integer
            Get
                Return Model.MaxHP
            End Get
            Set(value As Integer)
                If Not Model.MaxHP = value Then
                    Model.MaxHP = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(MaxHP)))
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

        Public Property Attack1 As ExplorersActiveAttack
            Get
                Return Model.Attack1
            End Get
            Set(value As ExplorersActiveAttack)
                If Model.Attack1 IsNot value Then
                    Model.Attack1 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack1)))
                End If
            End Set
        End Property

        Public Property Attack2 As ExplorersActiveAttack
            Get
                Return Model.Attack2
            End Get
            Set(value As ExplorersActiveAttack)
                If Model.Attack2 IsNot value Then
                    Model.Attack2 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack2)))
                End If
            End Set
        End Property

        Public Property Attack3 As ExplorersActiveAttack
            Get
                Return Model.Attack3
            End Get
            Set(value As ExplorersActiveAttack)
                If Model.Attack3 IsNot value Then
                    Model.Attack3 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack3)))
                End If
            End Set
        End Property

        Public Property Attack4 As ExplorersActiveAttack
            Get
                Return Model.Attack4
            End Get
            Set(value As ExplorersActiveAttack)
                If Model.Attack4 IsNot value Then
                    Model.Attack4 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack4)))
                End If
            End Set
        End Property

        Public Property Name As String Implements INamed.Name
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
                Return Lists.ExplorersPokemon
            End Get
        End Property

        Public ReadOnly Property LocationNames As Dictionary(Of Integer, String)
            Get
                If TypeOf Model Is TDActivePokemon Then
                    Return Lists.TDLocations
                Else
                    Return Lists.SkyLocations
                End If
            End Get
        End Property

    End Class

End Namespace

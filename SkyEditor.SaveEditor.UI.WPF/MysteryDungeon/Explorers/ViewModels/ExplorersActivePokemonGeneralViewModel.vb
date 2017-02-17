Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class ExplorersActivePokemonGeneralViewModel
        Inherits GenericViewModel(Of IExplorersActivePokemon)
        Implements IExplorersActivePokemon

        Implements INotifyPropertyChanged
        Implements INotifyModified
        Implements INamed

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Private Sub ExplorersActivePokemonGeneralViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

        Public Function ToStored() As IExplorersStoredPokemon Implements IExplorersActivePokemon.ToStored
            Return Model.ToStored
        End Function

        Public Property Level As Byte Implements IExplorersActivePokemon.Level
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

        Public Property ID As Integer Implements IExplorersActivePokemon.ID
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

        ''' <summary>
        ''' The index of the Pokemon in storage as stored in the save file.
        ''' </summary>
        ''' <returns></returns>
        Public Property RosterNumber As Integer Implements IExplorersActivePokemon.RosterNumber
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

        Public Property IsFemale As Boolean Implements IExplorersActivePokemon.IsFemale
            Get
                Return Model.IsFemale
            End Get
            Set(value As Boolean)
                If Not Model.IsFemale = value Then
                    Model.IsFemale = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsFemale)))
                End If
            End Set
        End Property

        Public Property MetAt As Integer Implements IExplorersActivePokemon.MetAt
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

        Public Property MetFloor As Integer Implements IExplorersActivePokemon.MetFloor
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

        Public Property IQ As Integer Implements IExplorersActivePokemon.IQ
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

        Public Property HP1 As Integer Implements IExplorersActivePokemon.HP1
            Get
                Return Model.HP1
            End Get
            Set(value As Integer)
                If Not Model.HP1 = value Then
                    Model.HP1 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(HP1)))
                End If
            End Set
        End Property

        Public Property HP2 As Integer Implements IExplorersActivePokemon.HP2
            Get
                Return Model.HP2
            End Get
            Set(value As Integer)
                If Not Model.HP2 = value Then
                    Model.HP2 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(HP2)))
                End If
            End Set
        End Property

        Public Property Attack As Byte Implements IExplorersActivePokemon.Attack
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

        Public Property Defense As Byte Implements IExplorersActivePokemon.Defense
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

        Public Property SpAttack As Byte Implements IExplorersActivePokemon.SpAttack
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

        Public Property SpDefense As Byte Implements IExplorersActivePokemon.SpDefense
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

        Public Property Exp As Integer Implements IExplorersActivePokemon.Exp
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

        Public Property Attack1 As ExplorersActiveAttack Implements IExplorersActivePokemon.Attack1
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

        Public Property Attack2 As ExplorersActiveAttack Implements IExplorersActivePokemon.Attack2
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

        Public Property Attack3 As ExplorersActiveAttack Implements IExplorersActivePokemon.Attack3
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

        Public Property Attack4 As ExplorersActiveAttack Implements IExplorersActivePokemon.Attack4
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

        Public Property Name As String Implements IExplorersActivePokemon.Name, INamed.Name
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

        Public ReadOnly Property PokemonNames As Dictionary(Of Integer, String) Implements IExplorersActivePokemon.PokemonNames
            Get
                Return Model.PokemonNames
            End Get
        End Property

        Public ReadOnly Property LocationNames As Dictionary(Of Integer, String) Implements IExplorersActivePokemon.LocationNames
            Get
                Return Model.LocationNames
            End Get
        End Property

    End Class

End Namespace

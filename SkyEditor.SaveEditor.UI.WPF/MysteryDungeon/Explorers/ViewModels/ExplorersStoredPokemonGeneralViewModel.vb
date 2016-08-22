Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.Core.Utilities
Imports SkyEditor.SaveEditor.MysteryDungeon
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class ExplorersStoredPokemonGeneralViewModel
        Inherits GenericViewModel(Of IExplorersStoredPokemon)
        Implements IExplorersStoredPokemon
        Implements INotifyPropertyChanged
        Implements INotifyModified
        Implements INamed

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified

        Private Sub ExplorersStoredPokemonGeneralViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, e)
        End Sub

        Public Function ToActive() As IExplorersActivePokemon Implements IExplorersStoredPokemon.ToActive
            Return Model.ToActive
        End Function

        Public Property Level As Byte Implements IExplorersStoredPokemon.Level
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

        Public Property ID As Integer Implements IExplorersStoredPokemon.ID
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

        Public Property IsFemale As Boolean Implements IExplorersStoredPokemon.IsFemale
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

        Public Property MetAt As Integer Implements IExplorersStoredPokemon.MetAt
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

        Public Property MetFloor As Integer Implements IExplorersStoredPokemon.MetFloor
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

        Public Property IQ As Integer Implements IExplorersStoredPokemon.IQ
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

        Public Property HP As Integer Implements IExplorersStoredPokemon.HP
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

        Public Property Attack As Byte Implements IExplorersStoredPokemon.Attack
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

        Public Property Defense As Byte Implements IExplorersStoredPokemon.Defense
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

        Public Property SpAttack As Byte Implements IExplorersStoredPokemon.SpAttack
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

        Public Property SpDefense As Byte Implements IExplorersStoredPokemon.SpDefense
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

        Public Property Exp As Integer Implements IExplorersStoredPokemon.Exp
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

        Public Property Attack1 As IMDAttack Implements IExplorersStoredPokemon.Attack1
            Get
                Return Model.Attack1
            End Get
            Set(value As IMDAttack)
                If Model.Attack1 IsNot value Then
                    Model.Attack1 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack1)))
                End If
            End Set
        End Property

        Public Property Attack2 As IMDAttack Implements IExplorersStoredPokemon.Attack2
            Get
                Return Model.Attack2
            End Get
            Set(value As IMDAttack)
                If Model.Attack2 IsNot value Then
                    Model.Attack2 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack2)))
                End If
            End Set
        End Property

        Public Property Attack3 As IMDAttack Implements IExplorersStoredPokemon.Attack3
            Get
                Return Model.Attack3
            End Get
            Set(value As IMDAttack)
                If Model.Attack3 IsNot value Then
                    Model.Attack3 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack3)))
                End If
            End Set
        End Property

        Public Property Attack4 As IMDAttack Implements IExplorersStoredPokemon.Attack4
            Get
                Return Model.Attack4
            End Get
            Set(value As IMDAttack)
                If Model.Attack4 IsNot value Then
                    Model.Attack4 = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Attack4)))
                End If
            End Set
        End Property

        Public Property Name As String Implements IExplorersStoredPokemon.Name, INamed.Name
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

        Public ReadOnly Property PokemonNames As Dictionary(Of Integer, String) Implements IExplorersStoredPokemon.PokemonNames
            Get
                Return Lists.ExplorersPokemon
            End Get
        End Property

        Public ReadOnly Property LocationNames As Dictionary(Of Integer, String) Implements IExplorersStoredPokemon.LocationNames
            Get
                Return Lists.GetSkyLocations
            End Get
        End Property

    End Class
End Namespace


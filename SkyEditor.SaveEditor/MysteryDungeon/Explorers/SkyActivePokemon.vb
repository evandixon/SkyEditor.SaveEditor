﻿Imports SkyEditor.Core.ConsoleCommands
Imports SkyEditor.Core.IO

Namespace MysteryDungeon.Explorers
    Public Class SkyActivePokemon
        Implements IExplorersActivePokemon
        Implements IOpenableFile
        Implements ISavableAs
        Implements IOnDisk

        Public Const Length = 546
        Public Const MimeType As String = "application/x-sky-active-pokemon"

        Public Event FileSaved As ISavable.FileSavedEventHandler Implements ISavable.FileSaved

        Public Sub New()
            Initialize(New Binary(Length))
        End Sub

        Public Sub New(bits As Binary)
            Initialize(bits)
        End Sub

        Private Sub Initialize(bits As Binary)
            With bits
                IsValid = .Bit(0)
                Unk1 = .Range(1, 4)
                Level = .Int(0, 5, 7)
                MetAt = .Int(0, 12, 8)
                MetFloor = .Int(0, 20, 7)
                Unk2 = .Range(27, 1)
                IQ = .Int(0, 28, 10)
                RosterNumber = .Int(0, 38, 10)
                Unk3 = .Range(48, 22)
                Dim idRaw As Integer = .Int(0, 70, 11)
                If idRaw > 600 Then
                    IsFemale = True
                    ID = idRaw - 600
                Else
                    IsFemale = False
                    ID = idRaw
                End If
                HP1 = .Int(0, 81, 10)
                HP2 = .Int(0, 91, 10)
                Attack = .Int(0, 101, 8)
                SpAttack = .Int(0, 109, 8)
                Defense = .Int(0, 117, 8)
                SpDefense = .Int(0, 125, 8)
                Exp = .Int(0, 133, 24)
                Attack1 = New ExplorersActiveAttack(.Range(157, ExplorersActiveAttack.Length))
                Attack2 = New ExplorersActiveAttack(.Range(186, ExplorersActiveAttack.Length))
                Attack3 = New ExplorersActiveAttack(.Range(215, ExplorersActiveAttack.Length))
                Attack4 = New ExplorersActiveAttack(.Range(244, ExplorersActiveAttack.Length))
                Unk4 = .Range(273, 105)
                IQMap = .Range(378, 69)
                Tactic = .Int(0, 447, 4)
                Unk5 = .Range(451, 15)
                Name = .GetStringPMD(0, 466, 10)
            End With
        End Sub

        Public Function GetActivePokemonBits() As Binary
            Dim out As New Binary(Length)
            With out
                .Bit(0) = IsValid
                .Range(1, 4) = Unk1
                .Int(0, 5, 7) = Level
                .Int(0, 12, 8) = MetAt
                .Int(0, 20, 7) = MetFloor
                .Range(27, 1) = Unk2
                .Int(0, 28, 10) = IQ
                .Int(0, 38, 10) = RosterNumber
                .Range(48, 22) = Unk3
                If IsFemale Then
                    .Int(0, 70, 11) = ID + 600
                Else
                    .Int(0, 70, 11) = ID
                End If
                .Int(0, 81, 10) = HP1
                .Int(0, 91, 10) = HP2
                .Int(0, 101, 8) = Attack
                .Int(0, 109, 8) = SpAttack
                .Int(0, 117, 8) = Defense
                .Int(0, 125, 8) = SpDefense
                .Int(0, 133, 24) = Exp
                .Range(157, ExplorersActiveAttack.Length) = _Attack1.GetAttackBits
                .Range(186, ExplorersActiveAttack.Length) = _Attack2.GetAttackBits
                .Range(215, ExplorersActiveAttack.Length) = _Attack3.GetAttackBits
                .Range(244, ExplorersActiveAttack.Length) = _Attack4.GetAttackBits
                .Range(273, 105) = Unk4
                .Range(378, 69) = IQMap
                .Int(0, 447, 4) = Tactic
                .Range(451, 15) = Unk5
                .SetStringPMD(0, 466, 10, Name)
            End With
            Return out
        End Function

        Public Async Function OpenFile(Filename As String, Provider As IOProvider) As Task Implements IOpenableFile.OpenFile
            Dim toOpen As New BinaryFile
            Await toOpen.OpenFile(Filename, Provider)

            'matix2267's convention adds 6 bits to the beginning of a file so that the name will be byte-aligned
            For i = 1 To 8 - (Length Mod 8)
                toOpen.Bits.Bits.RemoveAt(0)
            Next

            Initialize(toOpen.Bits)
        End Function

        Public Async Function Save(Filename As String, provider As IOProvider) As Task Implements ISavableAs.Save
            Dim toSave As New BinaryFile()
            toSave.CreateFile(Path.GetFileNameWithoutExtension(Filename))
            'matix2267's convention adds 6 bits to the beginning of a file so that the name will be byte-aligned
            For i = 1 To 8 - (Length Mod 8)
                toSave.Bits.Bits.Add(0)
            Next
            toSave.Bits.Bits.AddRange(GetActivePokemonBits)
            Await toSave.Save(Filename, provider)
            RaiseEvent FileSaved(Me, New EventArgs)
        End Function

        Public Function GetDefaultExtension() As String Implements ISavableAs.GetDefaultExtension
            Return ".skypkmex"
        End Function

        Public Async Function Save(provider As IOProvider) As Task Implements ISavable.Save
            Await Save(Filename, provider)
        End Function

        Public Property Filename As String Implements IOnDisk.Filename

        Public Overrides Function ToString() As String
            If IsValid Then
                Return String.Format(My.Resources.Language.SkyStoredPokemonToString, Name, Level, Lists.ExplorersPokemon(ID))
            Else
                Return My.Resources.Language.BlankPokemon
            End If
        End Function

        Public Function GetSupportedExtensions() As IEnumerable(Of String) Implements ISavableAs.GetSupportedExtensions
            Return {"skypkmex"}
        End Function

        Public Function ToStored() As IExplorersStoredPokemon Implements IExplorersActivePokemon.ToStored
            Throw New NotImplementedException()
        End Function

#Region "Properties"
        Private Property Unk1 As Binary
        Private Property Unk2 As Binary
        Private Property Unk3 As Binary
        Private Property Unk4 As Binary
        Private Property Unk5 As Binary

        Public Property IsValid As Boolean

        Public Property Level As Byte Implements IExplorersActivePokemon.Level

        Public Property ID As Integer Implements IExplorersActivePokemon.ID

        ''' <summary>
        ''' The index of the Pokemon in storage as stored in the save file.
        ''' </summary>
        ''' <returns></returns>
        Public Property RosterNumber As Integer Implements IExplorersActivePokemon.RosterNumber

        Public Property IsFemale As Boolean Implements IExplorersActivePokemon.IsFemale

        Public Property MetAt As Integer Implements IExplorersActivePokemon.MetAt

        Public Property MetFloor As Integer Implements IExplorersActivePokemon.MetFloor

        Public Property IQ As Integer Implements IExplorersActivePokemon.IQ

        Public Property HP1 As Integer Implements IExplorersActivePokemon.HP1

        Public Property HP2 As Integer Implements IExplorersActivePokemon.HP2

        Public Property Attack As Byte Implements IExplorersActivePokemon.Attack

        Public Property Defense As Byte Implements IExplorersActivePokemon.Defense

        Public Property SpAttack As Byte Implements IExplorersActivePokemon.SpAttack

        Public Property SpDefense As Byte Implements IExplorersActivePokemon.SpDefense

        Public Property Exp As Integer Implements IExplorersActivePokemon.Exp

        Public Property Attack1 As ExplorersActiveAttack Implements IExplorersActivePokemon.Attack1

        Public Property Attack2 As ExplorersActiveAttack Implements IExplorersActivePokemon.Attack2

        Public Property Attack3 As ExplorersActiveAttack Implements IExplorersActivePokemon.Attack3

        Public Property Attack4 As ExplorersActiveAttack Implements IExplorersActivePokemon.Attack4
        Public Property IQMap As Binary
        Public Property Tactic As Integer

        Public Property Name As String Implements IExplorersActivePokemon.Name

        Private ReadOnly Property PokemonNames As Dictionary(Of Integer, String) Implements IExplorersActivePokemon.PokemonNames
            Get
                Return Lists.ExplorersPokemon
            End Get
        End Property

        Private ReadOnly Property LocationNames As Dictionary(Of Integer, String) Implements IExplorersActivePokemon.LocationNames
            Get
                Return Lists.GetSkyLocations
            End Get
        End Property

#End Region

        <Obsolete("Testing code")> Public Sub DumpToConsole(Console As IConsoleProvider)
            Console.WriteLine($"Name: {Name}")
            Console.WriteLine($"Unknown 1: {Unk1.GetBigEndianStringRepresentation}")
            Console.WriteLine($"Unknown 2: {Unk2.GetBigEndianStringRepresentation}")
            Console.WriteLine($"Unknown 3: {Unk3.GetBigEndianStringRepresentation}")
            Console.WriteLine($"Unknown 4: {Unk4.GetBigEndianStringRepresentation}")
            Console.WriteLine($"Unknown 5: {Unk5.GetBigEndianStringRepresentation}")
        End Sub

    End Class
End Namespace

Imports SkyEditor.Core.IO

Namespace MysteryDungeon.Explorers
    Public Class TDStoredPokemon
        Implements IExplorersStoredPokemon
        Implements IOpenableFile
        Implements ISavableAs
        Implements IOnDisk

        Public Const Length = 388
        Public Const MimeType As String = "application/x-td-pokemon"

        Public Event FileSaved As ISavable.FileSavedEventHandler Implements ISavable.FileSaved

        Public Sub New()
            Unk2 = New Binary(96)
        End Sub

        Public Sub New(bits As Binary)
            Initialize(bits)
        End Sub

        Private Sub Initialize(bits As Binary)
            With bits
                'IsValid = .Bit(0) 'Not 100% sure about this
                Level = .Int(0, 1, 7)

                Dim idRaw As Integer = .Int(0, 8, 11)
                If idRaw > 600 Then
                    IsFemale = True
                    ID = idRaw - 600
                Else
                    IsFemale = False
                    ID = idRaw
                End If

                MetAt = .Int(0, 19, 8)
                MetFloor = .Int(0, 27, 7)

                Unk1 = .Bit(34)
                EvolvedAtLevel1 = .Int(0, 35, 7)
                EvolvedAtLevel2 = .Int(0, 42, 7)

                IQ = .Int(0, 49, 10)
                HP = .Int(0, 59, 10)
                Attack = .Int(0, 69, 8)
                SpAttack = .Int(0, 77, 8)
                Defense = .Int(0, 85, 8)
                SpDefense = .Int(0, 93, 8)
                Exp = .Int(0, 101, 24)

                Unk2 = .Range(125, 96)
                'Todo: set tactic

                Attack1 = New ExplorersAttack(.Range(221, ExplorersAttack.Length))
                Attack2 = New ExplorersAttack(.Range(242, ExplorersAttack.Length))
                Attack3 = New ExplorersAttack(.Range(263, ExplorersAttack.Length))
                Attack4 = New ExplorersAttack(.Range(284, ExplorersAttack.Length))
                Name = .GetStringPMD(0, 305, 10)
                Unk3 = .Range(385, 3)
            End With
        End Sub

        Public Function GetStoredPokemonBits() As Binary
            Dim out As New Binary(Length)
            With out
                '.Bit(0) = IsValid
                .Int(0, 1, 7) = Level

                If IsFemale Then
                    .Int(0, 8, 11) = ID + 600
                Else
                    .Int(0, 8, 11) = ID
                End If

                .Int(0, 19, 8) = MetAt
                .Int(0, 27, 7) = MetFloor

                .Bit(34) = Unk1
                .Int(0, 35, 7) = EvolvedAtLevel1
                .Int(0, 42, 7) = EvolvedAtLevel2

                .Int(0, 49, 10) = IQ
                .Int(0, 59, 10) = HP
                .Int(0, 69, 8) = Attack
                .Int(0, 77, 8) = SpAttack
                .Int(0, 85, 8) = Defense
                .Int(0, 93, 8) = SpDefense
                .Int(0, 101, 24) = Exp

                .Range(125, 96) = Unk2
                'Todo: set tactic

                .Range(221, ExplorersAttack.Length) = _attack1.GetAttackBits
                .Range(242, ExplorersAttack.Length) = _attack2.GetAttackBits
                .Range(263, ExplorersAttack.Length) = _attack3.GetAttackBits
                .Range(284, ExplorersAttack.Length) = _attack4.GetAttackBits
                .SetStringPMD(0, 305, 10, Name)
                .Range(385, 3) = Unk3
            End With
            Return out
        End Function

        Public Async Function OpenFile(Filename As String, Provider As IIOProvider) As Task Implements IOpenableFile.OpenFile
            Dim toOpen As New BinaryFile
            Await toOpen.OpenFile(Filename, Provider)

            'matix2267's convention adds 6 bits to the beginning of a file so that the name will be byte-aligned
            For i = 1 To 8 - (Length Mod 8)
                toOpen.Bits.Bits.RemoveAt(0)
            Next

            Initialize(toOpen.Bits)
        End Function

        Public Async Function Save(Filename As String, provider As IIOProvider) As Task Implements ISavableAs.Save
            Dim toSave As New BinaryFile()
            toSave.CreateFile(Path.GetFileNameWithoutExtension(Filename))
            'matix2267's convention adds 6 bits to the beginning of a file so that the name will be byte-aligned
            For i = 1 To 8 - (Length Mod 8)
                toSave.Bits.Bits.Add(0)
            Next
            toSave.Bits.Bits.AddRange(GetStoredPokemonBits)
            Await toSave.Save(Filename, provider)
            RaiseEvent FileSaved(Me, New EventArgs)
        End Function

        Public Function GetDefaultExtension() As String Implements ISavableAs.GetDefaultExtension
            Return "tdpkm"
        End Function

        Public Async Function Save(provider As IIOProvider) As Task Implements ISavable.Save
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
            Return {"tdpkm"}
        End Function

        Public Function ToActive(rosterNumber As Integer) As IExplorersActivePokemon Implements IExplorersStoredPokemon.ToActive
            Throw New NotImplementedException()
        End Function

#Region "Properties"
        Private Property Unk1 As Boolean
        Private Property Unk2 As Binary
        Private Property Unk3 As Binary

        Public ReadOnly Property IsValid As Boolean
            Get
                Return ID > 0
            End Get
        End Property

        Public Property Level As Byte Implements IExplorersStoredPokemon.Level

        Public Property ID As Integer Implements IExplorersStoredPokemon.ID

        Public Property IsFemale As Boolean Implements IExplorersStoredPokemon.IsFemale

        Public Property MetAt As Integer Implements IExplorersStoredPokemon.MetAt

        Public Property MetFloor As Integer Implements IExplorersStoredPokemon.MetFloor

        Public Property EvolvedAtLevel1 As Integer Implements IExplorersStoredPokemon.EvolvedAtLevel1

        Public Property EvolvedAtLevel2 As Integer Implements IExplorersStoredPokemon.EvolvedAtLevel2

        Public Property IQ As Integer Implements IExplorersStoredPokemon.IQ

        Public Property HP As Integer Implements IExplorersStoredPokemon.HP

        Public Property Attack As Byte Implements IExplorersStoredPokemon.Attack

        Public Property Defense As Byte Implements IExplorersStoredPokemon.Defense

        Public Property SpAttack As Byte Implements IExplorersStoredPokemon.SpAttack

        Public Property SpDefense As Byte Implements IExplorersStoredPokemon.SpDefense

        Public Property Exp As Integer Implements IExplorersStoredPokemon.Exp

        Public Property Tactic As Integer Implements IExplorersStoredPokemon.Tactic

        Public Property Attack1 As ExplorersAttack Implements IExplorersStoredPokemon.Attack1
            Get
                Return _attack1
            End Get
            Set(value As ExplorersAttack)
                _attack1 = value
            End Set
        End Property
        Private WithEvents _attack1 As ExplorersAttack

        Public Property Attack2 As ExplorersAttack Implements IExplorersStoredPokemon.Attack2
            Get
                Return _attack2
            End Get
            Set(value As ExplorersAttack)
                _attack2 = value
            End Set
        End Property
        Private WithEvents _attack2 As ExplorersAttack

        Public Property Attack3 As ExplorersAttack Implements IExplorersStoredPokemon.Attack3
            Get
                Return _attack3
            End Get
            Set(value As ExplorersAttack)
                _attack3 = value
            End Set
        End Property
        Private WithEvents _attack3 As ExplorersAttack

        Public Property Attack4 As ExplorersAttack Implements IExplorersStoredPokemon.Attack4
            Get
                Return _attack4
            End Get
            Set(value As ExplorersAttack)
                _attack4 = value
            End Set
        End Property
        Private WithEvents _attack4 As ExplorersAttack

        Public Property Name As String Implements IExplorersStoredPokemon.Name

        Public ReadOnly Property PokemonNames As Dictionary(Of Integer, String) Implements IExplorersStoredPokemon.PokemonNames
            Get
                Return Lists.ExplorersPokemon
            End Get
        End Property

        Public ReadOnly Property LocationNames As Dictionary(Of Integer, String) Implements IExplorersStoredPokemon.LocationNames
            Get
                Return Lists.TDLocations
            End Get
        End Property

#End Region

    End Class

End Namespace

Imports SkyEditor.Core.IO

Namespace MysteryDungeon.Explorers
    Public Class SkyStoredPokemon
        Implements IExplorersStoredPokemon
        Implements IOpenableFile
        Implements ISavableAs
        Implements IOnDisk

        Public Const Length = 362
        Public Const MimeType As String = "application/x-sky-pokemon"

        Public Event FileSaved As ISavable.FileSavedEventHandler Implements ISavable.FileSaved

        Public Sub New()
            Unk1 = New Binary(15)
            Unk2 = New Binary(73)
        End Sub

        Public Sub New(bits As Binary)
            Initialize(bits)
        End Sub

        Private Sub Initialize(bits As Binary)
            With bits
                IsValid = .Bit(0)
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

                Unk1 = .Range(34, 15)

                IQ = .Int(0, 49, 10)
                HP = .Int(0, 59, 10)
                Attack = .Int(0, 69, 8)
                Defense = .Int(0, 77, 8)
                SpAttack = .Int(0, 85, 8)
                SpDefense = .Int(0, 93, 8)
                Exp = .Int(0, 101, 24)

                Unk2 = .Range(125, 73)

                Attack1 = New ExplorersAttack(.Range(198, ExplorersAttack.Length))
                Attack2 = New ExplorersAttack(.Range(219, ExplorersAttack.Length))
                Attack3 = New ExplorersAttack(.Range(240, ExplorersAttack.Length))
                Attack4 = New ExplorersAttack(.Range(261, ExplorersAttack.Length))
                Name = .GetStringPMD(0, 282, 10)
            End With
        End Sub

        Public Function GetStoredPokemonBits() As Binary
            Dim out As New Binary(Length)
            With out
                .Bit(0) = IsValid
                .Int(0, 1, 7) = Level

                If IsFemale Then
                    .Int(0, 8, 11) = ID + 600
                Else
                    .Int(0, 8, 11) = ID
                End If

                .Int(0, 19, 8) = MetAt
                .Int(0, 27, 7) = MetFloor

                .Range(34, 15) = Unk1

                .Int(0, 49, 10) = IQ
                .Int(0, 59, 10) = HP
                .Int(0, 69, 8) = Attack
                .Int(0, 77, 8) = Defense
                .Int(0, 85, 8) = SpAttack
                .Int(0, 93, 8) = SpDefense
                .Int(0, 101, 24) = Exp

                .Range(125, 73) = Unk2

                .Range(198, ExplorersAttack.Length) = _attack1.GetAttackBits
                .Range(219, ExplorersAttack.Length) = _attack2.GetAttackBits
                .Range(240, ExplorersAttack.Length) = _attack3.GetAttackBits
                .Range(261, ExplorersAttack.Length) = _attack4.GetAttackBits
                .SetStringPMD(0, 282, 10, Name)
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

        Public Sub Save(Filename As String, provider As IOProvider) Implements ISavableAs.Save
            Dim toSave As New BinaryFile()
            toSave.CreateFile(Path.GetFileNameWithoutExtension(Filename))
            'matix2267's convention adds 6 bits to the beginning of a file so that the name will be byte-aligned
            For i = 1 To 8 - (Length Mod 8)
                toSave.Bits.Bits.Add(0)
            Next
            toSave.Bits.Bits.AddRange(GetStoredPokemonBits)
            toSave.Save(Filename, provider)
            RaiseEvent FileSaved(Me, New EventArgs)
        End Sub

        Public Function GetDefaultExtension() As String Implements ISavableAs.GetDefaultExtension
            Return ".skypkm"
        End Function

        Public Sub Save(provider As IOProvider) Implements ISavable.Save
            Save(Filename, provider)
        End Sub

        Public Property Filename As String Implements IOnDisk.Filename

        Public Overrides Function ToString() As String
            If IsValid Then
                Return String.Format(My.Resources.Language.SkyStoredPokemonToString, Name, Level, Lists.ExplorersPokemon(ID))
            Else
                Return My.Resources.Language.BlankPokemon
            End If
        End Function

#Region "Properties"
        Private Property Unk1 As Binary
        Private Property Unk2 As Binary

        Public Property IsValid As Boolean

        Public Property Level As Byte Implements IExplorersStoredPokemon.Level

        Public Property ID As Integer Implements IExplorersStoredPokemon.ID

        Public Property IsFemale As Boolean Implements IExplorersStoredPokemon.IsFemale

        Public Property MetAt As Integer Implements IExplorersStoredPokemon.MetAt

        Public Property MetFloor As Integer Implements IExplorersStoredPokemon.MetFloor

        Public Property IQ As Integer Implements IExplorersStoredPokemon.IQ

        Public Property HP As Integer Implements IExplorersStoredPokemon.HP

        Public Property Attack As Byte Implements IExplorersStoredPokemon.Attack

        Public Property Defense As Byte Implements IExplorersStoredPokemon.Defense

        Public Property SpAttack As Byte Implements IExplorersStoredPokemon.SpAttack

        Public Property SpDefense As Byte Implements IExplorersStoredPokemon.SpDefense

        Public Property Exp As Integer Implements IExplorersStoredPokemon.Exp

        Public Property Attack1 As IMDAttack Implements IExplorersStoredPokemon.Attack1
            Get
                Return _attack1
            End Get
            Set(value As IMDAttack)
                _attack1 = value
            End Set
        End Property
        Dim _attack1 As ExplorersAttack

        Public Property Attack2 As IMDAttack Implements IExplorersStoredPokemon.Attack2
            Get
                Return _attack2
            End Get
            Set(value As IMDAttack)
                _attack2 = value
            End Set
        End Property
        Dim _attack2 As ExplorersAttack

        Public Property Attack3 As IMDAttack Implements IExplorersStoredPokemon.Attack3
            Get
                Return _attack3
            End Get
            Set(value As IMDAttack)
                _attack3 = value
            End Set
        End Property
        Dim _attack3 As ExplorersAttack

        Public Property Attack4 As IMDAttack Implements IExplorersStoredPokemon.Attack4
            Get
                Return _attack4
            End Get
            Set(value As IMDAttack)
                _attack4 = value
            End Set
        End Property
        Dim _attack4 As ExplorersAttack

        Public Property Name As String Implements IExplorersStoredPokemon.Name

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

#End Region
    End Class
End Namespace


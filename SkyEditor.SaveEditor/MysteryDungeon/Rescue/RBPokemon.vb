Imports SkyEditor.Core.IO

Namespace MysteryDungeon.Rescue
    Public Class RBStoredPokemon
        Implements IOpenableFile
        Implements ISavableAs
        Implements IOnDisk

        Public Const Length = 362
        Public Const MimeType As String = "application/x-rb-pokemon"

        Public Event FileSaved As ISavable.FileSavedEventHandler Implements ISavable.FileSaved

        Public Sub New()
            Unk1 = New Binary(15)
            Unk2 = New Binary(73)
        End Sub

        Public Sub New(bits As Binary)
            Initialize(bits)
        End Sub

        Private Sub Initialize(bits As Binary)
            Dim e As New DSMysteryDungeonCharacterEncoding
            With bits
                Level = .Int(0, 0, 7)
                ID = .Int(0, 7, 9)
                MetAt = .Int(0, 16, 7)
                Unk1 = .Range(23, 21)
                IQ = .Int(0, 44, 10)
                HP = .Int(0, 54, 10)
                Attack = .Int(0, 64, 8)
                Defense = .Int(0, 72, 8)
                SpAttack = .Int(0, 80, 8)
                SpDefense = .Int(0, 88, 8)
                Exp = .Int(0, 96, 24)
                Unk2 = .Range(120, 43)
                Attack1 = New RBAttack(.Range(163, RBAttack.Length))
                Attack2 = New RBAttack(.Range(183, RBAttack.Length))
                Attack3 = New RBAttack(.Range(203, RBAttack.Length))
                Attack4 = New RBAttack(.Range(223, RBAttack.Length))
                Name = .Str(243, 10, e)
            End With
        End Sub

        Public Function GetStoredPokemonBits() As Binary
            Dim e As New DSMysteryDungeonCharacterEncoding
            Dim out As New Binary(Length)
            With out
                .Int(0, 0, 7) = Level
                .Int(0, 7, 9) = ID
                .Int(0, 16, 7) = MetAt
                .Range(23, 21) = Unk1
                .Int(0, 44, 10) = IQ
                .Int(0, 54, 10) = HP
                .Int(0, 64, 8) = Attack
                .Int(0, 72, 8) = Defense
                .Int(0, 80, 8) = SpAttack
                .Int(0, 88, 8) = SpDefense
                .Int(0, 96, 24) = Exp
                .Range(120, 43) = Unk2
                .Range(163, RBAttack.Length) = _attack1.GetAttackBits
                .Range(183, RBAttack.Length) = _attack2.GetAttackBits
                .Range(203, RBAttack.Length) = _attack3.GetAttackBits
                .Range(223, RBAttack.Length) = _attack4.GetAttackBits
                .Str(243, 10, e) = Name
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
            Return ".rbpkm"
        End Function

        Public Sub Save(provider As IOProvider) Implements ISavable.Save
            Save(Filename, provider)
        End Sub

        Public Property Filename As String Implements IOnDisk.Filename

        Public Overrides Function ToString() As String
            If ID > 0 Then
                Return String.Format(My.Resources.Language.SkyStoredPokemonToString, Name, Level, Lists.RBPokemon(ID))
            Else
                Return My.Resources.Language.BlankPokemon
            End If
        End Function

#Region "Properties"
        Private Property Unk1 As Binary
        Private Property Unk2 As Binary

        Public Property Level As Byte

        Public Property ID As Integer

        Public Property MetAt As Integer

        Public Property IQ As Integer

        Public Property HP As Integer

        Public Property Attack As Byte

        Public Property Defense As Byte

        Public Property SpAttack As Byte

        Public Property SpDefense As Byte

        Public Property Exp As Integer

        Public Property Attack1 As IMDAttack
            Get
                Return _attack1
            End Get
            Set(value As IMDAttack)
                _attack1 = value
            End Set
        End Property
        Private WithEvents _attack1 As RBAttack

        Public Property Attack2 As IMDAttack
            Get
                Return _attack2
            End Get
            Set(value As IMDAttack)
                _attack2 = value
            End Set
        End Property
        Private WithEvents _attack2 As RBAttack

        Public Property Attack3 As IMDAttack
            Get
                Return _attack3
            End Get
            Set(value As IMDAttack)
                _attack3 = value
            End Set
        End Property
        Private WithEvents _attack3 As RBAttack

        Public Property Attack4 As IMDAttack
            Get
                Return _attack4
            End Get
            Set(value As IMDAttack)
                _attack4 = value
            End Set
        End Property
        Private WithEvents _attack4 As RBAttack

        Public Property Name As String

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

#End Region
    End Class
End Namespace


Namespace MysteryDungeon.Explorers
    Public Class ExplorersAttack
        Implements IMDAttack

        Public Const Length = 21

        Public Sub New()

        End Sub
        Public Sub New(bits As Binary)
            With bits
                IsValid = .Bit(0)
                IsLinked = .Bit(1)
                IsSwitched = .Bit(2)
                IsSet = .Bit(3)
                ID = .Int(0, 4, 10)
                Ginseng = .Int(0, 14, 7)
            End With
        End Sub
        Public Function GetAttackBits() As Binary
            Dim out As New Binary(Length)
            With out
                .Bit(0) = IsValid
                .Bit(1) = IsLinked
                .Bit(2) = IsSwitched
                .Bit(3) = IsSet
                .Int(0, 4, 10) = ID
                .Int(0, 14, 7) = Ginseng
            End With
            Return out
        End Function

        Public Property IsValid As Boolean

        Public Property IsLinked As Boolean Implements IMDAttack.IsLinked

        Public Property IsSwitched As Boolean Implements IMDAttack.IsSwitched

        Public Property IsSet As Boolean Implements IMDAttack.IsSet

        Public Property ID As Integer Implements IMDAttack.ID

        Public Property Ginseng As Integer Implements IMDAttack.Ginseng

        Public ReadOnly Property MoveNames As Dictionary(Of Integer, String) Implements IMDAttack.MoveNames
            Get
                Return Lists.ExplorersMoves
            End Get
        End Property

        Public Function ToActive() As ExplorersActiveAttack
            Dim out As New ExplorersActiveAttack
            out.IsValid = Me.IsValid
            out.IsLinked = Me.IsLinked
            out.IsSwitched = Me.IsSwitched
            out.IsSet = Me.IsSet
            out.ID = Me.ID
            out.Ginseng = Me.Ginseng
            out.IsSealed = False
            Return out
        End Function

    End Class

End Namespace

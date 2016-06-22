Imports SkyEditor.Core
Imports SkyEditor.Core.IO

Namespace MysteryDungeon.Rescue
    Public Class RBSaveEU
        Inherits RBSave
        Implements IOpenableFile
        Implements IDetectableFileType

        Public Sub New()
            MyBase.New(New RBEUOffsets)
        End Sub
        'Public Sub New(Filename As String)
        '    MyBase.New(Filename)
        '    Bits = New Binary()
        '    For count As Integer = 0 To Length - 1
        '        Bits.AppendByte(RawData(count))
        '    Next
        'End Sub
        Public Overrides Async Function OpenFile(Filename As String, Provider As IOProvider) As Task Implements IOpenableFile.OpenFile
            Await MyBase.OpenFile(Filename, Provider)
        End Function

        Protected Class RBEUOffsets
            Inherits RBOffsets
            Public Overrides ReadOnly Property BackupSaveStart As Integer = &H6000
            Public Overrides ReadOnly Property ChecksumEnd As Integer = &H57D0
            Public Overrides ReadOnly Property BaseTypeOffset As Integer = &H67 * 8
            Public Overrides ReadOnly Property TeamNameStart As Integer = &H4ECC * 8
            Public Overrides ReadOnly Property HeldMoneyOffset As Integer = &H4E70 * 8
            Public Overrides ReadOnly Property StoredMoneyOffset As Integer = &H4E73 * 8
            Public Overrides ReadOnly Property RescuePointsOffset As Integer = &H4ED7 * 8
            Public Overrides ReadOnly Property HeldItemOffset As Integer = &H4CF4 * 8
            Public Overrides ReadOnly Property StoredItemOffset As Integer = &H4D2F * 8 - 2
            Public Overrides ReadOnly Property StoredPokemonOffset As Integer = (&H5B7 * 8 + 3) - (323 * 9)
            Public Overrides ReadOnly Property StoredPokemonLength As Integer = 323
            Public Overrides ReadOnly Property StoredPokemonNumber As Integer = 407 + 6
        End Class

        Public Overrides Function IsFileOfType(File As GenericFile) As Task(Of Boolean) Implements IDetectableFileType.IsOfType
            If File.Length > Offsets.ChecksumEnd Then
                Dim buffer = BitConverter.GetBytes(Checksums.Calculate32BitChecksum(File, 4, Offsets.ChecksumEnd) - 1)
                Return Task.FromResult(File.RawData(0) = buffer(0) AndAlso File.RawData(1) = buffer(1) AndAlso File.RawData(2) = buffer(2) AndAlso File.RawData(3) = buffer(3))
            Else
                Return Task.FromResult(False)
            End If
        End Function
    End Class

End Namespace
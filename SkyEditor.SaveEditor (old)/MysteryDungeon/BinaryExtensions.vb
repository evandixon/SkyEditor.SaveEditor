Namespace MysteryDungeon
    Public Module BinaryExtensions
        <Extension> Function GetStringPMD(binary As Binary, byteIndex As Integer, bitIndex As Integer, byteLength As Integer) As String
            Return binary.Str(byteIndex * 8 + bitIndex, byteLength, New DSMysteryDungeonCharacterEncoding)
        End Function
        <Extension> Sub SetStringPMD(binary As Binary, byteIndex As Integer, bitIndex As Integer, byteLength As Integer, value As String)
            binary.Str(byteIndex * 8 + bitIndex, byteLength, New DSMysteryDungeonCharacterEncoding) = value
        End Sub
    End Module
End Namespace


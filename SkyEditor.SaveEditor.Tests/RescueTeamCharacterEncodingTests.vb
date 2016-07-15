Imports SkyEditor.SaveEditor.MysteryDungeon

<TestClass()> Public Class RescueTeamCharacterEncodingTests
    Public Const TestCategory As String = "Character Encoding"
    <TestMethod()> <TestCategory(TestCategory)> Public Sub BasicNamesTests()
        Dim e As New DSMysteryDungeonCharacterEncoding
        Dim testNames As String() = {"Riolu", "Poochyena", "Pikachu", "Test Name", "Accent Test: éèê", "♀♂", "", vbCrLf}
        For Each item In testNames
            Dim bytes = e.GetBytes(item)
            Dim back = e.GetString(bytes)
            Assert.AreEqual(item, back)
        Next
    End Sub

    <TestMethod()> <TestCategory(TestCategory)> Public Sub BasicEscapeTests()
        Dim e As New DSMysteryDungeonCharacterEncoding
        Dim testNames As String() = {"\81FF", "This\That", "Line End Test\"}
        For Each item In testNames
            Dim bytes = e.GetBytes(item)
            Dim back = e.GetString(bytes)
            Assert.AreEqual(item, back)
        Next
    End Sub

    <TestMethod> <TestCategory(TestCategory)> Public Sub ByteCountTests()
        Dim data As New Dictionary(Of String, Integer)
        data.Add("\81FF", 2)
        data.Add("Riolu", 5)
        data.Add("Poochyena", 9)
        data.Add("This\That", 9)

        Dim e As New DSMysteryDungeonCharacterEncoding
        For Each item In data
            Dim count = e.GetByteCount(item.Key)
            Assert.AreEqual(item.Value, count)
        Next
    End Sub

    <TestMethod> <TestCategory(TestCategory)> Public Sub NullCharacterTest()
        Dim e As New DSMysteryDungeonCharacterEncoding
        Dim sequence As Byte() = {&H50, &H6F, &H6F, &H63, &H68, &H79, &H65, &H6E, &H61, 0, &H52, &H69, &H6F, &H6C, &H75}
        Dim back = e.GetString(sequence)
        Assert.AreEqual("Poochyena", back)
    End Sub

End Class
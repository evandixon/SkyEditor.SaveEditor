Imports System.Reflection
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

<TestClass> Public Class SkySaveDataTests
    Public Const TestCategory As String = "Explorers of Sky Data Test"

    Public Property Save As SkySave

    <TestInitialize> Public Sub SetupTest()
        Save = New SkySave(My.Resources.EoS)
    End Sub

#Region "Helpers"
    ''' <summary>
    ''' Tests a basic property of a save file.
    ''' </summary>
    ''' <param name="propertyName">Name of the property of <see cref="SkySaveDataTests.Save"/> to test.</param>
    ''' <param name="expectedValue">Expected value of the test.</param>
    ''' <remarks>
    ''' First, the value is read and compared to the expected value.
    ''' Then, the file is saved and re-opened, and re-evaluated.
    ''' </remarks>
    Private Sub TestPropertyRead(propertyName As String, expectedValue As Object)
        Dim info As PropertyInfo = Save.GetType.GetProperty(propertyName)
        Dim actualValue = info.GetValue(Save)
        Assert.AreEqual(expectedValue, actualValue, 0, "Failed to read property.")

        Dim save2 = New SkySave(Save.ToByteArray)
        Dim info2 As PropertyInfo = save2.GetType.GetProperty(propertyName)
        Dim actualValue2 = info.GetValue(save2)
        Assert.AreEqual(expectedValue, actualValue2, 0, "Failed to read property after saving.")
    End Sub

    ''' <summary>
    ''' Tests reading and writing a basic property of a save file.
    ''' </summary>
    ''' <param name="propertyName">Name of the property of <see cref="SkySaveDataTests.Save"/> to test.</param>
    ''' <param name="expectedValue">Expected value of the test.</param>
    ''' <param name="newValue">New value to use to test saving.</param>
    ''' <remarks>
    ''' First, the value is read and compared to the expected value.
    ''' Then, the value is altered.
    ''' Then, the file is saved and re-opened, and re-evaluated.
    ''' </remarks>
    Private Sub TestPropertyReadWrite(propertyName As String, expectedValue As Object, newValue As Object)
        Dim info As PropertyInfo = Save.GetType.GetProperty(propertyName)
        Dim actualValue = info.GetValue(Save)
        Assert.AreEqual(expectedValue, actualValue, "Failed to read property.")

        Dim save2 = New SkySave(Save.ToByteArray)
        Dim info2 As PropertyInfo = save2.GetType.GetProperty(propertyName)
        Dim actualValue2 = info.GetValue(save2)
        Assert.AreEqual(expectedValue, actualValue2, "Failed to read property after saving.")
    End Sub


#End Region

#Region "Tests"

#Region "General"
    <TestMethod> <TestCategory(TestCategory)>
    Public Sub TeamName()
        TestPropertyReadWrite(NameOf(Save.TeamName), "Blue", "BLARG!!!!")
    End Sub

    <TestMethod> <TestCategory(TestCategory)>
    Public Sub HeldMoney()
        TestPropertyReadWrite(NameOf(Save.HeldMoney), 42, 99999)
    End Sub

    <TestMethod> <TestCategory(TestCategory)>
    Public Sub StoredMoney()
        TestPropertyReadWrite(NameOf(Save.StoredMoney), 44459, 999999)
    End Sub

    <TestMethod> <TestCategory(TestCategory)>
    Public Sub Adventures()
        TestPropertyReadWrite(NameOf(Save.Adventures), 128, UInt32.MaxValue)
    End Sub
#End Region

#Region "Items"
    <TestMethod> <TestCategory(TestCategory)>
    Public Sub HeldItems()
        Assert.AreEqual(12, Save.HeldItems.Count, "Incorrect number of held items.")

        Assert.AreEqual(131, Save.HeldItems(0).ID, "Incorrect ID for item index 0") 'Gray Gummi
        Assert.AreEqual(128, Save.HeldItems(1).ID, "Incorrect ID for item index 1") 'Sky Gummi
        Assert.AreEqual(123, Save.HeldItems(2).ID, "Incorrect ID for item index 2") 'Yellow Gummi
        Assert.AreEqual(90, Save.HeldItems(3).ID, "Incorrect ID for item index 3") 'Chesto Berry
        Assert.AreEqual(69, Save.HeldItems(4).ID, "Incorrect ID for item index 4") 'Heal Seed
        Assert.AreEqual(83, Save.HeldItems(5).ID, "Incorrect ID for item index 5") 'Totter Seed
        Assert.AreEqual(86, Save.HeldItems(6).ID, "Incorrect ID for item index 6") 'Warp Seed
        Assert.AreEqual(86, Save.HeldItems(7).ID, "Incorrect ID for item index 7") 'Warp Seed
        Assert.AreEqual(187, Save.HeldItems(8).ID, "Incorrect ID for item index 8") 'Used TM
        Assert.AreEqual(309, Save.HeldItems(9).ID, "Incorrect ID for item index 9") 'Mug Orb
        Assert.AreEqual(84, Save.HeldItems(10).ID, "Incorrect ID for item index 10") 'Sleep Seed
        Assert.AreEqual(373, Save.HeldItems(11).ID, "Incorrect ID for item index 11") 'Nifty Box

        Assert.AreEqual(132, Save.HeldItems(11).ContainedItemID, "Incorrect contained item for item index 11") 'Purple Gummi

        Save.HeldItems.Clear()
        For count = 0 To 49
            Save.HeldItems.Add(SkyHeldItem.FromStoredItemParts(1, 99))
        Next

        Dim save2 = New SkySave(Save.ToByteArray)
        For count = 0 To 49
            Assert.AreEqual(1, save2.HeldItems(count).ID, $"Incorrect ID for item index {count} after saving")
            Assert.AreEqual(99, save2.HeldItems(count).Quantity, $"Incorrect Quantity for item index {count} after saving")
        Next
    End Sub

    <TestMethod> <TestCategory(TestCategory)>
    Public Sub StoredItems()
        Assert.AreEqual(321, Save.StoredItems.Count, "Incorrect number of held items.")

        Save.StoredItems.Clear()
        For count = 0 To 999
            Save.StoredItems.Add(New SkyStoredItem(1, 99))
        Next

        Dim save2 = New SkySave(Save.ToByteArray)
        For count = 0 To 999
            Assert.AreEqual(1, save2.StoredItems(count).ID, $"Incorrect ID for item index {count} after saving")
            Assert.AreEqual(99, save2.StoredItems(count).Quantity, $"Incorrect Quantity for item index {count} after saving")
        Next
    End Sub
#End Region

#End Region


End Class

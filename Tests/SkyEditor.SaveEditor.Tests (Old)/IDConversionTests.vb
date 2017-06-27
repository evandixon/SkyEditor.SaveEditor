Imports SkyEditor.SaveEditor.MysteryDungeon

<TestClass> Public Class IDConversionTests
    Private Const Category = "ID Conversion Tests"
    <TestMethod> <TestCategory(Category)> Public Sub TestEoSToRB()
        Assert.AreEqual(1, IDConversion.ConvertEoSPokemonToRB(1), "Failed to convert Bulbasaur")
        Assert.AreEqual(226, IDConversion.ConvertEoSPokemonToRB(226), "Failed to convert Unown Z")
        Assert.AreEqual(415, IDConversion.ConvertEoSPokemonToRB(227), "Failed to convert Unown !")
        Assert.AreEqual(416, IDConversion.ConvertEoSPokemonToRB(228), "Failed to convert Unown ?")
        Assert.AreEqual(227, IDConversion.ConvertEoSPokemonToRB(229), "Failed to convert Wobbuffet")
        Assert.AreEqual(276, IDConversion.ConvertEoSPokemonToRB(278), "Failed to convert Green Celebi")
        Assert.AreEqual(-1, IDConversion.ConvertEoSPokemonToRB(279, False), "Converting Pink Celebi without throwing exceptions should return -1.")
        Assert.AreEqual(277, IDConversion.ConvertEoSPokemonToRB(280), "Failed to convert Treecko")
        Assert.AreEqual(380, IDConversion.ConvertEoSPokemonToRB(383), "Failed to convert Green Keckleon")
        Assert.AreEqual(-1, IDConversion.ConvertEoSPokemonToRB(384, False), "Converting Purple Keckleon without throwing exceptions should return -1.")
        Assert.AreEqual(381, IDConversion.ConvertEoSPokemonToRB(385), "Failed to convert Shuppet.")
        Assert.AreEqual(414, IDConversion.ConvertEoSPokemonToRB(418), "Failed to convert a form of Deoxys")
        Assert.AreEqual(417, IDConversion.ConvertEoSPokemonToRB(419), "Failed to convert a form of Deoxys")
        Assert.AreEqual(418, IDConversion.ConvertEoSPokemonToRB(420), "Failed to convert a form of Deoxys")
        Assert.AreEqual(419, IDConversion.ConvertEoSPokemonToRB(421), "Failed to convert a form of Deoxys")
        Assert.AreEqual(420, IDConversion.ConvertEoSPokemonToRB(488), "Failed to convert Munchlax")
        Assert.AreEqual(421, IDConversion.ConvertEoSPokemonToRB(553), "Failed to convert Decoy")
        Assert.AreEqual(422, IDConversion.ConvertEoSPokemonToRB(554), "Failed to convert Statue")
        Assert.AreEqual(-1, IDConversion.ConvertEoSPokemonToRB(422, False), "Turtwig is not in Red/Blue Rescue Team.")
    End Sub

    <TestMethod> <TestCategory(Category)> Public Sub TestEoSToRBExceptions()
TestEoSToRB_TestCelebi:
        Try
            IDConversion.ConvertEoSPokemonToRB(279, True)
        Catch ex As ArgumentException
            GoTo TestEoSToRB_TestKeckleon
        End Try
        Assert.Fail("Testing Pink Celebi did not throw argument exception.")
TestEoSToRB_TestKeckleon:
        Try
            IDConversion.ConvertEoSPokemonToRB(384, True)
        Catch ex As ArgumentException
            GoTo TestEoSToRB_TestAboveRange
        End Try
        Assert.Fail("Testing Purple Keckleon did not throw argument exception.")
TestEoSToRB_TestAboveRange:
        Try
            IDConversion.ConvertEoSPokemonToRB(422, True)
        Catch ex As ArgumentException
            GoTo TestEoSToRB_TestBelowRange
        End Try
        Assert.Fail("Testing Turtwig did not throw argument exception.")
TestEoSToRB_TestBelowRange:
        Try
            IDConversion.ConvertEoSPokemonToRB(-1, True)
        Catch ex As ArgumentException
            Exit Sub
        End Try
        Assert.Fail("Testing negative number did not throw argument exception.")
    End Sub
End Class

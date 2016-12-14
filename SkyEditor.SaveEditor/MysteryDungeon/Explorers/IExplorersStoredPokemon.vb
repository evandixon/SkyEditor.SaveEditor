Namespace MysteryDungeon.Explorers
    Public Interface IExplorersStoredPokemon
        Property Level As Byte
        Property ID As Integer
        Property IsFemale As Boolean
        Property MetAt As Integer
        Property MetFloor As Integer
        Property EvolvedAtLevel1 As Integer
        Property EvolvedAtLevel2 As Integer
        Property IQ As Integer
        Property HP As Integer
        Property Attack As Byte
        Property Defense As Byte
        Property SpAttack As Byte
        Property SpDefense As Byte
        Property Exp As Integer
        Property Name As String
        Property Tactic As Integer
        ReadOnly Property PokemonNames As Dictionary(Of Integer, String)
        ReadOnly Property LocationNames As Dictionary(Of Integer, String)
        Property Attack1 As ExplorersAttack
        Property Attack2 As ExplorersAttack
        Property Attack3 As ExplorersAttack
        Property Attack4 As ExplorersAttack
        Function ToActive(rosterNumber As Integer) As IExplorersActivePokemon
    End Interface

End Namespace

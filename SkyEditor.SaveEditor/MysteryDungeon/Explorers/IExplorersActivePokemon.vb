Namespace MysteryDungeon.Explorers
    Public Interface IExplorersActivePokemon

        ''' <summary>
        ''' The index of the Pokemon in storage as stored in the save file.
        ''' </summary>
        ''' <returns></returns>
        Property RosterNumber As Integer

        Property Level As Byte
        Property ID As Integer
        Property IsFemale As Boolean
        Property MetAt As Integer
        Property MetFloor As Integer
        Property IQ As Integer
        Property HP1 As Integer 'Todo: rename
        Property HP2 As Integer 'Todo: rename
        Property Attack As Byte
        Property Defense As Byte
        Property SpAttack As Byte
        Property SpDefense As Byte
        Property Exp As Integer
        Property Name As String
        ReadOnly Property PokemonNames As Dictionary(Of Integer, String)
        ReadOnly Property LocationNames As Dictionary(Of Integer, String)

        Property Attack1 As ExplorersActiveAttack
        Property Attack2 As ExplorersActiveAttack
        Property Attack3 As ExplorersActiveAttack
        Property Attack4 As ExplorersActiveAttack


    End Interface
End Namespace


﻿Namespace MysteryDungeon.Explorers
    Public Interface IExplorersStoredPokemon
        Inherits I4Moves
        Property Level As Byte
        Property ID As Integer
        Property IsFemale As Boolean
        Property MetAt As Integer
        Property MetFloor As Integer
        Property IQ As Integer
        Property HP As Integer
        Property Attack As Byte
        Property Defense As Byte
        Property SpAttack As Byte
        Property SpDefense As Byte
        Property Exp As Integer
        Property Name As String
        ReadOnly Property PokemonNames As Dictionary(Of Integer, String)
        ReadOnly Property LocationNames As Dictionary(Of Integer, String)
    End Interface

End Namespace

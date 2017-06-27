Namespace MysteryDungeon
    Public Class IDConversion
        ''' <summary>
        ''' Converts a Pokémon ID from Explorers of Sky to Red/Blue Rescue Team.
        ''' </summary>
        ''' <param name="eosID">A Pokémon ID from Explorers of Sky.</param>
        ''' <param name="throwOnUnsupported">Whether or not to throw an exception if the Pokémon from Explorers of Sky is not in Red/Blue Rescue Team.</param>
        ''' <returns>An integer indicating the equivalent Red/Blue Rescue Team Pokémon, or -1 if the Pokémon is not in the game and <paramref name="throwOnUnsupported"/> is false.</returns>
        Public Shared Function ConvertEoSPokemonToRB(eosID As Integer, Optional throwOnUnsupported As Boolean = False) As Integer
            If eosID = 554 Then
                'Statue
                Return 422
            ElseIf eosID = 553 Then
                'Decoy
                Return 421
            ElseIf eosID = 488 Then
                'Munchlax
                Return 420
            ElseIf eosID = 421 Then
                Return 419
            ElseIf eosID = 420 Then
                Return 418
            ElseIf eosID = 419 Then
                Return 417
            ElseIf eosID > 420 Then
                If throwOnUnsupported Then
                    Throw New ArgumentException(NameOf(eosID), "The given Explorers of Sky Pokémon is not a Red/Blue Rescue Team Pokémon.")
                Else
                    Return -1
                End If
            ElseIf eosID >= 385 Then
                Return eosID - 4
            ElseIf eosID = 384 Then
                'Shiny Celebi
                If throwOnUnsupported Then
                    Throw New ArgumentException(NameOf(eosID), "Shiny/Pink Celebi is not in Red/Blue Rescue Team Pokémon.")
                Else
                    Return -1S
                End If
            ElseIf eosID >= 280 Then
                Return eosID - 3
            ElseIf eosID = 279 Then
                If throwOnUnsupported Then
                    Throw New ArgumentException(NameOf(eosID), "Purple Keckleon is not in Red/Blue Rescue Team Pokémon.")
                Else
                    Return -1
                End If
            ElseIf eosID >= 229 Then
                Return eosID - 2
            ElseIf eosID = 228 Then
                Return 416
            ElseIf eosID = 227 Then
                Return 415
            ElseIf eosID < 0 Then
                If throwOnUnsupported Then
                    Throw New ArgumentException(NameOf(eosID), "Explorers of Sky Pokemon ID must be 0 or greater")
                Else
                    Return -1
                End If
            Else
                Return eosID
            End If
        End Function
    End Class
End Namespace

Imports SkyEditor.Core.IO
Imports SkyEditor.SaveEditor.MysteryDungeon

Namespace MysteryDungeon.Explorers
    Public Class SkySave
        Inherits BinaryFile
        Implements IDetectableFileType

        Public Class SkyOffsets
            Public Overridable ReadOnly Property StoredPokemonOffset As Integer = &H464 * 8
            Public Overridable ReadOnly Property StoredPokemonLength As Integer = 362
            Public Overridable ReadOnly Property StoredPokemonNumber As Integer = 720

            Public Overridable ReadOnly Property ActivePokemon1RosterIndexOffset As Integer = &H83D1 * 8 + 1
            Public Overridable ReadOnly Property ActivePokemon2RosterIndexOffset As Integer = &H83D3 * 8 + 1
            Public Overridable ReadOnly Property ActivePokemon3RosterIndexOffset As Integer = &H83D5 * 8 + 1
            Public Overridable ReadOnly Property ActivePokemon4RosterIndexOffset As Integer = &H83D7 * 8 + 1
            Public Overridable ReadOnly Property ActivePokemonOffset As Integer = &H83D9 * 8 + 1
            Public Overridable ReadOnly Property SpActivePokemonOffset As Integer = &H84F4 * 8 + 2
            Public Overridable ReadOnly Property ActivePokemonLength As Integer = 546
            Public Overridable ReadOnly Property ActivePokemonNumber As Integer = 4

            Public Overridable ReadOnly Property ItemShop1Offset As Integer = &H98CA * 8 + 6
            Public Overridable ReadOnly Property ItemShopLength As Integer = 22
            Public Overridable ReadOnly Property ItemShop1Number As Integer = 8
            Public Overridable ReadOnly Property ItemShop2Offset As Integer = &H98E0 * 8 + 6
            Public Overridable ReadOnly Property ItemShop2Number As Integer = 4

            Public Overridable ReadOnly Property AdventureLogOffset As Integer = &H9958 * 8
            Public Overridable ReadOnly Property AdventureLogLength As Integer = 447 'Not tested

            Public Overridable ReadOnly Property CroagunkShopOffset As Integer = &HB475 * 8
            Public Overridable ReadOnly Property CroagunkShopLength As Integer = 11
            Public Overridable ReadOnly Property CroagunkShopNumber As Integer = 8

            Public Overridable ReadOnly Property QuicksavePokemonNumber As Integer = 20
            Public Overridable ReadOnly Property QuicksavePokemonLength As Integer = 429 * 8
            Public Overridable ReadOnly Property QuicksavePokemonOffset As Integer = &H19000 * 8 + (&H3170 * 8)
        End Class

        Public Sub New()
            MyBase.New
            Offsets = New SkyOffsets
        End Sub

        Public Sub New(rawData As Byte())
            MyBase.New(rawData)
            Offsets = New SkyOffsets
            Init()
        End Sub

        Public Overrides Async Function OpenFile(Filename As String, Provider As IIOProvider) As Task
            Await MyBase.OpenFile(Filename, Provider)
            Init()
        End Function

        Private Sub Init()
            LoadActivePokemon()
            LoadStoredPokemon()
            LoadQuicksavePokemon()
        End Sub

        Private Sub PreSave()
            SaveActivePokemon()
            SaveStoredPokemon()
            SaveQuicksavePokemon()
        End Sub

        Public Overrides Async Function Save(Destination As String, provider As IIOProvider) As Task
            PreSave()
            Await MyBase.Save(Destination, provider)
        End Function

        Public Overrides Function ToByteArray() As Byte()
            PreSave()
            FixChecksum()
            Return MyBase.ToByteArray()
        End Function

        Public ReadOnly Property Offsets As SkyOffsets

#Region "Save Interaction"

#Region "Stored Pokemon"
        Private Sub LoadStoredPokemon()
            StoredPokemon = New List(Of SkyStoredPokemon)

            For count = 0 To Offsets.StoredPokemonNumber - 1
                Dim pkm As New SkyStoredPokemon(Bits.Range(Offsets.StoredPokemonOffset + count * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength))
                StoredPokemon.Add(pkm)
            Next
        End Sub
        Private Sub SaveStoredPokemon()
            For count = 0 To Offsets.StoredPokemonNumber - 1
                Bits.Range(Offsets.StoredPokemonOffset + count * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength) = StoredPokemon(count).GetStoredPokemonBits
            Next
        End Sub

        Public Property StoredPokemon As List(Of SkyStoredPokemon)

#End Region

#Region "Active Pokemon"

        Private Sub LoadActivePokemon()
            ActivePokemon1RosterIndex = Me.Bits.GetShort(Offsets.ActivePokemon1RosterIndexOffset)
            ActivePokemon2RosterIndex = Me.Bits.GetShort(Offsets.ActivePokemon2RosterIndexOffset)
            ActivePokemon3RosterIndex = Me.Bits.GetShort(Offsets.ActivePokemon3RosterIndexOffset)
            ActivePokemon4RosterIndex = Me.Bits.GetShort(Offsets.ActivePokemon4RosterIndexOffset)

            Dim activePokemon As New List(Of SkyActivePokemon)
            Dim spEpisodeActivePokemon As New List(Of SkyActivePokemon)
            For count As Integer = 0 To Offsets.ActivePokemonNumber - 1
                Dim main = New SkyActivePokemon(Me.Bits.Range(Offsets.ActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength))
                Dim special = New SkyActivePokemon(Me.Bits.Range(Offsets.SpActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength))

                If main.IsValid Then
                    activePokemon.Add(main)
                End If

                If special.IsValid Then
                    spEpisodeActivePokemon.Add(special)
                End If
            Next

            Me.ActivePokemon = activePokemon
            Me.SpEpisodeActivePokemon = spEpisodeActivePokemon
        End Sub

        Private Sub SaveActivePokemon()
            'Update the Active Pokemon Roster Indexes
            If ActivePokemon.Count > 0 Then
                ActivePokemon1RosterIndex = ActivePokemon(0).RosterNumber
            Else
                ActivePokemon1RosterIndex = -1
            End If
            If ActivePokemon.Count > 1 Then
                ActivePokemon2RosterIndex = ActivePokemon(1).RosterNumber
            Else
                ActivePokemon2RosterIndex = -1
            End If
            If ActivePokemon.Count > 2 Then
                ActivePokemon3RosterIndex = ActivePokemon(2).RosterNumber
            Else
                ActivePokemon3RosterIndex = -1
            End If
            If ActivePokemon.Count > 3 Then
                ActivePokemon4RosterIndex = ActivePokemon(3).RosterNumber
            Else
                ActivePokemon4RosterIndex = -1
            End If

            'Write the Active Pokemon Roster Indexes
            Me.Bits.SetShort(Offsets.ActivePokemon1RosterIndexOffset, ActivePokemon1RosterIndex)
            Me.Bits.SetShort(Offsets.ActivePokemon2RosterIndexOffset, ActivePokemon2RosterIndex)
            Me.Bits.SetShort(Offsets.ActivePokemon3RosterIndexOffset, ActivePokemon3RosterIndex)
            Me.Bits.SetShort(Offsets.ActivePokemon4RosterIndexOffset, ActivePokemon4RosterIndex)

            'Write the Active Pokemon
            For count As Integer = 0 To Offsets.ActivePokemonNumber - 1
                If ActivePokemon.Count > count Then
                    Me.Bits.Range(Offsets.ActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength) = ActivePokemon(count).GetActivePokemonBits
                Else
                    Me.Bits.Range(Offsets.ActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength) = New Binary(Offsets.ActivePokemonLength)
                End If

                If SpEpisodeActivePokemon.Count > count Then
                    Me.Bits.Range(Offsets.SpActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength) = SpEpisodeActivePokemon(count).GetActivePokemonBits
                Else
                    Me.Bits.Range(Offsets.SpActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength) = New Binary(Offsets.ActivePokemonLength)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Index of the Stored Pokémon corresponding to the first party member of the main game.
        ''' </summary>
        ''' <returns>The index of the Stored Pokémon in <see cref="StoredPokemon"/> corresponding to the first Active Pokémon, or -1 if there isn't a first Active Pokémon.</returns>
        Protected Property ActivePokemon1RosterIndex As Short

        ''' <summary>
        ''' Index of the Stored Pokémon corresponding to the second party member of the main game.
        ''' </summary>
        ''' <returns>The index of the Stored Pokémon in <see cref="StoredPokemon"/> corresponding to the second Active Pokémon, or -1 if there isn't a second Active Pokémon.</returns>
        Protected Property ActivePokemon2RosterIndex As Short

        ''' <summary>
        ''' Index of the Stored Pokémon corresponding to the third party member of the main game.
        ''' </summary>
        ''' <returns>The index of the Stored Pokémon in <see cref="StoredPokemon"/> corresponding to the third Active Pokémon, or -1 if there isn't a third Active Pokémon.</returns>
        Protected Property ActivePokemon3RosterIndex As Short

        ''' <summary>
        ''' Index of the Stored Pokémon corresponding to the fourth party member of the main game.
        ''' </summary>
        ''' <returns>The index of the Stored Pokémon in <see cref="StoredPokemon"/> corresponding to the fourth Active Pokémon, or -1 if there isn't a fourth Active Pokémon.</returns>
        Protected Property ActivePokemon4RosterIndex As Short

        Public Property ActivePokemon As List(Of SkyActivePokemon)

        Public Property SpEpisodeActivePokemon As List(Of SkyActivePokemon)

#End Region

#Region "Quicksave Pokemon"
        Private Sub LoadQuicksavePokemon()
            _QuicksavePokemon = New List(Of SkyQuicksavePokemon)
            For count = 0 To Offsets.QuicksavePokemonNumber - 1
                Dim quick = New SkyQuicksavePokemon(Bits.Range(Offsets.QuicksavePokemonOffset + Offsets.QuicksavePokemonLength * count, Offsets.QuicksavePokemonLength))
                _quicksavePokemon.Add(quick)
            Next
        End Sub
        Private Sub SaveQuicksavePokemon()
            For count = 0 To Offsets.QuicksavePokemonNumber - 1
                If QuicksavePokemon.Count > count Then
                    Bits.Range(Offsets.QuicksavePokemonOffset + Offsets.QuicksavePokemonLength * count, Offsets.QuicksavePokemonLength) = QuicksavePokemon(count).GetQuicksavePokemonBits
                Else
                    QuicksavePokemon(count) = New SkyQuicksavePokemon()
                End If
            Next
        End Sub

        Public Property QuicksavePokemon As List(Of SkyQuicksavePokemon)

#End Region

#End Region

    End Class

End Namespace
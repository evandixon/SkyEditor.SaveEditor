Imports SkyEditor.Core.IO
Imports SkyEditor.SaveEditor.MysteryDungeon

Namespace MysteryDungeon.Explorers
    Public Class SkySave
        Inherits BinaryFile
        Implements IDetectableFileType

        Public Class SkyOffsets
            Public Overridable ReadOnly Property BackupSaveStart As Integer = &HC800
            Public Overridable ReadOnly Property ChecksumEnd As Integer = &HB65A
            Public Overridable ReadOnly Property QuicksaveStart As Integer = &H19000
            Public Overridable ReadOnly Property QuicksaveChecksumStart As Integer = &H19004
            Public Overridable ReadOnly Property QuicksaveChecksumEnd As Integer = &H1E7FF

            Public Overridable ReadOnly Property TeamNameStart As Integer = &H994E * 8
            Public Overridable ReadOnly Property TeamNameLength As Integer = 10

            Public Overridable ReadOnly Property ExplorerRank As Integer = &H9958 * 8

            Public Overridable ReadOnly Property Adventures As Integer = &H8B70 * 8

            Public Overridable ReadOnly Property WindowFrameType As Integer = &H995F * 8 + 5

            Public Overridable ReadOnly Property HeldMoney As Integer = &H990C * 8 + 6
            Public Overridable ReadOnly Property SPHeldMoney As Integer = &H990F * 8 + 6
            Public Overridable ReadOnly Property StoredMoney As Integer = &H9915 * 8 + 6

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

            Public Overridable ReadOnly Property HeldItemOffset As Integer = &H8BA2 * 8
            Public Overridable ReadOnly Property HeldItemLength As Integer = 33
            Public Overridable ReadOnly Property HeldItemNumber As Integer = 50 '1st 50 are the team's, 2nd 50 are the Sp. Episode

            Public Overridable ReadOnly Property StoredItemOffset As Integer = &H8E0C * 8 + 6
            Public Overridable ReadOnly Property StoredItemLength As Integer = 2 * 11
            Public Overridable ReadOnly Property StoredItemNumber As Integer = 1000

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
            LoadGeneral()
            LoadItems()
            LoadActivePokemon()
            LoadStoredPokemon()
            LoadQuicksavePokemon()
            LoadHistory()
            LoadSettings()
        End Sub

        Private Sub PreSave()
            SaveGeneral()
            SaveItems()
            SaveActivePokemon()
            SaveStoredPokemon()
            SaveQuicksavePokemon()
            SaveHistory()
            SaveSettings()
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

#Region "General"

        ''' <summary>
        ''' Loads the General properties from the raw data.
        ''' </summary>
        Private Sub LoadGeneral()
            TeamName = Bits.GetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength)
            HeldMoney = Bits.Int(0, Offsets.HeldMoney, 24)
            SpEpisodeHeldMoney = Bits.Int(0, Offsets.SPHeldMoney, 24)
            StoredMoney = Bits.Int(0, Offsets.StoredMoney, 24)
            Adventures = Bits.Int(0, Offsets.Adventures, 32)
            ExplorerRankPoints = Bits.Int(0, Offsets.ExplorerRank, 32)
        End Sub

        ''' <summary>
        ''' Saves the General properties to the raw data.
        ''' </summary>
        Private Sub SaveGeneral()
            Bits.SetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength, TeamName)
            Bits.Int(0, Offsets.HeldMoney, 24) = HeldMoney
            Bits.Int(0, Offsets.SPHeldMoney, 24) = SpEpisodeHeldMoney
            Bits.Int(0, Offsets.StoredMoney, 24) = StoredMoney
            Bits.Int(0, Offsets.Adventures, 32) = Adventures
            Bits.Int(0, Offsets.ExplorerRank, 32) = ExplorerRankPoints
        End Sub


        ''' <summary>
        ''' Gets or sets the save file's Team Name.
        ''' </summary>
        ''' <returns>The save file's current Team Name.</returns>
        Public Property TeamName As String

        ''' <summary>
        ''' Gets or sets the held money in the main game
        ''' </summary>
        ''' <returns>The amount of money held by the player in the main game.</returns>
        Public Property HeldMoney As Integer

        ''' <summary>
        ''' Gets or sets the held money in the active special episode
        ''' </summary>
        ''' <returns>The amount of money held by the player in the active special episode.</returns>
        Public Property SpEpisodeHeldMoney As Integer

        ''' <summary>
        ''' Gets or sets the money in storage
        ''' </summary>
        ''' <returns>The amount of money stored in the Duskull bank.</returns>
        Public Property StoredMoney As Integer

        ''' <summary>
        ''' Gets or sets the number of adventures the team has had.
        ''' </summary>
        ''' <returns>The number of adventures as reported by the save file.</returns>
        ''' <remarks>This is displayed as a signed integer in-game, so if this is set to a negative number, it will appear negative.</remarks>
        Public Property Adventures As Integer

        ''' <summary>
        ''' Gets or sets the team's exploration rank points.
        ''' When set in certain ranges, the rank changes (ex. Silver, Gold, Master, etc).
        ''' </summary>
        ''' <returns>The current number of explorer points.</returns>
        ''' <remarks>While this number is not directly visible in-game, it controls the current Explorer Rank.
        ''' Use this page as reference to know what values mean what:
        ''' http://bulbapedia.bulbagarden.net/wiki/Rank_(Mystery_Dungeon)#Exploration_Ranks
        ''' </remarks>
        Public Property ExplorerRankPoints As Integer

#End Region

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

#Region "History"
        Private Sub LoadHistory()
            '----------
            '--History
            '----------

            '-----Original Player ID & Gender
            Dim rawOriginalPlayerID = Bits.Int(&HBE, 0, 16)
            If rawOriginalPlayerID > 600 Then
                OriginalPlayerID = rawOriginalPlayerID - 600
                OriginalPlayerIsFemale = True
            Else
                OriginalPlayerID = rawOriginalPlayerID
                OriginalPlayerIsFemale = False
            End If

            '-----Original Partner ID & Gender
            Dim rawOriginalPartnerID = Bits.Int(&HC0, 0, 16)
            If rawOriginalPartnerID > 600 Then
                OriginalPartnerID = rawOriginalPartnerID - 600
                OriginalPartnerIsFemale = True
            Else
                OriginalPartnerID = rawOriginalPartnerID
                OriginalPartnerIsFemale = False
            End If

            '-----Original Names
            OriginalPlayerName = Bits.GetStringPMD(&H13F, 0, 10)
            OriginalPartnerName = Bits.GetStringPMD(&H149, 0, 10)
        End Sub

        Private Sub SaveHistory()
            '----------
            '--History
            '----------

            '-----Original Player ID & Gender
            Dim rawOriginalPlayerID = OriginalPlayerID
            If OriginalPlayerIsFemale Then
                rawOriginalPlayerID += 600
            End If
            Bits.Int(&HBE, 0, 16) = rawOriginalPlayerID

            '-----Original Partner ID & Gender
            Dim rawOriginalPartnerID = OriginalPartnerID
            If OriginalPartnerIsFemale Then
                rawOriginalPartnerID += 600
            End If
            Bits.Int(&HC0, 0, 16) = rawOriginalPartnerID

            '-----Original Names
            Bits.SetStringPMD(&H13F, 0, 10, OriginalPlayerName)
            Bits.SetStringPMD(&H149, 0, 10, OriginalPartnerName)
        End Sub

        ''' <summary>
        ''' Gets or sets the original player Pokemon.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPlayerID As Integer

        ''' <summary>
        ''' Gets or sets the original player gender.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPlayerIsFemale As Boolean

        ''' <summary>
        ''' Gets or sets the original partner Pokemon.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPartnerID As Integer

        ''' <summary>
        ''' Gets or sets the original partner gender.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPartnerIsFemale As Boolean

        ''' <summary>
        ''' Gets or sets the original player name.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPlayerName As String

        ''' <summary>
        ''' Gets or sets the original partner name.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPartnerName As String
#End Region

#Region "Settings"
        Private Sub LoadSettings()
            WindowFrameType = Bits.Int(0, Offsets.WindowFrameType, 3)
        End Sub
        Private Sub SaveSettings()
            Bits.Int(0, Offsets.WindowFrameType, 3) = WindowFrameType
        End Sub

        ''' <summary>
        ''' Gets or sets the type of window frame used in the game.  Must be 1-5.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WindowFrameType As Byte

#End Region

#End Region

    End Class

End Namespace
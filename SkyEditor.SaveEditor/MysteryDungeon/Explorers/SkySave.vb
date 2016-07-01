Imports SkyEditor.Core.IO
Imports SkyEditor.SaveEditor.Modeling
Imports SkyEditor.SaveEditor.MysteryDungeon

Namespace MysteryDungeon.Explorers
    Public Class SkySave
        Inherits BinaryFile
        Implements IDetectableFileType
        Implements IParty
        Implements INotifyPropertyChanged
        Implements INotifyModified

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

        Public Overrides Async Function OpenFile(Filename As String, Provider As IOProvider) As Task
            Await MyBase.OpenFile(Filename, Provider)

            LoadGeneral()
            LoadItems()
            LoadActivePokemon()
            LoadStoredPokemon()
            LoadQuicksavePokemon()
            LoadHistory()
            LoadSettings()

        End Function

        Public Overrides Sub Save(Destination As String, provider As IOProvider)

            SaveGeneral()
            SaveItems()
            SaveActivePokemon()
            SaveStoredPokemon()
            SaveQuicksavePokemon()
            SaveHistory()
            SaveSettings()

            MyBase.Save(Destination, provider)
        End Sub

        Public ReadOnly Property Offsets As SkyOffsets

#Region "Events"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified
#End Region

#Region "Event Handlers"
        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(Me, e)
        End Sub

        Private Sub Me_OnPropertyChanged(sender As Object, e As EventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, e)
        End Sub
#End Region

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

#Region "Items"

        Private Sub LoadItems()
            'Stored Items
            StoredItems = New List(Of SkyStoredItem)
            Dim ids = Bits.Range(Offsets.StoredItemOffset, 11 * Offsets.StoredItemNumber)
            Dim params = Bits.Range(Offsets.StoredItemOffset + 11 * Offsets.StoredItemNumber, 11 * Offsets.StoredItemNumber)
            For count As Integer = 0 To 999
                Dim id = ids.NextInt(11)
                Dim p = params.NextInt(11)
                If id > 0 Then
                    StoredItems.Add(New SkyStoredItem(id, p))
                Else
                    Exit For
                End If
            Next

            'Held Items
            HeldItems = New List(Of SkyHeldItem)
            For count As Integer = 0 To Offsets.HeldItemNumber - 1
                Dim item = SkyHeldItem.FromHeldItemBits(Me.Bits.Range(Offsets.HeldItemOffset + count * Offsets.HeldItemLength, Offsets.HeldItemLength))
                If item.IsValid Then
                    HeldItems.Add(item)
                Else
                    Exit For
                End If
            Next

            'Special Episode Held Items
            SpEpisodeHeldItems = New List(Of SkyHeldItem)
            For count As Integer = Offsets.HeldItemNumber To Offsets.HeldItemNumber + Offsets.HeldItemNumber - 1
                Dim item = SkyHeldItem.FromHeldItemBits(Me.Bits.Range(Offsets.HeldItemOffset + count * Offsets.HeldItemLength, Offsets.HeldItemLength))
                If item.IsValid Then
                    SpEpisodeHeldItems.Add(item)
                Else
                    Exit For
                End If
            Next
        End Sub

        Private Sub SaveItems()
            'Stored Items
            Dim ids As New Binary(11 * Offsets.StoredItemNumber)
            Dim params As New Binary(11 * Offsets.StoredItemNumber)
            For count As Integer = 0 To Offsets.StoredItemNumber - 1
                If StoredItems.Count > count Then
                    ids.NextInt(11) = StoredItems(count).ID
                    params.NextInt(11) = StoredItems(count).GetParameter
                Else
                    ids.NextInt(11) = 0
                    params.NextInt(11) = 0
                End If
            Next
            Bits.Range(Offsets.StoredItemOffset, 11 * Offsets.StoredItemNumber) = ids
            Bits.Range(Offsets.StoredItemOffset + 11 * Offsets.StoredItemNumber, 11 * Offsets.StoredItemNumber) = params

            'Held Items
            For count As Integer = 0 To Offsets.HeldItemNumber - 1
                Dim index = Offsets.HeldItemOffset + count * Offsets.HeldItemLength
                If HeldItems.Count > count Then
                    Me.Bits.Range(index, Offsets.HeldItemLength) = HeldItems(count).GetHeldItemBits
                Else
                    Me.Bits.Range(index, Offsets.HeldItemLength) = New Binary(Offsets.HeldItemLength)
                End If
            Next

            'Special Episode Held Items
            For count As Integer = Offsets.HeldItemNumber To Offsets.HeldItemNumber + Offsets.HeldItemNumber - 1
                Dim index = Offsets.HeldItemOffset + count * Offsets.HeldItemLength
                If SpEpisodeHeldItems.Count > count Then
                    Me.Bits.Range(index, Offsets.HeldItemLength) = SpEpisodeHeldItems(count).GetHeldItemBits
                Else
                    Me.Bits.Range(index, Offsets.HeldItemLength) = New Binary(Offsets.HeldItemLength)
                End If
            Next
        End Sub

        Public Property StoredItems As List(Of SkyStoredItem)

        Public Property HeldItems As List(Of SkyHeldItem)

        Public Property SpEpisodeHeldItems As List(Of SkyHeldItem)

#End Region

#Region "Stored Pokemon"
        Private Sub LoadStoredPokemon()
            StoredPlayerPartner = New List(Of SkyStoredPokemon)
            StoredSpEpisodePokemon = New List(Of SkyStoredPokemon)
            StoredPokemon = New List(Of SkyStoredPokemon)

            For count = 0 To Offsets.StoredPokemonNumber
                Dim pkm As New SkyStoredPokemon(Bits.Range(Offsets.StoredPokemonOffset + count * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength))
                StoredPokemon.Add(pkm)
            Next
        End Sub
        Private Sub SaveStoredPokemon()
            For count = 0 To Offsets.StoredPokemonNumber
                Bits.Range(Offsets.StoredPokemonOffset + count * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength) = StoredPokemon(count).GetStoredPokemonBits
            Next
        End Sub
        Public Property StoredPlayerPartner As List(Of SkyStoredPokemon)
        Public Property StoredSpEpisodePokemon As List(Of SkyStoredPokemon)
        Public Property StoredPokemon As List(Of SkyStoredPokemon)

#End Region

#Region "Active Pokemon"

        Private Sub LoadActivePokemon()
            Dim activePokemon As New ObservableCollection(Of SkyActivePokemon)
            Dim spEpisodeActivePokemon As New ObservableCollection(Of SkyActivePokemon)
            For count As Integer = 0 To Offsets.ActivePokemonNumber - 1
                Dim main = New SkyActivePokemon(Me.Bits.Range(Offsets.ActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength))
                Dim special = New SkyActivePokemon(Me.Bits.Range(Offsets.SpActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength))

                AddHandler main.Modified, AddressOf OnModified
                AddHandler main.PropertyChanged, AddressOf OnModified
                AddHandler special.Modified, AddressOf OnModified
                AddHandler special.PropertyChanged, AddressOf OnModified

                activePokemon.Add(main)
                spEpisodeActivePokemon.Add(special)
            Next

            Me.ActivePokemon = activePokemon
            Me.SpEpisodeActivePokemon = spEpisodeActivePokemon
        End Sub

        Private Sub SaveActivePokemon()
            For count As Integer = 0 To Offsets.ActivePokemonNumber - 1
                Me.Bits.Range(Offsets.ActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength) = ActivePokemon(count).GetActivePokemonBits
                Me.Bits.Range(Offsets.SpActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength) = SpEpisodeActivePokemon(count).GetActivePokemonBits
            Next
        End Sub

        Public Property ActivePokemon As ObservableCollection(Of SkyActivePokemon)
            Get
                Return _activePokemon
            End Get
            Set(value As ObservableCollection(Of SkyActivePokemon))
                If _activePokemon IsNot value Then
                    _activePokemon = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ActivePokemon)))
                End If
            End Set
        End Property
        Dim _activePokemon As ObservableCollection(Of SkyActivePokemon)

        Protected Property Party As IEnumerable Implements IParty.Party
            Get
                Return ActivePokemon
            End Get
            Set(value As IEnumerable)
                ActivePokemon = value
            End Set
        End Property

        Public Property SpEpisodeActivePokemon As ObservableCollection(Of SkyActivePokemon)
            Get
                Return _spEpisodeActivePokemon
            End Get
            Set(value As ObservableCollection(Of SkyActivePokemon))
                If _spEpisodeActivePokemon IsNot value Then
                    _spEpisodeActivePokemon = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SpEpisodeActivePokemon)))
                End If
            End Set
        End Property
        Dim _spEpisodeActivePokemon As ObservableCollection(Of SkyActivePokemon)

#End Region

#Region "Quicksave Pokemon"
        Private Sub LoadQuicksavePokemon()
            _quicksavePokemon = New ObservableCollection(Of SkyQuicksavePokemon)
            For count = 0 To Offsets.QuicksavePokemonNumber - 1
                Dim quick = New SkyQuicksavePokemon(Bits.Range(Offsets.QuicksavePokemonOffset + Offsets.QuicksavePokemonLength * count, Offsets.QuicksavePokemonLength))
                AddHandler quick.Modified, AddressOf OnModified
                AddHandler quick.PropertyChanged, AddressOf OnModified
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

        Public Property QuicksavePokemon As ObservableCollection(Of SkyQuicksavePokemon)
            Get
                Return _quicksavePokemon
            End Get
            Set(value As ObservableCollection(Of SkyQuicksavePokemon))
                If _quicksavePokemon IsNot value Then
                    _quicksavePokemon = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(QuicksavePokemon)))
                End If
            End Set
        End Property
        Dim _quicksavePokemon As ObservableCollection(Of SkyQuicksavePokemon)

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
            Dim rawOriginalPartnerID = OriginalPartnerIsFemale
            If OriginalPartnerIsFemale Then
                rawOriginalPartnerID += 600
            End If
            Bits.Int(&HC0, 0, 16) = rawOriginalPlayerID

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
            Get
                Return _originalPlayerID
            End Get
            Set(value As Integer)
                If Not value = _originalPlayerID Then
                    _originalPlayerID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerID)))
                End If
            End Set
        End Property
        Dim _originalPlayerID As Integer

        ''' <summary>
        ''' Gets or sets the original player gender.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPlayerIsFemale As Boolean
            Get
                Return _originalPlayerIsFemale
            End Get
            Set(value As Boolean)
                If Not value = _originalPlayerIsFemale Then
                    _originalPlayerIsFemale = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPlayerIsFemale)))
                End If
            End Set
        End Property
        Dim _originalPlayerIsFemale As Boolean

        ''' <summary>
        ''' Gets or sets the original partner Pokemon.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPartnerID As Integer
            Get
                Return _originalPartnerID
            End Get
            Set(value As Integer)
                If Not _originalPartnerID = value Then
                    _originalPartnerID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerID)))
                End If
            End Set
        End Property
        Dim _originalPartnerID As Integer

        ''' <summary>
        ''' Gets or sets the original partner gender.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPartnerIsFemale As Boolean
            Get
                Return _originalPartnerIsFemale
            End Get
            Set(value As Boolean)
                If Not _originalPartnerIsFemale = value Then
                    _originalPartnerIsFemale = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerIsFemale)))
                End If
            End Set
        End Property
        Dim _originalPartnerIsFemale As Boolean

        ''' <summary>
        ''' Gets or sets the original player name.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPlayerName As String
            Get
                Return _originalPlayerName
            End Get
            Set(value As String)
                If Not _originalPlayerName = value Then
                    _originalPlayerName = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPlayerName)))
                End If
            End Set
        End Property
        Dim _originalPlayerName As String

        ''' <summary>
        ''' Gets or sets the original partner name.
        ''' Used in-game for special episodes.
        ''' </summary>
        ''' <returns></returns>
        Public Property OriginalPartnerName As String
            Get
                Return _originalPartnerName
            End Get
            Set(value As String)
                If Not _originalPartnerName = value Then
                    _originalPartnerName = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerName)))
                End If
            End Set
        End Property
        Dim _originalPartnerName As String
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
            Get
                Return _windowFrameType
            End Get
            Set(value As Byte)
                If Not _windowFrameType = value Then
                    _windowFrameType = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(WindowFrameType)))
                End If
            End Set
        End Property
        Dim _windowFrameType As Byte

#End Region

#End Region

#Region "Technical Stuff"

        ''' <summary>
        ''' Fixes the save file's checksum to reflect any changes made
        ''' </summary>
        Protected Overrides Sub FixChecksum()
            'Fix the first checksum
            Dim buffer = BitConverter.GetBytes(Checksums.Calculate32BitChecksum(Bits, 4, Offsets.ChecksumEnd))
            For count = 0 To 3
                Bits.Int(count, 0, 8) = buffer(count)
            Next

            'Ensure backup save matches.
            'Not strictly required, as the first one will be loaded if it's valid, but looks nicer.
            CopyToBackup()

            'Quicksave checksum
            buffer = BitConverter.GetBytes(Checksums.Calculate32BitChecksum(Bits, Offsets.QuicksaveChecksumStart, Offsets.QuicksaveChecksumEnd))
            For x As Byte = 0 To 3
                Bits.Int(x + Offsets.QuicksaveStart, 0, 8) = buffer(x)
            Next
        End Sub

        ''' <summary>
        ''' Copies the primary save to the backup save.
        ''' </summary>
        Private Sub CopyToBackup()
            Dim e As Integer = Offsets.BackupSaveStart
            For i As Integer = 4 To e - 1
                Bits.Int(i + e, 0, 8) = Bits.Int(i, 0, 8)
            Next
        End Sub

        ''' <summary>
        ''' Determines whether or not the given file is a SkySave.
        ''' </summary>
        ''' <param name="File">File to determine the type of.</param>
        ''' <returns></returns>
        Public Function IsFileOfType(File As GenericFile) As Task(Of Boolean) Implements IDetectableFileType.IsOfType
            If File.Length > Offsets.ChecksumEnd Then
                Dim buffer = BitConverter.GetBytes(Checksums.Calculate32BitChecksum(File, 4, Offsets.ChecksumEnd))
                Return Task.FromResult(File.RawData(0) = buffer(0) AndAlso File.RawData(1) = buffer(1) AndAlso File.RawData(2) = buffer(2) AndAlso File.RawData(3) = buffer(3))
            Else
                Return Task.FromResult(False)
            End If
        End Function

#End Region

    End Class

End Namespace
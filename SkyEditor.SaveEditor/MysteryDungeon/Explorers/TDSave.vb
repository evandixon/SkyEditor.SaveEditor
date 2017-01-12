Imports System.Collections.Specialized
Imports SkyEditor.Core
Imports SkyEditor.Core.IO

Namespace MysteryDungeon.Explorers
    Public Class TDSave
        Inherits BinaryFile
        Implements IDetectableFileType

        Public Sub New()
            MyBase.New()

            Offsets = New TDOffsets
        End Sub

        Public Overrides Async Function OpenFile(Filename As String, Provider As IIOProvider) As Task
            Await MyBase.OpenFile(Filename, Provider)

            LoadGeneral()
            LoadItems()
            LoadStoredPokemon()
            LoadActivePokemon()
        End Function

        Public Overrides Async Function Save(Destination As String, provider As IIOProvider) As Task
            SaveGeneral()
            SaveItems()
            SaveStoredPokemon()
            SaveActivePokemon()

            Await MyBase.Save(Destination, provider)
        End Function

        Public Overridable ReadOnly Property Offsets As TDOffsets

#Region "Child Classes"
        Public Class TDOffsets
            Public Overridable ReadOnly Property ChecksumEnd As Integer = &HDC7B
            Public Overridable ReadOnly Property BackupSaveStart As Integer = &H10000
            Public Overridable ReadOnly Property QuicksaveStart As Integer = &H2E000
            Public Overridable ReadOnly Property QuicksaveChecksumStart As Integer = &H2E004
            Public Overridable ReadOnly Property QuicksaveChecksumEnd As Integer = &H2E0FF

            Public Overridable ReadOnly Property TeamNameStart As Integer = &H96F7 * 8
            Public Overridable ReadOnly Property TeamNameLength As Integer = 10

            Public Overridable ReadOnly Property HeldItemOffset As Integer = &H8B71 * 8
            Public Overridable ReadOnly Property HeldItemNumber As Integer = 48
            Public Overridable ReadOnly Property HeldItemLength As Integer = 31

            Public Overridable ReadOnly Property StoredPokemonOffset As Integer = &H460 * 8 + 3
            Public Overridable ReadOnly Property StoredPokemonLength As Integer = 388
            Public Overridable ReadOnly Property StoredPokemonNumber As Integer = 550

            Public Overridable ReadOnly Property ActivePokemonOffset As Integer = &H83CB * 8
            Public Overridable ReadOnly Property ActivePokemonLength As Integer = 544
            Public Overridable ReadOnly Property ActivePokemonNumber As Integer = 4
        End Class
#End Region

#Region "Save Interaction"

#Region "General"

        Sub LoadGeneral()
            TeamName = Bits.GetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength)
        End Sub

        Sub SaveGeneral()
            Bits.SetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength, TeamName)
        End Sub

        ''' <summary>
        ''' Gets or sets the save file's Team Name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TeamName As String
#End Region

#Region "Items"
        Public Sub LoadItems()
            _HeldItems = New List(Of TDHeldItem)
            For count As Integer = 0 To Offsets.HeldItemNumber - 1
                Dim i = TDHeldItem.FromHeldItemBits(Me.Bits.Range(Offsets.HeldItemOffset + count * Offsets.HeldItemLength, Offsets.HeldItemLength))
                If i.IsValid Then
                    _heldItems.Add(i)
                Else
                    Exit For
                End If
            Next
        End Sub

        Public Sub SaveItems()
            For count As Integer = 0 To Offsets.HeldItemNumber - 1
                If _heldItems.Count > count Then
                    Me.Bits.Range(Offsets.HeldItemOffset + count * Offsets.HeldItemLength, Offsets.HeldItemLength) = _heldItems(count).GetHeldItemBits
                Else
                    Me.Bits.Range(Offsets.HeldItemOffset + count * Offsets.HeldItemLength, Offsets.HeldItemLength) = New Binary(Offsets.HeldItemLength)
                End If
            Next
        End Sub

        Public Property HeldItems As List(Of TDHeldItem)


#End Region

#Region "Stored Pokemon"
        Private Sub LoadStoredPokemon()
            StoredPlayerPartner = New List(Of TDStoredPokemon)
            StoredPokemon = New List(Of TDStoredPokemon)

            For count = 0 To Offsets.StoredPokemonNumber - 1
                Dim pkm As New TDStoredPokemon(Bits.Range(Offsets.StoredPokemonOffset + count * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength))
                StoredPokemon.Add(pkm)
            Next
        End Sub
        Private Sub SaveStoredPokemon()
            For count = 0 To Offsets.StoredPokemonNumber - 1
                Bits.Range(Offsets.StoredPokemonOffset + count * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength) = StoredPokemon(count).GetStoredPokemonBits
            Next
        End Sub
        Public Property StoredPlayerPartner As List(Of TDStoredPokemon)
        Public Property StoredPokemon As List(Of TDStoredPokemon)

#End Region

#Region "Active Pokemon"

        Private Sub LoadActivePokemon()
            Dim activePokemon As New List(Of TDActivePokemon)
            Dim spEpisodeActivePokemon As New List(Of TDActivePokemon)
            For count As Integer = 0 To Offsets.ActivePokemonNumber - 1
                Dim main = New TDActivePokemon(Me.Bits.Range(Offsets.ActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength))
                activePokemon.Add(main)
            Next

            Me.ActivePokemon = activePokemon
        End Sub

        Private Sub SaveActivePokemon()
            For count As Integer = 0 To Offsets.ActivePokemonNumber - 1
                Me.Bits.Range(Offsets.ActivePokemonOffset + count * Offsets.ActivePokemonLength, Offsets.ActivePokemonLength) = ActivePokemon(count).GetActivePokemonBits
            Next
        End Sub

        Public Property ActivePokemon As List(Of TDActivePokemon)

#End Region

#End Region

#Region "Technical Stuff"
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
        Public Sub CopyToBackup()
            Dim e As Integer = Offsets.BackupSaveStart
            For i As Integer = 4 To e - 1
                Bits.Int(i + e, 0, 8) = Bits.Int(i, 0, 8)
            Next
        End Sub

        'Public Overrides Function DefaultSaveID() As String
        '    Return GameStrings.TDSave
        'End Function

        'Protected Overrides Sub PreSave()
        '    MyBase.PreSave()
        '    For count As Integer = 0 To Math.Ceiling(Bits.Count / 8) - 1
        '        RawData(count) = Bits.Int(count, 0, 8)
        '    Next
        'End Sub
#End Region

        Public Function IsFileOfType(File As GenericFile) As Task(Of Boolean) Implements IDetectableFileType.IsOfType
            If File.Length > Offsets.ChecksumEnd Then
                Dim buffer = BitConverter.GetBytes(Checksums.Calculate32BitChecksum(File, 4, Offsets.ChecksumEnd))
                Return Task.FromResult(File.RawData(0) = buffer(0) AndAlso File.RawData(1) = buffer(1) AndAlso File.RawData(2) = buffer(2) AndAlso File.RawData(3) = buffer(3))
            Else
                Return Task.FromResult(False)
            End If
        End Function

    End Class

End Namespace
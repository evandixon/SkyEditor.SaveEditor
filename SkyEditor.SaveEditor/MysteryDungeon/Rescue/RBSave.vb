Imports SkyEditor.Core.IO

Namespace MysteryDungeon.Rescue
    Public Class RBSave
        Inherits BinaryFile
        Implements IDetectableFileType
        Implements INotifyPropertyChanged
        Implements INotifyModified

        Public Class RBOffsets
            Public Overridable ReadOnly Property BackupSaveStart As Integer = &H6000
            Public Overridable ReadOnly Property ChecksumEnd As Integer = &H57D0
            Public Overridable ReadOnly Property BaseTypeOffset As Integer = &H67 * 8
            Public Overridable ReadOnly Property TeamNameStart As Integer = &H4EC8 * 8
            Public Overridable ReadOnly Property TeamNameLength As Integer = 10
            Public Overridable ReadOnly Property HeldMoneyOffset As Integer = &H4E6C * 8
            Public Overridable ReadOnly Property HeldMoneyLength As Integer = 24
            Public Overridable ReadOnly Property StoredMoneyOffset As Integer = &H4E6F * 8
            Public Overridable ReadOnly Property StoredMoneyLength As Integer = 24
            Public Overridable ReadOnly Property RescuePointsOffset As Integer = &H4ED3 * 8
            Public Overridable ReadOnly Property RescuePointsLength As Integer = 32
            Public Overridable ReadOnly Property HeldItemOffset As Integer = &H4CF0 * 8
            Public Overridable ReadOnly Property HeldItemLength As Integer = 23
            Public Overridable ReadOnly Property HeldItemNumber As Integer = 20
            Public Overridable ReadOnly Property StoredItemOffset As Integer = &H4D2B * 8 - 2
            Public Overridable ReadOnly Property StoredItemNumber As Integer = 239
            Public Overridable ReadOnly Property StoredPokemonOffset As Integer = (&H5B3 * 8 + 3) - (323 * 9)
            Public Overridable ReadOnly Property StoredPokemonLength As Integer = 323
            Public Overridable ReadOnly Property StoredPokemonNumber As Integer = 407 + 6
        End Class

        Public Sub New()
            MyBase.New
            Me.Offsets = New RBOffsets
        End Sub

        Protected Sub New(offsets As RBOffsets)
            MyBase.New
            Me.Offsets = offsets
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event Modified As INotifyModified.ModifiedEventHandler Implements INotifyModified.Modified

        Public Overrides Async Function OpenFile(Filename As String, Provider As IOProvider) As Task
            Await MyBase.OpenFile(Filename, Provider)

            LoadGeneral()
            LoadItems()
            LoadStoredItems()
            LoadStoredPokemon()
        End Function

        Public Overrides Async Function Save(Destination As String, provider As IOProvider) As Task
            SaveGeneral()
            SaveItems()
            SaveStoredItems()
            SaveStoredPokemon()

            Await MyBase.Save(Destination, provider)
        End Function

        Public Overridable ReadOnly Property Offsets As RBOffsets

#Region "Event Handlers"
        Private Sub Me_OnPropertyChanged(sender As Object, e As EventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub
        Private Sub OnModified(sender As Object, e As EventArgs)
            RaiseEvent Modified(sender, e)
        End Sub
#End Region

#Region "Save Interaction"

#Region "General"
        Private Sub LoadGeneral()
            TeamName = Bits.GetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength)
            HeldMoney = Bits.Int(0, Offsets.HeldMoneyOffset, Offsets.HeldMoneyLength)
            StoredMoney = Bits.Int(0, Offsets.StoredMoneyOffset, Offsets.StoredMoneyLength)
            RescuePoints = Bits.Int(0, Offsets.RescuePointsOffset, Offsets.RescuePointsLength)
            BaseType = Bits.Int(0, Offsets.BaseTypeOffset, 8)
        End Sub

        Private Sub SaveGeneral()
            Bits.SetStringPMD(0, Offsets.TeamNameStart, Offsets.TeamNameLength, TeamName)
            Bits.Int(0, Offsets.HeldMoneyOffset, Offsets.HeldMoneyLength) = HeldMoney
            Bits.Int(0, Offsets.StoredMoneyOffset, Offsets.StoredMoneyLength) = StoredMoney
            Bits.Int(0, Offsets.RescuePointsOffset, Offsets.RescuePointsLength) = RescuePoints
            Bits.Int(0, Offsets.BaseTypeOffset, 8) = BaseType
        End Sub

        ''' <summary>
        ''' Gets or sets the save file's Team Name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property TeamName As String
            Get
                Return _teamName
            End Get
            Set(value As String)
                If Not _teamName = value Then
                    _teamName = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TeamName)))
                End If
            End Set
        End Property
        Dim _teamName As String

        Public Property BaseType As Integer
            Get
                Return _baseType
            End Get
            Set(value As Integer)
                If Not _baseType = value Then
                    _baseType = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(BaseType)))
                End If
            End Set
        End Property
        Dim _baseType As Integer

        Public Property RescuePoints As Integer
            Get
                Return _rescuePoints
            End Get
            Set(value As Integer)
                If Not _rescuePoints = value Then
                    _rescuePoints = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(RescuePoints)))
                End If
            End Set
        End Property
        Dim _rescuePoints As Integer

        Public Property HeldMoney As Integer
            Get
                Return _heldMoney
            End Get
            Set(value As Integer)
                If Not _heldMoney = value Then
                    _heldMoney = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(HeldMoney)))
                End If
            End Set
        End Property
        Dim _heldMoney As Integer

        Public Property StoredMoney As Integer
            Get
                Return _storedMoney
            End Get
            Set(value As Integer)
                If Not _storedMoney = value Then
                    _storedMoney = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(StoredMoney)))
                End If
            End Set
        End Property
        Dim _storedMoney As Integer

#End Region

#Region "Held Items"
        Private Sub LoadItems()
            HeldItems = New List(Of RBHeldItem)
            For count = 0 To Offsets.HeldItemNumber
                Dim i As RBHeldItem = RBHeldItem.FromHeldItemBits(Me.Bits.Range(Offsets.HeldItemOffset + count * Offsets.HeldItemLength, Offsets.HeldItemLength))
                If i.IsValid Then
                    HeldItems.Add(i)
                Else
                    Exit For
                End If
            Next
        End Sub

        Private Sub SaveItems()
            For count = 0 To Offsets.HeldItemNumber
                If HeldItems.Count > count Then
                    Me.Bits.Range(Offsets.HeldItemOffset + count * Offsets.HeldItemLength, Offsets.HeldItemLength) = HeldItems(count).GetHeldItemBits
                Else
                    Me.Bits.Range(Offsets.HeldItemOffset + count * Offsets.HeldItemLength, Offsets.HeldItemLength) = New Binary(Offsets.HeldItemLength)
                End If
            Next
        End Sub

        Public Property HeldItems As List(Of RBHeldItem)

#End Region

#Region "Stored Items"
        Private Sub LoadStoredItems()
            StoredItems = New List(Of RBStoredItem)
            Dim block = Bits.Range(Offsets.StoredItemOffset, Offsets.StoredItemNumber * 10)
            For count As Integer = 0 To Offsets.StoredItemNumber - 1
                Dim quantity = block.NextInt(10)
                If quantity > 0 Then
                    StoredItems.Add(New RBStoredItem(count + 1, quantity))
                End If
            Next
        End Sub

        Private Sub SaveStoredItems()
            Dim compiledItems As New Dictionary(Of Integer, Integer) 'Key: Item ID, Value: Quantity

            'Combine the quantities
            For Each item In StoredItems
                If Not compiledItems.ContainsKey(item.ItemID) Then
                    compiledItems.Add(item.ItemID, 0)
                End If
                compiledItems(item.ItemID) = Math.Min(item.Quantity + compiledItems(item.ItemID), 1024) 'Cap at max count
            Next

            'Update the save
            Dim block = New Binary(Offsets.StoredItemNumber * 10)
            For count As Integer = 0 To Offsets.StoredItemNumber - 1
                If compiledItems.ContainsKey(count + 1) Then
                    block.NextInt(10) = compiledItems(count + 1)
                Else
                    block.NextInt(10) = 0
                End If
            Next
            Bits.Range(Offsets.StoredItemOffset, Offsets.StoredItemNumber * 10) = block
        End Sub

        Public Property StoredItems As List(Of RBStoredItem)
#End Region

#Region "Stored Pokemon"
        Private Sub LoadStoredPokemon()
            StoredPokemon = New List(Of RBStoredPokemon)
            For count = 0 To Offsets.StoredPokemonNumber - 1
                Dim p As New RBStoredPokemon(Me.Bits.Range(Offsets.StoredPokemonOffset + count * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength))
                StoredPokemon.Add(p)
            Next
        End Sub

        Private Sub SaveStoredPokemon()
            For count = 0 To Offsets.StoredPokemonNumber - 1
                Me.Bits.Range(Offsets.StoredPokemonOffset + count * Offsets.StoredPokemonLength, Offsets.StoredPokemonLength) = StoredPokemon(count).GetStoredPokemonBits
            Next
        End Sub

        Public Property StoredPokemon As List(Of RBStoredPokemon)

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

            ''Quicksave checksum
            'TODO: Research the quick save
            'buffer = BitConverter.GetBytes(Checksums.Calculate32BitChecksum(Me, Offsets.QuicksaveChecksumStart, Offsets.QuicksaveChecksumEnd))
            'For x As Byte = 0 To 3
            '    RawData(x + Offsets.QuicksaveStart) = buffer(x)
            'Next
        End Sub
        Public Sub CopyToBackup()
            Dim e As Integer = Offsets.BackupSaveStart
            For i As Integer = 4 To e - 1
                Bits.Int(i + e, 0, 8) = Bits.Int(i, 0, 8)
            Next
        End Sub
        'Public Overrides Function DefaultSaveID() As String
        '    Return GameStrings.RBSave
        'End Function
#End Region
        Public Overridable Function IsFileOfType(File As GenericFile) As Task(Of Boolean) Implements IDetectableFileType.IsOfType
            If File.Length > Offsets.ChecksumEnd Then
                Dim buffer = BitConverter.GetBytes(Checksums.Calculate32BitChecksum(File, 4, Offsets.ChecksumEnd))
                Return Task.FromResult(File.RawData(0) = buffer(0) AndAlso File.RawData(1) = buffer(1) AndAlso File.RawData(2) = buffer(2) AndAlso File.RawData(3) = buffer(3))
            Else
                Return Task.FromResult(False)
            End If
        End Function
    End Class

End Namespace
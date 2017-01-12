Imports SkyEditor.Core.IO
Imports SkyEditor.Core.Utilities

Public Class BinaryFile
    'Implements ICreatableFile
    Implements IOpenableFile
    Implements INamed
    Implements IOnDisk
    Implements ISavableAs

    Public Sub New()
        Bits = New Binary(0)
    End Sub

    Public Sub New(rawData As Byte())
        Bits = New Binary(rawData)
    End Sub

    Public Overridable Function OpenFile(Filename As String, Provider As IIOProvider) As Task Implements IOpenableFile.OpenFile
        Me.Filename = Filename
        Me.CurrentIOProvider = Provider
        Using f As New GenericFile(Provider, Filename, True, True)
            Bits = New Binary(0)
            ProcessRawData(f)
        End Using
        Return Task.FromResult(0)
    End Function

    Private Sub ProcessRawData(File As GenericFile)
        For count As Integer = 0 To File.Length - 1
            Bits.AppendByte(File.RawData(count))
        Next
    End Sub

    Public Property Bits As Binary
    Public Property Filename As String Implements IOnDisk.Filename
    Private Property CurrentIOProvider As IIOProvider

    ''' <summary>
    ''' Name of the file.
    ''' </summary>
    ''' <returns></returns>
    Public Property Name As String Implements INamed.Name
        Get
            If _name Is Nothing Then
                Return IO.Path.GetFileName(Filename)
            Else
                Return _name
            End If
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property
    Dim _name As String

    Protected Overridable Sub FixChecksum()

    End Sub

    Public Overridable Async Function Save(Destination As String, provider As IIOProvider) As Task Implements ISavableAs.Save
        FixChecksum()
        Dim tmp(Math.Ceiling(Bits.Count / 8) - 1) As Byte
        Using f As New GenericFile(provider, tmp)
            For count As Integer = 0 To Math.Ceiling(Bits.Count / 8) - 1
                f.RawData(count) = Bits.Int(count, 0, 8)
            Next
            Await f.Save(Destination, provider)
        End Using
        RaiseEvent FileSaved(Me, New EventArgs)
    End Function

    Public Function GetDefaultExtension() As String Implements ISavableAs.GetDefaultExtension
        Return "sav"
    End Function

    Public Event FileSaved As ISavable.FileSavedEventHandler Implements ISavable.FileSaved

    Public Async Function Save(provider As IIOProvider) As Task Implements ISavable.Save
        Await Save(Filename, provider)
    End Function

    Public Overridable Function ToByteArray() As Byte()
        Return Bits.ToByteArray
    End Function

    Public Sub CreateFile(Name As String) ' Implements ICreatableFile.CreateFile
        Me.Name = Name
    End Sub

    Public Function GetSupportedExtensions() As IEnumerable(Of String) Implements ISavableAs.GetSupportedExtensions
        Return {"sav"}
    End Function
End Class

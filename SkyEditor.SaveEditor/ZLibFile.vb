Imports System.IO.Compression
Imports SkyEditor.Core
Imports SkyEditor.Core.IO

''' <remarks>
''' Auto-detection only supports up to 32MB files to avoid hogging all the ram.
''' </remarks>
Public Class ZLibFile
    Implements IOpenableFile
    Implements IDetectableFileType

    Public Async Function OpenFile(filename As String, provider As IIOProvider) As Task Implements IOpenableFile.OpenFile
        Using file As New GenericFile
            Await file.OpenFile(filename, provider)
            Using compressed As New MemoryStream(file.Read)
                compressed.Seek(2, SeekOrigin.Begin)
                Using decompressed As New MemoryStream
                    Using zlib As New DeflateStream(compressed, CompressionMode.Decompress)
                        zlib.CopyTo(decompressed)
                    End Using
                    Me.RawData = decompressed.ToArray
                End Using
            End Using
        End Using

        ''Debug
        'Provider.WriteAllBytes(Filename & "-decompressed", RawData)
    End Function


    Public Async Function IsOfType(File As GenericFile) As Task(Of Boolean) Implements IDetectableFileType.IsOfType
        If File.Length > 2 AndAlso Await File.ReadAsync(0) = &H78 AndAlso {&H1, &H9C, &HDA}.Contains(Await File.ReadAsync(1)) AndAlso File.Length < 32 * 1024 * 1024 Then
            Try
                Using compressed As New MemoryStream(File.Read)
                    compressed.Seek(2, SeekOrigin.Begin)
                    Using decompressed As New MemoryStream
                        Using zlib As New DeflateStream(compressed, CompressionMode.Decompress)
                            zlib.CopyTo(decompressed)
                        End Using
                        Me.RawData = decompressed.ToArray
                    End Using
                End Using
            Catch ex As Exception
                Return False
            End Try
            'If we get here (without returning in the Catch), then this is a zlib file.
            Return True
        Else
            Return False
        End If
    End Function

    Public Property RawData As Byte()

End Class

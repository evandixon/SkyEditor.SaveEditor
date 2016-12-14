Imports Ionic.Zlib
Imports SkyEditor.Core
Imports SkyEditor.Core.IO

''' <remarks>
''' Auto-detection only supports up to 32MB files to avoid hogging all the ram.
''' </remarks>
Public Class ZLibFile
    Implements IOpenableFile
    Implements IDetectableFileType

    Public Function OpenFile(Filename As String, Provider As IOProvider) As Task Implements IOpenableFile.OpenFile
        Using File As New GenericFile(Provider, Filename)
            Using compressed As New MemoryStream(File.RawData)
                compressed.Seek(2, SeekOrigin.Begin)
                Using decompressed As New MemoryStream
                    Using zlib As New DeflateStream(compressed, CompressionMode.Decompress)
                        zlib.CopyTo(decompressed)
                    End Using
                    Me.RawData = decompressed.ToArray
                End Using
            End Using
        End Using

        'Debug
        Provider.WriteAllBytes(Filename & "-decompressed", RawData)

        Return Task.FromResult(0)
    End Function


    Public Function IsOfType(File As GenericFile) As Task(Of Boolean) Implements IDetectableFileType.IsOfType
        If File.Length > 2 AndAlso File.RawData(0) = &H78 AndAlso {&H1, &H9C, &HDA}.Contains(File.RawData(1)) AndAlso File.Length < 32 * 1024 * 1024 Then
            Try
                Using compressed As New MemoryStream(File.RawData)
                    compressed.Seek(2, SeekOrigin.Begin)
                    Using decompressed As New MemoryStream
                        Using zlib As New DeflateStream(compressed, CompressionMode.Decompress)
                            zlib.CopyTo(decompressed)
                        End Using
                        Me.RawData = decompressed.ToArray
                    End Using
                End Using
            Catch ex As Exception
                Return Task.FromResult(False)
            End Try
            'If we get here (without returning in the Catch), then this is a zlib file.
            Return Task.FromResult(True)
        Else
            Return Task.FromResult(False)
        End If
    End Function

    Public Property RawData As Byte()

End Class

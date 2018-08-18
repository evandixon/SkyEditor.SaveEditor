Imports SkyEditor.Core
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels

    'Stub view model for stub view
    Public Class SkyQuicksaveViewModel
        Inherits GenericViewModel(Of SkySave)

        Public ReadOnly Property QuicksavePokemon As List(Of Object)
            Get
                Return New List(Of Object)
            End Get
        End Property
    End Class
End Namespace

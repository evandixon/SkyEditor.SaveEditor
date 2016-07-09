Imports SkyEditor.Core.UI

Namespace ViewModelComponents
    Public Interface IPokemonBox
        Event SelectedPokemonChanged(sender As Object, e As EventArgs)
        Property ItemCollection As IEnumerable
        Property Name As String
        Property SelectedPokemon As FileViewModel
    End Interface

End Namespace

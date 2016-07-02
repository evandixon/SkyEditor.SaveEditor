Imports SkyEditor.Core.UI

Namespace ViewModelComponents
    Public Interface IPokemonBox
        Property ItemCollection As IEnumerable
        Property Name As String
        Property SelectedPokemon As FileViewModel
    End Interface

End Namespace

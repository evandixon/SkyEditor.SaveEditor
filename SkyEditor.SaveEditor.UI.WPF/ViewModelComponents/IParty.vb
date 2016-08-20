Imports SkyEditor.Core.UI

Namespace ViewModelComponents
    Public Interface IParty
        ReadOnly Property Party As IEnumerable(Of FileViewModel)
        Property SelectedPokemon As FileViewModel
    End Interface
End Namespace


Imports SkyEditor.Core.UI

Namespace ViewModelComponents
    Public Interface IPokemonStorage
        ReadOnly Property Storage As IEnumerable(Of IPokemonBox)
        Property SelectedBox As IPokemonBox
        ReadOnly Property AddToPartyCommand As RelayCommand
    End Interface
End Namespace


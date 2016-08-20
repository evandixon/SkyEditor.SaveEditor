Imports SkyEditor.Core.UI

Namespace ViewModelComponents
    Public Interface IParty
        ReadOnly Property Party As IEnumerable(Of FileViewModel)
        Property SelectedPokemon As FileViewModel
        ReadOnly Property StandbyCommand As RelayCommand
        ReadOnly Property PartyName As String
    End Interface
End Namespace


Namespace ViewModelComponents
    Public Interface IPokemonStorage
        ReadOnly Property Storage As IEnumerable(Of IPokemonBox)
        Property SelectedBox As IPokemonBox
    End Interface
End Namespace


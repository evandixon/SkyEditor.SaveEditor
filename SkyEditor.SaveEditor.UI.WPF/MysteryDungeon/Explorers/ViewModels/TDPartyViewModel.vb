Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers
Imports SkyEditor.SaveEditor.UI.WPF.ViewModelComponents

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class TDPartyViewModel
        Inherits GenericViewModel(Of TDSave)
        Implements IParty

        Public ReadOnly Property Party As IEnumerable Implements IParty.Party
            Get
                Return Model.ActivePokemon
            End Get
        End Property
    End Class

End Namespace

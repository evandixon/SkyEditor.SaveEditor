Imports System.ComponentModel
Imports System.Text
Imports SkyEditor.Core.UI

Namespace ViewModelComponents
    Public Class BasicPokemonBox
        Implements IPokemonBox
        Implements INotifyPropertyChanged

        Public Property ItemCollection As IEnumerable Implements IPokemonBox.ItemCollection

        Public Property Name As String Implements IPokemonBox.Name

        Public Property SelectedPokemon As FileViewModel Implements IPokemonBox.SelectedPokemon
            Get
                Return _selectedPokemon
            End Get
            Set(value As FileViewModel)
                If _selectedPokemon IsNot value Then
                    _selectedPokemon = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedPokemon)))
                    RaiseEvent SelectedPokemonChanged(Me, New EventArgs)
                End If
            End Set
        End Property
        Dim _selectedPokemon As FileViewModel

        Public Overrides Function ToString() As String
            Dim out As New StringBuilder
            out.Append(Name)

            If TypeOf ItemCollection Is IList Then
                out.Append(" (")
                out.Append(DirectCast(ItemCollection, IList).Count.ToString)
                out.Append(")")
            End If

            Return out.ToString
        End Function

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event SelectedPokemonChanged(sender As Object, e As EventArgs) Implements IPokemonBox.SelectedPokemonChanged

        Public Sub New(name As String, itemCollection As IEnumerable)
            Me.Name = name
            Me.ItemCollection = itemCollection
        End Sub
    End Class
End Namespace


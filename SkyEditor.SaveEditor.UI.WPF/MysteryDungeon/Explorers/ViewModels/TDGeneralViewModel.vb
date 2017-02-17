Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class TDGeneralViewModel
        Inherits GenericViewModel(Of TDSave)
        Implements INotifyPropertyChanged
        Implements INotifyModified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event Modified As EventHandler Implements INotifyModified.Modified

        Private Sub SkyGeneralViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

        Public Property TeamName As String
            Get
                Return Model.TeamName
            End Get
            Set(value As String)
                If Not Model.TeamName = value Then
                    Model.TeamName = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TeamName)))
                End If
            End Set
        End Property
    End Class
End Namespace
Imports System.ComponentModel
Imports SkyEditor.Core.IO
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class SkyHistoryViewModel
        Inherits GenericViewModel(Of SkySave)
        Implements INotifyPropertyChanged
        Implements INotifyModified

        Public Event Modified As EventHandler Implements INotifyModified.Modified
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub SkyGeneralViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
            RaiseEvent Modified(Me, New EventArgs)
        End Sub

        Public Property OriginalPlayerID As Integer
            Get
                Return Model.OriginalPlayerPokemon.ID
            End Get
            Set(value As Integer)
                If Not value = Model.OriginalPlayerPokemon.ID Then
                    Model.OriginalPlayerPokemon.ID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerID)))
                End If
            End Set
        End Property

        Public Property OriginalPlayerIsFemale As Boolean
            Get
                Return Model.OriginalPlayerPokemon.IsFemale
            End Get
            Set(value As Boolean)
                If Not value = Model.OriginalPlayerPokemon.IsFemale Then
                    Model.OriginalPlayerPokemon.IsFemale = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPlayerIsFemale)))
                End If
            End Set
        End Property

        Public Property OriginalPartnerID As Integer
            Get
                Return Model.OriginalPartnerPokemon.ID
            End Get
            Set(value As Integer)
                If Not Model.OriginalPartnerPokemon.ID = value Then
                    Model.OriginalPartnerPokemon.ID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerID)))
                End If
            End Set
        End Property

        Public Property OriginalPartnerIsFemale As Boolean
            Get
                Return Model.OriginalPartnerPokemon.IsFemale
            End Get
            Set(value As Boolean)
                If Not Model.OriginalPartnerPokemon.IsFemale = value Then
                    Model.OriginalPartnerPokemon.IsFemale = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerIsFemale)))
                End If
            End Set
        End Property

        Public Property OriginalPlayerName As String
            Get
                Return Model.OriginalPlayerName
            End Get
            Set(value As String)
                If Not Model.OriginalPlayerName = value Then
                    Model.OriginalPlayerName = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPlayerName)))
                End If
            End Set
        End Property

        Public Property OriginalPartnerName As String
            Get
                Return Model.OriginalPartnerName
            End Get
            Set(value As String)
                If Not Model.OriginalPartnerName = value Then
                    Model.OriginalPartnerName = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerName)))
                End If
            End Set
        End Property

        Public Overrides ReadOnly Property SortOrder As Integer
            Get
                Return 5
            End Get
        End Property

    End Class
End Namespace


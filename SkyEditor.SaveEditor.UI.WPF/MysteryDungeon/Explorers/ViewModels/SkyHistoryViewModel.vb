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
                Return Model.OriginalPlayerID
            End Get
            Set(value As Integer)
                If Not value = Model.OriginalPlayerID Then
                    Model.OriginalPlayerID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerID)))
                End If
            End Set
        End Property

        Public Property OriginalPlayerIsFemale As Boolean
            Get
                Return Model.OriginalPlayerIsFemale
            End Get
            Set(value As Boolean)
                If Not value = Model.OriginalPlayerIsFemale Then
                    Model.OriginalPlayerIsFemale = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPlayerIsFemale)))
                End If
            End Set
        End Property

        Public Property OriginalPartnerID As Integer
            Get
                Return Model.OriginalPartnerID
            End Get
            Set(value As Integer)
                If Not Model.OriginalPartnerID = value Then
                    Model.OriginalPartnerID = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(OriginalPartnerID)))
                End If
            End Set
        End Property

        Public Property OriginalPartnerIsFemale As Boolean
            Get
                Return Model.OriginalPartnerIsFemale
            End Get
            Set(value As Boolean)
                If Not Model.OriginalPartnerIsFemale = value Then
                    Model.OriginalPartnerIsFemale = value
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


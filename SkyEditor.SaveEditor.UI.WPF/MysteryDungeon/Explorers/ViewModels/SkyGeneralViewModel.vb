Imports System.ComponentModel
Imports SkyEditor.Core.UI
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class SkyGeneralViewModel
        Inherits GenericViewModel(Of SkySave)
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

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

        Public Property HeldMoney As Integer
            Get
                Return Model.HeldMoney
            End Get
            Set(value As Integer)
                If Not Model.HeldMoney = value Then
                    Model.HeldMoney = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(HeldMoney)))
                End If
            End Set
        End Property

        Public Property SpEpisodeHeldMoney As Integer
            Get
                Return Model.SpEpisodeHeldMoney
            End Get
            Set(value As Integer)
                If Not Model.SpEpisodeHeldMoney = value Then
                    Model.SpEpisodeHeldMoney = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SpEpisodeHeldMoney)))
                End If
            End Set
        End Property

        Public Property StoredMoney As Integer
            Get
                Return Model.StoredMoney
            End Get
            Set(value As Integer)
                If Not Model.StoredMoney = value Then
                    Model.StoredMoney = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(StoredMoney)))
                End If
            End Set
        End Property

        Public Property Adventures As Integer
            Get
                Return Model.Adventures
            End Get
            Set(value As Integer)
                If Not Model.Adventures = value Then
                    Model.Adventures = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Adventures)))
                End If
            End Set
        End Property

        Public Property ExplorerRank As Integer
            Get
                Return Model.ExplorerRankPoints
            End Get
            Set(value As Integer)
                If Not Model.ExplorerRankPoints = value Then
                    Model.ExplorerRankPoints = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ExplorerRank)))
                End If
            End Set
        End Property

    End Class

End Namespace

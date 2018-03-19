Imports System.Reflection
Imports SkyEditor.SaveEditor.MysteryDungeon
Imports SkyEditor.UI.WPF

Namespace Tabs
    Public Class GIGDGeneralTab
        Inherits ObjectControl

        Public Overrides Sub RefreshDisplay()
            With GetEditingObject(Of GtiGameData)()
                numGeneral_HeldMoney.Value = .HeldMoney
                numCompanionHeldMoney.Value = .CompanionHeldMoney
            End With
        End Sub

        Public Overrides Sub UpdateObject()
            With GetEditingObject(Of GtiGameData)()
                .HeldMoney = numGeneral_HeldMoney.Value
                .CompanionHeldMoney = numCompanionHeldMoney.Value
            End With
        End Sub

        Private Sub GeneralTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            Me.Header = My.Resources.Language.General
        End Sub

        Private Sub OnModified(sender As Object, e As EventArgs) Handles numGeneral_HeldMoney.ValueChanged, numCompanionHeldMoney.ValueChanged
            IsModified = True
        End Sub

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return {GetType(GtiGameData).GetTypeInfo}
        End Function

    End Class
End Namespace
﻿Imports System.Reflection
Imports SkyEditor.SaveEditor.Saves
Imports SkyEditor.UI.WPF

Namespace Tabs
    Public Class GIGDGeneralTab
        Inherits ObjectControl

        Public Overrides Sub RefreshDisplay()
            With GetEditingObject(Of GTIGameData)()
                numGeneral_HeldMoney.Value = .HeldMoney
                numCompanionHeldMoney.Value = .CompanionHeldMoney
            End With
        End Sub

        Public Overrides Sub UpdateObject()
            With GetEditingObject(Of GTIGameData)()
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
            Return {GetType(GTIGameData).GetTypeInfo}
        End Function

    End Class
End Namespace
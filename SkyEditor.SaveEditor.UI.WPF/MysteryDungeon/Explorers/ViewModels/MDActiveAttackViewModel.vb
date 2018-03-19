Imports System.Reflection
Imports SkyEditor.Core
Imports SkyEditor.SaveEditor.MysteryDungeon.Explorers

Namespace MysteryDungeon.Explorers.ViewModels
    Public Class MDActiveAttackViewModel
        Inherits MDAttackViewModel

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(model As IExplorersActiveAttack, appViewModel As ApplicationViewModel)
            MyBase.New(model, appViewModel)
        End Sub

        Public Shadows Property Model As IExplorersActiveAttack
            Get
                Return MyBase.Model
            End Get
            Set(value As IExplorersActiveAttack)
                MyBase.Model = value
            End Set
        End Property

        Public Overrides Function GetSupportedTypes() As IEnumerable(Of TypeInfo)
            Return MyBase.GetSupportedTypes().Concat({GetType(IExplorersActiveAttack).GetTypeInfo})
        End Function

        Public Overrides Function SupportsObject(Obj As Object) As Boolean
            Return MyBase.SupportsObject(Obj) AndAlso TypeOf Obj Is IExplorersActiveAttack
        End Function

        Public Property IsSealed As Boolean
            Get
                Return Model.IsSealed
            End Get
            Set(value As Boolean)
                If Not Model.IsSealed = value Then
                    Model.IsSealed = value
                    RaisePropertyChanged(NameOf(IsSealed))
                End If
            End Set
        End Property

        Public Property PP As Integer
            Get
                Return Model.PP
            End Get
            Set(value As Integer)
                If Not Model.PP = value Then
                    Model.PP = value
                    RaisePropertyChanged(NameOf(PP))
                End If
            End Set
        End Property
    End Class
End Namespace


Imports Bilancio.Models
Imports System.ComponentModel.DataAnnotations

Namespace ViewModels

    Public Enum HeadFood As Integer
        BODY = 0
        HEAD_WIDTH_FOOD = 1
        HEAD_WIDTHOUT_FOOD = 2
        FOOD = 3
    End Enum

    Public Class CreditDebitAccount

        <Display(Name:="Codice")>
        Public Property Code As String

        <Display(Name:="Descrizione")>
        Public Property Name As String

        Public Property creditDebit As CreditDebit

        Public Property headFoot As HeadFood

    End Class

End Namespace
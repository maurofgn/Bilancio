Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class Avis

        Public Property ID As Integer

        <Required>
        <StringLength(60, MinimumLength:=3, ErrorMessage:="La lunghezza massima é 60 caratteri.")>
        <Display(Name:="Descrizione")>
        Public Property Name As String

        '<Timestamp>
        'Public Property RowVersion As Byte()

        Public Property Active As Boolean = True

        <DataType(DataType.Date)>
        <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        <Display(Name:="Data Creazione")>
        Public Property dateCreated As DateTime = DateTime.Now

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        'Public Property lastUpdate As DateTime = DateTime.Now

        Public Property Address As String
        <RegularExpression("^[a-zA-Z''-'\s]{1,50}$", ErrorMessage:="Numeri e caratteri speciali non sono permessi.")>
        Public Property City As String

        <StringLength(5, MinimumLength:=3, ErrorMessage:="La lunghezza massima é 5 caratteri.")>
        Public Property PostalCode As String
        '<StringLength(4, MinimumLength:=2, ErrorMessage:="La lunghezza massima é 4 caratteri.")>
        Public Property Region As String
        <EmailAddress>
        Public Property Email As String
        Public Property Phone As String
        Public Property ContactName As String

    End Class

End Namespace

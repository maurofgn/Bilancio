Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class DocumentRow

        <Key>
        Public Property ID As Integer

        <Display(Name:="Nr. Riga")>
        Public Property rowNr As Integer = 0

        <Display(Name:="Dare")>
        Public Property debit As Boolean

        <DataType(DataType.Currency)>
        <Display(Name:="Importo")>
        <Required>
        Public Property amount As Decimal = 0

        <Display(Name:="Note")>
        Public Property note As String

        '<Required>
        '<StringLength(20, MinimumLength:=1, ErrorMessage:="La lunghezza massima é 20 caratteri.")>
        '<Display(Name:="Codice")>
        'Public Property Code As String

        '<Required>
        '<StringLength(60, MinimumLength:=3, ErrorMessage:="La lunghezza massima é 60 caratteri.")>
        '<Display(Name:="Descrizione")>
        'Public Property Name As String

        '<Timestamp>
        'Public Property RowVersion As Byte()

        'Public Property Active As Boolean = True

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Data Creazione")>
        'Public Property dateCreated As DateTime = DateTime.Now

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        'Public Property lastUpdate As DateTime = DateTime.Now

        <ForeignKey("Document")>
        <Required>
        Public Overridable Property Document_ID As Integer

        Public Overridable Property Document As Document

        <ForeignKey("AccountChart")>
        <Required>
        Public Overridable Property AccountChart_ID As Integer

        Public Overridable Property AccountChart As AccountChart

    End Class

End Namespace


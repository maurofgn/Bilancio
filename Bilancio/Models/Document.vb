Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class Document

        Public Property ID As Integer

        <Required>
        <StringLength(20, MinimumLength:=1, ErrorMessage:="La lunghezza massima é 20 caratteri.")>
        <Display(Name:="Codice")>
        Public Property Code As String

        <Required>
        <StringLength(60, MinimumLength:=3, ErrorMessage:="La lunghezza massima é 60 caratteri.")>
        <Display(Name:="Descrizione")>
        Public Property Name As String

        '<Timestamp>
        'Public Property RowVersion As Byte()

        Public Property Active As Boolean = True

        <DataType(DataType.Date)>
        <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        <Display(Name:="Data Registrazione")>
        Public Property dateReg As DateTime = DateTime.Now

        <DataType(DataType.Date)>
        <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        <Display(Name:="Data Documento")>
        Public Property dateDoc As DateTime = DateTime.Now

        <StringLength(20, ErrorMessage:="La lunghezza massima é 60 caratteri.")>
        <Display(Name:="Nr. Doc.")>
        Public Property DocNr As String

        Public Property Note As String

        <DataType(DataType.Currency)>
        <Display(Name:="Tot. Doc.")>
        Public Property Amount As Decimal


        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Data Creazione")>
        'Public Property dateCreated As DateTime = DateTime.Now

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        'Public Property lastUpdate As DateTime = DateTime.Now
        <Required>
        Public Overridable Property DocumentType As DocumentType

        Public Overridable Property DocumentRows As ICollection(Of DocumentRow)



    End Class

End Namespace


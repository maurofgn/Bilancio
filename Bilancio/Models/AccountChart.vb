Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema


Namespace Models
    Public Class AccountChart

        Public Property ID As Integer

        <Required>
        <StringLength(20, MinimumLength:=1, ErrorMessage:="La lunghezza massima é 20 caratteri.")>
        <Display(Name:="Codice")>
        <Index("codeIndex", IsUnique:=True)>
        Public Property Code As String

        <Required>
        <StringLength(60, MinimumLength:=3, ErrorMessage:="La lunghezza massima é 60 caratteri.")>
        <Display(Name:="Descrizione")>
        Public Property Name As String

        '<Timestamp>
        'Public Property RowVersion As Byte()

        Public Property Active As Boolean = True

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Data Creazione")>
        'Public Property dateCreated As DateTime = DateTime.Now

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        'Public Property lastUpdate As DateTime = DateTime.Now

        <ForeignKey("AccountCee")>
        Public Property AccountCeeID As Integer?

        Public Overridable Property AccountCee As AccountCee
        Public Overridable Property DocumentRows As ICollection(Of DocumentRow)



    End Class

End Namespace
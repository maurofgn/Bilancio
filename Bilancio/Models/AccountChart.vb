Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema


Namespace Models
    Public Class AccountChart

        Public Property ID As Integer

        <Required>
        <StringLength(20, MinimumLength:=1, ErrorMessage:="{0} deve essere di almeno {2} caratteri e massimo {1}.")>
        <Display(Name:="Codice")>
        <Index("codeIndex", IsUnique:=True)>
        Public Property Code As String

        <Required>
        <StringLength(60, MinimumLength:=3, ErrorMessage:="{0} deve essere di almeno {2} carattere e massimo {1}.")>
        <Display(Name:="Descrizione")>
        Public Property Name As String

        '<Timestamp>
        'Public Property RowVersion As Byte()

        '<Required(ErrorMessage:="Debit t/f")>
        <Display(Name:="Conto dare")>
        <ScaffoldColumn(False)>
        Public Property Debit As Boolean

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

        Public Overrides Function ToString() As String
            Return Name
        End Function


        Public Function getAncestorDebit()
            Return AccountCee.getAncestorDebit()
        End Function

    End Class

End Namespace
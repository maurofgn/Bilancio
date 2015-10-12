Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema


Namespace Models

    Public Enum NodeType
        ROOT
        PATRIMONIALE
        ECONOMICO
        ATTIVO
        PASSIVO
        COSTI
        RICAVI
        ALTRO
    End Enum


    Public Class AccountCee

        Public Property ID As Integer

        <Required>
        <StringLength(20, MinimumLength:=1, ErrorMessage:="almeno 1 carattere e massimo 20.")>
        <Display(Name:="Codice")>
        Public Property Code As String

        <Required>
        <StringLength(60, MinimumLength:=3, ErrorMessage:="almeno 3 caratteri e massimo 60.")>
        <Display(Name:="Descrizione")>
        Public Property Name As String

        '<ConcurrencyCheck>
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

        <Required>
        Public Property SeqNo As Integer = 0

        '<Required(ErrorMessage:="Summary t/f")>
        Public Property Summary As Boolean = True

        '<Required(ErrorMessage:="Total t/f")>
        Public Property Total As Boolean = False

        '<Required(ErrorMessage:="Debit t/f")>
        Public Property Debit As Boolean = True

        Public Property NodeType As NodeType = NodeType.ALTRO

        'Public Property Parent_ID As Integer

        '<Display(Name:="Padre")>
        Public Overridable Property Parent As AccountCee

        '<Display(Name:="Figli")>
        Public Overridable Property Sons As ICollection(Of AccountCee) = New HashSet(Of AccountCee)
        Public Overridable Property AccountCharts As ICollection(Of AccountChart) = New HashSet(Of AccountChart)

        Public Overrides Function ToString() As String
            Return Name.ToString()
        End Function

    End Class

End Namespace

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
        <Display(Name:="Segno")>
        <ScaffoldColumn(False)>
        Public Property Debit As DareAvere

        Public Property Active As Boolean = True

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Data Creazione")>
        'Public Property dateCreated As DateTime = DateTime.Now

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        'Public Property lastUpdate As DateTime = DateTime.Now

        <Display(Name:="Riferimento CEE")>
        <ForeignKey("AccountCee")>
        Public Property AccountCeeID As Integer?

        Public Overridable Property AccountCee As AccountCee
        Public Overridable Property DocumentRows As ICollection(Of DocumentRow)

        Private Property _creditDebit As Bilancio.CreditDebit


        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public ReadOnly Property creditDebit As CreditDebit
            Get
                Return _creditDebit
            End Get
        End Property

        'calcola e ritorna i totali del conto dalle righe documento che hanno l'anno compatibile
        Function calculateCreditDebit(year As Integer) As CreditDebit

            _creditDebit = New CreditDebit(year, Debit)

            DocumentRows.Where(Function(a) a.Document.dateReg.Year = year Or a.Document.dateReg.Year = year - 1).ToList().ForEach(Sub(a) _creditDebit.addRow(a))

            Return _creditDebit

        End Function


    End Class

End Namespace
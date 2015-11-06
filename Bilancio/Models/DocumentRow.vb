Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    Public Class DocumentRow
        Implements IComparable(Of DocumentRow)

        <Display(Name:="ID di Riga")>
        Public Property ID As Integer

        <Display(Name:="Nr. Riga")>
        Public Property rowNr As Integer = 0

        <Display(Name:="Segno")>
        Public Property debit As DareAvere

        <DataType(DataType.Currency)>
        <Display(Name:="Importo")>
        <Required>
        Public Property amount As Decimal = 0

        <Display(Name:="Note")>
        Public Property note As String

        Public Function amountSigned() As Decimal
            If (AccountChart_ID > 0 AndAlso AccountChart.Debit <> Me.debit) Then    'dare avere del conto è opposto del dare avere di riga ==> importo negativo
                Return -amount
            Else
                Return amount
            End If

        End Function

        'Public ReadOnly Property amountSigned() As Decimal
        '    Get
        '        If (AccountChart_ID > 0 AndAlso AccountChart.Debit <> Me.debit) Then    'dare avere del conto è opposto del dare avere di riga ==> importo negativo
        '            Return -amount
        '        Else
        '            Return amount
        '        End If

        '    End Get

        'End Property

        '<Required>
        '<StringLength(20, MinimumLength:=1, ErrorMessage:="La lunghezza massima é 20 caratteri.")>
        '<Display(Name:="Codice")>
        'Public Property Code As String

        '<Required>
        '<StringLength(60, MinimumLength:=3, ErrorMessage:="La lunghezza massima é 60 caratteri.")>
        '<Display(Name:="Descrizione")>
        'Public Property Name As String

        '<ScaffoldColumn(False)>
        '<Timestamp>
        'Public Property RowVersion As Byte()

        <ScaffoldColumn(False)>
        <DataType(DataType.Date)>
        <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        <Display(Name:="Data Creazione")>
        Public Property dateCreated As DateTime = DateTime.Now

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

        Public Function CompareTo(other As DocumentRow) As Integer Implements IComparable(Of Bilancio.Models.DocumentRow).CompareTo

            If (Document_ID > other.Document_ID) Then
                Return 1
            ElseIf (Document_ID < other.Document_ID) Then
                Return -1
            End If

            If (rowNr > other.rowNr) Then
                Return 1
            ElseIf (rowNr < other.rowNr) Then
                Return -1
            End If

            If (ID > other.ID) Then
                Return 1
            ElseIf (ID < other.ID) Then
                Return -1
            End If

            If (AccountChart_ID > other.AccountChart_ID) Then
                Return 1
            ElseIf (AccountChart_ID < other.AccountChart_ID) Then
                Return -1
            End If


            Return 0
        End Function

        Function dare() As Decimal
            Return IIf(debit = DareAvere.Dare, amount, Decimal.Zero)
        End Function

        Function avere() As Decimal
            Return IIf(debit = DareAvere.Avere, amount, Decimal.Zero)
        End Function

    End Class

End Namespace


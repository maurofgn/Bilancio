Imports Bilancio.Models
Imports System.ComponentModel.DataAnnotations

Public Class CreditDebit
    Public Property year As Integer         'anno di riferimento

    Public Property dareYear As Decimal = Decimal.Zero
    Public Property avereYear As Decimal = Decimal.Zero
    Public Property dareYearPrev As Decimal = Decimal.Zero
    Public Property avereYearPrev As Decimal = Decimal.Zero
    Public Property naturalSign As DareAvere        ''segno del conto. In base all'appartenenza a attivo (D), passivo (A), costo (D), ricavo (A)

    Public Sub New(year As Integer, naturalSign As DareAvere)
        Me.year = year
        Me.naturalSign = naturalSign
    End Sub

    Public Sub addRow(row As DocumentRow)

        If (IsNothing(row)) Then
            Return
        End If

        Dim yy = row.Document.dateReg.Year
        If (yy = year) Then
            dareYear += row.dare()
            avereYear += row.avere()
        ElseIf (yy = year - 1) Then
            dareYearPrev += row.dare()
            avereYearPrev += row.avere()
        End If

    End Sub

    Public Sub add(cd As CreditDebit)
        If (Not IsNothing(cd) AndAlso cd.year = year) Then
            dareYear += cd.dareYear
            avereYear += cd.avereYear
            dareYearPrev += cd.dareYearPrev
            avereYearPrev += cd.avereYearPrev
        End If
    End Sub

    <Display(Name:="Saldo anno")>
    Public ReadOnly Property balanceYear As Decimal
        Get
            Return (dareYear - avereYear) * naturalSign
        End Get
    End Property

    <Display(Name:="Saldo anno Prec.")>
    Public ReadOnly Property balanceYearPrev As Decimal
        Get
            Return (dareYearPrev - avereYearPrev) * naturalSign
        End Get
    End Property

    <Display(Name:="Differenza")>
    Public ReadOnly Property delta As Decimal
        Get
            Return ((dareYear - avereYear) - (dareYearPrev - avereYearPrev)) * naturalSign
        End Get
    End Property

    Public Function isEmpty() As Boolean
        Return dareYear = 0 AndAlso avereYear = 0 AndAlso dareYearPrev = 0 AndAlso avereYearPrev = 0
    End Function

    Public Overrides Function toString() As String
        Return " saldo " & year & ": " & balanceYear & IIf(balanceYear <> 0, " " & (year - 1) & " saldo " & (year - 1) & ": " & balanceYearPrev, "")
    End Function

End Class

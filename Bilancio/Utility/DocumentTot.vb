Imports Bilancio.Models

Public Class DocumentTot

    Private _amountDebit As Decimal = Decimal.Zero
    Private _amountCredit As Decimal = Decimal.Zero
    Private doc As Document
    Private _stateDoc As StateDoc

    Public Enum StateDoc As Integer
        Ok = 0
        TotRowNotEqTotHead = 1
        Unbalanced = TotRowNotEqTotHead << 1
        InvalidTotal = Unbalanced << 1
        NotValidDataReg = InvalidTotal << 1
        DataRegBeforeDoc = NotValidDataReg << 1
    End Enum

    Sub New(ByVal doc As Document)
        If (Not IsNothing(doc)) Then
            Me.doc = doc
            For Each row In doc.documentRows
                If (row.debit) Then
                    _amountDebit += row.amount
                ElseIf (Not row.debit) Then
                    _amountCredit += row.amount
                End If
            Next

            _stateDoc = StateDoc.Ok _
                Or (IIf(total().CompareTo(doc.amount), 0, StateDoc.TotRowNotEqTotHead)) _
                Or (IIf(balanced(), 0, StateDoc.Unbalanced)) _
                Or (IIf(doc.amount.CompareTo(Decimal.Zero) > 0, 0, StateDoc.InvalidTotal)) _
                Or (IIf(Not IsNothing(doc.dateReg), 0, StateDoc.NotValidDataReg)) _
                Or (IIf(IsNothing(doc.dateDoc) OrElse doc.dateReg.CompareTo(doc.dateDoc) < 0, 0, StateDoc.DataRegBeforeDoc))

        End If

    End Sub

    Public ReadOnly Property amountDebit() As Decimal
        Get
            Return _amountDebit
        End Get
    End Property

    Public ReadOnly Property amountCredit() As Decimal
        Get
            Return _amountCredit
        End Get
    End Property

    'ritorna true se il dare è uguale all'avere
    Public ReadOnly Property balanced() As Boolean
        Get
            Return _amountDebit.CompareTo(_amountCredit) = 0
        End Get
    End Property

    'return il totale documento, calcolato come il valore massimo tra il dare ed avere 
    Public ReadOnly Property total() As Decimal
        Get
            Return IIf(_amountDebit.CompareTo(_amountCredit) >= 0, _amountDebit, _amountCredit)
        End Get
    End Property

    Public ReadOnly Property state() As StateDoc
        Get
            Return _stateDoc
        End Get
    End Property

    'Ritorna il messaggio con gli errori sul documento
    Public ReadOnly Property stateMsg() As String
        Get
            If (_stateDoc = StateDoc.Ok) Then
                Return ""
            End If

            Return String.Join(", ", getErrors().Values.ToArray)

        End Get
    End Property

    'ritorna gli errori del documento sotto forma di Dictionary(Of String, String)
    Public ReadOnly Property getErrors() As Dictionary(Of String, String)
        Get
            Dim retValue As New Dictionary(Of String, String)

            If (_stateDoc = StateDoc.Ok) Then
                Return retValue
            End If

            Dim states() As Integer = CType([Enum].GetValues(GetType(StateDoc)), Integer())

            For Each value In states

                Select Case value And _stateDoc
                    Case StateDoc.TotRowNotEqTotHead
                        retValue.Add(value.ToString(), "Totale da righe diverso dal totale di testata")
                    Case StateDoc.Unbalanced
                        retValue.Add(value.ToString(), "Documento non quadrato")
                    Case StateDoc.InvalidTotal
                        retValue.Add(value.ToString(), "Totale documento minore o uguale a zero")
                    Case StateDoc.NotValidDataReg
                        retValue.Add(value.ToString(), "Data Registrazione non valida")
                    Case StateDoc.DataRegBeforeDoc
                        retValue.Add(value.ToString(), "Data Registrazione inferiore della data documento")
                End Select

            Next

            Return retValue
        End Get
    End Property






End Class
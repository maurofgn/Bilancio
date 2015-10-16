Imports Bilancio.Models
Imports Bilancio.DAL
Module Utility

    Public Const LINES_PER_PAGE = 10

    'Carica nuovi documenti (dati di test)
    'Public Sub mockDocument(Optional loopNr As Integer = 1)

    '    Dim db As New BilancioContext

    '    Dim docTypes As List(Of DocumentType) = db.DocumentTypes.ToList()

    '    If (docTypes.Count = 0) Then
    '        Return
    '    End If

    '    Dim attivo As AccountChart() = AccountCee.getNodeFromType(NodeType.ATTIVO).getAllAccountChart().ToArray()      ''dare
    '    Dim passivo As AccountChart() = AccountCee.getNodeFromType(NodeType.PASSIVO).getAllAccountChart().ToArray()    ''avere
    '    Dim costi As AccountChart() = AccountCee.getNodeFromType(NodeType.COSTI).getAllAccountChart().ToArray()        ''dare
    '    Dim ricavi As AccountChart() = AccountCee.getNodeFromType(NodeType.RICAVI).getAllAccountChart().ToArray()      ''avere

    '    Dim attivoIndex As Integer = 0
    '    Dim passivoIndex As Integer = 0
    '    Dim costiIndex As Integer = 0
    '    Dim ricaviIndex As Integer = 0
    '    Dim docNo As Integer = 0

    '    Dim oneGroup = Sub(cicleNr As Integer, dateReg As Date, delta As Decimal)

    '                       Dim year As Integer = dateReg.Year
    '                       Dim index As Integer = 0
    '                       docTypes.ForEach(Sub(docType)

    '                                            Dim even As Boolean = index Mod 2 = 0
    '                                            index += 1
    '                                            docNo += 1

    '                                            Dim doc As Document = New Document With {.documentRows = New List(Of DocumentRow), .documentType = docType, .dateReg = dateReg, .dateDoc = dateReg, .docNr = docNo, .amount = IIf(even, 30 + delta, 50 + delta), .note = "Nota su doc " & docNo}

    '                                            If (even) Then
    '                                                db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 1, .debit = even, .amount = 30 + delta, .AccountChart = attivo(attivoIndex Mod attivo.Length), .note = "attivo dare"})
    '                                                db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 2, .debit = Not even, .amount = 20 + delta, .AccountChart = ricavi(ricaviIndex Mod ricavi.Length), .note = "ricavo avere"})
    '                                                db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 3, .debit = Not even, .amount = 10, .AccountChart = passivo(passivoIndex Mod passivo.Length), .note = "passivo avere"})
    '                                                ricaviIndex += 1
    '                                                attivoIndex += 1
    '                                                passivoIndex += 1
    '                                            Else
    '                                                db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 1, .debit = Not even, .amount = 50 + delta, .AccountChart = passivo(passivoIndex Mod attivo.Length), .note = "passivo avere"})
    '                                                db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 2, .debit = even, .amount = 45 + delta, .AccountChart = costi(costiIndex Mod ricavi.Length), .note = "costo dare"})
    '                                                db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 3, .debit = even, .amount = 5, .AccountChart = attivo(attivoIndex Mod passivo.Length), .note = "attivo dare"})
    '                                                costiIndex += 1
    '                                                attivoIndex += 1
    '                                                passivoIndex += 1
    '                                            End If

    '                                            db.Documents.Add(doc)
    '                                            Dim changes As Integer = db.SaveChanges()

    '                                        End Sub)

    '                   End Sub

    '    Dim dateCurrent = Date.Now
    '    Dim datePrev = Date.Now.AddYears(-1)
    '    For i As Integer = 0 To loopNr - 1
    '        oneGroup(i, dateCurrent, 0)     'anno corrente
    '        oneGroup(i, datePrev, -2)       'anno precedente
    '    Next

    'End Sub


End Module

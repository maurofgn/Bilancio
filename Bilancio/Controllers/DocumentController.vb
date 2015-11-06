Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL
Imports PagedList
Imports System.Data.Entity.Validation
Imports System.Data.Entity.Infrastructure

Public Class DocumentController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /Document/

    'Function Index() As ActionResult
    '    Return View(db.Documents.ToList())
    'End Function

    Function Index(ByVal sortOrder As String, currentFilter As String, searchYearReg As Date?, page As Integer?) As ActionResult
        ViewBag.CurrentSort = sortOrder
        ViewBag.DateRegSortParm = If(String.IsNullOrEmpty(sortOrder), "dateReg", String.Empty)
        ViewBag.DateDocSortParm = If(sortOrder = "dateDoc", "dateDoc_desc", "dateDoc")
        ViewBag.NrDocSortParm = If(sortOrder = "nrDoc", "nrDoc_desc", "nrDoc")
        ViewBag.DocTypeSortParm = If(sortOrder = "DocType", "DocTypec_desc", "DocType")

        'If Not searchDateReg Is Nothing Then
        '    page = 1
        'Else
        '    searchDateReg = currentFilter
        'End If

        ViewBag.CurrentFilter = searchYearReg

        Dim query = From s In db.Documents Select s

        'If Not String.IsNullOrEmpty(searchYearReg) Then
        '    query = query.Where(Function(s) s.dateReg.CompareTo(searchYearReg))
        'End If

        Select Case sortOrder
            Case "dateReg"
                query = query.OrderBy(Function(s) s.dateReg)
            Case "dateDoc"
                query = query.OrderBy(Function(s) s.dateDoc)
            Case "dateDoc_desc"
                query = query.OrderByDescending(Function(s) s.dateDoc)
            Case "nrDoc"
                query = query.OrderBy(Function(s) s.docNr)
            Case "nrDoc_desc"
                query = query.OrderByDescending(Function(s) s.docNr)
            Case "DocType"
                query = query.OrderBy(Function(s) s.documentType.Name)
            Case "DocTypec_desc"
                query = query.OrderByDescending(Function(s) s.documentType.Name)
            Case Else
                query = query.OrderByDescending(Function(s) s.dateReg).ThenBy(Function(s) s.dateDoc).ThenBy(Function(s) s.docNr)
        End Select

        Dim pageNumber As Integer = If(page, 1)
        Dim pageSize As Integer = LINES_PER_PAGE

        Return View(query.ToPagedList(pageNumber, pageSize))

    End Function

    '
    ' GET: /Document/Details/5

    Function Details(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim document As Document = db.Documents.Find(id)
        If IsNothing(document) Then
            Return HttpNotFound()
        End If
        Return View(document)
    End Function

    '
    ' GET: /Document/Create

    Function Create() As ActionResult

        populateListView()

        Return View()

    End Function


    Private Function getMesErr() As String

        Return String.Join(",", ModelState.Values _
                                        .Where(Function(E) E.Errors.Count > 0) _
                                        .SelectMany(Function(E) E.Errors) _
                                        .[Select](Function(E) E.ErrorMessage).ToArray())
    End Function

    <HttpPost()>
    Function Create(document As Document) As JsonResult

        If (Request.IsAjaxRequest()) Then
            Trace.WriteLine("IsAjaxRequest")
        End If

        'Elimina l'errore per l'obbligotorietà dell'id di riga, debit e rowNr, questi campi vengono generati automaticamente. Sarebbe stato meglio non generare l'errore, ma avendo la key di riga un id ... non so come si fa
        Dim re As Regex = New Regex("documentRows\[\d+\].[ID|debit|rowNr]")

        For Each k In ModelState.Keys
            If (ModelState.Item(k).Errors.Count > 0 AndAlso re.Match(k).Success) Then
                ModelState.Item(k).Errors.Clear()
            End If
        Next

        If Not ModelState.IsValid Then
            Return Json(New With {.Success = 0, .ID = document.ID, .ex = getMesErr()})
        End If

        Dim maxRow As Integer = 0
        ''x tutte le righe:
        '' 1) se il dare/avere non è definito, va definito in base al conto
        '' 2) se l'importo è negativo, va forzato positivo ed invertito il dare/avere
        document.documentRows.ToList().ForEach(Sub(row)
                                                   If (maxRow < row.rowNr And row.rowNr > 0) Then
                                                       maxRow = row.rowNr
                                                   End If
                                                   If (IsNothing(row.debit) Or row.debit = 0) Then
                                                       row.debit = db.AccountCharts.Find(row.AccountChart_ID).Debit
                                                   End If

                                                   If (row.amount.CompareTo(Decimal.Zero) < 0) Then
                                                       row.amount = Math.Abs(row.amount)
                                                       row.debit = negate(row.debit)
                                                   End If
                                               End Sub)

        document.documentRows.Where(Function(a) a.rowNr <= 0).ToList().ForEach(Sub(row)
                                                                                   maxRow += 1
                                                                                   row.rowNr = maxRow
                                                                               End Sub)
        Dim totInfo = document.totalInfo()

        Dim errors As Dictionary(Of String, String) = totInfo.getErrors
        For Each key In errors.Keys
            ModelState.AddModelError(key, errors.Item(key))
        Next

        If Not ModelState.IsValid Then
            Return Json(New With {.Success = 0, .ID = document.ID, .ex = getMesErr()})
        End If

        Try
            ''is document has ID then we can undertand we have existing document information so we need to perform update operation

            If (document.ID > 0) Then
                ''update
                'Dim previousDoc = db.Documents.Find(document.ID)
                '                db.Documents.Attach(document)
                'db.Entry(document).State = EntityState.Modified

                ''lista degli id delle righe del doc originale
                Dim prevIdRows = (From a In db.DocumentRows Where a.Document_ID = document.ID Select a.ID).ToList()

                ''lista degli id delle righe del doc dopo il salvataggio, 0 per quelle nuove
                'Dim nextIdRows = document.documentRows.Select(Function(a) a.ID).ToList()
                'document.note = "oldRowId[" + String.Join(",", prevIdRows.ToArray) + "] newRowId[" + String.Join(",", nextIdRows.ToArray) + "]"
                ''
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                document.documentRows.ToList().ForEach(Sub(rowNew)
                                                           If (rowNew.ID <> 0 And prevIdRows.Contains(rowNew.ID)) Then
                                                               db.Entry(rowNew).State = EntityState.Modified
                                                               prevIdRows.Remove(rowNew.ID)
                                                           Else
                                                               db.Entry(rowNew).State = EntityState.Added
                                                           End If

                                                       End Sub)

                db.DocumentRows.Where(Function(p) prevIdRows.Contains(p.ID)).ToList().ForEach(Function(a) db.DocumentRows.Remove(a))

                db.Entry(document).State = EntityState.Modified

            Else
                ''documento nuovo
                db.Documents.Add(document)
            End If

            db.SaveChanges()
            'If Sucess then Save/Update Successfull else there it has Exception
            Return Json(New With {.Success = 1, .SalesID = document.ID, .ex = ""})

        Catch ex As Exception
            '' If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
            Return Json(New With {.Success = 0, .ex = ex.Message.ToString()})
        End Try

        Return Json(New With {.Success = 0, .ex = New Exception("unable to save").Message.ToString()})

    End Function

    '
    ' GET: /Document/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        ViewBag.Title = "Edit"
        ViewBag.Operationtype = "Edit"
        Dim document As Document = db.Documents.Find(id)
        If IsNothing(document) Then
            Return HttpNotFound()
        End If

        populateListView()

        Return View("Create", document)

    End Function

    '
    ' GET: /Document/Delete/5

    Function Delete(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim document As Document = db.Documents.Find(id)
        If IsNothing(document) Then
            Return HttpNotFound()
        End If
        Return View(document)
    End Function

    '
    ' POST: /Document/Delete/5

    <HttpPost()> _
    <ActionName("Delete")> _
    <ValidateAntiForgeryToken()> _
    Function DeleteConfirmed(ByVal id As Integer) As RedirectToRouteResult
        Dim document As Document = db.Documents.Find(id)
        db.Documents.Remove(document)
        db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub


    '
    ' GET: /Document/LoadMock

    Function LoadMock(Optional ByVal loopNr As Integer = 1) As ActionResult

        mockDocument(loopNr)  'load test documents

        Return RedirectToAction("Index")

    End Function


    'Carica nuovi documenti (dati di test)
    Private Sub mockDocument(Optional loopNr As Integer = 1)

        Dim docTypes As List(Of DocumentType) = db.DocumentTypes.ToList()

        If (docTypes.Count = 0) Then
            Return
        End If

        Dim attivo As AccountChart() = AccountCee.getNodeFromType(NodeType.ATTIVO).getAllAccountChart().ToArray()      ''dare
        Dim passivo As AccountChart() = AccountCee.getNodeFromType(NodeType.PASSIVO).getAllAccountChart().ToArray()    ''avere
        Dim costi As AccountChart() = AccountCee.getNodeFromType(NodeType.COSTI).getAllAccountChart().ToArray()        ''dare
        Dim ricavi As AccountChart() = AccountCee.getNodeFromType(NodeType.RICAVI).getAllAccountChart().ToArray()      ''avere

        Dim attivoIndex As Integer = 0
        Dim passivoIndex As Integer = 0
        Dim costiIndex As Integer = 0
        Dim ricaviIndex As Integer = 0
        Dim docNo As Integer = 0

        Dim oneGroup = Sub(cicleNr As Integer, dateReg As Date, delta As Decimal)

                           Dim year As Integer = dateReg.Year
                           Dim index As Integer = 0
                           docTypes.ForEach(Sub(docType)

                                                Dim even As Boolean = index Mod 2 = 0
                                                index += 1
                                                docNo += 1

                                                Dim doc As Document = New Document With {.documentType = docType, .dateReg = dateReg, .dateDoc = dateReg, .docNr = docNo, .amount = IIf(even, 30 + delta, 50 + delta), .note = "Nota su doc " & docNo}

                                                If (even) Then
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 1, .debit = DareAvere.Dare, .amount = 30 + delta, .AccountChart_ID = attivo(attivoIndex Mod attivo.Length).ID, .note = "attivo dare"})
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 2, .debit = DareAvere.Avere, .amount = 20 + delta, .AccountChart_ID = ricavi(ricaviIndex Mod ricavi.Length).ID, .note = "ricavo avere"})
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 3, .debit = DareAvere.Avere, .amount = 10, .AccountChart_ID = passivo(passivoIndex Mod passivo.Length).ID, .note = "passivo avere"})
                                                    ricaviIndex += 1
                                                    attivoIndex += 1
                                                    passivoIndex += 1
                                                Else
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 1, .debit = DareAvere.Avere, .amount = 50 + delta, .AccountChart_ID = passivo(passivoIndex Mod attivo.Length).ID, .note = "passivo avere"})
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 2, .debit = DareAvere.Dare, .amount = 45 + delta, .AccountChart_ID = costi(costiIndex Mod ricavi.Length).ID, .note = "costo dare"})
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 3, .debit = DareAvere.Dare, .amount = 5, .AccountChart_ID = attivo(attivoIndex Mod passivo.Length).ID, .note = "attivo dare"})
                                                    costiIndex += 1
                                                    attivoIndex += 1
                                                    passivoIndex += 1
                                                End If

                                                db.Documents.Add(doc)
                                                Dim changes As Integer = db.SaveChanges()

                                            End Sub)

                       End Sub

        Dim dateCurrent = Date.Now
        Dim datePrev = Date.Now.AddYears(-1)
        For i As Integer = 0 To loopNr - 1
            oneGroup(i, dateCurrent, 0)     'anno corrente
            oneGroup(i, datePrev, -2)       'anno precedente
        Next

    End Sub

    'Private Sub PopulateDocTypeDropDownList(Optional ByVal selected As Document = Nothing)

    '    Dim query = From c In db.DocumentTypes
    '    Where c.Active
    '    Order By c.Name, c.Code
    '    'Select c.ID, c.Name, c.Code

    '    Dim iappo As Integer
    '    If (IsNothing(selected)) Then
    '        iappo = 0
    '    Else
    '        iappo = selected.DocumentType_ID
    '    End If

    '    ViewBag.DocumentType_ID = New SelectList(query.ToList(), "ID", "Name", iappo)

    'End Sub

    'Private Function PopulateDocTypeList(Optional ByVal selected As Document = Nothing)

    '    Dim types = db.DocumentTypes.Select(Function(u) New SelectListItem With {.Text = u.Code + " " + u.Name, .Value = u.ID.ToString()})

    '    Dim query = From c In db.DocumentTypes
    '    Where c.Active
    '    Order By c.Name, c.Code
    '    Select c.ID, c.Name

    '    Dim iappo As Integer
    '    If (IsNothing(selected)) Then
    '        iappo = 0
    '    Else
    '        iappo = selected.DocumentType_ID
    '    End If

    '    Return query.ToList()

    '    '.Items = {
    '    '        New SelectListItem() With {.Value = "1", .Text = "item 1"},
    '    '        New SelectListItem() With {.Value = "2", .Text = "item 2"},
    '    '        New SelectListItem() With {.Value = "3", .Text = "item 3"}
    '    '    }

    '    'ViewBag.DocumentType_ID = New SelectList(query.ToList(), "ID", "Name", iappo)

    'End Function

    'load ViewBag.documentTypes with SelectListItem
    'Private Sub populateDocumentTypes()

    '    Dim types = db.DocumentTypes.Select(Function(u) New SelectListItem With {.Text = u.Name, .Value = u.ID.ToString()})
    '    ViewBag.documentTypes = types
    'End Sub

    'Non usata, ma viene lasciata solo come esempio
    Public Sub populateAccountCharts(Optional ByVal selected As AccountChart = Nothing)

        Dim query = From num In db.AccountCharts
                 Where num.Active
                 Order By num.Name, num.Code
                 Select num.ID, num.Code, num.Name

        Dim selectedId As Integer
        If (IsNothing(selected)) Then
            selectedId = 0
        Else
            selectedId = selected.ID
        End If

        Dim retValue As List(Of SelectListItem) = New List(Of SelectListItem)

        query.ToList().ForEach(Sub(a)
                                   retValue.Add(New SelectListItem With {.Value = a.ID.ToString(), .Text = a.Name, .Selected = (a.ID = selectedId)})
                               End Sub)

        ViewData("Conto") = retValue

    End Sub

    Public Sub populateListView()
        ViewBag.documentTypes = db.DocumentTypes.Select(Function(u) New SelectListItem With {.Text = u.Name, .Value = u.ID.ToString()})
        Dim listAccountChart = db.AccountCharts.Where(Function(a) a.Active).OrderBy(Function(a) a.Name).ToList()    'elenco di tutti i conti del pc
        ViewBag.ListChart = listAccountChart
        ViewBag.ListAvereAccountChartId = listAccountChart.Where(Function(a) DareAvere.Avere.Equals(a.Debit())).OrderBy(Function(a) a.ID).Select(Function(a) a.ID).ToArray() ''elenco conti (ID) in avere (gli avere sono meno dei dare) (x default se non sono avere, sono dare)

    End Sub

End Class
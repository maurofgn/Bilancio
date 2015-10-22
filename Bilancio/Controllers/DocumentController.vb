Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL
Imports PagedList
Imports System.Data.Entity.Validation

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
        Return View()
    End Function

    '
    ' POST: /Document/Create

    '<ValidateAntiForgeryToken()>   'non usato perchè i dati vengono forniti via ajax
    <HttpPost()>
    Function Create(ByVal document As Document) As ActionResult
        If ModelState.IsValid Then
            db.Documents.Add(document)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(document)
    End Function

    '
    ' GET: /Document/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim document As Document = db.Documents.Find(id)
        If IsNothing(document) Then
            Return HttpNotFound()
        End If
        Return View(document)
    End Function

    '
    ' POST: /Document/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal document As Document) As ActionResult
        If ModelState.IsValid Then
            db.Entry(document).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(document)
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

        Return View("index", db.Documents.ToList())

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
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 1, .debit = even, .amount = 30 + delta, .AccountChart_ID = attivo(attivoIndex Mod attivo.Length).ID, .note = "attivo dare"})
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 2, .debit = Not even, .amount = 20 + delta, .AccountChart_ID = ricavi(ricaviIndex Mod ricavi.Length).ID, .note = "ricavo avere"})
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 3, .debit = Not even, .amount = 10, .AccountChart_ID = passivo(passivoIndex Mod passivo.Length).ID, .note = "passivo avere"})
                                                    ricaviIndex += 1
                                                    attivoIndex += 1
                                                    passivoIndex += 1
                                                Else
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 1, .debit = Not even, .amount = 50 + delta, .AccountChart_ID = passivo(passivoIndex Mod attivo.Length).ID, .note = "passivo avere"})
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 2, .debit = even, .amount = 45 + delta, .AccountChart_ID = costi(costiIndex Mod ricavi.Length).ID, .note = "costo dare"})
                                                    db.DocumentRows.Add(New DocumentRow With {.Document = doc, .rowNr = 3, .debit = even, .amount = 5, .AccountChart_ID = attivo(attivoIndex Mod passivo.Length).ID, .note = "attivo dare"})
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

    Private Sub PopulateDocTypeDropDownList(Optional ByVal selected As Document = Nothing)

        Dim query = From c In db.DocumentTypes
        Where c.Active
        Order By c.Name, c.Code
        'Select c.ID, c.Name, c.Code

        Dim iappo As Integer
        If (IsNothing(selected)) Then
            iappo = 0
        Else
            iappo = selected.DocumentType_ID
        End If

        ViewBag.DocumentType_ID = New SelectList(query.ToList(), "ID", "Name", iappo)

    End Sub


End Class
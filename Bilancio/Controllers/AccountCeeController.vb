Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL
Imports PagedList


Public Class AccountCeeController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /AccountCee/

    'Function Index() As ActionResult


    '    ViewBag.NameSortParm = If(String.IsNullOrEmpty(sortOrder), "name_desc", String.Empty)
    '    ViewBag.DateSortParm = If(sortOrder = "Date", "date_desc", "Date")
    '    Dim students = From s In db.Students Select s
    '    Select Case sortOrder
    '        Case "name_desc"
    '            students = students.OrderByDescending(Function(s) s.LastName)
    '        Case "Date"
    '            students = students.OrderBy(Function(s) s.EnrollmentDate)
    '        Case "date_desc"
    '            students = students.OrderByDescending(Function(s) s.EnrollmentDate)
    '        Case Else
    '            students = students.OrderBy(Function(s) s.LastName)
    '    End Select
    '    Return View(students.ToList())



    '    Dim aa As List(Of AccountCee) = db.AccountCees.OrderBy(Function(a) a.Parent_ID).ToList()

    '    Return View(aa)
    'End Function

#Region "Public Methods"

    Function Index(ByVal sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
        ViewBag.CurrentSort = sortOrder
        ViewBag.NameSortParm = If(String.IsNullOrEmpty(sortOrder), "name_desc", String.Empty)
        ViewBag.CodeSortParm = If(sortOrder = "code", "code_desc", "code")

        If Not searchString Is Nothing Then
            page = 1
        Else
            searchString = currentFilter
        End If

        ViewBag.CurrentFilter = searchString

        Dim accs = From s In db.AccountCees Select s

        If Not String.IsNullOrEmpty(searchString) Then
            accs = accs.Where(Function(s) s.Name.ToUpper().Contains(searchString.ToUpper()) _
                                                  Or s.Code.ToUpper().Contains(searchString.ToUpper()))
        End If

        Select Case sortOrder
            Case "name_desc"
                accs = accs.OrderByDescending(Function(s) s.Name)
            Case "code"
                accs = accs.OrderBy(Function(s) s.Code)
            Case "code_desc"
                accs = accs.OrderByDescending(Function(s) s.Code)
            Case Else
                accs = accs.OrderBy(Function(s) s.Name).ThenBy(Function(s) s.Code)
        End Select

        Dim pageNumber As Integer = If(page, 1)
        Dim pageSize As Integer = 10

        Return View(accs.ToPagedList(pageNumber, pageSize))

    End Function

    '
    'elenco dei conti visualizzato ad albero
    Function Simple() As ActionResult

        Dim aa As List(Of AccountCee) = db.AccountCees.OrderBy(Function(a) a.Parent.ID).ToList()

        Return View(aa)
    End Function

    '
    ' GET: /AccountCee/Details/5

    Function Details(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim accountcee As AccountCee = db.AccountCees.Find(id)
        If IsNothing(accountcee) Then
            Return HttpNotFound()
        End If
        Return View(accountcee)
    End Function

    '
    ' GET: /AccountCee/Create

    Function Create() As ActionResult
        'loadListParent()
        PopulateParentsDropDownList()
        Return View()
    End Function

    '
    ' POST: /AccountCee/Create


    Private Function GetErrorListFromModelState(ms As ModelStateDictionary) As List(Of String)

        Dim query = From state In ModelState.Values
            From er In state.Errors
            Select er.ErrorMessage

        Dim errorList As List(Of String) = query.ToList()


        Return errorList

    End Function






    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Create(ByVal accountcee As AccountCee) As ActionResult

        'todo: il save di accountcee ha il modelState.isValid = false perchè il parent ha l'id, ma ha altri campi vuoti (codice, name, ...)
        'Dim parent As AccountCee = db.AccountCees.Find(accountcee.Parent.ID)
        'accountcee.Parent = parent

        If ModelState.IsValid Then

            If (accountcee.NodeType = NodeType.ALTRO) Then
                accountcee.Debit = getDebit(accountcee) 'definisce il dare/avere in base ai costi/ricavi/attivo/passivo
            End If

            db.AccountCees.Add(accountcee)
            db.SaveChanges()
            Return RedirectToAction("Index")
        Else
            Dim validationErrors As String = String.Join(",",
                ModelState.Values.Where(Function(E) E.Errors.Count > 0) _
                    .SelectMany(Function(E) E.Errors) _
                    .[Select](Function(E) E.ErrorMessage).ToArray())

            Trace.WriteLine(validationErrors)   'elenco erroricontenuti in validationErrors
        End If

        'loadListParent()
        PopulateParentsDropDownList(accountcee.Parent)
        Return View(accountcee)
    End Function

    '
    ' GET: /AccountCee/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim accountcee As AccountCee = db.AccountCees.Find(id)
        If IsNothing(accountcee) Then
            Return HttpNotFound()
        End If

        'loadListParent()
        PopulateParentsDropDownList(accountcee.Parent)

        Return View(accountcee)
    End Function

    '
    ' POST: /AccountCee/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal accountcee As AccountCee) As ActionResult
        If ModelState.IsValid Then
            db.Entry(accountcee).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        Else
            Dim validationErrors As String = String.Join(",",
                ModelState.Values.Where(Function(E) E.Errors.Count > 0) _
                    .SelectMany(Function(E) E.Errors) _
                    .[Select](Function(E) E.ErrorMessage).ToArray())

            Trace.WriteLine(validationErrors)   'elenco errori contenuti in validationErrors

        End If

        'loadListParent()
        PopulateParentsDropDownList(accountcee.Parent)

        Return View(accountcee)
    End Function

    '
    ' GET: /AccountCee/Delete/5

    Function Delete(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim accountcee As AccountCee = db.AccountCees.Find(id)
        If IsNothing(accountcee) Then
            Return HttpNotFound()
        End If
        Return View(accountcee)
    End Function

    '
    ' POST: /AccountCee/Delete/5

    <HttpPost()> _
    <ActionName("Delete")> _
    <ValidateAntiForgeryToken()> _
    Function DeleteConfirmed(ByVal id As Integer) As RedirectToRouteResult
        Dim accountcee As AccountCee = db.AccountCees.Find(id)
        db.AccountCees.Remove(accountcee)
        db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

#End Region

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub

#Region "Private Methods"


    'definisce il dare/avere in base ai costi/ricavi/attivo/passivo
    Private Function getDebit(accountcee As AccountCee) As Boolean

        If (accountcee Is Nothing Or accountcee.Parent Is Nothing) Then
            Return True
        End If

        Select Case accountcee.NodeType
            Case NodeType.ATTIVO, NodeType.COSTI
                Return True
            Case NodeType.PASSIVO, NodeType.RICAVI
                Return False
            Case Else
                Return getDebit(accountcee.Parent)
        End Select

        Return True
    End Function

    Private Sub loadListParent()
        Dim parents As List(Of AccountCee) = getParents()

        'Dim ListItem As List(Of SelectListItem) = New List(Of SelectListItem)
        'For Each row In parents
        '    ListItem.Add(New SelectListItem With {.Text = row.Code + " " + row.Name, .Value = row.ID})
        'Next

        ViewData("ListParent") = New SelectList(parents, "ID", "Name", "")
        '        ViewData("ListParent") = New SelectList(ListItem, "Value", "Text", "")
    End Sub

    'Private Sub PopulateDepartmentsDropDownList(Optional ByVal selectedDepartment As Object = Nothing)
    '    Dim departmentsQuery = db.Departments.OrderBy(Function(d) d.Name)
    '    ViewBag.DepartmentID = New SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment)
    'End Sub

    Private Sub PopulateParentsDropDownList(Optional ByVal selectedParent As Object = Nothing)

        Dim validTypeParents As Integer() = {NodeType.ATTIVO, NodeType.PASSIVO, NodeType.COSTI, NodeType.RICAVI, NodeType.ALTRO}

        Dim query = From c In db.AccountCees
        Where c.Summary = True And validTypeParents.Contains(c.NodeType)
        Order By c.Code, c.Name

        ViewBag.Parent = New SelectList(query, "ID", "Name", selectedParent)
    End Sub

    'elenco dei conti definiti parent
    Private Function getParents() As List(Of AccountCee)

        Dim validTypeParents As Integer() = {NodeType.ATTIVO, NodeType.PASSIVO, NodeType.COSTI, NodeType.RICAVI, NodeType.ALTRO}

        Dim query = From c In db.AccountCees
        Where c.Summary = True And validTypeParents.Contains(c.NodeType)
        Order By c.Code, c.Name
        Return query.ToList()

    End Function


#End Region


End Class
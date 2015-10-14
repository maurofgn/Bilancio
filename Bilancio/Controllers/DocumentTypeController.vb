Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL
Imports PagedList
Imports System.Data.Entity.Validation

Public Class DocumentTypeController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /DocumentType/

    'Function Index() As ActionResult
    '    Return View(db.DocumentTypes.ToList())
    'End Function

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

        Dim accs = From s In db.DocumentTypes Select s

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
        Dim pageSize As Integer = LINES_PER_PAGE

        Return View(accs.ToPagedList(pageNumber, pageSize))

    End Function

    '
    ' GET: /DocumentType/Details/5

    Function Details(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim documenttype As DocumentType = db.DocumentTypes.Find(id)
        If IsNothing(documenttype) Then
            Return HttpNotFound()
        End If
        Return View(documenttype)
    End Function

    '
    ' GET: /DocumentType/Create

    Function Create() As ActionResult
        Return View()
    End Function

    '
    ' POST: /DocumentType/Create

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Create(ByVal documenttype As DocumentType) As ActionResult
        If ModelState.IsValid Then
            db.DocumentTypes.Add(documenttype)
            Try
                db.SaveChanges()
                Return RedirectToAction("Index")
            Catch ex As DbEntityValidationException
                db.GetValidationErrors.ToList().ForEach(Sub(c) c.ValidationErrors.ToList().ForEach(Sub(e) ModelState.AddModelError(e.PropertyName, e.ErrorMessage)))
            End Try
        End If

        Return View(documenttype)
    End Function

    '
    ' GET: /DocumentType/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim documenttype As DocumentType = db.DocumentTypes.Find(id)
        If IsNothing(documenttype) Then
            Return HttpNotFound()
        End If
        Return View(documenttype)
    End Function

    '
    ' POST: /DocumentType/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal documenttype As DocumentType) As ActionResult
        If ModelState.IsValid Then
            db.Entry(documenttype).State = EntityState.Modified
            Try
                db.SaveChanges()
                Return RedirectToAction("Index")
            Catch ex As DbEntityValidationException
                db.GetValidationErrors.ToList().ForEach(Sub(c) c.ValidationErrors.ToList().ForEach(Sub(e) ModelState.AddModelError(e.PropertyName, e.ErrorMessage)))
            End Try
        End If

        Return View(documenttype)
    End Function

    '
    ' GET: /DocumentType/Delete/5

    Function Delete(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim documenttype As DocumentType = db.DocumentTypes.Find(id)
        If IsNothing(documenttype) Then
            Return HttpNotFound()
        End If
        Return View(documenttype)
    End Function

    '
    ' POST: /DocumentType/Delete/5

    <HttpPost()> _
    <ActionName("Delete")> _
    <ValidateAntiForgeryToken()> _
    Function DeleteConfirmed(ByVal id As Integer) As RedirectToRouteResult
        Dim documenttype As DocumentType = db.DocumentTypes.Find(id)
        db.DocumentTypes.Remove(documenttype)
        db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub

End Class
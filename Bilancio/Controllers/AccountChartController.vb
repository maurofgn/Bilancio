Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL
Imports PagedList
Imports System.Data.Entity.Validation


Public Class AccountChartController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /AccountChart/

    '    Function Index() As ActionResult
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

        Dim lines = From s In db.AccountCharts Select s

        If Not String.IsNullOrEmpty(searchString) Then
            lines = lines.Where(Function(s) s.Name.ToUpper().Contains(searchString.ToUpper()) _
                                                  Or s.Code.ToUpper().Contains(searchString.ToUpper()))
        End If

        Select Case sortOrder
            Case "name_desc"
                lines = lines.OrderByDescending(Function(s) s.Name)
            Case "code"
                lines = lines.OrderBy(Function(s) s.Code)
            Case "code_desc"
                lines = lines.OrderByDescending(Function(s) s.Code)
            Case Else
                lines = lines.OrderBy(Function(s) s.Name).ThenBy(Function(s) s.Code)
        End Select

        Dim pageNumber As Integer = If(page, 1)
        Dim pageSize As Integer = LINES_PER_PAGE

        Return View(lines.ToPagedList(pageNumber, pageSize))

    End Function

    '
    ' GET: /AccountChart/Details/5

    Function Details(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim accountchart As AccountChart = db.AccountCharts.Find(id)
        If IsNothing(accountchart) Then
            Return HttpNotFound()
        End If
        Return View(accountchart)
    End Function

    '
    ' GET: /AccountChart/Create

    Function Create() As ActionResult
        PopulateParentsDropDownList()
        Return View()
    End Function

    '
    ' POST: /AccountChart/Create

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Create(ByVal accountchart As AccountChart) As ActionResult

        If ModelState.IsValid Then
            db.AccountCharts.Add(accountchart)
            Try
                db.SaveChanges()
                Return RedirectToAction("Index")
            Catch ex As DbEntityValidationException
                db.GetValidationErrors.ToList().ForEach(Sub(c) c.ValidationErrors.ToList().ForEach(Sub(e) ModelState.AddModelError(e.PropertyName, e.ErrorMessage)))
            End Try
        End If

        PopulateParentsDropDownList(accountchart.AccountCee)
        Return View(accountchart)
    End Function

    '
    ' GET: /AccountChart/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim accountchart As AccountChart = db.AccountCharts.Find(id)
        If IsNothing(accountchart) Then
            Return HttpNotFound()
        End If

        PopulateParentsDropDownList(accountchart.AccountCee)
        Return View(accountchart)
    End Function

    '
    ' POST: /AccountChart/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal accountchart As AccountChart) As ActionResult
        If ModelState.IsValid Then
            db.Entry(accountchart).State = EntityState.Modified

            Try
                db.SaveChanges()
                Return RedirectToAction("Index")
            Catch ex As DbEntityValidationException
                db.GetValidationErrors.ToList().ForEach(Sub(c) c.ValidationErrors.ToList().ForEach(Sub(e) ModelState.AddModelError(e.PropertyName, e.ErrorMessage)))
            End Try
        End If

        PopulateParentsDropDownList(accountchart.AccountCee)
        Return View(accountchart)
    End Function

    '
    ' GET: /AccountChart/Delete/5

    Function Delete(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim accountchart As AccountChart = db.AccountCharts.Find(id)
        If IsNothing(accountchart) Then
            Return HttpNotFound()
        End If
        Return View(accountchart)
    End Function

    '
    ' POST: /AccountChart/Delete/5

    <HttpPost()> _
    <ActionName("Delete")> _
    <ValidateAntiForgeryToken()> _
    Function DeleteConfirmed(ByVal id As Integer) As RedirectToRouteResult
        Dim accountchart As AccountChart = db.AccountCharts.Find(id)
        db.AccountCharts.Remove(accountchart)
        db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Private Sub PopulateParentsDropDownList(Optional ByVal selectedParent As AccountCee = Nothing)

        Dim query = From c In db.AccountCees
        Where c.Summary = False And c.NodeType = NodeType.ALTRO
        Order By c.Code, c.Name
        'Select c.ID, c.Name, c.Code

        Dim iappo As Integer
        If (IsNothing(selectedParent)) Then
            iappo = 0
        Else
            iappo = selectedParent.ID
        End If

        ViewBag.AccountCeeID = New SelectList(query.ToList(), "ID", "Name", iappo)

    End Sub

End Class
Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL

Public Class AccountChartController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /AccountChart/

    Function Index() As ActionResult
        Return View(db.AccountCharts.ToList())
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
        Return View()
    End Function

    '
    ' POST: /AccountChart/Create

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Create(ByVal accountchart As AccountChart) As ActionResult
        If ModelState.IsValid Then
            db.AccountCharts.Add(accountchart)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(accountchart)
    End Function

    '
    ' GET: /AccountChart/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim accountchart As AccountChart = db.AccountCharts.Find(id)
        If IsNothing(accountchart) Then
            Return HttpNotFound()
        End If
        Return View(accountchart)
    End Function

    '
    ' POST: /AccountChart/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal accountchart As AccountChart) As ActionResult
        If ModelState.IsValid Then
            db.Entry(accountchart).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

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

End Class
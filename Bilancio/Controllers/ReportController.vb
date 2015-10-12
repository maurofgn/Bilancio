Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL

Public Class ReportController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /Report/

    Function Index() As ActionResult
        Return View(db.Reports.ToList())
    End Function

    '
    ' GET: /Report/Details/5

    Function Details(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim report As Report = db.Reports.Find(id)
        If IsNothing(report) Then
            Return HttpNotFound()
        End If
        Return View(report)
    End Function

    '
    ' GET: /Report/Create

    Function Create() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Report/Create

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Create(ByVal report As Report) As ActionResult
        If ModelState.IsValid Then
            db.Reports.Add(report)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(report)
    End Function

    '
    ' GET: /Report/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim report As Report = db.Reports.Find(id)
        If IsNothing(report) Then
            Return HttpNotFound()
        End If
        Return View(report)
    End Function

    '
    ' POST: /Report/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal report As Report) As ActionResult
        If ModelState.IsValid Then
            db.Entry(report).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(report)
    End Function

    '
    ' GET: /Report/Delete/5

    Function Delete(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim report As Report = db.Reports.Find(id)
        If IsNothing(report) Then
            Return HttpNotFound()
        End If
        Return View(report)
    End Function

    '
    ' POST: /Report/Delete/5

    <HttpPost()> _
    <ActionName("Delete")> _
    <ValidateAntiForgeryToken()> _
    Function DeleteConfirmed(ByVal id As Integer) As RedirectToRouteResult
        Dim report As Report = db.Reports.Find(id)
        db.Reports.Remove(report)
        db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub

End Class
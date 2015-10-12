Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL

Public Class DocumentController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /Document/

    Function Index() As ActionResult
        Return View(db.Documents.ToList())
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

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
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

End Class
Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL

Public Class DocumentTypeController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /DocumentType/

    Function Index() As ActionResult
        Return View(db.DocumentTypes.ToList())
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
            db.SaveChanges()
            Return RedirectToAction("Index")
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
            db.SaveChanges()
            Return RedirectToAction("Index")
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
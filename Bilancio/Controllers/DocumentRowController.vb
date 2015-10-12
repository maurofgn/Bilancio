Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL

Public Class DocumentRowController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /DocumentRow/

    Function Index() As ActionResult
        Return View(db.DocumentRows.ToList())
    End Function

    '
    ' GET: /DocumentRow/Details/5

    Function Details(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim documentrow As DocumentRow = db.DocumentRows.Find(id)
        If IsNothing(documentrow) Then
            Return HttpNotFound()
        End If
        Return View(documentrow)
    End Function

    '
    ' GET: /DocumentRow/Create

    Function Create() As ActionResult
        Return View()
    End Function

    '
    ' POST: /DocumentRow/Create

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Create(ByVal documentrow As DocumentRow) As ActionResult
        If ModelState.IsValid Then
            db.DocumentRows.Add(documentrow)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(documentrow)
    End Function

    '
    ' GET: /DocumentRow/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim documentrow As DocumentRow = db.DocumentRows.Find(id)
        If IsNothing(documentrow) Then
            Return HttpNotFound()
        End If
        Return View(documentrow)
    End Function

    '
    ' POST: /DocumentRow/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal documentrow As DocumentRow) As ActionResult
        If ModelState.IsValid Then
            db.Entry(documentrow).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(documentrow)
    End Function

    '
    ' GET: /DocumentRow/Delete/5

    Function Delete(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim documentrow As DocumentRow = db.DocumentRows.Find(id)
        If IsNothing(documentrow) Then
            Return HttpNotFound()
        End If
        Return View(documentrow)
    End Function

    '
    ' POST: /DocumentRow/Delete/5

    <HttpPost()> _
    <ActionName("Delete")> _
    <ValidateAntiForgeryToken()> _
    Function DeleteConfirmed(ByVal id As Integer) As RedirectToRouteResult
        Dim documentrow As DocumentRow = db.DocumentRows.Find(id)
        db.DocumentRows.Remove(documentrow)
        db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub

End Class
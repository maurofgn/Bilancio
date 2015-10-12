Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL

Public Class AvisController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /Avis/

    Function Index() As ActionResult
        Return View(db.Aviss.ToList())
    End Function

    '
    ' GET: /Avis/Details/5

    Function Details(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim avis As Avis = db.Aviss.Find(id)
        If IsNothing(avis) Then
            Return HttpNotFound()
        End If
        Return View(avis)
    End Function

    '
    ' GET: /Avis/Create

    Function Create() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Avis/Create

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Create(ByVal avis As Avis) As ActionResult
        If ModelState.IsValid Then
            db.Aviss.Add(avis)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(avis)
    End Function

    '
    ' GET: /Avis/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim avis As Avis = db.Aviss.Find(id)
        If IsNothing(avis) Then
            Return HttpNotFound()
        End If
        Return View(avis)
    End Function

    '
    ' POST: /Avis/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal avis As Avis) As ActionResult
        If ModelState.IsValid Then
            db.Entry(avis).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(avis)
    End Function

    '
    ' GET: /Avis/Delete/5

    Function Delete(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim avis As Avis = db.Aviss.Find(id)
        If IsNothing(avis) Then
            Return HttpNotFound()
        End If
        Return View(avis)
    End Function

    '
    ' POST: /Avis/Delete/5

    <HttpPost()> _
    <ActionName("Delete")> _
    <ValidateAntiForgeryToken()> _
    Function DeleteConfirmed(ByVal id As Integer) As RedirectToRouteResult
        Dim avis As Avis = db.Aviss.Find(id)
        db.Aviss.Remove(avis)
        db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub

End Class
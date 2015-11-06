Imports Bilancio.DAL

Public Class UtilController
    Inherits System.Web.Mvc.Controller


    Private db As New BilancioContext

    <HttpPost()>
    Function chkCode(tableName As String, code As String, id As Integer) As JsonResult

        'If (Request.IsAjaxRequest()) Then
        '    Trace.WriteLine("IsAjaxRequest")
        'End If

        If (String.IsNullOrEmpty(code)) Then
            Return Json(New With {.Success = 1, .SalesID = 1, .ex = ""})

        End If

        If (String.IsNullOrEmpty(tableName)) Then
            Return Json(New With {.Success = 0, .ex = New Exception("unable to chk (missing tablename)").Message.ToString()})
        End If

        Dim codeOther As String = ch(tableName, code, id)

        If (Not String.IsNullOrEmpty(codeOther)) Then
            Return Json(New With {.Success = 0, .ex = "codice usato da: " + codeOther})
        Else
            Return Json(New With {.Success = 1, .ex = ""})
        End If

    End Function

    Private Function ch(tableName As String, code As String, id As Integer) As String

        Dim retValue As List(Of String)

        If (tableName.ToUpper.Trim.Equals("AccountChart".ToUpper)) Then
            retValue = db.AccountCharts.Where(Function(a) a.Code = code And a.ID <> id).Select(Function(a) a.Name).Take(1).ToList()
        ElseIf (tableName.ToUpper.Trim.Equals("AccountCee".ToUpper)) Then
            retValue = db.AccountCees.Where(Function(a) a.Code = code And a.ID <> id).Select(Function(a) a.Name).Take(1).ToList()
        ElseIf (tableName.ToUpper.Trim.Equals("DocumentType".ToUpper)) Then
            retValue = db.DocumentTypes.Where(Function(a) a.Code = code And a.ID <> id).Select(Function(a) a.Name).Take(1).ToList()
        Else
            retValue = New List(Of String)
        End If

        If (retValue.Count > 0) Then
            Return retValue.First
        Else
            Return ""
        End If

    End Function


End Class

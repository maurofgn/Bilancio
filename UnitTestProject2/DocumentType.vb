'------------------------------------------------------------------------------
' <auto-generated>
'     Codice generato da un modello.
'
'     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
'     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class DocumentType
    Public Property ID As Integer
    Public Property Code As String
    Public Property Name As String
    Public Property Active As Boolean

    Public Overridable Property Document As ICollection(Of Document) = New HashSet(Of Document)

End Class

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    Public Enum ReportFormatType
        PDF
        HTML
        XML
        CSV
        XLS
        ODT
        ODS
        DOCX
        XLSX
        PPTX
    End Enum

    Public Class Report

        Public Property ID As Integer

        <Required>
        <StringLength(20, MinimumLength:=1, ErrorMessage:="La lunghezza massima é 20 caratteri.")>
        <Display(Name:="Codice")>
        Public Property Code As String

        <Required>
        <StringLength(60, MinimumLength:=3, ErrorMessage:="La lunghezza massima é 60 caratteri.")>
        <Display(Name:="Descrizione")>
        Public Property Name As String

        '<Timestamp>
        'Public Property RowVersion As Byte()

        Public Property Active As Boolean = True

        Public Property ModelName As String


        <DisplayFormat(NullDisplayText:="Non definito")>
        Public Property FormatType As ReportFormatType?

        Public Property OutFileName As String


        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Data Creazione")>
        'Public Property dateCreated As DateTime = DateTime.Now

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        'Public Property lastUpdate As DateTime = DateTime.Now

    End Class

End Namespace

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports Bilancio.DAL

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
        Implements EntityBase

        Public Property ID As Integer

        <Required>
        <StringLength(20, MinimumLength:=1, ErrorMessage:="La lunghezza massima é 20 caratteri.")>
        <Display(Name:="Codice")>
        <Index("codeIndex", IsUnique:=True)>
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

        <Display(Name:="Azione")>
        Public Property ActioneName As String
        <Display(Name:="Controllo")>
        Public Property ControllerName As String

        <DataType(DataType.DateTime)>
        <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode:=True)>
        <Display(Name:="Data Creazione")>
        <Editable(False)>
        Public Property dateCreated As DateTime Implements EntityBase.dateCreated

        <DataType(DataType.DateTime)>
        <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode:=True)>
        <Display(Name:="Ultimo Aggiornamento")>
        <Editable(False)>
        Public Property lastUpdate As DateTime Implements EntityBase.lastUpdate

    End Class

End Namespace

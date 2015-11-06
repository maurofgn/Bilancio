Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class Document

        Public Property ID As Integer

        <DataType(DataType.Date)>
        <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        <Display(Name:="Data Registrazione")>
        <Required(ErrorMessage:="valore richiesto")>
        Public Property dateReg As DateTime = DateTime.Now

        <DataType(DataType.Date)>
        <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        <Display(Name:="Data Documento")>
        Public Property dateDoc As DateTime = DateTime.Now

        '<UIHint("IndirizzoEmail")>
        <StringLength(20, ErrorMessage:="{0} deve essere al massimo {1} caratteri.")>
        <Display(Name:="Nr. Doc.")>
        Public Property docNr As String

        <Display(Name:="Nota")>
        Public Property note As String

        '<DisplayFormat(DataType.Currency)>

        '<UIHint("Valuta")>
        <DataType(DataType.Currency)>
        <Display(Name:="Tot. Doc.")>
        <Required>
        Public Property amount As Decimal

        <Display(Name:="Tipo Documento")>
        <Required>
        Public Overridable Property DocumentType_ID As Integer

        <ForeignKey("DocumentType_ID")>
        Public Overridable Property documentType As DocumentType

        Public Overridable Property documentRows As ICollection(Of DocumentRow)

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Data Creazione")>
        'Public Property dateCreated As DateTime = DateTime.Now

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        'Public Property lastUpdate As DateTime = DateTime.Now

        Public ReadOnly Property totalInfo() As DocumentTot
            Get
                Return New DocumentTot(Me)
            End Get

        End Property

    End Class

End Namespace


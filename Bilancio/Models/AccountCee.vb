Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema


Namespace Models

    Public Enum NodeType
        ROOT
        PATRIMONIALE
        ECONOMICO
        ATTIVO
        PASSIVO
        COSTI
        RICAVI
        ALTRO
    End Enum


    Public Class AccountCee
        Implements IValidatableObject

        Dim _Active As Boolean = True
        Dim _SeqNo As Integer = 0

        Public Property ID As Integer

        <Required>
        <StringLength(20, MinimumLength:=1, ErrorMessage:="almeno 1 carattere e massimo 20.")>
        <Display(Name:="Codice")>
        <Index("codeIndex", IsUnique:=True)>
        Public Property Code As String

        <Required>
        <StringLength(60, MinimumLength:=3, ErrorMessage:="almeno 3 caratteri e massimo 60.")>
        <Display(Name:="Descrizione")>
        Public Property Name As String

        '<ConcurrencyCheck>
        '<Timestamp>
        'Public Property RowVersion As Byte()

        <Display(Name:="Attivo")>
        Public Property Active As Boolean
            Get
                Return _Active
            End Get

            Set(ByVal Value As Boolean)
                _Active = Value
            End Set

        End Property

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Data Creazione")>
        'Public Property dateCreated As DateTime = DateTime.Now

        '<DataType(DataType.Date)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        'Public Property lastUpdate As DateTime = DateTime.Now

        <Required>
        <Display(Name:="Nr. Sequenza")>
        Public Property SeqNo As Integer
            Get
                Return _SeqNo
            End Get
            Set(value As Integer)
                _SeqNo = value
            End Set
        End Property


        <Display(Name:="Conto padre")>
        Public Property Summary As Boolean = True
        'Public Property Summary As Boolean = True

        '<Required(ErrorMessage:="Total t/f")>
        <Display(Name:="Conto con totale")>
        Public Property Total As Boolean = False

        '<Required(ErrorMessage:="Debit t/f")>
        <Display(Name:="Conto dare")>
        Public Property Debit As Boolean = True

        Public Property NodeType As NodeType = NodeType.ALTRO

        <MustBeSelectedAttribute(ErrorMessage:="Definire un conto di appartenenza")>
        Public Property ParentID As Integer?

        '<Display(Name:="Padre")>
        Public Overridable Property Parent As AccountCee

        '<Display(Name:="Figli")>
        Public Overridable Property Sons As ICollection(Of AccountCee) = New HashSet(Of AccountCee)
        Public Overridable Property AccountCharts As ICollection(Of AccountChart) = New HashSet(Of AccountChart)

        Public Overrides Function ToString() As String
            Return Name.ToString()
        End Function

        Function Validate(validationContext As ValidationContext) As IEnumerable(Of ValidationResult) Implements IValidatableObject.Validate

            If (NodeType.Equals(NodeType.ALTRO) AndAlso ParentID Is Nothing) Then
                Return New List(Of ValidationResult) From {New ValidationResult("Definire il conto di appartenza", New List(Of String) From {"ParentID"})}
            End If

            'If ContextDataSource.AccountCee.Any(Function(o) o.Code = AccountCee.Code) Then
            '    ' Match! "esiste un codice con lo stesso valore"
            'End If

            Return Nothing

        End Function

    End Class

    'Partial Public Class MustBeSelectedAttribute2
    '    Inherits ValidationAttribute



    '    Public Fuction IsOldEnough() as ValidationResult

    '        return ValidationResult.Success
    '    end Function


    'End Class

    Public Class MustBeSelectedAttribute
        Inherits ValidationAttribute

        'Questo pezzo viene lasciato solo come esempio di utilizzo per la creazione di una annotazione personalizzata 

        'Public Sub OnValidate(ByVal action As System.Data.Linq.ChangeAction)
        '    If Not Char.IsUpper(Me._LastName(0)) _
        '    OrElse Not Char.IsUpper(Me._FirstName(0)) _
        '    OrElse Not Char.IsUpper(Me._Title(0)) Then
        '        Throw New ValidationException( _
        '           "Data value must start with an uppercase letter.")
        '    End If
        'End Sub


        '    public static ValidationResult IsOldEnough(int givenAge)
        '{
        '    if (givenAge >= 20)
        '        return ValidationResult.Success;
        '    else
        '        return new ValidationResult("You're not old enough");
        '}

        '        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        'public class OldEnoughValidationAttribute : ValidationAttribute
        '{
        '    public static ValidationResult IsOldEnough(int givenAge)
        '    {
        '        if (givenAge >= 20)
        '            return ValidationResult.Success;
        '        else
        '            return new ValidationResult("You're not old enough");
        '    }
        '}

        Public Overrides Function IsValid(value As Object) As Boolean

            'If (value Is Nothing) Then
            '    Return False
            'Else
            '    Return True
            'End If

            '            Return Not value Is Nothing
            Return True

        End Function

    End Class

End Namespace

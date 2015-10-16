Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports Bilancio.DAL


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
        <StringLength(20, MinimumLength:=1, ErrorMessage:="{0} deve essere di almeno {2} caratteri e massimo {1}.")>
        <Display(Name:="Codice")>
        <Index("codeIndex", IsUnique:=True)>
        Public Property Code As String

        <Required>
        <StringLength(60, MinimumLength:=3, ErrorMessage:="{0} deve essere di almeno {2} caratteri e massimo {1}.")>
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

        <Display(Name:="[Codice]Nome")>
        Public ReadOnly Property CodeName() As String
            Get
                Return String.Format("[{0}]{1}", Code, Name)
            End Get
            'Set(value As String)

            'End Set
        End Property


        Function Validate(validationContext As ValidationContext) As IEnumerable(Of ValidationResult) Implements IValidatableObject.Validate

            If (NodeType.Equals(NodeType.ALTRO) AndAlso ParentID Is Nothing) Then
                Return New List(Of ValidationResult) From {New ValidationResult("Definire il conto di appartenza", New List(Of String) From {"ParentID"})}
            End If

            'If ContextDataSource.AccountCee.Any(Function(o) o.Code = AccountCee.Code) Then
            '    ' Match! "esiste un codice con lo stesso valore"
            'End If

            Return New List(Of ValidationResult) From {ValidationResult.Success}

        End Function

        Shared Function getNodeFromType(nodeType As Models.NodeType) As AccountCee

            Dim db As New BilancioContext
            Return db.AccountCees().Where(Function(p) p.NodeType = nodeType).First

        End Function

        'Lista di nodi ottenuta dall'attraversamento dell'albero a partire dal nodo corrente compreso
        'return lista con tutti i figli a tutti i livelli del nodo corrente
        Function getAllSons(Optional onlyLeaves As Boolean = False) As List(Of AccountCee)

            Dim retValue As List(Of AccountCee) = New List(Of AccountCee)
            Dim traverse As Action(Of AccountCee) = Sub(node As AccountCee)
                                                        If (Not node Is Nothing) Then
                                                            If (Not onlyLeaves Or Not node.Summary) Then
                                                                retValue.Add(node)
                                                            End If
                                                            If (node.Sons.Count > 0) Then
                                                                node.Sons.ToList.ForEach(Sub(s) traverse(s))
                                                            End If
                                                        End If
                                                    End Sub

            traverse(Me)

            Return retValue

        End Function

        'return i conti del piano dei conti agganciati ad un conto cee figlio a qualsiasi livello del corrente
        Function getAllAccountChart() As List(Of AccountChart)

            Dim rewtValue As List(Of AccountChart) = New List(Of AccountChart)

            getAllSons(True).ForEach(Sub(d) rewtValue.AddRange(d.AccountCharts))

            Return rewtValue

        End Function

    End Class

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

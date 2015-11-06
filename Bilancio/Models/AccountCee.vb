Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports Bilancio.DAL


Namespace Models

    Public Enum NodeType As Integer
        ROOT = 0
        PATRIMONIALE = 1
        ECONOMICO = 2
        ATTIVO = 3
        PASSIVO = 4
        COSTI = 5
        RICAVI = 6
        ALTRO = 7
    End Enum


    Public Class AccountCee
        Implements IValidatableObject

        Dim _Active As Boolean = True
        Dim _SeqNo As Integer = 0
        Dim _creditDebit As Bilancio.CreditDebit

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
        <Display(Name:="Segno")>
        Public Property Debit As DareAvere = DareAvere.Dare

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

        Public ReadOnly Property creditDebit As CreditDebit
            Get
                Return _creditDebit
            End Get
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

            Try
                Return db.AccountCees().Where(Function(p) p.NodeType = nodeType).Take(1).First
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        'Lista di nodi ottenuta dall'attraversamento dell'albero a partire dal nodo corrente compreso
        'return lista con tutti i figli a tutti i livelli del nodo corrente
        Function getAllSons(Optional onlyLeaves As Boolean = False) As List(Of AccountCee)

            Dim retValue As List(Of AccountCee) = New List(Of AccountCee)

            Dim traverse As Action(Of AccountCee) = Sub(node As AccountCee)
                                                        If (Not IsNothing(node)) Then
                                                            If (Not onlyLeaves Or Not node.Summary) Then
                                                                retValue.Add(node)
                                                            End If
                                                            'If (node.Sons.Count > 0) Then
                                                            '    node.Sons.ToList.ForEach(Sub(s) traverse(s))
                                                            'End If
                                                            node.Sons.ToList.ForEach(Sub(s) traverse(s))
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


        'return true se il conto è dare
        Public Function getAncestorDebit() As DareAvere
            Return getAncestor().Debit
        End Function


        'return il primo conto avo con docType diverso da ALTRO, quindi (attivo, passivo, costo, ricavo)
        Function getAncestor() As AccountCee
            Dim currentNode = Me

            While currentNode.ParentID > 0 AndAlso currentNode.NodeType = Models.NodeType.ALTRO
                currentNode = currentNode.Parent
            End While

            Return currentNode

        End Function

        Public Function getRootNode() As AccountCee
            If (Not IsNothing(Parent)) Then
                Return Parent.getRootNode() 'ricorsione
            Else
                Return Me
            End If

        End Function

        Public Function isLeaf()
            Return Sons.Count = 0
        End Function

        '
        ' calcola il totale dare e avere per anno e anno-1 sulla base dei documenti, solo per le foglie.
        '
        Private Sub calculateCreditDebit(year As Integer)

            If (Not isLeaf()) Then
                Return
            End If

            _creditDebit = New CreditDebit(year)

            'accumulo dei valori dei conti (AccountChart) appartenenti a questo conto cee
            AccountCharts.ToList().ForEach(Sub(a) add(year, a.calculateCreditDebit(year)))

            'If (Not IsNothing(Parent) AndAlso Not _creditDebit.isEmpty()) Then
            '    'log.info "${this.toString()} ${parent.toString()}"
            '    addOnParents(year, Parent, _creditDebit)
            'End If

            'Return _creditDebit

        End Sub

        Private Sub addOnParents(year As Integer, c As AccountCee)
            addOnParents(year, c.Parent, c.creditDebit)
        End Sub
        'accumulo sui parent
        Private Sub addOnParents(year As Integer, p As AccountCee, cd As CreditDebit)
            If (Not IsNothing(p) AndAlso Not cd.isEmpty()) Then
                p.add(year, cd)
                addOnParents(year, p.Parent, cd)
            End If
        End Sub

        Private Sub add(year As Integer, cd As Bilancio.CreditDebit)
            If (IsNothing(_creditDebit)) Then
                _creditDebit = New CreditDebit(year)
            End If
            _creditDebit.add(cd)
        End Sub


        Private Function isLastBrother() As Boolean
            Return IsNothing(Parent) OrElse Parent.Sons.Last.ID = Me.ID
        End Function

        'return lista di conti cee (summary compresi) con il valore da documenti CreditDebit
        Public Function getBalance(Optional year As Integer = 0) As List(Of AccountCee)

            If (year <= 0) Then
                year = Now.Year
            End If

            Dim retValue = New List(Of AccountCee)

            getAllSons().ForEach(Sub(n)
                                     If (n.isLeaf) Then
                                         n.calculateCreditDebit(year)
                                         addOnParents(year, n)
                                     End If

                                     retValue.Add(n)    'conto

                                     If (n.isLeaf AndAlso n.isLastBrother()) Then
                                         Dim parentsTotal As List(Of AccountCee) = n.getParentsTotal(ID)    ''righe totali
                                         parentsTotal.ForEach(Sub(pt) retValue.Add(pt)) ''accoda i nodi total per cui il nodo corrente è l'ultimo figlio
                                     End If

                                 End Sub)

            Return retValue

        End Function

        ' solo x foglie e ultimo nodo di un gruppo di fratelli si verifica il parent e vengono inclusi i parent di tipo total
        ' @param maxParentId ultimo nodo ID (compreso) dei parent. Nodo oltre il quale non si deve andare
        ' @return lista dei parent per cui va fatto il totale
        Private Function getParentsTotal(Optional maxParentId As Integer = 0) As IList(Of AccountCee)

            Dim retValue = New List(Of AccountCee)

            If (isLeaf() AndAlso isLastBrother()) Then

                Dim climb As Action(Of AccountCee, Integer)

                climb = Sub(node As AccountCee, level As Integer)
                            If (Not IsNothing(node)) Then

                                If (node.Total) Then
                                    'node.level = level
                                    retValue.Add(node)
                                End If

                                If (node.isLastBrother() AndAlso maxParentId <> node.ID) Then
                                    climb(node.Parent, (level - 1))
                                End If
                            End If
                        End Sub

                climb(Parent, -1)

            End If

            Return retValue

        End Function


        '  /**
        '	*
        '	* @return lista di conti cee (summary compresi) con il valore da documenti CreditDebit
        '	*/
        '	List<BilaRow> getBalance(int year) {
        '		def retValue = []

        '		List<AccountCee> allSons = getAllSons()

        '		allSons.each { c -> 
        '			CreditDebit cd = c.creditDebit(year)
        '//			println("conto: ${c} saldo: ${c.creditDebit}")
        '			BilaRow br = new BilaRow(
        '				accountCee: c, creditDebit: cd, 
        '				nodeType: this.nodeType, level: c.level, code:c.code, description:c.description, summary:c.summary, total:c.total,
        '				amountYear:cd.balanceYear, amountYearPre:cd.balanceYearPrev, amountDelta: cd.balanceYear-cd.balanceYearPrev
        '				);
        '			retValue << br

        '			List<AccountCee> parentsTotal = c.getParentsTotal(this)	//righe totali

        '			parentsTotal.each{ pt ->
        '				cd = pt.creditDebit
        '				br = new BilaRow(
        '					accountCee: pt, creditDebit: cd, 
        '					nodeType: this.nodeType, level: pt.level, code:pt.code, description:pt.description, summary:pt.summary, total:pt.total
        '					,amountYear:cd.balanceYear, amountYearPre:cd.balanceYearPrev, amountDelta: cd?.balanceYear-cd?.balanceYearPrev
        '					);

        '				retValue << br
        '			}
        '		}

        '		retValue.each { br -> 
        '			CreditDebit cd = br.creditDebit
        '			if (cd) {
        '				br.amountYear = cd.balanceYear
        '				br.amountYearPre = cd.balanceYearPrev
        '				br.amountDelta = cd.balanceYear-cd.balanceYearPrev
        '			}
        '		}

        '		return retValue
        '	}

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

Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
'Imports Bilancio.Models.AccountCee

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub TestMethod1()
        Trace.Write("cc")
    End Sub


    Sub TestFirst()
        ' Data source.
        'Dim numbers() As Integer = {0, 1, 2, 3, 4, 5, 6}

        '' Query creation.
        'Dim evensQuery = From num In numbers
        '                 Where num Mod 2 = 0
        '                 Select num

        '' Query execution.
        'For Each number In evensQuery
        '    Trace.Write(number & " ")
        'Next


        'Dim db As New DataContext("C:\Northwind\Northwnd.mdf")
        Dim db As New BilaTest

        ' Query creation.
        'Dim docTypes = From num In db.DocumentType
        '                 Where num.Code = "acqFat"   'acqFat	Fattura di Acquisto     acqRic	Ricevuta di Acquisto

        '' Query execution.
        'For Each number In docTypes
        '    Trace.WriteLine(number.ToString)
        'Next


        '       	static List<AccountCee> parentList() {

        '	return AccountCee.createCriteria().list{
        '		eq('summary', true)
        '		eq('active', true)
        '	}
        '}

        'Dim all As List(Of AccountCee) = db.AccountCee.ToList

        'Dim sorted = all.OrderBy(Function(x) x.Code)

        'Dim parents As List(Of AccountCee) = New List(Of AccountCee)
        'sorted.ToList.ForEach(Function(x)
        '                          If (x.Summary = True) Then
        '                              parents.Add(x)
        '                          End If
        '                          Return True
        '                      End Function)


        'Dim parents As List(Of AccountCee) = (From c In db.AccountCee
        '    Where c.Summary = True
        '    Order By c.Code()).ToList()


        Dim query = From c In db.AccountCee
            Where c.Summary = True And Not c.NodeType = 7
            Order By c.Code


        'Dim query = _
        'From order In orders _
        'Where order.OrderQty > orderQtyMin And order.OrderQty < orderQtyMax _
        'Select New With { _
        '    .SalesOrderID = order.SalesOrderID, _
        '    .OrderQty = order.OrderQty _
        '}

        Dim parents As List(Of AccountCee) = query.ToList()

        For Each row In parents
            Trace.WriteLine("code: " + row.Code + " name: " + row.Name + " Summary: " + row.Summary.ToString)
        Next


        'Dim all As List(Of AccountCee) = db.AccountCee.OrderBy(Function(a) a.Parent_ID).ToList()
        'For Each row In all
        '    Trace.WriteLine("code: " + row.Code + " name: " + row.Name + " attivo: " + row.Parent_ID)
        'Next



        'Dim dtna = From cust In db.DocumentType
        '           Let Discount = cust.Name + " 0.1"
        'Join doc In db.Document
        '          On cust.ID Equals doc.DocumentType_ID
        'Where cust.Code = "AcqFat"
        '        Order By cust.Code
        '        Select cust.Code, cust.Name, cust.Active, Discount

        'For Each row In dtna
        '    Trace.WriteLine("code: " + row.Code + " name: " + row.Name + " attivo: " + row.Active.ToString + "      colonna calcolata: " + row.Discount)
        'Next


        'Dim query = From c In db.DocumentType
        '        Group Join o In db.Document On c.ID Equals o.DocumentType_ID Into Group _
        '        From o In Group.DefaultIfEmpty() _
        '        Where c.Code = "AcqFat"
        '        Select Id = c.ID, DocType_ID = If(o.DocumentType_ID = Nothing, Nothing, o.DocumentType_ID)

        'For Each row In query
        '    Trace.WriteLine("id: " + row.Id + " DocType_ID: " + row.DocType_ID)
        'Next


        '    Dim query = From dts In db.DocumentType
        '    From doc In db.Document
        '    .Where(v => v.Id == order.VendorId)
        '    .DefaultIfEmpty()
        '    From status
        '    In dc.Status()
        '    .Where(s => s.Id == order.StatusId)
        '    .DefaultIfEmpty()
        'select new { Order = order, Vendor = vendor, Status = status } 
        '//Vendor and Status properties will be null if the left join is null


        'Dim docType As List(Of DocumentType) = db.DocumentType.ToList()

        'For Each row In docType
        '    Trace.WriteLine(row.ToString)
        'Next

        '' Create a data source from an XML document.
        'Dim contacts = XElement.Load("c:\books.xml")
        'Console.Write(contacts)


    End Sub


    <TestMethod()> Public Sub TestSelAccountCee()

        Dim db As New BilaTest    'BilancioContext

        Try
            Dim contiCee As List(Of AccountCee) = db.AccountCee.ToList()

            viewList(contiCee)

        Catch ex As Exception
            Stop
        End Try

        'Dim xx As List(Of AccountChart) = db.AccountCee.Select(Function(q) New AccountChart With {.Code = q.Code, .Name = q.Name, .Active = q.Active}).ToList()


        'Trace.WriteLine(xx.ToString())


        'Dim query = From p In db.AccountCees Select p

        'Dim summary = _
        '    query.Where(Function(p) p.Summary = True).Where(Function(p) p.Code = "B2")

        'Trace.WriteLine("Summary:")
        'For Each conto In summary

        '    Dim aa As StringBuilder = New StringBuilder()
        '    aa.Append("conto: ").Append(conto.Code).Append("name: ").Append(conto.Name)
        '    Trace.WriteLine(aa.ToString)
        'Next
        ''End Using

    End Sub

    Private Sub viewList(contiCee As List(Of AccountCee))
        For Each row In contiCee
            Trace.WriteLine(row.ToString)
        Next

    End Sub

    Sub TestLoadChart()

        Dim context As New BilaTest    'BilancioContext

        '        Dim pcs As List(Of AccountChart)


        Dim q = From c In context.AccountCee
            Where c.Summary = False And c.NodeType = 7
            Order By c.Code, c.Name()
        '            Select New AccountChart() With {.AccountCee = c, .Code = c.Code, .Name = c.Name}

        'q.ToList().ForEach(Sub(a)
        '                       Dim ac As AccountChart = New AccountChart() With {.AccountCee = a, .Code = a.Code, .Name = a.Name}
        '                       Trace.WriteLine(ac)
        '                   End Sub)
        q.ToList().ForEach(Sub(a) Console.WriteLine(a.Code))

        'Try
        '    pcs = (From c In context.AccountCee
        '    Where c.Summary = False And c.NodeType = 7
        '            Order By c.Code, c.Name()
        '            Select New AccountChart() With {.AccountCee = c, .Code = c.Code, .Name = c.Name}
        '            ).ToList()

        'Catch ex As Exception
        '    Trace.WriteLine(ex.Message)
        'End Try


        'pcs.ForEach(Sub(s)
        '                '                        context.AccountChart.Add(s)
        '                Trace.WriteLine(s)
        '            End Sub
        '                )


    End Sub

    '        pcs.ForEach(Function(s) context.AccountCharts.Add(s))
    '    context.SaveChanges()
    'End Sub

End Class
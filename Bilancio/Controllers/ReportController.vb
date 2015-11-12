Imports System.Data.Entity
Imports Bilancio.Models
Imports Bilancio.DAL
Imports Bilancio.ViewModels

Public Class ReportController
    Inherits System.Web.Mvc.Controller

    Private db As New BilancioContext

    '
    ' GET: /Report/

    Function Index() As ActionResult
        Return View(db.Reports.ToList())
    End Function

    '
    ' GET: /Report/Details/5

    Function Details(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim report As Report = db.Reports.Find(id)
        If IsNothing(report) Then
            Return HttpNotFound()
        End If
        Return View(report)
    End Function

    '
    ' GET: /Report/Create

    Function Create() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Report/Create

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Create(ByVal report As Report) As ActionResult
        If ModelState.IsValid Then
            db.Reports.Add(report)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(report)
    End Function

    '
    ' GET: /Report/Edit/5

    Function Edit(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim report As Report = db.Reports.Find(id)
        If IsNothing(report) Then
            Return HttpNotFound()
        End If
        Return View(report)
    End Function

    '
    ' POST: /Report/Edit/5

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Function Edit(ByVal report As Report) As ActionResult
        If ModelState.IsValid Then
            db.Entry(report).State = EntityState.Modified
            db.SaveChanges()
            Return RedirectToAction("Index")
        End If

        Return View(report)
    End Function

    '
    ' GET: /Report/Delete/5

    Function Delete(Optional ByVal id As Integer = Nothing) As ActionResult
        Dim report As Report = db.Reports.Find(id)
        If IsNothing(report) Then
            Return HttpNotFound()
        End If
        Return View(report)
    End Function

    '
    ' POST: /Report/Delete/5

    <HttpPost()> _
    <ActionName("Delete")> _
    <ValidateAntiForgeryToken()> _
    Function DeleteConfirmed(ByVal id As Integer) As RedirectToRouteResult
        Dim report As Report = db.Reports.Find(id)
        db.Reports.Remove(report)
        db.SaveChanges()
        Return RedirectToAction("Index")
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub


    Function Balance(Optional nodeType As Integer = 0, Optional ByVal year As Integer = 0) As ActionResult

        Dim rootNodeType As Models.NodeType = CType(nodeType, Models.NodeType)

        If (rootNodeType < 0 OrElse rootNodeType >= Models.NodeType.ALTRO) Then
            Return RedirectToAction("Index")    'tipo non valido
        End If

        Dim bal As List(Of CreditDebitAccount) = AccountCee.getNodeFromType(rootNodeType).getBalance(IIf(year > 0, year, Now.Year))

        'bal.ForEach(Sub(a)
        '                Dim cd As String = ""
        '                If (Not IsNothing(a.creditDebit)) Then
        '                    cd = a.creditDebit.toString()
        '                End If
        '                Trace.WriteLine(a.ToString() & cd)
        '            End Sub)


        Return View("viewBalance", bal)

        'Return RedirectToAction("Index")

    End Function


    Private Sub viewReportBalance(bal As List(Of AccountCee))
        ' ReportViewer1.Visible = true;

        ' '//Invoke Stored procedure With Input parameter to it.
        ' '//DataSet dsReport = objSP.GetTable(storedProcedure,txtParameter.Text));
        ' '//Hardcoded Values.
        ' 'IList >Customer< customerList = new List>Customer<();
        ' 'customerList.Add(new Customer(1,"Santosh Poojari"));
        ' 'customerList.Add(new Customer(2, "Santosh Poojari1"));
        ' 'customerList.Add(new Customer(3, "Santosh Poojari2"));

        'ReportParameter[] param = new ReportParameter[1];
        'param[0] = new ReportParameter("Report_Parameter_0",txtParameter.Text);
        'ReportViewer1.LocalReport.SetParameters(param);

        ' ReportDataSource rds = New ReportDataSource
        '    ("DataSet1_Customers_DataTable1", customerList);
        'ReportViewer1.LocalReport.DataSources.Clear();
        'ReportViewer1.LocalReport.DataSources.Add(rds);
        'ReportViewer1.LocalReport.Refresh();
    End Sub


    '        /* serves the report document for download*/
    '    def generateReport() {

    '		if (!params.year) {
    '			params.year = new Date()[Calendar.YEAR]
    '		}

    '		Integer year = Integer.parseInt(params.year)	// ? Integer.parseInt(params.year) : new Date()[Calendar.YEAR]
    '		params.year = year
    '		params.yearPrev = year-1

    '//parametri del report
    '        params.title = "Bilancio"
    '        params.reportId = "Mesis srl via dei Velini 19 Macerata Italy"
    '        params.operationTime = new Date()
    '		//il nome del file di output, il formato (pdf, xls, html) e il nome del file .jrxml viene passato dalla gsp con il tag 

    '		def oneRootNode = {NodeType nt ->
    '			def node = AccountCee.getNodeFromType(nt)
    '			return node?.getBalance(year)
    '		}
    '		List<BilaRow> bilaRows = []

    '		bilaRows += oneRootNode(NodeType.ATTIVO)
    '		bilaRows += oneRootNode(NodeType.PASSIVO)
    '		bilaRows += oneRootNode(NodeType.COSTI)
    '		bilaRows += oneRootNode(NodeType.RICAVI)


    '//		Map result = [:]
    '//        result.data = []
    '//        result.data = << [code:"vivek",description:"yadav"] // from here you can send any type of data  
    '//		
    '//		
    '////        result.data = bilarows	//<< [name:”vivek”,surName:”yadav”] // from here you can send any type of data  
    '//  // what ever you want
    '//        JasperReportDef rep = jasperService.buildReportDefinition(params, request.locale, result)
    '//        ByteArrayOutputStream stream = jasperService.generateReport(rep)
    '//        response.setHeader("Content-disposition", "attachment; filename=" + 'fileName' + ".pdf")
    '//        response.contentType = "application/pdf"
    '//        response.outputStream << stream.toByteArray()

    '		//chiamata al controller jasper
    '        chain(controller:'jasper', action:'index',
    '            model:[data:bilaRows],
    '            params:params)

    '//		render(view:"index")
    '        return false
    '    }


End Class
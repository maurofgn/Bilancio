Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient

' Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WebServiceCode
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function Somma(ByVal num1 As Short, ByVal num2 As Short) As Short
        Return num1 + num2
    End Function


    <WebMethod()> _
    Public Function ExistCode(ByVal tableName As String, ByVal code As String) As Boolean


        '        Dim connectionString As String = "Data Source=(LocalDb)\v11.0;Initial Catalog=Bilancio_1;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\Bilancio.mdf"

        Dim connect As String = "Server=SERVER;Database=Bilancio_1;Trusted_Connection=True;"
        Dim query As String = "SELECT COUNT(*) FROM " + tableName + " WHERE Code = @code"

        Using conn As SqlConnection = New SqlConnection(connect)

            Using cmd As SqlCommand = New SqlCommand(query, conn)

                cmd.Parameters.AddWithValue("Code", code)
                conn.Open()
                Dim count As Integer = CType(cmd.ExecuteScalar(), Integer)
                Return count = 0
            End Using

        End Using

        Return False
    End Function

End Class
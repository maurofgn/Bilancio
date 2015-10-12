Imports System.Data.Entity
Imports System.Data.Entity.SqlServer

Namespace DAL
    Public Class BilancioConfiguration
        Inherits DbConfiguration

        Public Sub New()
            SetExecutionStrategy("System.Data.SqlClient", Function() New SqlAzureExecutionStrategy())   'parametri usati per la resilienza (riprova su assenza temporanea della rete) può ritornare l'eccezione RetryLimitExceededException
        End Sub

    End Class
End Namespace

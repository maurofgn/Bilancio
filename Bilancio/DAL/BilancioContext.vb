Imports Bilancio.Models
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Namespace DAL
    Public Class BilancioContext
        Inherits DbContext

        Public Sub New()
            MyBase.New("Bilancio")
        End Sub

        Public Property Reports As DbSet(Of Report)
        Public Property Aviss As DbSet(Of Avis)
        Public Property AccountCees As DbSet(Of AccountCee)
        Public Property AccountCharts As DbSet(Of AccountChart)
        Public Property DocumentTypes As DbSet(Of DocumentType)
        Public Property Documents As DbSet(Of Document)
        Public Property DocumentRows As DbSet(Of DocumentRow)

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Conventions.Remove(Of PluralizingTableNameConvention)()
        End Sub

    End Class

End Namespace

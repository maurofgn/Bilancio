Imports Bilancio.Models
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.Entity.Validation

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

            'riferimento esplicito al campo parentID self reference
            modelBuilder.Entity(Of AccountCee)() _
                .HasOptional(Function(p) p.Parent) _
                .WithMany(Function(p) p.Sons) _
                .HasForeignKey(Function(p) p.ParentID) _
                .WillCascadeOnDelete(False)

        End Sub


        'Questa funzione viene richiamata da context.SaveChanges() e se questa funzione ritorna result con errori, SaveChanges lancia l'eccezione DbUnexpectedValidationException
        Protected Overrides Function ValidateEntity(
                                                   entityEntry As System.Data.Entity.Infrastructure.DbEntityEntry,
                                                   items As IDictionary(Of Object, Object)) As DbEntityValidationResult

            Dim result = New DbEntityValidationResult(entityEntry, New List(Of DbValidationError)())

            If (TypeOf entityEntry.Entity Is AccountCee AndAlso (entityEntry.State = EntityState.Added Or entityEntry.State = EntityState.Modified)) Then
                Dim record As AccountCee = CType(entityEntry.Entity, AccountCee)
                ''check for uniqueness of AccountCee.code 
                If (AccountCees.Where(Function(p) p.Code = record.Code).Count() > 0) Then

                    result.ValidationErrors.Add(
                            New System.Data.Entity.Validation.DbValidationError("Code", "Codice già esistente"))
                End If
            End If

            If (TypeOf entityEntry.Entity Is AccountChart AndAlso (entityEntry.State = EntityState.Added Or entityEntry.State = EntityState.Modified)) Then
                Dim record As AccountChart = CType(entityEntry.Entity, AccountChart)

                ''check for uniqueness of AccountChart.code 
                If (AccountCharts.Any(Function(p) p.Code = record.Code)) Then
                    result.ValidationErrors.Add(
                            New System.Data.Entity.Validation.DbValidationError("Code", "Codice già esistente"))
                End If

            End If

            If (TypeOf entityEntry.Entity Is DocumentType AndAlso (entityEntry.State = EntityState.Added Or entityEntry.State = EntityState.Modified)) Then
                Dim record As DocumentType = CType(entityEntry.Entity, DocumentType)
                ''check for uniqueness of DocumentType.code
                If (DocumentTypes.Where(Function(p) p.Code = record.Code).Count() > 0) Then

                    result.ValidationErrors.Add(
                            New System.Data.Entity.Validation.DbValidationError("Code", "Codice già esistente"))
                End If
            End If



            If (result.ValidationErrors.Count > 0) Then
                Return result
            Else
                Return MyBase.ValidateEntity(entityEntry, items)
            End If

        End Function

    End Class

End Namespace

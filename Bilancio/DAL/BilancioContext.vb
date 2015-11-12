Imports Bilancio.Models
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.Entity.Validation
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core.Objects
Imports System.IO
Imports System.Data.Entity.Core.Objects.DataClasses

'Imports System
'Imports System.Xml
'Imports System.Xml.Linq
'Imports System.Runtime.Serialization

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
                If (AccountCees.Where(Function(p) p.Code = record.Code And p.ID <> record.ID).Count() > 0) Then

                    result.ValidationErrors.Add(
                            New System.Data.Entity.Validation.DbValidationError("Code", "Codice già esistente"))
                End If
            End If

            If (TypeOf entityEntry.Entity Is AccountChart AndAlso (entityEntry.State = EntityState.Added Or entityEntry.State = EntityState.Modified)) Then
                Dim record As AccountChart = CType(entityEntry.Entity, AccountChart)

                ''check for uniqueness of AccountChart.code 
                If (AccountCharts.Any(Function(p) p.Code = record.Code And p.ID <> record.ID)) Then
                    result.ValidationErrors.Add(
                            New System.Data.Entity.Validation.DbValidationError("Code", "Codice già esistente"))
                End If

            End If

            If (TypeOf entityEntry.Entity Is DocumentType AndAlso (entityEntry.State = EntityState.Added Or entityEntry.State = EntityState.Modified)) Then
                Dim record As DocumentType = CType(entityEntry.Entity, DocumentType)
                ''check for uniqueness of DocumentType.code
                If (DocumentTypes.Where(Function(p) p.Code = record.Code And p.ID <> record.ID).Count() > 0) Then

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

        '
        'per le entità modificate/create assegna la data ultimo aggiornamento, se l'entità è da creare assegna anche la data di creazione
        '
        '
        Public Overrides Function SaveChanges() As Integer

            'AutoDetectChangesEnabled()

            Dim context As Core.Objects.ObjectContext = CType(Me, IObjectContextAdapter).ObjectContext

            ''Find all Entities that are Added/Modified that inherit from my EntityBase
            Dim objectStateEntries As IEnumerable(Of ObjectStateEntry) =
                From e In context.ObjectStateManager.GetObjectStateEntries(EntityState.Added Or EntityState.Modified)
                Where
                    e.IsRelationship = False And
                    Not IsNothing(e.Entity) And
                    GetType(EntityBase).IsAssignableFrom(e.Entity.GetType())
                Select e

            Dim currentTime = DateTime.Now

            For Each entry As ObjectStateEntry In objectStateEntries

                Dim entityBase As EntityBase = entry.Entity

                If (entry.State = EntityState.Added) Then
                    entityBase.dateCreated = currentTime
                    entityBase.lastUpdate = currentTime
                ElseIf (entry.State = EntityState.Modified) Then

                    ' ''capire se esiste almeno un campo modificato
                    ' ''Ho fatto diverse prove, ma non sono riuscito a capire quali campi sono stati effettivamente modificati
                    ' ''Sarebbe opportuno capire se c'è almeno una modifica in modo da aggiornare la data modifica solo in questo caso

                    'Dim aa As IEnumerable(Of String) = entry.GetModifiedProperties.Where(Function(modifiedPro) entry.IsPropertyChanged(modifiedPro))
                    'Dim almostOneUpdatedField = entry.GetModifiedProperties.Where(Function(modifiedPro) entry.IsPropertyChanged(modifiedPro)).Count > 0
                    'For Each modifiedPro In entry.GetModifiedProperties
                    '    Trace.WriteLine(String.Format("nome campo: {0} valore: {1}", modifiedPro, entry.IsPropertyChanged(modifiedPro)))
                    '    '_db.ObjectStateManager.GetObjectStateEntry(employee).SetModifiedProperty(modifiedPro)
                    'Next
                    'If (almostOneUpdatedField) Then
                    '    entityBase.lastUpdate = currentTime
                    'End If

                    entityBase.lastUpdate = currentTime
                End If
            Next

            Return MyBase.SaveChanges()

        End Function


        Private Function GetEntryValueInString(entry As ObjectStateEntry, isOrginal As Boolean) As String

            If TypeOf entry.Entity Is EntityObject Then

            End If
            '{
            '    object target = CloneEntity((EntityObject)entry.Entity);
            '    foreach (string propName in entry.GetModifiedProperties())
            '    {
            '        object setterValue = null;
            '        If (isOrginal) Then
            '        {
            '            //Get orginal value 
            '            setterValue = entry.OriginalValues[propName];
            '        }
            '        Else
            '        {
            '            //Get orginal value 
            '            setterValue = entry.CurrentValues[propName];
            '        }
            '        //Find property to update 
            '        PropertyInfo propInfo = target.GetType().GetProperty(propName);
            '        //update property with orgibal value 
            '        if (setterValue == DBNull.Value)
            '        {//
            '            setterValue = null;
            '        }
            '        propInfo.SetValue(target, setterValue, null);
            '    }//end foreach

            '    XmlSerializer formatter = new XmlSerializer(target.GetType());
            '    XDocument document = new XDocument();

            '    using (XmlWriter xmlWriter = document.CreateWriter())
            '    {
            '        formatter.Serialize(xmlWriter, target);
            '    }
            '    return document.Root.ToString();
            '}

            Return Nothing

        End Function

        'Public Function CloneEntity(obj As EntityObject) As EntityObject

        '    '            Dim dcSer = New System.Runtime.Serialization.DataContractSerializer(obj.GetType())

        '    Dim dcSer As System.Runtime.Serialization.DataContractSerializer = New System.Runtime.Serialization.DataContractSerializer(GetType(obj))

        '    Dim memoryStream = New MemoryStream()

        '    dcSer.WriteObject(memoryStream, obj)
        '    memoryStream.Position = 0

        '    Dim newObject As EntityObject = CType(dcSer.ReadObject(memoryStream), EntityObject)  ' (EntityObject)dcSer.ReadObject(memoryStream);
        '    Return newObject

        'End Function

    End Class

    Public Interface EntityBase

        Property dateCreated As DateTime
        Property lastUpdate As DateTime

    End Interface

End Namespace

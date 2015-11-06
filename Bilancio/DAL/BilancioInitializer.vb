Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.Entity
Imports Bilancio.Models

Namespace DAL
    Public Class BilancioInitializer

        Inherits DropCreateDatabaseIfModelChanges(Of BilancioContext)
        Protected Overrides Sub Seed(ByVal context As BilancioContext)

            'context.Database.ExecuteSqlCommand("alter table AccountCee add constraint CeeUniqueCode unique (Code)")
            'context.Database.ExecuteSqlCommand("alter table AccountChart add constraint ChartUniqueCode unique (Code)")

            context.Aviss.Add(New Avis() With {
                    .Name = "Comunale Morrovalle",
                    .Address = "piazza Vittorio Emanuele II n.12",
                    .City = "Morrovalle",
                    .PostalCode = "62010",
                    .Region = "MC",
                    .Email = "morrovalle.comunale@avis.it",
                    .Phone = "0733/222405",
                    .ContactName = "Dott. Signorini Mario"
                })
            context.SaveChanges()

            Dim docTypes = New List(Of DocumentType)() From {
                New DocumentType() With {.Code = "acqFat", .Name = "Fattura di Acquisto"},
                New DocumentType() With {.Code = "acqRic", .Name = "Ricevuta di Acquisto"},
                New DocumentType() With {.Code = "venFat", .Name = "Fattura di Vendita"},
                New DocumentType() With {.Code = "venRic", .Name = "Ricevuta di Vendita"},
                New DocumentType() With {.Code = "cassaUscita", .Name = "Uscita di cassa"},
                New DocumentType() With {.Code = "cassaEntrata", .Name = "Entrata di cassa"},
                New DocumentType() With {.Code = "bancaAddebito", .Name = "Addebito bancario"},
                New DocumentType() With {.Code = "bancaAccredito", .Name = "Accredito bancario"},
                New DocumentType() With {.Code = "giro", .Name = "Giroconti"}
            }
            docTypes.ForEach(Function(s) context.DocumentTypes.Add(s))
            context.SaveChanges()

            Dim root = New AccountCee() With {.NodeType = NodeType.ROOT, .Code = "Root", .Name = "Root", .SeqNo = 0, .Summary = False, .Total = False}
            context.AccountCees.Add(root)
            context.SaveChanges()

            Dim patriEco = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.PATRIMONIALE, .Code = "Patri", .Name = "Patrimoniale", .SeqNo = 10, .Summary = False, .Total = True, .Parent = root},
                New AccountCee() With {.NodeType = NodeType.ECONOMICO, .Code = "Econo", .Name = "Economico", .SeqNo = 20, .Summary = False, .Total = True, .Parent = root}
            }
            patriEco.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            Dim apcrList = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ATTIVO, .Code = "Attivo", .Debit = DareAvere.Dare, .Name = "Attivo", .SeqNo = 10, .Summary = True, .Total = True, .Parent = (patriEco.[Single](Function(s) s.Code = "Patri"))},
                New AccountCee() With {.NodeType = NodeType.PASSIVO, .Code = "Passivo", .Debit = DareAvere.Avere, .Name = "Passivo", .SeqNo = 20, .Summary = True, .Total = True, .Parent = (patriEco.[Single](Function(s) s.Code = "Patri"))},
                New AccountCee() With {.NodeType = NodeType.COSTI, .Code = "Costi", .Debit = DareAvere.Dare, .Name = "Costi", .SeqNo = 10, .Summary = True, .Total = True, .Parent = (patriEco.[Single](Function(s) s.Code = "Econo"))},
                New AccountCee() With {.NodeType = NodeType.RICAVI, .Code = "Ricavi", .Debit = DareAvere.Avere, .Name = "Ricavi", .SeqNo = 20, .Summary = True, .Total = True, .Parent = (patriEco.[Single](Function(s) s.Code = "Econo"))}
                }

            apcrList.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            '--Select per creare le righe delle liste da sql
            'SELECT
            'concat(
            ''New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "',c.code,'", .Name = "',c.description,'", .SeqNo = ',c.seq_no ,', .Summary = ', case when c.summary then 'True' else 'False' end,', .Total = ',case when c.total then 'True' else 'False' end,', .Parent = (ricaviList2.[Single](Function(s) s.Code = "',p1.code,'"))},' 
            ') a,
            'c.code, c.description, c.seq_no, c.summary, c.total,p1.code p_code
            'FROM 
            'account_cee c 
            'inner join account_cee p1 on p1.id = c.parent_id
            'left join account_cee p2 on p2.id = p1.parent_id
            'left join account_cee p3 on p3.id = p2.parent_id
            'left join account_cee p4 on p4.id = p3.parent_id
            'where 
            'p3.code = 'Attivo'
            'order by c.summary desc, p1.seq_no, c.seq_no, c.code
            'Attivo
            Dim attivoList = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "A", .Debit = DareAvere.Dare, .Name = "Crediti verso soci per versamento quote", .SeqNo = 10, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Attivo"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B", .Debit = DareAvere.Dare, .Name = "Immobilizzazioni", .SeqNo = 20, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Attivo"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C", .Debit = DareAvere.Dare, .Name = "Attivo circolante", .SeqNo = 30, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Attivo"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "D", .Debit = DareAvere.Dare, .Name = "Ratei e risconti attivi", .SeqNo = 50, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Attivo"))}
            }
            attivoList.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            Dim attivoList1 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B1", .Debit = DareAvere.Dare, .Name = "Immobilizzazioni immateriali", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "B"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B2", .Debit = DareAvere.Dare, .Name = "Immobilizzazioni materiali", .SeqNo = 20, .Summary = True, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "B"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B3", .Debit = DareAvere.Dare, .Name = "Immobilizzazioni finanziarie", .SeqNo = 30, .Summary = True, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "B"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C1", .Debit = DareAvere.Dare, .Name = "Rimanenze", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "C"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C2", .Debit = DareAvere.Dare, .Name = "Crediti", .SeqNo = 20, .Summary = True, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "C"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C3", .Debit = DareAvere.Dare, .Name = "Attività finanziarie che non costituiscono immobilizzazioni", .SeqNo = 30, .Summary = True, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "C"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C4", .Debit = DareAvere.Dare, .Name = "Disponibilità liquide", .SeqNo = 40, .Summary = True, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "C"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "D11", .Debit = DareAvere.Dare, .Name = "Ratei attivi", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "D"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "D12", .Debit = DareAvere.Dare, .Name = "Risconti attivi", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (attivoList.[Single](Function(s) s.Code = "D"))}
            }
            attivoList1.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            Dim attivoList2 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B11", .Debit = DareAvere.Dare, .Name = "Spese costituzione e modifiche statutarie", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C11", .Debit = DareAvere.Dare, .Name = "materiale per benemerenze", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B12", .Debit = DareAvere.Dare, .Name = "Costi di ricerca, sviluppo e pubblicità", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C12", .Debit = DareAvere.Dare, .Name = "materiale per la propaganda", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B13", .Debit = DareAvere.Dare, .Name = "Spese manutenzioni straordinarie da ammortizzare", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C13", .Debit = DareAvere.Dare, .Name = "materiale acquistato per attività di fund raising", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B14", .Debit = DareAvere.Dare, .Name = "Oneri pluriennali", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C14", .Debit = DareAvere.Dare, .Name = "materiale donato da terzi per attività di fund raising", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B15", .Debit = DareAvere.Dare, .Name = "Altre immobilizzazioni immateriali", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C15", .Debit = DareAvere.Dare, .Name = "altro materiale", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C16", .Debit = DareAvere.Dare, .Name = "lavorazioni in corso ed acconti", .SeqNo = 60, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B21", .Debit = DareAvere.Dare, .Name = "Terreni e fabbricati", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C21", .Debit = DareAvere.Dare, .Name = "Crediti per rimborsi su donazioni effettuate", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B22", .Debit = DareAvere.Dare, .Name = "Impianti ed attrezzature per la donazione", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C22", .Debit = DareAvere.Dare, .Name = "Crediti per liberalità da ricevere", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B23", .Debit = DareAvere.Dare, .Name = "Mobili e macchine d'ufficio", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C23", .Debit = DareAvere.Dare, .Name = "Crediti verso altre Avis", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B24", .Debit = DareAvere.Dare, .Name = "Altri beni", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C24", .Debit = DareAvere.Dare, .Name = "Crediti per contributi da ""cinque per mille""", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B25", .Debit = DareAvere.Dare, .Name = "Immobilizzazioni materiali in corso ed acconti", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C25", .Debit = DareAvere.Dare, .Name = "Crediti tributari", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C26", .Debit = DareAvere.Dare, .Name = "Crediti verso altri", .SeqNo = 60, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B31", .Debit = DareAvere.Dare, .Name = "Partecipazioni", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C31", .Debit = DareAvere.Dare, .Name = "Partecipazioni", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B32", .Debit = DareAvere.Dare, .Name = "Crediti (che costituiscono immobilizz.) v/altre Avis", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C32", .Debit = DareAvere.Dare, .Name = "Investimenti finanziari", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B33", .Debit = DareAvere.Dare, .Name = "Crediti (che costituiscono immobilizz.) v/altri soggetti", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C33", .Debit = DareAvere.Dare, .Name = "Altri titoli", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B34", .Debit = DareAvere.Dare, .Name = "Investimenti finanziari pluriennali", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "B35", .Debit = DareAvere.Dare, .Name = "Altri titoli (che costituiscono immobilizzazioni)", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "B3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C41", .Debit = DareAvere.Dare, .Name = "Depositi bancari", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C4"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C42", .Debit = DareAvere.Dare, .Name = "Conto corrente postale", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C4"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C43", .Debit = DareAvere.Dare, .Name = "Assegni e titoli di credito a vista", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C4"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "C44", .Debit = DareAvere.Dare, .Name = "Denaro contante e valori in cassa", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (attivoList1.[Single](Function(s) s.Code = "C4"))}
            }
            attivoList2.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()



            'Passivo
            Dim passivoList = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E", .Debit = DareAvere.Avere, .Name = "Patrimonio netto", .SeqNo = 10, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Passivo"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "F", .Debit = DareAvere.Avere, .Name = "Fondi per rischi ed oneri", .SeqNo = 20, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Passivo"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "G", .Debit = DareAvere.Avere, .Name = "T.F.R. lavoro subordinato", .SeqNo = 30, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Passivo"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H", .Debit = DareAvere.Avere, .Name = "Debiti", .SeqNo = 40, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Passivo"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "K", .Debit = DareAvere.Avere, .Name = "Ratei e risconti passivi", .SeqNo = 50, .Summary = True, .Total = True, .Parent = (apcrList.[Single](Function(s) s.Code = "Passivo"))}
            }
            passivoList.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            Dim passivoList1 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E1", .Debit = DareAvere.Avere, .Name = "Patrimonio libero", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "E"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E2", .Debit = DareAvere.Avere, .Name = "Fondo di dotazione (se previsto)", .SeqNo = 20, .Summary = True, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "E"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E3", .Debit = DareAvere.Avere, .Name = "Patrimonio vincolato", .SeqNo = 30, .Summary = True, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "E"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "F11", .Debit = DareAvere.Avere, .Name = "Fondi per trattamenti di quiescenza e simili", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "F"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "F12", .Debit = DareAvere.Avere, .Name = "Altri fondi di accantonamento", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "F"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "G11", .Debit = DareAvere.Avere, .Name = "Trattamento di fine rapporto di lavoro subordinato", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "G"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H11", .Debit = DareAvere.Avere, .Name = "Debiti per contributi da erogare", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H12", .Debit = DareAvere.Avere, .Name = "Debiti verso banche", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H13", .Debit = DareAvere.Avere, .Name = "Debiti verso altri finanziatori", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H14", .Debit = DareAvere.Avere, .Name = "Debiti verso fornitori", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H15", .Debit = DareAvere.Avere, .Name = "Debiti tributari", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H16", .Debit = DareAvere.Avere, .Name = "Debiti verso istituti di previdenza", .SeqNo = 60, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H17", .Debit = DareAvere.Avere, .Name = "Debiti verso altre Avis", .SeqNo = 70, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H18", .Debit = DareAvere.Avere, .Name = "Debiti per rimborsi spese nei confronti di soci volontari", .SeqNo = 80, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H19", .Debit = DareAvere.Avere, .Name = "Acconti ricevuti", .SeqNo = 90, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "H20", .Debit = DareAvere.Avere, .Name = "Altri debiti", .SeqNo = 100, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "H"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "K11", .Debit = DareAvere.Avere, .Name = "Ratei passivi", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "K"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "K12", .Debit = DareAvere.Avere, .Name = "Risconti passivi", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (passivoList.[Single](Function(s) s.Code = "K"))}
            }
            passivoList1.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()


            Dim passivoList2 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E11", .Debit = DareAvere.Avere, .Name = "Avanzo o disavanzo di gestione dell'esercizio", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E12", .Debit = DareAvere.Avere, .Name = "Riserve accantonate di esercizi precedenti", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E13", .Debit = DareAvere.Avere, .Name = "Contributi in conto capitalenon vincolati", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E14", .Debit = DareAvere.Avere, .Name = "Rivalutazioni di beni patrimoniali", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E1"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E21", .Debit = DareAvere.Avere, .Name = "Fondo di dotazione", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E2"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E31", .Debit = DareAvere.Avere, .Name = "Fondi vincolati destinati da terzi", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E32", .Debit = DareAvere.Avere, .Name = "Fondi vincolati per decisione di organi istituzionali", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E33", .Debit = DareAvere.Avere, .Name = "Contributi in conto capitale vincolati da  terzi", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E34", .Debit = DareAvere.Avere, .Name = "Riserve statutarie (se previste)", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E3"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "E35", .Debit = DareAvere.Avere, .Name = "Accantonamenti vincolati per scopi specifici", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (passivoList1.[Single](Function(s) s.Code = "E3"))}
            }
            passivoList2.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            'Costi
            Dim costiList1 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I", .Debit = DareAvere.Dare, .Name = "Oneri e spese da attività istituzionale", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Costi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "J", .Debit = DareAvere.Dare, .Name = "Oneri e spese per attività di raccolta fondi", .SeqNo = 20, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Costi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "M", .Debit = DareAvere.Dare, .Name = "Oneri e spese per attività accessorie", .SeqNo = 30, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Costi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "N", .Debit = DareAvere.Dare, .Name = "Oneri e spese finanziarie e patrimoniali", .SeqNo = 40, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Costi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "O", .Debit = DareAvere.Dare, .Name = "Oneri straordinari", .SeqNo = 50, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Costi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P", .Debit = DareAvere.Dare, .Name = "Oneri e spese di carattere generale ed indivisibile", .SeqNo = 60, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Costi"))}
            }
            costiList1.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            Dim costiList2 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.11", .Debit = DareAvere.Dare, .Name = "Acquisti", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "I"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12", .Debit = DareAvere.Dare, .Name = "Servizi", .SeqNo = 20, .Summary = True, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "I"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.13", .Debit = DareAvere.Dare, .Name = "Godimento beni di terzi", .SeqNo = 30, .Summary = True, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "I"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "N.11", .Debit = DareAvere.Dare, .Name = "Oneri da depositi bancari", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "N"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.11", .Debit = DareAvere.Dare, .Name = "Acquisti", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "P"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12", .Debit = DareAvere.Dare, .Name = "Servizi", .SeqNo = 20, .Summary = True, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "P"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.13", .Debit = DareAvere.Dare, .Name = "Godimento beni di terzi di supporto generale", .SeqNo = 30, .Summary = True, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "P"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "J.11", .Debit = DareAvere.Dare, .Name = "Oneri per attività 1", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "J"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "J.12", .Debit = DareAvere.Dare, .Name = "Oneri per attività 2", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "J"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "J.13", .Debit = DareAvere.Dare, .Name = "Oneri per attività 3", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "J"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "J.14", .Debit = DareAvere.Dare, .Name = "Oneri per attività ordinaria di raccolta fondi", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "J"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "M.11", .Debit = DareAvere.Dare, .Name = "Acquisti", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "M"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "M.12", .Debit = DareAvere.Dare, .Name = "Servizi", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "M"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "M.13", .Debit = DareAvere.Dare, .Name = "Personale dipendente e collaboratori autonomi", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "M"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "M.14", .Debit = DareAvere.Dare, .Name = "Godimento beni di terzi", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "M"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "M.15", .Debit = DareAvere.Dare, .Name = "Ammortamenti immobilizzaz. attività accessorie", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "M"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "M.16", .Debit = DareAvere.Dare, .Name = "Oneri diversi di gestione accessoria", .SeqNo = 60, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "M"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "N.12", .Debit = DareAvere.Dare, .Name = "Interessi ed oneri su altri prestiti", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "N"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "N.13", .Debit = DareAvere.Dare, .Name = "Oneri da patrimonio edilizio", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "N"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "N.14", .Debit = DareAvere.Dare, .Name = "Oneri da altri beni patrimoniali", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "N"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "O.11", .Debit = DareAvere.Dare, .Name = "Oneri da attività finanziarie", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "O"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "O.12", .Debit = DareAvere.Dare, .Name = "Oneri da attività immobiliari", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "O"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "O.13", .Debit = DareAvere.Dare, .Name = "Oneri da altre attività", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList1.[Single](Function(s) s.Code = "O"))}
            }
            costiList2.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            Dim costiList3 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.11.1", .Debit = DareAvere.Dare, .Name = "Materiale sanitario per raccolta", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "N.11.1", .Debit = DareAvere.Dare, .Name = "Interessi passivi", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "N.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.11.1", .Debit = DareAvere.Dare, .Name = "Cancelleria", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.11.2", .Debit = DareAvere.Dare, .Name = "Materiale per benemerenze", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "N.11.2", .Debit = DareAvere.Dare, .Name = "Oneri e spese bancarie", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "N.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.11.2", .Debit = DareAvere.Dare, .Name = "Riviste e pubblicazioni", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.11.3", .Debit = DareAvere.Dare, .Name = "Materiale di consumo", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "N.11.3", .Debit = DareAvere.Dare, .Name = "Ritenute fiscali su interessi attivi", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "N.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.1", .Debit = DareAvere.Dare, .Name = "Aree promozione e propaganda*", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12.1", .Debit = DareAvere.Dare, .Name = "Spese postali e telefoniche", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.2", .Debit = DareAvere.Dare, .Name = "Personale e collaboratori per la raccolta", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12.2", .Debit = DareAvere.Dare, .Name = "Manutenzioni", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.3", .Debit = DareAvere.Dare, .Name = "Spese trasporto sangue", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12.3", .Debit = DareAvere.Dare, .Name = "Omaggi e regalie", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.4", .Debit = DareAvere.Dare, .Name = "Spese per servizio civile", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12.4", .Debit = DareAvere.Dare, .Name = "Spese pulizia", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.5", .Debit = DareAvere.Dare, .Name = "Spese per assemblee e oneri connessi", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12.5", .Debit = DareAvere.Dare, .Name = "Assicurazioni volontari", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.6", .Debit = DareAvere.Dare, .Name = "Quote associative Avis provinciale/regionale/nazionale", .SeqNo = 60, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12.6", .Debit = DareAvere.Dare, .Name = "Spese funzionamento organi associativi", .SeqNo = 60, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.7", .Debit = DareAvere.Dare, .Name = "Convegni  e formazione volontari e collaboratori", .SeqNo = 70, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12.7", .Debit = DareAvere.Dare, .Name = "Erogazioni liberali", .SeqNo = 70, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.8", .Debit = DareAvere.Dare, .Name = "Carburante, assic., manutenz.auto (attività istituz.)", .SeqNo = 80, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.12.8", .Debit = DareAvere.Dare, .Name = "Servizi diversi", .SeqNo = 80, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.9", .Debit = DareAvere.Dare, .Name = "30° anniversario di fondazione*", .SeqNo = 90, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.12.10", .Debit = DareAvere.Dare, .Name = "Altre spese per servizi", .SeqNo = 100, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.12"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.13.1", .Debit = DareAvere.Dare, .Name = "Canoni li leasing beni destinati alla raccolta", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.13"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.13.1", .Debit = DareAvere.Dare, .Name = "Canoni di leasing", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.13"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.13.2", .Debit = DareAvere.Dare, .Name = "Locazione locali destinati alla raccolta", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.13"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.13.2", .Debit = DareAvere.Dare, .Name = "Locazione locali sede amministrativa", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.13"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.14", .Debit = DareAvere.Dare, .Name = "Ammortamenti immobilizzazioni (attività istituz.)", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.13"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.14", .Debit = DareAvere.Dare, .Name = "Spese per il personale di supporto generale", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.13"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "I.15", .Debit = DareAvere.Dare, .Name = "Oneri diversi di gestione", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "I.13"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.15", .Debit = DareAvere.Dare, .Name = "Ammortamenti beni di supporto generale", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.13"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "P.16", .Debit = DareAvere.Dare, .Name = "Altri oneri di supporto generale", .SeqNo = 50, .Summary = False, .Total = False, .Parent = (costiList2.[Single](Function(s) s.Code = "P.13"))}
            }
            costiList3.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            'Ricavi
            Dim ricaviList1 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "T", .Debit = DareAvere.Avere, .Name = "Proventi da attività tipiche", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Ricavi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "U", .Debit = DareAvere.Avere, .Name = "Proventi da attività di raccolta fondi", .SeqNo = 20, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Ricavi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "V", .Debit = DareAvere.Avere, .Name = "Proventi da attività accessorie", .SeqNo = 30, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Ricavi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "W", .Debit = DareAvere.Avere, .Name = "Proventi finanziari e patrimoniali", .SeqNo = 40, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Ricavi"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "X", .Debit = DareAvere.Avere, .Name = "Proventi straordinari", .SeqNo = 50, .Summary = True, .Total = False, .Parent = (apcrList.[Single](Function(s) s.Code = "Ricavi"))}
            }
            ricaviList1.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            Dim ricaviList2 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "T.11", .Debit = DareAvere.Avere, .Name = "Convenzioni con Aziende Sanitarie", .SeqNo = 10, .Summary = True, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "T"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "T.12", .Debit = DareAvere.Avere, .Name = "Contributi su progetti", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "T"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "T.14", .Debit = DareAvere.Avere, .Name = "Altri proventi", .SeqNo = 60, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "T"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "U.11", .Debit = DareAvere.Avere, .Name = "Proventi da attività 1", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "U"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "U.12", .Debit = DareAvere.Avere, .Name = "Proventi da attività 2", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "U"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "U.13", .Debit = DareAvere.Avere, .Name = "Proventi da attività 3", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "U"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "U.14", .Debit = DareAvere.Avere, .Name = "Proventi da ""5 per mille""", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "U"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "V.11", .Debit = DareAvere.Avere, .Name = "Contributi e ricavi per progetti ed iniziative marginali", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "V"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "V.12", .Debit = DareAvere.Avere, .Name = "Contributi da soci ed associati", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "V"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "V.13", .Debit = DareAvere.Avere, .Name = "Contributi da altri soggetti", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "V"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "V.14", .Debit = DareAvere.Avere, .Name = "Altri proventi", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "V"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "W.11", .Debit = DareAvere.Avere, .Name = "Proventi lordi da depositi bancari", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "W"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "W.12", .Debit = DareAvere.Avere, .Name = "Proventi da investimenti ed altre attività finanziarie", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "W"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "W.13", .Debit = DareAvere.Avere, .Name = "Proventi dal patrimonio edilizio", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "W"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "W.14", .Debit = DareAvere.Avere, .Name = "Proventi da altri beni patrimoniali", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "W"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "X.11", .Debit = DareAvere.Avere, .Name = "Proventi da attività finanziarie", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "X"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "X.12", .Debit = DareAvere.Avere, .Name = "Proventi da attività immobiliari", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "X"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "X.13", .Debit = DareAvere.Avere, .Name = "Proventi da altre attività", .SeqNo = 30, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "X"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "X.14", .Debit = DareAvere.Avere, .Name = "Ripresa fondo per festa sociale", .SeqNo = 40, .Summary = False, .Total = False, .Parent = (ricaviList1.[Single](Function(s) s.Code = "X"))}
            }
            ricaviList2.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()

            Dim ricaviList3 = New List(Of AccountCee)() From {
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "T.11.1", .Debit = DareAvere.Avere, .Name = "Rimborsi per donazioni", .SeqNo = 10, .Summary = False, .Total = False, .Parent = (ricaviList2.[Single](Function(s) s.Code = "T.11"))},
                New AccountCee() With {.NodeType = NodeType.ALTRO, .Code = "T.11.2", .Debit = DareAvere.Avere, .Name = "Rimborsi per trasporto sangue", .SeqNo = 20, .Summary = False, .Total = False, .Parent = (ricaviList2.[Single](Function(s) s.Code = "T.11"))}
            }
            ricaviList3.ForEach(Function(s) context.AccountCees.Add(s))
            context.SaveChanges()


            'load piano dei conti interno dedotto dalle foglie dal pc cee
            Dim query = From c In context.AccountCees
                Where c.Summary = False And c.NodeType = NodeType.ALTRO
                Order By c.Code, c.Name

            query.ToList().ForEach(Function(s) context.AccountCharts.Add(New AccountChart() With {.Name = s.Name, .Code = s.Code, .AccountCee = s, .Debit = s.getAncestorDebit}))
            context.SaveChanges()

            'mockDocument()  'load documenti di test

        End Sub

    End Class

End Namespace


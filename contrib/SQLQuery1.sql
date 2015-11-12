select
dt.name type,
h.docNr,h.dateReg,
c.code, c.name,
 r.*
from AccountChart c
inner join DocumentRow r on r.AccountChart_ID = c.ID
inner join Document h on h.id = r.Document_ID
inner join DocumentType dt on dt.ID = h.DocumentType_ID
where c.Code = 'T.11.1'	--T.11.1 	Rimborsi per donazioni 	-20,00 	0,00 	0,00
and year(h.dateReg) = 2015

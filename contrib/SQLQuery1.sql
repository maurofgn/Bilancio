select *
from AccountCee
--where parentID is null
where NodeType = 7
and summary = 1
order by code

select c.*
,p.code padre_code, p.name padre_name
from AccountCee c
left join AccountCee p on p.ID = c.ParentID
where c.id = (select max(ID) from AccountCee)



select c.active
,c.code, c.name 
from AccountCee c
where active = 0

select *
from AccountChart
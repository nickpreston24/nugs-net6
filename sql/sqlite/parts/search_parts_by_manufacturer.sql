select name, cost --, sum(cost) totalsales, avg(cost) avgcost
from parts
where manufacturer like '%iel Def%'
-- group by name
;
-- 
-- select count(id)
-- from parts;
-- select -- top 1000
--        Manufacturer,
--        Name,
--        Cost,
--        Id,
--        count(Id)
-- --         ,
-- --        avg(select cost from parts)
-- from parts
-- -- where manufacturer is not null or manufacturer like 'D%'
-- group by Id
-- 
-- order by manufacturer--, count(manufacturer)
-- limit 200

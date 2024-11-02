SELECT Id,
       Name,
       Cost,
       -- https://www.sqlitetutorial.net/sqlite-window-functions/sqlite-rank/
       RANK () OVER (
           ORDER BY cost DESC
           ) CostRank,
       -- https://www.sqlitetutorial.net/sqlite-window-functions/sqlite-dense_rank/
       DENSE_RANK() OVER (
           PARTITION BY manufacturer
           ORDER BY cost
           )         CostDenseRank,

       -- https://www.sqlitetutorial.net/sqlite-window-functions/sqlite-percent_rank/
       printf('%.2f', PERCENT_RANK() OVER (
           PARTITION BY id
           ORDER BY cost
           ))        CostPercentRank
        ,
       -- https://www.sqlitetutorial.net/sqlite-window-functions/sqlite-cume_dist/
       CUME_DIST()
               OVER (
                   ORDER BY cost
                   ) CumulativeDistribution
FROM parts;

-- 
-- order by cost;


-- where id = @id
--    or cost = @cost
-- 
--    or kind = @kind
--    OR kind like '%' + @kind + '%'
-- 
--    or name = @name
--    OR name like '%' + @name + '%'
-- 
--    or manufacturer = @manufacturer
--    OR manufacturer like '%' + @manufacturer + '%'
-- ;
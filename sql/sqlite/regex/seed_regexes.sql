drop table if exists regexes;
create table if not exists regexes
(
    id            INT PRIMARY KEY,
    type_name     text,
    assembly_name text,
    namespace     text
);

select *
from regexes;

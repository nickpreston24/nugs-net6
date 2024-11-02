drop table if exists rounds;
create table if not exists rounds
(
    id             INT PRIMARY KEY,
    name           text,
    url            text,

    cost_per_round double,
    price          double
);

select *
from rounds;

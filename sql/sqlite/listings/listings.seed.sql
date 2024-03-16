drop table if exists listings;

create table if not exists listings
(
    id    INTEGER PRIMARY KEY AUTOINCREMENT not null,
    title text default '',
    url   text default ''
);

insert into listings (title, url)
values ('', ''),
       ('', '')
;

select *
from listings;
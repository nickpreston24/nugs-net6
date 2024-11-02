drop table if exists builds;

create table if not exists builds
(
    id    INTEGER PRIMARY KEY AUTOINCREMENT not null,
    title text default '',
    url   text default ''
);

insert into builds (title, url)
values ('', ''),
       ('', '')
;

select *
from builds;
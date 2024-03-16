drop table if exists news;

create table if not exists news
(
    id    INTEGER PRIMARY KEY AUTOINCREMENT not null,
    title text default '',
    url   text default ''
);

insert into news (title, url)
values ('ATF Glock Assembly Fail ... zzz',
        'https://www.txgunrights.org/atf-officials-glock-disassembly-fail-sparks-controversy-and-mockery/')
     , ('Pro-Gun Brandon Herrera ...zzz',
        'https://www.txgunrights.org/pro-gun-brandon-herrera-forces-tony-gonzales-to-runoff/')
     , ('SOTU Biden Demands Extreme Gun Control', 'https://www.txgunrights.org/sotu-biden-demands-extreme-gun-control/')
     , ('1zzz', 'example.com')
     , ('2zzz', 'example.com')
;

select *
from news;
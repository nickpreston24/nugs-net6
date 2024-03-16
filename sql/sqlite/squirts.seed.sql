drop table if exists squirts;

create table if not exists squirts
(
    id   INTEGER PRIMARY KEY AUTOINCREMENT not null,
    name text default '',
    url  text default ''
);

insert into squirts (name, url)
values ('DD Magpul Grip',
        'https://www.thingiverse.com/search?q=daniel+defense+grip&page=1'),
       ('DD Style Grip Airsoft',
        'https://cults3d.com/en/3d-model/tool/airsoft-aeg-motor-grip-daniel-defense-style-stp-version');

select *
from squirts;
drop table ammoseek_prices;
CREATE TABLE ammoseek_prices
(
    id              serial primary key,
    retailer        varchar(250),
    description     varchar(250),
    brand           varchar(250),
    caliber         varchar(250),
    grains          varchar(250),
    last_update     varchar(250),
    limits          varchar(250) default '-',
    casing          varchar(250),
    is_new          varchar(250),
    price           varchar(250),
    rounds          varchar(250),
    price_per_round varchar(250),
    shipping_rating varchar(250)

);

-- 
-- CREATE TABLE Users (Id integer NOT NULL, JsonData json);
-- CREATE TABLE Users2 (Id integer NOT NULL, JsonData json);
-- select * from Users;
-- select * from Users2;

-- drop table Users;
-- drop table Users2;

drop table parts;
create table parts (id integer not null, name varchar(250));

insert into parts values (2, 'nug2');
select * from parts;


-- select * from lemmings;

drop table if exists ammoseek_prices;
CREATE TABLE if not exists ammoseek_prices
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

select *
from ammoseek_prices;
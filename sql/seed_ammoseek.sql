INSERT INTO ammoseek_prices(retailer,
                            description,
                            brand,
                            caliber,
                            grains,
                            last_update,
                            limits,
                            casing,
                            is_new,
                            price,
                            rounds,
                            price_per_round,
                            shipping_rating)

VALUES ( 'Botach'
       , 'Fiocchi Shooting Dynamics .300 Blackout 150 Grain FMJBT Ammunition 50rds 762345000000'
       , 'Fiocchi'
       , '.300 AAC Blackout'
       , '150'
       , '-'
       , '-'
       , 'brass'
       , true
       , '$66.95'
       , '50'
       , '$1.34'
       , '6'),
       ( 'LeadFeather Guns & Ammo'
       , 'Fiocchi 150gr FMJBT 300BLK 762344711935'
       , 'Fiocchi'
       , '.300 AAC Blackout'
       , '150'
       , '2m9s'
       , '-'
       , 'brass'
       , true
       , '$149.99'
       , '100'
       , '$1.50'
       , '5');

select retailer,
       description,
       brand,
       caliber,
       grains,
       last_update,
       limits,
       casing,
       is_new,
       price,
       rounds,
       price_per_round,
       shipping_rating
from ammoseek_prices;



ALTER TABLE ammoseek_prices
    ADD COLUMN is_deleted boolean not null default false;


-- get all faked data:
SELECT distinct SUBSTRING(retailer FROM '.*zzz.*') as fake_retailers,
                SUBSTRING(brand FROM '.*zzz.*')    as fake_brands
FROM ammoseek_prices;

-- get all faked data:
SELECT distinct SUBSTRING(retailer FROM '.*zzz.*') as fake_retailers,
                SUBSTRING(brand FROM '.*zzz.*')    as fake_brands
FROM ammoseek_prices;

-- Check distinct ammunition

select distinct *
from ammoseek_prices;


-- Check for incomplete rows

create or replace procedure find_incomplete_ammoseek_rows(
    caliber varchar(150) = '',
    retailer varchar(150) = '',
    grain varchar(150) = ''
)
    language plpgsql
as
$$
begin

    select *
    from ammoseek_prices prices
    where prices.caliber = ''
       or grains is null
       or last_update is null
       or casing is null;

end;
$$;

/*
call find_incomplete_ammoseek_rows();
*/


-- TODO: Check for duplicates


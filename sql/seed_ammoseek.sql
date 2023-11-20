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

-- get all faked data:
SELECT distinct SUBSTRING(retailer FROM '.*zzz.*') as fake_retailers,
                SUBSTRING(brand FROM '.*zzz.*')    as fake_brands
FROM ammoseek_prices;

select *
from parts
where cost > 0 && cost is not null





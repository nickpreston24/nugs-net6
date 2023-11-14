--         Gordy & Sons
--         3	Fiocchi 300 Blackout Subsonic 220gr Sierra Matchking HP 300BLKMB56178 762344711690	Fiocchi	.300 AAC Blackout	220	2m31s	-	brass		$32.99	20	$1.65
--         6

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

select *
from ammoseek_prices

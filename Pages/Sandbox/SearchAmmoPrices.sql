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
from ammoseek_prices
where description is not null and description != ''
   or (price_per_round is not null 
--            and price_per_round > 0
       )
;

-- Search for any rows that are considered incomplete, based on certain criterion
-- drop function get_ammo(@caliber varchar, @retailer varchar, @grains varchar)
create or replace function get_ammo(@caliber varchar = '', @retailer varchar = '', @grains varchar = '')
    returns table
            (
                caliber  varchar(150),
                retailer varchar(150),
                grains   varchar(150)
            )
    language plpgsql
as
$$
begin

    return query
        select prices.caliber,
               prices.retailer,
               prices.grains
        from ammoseek_prices prices
        where 1 = 1
          AND (
            @caliber <> '' OR prices.caliber = @caliber
            );
end;
$$;

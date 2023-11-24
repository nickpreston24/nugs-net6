-- Search for any rows that are considered incomplete, based on certain criterion
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
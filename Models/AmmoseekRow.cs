namespace nugsnet6.Models;

public class AmmoseekRow
{
    public string retailer { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string brand { get; set; } = string.Empty;
    public string caliber { get; set; } = string.Empty;
    public string grains { get; set; } = string.Empty;
    public string limits { get; set; } = string.Empty;
    public string casing { get; set; } = string.Empty;
    public string is_new { get; set; } = string.Empty;
    public string price { get; set; } = string.Empty;
    public string rounds { get; set; } = string.Empty;
    public string price_per_round { get; set; } = string.Empty;
    public string shipping_rating { get; set; } = string.Empty;
    public string last_update { get; set; } = string.Empty; // last time Ammoseek updated this row.

    // Admin properties
    public string environment { get; set; } = ""; // Dev or prod
    public DateTimeOffset created_at { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset last_updated_at { get; set; } = DateTimeOffset.Now; // last time I updated this row!

    public string last_updated_by { get; set; } = string.Empty;
    public string created_by { get; set; } = string.Empty;
}

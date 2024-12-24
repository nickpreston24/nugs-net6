namespace nugsnet6.Pages.PrivateSales;

public record PrivateSale
{
    public string Header = string.Empty;
    public string Frame { get; set; } = string.Empty;
    public string Id { get; } = new Guid().ToString(); // dummy value
    public bool Done { get; set; } = false; // dummy value
}

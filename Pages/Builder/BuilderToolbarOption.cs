namespace nugsnet6.Pages.Builder;

public record BuilderToolbarOption
{
    public bool Enabled { get; set; } = true;
    public bool Active { get; set; } = false;
    public string Name { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}

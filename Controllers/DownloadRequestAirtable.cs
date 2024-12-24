namespace nugs_seeder.Controllers;

public record DownloadRequestAirtable
{
    public string base_name { get; set; } = string.Empty;
    public int limit { get; set; } = 100;
}

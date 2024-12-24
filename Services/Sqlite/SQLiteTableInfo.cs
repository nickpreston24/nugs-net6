namespace nugsnet6.Services.Sqlite;

public record SQLiteTableInfo
{
    public string type { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string tbl_name { get; set; } = string.Empty;
    public string rootpage { get; set; } = string.Empty;
    public string sql { get; set; } = string.Empty;
}

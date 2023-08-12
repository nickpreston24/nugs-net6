namespace nugsnet6;

public sealed class NugsSettings 
{
    public string Neo4jUri { get; set; }
    public string Neo4jUser { get; set; }
    public string Neo4jPassword { get; set; }
    public string? MySqlConnectionString { get; set; }
}

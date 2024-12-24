namespace nugsnet6;

public sealed class NugsSettings
{
    public string Neo4jUri { get; set; }
    public string Neo4jUser { get; set; }
    public string Neo4jPassword { get; set; }
    public string MySqlConnectionString { get; set; } = string.Empty;

    public string ASPNET_ENV =
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "development";
    // public string DevMode = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
}

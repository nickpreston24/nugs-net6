namespace nugsnet6.Services.Sqlite;

public interface ISqliteInsightsService
{
    Task<List<string>> FindTables();
}
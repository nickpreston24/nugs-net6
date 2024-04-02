using CodeMechanic.Diagnostics;
using Dapper;
using Microsoft.Data.Sqlite;

namespace nugsnet6.Services.Sqlite;

public class SqliteInsightsService : ISqliteInsightsService
{
    private string connectionstring = string.Empty;

    public SqliteInsightsService(string connection_string = null)
    {
        string env_connection = Environment.GetEnvironmentVariable("SQLITE_CONNECTIONSTRING");
        connectionstring = connection_string ?? env_connection ?? "Data Source=Nugs.db";
    }

    public async Task<List<string>> FindTables()
    {
        using var connection = CreateConnection();

        var tables = await connection.QueryAsync<string>("SELECT * FROM sqlite_master WHERE type='table'");
        var tableNames = tables.Dump("tables found");
        return tableNames.ToList();
    }

    private SqliteConnection CreateConnection() => new SqliteConnection(connectionstring);
}
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using Dapper;
using Microsoft.Data.Sqlite;

namespace nugsnet6.Services.Sqlite;

public class SqliteInsightsService : ISqliteInsightsService
{
    private string connectionstring = string.Empty;
    private readonly EmbeddedResourceService embeds;

    public SqliteInsightsService(
        IEmbeddedResourceQuery embeddedResourceQuery,
        string connection_string = null
    )
    {
        string env_connection = Environment.GetEnvironmentVariable("SQLITE_CONNECTIONSTRING");
        connectionstring = connection_string ?? env_connection ?? "Data Source=Nugs.db";

        this.embeds = (EmbeddedResourceService)embeddedResourceQuery;
    }

    public async Task<List<SQLiteTableInfo>> FindTables()
    {
        using var connection = CreateConnection();

        var tables = await connection.QueryAsync<SQLiteTableInfo>(
            "SELECT * FROM sqlite_master WHERE type='table'"
        );
        var tableNames = tables.Dump("tables found");
        return tableNames.ToList();
    }

    public async Task<string> ScriptInsertFrom(string table_name)
    {
        string generated_query = "";

        /*
       * 1.  Get all table names
       * 2.  Match to given table name
       * 3.  Get that table's schema
       */
        return generated_query;
    }

    // Scripts a table from a C# model
    public async Task<String> ScriptTableAs<T>(T model)
    {
        string generated_query = "";

        return generated_query;
    }

    // Gets all SQL files that are called in the source code, but don't have a matching file.
    public async Task<string[]> FindMismatchedEmbeds()
    {
        string[] bad_files = Enumerable.Empty<string>().ToArray();
        /*
         *  1. Get all sql embedded resources.
         *  2. Scan all *.cs files for sql file references.
         *  3. Return any mismatches.
         */

        return bad_files;
    }

    private SqliteConnection CreateConnection() => new SqliteConnection(connectionstring);
}

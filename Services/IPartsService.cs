using System.Text;
using System.Text.RegularExpressions;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Dapper;
using Microsoft.Data.Sqlite;
using nugsnet6.Models;

namespace nugsnet6;

public interface IPartsService
{
    Task<List<Part>> GetAll();
    Task<List<Part>> Search(Part search);
    Task<Part> GetById(int id);
    Task<int> Create(params Part[] model);
    Task Update(int id, Part model);
    Task Delete(int id);
    Task<int> GetCount();
    Task<List<string>> FindTables();
}

public class PartsService : IPartsService
{
    private readonly EmbeddedResourceService embeds;

    public PartsService(IEmbeddedResourceQuery embed_service)
    {
        embeds = (EmbeddedResourceService?)embed_service;
        // FindTables();
    }

    public async Task<List<string>> FindTables()
    {
        using var connection = CreateConnection();

        var tables = await connection.QueryAsync<string>("SELECT * FROM sqlite_master WHERE type='table'");
        var tableNames = tables.Dump("tables found");
        return tableNames.ToList();
    }


    public async Task<int> GetCount()
    {
        string sql = """SELECT count(id) FROM parts""";
        using var connection = CreateConnection();
        var count = await connection.ExecuteAsync(sql);
        return count;
    }

    public async Task<List<Part>> GetAll()
    {
        string sql = """SELECT * FROM parts""";
        using var connection = CreateConnection();
        var records = await connection.QueryAsync<Part>(sql);
        return records.ToList();
    }

    public async Task<List<Part>> Search(Part search)
    {
        string sql = embeds.GetFileContents<PartsService>("search_parts.sql");
        // search.Dump(nameof(search));
        using var connection = CreateConnection();
        var records = await connection.QueryAsync<Part>(sql, search);
        return records.ToList();
    }

    private SqliteConnection CreateConnection() => new SqliteConnection("Data Source=Nugs.db");

    public Task<Part> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Create(params Part[] records)
    {
        string sql = embeds.GetFileContents<PartsService>("create_part.sql");
        records.Dump("creating parts");
        // https://regex101.com/r/XyhgkI/1
        // string full =
        //     @"(?<insert_clause>insert\s*into\s\w+\s*\([\w,\s*]+\))\s*(?<values_clause>values\s*\([@\w,\s]+\)\s*\;?)$";
        string values_clause = @"(?<values_clause>values\s*\([@\w,\s]+\)\s*\;?)$";
        // var values_regex = new Regex(values_clause,
        //     RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        string updated_sql = Regex.Replace(sql, values_clause, "",
            RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

        Console.WriteLine($"without values clause : >> {updated_sql}");

        string bulk_sql = records
            .ToList()
            .Aggregate(new StringBuilder("values ")
                    .Prepend(updated_sql)
                    .Prepend("begin transaction; \n")
                , (builder, part) =>
                {
                    builder.Append("(");
                    builder.Append($"'{part.Name}', ");
                    builder.Append($"{part.Cost}, ");
                    builder.Append($"'{part.Kind}', ");
                    builder.Append($"'{part.Manufacturer}'");
                    builder.AppendLine("),");
                    return builder; //.RemoveFromEnd(2);
                })
            .RemoveFromEnd(2)
            .AppendLine("\n commit;")
            .ToString();

        Console.WriteLine($"bulk sql for {records.Length} records :>> " + bulk_sql);

        using var connection = CreateConnection();

        var record_count = connection.Execute(bulk_sql, records);
        Console.WriteLine("RECORDS CREATED : " + record_count);
        return record_count;
    }

    public async Task Update(int id, Part model)
    {
        string sql = embeds.GetFileContents<PartsService>("update_part.sql");
        // Console.WriteLine(sql);
        // search.Dump();
        using var connection = CreateConnection();
        var records = await connection.ExecuteAsync(sql, new
        {
            url = model.Url, name = model.Name, imageurl = model.ImageUrl, kind = model.Kind
        });

        Console.WriteLine($"Records changed: {records}");
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}
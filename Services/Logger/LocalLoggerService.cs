using System.Runtime.CompilerServices;
using CodeMechanic.FileSystem;
using CodeMechanic.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodeMechanic.RazorHAT.Services;

public class LocalLoggerService : ILocalLogger
{
    public LocalLoggingSettings Settings = new();
    private bool dev_mode = false;

    public LocalLoggerService(bool devMode = false)
    {
        dev_mode = devMode;

        var grepper = new Grepper()
        {
            FileSearchMask = "*.log",
            RootPath = Environment.CurrentDirectory,
        };

        var files = grepper.GetFileNames().Select(fn => new FileInfo(fn));

        var now = DateTime.Now;
        var expiration = now.Add(Settings.ExpiresIn);
        var stale_files = files.Where(fi => fi.CreationTime > expiration).ToArray();

        foreach (var doomed_file in stale_files)
        {
            doomed_file.Delete();
        }

        if (dev_mode)
            Console.WriteLine("Deleted " + stale_files.Length + " files.");
    }

    public FileInfo WriteToDatabase<T>(string content, [CallerMemberName] string service_name = "")
    {
        using var connection = CreateConnection();
        string file_path = "create_log.sql";
        // ...
        return default;
    }

    public async Task<List<LogRow>> GetAll()
    {
        string sql = """SELECT * FROM logs""";
        using var connection = CreateConnection();
        var records = await connection.QueryAsync<LogRow>(sql);
        return records.ToList();
    }

    public Task<List<LogRow>> Search(LogRow search)
    {
        throw new NotImplementedException();
    }

    public async Task<LogRow> GetById(int id)
    {
        string sql = """SELECT * FROM logs where id = @id""";
        using var connection = CreateConnection();
        var records = await connection.QueryAsync<LogRow>(sql, param: new { id = id });
        return records.SingleOrDefault();
    }

    public Task<int> Create(params LogRow[] model)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, LogRow model)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCount()
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> FindTables()
    {
        throw new NotImplementedException();
    }

    public FileInfo WriteToFile<T>(string content, [CallerMemberName] string service_name = "")
    {
        var type = typeof(T);
        string type_name = type.Name;
        string loggingdir = $"{Environment.CurrentDirectory}/logs/";

        string service_folder = $"{Environment.CurrentDirectory}/logs/{service_name}";

        if (!Directory.Exists(loggingdir))
            Directory.CreateDirectory(loggingdir);

        if (!Directory.Exists(service_folder))
            Directory.CreateDirectory(service_folder);

        string timestamp_utc = DateTime.UtcNow.ToFileTimeUtc().ToString();
        string filename = $"{timestamp_utc}{type_name}.log";
        string file_path = Path.Combine(loggingdir, service_folder, filename);

        File.WriteAllText(file_path, content);
        return new FileInfo(file_path);
    }

    private SqliteConnection CreateConnection()
    {
        return new SqliteConnection("Data Source=Nugs.db");
    }
}

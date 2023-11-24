using CodeMechanic.FileSystem;

namespace CodeMechanic.RazorHAT.Services;

public class LocalLoggerService : ILocalLogger
{
    private readonly LocalLoggingSettings logging_settings;
    private bool dev_mode = false;

    public LocalLoggerService(
        LocalLoggingSettings loggingSettings = null
        , bool devMode = false)
    {
        logging_settings = loggingSettings;
        dev_mode = devMode;

        var grepper = new Grepper()
        {
            FileSearchMask = "*.log"
        };

        var files = grepper
            .GetFileNames()
            .Select(fn => new FileInfo(fn));

        var now = DateTime.Now;
        var expiration = now.Add(loggingSettings.ExpiresIn);
        var stale_files = files.Where(fi => fi.CreationTime > expiration).ToArray();

        foreach (var doomed_file in stale_files)
        {
            doomed_file.Delete();
        }

        if (dev_mode)
            Console.WriteLine("Deleted " + stale_files.Length + " files.");
    }

    public FileInfo WriteLogs<T>(string service_name, string content)
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
}

public class LocalLoggingSettings
{
    public TimeSpan ExpiresIn { get; set; } = TimeSpan.FromDays(30);
}

public interface ILocalLogger
{
    FileInfo WriteLogs<T>(string service_name, string content);
}
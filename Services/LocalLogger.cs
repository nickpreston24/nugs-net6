namespace CodeMechanic.RazorHAT.Services;

public static class LocalLogger
{
    public static FileInfo WriteLogs<T>(string service_name, string content)
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
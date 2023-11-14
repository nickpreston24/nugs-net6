using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;

namespace nugsnet6.Services;

public interface IJsonConfigService
{
    public string ReadConfig(string filename);
}

public class JsonConfigService : IJsonConfigService
{
    private IEnumerable<string> json_files;
    private readonly bool dev_mode;

    public JsonConfigService(bool dev_mode = false)
    {
        json_files = GetConfigs();
        this.dev_mode = dev_mode;
    }

    public string FindConfig(string filename)
    {
        string path = json_files.FirstOrDefault(x => x.EndsWith(filename));
        if (dev_mode) Console.WriteLine("config found :>> " + path);
        return path;
    }

    private IEnumerable<string> GetConfigs()
    {
        string current_directory = Environment.CurrentDirectory;

        if (dev_mode) Console.WriteLine("cwd :>> " + current_directory);
        var grepper = new Grepper()
        {
            RootPath = current_directory,
            FileSearchMask = "*.config.json",
            Recursive = true
        };

        var not_blacklisted = new NSpecifications.Spec<string>(
            filepath => filepath.Contains("node_modules/")
                  || filepath.Contains("wwwroot/")
                  || filepath.Contains("bin/")
                  || filepath.Contains("obj/")
        );

        var configs = grepper.GetFileNames()
            .Where(not_blacklisted)
            // .Dump("configs")
            ;
        return configs;
    }

    public string ReadConfig(string filename)
    {
        string filepath = FindConfig(filename);
        string lines = File.ReadAllText(filepath);
        if (dev_mode) Console.WriteLine("Lines :>> \n" + lines);
        return lines;
    }
}
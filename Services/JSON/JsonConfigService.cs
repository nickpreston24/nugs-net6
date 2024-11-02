using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using CodeMechanic.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSpecifications;

namespace CodeMechanic.RazorHAT.Services;

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
            FileSearchMask = "*.config.json,*.json",
            Recursive = true
        };

        var not_blacklisted = new Spec<string>(
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
        string lines = File.Exists(filepath) ? File.ReadAllText(filepath) : string.Empty;
        if (dev_mode) Console.WriteLine("Lines :>> \n" + lines);
        return lines;
    }


    public T GetSetting<T>(string key = "", string json = "{}")
    {
        throw new NotImplementedException("Finish this...if you dare!");

        if (key.IsEmpty() || json.IsEmpty())
            return default;

        JObject search = JObject.Parse(json);

        IList<JToken> results = search.Children().ToList();

        // serialize JSON results into .NET objects
        IList<T> searchResults = new List<T>();
        foreach (JToken result in results)
        {
            // JToken.ToObject is a helper method that uses JsonSerializer internally
            T searchResult = result.ToObject<T>();
            searchResults.Add(searchResult);
        }

        return searchResults.FirstOrDefault();
    }

    public T ReadConfigSettings<T>(string filename)
    {
        var config_json = ReadConfig(filename);
        Console.WriteLine("JSON : >> " + config_json);
        var settings = JsonConvert.DeserializeObject<T>(config_json);
        settings.Dump("After convert :>> ");
        return settings ?? Activator.CreateInstance<T>();
    }

    private void CheckForWatchedConfigFiles()
    {
        //Todo: Search for Watch tags in the current .csproj and check that they include .config.js*
        // Then, Console.Warn the user:
        string cs_projname = "";
        Console.WriteLine($"WARNING: Could not find 'config.json' files in a <Watch> inside project '{cs_projname}'");
    }


    // private void SqlFormatting<Part>(Part part, params string[] values)
    // {
    //     var subquery = new StringBuilder();
    //     subquery.AppendFormat("{0}{1}", values[0], values[1]);
    //     subquery.AppendFormat($"{values[0]}{values[1]}");
    //     subquery.AppendFormat($"{part.Name}{part.Cost}");
    // }

    // private void DoStuff()
    // {
    //     // select name, crested from posts where create >= {:from} and 
    //     var query = SqlFormatting(_, "Magpul 5.56", "$23.00", "1-1-23");
    //
    //     
    //     new SqlParameter(@"name", "blah");
    //     
    //
    //
    // }
}
using System.Net.Http.Headers;
using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nugsnet6.Extensions;

namespace CodeMechanic.RazorHAT.Services;

public interface IAirtableQueryingService
{
    Task<List<T>> SearchRecords<T>(AirtableSearchV2 search);
}

public class AirtableQueryingService : IAirtableQueryingService
{
    // private string base_id = string.Empty;
    private string personal_access_token = string.Empty;
    private readonly HttpClient http_client;
    private readonly bool debug_mode;

    public AirtableQueryingService(
        HttpClient client = null
        // , string base_id = ""
        , string personal_access_token = ""
        , bool debug_mode = false)
    {
        this.http_client = client ?? new HttpClient(); //?? throw new ArgumentNullException(nameof(client));

        // this.base_id = base_id;
        this.personal_access_token = personal_access_token;
        this.http_client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", personal_access_token);

        this.debug_mode = debug_mode;

        // Show warnings to coders:
        // if (base_id.IsEmpty())
        //     Console.WriteLine("WARNING!!! " + nameof(base_id) + " should not be empty!");

        if (this.personal_access_token.IsEmpty())
            Console.WriteLine("WARNING!!! " + nameof(personal_access_token) + " should not be empty!");

        if (debug_mode) Console.WriteLine("Airtable Querying Service is initialized and running! 🥳 ");
    }

    public async Task<List<T>> SearchRecords<T>(AirtableSearchV2 search)
    {
        var response = await http_client.GetAsync(search.AsQuery());

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        try
        {
            JObject parsed_object = JObject.Parse(json);

            // get JSON result objects into a list
            IList<JToken> results = parsed_object["records"]
                .Children()
                ["fields"]
                // .Children()
                .Dump("Children")
                .ToList();

            // serialize JSON results into .NET objects
            IList<T> records = new List<T>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                T searchResult = result.ToObject<T>();
                records.Add(searchResult);
            }

            if (debug_mode) records.Count.Dump("records from airtable search :>> ");
            return records.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            if (debug_mode)
                WriteLogs<T>(nameof(AirtableQueryingService), json);

            return new List<T>();
        }
    }

    private FileInfo WriteLogs<T>(string service_name, string json)
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

        File.WriteAllText(file_path, json);
        return new FileInfo(file_path);
    }
}
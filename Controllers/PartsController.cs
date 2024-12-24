using System.Reflection;
using System.Text;
using CodeMechanic.Diagnostics;
using CodeMechanic.RegularExpressions;
using CodeMechanic.Types;
using Insight.Database;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using nugsnet6;
using nugsnet6.Models;

namespace nugs_seeder.Controllers;

[ApiController]
[Route("[controller]")]
public class PartsController : ControllerBase
{
    private static readonly IDictionary<Type, ICollection<PropertyInfo>> _propertyCache =
        new Dictionary<Type, ICollection<PropertyInfo>>();

    private readonly ILogger<PartsController> logger;
    private readonly IWebHostEnvironment env;
    private readonly NugsSettings settings;

    public PartsController(ILogger<PartsController> logs, IWebHostEnvironment environment_vars)
    {
        logger = logs;
        env = environment_vars;

        settings = new NugsSettings().With(setting =>
        {
            setting.Neo4jUri = "blarg";
            setting.Neo4jUser = "blarg";
            setting.Neo4jPassword = "blarg";
            setting.MySqlConnectionString = Environment.GetEnvironmentVariable(
                "MYSQL_CONNECTIONSTRING"
            );
        })
        // .Dump("current settings")
        ; // doesn't work on startup.  Who knew?
    }

    [HttpPatch(nameof(ImportRecordsToNeo4jFromCSV))]
    public async Task<IEnumerable<Part>> ImportRecordsToNeo4jFromCSV(
        [FromBody] ImportRequest request
    )
    {
        // request.Dump("my request");

        var csv_lines = System.IO.File.ReadAllLines(request.import_file_path);
        var csv_regex = csv_lines[0]
            .Split(',')
            .Aggregate(
                new StringBuilder(),
                (builder, next_header) =>
                {
                    builder.Append($"(?<{next_header}>.*),");
                    return builder;
                }
            )
            .RemoveFromEnd(1)
            .ToString()
        // .Dump("generated regex")
        ;

        var parts_for_upload = csv_lines.SelectMany(csv => csv.Extract<Part>(csv_regex)).ToList();

        return parts_for_upload;
    }

    [HttpPatch(nameof(ImportRecordsToNeo4j))]
    public async Task<IEnumerable<Part>> ImportRecordsToNeo4j(
        [FromBody] UploadRequestNeo4j<Part> upload_request
    )
    {
        var parts_for_upload = new Part()
            .With(part =>
            {
                part.Name = "Test Part";
                part.Notes = "All your rels belong to us, too";
            })
            .Repeat(10)
            .ToList();

        return parts_for_upload;

        // var diffObj = new JsonDiffPatch();
        //
        // var left = """{ "name": "John"}""";
        // var right = """{ "name": "Tod" }""";
        //
        // var patch = diffObj.Diff(left, right);
        //
        // Console.WriteLine(patch);
        // // var result = JsonConvert.DeserializeObject<Part>(patch.ToString());
        //
        // // return result.AsList();
        //
        // return null;
    }

    [HttpPost(nameof(DownloadRecordsFromAirtable))]
    public async Task<IEnumerable<Part>> DownloadRecordsFromAirtable(
        [FromBody] DownloadRequestAirtable download_request
    )
    {
        return new Part()
            .With(part =>
            {
                part.Name = download_request.base_name;
                part.Notes = "A fabulous scope mount that zooms with a slider";
            })
            .Repeat(10)
            .ToList()
            .Take(5);
    }

    [HttpPost(nameof(AddNewRounds))]
    public async Task<IEnumerable<Round>> AddNewRounds([FromBody] Round next_round)
    {
        // https://www.midwayusa.com/s?searchTerm=proof%20research%20barrel
        string url = "https://ammoseek.com/ammo/224-valkyrie";

        return new Round().AsList();
    }

    [HttpPost(nameof(AddNewParts))]
    public async Task<IEnumerable<Part>> AddNewParts([FromBody] Part next_part)
    {
        // settings.Dump("current settings");

        // var batches = new Part()
        //     .Repeat(19)
        //     .Batch(4)
        //     .Dump("batches");

        string connection_string = settings.MySqlConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connection_string))
        {
            string query = """
                    INSERT INTO Part (Name)
                    OUTPUT Inserted.ID
                    VALUES (@Name)
                """;

            var part = new Part() { Name = "Fake Part" };
            connection.Insert("InsertPart", part);
            // var results = connection
            //     .QuerySql<Part>(query)
            //     .ToMaybe(); // I like maybe.  So sue me.

            // return results
            //     .Case(
            //         some: (parts) => parts?.Dump("results"),
            //         none: () =>
            //         {
            //             "No results.  Sending back original part".Dump();
            //             return next_part.AsList();
            //         }
            //     );
            // var results = connection
            //     .QuerySql<Part>(query)
            //     .ToMaybe(); // I like maybe.  So sue me.

            // return results
            //     .Case(
            //         some: (parts) => parts?.Dump("results"),
            //         none: () =>
            //         {
            //             "No results.  Sending back original part".Dump();
            //             return next_part.AsList();
            //         }
            //     );

            return part.AsList();
        }
    }

    // [HttpGet(nameof(FillRegexRepo))]
    // public async Task<IEnumerable<MarkdownTableRow>> FillRegexRepo()
    // {
    //     // string local_path = new StackTrace().GetCurrentResourcePath(extension: ".md").Dump("local path");
    //     var search_results = _regexRepository.SearchLocalMarkdownTables();
    //     return search_results;
    // }


    [HttpPost(nameof(DownloadAmmoSeek))]
    public async Task<object> DownloadAmmoSeek(AmmoseekRequest request)
    {
        string html = await SearchAmmoSeek(request);
        // html.Dump("html");
        var file_info = await SaveFileAS(request, html);

        return file_info;
    }

    private static async Task<string> SaveFileAS(AmmoseekRequest request, string html)
    {
        string current_directory = Directory.GetCurrentDirectory();
        Console.WriteLine(current_directory);
        var now = DateTime.Now.ToFileTimeUtc();
        string filename = $"ammoseek_{request.caliber}_{now}.html";
        string save_folder = Path.Combine(current_directory, "ammoseek");
        if (!Directory.Exists(save_folder))
            Directory.CreateDirectory(save_folder);
        string save_path = Path.Combine(save_folder, filename);

        if (!System.IO.File.Exists(save_path))
        {
            save_path.Dump("saving to...");
            await System.IO.File.WriteAllTextAsync(save_path, html);
            return save_path;
        }

        return default;
    }

    private static async Task<string> SearchAmmoSeek(AmmoseekRequest request)
    {
        using var client = new HttpClient();
        var content = await client.GetStringAsync("https://ammoseek.com/ammo/224-valkyrie");
        return content;

        // string base_url = @"https://ammoseek.com/ammo/";
        // using var client = new HttpClient();
        // string? full_url =
        //     @"https://www.scrapingbee.com/blog/web-scraping-csharp/";
        // //    @"https://ammoseek.com/ammo/224-valkyrie";
        // //$"{base_url}/{request.caliber}";
        // Console.WriteLine(full_url.Dump("full url"));
        // var html = await client.GetStringAsync(full_url);
        // Console.WriteLine(html);
        // return html;
    }
}

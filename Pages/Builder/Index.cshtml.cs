using System.Diagnostics;
using System.Text;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neo4j.Driver;
using nugsnet6.Models;

namespace nugsnet6.Pages.Builder;

public class IndexModel : PageModel
{
    private static string table_name = "Builds";
    private static AirtableSearch _search = new AirtableSearch();
    private static List<Build> builds_found = new List<Build>();

    private static List<nugsnet6.Models.Part> parts_found = new List<nugsnet6.Models.Part>();
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;
    private readonly IAirtableQueryingService airtable_service;
    private readonly ICsvService csv_service;
    private readonly IDriver driver;
    private readonly IAirtableRepo airtable_repo;

    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , IAirtableQueryingService airtableQueryingService
        , ICsvService csvService
        , IDriver driver
        , IAirtableRepo airtableRepo
    )
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
        this.airtable_service = airtableQueryingService;
        this.csv_service = csvService;
        this.driver = driver;
        this.airtable_repo = airtableRepo;
    }

    public List<nugsnet6.Models.Part> SampleParts { get; set; } = new List<nugsnet6.Models.Part>()
    {
        new nugsnet6.Models.Part()
        {
            Name = "BCM Charging Handle"
        }
    };


    public List<nugsnet6.Models.Part> PartsFromCsv { get; set; } = new List<nugsnet6.Models.Part>()
    {
    };

    public Build CurrentBuild { get; set; }


    public async Task<IActionResult> OnGetPartsFromCsvFile()
    {
        Console.WriteLine("HELLO FROM CSV IMPORT");
        var userDir = new DirectoryInfo(Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile))
            ?.FullName;

        string filedir = "Downloads";
        string filename = "Parts-Grid view.csv";

        string filepath = Path.Combine(userDir, filedir, filename);
        Console.WriteLine(filepath);

        Console.WriteLine(Environment.GetEnvironmentVariable("HOME"));

        return Content("Done!");


        string regex_pattern_for_parts = $"""
            (?<Name>\w+)\,
        """ ;

        var csv_parts = csv_service.ImportAs<nugsnet6.Models.Part>(filepath, regex_pattern_for_parts);
        csv_parts.Count.Dump($"# of parts from csv '{filepath}'");

        return Partial("_PartsTable", csv_parts);
    }

    public async Task<IActionResult> OnGetSamplePartsFromAirtable()
    {
        Console.WriteLine("Getting sample parts...");
        // var parts = Model ?? Enumerable.Empty<Part>();

        return Content("Done!");

        // TODO: need to unencrypt airtable rows.

        // var base_id = Environment.GetEnvironmentVariable("NUGS_BASE_KEY");
        //
        // bool debug_mode = true;
        //
        // var parts_search = new AirtableSearchV2(base_id, "Parts")
        // {
        //     maxRecords = 2
        // };
        //
        // var builds_search = new AirtableSearchV2(base_id, "Builds")
        // {
        //     maxRecords = 2
        // };
        //
        // var builds = await airtable_service.SearchRecords<Build>(builds_search);
        // builds.Dump("sample builds search");
        //
        // var parts = await airtable_service.SearchRecords<Part>(parts_search);
        // parts.Dump("sample parts search");
        // // return Content($"<b class='alert alert-primary'>Success! <br/> # of parts found: {parts.Count}</b>");
        //
        // return Partial("_PartsList", parts);
    }

    public void OnPatchSetTableName(string next_table_name = "Loadouts") => table_name = next_table_name;


    public IActionResult OnPatchPatchPart([FromForm] nugsnet6.Models.Part request)
    {
        return Content(
            $"<div class='alert alert-primary'>Updated part as {request.Name}!</div>",
            "text/html"
        );
    }

    public async Task<IActionResult> OnGetSearchParts(string Name = "")
    {
        parts_found = await airtable_repo
            .SearchRecords<nugsnet6.Models.Part>(_search
                    .With(s =>
                    {
                        s.table_name = "Parts";
                        s.maxRecords = 3;
                        s.filterByFormula = $"(FIND(\"{Name}\", {{Name}}))";
                    })
                , debug: true
            );

        return Partial("_PartsTable", parts_found);
    }

    public async Task<IActionResult> OnGetSearchBuilds(string Name = "")
    {
        builds_found = await airtable_repo
            .SearchRecords<Build>(_search
                    .With(s =>
                    {
                        s.table_name = "Builds";
                        s.maxRecords = 3;
                        // s.pageSize = 20;
                        // s.offset = "30";
                        s.filterByFormula = $"(FIND(\"{Name}\", {{Name}}))";
                    })
                , debug: true
            );

        string html = new StringBuilder()
            .AppendEach(
                builds_found,
                build =>
                    $"""
                         <tr>
                             <th>
                                 <label>
                                     <input type="checkbox" class="checkbox" />
                                 </label>
                             </th>
                             <th class='text-primary'>{ build.Name}                               </th>
                             <td class='text-accent'>${ build.Total_Cost.ToString()}
                                                           </td>
                             <td class='text-secondary'>{ build.Reasoning}                               </td>
                         </tr>
                     """ ).ToString();
        return Content(html);
        // return Partial("_BuildsTable", builds_found);
    }

    public async Task<IActionResult> OnPostFavoriteBuild()
    {
        return Content("<p class='alert alert-primary text-accent'>He's a chicken!</p>");
    }

    // public async Task<IActionResult> OnGetRecommendedBarrels()
    // {
    //     var failure = Content(
    //         $"<div class='alert alert-error'><p class='text-xl text-warning text-sh'>An Error Occurred...  But fret not! Our team of intelligent lab mice are on the job!</p></div>");
    //
    //     string query = "...";
    //
    //     string resource = new StackTrace().GetCurrentResourcePath();
    //     if (embeddedResourceQuery == null)
    //         return failure;
    //
    //     await using Stream stream = embeddedResourceQuery.Read<IndexModel>(resource);
    //
    //     query = await stream.ReadAllLinesFromStreamAsync();
    //
    //     var records = await SearchNeo4J<Build>(query, new { });
    //
    //     return Content("""
    //                        <div class='alert alert-success shadow-lg'>
    //                            <span>Confirmed!</span>
    //                        </div>
    //                    """);
    // }
}
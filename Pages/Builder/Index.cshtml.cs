using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neo4j.Driver;
using nugsnet6.Models;
using nugsnet6.Services;

namespace nugsnet6.Pages.Builder;

[BindProperties(SupportsGet = true)]
public class IndexModel : PageModel
{
    private static string table_name = "Builds";

    // private static AirtableSearch _search = new();
    private static List<Build> builds_found = new();

    private static List<Part> parts_found = new();

    private readonly IEmbeddedResourceQuery embeddedResourceQuery;

    // private readonly IAirtableQueryingService airtable_service;
    private readonly ICsvService csv_service;
    private readonly IDriver driver;
    private readonly IAirtableRepo airtable_repo;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IBuilderService builder_svc;
    private readonly IPartsService parts_svc;

    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery,
        // IAirtableQueryingService airtableQueryingService,
        ICsvService csvService,
        IDriver driver,
        IAirtableRepo airtableRepo,
        IBuilderService builderService,
        IHttpClientFactory httpClientFactory,
        IPartsService partsService
    )
    {
        _httpClientFactory = httpClientFactory;
        parts_svc = partsService;
        this.builder_svc = builderService;
        this.embeddedResourceQuery = embeddedResourceQuery;
        // airtable_service = airtableQueryingService;
        csv_service = csvService;
        this.driver = driver;
        airtable_repo = airtableRepo;
    }

    public List<Part> SampleParts { get; set; } =
        new() { new Part() { Name = "BCM Charging Handle" } };

    public List<Part> PartsFromCsv { get; set; } = new();

    public Build CurrentBuild { get; set; } = new();
    public Part UpdatePart { get; set; } = new Part() { Id = "-1" };

    public string Url { get; set; } = string.Empty;
    public string CSS_Selector { get; set; } = string.Empty;

    public async void OnGet()
    {
        string filepath = "Experimental/Parts-Grid view.csv";

        var parts_from_csv = csv_service
            .Read<Part>(
                filepath,
                (csv) =>
                {
                    var record = new Part
                    {
                        Id = csv.GetField<string>("Id"),
                        Name = csv.GetField("Name"),
                        Cost = csv.GetField("Cost").Replace("$", "").ToDouble(),
                        Combo = csv.GetField("Combo"),
                        Type = csv.GetField("Type"),
                        Attachments = csv.GetField("Attachments"),
                        Url = csv.GetField("Url"),
                    };
                    return record;
                }
            )
            .ToList();

        var parts_from_sqlite = await parts_svc.GetAll();

        // parts_from_sqlite.Count.Dump("from sqlite");

        if (parts_from_sqlite.Count == 0)
        {
            // Seed
            int count = await parts_svc.Create(parts_from_csv.ToArray());
            Console.WriteLine($"Seeded {count} parts in sqlite db");
        }

        PartsFromCsv = parts_from_csv;

        var query = "match (b:Build) return b limit 10";
        var existing_builds =
            // await driver.SearchNeo4J<Build>(query, null, debug_mode: true);
            await builder_svc.GetAll<Build>(query);

        Console.WriteLine("existing builds :>> " + existing_builds.Count);
    }

    public async Task<IActionResult> OnGetClip()
    {
        Console.WriteLine($"Requesting clip from url '{Url}'");
        // Console.WriteLine($"Passed in url '{url}'");
        var clipped_images = Enumerable.Empty<ScrapedImage>().ToList();

        // var httpClient = _httpClientFactory.CreateClient();
        //
        // using var response = await httpClient.GetAsync(Url);

        try
        {
            var web = new HtmlWeb();
            var document = web.Load(Url);
            var selectors = "gallery-image"
                .AsArray()
                .Concat(new[] { UpdatePart.ImageCssSelector })
                .ToArray();
            selectors.Dump("looking for selectors");
            clipped_images = GetImages(document)
                // .Dump("all")
                .HavingSelectors(selectors)
                .ToList();

            // Console.WriteLine(response);
            clipped_images?.Dump(nameof(clipped_images));
            return Content($"<span class='alert alert-primary'>{clipped_images?.Count}</span>");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
        }
    }

    private List<ScrapedImage> GetImages(HtmlDocument document)
    {
        var urls = document
            .DocumentNode.Descendants("img")
            .Select(e => new ScrapedImage()
            {
                src = e.GetAttributeValue("src", null),
                css_selector = e.GetAttributeValue("class", null),
            })
            .Where(s => s.src.NotEmpty())
            .ToList();

        return urls;
    }

    // public async Task<IActionResult> OnGetExistingBuilds()
    // {
    //     Console.WriteLine(nameof(OnGetExistingBuilds));
    //     var existing_builds = await builder_svc.GetAll<Build>(
    //         "match (b:Build) return b limit 10"
    //     );
    //     // existing_builds.Dump(nameof(existing_builds));
    //     return Content($"<span class='alert alert-primary'>{existing_builds.Count}</span>");
    // }

    public async Task<IActionResult> OnGetPartsFromCsvFile()
    {
        Console.WriteLine("HELLO FROM CSV IMPORT");
        var userDir = new DirectoryInfo(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        )?.FullName;

        string filedir = "Downloads";
        string filename = "Parts-Grid view.csv";

        string filepath = Path.Combine(userDir, filedir, filename);
        Console.WriteLine(filepath);

        Console.WriteLine(Environment.GetEnvironmentVariable("HOME"));

        return Content("Done!");
        //
        //
        //         string regex_pattern_for_parts = $"""
        //             (?<Name>\w+)\,
        //         """ ;
        //
        //         var csv_parts = csv_service.ImportAs<nugsnet6.Models.Part>(filepath, regex_pattern_for_parts);
        //         csv_parts.Count.Dump($"# of parts from csv '{filepath}'");
        //
        //         return Partial("_PartsTable", csv_parts);
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

    public void OnPatchSetTableName(string next_table_name = "Loadouts") =>
        table_name = next_table_name;

    public IActionResult OnPatchPatchPart([FromForm] Part request)
    {
        return Content(
            $"<div class='alert alert-primary'>Updated part as {request.Name}!</div>",
            "text/html"
        );
    }

    public async Task<IActionResult> OnGetSearchParts(string Name = "")
    {
        // parts_found = await airtable_repo.SearchRecords<Part>(
        //     _search.With(s =>
        //     {
        //         s.table_name = "Parts";
        //         s.maxRecords = 3;
        //         s.filterByFormula = $"(FIND(\"{Name}\", {{Name}}))";
        //     }),
        //     debug: true
        // );

        return Partial("_PartsTable", default);
    }

    public async Task<IActionResult> OnGetSearchBuilds(string Name = "")
    {
        // builds_found = await airtable_repo.SearchRecords<Build>(
        //     _search.With(s =>
        //     {
        //         s.table_name = "Builds";
        //         s.maxRecords = 3;
        //         // s.pageSize = 20;
        //         // s.offset = "30";
        //         s.filterByFormula = $"(FIND(\"{Name}\", {{Name}}))";
        //     }),
        //     debug: true
        // );

        //         string html = new StringBuilder()
        //             .AppendEach(
        //                 builds_found,
        //                 build =>
        //                     $"""
        //                          <tr>
        //                              <th>
        //                                  <label>
        //                                      <input type="checkbox" class="checkbox" />
        //                                  </label>
        //                              </th>
        //                              <th class='text-primary'>{ build.Name}
        //                                                                             </th>
        //                              <td class='text-accent'>${ build.Total_Cost.ToString()}
        //
        //                                                            </td>
        //                              <td class='text-secondary'>{ build.Reasoning}
        //
        //                                                                      </td>
        //                          </tr>
        //                      """ ).ToString();
        //         return Content(html);
        return Partial("_BuildsTable", builds_found);
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

public static class ScrapedImageExtensions
{
    public static IEnumerable<ScrapedImage> HavingSelectors(
        this IEnumerable<ScrapedImage> images,
        params string[] cssselectors
    ) => images.Where(img => cssselectors.Contains(img.css_selector));
}

public record ScrapedImage
{
    public string src { get; set; } = string.Empty;
    public string css_selector { get; set; } = string.Empty;
}

//     var nodes = document.DocumentNode.QuerySelectorAll("div.product-image-gallery");
//
//     /*
//      * // scraping the interesting data from the current HTML element
// var url = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("a").Attributes["href"].Value);
//
// var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("h2").InnerText);
// var price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector(".price").InnerText);
//      */
//
//     if (nodes?.Count == 0)
//         return Content("no nodes");
//     // nodes?.Count.Dump("total nodes");
//
//     foreach (var node in nodes)
//     {
//         // node.Dump("node");
//         var image = HtmlEntity.DeEntitize(node.QuerySelector("img")?.Attributes["gallery-image"]?.Value);
//         if (image.NotEmpty())
//             clipped_images.Add(image);
//     }
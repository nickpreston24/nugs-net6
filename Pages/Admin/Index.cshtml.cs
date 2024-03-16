using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CodeMechanic.Diagnostics;
using Neo4j.Driver;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Admin;

public class IndexModel : PageModel
{
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;
    private readonly IDriver driver;

    private static int count = 0;
    private readonly ICsvService csv;

    public List<Models.Part> PartsFromCsv { get; set; } = new();

    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , ICsvService csvService
        , IDriver driver)
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
        this.driver = driver;
        csv = csvService;
    }

    public void OnGet()
    {
        // reset on refresh
        count = 0;
        PartsFromCsv = csv.Read<Models.Part>("Experimental/Parts-Grid view.csv", (csv) =>
        {
            string cost_wo_dollar_sign = csv.GetField("Cost")
                    .Replace("$", "")
                // .Dump("after replace")
                ;

            cost_wo_dollar_sign.Dump("cost field");
            var record = new Models.Part
            {
                Id = csv.GetField<string>("Id"),
                Name = csv.GetField("Name"),
                Cost = cost_wo_dollar_sign.ToDouble()
                // Combo = cost_wo_dollar_sign.ToDouble()
                // Cost = TypeExtensions.ToDouble(csv.GetField("Cost").ToString())
            };
            return record;
        }).ToList();
    }

    public async Task<IActionResult> OnGetStuff()
    {
        var failure = Content(
            $"""
            <div class='alert alert-error'>
                <p class='text-3xl text-warning text-sh'>
                    An Error Occurred...  But fret not! Our team of intelligent lab mice are on the job!
                </p>
            </div>
        """ );

        string query = "..."; // This can be ANY SQL query.  In my case, I'm using cypher, because it's lovely.

        // Magically infers from the tract that the current method name is referring to 'Recommendations.cypher'
        query = await embeddedResourceQuery
            .GetQueryAsync<IndexModel>(new StackTrace());

        if (string.IsNullOrEmpty(query))
            return failure; // If for some reason, nothing comes back, alert the user with this div.

        // This can also be a template, if we want, but here's a fancy-schmancy use of the triple-double quotes to easily send back anything in C# directly to HTML/X:
        return Content(
            $"""
            <div class='alert alert-primary'>
                <p class='text-xl text-secondary text-sh'>
                { query}        
                </p>
            </div>
        """ );
    }
}
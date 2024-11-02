using CodeMechanic.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using nugsnet6.Models;
using CodeMechanic.RazorHAT;
using CodeMechanic.Embeds;
using CodeMechanic.Types;
using Htmx;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Loadouts
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IEmbeddedResourceQuery embeddedResourceQuery;
        private readonly IDriver driver;
        private readonly IAirtableRepo airtable_repo;
        private static AirtableSearch currentAirtableSearch = new AirtableSearch();
        public AirtableSearch CurrentAirtableSearch => currentAirtableSearch;

        public Loadout LoadoutSearch { get; set; }

        public IEnumerable<string> Items { get; }
            = new[] { "_LoadoutEditor", "_LoadoutSearcher", "_Third" };

        [BindProperty(Name = "tab", SupportsGet = true)]
        public string? Tab { get; set; }

        public string Title { get; set; } = string.Empty;

        public bool IsSelected(string name) =>
            name.Equals(Tab?.Trim(), StringComparison.OrdinalIgnoreCase);


        public IndexModel(
            IEmbeddedResourceQuery embeddedResourceQuery
            , IDriver driver
            , IAirtableRepo airtableRepo
        )
        {
            this.embeddedResourceQuery = embeddedResourceQuery;
            this.driver = driver;
            this.airtable_repo = airtableRepo;
        }

        public IActionResult OnGet()
        {
            // make sure we have a tab
            Tab = Items.Any(IsSelected) ? Tab : Items.First();
            // Tab.Dump("put it on my tab");
            return Request.IsHtmx()
                ? Partial("_Tabs", this)
                : Page();
        }

        public string? IsSelectedCss(string tab, string? cssClass)
            => IsSelected(tab) ? cssClass /*.Dump("css class")*/ : null;

        public async Task<IActionResult> OnGetSearchLoadouts(
            [FromForm] Loadout search
            // , string Name = "Snow Owl"
        )
        {
            try
            {
                // search.Dump("initial search for loadouts");
                var results = await airtable_repo
                    .SearchRecords<Loadout>(currentAirtableSearch
                        .With(s =>
                        {
                            s.maxRecords = 12;
                            s.filterByFormula = $"(FIND(\"{search.Name}\", {{Name}}))";
                        })
                    );


                search.Name.Dump("Passed in name");

                return Partial("_LoadoutsTable", results);
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                var title = ex.Message;

                return Content($"""
                    <b class='alert alert-error'>{ title}     </b>
                """ );
            }
        }
    }
}
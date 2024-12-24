using CodeMechanic.Airtable;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neo4j.Driver;

namespace nugsnet6.Pages.Loadouts
{
    public class AirsoftLoadoutPageModel : PageModel
    {
        private readonly IEmbeddedResourceQuery embeddedResourceQuery;
        private readonly IDriver driver;

        private readonly IAirtableRepo airtable_repo;

        // private static AirtableSearchV2 _search = new AirtableSearchV2();
        // public AirtableSearch Search => _search;
        public string Title { get; set; } = string.Empty;

        public AirsoftLoadoutPageModel(
            IEmbeddedResourceQuery embeddedResourceQuery,
            IDriver driver,
            IAirtableRepo airtableRepo
        )
        {
            this.embeddedResourceQuery = embeddedResourceQuery;
            this.driver = driver;
            this.airtable_repo = airtableRepo;
        }

        public async Task<IActionResult> OnGetSearchAirsoftLoadouts(
            AirsoftLoadout search,
            string Name = "Snow Owl"
        )
        {
            try
            {
                // search.Dump("initial search for loadouts");
                // Name.Dump("Passed in name");
                // var results = await airtable_repo.SearchRecords<AirsoftLoadout>(
                //     _search.With(s =>
                //     {
                //         s.table_name = "Airsoft Loadouts";
                //         s.maxRecords = 12;
                //         s.filterByFormula = $"(FIND(\"{Name}\", {{Name}}))";
                //     })
                // );

                return Partial("LoadoutsGrid", default);
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                var title = ex.Message;

                return Content(
                    $"""
                         <b class='alert alert-error'>{title}        </b>
                     """
                );
            }
        }
    }
}
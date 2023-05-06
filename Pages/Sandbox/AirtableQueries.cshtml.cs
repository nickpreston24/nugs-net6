using Microsoft.AspNetCore.Mvc;
using CodeMechanic.RazorHAT;
using CodeMechanic.Embeds;

using Neo4j.Driver;

namespace nugsnet6.Pages.Sandbox;

public class AirtableModel : HighSpeedPageModel
{

    private static int count = 0;

    public AirtableModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , IDriver driver) 
    : base(embeddedResourceQuery, driver)
    {
    }

    public void OnGet()
    {
        // reset on refresh
        count = 0;
    }

    

    public IActionResult OnGetStuff()
    {
        return Content($"<b>Stuff for u!<br/> One lump, or 2?</b>");
    }

   

}



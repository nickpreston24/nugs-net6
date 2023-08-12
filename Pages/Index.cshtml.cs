using CodeMechanic.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nugsnet6.Experimental;

namespace nugsnet6.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnGetPricing()
    {
        return Partial("Pricing", default);
    }

    public async Task<IActionResult> OnGetSurvey()
    {
        return Partial("_Survey".Dump(), default);
    }

    public async Task<IActionResult> OnPostUpdateSurvey([FromBody] Survey survey)
    {
        survey.Dump("survey contents");
        // return Partial("Survey", default);
        // return Content("<p>BLARG!!!</p>");
        return Partial("Pricing", default);
    }

    public async Task<IActionResult> OnGetSwap()
    {
        return Partial("AlpineMorphSample", default);
    }
}
using CodeMechanic.Advanced.Extensions;
using CodeMechanic.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nugsnet6.Experimental;
using nugsnet6.Extensions;

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
        // @await Html.PartialAsync("_LoginPartial")
        // "".AsH

        return Partial("_Survey", default);
//         return Content($"""
//                         <div>
//                             <h1>Hang on sloopy!</h1>
//                             {}
//                         </div>
//                         """);
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

    public async Task<IActionResult> OnGetRenderSection(string section_name, object value)
    {
        var section_missing = Content($"""
                                           <p class='alert alert-error'>The section '{section_name ?? "<unspecified>" }' could not be loaded... please contact your administrator!</p>
                                           <script>
                                                alert('boop!')
                                           </script>
                                       """);

        if (section_name.IsEmpty())
            return section_missing;

        return Partial(section_name, value);
    }


    //IDEA: Not possible, but certainly fun
    // public async IAsyncEnumerable<IActionResult> OnGetNextSection(string section_name = "")
    // {
    //     section_name.Dump();
    //     // if (section_name.IsEmpty()) return Content("<p>No content!</p>");
    //
    //     await foreach (var content in FetchSectionContents())
    //     {
    //         // yield return content;
    //         yield return content;
    //     }
    //
    //     // return Content("<p>Done!</p>");
    // }

    // IDEA: yield return Partials.
    public async IAsyncEnumerable<IActionResult> FetchSectionContents()
    {
        // List<string> contents = new List<string>();
        for (int i = 1; i <= 10; i++)
        {
            // await Task.Delay(1000);
            string html = $"""
                           <section>
                               <h3>Section {i}</h3>
                               <p>lorem</p>
                           </div>
                           """;
            Thread.Sleep(1000);
            yield return Content(html);
        }
    }
}
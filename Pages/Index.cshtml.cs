using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using CodeMechanic.UniqueId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nugsnet6.Models;

namespace nugsnet6.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public string CopyUrl { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(string u, string b)
    {
        u.Dump("user id");
        b.Dump("build id");

        string prod_url_start = "https://lock-n-loadout.up.railway.app/";
        this.CopyUrl = prod_url_start + $"?u={u}" + $"&b={b}";
    }


    public IActionResult OnGetAnonymousUser()
    {
        var uuid = Guid.NewGuid().AsUUID();
        Console.WriteLine("UuId : >> " + uuid);

        return Partial("_Anonymous", new AnonymousUser()
            {
                uuid = uuid
            }
        );
    }

    public async Task<IActionResult> OnGetPricing()
    {
        return Partial("Pricing", default);
    }

    public async Task<IActionResult> OnGetSurvey()
    {
        return Partial("_Survey", default);
    }

    public async Task<IActionResult> OnPostUpdateSurvey([FromBody] Survey survey)
    {
        survey.Dump("survey contents");
        return Partial("Pricing", default);
    }

    public async Task<IActionResult> OnGetSwap()
    {
        return Partial("AlpineMorphSample", default);
    }

    public async Task<IActionResult> OnGetRenderSection(string section_name, object value)
    {
        section_name.Dump("section requested");

        var section_missing = Content(
                $"""
                     <p class='alert alert-error'>Could not load section because the name was unspecified .. Please contact your administrator!</p>
                     <script>
                          alert('boop!')
                     </script>
                 """
            )
            ;

        if (section_name.IsEmpty())
            return section_missing;

        return Partial(section_name, value);
    }
}

public class AnonymousUser
{
    public string uuid { get; set; }
    public string uri { get; set; }
}
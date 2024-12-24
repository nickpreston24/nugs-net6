using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using CodeMechanic.UniqueId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nugsnet6.Models;

namespace nugsnet6.Pages.OldSplash;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    // public string CopyUrl { get; set; }


    public AnonymousUser AnonUser { get; set; } = new AnonymousUser();

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(string u, string b)
    {
        u.Dump("user id");
        b.Dump("build id");

        string prod_url_start = "https://lock-n-loadout.up.railway.app/";
        AnonUser.CopyUrl = prod_url_start + $"?u={u}" + $"&b={b}";
    }

    public IActionResult OnGetAnonymousUser()
    {
        return Partial(
            "_Anonymous",
            new AnonymousUser() { uuid = Guid.NewGuid().AsUUID() }.With(au => AnonUser = au)
        );
    }

    public async Task<IActionResult> OnGetPricing() => Partial("Pricing", default);

    public async Task<IActionResult> OnGetSurvey() => Partial("_Survey", default);

    public async Task<IActionResult> OnPostUpdateSurvey([FromBody] Survey survey) =>
        Partial("_Survey", survey);

    public async Task<IActionResult> OnGetSwap() => Partial("AlpineMorphSample", default);

    public async Task<IActionResult> OnGetRenderSection(string section_name, object value) =>
        section_name.IsEmpty()
            ? Partial("_MissingSection", section_name)
            : Partial(section_name, value);
}

public record BuyerWelcomeConfig
{
    public bool show_private_sales_login { get; set; }
    public bool show_config_test_div { get; set; }
    public DateTimeOffset some_date { get; set; }
    public double some_double { get; set; }
    public float some_float { get; set; }
    public int some_int { get; set; }
    public bool enable_lightsabers { get; set; }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using nugsnet6.Models;
using nugsnet6.Services;

namespace nugsnet6.Pages.Admin;

public class RegexPatterns : PageModel
{
    private readonly IRegexPatternsService regex_svc;
    public List<RegexPattern> CurrentPatterns { get; set; } = new();

    public RegexPatterns(IRegexPatternsService regexSvc)
    {
        regex_svc = regexSvc;
    }

    public async Task OnGet()
    {
        CurrentPatterns = await regex_svc.GetAll();
    }
}
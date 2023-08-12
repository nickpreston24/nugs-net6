using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGetPricing()
    {

    }
}

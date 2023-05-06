using Microsoft.AspNetCore.Mvc.RazorPages;
using CodeMechanic.Embeds;
namespace nugsnet6.Pages.Contribute;

public class IndexModel : PageModel
{
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;

    public IndexModel(IEmbeddedResourceQuery embeddedResourceQuery)
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
    }

    
}
using CodeMechanic.Embeds;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neo4j.Driver;

namespace nugsnet6.Pages.FreeTier;

public class IndexModel : PageModel
{
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;
    private readonly IDriver driver;
    private readonly IAirtableRepo repo;

    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery,
        IDriver driver,
        IAirtableRepo repo
    )
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
        this.driver = driver;
        this.repo = repo;
    }
}

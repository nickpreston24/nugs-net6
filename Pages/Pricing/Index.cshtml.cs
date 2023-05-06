using CodeMechanic.RazorHAT;
using Neo4j.Driver;


using CodeMechanic.Embeds;

namespace nugsnet6.Pages.FreeTier;

public class IndexModel : HighSpeedPageModel
{
    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , IDriver driver
        , IAirtableRepo repo
    ) : base(embeddedResourceQuery, driver, repo)
    {
    }
}
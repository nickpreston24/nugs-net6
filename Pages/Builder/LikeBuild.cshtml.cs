using CodeMechanic.Embeds;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Builder;

public class LikeBuild : PageModel
{
    private readonly EmbeddedResourceService embeds;

    public LikeBuild(IEmbeddedResourceQuery embeds)
    {
        this.embeds = (EmbeddedResourceService)embeds;
    }

    public async void OnGet()
    {
        // string likes_query = await embeds.Read<Program>("BuildsLiked.cypher").ReadAllLinesFromStreamAsync();

        string likes_query = """
                MERGE (p1:User { id: $userName })
                MERGE (p2:Build { name: $buildName })
                MERGE (p1)-[:LIKES]->(p2)
                RETURN p1, p2
            """;
        Console.WriteLine("query :>> " + likes_query);
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CodeMechanic.Extensions;

namespace nugsnet6.Pages.NugBuilder;

public class IndexModel : PageModel
{
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;

    public IndexModel(IEmbeddedResourceQuery embeddedResourceQuery)
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
        // this.assembly = typeof(IndexModel).Assembly;
    }

    public async Task<IActionResult> OnGetRecommendedBuilds()
    {
        string resource = "Pages.NugBuilder.RecommendedBuilds.cypher";

        await using Stream stream = embeddedResourceQuery.Read<IndexModel>(resource);

        string query = await stream.ReadAllLinesFromStreamAsync();

        query = new StackTrace().GetCurrentResourcePath();

        return Content($"{query}");
    }
    
}
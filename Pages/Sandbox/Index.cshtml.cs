
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CodeMechanic.Extensions;
using CodeMechanic.RazorPages;
using Neo4j.Driver;

namespace nugsnet6.Pages.Sandbox;

public class IndexModel : HighSpeedPageModel
{

    private static int count = 0;

    private readonly IEmbeddedResourceQuery embeddedResourceQuery;

    public IndexModel(
    IEmbeddedResourceQuery embeddedResourceQuery
    , IDriver driver)
    :base(embeddedResourceQuery, driver)
    {
        // this.embeddedResourceQuery = embeddedResourceQuery;
        // this.driver = driver;
    }

    public void OnGet()
    {
        // reset on refresh
        count = 0;
    }

    public IActionResult OnPostIncrement()
    {
        Console.WriteLine("Post!");
        return Content($"<span>{++count}</span>", "text/html");
    }

    public IActionResult OnGetStuff()
    {
        return Content($"<b>Stuff for u!<br/> One lump, or 2?</b>");
    }

    public async Task<IActionResult> OnGetRecommendedNugs()
    {
        string query = "...";

        // Magically infers that the current method name is referring to 'RecommendedNugs.cypher'
        string resource = new StackTrace().GetCurrentResourcePath();

        // Reads from file system...
        await using Stream stream = embeddedResourceQuery.Read<IndexModel>(resource);

        // Reads the any file I tell it to as a query.
        query = await stream.ReadAllLinesFromStreamAsync();


        // Run Neo4j query...
        // IDriver driver = GraphDatabase.Driver("neo4j://localhost:7687", AuthTokens.Basic("username", "pasSW0rd"));
        // IAsyncSession session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        // try
        // {
        //     IResultCursor cursor = await session.RunAsync(query);
        //     await cursor.ConsumeAsync();
        // }
        // finally
        // {
        //     await session.CloseAsync();
        // }

        await driver.CloseAsync();
        
        // This can also be a template
        return Content(
            $"<div class='alert alert-primary'><p class='text-xl text-secondary text-sh'>{query}</p></div>");

        // return Content(
        //     $"""
        //     <div class='alert alert-primary'>
        //         <p class='text-xl text-secondary text-sh'>{query}</p>
        //     </div>
        //     """);
    }

}
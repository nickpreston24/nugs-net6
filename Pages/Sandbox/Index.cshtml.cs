
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace nugsnet6.Pages.Sandbox;

public class IndexModel : PageModel
{

    private static int count = 0;

    private IEmbeddedResourceQuery embeddedResourceQuery;

    public IndexModel(IEmbeddedResourceQuery embeddedResourceQuery)
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
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
        string contents = "bob";

        var assembly = typeof(IndexModel).Assembly;

        string resource = "Pages.Sandbox.RecommendedNugs.cypher";
        // string assembly_name  = "nugsnet6";

        await using Stream stream = embeddedResourceQuery.Read<IndexModel>(resource);

        contents = await ReadAllLinesFromStreamAsync(stream);

        return Content($"{contents}");
    }

    private async Task<string> ReadAllLinesFromStreamAsync(Stream stream)
    {
        if (stream == null)
        {
            return string.Empty;
        }
        using (StreamReader reader = new StreamReader(stream))
        {
            // Console.WriteLine("YA MADE IT!!");
            // System.Diagnostics.Debug.WriteLine("YA MADE IT!!");
            string contents = await reader.ReadToEndAsync();

            return contents;
        }

    }

    /// Credit: https://gist.github.com/rflechner/fab685187f10b8eb9815c6af1f874d3d
    /// https://josef.codes/using-embedded-files-in-dotnet-core/
    // public string ReadLocalQuery(Assembly assembly,  string filename) {


    //     using (var stream = assembly.GetManifestResourceStream(filename))
    //         {
    //             if(stream == null)
    //                 return "Stream could not be created for file " + filename;

    //             using (var reader = new StreamReader(stream))
    //             {
    //                 var cypher = reader.ReadToEnd();
    //                 return cypher;
    //             }
    //         }
    // }
}
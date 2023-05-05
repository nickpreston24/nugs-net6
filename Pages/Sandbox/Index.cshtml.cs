
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
using CodeMechanic.RazorHAT;
using CodeMechanic.Embeds;
using Neo4j.Driver;

namespace nugsnet6.Pages.Sandbox;

public class IndexModel : HighSpeedPageModel
{

    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , IDriver driver) 
    : base(embeddedResourceQuery, driver)
    {
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetRecommendedRifles()
    {
        var failure = Content(
            $"<div class='alert alert-error'><p class='text-xl text-warning text-sh'>An Error Occurred...  But fret not! Our team of intelligent lab mice are on the job!</p></div>");

        string query = "...";

        // Magically infers that the current method name is referring to 'RecommendedNugs.cypher'
        string resource = new StackTrace().GetCurrentResourcePath().Dump("resource path");
        if(embeddedResourceQuery == null) 
            return failure;

        // throw new Exception("d'oh!");
        // Reads from file system...
        await using Stream stream = embeddedResourceQuery.Read<IndexModel>(resource);
        if(stream == null)
            Console.WriteLine("Oh no!  I'm deaf!");

        // Reads the any file I tell it to as a query.
        query = await stream.ReadAllLinesFromStreamAsync();


// run query

        // This can also be a template
        return Content(
            $"<div class='alert alert-primary'><p class='text-xl text-secondary text-sh'>{query}</p></div>");

        // return JsonResult(records); // mustache

        // return Content(
        //     $"""
        //     <div class='alert alert-primary'>
        //         <p class='text-xl text-secondary text-sh'>{query}</p>
        //     </div>
        //     """);
    }

}



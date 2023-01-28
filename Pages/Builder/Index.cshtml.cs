
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

namespace nugsnet6.Pages.Builder;

public class IndexModel : HighSpeedPageModel
{

    private static int count = 0;

    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , IDriver driver) 
    : base(embeddedResourceQuery, driver)
    {
    }

    public void OnGet()
    {
        // reset on refresh
        count = 0;
    }
    
    public async Task<IActionResult> OnGetRecommendedBarrels()
    {       
        var failure = Content(
            $"<div class='alert alert-error'><p class='text-xl text-warning text-sh'>An Error Occurred...  But fret not! Our team of intelligent lab mice are on the job!</p></div>");

        string query = "...";

        string resource = new StackTrace().GetCurrentResourcePath();
        if(embeddedResourceQuery == null) 
            return failure;

        await using Stream stream = embeddedResourceQuery.Read<IndexModel>(resource);

        query = await stream.ReadAllLinesFromStreamAsync();

        var records = await NeoFind(query, new {});
        // var graph = records.ToD3Graph();

        return Partial("_PartsGrid", records);
    }

}


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
using nugsnet6.Models;

namespace nugsnet6.Pages.Builder;

public class IndexModel : HighSpeedPageModel
{

    // 'caches' the count value, almost like a reacitve variable, but on the server side...
    private static int count = 0;
    private static AirtableSearch _search = new AirtableSearch();

    private static List<Build> builds_found = new List<Build>();
    public List<Build> BuildsFound => builds_found;

    public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , IDriver driver
        , IAirtableRepo repo
        ) 
    : base(embeddedResourceQuery, driver, repo)
    {
    }

    public void OnGet()
    {
        // reset on the page's refresh
        count = 0;
    }

    public async Task<IActionResult> OnGetSearchBuilds(string Name = "")
    {  
        Name.Dump("name searched");
         var results = await airtable_repo
         .SearchRecords<Build>(_search
            .With(s=>
            {   
                s.table_name = "Builds";
                s.maxRecords = 1000;
                // s.pageSize = 20;
                // s.offset = "30";
                s.filterByFormula = $"(FIND(\"{Name}\", {{Name}}))";
            })
        );
        builds_found = results;
        // results.Dump("results raw");


        return Partial("_BuildsTable", builds_found);
        // return Content($"<p class='alert alert-primary text-white'>Buckaww!! {results.Count}</p>");
    }

    public async Task<IActionResult> OnPostFavoriteBuild()
    {  
        return Content("<p class='alert alert-primary text-white'>He's a chicken!</p>");

    }

    public async Task<IActionResult> OnGetRecommendedBarrels()
    {       
        Console.WriteLine("@ Hello:>> ");
        Debug.WriteLine("@ Hello:>> ");
        
        var failure = Content(
            $"<div class='alert alert-error'><p class='text-xl text-warning text-sh'>An Error Occurred...  But fret not! Our team of intelligent lab mice are on the job!</p></div>");

        string query = "...";

        string resource = new StackTrace().GetCurrentResourcePath();
        if(embeddedResourceQuery == null) 
            return failure;

        await using Stream stream = embeddedResourceQuery.Read<IndexModel>(resource);

        query = await stream.ReadAllLinesFromStreamAsync();

        var records = await SearchNeo4J<Build>(query, new {});
        // var graph = records.ToD3Graph();

        // return Partial("_PartsGrid", records);
        return Content("<div class='alert alert-success shadow-lg'><div><span>Confirmed!</span></div></div>");
    }

}



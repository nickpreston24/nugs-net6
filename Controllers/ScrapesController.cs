using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using nugsnet6.Models;

namespace nugs_seeder.Controllers;

public class ScrapesController : Controller
{
    // private static string pattern = @"((&#36;)|$)(?<price>\d+\.\d+)"\s*\/>"
    public async Task<string> GetContentAsync(string uri, string bearer_token = "", bool debug = false)
    {
        using HttpClient http = new HttpClient();
        if (bearer_token.NotEmpty())
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token);
        var response = await http.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        if (debug)
            Console.WriteLine("content :>> " + content);
        return content;
    }

    [HttpGet]
    public async Task<IActionResult> Welcome(
        string greeting = "Hello there!"
    )
    {
        return Ok(greeting);
    }

    [HttpGet]
    public async Task<IActionResult> Search(
        int limit = 5
    )
    {
        string underwood_uri =
            @"https://www.powdervalley.com/product/underwood-300-blackout-125-grain-ballistic-tip-20/";
        string rounds_uri = @"https://api.airtable.com/v0/app33DDBeyXEGRflo/Rounds?maxRecords=10&view=Grid%20view";
        string builds = await GetContentAsync(rounds_uri, Environment.GetEnvironmentVariable("NUGS_PAT") ?? "",
            debug: false);

        // var content = await GetContentAsync(uri);
        // var rounds_pattern = new Regex();
        // var round = content.Extract<Round>();

        return Ok(
            builds
            // content
            // new
            // {
            //     // round
            //     // content,
            //     // Limit = limit,
            //     // term,
            //     // category,
            //     // findupdates
            // }
        );
    }
}
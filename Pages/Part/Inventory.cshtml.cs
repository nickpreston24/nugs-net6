using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using CodeMechanic.RazorHAT;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neo4j.Driver;
using NSpecifications;
using nugsnet6.Experimental;
using nugsnet6.Extensions;
using nugsnet6;

namespace nugsnet6.Pages.Parts;

public class InventoryModel : PageModel
{
    private readonly IEmbeddedResourceQuery embeddedResourceQuery;
    private readonly IDriver driver;
    private readonly IAirtableRepo repo;

    public void OnGetAllParts()
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> OnGetAllPartsFromCSV()
    {
        var valid_part = new Spec<Models.Part>(p => p.Name.NotEmpty()
                                                    || p.Cost > 0.00
                                                    || p.Notes.NotEmpty()
        );
        
        var text = ReadFromCsv("Parts-Grid view.csv");
        var lines = text.Split('\n');
        // lines.Length.Dump("# of lines");
        if (text.IsEmpty()) throw new Exception("no text found!");
        var headers = text
                .Split('\n')
                .FirstOrDefault()
                .Split(',')
                .Select(header => header.Replace(" ",""))
                .ToArray()
            ;
        headers.Dump("headers");
        var parts = text.Split('\n')
            .Skip(1)
            .Take(1)
            // .Dump("first lines")
            .SelectMany(line => line
                .ExtractFromCsv<Models.Part>(headers))
            .ToList();

        // parts.Where(valid_part).Dump("parts from csv");
        parts.Count.Dump("total found parts");
        // text.Length.Dump("length of csv text");
        return Partial("PartsList", parts.Where(valid_part).Take(10).ToList());
    }


    // TODO: temporary, until I can perfect the EmbeddedResources.cs
    private string ReadFromCsv(string file_name)
    {
        string cwd = Directory.GetCurrentDirectory();
        // cwd.Dump("current dir");

        var grep = new Grepper()
        {
            RootPath = cwd,
            FileSearchMask = "*.*"
        };

        string found_file = grep
            .GetFileNames()
            // .Dump("all found")
            .FirstOrDefault(filename => filename
                .Contains(file_name, StringComparison.OrdinalIgnoreCase));

        // found_file.Dump("file found ");

        string text = System.IO.File.ReadAllText(found_file);

        return text;
    }

    public InventoryModel(
        IEmbeddedResourceQuery embeddedResourceQuery
        , IDriver driver
        , IAirtableRepo repo
    ) 
        // : base(embeddedResourceQuery, driver, repo, nameof(AirsoftLoadout))
    {
        this.embeddedResourceQuery = embeddedResourceQuery;
        this.driver = driver;
        this.repo = repo;
    }
}
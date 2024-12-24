using System.Text.RegularExpressions;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.RegularExpressions;
using Hydro;
using Newtonsoft.Json;
using nugsnet6.Models;

namespace nugsnet6.Pages.Admin;

public class WebClipper : HydroComponent
{
    public string title { get; set; } = "Parts";
    public Part part { get; set; } = new Part();
    public Seller seller { get; set; } = new Seller();

    private readonly IPartsService _database;
    private readonly IJsonConfigService json_svc;

    public WebClipper(IPartsService database, IJsonConfigService jsonSvc)
    {
        json_svc = jsonSvc;
        string json = json_svc.ReadConfig("seed_data.json");
        seller = JsonConvert.DeserializeObject<Seller>(json);

        string regex = seller.cost.regex;
        var regexp = new System.Text.RegularExpressions.Regex(
            regex,
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );
        string cost_text = " $ 999.99";
        cost = cost_text.Extract<ExtractedCost>(regexp).FirstOrDefault();

        // costs.Dump("costs extracted");
        _database = database;

        // Subscribe<SystemMessageEvent>(Handle);
    }

    public ExtractedCost cost { get; set; } = new();

    public override async Task MountAsync()
    {
        // var formData =    ...; // fetch data from database

        // Name = formData.Name;
    }

    public override void Render()
    {
        // ViewBag.IsLongName = Name.Length > 20;
    }

    public async Task Save()
    {
        // save the data
        Console.WriteLine(nameof(Save));
    }

    // public void Handle(SystemMessageEvent message)
    // {
    //     Message = message.Text;
    // }
}

public record Seller
{
    public string url { get; set; } =
        "https://www.sportsmans.com/shooting-gear-gun-supplies/handguns/fn-510-tactical-10mm-auto-47in-black-pistol-221-rounds/p/1794044";

    public Cost cost { get; set; } = new Cost();
}

public record Cost
{
    public string css_selectors { get; set; } = "p.price";

    public string regex { get; set; } = @"(?<currency>\$)\s*?(?<raw_number>\d+\.?\d+)"; // https://regex101.com/r/KaxyoL/1

    public double Value { get; set; } = 0.00;
}

public record ExtractedCost
{
    public string currency { get; set; } = "$";
    public string raw_number { get; set; } = (-1).ToString();

    public override string ToString() => currency + raw_number;
}

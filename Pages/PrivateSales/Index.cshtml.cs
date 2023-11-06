using System.Text.RegularExpressions;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using CodeMechanic.RazorHAT;
using Htmx;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using nugsnet6.Extensions;

namespace nugsnet6.Pages.PrivateSales
{
    [BindProperties]
    public class IndexModel : HighSpeedPageModel
    {
        public List<string> Tabs { get; }
            = new[] { "_MarketPrices", "_PrivateSales" }.ToList();

        [BindProperty(Name = nameof(Tab), SupportsGet = true)]
        public string Tab { get; set; } = string.Empty;

        public bool IsSelected(string name) =>
            name.Equals(Tab?.Trim(), StringComparison.OrdinalIgnoreCase);

        public IndexModel(
            IEmbeddedResourceQuery embeddedResourceQuery
            , IDriver driver
            , IAirtableRepo repo
        ) : base(embeddedResourceQuery, driver, repo, nameof(Loadouts))
        {
            var view_names = new Grepper()
                    {
                        RootPath = Directory.GetCurrentDirectory().Dump("current dir"),
                        FileSearchMask = "*.cshtml",
                        FileNamePattern = @"_\w+",
                        FileSearchLinePattern = @"iframe"
                    }
                    .GetFileNames()
                    .Where(filepath =>
                        filepath.Contains("PrivateSales", StringComparison.OrdinalIgnoreCase)
                        && !filepath.Contains("Tabs", StringComparison.OrdinalIgnoreCase))
                    // .Dump("all partials found in dir")
                    .Select(path => Regex.Replace(path, @".*\/", ""))
                    .Select(path => Regex.Replace(path, @"\.cshtml", ""))
                    // .Dump("partial names")
                ;

            var updated = new List<string>(view_names).Concat(Tabs).Distinct().ToList();

            Tabs = updated;
        }

        public IActionResult OnGet()
        {
            // make sure we have a tab
            Tab = Tabs.Any(IsSelected) ? Tab : Tabs.FirstOrDefault();
            // Tab.Dump("put it on my tab");
            return Request.IsHtmx() && Tab.IsNotEmpty()
                ? Partial("_Tabs", this)
                : Page();
        }

        public string? IsSelectedCss(string tab, string? cssClass)
            => IsSelected(tab) ? cssClass : null;
    }
}
using System.Text.RegularExpressions;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using CodeMechanic.RazorHAT;
using Htmx;
using HyperTextExpression;
// using HyperTextExpression;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using nugsnet6.Extensions;
using static HyperTextExpression.HtmlExp;


namespace nugsnet6.Pages.PrivateSales
{
    [BindProperties]
    public class IndexModel : HighSpeedPageModel
    {
        // private static AirtableSearch currentAirtableSearch = new AirtableSearch();

        public List<string> Views { get; }
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
                    .Dump("partial names")
                ;

            var updated = new List<string>(view_names).Concat(Views).Distinct().ToList();

            updated.Dump("mreged");

            Views = updated;
        }

        public IActionResult OnGet()
        {
            // make sure we have a tab
            Tab = Views.Any(IsSelected) ? Tab : Views.FirstOrDefault();
            // Tab.Dump("put it on my tab");
            return Request.IsHtmx() && Tab.IsNotEmpty()
                ? Partial("_Tabs", this)
                : Page();
        }

        public string? IsSelectedCss(string tab, string? cssClass)
            => IsSelected(tab) ? cssClass /*.Dump("css class")*/ : null;


        private static HtmlEl RenderSalesTab(PrivateSale sale, int index) =>
            HtmlEl("form",
                ("span", Children(
                    ("strong", $"{sale.Id}.")
                )),
                ("input",
                    Attrs(
                        ("type", "checkbox"),
                        (sale.Done ? "checked" : ""),
                        ("hx-put", "/todo-app/update-status"),
                        ("hx-ext", "json-enc"),
                        ("hx-target", "#todo-list")
                    )
                ),
                ("div", "<p>hi ther</p>")
            );
    }

    public record PrivateSale
    {
        public string Header = string.Empty;
        public string Frame { get; set; } = string.Empty;
        public string Id { get; } = new Guid().ToString(); // dummy value
        public bool Done { get; set; } = false; // dummy value
    }
}
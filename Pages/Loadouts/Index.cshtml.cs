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

namespace nugsnet6.Pages.Loadouts
{
    public class IndexModel : HighSpeedPageModel
    {
        private static string _foo;
        public string Foo  => _foo;

        private static AirtableSearch _search = new AirtableSearch();
        public AirtableSearch Search => _search;

        public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
            , IDriver driver
            , IAirtableRepo repo
        ) : base(embeddedResourceQuery, driver, repo, nameof(Loadouts))
        {
        }

        public async void OnGet(string foo = "bar")
        {
            _foo = foo;
        }

        public async Task<IActionResult> OnGetSearchLoadouts(Loadout search, string Name = "Snow Owl")
        {
            try
            {
                search
                .Dump("initial search for loadouts");
                Name.Dump("Passed in name");
                var results = await airtable_repo.SearchRecords<Loadout>(_search
                    .With(s=>
                    {
                        s.maxRecords = 12;
                        s.filterByFormula = $"(FIND(\"{Name}\", {{Name}}))";
                    })
                );

                return Partial("_LoadoutsTable", results);
            }
            catch (Exception ex) {
                var message = ex.ToString();
                var title = ex.Message;

                  return Content($"""
                    <b class='alert alert-error'>{title}</b>
                """);
            }
        }
    }
}

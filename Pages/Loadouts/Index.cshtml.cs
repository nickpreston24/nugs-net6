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

        private static AirtableSearch _search = new AirtableSearch() {table_name = "Parts"};
        public AirtableSearch Search => _search;

        public IndexModel(
        IEmbeddedResourceQuery embeddedResourceQuery
            , IDriver driver
            , IAirtableRepo repo
        ) : base(embeddedResourceQuery, driver, repo)
        {
        }

        public async void OnGet(string foo = "bar")
        {
            _foo = foo;

            var test_search = new AirtableSearch()
                .With(s=>
                    {
                        s.table_name = "Parts";
                        s.offset="5";
                        s.fields= new List<string> {"Notes", "Cost", "Url"};
                    });

            _search = test_search;

            test_search.Dump("test_search");

            var results = await airtable_repo.ListRecords<Part>(test_search);

            results.Dump("results");

        }

        public async Task<IActionResult> OnGetSearchLoadouts(AirtableSearch search)
        {
            _search = search;

            try
            {
                var results = await airtable_repo.ListRecords<Part>(_search);
                return Content("""
                    <b class='alert alert-success'>success</b>
                """);
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

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
        ) : base(embeddedResourceQuery, driver, repo)
        {
        }

        public async void OnGet(string foo = "bar")
        {
            _foo = foo;

            // _search = new AirtableSearch()
            //     .With(s=>
            //         {
            //             s.table_name = "Loadouts";
            //             // s.offset="5";
            //             s.maxRecords = 2;
            //             s.fields= new List<string> {"Notes", "Cost", "Url"};
            //         });

            // var results = await airtable_repo.SearchRecords<Loadout>(_search);

            // results.Dump("results");

        }

        public async Task<IActionResult> OnGetSearchLoadouts(AirtableSearch search)
        {
            try
            {
                var results = await airtable_repo.SearchRecords<Loadout>(_search
                    .With(s=>
                    {
                        s.maxRecords = 12;
                    })
                );

                results.Dump("results");

                // return Content($"""
                //     <b class='alert alert-success'># found: {results.Count}</b>
                // """);

                return Partial("_LoadoutsPanel", results);
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

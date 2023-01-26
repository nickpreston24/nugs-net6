using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using AirtableApiClient;

namespace nugsnet6.Pages.Guides
{
    public class GuideGrid : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnGetStuff() 
        {
            string message = "Guides Grabbed!";
            Thread.Sleep(3000);

            return Content($"<div class='alert alert-secondary'><p>{message}</p></div>");
        }
    }
}

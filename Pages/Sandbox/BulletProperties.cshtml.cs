using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace nugsnet6.Pages.Sandbox
{
    public class BulletPropertiesModel : PageModel
    {
        private static int _count = 0;
        public int Count => _count;

        public void OnGet()
        {
            _count = 0;
        }       

        public IActionResult OnPostIncrementBullets()
        {
             Console.WriteLine("Gotcha!");
            Debug.WriteLine("Gotcha!");
            return Content($"<span>round count{++_count}</span>", "text/html");
        }

        public IActionResult OnGetBullets()
        {
            Console.WriteLine("Gotcha!");
            Debug.WriteLine("Gotcha!");
            return Content($"<b>BAR</b>");
        }


        // public IActionResult OnGetBullets()
        // {
        //     return Content($"<b class='text-8xl'>bar</b>", "text/html");
        // }
    }
}

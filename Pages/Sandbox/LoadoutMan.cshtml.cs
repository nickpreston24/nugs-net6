using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Sandbox;

public class LoadoutMan : PageModel //: PageModel
{
    public LoadoutMan() { }

    public void OnGet() { }

    public async Task<IActionResult> OnGetStuff()
    {
        Console.WriteLine("Hello there! ");
        return Content("<b>stuff aqcuired</b>");
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace nugsnet6.Pages.Sandbox;

public class EleventyModel: PageModel
{

    public EleventyModel() 
    {
 
    }

    public async Task<IActionResult> OnGetStuff() {

        return Content("<b>stuff aqcuired</b>");
    }
}
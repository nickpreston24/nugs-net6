using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Admin;

public class Modal : PageModel
{
    public IActionResult OnGetMyModal()
    {
        return Partial("_Modal", null);
    }
}
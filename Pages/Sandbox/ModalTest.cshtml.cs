using CodeMechanic.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Sandbox;

public class ModalTest : PageModel
{
    public string Message { get; set; } = string.Empty;

    public void OnGet() { }

    public IActionResult OnPostModal()
    {
        Message.Dump("message submitted");
        return Partial("Success", this);
    }
}

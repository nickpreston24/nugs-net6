using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Rounds;
//Note: to remove all comments, replace this pattern with nothing:  // .*$

[BindProperties]
public class HxPostExample : PageModel
{
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; } = -1;

    public HxPostExample()
    {
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostValidate()
    {
        return Content($"<b>Name: {Name}, Age: {Age}</b>");
    }


    public async Task<IActionResult> OnPostRegisterAsync()
    {
        Console.WriteLine(nameof(OnPostRegisterAsync));
        // return RedirectToPage();
        return Content("Got records!");
    }
    
    public async Task<IActionResult> OnDeleteUnRegisterAsync()
    {
        Console.WriteLine(nameof(OnDeleteUnRegisterAsync));
        return Content("unregistered user!");
    }

    public async Task<IActionResult> OnPostRequestInfo()
    {
        Console.WriteLine(nameof(OnPostRequestInfo));

        // return RedirectToPage();
        return Content("Got records!");
    }


    public async Task<IActionResult> OnPostOtherStuff()
    {
        return default;
    }

    public async Task<IActionResult> OnGetMyStuff()
    {
        return Content("Got records!");
    }

    public async Task<IActionResult> OnDeleteSomeStuff()
    {
        Console.WriteLine("Deleting...");
        return Content("Deleted!");
    }

    public async Task<IActionResult> OnPatchNiceStuff()
    {
        return Content("Patched!");
    }
}
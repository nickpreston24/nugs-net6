using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class LoadoutsModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnGetFollowAnotherUser() 
        {
            string message = "Follow Success!";
            
            return Content($"<div class='alert alert-secondary'><p>{message}</p></div>");
        }
    }
}

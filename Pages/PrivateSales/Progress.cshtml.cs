using CodeMechanic.Diagnostics;
using Htmx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nugs_seeder.Controllers;

namespace nugsnet6.Pages.PrivateSales;

public class Progress : PageModel
{
    public List<Listing> Listings { get; set; } = new();
    public List<ListingFilter> Filters { get; set; } = new();

    public void OnGet()
    {
        Listings = new Listing().Repeat(5).ToList();
        Filters.Add(new ListingFilter
        {
            SortDirection = "ASC"
        });
    }


    public IActionResult GetAllListings()
    {
        Console.WriteLine("hello from AllListings!".Dump());
        var listings = new List<Listing>()
        {
            new Listing
            {
                Image_Url =
                    "https://tesla-cdn.thron.com/delivery/public/image/tesla/03c34975-991c-45ee-a340-2b700bf7de01/bvlatuR/std/960x540/meet-your-tesla_model-s",
                Name = "Model S",
            },
            new Listing
            {
                Image_Url =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/2011_Chevrolet_Volt_--_NHTSA_1.jpg/1200px-2011_Chevrolet_Volt_--_NHTSA_1.jpg",
                Name = "Chevy Volt",
            },
            new Listing()
            {
                Image_Url =
                    "https://images.unsplash.com/flagged/photo-1553505192-acca7d4509be?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1yZWxhdGVkfDIwfHx8ZW58MHx8fHx8&auto=format&fit=crop&w=700&q=60",
                Name = "BMW Vista",
            }
        };

        return Request.IsHtmx().Dump("is htmx?")
            ? Partial("_Listings", listings.Dump("listings all"))
            : Page();
    }
}
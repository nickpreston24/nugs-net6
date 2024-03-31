using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Shared;

public class BreadCrumbBar2 : HydroView
{
    public string CurrentPage { get; set; } = "Builder";
}
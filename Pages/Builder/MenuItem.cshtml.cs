using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Builder;

[HtmlTargetElement("menu-item")]
public class MenuItem : HydroView
{
    public string badge_classname => $"badge badge-{badge_size} badge-{badge_type}";
    public string badge_size { get; set; } = "sm";

    public string badge_type { get; set; } = "warning";
}
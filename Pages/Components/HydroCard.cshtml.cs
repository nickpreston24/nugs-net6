using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Components;

// based off of: https://daisyui.com/components/card/
[HtmlTargetElement("hydro-card")]
public class HydroCard : HydroView
{
    public string Title { get; set; } = "Shoes!";
}

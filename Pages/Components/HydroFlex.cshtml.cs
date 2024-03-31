using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Hydro;

namespace nugsnet6.Pages.Components;

[HtmlTargetElement("flex")]
public class HydroFlex : HydroView
{
    public string classname => Orientation.ToString();
    public Flexies Orientation { get; set; } = Flexies.Col;
}
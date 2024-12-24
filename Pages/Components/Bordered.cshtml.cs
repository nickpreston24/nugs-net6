using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Components;

[HtmlTargetElement("bordered")]
public class Bordered : HydroView
{
    public bool Visible { get; set; } = true; //toggles the border
    public string color { get; set; } = "primary";
    public uint Size { get; set; } = 2;
    public string classname => Visible ? $"border-{Size} border-{color}" : "";
}

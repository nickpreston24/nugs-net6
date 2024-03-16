using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Components;

[HtmlTargetElement("box")]
public class HydroBox : HydroView
{
    public int Width { get; set; } = 16;
    public int Height { get; set; } = 16;
    public string classname => $"w-{Width} h-{Height} bg-secondary text-accent border-2 border-accent";
}
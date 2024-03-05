using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Components;

[HtmlTargetElement("hydrosection")]
public class HydroSection : HydroView
{
    public bool Hidden { get; set; }
}
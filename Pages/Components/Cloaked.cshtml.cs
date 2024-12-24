using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Components;

[HtmlTargetElement("cloaked")]
public class Cloaked : HydroView
{
    public bool Hidden { get; set; }
}

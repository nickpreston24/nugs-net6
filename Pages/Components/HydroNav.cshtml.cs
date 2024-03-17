using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Components;

[HtmlTargetElement("hydro-nav")]
public class HydroNav : HydroView
{
    public object [] Links { get; set; }
}
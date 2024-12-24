using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Components;

[HtmlTargetElement("flex")]
public class HydroFlex : HydroView
{
    public string classname => Orientation.ToString();
    public Flexies Orientation { get; set; } = Flexies.Col;
}

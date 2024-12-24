using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;
using nugsnet6.Models;

namespace nugsnet6.Pages.Builder;

[HtmlTargetElement("builder-menu")]
public class BuilderMenu : HydroView
{
    public List<Part> Parts { get; set; } = Enumerable.Empty<Part>().ToList();
}

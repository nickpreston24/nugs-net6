using CodeMechanic.Types;
using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace nugsnet6.Pages.Components;

[HtmlTargetElement("split")]
public class HydroSplit : HydroView
{
    public override void Init(TagHelperContext context)
    {
        Orientation = direction;
        base.Init(context);
    }

    public string direction { get; set; } = "Vertical";
    public SplitOrientation Orientation { get; internal set; } = SplitOrientation.Horizontal;
}

public class SplitOrientation : Enumeration
{
    public static implicit operator SplitOrientation(string name)
    {
        var matching = GetAll<SplitOrientation>().Single(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return matching;
    }

    public static SplitOrientation Vertical = new SplitOrientation(1, nameof(Vertical));
    public static SplitOrientation Horizontal = new SplitOrientation(2, nameof(Horizontal));

    public SplitOrientation(int id, string name) : base(id, name)
    {
    }
}
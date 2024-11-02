using Hydro;

namespace nugsnet6.Pages.Components;

// [HtmlTargetElement("hydro-collapse")]
public class HydroCollapse : HydroComponent
{
    public HydroCollapseProps Props { get; set; } = new();
    // public bool Focused { get; set; }

    public string classname { get; private set; } =
        "collapse collapse-arrow border border-base-300 bg-info text-primary-content focus:bg-secondary focus:text-secondary-content";


    // public void SetFocus(bool value = true)
    // {
    //     Focused = value;
    //     // Focused.Dump("state");
    // }
}

public record HydroCollapseProps
{
    public string Title { get; set; } = "";
    public string Text { get; set; } = "";
}
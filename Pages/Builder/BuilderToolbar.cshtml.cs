using Hydro;

namespace nugsnet6.Pages.Builder;

public class BuilderToolbar : HydroComponent
{
    public BuilderToolbarOption Selected { get; set; } = new();
    public BuilderToolbarOption[] MenuOptions { get; set; } =
        Enumerable.Empty<BuilderToolbarOption>().ToArray();
}

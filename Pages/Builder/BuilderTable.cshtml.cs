using Hydro;
using nugsnet6.Models;

namespace nugsnet6.Pages.Builder;

public class BuilderTable : HydroComponent
{
    public List<Part> Parts { get; set; } = new List<Part>();
}

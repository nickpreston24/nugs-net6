using Hydro;
using nugsnet6.Models;

namespace nugsnet6.Pages.Admin;

public class PartsStats : HydroComponent
{
    public List<Part> Parts { get; set; } = new();
}

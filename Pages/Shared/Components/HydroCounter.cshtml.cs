using Hydro;

namespace nugsnet6.Pages.Shared.Components;

public class HydroCounter : HydroComponent
{
    public int Count { get; set; }

    public void Add()
    {
        Count++;
    }

    public void Reset()
    {
        Count = 0;
    }
}
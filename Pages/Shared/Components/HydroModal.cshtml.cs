using Hydro;

namespace nugsnet6.Pages.Shared.Components;

public class HydroModal : HydroComponent
{
    public bool Show { get; set; } = false;
    public string Title { get; set; } = "Title";
    public string Message { get; set; } = "Content goes here";

    public void ShowModal()
    {
        Show = true;
    }

    public void CloseModal()
    {
        Show = false;
    }
}

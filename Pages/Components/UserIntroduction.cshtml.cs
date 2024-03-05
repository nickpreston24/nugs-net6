using Hydro;

namespace nugsnet6.Pages.Components;

public class UserIntroduction : HydroComponent
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => LastName + ", " + FirstName;
}
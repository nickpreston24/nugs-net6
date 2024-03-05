using CodeMechanic.Types;
using Hydro;

namespace nugsnet6.Pages.Components;

public class RegisterUser : HydroComponent
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => LastName.NotEmpty() && FirstName.NotEmpty() ? LastName + ", " + FirstName : string.Empty;
    public string Message => $"Hello, {FullName}.  Your username will be {user_name}";
    public string user_name => $"lnl.me/{FirstName}-{LastName}";
    public string user_unique_id { get; set; } = string.Empty;
}
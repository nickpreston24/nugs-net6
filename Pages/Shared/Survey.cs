using nugsnet6.Models;

namespace nugsnet6.Models;

public class Survey
{
    public FakeUser FakeUser { get; set; } = new();
    public string Favorite_Caliber { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
}
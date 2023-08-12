namespace nugsnet6;

public class Survey
{
    public User User { get; set; } = new();
    public string Favorite_Caliber { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
}
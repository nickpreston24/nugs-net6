using CodeMechanic.RazorHAT.Models;

namespace nugsnet6;

public class CarouselCardModel
{
    public List<LocalImage> Images { get; set; } = new List<LocalImage>();
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}
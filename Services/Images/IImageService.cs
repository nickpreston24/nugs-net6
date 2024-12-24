using CodeMechanic.RazorHAT.Models;

namespace CodeMechanic.RazorHAT.Services;

public interface IImageService
{
    LocalImage[] GetAllImages(bool devmode = false);
}

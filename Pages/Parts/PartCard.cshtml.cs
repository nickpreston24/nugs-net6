using CodeMechanic.Diagnostics;
using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nugsnet6.Models;

namespace nugsnet6.Pages.Parts;

public class PartCard : HydroComponent
{
    public Part CurrentPart { get; set; } = new();

    public int Count { get; set; }
}
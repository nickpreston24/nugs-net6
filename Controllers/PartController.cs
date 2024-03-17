using CodeMechanic.RazorHAT.Services;
using Microsoft.AspNetCore.Mvc;
using nugsnet6;
using nugsnet6.Models;

namespace TPOT_Links.Controllers;

[Produces("application/json")]
[Route("api/" + nameof(Part))]
public class PartController : Controller
{
    private readonly IPartsService partService;

    public PartController(IPartsService partService)
    {
        this.partService = partService;
    }

    // GET: api/Part    
    [HttpGet]
    public async Task<List<Part>> Get()
    {
        return await partService.GetAll();
    }
}
using Microsoft.AspNetCore.Mvc;
using nugsnet6.Models;

namespace TPOT_Links.Controllers;

[Produces("application/json")]
[Route("api/" + nameof(Part))]
public class PartController : Controller
{
    private readonly IPartService partService;

    public PartController(IPartService partService)
    {
        this.partService = partService;
    }

    // GET: api/Part    
    [HttpGet]
    public IEnumerable<Part> Get()
    {
        return partService.ReadAll();
    }
}
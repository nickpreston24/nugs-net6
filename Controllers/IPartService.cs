using nugsnet6;
using nugsnet6.Models;

namespace TPOT_Links.Controllers;

public interface IPartService
{
    List<Part> ReadAll();
}
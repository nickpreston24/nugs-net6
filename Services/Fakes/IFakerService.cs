using nugsnet6.Models;

namespace CodeMechanic.RazorHAT.Services;

public interface IFakerService
{
    // string[] GetAllRoutes();
    List<AmmoseekRow> GetFakeAmmoPrices(int limit);

    List<Part> ImportPartsFromFile();
}
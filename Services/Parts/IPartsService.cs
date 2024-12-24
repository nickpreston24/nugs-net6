using nugsnet6.Models;

namespace nugsnet6;

public interface IPartsService
{
    Task<List<Part>> GetAll();
    Task<List<Part>> Search(Part search);
    Task<Part> GetById(int id);
    Task<int> Create(params Part[] model);
    Task Update(int id, Part model);
    Task Delete(int id);
    Task<int> GetCount();
    Task<List<string>> FindTables();
}

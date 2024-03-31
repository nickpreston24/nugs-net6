using Neo4j.Driver;
using nugsnet6.Models;

namespace nugsnet6.Services;

public interface IBuilderService
{
    // TODO: look for a way to sync Alpine.js and C# in THIS service...
    Task<int> StartBuild();
    Task<int> SeedPartTypes(int limit = 100);

    Task<List<Build>> Search(Build search);
    Task<Build> GetById(int id);
    Task<int> Create(params Build[] model);
    Task Update(int id, Build model);
    Task Delete(int id);
    Task<int> GetCount();
    Task<List<string>> FindTables();

    Task<List<Build>> GetAll(
        int limit = 1000
        , Func<IRecord, Build> mapper = null
    );
}
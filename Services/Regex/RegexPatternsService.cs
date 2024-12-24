using nugsnet6.Models;

namespace nugsnet6.Services;

public class RegexPatternsService : IRegexPatternsService
{
    public Task<List<RegexPattern>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<RegexPattern>> Search(RegexPattern search)
    {
        throw new NotImplementedException();
    }

    public Task<RegexPattern> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> Create(params RegexPattern[] model)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, RegexPattern model)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCount()
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> FindTables()
    {
        throw new NotImplementedException();
    }
}

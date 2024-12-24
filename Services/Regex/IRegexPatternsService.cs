using nugsnet6.Models;

namespace nugsnet6.Services;

public interface IRegexPatternsService
{
    Task<List<RegexPattern>> GetAll();
    Task<List<RegexPattern>> Search(RegexPattern search);
    Task<RegexPattern> GetById(int id);
    Task<int> Create(params RegexPattern[] model);
    Task Update(int id, RegexPattern model);
    Task Delete(int id);
    Task<int> GetCount();
    Task<List<string>> FindTables();
}

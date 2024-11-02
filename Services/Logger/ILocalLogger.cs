using System.Runtime.CompilerServices;
using CodeMechanic.Models;

namespace CodeMechanic.RazorHAT.Services;

public interface ILocalLogger
{
    FileInfo WriteToFile<T>(string content, [CallerMemberName] string service_name = "");

    Task<List<LogRow>> GetAll();
    Task<List<LogRow>> Search(LogRow search);
    Task<LogRow> GetById(int id);
    Task<int> Create(params LogRow[] model);
    Task Update(int id, LogRow model);
    Task Delete(int id);
    Task<int> GetCount();
    Task<List<string>> FindTables();
}
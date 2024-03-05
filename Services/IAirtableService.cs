namespace CodeMechanic.Airtable;

public interface IAirtableService
{
    List<CurlOptions> GetClient(string curl);

    Task<AirtableRecords> GetProjectsAndTasks();
}
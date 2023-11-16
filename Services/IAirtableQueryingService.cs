using System.Net.Http.Headers;
using CodeMechanic.Diagnostics;
using CodeMechanic.Types;
using nugsnet6.Extensions;

namespace CodeMechanic.RazorHAT.Services;

public class IAirtableQueryingService
{
}

public class AirtableQueryingService : IAirtableQueryingService
{
    protected string base_id = string.Empty;

    protected string personal_access_token = string.Empty;
    private readonly HttpClient http_client;
    private readonly bool debug_mode;

    public AirtableQueryingService(
        HttpClient client
        , string base_id = ""
        , string personal_access_token = ""
        , bool debug_mode = false)
    {
        this.http_client = client ?? throw new ArgumentNullException(nameof(client));

        this.base_id = base_id;
        this.personal_access_token = personal_access_token;
        this.http_client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", personal_access_token);

        this.debug_mode = debug_mode;

        // Show warnings to coders:

        if (base_id.IsEmpty()) Console.WriteLine("WARNING!!! " + nameof(base_id) + " should not be empty!");
        if (this.personal_access_token.IsEmpty())
            Console.WriteLine("WARNING!!! " + nameof(personal_access_token) + " should not be empty!");
    }

    public async Task<List<T>> SearchRecords<T>(AirtableSearch search)
    {
        var (
            table_name,
            offset,
            fields,
            filterByFormula,
            maxRecords,
            pageSize,
            sort,
            view,
            cellFormat,
            timeZone,
            userLocale,
            returnFieldsByFieldId
            ) = search;

        if (debug_mode) search.Dump("Airtable is searching using the following parameters :>> ");

        if (string.IsNullOrEmpty(table_name))
        {
            Console.WriteLine(
                "WARNING: You did not provide a table name for Airtable to search.  Returning an empty list.");
            // table_name = typeof(T).Name + "s";
            return new List<T>();
        }

        var response = await http_client
            .GetAsync(search
                .With(my_search =>
                {
                    my_search.base_id = this.base_id;
                    my_search.table_name = table_name;
                })
                .AsQuery()
            );

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        if (debug_mode) Console.WriteLine("Here's the raw JSON:");
        if (debug_mode) Console.WriteLine(json);

        // var list = new RecordList<T>(json);
        var (_, records) = new RecordList<T>(json);
        return records;
    }
}
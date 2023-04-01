using tpotsnet6;
using CodeMechanic.Extensions;

public static class AirtableConfigurations {
   public static void ConfigureAirtable(this IServiceCollection services)
    {
        string PAT = Environment.GetEnvironmentVariable("NUGS_PAT");
        string tpot_base_key = Environment.GetEnvironmentVariable("NUGS_BASE_KEY");
        services.AddHttpClient<IAirtableRepo, AirtableRepo>(client => {
            client.BaseAddress = new Uri($"https://api.airtable.com/v0/{PAT}");
            return new AirtableRepo(client, tpot_base_key, PAT);
        });
    }
}

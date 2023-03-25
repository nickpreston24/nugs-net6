using nugsnet6;
using CodeMechanic.Extensions;

public static class AirtableConfigurations {
   public static void ConfigureAirtable(this IServiceCollection services)
    {
        string PAT = Environment.GetEnvironmentVariable("NUGS_PAT");
        string nugs_base_key = Environment.GetEnvironmentVariable("NUGS_BASE_KEY");
        // PAT.Dump("api kee");
        // nugs_base_key.Dump("nug kee");

        // services.AddSingleton<IAirtableRepo, AirtableRepo>();

        services.AddHttpClient<IAirtableRepo, AirtableRepo>(client => {
            client.BaseAddress = new Uri($"https://api.airtable.com/v0/{PAT}");
            return new AirtableRepo(client, nugs_base_key, PAT);
        });
    }
}

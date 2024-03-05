using Neo4j.Driver;

using CodeMechanic.RazorHAT;


namespace nugsnet6;

public static class AirtableConfigurations {
    public static void ConfigureAirtable(this IServiceCollection services)
    {
        string PAT = Environment.GetEnvironmentVariable("NUGS_PAT");
        string base_key = Environment.GetEnvironmentVariable("NUGS_BASE_KEY");
        if(string.IsNullOrEmpty(PAT) || string.IsNullOrEmpty(base_key))
            return;

        services.AddHttpClient<IAirtableRepo, AirtableRepo>(client => {
            client.BaseAddress = new Uri($"https://api.airtable.com/v0/{PAT}");
            return new AirtableRepo(client, base_key, PAT);
        });
    }
}

public static class Neo4jConfigurations {
    
    public static void ConfigureNeo4j(this IServiceCollection services)
    {
        string uri =  Environment.GetEnvironmentVariable("NEO4J_URI") ?? string.Empty;
        string user = Environment.GetEnvironmentVariable("NEO4J_USER") ?? string.Empty;
        string password = Environment.GetEnvironmentVariable("NEO4J_PASSWORD") ?? string.Empty;
        
        string airtable_personal_access_token = Environment.GetEnvironmentVariable("NUGS_PAT") ?? string.Empty;


        services.AddControllers();
        services.ConfigureAirtable();
        if(string.IsNullOrEmpty(uri))
            return;

        services.AddSingleton(GraphDatabase.Driver(
            uri
            , AuthTokens.Basic(
                user,
                password
            )
        ));

        // services.AddTransient<IAirtableRepo, AirtableRepo>();
    }

}

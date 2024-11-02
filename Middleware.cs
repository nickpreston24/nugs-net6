using Neo4j.Driver;
using CodeMechanic.RazorHAT;
using CodeMechanic.Types;


namespace nugsnet6;

public static class AirtableConfigurations
{
    public static void ConfigureAirtable(this IServiceCollection services)
    {
        string PAT = Environment.GetEnvironmentVariable("NUGS_PAT");
        string base_key = Environment.GetEnvironmentVariable("NUGS_BASE_KEY");
        if (string.IsNullOrEmpty(PAT) || string.IsNullOrEmpty(base_key))
            return;

        services.AddHttpClient<IAirtableRepo, AirtableRepo>(client =>
        {
            client.BaseAddress = new Uri($"https://api.airtable.com/v0/{PAT}");
            return new AirtableRepo(client, base_key, PAT);
        });
    }
}

public static class Neo4jConfigurations
{
    public static void ConfigureNeo4j(this IServiceCollection services)
    {
        bool is_devmode = (Environment.GetEnvironmentVariable("DEVMODE") ?? string.Empty).ToBoolean();

        string uri = (is_devmode
            ? Environment.GetEnvironmentVariable("LOCAL_NEO4J_URI")
            : Environment.GetEnvironmentVariable("NEO4J_URI")) ?? string.Empty;

        string user = (
            is_devmode
                ? Environment.GetEnvironmentVariable("LOCAL_NEO4J_USER")
                : Environment.GetEnvironmentVariable("NEO4J_USER")) ?? string.Empty;

        string password = (
            is_devmode
                ? Environment.GetEnvironmentVariable("LOCAL_NEO4J_PASSWORD")
                : Environment.GetEnvironmentVariable("NEO4J_PASSWORD")) ?? string.Empty;

        Console.WriteLine("neo uri: " + uri);
        Console.WriteLine("neo user: " + user);
        Console.WriteLine("neo pwd: " + password);


        services.AddControllers();
        services.ConfigureAirtable();

        // Allow empty uri for the sake of launching w/o neo4j support
        if (string.IsNullOrEmpty(uri))
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
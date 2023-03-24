using nugsnet6;

public static class AirtableConfigurations {
   public static void ConfigureAirtable(this IServiceCollection services)
    {
        services.AddScoped<IAirtableRepo>(x =>
        {
            string airtable_api_key = Environment.GetEnvironmentVariable("AIRTABLE_API_KEY");
            string nugs_api_key = Environment.GetEnvironmentVariable("NUGS_BASE_KEY");

            return new AirtableRepo(airtable_api_key, nugs_api_key);
        });
    }
}

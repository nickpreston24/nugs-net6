using nugsnet6;
using CodeMechanic.Extensions;
using Neo4j.Driver;
using DotEnv.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmbeddedResourceQuery, EmbeddedResourceQuery>();


new EnvLoader().Load();

// builder.UseElectron(args);
// builder.UseStartup<Startup>();



// builder.Services.AddServerSideBlazor();

// This method gets called by the runtime. Use this method to add services to the container.
void ConfigureServices(IServiceCollection services)
{
    var reader = new EnvReader();
    string uri = reader["NEO4J_URI"];
    string user = reader["NEO4J_USER"];
    string password = reader["NEO4J_PASSWORD"];

    string airtable_api_key = reader["AIRTABLE_API_KEY"];
    string airtable_bearer_token = reader["AIRTABLE_BEARER_TOKEN"];
    string nugs_api_key = reader["NUGS_BASE_KEY"]; // 

    Console.WriteLine("URI :>> "+ uri);
    Console.WriteLine("User :>> "+ user);
    Console.WriteLine("Password :>> "+ password);
    Console.WriteLine("airtable_api_key :>> "+ airtable_api_key);
    Console.WriteLine("nugs_api_key :>> "+ nugs_api_key);
    Console.WriteLine("airtable_bearer_token :>> "+ airtable_bearer_token);

    services.AddControllers();
    // services.AddScoped<IMovieRepository, MovieRepository>();
    services.AddSingleton(GraphDatabase.Driver(
        uri
        , AuthTokens.Basic(
            user,
            password
        )
    ));
    services.AddSingleton<IAirtableRepo, AirtableRepo>(
        

    );
}

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
// app.MapBlazorHub();

app.Run();

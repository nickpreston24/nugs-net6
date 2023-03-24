using nugsnet6;
using CodeMechanic.Extensions;
using Neo4j.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmbeddedResourceQuery, EmbeddedResourceQuery>();

DotEnv.Load();

// builder.UseElectron(args);
// builder.UseStartup<Startup>();



// builder.Services.AddServerSideBlazor();

// This method gets called by the runtime. Use this method to add services to the container.
void ConfigureServices(IServiceCollection services)
{
    string uri =  Environment.GetEnvironmentVariable("NEO4J_URI") ?? string.Empty;
    string user = Environment.GetEnvironmentVariable("NEO4J_USER") ?? string.Empty;
    string password = Environment.GetEnvironmentVariable("NEO4J_PASSWORD") ?? string.Empty;

    string airtable_api_key = Environment.GetEnvironmentVariable("AIRTABLE_API_KEY") ?? string.Empty;
    string airtable_bearer_token = Environment.GetEnvironmentVariable("AIRTABLE_BEARER_TOKEN") ?? string.Empty;
    string nugs_api_key = Environment.GetEnvironmentVariable("NUGS_BASE_KEY") ?? string.Empty;

    bool devmode = Environment.GetEnvironmentVariable("DEVMODE").ToBoolean();   

    services.AddControllers();
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

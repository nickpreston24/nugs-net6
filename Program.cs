// using ElectronNET.API;
using nugsnet6;
using CodeMechanic.Extensions;
using Neo4j.Driver;
using dotenv.net;
using dotenv.net.Utilities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmbeddedResourceQuery, EmbeddedResourceQuery>();

// builder.UseElectron(args);
// builder.UseStartup<Startup>();


// This method gets called by the runtime. Use this method to add services to the container.
void ConfigureServices(IServiceCollection services)
{
    var envVars = DotEnv.Read();
    string uri = envVars["NEO4J_URI"];
    string user = envVars["NEO4J_USER"];
    string password = envVars["NEO4J_PASSWORD"];


    Console.WriteLine("URI :>> "+ uri);
    Console.WriteLine("User :>> "+ user);
    Console.WriteLine("Password :>> "+ password);

    services.AddControllers();
    // services.AddScoped<IMovieRepository, MovieRepository>();
    services.AddSingleton(GraphDatabase.Driver(
        uri
        , AuthTokens.Basic(
            user,
            password
        )
    ));
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

app.Run();

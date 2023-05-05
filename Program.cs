using nugsnet6;
using CodeMechanic.Extensions;
using Neo4j.Driver;


using CodeMechanic.Embeds;



var builder = WebApplication.CreateBuilder(args);

// Load and inject .env files & values
DotEnv.Load();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmbeddedResourceQuery, EmbeddedResourceQuery>();
// builder.UseElectron(args);


bool devmode = Environment.GetEnvironmentVariable("DEVMODE").ToBoolean();   
bool use_blazor = true;

builder.Services.ConfigureAirtable();
builder.Services.ConfigureNeo4j();
if(use_blazor)
    builder.Services.AddServerSideBlazor();

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
use_blazor = false;
if(use_blazor)
    app.UseEndpoints(endpoints =>
    {	
        endpoints.MapRazorPages(); // existing endpoints
        endpoints.MapBlazorHub();                
    });
else
    app.MapRazorPages();

app.Run();

using System.Reflection;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using nugsnet6;
using TPOT_Links.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Load and inject .env files & values
DotEnv.Load();

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddTransient<IEmbeddedResourceQuery, EmbeddedResourceQuery>();
builder.Services.AddScoped<IPartService, PartService>();
builder.Services.AddSingleton<IJsonConfigService, JsonConfigService>();
builder.Services.AddSingleton<IRazorRoutesService, RazorRoutesService>();

var main_assembly = Assembly.GetExecutingAssembly();
builder.Services.AddSingleton<IEmbeddedResourceQuery>(new EmbeddedResourceService(
    new Assembly[] { main_assembly }, debugMode: false
).CacheAllEmbeddedFileContents());

bool dev_mode = Environment.GetEnvironmentVariable("DEVMODE").ToBoolean();
bool use_blazor = false;

builder.Services.ConfigureAirtable();
builder.Services.ConfigureNeo4j();

if (use_blazor) builder.Services.AddServerSideBlazor();

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
if (use_blazor)
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages(); // existing endpoints
        endpoints.MapBlazorHub();
    });
else
    app.MapRazorPages();

app.Run();
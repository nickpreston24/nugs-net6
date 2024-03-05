using System.Reflection;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.RazorHAT.Services;
using CodeMechanic.Types;
using Hydro.Configuration;
using nugsnet6;
using TPOT_Links.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Load and inject .env files & values
DotEnv.Load();

// Read .env values for setting up services
bool dev_mode = Environment.GetEnvironmentVariable("DEVMODE").ToBoolean();
Console.WriteLine("Developer mode (all debugs enabled)? " + dev_mode);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPartService, PartService>();
var props_service = new PropertyCache();
builder.Services.AddScoped<IJsonConfigService, JsonConfigService>();
builder.Services.AddScoped<IFakerService, FakerService>();

builder.Services.AddSingleton<ILocalLogger, LocalLoggerService>();
builder.Services.AddSingleton<IMarkdownService, MarkdownService>();
builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<IRazorRoutesService, RazorRoutesService>();
builder.Services.AddSingleton<IPropertyCache>(props_service);
builder.Services.AddSingleton<ICsvService>(new CsvService(props_service, dev_mode));

var main_assembly = Assembly.GetExecutingAssembly();
builder.Services.AddSingleton<IEmbeddedResourceQuery>(
    new EmbeddedResourceService(
            new Assembly[]
            {
                main_assembly
            },
            debugMode: false
        )
        .CacheAllEmbeddedFileContents());


builder.Services.AddSingleton<IAirtableQueryingService>(new AirtableQueryingService(
    personal_access_token: Environment.GetEnvironmentVariable("NUGS_PAT")
    , debug_mode: dev_mode
));

builder.Services.ConfigureAirtable();
builder.Services.ConfigureNeo4j();
builder.Services.AddHydro();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHydro(builder.Environment);

app.Run();
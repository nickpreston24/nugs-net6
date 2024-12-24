using CodeMechanic.RazorHAT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nugsnet6.Services.Sqlite;

namespace nugsnet6.Pages.Admin;

public class SqlInsights : PageModel
{
    private readonly ILocalLogger logs_svc;
    private readonly ISqliteInsightsService sqlinsights;
    public List<SQLiteTableInfo> AllTables { get; set; } = new();

    public SqlInsights(ILocalLogger logs_svc, ISqliteInsightsService sqlinsights)
    {
        this.logs_svc = logs_svc;
        this.sqlinsights = sqlinsights;
    }

    public async Task OnGet()
    {
        this.AllTables = await sqlinsights.FindTables();
    }

    public async Task<IActionResult> OnGetSqlPreview()
    {
        Console.WriteLine(nameof(OnGetSqlPreview));
        return Content("Done");
    }
}

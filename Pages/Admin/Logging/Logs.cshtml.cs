using CodeMechanic.Models;
using CodeMechanic.RazorHAT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nugsnet6.Pages.Admin;

[BindProperties]
public class Logs : PageModel
{
    private readonly ILocalLogger logs_svc;
    public List<LogRow> CurrentLogs { get; set; } = new();

    public Logs(ILocalLogger logs_svc)
    {
        this.logs_svc = logs_svc;
    }

    public async Task OnGet()
    {
        CurrentLogs = await logs_svc.GetAll();
    }
}
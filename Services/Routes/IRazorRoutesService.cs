namespace CodeMechanic.RazorHAT.Services;

public interface IRazorRoutesService
{
    string[] GetAllRoutes();
    string[] GetBreadcrumbsForPage(string builder);
    string[] AllRoutes { get; set; }
}

public class RazorRoute
{
    public string subdirectory { get; set; }
    public string file_name { get; set; }
    public string extension { get; set; }
}

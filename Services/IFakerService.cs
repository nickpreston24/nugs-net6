using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using NSpecifications;

namespace CodeMechanic.RazorHAT.Services;

public interface IFakerService
{
    string[] GetAllRoutes();
}

public class FakerService : IFakerService
{
    private readonly bool dev_mode;
    private readonly IEnumerable<string> razor_page_routes;

    public FakerService(bool dev_mode = false)
    {
        razor_page_routes = GetAllRoutes();
        this.dev_mode = dev_mode;
    }

    public string[] GetAllRoutes()
    {
        string current_directory = Environment.CurrentDirectory;

        if (dev_mode) Console.WriteLine("cwd :>> " + current_directory);

        var grepper = new Grepper()
        {
            RootPath = current_directory,
            FileSearchMask = "**.cshtml,**.cshtml.cs",
            Recursive = true,
        };

        var not_blacklisted = new Spec<string>(
            filepath => filepath.Contains("node_modules/")
                        || filepath.Contains("wwwroot/")
                        || filepath.Contains("bin/")
                        || filepath.Contains("obj/")
        );

        var routes = grepper.GetFileNames()
                .Where(not_blacklisted)
                .Dump("routes")
            ;

        return routes.ToArray();
    }
}
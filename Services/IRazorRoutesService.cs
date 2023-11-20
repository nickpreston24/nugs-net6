using System.Text.RegularExpressions;
using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using NSpecifications;
using nugsnet6.Extensions;

namespace CodeMechanic.RazorHAT.Services;

public interface IRazorRoutesService
{
    string[] GetAllRoutes();
    string[] GetBreadcrumbsForPage(string builder);
    string[] AllRoutes { get; set; }
}

public class RazorRoutesService : IRazorRoutesService
{
    private readonly bool dev_mode;
    private static string[] razor_page_routes;

    public RazorRoutesService(
        // string [] blacklist,
        bool dev_mode = false)
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
            FileSearchMask = "**.cs",
            Recursive = true,
        };

        var blacklist = new string[]
        {
            "/Shared/", "/PrivateSales/", "/RSSFeeds/", "/PrivateSales/", "/PrivateSales/"
        };

        var is_blacklisted = new Spec<string>(
            filepath =>
                filepath.Contains("node_modules/")
                || filepath.Contains("wwwroot/")
                || filepath.Contains("bin/")
                || filepath.Contains("obj/")
        );

        RegexOptions options = RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace |
                               RegexOptions.IgnoreCase;

        var regex = new Regex(@"(?<subdirectory>\/?(\w+\/)*)(?<file_name>.*\.(?<extension>cshtml|cs))",
            options);

        var routes = grepper.GetFileNames()
                         .Where(!is_blacklisted)
                         .Select(p => p.Replace(current_directory, ""))
                         .Where(p => p.StartsWith("/Pages") || p.Equals("/"))
                         .Select(p => p.Extract<RazorRoute>(regex)?.FirstOrDefault()?.subdirectory)
                         .Select(p => p.Replace("/Pages", ""))
                         .Except(blacklist)
                         .Distinct()
                     ?? Enumerable.Empty<string>()
            // .Dump("routes")
            ;

        return routes.ToArray();
    }

    public string[] GetBreadcrumbsForPage(string page_name)
    {
        var current_breadcrumbs = this.GetAllRoutes()
                .Where(path => path.Contains(page_name))
            // .Dump("Current breadcrumbs")
            ;

        return current_breadcrumbs.ToArray();
    }

    public string[] AllRoutes { get; set; } = razor_page_routes;
}

public class RazorRoute
{
    public string subdirectory { get; set; }
    public string file_name { get; set; }
    public string extension { get; set; }
}
using System.Text.RegularExpressions;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using NSpecifications;
using nugsnet6.Experimental;
using nugsnet6.Extensions;

namespace CodeMechanic.RazorHAT.Services;

public interface IMarkdownService
{
    string[] AllRoutes { get; set; }
    string[] GetAllMarkdownFiles(string rootpath, bool devmode);
    string[] GetAllMarkdownFolders(string rootpath, bool devmode);
    string[] GetAllEmbeddedMarkdownFiles(string rootpath, bool devmode);
}

public class MarkdownService : IMarkdownService
{
    private readonly IEmbeddedResourceQuery embeds;
    public string[] AllRoutes { get; set; }

    // public MarkdownService()
    // {
    // }

    // public MarkdownService(IEmbeddedResourceQuery embeds)
    // {
    //     this.embeds = embeds;
    // }

    public string[] GetAllEmbeddedMarkdownFiles(string rootpath, bool devmode)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Use this when we only want to discover folders with markdown files (.md)
    /// </summary>
    public string[] GetAllMarkdownFolders(
        string root_folder = ""
        , bool dev_mode = false)
    {
        string current_directory = root_folder.IsEmpty() ? Environment.CurrentDirectory : string.Empty;
        if (dev_mode) Console.WriteLine("cwd :>> " + current_directory);


        var grepper = new Grepper()
        {
            RootPath = current_directory,
            FileSearchMask = "**.md",
            Recursive = true,
            FileSearchLinePattern = MarkdownHeader.header_pattern
        };

        var blacklist = new string[] { "/Shared/", "" };

        var is_blacklisted = new Spec<string>(
            filepath =>
                filepath.Contains("node_modules/")
                || filepath.Contains("wwwroot/")
                || filepath.Contains("bin/")
                || filepath.Contains("obj/")
        );

        RegexOptions options = RegexOptions.Compiled
                               | RegexOptions.Multiline
                               | RegexOptions.IgnorePatternWhitespace
                               | RegexOptions.IgnoreCase;

        var regex = new Regex(@"(?<subdirectory>\/?(\w+\/)*)(?<file_name>.*\.(?<extension>cshtml|cs))",
            options);

        var routes = grepper
                .GetFileNames()
                .Where(!is_blacklisted)
                // .Select(p => p.Replace(current_directory, ""))
                // .Where(p => p.StartsWith("/Pages") || p.Equals("/"))
                // .Select(p => p.Extract<RazorRoute>(regex)?.FirstOrDefault()?.subdirectory)
                // .Select(p => p.Replace("/Pages", ""))
                .Except(blacklist)
                .Distinct()
            ;

        if (dev_mode) routes.Dump("routes containing markdown files");

        return routes.ToArray();
    }

    /// <summary>
    /// Use this when we want to surgically deep-dive into the Markdown itself and verify it's actually markdown, not just an .md
    /// </summary>
    /// <param name="rootpath"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string[] GetAllMarkdownFiles(string rootpath, bool devmode = false)
    {
        throw new NotImplementedException();
    }
}

public class MarkdownFile
{
    public string FilePath { get; set; }

    public MarkdownHeader[] Headers { get; set; }
    public MarkdownTable[] Tables { get; set; }
}

public class MarkdownTable
{
    public static string table_pattern = """
        # TODO: Untested
        /((\r?\n){2}|^)([^\r\n]*\|[^\r\n]*(\r?\n)?)+(?=(\r?\n){2}|$)/  # https://regex101.com/r/8pNnaG/1/codegen?language=csharp
    """;
}

public class MarkdownHeader
{
    public static string header_pattern = """
                (?<=(?<pounds>^#{1,6})\s)(?<text>.*) # https://regex101.com/r/S8sluj/1
        """;

    public static Regex regexp = new Regex(header_pattern);
}
using System.Text.RegularExpressions;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;
using CodeMechanic.FileSystem;
using CodeMechanic.Types;

namespace CodeMechanic.RazorHAT.Services;

public interface IMarkdownService
{
    string[] AllRoutes { get; set; }

    List<MarkdownFile> GetAllMarkdownFiles(string rootpath = "", bool devmode = false);
}

public class MarkdownService : IMarkdownService
{
    private readonly IEmbeddedResourceQuery embeds;
    public string[] AllRoutes { get; set; }


    public List<MarkdownFile> GetAllMarkdownFiles(
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

        var is_blacklisted = new Func<string, bool>(filepath =>
            filepath.Contains("node_modules")
            || filepath.Contains("wwwroot")
            || filepath.Contains("bin")
            || filepath.Contains("obj"));

        RegexOptions options = RegexOptions.Compiled
                               | RegexOptions.Multiline
                               | RegexOptions.IgnorePatternWhitespace
                               | RegexOptions.IgnoreCase;

        var matching_files = grepper
            .GetMatchingFiles()
            .Where(gr => !is_blacklisted(gr.FilePath))
            .ToList();

        var matching_filenames_only = grepper
                .GetFileNames()
                .Where(path => !is_blacklisted(path))
                .ToList()
            ;

        var files_containing_markdown = matching_files
                .Select(grepResult => new MarkdownFile()
                {
                    FilePath = grepResult.FilePath,
                })
                .ToList()
            ;


        var markdownFiles = matching_filenames_only
                .Select(filepath => new MarkdownFile()
                {
                    FilePath = filepath,
                })
                .ToList()
            ;

        if (dev_mode) files_containing_markdown.Dump("files containing markdown text :>> ");

        if (dev_mode) matching_filenames_only.Dump("markdown file names (only) :>> ");

        // is_blacklisted("home/wwwroot/blah/123").Dump("blacklist test?");

        return markdownFiles;
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
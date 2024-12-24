using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using CodeMechanic.RegularExpressions;
using CodeMechanic.Types;

namespace nugsnet6.Models;

/// <summary>
/// This is a nice to have
/// 1. Read regex from specialized Markdown files (tables)
/// 2. Print any Regex string to Markdown table
/// </summary>
public sealed class RegexRepository
{
    private readonly string root_folder;
    public Func<List<MarkdownTableRow>> MarkdownGenerator { get; set; }

    public RegexRepository(string root, IWebHostEnvironment env)
    {
        root_folder = string.IsNullOrWhiteSpace(root)
            ? Path.Combine(env.ContentRootPath, "Regex").Dump("root")
            : root;

        // Yup, you can assign properties to methods as Func<>.
        MarkdownGenerator = FindRegexDefinitionsInMarkdownTables;
    }

    /// <summary>
    /// If there are any local `.md` files containing tables, extract them according to a specific format.
    /// </summary>
    public List<MarkdownTableRow> FindRegexDefinitionsInMarkdownTables()
    {
        // https://regex101.com/r/3EkgmM/1
        string pattern =
            @"^\|(?<name>\s*`?[\s\w]+`?\s*)\|(?<pattern>\s*`{3}.*`{3}\s*)\|(?<description>.*)\|$";
        var grep = new Grepper()
        {
            RootPath = root_folder ?? ".",
            FileSearchMask = @"*.regex.md",
            FileSearchLinePattern = pattern,
        }.Dump("grepper");

        var files_matched = grep.GetMatchingFiles().Dump("raw results");

        // var markdown_files = grep.GetFileNames().Dump("raw file names");

        var lines_only = files_matched.Select(fm => fm.Line);

        var md_from_lines = lines_only
            .SelectMany(line => line.Extract<MarkdownTableRow>(pattern))
            .ToList();

        if (md_from_lines.Dump("markdown from lines only").Count > 0)
            return md_from_lines;

        return new MarkdownTableRow().AsList();
    }
}

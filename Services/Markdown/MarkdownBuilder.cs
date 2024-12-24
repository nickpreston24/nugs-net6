using System.Text;
using CodeMechanic.Types;

namespace nugsnet6;

public sealed class MarkdownBuilder
{
    private readonly List<string> patterns;

    public MarkdownBuilder(List<string> markdown_patterns)
    {
        patterns = markdown_patterns;
    }

    public override string ToString()
    {
        string front = @"""| Name    | Pattern | Description |
                       |--------------|:-----:|-----------:|
        """.Trim();

        string markdown_rows = new StringBuilder("|    ")
            .AppendEach(patterns, null, "|")
            .ToString();

        string markdown_table = $"""
                {front}
                {markdown_rows}
            """.Trim();

        return markdown_table;
    }
}

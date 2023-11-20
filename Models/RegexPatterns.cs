namespace nugsnet6.Models;

public sealed class RegexPatterns
{
    // TODO: Move these to a special JSON CMS (Parsely)
    public readonly static Dictionary<int, string> FacebookComments = new string [] { 
        """
        blah
        """
    }
    .Select((s, index) => new { s, index })
    .ToDictionary(x => x.index, x => x.s.Trim());
}
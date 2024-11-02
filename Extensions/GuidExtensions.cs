using CodeMechanic.Types;

namespace CodeMechanic.UniqueId;

public static class GuidExtensions
{
    public static string AsUUID(this Guid g) => new Ascii85()
        .With(e => e.EnforceMarks = false)
        .Encode(g.ToByteArray());
}
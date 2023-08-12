namespace nugsnet6.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string text) => string.IsNullOrWhiteSpace(text);
    public static bool IsNotEmpty(this string text) => !text.IsEmpty();
    public static bool NotEmpty(this string text) => IsNotEmpty(text);

    public static string ToHumanCase(this string text)
    {
        var words = text.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);

        var tailWords = words.Skip(1)
            .Select(word => char.ToUpper(word[0]) + word.Substring(1))
            .ToArray();

        return $"{string.Join(string.Empty, tailWords)}";
    }

    public static string ToCamelCase2(this string text, bool spaced = false)
    {
        var words = text.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);

        var leadWord = words[0].ToLower();

        var tailWords = words.Skip(1)
            .Select(word => char.ToUpper(word[0])
                            + word.Substring(1)
            )
            .ToArray();

        return $"{leadWord}{string.Join(string.Empty, tailWords)}";
    }


    // public record StringEdit
    // {
    // }
    //
    // public static StringEdit FromPascalCase(this string pascal_text)
    // {
    //     return new StringEdit();
    // }
}
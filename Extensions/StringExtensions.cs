namespace nugsnet6.Extensions;

public static class StringExtensions
{
    public static string AsCurrency(this double num, string not_euros = "$") => $"{not_euros}{num}";

    // public record StringEdit
    // {
    // }
    //
    // public static StringEdit FromPascalCase(this string pascal_text)
    // {
    //     return new StringEdit();
    // }
}

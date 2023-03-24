using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeMechanic.Extensions;

public static class StringExtensions
{
    private const string lower_alphas = "[a-z]+";
    private const string non_alphas_or_digits = @"\W";

    // public static bool IsNumeric(this string input) => Regex.IsMatch(input, @"\d+");

    public static bool IsDate(this string input)
    {
        DateTime nonce = DateTime.Now;
        return DateTime.TryParse(input, out nonce);
    }

    /// <summary>
    /// Returns the left part of this string instance.
    /// </summary>
    /// <param name="count">Number of characters to return.</param>
    public static string Left(this string input, int count)
    {
        return input.Substring(0, Math.Min(input.Length, count));
    }

    /// <summary>
    /// Returns the right part of the string instance.
    /// </summary>
    /// <param name="count">Number of characters to return.</param>
    public static string Right(this string input, int count)
    {
        return input.Substring(
            Math.Max(input.Length - count, 0),
            Math.Min(count, input.Length)
        );
    }

    /// <summary>
    /// Returns the mid part of this string instance.
    /// </summary>
    /// <param name="start">Character index to start return the midstring from.</param>
    /// <returns>Substring or empty string when start is outside range.</returns>
    public static string Mid(this string input, int start)
    {
        return input.Substring(Math.Min(start, input.Length));
    }

    /// <summary>
    /// Returns the mid part of this string instance.
    /// </summary>
    /// <param name="start">Starting character index number.</param>
    /// <param name="count">Number of characters to return.</param>
    /// <returns>Substring or empty string when out of range.</returns>
    public static string Mid(this string input, int start, int count)
    {
        return input.Substring(
            Math.Min(start, input.Length),
            Math.Min(count, Math.Max(input.Length - start, 0))
        );
    }

    // https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.strings.instr?view=net-6.0
    public static int InStr(this string input, string part = "", int start = 0)
    {
        if (input?.Length == 0)
            return 0;

        if (part?.Length == 0)
            return start;

        if (start > input?.Length)
            return 0;

        if (!input.Contains(part))
            return 0;

        return input.IndexOf(part);
    }      
    

    /* COMPARISONS */

    /// <summary>
    /// https://www.dotnetperls.com/levenshtein
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static int DistanceTo(this string first, string second)
    {
        int n = first.Length;
        int m = second.Length;
        int[,] dist = new int[n + 1, m + 1];

        // Verify arguments.
        if (n == 0)
        {
            return m;
        }

        if (m == 0)
        {
            return n;
        }

        // Initialize arrays.
        for (int i = 0; i <= n; dist[i, 0] = i++) { }

        for (int j = 0; j <= m; dist[0, j] = j++) { }

        // Begin looping.
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                // Compute cost.
                int cost = (second[j - 1] == first[i - 1]) ? 0 : 1;
                dist[i, j] = Math.Min(
                    Math.Min(dist[i - 1, j] + 1, dist[i, j - 1] + 1),
                    dist[i - 1, j - 1] + cost
                );
            }
        }
        // Return cost.
        return dist[n, m];
    }

    /// <summary>
    /// Performs equality checking using behaviour similar to that of SQL's LIKE.
    /// </summary>
    /// <param name="text">The string to check for equality.</param>
    /// <param name="match">The mask to check the string against.</param>
    /// <param name="CaseInsensitive">True if the check should be case insensitive.</param>
    /// <returns>Returns true if the string matches the mask.</returns>
    /// <remarks>
    /// All matches are case-insensitive in the invariant culture.
    /// % acts as a multi-character wildcard.
    /// * acts as a multi-character wildcard.
    /// _ acts as a single-character wildcard.
    /// Backslash acts as an escape character.  It needs to be doubled if you wish to
    /// check for an actual backslash.
    /// [abc] searches for multiple characters.
    /// [^abc] matches any character that is not a,b or c
    /// [a-c] matches a, b or c
    /// Published on CodeProject: http://www.codeproject.com/Articles/
    ///         608266/A-Csharp-LIKE-implementation-that-mimics-SQL-LIKE
    /// Credit: https://www.codeproject.com/Tips/608266/A-Csharp-LIKE-implementation-that-mimics-SQL-LIKE
    /// </remarks>
    public static bool Like(this string text, string match, bool CaseInsensitive = true)
    {
        //Nothing matches a null mask or null input string
        if (string.IsNullOrWhiteSpace(match) || string.IsNullOrWhiteSpace(text))
            return false;
        //Null strings are treated as empty and get checked against the mask.
        //If checking is case-insensitive we convert to uppercase to facilitate this.
        if (CaseInsensitive)
        {
            text = text.ToUpperInvariant();
            match = match.ToUpperInvariant();
        }
        //Keeps track of our position in the primary string - s.
        int j = 0;
        //Used to keep track of multi-character wildcards.
        bool matchanymulti = false;
        //Used to keep track of multiple possibility character masks.
        string multicharmask = null;
        bool inversemulticharmask = false;
        for (int i = 0; i < match.Length; i++)
        {
            //If this is the last character of the mask and its a % or * we are done
            if (i == match.Length - 1 && (match[i] == '%' || match[i] == '*'))
                return true;
            //A direct character match allows us to proceed.
            var charcheck = true;
            //Backslash acts as an escape character.  If we encounter it, proceed
            //to the next character.
            if (match[i] == '\\')
            {
                i++;
                if (i == match.Length)
                    i--;
            }
            else
            {
                //If this is a wildcard mask we flag it and proceed with the next character
                //in the mask.
                if (match[i] == '%' || match[i] == '*')
                {
                    matchanymulti = true;
                    continue;
                }
                //If this is a single character wildcard advance one character.
                if (match[i] == '_')
                {
                    //If there is no character to advance we did not find a match.
                    if (j == text.Length)
                        return false;
                    j++;
                    continue;
                }
                if (match[i] == '[')
                {
                    var endbracketidx = match.IndexOf(']', i);
                    //Get the characters to check for.
                    multicharmask = match.Substring(i + 1, endbracketidx - i - 1);
                    //Check for inversed masks
                    inversemulticharmask = multicharmask.StartsWith("^");
                    //Remove the inversed mask character
                    if (inversemulticharmask)
                        multicharmask = multicharmask.Remove(0, 1);
                    //Unescape \^ to ^
                    multicharmask = multicharmask.Replace("\\^", "^");

                    //Prevent direct character checking of the next mask character
                    //and advance to the next mask character.
                    charcheck = false;
                    i = endbracketidx;
                    //Detect and expand character ranges
                    if (multicharmask.Length == 3 && multicharmask[1] == '-')
                    {
                        var newmask = "";
                        var first = multicharmask[0];
                        var last = multicharmask[2];
                        if (last < first)
                        {
                            first = last;
                            last = multicharmask[0];
                        }
                        var c = first;
                        while (c <= last)
                        {
                            newmask += c;
                            c++;
                        }
                        multicharmask = newmask;
                    }
                    //If the mask is invalid we cannot find a mask for it.
                    if (endbracketidx == -1)
                        return false;
                }
            }
            //Keep track of match finding for this character of the mask.
            var matched = false;
            while (j < text.Length)
            {
                //This character matches, move on.
                if (charcheck && text[j] == match[i])
                {
                    j++;
                    matched = true;
                    break;
                }
                //If we need to check for multiple charaters to do.
                if (multicharmask != null)
                {
                    var ismatch = multicharmask.Contains(text[j]);
                    //If this was an inverted mask and we match fail the check for this string.
                    //If this was not an inverted mask check and we did not match fail for this string.
                    if (inversemulticharmask && ismatch || !inversemulticharmask && !ismatch)
                    {
                        //If we have a wildcard preceding us we ignore this failure
                        //and continue checking.
                        if (matchanymulti)
                        {
                            j++;
                            continue;
                        }
                        return false;
                    }
                    j++;
                    matched = true;
                    //Consumse our mask.
                    multicharmask = null;
                    break;
                }
                //We are in an multiple any-character mask, proceed to the next character.
                if (matchanymulti)
                {
                    j++;
                    continue;
                }
                break;
            }
            //We've found a match - proceed.
            if (matched)
            {
                matchanymulti = false;
                continue;
            }

            //If no match our mask fails
            return false;
        }
        //Some characters are left - our mask check fails.
        if (j < text.Length)
            return false;
        //We've processed everything - this is a match.
        return true;
    }

    public static string Santize(this string input_text)
    {
        //return input_text?.Replace("'", "''") ?? "";

        // From: https://stackoverflow.com/questions/3479434/sanitizing-sql-data
        // it escapes \r, \n, \x00, \x1a, baskslash, single quotes, and double quotes
        return Regex.Replace(input_text ?? string.Empty, @"[\r\n\x00\x1a\\'""]", @"\$0");
    }

    public static string ToFormat(this string text, params object[] args) =>
        string.Format(text, args);


    public static string ToPascalCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "";

        value = value.Trim();

        return char.ToUpperInvariant(value[0]) + (value.Length > 1 ? value.Substring(1) : "");
    }
    public static string ToCamelCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "";

        value = value.Trim();

        return char.ToLowerInvariant(value[0]) + (value.Length > 1 ? value.Substring(1) : "");
    }

    public static string ToTitleCase(this string value, CultureInfo culture = null)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        if (culture == null)
            culture = CultureInfo.CurrentCulture;

        return culture.TextInfo.ToTitleCase(value.ToLower());
    }

    public static string RemoveNonAlphanumeric(this string value)
    {
        var next = Regex.Replace(value, non_alphas_or_digits, "");
        return next;
    }

    public static string Wrap(this string text, string line = "") => Wrap(text, line, line);

    public static string Wrap(this string text, string front = "", string back = "")
    {
        return !string.IsNullOrWhiteSpace(text) ? $"{front}{text}{back}" : string.Empty;
    }

    // public static string Prepend(this string text, string line)
    // {
    //     var copy = new StringBuilder(text);
    //     copy.Prepend(line);
    //     var result = copy.ToString();
    //     text = result;
    //     return text;
    //     //return new StringBuilder(text).Prepend(line).ToString();
    // }
}


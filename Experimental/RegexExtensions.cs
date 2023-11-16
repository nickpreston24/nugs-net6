using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;
using CodeMechanic.Diagnostics;
using CodeMechanic.Reflection;
using CodeMechanic.Types;

namespace nugsnet6.Experimental;

public static class RegexExtensions
{
    private static readonly IDictionary<Type, ICollection<PropertyInfo>> _propertyCache =
        new Dictionary<Type, ICollection<PropertyInfo>>();

    private static readonly IDictionary<string, Regex> _regex_cache = new Dictionary<string, Regex>();


    public static bool Matches(this string text, string pattern)
    {
        var options = RegexOptions.Compiled
                      | RegexOptions.IgnoreCase
                      | RegexOptions.ExplicitCapture
                      | RegexOptions.Singleline
                      | RegexOptions.IgnorePatternWhitespace;

        Regex regexp = _regex_cache.TryGetValue(pattern, out regexp)
            ? regexp
            : new Regex(pattern, options);

        var exists = _regex_cache.TryAdd(pattern, regexp);

        return regexp.IsMatch(text);
    }


    // https://regex101.com/r/fs66ZD/1
    private static string csv_parsing_pattern = """
                                                (?<=^|,)(("[^"]*")|([^,]*))(?=$|,)
                                                """;

    // WIP: https://regex101.com/r/Qpka5B/1

    private static string sample_csv_file_contents = """
                                                         "SMITH, JOHN",1234567890,"12/20/2012,11:00",,DRSCONSULT,DR BOB - OFFICE VISIT - CONSULT,SLEEP CENTER,1234567890,,,"a, b"
                                                         "JONES, WILLIAM",1234567890,12/20/2012,12:45,,DRSCONSULT,DR BOB - OFFICE VISIT - CONSULT,SLEEP CENTER,,,,
                                                     """.Trim();

    public static readonly Regex csv_parser = new Regex(csv_parsing_pattern, RegexOptions.Compiled);

    // https://stackoverflow.com/questions/23527369/regular-expression-for-double-quoted-values-in-csv
    public static List<T> ExtractFromCsv<T>(this string csv_text, string[] headers)
    {
        var options =
            RegexOptions.Compiled
            | RegexOptions.IgnoreCase
            | RegexOptions.ExplicitCapture
            | RegexOptions.Singleline
            | RegexOptions.IgnorePatternWhitespace;

        var props = _propertyCache.TryGetProperties<T>().ToList();

        // props.Select(x=>x.Name).Dump("original props");
        var sorted_props = props.OrdinalSort(headers);
        // sorted_props.Select(x=>x.Name).Dump("sorted prop names");

        string adapted_pattern = new StringBuilder("(?<=^|,)")
            .AppendEach(sorted_props, property => $"""(?<{property.Name}>("[^"]*")|([^,]*))(?=$|,)""",
                delimiter: "")
            .ToString();

        adapted_pattern.Dump("final pattern");

        var regex = new Regex(adapted_pattern, options);
        var items = csv_text.Extract<T>(regex);

        // pairs.FirstOrDefault().Dump("csv value pairs");
        return items;
    }


    public static int FindIndex<T>(this List<T> collection, T item)
    {
        // return Array.IndexOf(collection.ToArray(), item);
        return collection
            .Select((element, index) => new { element, index })
            .FirstOrDefault(x => x.element.Equals(item))?.index ?? -1;
    }


    /// <summary>
    /// Sort 
    /// </summary>
    /// <param name="items"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<PropertyInfo> OrdinalSort(
        this List<PropertyInfo> props
        , params string[] preferred)
    {
        // var results = new List<PropertyInfo>(props.ToList().Count);

        var results = props
            .Select((element, index) => new { element, index })
            .OrderBy(kvp =>
            {
                for (var i = 0; i < preferred.Length; ++i)
                {
                    if (kvp.element.Name.Equals(preferred[i], StringComparison.OrdinalIgnoreCase))
                    {
                        return i;
                    }
                }

                return int.MaxValue;
            })
            .ThenByDescending(x => x.index);

        // int ordinal_index = -1;
        // foreach (var propname in expected_order)
        // {
        //     var prop = props.Single(p => p.Name.Equals(propname));
        //     if (prop == null) throw new Exception($"Property {propname} could not be found!");
        //     ordinal_index = props.FindIndex(prop);
        //     results[ordinal_index] = prop;
        // }

        return results.Select(x => x.element).ToList();
    }

    class CsvValuePair
    {
        public string PropertyKey { get; set; } = string.Empty;
        public string RawValue { get; set; } = string.Empty;
    }

    public static List<T> Extract<T>(
        this string text,
        string delimiter = "",
        params Expression<Func<T, object>>[] patterns
    )
    {
        var options =
            RegexOptions.Compiled
            | RegexOptions.IgnoreCase
            | RegexOptions.ExplicitCapture
            | RegexOptions.Multiline
            | RegexOptions.IgnorePatternWhitespace;

        var dictionary = patterns
            .GetExpressionParts()
            .ToDictionary(kv => kv.Key, kv => kv.Value);

        StringBuilder pattern_builder = new StringBuilder();

        foreach (var kvp in dictionary)
        {
            string pattern = kvp.Value.ToString();
            string propertyname = kvp.Key;

            string named_group_pattern = @$"(?<{propertyname}>{pattern}){delimiter}" /*.Dump("named group")*/;

            pattern_builder.Append(named_group_pattern);
        }

        string full_pattern = pattern_builder
                .RemoveFromEnd(delimiter.Length)
                .ToString()
            /* .Dump("full pattern")*/;

        Console.WriteLine(full_pattern);

        var batch = text.Extract<T>(
            full_pattern,
            enforce_exact_match: false,
            options: options);

        return batch;
    }


    public static List<T> Extract<T>(
        this string text,
        Regex regex,
        bool enforce_exact_match = false,
        bool debug = false
    )
    {
        var collection = new List<T>();

        // If we get no text, throw if we're in devmode (debug == true)
        // If in prod, we want to probably return an empty set.
        if (string.IsNullOrWhiteSpace(text))
            return debug
                ? throw new ArgumentNullException(nameof(text))
                : collection;

        // Get the class properties so we can set values to them.
        var props = _propertyCache.TryGetProperties<T>().ToList();

        // If in prod, we want to probably return an empty set.
        if (props.Count == 0)
            return debug
                ? throw new ArgumentNullException($"No properties found for type {typeof(T).Name}")
                : collection;

        var errors = new StringBuilder();

        // if (options == RegexOptions.None)
        //     options =
        //         RegexOptions.Compiled
        //         | RegexOptions.IgnoreCase
        //         | RegexOptions.ExplicitCapture
        //         | RegexOptions.Singleline
        //         | RegexOptions.IgnorePatternWhitespace;

        // var regex = new System.Text.RegularExpressions.Regex(regex_pattern, options, TimeSpan.FromMilliseconds(250));

        var matches = regex.Matches(text).Cast<Match>();

        matches.Aggregate(
            collection,
            (list, match) =>
            {
                if (!match.Success)
                {
                    errors.AppendLine(
                        $"No matches found! Could not extract a '{typeof(T).Name}' instance from regex pattern"
                    );

                    errors.AppendLine(text);

                    var missing = props
                        .Select(property => property.Name)
                        .Except(regex.GetGroupNames(), StringComparer.OrdinalIgnoreCase)
                        .ToArray();

                    if (missing.Length > 0)
                    {
                        errors.AppendLine("Properties without a mapped Group:");
                        missing.Aggregate(errors, (result, name) => result.AppendLine(name));
                    }

                    if (errors.Length > 0)
                        //throw new Exception(errors.ToString());
                        Debug.WriteLine(errors.ToString());
                }

                // This rolls up any and all exceptions encountered and rethrows them,
                // if we're trying to go for an absolute, no exceptions matching of Regex Groups to Class Properties:
                if (enforce_exact_match && match.Groups.Count - 1 != props.Count)
                {
                    errors.AppendLine(
                        $"{MethodBase.GetCurrentMethod().Name}() "
                        + $"WARNING: Number of Matched Groups ({match.Groups.Count}) "
                        + $"does not equal the number of properties for the given class '{typeof(T).Name}'({props.Count})!  "
                        + $"Check the class type and regex pattern for errors and try again."
                    );

                    errors.AppendLine("Values Parsed Successfully:");

                    for (int groupIndex = 1; groupIndex < match.Groups.Count; groupIndex++)
                    {
                        errors.Append($"{match.Groups[groupIndex].Value}\t");
                    }

                    errors.AppendLine();
                    Debug.WriteLine(errors.ToString());
                    //throw new Exception(errors.ToString());
                }

                object instance = Activator.CreateInstance(typeof(T));

                foreach (var property in props)
                {
                    // Get the raw string value that was matched by the Regex for each Group that was captured:
                    string value = match
                        .Groups
                        .Cast<Group>()
                        .SingleOrDefault(group => group.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                        ?.Value
                        .Trim();

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        property.SetValue(
                            instance,
                            value: TypeDescriptor
                                .GetConverter(property.PropertyType)
                                .ConvertFrom(value),
                            index: null
                        );
                    }
                    else if (property.CanWrite)
                    {
                        property?.SetValue(instance, value: null, index: null);
                    }
                }

                list.Add((T)instance);
                return list;
            }
        );

        return collection;
    }
}
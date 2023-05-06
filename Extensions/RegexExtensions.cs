using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using CodeMechanic.Extensions;

namespace CodeMechanic.Advanced.Extensions
{
    public static class RegexExtensions
    {
        private static readonly IDictionary<Type, ICollection<PropertyInfo>> _propertyCache =
                   new Dictionary<Type, ICollection<PropertyInfo>>();

        /// <summary>
        /// Takes a dictionary full of Regex patterns (or words) and swaps those values with whatever you set as the .Value.
        /// 
        /// <usage>
        /// So, for example, a dictionary like this:
        /// 
        /// var replacements = new Dictionary<..>{ { "\d+", "hello there!"}, {"Order", "66"}  }
        /// 
        /// ... and a text string like this:
        /// 
        /// string text = "Order was valued at $100.00";
        /// var altered_text = text.ReplaceAll(replacements);
        /// 
        /// Should look something like:
        /// 
        /// `66 was valued at $hello there!.hello there!`
        /// 
        /// This can be used to do quick (but not comprehensive) replacements to format things like:
        /// * Random Unicode chars you don't want
        /// * Extra spaces
        /// * Other garbage like CLRF
        /// 
        /// It does have a flaw in that the more you replace things, the less reliable it can be, especially if your replacements replace OTHER replacements.  So, tread lightly...
        /// </usage>
        /// </summary>
        public static string[] ReplaceAll(
            this string[] lines,
            Dictionary<string, string> replacementMap
        )
        {
            Dictionary<string, string> map = replacementMap.Aggregate(
                new Dictionary<string, string>(),
                (modified, next) =>
                {
                    // Sometimes in JSON \ have to be represented in unicode.  This reverts it.
                    string fixedKey = next.Key.Replace("%5C", @"\").Replace(@"\\", @"\");
                    string fixedValue = System.Text.RegularExpressions.Regex.Replace(next.Value, @"\""", "'");

                    modified.Add(fixedKey, fixedValue);
                    return modified;
                }
            );

            List<string> results = new List<string>();

            foreach (string line in lines)
            {
                string modified = line;
                foreach (KeyValuePair<string, string> replacement in map)
                {
                    modified = System.Text.RegularExpressions.Regex.Replace(
                        modified,
                        replacement.Key,
                        replacement.Value,
                        RegexOptions.IgnoreCase
                    );
                }
                results.Add(modified);
            }

            return results.ToArray();
        }



        public static string[] ReplaceAll(
            this string[] lines
            , params (string, string)[] replacements
        )
        {
            var dict = replacements.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            return lines.ReplaceAll(dict);
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

            //dictionary.Dump("patterns");
            StringBuilder pattern_builder = new StringBuilder();

            foreach (var kvp in dictionary)
            {
                string pattern = kvp.Value.ToString();
                string propertyname = kvp.Key;

                string named_group_pattern = @$"(?<{propertyname}>{pattern}){delimiter}"/*.Dump("named group")*/;

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

        /// <summary>
        /// 
        /// Extracts any class from text, given a Regex Pattern with Named Capture groups.
        /// 
        /// <usage>
        /// 
        /// 
        /// /* --- ArgumentExtensions.cs --- */
        /// public static IEnumerable<Argument> ExtractArguments(this string[] args)
        ///{
        ///    if (args == null || args.Length == 0)
        ///    {
        ///        return Enumerable.Empty<Argument>();
        ///    }
        ///
        ///    string rawText = string.Join(' ', args);
        ///    string pattern = cli_pattern;
        ///
        ///    var actual_args = rawText.Extract<Argument>(pattern);
        ///
        ///    return actual_args;
        ///}
        ///
        /// /* -- Argument.cs --- */
        /// 
        ///  public string RawCommand { get; set; }= string.Empty;// e.g. 'run'
        ///  public string Flag { get; set; } 
        ///  public string RawValues { get; set; } = string.Empty;
        /// .. other stuff ..
        /// 
        /// The Extract() method requires a pattern.  Here's the one embedded in ArgumentExtenions.cs (from project Shargs)
        /// 
        /// /*        
        /// private static string cli_pattern =
        ///     @"(?<RawCommand>\w+)?\s*" + // Matches command text, e.g. 'run, build, install, whatever'
        ///     @"(?<Flag>--?[a-zA-Z-?]+)" + // Matches flag with one or two dashes, e.g. -d or --dir, for example.
        ///     @"\s*" +  //Matches spaces
        ///     @"(?<RawValues>([a-zA-Z.-?_0-9]+?\s*)*)"; // Matches values after a Flag.  Note: at time of writing, will only match ONE command name IFF a Command is matched.
        /// */
        /// 
        /// See the '?<' ?  That lets us name something that we capture in side of our parenthesis '()'.
        /// 
        /// So, in the first line, we look for '\w+' or any 'word' characters A thru Z and the underscore '_'.
        /// 
        /// This method will match the first words you type and assign those words to the 'RawCommand' property.
        /// 
        /// And so on, and so on, through all the Regexes and class properties, by name.
        /// 
        /// As long as you name your Captured Groups the same as your Class Properties, you're good to go.
        /// 
        /// I did not support private fields, but that's IS possible.  It's just so unlikely, I left it out.
        /// 
        /// </usage> 
        /// 
        /// What is this good for???
        /// 1. Line Items - Got a weird record in a database?  Not in XML or JSON?  Parse it directly to a DTO using this Extract() method.
        /// 
        /// 2. Grepping - If you wanted, you could search an entire drive for a particular bit of text (FH_Grep), then Extract() out whatever you wanted from that file, no matter how badly formatted it is.
        /// 
        /// 3. Validation - You could easily find, say, a Regex for Emails and add that to your API validation to complement other validators. 
        /// 
        /// For example, using Fluent Validation, you could adapt this [example](https://docs.fluentvalidation.net/en/latest/index.html):
        /// 
        /// ... 
        /// RuleFor(x => x.Surname).NotEmpty();
        /// RuleFor(x => x.Forename).NotEmpty().WithMessage("Please specify a first name");
        /// RuleFor(x => x.Discount).NotEqual(0).When(x => x.HasDiscount);
        /// RuleFor(x => x.Address).Length(20, 250);
        /// RuleFor(x => x.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        /// 
        /// // Your code:
        /// // RuleFor(customer => customer.email).Matches("email regex");  // Meh...
        /// 
        /// // Could instead be ...
        /// string customer_regex = "(?<Surname>\w+)?\s* ...[more properties here]... (?<Email>\w+@\w*.com...blahdeeblah) ";
        /// var customer_dto = customer_line.Extract<Customer>();
        /// ...
        /// 
        /// With this, you get Validation + Mapping, all in one line.
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">
        /// This can be any string, but for best results, go with a single line at a time
        /// </param>
        /// <param name="regex_pattern"></param>
        /// <param name="enforce_exact_match">
        /// Toggles whether to get all or nothing when it comes to the object's properties matching the Regex Captured Groups
        /// </param>
        /// <param name="debug">
        /// Determines whether this will throw Exceptions on bad inputs.  The default is to quietly fail.
        /// </param>
        /// <param name="options">
        /// Overload this with whatever RegexOption you need.  
        /// I've added a good default set that covers most cases of Extracting most C# classes reliably.
        /// </param>
        /// <returns>
        /// List<typeparamref name="T"/>
        /// </returns>
        public static List<T> Extract<T>(
           this string text,
           string regex_pattern,
           bool enforce_exact_match = false,
           bool debug = false,
           RegexOptions options = RegexOptions.None
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
            var props = _propertyCache.TryGetProperties<T>();

            // If in prod, we want to probably return an empty set.
            if (props.Count == 0)
                return debug
                    ? throw new ArgumentNullException($"No properties found for type {typeof(T).Name}")
                    : collection;

            var errors = new StringBuilder();

            if (options == RegexOptions.None)
                options =
                    RegexOptions.Compiled
                    | RegexOptions.IgnoreCase
                    | RegexOptions.ExplicitCapture
                    | RegexOptions.Singleline
                    | RegexOptions.IgnorePatternWhitespace;

            var regex = new System.Text.RegularExpressions.Regex(regex_pattern, options, TimeSpan.FromMilliseconds(250));

            var matches = regex.Matches(text).Cast<Match>();


            matches.Aggregate(
                collection,
                (list, match) =>
            {
                if (!match.Success)
                {
                    errors.AppendLine(
                            $"No matches found! Could not extract a '{typeof(T).Name}' instance from regex pattern:\n{regex_pattern}.\n"
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
                       .SingleOrDefault(group => group.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase))?.Value
                       .Trim();

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        property.SetValue(
                            instance,
                            TypeDescriptor
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

        // // (Experimental)
        // public static List<T> Extract<T>(
        //     this string text,
        //     Dictionary<Expression<Func<T, object>>, string> patterns
        // )
        // {
        //     var pattern_strings = patterns
        //         .Aggregate(new List<string>(), (current, kvp) =>
        //         {
        //             var property_name = MemberExtensions.GetMemberName(kvp.Key).Dump("propertyname");
        //             var raw_pattern = kvp.Value;

        //             string pattern_segment = string.IsNullOrWhiteSpace(raw_pattern) || !raw_pattern.Contains("?<")
        //                 ? $@"(?<{property_name}>{raw_pattern})"
        //                 : raw_pattern;

        //             current.Add(pattern_segment);
        //             return current;
        //         });

        //     var possible_patterns = pattern_strings
        //         .ToArray().GetPermutations().Dump("possible patterns");

        //     string built_pattern = pattern_strings
        //         .Aggregate(new StringBuilder()
        //         , (sb, next) => sb.Append(next)).ToString();

        //     built_pattern.Dump("built_pattern");

        //     var options =
        //             RegexOptions.Compiled
        //             | RegexOptions.IgnoreCase
        //             | RegexOptions.ExplicitCapture
        //             | RegexOptions.Multiline
        //             | RegexOptions.IgnorePatternWhitespace;

        //     return Extract<T>(text, built_pattern, options: options);
        // }


    }
}
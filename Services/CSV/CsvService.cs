using System.Globalization;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;
using CodeMechanic.Types;
using CsvHelper;
using CsvHelper.Configuration;

namespace CodeMechanic.RazorHAT.Services;

public class CsvService : ICsvService
{
    private string starting_directory = "Downloads";
    private readonly bool debug_mode;
    private readonly IPropertyCache property_cache;

    public CsvService(
        IPropertyCache propertyCache = null
        , bool debugmode = false)
    {
        this.property_cache = propertyCache ?? throw new Exception(nameof(IPropertyCache) + " wired up incorrectly!");
        this.debug_mode = debugmode;
    }

    public List<T> ImportAs<T>(string filepath, string regexp)
    {
        var options = RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace |
                      RegexOptions.IgnoreCase;
        var rgx = new Regex(regexp, options);

        var all_lines = File.ReadAllLines(filepath);

        Console.WriteLine("headers line \n" + all_lines[0]);
        Console.WriteLine("next line \n" + all_lines[1]);


        var result = all_lines
            .SelectMany(line => line.Extract<T>(rgx))
            .ToList();

        return result;
    }

    public IEnumerable<T> Read<T>(string filepath)
    {
        if (filepath.IsEmpty()) throw new ArgumentNullException(nameof(filepath));

        using var reader = new StreamReader(filepath);
        var csvConfig = GetCsvConfig();

        using var csv = new CsvReader(reader, csvConfig);
        var records = csv.GetRecords<T>();
        return records.ToList();
    }

    private static CsvConfiguration GetCsvConfig()
    {
        return new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            IgnoreBlankLines = true,
            PrepareHeaderForMatch = args =>
            {
                string header = args.Header.ToLowerInvariant();
                Regex.Replace(header, @"\s", string.Empty);
                return header;
            }
        };
    }

    public IEnumerable<T> Read<T>(string filepath, Func<CsvReader, T> recordmapper)
    {
        // https://stackoverflow.com/questions/49521193/csvhelper-ignore-case-for-header-names
        var csvConfig = GetCsvConfig();

        using var reader = new StreamReader(filepath);
        using var csv = new CsvReader(reader, csvConfig);
        var records = new List<T>();
        csv.Read();
        csv.ReadHeader();
        while (csv.Read())
        {
            var record = recordmapper(csv);
            records.Add(record);
        }

        return records;
    }
}
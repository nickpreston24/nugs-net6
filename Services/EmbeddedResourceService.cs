using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;
using CodeMechanic.Diagnostics;
using CodeMechanic.Embeds;

namespace CodeMechanic.RazorHAT.Services;

public class EmbeddedResourceService : IEmbeddedResourceQuery
{
    private readonly Dictionary<Assembly, string> _assemblyNames;
    private Dictionary<string, Stream?> cached_resources = new Dictionary<string, Stream?>();
    private Dictionary<string, string> cached_contents = new Dictionary<string, string>();

    private bool debug_mode;
    private readonly List<Assembly> _assembies_to_preload;


    public EmbeddedResourceService(bool debug_mode = false) : this(Array.Empty<Assembly>(), debug_mode)
    {
        this._assemblyNames.Dump("Assemblies to preload");
        this.debug_mode = debug_mode;
    }

    public EmbeddedResourceService(IEnumerable<Assembly> assembliesToPreload, bool debugMode = false)
    {
        _assemblyNames = new Dictionary<Assembly, string>();
        _assembies_to_preload = assembliesToPreload.ToList();
        this.debug_mode = debugMode;

        foreach (var assembly in assembliesToPreload)
        {
            _assemblyNames.Add(assembly, assembly.GetName().Name!);
        }
    }


    public EmbeddedResourceService CacheAllEmbeddedFileContents()
    {
        PreloadAllResources();

        cached_contents = this.cached_resources
            .Aggregate(new Dictionary<string, string>(),
                (file_texts, next_filename) =>
                {
                    // Console.WriteLine("reading file " + next_filename.Key);
                    string text = next_filename.Value.ReadAllLinesFromStream();
                    // Console.WriteLine("text :>> " + text);
                    file_texts.Add(next_filename.Key, text);
                    return file_texts;
                });

        if (debug_mode) cached_contents.Dump("all cached resource text");

        return this;
    }

    public string GetFileContents<T>(
        string file_name
        // , StackTrace trace = null
        // , [CallerMemberName] string caller_method = ""
        , [CallerFilePath] string caller_path = ""
        , bool debug_mode = false
    )
    {
        var assembly = typeof(T).Assembly;
        string assembly_name = assembly.GetName().Name;
        if (debug_mode) Console.WriteLine("assembly name :>> " + assembly_name);
        var options = RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase |
                      RegexOptions.IgnorePatternWhitespace;

        if (debug_mode) Console.WriteLine("caller path :>> " + caller_path);
        if (debug_mode) Console.WriteLine("type name :>> " + typeof(T).Name);
        if (debug_mode) Console.WriteLine("namespace name :>> " + typeof(T).Namespace);
        // if (debug_mode) Console.WriteLine("caller method :>> " + caller_method);

        string regexp = $@"(Pages\/)(?<directory>([\da-zA-Z_-]+\/)*)(?<filename>[\da-zA-Z_-]+)\.cshtml";
        // Console.WriteLine(regexp);
        // https://regex101.com/r/uCRlKe/1
        var assembly_info = caller_path
                .Extract<AssemblyInfo>(
                    regexp,
                    options: options)
            ;
        string subdirectory = assembly_info /*.Dump("assemblyinfo")*/
            .FirstOrDefault()?
            .Directory.Replace("/", ".");

        if (debug_mode) Console.WriteLine("subdir:>" + subdirectory);

        if (debug_mode) cached_contents.Dump("all files on record...");
        string full_path = assembly_name + ".Pages." + subdirectory + file_name;
        if (debug_mode) Console.WriteLine("full path: " + full_path);
        var file_contents = this.cached_contents.TryGetValue(full_path, out var file_text)
                ? file_text
                : cached_contents.TryGetValue(this.cached_contents.Keys.FirstOrDefault(x => x.Contains(file_name))
                    , out file_text)
                    ? file_text
                    : throw new Exception(
                        $"Could not find embedded resource {file_name} in subdirectory {subdirectory}")
            ;
        return file_contents;
    }


    public EmbeddedResourceService PreloadAllResources()
    {
        foreach (var assembly in _assembies_to_preload)
        {
            string[] names = assembly.GetManifestResourceNames();
            Print(names);

            if (!_assemblyNames.ContainsKey(assembly))
            {
                _assemblyNames[assembly] = assembly.GetName().Name!;
            }

            var next_cache = names
                .Aggregate(new Dictionary<string, Stream?>()
                    , (cache, next_resource) =>
                    {
                        var stream = assembly.GetManifestResourceStream(next_resource);
                        cache.TryAdd(next_resource, stream);
                        return cache;
                    });

            cached_resources = next_cache;
        }

        return this;
    }


    public Stream? Read<T>(string resource)
    {
        var assembly = typeof(T).Assembly;
        return ReadInternal(assembly, resource);
    }

    public Stream? Read(Assembly assembly, string resource)
    {
        return ReadInternal(assembly, resource);
    }

    public Stream? Read(string assemblyName, string resource)
    {
        var assembly = Assembly.Load(assemblyName);
        return ReadInternal(assembly, resource);
    }

    internal Stream? ReadInternal(Assembly assembly, string resource)
    {
        Console.WriteLine("Resource: " + resource);

        string[] names = assembly.GetManifestResourceNames();
        Print(names);

        if (!_assemblyNames.ContainsKey(assembly))
        {
            _assemblyNames[assembly] = assembly.GetName().Name!;
        }

        return assembly.GetManifestResourceStream(resource);
    }

    private void Print(params string[] values)
    {
        if (!debug_mode) return;

        Console.WriteLine("<ul>");

        foreach (var value in values)
        {
            Console.WriteLine(value);
            Debug.WriteLine(value);
        }

        Console.WriteLine("</ul>");
    }
}

public class AssemblyInfo
{
    public string Directory { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}

public static class EmbedExtensions
{
    public static string ReadAllLinesFromStream(this Stream stream)
    {
        if (stream == null)
        {
            return string.Empty;
        }

        using StreamReader reader = new StreamReader(stream);
        string contents = reader.ReadToEnd();

        return contents;
    }
}
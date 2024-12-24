using System.Diagnostics;
using System.Text;
using CodeMechanic.Types;
using CsvHelper;

namespace CodeMechanic.RazorHAT.Services;

public interface ICsvService
{
    // List<T> ImportAs<T>(string filepath, string regexp);
    public IEnumerable<T> Read<T>(string filepath);
    IEnumerable<T> Read<T>(string filepath, Func<CsvReader, T> recordmapper);
}

public class FilePathBuilder
{
    private readonly string root_directory;
    private List<string> build_history = new List<string>();

    public FilePathBuilder(string root)
    {
        this.root_directory = root.IsEmpty()
            ? Environment.CurrentDirectory
            : throw new ArgumentNullException(root);

        if (!Directory.Exists(this.root_directory))
            Directory.CreateDirectory(this.root_directory);
    }

    public FilePathBuilder AppendDirectory(string folder_name = "")
    {
        string next_subdirectory = Path.Combine(this.root_directory, folder_name);

        // Save the paths we add, in case of the need for rollbacks.
        build_history.Add(next_subdirectory);

        if (!Directory.Exists(next_subdirectory))
            Directory.CreateDirectory(next_subdirectory);

        return this;
    }

    public PathInfo FullPath() => new PathInfo();

    public string PrintHistory()
    {
        string text = new StringBuilder().AppendEach(build_history, delimiter: "\n").ToString();
        Console.WriteLine(text);
        Debug.WriteLine(text);
        return text;
    }
}

public class PathInfo
{
    public FileInfo FileMeta { get; }
    public DirectoryInfo DirMeta { get; }
    public string FullPath { get; set; }
}

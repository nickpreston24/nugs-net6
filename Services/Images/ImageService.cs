using System.Text;
using System.Text.RegularExpressions;
using CodeMechanic.RegularExpressions;
using CodeMechanic.Diagnostics;
using CodeMechanic.FileSystem;
using CodeMechanic.RazorHAT.Models;
using CodeMechanic.Types;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CodeMechanic.RazorHAT.Services;

public class ImageService : IImageService
{
    private IHostingEnvironment Environment;

    public ImageService(IHostingEnvironment _environment)
    {
        Environment = _environment ?? throw new ArgumentNullException(nameof(_environment));
    }

    public LocalImage[] GetAllImages(bool dev_mode = true)
    {
        string wwwroot_folder = Environment.WebRootPath;

        if (dev_mode) Console.WriteLine("wwwroot path :>> " + wwwroot_folder);

        // TODO: Work on supporting more of these
        string[] supported_file_types = new[] { ".png", ".jpg", ".webp" };
        string full_mask = new StringBuilder()
            .AppendEach(supported_file_types, ft => "**" + ft, delimiter: ",")
            .ToString();

        
        if (dev_mode) Console.WriteLine("full mask :>> " + full_mask);

        var grepper = new Grepper()
        {
            RootPath = wwwroot_folder + "/img",
            FileSearchMask = full_mask,
            Recursive = true,
        };

        // var blacklist = new string[] { "lib/" };

        var results = grepper
                .GetFileNames()
                // .Except(blacklist)
                .Select(file_path => new LocalImage()
                    {
                        FilePath = file_path
                    }
                    .With(updates =>
                    {
                        var opts = RegexOptions.IgnoreCase
                                   | RegexOptions.IgnorePatternWhitespace
                                   | RegexOptions.Multiline;

                        var pattern = """
                            (?<front>.*)\b\/wwwroot\b(?<back>.*)   
                        """;

                        var split_path = updates.FilePath
                            .Extract<PathSplit>(pattern, options: opts)
                            .SingleOrDefault();

                        updates.src = updates.RelativePath = split_path?.back;
                    })
                )
                .ToArray()
            ;
        if (dev_mode) results.Dump("local images found");

        return results;
    }

    public class PathSplit
    {
        public string front { get; set; } = string.Empty;
        public string back { get; set; } = string.Empty;
    }
}
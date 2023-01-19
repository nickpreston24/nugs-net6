using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeMechanic.Extensions 
{

    public static class  EmbedExtensions {

        /// Credit: https://gist.github.com/rflechner/fab685187f10b8eb9815c6af1f874d3d
        /// https://josef.codes/using-embedded-files-in-dotnet-core/
        public static string GetCurrentResourcePath(
            this StackTrace stackTrace
        , string extension = "cypher"
        , [CallerFilePath] string caller_path = ""
        , [CallerMemberName] string caller_method =""
        , bool exclude_crud = true)  
        {
            var frame = stackTrace.GetFrame(0); // this int controls the level by which we go up or down the stack.
            var method = frame.GetMethod();
            // var assembly = method.DeclaringType.Assembly;

            string full_namespace = method?.DeclaringType?.Namespace;

            string path = $"{full_namespace}.{caller_method}.{extension}";

            if(exclude_crud)
                path = path
                    .Replace("OnGet", "")
                    .Replace("OnPatch", "")
                    .Replace("OnDelete", "")
                    .Replace("OnUpdate", "")
                    .Replace("OnPost", "");

            return path;
        }
    
        public static async Task<string> ReadAllLinesFromStreamAsync(this Stream stream)
        {
            if (stream == null)
            {
                return string.Empty;
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                // Console.WriteLine("YA MADE IT!!");
                // System.Diagnostics.Debug.WriteLine("YA MADE IT!!");
                string contents = await reader.ReadToEndAsync();

                return contents;
            }

        }
    }
}
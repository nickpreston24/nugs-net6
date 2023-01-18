using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace nugsnet6
{
    public class EmbeddedResourceQuery : IEmbeddedResourceQuery
    {
        private readonly Dictionary<Assembly, string> _assemblyNames;

        public EmbeddedResourceQuery() : this(Array.Empty<Assembly>())
        {
            
        }

        public EmbeddedResourceQuery(IEnumerable<Assembly> assembliesToPreload)
        {
            _assemblyNames = new Dictionary<Assembly, string>();
            foreach (var assembly in assembliesToPreload)
            {
                _assemblyNames.Add(assembly, assembly.GetName().Name!);
            }
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
            string[] names = assembly.GetManifestResourceNames(); 
            Print(names);

            string assembly_name = assembly.GetName().Name;
            Console.WriteLine(assembly_name);
            // Debug.WriteLine(assembly_name);
            if (!_assemblyNames.ContainsKey(assembly))
            {
                _assemblyNames[assembly] = assembly.GetName().Name!;
            }
            string stream_path = $"{_assemblyNames[assembly]}.{resource}";
            Console.WriteLine("STREAM: " + stream_path);
            return assembly.GetManifestResourceStream(stream_path);
        }

        private void Print(params string [] values) {
            Console.WriteLine("<ul>");

            foreach(var value in values) {
                Console.WriteLine(value);
                // Debug.WriteLine(value);
            }
            Console.WriteLine("</ul>");

        }
    }
}

using System.Reflection;
using CodeMechanic.Reflection;
using CodeMechanic.Types;

namespace CodeMechanic.RazorHAT.Services;

public interface IPropertyCache
{
    PropertyInfo[] GetProperties<T>(params PropertyInfo[] props);
    string[] GetPropertyNames<T>();
}

public class PropertyCache : IPropertyCache
{
    private static readonly IDictionary<Type, ICollection<PropertyInfo>> property_cache =
        new Dictionary<Type, ICollection<PropertyInfo>>();

    private static readonly IDictionary<Type, string> property_names = new Dictionary<Type, string>();

    public PropertyInfo[] GetProperties<T>(params PropertyInfo[] props)
    {
        var properties = props?.Length > 0
            ? property_cache
                .TryGetProperties<T>(true)
                .ToArray()
            : Enumerable.Empty<PropertyInfo>();

        return properties.ToArray();
    }


    public string[] GetPropertyNames<T>()
    {
        Type objType = typeof(T);
        // string prop_name;

        lock (property_cache)
        {
            var names = property_cache
                .TryGetProperties<T>()
                .Select(x => x.Name)
                .ToArray();

            return names;

            // var got_value = property_names.TryGetValue(objType, out prop_name);
            // if (!got_value)
            // {
            //     var next_prop_name = property_cache
            //         .TryGetProperties<T>()
            //         .SingleOrDefault(p => p.Name.Equals(prop_name, StringComparison.InvariantCultureIgnoreCase))
            //         .ToMaybe()
            //         .Case(some: (pi) => pi.Name, () => { return ""; });
            //
            //
            //     bool success = property_names.TryAdd(objType, next_prop_name);
            // }
            //
            // return prop_name;
        }
    }
}
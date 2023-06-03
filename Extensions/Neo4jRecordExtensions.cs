using System.Reflection;
using CodeMechanic.Diagnostics;
using CodeMechanic.Reflection;
using CodeMechanic.Types;
using Neo4j.Driver;

namespace CodeMechanic.Extensions;

public static class Neo4jRecordExtensions 
{
    /// <summary>
    /// PropertyCache stores the properties we wish to use again so we only have to run Reflection once per property.
    /// </summary>
    private static readonly IDictionary<Type, ICollection<PropertyInfo>> _propertyCache =
        new Dictionary<Type, ICollection<PropertyInfo>>();

    public static T MapTo<T>(this IRecord record
    , string label = ""
    ) 
        where T: class, new()
    {
        var type = typeof(T);
        label = label.IsNullOrEmpty() ? type.Name.ToLowerInvariant() : label;
        var node = record[label].As<INode>();

        ICollection<PropertyInfo> properties = _propertyCache
                .TryGetProperties<T>(true);

        if (properties.Count == 0)
        {
            return new T();
        }

        var obj = new T();

        foreach (var prop in properties ?? Enumerable.Empty<PropertyInfo>())
        {
            string name = prop.Name/*.Dump("key")*/;
            // var value = node.Properties[name].Dump("value");
            node.Properties.TryGetValue(name, out var value);

            var next_value = CreateSafeValue(value, prop);

            prop.SetValue(obj, next_value/*.Dump("value")*/, null);
        }

        obj.Dump("T's obj");

        return obj;
    }

    private static object CreateSafeValue(object value, PropertyInfo prop){

        Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

        object safeValue =
            value == null
                ? null
                : Convert.ChangeType(value, propType);

        return safeValue;
    } 

}

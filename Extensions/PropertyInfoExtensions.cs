// using System.Collections;
// using System.Reflection;
// using System.Text;
// using CodeMechanic.Types;
//
// namespace CodeMechanic.Reflection;
//
// public static class PropertyInfoExtensions
// {
//     // public static Dictionary<string, string> ToPropertyValueDictionary<T>(
//     //     this PropertyInfo[] properties,
//     //     T item
//     // )
//     // {
//     //     var props_array = properties.ToArray();
//     //
//     //     //Add all the values as new key value pairs:
//     //     Dictionary<string, string> lookup = new Dictionary<string, string>();
//     //
//     //     foreach (var prop in props_array)
//     //     {
//     //         string key = prop.Name;
//     //         var obj_value = prop.GetValue(item);
//     //
//     //         string text_value = obj_value
//     //             .IsList() // TODO: Update this to include Dictionary (below)
//     //             ? new StringBuilder()
//     //                 .AppendEach(
//     //                     (obj_value as List<object>) ?? new List<object>(), (o) => o.ToString())
//     //                 .ToString()
//     //             : obj_value.ToString();
//     //
//     //         lookup.TryAdd(key, text_value);
//     //     }
//     //
//     //     return lookup;
//     // }
//
//     public static bool IsList(this object o)
//     {
//         if (o == null) return false;
//         return o is IList &&
//                o.GetType().IsGenericType &&
//                o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
//     }
//
//     public static bool IsDictionary(object o)
//     {
//         if (o == null) return false;
//         return o is IDictionary &&
//                o.GetType().IsGenericType &&
//                o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
//     }
// }
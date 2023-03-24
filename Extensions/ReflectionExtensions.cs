using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CodeMechanic.Extensions
{
    public static class ReflectionExtensions
    {
        public static ICollection<PropertyInfo> TryGetProperties<T>(
         this IDictionary<Type, ICollection<PropertyInfo>> property_cache
            , bool ignore_case = true
            , BindingFlags flags = BindingFlags.Default
            , params string[] exclusions
        )
        {
            ICollection<PropertyInfo> properties;
            var objType = typeof(T);

            lock (property_cache)
            {
                if (!property_cache.TryGetValue(objType, out properties))
                {
                    $"Prop not found for {objType.Name} so running reflection".Dump();

                    var type_props = objType.GetProperties();

                    var lowercased_prop_names = type_props.Select(p => p.Name.ToLowerInvariant());

                    var joined_names = lowercased_prop_names.Except(exclusions);

                    properties = type_props
                        .Where(
                            property => property.CanWrite
                            && joined_names.Contains(property.Name.ToLowerInvariant())
                        )
                        .ToList();

                    property_cache.Add(objType, properties);

                    property_cache.Count.Dump("propcache size");
                }
            }

            return properties;
        }
    }
}

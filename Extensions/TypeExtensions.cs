using Newtonsoft.Json;

namespace nugsnet6.Extensions;

public static class TypeExtensions
{
    public static string AsJS<T>(this T value) =>
        value != null ? JsonConvert.SerializeObject(value) : "";
}

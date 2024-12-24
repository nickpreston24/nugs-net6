using System.Reflection;

namespace CodeMechanic.RazorHAT.Services;

public interface IPropertyCache
{
    PropertyInfo[] GetProperties<T>(params PropertyInfo[] props);
    string[] GetPropertyNames<T>();
}

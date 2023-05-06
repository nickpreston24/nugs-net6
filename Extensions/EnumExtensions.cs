using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CodeMechanic.Advanced.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string input, T fallback = default)
             where T : struct
        {
            if (string.IsNullOrWhiteSpace(input))
                return fallback;

            Enum.TryParse(input, ignoreCase: true, out T result);
            return result;
        }

        public static IEnumerable<TEnum> GetValues<TEnum>()
            where TEnum : struct, IConvertible, IFormattable, IComparable => Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

        //public static IEnumerable<TEnum> GetNames<TEnum>()
        //    where TEnum : struct, IConvertible, IFormattable, IComparable => Enum.GetNames(typeof(TEnum)).Cast<TEnum>();

        public static TValue GetValueOrDefault<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary,
                TKey key,
                TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value)
                ? value
                : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>
            (this IDictionary<TKey, TValue> dictionary,
             TKey key,
             Func<TValue> defaultValueProvider)
        {
            return dictionary.TryGetValue(key, out TValue value)
                ? value
                : defaultValueProvider();
        }

        public static Dictionary<int, string> ToDictionary(this Enum @enum)
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().ToDictionary(value => value, value => Enum.GetName(type, value));
        }

        public static string GetDescription(this Enum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                return Convert.ToString((attributes.Length > 0) ? attributes[0].Description : value.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static TEnum Next<TEnum>(this TEnum source)
            where TEnum : struct, IConvertible, IFormattable, IComparable
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(TEnum).FullName));
            }

            var values = (TEnum[])Enum.GetValues(source.GetType());

            int j = Array.IndexOf(values, source) + 1;

            return (values.Length == j) ? values[0] : values[j];
        }

        public static TEnum Previous<TEnum>(this TEnum source)
            where TEnum : struct, IConvertible, IFormattable, IComparable
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(TEnum).FullName));
            }

            var values = (TEnum[])Enum.GetValues(source.GetType());

            int j = Array.IndexOf(values, source) - 1;

            return (values.Length == j) ? values[0] : values[j];
        }

        // Intended for enums to create conditions that are comparable to instances with given enum as a property
        public static string ToExpressionEnumCondition<TEnum>(this TEnum enumType)
            where TEnum : struct, IConvertible, IFormattable, IComparable
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new NotSupportedException($"Type {typeof(TEnum).Name} is not a valid Enum.");
            }

            var comparableEnumCondition = new StringBuilder();

            comparableEnumCondition.Append("\"");
            comparableEnumCondition.Append(enumType.ToString());
            comparableEnumCondition.Append("\"");

            return comparableEnumCondition.ToString();
        }

        public static TEnum GetRandom<TEnum>()
            where TEnum : struct, IConvertible, IFormattable, IComparable => GetValues<TEnum>().OrderBy(e => Guid.NewGuid()).FirstOrDefault();

        public static TEnum GetRandom<TEnum>(this TEnum @enum)
            where TEnum : struct, IConvertible, IFormattable, IComparable => GetRandom<TEnum>();

    }


    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; } = string.Empty;

        public int Id { get; private set; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        // Other utility methods ...
    }
}

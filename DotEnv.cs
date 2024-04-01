using System.Collections.Concurrent;
using System.Reflection;
using System.Text.RegularExpressions;
using CodeMechanic.Advanced.Regex;
using CodeMechanic.Diagnostics;
using CodeMechanic.Types;

public static class DotEnv
{
    public static Env Load(string file_path = ".env")
    {
        if (!File.Exists(file_path))
            return new Env();

        string raw_text = File.ReadAllText(file_path);

        var regex_options =
            RegexOptions.Compiled
            | RegexOptions.IgnoreCase
            | RegexOptions.ExplicitCapture
            | RegexOptions.Multiline
            | RegexOptions.IgnorePatternWhitespace;

        var raw_settings = raw_text.Extract<DotEnvSetting>(
            DotEnvSetting.settings_pattern
            // options: regex_options
        );

        // raw_settings.Dump();
        Console.WriteLine("# of settings loaded :>> " + raw_settings.Count);

        foreach (var setting in raw_settings)
        {
            Environment.SetEnvironmentVariable(setting.Left, setting.Right);
        }

        return new Env() { Settings = raw_settings.ToArray() };
    }
}

public record Env
{
    public DotEnvSetting[] Settings { get; init; } = new DotEnvSetting[] { };


    public string Get(string key)
    {
        var found_setting = Settings.FirstOrDefault(s => s.Left.Equals(key)); // NOT ignoring case, on purpose here..
        if (found_setting == null)
            throw new KeyNotFoundException($"Could not find setting '{key}' in .env settings!");
        return found_setting.Right;
    }

    public bool IsTrue(string key)
    {
        var found_setting = Settings.FirstOrDefault(s => s.Left.Equals(key)); // NOT ignoring case, on purpose here..
        if (found_setting == null)
            throw new KeyNotFoundException($"Could not find setting '{key}' in .env settings!");

        return found_setting.Right.ToBoolean();
    }

    public bool Exists(string key)
    {
        var found_setting = Settings.FirstOrDefault(s => s.Left.Equals(key)); // NOT ignoring case, on purpose here..
        return (found_setting != null);
    }
}

public record DotEnvSetting
{
    public string Left { get; set; } = string.Empty;
    public string Right { get; set; } = string.Empty;

    // https://regex101.com/r/pCJtks/1
    public const string regex_pattern = $"""
         (?<!\#)(?<Left>\w+) # Match alphas and underscore
         =                  # Match only the first equals sign
         (?<Right>.*) 
    """ ;

    public static Regex settings_pattern = new Regex(regex_pattern,
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
}

public static class DotEnvSettingExtensions
{
    public static void Deconstruct<T>(
        this T? setting,
        out string left,
        out string right) where T : DotEnvSetting
    {
        left = setting.Left;
        right = setting.Right;
    }
}


/// Factory    
public static class Singleton<T> where T : class, ISingleton
{
    static volatile T instance;
    static readonly object @lock = new object();
    const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.NonPublic;

    static Singleton()
    {
    }

    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            lock (@lock)
            {
                if (instance == null)
                {
                    ConstructorInfo constructor = null;

                    try
                    {
                        // Binding flags exclude public constructors.
                        constructor = typeof(T).GetConstructor(FLAGS, null, new Type[0], null);
                    }
                    catch (Exception exception)
                    {
                        throw new SingletonException(exception);
                    }

                    if (constructor == null || constructor.IsAssembly)
                    {
                        // Also exclude internal constructors.
                        throw new SingletonException(string.Format("A private or " +
                                                                   "protected empty parameterless constructor is missing for '{0}'.",
                            typeof(T).Name));
                    }

                    instance = (T)constructor.Invoke(null);
                    // instance.Selector = instance.Selector ?? new Selector();
                }
            }

            return instance;
        }
    }
}

public class SingletonException : Exception
{
    public SingletonException(string exception)
    {
        throw new Exception(exception);
    }

    public SingletonException(Exception exception)
    {
        throw exception;
    }
}

public interface ISingleton
{
    public ISingleton GetInstance<T>() where T : class, ISingleton => Multiton.GetInstance<T>();

    // ISelector Selector { get; set; }  //Todo: Replace with Selector's implementation when C# 8.0 comes out.
}

public class Multiton
{
    private static readonly ConcurrentDictionary<Type, ISingleton> instances =
        new ConcurrentDictionary<Type, ISingleton>();

    private Multiton()
    {
    }

    public static T GetInstance<T>() where T : class, ISingleton
    {
        lock (instances)
        {
            var key = typeof(T);

            if (!instances.TryGetValue(key, out var instance))
            {
                instance = Singleton<T>.Instance as ISingleton;
                instances.TryAdd(key, instance);
            }

            return instance as T;
        }
    }
}

//
// ///Implements
// internal class SelectorImplementation : ISelector
// {
//     public virtual ISingleton GetInstance<T>() where T : class, ISingleton
//     {
//         return Multiton.GetInstance<T>();
//     }
// }
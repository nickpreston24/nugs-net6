namespace CodeMechanic;

/// <summary>
/// Adapted from:
/// http://joelabrahamsson.com/a-neat-little-type-inference-trick-with-c/
/// <usage>
///         var wrappedInt = Wrapper.Create(42);
/// </usage>
/// </summary>
/// <typeparam name="T"></typeparam>
public class Wrapper<T>
{
    public Wrapper(T wrapped) => Wrapped = wrapped;
    public static Wrapper<T> Create<T>(T wrapped) => new Wrapper<T>(wrapped);

    public T Wrapped { get; set; }
}

public static class Wrapper
{
    public static Wrapper<T> Create<T>(T wrapped) => new Wrapper<T>(wrapped);
}

// Extensions
public static class WrapperExtensions
{
    public static Wrapper<T> Create<T>(T wrapped) => new Wrapper<T>(wrapped);
    public static Wrapper<T> Wrap<T>(this T wrapped) => Create(wrapped);
}
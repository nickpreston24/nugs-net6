using CodeMechanic.Types;

namespace nugsnet6.Services;

public class PartAccessoryType : Enumeration
{
    public static PartAccessoryType Scope = new PartAccessoryType(1, nameof(Scope));
    public static PartAccessoryType RedDot = new PartAccessoryType(2, "Red Dot");
    public static PartAccessoryType HoloDot = new PartAccessoryType(3, "Holo Dot");
    public static PartAccessoryType Riser = new PartAccessoryType(4, nameof(Riser));
    public static PartAccessoryType Bipod = new PartAccessoryType(5, nameof(Bipod));
    public static PartAccessoryType Cerakote = new PartAccessoryType(6, nameof(Cerakote));
    private readonly string[] aliases;

    public static implicit operator PartAccessoryType(string name)
    {
        if (name.IsEmpty())
            throw new ArgumentNullException(nameof(name));
        var matching = PartAccessoryType
            .GetAll<PartAccessoryType>()
            .Single(upt =>
                upt.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                || upt.aliases.Contains(name, StringComparer.OrdinalIgnoreCase)
            );

        return matching;
    }

    public PartAccessoryType(int id, string name, string[] aliases = null)
        : base(id, name)
    {
        this.aliases = aliases ?? Enumerable.Empty<string>().ToArray();
    }
}

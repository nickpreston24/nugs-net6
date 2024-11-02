using CodeMechanic.Types;

namespace nugsnet6.Services;

public class LowerPartType : Enumeration
{
    public static LowerPartType Lower = new LowerPartType(1, nameof(Lower), aliases: new[] { "Lower Receiver" });
    public static LowerPartType LPK = new LowerPartType(2, nameof(LPK), aliases: new[] { "Lower Parts Kit" });
    public static LowerPartType Grip = new LowerPartType(3, nameof(Grip));
    public static LowerPartType Trigger = new LowerPartType(4, nameof(Trigger));

    public static LowerPartType Stock =
        new LowerPartType(5, nameof(Stock), aliases: new[] { "Buttstock", "Butt Stock" });

    public static LowerPartType Buffer = new LowerPartType(6, nameof(Buffer));
    public static LowerPartType BufferSpring = new LowerPartType(7, "Buffer Spring");
    public static LowerPartType CastleNut = new LowerPartType(8, "Castle Nut");
    public static LowerPartType TakedownPin = new LowerPartType(9, "Takedown Pin");
    public static LowerPartType PivotPin = new LowerPartType(10, "Pivot Pin");
    public static LowerPartType HammerPin = new LowerPartType(11, "Hammer Pin");
    public static LowerPartType BufferTube = new LowerPartType(12, "Buffer Tube");

    public readonly string[] aliases;

    public static implicit operator LowerPartType(string name)
    {
        if (name.IsEmpty()) throw new ArgumentNullException(nameof(name));
        var matching = LowerPartType.GetAll<LowerPartType>()
            .Single(lpt =>
                lpt.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                || lpt.aliases.Contains(name, StringComparer.OrdinalIgnoreCase)
            );

        return matching;
    }

    public LowerPartType(int id, string name, string[] aliases = null) : base(id, name)
    {
        this.aliases = aliases ?? Enumerable.Empty<string>().ToArray();
    }
}
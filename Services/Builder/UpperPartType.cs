using CodeMechanic.Types;

namespace nugsnet6.Services;

public class UpperPartType : Enumeration
{
    public static UpperPartType UpperReciever = new UpperPartType(1, "Upper Receiver");
    public static UpperPartType Upper = new UpperPartType(2, nameof(Upper));
    public static UpperPartType Barrel = new UpperPartType(3, nameof(Barrel));
    public static UpperPartType GasTube = new UpperPartType(4, "Gas Tube");
    public static UpperPartType BarrelNut = new UpperPartType(5, "Barrel Nut");
    public static UpperPartType Handguard = new UpperPartType(6, nameof(Handguard));
    public static UpperPartType Foregrip = new UpperPartType(7, nameof(Foregrip));
    public static UpperPartType BCG = new UpperPartType(8, nameof(BCG), aliases: new[] { "Bolt Carrier Group" });
    public static UpperPartType ChargingHandle = new UpperPartType(9, "Charging Handle", aliases: new[] { "Charger" });
    public static UpperPartType Rail = new UpperPartType(10, nameof(Rail));

    public readonly string[] aliases;

    public override bool Equals(object obj)
    {
        var type = obj.GetType();

        if (type == typeof(string))
        {
            return aliases.Contains((string)obj, StringComparer.OrdinalIgnoreCase);
        }

        if (type == typeof(Enumeration))
        {
            var upt = (UpperPartType)obj;
            return this.Name.Equals(upt?.Name);
        }

        return false;
    }

    public UpperPartType(int id, string name, string[] aliases = null) : base(id, name)
    {
        this.aliases = aliases ?? Enumerable.Empty<string>().ToArray();
    }
}
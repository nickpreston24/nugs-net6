using CodeMechanic.Types;

namespace nugsnet6.Services;

public abstract class Recipe
{
    public string Name { get; set; } = "RECCE Riflezzz";

    protected List<Enumeration> Requirements = new List<Enumeration>(0);
    protected abstract Recipe Init();
}

public class RecceRifle : Recipe
{
    protected override Recipe Init()
    {
        var upper_parts = new List<UpperPartType>()
        {
            UpperPartType.Barrel,
            UpperPartType.GasTube,
            UpperPartType.BarrelNut,
            UpperPartType.BCG,
            UpperPartType.UpperReciever,
            UpperPartType.Rail,
            UpperPartType.Foregrip,
            UpperPartType.Handguard,
        };

        var lower_parts = new List<LowerPartType>()
        {
            LowerPartType.Lower,
            LowerPartType.BufferTube,
            LowerPartType.BufferSpring,
            LowerPartType.Buffer,
            LowerPartType.Stock,
            LowerPartType.LPK,
            LowerPartType.Grip,
        };

        Requirements.AddRange(upper_parts.DistinctBy(x => x.Name));
        Requirements.AddRange(lower_parts.DistinctBy(x => x.Name));
        return this;
    }
}
using System.Linq.Expressions;
using NSpecifications;

public class SandboxStats
{
    public string Title { get; set; } = string.Empty;
    public double TotalValid { get; set; }
    public double TotalFound { get; set; }
    public double PercentValid { get; set; }
    public double PercentValidSpec { get; set; }
    public List<object> ValidCollection { get; set; } = new List<object>();
}

public class StatsCalculator<T>
{
    public List<T> ValidMatches { get; set; }
    public List<T> InvalidMatches { get; set; }
    private List<T> Collection { get; }

    public StatsCalculator(List<T> collection) => Collection = collection;

    public ASpec<T> PercentValidSpec { get; set; } = new Spec<T>(x => x != null);

    public StatsCalculator<T> AddValidation(Expression<Func<T, bool>> expression)
    {
        var next_validation = new Spec<T>(expression);
        PercentValidSpec = next_validation & PercentValidSpec;
        return this;
    }

    public StatsCalculator<T> AddValidation(Spec<T> next)
    {
        PercentValidSpec = next & PercentValidSpec;
        return this;
    }

    public SandboxStats ComputeStats()
    {
        var stats = new SandboxStats();
        ValidMatches = Collection.Where(PercentValidSpec).ToList();
        InvalidMatches = Collection.Where(!PercentValidSpec).ToList();

        stats.TotalValid = ValidMatches.Count;
        stats.TotalFound = Collection.Count;

        stats.PercentValid = stats.TotalValid / (Collection.Count > 0 ? Collection.Count : 1) * 100;

        return stats;
    }
}

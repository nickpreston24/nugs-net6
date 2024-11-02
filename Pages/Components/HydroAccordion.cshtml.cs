using Hydro;

namespace nugsnet6.Pages.Components;

public class HydroAccordion : HydroComponent
{
    public HydroCollapseProps[]
        Children { get; set; }
        = Enumerable.Empty<HydroCollapseProps>().ToArray();
    // = Enumerable.Repeat<HydroCollapse>(new HydroCollapse(), 1).ToArray();

    public override async Task MountAsync()
    {
        Console.WriteLine(nameof(MountAsync));
        // Children.Dump("children");
        foreach (var child in Children.Select(props => new HydroCollapse { Props = props }))
        {
            await child.RenderAsync();
        }
    }

    public void FocusRandom()
    {
        // Children.TakeFirstRandom().SetFocus(true);
        // var key = first.Key;
    }
}

public static class Dotnet9Extensions
{
    public static IEnumerable<(T, int i)> WithIndex<T>(this IEnumerable<T> collection)
    {
        var arr = collection.ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            yield return (arr[i], i);
        }
    }

    // Gives previously unidentified collection a set of unique identifiers.
    public static List<(T, Guid)> AsUniqueCollection<T>(this IEnumerable<T> collection)
    {
        List<(T, Guid)> result = collection
                .Select(obj =>
                    (obj, Guid.NewGuid())
                )
                .ToList()
            ;
        return result;
    }
}
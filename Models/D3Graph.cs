using Neo4j.Driver;

namespace nugsnet6;

public class D3Graph
{
    public IEnumerable<D3Node> Nodes { get; }
    public IEnumerable<D3Link> Links { get; }

    public D3Graph(IEnumerable<D3Node> nodes, IEnumerable<D3Link> links)
    {
        Nodes = nodes;
        Links = links;
    }
}

public class D3Node
{
    public string Title { get; } = string.Empty;
    public string Label { get; } = string.Empty;

    public D3Node(string title, string label)
    {
        Title = title;
        Label = label;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((D3Node) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Label);
    }

    protected bool Equals(D3Node other)
    {
        return Title == other.Title && Label == other.Label;
    }
}

public class D3Link
{
    public int Source { get; }
    public int Target { get; }

    public D3Link(int source, int target)
    {
        Source = source;
        Target = target;
    }
}


public static class D3GraphExtensions {

    public static D3Graph ToD3Graph<A,B>(this IEnumerable<IRecord> records) {

        var nodes = new List<D3Node>();
        var links = new List<D3Link>();
        // var records = await cursor.ToListAsync();

        var typeA = typeof(A);
        var typeB = typeof(B);
        var first_label = typeA.Name;
        var second_label = typeA.Name;

        foreach (var record in records)
        {
            var movie = new D3Node(record["title"].As<string>(), first_label);

            var movieIndex = nodes.Count;

            nodes.Add(movie);

            foreach (var actorName in record["cast"].As<IList<string>>())
            {
                var actor = new D3Node(actorName, "actor");
                var actorIndex = nodes.IndexOf(actor);
                actorIndex = actorIndex == -1 ? nodes.Count : actorIndex;
                nodes.Add(actor);
                links.Add(new D3Link(actorIndex, movieIndex));
            }
        }

        return new D3Graph(nodes, links);
    }
}
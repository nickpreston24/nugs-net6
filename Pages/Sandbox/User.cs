using Insight.Database;

namespace nugsnet6.Models;

public class User
{
    public long Id { get; set; }

    [Column(SerializationMode = SerializationMode.Json)]
    public TestData JsonData { get; set; }

    public string Ebay { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string UUID { get; set; } = string.Empty;
}
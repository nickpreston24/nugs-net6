namespace nugs_seeder.Controllers;

public record UploadRequestNeo4j<T> //where T : new()
{
    public string label_name { get; set; } = string.Empty;
    public List<T> Payload { get; set; } = new List<T>();
}
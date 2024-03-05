namespace nugs_seeder.Controllers;

public record ImportRequest
{
    public string import_file_path { get; set; }
    public string neo4j_user { get; set; } = "neo4j";
    public string neo4j_password { get; set; } = "neo4j";
}
namespace nugsnet6;

public class BuildStep {

    public static BuildStep Start => new BuildStep();

    public string Label { get; set; } = "Step";
    public string Description { get; set; } = string.Empty;
}
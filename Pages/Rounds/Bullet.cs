public class Bullet 
{

    public string Name { get; set; } = string.Empty;

    public string SectionalDensity { get; set; }

    public string Weight { get; set; } = string.Empty;

    public string Diameter { get; set; } = string.Empty;

    public string JacketMaterial { get; set; } = string.Empty;

    public string BallisticCoefficient { get; set; } = string.Empty;

    public string BallisticCoefficientType { get; set; } = string.Empty; // G1 or G7
    
    public string EstimatedPower { get; set; } = string.Empty; // kgr * fps (unit)

    public string SuggestedUse { get; set; } = string.Empty;

    public string TestBarrelLength { get; set; } = string.Empty; // Usually 20" for 5.56

    public string Features { get; set; } = string.Empty;

} 
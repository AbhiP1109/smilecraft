namespace SmileCraft.API.Models;

/// <summary>
/// Represents a doctor at the clinic.
/// </summary>
public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public int Experience { get; set; }
    public string Image { get; set; } = string.Empty;
    public double Rating { get; set; }
}

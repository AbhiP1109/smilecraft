namespace SmileCraft.API.Models;

/// <summary>
/// Represents a dental service offered by the clinic.
/// </summary>
public class Service
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

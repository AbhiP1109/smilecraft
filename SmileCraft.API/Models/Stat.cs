namespace SmileCraft.API.Models;

/// <summary>
/// Represents a clinic statistic/metric.
/// </summary>
public class Stat
{
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

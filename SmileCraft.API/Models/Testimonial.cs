namespace SmileCraft.API.Models;

/// <summary>
/// Represents a patient testimonial/review.
/// </summary>
public class Testimonial
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Review { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Image { get; set; } = string.Empty;
}

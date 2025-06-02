namespace Wanderer.Application.Dtos.Post.Response;

public class PostImageDto
{
    public string ImageUrl { get; set; } = string.Empty;
    
    public string? CityPlaceId { get; set; }

    public string? WaypointPlaceId { get; set; }

    public string? CityName { get; set; }

    public string? WaypointName { get; set; }
    
    public DateTime CreatedAt { get; set; }
}

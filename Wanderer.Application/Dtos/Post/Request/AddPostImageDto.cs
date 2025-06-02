namespace Wanderer.Application.Dtos.Post.Request;

public class AddPostImageDto
{
    public required string ImageUrl { get; set; }
    
    public string? CityPlaceId { get; set; }

    public string? WaypointPlaceId { get; set; }
}

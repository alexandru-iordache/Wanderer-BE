namespace Wanderer.Application.Dtos.User.Common;

public class UserFeatureVectorDto
{
    public Guid UserId { get; set; }
    
    public Dictionary<Guid, int> FeatureVector { get; set; } = new Dictionary<Guid, int>();
}

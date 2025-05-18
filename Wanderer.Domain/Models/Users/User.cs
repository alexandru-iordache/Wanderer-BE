using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Users;

public class User : BaseEntity
{
    public string ProfileName { get; set; }

    public string FirebaseId { get; set; }

    public string Email { get; set; }

    [Column("PROFILE_DESCRIPTION")]
    [MaxLength(200)]
    public string? ProfileDescription { get; set; }

    public Guid? HomeCityId { get; set; }

    public City? HomeCity { get; set; }

    public ICollection<Trip> Trips { get; set; }

    [Column("AVATAR_URL")]
    public string? AvatarUrl { get; set; }
}

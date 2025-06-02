using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Posts;
using Wanderer.Domain.Models.Trips;
using Wanderer.Domain.Models.Trips.Visits;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Application.Repositories.Constants;

public static class IncludeConstants
{
    public static class TripConstants
    {
        public const string IncludeAll = $"{nameof(Trip.CityVisits)},{nameof(Trip.CityVisits)}.{nameof(CityVisit.City)}," +
                                         $"{nameof(Trip.CityVisits)},{nameof(Trip.CityVisits)}.{nameof(CityVisit.City)}.{nameof(City.Country)}," +
                                         $"{nameof(Trip.CityVisits)}.{nameof(CityVisit.Days)}.{nameof(DayVisit.WaypointVisits)}.{nameof(WaypointVisit.Waypoint)}";
    }

    public static class UserConstants
    {
        public const string IncludeAll = $"{nameof(User.HomeCity)},{nameof(User.HomeCity)}.{nameof(City.Country)}," +
                                         $"{nameof(User.Followers)},{nameof(User.Following)}";
    }

    public static class PostConstants
    {
        public const string IncludeAll = $"{nameof(Post.Images)}.{nameof(PostImage.City)},{nameof(Post.Images)}.{nameof(PostImage.Waypoint)},{nameof(Post.Owner)}," +
                                         $"{nameof(Post.Comments)},{nameof(Post.Likes)}.{nameof(PostLike.User)}";
    }
}

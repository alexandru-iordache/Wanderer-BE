using AutoMapper;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;
using UserModel = Wanderer.Domain.Models.Users.User;

namespace Wanderer.Infrastructure.Profiles.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserDto, UserModel>()
            .ForMember(dest => dest.FirebaseId, opt => opt.MapFrom((src, dest, destMember, ctx) =>
            {
                var firebaseId = ctx.Items[nameof(UserModel.FirebaseId)];
                return firebaseId != null ? (string)firebaseId : throw new InvalidOperationException("FirebaseId is not provided in the mapping context.");
            }));

        CreateMap<UserModel, UserDto>();
    }
}

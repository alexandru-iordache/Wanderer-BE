using AutoMapper;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;
using UserModel = Wanderer.Domain.Models.Users.User;

namespace Wanderer.Infrastructure.Profiles.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserDto, UserModel>();

        CreateMap<UserModel, UserDto>();
    }
}

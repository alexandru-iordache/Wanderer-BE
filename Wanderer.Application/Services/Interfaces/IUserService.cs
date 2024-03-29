﻿using System.Linq.Expressions;
using Wanderer.Application.Dtos.User;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> Get();

    Task<UserDto> InsertUser(UserInsertDto userInsertDto);
}
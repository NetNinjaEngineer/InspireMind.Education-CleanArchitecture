﻿using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Users.DTOs;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;

namespace InspireMind.Education.Application.Contracts.Identity;

public interface IUser
{
    string? Id { get; }
    Task<Pagination<UserListDto>> GetPaginatedUsersAsync(UserRequestParameters userParams);
    Task<Result<UserListDto>> GetUserAsync(Guid userId);
    Task<Result<string>> UpdateUserAsync(Guid userId, UserForUpdateDto newUser);
    Task<Result<string>> DeleteUserAsync(Guid userId);
    Task<Result<IEnumerable<string>>> GetCurrentUserRoles();
    Task<Result<IEnumerable<string>>> GetCurrentUserClaims();
}
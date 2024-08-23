using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.DTOs.User;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;

namespace InspireMind.Education.Application.Contracts.Identity;

public interface IUser
{
    string? Id { get; }
    Task<Pagination<UserListDto>> GetPaginatedUsersAsync(UserRequestParameters userParams);
    Task<Result<UserListDto>> GetUserAsync(Guid userId);
    Task<Result<string>> UpdateUserAsync(Guid userId, UserForUpdateDto newUser);
}
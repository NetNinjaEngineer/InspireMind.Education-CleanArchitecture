using AutoMapper;
using CleanArchitecture.Application.Contracts.Identity;
using FluentValidation;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Users.DTOs;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using InspireMind.Education.Identity.Entities;
using InspireMind.Education.Identity.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace InspireMind.Education.Identity.Services;
public class UserService(IHttpContextAccessor context,
                   UserManager<AppUser> userManager,
                   IMapper mapper,
                   IStringLocalizer<BaseResponseHandler> localizer,
                   IRoleService roleService) : BaseResponseHandler(localizer), IUser
{
    public string? Id => context.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);

    public async Task<Result<string>> DeleteUserAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return NotFound<string>(_localizer["UnknownUser"]);

        var result = await userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest<string>(_localizer["DeleteFailed"], errors);
        }

        return Success(string.Empty);
    }

    public async Task<Result<IEnumerable<string>>> GetCurrentUserClaims()
        => await roleService.GetUserClaims(Id!);

    public async Task<Result<IEnumerable<string>>> GetCurrentUserRoles()
        => await roleService.GetUserRoles(Id!);

    public async Task<Pagination<UserListDto>> GetPaginatedUsersAsync(UserRequestParameters userParams)
    {
        var users = userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(userParams.SearchTerm))
        {
            users = userManager.Users.Where(u => u.FirstName!.Contains(userParams.SearchTerm)
                                                  || u.LastName!.Contains(userParams.SearchTerm)
                                                  || u.UserName!.Contains(userParams.SearchTerm)
                                                  || u.Email!.Contains(userParams.SearchTerm));
        }

        var count = await users.CountAsync(); // count filteration

        var paginatedResult = await users
            .Skip((userParams.PageNumber - 1) * userParams.PageSize)
            .Take(userParams.PageSize)
            .AsNoTracking()
            .ToListAsync(); // immediate execution

        var mappedResult = mapper.Map<List<UserListDto>>(paginatedResult);

        return Pagination<UserListDto>.ToPaginatedResult(userParams.PageNumber,
                                                         userParams.PageSize,
                                                         count,
                                                         mappedResult);
    }

    public async Task<Result<UserListDto>> GetUserAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        return user == null ?
            NotFound<UserListDto>(_localizer["UnknownUser"]) :
            Success(mapper.Map<UserListDto>(user));
    }

    public async Task<Result<string>> UpdateUserAsync(Guid userId, UserForUpdateDto newUser)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return NotFound<string>(_localizer["UnknownUser"]);

        mapper.Map(newUser, user);

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest<string>(_localizer["UpdateFailed"], errors);
        }

        return Success(string.Empty);
    }
}

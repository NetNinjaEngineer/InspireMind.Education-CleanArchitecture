using CleanArchitecture.Application.Contracts.Identity;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Roles.Requests.Commands;
using InspireMind.Education.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace InspireMind.Education.Identity.Services;
public class RoleService : BaseResponseHandler, IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public RoleService(
        IStringLocalizer<BaseResponseHandler> localizer,
        RoleManager<IdentityRole> roleManager,
        UserManager<AppUser> userManager) : base(localizer)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    //public Task<Result<string>> AddClaimToRole(AddClaimToRoleCommand request)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<Result<string>> AddClaimToUser(AssignClaimToUserCommand request)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Result<string>> AddRoleToUser(AssignRoleToUserCommand request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
            return BadRequest<string>(_localizer["UserNotExists", request.UserId]);

        var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);

        if (!roleExists)
            return BadRequest<string>(_localizer["RoleNotExists", request.RoleName]);

        var result = await _userManager.AddToRoleAsync(user, request.RoleName);

        return result.Succeeded ?
            Success<string>(_localizer["RoleAssignedSuccessfully", request.RoleName]) :
            BadRequest<string>(_localizer["FaildToAssignRole", request.RoleName]);

    }

    public async Task<Result<string>> CreateRole(CreateRoleCommand request)
    {
        var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
        if (!roleExists)
        {
            var identityRole = new IdentityRole(request.RoleName);
            await _roleManager.CreateAsync(identityRole);
            return Created<string>(_localizer["RoleCreatedSuccessfully", request.RoleName]);
        }

        return BadRequest<string>(_localizer["RoleExisted", request.RoleName]);
    }

    public async Task<Result<string>> DeleteRole(DeleteRoleCommand request)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);

        if (role is not null)
        {
            var result = await _roleManager.DeleteAsync(role);

            return result.Succeeded ?
                Success<string>(_localizer["RoleDeleted", request.RoleId]) :
                BadRequest<string>(_localizer["FailedToDelete", request.RoleId]);
        }

        return BadRequest<string>(_localizer["RoleNotExists", request.RoleId]);

    }

    public async Task<Result<string>> EditRole(EditRoleCommand request)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        if (role is not null)
        {
            role.Name = request.RoleName;
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded ?
                Success<string>(_localizer["RoleUpdated", request.RoleId]) :
                BadRequest<string>(_localizer["FailedToUpdateRole", request.RoleId]);

        }

        return BadRequest<string>(_localizer["RoleNotExists", request.RoleId]);

    }

    public Task<Result<IEnumerable<string>>> GetAllClaims()
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<string>>> GetAllRoles()
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<string>>> GetRoleClaims(string roleName)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<string>>> GetUserClaims(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<string>>> GetUserRoles(string userId)
    {
        throw new NotImplementedException();
    }
}

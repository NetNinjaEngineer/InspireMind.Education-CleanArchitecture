using CleanArchitecture.Application.Contracts.Identity;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Roles.Requests.Commands;
using InspireMind.Education.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace InspireMind.Education.Identity.Services;
public class RoleService(IStringLocalizer<BaseResponseHandler> localizer,
                         RoleManager<IdentityRole> roleManager,
                         UserManager<AppUser> userManager) : BaseResponseHandler(localizer), IRoleService
{
    public async Task<Result<string>> AddRoleToUser(AssignRoleToUserCommand request)
    {
        var user = await userManager.FindByIdAsync(request.UserId);

        if (user is null)
            return BadRequest<string>(_localizer["UserNotExists", request.UserId]);

        var roleExists = await roleManager.RoleExistsAsync(request.RoleName);

        if (!roleExists)
            return BadRequest<string>(_localizer["RoleNotExists", request.RoleName]);

        var result = await userManager.AddToRoleAsync(user, request.RoleName);

        return result.Succeeded ?
            Success<string>(_localizer["RoleAssignedSuccessfully", request.RoleName]) :
            BadRequest<string>(_localizer["FaildToAssignRole", request.RoleName]);

    }

    public async Task<Result<string>> CreateRole(CreateRoleCommand request)
    {
        var roleExists = await roleManager.RoleExistsAsync(request.RoleName);
        if (!roleExists)
        {
            var identityRole = new IdentityRole(request.RoleName);
            await roleManager.CreateAsync(identityRole);
            return Created<string>(_localizer["RoleCreatedSuccessfully", request.RoleName]);
        }

        return BadRequest<string>(_localizer["RoleExisted", request.RoleName]);
    }

    public async Task<Result<string>> DeleteRole(DeleteRoleCommand request)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId);

        if (role is not null)
        {
            var result = await roleManager.DeleteAsync(role);

            return result.Succeeded ?
                Success<string>(_localizer["RoleDeleted", request.RoleId]) :
                BadRequest<string>(_localizer["FailedToDelete", request.RoleId]);
        }

        return BadRequest<string>(_localizer["RoleNotExists", request.RoleId]);

    }

    public async Task<Result<string>> EditRole(EditRoleCommand request)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId);
        if (role is not null)
        {
            role.Name = request.RoleName;
            var result = await roleManager.UpdateAsync(role);

            return result.Succeeded ?
                Success<string>(_localizer["RoleUpdated", request.RoleId]) :
                BadRequest<string>(_localizer["FailedToUpdateRole", request.RoleId]);

        }

        return BadRequest<string>(_localizer["RoleNotExists", request.RoleId]);

    }

    public async Task<Result<IEnumerable<string?>>> GetAllRoles()
    {
        return Success(roleManager.Roles.AsEnumerable().Select(x => x.Name));
    }


    public async Task<Result<string>> AddClaimToUser(AssignClaimToUserCommand request)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return BadRequest<string>(_localizer["UserNotFound", request.UserId]);
        }

        var result = await userManager.AddClaimAsync(user, new Claim(request.ClaimType, request.ClaimValue));

        return result.Succeeded
            ? Success<string>(_localizer["ClaimAddedSuccessfully", request.ClaimType, request.ClaimValue, user.UserName])
            : BadRequest<string>(_localizer["ErrorAddingClaim", user.UserName]);
    }

    public async Task<Result<IEnumerable<string>>> GetUserClaims(string userId)
    {
        var currentUser = await userManager.FindByIdAsync(userId);

        if (currentUser is null)
            return BadRequest<IEnumerable<string>>(_localizer["UnknownUser"]);

        var userClaims = await userManager.GetClaimsAsync(currentUser);

        return Success(userClaims.Select(c => $"{c.Type}:{c.Value}"));
    }

    public async Task<Result<IEnumerable<string>>> GetUserRoles(string userId)
    {
        var currentUser = await userManager.FindByIdAsync(userId);

        if (currentUser is null)
            return BadRequest<IEnumerable<string>>(_localizer["UnknownUser"]);

        var userRoles = await userManager.GetRolesAsync(currentUser);

        return Success(userRoles.AsEnumerable());
    }
}

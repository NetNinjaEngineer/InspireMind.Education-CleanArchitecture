using AutoMapper;
using FluentValidation;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.DTOs.User;
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
public class UserService : BaseResponseHandler, IUser
{
    private readonly IHttpContextAccessor _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IValidator<UserForUpdateDto> _userUpdateValidator;

    public UserService(IHttpContextAccessor context,
                       UserManager<AppUser> userManager,
                       IMapper mapper,
                       IStringLocalizer<BaseResponseHandler> localizer,
                       IValidator<UserForUpdateDto> userUpdateValidator) : base(localizer)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
        _userUpdateValidator = userUpdateValidator;
    }

    public string? Id => _context.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);

    public async Task<Result<string>> DeleteUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return NotFound<string>(_localizer["UnknownUser"]);

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest<string>(_localizer["DeleteFailed"], errors);
        }

        return Success(string.Empty);
    }

    public async Task<Pagination<UserListDto>> GetPaginatedUsersAsync(UserRequestParameters userParams)
    {
        var users = _userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(userParams.SearchTerm))
        {
            users = _userManager.Users.Where(u => u.FirstName!.Contains(userParams.SearchTerm)
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

        var mappedResult = _mapper.Map<List<UserListDto>>(paginatedResult);

        return Pagination<UserListDto>.ToPaginatedResult(userParams.PageNumber,
                                                         userParams.PageSize,
                                                         count,
                                                         mappedResult);
    }

    public async Task<Result<UserListDto>> GetUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        return user == null ?
            NotFound<UserListDto>(_localizer["UnknownUser"]) :
            Success(_mapper.Map<UserListDto>(user));
    }

    public async Task<Result<string>> UpdateUserAsync(Guid userId, UserForUpdateDto newUser)
    {
        var validationResult = _userUpdateValidator.Validate(newUser);
        if (!validationResult.IsValid)
            return UnprocessableEntity<string>(validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault()!);

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return NotFound<string>(_localizer["UnknownUser"]);

        _mapper.Map(newUser, user);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest<string>(_localizer["UpdateFailed"], errors);
        }

        return Success(string.Empty);
    }
}

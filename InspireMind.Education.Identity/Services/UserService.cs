using AutoMapper;
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

    public UserService(IHttpContextAccessor context,
                       UserManager<AppUser> userManager,
                       IMapper mapper,
                       IStringLocalizer<BaseResponseHandler> localizer) : base(localizer)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public string? Id => _context.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);

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
}

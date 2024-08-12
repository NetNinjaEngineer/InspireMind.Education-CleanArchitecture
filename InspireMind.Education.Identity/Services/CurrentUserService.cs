using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Identity.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace InspireMind.Education.Identity.Services;
public class CurrentUserService : IUser
{
    private readonly IHttpContextAccessor _context;

    public CurrentUserService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string? Id => _context.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
}

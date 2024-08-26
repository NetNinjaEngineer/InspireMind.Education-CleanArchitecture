using CleanArchitecture.Application.Contracts.Identity;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Roles.Requests.Queries;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Handlers.Queries;
public sealed class RolesQueryHandler(IRoleService roleService) :
    IRequestHandler<GetAllRolesQuery, Result<IEnumerable<string?>>>,
    IRequestHandler<GetUserRolesQuery, Result<IEnumerable<string>>>,
    IRequestHandler<GetUserClaimsQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string?>>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
        => await roleService.GetAllRoles();

    public async Task<Result<IEnumerable<string>>> Handle(
        GetUserRolesQuery request,
        CancellationToken cancellationToken)
        => await roleService.GetUserRoles(request.UserId.ToString());

    public async Task<Result<IEnumerable<string>>> Handle(
        GetUserClaimsQuery request,
        CancellationToken cancellationToken)
        => await roleService.GetUserClaims(request.UserId.ToString());
}

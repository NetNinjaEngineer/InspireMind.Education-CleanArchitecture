using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Users.DTOs;
using InspireMind.Education.Application.Features.Users.Requests.Queries;
using InspireMind.Education.Application.Wrappers;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Handlers.Queries;
public class UsersQueryHandler(IUser user) : IRequestHandler<GetPaginatedUsersQuery, Pagination<UserListDto>>,
                                             IRequestHandler<GetSingleUserQuery, Result<UserListDto>>,
                                             IRequestHandler<GetCurrentUserClaimsQuery, Result<IEnumerable<string>>>,
                                             IRequestHandler<GetCurrentUserRolesQuery, Result<IEnumerable<string>>>
{
    public async Task<Pagination<UserListDto>> Handle(GetPaginatedUsersQuery request,
                                                CancellationToken cancellationToken)
        => await user.GetPaginatedUsersAsync(request.UserParameters);

    public async Task<Result<UserListDto>> Handle(GetSingleUserQuery request,
                                               CancellationToken cancellationToken)
       => await user.GetUserAsync(request.UserId);

    public async Task<Result<IEnumerable<string>>> Handle(GetCurrentUserClaimsQuery request,
                                                          CancellationToken cancellationToken)
        => await user.GetCurrentUserClaims();

    public async Task<Result<IEnumerable<string>>> Handle(GetCurrentUserRolesQuery request,
                                                          CancellationToken cancellationToken)
        => await user.GetCurrentUserRoles();
}

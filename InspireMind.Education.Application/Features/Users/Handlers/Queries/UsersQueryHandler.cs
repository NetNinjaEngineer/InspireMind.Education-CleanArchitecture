using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.DTOs.User;
using InspireMind.Education.Application.Features.Users.Requests.Queries;
using InspireMind.Education.Application.Wrappers;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Handlers.Queries;
public class UsersQueryHandler(IUser user) : IRequestHandler<GetPaginatedUsersQuery, Pagination<UserListDto>>,
                                               IRequestHandler<GetSingleUserQuery, Result<UserListDto>>
{
    public async Task<Pagination<UserListDto>> Handle(GetPaginatedUsersQuery request,
                                                CancellationToken cancellationToken)
        => await user.GetPaginatedUsersAsync(request.UserParameters);

    public async Task<Result<UserListDto>> Handle(GetSingleUserQuery request,
                                               CancellationToken cancellationToken)
       => await user.GetUserAsync(request.UserId);

}

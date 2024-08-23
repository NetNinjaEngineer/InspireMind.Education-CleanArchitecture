using InspireMind.Education.Application.DTOs.User;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Requests.Queries;
public class GetPaginatedUsersQuery : IRequest<Pagination<UserListDto>>
{
    public UserRequestParameters UserParameters { get; set; } = null!;
}

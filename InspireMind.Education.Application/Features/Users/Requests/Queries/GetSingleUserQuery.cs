using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Users.DTOs;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Requests.Queries;
public class GetSingleUserQuery : IRequest<Result<UserListDto>>
{
    public Guid UserId { get; set; }
}

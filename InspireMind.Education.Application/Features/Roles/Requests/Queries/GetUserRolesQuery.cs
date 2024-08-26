using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Requests.Queries;
public class GetUserRolesQuery : IRequest<Result<IEnumerable<string>>>
{
    public Guid UserId { get; set; }
}

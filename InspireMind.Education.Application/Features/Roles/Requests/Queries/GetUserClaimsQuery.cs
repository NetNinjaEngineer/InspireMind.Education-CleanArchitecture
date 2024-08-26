using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Requests.Queries;

public class GetUserClaimsQuery : IRequest<Result<IEnumerable<string>>>
{
    public Guid UserId { get; set; }
}

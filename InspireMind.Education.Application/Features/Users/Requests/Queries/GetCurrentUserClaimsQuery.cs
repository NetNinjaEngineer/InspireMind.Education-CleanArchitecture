using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Requests.Queries;

public class GetCurrentUserClaimsQuery : IRequest<Result<IEnumerable<string>>>
{
}

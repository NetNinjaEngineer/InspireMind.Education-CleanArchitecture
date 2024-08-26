using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Requests.Queries;
public class GetCurrentUserRolesQuery : IRequest<Result<IEnumerable<string>>>
{
}

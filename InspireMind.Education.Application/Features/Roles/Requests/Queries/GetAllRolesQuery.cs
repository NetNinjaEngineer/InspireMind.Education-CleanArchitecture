using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Requests.Queries;
public class GetAllRolesQuery : IRequest<Result<IEnumerable<string?>>>
{
}
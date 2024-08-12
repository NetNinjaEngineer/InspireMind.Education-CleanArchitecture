using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Requests.Commands;
public class AssignRoleToUserCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}

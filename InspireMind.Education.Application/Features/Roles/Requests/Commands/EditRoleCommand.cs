using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Requests.Commands;
public class EditRoleCommand : IRequest<Result<string>>
{
    public string RoleId { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}

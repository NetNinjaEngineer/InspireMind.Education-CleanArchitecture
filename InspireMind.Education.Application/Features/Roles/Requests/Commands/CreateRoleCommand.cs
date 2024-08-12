using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Requests.Commands;
public class CreateRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = null!;
}

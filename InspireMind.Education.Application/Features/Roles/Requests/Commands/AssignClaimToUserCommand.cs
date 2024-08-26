using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Requests.Commands;

public class AssignClaimToUserCommand : IRequest<Result<string>>
{
    public Guid UserId { get; set; }
    public string ClaimType { get; set; } = null!;
    public string ClaimValue { get; set; } = null!;
}

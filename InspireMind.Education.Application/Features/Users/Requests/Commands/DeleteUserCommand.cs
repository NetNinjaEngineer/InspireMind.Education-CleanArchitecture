using InspireMind.Education.Application.Bases;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Requests.Commands;
public class DeleteUserCommand(Guid userId) : IRequest<Result<string>>
{
    public Guid UserId { get; } = userId;
}

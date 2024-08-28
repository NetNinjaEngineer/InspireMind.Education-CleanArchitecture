using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Users.DTOs;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Requests.Commands;
public class UpdateUserCommand(Guid userId, UserForUpdateDto updateModel) : IRequest<Result<string>>
{
    public Guid UserId { get; } = userId;
    public UserForUpdateDto User { get; } = updateModel;
}

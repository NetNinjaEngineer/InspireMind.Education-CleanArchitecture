using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.DTOs.User;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Requests.Commands;
public class UpdateUserCommand(Guid userId, UserForUpdateDto updateModel) : IRequest<Result<string>>
{
    public Guid UserId { get; } = userId;
    public UserForUpdateDto UpdateModel { get; } = updateModel;
}

using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Users.Requests.Commands;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Handlers.Commands;
public class UsersCommandHandler(IUser user) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request,
                                       CancellationToken cancellationToken)
        => await user.UpdateUserAsync(request.UserId, request.UpdateModel);
}

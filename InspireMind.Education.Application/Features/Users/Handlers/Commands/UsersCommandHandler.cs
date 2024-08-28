using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Identity;
using InspireMind.Education.Application.Features.Users.Requests.Commands;
using MediatR;

namespace InspireMind.Education.Application.Features.Users.Handlers.Commands;
public class UsersCommandHandler(IUser user) : IRequestHandler<UpdateUserCommand, Result<string>>,
    IRequestHandler<DeleteUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request,
                                       CancellationToken cancellationToken)
        => await user.UpdateUserAsync(request.UserId, request.User);

    public async Task<Result<string>> Handle(DeleteUserCommand request,
                                             CancellationToken cancellationToken)
        => await user.DeleteUserAsync(request.UserId);
}

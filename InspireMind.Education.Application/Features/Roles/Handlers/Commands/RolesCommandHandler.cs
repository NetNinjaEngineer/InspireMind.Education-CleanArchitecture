using CleanArchitecture.Application.Contracts.Identity;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Roles.Requests.Commands;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Handlers.Commands;
public sealed class RolesCommandHandler(IRoleService roleService) :
                                          IRequestHandler<AssignRoleToUserCommand, Result<string>>,
                                          IRequestHandler<DeleteRoleCommand, Result<string>>,
                                          IRequestHandler<EditRoleCommand, Result<string>>,
                                          IRequestHandler<CreateRoleCommand, Result<string>>,
                                          IRequestHandler<AssignClaimToUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        AssignRoleToUserCommand request,
        CancellationToken cancellationToken)
    {
        return await roleService.AddRoleToUser(request);
    }

    public async Task<Result<string>> Handle(DeleteRoleCommand request,
                                             CancellationToken cancellationToken)
    {
        return await roleService.DeleteRole(request);
    }

    public async Task<Result<string>> Handle(EditRoleCommand request,
                                             CancellationToken cancellationToken)
    {
        return await roleService.EditRole(request);
    }

    public async Task<Result<string>> Handle(CreateRoleCommand request,
                                             CancellationToken cancellationToken)
    {
        return await roleService.CreateRole(request);
    }

    public async Task<Result<string>> Handle(AssignClaimToUserCommand request,
                                             CancellationToken cancellationToken)
    {
        return await roleService.AddClaimToUser(request);
    }
}

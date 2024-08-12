using CleanArchitecture.Application.Contracts.Identity;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Roles.Requests.Commands;
using MediatR;

namespace InspireMind.Education.Application.Features.Roles.Handlers.Commands;
public sealed class RolesCommandHandler : IRequestHandler<AssignRoleToUserCommand, Result<string>>,
                                          IRequestHandler<DeleteRoleCommand, Result<string>>,
                                          IRequestHandler<EditRoleCommand, Result<string>>,
                                          IRequestHandler<CreateRoleCommand, Result<string>>
{
    private readonly IRoleService _roleService;

    public RolesCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<Result<string>> Handle(
        AssignRoleToUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _roleService.AddRoleToUser(request);
    }

    public async Task<Result<string>> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken)
    {
        return await _roleService.DeleteRole(request);
    }

    public async Task<Result<string>> Handle(
        EditRoleCommand request,
        CancellationToken cancellationToken)
    {
        return await _roleService.EditRole(request);
    }

    public async Task<Result<string>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        return await _roleService.CreateRole(request);
    }
}

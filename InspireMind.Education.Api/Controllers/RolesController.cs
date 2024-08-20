using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Features.Roles.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RolesController(IMediator mediator) : AppControllerBase(mediator)
{
    [HttpPost]
    public async Task<ActionResult<string>> CreateRole(string roleName)
    {
        return CustomResult(await _mediator.Send(new CreateRoleCommand { RoleName = roleName }));
    }

    [HttpPost("assign-role/{userId}")]
    public async Task<ActionResult<string>> AssignRole([FromRoute] string userId, string roleName)
    {
        return CustomResult(await _mediator.Send(new AssignRoleToUserCommand { UserId = userId, RoleName = roleName }));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateRole([FromRoute] string id, string roleName)
    {
        return CustomResult(await _mediator.Send(new EditRoleCommand { RoleId = id, RoleName = roleName }));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> RemoveRole([FromRoute] string id)
    {
        return CustomResult(await _mediator.Send(new DeleteRoleCommand { RoleId = id }));
    }

}

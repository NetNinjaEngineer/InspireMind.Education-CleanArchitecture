using Asp.Versioning;
using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Roles.Requests.Commands;
using InspireMind.Education.Application.Features.Roles.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers
{
    /// <summary>
    /// Manages operations related to user roles, including creating, assigning, updating, and removing roles.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/roles")]
    [ApiController]
    public class RolesController : AppControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator service instance used for handling commands and queries.</param>
        public RolesController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Creates a new role with the specified name.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        /// <returns>A response indicating the success or failure of the role creation.</returns>
        /// <response code="200">Returns a confirmation message indicating the role has been successfully created.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<string>>> CreateRole([FromQuery] string roleName)
        {
            return CustomResult(await _mediator.Send(new CreateRoleCommand { RoleName = roleName }));
        }

        /// <summary>
        /// Assigns a specified role to a user.
        /// </summary>
        /// <param name="userId">The ID of the user to whom the role will be assigned.</param>
        /// <param name="roleName">The name of the role to assign to the user.</param>
        /// <returns>A response indicating the success or failure of the role assignment.</returns>
        /// <response code="200">Returns a confirmation message indicating the role has been successfully assigned to the user.</response>
        [HttpPost("assign-role/{userId}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<string>>> AssignRole([FromRoute] string userId, [FromQuery] string roleName)
        {
            return CustomResult(await _mediator.Send(new AssignRoleToUserCommand { UserId = userId, RoleName = roleName }));
        }

        /// <summary>
        /// Updates an existing role with a new name.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <param name="roleName">The new name for the role.</param>
        /// <returns>A response indicating the success or failure of the role update.</returns>
        /// <response code="200">Returns a confirmation message indicating the role has been successfully updated.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<string>>> UpdateRole([FromRoute] string id, [FromQuery] string roleName)
        {
            return CustomResult(await _mediator.Send(new EditRoleCommand { RoleId = id, RoleName = roleName }));
        }

        /// <summary>
        /// Removes an existing role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to remove.</param>
        /// <returns>A response indicating the success or failure of the role removal.</returns>
        /// <response code="200">Returns a confirmation message indicating the role has been successfully removed.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<string>>> RemoveRole([FromRoute] string id)
        {
            return CustomResult(await _mediator.Send(new DeleteRoleCommand { RoleId = id }));
        }
        /// <summary>
        /// Retrieves a list of all roles.
        /// </summary>
        /// <returns>A result containing an enumerable of role names.</returns>
        /// <response code="200">Returns the list of roles.</response
        [HttpGet]
        [Route("getAllRoles")]
        [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<IEnumerable<string>>>> ListAllRoles()
            => CustomResult(await _mediator.Send(new GetAllRolesQuery()));
        /// <summary>
        /// Retrieves the roles assigned to a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A result containing an enumerable of the user's roles.</returns>
        /// <response code="200">Returns the list of roles assigned to the user.</response>
        [HttpGet]
        [Route("getUserRoles/{userId:guid}")]
        [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<IEnumerable<string>>>> GetUserRoles([FromRoute] Guid userId)
            => CustomResult(await _mediator.Send(new GetUserRolesQuery() { UserId = userId }));

        /// <summary>
        /// Retrieves the claims associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A result containing an enumerable of the user's claims.</returns>
        /// <response code="200">Returns the list of claims assigned to the user.</response>
        [HttpGet]
        [Route("getUserClaims/{userId:guid}")]
        [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<IEnumerable<string>>>> GetUserClaims([FromRoute] Guid userId)
            => CustomResult(await _mediator.Send(new GetUserClaimsQuery() { UserId = userId }));

        /// <summary>
        /// Assigns a claim to a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="claimType">The type of the claim to be assigned.</param>
        /// <param name="claimValue">The value of the claim to be assigned.</param>
        /// <returns>A result containing the status of the operation.</returns>
        /// <response code="200">Returns the status of the claim assignment operation.</response>
        [HttpGet]
        [Route("users/{userId:guid}/assign-claim")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<IEnumerable<string>>>> AssignClaim(
            [FromRoute] Guid userId,
            [FromQuery] string claimType,
            [FromQuery] string claimValue)
            => CustomResult(await _mediator.Send(new AssignClaimToUserCommand()
            {
                UserId = userId,
                ClaimType = claimType,
                ClaimValue = claimValue
            }));
    }
}
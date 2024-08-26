using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Users.DTOs;
using InspireMind.Education.Application.Features.Users.Requests.Commands;
using InspireMind.Education.Application.Features.Users.Requests.Queries;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers
{
    /// <summary>
    /// Manages user-related operations.
    /// </summary>
    /// <remarks>
    /// This controller handles CRUD operations for users, including retrieving, updating, and deleting user data.
    /// </remarks>
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UsersController : AppControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance used for sending commands and queries.</param>
        public UsersController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a paginated list of users.
        /// </summary>
        /// <param name="userParams">Parameters to filter and paginate the user list.</param>
        /// <returns>A paginated response containing a list of users.</returns>
        /// <response code="200">Returns a paginated list of users.</response>
        [HttpGet("paginatedUsers")]
        [ProducesResponseType(typeof(Pagination<UserListDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<UserListDto>>> GetPaginatedUsers(
            [FromQuery] UserRequestParameters userParams)
        {
            var result = await _mediator.Send(new GetPaginatedUsersQuery { UserParameters = userParams });
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a single user by ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The details of the specified user.</returns>
        /// <response code="200">Returns the details of the user.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(Result<UserListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<UserListDto>>> GetSingleUser(Guid userId)
        {
            var result = await _mediator.Send(new GetSingleUserQuery { UserId = userId });
            return CustomResult(result);
        }

        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="updateModel">The model containing updated user information.</param>
        /// <returns>A result indicating the success or failure of the update operation.</returns>
        /// <response code="200">Returns a result indicating the update status.</response>
        /// <response code="404">If the user to be updated is not found.</response>
        [HttpPut("{userId:guid}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<string>>> UpdateUser(Guid userId, UserForUpdateDto updateModel)
        {
            var result = await _mediator.Send(new UpdateUserCommand(userId, updateModel));
            return CustomResult(result);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be deleted.</param>
        /// <returns>A result indicating the success or failure of the delete operation.</returns>
        /// <response code="200">Returns a result indicating the delete status.</response>
        /// <response code="404">If the user to be deleted is not found.</response>
        [HttpDelete("{userId:guid}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<string>>> DeleteUser(Guid userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand { UserId = userId });
            return CustomResult(result);
        }
    }
}

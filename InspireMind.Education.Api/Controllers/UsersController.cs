using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.DTOs.User;
using InspireMind.Education.Application.Features.Users.Requests.Queries;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IMediator mediator) : AppControllerBase(mediator)
    {
        [HttpGet]
        [Route("paginatedUsers")]
        [ProducesResponseType(typeof(Pagination<UserListDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<UserListDto>>> GetPaginatedUsers(
            [FromQuery] UserRequestParameters userParams)
            => Ok(await _mediator.Send(new GetPaginatedUsersQuery { UserParameters = userParams }));

        [HttpGet]
        [Route("{userId:guid}")]
        [ProducesResponseType(typeof(Result<UserListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<UserListDto>>> GetSingleUser(Guid userId)
        {
            return CustomResult(await _mediator.Send(new GetSingleUserQuery { UserId = userId }));
        }
    }
}

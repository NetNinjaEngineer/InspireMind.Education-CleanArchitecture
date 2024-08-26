﻿using InspireMind.Education.Api.Base;
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
    [Authorize]
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

        [HttpPut]
        [Route("{userId:guid}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<string>>> UpdateUser(Guid userId, UserForUpdateDto updateModel)
        {
            return CustomResult(await _mediator.Send(new UpdateUserCommand(userId, updateModel)));
        }

        [HttpDelete]
        [Route("{userId:guid}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<string>>> DeleteUser(Guid userId)
        {
            return CustomResult(await _mediator.Send(new DeleteUserCommand { UserId = userId }));
        }
    }
}

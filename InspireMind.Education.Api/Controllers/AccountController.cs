using Asp.Versioning;
using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Auth.Handlers.Result;
using InspireMind.Education.Application.Features.Auth.Requests.Commands;
using InspireMind.Education.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers
{
    /// <summary>
    /// Manages operations related to authentication, including registration, login, password management, and email confirmation.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account")]
    [ApiController]
    public class AccountController : AppControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator service instance used for handling commands and queries.</param>
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Registers a new user by providing their credentials.
        /// </summary>
        /// <param name="request">The registration request containing the user's details.</param>
        /// <returns>The registration result, including user details or error messages.</returns>
        /// <response code="200">Returns the registration details if successful.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(Result<RegisterResult>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<RegisterResult>>> Register([FromBody] RegisterModel request)
        {
            return CustomResult(await _mediator.Send(new RegisterCommand { RegisterModel = request }));
        }

        /// <summary>
        /// Authenticates an existing user by providing their credentials.
        /// </summary>
        /// <param name="request">The login request containing the user's credentials.</param>
        /// <returns>The login result, including authentication tokens or error messages.</returns>
        /// <response code="200">Returns the login details if authentication is successful.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(Result<LoginResult>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<LoginResult>>> Login([FromBody] LoginModel request)
        {
            return CustomResult(await _mediator.Send(new LoginCommand { LoginModel = request }));
        }

        /// <summary>
        /// Initiates a password reset request by providing the user's email address.
        /// </summary>
        /// <param name="request">The forget password request containing the user's email address.</param>
        /// <returns>The result of the password reset request, including any relevant messages.</returns>
        /// <response code="200">Returns a result indicating whether the password reset request was processed.</response>
        [HttpPost("forget-password")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<string>>> ForgetPassword([FromBody] ForgetPasswordModel request)
        {
            return CustomResult(await _mediator.Send(new ForgetPasswordCommand { ForgetRequest = request }));
        }

        /// <summary>
        /// Resets the user's password by providing their email, reset token, and new password.
        /// </summary>
        /// <param name="email">The email address of the user requesting the password reset.</param>
        /// <param name="token">The reset token received by the user.</param>
        /// <param name="request">The reset password request containing the new password.</param>
        /// <returns>The result of the password reset operation.</returns>
        /// <response code="200">Returns a result indicating whether the password was successfully reset.</response>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<string>>> ResetPassword(
            [FromQuery] string email,
            [FromQuery] string token,
            [FromBody] ResetPasswordModel request)
        {
            var command = new ResetPasswordCommand { Email = email, Token = token, ResetRequest = request };
            return CustomResult(await _mediator.Send(command));
        }

        /// <summary>
        /// Requests to confirm a user's email address by sending a confirmation message to the user's email.
        /// </summary>
        /// <param name="request">The request containing the email address to be confirmed.</param>
        /// <returns>The result of the email confirmation request.</returns>
        /// <response code="200">Returns a result indicating whether the email confirmation request was successful.</response>
        [HttpPost("request-confirm-email")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<string>>> RequestConfirmEmail([FromBody] RequestConfirmEmailModel request)
        {
            return CustomResult(await _mediator.Send(new RequestConfirmEmailCommand { RequestConfirmModel = request }));
        }

        /// <summary>
        /// Confirms the user's email address by providing the email and confirmation token.
        /// </summary>
        /// <param name="email">The email address of the user to be confirmed.</param>
        /// <param name="token">The confirmation token received by the user.</param>
        /// <returns>The result of the email confirmation operation.</returns>
        /// <response code="200">Returns a result indicating whether the email was successfully confirmed.</response>
        [HttpPost("confirm-email")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<string>>> ConfirmEmail(
            [FromQuery] string email,
            [FromQuery] string token)
        {
            return CustomResult(await _mediator.Send(new ConfirmEmailCommand { Email = email, Token = token }));
        }
    }
}

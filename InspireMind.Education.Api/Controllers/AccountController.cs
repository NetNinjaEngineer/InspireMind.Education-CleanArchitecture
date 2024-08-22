using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Features.Authentication.Handlers.Result;
using InspireMind.Education.Application.Features.Authentication.Requests.Commands;
using InspireMind.Education.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers;
[Route("api/account")]
[ApiController]
public class AccountController : AppControllerBase
{
    public AccountController(IMediator mediator) : base(mediator) { }


    [HttpPost("register")]
    public async Task<ActionResult<RegisterResult>> Register(RegisterModel request)
    {
        return CustomResult(await _mediator.Send(new RegisterCommand { RegisterModel = request }));
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login(LoginModel request)
    {
        return CustomResult(await _mediator.Send(new LoginCommand { LoginModel = request }));
    }

    [HttpPost("forget-password")]
    public async Task<ActionResult<string>> ForgetPassword(ForgetPasswordModel request)
    {
        return CustomResult(await _mediator.Send(new ForgetPasswordCommand { ForgetRequest = request }));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromQuery] string email,
        [FromQuery] string token,
        [FromBody] ResetPasswordModel request)
    {
        var result = await _mediator.Send(new ResetPasswordCommand
        {
            Email = email,
            Token = token,
            ResetRequest = request
        });

        return Ok(result);
    }

    [HttpPost("request-confirm-email")]
    public async Task<ActionResult<string>> RequestConfirmEmail(RequestConfirmEmailModel request)
    {
        return CustomResult(await _mediator.Send(new RequestConfirmEmailCommand { RequestConfirmModel = request }));
    }

    [HttpPost("confirm-email")]
    public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
    {
        return CustomResult(await _mediator.Send(new ConfirmEmailCommand { Email = email, Token = token }));
    }


}
